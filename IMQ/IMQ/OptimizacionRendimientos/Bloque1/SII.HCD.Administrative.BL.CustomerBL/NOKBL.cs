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
using SII.HCD.BackOffice.BL;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;
using SII.HCD.Common.BL;
using SII.HCD.Administrative.Services;

namespace SII.HCD.Administrative.BL
{
    public class NOKBL : PersonBL, INOKService
    {
        #region Consts
        //private const string NOKEntityName = "NOKEntity";
        #endregion

        #region DA definition
        private NOKDA _nokDA;
        private PersonDA _personDA;
        private PersonCatRelDA _personCatRelDA;
        private CategoryDA _categoryDA;
        private KinshipDA _kinshipDA;
        private CustomerDA _customerDA;
        #endregion

        #region Constructors
        public NOKBL()
        {
            _nokDA = new NOKDA();
            _personDA = new PersonDA();
            _personCatRelDA = new PersonCatRelDA();
            _categoryDA = new CategoryDA();
            _kinshipDA = new KinshipDA();
            _customerDA = new CustomerDA();
        }
        #endregion

        #region private methods
        private void ResetNOK(NOKEntity nok)
        {
            nok.EditStatus.Reset();
            base.ResetPerson(nok.Person);
        }

        private NOKEntity InnerInsert(NOKEntity nok, string userName, int categoryID)
        {
            //Int64 mainDBTimeStamp = _customerDA.GetDBTimeStamp(nok.CustomerID);
            //if (mainDBTimeStamp != nok.MainDBTimeStamp)
            //    throw new Exception(
            //        string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, nok.CustomerID));

            #region Person
            switch (nok.Person.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    {
                        nok.Person = base.InnerInsert(nok.Person, userName);
                        break;
                    }
                case StatusEntityValue.Updated:
                    {
                        nok.Person = base.InnerUpdate(nok.Person, userName, true);
                        break;
                    }
                default: break;
            }
            #endregion

            nok.ID = _nokDA.Insert(nok.Person.ID, nok.CustomerID, (nok.Kinship == null) ? 0 : nok.Kinship.ID, nok.UrgentContact, nok.AlternativeContact, userName);
            nok.DBTimeStamp = _nokDA.GetDBTimeStamp(nok.ID);

            if (_nokDA.GetNOKsFromPerson(nok.Person.ID) == 1)
            {
                _personCatRelDA.Insert(nok.Person.ID, categoryID, userName);
            }

            //_customerDA.Update(nok.CustomerID, userName);
            //nok.MainDBTimeStamp = _customerDA.GetDBTimeStamp(nok.CustomerID);

            return nok;
        }

        private NOKEntity InnerUpdate(NOKEntity nok, string userName)
        {
            Int64 dbTimeStamp = _nokDA.GetDBTimeStamp(nok.ID);
            if (dbTimeStamp != nok.DBTimeStamp)
                throw new Exception(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, nok.ID));

            //Int64 mainDBTimeStamp = _customerDA.GetDBTimeStamp(nok.CustomerID);
            //if (mainDBTimeStamp != nok.MainDBTimeStamp)
            //    throw new Exception(
            //        string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, nok.CustomerID));

            _nokDA.Update(nok.ID, nok.CustomerID, (nok.Kinship == null) ? 0 : nok.Kinship.ID, nok.UrgentContact, nok.AlternativeContact, userName);
            nok.DBTimeStamp = _nokDA.GetDBTimeStamp(nok.ID);

            //_customerDA.Update(nok.CustomerID, userName);
            //nok.MainDBTimeStamp = _customerDA.GetDBTimeStamp(nok.CustomerID);

            return nok;
        }

        private NOKEntity Insert(NOKEntity nok)
        {
            if (nok == null) throw new ArgumentNullException("nok");

            string userName = IdentityUser.GetIdentityUserName();
            int categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.NOK);
            if (categoryID <= 0)
            {
                throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForNOKs);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerInsert(nok, userName, categoryID);

