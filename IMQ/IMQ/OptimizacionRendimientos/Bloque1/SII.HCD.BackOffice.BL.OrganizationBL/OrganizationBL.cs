using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using SII.Framework.Common;
using SII.Framework.Entities.Services;
using SII.Framework.ExceptionHandling;
using SII.Framework.Interfaces;
using SII.Framework.Logging.LOPD;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.BackOffice.Services;
using SII.HCD.Common.BL;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Configuration;
using SII.HCD.Misc;
using SII.HCD.Misc.Validators;
using SII.SIFP.Configuration;
using CommonEntities = SII.HCD.Common.Entities;
using TableNames = SII.HCD.BackOffice.Entities.TableNames;
namespace SII.HCD.BackOffice.BL
{
    public class OrganizationBL : IOrganizationService
    {
        #region Consts
        //private const string OrganizationEntityName = "OrganizationEntity";
        //private const string AddressEntityName = "AddressEntity";
        //private const string TelephoneEntityName = "TelephoneEntity";
        //private const string IdentifierEntityName = "IdentifierEntity";
        #endregion

        #region Fields
        private IdentifierTypeBL _identifierTypeBL = null;
        #endregion

        #region DA definition
        private OrganizationDA _organizationDA;

        private OrgIdentifierRelDA _orgIdentifierRelDA;
        private IdentifierTypeDA _identifierTypeDA;

        private OrgTelephoneRelDA _orgTelephoneRelDA;
        private TelephoneDA _telephoneDA;

        private OrganizationCatRelDA _organizationCatRelDA;
        private CategoryDA _categoryDA;

        private AddressDA _addressDA;

        private ElementBL _elementBL;
        #endregion

        #region Properties
        private IdentifierTypeBL IdentifierTypeBL
        {
            get
            {
                if (_identifierTypeBL == null)
                {
                    _identifierTypeBL = new IdentifierTypeBL();
                }
                return _identifierTypeBL;
            }
        }
        #endregion

        #region Constructors
        public OrganizationBL()
        {
            _organizationDA = new OrganizationDA();

            _orgIdentifierRelDA = new OrgIdentifierRelDA();
            _identifierTypeDA = new IdentifierTypeDA();

            _orgTelephoneRelDA = new OrgTelephoneRelDA();
            _telephoneDA = new TelephoneDA();

            _organizationCatRelDA = new OrganizationCatRelDA();
            _categoryDA = new CategoryDA();

            _addressDA = new AddressDA();

            _elementBL = new ElementBL();
        }
        #endregion

