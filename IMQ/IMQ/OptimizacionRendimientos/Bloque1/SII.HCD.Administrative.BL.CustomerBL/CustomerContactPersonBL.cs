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
using SII.HCD.Common.BL;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.BL
{
    public class CustomerContactPersonBL : PersonBL, ICustomerContactPersonService
    {
        #region Consts
        //private const string CustomerContactPersonEntityName = "CustomerContactPersonEntity";
        #endregion

        #region DA definition
        private CustomerContactPersonDA _customerContactPersonDA;
        private PersonDA _personDA;
        private PersonCatRelDA _personCatRelDA;
        private CategoryDA _categoryDA;
        private ContactTypeDA _contactTypeDA;
        #endregion

        #region Constructors
        public CustomerContactPersonBL()
        {
            _customerContactPersonDA = new CustomerContactPersonDA();
            _personDA = new PersonDA();
            _personCatRelDA = new PersonCatRelDA();
            _categoryDA = new CategoryDA();
            _contactTypeDA = new ContactTypeDA();
        }
        #endregion

        #region private methods
        private void ResetCustomerContactPerson(CustomerContactPersonEntity customerContactPerson)
        {
            customerContactPerson.EditStatus.Reset();
            base.ResetPerson(customerContactPerson.Person);
        }

        private CustomerContactPersonEntity InnerInsert(CustomerContactPersonEntity customerContactPerson, string userName, int categoryID)
        {
            #region Person
            switch (customerContactPerson.Person.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    {
                        customerContactPerson.Person = base.InnerInsert(customerContactPerson.Person, userName);
                        break;
                    }
                case StatusEntityValue.Updated:
                    {
                        customerContactPerson.Person = base.InnerUpdate(customerContactPerson.Person, userName, true);
                        break;
                    }
                default: break;
            }
            #endregion

            customerContactPerson.ID = _customerContactPersonDA.Insert(customerContactPerson.Person.ID, customerContactPerson.CustomerID,
                (customerContactPerson.ContactType == null) ? 0 : customerContactPerson.ContactType.ID, customerContactPerson.UrgentContact,
                customerContactPerson.AlternativeContact, userName);
            customerContactPerson.DBTimeStamp = _customerContactPersonDA.GetDBTimeStamp(customerContactPerson.ID);

            if (_customerContactPersonDA.GetCustomerContactPersonsFromPerson(customerContactPerson.Person.ID) == 1)
            {
                _personCatRelDA.Insert(customerContactPerson.Person.ID, categoryID, userName);
            }

            return customerContactPerson;
        }

        private CustomerContactPersonEntity InnerUpdate(CustomerContactPersonEntity customerContactPerson, string userName)
        {
            Int64 dbTimeStamp = _customerContactPersonDA.GetDBTimeStamp(customerContactPerson.ID);
            if (dbTimeStamp != customerContactPerson.DBTimeStamp)
                throw new Exception(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customerContactPerson.ID));

            _customerContactPersonDA.Update(customerContactPerson.ID, customerContactPerson.CustomerID,
                (customerContactPerson.ContactType == null) ? 0 : customerContactPerson.ContactType.ID, customerContactPerson.UrgentContact,
                customerContactPerson.AlternativeContact, userName);

            customerContactPerson.DBTimeStamp = _customerContactPersonDA.GetDBTimeStamp(customerContactPerson.ID);

            return customerContactPerson;
        }

        private CustomerContactPersonEntity Insert(CustomerContactPersonEntity customerContactPerson)
        {
            if (customerContactPerson == null) throw new ArgumentNullException("customerContactPerson");

            string userName = IdentityUser.GetIdentityUserName();
            int categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.CustContactPerson);
            if (categoryID <= 0)
            {
                throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForCustomerContactPersons);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerInsert(customerContactPerson, userName, categoryID);

                scope.Complete();
            }

            this.ResetCustomerContactPerson(customerContactPerson);
            LOPDLogger.Write(EntityNames.CustomerContactPersonEntityName, customerContactPerson.ID, ActionType.Create);
            return customerContactPerson;
        }

        private CustomerContactPersonEntity Update(CustomerContactPersonEntity customerContactPerson)
        {
            if (customerContactPerson == null) throw new ArgumentNullException("customerContactPerson");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                if (customerContactPerson.Person.EditStatus.Value == StatusEntityValue.Updated)
                {
                    customerContactPerson.Person = base.InnerUpdate(customerContactPerson.Person, userName, true);
                }
                //else
                //{
                //    customerContactPerson.Person = base.InnerUpdate(customerContactPerson.Person, userName, false);
                //}

                this.InnerUpdate(customerContactPerson, userName);

                scope.Complete();
            }

            this.ResetCustomerContactPerson(customerContactPerson);
            LOPDLogger.Write(EntityNames.CustomerContactPersonEntityName, customerContactPerson.ID, ActionType.Modify);
            return customerContactPerson;
        }

        private void ValidateCustomerContactPerson(CustomerContactPersonEntity customerContactPerson, ElementBL elementBL)
        {
            if (customerContactPerson == null) throw new ArgumentNullException("customerContactPerson");

            CommonEntities.ElementEntity _customerContactPersonMetadata = base.GetElementByName(EntityNames.CustomerContactPersonEntityName, elementBL);
            CustomerContactPersonHelper customerContactPersonHelper = new CustomerContactPersonHelper(_customerContactPersonMetadata);

            ValidationResults result = customerContactPersonHelper.Validate(customerContactPerson);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_CustomerContactPersonValidationError, sb));
            }

            //base.ValidatePerson(customerContactPerson.Person, elementBL);
        }

        #region CheckPreconditions
        private void CheckInsertPreconditions(CustomerContactPersonEntity customerContactPerson, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (customerContactPerson == null) throw new ArgumentNullException("customerContactPerson");

            ValidateCustomerContactPerson(customerContactPerson, elementBL);

            #region Comentado por SALVA
            //PersonFindRequest personFind = new PersonFindRequest();
            //BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;

            //if (backOfficeConfig.EntitySettings.PersonEntity.Attributes != null)
            //{
            //    foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.PersonEntity.Attributes)
            //    {
            //        if ((attrib.Name == "FirstName") && (attrib.Mandatory))
            //        {
            //            personFind.FirstName = customerContactPerson.Person.FirstName;
            //            personFind.MandatoryFirstName = true;
            //        }

            //        if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //        {
            //            personFind.LastName = customerContactPerson.Person.LastName;
            //            personFind.MandatoryLastName = true;
            //        }
            //    }
            //}
            #endregion

            homonymPersons = null;

            switch (customerContactPerson.Person.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    //int id = _personDA.GetPerson(personFind);
                    //if (id > 0)
                    //{
                    //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(customerContactPerson.Person.FirstName, " ", customerContactPerson.Person.LastName)));
                    //}
                    //DO SALVA: Llamamos al algoritmo de validación de Persona.
                    base.CheckInsertPreconditions(customerContactPerson.Person, customerContactPerson.CustomerID, CategoryPersonKeyEnum.CustContactPerson, 
                        forceSave, true, true, out homonymPersons, elementBL);
                    break;
                case StatusEntityValue.Updated:
                    //int id2 = _personDA.GetPerson(personFind);
                    //if ((id2 > 0) && (id2 != customerContactPerson.Person.ID))
                    //{
                    //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(customerContactPerson.Person.FirstName, " ", customerContactPerson.Person.LastName)));
                    //}
                    //DO SALVA
                    base.CheckUpdatePreconditions(customerContactPerson.Person, customerContactPerson.CustomerID, CategoryPersonKeyEnum.CustContactPerson,
                        forceSave, true, true, out homonymPersons, elementBL);
                    break;
            }
        }

        protected virtual void CheckUpdatePreconditions(CustomerContactPersonEntity customerContactPerson, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (customerContactPerson == null) throw new ArgumentNullException("customerContactPerson");

            ValidateCustomerContactPerson(customerContactPerson, elementBL);

            #region Comentado por SALVA
            //PersonFindRequest personFind = new PersonFindRequest();
            //BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            //if (backOfficeConfig != null)
            //{
            //    if (backOfficeConfig.EntitySettings.PersonEntity.Attributes != null)
            //    {
            //        foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.PersonEntity.Attributes)
            //        {
            //            if ((attrib.Name == "FirstName") && (attrib.Mandatory))
            //            {
            //                personFind.FirstName = customerContactPerson.Person.FirstName;
            //                personFind.MandatoryFirstName = true;
            //            }

            //            if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //            {
            //                personFind.LastName = customerContactPerson.Person.LastName;
            //                personFind.MandatoryLastName = true;
            //            }
            //        }
            //    }
            //}

            //DO SALVA
            //int id = _personDA.GetPerson(personFind);
            //if ((id > 0) && (id != customerContactPerson.Person.ID))
            //{
            //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(customerContactPerson.Person.FirstName, " ", customerContactPerson.Person.LastName)));
            //}
            #endregion

            homonymPersons = null;

            //DO SALVA
            base.CheckUpdatePreconditions(customerContactPerson.Person, customerContactPerson.CustomerID, CategoryPersonKeyEnum.CustContactPerson, 
                forceSave, false, true, out homonymPersons, elementBL);
        }
        #endregion

        #endregion

        #region ICustomerContactPersonService members
        public int Delete(int customerContactPersonID, int personID)
        {
            try
            {
                int result = 0;
                int categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.CustContactPerson);
                if (categoryID <= 0)
                {
                    throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForCCPs);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    result = _customerContactPersonDA.Delete(customerContactPersonID);
                    if (_customerContactPersonDA.GetCustomerContactPersonsFromPerson(personID) == 0)
                    {
                        _personCatRelDA.Delete(personID, categoryID);
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

        public CustomerContactPersonEntity Save(CustomerContactPersonEntity customerContactPerson, bool forceSave, out PersonAddressListDTO[] homonymPersons)
        {
            try
            {
                if (customerContactPerson == null) throw new ArgumentNullException("customerContactPerson");

                ElementBL _elementBL = new ElementBL();
                homonymPersons = null;

                switch (customerContactPerson.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return customerContactPerson;
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(customerContactPerson, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return customerContactPerson;
                        return this.Insert(customerContactPerson);
                    case StatusEntityValue.NewAndDeleted:
                        return customerContactPerson;
                    case StatusEntityValue.None:
                        CheckUpdatePreconditions(customerContactPerson, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return customerContactPerson;
                        if ((customerContactPerson.Person != null) && (customerContactPerson.Person.EditStatus.Value == StatusEntityValue.Updated))
                        {
                            customerContactPerson.Person = base.Update(customerContactPerson.Person);
                        }
                        return customerContactPerson;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(customerContactPerson, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return customerContactPerson;
                        return this.Update(customerContactPerson);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                homonymPersons = null;
                return null;
            }
        }

        public CustomerContactPersonListDTO[] GetCustomerContactPersons(int customerID)
        {
            try
            {
                CustomerContactPersonListDTOAdapter customerContactPersonListDTOAdapter = new CustomerContactPersonListDTOAdapter();

                DataSet ds = _customerContactPersonDA.GetCustomerContactPersons(customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonListDTOTable)))
                {
                    CustomerContactPersonListDTO[] customerContactPersons = customerContactPersonListDTOAdapter.GetData(ds);
                    return customerContactPersons;
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
        public CustomerContactPersonEntity GetCustomerContactPerson(int customerContactPersonID)
        {
            try
            {
                PersonBL personBL = new PersonBL();

                DataSet ds = _customerContactPersonDA.GetCustomerContactPerson(customerContactPersonID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonTable].Rows.Count > 0))
                {
                    int personID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonTable].Rows[0]["PersonID"].ToString(), 0);
                    int contactTypeID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonTable].Rows[0]["ContactTypeID"].ToString(), 0);

                    DataSet ds2;

                    #region Contact Types
                    ds2 = _contactTypeDA.GetContactTypeByID(contactTypeID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.ContactTypeTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.ContactTypeTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Person
                    if (personID <= 0)
                    {
                        throw new Exception(Properties.Resources.ERROR_CustomerContactPersonNotFound);
                    }
                    SII.HCD.BackOffice.Entities.PersonEntity myPerson = personBL.GetPerson(personID);
                    #endregion

                    CustomerContactPersonAdvancedAdapter customerContactPersonAdapter = new CustomerContactPersonAdvancedAdapter();
                    CustomerContactPersonEntity result = customerContactPersonAdapter.GetInfo(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerContactPersonTable].Rows[0], ds);
                    result.Person = myPerson;
                    LOPDLogger.Write(EntityNames.CustomerContactPersonEntityName, customerContactPersonID, ActionType.View);
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
        public CustomerContactPersonEntity GetCustomerContactPerson(int customerContactPersonID)
        {
            try
            {
                #region Person
                PersonBL personBL = new PersonBL();
                int personID = 0;
                personID = personBL.obtenerPersonID_From_CustomerContactPerson(customerContactPersonID);

                if (personID == 0) throw new Exception(Properties.Resources.ERROR_CustomerContactPersonNotFound);
                PersonEntity myPerson = null;
                var HiloPerson = System.Threading.Tasks.Task.Factory.StartNew(() => myPerson = personBL.GetPerson(personID));
                #endregion

                DataSet ds = _customerContactPersonDA.GetCustomerContactPerson(customerContactPersonID);
                if ((ds.Tables != null) && (ds.Tables.Contains(Entities.TableNames.CustomerContactPersonTable)) && (ds.Tables[Entities.TableNames.CustomerContactPersonTable].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());

                    CustomerContactPersonAdvancedAdapter customerContactPersonAdapter = new CustomerContactPersonAdvancedAdapter();
                    CustomerContactPersonEntity result = customerContactPersonAdapter.GetInfo(ds.Tables[Entities.TableNames.CustomerContactPersonTable].Rows[0], ds2);
                    HiloPerson.Wait();
                    result.Person = myPerson;
                    LOPDLogger.Write(EntityNames.CustomerContactPersonEntityName, customerContactPersonID, ActionType.View);
                    ds2.Dispose();
                    ds.Dispose();
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
