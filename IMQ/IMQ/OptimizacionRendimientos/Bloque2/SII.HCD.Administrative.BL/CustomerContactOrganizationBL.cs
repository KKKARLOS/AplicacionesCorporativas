using System;
using System.Data;
using System.Text;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using SII.Framework.Common;
using SII.Framework.Entities.Services;
using SII.Framework.ExceptionHandling;
using SII.Framework.Interfaces;
using SII.Framework.Logging.LOPD;
using SII.HCD.Administrative.DA;
using SII.HCD.Administrative.Entities;
using SII.HCD.Administrative.Services;
using SII.HCD.BackOffice.BL;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Configuration;
using SII.HCD.Misc;
using SII.SIFP.Configuration;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.BL
{
    public class CustomerContactOrganizationBL : OrganizationBL, ICustomerContactOrganizationService
    {
        #region Consts
        //private const string CustomerContactOrganizationEntityName = "CustomerContactOrganizationEntity";
        #endregion

        #region DA definition
        private CustomerContactOrganizationDA _customerContactOrganizationDA;
        private OrganizationDA _organizationDA;
        private OrganizationCatRelDA _organizationCatRelDA;
        private CategoryDA _categoryDA;
        private ContactTypeDA _contactTypeDA;
        #endregion

        #region Constructors
        public CustomerContactOrganizationBL()
        {
            _customerContactOrganizationDA = new CustomerContactOrganizationDA();
            _organizationDA = new OrganizationDA();
            _organizationCatRelDA = new OrganizationCatRelDA();
            _categoryDA = new CategoryDA();
            _contactTypeDA = new ContactTypeDA();
        }
        #endregion

        #region private methods
        private void ResetCustomerContactOrganization(CustomerContactOrganizationEntity customerContactOrganization)
        {
            customerContactOrganization.EditStatus.Reset();
            base.ResetOrganization(customerContactOrganization.Organization);
        }

        private CustomerContactOrganizationEntity InnerInsert(CustomerContactOrganizationEntity customerContactOrganization, string userName, int categoryID)
        {
            #region Organization
            switch (customerContactOrganization.Organization.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    {
                        customerContactOrganization.Organization = base.InnerInsert(customerContactOrganization.Organization, userName);
                        break;
                    }
                case StatusEntityValue.Updated:
                    {
                        customerContactOrganization.Organization = base.InnerUpdate(customerContactOrganization.Organization, userName, true);
                        break;
                    }
                default: break;
            }
            #endregion

            customerContactOrganization.ID = _customerContactOrganizationDA.Insert(customerContactOrganization.Organization.ID, customerContactOrganization.CustomerID,
                (customerContactOrganization.ContactType == null) ? 0 : customerContactOrganization.ContactType.ID, customerContactOrganization.UrgentContact,
                customerContactOrganization.AlternativeContact, userName);
            customerContactOrganization.DBTimeStamp = _customerContactOrganizationDA.GetDBTimeStamp(customerContactOrganization.ID);

            if (_customerContactOrganizationDA.GetCustomerContactOrganizationsFromOrganization(customerContactOrganization.Organization.ID) == 1)
            {
                _organizationCatRelDA.Insert(customerContactOrganization.Organization.ID, categoryID, userName);
            }

            return customerContactOrganization;
        }

        private CustomerContactOrganizationEntity InnerUpdate(CustomerContactOrganizationEntity customerContactOrganization, string userName)
        {
            Int64 dbTimeStamp = _customerContactOrganizationDA.GetDBTimeStamp(customerContactOrganization.ID);
            if (dbTimeStamp != customerContactOrganization.DBTimeStamp)
                throw new Exception(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customerContactOrganization.ID));

            _customerContactOrganizationDA.Update(customerContactOrganization.ID, customerContactOrganization.CustomerID,
                (customerContactOrganization.ContactType == null) ? 0 : customerContactOrganization.ContactType.ID, customerContactOrganization.UrgentContact,
                customerContactOrganization.AlternativeContact, userName);

            customerContactOrganization.DBTimeStamp = _customerContactOrganizationDA.GetDBTimeStamp(customerContactOrganization.ID);

            return customerContactOrganization;
        }

        private CustomerContactOrganizationEntity Insert(CustomerContactOrganizationEntity customerContactOrganization)
        {
            if (customerContactOrganization == null) throw new ArgumentNullException("customerContactOrganization");

            string userName = IdentityUser.GetIdentityUserName();
            int categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)CategoryOrganizationKeyEnum.CustContactOrg);
            if (categoryID <= 0)
            {
                throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForCustomerContactOrganizations);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerInsert(customerContactOrganization, userName, categoryID);

                scope.Complete();
            }

            this.ResetCustomerContactOrganization(customerContactOrganization);
            LOPDLogger.Write(EntityNames.CustomerContactOrganizationEntityName, customerContactOrganization.ID, ActionType.Create);
            return customerContactOrganization;
        }

        private CustomerContactOrganizationEntity Update(CustomerContactOrganizationEntity customerContactOrganization)
        {
            if (customerContactOrganization == null) throw new ArgumentNullException("customerContactOrganization");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                if (customerContactOrganization.Organization.EditStatus.Value == StatusEntityValue.Updated)
                {
                    customerContactOrganization.Organization = base.InnerUpdate(customerContactOrganization.Organization, userName, true);
                }
                //else
                //{
                //    customerContactOrganization.Organization = base.InnerUpdate(customerContactOrganization.Organization, userName, false);
                //}

                this.InnerUpdate(customerContactOrganization, userName);
                LOPDLogger.Write(EntityNames.CustomerContactOrganizationEntityName, customerContactOrganization.ID, ActionType.Modify);
                scope.Complete();
            }

            this.ResetCustomerContactOrganization(customerContactOrganization);
            return customerContactOrganization;
        }

        private void ValidateCustomerContactOrganization(CustomerContactOrganizationEntity customerContactOrganization)
        {
            if (customerContactOrganization == null) throw new ArgumentNullException("customerContactOrganization");

            CommonEntities.ElementEntity _customerContactOrganizationMetadata = base.GetElementByName(EntityNames.CustomerContactOrganizationEntityName);
            CustomerContactOrganizationHelper customerContactOrganizationHelper = new CustomerContactOrganizationHelper(_customerContactOrganizationMetadata);

            ValidationResults result = customerContactOrganizationHelper.Validate(customerContactOrganization);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_CustomerContactOrganizationValidationError, sb));
            }

            base.ValidateOrganization(customerContactOrganization.Organization, EntityNames.CustomerContactOrganizationEntityName);
        }

        #region CheckPreconditions
        private void CheckInsertPreconditions(CustomerContactOrganizationEntity customerContactOrganization)
        {
            if (customerContactOrganization == null) throw new ArgumentNullException("customerContactOrganization");

            ValidateCustomerContactOrganization(customerContactOrganization);

            OrganizationFindRequest organizationFind = new OrganizationFindRequest();
            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;

            if (backOfficeConfig.EntitySettings.OrganizationEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.OrganizationEntity.Attributes)
                {
                    if ((attrib.Name == "Name") && (attrib.Mandatory))
                    {
                        organizationFind.Name = customerContactOrganization.Organization.Name;
                        organizationFind.MandatoryName = true;
                    }
                }
            }

            switch (customerContactOrganization.Organization.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    int id = _organizationDA.GetOrganization(organizationFind);
                    if (id > 0)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_OrganizationAlreadyExists, customerContactOrganization.Organization.Name));
                    }
                    break;
                case StatusEntityValue.Updated:
                    int id2 = _organizationDA.GetOrganization(organizationFind);
                    if ((id2 > 0) && (id2 != customerContactOrganization.Organization.ID))
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_OrganizationAlreadyExists, customerContactOrganization.Organization.Name));
                    }
                    break;
            }
        }

        protected virtual void CheckUpdatePreconditions(CustomerContactOrganizationEntity customerContactOrganization)
        {
            if (customerContactOrganization == null) throw new ArgumentNullException("customerContactOrganization");

            ValidateCustomerContactOrganization(customerContactOrganization);

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
                            organizationFind.Name = customerContactOrganization.Organization.Name;
                            organizationFind.MandatoryName = true;
                        }
                    }
                }
            }

            int id = _organizationDA.GetOrganization(organizationFind);
            if ((id > 0) && (id != customerContactOrganization.Organization.ID))
            {
                throw new Exception(string.Format(Properties.Resources.MSG_OrganizationAlreadyExists, customerContactOrganization.Organization.Name));
            }
        }
        #endregion

        #endregion

        #region ICustomerContactOrganizationService members
        public int Delete(int customerContactOrganizationID, int organizationID)
        {
            try
            {
                int result = 0;
                int categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)CategoryOrganizationKeyEnum.CustContactOrg);
                if (categoryID <= 0)
                {
                    throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForCCOs);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    result = _customerContactOrganizationDA.Delete(customerContactOrganizationID);
                    if (_customerContactOrganizationDA.GetCustomerContactOrganizationsFromOrganization(organizationID) == 0)
                    {
                        _organizationCatRelDA.Delete(organizationID, categoryID);
                    }
                    scope.Complete();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public CustomerContactOrganizationEntity Save(CustomerContactOrganizationEntity customerContactOrganization)
        {
            try
            {
                if (customerContactOrganization == null)
                    throw new ArgumentNullException("customerContactOrganization");

                switch (customerContactOrganization.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return customerContactOrganization;
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(customerContactOrganization);
                        return this.Insert(customerContactOrganization);
                    case StatusEntityValue.NewAndDeleted:
                        return customerContactOrganization;
                    case StatusEntityValue.None:
                        CheckUpdatePreconditions(customerContactOrganization);
                        if ((customerContactOrganization.Organization != null) && (customerContactOrganization.Organization.EditStatus.Value == StatusEntityValue.Updated))
                        {
                            customerContactOrganization.Organization = base.Update(customerContactOrganization.Organization);
                        }
                        return customerContactOrganization;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(customerContactOrganization);
                        return this.Update(customerContactOrganization);
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

        public CustomerContactOrganizationListDTO[] GetCustomerContactOrganizations(int customerID)
        {
            try
            {
                CustomerContactOrganizationListDTOAdapter customerContactOrganizationListDTOAdapter = new CustomerContactOrganizationListDTOAdapter();

                DataSet ds = _customerContactOrganizationDA.GetCustomerContactOrganizations(customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationListDTOTable)))
                {
                    CustomerContactOrganizationListDTO[] customerContactOrganizations = customerContactOrganizationListDTOAdapter.GetData(ds);
                    return customerContactOrganizations;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
/*
        public CustomerContactOrganizationEntity GetCustomerContactOrganization(int customerContactOrganizationID)
        {
            try
            {
                OrganizationBL organizationBL = new OrganizationBL();

                DataSet ds = _customerContactOrganizationDA.GetCustomerContactOrganization(customerContactOrganizationID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationTable].Rows.Count > 0))
                {
                    int organizationID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationTable].Rows[0]["OrganizationID"].ToString(), 0);
                    int contactTypeID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationTable].Rows[0]["ContactTypeID"].ToString(), 0);

                    DataSet ds2;

                    #region Contact Types
                    ds2 = _contactTypeDA.GetContactTypeByID(contactTypeID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.ContactTypeTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.ContactTypeTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Organization
                    if (organizationID <= 0)
                    {
                        throw new Exception(Properties.Resources.ERROR_CustomerContactOrganizationNotFound);
                    }
                    SII.HCD.BackOffice.Entities.OrganizationEntity myOrganization = organizationBL.GetOrganization(organizationID);
                    #endregion

                    CustomerContactOrganizationAdvancedAdapter customerContactOrganizationAdapter = new CustomerContactOrganizationAdvancedAdapter();
                    CustomerContactOrganizationEntity result = customerContactOrganizationAdapter.GetInfo(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactOrganizationTable].Rows[0], ds);
                    result.Organization = myOrganization;
                    LOPDLogger.Write(EntityNames.CustomerContactOrganizationEntityName, customerContactOrganizationID, ActionType.View);
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
        public CustomerContactOrganizationEntity GetCustomerContactOrganization(int customerContactOrganizationID)
        {
            try
            {
                OrganizationBL organizationBL = new OrganizationBL();
                DataSet ds = _customerContactOrganizationDA.GetCustomerContactOrganization(customerContactOrganizationID);

                if ((ds.Tables != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerContactOrganizationTable)) && (ds.Tables[Administrative.Entities.TableNames.CustomerContactOrganizationTable].Rows.Count > 0))
                {
                    int organizationID = SIIConvert.ToInteger(ds.Tables[Administrative.Entities.TableNames.CustomerContactOrganizationTable].Rows[0]["OrganizationID"].ToString(), 0);

                    #region Organization
                    if (organizationID <= 0) throw new Exception(Properties.Resources.ERROR_CustomerContactOrganizationNotFound);

                    SII.HCD.BackOffice.Entities.OrganizationEntity myOrganization = null;
                    var HiloOrganization = System.Threading.Tasks.Task.Factory.StartNew(() => myOrganization = organizationBL.GetOrganization(organizationID));

                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());
                    #endregion

                    CustomerContactOrganizationAdvancedAdapter customerContactOrganizationAdapter = new CustomerContactOrganizationAdvancedAdapter();
                    CustomerContactOrganizationEntity result = customerContactOrganizationAdapter.GetInfo(ds.Tables[Administrative.Entities.TableNames.CustomerContactOrganizationTable].Rows[0], ds2);
                    HiloOrganization.Wait();
                    result.Organization = myOrganization;

                    LOPDLogger.Write(EntityNames.CustomerContactOrganizationEntityName, customerContactOrganizationID, ActionType.View);
                    ds.Dispose();
                    ds2.Dispose();
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
        #endregion
    }
}