        #region Private methods
        #region ValidateEntities
        private void ValidateAddress(AddressEntity address)
        {
            if (address == null) throw new ArgumentNullException("address");

            CommonEntities.ElementEntity _addressMetadata = this.GetElementByName(EntityNames.AddressEntityName);
            AddressHelper addressHelper = new AddressHelper(_addressMetadata);

            ValidationResults result = addressHelper.Validate(address);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_addressValidationError, sb));
            }
        }

        private void ValidateTelephone(TelephoneEntity telephone)
        {
            if (telephone == null) throw new ArgumentNullException("telephone");

            CommonEntities.ElementEntity _telephoneMetada = this.GetElementByName(EntityNames.TelephoneEntityName);
            TelephoneHelper telephoneHelper = new TelephoneHelper(_telephoneMetada);

            ValidationResults result = telephoneHelper.Validate(telephone);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_telephoneValidationError, sb));
            }
        }

        private void ValidateIdentifier(IdentifierEntity identifier)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");

            CommonEntities.ElementEntity _identifierMetada = this.GetElementByName(EntityNames.IdentifierEntityName);
            IdentifierHelper identifierHelper = new IdentifierHelper(_identifierMetada);

            ValidationResults result = identifierHelper.Validate(identifier);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }
                throw new Exception(
                    string.Format(Properties.Resources.ERROR_identifierValidationError, sb));
            }

            if ((identifier.EditStatus.Value == StatusEntityValue.New) || (identifier.EditStatus.Value == StatusEntityValue.Updated))
            {
                if (identifier.IdentifierType.RequiredValidation)
                {
                    Type validatorType = Type.GetType(identifier.IdentifierType.ValidationClass);
                    ICustomValidator<string, string> validator = Activator.CreateInstance(validatorType) as ICustomValidator<string, string>;
                    if (!validator.Validate(identifier.IDNumber))
                    {
                        throw new Exception(String.Format(Properties.Resources.MSG_CannotValidateIdentifier, identifier.IDNumber));
                    }
                }
            }
        }
        #endregion

        private OrganizationEntity Insert(OrganizationEntity organization)
        {
            if (organization == null) throw new ArgumentNullException("organization");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerInsert(organization, userName);

                scope.Complete();
            }

            this.ResetOrganization(organization);
            LOPDLogger.Write(EntityNames.OrganizationEntityName, organization.ID, ActionType.Create);
            return organization;
        }
        #endregion

        #region Protected methods
        protected virtual OrganizationEntity Update(OrganizationEntity organization)
        {
            if (organization == null) throw new ArgumentNullException("organization");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerUpdate(organization, userName, true);

                scope.Complete();
            }

            this.ResetOrganization(organization);
            LOPDLogger.Write(EntityNames.OrganizationEntityName, organization.ID, ActionType.Modify);
            return organization;
        }

        protected virtual CommonEntities.ElementEntity GetElementByName(string entityName)
        {
            try
            {
                CommonEntities.ElementEntity result = _elementBL.GetElementByName(entityName);
                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        protected virtual void ValidateOrganization(OrganizationEntity organization, string configurationEntityName)
        {
            if (organization == null) throw new ArgumentNullException("organization");

            CommonEntities.ElementEntity _organizationMetadata = this.GetElementByName(EntityNames.OrganizationEntityName);
            OrganizationHelper organizationHelper = new OrganizationHelper(_organizationMetadata);

            ValidationResults result = organizationHelper.Validate(organization);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(string.Format(Properties.Resources.ERROR_organizationValidationError, sb));
            }

            if (_organizationMetadata.GetAttribute("Address").Visible)
            {
                if (organization.Address != null)
                {
                    ValidateAddress(organization.Address);
                }
            }

            if (_organizationMetadata.GetAttribute("Address2").Visible)
            {
                if (organization.Address2 != null)
                {
                    ValidateAddress(organization.Address2);
                }
            }

            if (organization.Telephones != null)
            {
                foreach (OrgTelephoneEntity telephone in organization.Telephones)
                {
                    if (telephone.Telephone != null)
                    {
                        ValidateTelephone(telephone.Telephone);
                    }
                }
            }

            CommonEntities.AttributeEntity _organizationIdentifiersAttr = _organizationMetadata.GetAttribute("Identifiers");
            if (_organizationIdentifiersAttr != null
                && _organizationIdentifiersAttr.HasOptions
                && _organizationIdentifiersAttr.AttributeOptions.Any(at => at.Value == configurationEntityName
                                                                    && at.Status == CommonEntities.AttributeStatusEnum.InUse))
            {
                CommonEntities.ElementEntity _orgIdentifierMetadata = this.GetElementByName(EntityNames.OrgIdentifierEntityName);
                CommonEntities.AttributeEntity _orgIdentifierTypeAttr = _orgIdentifierMetadata.GetAttribute("IdentifierType");
                if (_orgIdentifierTypeAttr != null
                    && _orgIdentifierTypeAttr.Required)
                {
                    if (organization.Identifiers == null
                        || !organization.Identifiers.Any(oi => oi.EditStatus.Value != StatusEntityValue.Deleted
                                                            && oi.EditStatus.Value != StatusEntityValue.NewAndDeleted
                                                            && ((!_orgIdentifierTypeAttr.HasOptions && oi.IdentifierType.Name == _orgIdentifierTypeAttr.DefaultValue)
                                                                || (_orgIdentifierTypeAttr.HasOptions && _orgIdentifierTypeAttr.AttributeOptions.Any(opt => opt.Value == oi.IdentifierType.Name)))))
                    {
                        IdentifierTypeEntity[] activeIdentifierTypesFromOrganization = IdentifierTypeBL.GetIdentifierTypes(CategoryTypeEnum.Organization);
                        if (_orgIdentifierTypeAttr.HasOptions)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine();
                            string resultPersonIdentifierMsg = !string.IsNullOrWhiteSpace(_orgIdentifierTypeAttr.DefaultValue)
                                                                ? _orgIdentifierTypeAttr.DefaultValue
                                                                : string.Empty;
                            foreach (CommonEntities.AttributeOptionEntity option in _orgIdentifierTypeAttr.AttributeOptions)
                            {
                                if (option.Value != _orgIdentifierTypeAttr.DefaultValue
                                    && activeIdentifierTypesFromOrganization != null
                                    && activeIdentifierTypesFromOrganization.Any(it => it.Name == option.Value))
                                {
                                    resultPersonIdentifierMsg = resultPersonIdentifierMsg + " " + Properties.Resources.or + " " + option.Value;
                                }
                            }
                            sb.AppendFormat("■ {0}", string.Concat(resultPersonIdentifierMsg, " ", Properties.Resources.MSG_IsRequired));
                            throw new Exception(string.Format(Properties.Resources.ERROR_organizationValidationError, sb));
                        }
                        else if (!string.IsNullOrWhiteSpace(_orgIdentifierTypeAttr.DefaultValue))
                        {
                            if (activeIdentifierTypesFromOrganization != null
                                && activeIdentifierTypesFromOrganization.Any(it => it.Name == _orgIdentifierTypeAttr.DefaultValue))
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine();
                                sb.AppendFormat("■ {0}", string.Concat(_orgIdentifierTypeAttr.DefaultValue, " ", Properties.Resources.MSG_IsRequired));
                                throw new Exception(string.Format(Properties.Resources.ERROR_organizationValidationError, sb));
                            }
                        }
                    }
                }
            }

            if (organization.Identifiers != null)
            {
                foreach (OrgIdentifierEntity identifier in organization.Identifiers)
                {
                    ValidateIdentifier(identifier);
                }
            }
        }

        protected virtual void ResetOrganization(OrganizationEntity organization)
        {
            if (organization.Address != null)
            {
                if (organization.Address.AddressIsNullOrEmpty())
                {
                    organization.Address2.EditStatus.Reset();
                    organization.Address.EditStatus.New();
                }
                else
                {
                    organization.Address.EditStatus.Reset();
                }
            }
            if (organization.Address2 != null)
            {
                if (organization.Address2.AddressIsNullOrEmpty())
                {
                    organization.Address2.EditStatus.Reset();
                    organization.Address2.EditStatus.New();
                }
                else
                {
                    organization.Address2.EditStatus.Reset();
                }
            }
            if (organization.Categories != null)
            {
                List<OrgCategoryEntity> Categories = new List<OrgCategoryEntity>();
                foreach (OrgCategoryEntity category in organization.Categories)
                {
                    if (!((category.EditStatus.Value == StatusEntityValue.Deleted) || (category.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        category.EditStatus.Reset();
                        Categories.Add(category);
                    }
                }
                organization.Categories = Categories.ToArray();
            }
            if (organization.Telephones != null)
            {
                List<OrgTelephoneEntity> Telephones = new List<OrgTelephoneEntity>();
                foreach (OrgTelephoneEntity telephone in organization.Telephones)
                {
                    if (!((telephone.EditStatus.Value == StatusEntityValue.Deleted) || (telephone.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        telephone.EditStatus.Reset();
                        Telephones.Add(telephone);
                    }
                }
                organization.Telephones = Telephones.ToArray();
            }
            if (organization.Identifiers != null)
            {
                List<OrgIdentifierEntity> Identifiers = new List<OrgIdentifierEntity>();
                foreach (OrgIdentifierEntity identifier in organization.Identifiers)
                {
                    if (!((identifier.EditStatus.Value == StatusEntityValue.Deleted) || (identifier.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        identifier.EditStatus.Reset();
                        Identifiers.Add(identifier);
                    }
                }
                organization.Identifiers = Identifiers.ToArray();
            }

            organization.EditStatus.Reset();
        }

        protected virtual OrganizationEntity InnerInsert(OrganizationEntity organization, string userName)
        {
            #region Organization.Address
            if (organization.Address != null)
            {
                switch (organization.Address.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        break;
                    case StatusEntityValue.New:
                        organization.Address.ID = _addressDA.Insert(organization.Address.Address1, organization.Address.Address2, organization.Address.AddressType, organization.Address.City, organization.Address.Country,
                            organization.Address.Province, organization.Address.State, organization.Address.ZipCode, userName);
                        break;
                    case StatusEntityValue.NewAndDeleted:
                        break;
                    case StatusEntityValue.None:
                        break;
                    case StatusEntityValue.Updated:
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Organization.Address2
            if (organization.Address2 != null)
            {
                switch (organization.Address2.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        break;
                    case StatusEntityValue.New:
                        organization.Address2.ID = _addressDA.Insert(organization.Address2.Address1, organization.Address2.Address2, organization.Address2.AddressType, organization.Address2.City, organization.Address2.Country,
                            organization.Address2.Province, organization.Address2.State, organization.Address2.ZipCode, userName);
                        break;
                    case StatusEntityValue.NewAndDeleted:
                        break;
                    case StatusEntityValue.None:
                        break;
                    case StatusEntityValue.Updated:
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Organization
            organization.ID = _organizationDA.Insert(organization.Name, organization.SocialReason, organization.Comments, organization.TelecomAddress,
                (organization.Address != null) ? organization.Address.ID : 0, (organization.Address2 != null) ? organization.Address2.ID : 0, userName, (int)organization.Status);
            #endregion

            #region Categories
            if (organization.Categories != null)
            {
                foreach (OrgCategoryEntity category in organization.Categories)
                {
                    if (category.Category != null)
                    {
                        switch (category.EditStatus.Value)
                        {
                            case SII.Framework.Interfaces.StatusEntityValue.Deleted:
                                _organizationCatRelDA.Delete(category.ID);
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.New:
                                if (category.Category != null)
                                {
                                    _organizationCatRelDA.Insert(organization.ID, category.Category.ID, userName);
                                }
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.NewAndDeleted:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.None:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.Updated:
                                _organizationCatRelDA.Update(category.ID, organization.ID, category.Category.ID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion

            #region Telephones
            if (organization.Telephones != null)
            {
                foreach (OrgTelephoneEntity organizationTelephone in organization.Telephones)
                {
                    if (organizationTelephone.Telephone != null)
                    {
                        switch (organizationTelephone.EditStatus.Value)
                        {
                            case SII.Framework.Interfaces.StatusEntityValue.Deleted:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.New:
                                if (organizationTelephone.Telephone != null)
                                {
                                    organizationTelephone.Telephone.ID = _telephoneDA.Insert(organizationTelephone.Telephone.Telephone, organizationTelephone.Telephone.Comments,
                                        organizationTelephone.Telephone.TelephoneType, organizationTelephone.Telephone.EmergencyContactPhone, userName);
                                    _orgTelephoneRelDA.Insert(organization.ID, organizationTelephone.Telephone.ID, userName);
                                }
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.NewAndDeleted:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.None:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.Updated:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion

            #region Identifiers
            if (organization.Identifiers != null)
            {
                foreach (OrgIdentifierEntity identifier in organization.Identifiers)
                {
                    switch (identifier.EditStatus.Value)
                    {
                        case SII.Framework.Interfaces.StatusEntityValue.Deleted:
                            break;
                        case SII.Framework.Interfaces.StatusEntityValue.New:
                            if (identifier.IdentifierType != null)
                            {
                                _orgIdentifierRelDA.Insert(organization.ID, identifier.IdentifierType.ID, identifier.IDNumber, userName);
                            }
                            break;
                        case SII.Framework.Interfaces.StatusEntityValue.NewAndDeleted:
                            break;
                        case SII.Framework.Interfaces.StatusEntityValue.None:
                            break;
                        case SII.Framework.Interfaces.StatusEntityValue.Updated:
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            organization.DBTimeStamp = _organizationDA.GetDBTimeStamp(organization.ID);

            return organization;
        }

        protected virtual OrganizationEntity InnerUpdate(OrganizationEntity organization, string userName, bool fullOrganizationUpdate)
        {
            Int64 dbTimeStamp = _organizationDA.GetDBTimeStamp(organization.ID);
            if (dbTimeStamp != organization.DBTimeStamp)
                throw new Exception(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, organization.ID));

            if (fullOrganizationUpdate)
            {

                #region Organization.Address
                if (organization.Address != null)
                {
                    switch (organization.Address.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _addressDA.Delete(organization.Address.ID);
                            break;
                        case StatusEntityValue.New:
                            organization.Address.ID = _addressDA.Insert(organization.Address.Address1, organization.Address.Address2, organization.Address.AddressType, organization.Address.City, organization.Address.Country,
                                organization.Address.Province, organization.Address.State, organization.Address.ZipCode, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _addressDA.Update(organization.Address.ID, organization.Address.Address1, organization.Address.Address2, organization.Address.AddressType, organization.Address.City,
                                organization.Address.Country, organization.Address.Province, organization.Address.State, organization.Address.ZipCode, userName);
                            break;
                        default:
                            break;
                    }
                }
                #endregion

                #region Organization.Address2
                if (organization.Address2 != null)
                {
                    switch (organization.Address2.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _addressDA.Delete(organization.Address2.ID);
                            break;
                        case StatusEntityValue.New:
                            organization.Address2.ID = _addressDA.Insert(organization.Address2.Address1, organization.Address2.Address2, organization.Address2.AddressType, organization.Address2.City, organization.Address2.Country,
                                organization.Address2.Province, organization.Address2.State, organization.Address2.ZipCode, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _addressDA.Update(organization.Address2.ID, organization.Address2.Address1, organization.Address2.Address2, organization.Address2.AddressType, organization.Address2.City,
                                organization.Address2.Country, organization.Address2.Province, organization.Address2.State, organization.Address2.ZipCode, userName);
                            break;
                        default:
                            break;
                    }
                }
                #endregion

                #region Organization
                bool result = _organizationDA.Update(organization.ID, organization.Name, organization.SocialReason, organization.Comments, organization.TelecomAddress,
                    (organization.Address != null) ? organization.Address.ID : 0, (organization.Address2 != null) ? organization.Address2.ID : 0, userName, (int)organization.Status) > 0;
                #endregion

                #region Categories
                if (organization.Categories != null)
                {
                    foreach (OrgCategoryEntity category in organization.Categories)
                    {
                        switch (category.EditStatus.Value)
                        {
                            case SII.Framework.Interfaces.StatusEntityValue.Deleted:
                                _organizationCatRelDA.Delete(category.ID);
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.New:
                                if (category.Category != null)
                                {
                                    _organizationCatRelDA.Insert(organization.ID, category.Category.ID, userName);
                                }
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.NewAndDeleted:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.None:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.Updated:
                                _organizationCatRelDA.Update(category.ID, organization.ID, category.Category.ID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion

                #region Telephones
                if (organization.Telephones != null)
                {
                    foreach (OrgTelephoneEntity organizationTelephone in organization.Telephones)
                    {
                        switch (organizationTelephone.EditStatus.Value)
                        {
                            case SII.Framework.Interfaces.StatusEntityValue.Deleted:
                                if (organizationTelephone.Telephone != null)
                                {
                                    _telephoneDA.Delete(organizationTelephone.Telephone.ID);
                                    _orgTelephoneRelDA.Delete(organizationTelephone.ID);
                                }
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.New:
                                if (organizationTelephone.Telephone != null)
                                {
                                    organizationTelephone.Telephone.ID = _telephoneDA.Insert(organizationTelephone.Telephone.Telephone, organizationTelephone.Telephone.Comments,
                                        organizationTelephone.Telephone.TelephoneType, organizationTelephone.Telephone.EmergencyContactPhone, userName);
                                    _orgTelephoneRelDA.Insert(organization.ID, organizationTelephone.Telephone.ID, userName);
                                }
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.NewAndDeleted:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.None:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.Updated:
                                if (organizationTelephone.Telephone != null)
                                {
                                    _orgTelephoneRelDA.Update(organizationTelephone.ID, organization.ID, organizationTelephone.Telephone.ID, userName);
                                    _telephoneDA.Update(organizationTelephone.Telephone.ID, organizationTelephone.Telephone.Telephone, organizationTelephone.Telephone.Comments,
                                    organizationTelephone.Telephone.TelephoneType, organizationTelephone.Telephone.EmergencyContactPhone, userName);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion

                #region Identifiers
                if (organization.Identifiers != null)
                {
                    foreach (OrgIdentifierEntity identifier in organization.Identifiers)
                    {
                        switch (identifier.EditStatus.Value)
                        {
                            case SII.Framework.Interfaces.StatusEntityValue.Deleted:
                                _orgIdentifierRelDA.Delete(identifier.ID);
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.New:
                                if (identifier.IdentifierType != null)
                                {
                                    _orgIdentifierRelDA.Insert(organization.ID, identifier.IdentifierType.ID, identifier.IDNumber, userName);
                                }
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.NewAndDeleted:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.None:
                                break;
                            case SII.Framework.Interfaces.StatusEntityValue.Updated:
                                if (identifier.IdentifierType != null)
                                {
                                    _orgIdentifierRelDA.Update(identifier.ID, organization.ID, identifier.IdentifierType.ID, identifier.IDNumber, userName);
                                    identifier.EditStatus.Reset();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion
            }
            else
            {
                _organizationDA.Update(organization.ID, userName);
            }

            organization.DBTimeStamp = _organizationDA.GetDBTimeStamp(organization.ID);

            #region Update Related Caches
            _organizationDA.MarkUpdatedRelatedProcessChart(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromAdmissionConfigAssistanceCoverGroupRel(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromReceptionConfigAssistanceCoverGroupRel(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromAdmissionConfigAssistanceCoverGroupRelInsurerCoverAgreement(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromReceptionConfigAssistanceCoverGroupRelInsurerCoverAgreement(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromTransferConfigTransferProcessRel(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromInvoiceConfigInvoiceConfigInvoiceAgreeRelInvoiceConfigAgreementInsurer(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromInvoiceConfigInvoiceConfigInvoiceAgreeRelInvoiceConfigAgreementCareCenter(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRemittanceRelInvoiceAgreeRemittanceConfigInsurer(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRemittanceRelInvoiceAgreeRemittanceConfigCareCenter(organization.ID, userName);
            //_organizationDA.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRemittanceRelInvoiceAgreeRemittanceConfigBankOrganization(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigExportElementRelExportElementConfigRelInsurer(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigExportElementRelExportElementConfigRelCareCenter(organization.ID, userName);
            _organizationDA.MarkRelatedUpdatedItemsFromSupplierItemRelationshipSupplier(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedRoutineFromRoutineDietaryServiceRelCareCenter(organization.ID, userName);

            _organizationDA.MarkUpdatedRelatedPhysician(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedPhysicianFromPersonAvailPatternCareCenter(organization.ID, userName);
            _organizationDA.MarkUpdatedRelatedPhysicianFromPersonCareCenterAccessCareCenter(organization.ID, userName);
            #endregion

            return organization;
        }

        #region CheckPreconditions
        protected virtual void CheckInsertPreconditions(OrganizationEntity organization)
        {
            if (organization == null) throw new ArgumentNullException("organization");

            ValidateOrganization(organization, EntityNames.OrganizationEntityName);

            OrganizationFindRequest organizationFind = new OrganizationFindRequest();
            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            if (backOfficeConfig.EntitySettings.OrganizationEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.OrganizationEntity.Attributes)
                {
                    if ((attrib.Name == "Name") && (attrib.Mandatory))
                    {
                        organizationFind.Name = organization.Name;
                        organizationFind.MandatoryName = true;
                    }
                }
            }

            int id = _organizationDA.GetOrganization(organizationFind);
            if (id > 0)
            {
                throw new Exception(string.Format(Properties.Resources.MSG_OrganizationAlreadyExists, organization.Name));
            }
        }

        private void CheckUpdatePreconditions(OrganizationEntity organization)
        {
            if (organization == null) throw new ArgumentNullException("organization");

            ValidateOrganization(organization, EntityNames.OrganizationEntityName);

            OrganizationFindRequest organizationFind = new OrganizationFindRequest();

            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            if (backOfficeConfig != null)
            {
                if (backOfficeConfig.EntitySettings.OrganizationEntity.Attributes != null)
                {
                    foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.OrganizationEntity.Attributes)
                    {
                        if ((attrib.Name == "Name") && (attrib.Mandatory))
                        {
                            organizationFind.Name = organization.Name;
                            organizationFind.MandatoryName = true;
                        }
                    }
                }
            }

            int id = _organizationDA.GetOrganization(organizationFind);
            if ((id > 0) && (id != organization.ID))
            {
                throw new Exception(string.Format(Properties.Resources.MSG_OrganizationAlreadyExists, organization.Name));
            }
        }
        #endregion
        #endregion

        #region IOrganizationService members
        public OrganizationEntity Save(OrganizationEntity organization)
        {
            try
            {
                if (organization == null)
                    throw new ArgumentNullException("organization");

                switch (organization.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return organization;
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(organization);
                        return Insert(organization);
                    case StatusEntityValue.NewAndDeleted:
                        return organization;
                    case StatusEntityValue.None:
                        CheckUpdatePreconditions(organization);
                        return organization;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(organization);
                        return Update(organization);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationListDTO[] GetOrganizations(DateTime fromDate, DateTime toDate, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.OrganizationEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                OrganizationListDTOAdapter organizationListDTOAdapter = new OrganizationListDTOAdapter();

                DataSet ds = _organizationDA.GetOrganizations(fromDate, toDate);
                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationListDTOTable)))
                {
                    OrganizationListDTO[] organizations = organizationListDTOAdapter.GetData(ds);
                    if (organizations != null)
                    {
                        maxRecordsExceded = (organizations.Length >= maxRows);
                    }
                    return organizations;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationAddressListDTO[] GetOrganizations(string name, string socialReason, int identifierTypeID, string idNumber, int categoryID, int profileID,
            out Boolean maxRecordsExceded, OrganizationFindTypeEnum findType, int customerID)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.OrganizationEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                OrganizationAddressListDTOAdapter organizationAddressListDTOAdapter = new OrganizationAddressListDTOAdapter();

                DataSet ds = null;
                CategoryAdapter myCategoryAdapter = new CategoryAdapter();
                CategoryOrganizationKeyEnum myCategoryKeyEnum = CategoryOrganizationKeyEnum.None;
                ds = _categoryDA.GetCategoryByID(categoryID);
                if ((ds != null) && (ds.Tables != null) && ds.Tables.Contains(TableNames.CategoryTable) && (ds.Tables[TableNames.CategoryTable].Rows.Count > 0))
                {
                    CategoryEntity myCategory = myCategoryAdapter.GetInfo(ds.Tables[TableNames.CategoryTable].Rows[0], ds);
                    myCategoryKeyEnum = (CategoryOrganizationKeyEnum)myCategory.CategoryKey;
                }

                switch (findType)
                {
                    case OrganizationFindTypeEnum.ContactOrganization:
                        {
                            ds = _organizationDA.GetOrganizationsExcludeCCO(name, socialReason, identifierTypeID, idNumber, categoryID, maxRows, customerID);
                        }
                        break;
                    case OrganizationFindTypeEnum.CareCenter:
                        {
                            ds = _organizationDA.GetOrganizationsExcludeCareCenters(name, socialReason, identifierTypeID, idNumber, categoryID, maxRows);
                        }
                        break;
                    case OrganizationFindTypeEnum.Insurer:
                        {
                            ds = _organizationDA.GetOrganizationsExcludeInsurers(name, socialReason, identifierTypeID, idNumber, categoryID, maxRows);
                        }
                        break;
                    case OrganizationFindTypeEnum.Supplier:
                        {
                            ds = _organizationDA.GetOrganizationsExcludeSuppliers(name, socialReason, identifierTypeID, idNumber, categoryID, maxRows);
                        }
                        break;
                    case OrganizationFindTypeEnum.Buyer:
                        {
                            ds = _organizationDA.GetOrganizationsExcludeBuyers(name, socialReason, identifierTypeID, idNumber, categoryID, maxRows);
                        }
                        break;
                    default:
                        break;
                }

                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationAddressListDTOTable)))
                {
                    OrganizationAddressListDTO[] organizations = organizationAddressListDTOAdapter.GetData(ds);
                    if (organizations != null)
                    {
                        maxRecordsExceded = (organizations.Length >= maxRows);
                    }
                    return organizations;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        public OrganizationEntity GetOrganization(int organizationID)
        {
            try
            {
                OrganizationAdapter organizationAdapter = new OrganizationAdapter();

                #region Organization
                DataSet ds = _organizationDA.GetOrganization(organizationID);
                #endregion


                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationTable)) && (ds.Tables[TableNames.OrganizationTable].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());

                    OrganizationEntity result = organizationAdapter.GetInfo(ds.Tables[TableNames.OrganizationTable].Rows[0], ds2);
                    LOPDLogger.Write(EntityNames.OrganizationEntityName, organizationID, ActionType.View);
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        /*
        public OrganizationEntity GetOrganization(int organizationID)
        {
            try
            {
                OrganizationAdapter organizationAdapter = new OrganizationAdapter();

                #region Organization
                DataSet ds = _organizationDA.GetOrganization(organizationID);
                #endregion

                #region Organization Telephones
                DataSet ds2 = _orgTelephoneRelDA.GetOrganizationTelephone(organizationID);
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.OrganizationTelephoneTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.OrganizationTelephoneTable].Copy();
                    ds.Tables.Add(dt);
                }

                ds2 = _telephoneDA.GetTelephoneFromOrganization(organizationID);
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.TelephoneTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.TelephoneTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion

                #region Address1
                ds2 = _addressDA.GetAddress(SIIConvert.ToInteger(ds.Tables[TableNames.OrganizationTable].Rows[0]["AddressID"].ToString(), 0));
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.AddressTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.AddressTable].Copy();
                    dt.TableName = TableNames.AddressTable;
                    ds.Tables.Add(dt);
                }
                #endregion

                #region Address2
                ds2 = _addressDA.GetAddress(SIIConvert.ToInteger(ds.Tables[TableNames.OrganizationTable].Rows[0]["Address2ID"].ToString(), 0));
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.AddressTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.AddressTable].Copy();
                    dt.TableName = TableNames.Address2Table;
                    ds.Tables.Add(dt);
                }
                #endregion

                #region Categories
                ds2 = _organizationCatRelDA.GetOrganizationCategory(organizationID);
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.OrganizationCategoryTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.OrganizationCategoryTable].Copy();
                    dt.TableName = TableNames.OrganizationCategoryTable;
                    ds.Tables.Add(dt);
                }
                ds2 = _categoryDA.GetCategoryFromOrganization(organizationID);
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.CategoryTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.CategoryTable].Copy();
                    dt.TableName = TableNames.CategoryTable;
                    ds.Tables.Add(dt);
                }
                #endregion

                #region contact Persons
                //TODO: a la espera de que se haga...
                #endregion

                #region Identifiers
                ds2 = _orgIdentifierRelDA.GetOrganizationIdentifier(organizationID);
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.IdentifierTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.IdentifierTable].Copy();
                    dt.TableName = TableNames.IdentifierTable;
                    ds.Tables.Add(dt);
                }
                ds2 = _identifierTypeDA.GetIdentifierFromOrganization(organizationID);
                if ((ds2 != null) && (ds2.Tables.Contains(TableNames.IdentifierTypeTable)))
                {
                    DataTable dt = ds2.Tables[TableNames.IdentifierTypeTable].Copy();
                    dt.TableName = TableNames.IdentifierTypeTable;
                    ds.Tables.Add(dt);
                }
                #endregion

                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationTable)) && (ds.Tables[TableNames.OrganizationTable].Rows.Count > 0))
                {
                    OrganizationEntity result = organizationAdapter.GetInfo(ds.Tables[TableNames.OrganizationTable].Rows[0], ds);
                    LOPDLogger.Write(EntityNames.OrganizationEntityName, organizationID, ActionType.View);
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        */
        public OrganizationEntity GetSimpleOrganization(int organizationID)
        {
            try
            {
                OrganizationAdapter organizationAdapter = new OrganizationAdapter();

                #region Organization
                DataSet ds = _organizationDA.GetOrganization(organizationID);
                #endregion

                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationTable)) && (ds.Tables[TableNames.OrganizationTable].Rows.Count > 0))
                {
                    OrganizationEntity result = organizationAdapter.GetInfo(ds.Tables[TableNames.OrganizationTable].Rows[0], ds);
                    LOPDLogger.Write(EntityNames.OrganizationEntityName, organizationID, ActionType.View);
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CategoryProfileDTO[] GetOrganizationCategoryProfile(int organizationID)
        {
            try
            {
                CategoryProfileDTOAdapter categoryProfileDTOAdapter = new CategoryProfileDTOAdapter();

                DataSet ds = _organizationCatRelDA.GetOrganizationCategoryProfile(organizationID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(TableNames.CategoryProfileTable))
                    && (ds.Tables[TableNames.CategoryProfileTable].Rows.Count > 0))
                {
                    return categoryProfileDTOAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationBaseEntity[] GetAllOrganizationBase()
        {
            try
            {
                DataSet ds = _organizationDA.GetAllOrganization();
                ds.Tables[TableNames.OrganizationTable].TableName = TableNames.OrganizationBaseTable;
                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationBaseTable)) && (ds.Tables[TableNames.OrganizationBaseTable].Rows.Count > 0))
                {
                    OrganizationBaseAdapter organizationBaseAdapter = new OrganizationBaseAdapter();
                    return organizationBaseAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationBaseEntity[] GetAllCareCenterOrganizationBase()
        {
            try
            {
                DataSet ds = _organizationDA.GetAllCareCenterOrganization();
                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationBaseTable)) && (ds.Tables[TableNames.OrganizationBaseTable].Rows.Count > 0))
                {
                    OrganizationBaseAdapter organizationBaseAdapter = new OrganizationBaseAdapter();
                    return organizationBaseAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationBaseEntity[] GetOrganizationBaseUsedInLocationAsCareCenter()
        {
            try
            {
                DataSet ds = _organizationDA.GetOrganizationBaseUsedInLocationAsCareCenter();
                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationBaseTable)) && (ds.Tables[TableNames.OrganizationBaseTable].Rows.Count > 0))
                {
                    OrganizationBaseAdapter organizationBaseAdapter = new OrganizationBaseAdapter();
                    return organizationBaseAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationBaseEntity[] GetOrganizationBaseWithCustomer()
        {
            try
            {
                OrganizationBaseAdapter organizationBaseAdapter = new OrganizationBaseAdapter();
                DataSet ds = _organizationDA.GetOrganizationWithCustomer();
                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationBaseTable)) && (ds.Tables[TableNames.OrganizationBaseTable].Rows.Count > 0))
                {
                    return organizationBaseAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationBaseEntity[] GetOrganizationBaseWithPhysician()
        {
            try
            {
                OrganizationBaseAdapter organizationBaseAdapter = new OrganizationBaseAdapter();
                DataSet ds = _organizationDA.GetOrganizationWithPhysician();
                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationBaseTable)) && (ds.Tables[TableNames.OrganizationBaseTable].Rows.Count > 0))
                {
                    return organizationBaseAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationListDTO[] GetRelatedOrganizationByInvoicePrintRule(int invoicePrintRuleID)
        {
            try
            {
                OrganizationListDTOAdapter organizationListDTOAdapter = new OrganizationListDTOAdapter();
                DataSet ds = _organizationDA.GetRelatedOrganizationByInvoicePrintRule(invoicePrintRuleID);
                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.OrganizationListDTOTable)) && (ds.Tables[TableNames.OrganizationListDTOTable].Rows.Count > 0))
                {
                    return organizationListDTOAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public Int64 GetOrganizationDBTimeStamp(int organizationID)
        {
            try
            {
                return _organizationDA.GetDBTimeStamp(organizationID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public CommonEntities.IDDescriptionEntity[] GetIDDescriptionByCategory(string name, int categoryID)
        {
            try
            {
                if (categoryID > 0)
                {
                    CommonEntities.IDDescriptionAdapter idDescriptionAdapter = new CommonEntities.IDDescriptionAdapter();
                    DataSet ds = _organizationDA.GetIDDescriptionByCategory(name, categoryID);
                    if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                    {
                        CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                        return persons;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        /// <summary>
        /// DEVUELVE EN EL ID EL CARECENTERID Y EN EL MASTERID EL ORGANIZATIONID
        /// </summary>
        /// <returns></returns>
        public CommonEntities.IDDescriptionWithMasterIDEntity[] GetReferringCareCenters()
        {
            try
            {

                CommonEntities.IDDescriptionWithMasterIDAdapter idDescriptionAdapter = new CommonEntities.IDDescriptionWithMasterIDAdapter();
                DataSet ds = _organizationDA.GetIDDescriptionWithMasterID(CategoryOrganizationKeyEnum.RefHealthCenter);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionWithMasterIDTable)))
                {
                    return idDescriptionAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public OrganizationEntity GetParentOrganizationFromLocation(int locationID)
        {
            try
            {
                int organizationID = _organizationDA.GetParentOrganizationIDFromLocation(locationID, CommonEntities.StatusEnum.Active);
                if (organizationID > 0)
                    return GetOrganization(organizationID);

                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        #endregion
    }
}
