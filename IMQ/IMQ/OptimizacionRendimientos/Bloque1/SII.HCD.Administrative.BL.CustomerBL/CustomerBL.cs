using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using SII.Framework.Common;
using SII.Framework.Entities.Services;
using SII.Framework.ExceptionHandling;
using SII.Framework.Interfaces;
using SII.Framework.Logging;
using SII.Framework.Logging.LOPD;
using SII.HCD.Addin.Entities;
using SII.HCD.Addin.Host.View;
using SII.HCD.Administrative.DA;
using SII.HCD.Administrative.Entities;
using SII.HCD.Administrative.Services;
using SII.HCD.Assistance.DA;
using SII.HCD.Assistance.Entities;
using SII.HCD.BackOffice.BL;
using SII.HCD.BackOffice.BL.CodeProvider;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Common.BL;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Common.Services;
using SII.HCD.Configuration;
using SII.HCD.Misc;
using SII.HCD.Sync.SA.SATIPServiceProxy;
using SII.SIFP.Configuration;
using CommonEntities = SII.HCD.Common.Entities;
using System.Collections;
using System.Threading.Tasks;

namespace SII.HCD.Administrative.BL
{
    public class CustomerBL : PersonBL, ICustomerService
    {
        #region Consts
        //private const string CustomerEntityName = "CustomerEntity";
        private const string ROOMTELEPHONEDEVICETYPE = "ROOMTELEPHONE";
        #endregion

        #region fileds definition
        private CustomerDataAccess _dataAccess = null;
        private CustomerHelpers _helpers = null;

        private ValidationResults _validationResults = null;

        private NOKBL _nokBL;
        private CustomerContactPersonBL _customerContactPersonBL;
        private CustomerContactOrganizationBL _customerContactOrganizationBL;
        private DBMaskedCodeGeneratorBL _dbMaskedCodeGeneratorBL;
        private CustomerEpisodeBL _customerEpisodeBL = null;
        private ObservationTemplateBL _observationTemplateBL = null;

        private CommonEntities.ElementEntity _timePatternDTOMetadata;

        private Dictionary<string, object> _entities = null;
        private Dictionary<string, object> _bls = null;
        private HL7MessagingProcessor _hl7processor = null;
        private ElementBL _elementBL = null;
        private CustomerAccountDA _customerAccountDA = null;

        private List<string> alergiasIndigo = new List<string>();
        private List<string> antecedecntesPaciente = new List<string>();
        #endregion

        #region Properties
        public CustomerDataAccess DataAccess
        {
            get
            {
                if (_dataAccess == null)
                    InitializeDataAccess();
                return _dataAccess;
            }
        }

        private CustomerHelpers Helpers
        {
            get
            {
                if (_helpers == null)
                    InitializeHelpers();
                return _helpers;
            }
        }



        private Dictionary<string, object> Entities
        {
            get
            {
                if (_entities == null) _entities = new Dictionary<string, object>();
                return _entities;
            }
        }
        private Dictionary<string, object> BLs
        {
            get
            {
                if (_bls == null) _bls = new Dictionary<string, object>();
                return _bls;
            }
        }
        private HL7MessagingProcessor HL7MessagingProcessor
        {
            get
            {
                if (_hl7processor == null) _hl7processor = new HL7MessagingProcessor(BLs, Entities);
                return _hl7processor;
            }
        }
        public CommonEntities.ElementEntity TimePatternDTOMetadata
        {
            get
            {
                if (_timePatternDTOMetadata == null)
                    _timePatternDTOMetadata = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.TimePatternDTOName);
                return _timePatternDTOMetadata;
            }
        }

        private ElementBL ElementBL
        {
            get
            {
                if (_elementBL == null)
                {
                    _elementBL = new ElementBL();
                    if (!BLs.ContainsKey("ElementBL"))
                        BLs.Add("ElementBL", _elementBL);
                }
                return _elementBL;
            }
        }
        private CustomerEpisodeBL CustomerEpisodeBL
        {
            get
            {
                if (_customerEpisodeBL == null) _customerEpisodeBL = new CustomerEpisodeBL();
                return _customerEpisodeBL;
            }
        }
        private ObservationTemplateBL ObservationTemplateBL
        {
            get
            {
                if (_observationTemplateBL == null) _observationTemplateBL = new ObservationTemplateBL();
                return _observationTemplateBL;
            }
        }
        private NOKBL NOKBL
        {
            get
            {
                if (_nokBL == null) _nokBL = new NOKBL();
                return _nokBL;
            }
        }
        private CustomerContactPersonBL CustomerContactPersonBL
        {
            get
            {
                if (_customerContactPersonBL == null) _customerContactPersonBL = new CustomerContactPersonBL();
                return _customerContactPersonBL;
            }
        }
        private CustomerContactOrganizationBL CustomerContactOrganizationBL
        {
            get
            {
                if (_customerContactOrganizationBL == null) _customerContactOrganizationBL = new CustomerContactOrganizationBL();
                return _customerContactOrganizationBL;
            }
        }
        private DBMaskedCodeGeneratorBL DBMaskedCodeGeneratorBL
        {
            get
            {
                if (_dbMaskedCodeGeneratorBL == null) _dbMaskedCodeGeneratorBL = new DBMaskedCodeGeneratorBL();
                return _dbMaskedCodeGeneratorBL;
            }
        }

        #endregion

        #region Constructors
        public CustomerBL()
        {
        }
        #endregion

        #region private methods
        private void InitializeDataAccess()
        {
            _dataAccess = new CustomerDataAccess
            {
                CustomerDA = new CustomerDA(),
                PersonDA = new PersonDA(),
                PersonCatRelDA = new PersonCatRelDA(),
                CategoryDA = new CategoryDA(),
                OrganizationDA = new OrganizationDA(),
                OrganizationCatRelDA = new OrganizationCatRelDA(),

                ProfileDA = new ProfileDA(),
                CustomerClassificationDA = new CustomerClassificationDA(),
                CustomerAdmissionDA = new CustomerAdmissionDA(),

                NOKDA = new NOKDA(),
                CustomerContactPersonDA = new CustomerContactPersonDA(),
                CustomerContactOrganizationDA = new CustomerContactOrganizationDA(),
                LocationDA = new LocationDA(),

                ProcedureActDA = new ProcedureActDA(),
                RoutineActDA = new RoutineActDA(),
                RoutineActResourceRelDA = new RoutineActResourceRelDA(),
                RoutineStepActDA = new RoutineStepActDA(),
                RoutineActHumanResourceRelDA = new RoutineActHumanResourceRelDA(),
                RoutineActEquipmentRelDA = new RoutineActEquipmentRelDA(),
                RoutineActActRelDA = new RoutineActActRelDA(),
                RoutineActBodySiteRelDA = new RoutineActBodySiteRelDA(),

                ProcedureActResourceRelDA = new ProcedureActResourceRelDA(),
                ProcedureActHumanResourceRelDA = new ProcedureActHumanResourceRelDA(),
                ProcedureActEquipmentRelDA = new ProcedureActEquipmentRelDA(),
                ProcedureActActRelDA = new ProcedureActActRelDA(),
                OrderRequestConsentRelDA = new OrderRequestConsentRelDA(),
                ConsentPreprintDA = new ConsentPreprintDA(),
                ConsentTypeDA = new ConsentTypeDA(),

                CustomerRelatedCHNumberDA = new CustomerRelatedCHNumberDA(),
                CareCenterRelatedCodeGeneratorDA = new CareCenterRelatedCodeGeneratorDA(),

                CustomerProcessDA = new CustomerProcessDA(),
                CustomerAccountDA = new CustomerAccountDA(),
                CustomerObservationDA = new CustomerObservationDA(),

                //merge
                PersonProcessMergedDA = new PersonProcessMergedDA(),
                CustomerProcessMergedDA = new CustomerProcessMergedDA(),
                //personmerge
                RelatedContactPersonMergedDA = new RelatedContactPersonMergedDA(),
                RelatedHumanResourceMergedDA = new RelatedHumanResourceMergedDA(),
                RelatedPhysicianMergedDA = new RelatedPhysicianMergedDA(),
                RelatedNOKMergedDA = new RelatedNOKMergedDA(),
                RelatedOrganizationContactPersonMergedDA = new RelatedOrganizationContactPersonMergedDA(),
                RelatedPersonReplacementMergedDA = new RelatedPersonReplacementMergedDA(),
                //customermerge
                RelatedCustomerProcessMergedDA = new RelatedCustomerProcessMergedDA(),
                RelatedCustomerMedProcessMergedDA = new RelatedCustomerMedProcessMergedDA(),
                RelatedCustomerOrderReqMergedDA = new RelatedCustomerOrderReqMergedDA(),
                RelatedCustomerObsMergedDA = new RelatedCustomerObsMergedDA(),
                RelatedCustomerPolicyMergedDA = new RelatedCustomerPolicyMergedDA(),
                RelatedCustomerCardMergedDA = new RelatedCustomerCardMergedDA(),
                RelatedCustomerAccountChargeMergedDA = new RelatedCustomerAccountChargeMergedDA(),
                RelatedCustomerNotifMergedDA = new RelatedCustomerNotifMergedDA(),
                RelatedCustomerNOKMergedDA = new RelatedCustomerNOKMergedDA(),
                RelatedCustomerContactPersonMergedDA = new RelatedCustomerContactPersonMergedDA(),
                RelatedCustomerContactOrgMergedDA = new RelatedCustomerContactOrgMergedDA(),
                RelatedBatchMovementMergedDA = new RelatedBatchMovementMergedDA(),
                RelatedCustomerCHNumberMergedDA = new RelatedCustomerCHNumberMergedDA(),


            };
        }