                scope.Complete();
            }

            this.ResetNOK(nok);
            LOPDLogger.Write(EntityNames.NOKEntityName, nok.ID, ActionType.Create);
            return nok;
        }

        private NOKEntity Update(NOKEntity nok)
        {
            if (nok == null) throw new ArgumentNullException("nok");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                if (nok.Person.EditStatus.Value == StatusEntityValue.Updated)
                {
                    nok.Person = base.InnerUpdate(nok.Person, userName, true);
                }

                this.InnerUpdate(nok, userName);

                scope.Complete();
            }

            this.ResetNOK(nok);
            LOPDLogger.Write(EntityNames.NOKEntityName, nok.ID, ActionType.Modify);
            return nok;
        }

        private void ValidateNOK(NOKEntity nok, ElementBL elementBL)
        {
            if (nok == null) throw new ArgumentNullException("nok");

            CommonEntities.ElementEntity _nokMetadata = base.GetElementByName(EntityNames.NOKEntityName, elementBL);
            NOKHelper nokHelper = new NOKHelper(_nokMetadata);

            ValidationResults result = nokHelper.Validate(nok);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_nokValidationError, sb));
            }

            //base.ValidatePerson(nok.Person, elementBL);
        }

        #region CheckPreconditions
        private void CheckInsertPreconditions(NOKEntity nok, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (nok == null) throw new ArgumentNullException("nok");

            ValidateNOK(nok, elementBL);

            #region Comentado por SALVA
            //PersonFindRequest personFind = new PersonFindRequest();
            //BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            //if (backOfficeConfig.EntitySettings.PersonEntity.Attributes != null)
            //{
            //    foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.PersonEntity.Attributes)
            //    {
            //        if ((attrib.Name == "FirstName") && (attrib.Mandatory))
            //        {
            //            personFind.FirstName = nok.Person.FirstName;
            //            personFind.MandatoryFirstName = true;
            //        }

            //        if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //        {
            //            personFind.LastName = nok.Person.LastName;
            //            personFind.MandatoryLastName = true;
            //        }
            //    }
            //}
            #endregion

            homonymPersons = null;

            switch (nok.Person.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    //int id = _personDA.GetPerson(personFind);
                    //if (id > 0)
                    //{
                    //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(nok.Person.FirstName, " ", nok.Person.LastName)));
                    //}
                    //DO SALVA
                    base.CheckInsertPreconditions(nok.Person, nok.CustomerID, CategoryPersonKeyEnum.NOK, forceSave, false, true, out homonymPersons, elementBL);
                    break;
                case StatusEntityValue.Updated:
                    //int id2 = _personDA.GetPerson(personFind);
                    //if ((id2 > 0) && (id2 != nok.Person.ID))
                    //{
                    //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(nok.Person.FirstName, " ", nok.Person.LastName)));
                    //}
                    //DO SALVA
                    base.CheckUpdatePreconditions(nok.Person, nok.CustomerID, CategoryPersonKeyEnum.NOK, forceSave, false, true, out homonymPersons, elementBL);
                    break;
            }
        }

        protected virtual void CheckUpdatePreconditions(NOKEntity nok, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (nok == null) throw new ArgumentNullException("nok");

            ValidateNOK(nok, elementBL);

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
            //                personFind.FirstName = nok.Person.FirstName;
            //                personFind.MandatoryFirstName = true;
            //            }

            //            if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //            {
            //                personFind.LastName = nok.Person.LastName;
            //                personFind.MandatoryLastName = true;
            //            }
            //        }
            //    }
            //}
            //int id = _personDA.GetPerson(personFind);
            //if ((id > 0) && (id != nok.Person.ID))
            //{
            //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(nok.Person.FirstName, " ", nok.Person.LastName)));
            //}
            #endregion

            base.CheckUpdatePreconditions(nok.Person, nok.CustomerID, CategoryPersonKeyEnum.NOK, forceSave, false, true, out homonymPersons, elementBL);
        }
        #endregion

        #endregion

        #region INOKService members
        public int Delete(int nokID, int personID, out Int64 mainDBTimeStamp)
        {
            mainDBTimeStamp = 0;
            try
            {
                int result = 0;
                string userName = IdentityUser.GetIdentityUserName();
                int categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.NOK);
                if (categoryID <= 0)
                {
                    throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForNOKs);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    //_customerDA.Update(customerID, userName);
                    //mainDBTimeStamp = _customerDA.GetDBTimeStamp(customerID);
                    result = _nokDA.Delete(nokID);
                    if (_nokDA.GetNOKsFromPerson(personID) == 0)
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

        public NOKEntity Save(NOKEntity nok, bool forceSave, out PersonAddressListDTO[] homonymPersons)
        {
            try
            {
                if (nok == null) throw new ArgumentNullException("nok");

                ElementBL _elementBL = new ElementBL();
                homonymPersons = null;

                switch (nok.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return nok;
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(nok, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return nok;
                        return this.Insert(nok);
                    case StatusEntityValue.NewAndDeleted:
                        return nok;
                    case StatusEntityValue.None:
                        CheckUpdatePreconditions(nok, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return nok;
                        if ((nok.Person != null) && (nok.Person.EditStatus.Value == StatusEntityValue.Updated))
                        {
                            nok.Person = base.Update(nok.Person);
                        }
                        return nok;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(nok, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return nok;
                        return this.Update(nok);
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

        public NOKListDTO[] GetNOKs(int customerID)
        {
            try
            {
                NOKListDTOAdapter nokListDTOAdapter = new NOKListDTOAdapter();

                DataSet ds = _nokDA.GetNOKs(customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.NOKListDTOTable)))
                {
                    NOKListDTO[] noks = nokListDTOAdapter.GetData(ds);
                    return noks;
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
        public NOKEntity GetNOK(int nokID)
        {
            try
            {
                NOKAdapter nokAdapter = new NOKAdapter();
                PersonBL personBL = new PersonBL();

                DataSet ds = _nokDA.GetNOK(nokID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.NOKTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.NOKTable].Rows.Count > 0))
                {
                    int personID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.NOKTable].Rows[0]["PersonID"].ToString(), 0);
                    int kinshipID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.NOKTable].Rows[0]["KinshipID"].ToString(), 0);

                    DataSet ds2;

                    #region Kinships
                    ds2 = _kinshipDA.GetKinshipByID(kinshipID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.KinshipTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.KinshipTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Person
                    if (personID <= 0)
                    {
                        throw new Exception(Properties.Resources.ERROR_NOKPersonNotFound);
                    }
                    SII.HCD.BackOffice.Entities.PersonEntity myPerson = personBL.GetPerson(personID);
                    #endregion

                    NOKEntity result = nokAdapter.GetInfo(ds.Tables[SII.HCD.Administrative.Entities.TableNames.NOKTable].Rows[0], ds);
                    result.Person = myPerson;
                    LOPDLogger.Write(EntityNames.NOKEntityName, nokID, ActionType.View);
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
        public NOKEntity GetNOK(int nokID)
        {
            try
            {
                #region Person
                PersonBL personBL = new PersonBL();
                int personID = 0;
                personID = personBL.obtenerPersonID_From_NOK(nokID);

                if (personID == 0) throw new Exception(Properties.Resources.ERROR_NOKPersonNotFound);
                PersonEntity myPerson = null;
                var HiloPerson = System.Threading.Tasks.Task.Factory.StartNew(() => myPerson = personBL.GetPerson(personID));
                #endregion

                DataSet ds = _nokDA.GetNOK(nokID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.NOKTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.NOKTable].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());

                    NOKAdapter nokAdapter = new NOKAdapter();
                    NOKEntity result = nokAdapter.GetInfo(ds.Tables[SII.HCD.Administrative.Entities.TableNames.NOKTable].Rows[0], ds2);
                    HiloPerson.Wait();
                    result.Person = myPerson;
                    LOPDLogger.Write(EntityNames.NOKEntityName, nokID, ActionType.View);
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