        private void InitializeHelpers()
        {
            _helpers = new CustomerHelpers
            {
                PersonHelper = new PersonHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.PersonEntityName, true)),
                CustomerHelper = new CustomerHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.CustomerEntityName, true)),
                //merge
                PersonProcessMergedHelper = new PersonProcessMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.PersonProcessMergedEntityName, true)),
                CustomerProcessMergedHelper = new CustomerProcessMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.CustomerProcessMergedEntityName, true)),
                //personmerge
                RelatedContactPersonMergedHelper = new RelatedContactPersonMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedContactPersonMergedEntityName, true)),
                RelatedHumanResourceMergedHelper = new RelatedHumanResourceMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedHumanResourceMergedEntityName, true)),
                RelatedPhysicianMergedHelper = new RelatedPhysicianMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedPhysicianMergedEntityName, true)),
                RelatedNOKMergedHelper = new RelatedNOKMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedNOKMergedEntityName, true)),
                RelatedOrganizationContactPersonMergedHelper = new RelatedOrganizationContactPersonMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedOrganizationContactPersonMergedEntityName, true)),
                RelatedPersonReplacementMergedHelper = new RelatedPersonReplacementMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedPersonReplacementMergedEntityName, true)),
                //customermerge
                RelatedCustomerProcessMergedHelper = new RelatedCustomerProcessMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerProcessMergedEntityName, true)),
                RelatedCustomerMedProcessMergedHelper = new RelatedCustomerMedProcessMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerMedProcessMergedEntityName, true)),
                RelatedCustomerOrderReqMergedHelper = new RelatedCustomerOrderReqMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerOrderReqMergedEntityName, true)),
                RelatedCustomerObsMergedHelper = new RelatedCustomerObsMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerObsMergedEntityName, true)),
                RelatedCustomerPolicyMergedHelper = new RelatedCustomerPolicyMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerPolicyMergedEntityName, true)),
                RelatedCustomerCardMergedHelper = new RelatedCustomerCardMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerCardMergedEntityName, true)),
                RelatedCustomerAccountChargeMergedHelper = new RelatedCustomerAccountChargeMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerAccountChargeMergedEntityName, true)),
                RelatedCustomerNotifMergedHelper = new RelatedCustomerNotifMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerNotifMergedEntityName, true)),
                RelatedCustomerNOKMergedHelper = new RelatedCustomerNOKMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerNOKMergedEntityName, true)),
                RelatedCustomerContactPersonMergedHelper = new RelatedCustomerContactPersonMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerContactPersonMergedEntityName, true)),
                RelatedCustomerContactOrgMergedHelper = new RelatedCustomerContactOrgMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerContactOrgMergedEntityName, true)),
                RelatedBatchMovementMergedHelper = new RelatedBatchMovementMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedBatchMovementMergedEntityName, true)),
                RelatedCustomerCHNumberMergedHelper = new RelatedCustomerCHNumberMergedHelper(ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.RelatedCustomerCHNumberMergedEntityName, true)),

            };
        }

        private string GetDefaultIdentifierType()
        {
            CommonEntities.ElementEntity personIdentifierType = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.PersonIdentifierEntityName);
            CommonEntities.AttributeEntity identifierType = (personIdentifierType != null) ? personIdentifierType.GetAttribute("IdentifierType") : null;
            return (identifierType != null)
                ? identifierType.DefaultValue
                : string.Empty;

        }

        private string[] GetTelephoneDeviceCodes(int customerID)
        {
            List<string> result = new List<string>();
            DataSet ds = DataAccess.CustomerDA.GetCustomerDevices(customerID);
            if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DeviceTable))
                && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.DeviceTable].Rows.Count > 0))
            {
                DataRowCollection rows = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.DeviceTable].Rows;
                foreach (DataRow row in rows)
                {
                    string deviceTypeName = row["DeviceTypeName"].ToString();
                    if (string.Compare(deviceTypeName, ROOMTELEPHONEDEVICETYPE, true) == 0)
                    {
                        result.Add(row["Code"].ToString());
                    }
                }
            }

            return result.ToArray();
        }

        private CustomerLookupHostView GetAddinInstanceCustomerLookup(string name)
        {
            CustomerLookupHostView result = AddInRepository.GetAddIn<CustomerLookupHostView>(name);
            if (result == null)
                throw new NullReferenceException(string.Format(
                    Properties.Resources.ERROR_CustomerLookupProviderAddinNotFound, name));

            return result;
        }

        private RegistrationStepHostView GetAddinInstanceRegistrationStep(string name)
        {
            RegistrationStepHostView result = AddInRepository.GetAddIn<RegistrationStepHostView>(name);
            if (result == null)
                throw new NullReferenceException(string.Format(
                    Properties.Resources.ERROR_RegistrationStepProviderAddinNotFound, name));

            return result;
        }

        private CustomerEntity Insert(CustomerEntity customer, ElementBL elementBL)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            string userName = IdentityUser.GetIdentityUserName();
            int categoryID = DataAccess.CategoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.Customer);
            if (categoryID <= 0)
            {
                throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForCustomers);
            }

            CommonEntities.ElementEntity _customerMetadata = base.GetElementByName(EntityNames.CustomerEntityName, elementBL);
            string customerIDNumber = _customerMetadata.GetCodeGeneratorName("IdentificationNumber"); ;
            string customerCHNumber = String.Empty;
            int chNumberAttributeID = 0;
            if ((_customerMetadata != null) && (_customerMetadata.Attributes != null))
            {
                CommonEntities.AttributeEntity chNumberAttribute = _customerMetadata.GetAttribute("CHNumber");
                chNumberAttributeID = chNumberAttribute.ID;
                customerCHNumber = chNumberAttribute.CodeGenerator;
            }

            List<Tuple<int, string>> careCentersWitCodeGenerator = GetCareCentersWithCodeGenerator(_customerMetadata, chNumberAttributeID);

            using (TransactionScope scope = new TransactionScope())
            {
                customer = this.InnerInsert(customer, userName, categoryID, customerIDNumber, customerCHNumber, careCentersWitCodeGenerator);
                scope.Complete();
            }

            this.ResetCustomer(customer);
            LOPDLogger.Write(EntityNames.CustomerEntityName, customer.ID, ActionType.Create);

            return customer;
        }

        private CustomerEntity Update(CustomerEntity customer, ElementBL elementBL)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            string userName = IdentityUser.GetIdentityUserName();
            CommonEntities.ElementEntity _customerMetadata = base.GetElementByName(EntityNames.CustomerEntityName, elementBL);
            int _chNumberAttributeID = 0;
            if ((_customerMetadata != null)
                && (_customerMetadata.Attributes != null))
            {
                CommonEntities.AttributeEntity attribute = _customerMetadata.Attributes.Where(attr => attr.Name == "CHNumber").Select(attr => attr).FirstOrDefault();
                if (attribute != null)
                    _chNumberAttributeID = attribute.ID;
            }

            List<Tuple<int, string>> careCentersWitCodeGenerator = GetCareCentersWithCodeGenerator(_customerMetadata, _chNumberAttributeID);

            using (TransactionScope scope = new TransactionScope())
            {
                if (customer.Person.EditStatus.Value == StatusEntityValue.Updated)
                {
                    customer.Person = base.InnerUpdate(customer.Person, userName, true);
                }
                //else
                //{
                //    customer.Person = base.InnerUpdate(customer.Person, userName, false);
                //}

                if (customer.EditStatus.Value == StatusEntityValue.Updated)
                {
                    customer = this.InnerUpdate(customer, userName, true, careCentersWitCodeGenerator);
                }
                //else
                //{
                //    this.InnerUpdate(customer, userName, false);
                //}

                scope.Complete();
            }

            this.ResetCustomer(customer);
            LOPDLogger.Write(EntityNames.CustomerEntityName, customer.ID, ActionType.Modify);
            return customer;
        }

        private void SaveCustomerCHNumbers(CustomerEntity customer, string userName)
        {
            if (customer.CustomerCHNumbers != null
                && customer.CustomerCHNumbers.Length > 0)
            {
                List<RelatedCHNumberEntity> rchnumberList = new List<RelatedCHNumberEntity>();
                rchnumberList.AddRange(customer.CustomerCHNumbers);

                foreach (RelatedCHNumberEntity relatedCHNumber in customer.CustomerCHNumbers)
                {
                    relatedCHNumber.CustomerID = customer.ID;
                    switch (relatedCHNumber.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            DataAccess.CustomerRelatedCHNumberDA.Delete(relatedCHNumber.ID);
                            rchnumberList.Remove(relatedCHNumber);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            rchnumberList.Remove(relatedCHNumber);
                            break;
                        case StatusEntityValue.New:
                            DataAccess.CustomerRelatedCHNumberDA.Insert(relatedCHNumber.CustomerID, relatedCHNumber.CareCenterID, relatedCHNumber.CHNumber, userName);
                            relatedCHNumber.EditStatus.Reset();
                            break;
                        case StatusEntityValue.Updated:
                            DataAccess.CustomerRelatedCHNumberDA.Update(relatedCHNumber.ID, relatedCHNumber.CustomerID, relatedCHNumber.CareCenterID, relatedCHNumber.CHNumber, userName);
                            relatedCHNumber.EditStatus.Reset();
                            break;
                        default:
                            break;
                    }
                }

                customer.CustomerCHNumbers = (rchnumberList.Count > 0) ? rchnumberList.ToArray() : null;
            }
        }

        private string GetIDNumber(PersonIdentifierEntity[] identifiers, string identifierTypeName)
        {
            String IDNumber = String.Empty;
            if (identifiers != null)
            {
                foreach (PersonIdentifierEntity identifier in identifiers)
                {
                    if ((identifier.IdentifierType != null) && (identifier.IdentifierType.Name == identifierTypeName)
                        && (identifier.EditStatus.Value != StatusEntityValue.Deleted) && (identifier.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        IDNumber = identifier.IDNumber;
                    }
                }
            }
            return IDNumber;
        }

        private PersonIdentifierEntity GetPersonIdentifierEntityDefaultIdentifier(PersonIdentifierEntity[] identifiers)
        {
            AdministrativeConfigurationSection administrativeConfiguraction = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative");
            string defaultIdentifierType = administrativeConfiguraction.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].DefaultValue;
            List<string> alternativeDefaultIdentifierType = new List<string>();
            if ((administrativeConfiguraction.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null)
                && (administrativeConfiguraction.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
            {
                foreach (EntityAttributeOptionElement option in administrativeConfiguraction.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
                {
                    alternativeDefaultIdentifierType.Add(option.Value);
                }
            }

            PersonIdentifierEntity result = null;
            if (identifiers != null)
            {
                foreach (PersonIdentifierEntity identifier in identifiers)
                {
                    if (identifier.IdentifierType.Name == defaultIdentifierType)
                    {
                        result = identifier;
                        break;
                    }
                }

                if (result == null)
                {
                    foreach (string alternative in alternativeDefaultIdentifierType)
                    {
                        foreach (PersonIdentifierEntity identifier in identifiers)
                        {
                            if (identifier.IdentifierType.Name == alternative)
                            {
                                result = identifier;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private CommonEntities.AddInTokenBaseEntity[] GetRegistrationStepListToken()
        {
            try
            {
                Collection<AddInToken> tokens = AddInRepository.GetAddInTokens<RegistrationStepHostView>();
                List<CommonEntities.AddInTokenBaseEntity> result = null;
                if (tokens != null)
                {
                    result = new List<CommonEntities.AddInTokenBaseEntity>();
                    foreach (AddInToken token in tokens)
                    {
                        result.Add(new CommonEntities.AddInTokenBaseEntity(token.Name, token.Description, token.Publisher, token.Version));
                    }
                }
                return (result != null) ? result.ToArray() : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private bool ExistCustomerProcess(int careCenterID, int customerID)
        {
            if (careCenterID <= 0 || customerID <= 0) return false;
            return DataAccess.CustomerProcessDA.ExistCustomerProcess(careCenterID, customerID);
        }

        private CustomerBasicEntity[] GetFilteredCustomers(CustomerFilterSpecification filters, ref bool maxRecordsExceded, int maxRows, CommonEntities.AddInTokenBaseEntity[] phoneticAddinNames)
        {
            DataSet ds = null;
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByFullName | CustomerFindOptionEnum.PhoneticLookupByNameParts))
            {
                List<CustomerBasicEntity> myCustomers = new List<CustomerBasicEntity>();

                CustomerFilterSpecification mySpecification = filters.Clone() as CustomerFilterSpecification;
                foreach (CommonEntities.AddInTokenBaseEntity phoneticAddinName in phoneticAddinNames)
                {
                    PhoneticTranslatorHostView host = AddInRepository.GetAddIn<PhoneticTranslatorHostView>(phoneticAddinName.AddinName);
                    if (host != null)
                    {
                        if (mySpecification.IsFilteredByAny(CustomerFindOptionEnum.FirstName))
                            mySpecification.FirstName = host.Translate(mySpecification.FirstName);
                        if (mySpecification.IsFilteredByAny(CustomerFindOptionEnum.LastName))
                            mySpecification.LastName = host.Translate(mySpecification.LastName);
                        if (mySpecification.IsFilteredByAny(CustomerFindOptionEnum.LastName2))
                            mySpecification.LastName2 = host.Translate(mySpecification.LastName2);
                        if (mySpecification.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByFullName))
                            mySpecification.PhoneticLookupFullName = host.Translate(mySpecification.PhoneticLookupFullName);
                    }

                    ds = DataAccess.CustomerDA.GetCustomerBasicList(mySpecification, maxRows, GetDefaultIdentifierType(), phoneticAddinName.AddinName);
                    if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable)
                            && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable].Rows.Count > 0)))
                    {
                        IEnumerable<int> cIDs = ds.Tables[Administrative.Entities.TableNames.CustomerBasicTable].AsEnumerable()
                                            .Select(r => r.Field<int>("ID"))
                                            .Distinct()
                                            .OrderBy(i => i);

                        int observationTemplateID = ObservationTemplateBL.GetExceptionalInfoTemplate();
                        int observationID = ObservationTemplateBL.GetExceptionalInfoLOPD();

                        DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOByCustomerIDs(cIDs, ObservationStatusEnum.Confirmed),
                            ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationTable);

                        DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOValueByCustomerIDsAndObservationTemplate(cIDs, observationTemplateID, observationID, ObservationStatusEnum.Confirmed),
                            ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationValueTable);

                        DataSet myds = new DataSet();
                        DataTable myTable = new DataTable(Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);
                        myTable.Columns.Add("ObservationID", typeof(int));
                        myTable.Columns.Add("ObservationTemplateID", typeof(int));
                        myTable.Rows.Add(observationID, observationTemplateID);
                        myds.Tables.Add(myTable);
                        DatasetUtils.MergeTable(myds, ds, Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);

                        CustomerBasicAdvancedAdapter cbaa = new CustomerBasicAdvancedAdapter();
                        CustomerBasicEntity[] cpe = cbaa.GetData(ds);
                        if (cpe != null)
                        {
                            myCustomers.AddRange(cpe);
                            maxRecordsExceded = (cpe.Length >= maxRows);
                            if (maxRecordsExceded)
                            {
                                myCustomers = myCustomers
                                                .Take(maxRows)
                                                .ToList();
                                break;
                            }
                        }
                    }
                }
                return myCustomers.Count > 0 ? myCustomers.ToArray() : null;
            }
            else
            {
                ds = DataAccess.CustomerDA.GetCustomerBasicList(filters, maxRows, GetDefaultIdentifierType());
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable)
                        && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable].Rows.Count > 0)))
                {
                    IEnumerable<int> cIDs = ds.Tables[Administrative.Entities.TableNames.CustomerBasicTable].AsEnumerable()
                                        .Select(r => r.Field<int>("ID"))
                                        .Distinct()
                                        .OrderBy(i => i);

                    int observationTemplateID = ObservationTemplateBL.GetExceptionalInfoTemplate();
                    int observationID = ObservationTemplateBL.GetExceptionalInfoLOPD();

                    DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOByCustomerIDs(cIDs, ObservationStatusEnum.Confirmed),
                        ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationTable);

                    DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOValueByCustomerIDsAndObservationTemplate(cIDs, observationTemplateID, observationID, ObservationStatusEnum.Confirmed),
                        ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationValueTable);

                    DataSet myds = new DataSet();
                    DataTable myTable = new DataTable(Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);
                    myTable.Columns.Add("ObservationID", typeof(int));
                    myTable.Columns.Add("ObservationTemplateID", typeof(int));
                    myTable.Rows.Add(observationID, observationTemplateID);
                    myds.Tables.Add(myTable);
                    DatasetUtils.MergeTable(myds, ds, Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);

                    CustomerBasicAdvancedAdapter cbaa = new CustomerBasicAdvancedAdapter();
                    CustomerBasicEntity[] cpe = cbaa.GetData(ds);
                    if (cpe != null)
                    {
                        maxRecordsExceded = (cpe.Length >= maxRows);
                    }
                    return cpe;
                }
            }
            return null;
        }

        private int[] FindMergedPersons(int personID)
        {
            List<int> personIDs = new List<int>();

            DataSet myDS = DataAccess.PersonDA.GetPerson(personID);
            if ((myDS.Tables != null)
                && (myDS.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable))
                && (myDS.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0))
            {
                int recordMergedID = myDS.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["RecordMerged"] as int? ?? 0;
                bool hasMergedRegisters = myDS.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["HasMergedRegisters"] as bool? ?? false;
                //Si es un fusionado
                if (recordMergedID > 0)
                {
                    //Se añade el resultado de la fusión
                    personIDs.Add(recordMergedID);

                    //Se añaden todos los fusionados que apuntan a ese resultado de fusión
                    myDS = DataAccess.PersonDA.GetPersonsByRecordMerged(recordMergedID);
                    if ((myDS.Tables != null)
                        && (myDS.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable))
                        && (myDS.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0))
                    {
                        personIDs.AddRange(myDS.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].AsEnumerable()
                                                .Select(r => r["ID"] as int? ?? 0));
                    }
                }
                else
                {
                    //Si es un resultado de una fusión
                    if (hasMergedRegisters)
                    {
                        //Se añade él mismo
                        personIDs.Add(personID);

                        //Se añaden todos los fusionados que apuntan a ese resultado de fusión
                        myDS = DataAccess.PersonDA.GetPersonsByRecordMerged(personID);
                        if ((myDS.Tables != null)
                            && (myDS.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable))
                            && (myDS.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0))
                        {
                            personIDs.AddRange(myDS.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].AsEnumerable()
                                                    .Select(r => r["ID"] as int? ?? 0));
                        }
                    }
                }
            }

            return personIDs.Count > 0 ? personIDs.ToArray() : null;
        }
        #endregion

        #region protected methods
        protected void ResetCustomer(CustomerEntity customer)
        {
            customer.EditStatus.Reset();
            base.ResetPerson(customer.Person);
        }
        #endregion

        #region Public methods without service
        public Customer ConvertCustomerEntityInCustomer(CustomerEntity customer)
        {
            Customer registrationInfo = new Customer();
            registrationInfo.FirstName = customer.Person.FirstName;
            registrationInfo.LastName = customer.Person.LastName;
            registrationInfo.LastName2 = customer.Person.LastName2;
            registrationInfo.EmailAddress = customer.Person.EmailAddress;

            if (registrationInfo.CustomerAddress == null) registrationInfo.CustomerAddress = new Address();
            registrationInfo.CustomerAddress.TypeAddress = (customer.Person.Address != null) ? customer.Person.Address.AddressType : string.Empty;
            registrationInfo.CustomerAddress.StAddress = (customer.Person.Address != null) ? customer.Person.Address.Address1 : string.Empty;
            registrationInfo.CustomerAddress.City = (customer.Person.Address != null) ? customer.Person.Address.City : string.Empty;
            registrationInfo.CustomerAddress.Province = (customer.Person.Address != null) ? customer.Person.Address.Province : string.Empty;
            registrationInfo.CustomerAddress.State = (customer.Person.Address != null) ? customer.Person.Address.State : string.Empty;
            registrationInfo.CustomerAddress.ZipCode = (customer.Person.Address != null) ? customer.Person.Address.ZipCode : string.Empty;
            registrationInfo.CustomerAddress.Country = (customer.Person.Address != null) ? customer.Person.Address.Country : string.Empty;

            registrationInfo.IdentificationNumber = customer.IdentificationNumber;
            registrationInfo.CHNumber = customer.CHNumber;
            registrationInfo.ShortIDNumber = customer.ShortIDNumber;

            registrationInfo.BirthDate = customer.Person.SensitiveData.BirthDate;
            registrationInfo.Sex = (SII.HCD.Addin.Entities.SexEnum)((int)customer.Person.SensitiveData.Sex);
            registrationInfo.SexCode = customer.Person.SensitiveData.Sex.ToString();
            registrationInfo.EducationLevel = (Addin.Entities.EducationLevelEnum)((int)customer.Person.SensitiveData.EducationLevel);
            registrationInfo.EducationLevelCode = customer.Person.SensitiveData.EducationLevel.ToString();
            registrationInfo.Language = (Addin.Entities.LanguageEnum)((int)customer.Person.SensitiveData.Language);
            registrationInfo.LanguageCode = customer.Person.SensitiveData.Language.ToString();
            registrationInfo.MaritalStatus = (Addin.Entities.MaritalStatusEnum)((int)customer.Person.SensitiveData.MaritalStatus);
            registrationInfo.MaritalStatusCode = customer.Person.SensitiveData.MaritalStatus.ToString();
            registrationInfo.ReligiousPreference = (Addin.Entities.ReligiousEnum)((int)customer.Person.SensitiveData.ReligiousPreference);
            registrationInfo.ReligiousPreferenceCode = customer.Person.SensitiveData.ReligiousPreference.ToString();

            List<NOK> noks = new List<NOK>();
            if ((customer.NOKs != null) && (customer.NOKs.Length > 0))
            {
                foreach (NOKEntity nok in customer.NOKs)
                {
                    NOK myNOK = new NOK(nok.Person.FirstName, nok.Person.LastName, nok.Person.LastName2, nok.Person.EmailAddress,
                        this.GetIdentifiers(nok.Person.Identifiers),
                        null, (Addin.Entities.SexEnum)((int)nok.Person.SensitiveData.Sex), nok.Person.SensitiveData.Sex.ToString(), (nok.Kinship != null) ? nok.Kinship.Code : string.Empty);
                    if ((nok.Person.Telephones != null) && (nok.Person.Telephones.Length > 0))
                    {
                        myNOK.Telephones = (from tlf in nok.Person.Telephones
                                            where tlf.Telephone != null
                                            select new Telephone(tlf.Telephone.TelephoneType, tlf.Telephone.Telephone, tlf.Telephone.EmergencyContactPhone, tlf.Telephone.Comments)).ToArray();
                    }
                    noks.Add(myNOK);
                }
            }
            registrationInfo.NOKs = noks.ToArray();

            registrationInfo.Identifiers = this.GetIdentifiers(customer.Person.Identifiers);
            if ((customer.Person.Telephones != null) && (customer.Person.Telephones.Length > 0))
            {
                registrationInfo.Telephones = (from tlf in customer.Person.Telephones
                                               where tlf.Telephone != null
                                               select new Telephone(tlf.Telephone.TelephoneType, tlf.Telephone.Telephone, tlf.Telephone.EmergencyContactPhone, tlf.Telephone.Comments)).ToArray();
            }

            return registrationInfo;
        }

        public Identifier[] GetIdentifiers(PersonIdentifierEntity[] identifiers)
        {
            if (identifiers == null || identifiers.Length <= 0) return null;
            return (from idt in identifiers
                    where idt.IdentifierType != null
                    select new Identifier(idt.IDNumber, string.Empty, idt.IdentifierType.Name, idt.IdentifierType.RequiredValidation, idt.IdentifierType.ValidationMask)
                    ).ToArray();

        }

        public void AssignCustomerInCustomerEntity(Customer registrationInfo, CustomerEntity customer)
        {
            IdentifierTypeBL _identifierTypeBL = new IdentifierTypeBL();

            customer.Person.FirstName = registrationInfo.FirstName;
            customer.Person.LastName = registrationInfo.LastName;
            customer.Person.LastName2 = registrationInfo.LastName2;
            customer.Person.EmailAddress = registrationInfo.EmailAddress;

            if (customer.Person.Address == null)
            {
                customer.Person.Address = new AddressEntity();
                customer.Person.Address.EditStatus.New();
                customer.Person.EditStatus.Update();
                customer.EditStatus.Update();
            }
            customer.Person.Address.AddressType = (registrationInfo.CustomerAddress != null) ? registrationInfo.CustomerAddress.TypeAddress : string.Empty;
            customer.Person.Address.Address1 = (registrationInfo.CustomerAddress != null) ? registrationInfo.CustomerAddress.StAddress : string.Empty;
            customer.Person.Address.City = (registrationInfo.CustomerAddress != null) ? registrationInfo.CustomerAddress.City : string.Empty;
            customer.Person.Address.Province = (registrationInfo.CustomerAddress != null) ? registrationInfo.CustomerAddress.Province : string.Empty;
            customer.Person.Address.State = (registrationInfo.CustomerAddress != null) ? registrationInfo.CustomerAddress.State : string.Empty;
            customer.Person.Address.ZipCode = (registrationInfo.CustomerAddress != null) ? registrationInfo.CustomerAddress.ZipCode : string.Empty;
            customer.Person.Address.Country = (registrationInfo.CustomerAddress != null) ? registrationInfo.CustomerAddress.Country : string.Empty;

            customer.IdentificationNumber = registrationInfo.IdentificationNumber;
            customer.CHNumber = registrationInfo.CHNumber;
            customer.ShortIDNumber = registrationInfo.ShortIDNumber;

            customer.Person.SensitiveData.BirthDate = registrationInfo.BirthDate;
            customer.Person.SensitiveData.Sex = (BackOffice.Entities.SexEnum)((int)registrationInfo.Sex);
            customer.Person.SensitiveData.EducationLevel = (BackOffice.Entities.EducationLevelEnum)((int)registrationInfo.EducationLevel);
            customer.Person.SensitiveData.Language = (BackOffice.Entities.LanguageEnum)((int)registrationInfo.Language);
            customer.Person.SensitiveData.MaritalStatus = (BackOffice.Entities.MaritalStatusEnum)((int)registrationInfo.MaritalStatus);
            customer.Person.SensitiveData.ReligiousPreference = (BackOffice.Entities.ReligiousEnum)((int)registrationInfo.ReligiousPreference);

            List<NOKEntity> noks = new List<NOKEntity>();
            if ((registrationInfo.NOKs != null) && (registrationInfo.NOKs.Length > 0))
            {
                foreach (NOK nok in registrationInfo.NOKs)
                {
                    NOKEntity myNOK = ((customer.NOKs != null) && (customer.NOKs.Length > 0)) ?
                                       (from nokPerson in customer.NOKs
                                        where (nokPerson.Person.FirstName == nok.FirstName) && (nokPerson.Person.LastName == nok.LastName) && (nokPerson.Person.LastName2 == nok.LastName2)
                                             && ((nokPerson.Person.Identifiers == null) || (nok.Identifiers == null)
                                                 || (Array.Exists(nokPerson.Person.Identifiers, (PersonIdentifierEntity pi) =>
                                                      Array.Exists(nok.Identifiers, nokid => (pi.IdentifierType.Name == nokid.IdentifierTypeName) && (pi.IDNumber == nokid.IDNumber)))))
                                        select nokPerson).FirstOrDefault() : null;
                    if (myNOK == null)
                    {
                        myNOK = new NOKEntity();
                        myNOK.Person.FirstName = nok.FirstName;
                        myNOK.Person.LastName = nok.LastName;
                        myNOK.Person.LastName2 = nok.LastName2;
                        myNOK.Person.EmailAddress = nok.EmailAddress;
                        if (nok.Identifiers != null && nok.Identifiers.Length > 0)
                        {
                            IdentifierTypeEntity identifierType = _identifierTypeBL.GetIdentifierType(_identifierTypeBL.GetIdentifierTypeID(nok.Identifiers[0].IdentifierTypeName));
                            if (identifierType != null)
                            {
                                PersonIdentifierEntity ident = new PersonIdentifierEntity(0, nok.Identifiers[0].IDNumber, 0, identifierType, 0);
                                ident.EditStatus.New();
                                myNOK.Person.Identifiers = new PersonIdentifierEntity[1] { ident };
                                customer.Person.EditStatus.Update();
                                customer.EditStatus.Update();
                            }
                        }
                        //TODO: Buscar cual es la entidad de relacion de la persona de contacto.
                        myNOK.Kinship = null;

                        myNOK.Person.SensitiveData.Sex = (BackOffice.Entities.SexEnum)((int)nok.Sex);

                        if (nok.Telephones != null && nok.Telephones.Length > 0)
                        {
                            TelephoneEntity phone = new TelephoneEntity(0, nok.Telephones[0].Phone, nok.Telephones[0].Comments, nok.Telephones[0].TelephoneType, nok.Telephones[0].Emergency, 0);
                            phone.EditStatus.New();
                            PersonTelephoneEntity personPhone = new PersonTelephoneEntity(0, 0, phone, 0);
                            personPhone.EditStatus.New();
                            myNOK.Person.Telephones = new PersonTelephoneEntity[1] { personPhone };
                            customer.Person.EditStatus.Update();
                            customer.EditStatus.Update();
                        }
                    }
                    noks.Add(myNOK);
                }
            }
            customer.NOKs = noks.ToArray();

            List<PersonIdentifierEntity> customerIdentifiers = (customer.Person.Identifiers != null) ? new List<PersonIdentifierEntity>(customer.Person.Identifiers) : new List<PersonIdentifierEntity>();
            PersonIdentifierEntity defaultIdentifier = this.GetPersonIdentifierEntityDefaultIdentifier(customer.Person.Identifiers);
            if ((defaultIdentifier == null) && (registrationInfo.Identifiers != null) && (registrationInfo.Identifiers.Length > 0))
            {
                foreach (Identifier ident in registrationInfo.Identifiers)
                {
                    if (!customerIdentifiers.Exists((PersonIdentifierEntity pi) => (pi.IdentifierType.Name == ident.IdentifierTypeName)))
                    {
                        PersonIdentifierEntity identOther = new PersonIdentifierEntity(0, ident.IDNumber, 0, _identifierTypeBL.GetIdentifierType(_identifierTypeBL.GetIdentifierTypeID(ident.IdentifierTypeName)), 0);
                        identOther.EditStatus.New();
                        customerIdentifiers.Add(identOther);
                        customer.Person.EditStatus.Update();
                        customer.EditStatus.Update();
                    }
                }
            }
            customer.Person.Identifiers = customerIdentifiers.ToArray();

            List<PersonTelephoneEntity> customerTelephones = (customer.Person.Telephones != null) ? new List<PersonTelephoneEntity>(customer.Person.Telephones) : new List<PersonTelephoneEntity>();
            if ((registrationInfo.Telephones != null) && (registrationInfo.Telephones.Length > 0))
            {
                foreach (Telephone tlf in registrationInfo.Telephones)
                {
                    if (!customerTelephones.Exists((PersonTelephoneEntity pt) => (pt.Telephone.Telephone == tlf.Phone)))
                    {
                        TelephoneEntity phone = new TelephoneEntity(0, tlf.Phone, tlf.Comments, tlf.TelephoneType, tlf.Emergency, 0);
                        phone.EditStatus.New();
                        PersonTelephoneEntity personPhone = new PersonTelephoneEntity(0, 0, phone, 0);
                        personPhone.EditStatus.New();
                        customerTelephones.Add(personPhone);
                        customer.Person.EditStatus.Update();
                        customer.EditStatus.Update();
                    }
                }
            }
            customer.Person.Telephones = customerTelephones.ToArray();
        }

        public void ForceUpdateCustomer(int customerID)
        {
            if (customerID <= 0) return;
            try
            {
                CustomerEntity customer = this.GetCustomer(customerID);
                customer.EditStatus.Update();
                PersonAddressListDTO[] homonymPersons = null;
                Save(customer, true, out homonymPersons);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }

        }
        #endregion

        #region public methods
        public void CheckInsertPreconditions(CustomerEntity customer, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            ValidateCustomer(customer, elementBL);

            #region Comentado por SALVA
            //CustomerFindRequest customerFind = new CustomerFindRequest();
            //AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;

            //if (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null)
            //{
            //    foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.CustomerEntity.Attributes)
            //    {
            //        if ((attrib.Name == "FirstName") && (attrib.Mandatory))
            //        {
            //            customerFind.FirstName = customer.Person.FirstName;
            //            customerFind.MandatoryFirstName = true;
            //        }

            //        if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //        {
            //            customerFind.LastName = customer.Person.LastName;
            //            customerFind.MandatoryLastName = true;
            //        }

            //        if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(customer.AllowNoDefaultIdentifier))
            //        {
            //            customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
            //            customerFind.MandatoryIdentifierType = true;
            //            customerFind.IdentifierIDNumber = GetIDNumber(customer.Person.Identifiers, attrib.DefaultValue);
            //        }
            //    }
            //}

            //if (String.IsNullOrEmpty(customerFind.IdentifierIDNumber) && (customerFind.MandatoryIdentifierType))
            //{
            //    //checking alternatives
            //    if ((administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
            //        (administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
            //    {
            //        Boolean alternativeFound = false;
            //        String alternatives = customerFind.MandatoryIdentifierTypeDefaultValue;
            //        foreach (EntityAttributeOptionElement alternative in administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
            //        {
            //            string idNumber = GetIDNumber(customer.Person.Identifiers, alternative.Value);
            //            if (!String.IsNullOrEmpty(idNumber))
            //            {
            //                alternativeFound = true;
            //                customerFind.IdentifierIDNumber = idNumber;
            //                customerFind.MandatoryIdentifierTypeDefaultValue = alternative.Value;
            //                break;
            //            }
            //            else
            //            {
            //                alternatives = String.Concat(alternatives, " ", Properties.Resources.MSG_or, " ", alternative.Value);
            //            }
            //        }
            //        if (!alternativeFound)
            //        {
            //            throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, alternatives));
            //        }
            //        else
            //        {
            //            //customerFind.MandatoryIdentifierType = false;
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, customerFind.MandatoryIdentifierTypeDefaultValue));
            //    }
            //}
            #endregion

            homonymPersons = null;

            //DO SALVA: Person check
            switch (customer.Person.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    //DO SALVA
                    base.CheckInsertPreconditions(customer.Person, customer.ID, CategoryPersonKeyEnum.Customer, forceSave,
                        customer.AllowNoDefaultIdentifier, !customer.PoorlyIdentified, out homonymPersons, elementBL);
                    break;
                case StatusEntityValue.Updated:
                    //DO SALVA
                    base.CheckUpdatePreconditions(customer.Person, customer.ID, CategoryPersonKeyEnum.Customer, forceSave,
                        customer.AllowNoDefaultIdentifier, !customer.PoorlyIdentified, out homonymPersons, elementBL);
                    break;
            }

            if (!customer.PoorlyIdentified)
            {
                //Identification Number check
                if ((!String.IsNullOrEmpty(customer.IdentificationNumber)) && (DataAccess.CustomerDA.FindIdentificationNumber(customer.IdentificationNumber) > 0))
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_IdentificationNumberAlreadyExists, customer.IdentificationNumber));
                }

                bool CHNumberIsCodeGenerator = false;
                CommonEntities.ElementEntity _customerMetadata = this.GetElementByName(EntityNames.CustomerEntityName, elementBL);
                if (_customerMetadata != null
                    && _customerMetadata.Attributes != null)
                {
                    CHNumberIsCodeGenerator = !string.IsNullOrWhiteSpace(_customerMetadata.GetCodeGeneratorName("CHNumber"));
                }
                //CH Number check
                if ((CHNumberIsCodeGenerator || !ThereAreNCareCentersWithCodeGenerator())
                    && (!String.IsNullOrEmpty(customer.CHNumber))
                    && (DataAccess.CustomerDA.FindCHNumber(customer.CHNumber) > 0))
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_CHNumberAlreadyExists, customer.CHNumber));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(customer.Person.FirstName))
                    throw new Exception(Properties.Resources.MSG_RequiredFirstName);

                if (string.IsNullOrEmpty(customer.Person.LastName))
                    throw new Exception(Properties.Resources.MSG_RequiredLastName);

                if (string.IsNullOrEmpty(customer.Person.LastName2))
                {
                    CommonEntities.ElementEntity _personMetadata = this.GetElementByName(EntityNames.PersonEntityName, elementBL);

                    if (_personMetadata != null)
                    {
                        Common.Entities.AttributeEntity attributeLastName2 = _personMetadata.GetAttribute("LastName2");

                        if ((attributeLastName2 != null) && attributeLastName2.Required)
                            throw new Exception(Properties.Resources.MSG_RequiredLastName2);
                    }
                }
            }
        }

        public void CheckUpdatePreconditions(CustomerEntity customer, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            ValidateCustomer(customer, elementBL);

            #region //Comentado por SALVA
            //CustomerFindRequest customerFind = new CustomerFindRequest();
            //AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
            //if (administrativeConfig != null)
            //{
            //    if (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null)
            //    {
            //        foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.CustomerEntity.Attributes)
            //        {
            //            if ((attrib.Name == "FirstName") && (attrib.Mandatory))
            //            {
            //                customerFind.FirstName = customer.Person.FirstName;
            //                customerFind.MandatoryFirstName = true;
            //            }

            //            if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //            {
            //                customerFind.LastName = customer.Person.LastName;
            //                customerFind.MandatoryLastName = true;
            //            }

            //            if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(customer.AllowNoDefaultIdentifier))
            //            {
            //                customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
            //                customerFind.MandatoryIdentifierType = true;
            //                customerFind.IdentifierIDNumber = GetIDNumber(customer.Person.Identifiers, attrib.DefaultValue);
            //            }
            //        }
            //    }
            //}

            //if (String.IsNullOrEmpty(customerFind.IdentifierIDNumber) && (customerFind.MandatoryIdentifierType))
            //{
            //    //checking alternatives
            //    if ((administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
            //        (administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
            //    {
            //        Boolean alternativeFound = false;
            //        String alternatives = customerFind.MandatoryIdentifierTypeDefaultValue;
            //        foreach (EntityAttributeOptionElement alternative in administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
            //        {
            //            if (!String.IsNullOrEmpty(GetIDNumber(customer.Person.Identifiers, alternative.Value)))
            //            {
            //                alternativeFound = true;
            //                break;
            //            }
            //            else
            //            {
            //                alternatives = String.Concat(alternatives, " ", Properties.Resources.MSG_or, " ", alternative.Value);
            //            }
            //        }
            //        if (!alternativeFound)
            //        {
            //            throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, alternatives));
            //        }
            //        else
            //        {
            //            customerFind.MandatoryIdentifierType = false;
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, customerFind.MandatoryIdentifierTypeDefaultValue));
            //    }
            //}

            //int id = DataAccess.PersonDA.GetPerson(customerFind);
            //if ((id > 0) && (id != customer.Person.ID))
            //{
            //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(customer.Person.FirstName, " ", customer.Person.LastName)));
            //}
            #endregion

            homonymPersons = null;

            //Person check
            base.CheckUpdatePreconditions(customer.Person, customer.ID, CategoryPersonKeyEnum.Customer, forceSave,
                customer.AllowNoDefaultIdentifier, !customer.PoorlyIdentified, out homonymPersons, elementBL);

            int customerID = 0;

            if (customer.PoorlyIdentified)
            {
                if (string.IsNullOrEmpty(customer.Person.FirstName))
                    throw new Exception(Properties.Resources.MSG_RequiredFirstName);

                if (string.IsNullOrEmpty(customer.Person.LastName))
                    throw new Exception(Properties.Resources.MSG_RequiredLastName);

                if (string.IsNullOrEmpty(customer.Person.LastName2))
                {
                    CommonEntities.ElementEntity _personMetadata = this.GetElementByName(EntityNames.PersonEntityName, elementBL);

                    if (_personMetadata != null)
                    {
                        Common.Entities.AttributeEntity attributeLastName2 = _personMetadata.GetAttribute("LastName2");

                        if ((attributeLastName2 != null) && attributeLastName2.Required)
                            throw new Exception(Properties.Resources.MSG_RequiredLastName2);
                    }
                }
            }

            if (!String.IsNullOrEmpty(customer.IdentificationNumber))
            {
                customerID = DataAccess.CustomerDA.FindIdentificationNumber(customer.IdentificationNumber);
                if ((customerID > 0) && (customerID != customer.ID))
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_IdentificationNumberAlreadyExists, customer.IdentificationNumber));
                }
            }

            if (!ThereAreNCareCentersWithCodeGenerator()
                && !String.IsNullOrEmpty(customer.CHNumber))
            {
                customerID = DataAccess.CustomerDA.FindCHNumber(customer.CHNumber);
                if ((customerID > 0) && (customerID != customer.ID))
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_CHNumberAlreadyExists, customer.CHNumber));
                }
            }

            SensitiveDataDA _sensitiveDataDA = new SensitiveDataDA();
            DataSet ds = _sensitiveDataDA.GetSensitiveDataByCustomer(customer.ID);
            DateTime? actualDeathDate = null;
            if ((ds != null) && (ds.Tables.Contains(BackOffice.Entities.TableNames.SensitiveDataTable)) &&
                (ds.Tables[BackOffice.Entities.TableNames.SensitiveDataTable].Rows.Count > 0))
                actualDeathDate = ds.Tables[BackOffice.Entities.TableNames.SensitiveDataTable].Rows[0]["DeathDateTime"] as DateTime?;

            if ((customer.Person.SensitiveData != null) && (customer.Person.SensitiveData.DeathDateTime != null) && (actualDeathDate == null))
            {
                CustomerEpisodeDA _customerEpisodeDA = new CustomerEpisodeDA();
                if (_customerEpisodeDA.HasCustomerEpisodes(customer.ID, CommonEntities.StatusEnum.Active))
                    throw new Exception(string.Format(Properties.Resources.MSG_CannotSetDeceasedDataCustomerHasActiveEpisodes, customer.CHNumber));
            }
        }

        public void ValidateCustomer(CustomerEntity customer, ElementBL elementBL)
        {
            if (customer == null) throw new ArgumentNullException("customer");

            CommonEntities.ElementEntity _customerMetadata = base.GetElementByName(EntityNames.CustomerEntityName, elementBL);
            CustomerHelper customerHelper = new CustomerHelper(_customerMetadata);

            ValidationResults result = customerHelper.Validate(customer);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_customerValidationError, sb));
            }

            //base.ValidatePerson(customer.Person, customer.AllowNoDefaultIdentifier, elementBL);
        }

        public List<Tuple<int, string>> GetCareCentersWithCodeGenerator(CommonEntities.ElementEntity _customerMetadata, int _chNumberAttributeID)
        {
            List<Tuple<int, string>> careCentersWitCodeGenerator = DataAccess.CareCenterRelatedCodeGeneratorDA.GetCareCentersWithCodeGenerator(_customerMetadata.ID, _chNumberAttributeID);
            return careCentersWitCodeGenerator;
        }

        public CustomerEntity InnerInsert(CustomerEntity customer, string userName, int categoryID, string customerIDNumber, string customerCHNumber, List<Tuple<int, string>> careCentersWitCodeGenerator)
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            if (!String.IsNullOrEmpty(customerIDNumber))
            {
                customer.IdentificationNumber = codeGenerator.Generate(String.Empty, customerIDNumber);
            }

            if (careCentersWitCodeGenerator != null && careCentersWitCodeGenerator.Count > 0)
            {
                List<RelatedCHNumberEntity> chNumbers = new List<RelatedCHNumberEntity>();
                if (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
                    chNumbers.AddRange(customer.CustomerCHNumbers);

                foreach (Tuple<int, string> tuple in careCentersWitCodeGenerator)
                {
                    RelatedCHNumberEntity relatedCHNumber = (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
                        ? Array.Find(customer.CustomerCHNumbers, cchn => cchn.CareCenterID == tuple.Item1)
                        : null;
                    if (relatedCHNumber == null)
                    {
                        if (this.ExistCustomerProcess(tuple.Item1, customer.ID))
                        {
                            relatedCHNumber = new RelatedCHNumberEntity();
                            relatedCHNumber.CareCenterID = tuple.Item1;
                            relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
                            relatedCHNumber.EditStatus.New();

                            chNumbers.Add(relatedCHNumber);
                        }
                    }
                    else
                    {
                        switch (relatedCHNumber.EditStatus.Value)
                        {
                            case StatusEntityValue.New:
                                relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
                                break;
                            default: break;
                        }
                        chNumbers.Add(relatedCHNumber);
                    }
                }
                customer.CustomerCHNumbers = (chNumbers.Count > 0) ? chNumbers.ToArray() : null;
            }

            if (!string.IsNullOrEmpty(customerCHNumber))
            {
                customer.CHNumber = codeGenerator.Generate(String.Empty, customerCHNumber);
            }
            //if (!String.IsNullOrEmpty(customerCHNumber))
            //{
            //    if (careCentersWitCodeGenerator != null
            //        && careCentersWitCodeGenerator.Count > 0)
            //    {
            //        List<RelatedCHNumberEntity> chNumbers = new List<RelatedCHNumberEntity>();
            //        if (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
            //            chNumbers.AddRange(customer.CustomerCHNumbers);

            //        foreach (Tuple<int, string> tuple in careCentersWitCodeGenerator)
            //        {
            //            RelatedCHNumberEntity relatedCHNumber = (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
            //                ? Array.Find(customer.CustomerCHNumbers, cchn => cchn.CareCenterID == tuple.Item1)
            //                : null;
            //            if (relatedCHNumber == null)
            //            {
            //                if (this.ExistCustomerProcess(tuple.Item1, customer.ID))
            //                {
            //                    relatedCHNumber = new RelatedCHNumberEntity();
            //                    relatedCHNumber.CareCenterID = tuple.Item1;
            //                    relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
            //                    relatedCHNumber.EditStatus.New();

            //                    chNumbers.Add(relatedCHNumber);
            //                }
            //            }
            //            else
            //            {
            //                switch (relatedCHNumber.EditStatus.Value)
            //                {
            //                    case StatusEntityValue.New:
            //                        relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
            //                        break;
            //                    default: break;
            //                }
            //                chNumbers.Add(relatedCHNumber);
            //            }
            //        }
            //        customer.CustomerCHNumbers = (chNumbers.Count > 0) ? chNumbers.ToArray() : null;
            //    }
            //    else
            //        customer.CHNumber = codeGenerator.Generate(String.Empty, customerCHNumber);
            //}

            switch (customer.Person.EditStatus.Value)
            {
                case StatusEntityValue.New: customer.Person = base.InnerInsert(customer.Person, userName); break;
                case StatusEntityValue.Updated: customer.Person = base.InnerUpdate(customer.Person, userName, true); break;
                default: break;
            }

            customer.ID = DataAccess.CustomerDA.Insert(customer.Person.ID, customer.PoorlyIdentified, customer.IdentificationNumber, customer.ShortIDNumber, customer.CHNumber, 0, 0,
                (customer.CustomerProfile == null) ? 0 : customer.CustomerProfile.ID, (customer.CustomerClassification == null) ? 0 : customer.CustomerClassification.ID,
                0, (int)customer.CustomerNameConfidentiality, (int)customer.CustomerIdentifierConfidentiality,
                userName);

            SaveCustomerCHNumbers(customer, userName);

            customer.DBTimeStamp = DataAccess.CustomerDA.GetDBTimeStamp(customer.ID);
            return customer;
        }

        public CustomerEntity InnerUpdate(CustomerEntity customer, string userName, bool fullCustomerUpdate, List<Tuple<int, string>> careCentersWitCodeGenerator)
        {
            Int64 dbTimeStamp = DataAccess.CustomerDA.GetDBTimeStamp(customer.ID);
            if (dbTimeStamp != customer.DBTimeStamp)
                throw new FaultException<DBConcurrencyException>(
                    new DBConcurrencyException(string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customer.ID)), string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customer.ID));

            CodeGenerator codeGenerator = new CodeGenerator();
            int customerRelatedOrg = (customer.RelatedOrganization == null) ? 0 : customer.RelatedOrganization.ID;

            if (fullCustomerUpdate)
            {
                if (careCentersWitCodeGenerator != null
                    && careCentersWitCodeGenerator.Count > 0)
                {
                    List<RelatedCHNumberEntity> chNumbers = new List<RelatedCHNumberEntity>();
                    if (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
                        chNumbers.AddRange(customer.CustomerCHNumbers);

                    foreach (Tuple<int, string> tuple in careCentersWitCodeGenerator)
                    {
                        RelatedCHNumberEntity relatedCHNumber = (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
                            ? Array.Find(customer.CustomerCHNumbers, cchn => cchn.CareCenterID == tuple.Item1)
                            : null;
                        if (relatedCHNumber == null)
                        {
                            if (this.ExistCustomerProcess(tuple.Item1, customer.ID))
                            {
                                relatedCHNumber = new RelatedCHNumberEntity();
                                relatedCHNumber.CareCenterID = tuple.Item1;
                                relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
                                relatedCHNumber.EditStatus.New();

                                chNumbers.Add(relatedCHNumber);
                            }
                        }
                        else
                        {
                            switch (relatedCHNumber.EditStatus.Value)
                            {
                                case StatusEntityValue.New:
                                    relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
                                    break;
                                default: break;
                            }

                            chNumbers.Add(relatedCHNumber);
                        }

                    }
                    customer.CustomerCHNumbers = (chNumbers.Count > 0) ? chNumbers.ToArray() : null;

                    SaveCustomerCHNumbers(customer, userName);
                }

                DataAccess.CustomerDA.Update(customer.ID, customer.PoorlyIdentified, customer.IdentificationNumber, customer.ShortIDNumber, customer.CHNumber,
                    (customer.CustomerProfile == null) ? 0 : customer.CustomerProfile.ID,
                    (customer.CustomerClassification == null) ? 0 : customer.CustomerClassification.ID,
                    customerRelatedOrg, (int)customer.CustomerNameConfidentiality, (int)customer.CustomerIdentifierConfidentiality,
                    userName);

            }
            else
            {
                DataAccess.CustomerDA.Update(customer.ID, userName);
            }

            customer.DBTimeStamp = DataAccess.CustomerDA.GetDBTimeStamp(customer.ID);
            return customer;
        }

        public CustomerEntity InnerUpdate(CustomerEntity customer, List<Tuple<int, string>> careCentersWitCodeGenerator, string userName)
        {
            Int64 dbTimeStamp = DataAccess.CustomerDA.GetDBTimeStamp(customer.ID);
            if (dbTimeStamp != customer.DBTimeStamp)
                throw new FaultException<DBConcurrencyException>(
                    new DBConcurrencyException(string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customer.ID)), string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customer.ID));

            if (customer.Person.EditStatus.Value == StatusEntityValue.Updated)
            {
                customer.Person = base.InnerUpdate(customer.Person, userName, true);
            }

            CodeGenerator codeGenerator = new CodeGenerator();
            int customerRelatedOrg = (customer.RelatedOrganization == null) ? 0 : customer.RelatedOrganization.ID;

            if (careCentersWitCodeGenerator != null
                && careCentersWitCodeGenerator.Count > 0)
            {
                List<RelatedCHNumberEntity> chNumbers = new List<RelatedCHNumberEntity>();
                if (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
                    chNumbers.AddRange(customer.CustomerCHNumbers);

                foreach (Tuple<int, string> tuple in careCentersWitCodeGenerator)
                {
                    RelatedCHNumberEntity relatedCHNumber = (customer.CustomerCHNumbers != null && customer.CustomerCHNumbers.Length > 0)
                        ? Array.Find(customer.CustomerCHNumbers, cchn => cchn.CareCenterID == tuple.Item1)
                        : null;
                    if (relatedCHNumber == null)
                    {
                        if (this.ExistCustomerProcess(tuple.Item1, customer.ID))
                        {
                            relatedCHNumber = new RelatedCHNumberEntity();
                            relatedCHNumber.CareCenterID = tuple.Item1;
                            relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
                            relatedCHNumber.EditStatus.New();

                            chNumbers.Add(relatedCHNumber);
                        }
                    }
                    else
                    {
                        switch (relatedCHNumber.EditStatus.Value)
                        {
                            case StatusEntityValue.New:
                                relatedCHNumber.CHNumber = codeGenerator.Generate(String.Empty, tuple.Item2);
                                break;
                            default: break;
                        }

                        chNumbers.Add(relatedCHNumber);
                    }
                }
                customer.CustomerCHNumbers = (chNumbers.Count > 0) ? chNumbers.ToArray() : null;
            }

            DataAccess.CustomerDA.Update(customer.ID, customer.PoorlyIdentified, customer.IdentificationNumber, customer.ShortIDNumber, customer.CHNumber,
                (customer.CustomerProfile == null) ? 0 : customer.CustomerProfile.ID, (customer.CustomerClassification == null) ? 0 : customer.CustomerClassification.ID,
                customerRelatedOrg, (int)customer.CustomerNameConfidentiality, (int)customer.CustomerIdentifierConfidentiality,
                userName);

            SaveCustomerCHNumbers(customer, userName);

            customer.DBTimeStamp = DataAccess.CustomerDA.GetDBTimeStamp(customer.ID);
            return customer;
        }

        public CustomerEntity Save(CustomerEntity customer, bool forceSave, out PersonAddressListDTO[] homonymPersons)
        {
            try
            {
                if (customer == null) throw new ArgumentNullException("customer");
                forceSave = true; //ESTO HAY QUE ANALIZARLO SERIAMENTE CON ALBERTO Y SALVA
                CustomerEpisodeEntity customerEpisode = null;  /// esto es para el mensaje HL7
                CustomerEpisodeBL customerEpisodeBL = null; /// esto es solo para el mensaje HL7
                ProcessChartBL processChartBL = null;   /// esto es solo para el mensaje HL7
                homonymPersons = null;
                Entities.Clear();
                switch (customer.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return customer;
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(customer, forceSave, out homonymPersons, ElementBL);
                        if (homonymPersons != null)
                            return customer;
                        customer = this.Insert(customer, ElementBL);
                        if (customer != null && customer.Person != null)
                        {
                            Entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, customer);
                            //// envia mensaje HL7  ADT^A28 
                            ///////// aqui no va el customerepisode
                            HL7MessagingProcessor.ResetEntities(Entities);
                            HL7MessagingProcessor.ResetBLs(BLs);
                            HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT28);
                            /////////////////////////////////////////////////////
                        }
                        return customer;
                    case StatusEntityValue.NewAndDeleted:
                        return customer;
                    case StatusEntityValue.None:
                        CheckUpdatePreconditions(customer, forceSave, out homonymPersons, ElementBL);
                        if (homonymPersons != null)
                            return customer;
                        if ((customer.Person != null) && (customer.Person.EditStatus.Value == StatusEntityValue.Updated))
                        {
                            customer = this.Update(customer, ElementBL);
                            Entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, customer);
                            customerEpisodeBL = new CustomerEpisodeBL();
                            customerEpisode = customerEpisodeBL.GetCurrentCustomerEpisodeByCustomerID(customer.ID);
                            if (customerEpisode != null)
                            {
                                Entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, customerEpisode);
                                CustomerProcessEntity customerProcess = customerEpisodeBL.GetCustomerProcessByEpisodeID(customerEpisode.ID);
                                Entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, customerProcess);
                                processChartBL = new ProcessChartBL();
                                ProcessChartEntity processChart = processChartBL.GetByID(customerEpisode.ProcessChartID);
                                Entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);
                                //// envia mensaje HL7  ADT^A08 
                                ///////// si falta el customerepisode no sale el segmento PV1
                                HL7MessagingProcessor.ResetEntities(Entities);
                                HL7MessagingProcessor.ResetBLs(BLs);
                                HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT08);
                                /////////////////////////////////////////////////////
                            }
                            else
                            {
                                //// envia mensaje HL7  ADT^A31 
                                ///////// si falta el customerepisode no sale el segmento PV1
                                HL7MessagingProcessor.ResetEntities(Entities);
                                HL7MessagingProcessor.ResetBLs(BLs);
                                HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT31);
                                /////////////////////////////////////////////////////
                            }
                        }
                        return customer;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(customer, forceSave, out homonymPersons, ElementBL);
                        if (homonymPersons != null)
                            return customer;
                        customer = this.Update(customer, ElementBL);
                        if (customer != null && customer.Person != null)
                        {
                            Entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, customer);
                            customerEpisodeBL = new CustomerEpisodeBL();
                            customerEpisode = customerEpisodeBL.GetCurrentCustomerEpisodeByCustomerID(customer.ID);
                            if (customerEpisode != null)
                            {
                                Entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, customerEpisode);
                                CustomerProcessEntity customerProcess = customerEpisodeBL.GetCustomerProcessByEpisodeID(customerEpisode.ID);
                                Entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, customerProcess);
                                processChartBL = new ProcessChartBL();
                                ProcessChartEntity processChart = processChartBL.GetByID(customerEpisode.ProcessChartID);
                                Entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);
                                //// envia mensaje HL7  ADT^A08 
                                ///////// si falta el customerepisode no sale el segmento PV1
                                HL7MessagingProcessor.ResetEntities(Entities);
                                HL7MessagingProcessor.ResetBLs(BLs);
                                HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT08);
                                /////////////////////////////////////////////////////
                            }
                            else
                            {
                                //// envia mensaje HL7  ADT^A31 
                                ///////// si falta el customerepisode no sale el segmento PV1
                                HL7MessagingProcessor.ResetEntities(Entities);
                                HL7MessagingProcessor.ResetBLs(BLs);
                                HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT31);
                                /////////////////////////////////////////////////////
                            }
                        }
                        return customer;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                homonymPersons = null;
                return customer;
            }
        }

        public CustomerEntity GetCustomer(int customerID)
        {
            try
            {
                return this.GetCustomer(customerID, false, false, false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEntity[] GetCustomers(int[] customerIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) return null;
                customerIDs = customerIDs.OrderBy(id => id).Distinct().ToArray();

                PersonBL personBL = new PersonBL();

                DataSet ds = DataAccess.CustomerDA.GetCustomersByIDs(customerIDs);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
                {
                    #region Person

                    List<Tuple<int, int>> parCustomerPerson = new List<Tuple<int,int>>();
                    List<int> profileIDs = new List<int>();
                    List<int> currentAdmissionIDs = new List<int>();
                    List<int> customerClassificationIDs = new List<int>();
                    List<int> organizationIDs = new List<int>();

                    foreach (DataRow row in ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows)
                    {
                        parCustomerPerson.Add(new Tuple<int, int>(Convert.ToInt32(row["ID"]), Convert.ToInt32(row["PersonID"])));
                        profileIDs.Add(Convert.ToInt32(row["ProfileID"]));
                        currentAdmissionIDs.Add(Convert.ToInt32(row["CurrentAdmissionID"]));
                        customerClassificationIDs.Add(Convert.ToInt32(row["CustomerClassificationID"]));
                        organizationIDs.Add(Convert.ToInt32(row["OrganizationID"]));
                    }

                    profileIDs = profileIDs.OrderBy(id => id).Distinct().ToList();
                    currentAdmissionIDs = currentAdmissionIDs.OrderBy(id => id).Distinct().ToList();
                    customerClassificationIDs = customerClassificationIDs.OrderBy(id => id).Distinct().ToList();
                    organizationIDs = organizationIDs.OrderBy(id => id).Distinct().ToList();

                    PersonEntity[] myPersons = personBL.GetPersonByCustomerIDs(customerIDs);
                    #endregion

                    DataSet ds2;

                    #region Customer Profile

                    if (profileIDs.Count > 0)
                    {
                        ds2 = DataAccess.ProfileDA.GetProfileByIDs(profileIDs.ToArray());
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ProfileTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ProfileTable].Copy();
                            ds.Tables.Add(dt);
                        }
                    }

                    #endregion

                    #region Customer Admission

                    if (currentAdmissionIDs.Count > 0)
                    {
                        ds2 = DataAccess.CustomerAdmissionDA.GetCustomerAdmissionByIDs(currentAdmissionIDs.ToArray());
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable].Copy();
                            ds.Tables.Add(dt);

                            if ((dt != null) && (dt.Rows.Count > 0))
                            {
                                List<int> locationIDs = new List<int>();
                                foreach (DataRow row in dt.Rows)
                                    locationIDs.Add(Convert.ToInt32(row["CurrentLocationID"]));

                                locationIDs = locationIDs.OrderBy(id => id).Distinct().ToList();

                                ds2 = DataAccess.LocationDA.GetLocationByIDs(locationIDs.ToArray());
                                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.LocationTable)))
                                {
                                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.LocationTable].Copy();
                                    ds.Tables.Add(dt);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Customer Classification
                    
                    if (customerClassificationIDs.Count > 0)
                    {
                        ds2 = DataAccess.CustomerClassificationDA.GetCustomerClassificationByIDs(customerClassificationIDs.ToArray());
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerClassificationTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerClassificationTable].Copy();
                            ds.Tables.Add(dt);
                        }
                    }
                    #endregion

                    #region Organization
                    
                    if (organizationIDs.Count > 0)
                    {
                        ds2 = DataAccess.OrganizationDA.GetOrganizationByIDs(organizationIDs.ToArray());
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.OrganizationTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.OrganizationTable].Copy();
                            ds.Tables.Add(dt);
                        }
                    }
                    #endregion

                    #region RelatedCHNumbers
                    ds2 = DataAccess.CustomerRelatedCHNumberDA.GetByCustomer(customerIDs);
                    if ((ds2.Tables != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedCHNumberTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    CustomerAdvancedAdapter customerAdapter = new CustomerAdvancedAdapter();
                    CustomerEntity[] customers = customerAdapter.GetData(ds);

                    foreach (var person in myPersons)
                    {
                        int personID = parCustomerPerson.FirstOrDefault(f => f.Item2 == person.ID).Item2;
                        int customerID = parCustomerPerson.FirstOrDefault(f => f.Item2 == person.ID).Item1;

                        var cust = customers.FirstOrDefault(c => c.ID == customerID);
                        cust.Person = person;

                        //Task tLog = Task.Factory.StartNew(() =>
                        //{
                            LOPDLogger.Write(EntityNames.CustomerEntityName, customerID, ActionType.View);
                        //});
                    }                    

                    return customers;
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

        //public CustomerEntity GetCustomer(int customerID, bool getNOKs, bool getCCPs, bool getCCOs)
        //{
        //    try
        //    {
        //        //int t1 = Environment.TickCount;
        //        //CustomerEntity customer = GetCustomer(customerID);
        //        //System.Diagnostics.Trace.WriteLine("GetCustomer():" + (Environment.TickCount - t1).ToString());
        //        PersonBL personBL = new PersonBL();

        //        DataSet ds = DataAccess.CustomerDA.GetCustomer(customerID);
        //        if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
        //        {
        //            #region Person
        //            int personID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["PersonID"].ToString(), 0);
        //            if (personID <= 0)
        //            {
        //                throw new Exception(Properties.Resources.ERROR_CustomerPersonNotFound);
        //            }
        //            PersonEntity myPerson = personBL.GetPerson(personID);
        //            #endregion

        //            DataSet ds2;

        //            #region Customer Profile
        //            int profileID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["ProfileID"].ToString(), 0);
        //            if (profileID > 0)
        //            {
        //                ds2 = DataAccess.ProfileDA.GetProfileByID(profileID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ProfileTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ProfileTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region Customer Admission
        //            int currentAdmissionID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["CurrentAdmissionID"].ToString(), 0);
        //            if (currentAdmissionID > 0)
        //            {
        //                ds2 = DataAccess.CustomerAdmissionDA.GetCustomerAdmission(currentAdmissionID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable].Copy();
        //                    ds.Tables.Add(dt);

        //                    if ((dt != null) && (dt.Rows.Count > 0))
        //                    {
        //                        int locationID = SIIConvert.ToInteger(dt.Rows[0]["CurrentLocationID"].ToString(), 0);
        //                        ds2 = DataAccess.LocationDA.GetLocationByID(locationID);
        //                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.LocationTable)))
        //                        {
        //                            dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.LocationTable].Copy();
        //                            ds.Tables.Add(dt);
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion

        //            #region Customer Classification
        //            int customerClassificationID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["CustomerClassificationID"].ToString(), 0);
        //            if (customerClassificationID > 0)
        //            {
        //                ds2 = DataAccess.CustomerClassificationDA.GetCustomerClassificationByID(customerClassificationID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerClassificationTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerClassificationTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region Organization
        //            int organizationID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["OrganizationID"].ToString(), 0);
        //            if (organizationID > 0)
        //            {
        //                ds2 = DataAccess.OrganizationDA.GetOrganization(organizationID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.OrganizationTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.OrganizationTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region RelatedCHNumbers
        //            ds2 = DataAccess.CustomerRelatedCHNumberDA.GetByCustomer(customerID);
        //            if ((ds2.Tables != null)
        //                && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedCHNumberTable))
        //                && (ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            CustomerAdvancedAdapter customerAdapter = new CustomerAdvancedAdapter();
        //            CustomerEntity[] customers = customerAdapter.GetData(ds);
        //            CustomerEntity result = customers[0];
        //            result.Person = myPerson;

        //            #region NOKs
        //            //t1 = Environment.TickCount;
        //            if (getNOKs)
        //            {
        //                List<NOKEntity> noks = new List<NOKEntity>();
        //                ds2 = DataAccess.NOKDA.GetNOKs(customerID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.NOKListDTOTable)))
        //                {
        //                    foreach (DataRow row in ds2.Tables[SII.HCD.Administrative.Entities.TableNames.NOKListDTOTable].Rows)
        //                    {
        //                        int nokID = SIIConvert.ToInteger(row["ID"].ToString(), 0);
        //                        if (nokID > 0)
        //                            noks.Add(NOKBL.GetNOK(nokID));
        //                    }
        //                }

        //                result.NOKs = noks.Count > 0 ? noks.ToArray() : null;
        //            }
        //            //System.Diagnostics.Trace.WriteLine("getNOKs:" + (Environment.TickCount - t1).ToString());
        //            #endregion

        //            #region Customer Contact Persons
        //            //t1 = Environment.TickCount;
        //            if (getCCPs)
        //            {
        //                List<CustomerContactPersonEntity> customerContactPersons = new List<CustomerContactPersonEntity>();
        //                ds2 = DataAccess.CustomerContactPersonDA.GetCustomerContactPersons(customerID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonListDTOTable)))
        //                {
        //                    foreach (DataRow row in ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonListDTOTable].Rows)
        //                    {
        //                        int customerContactPersonID = SIIConvert.ToInteger(row["ID"].ToString(), 0);
        //                        if (customerContactPersonID > 0)
        //                            customerContactPersons.Add(CustomerContactPersonBL.GetCustomerContactPerson(customerContactPersonID));
        //                    }
        //                }

        //                result.ContactPersons = customerContactPersons.Count > 0 ? customerContactPersons.ToArray() : null;
        //            }
        //            //System.Diagnostics.Trace.WriteLine("getCCPs:" + (Environment.TickCount - t1).ToString());
        //            #endregion

        //            #region Customer Contact Organizations
        //            //t1 = Environment.TickCount;
        //            if (getCCOs)
        //            {
        //                List<CustomerContactOrganizationEntity> customerContactOrganizations = new List<CustomerContactOrganizationEntity>();
        //                ds2 = DataAccess.CustomerContactOrganizationDA.GetCustomerContactOrganizations(customerID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationListDTOTable)))
        //                {
        //                    foreach (DataRow row in ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationListDTOTable].Rows)
        //                    {
        //                        int customerContactOrganizationID = SIIConvert.ToInteger(row["ID"].ToString(), 0);
        //                        if (customerContactOrganizationID > 0)
        //                            customerContactOrganizations.Add(CustomerContactOrganizationBL.GetCustomerContactOrganization(customerContactOrganizationID));
        //                    }
        //                }

        //                result.ContactOrganizations = customerContactOrganizations.Count > 0 ? customerContactOrganizations.ToArray() : null;
        //            }
        //            //System.Diagnostics.Trace.WriteLine("getCCOs:" + (Environment.TickCount - t1).ToString());
        //            #endregion

        //            LOPDLogger.Write(EntityNames.CustomerEntityName, result.ID, ActionType.View);

        //            return result;
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}
        public CustomerEntity GetCustomer(int customerID, bool getNOKs, bool getCCPs, bool getCCOs)
        {
            try
            {
                PersonBL personBL = new PersonBL();
                int personID = personBL.obtenerPersonID_From_Customer(customerID);
                if (personID == 0) throw new Exception(Properties.Resources.ERROR_CustomerPersonNotFound);

                DataSet ds = new DataSet();
                PersonEntity myPerson = null;

                var HiloPerson = System.Threading.Tasks.Task.Factory.StartNew(() => myPerson = personBL.GetPerson(personID));
                ds = DataAccess.CustomerDA.GetCustomer(customerID, getNOKs, getCCPs, getCCOs);

                if ((ds.Tables != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerTable)) && (ds.Tables[Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());

                    CustomerAdvancedAdapter customerAdapter = new CustomerAdvancedAdapter();
                    CustomerEntity[] customers = customerAdapter.GetData(ds2);
                    CustomerEntity result = customers[0];

                    #region NOKs
                    //t1 = Environment.TickCount;
                    List<NOKEntity> noks = new List<NOKEntity>();
                    var tNOKs = System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (getNOKs)
                            {                                
                                if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.NOKListDTOTable)))
                                {
                                    foreach (DataRow row in ds.Tables[SII.HCD.Administrative.Entities.TableNames.NOKListDTOTable].Rows)
                                    {
                                        int nokID = SIIConvert.ToInteger(row["ID"].ToString(), 0);
                                        if (nokID > 0) noks.Add(NOKBL.GetNOK(nokID));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service);
                        }
                    });					
                    //System.Diagnostics.Trace.WriteLine("getNOKs:" + (Environment.TickCount - t1).ToString());
                    #endregion

                    #region Customer Contact Persons
                    //t1 = Environment.TickCount;
                    List<CustomerContactPersonEntity> customerContactPersons = new List<CustomerContactPersonEntity>();
                    var tCCPs = System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (getCCPs)
                            {                                
                                if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonListDTOTable)))
                                {
                                    foreach (DataRow row in ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonListDTOTable].Rows)
                                    {
                                        int customerContactPersonID = SIIConvert.ToInteger(row["ID"].ToString(), 0);
                                        if (customerContactPersonID > 0)
                                            customerContactPersons.Add(CustomerContactPersonBL.GetCustomerContactPerson(customerContactPersonID));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service);
                        }
                    });					
                    //System.Diagnostics.Trace.WriteLine("getCCPs:" + (Environment.TickCount - t1).ToString());
                    #endregion

                    #region Customer Contact Organizations
                    //t1 = Environment.TickCount;
                    List<CustomerContactOrganizationEntity> customerContactOrganizations = new List<CustomerContactOrganizationEntity>();
                    var tCCOs = System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (getCCOs)
                            {
                                ds2 = DataAccess.CustomerContactOrganizationDA.GetCustomerContactOrganizations(customerID);
                                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationListDTOTable)))
                                {
                                    foreach (DataRow row in ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationListDTOTable].Rows)
                                    {
                                        int customerContactOrganizationID = SIIConvert.ToInteger(row["ID"].ToString(), 0);
                                        if (customerContactOrganizationID > 0)
                                            customerContactOrganizations.Add(CustomerContactOrganizationBL.GetCustomerContactOrganization(customerContactOrganizationID));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service);
                        }
                    });					
                    //System.Diagnostics.Trace.WriteLine("getCCOs:" + (Environment.TickCount - t1).ToString());
                    #endregion

                    LOPDLogger.Write(EntityNames.CustomerEntityName, result.ID, ActionType.View);
                    ds.Dispose();
                    ds2.Dispose();

                    HiloPerson.Wait();
                    result.Person = myPerson;
                    tNOKs.Wait();
                    result.NOKs = noks.Count > 0 ? noks.ToArray() : null;
                    tCCPs.Wait();
                    result.ContactPersons = customerContactPersons.Count > 0 ? customerContactPersons.ToArray() : null;
                    tCCOs.Wait();
                    result.ContactOrganizations = customerContactOrganizations.Count > 0 ? customerContactOrganizations.ToArray() : null;
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

        public CustomerEntity GetCustomerPrescription(int customerID)
        {
            try
            {
                PersonBL personBL = new PersonBL();

                DataSet ds = DataAccess.CustomerDA.GetCustomer(customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
                {
                    #region Person
                    int personID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["PersonID"].ToString(), 0);
                    if (personID <= 0)
                    {
                        throw new Exception(Properties.Resources.ERROR_CustomerPersonNotFound);
                    }
                    PersonEntity myPerson = personBL.GetPerson(personID);
                    #endregion

                    DataSet ds2;

                    #region RelatedCHNumbers
                    ds2 = DataAccess.CustomerRelatedCHNumberDA.GetByCustomer(customerID);
                    if ((ds2.Tables != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedCHNumberTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    CustomerAdvancedAdapter customerAdapter = new CustomerAdvancedAdapter();
                    CustomerEntity[] customers = customerAdapter.GetData(ds);
                    CustomerEntity result = customers[0];
                    result.Person = myPerson;

                    LOPDLogger.Write(EntityNames.CustomerEntityName, result.ID, ActionType.View);

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

        public CustomerEntity[] GetByIDsForPrescription(int[] customerIDs)
        {
            try
            {
                PersonBL personBL = new PersonBL();

                DataSet ds = DataAccess.CustomerDA.GetCustomersByIDs(customerIDs);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
                {
                    #region Person

                    List<int> personIDs = new List<int>();
                    Hashtable personCustomerRel = new Hashtable();
                    var custs = ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.GetEnumerator();
                    while (custs.MoveNext())
	                {
		                personIDs.Add(Convert.ToInt32(((DataRow)custs.Current)["PersonID"]));
                        
                        personCustomerRel.Add(Convert.ToInt32(((DataRow)custs.Current)["ID"]), Convert.ToInt32(((DataRow)custs.Current)["PersonID"]));
	                }

                    PersonBaseEntity[] myPersons = personBL.GetPersonsBaseByIDs(personIDs.ToArray());
                    #endregion

                    DataSet ds2;

                    #region RelatedCHNumbers
                    ds2 = DataAccess.CustomerRelatedCHNumberDA.GetRelatedCHNumberFromPersons(personIDs.ToArray());
                    if ((ds2.Tables != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedCHNumberTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    CustomerAdvancedAdapter customerAdapter = new CustomerAdvancedAdapter();
                    CustomerEntity[] customers = customerAdapter.GetData(ds);

                    foreach (var cust in customers)
                    {
                        PersonBaseEntity persBaseEnt = myPersons.FirstOrDefault(pers => pers.ID == Convert.ToInt32(personCustomerRel[cust.ID]));
                        cust.Person = new PersonEntity();
                        cust.Person.FirstName = persBaseEnt.FirstName;
                        cust.Person.LastName = persBaseEnt.LastName;
                        cust.Person.LastName2 = persBaseEnt.LastName2;
                    }

                    return customers;
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

        /// <summary>
        /// Carga un cliente con todos sus datos
        /// </summary>
        /// <returns>Customer Entity</returns>
        public CustomerEntity GetFullCustomer(int customerID)
        {
            try
            {
                return GetCustomer(customerID, true, true, true);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        //public CustomerEntity GetCustomerByPersonID(int personID)
        //{
        //    try
        //    {
        //        PersonBL personBL = new PersonBL();
        //        DataSet ds = DataAccess.CustomerDA.GetCustomerByPersonID(personID);

        //        if ((ds.Tables != null)
        //            && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerTable))
        //            && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
        //        {
        //            int customerID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["ID"].ToString(), 0);
        //            int profileID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["ProfileID"].ToString(), 0);
        //            int currentAdmissionID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["CurrentAdmissionID"].ToString(), 0);
        //            int customerClassificationID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["CustomerClassificationID"].ToString(), 0);

        //            DataSet ds2;

        //            #region Customer Profile
        //            if (profileID > 0)
        //            {
        //                ds2 = DataAccess.ProfileDA.GetProfileByID(profileID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ProfileTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ProfileTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region Customer Admission
        //            if (currentAdmissionID > 0)
        //            {
        //                ds2 = DataAccess.CustomerAdmissionDA.GetCustomerAdmission(currentAdmissionID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable].Copy();
        //                    ds.Tables.Add(dt);

        //                    if ((dt != null) && (dt.Rows.Count > 0))
        //                    {
        //                        int locationID = SIIConvert.ToInteger(dt.Rows[0]["CurrentLocationID"].ToString(), 0);
        //                        ds2 = DataAccess.LocationDA.GetLocationByID(locationID);
        //                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.LocationTable)))
        //                        {
        //                            dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.LocationTable].Copy();
        //                            ds.Tables.Add(dt);
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion

        //            #region Customer Classification
        //            if (customerClassificationID > 0)
        //            {
        //                ds2 = DataAccess.CustomerClassificationDA.GetCustomerClassificationByID(customerClassificationID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerClassificationTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerClassificationTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region RelatedCHNumbers
        //            ds2 = DataAccess.CustomerRelatedCHNumberDA.GetByCustomer(customerID);
        //            if ((ds2.Tables != null)
        //                && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedCHNumberTable))
        //                && (ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Person
        //            SII.HCD.BackOffice.Entities.PersonEntity myPerson = personBL.GetPerson(personID);
        //            #endregion

        //            CustomerAdvancedAdapter customerAdapter = new CustomerAdvancedAdapter();
        //            CustomerEntity result = customerAdapter.GetByID(customerID, ds);
        //            result.Person = myPerson;
        //            return result;
        //        }
        //        else return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}

        public CustomerEntity GetCustomerByPersonID(int personID)
        {
            try
            {
                PersonBL personBL = new PersonBL();
                PersonEntity myPerson = null;

                var HiloPerson = System.Threading.Tasks.Task.Factory.StartNew(() => myPerson = personBL.GetPerson(personID));
                DataSet ds = DataAccess.CustomerDA.GetCustomerByPersonID(personID);

                if ((ds.Tables != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerTable)) && (ds.Tables[Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
                {
                    int customerID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["ID"].ToString(), 0);

                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());
 
                    CustomerAdvancedAdapter customerAdapter = new CustomerAdvancedAdapter();
                    CustomerEntity result = customerAdapter.GetByID(customerID, ds2);
                    HiloPerson.Wait();
                    result.Person = myPerson;
                    ds.Dispose();
                    ds2.Dispose();
                    return result;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        public Int64 GetCustomerDBTimeStamp(int customerID)
        {
            try
            {
                return DataAccess.CustomerDA.GetDBTimeStamp(customerID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public TelephoneEntity[] GetCustomerTelephones(int customerID)
        {
            try
            {
                TelephoneAdapter adapter = new TelephoneAdapter();
                DataSet ds = DataAccess.CustomerDA.GetCustomerTelephones(customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.TelephoneTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.TelephoneTable].Rows.Count > 0))
                {
                    return adapter.GetData(ds);
                }

                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public void SendPhones(int customerID)
        {
            try
            {
                //string deviceCode = GetDeviceCode(customerID);
                ServiciosClient service = new ServiciosClient();
                try
                {
                    List<DevicePhoneAction> phones = new List<DevicePhoneAction>();
                    TelephoneEntity[] telephones;

                    DataSet ds = DataAccess.CustomerDA.GetCustomerNOKTelephones(customerID);
                    if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.TelephoneTable))
                        && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.TelephoneTable].Rows.Count > 0))
                    {
                        DataRowCollection rows = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.TelephoneTable].Rows;
                        foreach (DataRow row in rows)
                        {
                            DevicePhoneAction action = new DevicePhoneAction();
                            action.PhoneNumber = row["Telephone"].ToString();
                            action.PhoneName = string.Format("{0} {1} {2}",
                                row["FirstName"].ToString(),
                                row["LastName"].ToString(),
                                row["LastName2"].ToString());
                            action.EmergencyPhone = SIIConvert.ToBoolean(row["EmergencyContactPhone"].ToString());
                            phones.Add(action);
                        }
                    }

                    telephones = GetCustomerTelephones(customerID);
                    foreach (TelephoneEntity entity in telephones)
                    {
                        if (!string.IsNullOrEmpty(entity.Comments))
                        {
                            DevicePhoneAction action = new DevicePhoneAction();
                            action.PhoneNumber = entity.Telephone;
                            action.PhoneName = entity.Comments;
                            action.EmergencyPhone = entity.EmergencyContactPhone;
                            phones.Add(action);
                        }
                    }

                    string[] deviceCodes = GetTelephoneDeviceCodes(customerID);
                    foreach (string deviceCode in deviceCodes)
                    {
                        SyncStatusEntity statusEntity = service.SetPhones(deviceCode, phones.ToArray());
                        if (statusEntity.Status == InterfaceStatus.Error)
                            throw new Exception(statusEntity.Message);

                        Logger.Write(string.Format(Properties.Resources.MSG_PhoneNumbersSentToDevice,
                            customerID, deviceCode), Category.Information, Priority.Low, 0, TraceEventType.Information);

                    }

                    service.Close();
                }
                catch (Exception)
                {
                    service.Abort();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format(Properties.Resources.ERROR_FailedToSendPhoneNumbers,
                    customerID, ex.Message), Category.Error, Priority.High, 0, TraceEventType.Error);
            }
        }

        public void ClearPhones(int customerID)
        {
            try
            {
                string[] deviceCodes = GetTelephoneDeviceCodes(customerID);

                ServiciosClient service = new ServiciosClient();
                try
                {
                    foreach (string deviceCode in deviceCodes)
                    {
                        SyncStatusEntity statusEntity = service.ClearPhones(deviceCode);
                        if (statusEntity.Status == InterfaceStatus.Error)
                            throw new Exception(statusEntity.Message);

                        Logger.Write(string.Format(Properties.Resources.MSG_PhoneNumbersClearedFromDevice,
                            deviceCode), Category.Information, Priority.Low, 0, TraceEventType.Information);

                    }

                    service.Close();
                }
                catch (Exception)
                {
                    service.Abort();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format(Properties.Resources.ERROR_FailedToClearPhoneNumbers,
                    customerID, ex.Message), Category.Error, Priority.High, 0, TraceEventType.Error);
            }
        }

        public CustomerEntity ChangeCustomerOrganization(CustomerEntity customer, OrganizationEntity organization)
        {
            if (customer != null)
            {
                int legalPersonCategoryID = DataAccess.CategoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.LegalPerson);
                if (legalPersonCategoryID <= 0)
                {
                    throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForLegalPerson);
                }
                int orgAsCustomerCategoryID = DataAccess.CategoryDA.GetCategoryIDByCategoryKey((int)CategoryOrganizationKeyEnum.OrgAsCustomer);
                if (orgAsCustomerCategoryID <= 0)
                {
                    throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForOrganizationAsCustomer);
                }

                //Customer related organization check
                if (organization != null)
                {
                    int myCustomerID = DataAccess.CustomerDA.GetCustomerByOrganizationID(organization.ID);
                    if ((myCustomerID > 0) && (myCustomerID != customer.ID))
                    {
                        throw new Exception(Properties.Resources.MSG_OrganizationAlreadyUsedByOtherCustomer);
                    }
                }

                string userName = IdentityUser.GetIdentityUserName();

                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccess.CustomerDA.SetCustomerOrganization(customer.ID, organization.ID, userName);
                    if (customer.RelatedOrganization == null)
                    {
                        DataAccess.PersonCatRelDA.Insert(customer.Person.ID, legalPersonCategoryID, userName);
                        DataAccess.OrganizationCatRelDA.Insert(organization.ID, orgAsCustomerCategoryID, userName);
                    }
                    else
                    {
                        DataAccess.OrganizationCatRelDA.Delete(customer.RelatedOrganization.ID, orgAsCustomerCategoryID);
                        DataAccess.OrganizationCatRelDA.Insert(organization.ID, orgAsCustomerCategoryID, userName);
                    }

                    customer.DBTimeStamp = DataAccess.CustomerDA.GetDBTimeStamp(customer.ID);

                    scope.Complete();
                }

                customer.RelatedOrganization = organization;
            }

            return customer;
        }

        public CustomerEntity RemoveCustomerOrganization(CustomerEntity customer)
        {
            if (customer != null)
            {
                int legalPersonCategoryID = DataAccess.CategoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.LegalPerson);
                if (legalPersonCategoryID <= 0)
                {
                    throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForLegalPerson);
                }
                int orgAsCustomerCategoryID = DataAccess.CategoryDA.GetCategoryIDByCategoryKey((int)CategoryOrganizationKeyEnum.OrgAsCustomer);
                if (orgAsCustomerCategoryID <= 0)
                {
                    throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForOrganizationAsCustomer);
                }

                string userName = IdentityUser.GetIdentityUserName();

                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccess.CustomerDA.SetCustomerOrganization(customer.ID, 0, userName);

                    DataAccess.PersonCatRelDA.Delete(customer.Person.ID, legalPersonCategoryID);
                    DataAccess.OrganizationCatRelDA.Delete(customer.RelatedOrganization.ID, orgAsCustomerCategoryID);

                    customer.DBTimeStamp = DataAccess.CustomerDA.GetDBTimeStamp(customer.ID);
                    scope.Complete();
                }

                customer.RelatedOrganization = null;
            }

            return customer;
        }

        public void InnerUpdateCustomerProfileAndClassification(int customerID, int profileID, int classificationID, int admissionID, int episodeID)
        {
            DataAccess.CustomerDA.UpdateCustomerProfileAndClassification(customerID, profileID, classificationID, admissionID, episodeID);
        }

        //public int GetCurrentEpisodeID(int customerID)
        //{
        //    try
        //    {
        //        return DataAccess.CustomerDA.GetCurrentEpisodeID(customerID);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return 0;
        //    }
        //}

        //public int GetCurrentLocationID(int customerID)
        //{
        //    try
        //    {
        //        return DataAccess.CustomerDA.GetCurrentLocationID(customerID);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return 0;
        //    }
        //}

        //public LocationBaseEntity GetCurrentCustomerLocation(int customerID)
        //{
        //    try
        //    {
        //        int locationID = GetCurrentLocationID(customerID);
        //        if (locationID <= 0)
        //            return null;

        //        LocationBL service = new LocationBL();
        //        return service.GetLocationBase(locationID);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}

        public ProcedureActTreeRealizationDTO[] GetProcessRealization(int customerID, int episodeID)
        {
            try
            {
                ProcedureActTreeRealizationDTOAdapter myAdapter = new ProcedureActTreeRealizationDTOAdapter();
                ProcedureActTreeRealizationDTO[] result = null;

                DataSet ds = DataAccess.ProcedureActDA.GetByCustomerIDEpisodeIDLocationID(episodeID);
                ds.Tables.Add(DataAccess.RoutineActDA.GetRoutineActWithProcedureActByEpisodeID(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActRealizationDTOTable].Copy());
                ds.Tables.Add(DataAccess.OrderRequestConsentRelDA.GetOrderRequestConsentRelsByCustomerEpisode(episodeID).Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestConsentRelTable].Copy());
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                    && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                {
                    MergeTable(DataAccess.ConsentPreprintDA.GetConsentPreprintsByOrderRequestConsentRelCustomerEpisode(episodeID),
                        ds, BackOffice.Entities.TableNames.ConsentPreprintTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(BackOffice.Entities.TableNames.ConsentPreprintTable))
                        && (ds.Tables[BackOffice.Entities.TableNames.ConsentPreprintTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelConsentPreprintCustomerEpisode(episodeID),
                            ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                    }
                    MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelCustomerEpisode(episodeID),
                        ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                }

                if (ds.Tables.Contains(SII.HCD.Assistance.Entities.TableNames.RoutineActRealizationDTOTable)
                    && (ds.Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActRealizationDTOTable].Rows.Count > 0))
                {
                    DataRow row = ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable].NewRow();
                    row["ID"] = 0;
                    row["CustomerOrderRequestID"] = 0;
                    row["CustomerProcedureID"] = 0;
                    row["ProcedureID"] = 0;
                    row["ProcedureName"] = Properties.Resources.MSG_OtherActs;
                    row["RoutineControl"] = true;
                    row["RealizationComments"] = String.Empty;
                    row["StartDateTime"] = DBNull.Value;
                    row["EndDateTime"] = DBNull.Value;
                    row["RealizationDayOrder"] = -1;
                    row["RelatedMedicalAct"] = String.Empty;
                    row["Status"] = (int)SII.HCD.BackOffice.Entities.ActionStatusEnum.Completed;
                    row["OrderNumber"] = string.Empty;
                    row["OrderName"] = string.Empty;
                    row["RequestedPhysicianFirstName"] = string.Empty;
                    row["RequestedPhysicianLastName"] = string.Empty;
                    row["RequestedPhysicianLastName2"] = string.Empty;
                    row["RequestedOrderDateTime"] = DBNull.Value;
                    row["DBTimeStamp"] = 0;
                    ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable].Rows.Add(row);
                    ds.Tables.Add(DataAccess.RoutineActResourceRelDA.GetResourcesByRoutineActsWithoutProcedureAct(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineStepActDA.GetStepByRoutineActs(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineStepActDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineActHumanResourceRelDA.GetHumanResourcesByRoutineActsWithoutProcedureAct(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActHumanResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineActEquipmentRelDA.GetEquipmentsByRoutineActsWithoutProcedureAct(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActEquipmentDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineActActRelDA.GetActsByRoutineActsWithoutProcedureAct(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActActDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineActBodySiteRelDA.GetBodySiteDTOsByCustomerEpisode(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActBodySiteDTOTable].Copy());
                }
                if (ds.Tables.Contains(SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable)
                    && (ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable].Rows.Count > 0))
                {
                    ds.Tables.Add(DataAccess.ProcedureActResourceRelDA.GetResourcesByProcedureActs(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.ProcedureActHumanResourceRelDA.GetHumanResourcesByProcedureActs(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActHumanResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.ProcedureActEquipmentRelDA.GetEquipmentsByProcedureActs(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActEquipmentDTOTable].Copy());
                    ds.Tables.Add(DataAccess.ProcedureActActRelDA.GetActsByProcedureActs(episodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActActDTOTable].Copy());

                    result = myAdapter.GetData(ds);
                }

                if (result != null)
                {
                    CustomerObservationBL _customerObservationBL = new CustomerObservationBL();
                    foreach (ProcedureActTreeRealizationDTO procedureAct in result)
                    {
                        if (procedureAct.ID != 0)
                        {
                            procedureAct.RegisteredLayout = _customerObservationBL.GetRegisteredLayoutByCustomerAndProcedureAct(customerID, procedureAct.ID);
                        }
                        else
                        {
                            if ((procedureAct.RoutineActs != null) && (procedureAct.RoutineActs.Length > 0))
                            {
                                foreach (RoutineActTreeRealizationDTO routineAct in procedureAct.RoutineActs)
                                {
                                    routineAct.RegisteredLayout = _customerObservationBL.GetRegisteredLayoutByCustomerAndRoutineAct(customerID, routineAct.ID);
                                }
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public ProcedureActTreeRealizationDTO[] GetProcessRealizationMedicalEpisode(int customerID, int customerMedEpisodeActID, int medicalEpisodeID)
        {
            try
            {
                ProcedureActTreeRealizationDTOAdapter myAdapter = new ProcedureActTreeRealizationDTOAdapter();
                ProcedureActTreeRealizationDTO[] result = null;
                DataSet ds = DataAccess.ProcedureActDA.GetByCustomerMedEpisodeActIDMedcialEpisodeID(customerMedEpisodeActID, medicalEpisodeID);
                ds.Tables.Add(DataAccess.RoutineActDA.GetRoutineActWithProcedureActByCustomerMedEpisodeActIDMedcialEpisodeID(customerMedEpisodeActID,
                    medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActRealizationDTOTable].Copy());

                if (ds.Tables.Contains(Assistance.Entities.TableNames.RoutineActRealizationDTOTable)
                    && (ds.Tables[Assistance.Entities.TableNames.RoutineActRealizationDTOTable].Rows.Count > 0))
                {
                    DataRow row = ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable].NewRow();
                    row["ID"] = 0;
                    row["CustomerProcedureID"] = 0;
                    row["ProcedureID"] = 0;
                    row["ProcedureName"] = Properties.Resources.MSG_OtherActs;
                    row["RoutineControl"] = true;
                    row["RealizationComments"] = String.Empty;
                    row["StartDateTime"] = DBNull.Value;
                    row["EndDateTime"] = DBNull.Value;
                    row["RealizationDayOrder"] = -1;
                    row["RelatedMedicalAct"] = String.Empty;
                    row["Status"] = (int)SII.HCD.BackOffice.Entities.ActionStatusEnum.Completed;
                    row["OrderNumber"] = string.Empty;
                    row["OrderName"] = string.Empty;
                    row["RequestedPhysicianFirstName"] = string.Empty;
                    row["RequestedPhysicianLastName"] = string.Empty;
                    row["RequestedPhysicianLastName2"] = string.Empty;
                    row["RequestedOrderDateTime"] = DBNull.Value;
                    row["DBTimeStamp"] = 0;
                    ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable].Rows.Add(row);

                    ds.Tables.Add(DataAccess.RoutineActResourceRelDA.GetResourcesByCustomerMedEpisodeActIDMedicalEpisodeIDWithoutProcedureAct(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineStepActDA.GetStepByCustomerMedEpisodeActIDMedicalEpisodeID(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineStepActDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineActHumanResourceRelDA.GetHumanResourcesByCustomerMedEpisodeActIDWithoutProcedureAct(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActHumanResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineActEquipmentRelDA.GetEquipmentsByCustomerEpisodeActIDMedicalEpisodeIDWithoutProcedureAct(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActEquipmentDTOTable].Copy());
                    ds.Tables.Add(DataAccess.RoutineActActRelDA.GetActsByCustomerEpisodeActIDMedicalEpisodeIDWithoutProcedureAct(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActActDTOTable].Copy());
                }
                if (ds.Tables.Contains(SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable)
                    && (ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActRealizationDTOTable].Rows.Count > 0))
                {
                    ds.Tables.Add(DataAccess.ProcedureActResourceRelDA.GetResourcesByCustomerEpisodeActIDMedicalEpisodeID(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.ProcedureActHumanResourceRelDA.GetHumanResourcesByCustomerEpisodeActIDMedicalEpisodeID(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActHumanResourceDTOTable].Copy());
                    ds.Tables.Add(DataAccess.ProcedureActEquipmentRelDA.GetEquipmentsByCustomerEpisodeActIDMedicalEpisodeID(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActEquipmentDTOTable].Copy());
                    ds.Tables.Add(DataAccess.ProcedureActActRelDA.GetActsByCustomerEpisodeActIDMedicalEpisodeID(customerMedEpisodeActID,
                        medicalEpisodeID).Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActActDTOTable].Copy());

                    result = myAdapter.GetData(ds);
                }

                if (result != null)
                {
                    CustomerObservationBL _customerObservationBL = new CustomerObservationBL();
                    foreach (ProcedureActTreeRealizationDTO procedureAct in result)
                    {
                        if (procedureAct.ID != 0)
                        {
                            procedureAct.RegisteredLayout = _customerObservationBL.GetRegisteredLayoutByCustomerAndProcedureAct(customerID, procedureAct.ID);
                        }
                        else
                        {
                            if ((procedureAct.RoutineActs != null) && (procedureAct.RoutineActs.Length > 0))
                            {
                                foreach (RoutineActTreeRealizationDTO routineAct in procedureAct.RoutineActs)
                                {
                                    routineAct.RegisteredLayout = _customerObservationBL.GetRegisteredLayoutByCustomerAndRoutineAct(customerID, routineAct.ID);
                                }
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.IDDescriptionEntity[] GetIDDescriptionCustomers()
        {
            try
            {
                CommonEntities.IDDescriptionAdapter myAdapter = new CommonEntities.IDDescriptionAdapter();

                DataSet ds = DataAccess.CustomerDA.GetIDDescriptionAllCustomers();
                if (ds.Tables.Contains(CommonEntities.TableNames.IDDescriptionTable)
                    && (ds.Tables[CommonEntities.TableNames.IDDescriptionTable].Rows.Count > 0))
                {
                    return myAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public decimal GetCustomerDebtQty(int customerID)
        {
            try
            {
                return DataAccess.CustomerDA.GetCustomerDebtQty(customerID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public string GetRelationship(int customerID, int personID)
        {
            try
            {
                return DataAccess.CustomerDA.GetRelationship(customerID, personID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return string.Empty;
            }
        }

        /// <summary>
        /// ampliaciones realizadas para dar solución al laboratorio
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomerEntity GetCustomerByCHNumber(string chNumber)
        {
            try
            {
                int customerID = DataAccess.CustomerDA.GetCustomerByCHNumber(chNumber);
                return this.GetCustomer(customerID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEntity GetCustomerByCHNumber(string chNumber, int careCenterID)
        {
            try
            {
                int customerID = (careCenterID > 0)
                    ? DataAccess.CustomerDA.GetCustomerByCHNumber(chNumber, careCenterID)
                    : DataAccess.CustomerDA.GetCustomerByCHNumber(chNumber);
                return this.GetCustomer(customerID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEntity GetCustomerByIdentificationNumber(string identificationNumber)
        {
            try
            {
                int customerID = DataAccess.CustomerDA.GetCustomerIdentifierNumber(identificationNumber);
                return this.GetCustomer(customerID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEntity GetCustomerByPersonData(string firstName, string lastName, string idNumber, string identifierType)
        {
            try
            {
                IdentifierTypeDA _identifierTypeDA = new IdentifierTypeDA();
                int identifierTypeID = _identifierTypeDA.GetIdentifierTypeID(identifierType);
                int customerID = DataAccess.CustomerDA.GetCustomerByPersonData(firstName, lastName, idNumber, identifierTypeID);
                return this.GetCustomer(customerID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEntity SaveExternalCustomer(string firstName, string lastName, string lastName2, string idNumber, string telephone, DateTime birthDate, int sex)
        {
            try
            {
                AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
                string defaultIdentifierType = administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].DefaultValue;
                IdentifierTypeBL _identifierTypeBL = new IdentifierTypeBL();
                IdentifierTypeEntity identifierType = _identifierTypeBL.GetIdentifierType(_identifierTypeBL.GetIdentifierTypeID(defaultIdentifierType));
                CategoryBL categoryBL = new CategoryBL();
                CategoryEntity category = categoryBL.GetCategory(categoryBL.GetCategoryKeyID((int)CategoryPersonKeyEnum.Customer));
                PersonCategoryEntity personCategory = new PersonCategoryEntity(0, 0, category, 0);
                personCategory.EditStatus.New();
                PersonIdentifierEntity personIdentifier = new PersonIdentifierEntity(0, idNumber, 0, identifierType, 0);
                personIdentifier.EditStatus.New();
                TelephoneEntity _telephone = new TelephoneEntity(0, telephone, string.Empty, string.Empty, false, 0);
                _telephone.EditStatus.New();
                PersonTelephoneEntity personTelephone = new PersonTelephoneEntity(0, 0, _telephone, 0);
                personTelephone.EditStatus.New();
                SensitiveDataEntity sensitiveData = new SensitiveDataEntity(0, birthDate, 0,
                    (sex == 1) ? BackOffice.Entities.SexEnum.Male : (sex == 2) ? BackOffice.Entities.SexEnum.Female : BackOffice.Entities.SexEnum.Unknown,
                    BackOffice.Entities.ReligiousEnum.Unknown, BackOffice.Entities.LanguageEnum.Unknown, BackOffice.Entities.EducationLevelEnum.Unknown,
                    BackOffice.Entities.MaritalStatusEnum.Unknown, string.Empty, string.Empty, string.Empty,
                    null, string.Empty, 0);
                sensitiveData.EditStatus.New();
                PersonEntity person = new PersonEntity(0, firstName, lastName, lastName2, CommonEntities.StatusEnum.Active, 0,
                                null, null, string.Empty,
                                new List<PersonCategoryEntity> { personCategory }.ToArray(),
                                new List<PersonIdentifierEntity> { personIdentifier }.ToArray(),
                                new List<PersonTelephoneEntity> { personTelephone }.ToArray(),
                                0, 0, 0, false, false, string.Empty, sensitiveData, DateTime.Now);
                CustomerEntity customer = new CustomerEntity(0, person, null, null, null, null, null, null, null, null, string.Empty, string.Empty, null, string.Empty, false, false, null, null,
                    PersonConfidentialityEnum.Normal, PersonConfidentialityEnum.Normal, null, string.Empty, DateTime.Now, 0);
                customer.EditStatus.New();
                PersonAddressListDTO[] homonymPersons = null;
                return this.Save(customer, false, out homonymPersons);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.AddInTokenBaseEntity[] GetCustomerLookupListToken()
        {
            try
            {
                Collection<AddInToken> tokens = AddInRepository.GetAddInTokens<CustomerLookupHostView>();
                List<CommonEntities.AddInTokenBaseEntity> result = null;
                if (tokens != null)
                {
                    result = new List<CommonEntities.AddInTokenBaseEntity>();
                    foreach (AddInToken token in tokens)
                    {
                        result.Add(new CommonEntities.AddInTokenBaseEntity(token.Name, token.Description, token.Publisher, token.Version));
                    }
                }
                return (result != null) ? result.ToArray() : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public Customer[] GetCustomersLookupExternal(string addin, string firstName, string lastName, string lastName2, string identifierType, string identifier,
            string categoryCode, string profileCode, string customerIdentificationName, string visitNumber, string visitType, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                //if (string.IsNullOrWhiteSpace(addin))
                //    throw new ArgumentException(string.Format(Properties.Resources.ERROR_CustomerLookupProviderAddinNotFound, addin));

                //CustomerLookupParameters parameters = new CustomerLookupParameters(firstName, lastName, lastName2,
                //    identifierType, identifier, categoryCode, profileCode, customerIdentificationName, visitNumber, visitType);

                //Customer[] customers = null;
                //if (!string.IsNullOrEmpty(visitNumber))
                //{
                //    CustomerLookupHostView instance = GetAddinInstanceCustomerLookup(addin);
                //    if (instance.SupportVisitLookup())
                //    {
                //        customers = instance.LookupVisit(parameters, out maxRecordsExceded);
                //    }
                //    else throw new Exception("Esta configuración de busqueda externa no soporta el filtro por número de visita externa");
                //}
                //else
                //{
                //    CustomerLookupHostView instance = GetAddinInstanceCustomerLookup(addin);
                //    if (instance.SupportCustomerLookup())
                //    {
                //        customers = instance.Lookup(parameters, out maxRecordsExceded);
                //    }
                //    else throw new Exception("Esta configuración de busqueda externa no soporta el filtro por número de identificador externo");
                //}

                //return customers;
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonEntity FindPersonWithCustomerFindRequest(string firstName, string lastName, string idNumber, string identifierType)
        {
            try
            {
                CustomerFindRequest filters = new CustomerFindRequest(firstName, lastName, idNumber, false, false, false, identifierType);
                AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
                if ((administrativeConfig != null) && (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null))
                {
                    foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.CustomerEntity.Attributes)
                    {
                        if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                        {
                            filters.MandatoryFirstName = true;
                        }

                        if ((attrib.Name == "LastName") && (attrib.Mandatory))
                        {
                            filters.MandatoryLastName = true;
                        }

                        if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))
                        {
                            filters.MandatoryIdentifierType = true;
                        }
                    }
                }
                int peronID = DataAccess.PersonDA.GetPerson(filters);
                return this.GetPerson(DataAccess.PersonDA.GetPerson(filters));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEntity FindCustomerWithCustomerFindRequest(string firstName, string lastName, string idNumber, string identifierType)
        {
            try
            {
                CustomerFindRequest filters = new CustomerFindRequest(firstName, lastName, idNumber, false, false, false, identifierType);
                AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
                if ((administrativeConfig != null) && (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null))
                {
                    foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.CustomerEntity.Attributes)
                    {
                        if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                        {
                            filters.MandatoryFirstName = true;
                        }

                        if ((attrib.Name == "LastName") && (attrib.Mandatory))
                        {
                            filters.MandatoryLastName = true;
                        }

                        if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))
                        {
                            filters.MandatoryIdentifierType = true;
                        }
                    }
                }
                int peronID = DataAccess.PersonDA.GetPerson(filters);
                return this.GetCustomerByPersonID(DataAccess.PersonDA.GetPerson(filters));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public bool AllowNewCustomerRegistrationStep()
        {
            CommonEntities.AddInTokenBaseEntity[] tokens = this.GetRegistrationStepListToken();
            bool result = true;
            if ((tokens != null) && (tokens.Length > 0))
            {
                foreach (CommonEntities.AddInTokenBaseEntity addin in tokens)
                {
                    try
                    {
                        RegistrationStepHostView instance = GetAddinInstanceRegistrationStep(addin.AddinName);
                        if (instance.RegistrationFirstInHis())
                        {
                            if (!instance.AllowNewCustomer())
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }

            return result;
        }

        //public bool AllowEditCustomerLookupExternal(string addin)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(addin))
        //            throw new ArgumentException(string.Format(
        //                Properties.Resources.ERROR_CustomerLookupProviderAddinNotFound, addin));

        //        CustomerLookupHostView instance = GetAddinInstance(addin);
        //        return instance.AllowCustomerEdit();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return true;
        //    }
        //}

        //public long GetContractFieldsOfCustomerLookupExternal(string addin)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(addin))
        //            throw new ArgumentException(string.Format(
        //                Properties.Resources.ERROR_CustomerLookupProviderAddinNotFound, addin));

        //        CustomerLookupHostView instance = GetAddinInstance(addin);
        //        return instance.GetContractFields();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return 0;
        //    }
        //}

        //public bool SupportCustomerLookupExternal()
        //{
        //    try
        //    {
        //        CommonEntities.AddInTokenBaseEntity[] tokens = this.GetCustomerLookupListToken();
        //        bool result = false;
        //        if ((tokens != null) && (tokens.Length > 0))
        //        {
        //            foreach (CommonEntities.AddInTokenBaseEntity addin in tokens)
        //            {
        //                try
        //                {
        //                    CustomerLookupHostView instance = GetAddinInstanceCustomerLookup(addin.AddinName);
        //                    if (instance.SupportCustomerLookup())
        //                    {
        //                        result = true;
        //                        break;
        //                    }
        //                }
        //                catch { }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return true;
        //    }
        //}

        //public bool SupportVisitLookupExternal()
        //{
        //    try
        //    {
        //        CommonEntities.AddInTokenBaseEntity[] tokens = this.GetCustomerLookupListToken();
        //        bool result = false;
        //        if ((tokens != null) && (tokens.Length > 0))
        //        {
        //            foreach (CommonEntities.AddInTokenBaseEntity addin in tokens)
        //            {
        //                try
        //                {
        //                    CustomerLookupHostView instance = GetAddinInstanceCustomerLookup(addin.AddinName);
        //                    if (instance.SupportVisitLookup())
        //                    {
        //                        result = true;
        //                        break;
        //                    }
        //                }
        //                catch { }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return true;
        //    }
        //}

        public CustomerBaseDTO GetCustomerBaseDTOByID(int customerID)
        {
            try
            {
                CustomerBaseWithIdentifierDTOAdapter adapter = new CustomerBaseWithIdentifierDTOAdapter();

                DataSet ds = DataAccess.CustomerDA.GetCustomerBaseDTOWithIdentifierByID(customerID, GetDefaultIdentifierType());
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable].Rows.Count > 0))
                {
                    CustomerBaseDTO[] customers = adapter.GetData(ds);
                    return (customers != null && customers.Length > 0) ? customers[0] : null;
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

        public CustomerBaseDTO[] GetCustomerBaseDTOByDemographics(string firstName, string lastName,
            string lastName2, string identifierNumber, string identifierType)
        {
            try
            {
                int identifierTypeID = 0;
                if (!string.IsNullOrWhiteSpace(identifierNumber))
                {
                    IdentifierTypeBL identifierBL = new IdentifierTypeBL();
                    identifierTypeID = identifierBL.GetIdentifierTypeID(identifierType);
                }

                CustomerBaseWithIdentifierDTOAdapter adapter = new CustomerBaseWithIdentifierDTOAdapter();

                DataSet ds = DataAccess.CustomerDA.GetCustomerBaseDTOByDemographics(identifierTypeID,
                    identifierNumber, firstName, lastName, lastName2);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable].Rows.Count > 0))
                {
                    return adapter.GetData(ds);
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

        public CommonEntities.IDDescriptionEntity[] GetIDDescriptionCustomers(string firstName, string lastName, string lastName2, string identifier, string identifierType)
        {
            try
            {
                CommonEntities.IDCustomerNameAdapter idDescriptionAdapter = new CommonEntities.IDCustomerNameAdapter();
                int defaultIdentifierID = 0;
                if (!string.IsNullOrWhiteSpace(identifierType))
                {
                    IdentifierTypeBL identifierBL = new IdentifierTypeBL();
                    defaultIdentifierID = identifierBL.GetIdentifierTypeID(identifierType);
                }
                DataSet ds = DataAccess.CustomerDA.GetIDDescriptionCustomers(firstName, lastName, lastName2, identifier, defaultIdentifierID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                {
                    CommonEntities.IDDescriptionEntity[] customers = idDescriptionAdapter.GetData(ds);
                    return customers;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        public string GetCustomerFullName(int customerID)
        {
            try
            {
                if (customerID <= 0) return string.Empty;
                DataSet ds = DataAccess.CustomerDA.GetIDDescriptionCustomer(customerID);
                if ((ds.Tables != null) && ds.Tables.Contains(Common.Entities.TableNames.IDDescriptionTable)
                    && (ds.Tables[Common.Entities.TableNames.IDDescriptionTable].Rows.Count > 0))
                {
                    return CommonEntities.DescriptionBuilder.PersonBuildName(ds.Tables[Common.Entities.TableNames.IDDescriptionTable].Rows[0]["FirstName"].ToString(),
                        ds.Tables[Common.Entities.TableNames.IDDescriptionTable].Rows[0]["LastName"].ToString(), ds.Tables[Common.Entities.TableNames.IDDescriptionTable].Rows[0]["LastName2"].ToString());
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return string.Empty;
            }
        }

        public CustomerEntity FindCustomerWithCustomerExternal(Customer customerExternal, string addin)
        {
            try
            {
                //Comentado por cambios en el AddIn

                //if (string.IsNullOrWhiteSpace(addin))
                //    throw new ArgumentException(string.Format(Properties.Resources.ERROR_CustomerLookupProviderAddinNotFound, addin));

                //CustomerLookupHostView instance = GetAddinInstanceCustomerLookup(addin);
                //Identifier identifier = instance.HisPatientIdentifier();

                //if (identifier == null)
                //    throw new ArgumentException(string.Format(Properties.Resources.MSG_ErrorNotDefinedIdentifiertoCustomerLookupAddin, addin));

                //int customerID = 0;
                //if ((customerExternal.OtherIdentifiers != null) && (customerExternal.OtherIdentifiers.Length > 0))
                //{
                //    Identifier idSincronization = Array.Find(customerExternal.OtherIdentifiers, (Identifier i1) => i1.IdentifierTypeName == identifier.IdentifierTypeName);

                //    if (idSincronization == null)
                //        throw new ArgumentException(string.Format(Properties.Resources.MSG_ErrorNotFoundIdentifiertoCustomerLookupAddin, addin));

                //    customerID = DataAccess.CustomerDA.GetIDFromIdentificationNumber(idSincronization.IDNumber, idSincronization.IdentifierTypeName);
                //}
                //else throw new ArgumentException(string.Format(Properties.Resources.MSG_ErrorNotFoundIdentifiertoCustomerLookupAddin, addin));

                //return (customerID > 0) ? this.GetCustomer(customerID) : null;
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.IDDescriptionWithMasterIDEntity[] GetActiveCustomerInsurers(int customerID)
        {
            try
            {
                CustomerPolicyDA _customerPolicyDA = new CustomerPolicyDA();
                DataSet ds = _customerPolicyDA.GetInsurersActiveByCustomerID(customerID);
                if (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerPolicyTable)
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerPolicyTable].Rows.Count > 0))
                {
                    return (from row in ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerPolicyTable].AsEnumerable()
                            select
                            new CommonEntities.IDDescriptionWithMasterIDEntity(
                                row["InsurerID"] as int? ?? 0,
                                row["Name"] as string,
                                row["HistoryInsurerID"] as int? ?? 0)
                            ).ToArray();
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        public int GetLastInsurerIDCustomerEpisodeByCustomerID(int customerID)
        {
            try
            {
                CustomerPolicyDA _customerPolicyDA = new CustomerPolicyDA();
                return _customerPolicyDA.GetLastInsurerIDCustomerEpisodeByCustomerID(customerID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }

        }

        public int GetCustomerIDByDeviceCode(string deviceCode)
        {
            try
            {
                return DataAccess.CustomerDA.GetCustomerIDByDeviceCode(deviceCode);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }

        }

        public int GetNumberOfCustomers(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return DataAccess.CustomerDA.GetNumberOfCustomers(startDateTime, endDateTime);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }

        }

        public bool ThereAreNCareCentersWithCodeGenerator()
        {
            CommonEntities.ElementEntity _customerMetadata = base.GetElementByName(EntityNames.CustomerEntityName, ElementBL);
            int _chNumberAttributeID = 0;
            if ((_customerMetadata != null)
                && (_customerMetadata.Attributes != null))
            {
                CommonEntities.AttributeEntity attribute = _customerMetadata.Attributes.Where(attr => attr.Name == "CHNumber").Select(attr => attr).FirstOrDefault();
                if (attribute != null)
                    _chNumberAttributeID = attribute.ID;
            }
            return DataAccess.CareCenterRelatedCodeGeneratorDA.ThereAreNCareCentersWithCodeGenerator(_customerMetadata.ID, _chNumberAttributeID);
        }

        public SII.HCD.Administrative.Entities.CustomerCareCenterDTO[] GetCustomerCareCenterInfo(PersonSpecification specification, out Boolean maxRecordsExceded,
            out CommonEntities.GenericErrorLogEntity[] errors)
        {
            List<SII.HCD.Administrative.Entities.CustomerCareCenterDTO> customers = new List<SII.HCD.Administrative.Entities.CustomerCareCenterDTO>();

            PersonBL personBL = new PersonBL();
            PersonLookupDTO[] persons = personBL.GetPersons(specification, out maxRecordsExceded, out errors);

            if ((persons != null) && (persons.Length > 0))
            {
                bool chNumberByCareCenter = ThereAreNCareCentersWithCodeGenerator();
                int defaultIdentifierTypeID = 0;
                string defaultIdentifierTypeName = string.Empty;

                CommonEntities.ElementEntity personIdentierMetadata = base.GetElementByName(EntityNames.PersonIdentifierEntityName, ElementBL);
                defaultIdentifierTypeName = (personIdentierMetadata != null)
                                                        ? personIdentierMetadata.GetDefaultValue("IdentifierType")
                                                        : string.Empty;
                if (!string.IsNullOrWhiteSpace(defaultIdentifierTypeName))
                {
                    IdentifierTypeBL itBL = new IdentifierTypeBL();
                    defaultIdentifierTypeID = itBL.GetIdentifierTypeID(defaultIdentifierTypeName);
                }

                if (chNumberByCareCenter)
                {
                    int[] personIDs = (from p in persons
                                       select p.ID).ToArray();

                    PersonDA personDA = new PersonDA();
                    DataSet ds = personDA.GetPersonCustomerCHNumberData(personIDs);
                    if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedCHNumberTable))
                        && (ds.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable].Rows.Count > 0))
                    {
                        DataTable t = ds.Tables[Administrative.Entities.TableNames.CustomerRelatedCHNumberTable];
                        foreach (PersonLookupDTO person in persons)
                        {
                            DataRow[] rows = t.AsEnumerable()
                                            .Where(r => (r["PersonID"] as int? ?? 0) == person.ID)
                                            .ToArray();
                            if (rows != null)
                            {
                                foreach (DataRow row in rows)
                                {
                                    Administrative.Entities.CustomerCareCenterDTO myCustomer = new Administrative.Entities.CustomerCareCenterDTO(
                                        person.ID, row["CareCenterID"] as int? ?? 0, row["CareCenterName"] as string ?? string.Empty,
                                        row["CHNumber"] as string ?? string.Empty, person.FirstName, person.LastName, person.LastName2, defaultIdentifierTypeID,
                                        defaultIdentifierTypeName, person.DefaultIdentifier);
                                    customers.Add(myCustomer);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (PersonLookupDTO person in persons)
                    {
                        Administrative.Entities.CustomerCareCenterDTO myCustomer = new Administrative.Entities.CustomerCareCenterDTO(
                            person.ID, 0, string.Empty, person.CHNumber, person.FirstName, person.LastName, person.LastName2, defaultIdentifierTypeID,
                            defaultIdentifierTypeName, person.DefaultIdentifier);
                        customers.Add(myCustomer);
                    }
                }
            }

            return customers.Count > 0 ? customers.ToArray() : null;
        }

        public SII.HCD.Administrative.Entities.CustomerWithDuplicatesCareCenterDTO GetCustomerWithMergedCustomersInfo(int personID)
        {
            SII.HCD.Administrative.Entities.CustomerWithDuplicatesCareCenterDTO myCustomer = null;
            if (personID > 0)
            {
                bool maxRecordsExceded = false;
                CommonEntities.GenericErrorLogEntity[] errors = null;

                int recordMergedPersonID = 0;
                bool hasMergedRegisters = false;

                DataSet ds = DataAccess.PersonDA.GetPerson(personID);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable) &&
                    (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0)))
                {
                    recordMergedPersonID = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["RecordMerged"] as int? ?? 0;
                    hasMergedRegisters = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["HasMergedRegisters"] as bool? ?? false;
                }

                PersonSpecification spec1 = PersonSpecification.Create();
                spec1.ByPersonID(personID);
                CustomerCareCenterDTO[] customers = GetCustomerCareCenterInfo(spec1, out maxRecordsExceded, out errors);

                if ((customers != null) && (customers.Length > 0))
                {
                    PersonSpecification spec2 = PersonSpecification.Create();

                    if (recordMergedPersonID > 0)
                    {
                        //Busca el resultante de la fusión
                        List<CustomerCareCenterDTO> fullMergedCustomers = new List<CustomerCareCenterDTO>();
                        CustomerCareCenterDTO[] mergedCustomers = null;
                        spec2.ByPersonID(recordMergedPersonID);
                        mergedCustomers = GetCustomerCareCenterInfo(spec2, out maxRecordsExceded, out errors);
                        if (mergedCustomers != null)
                        {
                            fullMergedCustomers.AddRange(mergedCustomers);
                        }
                        //Busca los hermanos fusionados
                        spec2 = PersonSpecification.Create();
                        spec2.ByRecordMergedPersonID(recordMergedPersonID);
                        mergedCustomers = GetCustomerCareCenterInfo(spec2, out maxRecordsExceded, out errors);
                        if (mergedCustomers != null)
                        {
                            //y se elimina él mismo
                            mergedCustomers = mergedCustomers
                                                .Where(cu => cu.PersonID != personID)
                                                .ToArray();
                            fullMergedCustomers.AddRange(mergedCustomers);
                        }
                        myCustomer = new CustomerWithDuplicatesCareCenterDTO(customers, fullMergedCustomers.ToArray());
                    }
                    else
                    {
                        if (hasMergedRegisters)
                        {
                            spec2.ByRecordMergedPersonID(personID);
                            CustomerCareCenterDTO[] mergedCustomers = GetCustomerCareCenterInfo(spec2, out maxRecordsExceded, out errors);
                            if (mergedCustomers != null)
                            {
                                //y se elimina él mismo
                                mergedCustomers = mergedCustomers
                                                    .Where(cu => cu.PersonID != personID)
                                                    .ToArray();
                            }
                            myCustomer = new CustomerWithDuplicatesCareCenterDTO(customers, mergedCustomers);
                        }
                    }
                }
            }
            return myCustomer;
        }

        public SII.HCD.Administrative.Entities.CustomerWithDuplicatesCareCenterDTO GetCustomerWithDuplicatedCustomersInfo(int personID)
        {
            SII.HCD.Administrative.Entities.CustomerWithDuplicatesCareCenterDTO myCustomer = null;
            if (personID > 0)
            {
                bool maxRecordsExceded = false;
                CommonEntities.GenericErrorLogEntity[] errors = null;

                int duplicateGroupID = 0;

                DataSet ds = DataAccess.PersonDA.GetPerson(personID);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable) &&
                    (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0)))
                {
                    duplicateGroupID = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["DuplicateGroupID"] as int? ?? 0;
                }

                PersonSpecification spec1 = PersonSpecification.Create();
                spec1.ByPersonID(personID);
                CustomerCareCenterDTO[] customers = GetCustomerCareCenterInfo(spec1, out maxRecordsExceded, out errors);

                if ((customers != null) && (customers.Length > 0))
                {
                    PersonSpecification spec2 = PersonSpecification.Create();
                    if (duplicateGroupID > 0)
                    {
                        spec2.ByDuplicateGroupID(duplicateGroupID);
                        CustomerCareCenterDTO[] mergedCustomers = GetCustomerCareCenterInfo(spec2, out maxRecordsExceded, out errors);
                        if (mergedCustomers != null)
                        {
                            //y se elimina él mismo
                            mergedCustomers = mergedCustomers
                                                .Where(cu => cu.PersonID != personID)
                                                .ToArray();
                        }
                        myCustomer = new CustomerWithDuplicatesCareCenterDTO(customers, mergedCustomers);
                    }
                }
            }
            return myCustomer;
        }

        public CustomerMergedDTO[] GetDuplicatedPersons(int personID)
        {
            try
            {
                int duplicateGroupID = DataAccess.PersonDA.GetDuplicateGroupIDByPersonID(personID);
                DataSet ds = DataAccess.CustomerDA.GetDuplicatedCustomers(duplicateGroupID, GetDefaultIdentifierType());
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerMergedDTOTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerMergedDTOTable].Rows.Count > 0))
                {
                    MergeTable(DataAccess.PersonCatRelDA.GetPersonCatRelFromPersonsInDuplicateGroup(duplicateGroupID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable);

                    MergeTable(DataAccess.CategoryDA.GetCategoriesFromPersonsInDuplicateGroup(duplicateGroupID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.CategoryTable);

                    MergeTable(DataAccess.CustomerRelatedCHNumberDA.GetRelatedCHNumberFromPersonsInDuplicateGroup(duplicateGroupID),
                        ds, SII.HCD.Administrative.Entities.TableNames.CustomerRelatedCHNumberTable);

                    CustomerMergedDTOAdvancedAdapter adapter = new CustomerMergedDTOAdvancedAdapter();
                    CustomerMergedDTO[] customers = adapter.GetData(ds);
                    if ((customers != null) && (customers.Length > 0))
                    {
                        bool hasCHNumberByCareCenter = ThereAreNCareCentersWithCodeGenerator();
                        foreach (CustomerMergedDTO customer in customers)
                        {
                            customer.HasCHNumberByCareCenter = hasCHNumberByCareCenter;
                        }
                        return customers;
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

        public CustomerMergedDTO[] GetMergedPersons(int personID)
        {
            try
            {
                int[] personIDs = FindMergedPersons(personID);

                if ((personIDs != null) && (personIDs.Length > 0))
                {
                    DataSet ds = DataAccess.CustomerDA.GetDuplicatedCustomers(personIDs.ToArray(), GetDefaultIdentifierType());
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerMergedDTOTable))
                        && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerMergedDTOTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.PersonCatRelDA.GetPersonCatRelFromPersons(personIDs.ToArray()),
                            ds, SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable);

                        MergeTable(DataAccess.CategoryDA.GetCategoriesFromPersons(personIDs.ToArray()),
                            ds, SII.HCD.BackOffice.Entities.TableNames.CategoryTable);

                        MergeTable(DataAccess.CustomerRelatedCHNumberDA.GetRelatedCHNumberFromPersons(personIDs.ToArray()),
                            ds, SII.HCD.Administrative.Entities.TableNames.CustomerRelatedCHNumberTable);

                        CustomerMergedDTOAdvancedAdapter adapter = new CustomerMergedDTOAdvancedAdapter();
                        CustomerMergedDTO[] customers = adapter.GetData(ds);
                        if ((customers != null) && (customers.Length > 0))
                        {
                            bool hasCHNumberByCareCenter = ThereAreNCareCentersWithCodeGenerator();
                            foreach (CustomerMergedDTO customer in customers)
                            {
                                customer.HasCHNumberByCareCenter = hasCHNumberByCareCenter;
                            }
                            return customers;
                        }
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

        #endregion

        #region new ICustomerService Member
        public bool ValidateCustomer(CustomerEntity customer, out ValidationResults results)
        {
            results = new ValidationResults();
            try
            {
                if (customer == null)
                    throw new ArgumentNullException("customer");

                CommonEntities.ElementEntity _metadata = ElementBL.GetElementByName(EntityNames.CustomerEntityName);
                CustomerHelper customerHelper = new CustomerHelper(_metadata);
                customerHelper.Validate(customer, results);
                if (customer.Person == null)
                    throw new ArgumentNullException("person");

                _metadata = ElementBL.GetElementByName(EntityNames.PersonEntityName);
                PersonHelper personHelper = new PersonHelper(_metadata);
                personHelper.Validate(customer.Person, results);
                if (customer.Person.SensitiveData == null)
                    throw new ArgumentNullException("sensitiveData");

                _metadata = ElementBL.GetElementByName(EntityNames.SensitiveDataEntityName);
                SensitiveDataHelper sensitiveDataHelper = new SensitiveDataHelper(_metadata);
                sensitiveDataHelper.Validate(customer.Person.SensitiveData, results);

                return results.IsValid;
            }
            catch (Exception ex)
            {
                results.AddResult(new ValidationResult(ex.Message, this, string.Empty, string.Empty, null));

                return false;
            }
        }


        public CustomerBasicEntity GetCustomerBasic(int customerID)
        {
            if (customerID <= 0)
                return null;

            try
            {
                DataSet ds = DataAccess.CustomerDA.GetCustomerBasic(customerID, GetDefaultIdentifierType());
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable)
                        && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable].Rows.Count > 0)))
                {
                    #region Special Categories Data
                    int[] cIDs = new int[] { customerID };
                    int observationTemplateID = ObservationTemplateBL.GetExceptionalInfoTemplate();
                    int observationID = ObservationTemplateBL.GetExceptionalInfoLOPD();

                    DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOByCustomerIDs(cIDs, ObservationStatusEnum.Confirmed),
                        ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationTable);

                    DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOValueByCustomerIDsAndObservationTemplate(cIDs, observationTemplateID, observationID, ObservationStatusEnum.Confirmed),
                        ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationValueTable);

                    DataSet myds = new DataSet();
                    DataTable myTable = new DataTable(Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);
                    myTable.Columns.Add("ObservationID", typeof(int));
                    myTable.Columns.Add("ObservationTemplateID", typeof(int));
                    myTable.Rows.Add(observationID, observationTemplateID);
                    myds.Tables.Add(myTable);
                    DatasetUtils.MergeTable(myds, ds, Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);
                    #endregion

                    CustomerBasicAdvancedAdapter cbaa = new CustomerBasicAdvancedAdapter();
                    CustomerBasicEntity cpe = cbaa.GetByID(customerID, ds);

                    if (cpe != null && cpe.ImageID > 0)
                    {
                        DBImageStorageDA _dbImageStorageDA = new DBImageStorageDA();
                        cpe.SetCustomerImageData(_dbImageStorageDA.Get(cpe.ImageID));
                    }
                    return cpe;
                }

                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerBasicEntity GetCustomerBasic(int customerID, int careCenterID)
        {
            if (customerID <= 0)
                return null;

            try
            {
                bool chNumberInCareCenter = ThereAreNCareCentersWithCodeGenerator();

                DataSet ds = DataAccess.CustomerDA.GetCustomerBasic(customerID, careCenterID, chNumberInCareCenter, GetDefaultIdentifierType());
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable)
                        && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable].Rows.Count > 0)))
                {
                    int[] cIDs = new int[] { customerID };
                    int observationTemplateID = ObservationTemplateBL.GetExceptionalInfoTemplate();
                    int observationID = ObservationTemplateBL.GetExceptionalInfoLOPD();

                    DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOByCustomerIDs(cIDs, ObservationStatusEnum.Confirmed),
                        ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationTable);

                    DatasetUtils.MergeTable(DataAccess.CustomerObservationDA.GetCOValueByCustomerIDsAndObservationTemplate(cIDs, observationTemplateID, observationID, ObservationStatusEnum.Confirmed),
                        ds, Administrative.Entities.TableNames.CustomerBasicCustomerObservationValueTable);

                    DataSet myds = new DataSet();
                    DataTable myTable = new DataTable(Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);
                    myTable.Columns.Add("ObservationID", typeof(int));
                    myTable.Columns.Add("ObservationTemplateID", typeof(int));
                    myTable.Rows.Add(observationID, observationTemplateID);
                    myds.Tables.Add(myTable);
                    DatasetUtils.MergeTable(myds, ds, Administrative.Entities.TableNames.CustomerBasicObservationIDsTable);

                    CustomerBasicAdvancedAdapter cbaa = new CustomerBasicAdvancedAdapter();
                    CustomerBasicEntity cpe = cbaa.GetByID(customerID, ds);

                    if (cpe.ImageID > 0)
                    {
                        DBImageStorageDA _dbImageStorageDA = new DBImageStorageDA();
                        cpe.SetCustomerImageData(_dbImageStorageDA.Get(cpe.ImageID));
                    }
                    return cpe;
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerLookupDTO[] GetCustomerLookupList(CustomerFilterSpecification filters, out bool maxRecordsExceded)
        {
            List<CustomerLookupDTO> customerLookupList = new List<CustomerLookupDTO>();

            maxRecordsExceded = false;

            if (ThereAreNCareCentersWithCodeGenerator())
            {
                filters.ByCHNumberInCareCenter();
            }

            CustomerBasicEntity[] customers = GetCustomerBasicList(filters, out maxRecordsExceded);
            if (customers != null)
            {
                //Buscar los números de historia actuales...
                int[] mergedPersonIDs = customers
                                        .Where(cu => cu.RecordMergedID > 0)
                                        .Select(cu => cu.RecordMergedID)
                                        .Distinct()
                                        .ToArray();

                var chNumbers = Enumerable.Empty<object>()
                                                .Select(o => new { PersonID = 0, CHNumber = string.Empty });

                if ((mergedPersonIDs != null) && (mergedPersonIDs.Length > 0))
                {
                    DataSet ds = null;
                    if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter) && filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberCareCenterID))
                    {
                        ds = DataAccess.CustomerDA.GetCustomerCHNumbersOfPersonsByCareCenter(mergedPersonIDs, filters.CHNumberCareCenterID);
                    }
                    else
                    {
                        ds = DataAccess.CustomerDA.GetCustomerCHNumbersOfPersons(mergedPersonIDs);
                    }
                    if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0))
                    {
                        chNumbers = ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].AsEnumerable()
                                       .Select(r => new { PersonID = r.Field<int>("PersonID"), CHNumber = r.Field<string>("CHNumber") });
                    }
                }

                alergiasIndigo = GetValueAlergiasIndigo(customers.Select(p=> p.ID).ToArray());
                antecedecntesPaciente = HasAntecedentes(customers.Select(p => p.ID).ToArray());

                foreach (CustomerBasicEntity item in customers)
                {
                    CustomerLookupDTO myCustomer = new CustomerLookupDTO();
                    myCustomer.AssignFrom(item);
                    //Mostrar el numero de historia vigente sólo si es un fusionado y se ha especificado un centro (tiene un numero de historia antiguo).
                    if ((myCustomer.RecordMergedID > 0) && (!string.IsNullOrWhiteSpace(myCustomer.CHNumber)))
                    {
                        myCustomer.CurrentCHNumber = chNumbers
                                                        .Where(cu => cu.PersonID == myCustomer.RecordMergedID)
                                                        .Select(cu => cu.CHNumber)
                                                        .FirstOrDefault();
                    }
                    else if (myCustomer.RecordMergedID == 0)
                    {
                        myCustomer.CurrentCHNumber = myCustomer.CHNumber;
                    }

                    myCustomer.AllergyValue = GetValueAlergiasPacienteIndigo(item.ID);
                    myCustomer.HasAntecedentes = GetHasAntecedentes(item.ID);
                    customerLookupList.Add(myCustomer);
                }
            }
            return customerLookupList.Count > 0 ? customerLookupList.ToArray() : null;
        }

        private bool GetHasAntecedentes(int p)
        {
            if(antecedecntesPaciente.Contains(p.ToString()))
                return true;
            else
                return false;
        }

        private string GetValueAlergiasPacienteIndigo(int p)
        {
            if (alergiasIndigo == null || !alergiasIndigo.Any())
                return null;

            string value = alergiasIndigo.Where(pac => pac.Substring(0, pac.IndexOf('_')) == p.ToString()).FirstOrDefault();


            if (value != null)
                return value.Substring(value.IndexOf('_')).Trim('_');
            else
                return null;
        }

        public List<string> GetValueAlergiasIndigo(int[] customerIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) throw new ArgumentNullException("customerIDs");

                return DataAccess.CustomerObservationDA.GetValueAlergiasIndigo(customerIDs);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public List<string> HasAntecedentes(int[] customerIDs)
        {
            try
            {
                if (customerIDs.Count() <= 0) throw new ArgumentNullException("customerIDs");

                return DataAccess.CustomerObservationDA.HasAntecedentes(customerIDs);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerBasicEntity[] GetCustomerBasicList(CustomerFilterSpecification filters, out bool maxRecordsExceded)
        {
            maxRecordsExceded = false;
            if (filters == null) return null;
            try
            {
                AdministrativeConfigurationSection admConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
                int maxRows = admConfig.EntitySettings.CustomerEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                CommonEntities.AddInTokenBaseEntity[] phoneticAddinNames = null;
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByFullName | CustomerFindOptionEnum.PhoneticLookupByNameParts))
                {
                    phoneticAddinNames = GetAvailablePhoneticAddins();
                    if ((phoneticAddinNames == null) || (phoneticAddinNames.Length == 0))
                        throw new Exception(Properties.Resources.MSG_NoPhoneticAddins);
                }

                if (!filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter))
                {
                    if (ThereAreNCareCentersWithCodeGenerator())
                    {
                        filters.ByCHNumberInCareCenter();
                    }
                }

                CustomerBasicEntity[] myCustomers = null;
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.FindMerged))
                {
                    //Se busca aplicando el filtro en todas las personas registradas.
                    myCustomers = GetFilteredCustomers(filters, ref maxRecordsExceded, maxRows, phoneticAddinNames);
                    if ((maxRecordsExceded) || (myCustomers == null) || (myCustomers.Length == 0))
                    {
                        return myCustomers;
                    }
                    //Si no se sobrepasa el límite de registros...buscar de los que estan fusionados sus productos de fusión
                    int[] mergedCustomers = myCustomers
                                                .Where(cust => cust.RecordMergedID > 0)
                                                .Select(cust => cust.RecordMergedID)
                                                .Distinct()
                                                .ToArray();
                    if (mergedCustomers.Length > 0)
                    {
                        CustomerFilterSpecification mySpecification = new CustomerFilterSpecification();
                        mySpecification.ByPersonIDs(mergedCustomers);
                        mySpecification.ByStatus(new CommonEntities.StatusEnum[] { CommonEntities.StatusEnum.Active });
                        mySpecification.ByCHNumberCareCenterID(filters.CHNumberCareCenterID);
                        if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter))
                        {
                            mySpecification.ByCHNumberInCareCenter();
                        }
                        CustomerBasicEntity[] myMergedCustomers = GetFilteredCustomers(mySpecification, ref maxRecordsExceded, maxRows, phoneticAddinNames);
                        //y mezclar ambas listas evitando duplicados
                        if ((myMergedCustomers != null) && (myMergedCustomers.Length > 0))
                        {
                            myCustomers = myCustomers.Union(myMergedCustomers.Where(cust => !myCustomers.Any(cu => cu.PersonID == cust.PersonID))).ToArray();
                        }
                    }
                }
                else
                {
                    if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumber))
                    {
                        //Búsqueda normal pero por CHNumber
                        //Se busca aplicando el filtro en todas las personas registradas.
                        myCustomers = GetFilteredCustomers(filters, ref maxRecordsExceded, maxRows, phoneticAddinNames);
                        if ((maxRecordsExceded) || (myCustomers == null) || (myCustomers.Length == 0))
                        {
                            return myCustomers;
                        }
                        //buscar de los que estan fusionados sus productos de fusión
                        int[] mergedCustomers = myCustomers
                                                    .Where(cust => cust.RecordMergedID > 0)
                                                    .Select(cust => cust.RecordMergedID)
                                                    .Distinct()
                                                    .ToArray();
                        if (mergedCustomers.Length > 0)
                        {
                            CustomerFilterSpecification mySpecification = new CustomerFilterSpecification();
                            mySpecification.ByPersonIDs(mergedCustomers);
                            mySpecification.ByStatus(new CommonEntities.StatusEnum[] { CommonEntities.StatusEnum.Active });
                            mySpecification.ByCHNumberCareCenterID(filters.CHNumberCareCenterID);
                            if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter))
                            {
                                mySpecification.ByCHNumberInCareCenter();
                            }
                            CustomerBasicEntity[] myMergedCustomers = GetFilteredCustomers(mySpecification, ref maxRecordsExceded, maxRows, phoneticAddinNames);
                            //y mezclar ambas listas evitando duplicados y personas fusionadas
                            if ((myMergedCustomers != null) && (myMergedCustomers.Length > 0))
                            {
                                myCustomers = myCustomers
                                                .Where(cust => cust.RecordMergedID == 0)
                                                .Union(myMergedCustomers
                                                            .Where(cust => !myCustomers.Any(cu => cu.PersonID == cust.PersonID) && (cust.RecordMergedID == 0)))
                                                .ToArray();
                            }
                        }
                    }
                    else
                    {
                        //Búsqueda normal. Solo se busca en personas activas
                        filters.ByStatus(new CommonEntities.StatusEnum[] { CommonEntities.StatusEnum.Active });
                        myCustomers = GetFilteredCustomers(filters, ref maxRecordsExceded, maxRows, phoneticAddinNames);
                    }
                }
                return myCustomers;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public int GetCustomerIDByPersonID(int personID)
        {
            try
            {
                if (personID <= 0) return 0;
                return DataAccess.CustomerDA.GetCustomerIDByPersonID(personID);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }
        #endregion

        #region fusion
        public CustomerEntity[] GetCustomersByDuplicateGroup(int duplicateGroupID)
        {
            try
            {
                if (duplicateGroupID <= 0) return null;
                int[] personIDs = DataAccess.PersonDA.GetDuplicatePersonIDsByDuplicateGroup(duplicateGroupID);
                if (personIDs != null && personIDs.Length > 0)
                {
                    return personIDs
                        .Where(id => id > 0)
                        .Select(id => this.GetCustomerByPersonID(id))
                        .ToArray();
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEntity GetResultCustomerByDuplicateGroup(int duplicateGroupID)
        {
            try
            {
                if (duplicateGroupID <= 0) return null;
                int resultpersonID = DataAccess.PersonDA.GetResultPersonIDByDuplicateGroup(duplicateGroupID);
                return resultpersonID > 0 ? GetCustomerByPersonID(resultpersonID) : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.IDDescriptionEntity[] GetMergePersons(int personID)
        {
            try
            {
                if (personID <= 0) return null;
                //DataSet ds = DataAccess.PersonDA.GetMergePersons(personID);
                //if (ds != null && ds.Tables != null && ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)
                //    && ds.Tables[SII.HCD.Common.Entities.TableNames.IDDescriptionTable].Rows.Count > 0)
                //{
                //    (from row in ds.Tables[SII.HCD.Common.Entities.TableNames.IDDescriptionTable].AsEnumerable()
                //     select new CommonEntities.IDDescriptionEntity(
                //         row["ID"] as int? ?? 0,

                //         )
                //         ).ToArray();

                //}
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        //public bool MergeCustomers(CustomerEntity newcustomer, CustomerEntity[] oldcustomers)
        //{
        //    try
        //    {
        //        if (newcustomer == null || newcustomer.Person == null || newcustomer.ID <= 0 || newcustomer.Person.ID <= 0)
        //            throw new ArgumentNullException("Customer");
        //        if (oldcustomers == null || oldcustomers.Length <= 0)
        //            throw new ArgumentNullException("OldCustomer");

        //        CustomerEpisodeDA _customerEpisodeDA = new CustomerEpisodeDA();
        //        bool newcustomerHasEpisode = _customerEpisodeDA.HasCustomerEpisodes(newcustomer.ID, CommonEntities.StatusEnum.Active);
        //        List<CustomerEpisodeEntity> customerepisodes = new List<CustomerEpisodeEntity>();
        //        CustomerEpisodeEntity customerepisode = null;
        //        foreach (CustomerEntity oldce in oldcustomers)
        //        {
        //            bool oldCustomerHasEpisode = _customerEpisodeDA.HasCustomerEpisodes(oldce.ID, CommonEntities.StatusEnum.Active);
        //            if (newcustomerHasEpisode && oldCustomerHasEpisode)
        //                throw new Exception(string.Format(Properties.Resources.MSG_CannotMergeCustomerBecauseHasActiveEpisodes,
        //                   string.Concat(oldce.Person.FullName, " - ", newcustomer.Person.FirstName)));
        //            if (oldCustomerHasEpisode)
        //            {
        //                int customerEpisodeID = _customerEpisodeDA.CustomerEpisodeID(oldce.ID, CommonEntities.StatusEnum.Active);
        //                if (customerEpisodeID > 0)
        //                {
        //                    customerepisode = CustomerEpisodeBL.GetFullCustomerEpisode(customerEpisodeID);
        //                    if (customerepisode != null)
        //                        customerepisodes.Add(customerepisode);
        //                }
        //            }
        //        }

        //        Entities.Clear();
        //        List<CustomerEntity> customers = new List<CustomerEntity>();
        //        foreach (CustomerEntity oldce in oldcustomers)
        //        {
        //            if (oldce.ID > 0 && oldce.Person != null && oldce.Person.ID > 0)
        //            {
        //                customers.Add(oldce);
        //                DataAccess.CustomerDA.MergeCustomerInfo(newcustomer.ID, oldce.ID, newcustomer.Person.ID, oldce.Person.ID);
        //                //throw new ArgumentNullException("OldCustomer");
        //                //aqui falta el HL7 d MERGE ADT^40
        //            }
        //        }
        //        if (customers.Count > 0)
        //        {
        //            Entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, newcustomer);
        //            Entities.Add(HL7EntityNames.HL7CustomerInfoListName, customers.ToArray());
        //            if (customerepisodes != null && customerepisodes.Count > 0)
        //            {
        //                Entities.Add(HL7EntityNames.HL7CustomerEpisodeInfoListName, customerepisodes.ToArray());
        //            }
        //            if (newcustomer.CustomerCHNumbers != null && newcustomer.CustomerCHNumbers.Length > 0)
        //            {
        //                CareCenterBasicEntity careCenter = new CareCenterBasicEntity(newcustomer.CustomerCHNumbers[0].CareCenterID, newcustomer.CustomerCHNumbers[0].CareCenterName);
        //                Entities.Add(CommonEntities.Constants.EntityNames.CareCenterBasicEntityName, careCenter);
        //            }
        //            //// envia mensaje HL7  ADT^A40 
        //            HL7MessagingProcessor.ResetEntities(Entities);
        //            HL7MessagingProcessor.ResetBLs(BLs);
        //            HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT40);
        //            /////////////////////////////////////////////////////
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return false;
        //    }
        //}

        #region merge methods
        #region hl7 merge customers
        private void MessageHL7MergePatients(CustomerEntity newcustomer, CustomerEntity[] oldcustomers)
        {
            if (newcustomer == null || oldcustomers == null || oldcustomers.Length <= 0) return;

            Entities.Clear();
            List<CustomerEntity> customers = new List<CustomerEntity>();
            customers.AddRange(oldcustomers);
            if (customers.Count > 0)
            {
                Entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, newcustomer);
                Entities.Add(HL7EntityNames.HL7CustomerInfoListName, customers.ToArray());
                if (newcustomer.CustomerCHNumbers != null && newcustomer.CustomerCHNumbers.Length > 0)
                {
                    CareCenterBasicEntity careCenter = new CareCenterBasicEntity(newcustomer.CustomerCHNumbers[0].CareCenterID, newcustomer.CustomerCHNumbers[0].CareCenterName);
                    Entities.Add(CommonEntities.Constants.EntityNames.CareCenterBasicEntityName, careCenter);
                }
                //// envia mensaje HL7  ADT^A40 
                HL7MessagingProcessor.ResetEntities(Entities);
                HL7MessagingProcessor.ResetBLs(BLs);
                HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT40);
                /////////////////////////////////////////////////////
            }
        }
        #endregion

        private void ValidateCustomerRegistration(CustomerEntity entity, ValidationResults validationResults)
        {
            if (entity == null || validationResults == null)
                return;

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    this.ValidateInsertCustomer(entity, validationResults);
                    break;
                case StatusEntityValue.Updated:
                    this.ValidateUpdateCustomer(entity, validationResults);
                    break;
            }
        }

        private void ValidateInsertCustomer(CustomerEntity entity, ValidationResults validationResults)
        {
            try
            {
                PersonAddressListDTO[] homonymPersons = null;
                CheckInsertPreconditions(entity, true, out homonymPersons, ElementBL);
            }
            catch (Exception ex)
            {
                validationResults.AddResult(new ValidationResult(ex.Message, null, null, null, null));
            }

        }

        private void ValidateUpdateCustomer(CustomerEntity entity, ValidationResults validationResults)
        {
            try
            {
                PersonAddressListDTO[] homonymPersons = null;
                CheckUpdatePreconditions(entity, true, out homonymPersons, ElementBL);
            }
            catch (Exception ex)
            {
                validationResults.AddResult(new ValidationResult(ex.Message, null, null, null, null));
            }
        }

        private int[] GetCategoriesIDs(CustomerEntity[] customersToMerge)
        {
            return customersToMerge
                    .Where(c => c.Person != null && c.Person.Categories != null && c.Person.Categories.Length > 0)
                    .SelectMany(c => c.Person.Categories)
                    .Where(ct => ct.Category != null)
                    .Select(ct => ct.Category.CategoryKey)
                    .Distinct()
                    .ToArray();
        }

        private bool ValidateCount(CustomerEntity[] customersToMerge)
        {
            int[] _categoryKeys = GetCategoriesIDs(customersToMerge);
            if (_categoryKeys == null || _categoryKeys.Length <= 0) return false;
            foreach (int cat in _categoryKeys)
            {
                if (cat != (int)CategoryPersonKeyEnum.Customer)
                {
                    if (customersToMerge
                               .Where(c => c.Person != null && c.Person.Categories != null && c.Person.Categories.Length > 0)
                               .SelectMany(c => c.Person.Categories)
                               .Where(ct => ct.Category != null && ct.Category.CategoryKey == cat)
                               .Count() > 1)
                        return false;
                }
            }
            return true;
        }

        private void ValidateMergePersons(CustomerEntity newcustomer, CustomerEntity[] oldcustomers, ValidationResults validationResults)
        {
            //las personas no pueden tener RecordMerged y tampoco estar superceded
            if (oldcustomers.Any(c => (c.Person.RecordMerged as int? ?? 0) > 0 || c.Person.Status == CommonEntities.StatusEnum.Superceded))
                validationResults.AddResult(new ValidationResult(Properties.Resources.MSG_MergeWillBeImpossibleByPersonInfo, null, null, null, null));
        }

        private void ValidateMergeOtherCategories(CustomerEntity newcustomer, CustomerEntity[] oldcustomers, ValidationResults validationResults)
        {
            //aqui validamos las categorias de las personas a fusionar
            if (!ValidateCount(oldcustomers))
                validationResults.AddResult(new ValidationResult(Properties.Resources.MSG_MergeWillBeImpossible, null, null, null, null));
        }

        private void ValidateMergeCustomers(CustomerEntity newcustomer, CustomerEntity[] oldcustomers, ValidationResults validationResults)
        {
            if ((newcustomer.Person.RecordMerged as int? ?? 0) > 0 || newcustomer.Person.Status == CommonEntities.StatusEnum.Superceded)
                validationResults.AddResult(new ValidationResult(Properties.Resources.MSG_MergeWillBeImpossibleByPersonInfo, null, null, null, null));
        }

        private CustomerProcessMergedEntity[] GetCustomerProcessMergedRegisters(CustomerEntity newcustomer, CustomerEntity[] oldcustomers)
        {

            List<CustomerProcessMergedEntity> cpmlist = new List<CustomerProcessMergedEntity>();
            foreach (CustomerEntity customer in oldcustomers)
            {
                CustomerProcessMergedEntity cpm = Helpers.CustomerProcessMergedHelper.Create();
                cpm.EditStatus.New();
                cpm.CustomerID = customer.ID;
                cpm.DuplicateGroupID = customer.Person.DuplicateGroupID;
                cpmlist.Add(cpm);
            }

            int[] oldcustomerIDs = oldcustomers.Select(c => c.ID).Distinct().OrderBy(id => id).ToArray();
            Tuple<int, int>[] tupleids = null;
            //buscando CustomerProcess
            //tuple.item1 = oldcustomerID, tuple.item2 = customerProcessID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "CustomerProcess");//Esta tupla se va a componer del (CustomerId, CustomerProcess.ID)
            List<RelatedCustomerProcessMergedEntity> relatedCustomerProcessMergeds = new List<RelatedCustomerProcessMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedCustomerProcessMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerProcessMergedEntity relatedCustomerProcessMerged = Helpers.RelatedCustomerProcessMergedHelper.Create();
                    relatedCustomerProcessMerged.EditStatus.New();
                    relatedCustomerProcessMerged.CustomerID = tuple.Item1;
                    relatedCustomerProcessMerged.CustomerProcessID = tuple.Item2;
                    relatedCustomerProcessMergeds.Add(relatedCustomerProcessMerged);
                }
            }

            //buscando CustomerMedProcess
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerMedProcessID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "CustomerMedProcess");
            List<RelatedCustomerMedProcessMergedEntity> relatedCustomerMedProcessMergeds = new List<RelatedCustomerMedProcessMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedCustomerMedProcessMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerMedProcessMergedEntity relatedCustomerMedProcessMerged = Helpers.RelatedCustomerMedProcessMergedHelper.Create();
                    relatedCustomerMedProcessMerged.EditStatus.New();
                    relatedCustomerMedProcessMerged.CustomerID = tuple.Item1;
                    relatedCustomerMedProcessMerged.CustomerMedProcessID = tuple.Item2;
                    relatedCustomerMedProcessMergeds.Add(relatedCustomerMedProcessMerged);
                }
            }

            //buscando CustomerPolicy 
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerPolicyID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "CustomerPolicy");
            List<RelatedCustomerPolicyMergedEntity> relatedCustomerPolicyMergeds = new List<RelatedCustomerPolicyMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedCustomerPolicyMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerPolicyMergedEntity relatedCustomerPolicyMerged = Helpers.RelatedCustomerPolicyMergedHelper.Create();
                    relatedCustomerPolicyMerged.EditStatus.New();
                    relatedCustomerPolicyMerged.CustomerID = tuple.Item1;
                    relatedCustomerPolicyMerged.CustomerPolicyID = tuple.Item2;
                    relatedCustomerPolicyMergeds.Add(relatedCustomerPolicyMerged);
                }
            }

            //buscando CustomerNotificationRel 
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerNotificationRelID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "CustomerNotificationRel");
            List<RelatedCustomerNotifMergedEntity> relatedCustomerNotifMergeds = new List<RelatedCustomerNotifMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedCustomerNotifMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerNotifMergedEntity relatedCustomerNotifMerged = Helpers.RelatedCustomerNotifMergedHelper.Create();
                    relatedCustomerNotifMerged.EditStatus.New();
                    relatedCustomerNotifMerged.CustomerID = tuple.Item1;
                    relatedCustomerNotifMerged.CustomerNotificationRelID = tuple.Item2;
                    relatedCustomerNotifMergeds.Add(relatedCustomerNotifMerged);
                }
            }

            //buscando CustomerNOKRel 
            //tuple.item1 = oldcustomerID, tuple.item2 = NOKID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "NOK");
            List<RelatedCustomerNOKMergedEntity> relatedCustomerNOKMergeds = new List<RelatedCustomerNOKMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedCustomerNOKMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerNOKMergedEntity relatedCustomerNOKMerged = Helpers.RelatedCustomerNOKMergedHelper.Create();
                    relatedCustomerNOKMerged.EditStatus.New();
                    relatedCustomerNOKMerged.CustomerID = tuple.Item1;
                    relatedCustomerNOKMerged.NOKID = tuple.Item2;
                    relatedCustomerNOKMergeds.Add(relatedCustomerNOKMerged);
                }
            }

            //buscando CustomerContactPersonRel 
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerContactPersonID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "CustomerContactPerson");
            List<RelatedCustomerContactPersonMergedEntity> relatedCustomerContactPersonMergeds = new List<RelatedCustomerContactPersonMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedCustomerContactPersonMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerContactPersonMergedEntity relatedCustomerContactPersonMerged = Helpers.RelatedCustomerContactPersonMergedHelper.Create();
                    relatedCustomerContactPersonMerged.EditStatus.New();
                    relatedCustomerContactPersonMerged.CustomerID = tuple.Item1;
                    relatedCustomerContactPersonMerged.CustomerContactPersonID = tuple.Item2;
                    relatedCustomerContactPersonMergeds.Add(relatedCustomerContactPersonMerged);
                }
            }

            //buscando CustomerContactOrgRel 
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerContactOrganizationID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "CustomerContactOrganization");
            List<RelatedCustomerContactOrgMergedEntity> relatedCustomerContactOrgMergeds = new List<RelatedCustomerContactOrgMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedCustomerContactOrgMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerContactOrgMergedEntity relatedCustomerContactOrgMerged = Helpers.RelatedCustomerContactOrgMergedHelper.Create();
                    relatedCustomerContactOrgMerged.EditStatus.New();
                    relatedCustomerContactOrgMerged.CustomerID = tuple.Item1;
                    relatedCustomerContactOrgMerged.CustomerContactOrganizationID = tuple.Item2;
                    relatedCustomerContactOrgMergeds.Add(relatedCustomerContactOrgMerged);
                }
            }

            //buscando BatchMovement 
            //tuple.item1 = oldcustomerID, tuple.item2 = BatchMovementID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerByTableName(oldcustomerIDs, "BatchMovement");
            List<RelatedBatchMovementMergedEntity> relatedBatchMovementMergeds = new List<RelatedBatchMovementMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedBatchMovementMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedBatchMovementMergedEntity relatedBatchMovementMerged = Helpers.RelatedBatchMovementMergedHelper.Create();
                    relatedBatchMovementMerged.EditStatus.New();
                    relatedBatchMovementMerged.CustomerID = tuple.Item1;
                    relatedBatchMovementMerged.BatchMovementID = tuple.Item2;
                    relatedBatchMovementMergeds.Add(relatedBatchMovementMerged);
                }
            }

            //aqui buscamos todos los CustomerOrderRequest que no están en ningún episodio
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerOrderRequestID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerOfCustomerOrderRequest(oldcustomerIDs);
            List<RelatedCustomerOrderReqMergedEntity> relatedCustomerOrderReqMergeds = new List<RelatedCustomerOrderReqMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedBatchMovementMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerOrderReqMergedEntity relatedCustomerOrderReqMerged = Helpers.RelatedCustomerOrderReqMergedHelper.Create();
                    relatedCustomerOrderReqMerged.EditStatus.New();
                    relatedCustomerOrderReqMerged.CustomerID = tuple.Item1;
                    relatedCustomerOrderReqMerged.CustomerOrderRequestID = tuple.Item2;
                    relatedCustomerOrderReqMergeds.Add(relatedCustomerOrderReqMerged);
                }
            }

            //aqui buscamos todos los CustomerObservations que no están en ningún episodio, ni en nigún otro sitio
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerObservationID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerOfCustomerObservations(oldcustomerIDs);
            List<RelatedCustomerObsMergedEntity> relatedCustomerObsMergeds = new List<RelatedCustomerObsMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedBatchMovementMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerObsMergedEntity relatedCustomerObsMerged = Helpers.RelatedCustomerObsMergedHelper.Create();
                    relatedCustomerObsMerged.EditStatus.New();
                    relatedCustomerObsMerged.CustomerID = tuple.Item1;
                    relatedCustomerObsMerged.CustomerObservationID = tuple.Item2;
                    relatedCustomerObsMergeds.Add(relatedCustomerObsMerged);
                }
            }

            //aqui buscamos todos los CustomerCard que no están en ninguna Póliza
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerCardID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerOfCustomerCards(oldcustomerIDs);
            List<RelatedCustomerCardMergedEntity> relatedCustomerCardMergeds = new List<RelatedCustomerCardMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedBatchMovementMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerCardMergedEntity relatedCustomerCardMerged = Helpers.RelatedCustomerCardMergedHelper.Create();
                    relatedCustomerCardMerged.EditStatus.New();
                    relatedCustomerCardMerged.CustomerID = tuple.Item1;
                    relatedCustomerCardMerged.CustomerCardID = tuple.Item2;
                    relatedCustomerCardMergeds.Add(relatedCustomerCardMerged);
                }
            }

            //aqui buscamos todos los CustomerAccountCharges que no están en ningún episodio
            //tuple.item1 = oldcustomerID, tuple.item2 = CustomerAccountChargeID
            tupleids = DataAccess.CustomerDA.GetFromDuplicatesCustomerOfCustomerAccountCharges(oldcustomerIDs);
            List<RelatedCustomerAccountChargeMergedEntity> relatedCustomerAccountChargeMergeds = new List<RelatedCustomerAccountChargeMergedEntity>();
            if (tupleids != null && tupleids.Length > 0)
            {
                //aqui tengo que recoger los RelatedBatchMovementMergedEntity
                foreach (Tuple<int, int> tuple in tupleids)
                {
                    RelatedCustomerAccountChargeMergedEntity relatedCustomerAccountChargeMerged = Helpers.RelatedCustomerAccountChargeMergedHelper.Create();
                    relatedCustomerAccountChargeMerged.EditStatus.New();
                    relatedCustomerAccountChargeMerged.CustomerID = tuple.Item1;
                    relatedCustomerAccountChargeMerged.CustomerAccountChargeID = tuple.Item2;
                    relatedCustomerAccountChargeMergeds.Add(relatedCustomerAccountChargeMerged);
                }
            }

            foreach (CustomerProcessMergedEntity cpm in cpmlist)
            {
                cpm.CustomerProcesses = (relatedCustomerProcessMergeds.Count > 0)
                    ? relatedCustomerProcessMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.CustomerMedProcesses = (relatedCustomerMedProcessMergeds.Count > 0)
                    ? relatedCustomerMedProcessMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerObsMergeds = (relatedCustomerObsMergeds.Count > 0)
                    ? relatedCustomerObsMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerOrderRequestMergeds = (relatedCustomerOrderReqMergeds.Count > 0)
                    ? relatedCustomerOrderReqMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerPolicyMergeds = (relatedCustomerPolicyMergeds.Count > 0)
                    ? relatedCustomerPolicyMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerCardMergeds = (relatedCustomerCardMergeds.Count > 0)
                    ? relatedCustomerCardMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerAccountChargeMergeds = (relatedCustomerAccountChargeMergeds.Count > 0)
                    ? relatedCustomerAccountChargeMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedBatchMovementMergeds = (relatedBatchMovementMergeds.Count > 0)
                    ? relatedBatchMovementMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerNotificationMergeds = (relatedCustomerNotifMergeds.Count > 0)
                    ? relatedCustomerNotifMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerNOKMergeds = (relatedCustomerNOKMergeds.Count > 0)
                    ? relatedCustomerNOKMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerContactPersonMergeds = (relatedCustomerContactPersonMergeds.Count > 0)
                    ? relatedCustomerContactPersonMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
                cpm.RelatedCustomerContactOrgMergeds = (relatedCustomerContactOrgMergeds.Count > 0)
                    ? relatedCustomerContactOrgMergeds
                            .Where(c => c.CustomerID == cpm.CustomerID)
                            .ToArray()
                    : null;
            }
            return cpmlist.ToArray();
        }

        private PersonProcessMergedEntity[] GetPersonProcessMergedRegisters(CustomerEntity newcustomer, CustomerEntity[] oldcustomers, int aaCoverElementID)
        {
            List<PersonProcessMergedEntity> ppmlist = new List<PersonProcessMergedEntity>();
            foreach (CustomerEntity customer in oldcustomers)
            {
                PersonProcessMergedEntity ppm = Helpers.PersonProcessMergedHelper.Create();
                ppm.EditStatus.New();
                ppm.PersonID = customer.Person.ID;
                ppm.DuplicateGroupID = customer.Person.DuplicateGroupID;
                ppmlist.Add(ppm);
            }

            Tuple<int, int>[] tupleids = null;

            List<RelatedNOKMergedEntity> relatedNOKMergeds = new List<RelatedNOKMergedEntity>();
            if (oldcustomers.Any(c => c.Person.Categories != null &&
                    c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.NOK)))
            {
                //significa que hay una persona que es NOK
                //tuple.item1 = oldpersonID, tuple.item2 = NOKID
                int oldpersonID = oldcustomers
                    .Where(c => c.Person.Categories != null &&
                                c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.NOK))
                    .Select(c => c.Person.ID)
                    .FirstOrDefault();
                tupleids = DataAccess.PersonDA.GetFromDuplicatesPersonByTableName(new int[] { oldpersonID }, "NOK");
                if (tupleids != null && tupleids.Length > 0)
                {
                    //aqui tengo que recoger los relatedNOKMergeds
                    foreach (Tuple<int, int> tuple in tupleids)
                    {
                        RelatedNOKMergedEntity relatedNOKMerged = Helpers.RelatedNOKMergedHelper.Create();
                        relatedNOKMerged.EditStatus.New();
                        relatedNOKMerged.PersonID = tuple.Item1;
                        relatedNOKMerged.NOKID = tuple.Item2;
                        relatedNOKMergeds.Add(relatedNOKMerged);
                    }
                }
            }
            List<RelatedContactPersonMergedEntity> relatedContactPersonMergeds = new List<RelatedContactPersonMergedEntity>();
            if (oldcustomers.Any(c => c.Person.Categories != null &&
                    c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.CustContactPerson)))
            {
                //significa que hay una persona que es CustContactPerson
                //tuple.item1 = oldpersonID, tuple.item2 = ContactPersonID
                int oldpersonID = oldcustomers
                    .Where(c => c.Person.Categories != null &&
                                c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.CustContactPerson))
                    .Select(c => c.Person.ID)
                    .FirstOrDefault();
                tupleids = DataAccess.PersonDA.GetFromDuplicatesPersonByTableName(new int[] { oldpersonID }, "CustomerContactPerson");
                if (tupleids != null && tupleids.Length > 0)
                {
                    //aqui tengo que recoger los relatedContactPersonMergeds
                    foreach (Tuple<int, int> tuple in tupleids)
                    {
                        RelatedContactPersonMergedEntity relatedContactPersonMerged = Helpers.RelatedContactPersonMergedHelper.Create();
                        relatedContactPersonMerged.EditStatus.New();
                        relatedContactPersonMerged.PersonID = tuple.Item1;
                        relatedContactPersonMerged.CustomerContactPersonID = tuple.Item2;
                        relatedContactPersonMergeds.Add(relatedContactPersonMerged);
                    }
                }
            }
            List<RelatedOrganizationContactPersonMergedEntity> relatedOrganizationContactPersonMergeds = new List<RelatedOrganizationContactPersonMergedEntity>();
            if (oldcustomers.Any(c => c.Person.Categories != null &&
                    c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.OrgContactPerson)))
            {
                //significa que hay una persona que es OrgContactPerson
                //tuple.item1 = oldpersonID, tuple.item2 = OrganizationContactPersonID
                int oldpersonID = oldcustomers
                    .Where(c => c.Person.Categories != null &&
                                c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.OrgContactPerson))
                    .Select(c => c.Person.ID)
                    .FirstOrDefault();
                tupleids = DataAccess.PersonDA.GetFromDuplicatesPersonByTableName(new int[] { oldpersonID }, "OrganizationContactPerson");
                if (tupleids != null && tupleids.Length > 0)
                {
                    //aqui tengo que recoger los relatedOrganizationContactPersonMergeds
                    foreach (Tuple<int, int> tuple in tupleids)
                    {
                        RelatedOrganizationContactPersonMergedEntity relatedOrganizationContactPersonMerged = Helpers.RelatedOrganizationContactPersonMergedHelper.Create();
                        relatedOrganizationContactPersonMerged.EditStatus.New();
                        relatedOrganizationContactPersonMerged.PersonID = tuple.Item1;
                        relatedOrganizationContactPersonMerged.OrganizationContactPersonID = tuple.Item2;
                        relatedOrganizationContactPersonMergeds.Add(relatedOrganizationContactPersonMerged);
                    }
                }
            }
            List<RelatedHumanResourceMergedEntity> relatedHumanResourceMergeds = new List<RelatedHumanResourceMergedEntity>();
            if (oldcustomers.Any(c => c.Person.Categories != null &&
                    c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.HHRR)))
            {
                //significa que hay una persona que es HumanResource
                //tuple.item1 = oldpersonID, tuple.item2 = HumanResourceID
                int oldpersonID = oldcustomers
                    .Where(c => c.Person.Categories != null &&
                                c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.HHRR))
                    .Select(c => c.Person.ID)
                    .FirstOrDefault();
                tupleids = DataAccess.PersonDA.GetFromDuplicatesPersonByTableName(new int[] { oldpersonID }, "HumanResource");
                if (tupleids != null && tupleids.Length > 0)
                {
                    //aqui tengo que recoger los relatedHumanResourceMergeds
                    foreach (Tuple<int, int> tuple in tupleids)
                    {
                        RelatedHumanResourceMergedEntity relatedHumanResourceMerged = Helpers.RelatedHumanResourceMergedHelper.Create();
                        relatedHumanResourceMerged.EditStatus.New();
                        relatedHumanResourceMerged.PersonID = tuple.Item1;
                        relatedHumanResourceMerged.HumanResourceID = tuple.Item2;
                        relatedHumanResourceMergeds.Add(relatedHumanResourceMerged);
                    }
                }
            }
            List<RelatedPhysicianMergedEntity> relatedPhysicianMergeds = new List<RelatedPhysicianMergedEntity>();
            if (oldcustomers.Any(c => c.Person.Categories != null &&
                    c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.Physician)))
            {
                //significa que hay una persona que es Physician
                //tuple.item1 = oldpersonID, tuple.item2 = PhysicianID
                int oldpersonID = oldcustomers
                    .Where(c => c.Person.Categories != null &&
                                c.Person.Categories.Any(ct => ct.Category != null && ct.Category.CategoryKey == (int)CategoryPersonKeyEnum.Physician))
                    .Select(c => c.Person.ID)
                    .FirstOrDefault();
                tupleids = DataAccess.PersonDA.GetFromDuplicatesPersonByTableName(new int[] { oldpersonID }, "Physician");
                if (tupleids != null && tupleids.Length > 0)
                {
                    //aqui tengo que recoger los relatedPhysicianMergeds
                    foreach (Tuple<int, int> tuple in tupleids)
                    {
                        RelatedPhysicianMergedEntity relatedPhysicianMerged = Helpers.RelatedPhysicianMergedHelper.Create();
                        relatedPhysicianMerged.EditStatus.New();
                        relatedPhysicianMerged.PersonID = tuple.Item1;
                        relatedPhysicianMerged.PhysicianID = tuple.Item2;
                        relatedPhysicianMergeds.Add(relatedPhysicianMerged);
                    }
                }
            }

            int[] oldpersonIDs = oldcustomers.Select(c => c.Person.ID).Distinct().OrderBy(id => id).ToArray();
            Tuple<int, int, string, string>[] tuplereplacements = DataAccess.PersonDA.GetFromDuplicatesPersonByReplacementTables(oldpersonIDs, aaCoverElementID);
            List<RelatedPersonReplacementMergedEntity> relatedPersonReplacementMergeds = new List<RelatedPersonReplacementMergedEntity>();
            if (tuplereplacements != null && tuplereplacements.Length > 0)
            {
                //aqui tengo que recoger los relatedPhysicianMergeds
                foreach (Tuple<int, int, string, string> tuple in tuplereplacements)
                {
                    RelatedPersonReplacementMergedEntity relatedPersonReplacementMerged = Helpers.RelatedPersonReplacementMergedHelper.Create();
                    relatedPersonReplacementMerged.EditStatus.New();
                    relatedPersonReplacementMerged.PersonID = tuple.Item1;
                    relatedPersonReplacementMerged.ReplacementID = tuple.Item2;
                    relatedPersonReplacementMerged.ReplacementName = tuple.Item3;
                    relatedPersonReplacementMerged.ReplacementColumn = tuple.Item4;
                    relatedPersonReplacementMergeds.Add(relatedPersonReplacementMerged);
                }
            }

            foreach (PersonProcessMergedEntity ppm in ppmlist)
            {
                ppm.RelatedNOKMergeds = (relatedNOKMergeds.Count > 0)
                    ? relatedNOKMergeds
                            .Where(p => p.PersonID == ppm.PersonID)
                            .ToArray()
                    : null;
                ppm.RelatedContactPersonMergeds = (relatedContactPersonMergeds.Count > 0)
                    ? relatedContactPersonMergeds
                            .Where(p => p.PersonID == ppm.PersonID)
                            .ToArray()
                    : null;
                ppm.RelatedOrganizationContactPersonMergeds = (relatedOrganizationContactPersonMergeds.Count > 0)
                    ? relatedOrganizationContactPersonMergeds
                            .Where(p => p.PersonID == ppm.PersonID)
                            .ToArray()
                    : null;
                ppm.RelatedHumanResourceMergeds = (relatedHumanResourceMergeds.Count > 0)
                    ? relatedHumanResourceMergeds
                            .Where(p => p.PersonID == ppm.PersonID)
                            .ToArray()
                    : null;
                ppm.RelatedPhysicianMergeds = (relatedPhysicianMergeds.Count > 0)
                    ? relatedPhysicianMergeds
                            .Where(p => p.PersonID == ppm.PersonID)
                            .ToArray()
                    : null;
                ppm.RelatedPersonReplacementMergeds = (relatedPersonReplacementMergeds.Count > 0)
                    ? relatedPersonReplacementMergeds
                            .Where(p => p.PersonID == ppm.PersonID)
                            .ToArray()
                    : null;
            }

            return ppmlist.ToArray();
        }

        private void InnerInsertPersonProcessMergeds(int newPersonID, PersonProcessMergedEntity[] ppms, string userName)
        {
            if (ppms == null || ppms.Length <= 0 || newPersonID <= 0) return;
            foreach (PersonProcessMergedEntity ppm in ppms)
            {
                if (ppm.EditStatus.Value == StatusEntityValue.New)
                {
                    ppm.ResultPersonID = newPersonID;
                    ppm.ID = DataAccess.PersonProcessMergedDA.Insert(ppm.DuplicateGroupID, ppm.PersonID, ppm.ResultPersonID, userName);
                    if (ppm.RelatedNOKMergeds != null && ppm.RelatedNOKMergeds.Length > 0)
                    {
                        foreach (RelatedNOKMergedEntity item in ppm.RelatedNOKMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.PersonProcessMergedID = ppm.ID;
                                item.ResultPersonID = newPersonID;
                                DataAccess.RelatedNOKMergedDA.Insert(item.PersonProcessMergedID, item.PersonID, item.ResultPersonID, item.NOKID, userName);
                                DataAccess.RelatedNOKMergedDA.ReplaceNOKPerson(item.PersonID, item.ResultPersonID, item.NOKID, userName);
                            }
                        }
                    }
                    if (ppm.RelatedContactPersonMergeds != null && ppm.RelatedContactPersonMergeds.Length > 0)
                    {
                        foreach (RelatedContactPersonMergedEntity item in ppm.RelatedContactPersonMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.PersonProcessMergedID = ppm.ID;
                                item.ResultPersonID = newPersonID;
                                DataAccess.RelatedContactPersonMergedDA.Insert(item.PersonProcessMergedID, item.PersonID, item.ResultPersonID, item.CustomerContactPersonID, userName);
                                DataAccess.RelatedContactPersonMergedDA.ReplaceContactPerson(item.PersonID, item.ResultPersonID, item.CustomerContactPersonID, userName);
                            }
                        }
                    }
                    if (ppm.RelatedOrganizationContactPersonMergeds != null && ppm.RelatedOrganizationContactPersonMergeds.Length > 0)
                    {
                        foreach (RelatedOrganizationContactPersonMergedEntity item in ppm.RelatedOrganizationContactPersonMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.PersonProcessMergedID = ppm.ID;
                                item.ResultPersonID = newPersonID;
                                DataAccess.RelatedOrganizationContactPersonMergedDA.Insert(item.PersonProcessMergedID, item.PersonID, item.ResultPersonID, item.OrganizationContactPersonID, userName);
                                DataAccess.RelatedOrganizationContactPersonMergedDA.ReplaceOrganizationContactPersonPerson(item.PersonID, item.ResultPersonID, item.OrganizationContactPersonID, userName);
                            }
                        }
                    }
                    if (ppm.RelatedHumanResourceMergeds != null && ppm.RelatedHumanResourceMergeds.Length > 0)
                    {
                        foreach (RelatedHumanResourceMergedEntity item in ppm.RelatedHumanResourceMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.PersonProcessMergedID = ppm.ID;
                                item.ResultPersonID = newPersonID;
                                DataAccess.RelatedHumanResourceMergedDA.Insert(item.PersonProcessMergedID, item.PersonID, item.ResultPersonID, item.HumanResourceID, userName);
                                DataAccess.RelatedHumanResourceMergedDA.ReplaceHumanResourcePerson(item.PersonID, item.ResultPersonID, item.HumanResourceID, userName);
                            }
                        }
                    }
                    if (ppm.RelatedPhysicianMergeds != null && ppm.RelatedPhysicianMergeds.Length > 0)
                    {
                        foreach (RelatedPhysicianMergedEntity item in ppm.RelatedPhysicianMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.PersonProcessMergedID = ppm.ID;
                                item.ResultPersonID = newPersonID;
                                DataAccess.RelatedPhysicianMergedDA.Insert(item.PersonProcessMergedID, item.PersonID, item.ResultPersonID, item.PhysicianID, userName);
                                DataAccess.RelatedPhysicianMergedDA.ReplacePhysicianPerson(item.PersonID, item.ResultPersonID, item.PhysicianID, userName);
                            }
                        }
                    }
                    if (ppm.RelatedPersonReplacementMergeds != null && ppm.RelatedPersonReplacementMergeds.Length > 0)
                    {
                        foreach (RelatedPersonReplacementMergedEntity item in ppm.RelatedPersonReplacementMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.PersonProcessMergedID = ppm.ID;
                                item.ResultPersonID = newPersonID;
                                DataAccess.RelatedPersonReplacementMergedDA.Insert(item.PersonProcessMergedID, item.PersonID, item.ResultPersonID, item.ReplacementID, item.ReplacementName, item.ReplacementColumn, userName);
                                DataAccess.RelatedPersonReplacementMergedDA.ReplacePersonReplacementPerson(item.PersonID, item.ResultPersonID, item.ReplacementID, item.ReplacementName, item.ReplacementColumn, userName);
                            }
                        }
                    }
                }
            }
        }

        private void InnerInsertCustomerProcessMergeds(int newCustomerID, CustomerProcessMergedEntity[] cpms, int aaCoverElementID, string userName)
        {
            if (cpms == null || cpms.Length <= 0 || newCustomerID <= 0) return;
            foreach (CustomerProcessMergedEntity cpm in cpms)
            {
                if (cpm.EditStatus.Value == StatusEntityValue.New)
                {
                    cpm.ResultCustomerID = newCustomerID;
                    cpm.ID = DataAccess.CustomerProcessMergedDA.Insert(cpm.DuplicateGroupID, cpm.CustomerID, cpm.ResultCustomerID, userName);
                    //RelatedCustomerProcessMergedEntity
                    if (cpm.CustomerProcesses != null && cpm.CustomerProcesses.Length > 0)
                    {
                        foreach (RelatedCustomerProcessMergedEntity item in cpm.CustomerProcesses)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerProcessMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerProcessID, userName);
                            }
                        }
                        int[] customerProcessIDs = cpm.CustomerProcesses.Select(cp => cp.CustomerProcessID).Distinct().ToArray();
                        DataAccess.RelatedCustomerProcessMergedDA.ReplaceAllCustomerProcess(cpm.CustomerID, cpm.ResultCustomerID, customerProcessIDs, aaCoverElementID, userName);
                    }
                    //RelatedCustomerMedProcessMergedEntity
                    if (cpm.CustomerMedProcesses != null && cpm.CustomerMedProcesses.Length > 0)
                    {
                        foreach (RelatedCustomerMedProcessMergedEntity item in cpm.CustomerMedProcesses)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerMedProcessMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerMedProcessID, userName);
                            }
                        }
                        int[] customerMedProcessIDs = cpm.CustomerMedProcesses.Select(cp => cp.CustomerMedProcessID).Distinct().ToArray();
                        DataAccess.RelatedCustomerMedProcessMergedDA.ReplaceAllCustomerMedProcess(cpm.CustomerID, cpm.ResultCustomerID, customerMedProcessIDs, userName);
                    }
                    //RelatedCustomerOrderReqMergedEntity
                    if (cpm.RelatedCustomerOrderRequestMergeds != null && cpm.RelatedCustomerOrderRequestMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerOrderReqMergedEntity item in cpm.RelatedCustomerOrderRequestMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerMedProcessMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerOrderRequestID, userName);
                            }
                        }
                       int[] customerOrderRequestIDs = cpm.RelatedCustomerOrderRequestMergeds.Select(cp => cp.CustomerOrderRequestID).Distinct().ToArray();
                        DataAccess.RelatedCustomerOrderReqMergedDA.ReplaceAllCustomerOrderRequest(cpm.CustomerID, cpm.ResultCustomerID, customerOrderRequestIDs, userName);
                    }
                    //RelatedCustomerObsMergedEntity
                    if (cpm.RelatedCustomerObsMergeds != null && cpm.RelatedCustomerObsMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerObsMergedEntity item in cpm.RelatedCustomerObsMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerObsMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerObservationID, userName);
                            }
                        }
                        int[] customerObservationIDs = cpm.RelatedCustomerObsMergeds.Select(cp => cp.CustomerObservationID).Distinct().ToArray();
                        DataAccess.RelatedCustomerObsMergedDA.ReplaceAllCustomerObservation(cpm.CustomerID, cpm.ResultCustomerID, customerObservationIDs, userName);
                    }
                    //RelatedCustomerPolicyMergedEntity
                    if (cpm.RelatedCustomerPolicyMergeds != null && cpm.RelatedCustomerPolicyMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerPolicyMergedEntity item in cpm.RelatedCustomerPolicyMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerPolicyMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerPolicyID, userName);
                            }
                        }
                        int[] customerPolicyIDs = cpm.RelatedCustomerPolicyMergeds.Select(cp => cp.CustomerPolicyID).Distinct().ToArray();
                        DataAccess.RelatedCustomerPolicyMergedDA.ReplaceAllCustomerPolicy(cpm.CustomerID, cpm.ResultCustomerID, customerPolicyIDs, userName);
                    }
                    //RelatedCustomerCardMergedEntity
                    if (cpm.RelatedCustomerCardMergeds != null && cpm.RelatedCustomerCardMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerCardMergedEntity item in cpm.RelatedCustomerCardMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerCardMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerCardID, userName);
                            }
                        }
                        int[] customerCardIDs = cpm.RelatedCustomerCardMergeds.Select(cp => cp.CustomerCardID).Distinct().ToArray();
                        DataAccess.RelatedCustomerCardMergedDA.ReplaceAllCustomerCard(cpm.CustomerID, cpm.ResultCustomerID, customerCardIDs, userName);
                    }
                    //RelatedCustomerAccountChargeMergedEntity
                    if (cpm.RelatedCustomerAccountChargeMergeds != null && cpm.RelatedCustomerAccountChargeMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerAccountChargeMergedEntity item in cpm.RelatedCustomerAccountChargeMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerAccountChargeMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerAccountChargeID, userName);
                            }
                        }
                        int[] customerAccountChargeIDs = cpm.RelatedCustomerAccountChargeMergeds.Select(cp => cp.CustomerAccountChargeID).Distinct().ToArray();
                        DataAccess.RelatedCustomerAccountChargeMergedDA.ReplaceAllCustomerAccountCharge(cpm.CustomerID, cpm.ResultCustomerID, customerAccountChargeIDs, userName);
                    }
                    //RelatedCustomerNotifMergedEntity
                    if (cpm.RelatedCustomerNotificationMergeds != null && cpm.RelatedCustomerNotificationMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerNotifMergedEntity item in cpm.RelatedCustomerNotificationMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerNotifMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerNotificationRelID, userName);
                            }
                        }
                        int[] customerNotifIDs = cpm.RelatedCustomerNotificationMergeds.Select(cp => cp.CustomerNotificationRelID).Distinct().ToArray();
                        DataAccess.RelatedCustomerNotifMergedDA.ReplaceAllCustomerNotification(cpm.CustomerID, cpm.ResultCustomerID, customerNotifIDs, userName);
                    }
                    //RelatedBatchMovementMergedEntity
                    if (cpm.RelatedBatchMovementMergeds != null && cpm.RelatedBatchMovementMergeds.Length > 0)
                    {
                        foreach (RelatedBatchMovementMergedEntity item in cpm.RelatedBatchMovementMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedBatchMovementMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.BatchMovementID, userName);
                            }
                        }
                        int[] batchMovementIDs = cpm.RelatedBatchMovementMergeds.Select(cp => cp.BatchMovementID).Distinct().ToArray();
                        DataAccess.RelatedBatchMovementMergedDA.ReplaceAllBatchMovement(cpm.CustomerID, cpm.ResultCustomerID, batchMovementIDs, userName);
                    }
                    //RelatedCustomerNOKMergedEntity
                    if (cpm.RelatedCustomerNOKMergeds != null && cpm.RelatedCustomerNOKMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerNOKMergedEntity item in cpm.RelatedCustomerNOKMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerNOKMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.NOKID, userName);
                            }
                        }
                        int[] nokIDs = cpm.RelatedCustomerNOKMergeds.Select(cp => cp.NOKID).Distinct().ToArray();
                        DataAccess.RelatedCustomerNOKMergedDA.ReplaceAllCustomerNOK(cpm.CustomerID, cpm.ResultCustomerID, nokIDs, userName);
                    }
                    //RelatedCustomerContactPersonMergedEntity
                    if (cpm.RelatedCustomerContactPersonMergeds != null && cpm.RelatedCustomerContactPersonMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerContactPersonMergedEntity item in cpm.RelatedCustomerContactPersonMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerContactPersonMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerContactPersonID, userName);
                            }
                        }
                        int[] customerContactPersonIDs = cpm.RelatedCustomerContactPersonMergeds.Select(cp => cp.CustomerContactPersonID).Distinct().ToArray();
                        DataAccess.RelatedCustomerContactPersonMergedDA.ReplaceAllCustomerContactPerson(cpm.CustomerID, cpm.ResultCustomerID, customerContactPersonIDs, userName);
                    }
                    //RelatedCustomerContactOrgMergedEntity
                    if (cpm.RelatedCustomerContactOrgMergeds != null && cpm.RelatedCustomerContactOrgMergeds.Length > 0)
                    {
                        foreach (RelatedCustomerContactOrgMergedEntity item in cpm.RelatedCustomerContactOrgMergeds)
                        {
                            if (item.EditStatus.Value == StatusEntityValue.New)
                            {
                                item.CustomerProcessMergedID = cpm.ID;
                                item.ResultCustomerID = newCustomerID;
                                DataAccess.RelatedCustomerContactOrgMergedDA.Insert(item.CustomerProcessMergedID, item.CustomerID, item.ResultCustomerID, item.CustomerContactOrganizationID, userName);
                            }
                        }
                        int[] customerContactOrgIDs = cpm.RelatedCustomerContactOrgMergeds.Select(cp => cp.CustomerContactOrganizationID).Distinct().ToArray();
                        DataAccess.RelatedCustomerContactOrgMergedDA.ReplaceAllCustomerContactOrg(cpm.CustomerID, cpm.ResultCustomerID, customerContactOrgIDs, userName);
                    }
                }
            }
        }

        private CustomerEntity InnerMergeWithCreate(CustomerEntity newcustomer, CustomerEntity[] oldcustomers,
            PersonProcessMergedEntity[] ppms, CustomerProcessMergedEntity[] cpms, int aaCoverElementID, string userName)
        {

            int categoryID = DataAccess.CategoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.Customer);
            if (categoryID <= 0)
            {
                throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForCustomers);
            }

            CommonEntities.ElementEntity _customerMetadata = base.GetElementByName(EntityNames.CustomerEntityName, ElementBL);
            string customerIDNumber = _customerMetadata.GetCodeGeneratorName("IdentificationNumber"); ;
            string customerCHNumber = String.Empty;
            int chNumberAttributeID = 0;
            if ((_customerMetadata != null) && (_customerMetadata.Attributes != null))
            {
                CommonEntities.AttributeEntity chNumberAttribute = _customerMetadata.GetAttribute("CHNumber");
                chNumberAttributeID = chNumberAttribute.ID;
                customerCHNumber = chNumberAttribute.CodeGenerator;
            }

            List<Tuple<int, string>> careCentersWitCodeGenerator = null;
            //se pasa nulo para que no cree ningún número de HC , ya que este es un fusionado y viene con sus CHNumber
            //GetCareCentersWithCodeGenerator(_customerMetadata, chNumberAttributeID);

            using (TransactionScope scope = new TransactionScope())
            {
                newcustomer = this.InnerInsert(newcustomer, userName, categoryID, customerIDNumber, customerCHNumber, careCentersWitCodeGenerator);
                //registro de las entidades del merge
                InnerInsertPersonProcessMergeds(newcustomer.Person.ID, ppms, userName);
                InnerInsertCustomerProcessMergeds(newcustomer.ID, cpms, aaCoverElementID, userName);
                scope.Complete();
            }
            return newcustomer;
        }

        private CustomerEntity InnerMergeWithUpdate(CustomerEntity newcustomer, CustomerEntity[] oldcustomers,
            PersonProcessMergedEntity[] ppms, CustomerProcessMergedEntity[] cpms, int aaCoverElementID, string userName)
        {
            CommonEntities.ElementEntity _customerMetadata = base.GetElementByName(EntityNames.CustomerEntityName, ElementBL);
            string customerIDNumber = _customerMetadata.GetCodeGeneratorName("IdentificationNumber"); ;
            string customerCHNumber = String.Empty;
            int chNumberAttributeID = 0;
            if ((_customerMetadata != null) && (_customerMetadata.Attributes != null))
            {
                CommonEntities.AttributeEntity chNumberAttribute = _customerMetadata.GetAttribute("CHNumber");
                chNumberAttributeID = chNumberAttribute.ID;
                customerCHNumber = chNumberAttribute.CodeGenerator;
            }
            List<Tuple<int, string>> careCentersWitCodeGenerator = null;
            //se pasa nulo para que no cree ningún número de HC , ya que este es un fusionado y viene con sus CHNumber
            //GetCareCentersWithCodeGenerator(_customerMetadata, chNumberAttributeID);

            using (TransactionScope scope = new TransactionScope())
            {
                if (newcustomer.Person.EditStatus.Value == StatusEntityValue.Updated)
                {
                    newcustomer.Person = base.InnerUpdate(newcustomer.Person, userName, true);
                }
                newcustomer = this.InnerUpdate(newcustomer, userName, true, careCentersWitCodeGenerator);
                //registro de las entidades del merge
                InnerInsertPersonProcessMergeds(newcustomer.Person.ID, ppms, userName);
                InnerInsertCustomerProcessMergeds(newcustomer.ID, cpms, aaCoverElementID, userName);
                scope.Complete();
            }
            return newcustomer;

        }

        public bool MergeCustomers(CustomerEntity newcustomer, CustomerEntity[] oldcustomers)
        {
            

   
            _customerAccountDA = new CustomerAccountDA();



            try
            {
                if (newcustomer == null || newcustomer.Person == null ||
                    (newcustomer.EditStatus.Value != StatusEntityValue.New && newcustomer.EditStatus.Value != StatusEntityValue.Updated))
                    throw new ArgumentNullException("Customer");
                if (oldcustomers == null || oldcustomers.Length <= 0 || oldcustomers.Any(c => c.ID <= 0 || c.Person == null || c.Person.ID <= 0))
                    throw new ArgumentNullException("OldCustomer");

                int aaCoverElementID = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.AssistanceAgreementEntityName).ID;

                ValidationResults vr = new ValidationResults();
                ValidateCustomerRegistration(newcustomer, vr);
                /// Merge de personas customer
                ValidateMergePersons(newcustomer, oldcustomers, vr);
                /// Merge de otras categorías
                ValidateMergeOtherCategories(newcustomer, oldcustomers, vr);
                /// Merge de customers
                ValidateMergeCustomers(newcustomer, oldcustomers, vr);

                PersonProcessMergedEntity[] ppms = GetPersonProcessMergedRegisters(newcustomer, oldcustomers, aaCoverElementID);
                CustomerProcessMergedEntity[] cpms = GetCustomerProcessMergedRegisters(newcustomer, oldcustomers);

                string userName = IdentityUser.GetIdentityUserName();
                int[] allPersonIDsToUpdate = DataAccess.PersonDA.GetAllPersonIDsToUpdate(oldcustomers.Select(c => c.Person.ID).ToArray());
                if (vr.IsValid)
                {
                    switch (newcustomer.EditStatus.Value)
                    {
                        case StatusEntityValue.New:
                            newcustomer = InnerMergeWithCreate(newcustomer, oldcustomers, ppms, cpms, aaCoverElementID, userName);
                            break;
                        case StatusEntityValue.Updated:
                            newcustomer = InnerMergeWithUpdate(newcustomer, oldcustomers, ppms, cpms, aaCoverElementID, userName);
                            break;
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //genero otra transacción por si acaso
                        //final fuera de la primera transacción, pero se supone que si llega aquí no ha habido error
                        foreach (CustomerEntity oldCustome in oldcustomers)
                        {
                            //el objetivo es poner todos los customer a Superceded y todos los Person a superceded
                            //TODO: TENGO QUE REVISARLO PARA CONVERTIRLO EN UNA SOLA LÍNEA UTILIZANDO TVP COMO EL DA DE ABAJO
                            DataAccess.PersonDA.Update(oldCustome.Person.ID, (int)CommonEntities.StatusEnum.Superceded, newcustomer.Person.ID, oldCustome.Person.HasMergedRegisters, userName);
                        }
                        if (allPersonIDsToUpdate != null && allPersonIDsToUpdate.Length > 0)
                        {
                            DataAccess.PersonDA.Update(allPersonIDsToUpdate, newcustomer.ID, userName);
                        }
                        DataAccess.PersonDA.Update(newcustomer.Person.ID, (int)CommonEntities.StatusEnum.Active, 0, true, userName);
                        DataAccess.PersonProcessMergedDA.Update(ppms[0].DuplicateGroupID, newcustomer.Person.ID, userName);
                        DataAccess.CustomerProcessMergedDA.Update(cpms[0].DuplicateGroupID, newcustomer.ID, userName);

                        scope.Complete();
                    }
                    MessageHL7MergePatients(newcustomer, oldcustomers);


                    //// Crear numero de cuenta  de cliente en Tabla CustomerAccount si no existe ninguno
                    if (_customerAccountDA.GetCustomerAccountIDByCustomerID(newcustomer.ID) == 0)
                    {
                        #region Generar código cuenta de cliente
                        string accountNumber = String.Empty;
                        CodeGenerator codeGenerator = new CodeGenerator();
        
                        accountNumber = codeGenerator.Generate(String.Empty, "CuentaPaciente");
            
                        #endregion

                        _customerAccountDA.Insert(newcustomer.ID, accountNumber, 0.0m, DateTime.Now, (int)CommonEntities.StatusEnum.Active, userName);
                    }

                }
                else
                {
                    ProcessValidationResult(vr);
                }
                return vr.IsValid;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }
        #endregion

        #endregion
    }

    #region Support classes
    public class CustomerDataAccess
    {
        public CustomerDA CustomerDA { get; set; }
        public PersonDA PersonDA { get; set; }
        public PersonCatRelDA PersonCatRelDA { get; set; }
        public CategoryDA CategoryDA { get; set; }
        public OrganizationDA OrganizationDA { get; set; }
        public OrganizationCatRelDA OrganizationCatRelDA { get; set; }

        public ProfileDA ProfileDA { get; set; }
        public CustomerClassificationDA CustomerClassificationDA { get; set; }
        public CustomerAdmissionDA CustomerAdmissionDA { get; set; }

        public NOKDA NOKDA { get; set; }
        public CustomerContactPersonDA CustomerContactPersonDA { get; set; }
        public CustomerContactOrganizationDA CustomerContactOrganizationDA { get; set; }
        public LocationDA LocationDA { get; set; }

        public ProcedureActDA ProcedureActDA { get; set; }
        public RoutineActDA RoutineActDA { get; set; }
        public RoutineActResourceRelDA RoutineActResourceRelDA { get; set; }
        public RoutineStepActDA RoutineStepActDA { get; set; }
        public RoutineActHumanResourceRelDA RoutineActHumanResourceRelDA { get; set; }
        public RoutineActEquipmentRelDA RoutineActEquipmentRelDA { get; set; }
        public RoutineActActRelDA RoutineActActRelDA { get; set; }
        public RoutineActBodySiteRelDA RoutineActBodySiteRelDA { get; set; }

        public ProcedureActResourceRelDA ProcedureActResourceRelDA { get; set; }
        public ProcedureActHumanResourceRelDA ProcedureActHumanResourceRelDA { get; set; }
        public ProcedureActEquipmentRelDA ProcedureActEquipmentRelDA { get; set; }
        public ProcedureActActRelDA ProcedureActActRelDA { get; set; }
        public OrderRequestConsentRelDA OrderRequestConsentRelDA { get; set; }
        public ConsentPreprintDA ConsentPreprintDA { get; set; }
        public ConsentTypeDA ConsentTypeDA { get; set; }

        public CustomerRelatedCHNumberDA CustomerRelatedCHNumberDA { get; set; }
        public CareCenterRelatedCodeGeneratorDA CareCenterRelatedCodeGeneratorDA { get; set; }

        public CustomerProcessDA CustomerProcessDA { get; set; }
        public CustomerAccountDA CustomerAccountDA { get; set; }
        public CustomerObservationDA CustomerObservationDA { get; set; }

        public PersonProcessMergedDA PersonProcessMergedDA { get; set; }
        public CustomerProcessMergedDA CustomerProcessMergedDA { get; set; }

        //personmerge
        public RelatedContactPersonMergedDA RelatedContactPersonMergedDA { get; set; }
        public RelatedHumanResourceMergedDA RelatedHumanResourceMergedDA { get; set; }
        public RelatedPhysicianMergedDA RelatedPhysicianMergedDA { get; set; }
        public RelatedNOKMergedDA RelatedNOKMergedDA { get; set; }
        public RelatedOrganizationContactPersonMergedDA RelatedOrganizationContactPersonMergedDA { get; set; }
        public RelatedPersonReplacementMergedDA RelatedPersonReplacementMergedDA { get; set; }

        //customermerge
        public RelatedCustomerProcessMergedDA RelatedCustomerProcessMergedDA { get; set; }
        public RelatedCustomerMedProcessMergedDA RelatedCustomerMedProcessMergedDA { get; set; }
        public RelatedCustomerOrderReqMergedDA RelatedCustomerOrderReqMergedDA { get; set; }
        public RelatedCustomerObsMergedDA RelatedCustomerObsMergedDA { get; set; }
        public RelatedCustomerPolicyMergedDA RelatedCustomerPolicyMergedDA { get; set; }
        public RelatedCustomerCardMergedDA RelatedCustomerCardMergedDA { get; set; }
        public RelatedCustomerAccountChargeMergedDA RelatedCustomerAccountChargeMergedDA { get; set; }
        public RelatedCustomerNotifMergedDA RelatedCustomerNotifMergedDA { get; set; }
        public RelatedCustomerNOKMergedDA RelatedCustomerNOKMergedDA { get; set; }
        public RelatedCustomerContactPersonMergedDA RelatedCustomerContactPersonMergedDA { get; set; }
        public RelatedCustomerContactOrgMergedDA RelatedCustomerContactOrgMergedDA { get; set; }
        public RelatedBatchMovementMergedDA RelatedBatchMovementMergedDA { get; set; }
        public RelatedCustomerCHNumberMergedDA RelatedCustomerCHNumberMergedDA { get; set; }

    }

    public class CustomerHelpers
    {
        public PersonHelper PersonHelper { get; set; }
        public CustomerHelper CustomerHelper { get; set; }

        public PersonProcessMergedHelper PersonProcessMergedHelper { get; set; }
        public CustomerProcessMergedHelper CustomerProcessMergedHelper { get; set; }
        //personmerge
        public RelatedContactPersonMergedHelper RelatedContactPersonMergedHelper { get; set; }
        public RelatedHumanResourceMergedHelper RelatedHumanResourceMergedHelper { get; set; }
        public RelatedPhysicianMergedHelper RelatedPhysicianMergedHelper { get; set; }
        public RelatedNOKMergedHelper RelatedNOKMergedHelper { get; set; }
        public RelatedOrganizationContactPersonMergedHelper RelatedOrganizationContactPersonMergedHelper { get; set; }
        public RelatedPersonReplacementMergedHelper RelatedPersonReplacementMergedHelper { get; set; }

        //customermerge
        public RelatedCustomerProcessMergedHelper RelatedCustomerProcessMergedHelper { get; set; }
        public RelatedCustomerMedProcessMergedHelper RelatedCustomerMedProcessMergedHelper { get; set; }
        public RelatedCustomerOrderReqMergedHelper RelatedCustomerOrderReqMergedHelper { get; set; }
        public RelatedCustomerObsMergedHelper RelatedCustomerObsMergedHelper { get; set; }
        public RelatedCustomerPolicyMergedHelper RelatedCustomerPolicyMergedHelper { get; set; }
        public RelatedCustomerCardMergedHelper RelatedCustomerCardMergedHelper { get; set; }
        public RelatedCustomerAccountChargeMergedHelper RelatedCustomerAccountChargeMergedHelper { get; set; }
        public RelatedCustomerNotifMergedHelper RelatedCustomerNotifMergedHelper { get; set; }
        public RelatedCustomerNOKMergedHelper RelatedCustomerNOKMergedHelper { get; set; }
        public RelatedCustomerContactPersonMergedHelper RelatedCustomerContactPersonMergedHelper { get; set; }
        public RelatedCustomerContactOrgMergedHelper RelatedCustomerContactOrgMergedHelper { get; set; }
        public RelatedBatchMovementMergedHelper RelatedBatchMovementMergedHelper { get; set; }
        public RelatedCustomerCHNumberMergedHelper RelatedCustomerCHNumberMergedHelper { get; set; }
    }
    #endregion

}


