using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using SII.Framework.Common;
using SII.Framework.Entities.Services;
using SII.Framework.ExceptionHandling;
using SII.Framework.Interfaces;
using SII.Framework.LLDA;
using SII.Framework.Logging.LOPD;
using SII.HCD.Addin.Host.View;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.BackOffice.Services;
using SII.HCD.Common.BL;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Common.Services;
using SII.HCD.Configuration;
using SII.HCD.Misc;
using SII.HCD.Misc.Validators;
using SII.SIFP.Configuration;
using CommonEntities = SII.HCD.Common.Entities;
using System.Threading.Tasks;

namespace SII.HCD.BackOffice.BL
{
    public class PersonBL : BusinessLayerBase<PersonEntity>, IPersonService
    {
        #region Consts
        //private const string PersonEntityName = "PersonEntity";
        //private const string AddressEntityName = "AddressEntity";
        //private const string TelephoneEntityName = "TelephoneEntity";
        //private const string IdentifierEntityName = "IdentifierEntity";
        //private const string SensitiveDataEntityName = "SensitiveDataEntity";
        #endregion

        #region DA definition
        private PersonDA _personDA;

        private PersonIdentifierRelDA _personIdentifierRelDA;
        private IdentifierTypeDA _identifierTypeDA;

        private PersonTelephoneRelDA _personTelephoneRelDA;
        private TelephoneDA _telephoneDA;

        private PersonCatRelDA _personCatRelDA;
        private CategoryDA _categoryDA;

        private SensitiveDataDA _sensitiveDataDA;
        private AddressDA _addressDA;

        private DuplicateGroupDA _duplicateGroupDA;

        private DBImageStorageDA _dbImageStorageDA;
        #endregion

        #region Constructors
        public PersonBL()
        {
            _personDA = new PersonDA();

            _personIdentifierRelDA = new PersonIdentifierRelDA();
            _identifierTypeDA = new IdentifierTypeDA();

            _personTelephoneRelDA = new PersonTelephoneRelDA();
            _telephoneDA = new TelephoneDA();

            _personCatRelDA = new PersonCatRelDA();
            _categoryDA = new CategoryDA();

            _sensitiveDataDA = new SensitiveDataDA();
            _addressDA = new AddressDA();

            _duplicateGroupDA = new DuplicateGroupDA();

            _dbImageStorageDA = new DBImageStorageDA();
        }

        /*
        private void LoadPhysicianConfiguration(CategoryPersonKeyEnum categoryPerson)
        {
            CustomerFindRequest customerFind = new CustomerFindRequest();
            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;

            if (backOfficeConfig.EntitySettings.PhysicianEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.PhysicianEntity.Attributes)
                {
                    if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryFirstName = true;
                    }

                    if ((attrib.Name == "LastName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryLastName = true;
                    }

                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        customerFind.MandatoryIdentifierType = true;
                    }
                }
            }

            categoryConfigurations.Add(categoryPerson, customerFind);
        }

        private void LoadOrganizationContactPersonConfiguration(CategoryPersonKeyEnum categoryPerson)
        {
            CustomerFindRequest customerFind = new CustomerFindRequest();
            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;

            if (backOfficeConfig.EntitySettings.PersonEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.PersonEntity.Attributes)
                {
                    if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryFirstName = true;
                    }
                    if ((attrib.Name == "LastName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryLastName = true;
                    }
                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        customerFind.MandatoryIdentifierType = true;
                    }
                }
            }
            categoryConfigurations.Add(categoryPerson, customerFind);
        }

        private void LoadHHRRConfiguration(CategoryPersonKeyEnum categoryPerson)
        {
            CustomerFindRequest customerFind = new CustomerFindRequest();
            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;

            if (backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes)
                {
                    if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryFirstName = true;
                    }

                    if ((attrib.Name == "LastName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryLastName = true;
                    }

                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        customerFind.MandatoryIdentifierType = true;
                    }
                }
            }
            categoryConfigurations.Add(categoryPerson, customerFind);
        }

        private void LoadCustomerContactPerson(CategoryPersonKeyEnum categoryPerson)
        {
            CustomerFindRequest customerFind = new CustomerFindRequest();
            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;

            if (backOfficeConfig.EntitySettings.PersonEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.PersonEntity.Attributes)
                {
                    if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryFirstName = true;
                    }

                    if ((attrib.Name == "LastName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryLastName = true;
                    }
                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        customerFind.MandatoryIdentifierType = true;
                    }
                }
            }
            categoryConfigurations.Add(categoryPerson, customerFind);
        }

        private void LoadNOKConfiguration(CategoryPersonKeyEnum categoryPerson)
        {
            CustomerFindRequest customerFind = new CustomerFindRequest();
            BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;

            if (backOfficeConfig.EntitySettings.PersonEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.PersonEntity.Attributes)
                {
                    if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryFirstName = true;
                    }

                    if ((attrib.Name == "LastName") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryLastName = true;
                    }
                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))
                    {
                        customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        customerFind.MandatoryIdentifierType = true;
                    }
                }
            }
            categoryConfigurations.Add(categoryPerson, customerFind);
        }

        private void LoadCustomerConfiguration(CategoryPersonKeyEnum categoryPerson)
        {
            CustomerFindRequest customerFind = new CustomerFindRequest();
            AdministrativeConfigurationSection administrativeConfig =
                FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;

            if (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.CustomerEntity.Attributes)
                {
                    if ((attrib.Name == "FirstName") && (attrib.Mandatory))
                    {
                        //customerFind.FirstName = customer.Person.FirstName;
                        customerFind.MandatoryFirstName = true;
                    }

                    if ((attrib.Name == "LastName") && (attrib.Mandatory))
                    {
                        //customerFind.LastName = customer.Person.LastName;
                        customerFind.MandatoryLastName = true;
                    }

                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory))//&& !(customer.AllowNoDefaultIdentifier))
                    {
                        customerFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        customerFind.MandatoryIdentifierType = true;
                        //customerFind.IdentifierIDNumber = GetIDNumber(customer.Person.Identifiers, attrib.DefaultValue);
                    }
                }
            }

            categoryConfigurations.Add(categoryPerson, customerFind);
        }
        */
        #endregion

        #region Fields
        private IdentifierTypeBL _identifierTypeBL = null;
        private ElementBL _elementBL = null;
        private ProcessChartEntity[] _processCharts = null;
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

        private ElementBL ElementBL
        {
            get
            {
                if (_elementBL == null)
                {
                    _elementBL = new ElementBL();
                }
                return _elementBL;
            }
        }

        private ProcessChartEntity[] ProcessCharts
        {
            get
            {
                if (_processCharts == null)
                    _processCharts = this.GetProcessChartsFromCache();

                return _processCharts;
            }
        }
        #endregion


        #region Private methods
        #region ValidateEntities
        private void ValidateAddress(AddressEntity address, ElementBL elementBL)
        {
            if (address == null) throw new ArgumentNullException("address");

            CommonEntities.ElementEntity _addressMetadata = this.GetElementByName(EntityNames.AddressEntityName, elementBL);
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

        private void ValidateTelephone(TelephoneEntity telephone, ElementBL elementBL)
        {
            if (telephone == null) throw new ArgumentNullException("telephone");

            CommonEntities.ElementEntity _telephoneMetada = this.GetElementByName(EntityNames.TelephoneEntityName, elementBL);
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

        private void ValidateIdentifier(IdentifierEntity identifier, bool ignoreIdentifier, ElementBL elementBL)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");

            CommonEntities.ElementEntity _identifierMetada = this.GetElementByName(EntityNames.IdentifierEntityName, elementBL);
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
                if ((identifier.IdentifierType.RequiredValidation) && !ignoreIdentifier)
                {
                    Type validatorType = Type.GetType(identifier.IdentifierType.ValidationClass);
                    ICustomValidator<string, string> validator = Activator.CreateInstance(validatorType) as ICustomValidator<string, string>;
                    if (!validator.Validate(identifier.IDNumber))
                    {
                        throw new Exception(String.Format(Properties.Resources.MSG_CannotValidateIdentifier, identifier.IdentifierType.Name));
                    }
                }
            }
        }

        private void ValidateSensitiveData(SensitiveDataEntity sensitiveData, ElementBL elementBL)
        {
            if (sensitiveData == null) throw new ArgumentNullException("sensitiveData");

            CommonEntities.ElementEntity _sensitiveDataMetadata = this.GetElementByName(EntityNames.SensitiveDataEntityName, elementBL);
            SensitiveDataHelper sensitiveDataHelper = new SensitiveDataHelper(_sensitiveDataMetadata);

            ValidationResults result = sensitiveDataHelper.Validate(sensitiveData);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_sensitiveDataValidationError, sb));
            }
        }
        #endregion

        private ProcessChartEntity[] GetProcessChartsFromCache()
        {
            ProcessChartBL processChartBL = new ProcessChartBL();
            return processChartBL.GetAllProcessCharts();
        }

        private PersonEntity Insert(PersonEntity person)
        {
            if (person == null) throw new ArgumentNullException("person");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                person = this.InnerInsert(person, userName);

                scope.Complete();
            }

            this.ResetPerson(person);

            LOPDLogger.Write(EntityNames.PersonEntityName, person.ID, ActionType.Create);
            return person;
        }

        private void UpdatePhoneticInfo(int personID)
        {
            CommonEntities.AddInTokenBaseEntity[] addins = GetAvailablePhoneticAddins();
            if ((addins != null) && (addins.Length > 0))
            {
                string[] addinNames = (from addin in addins
                                       select addin.AddinName).ToArray();
                UpdatePhoneticInfo(personID, addinNames);
            }
        }

        private void UpdatePhoneticInfo(PersonBaseEntity[] persons)
        {
            CommonEntities.AddInTokenBaseEntity[] addins = GetAvailablePhoneticAddins();
            if ((addins != null) && (addins.Length > 0))
            {
                string[] addinNames = (from addin in addins
                                       select addin.AddinName).ToArray();
                UpdatePhoneticInfo(persons, addinNames);
            }
        }

        private void UpdatePhoneticInfo(int personID, string[] addinNames)
        {
            if ((addinNames == null) || (addinNames.Length == 0))
                return;

            PersonPhoneticInfoDA personPhoneticInfoDA = new PersonPhoneticInfoDA();

            foreach (string addinName in addinNames)
            {
                int personPhoneticInfoID = personPhoneticInfoDA.GetPersonPhoneticInfoIDByPersonIDAndAddinName(personID, addinName);
                PersonEntity myPerson = this.GetPerson(personID);

                //Llamar al addin para calcular estos campos...
                PhoneticTranslatorHostView host = AddInRepository.GetAddIn<PhoneticTranslatorHostView>(addinName);
                if (host != null)
                {
                    string firstName = host.Translate(myPerson.FirstName);
                    string lastName = host.Translate(myPerson.LastName);
                    string lastName2 = host.Translate(myPerson.LastName2);
                    string fullName = host.Translate(myPerson.FullName);

                    if (personPhoneticInfoID > 0)
                        personPhoneticInfoDA.Update(personPhoneticInfoID, personID, addinName, firstName, lastName, lastName2, fullName);
                    else
                        personPhoneticInfoID = personPhoneticInfoDA.Insert(personID, addinName, firstName, lastName, lastName2, fullName);
                }
            }
        }

        private void UpdatePhoneticInfo(PersonBaseEntity[] persons, string[] addinNames)
        {
            if ((addinNames == null) || (addinNames.Length == 0))
                return;
            if (persons == null || persons.Length <= 0)
                return;

            PersonPhoneticInfoDA personPhoneticInfoDA = new PersonPhoneticInfoDA();

            foreach (string addinName in addinNames)
            {
                //Llamar al addin para calcular estos campos...
                PhoneticTranslatorHostView host = AddInRepository.GetAddIn<PhoneticTranslatorHostView>(addinName);
                if (host != null)
                {
                    foreach (PersonBaseEntity myPerson in persons)
                    {
                        int personPhoneticInfoID = personPhoneticInfoDA.GetPersonPhoneticInfoIDByPersonIDAndAddinName(myPerson.ID, addinName);
                        string firstName = host.Translate(myPerson.FirstName);
                        string lastName = host.Translate(myPerson.LastName);
                        string lastName2 = host.Translate(myPerson.LastName2);
                        string fullName = host.Translate(myPerson.FullName);

                        if (personPhoneticInfoID > 0)
                            personPhoneticInfoDA.Update(personPhoneticInfoID, myPerson.ID, addinName, firstName, lastName, lastName2, fullName);
                        else
                            personPhoneticInfoID = personPhoneticInfoDA.Insert(myPerson.ID, addinName, firstName, lastName, lastName2, fullName);
                    }
                }
            }
        }

        private PersonLookupDTO[] MergePhoneticPersons(PersonLookupDTO[] sourcePersons, PersonLookupDTO[] targetPersons, int maxRows,
            CommonEntities.AddInTokenBaseEntity phoneticAddinName, PersonSpecification specification)
        {
            if ((sourcePersons == null) || (sourcePersons.Length == 0))
                return targetPersons;

            List<PersonLookupDTO> myTargetPersons = new List<PersonLookupDTO>();
            if (targetPersons != null)
                myTargetPersons.AddRange(targetPersons);

            foreach (PersonLookupDTO person in sourcePersons)
            {
                if (!myTargetPersons.Exists(p => p.ID == person.ID))
                {
                    if (specification.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByNameParts))
                        person.MatchFlags = PersonMatchFlagEnum.PartialNamePhoneticMatch;
                    else
                        person.MatchFlags = PersonMatchFlagEnum.FullNamePhoneticMatch;
                    person.ExternalSourceName = phoneticAddinName.AddinName;
                    person.ExternalSourceDescription = phoneticAddinName.AddinDescription;
                    myTargetPersons.Add(person);
                }
                if (myTargetPersons.Count >= maxRows)
                    break;
            }

            return myTargetPersons.Count > 0 ? myTargetPersons.ToArray() : null;
        }

        private PersonLookupDTO[] AddCustomerProcessInfo(PersonLookupDTO[] persons, PersonSpecification specification, int maxRows, string phoneticAddinName = "")
        {
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.RetrieveExclusiveInformation))
            {
                int[] exclusiveProcessChartIDs = GetExclusiveProcessChartsFromProcess(specification.ExclusiveProcessChartID);

                DataSet ds = _personDA.GetPersonCustomerProcessData(specification, maxRows, CommonEntities.StatusEnum.Active, exclusiveProcessChartIDs, phoneticAddinName);
                PersonCustomerProcessInfoDTOAdvancedAdapter infoAdapter = new PersonCustomerProcessInfoDTOAdvancedAdapter();
                PersonCustomerProcessInfoDTO[] customerProcessPersonInfo = infoAdapter.GetData(ds);
                if (customerProcessPersonInfo != null)
                {
                    //Completar datos exclusivos
                    foreach (PersonLookupDTO item in persons)
                    {
                        PersonCustomerProcessInfoDTO personProcess = Array.Find(customerProcessPersonInfo, cp => cp.PersonID == item.ID);
                        if (personProcess != null)
                        {
                            item.ExclusiveProcessChartID = personProcess.ProcessChartID;
                            if (ProcessCharts != null)
                                item.ExclusiveProcessChartName = (from p in ProcessCharts
                                                                  where p.ID == personProcess.ProcessChartID
                                                                  select p.Name).FirstOrDefault();
                        }
                    }
                }
            }
            return persons;
        }

        private PersonLookupDTO[] AddCustomerCategoriesInfo(PersonLookupDTO[] persons, PersonSpecification specification, int maxRows, string phoneticAddinName = "")
        {
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.RetrieveCategoriesInformation))
            {
                if ((persons != null) && (persons.Length > 0))
                {
                    DataSet dsPersonCatRels = _personDA.GetPersonCustomerCategoriesData(specification, maxRows, phoneticAddinName);
                    if ((dsPersonCatRels != null) && (dsPersonCatRels.Tables != null)
                        && dsPersonCatRels.Tables.Contains(BackOffice.Entities.TableNames.CategoryTable)
                        && (dsPersonCatRels.Tables[BackOffice.Entities.TableNames.CategoryTable].Rows.Count > 0))
                    {
                        DataTable myTable = dsPersonCatRels.Tables[BackOffice.Entities.TableNames.CategoryTable];
                        foreach (PersonLookupDTO person in persons)
                        {
                            DataRow[] rows = myTable.AsEnumerable()
                                .Where(row => (row["PersonID"] as int? ?? 0) == person.ID)
                                .OrderBy(row => row["Name"] as string ?? string.Empty)
                                .ToArray();

                            String categories = String.Empty;
                            if (rows != null)
                            {
                                foreach (DataRow row in rows)
                                {
                                    int personID = row["PersonID"] as int? ?? 0;
                                    if (person.ID == personID)
                                    {
                                        if (String.IsNullOrWhiteSpace(categories))
                                            categories = row["Name"] as string ?? string.Empty;
                                        else
                                            categories = String.Concat(categories, ", ", row["Name"] as string ?? string.Empty);
                                    }
                                }
                            }
                            person.Categories = categories;
                        }
                    }
                }
            }
            return persons;
        }

        private int[] GetExclusiveProcessChartsFromProcess(int processChartID)
        {
            if (ProcessCharts != null)
            {
                ProcessChartEntity myPC = Array.Find(ProcessCharts, pc => pc.ID == processChartID);
                if (myPC != null)
                {
                    List<int> exclusiveProcessChartIDs = new List<int>();
                    if (myPC.AdmitMultipleInstance)
                    {
                        exclusiveProcessChartIDs.Add(myPC.ID);
                    }
                    if (myPC.ExclusiveConditions != null)
                    {
                        foreach (ProcessChartExclusiveConditionRelEntity item in myPC.ExclusiveConditions)
                        {
                            exclusiveProcessChartIDs.Add(item.ExclusiveProcessChartID);
                        }
                    }
                    return exclusiveProcessChartIDs.Count > 0 ? exclusiveProcessChartIDs.ToArray() : null;
                }
            }
            return null;
        }

        private bool CheckCHNumberSpecification(PersonSpecification mySpecification, int chNumberCareCenterID)
        {
            bool findByCareCenter = false;
            if (chNumberCareCenterID > 0)
            {
                CommonEntities.ElementEntity _customerMetadata = GetElementByName(EntityNames.CustomerEntityName, ElementBL);
                if ((_customerMetadata != null) && (_customerMetadata.Attributes != null))
                {
                    CommonEntities.AttributeEntity chNumberAttribute = _customerMetadata.GetAttribute("CHNumber");
                    CareCenterRelatedCodeGeneratorDA _careCenterRelatedCodeGeneratorDA = new CareCenterRelatedCodeGeneratorDA();
                    findByCareCenter = _careCenterRelatedCodeGeneratorDA.GetCareCenterRelatedCodeGenerator(chNumberCareCenterID, _customerMetadata.ID, chNumberAttribute.ID) > 0;
                }
            }

            if (mySpecification.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter))
            {
                bool byCareCenter = false;
                if ((mySpecification.CHNumberCareCenterID > 0) && (mySpecification.CHNumberCareCenterID != chNumberCareCenterID))
                {
                    CommonEntities.ElementEntity _customerMetadata = GetElementByName(EntityNames.CustomerEntityName, ElementBL);
                    if ((_customerMetadata != null) && (_customerMetadata.Attributes != null))
                    {
                        CommonEntities.AttributeEntity chNumberAttribute = _customerMetadata.GetAttribute("CHNumber");
                        CareCenterRelatedCodeGeneratorDA _careCenterRelatedCodeGeneratorDA = new CareCenterRelatedCodeGeneratorDA();
                        byCareCenter = _careCenterRelatedCodeGeneratorDA.GetCareCenterRelatedCodeGenerator(mySpecification.CHNumberCareCenterID, _customerMetadata.ID, chNumberAttribute.ID) > 0;
                    }
                }
                else
                    byCareCenter = findByCareCenter;

                if (!byCareCenter)
                {
                    mySpecification.Options ^= PersonSearchOptionEnum.CHNumberCareCenter;
                    mySpecification.Options |= PersonSearchOptionEnum.CHNumber;
                }
            }

            return findByCareCenter;
        }

        private PersonLookupDTO[] AddCustomerLookupAddins(PersonLookupDTO[] actualPersons, PersonSpecification specification, int maxRows, out CommonEntities.GenericErrorLogEntity[] errors)
        {
            errors = null;

            if (!specification.IsFilteredByAny(PersonSearchOptionEnum.AddinLookup))
                return actualPersons;

            CommonEntities.AddInTokenBaseEntity[] customerLookupAddins = GetAvailableCustomerLookupAddins(specification.Options);
            if ((customerLookupAddins == null) || (customerLookupAddins.Length == 0))
                return actualPersons;

            List<PersonLookupDTO> persons = new List<PersonLookupDTO>();
            if (actualPersons != null)
                persons.AddRange(actualPersons);

            foreach (CommonEntities.AddInTokenBaseEntity customerLookupAddin in customerLookupAddins)
            {
                if ((specification.AddinLookup == AddinLookupModeEnum.All) ||
                    ((specification.AddinLookup == AddinLookupModeEnum.Single) && (specification.AddinName == customerLookupAddin.AddinName)))
                {
                    CustomerLookupHostView host = AddInRepository.GetAddIn<CustomerLookupHostView>(customerLookupAddin.AddinName);
                    if (host != null)
                    {
                        try
                        {
                            Addin.Entities.CustomerLookupParameters param = new Addin.Entities.CustomerLookupParameters();
                            param = SetCustomerLookupParameters(specification);
                            bool maxRecordsExceeded = false;
                            Addin.Entities.GenericErrorLog[] addinErrors = null;
                            Addin.Entities.Customer[] customers = host.Lookup(param, out maxRecordsExceeded, out addinErrors);
                            errors = AddAddinErrorsToErrors(addinErrors);
                            AddCustomersToPersons(customers, persons, maxRows, customerLookupAddin.AddinName, customerLookupAddin.AddinDescription);
                        }
                        catch (Exception ex)
                        {
                            ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service);
                        }
                        if (persons.Count >= maxRows)
                            break;
                    }
                }
            }

            return persons.Count > 0 ? persons.ToArray() : null;
        }

        private static CommonEntities.GenericErrorLogEntity[] AddAddinErrorsToErrors(Addin.Entities.GenericErrorLog[] addinErrors)
        {
            if ((addinErrors == null) || (addinErrors.Length == 0))
                return null;

            List<CommonEntities.GenericErrorLogEntity> myErrors = new List<CommonEntities.GenericErrorLogEntity>();
            foreach (Addin.Entities.GenericErrorLog item in addinErrors)
            {
                CommonEntities.ErrorLevelEnum myErrorLevel = CommonEntities.ErrorLevelEnum.Unknown;
                switch (item.ErrorLevel)
                {
                    case SII.HCD.Addin.Entities.ErrorLevelEnum.Unknown: myErrorLevel = CommonEntities.ErrorLevelEnum.Unknown; break;
                    case SII.HCD.Addin.Entities.ErrorLevelEnum.Information: myErrorLevel = CommonEntities.ErrorLevelEnum.Information; break;
                    case SII.HCD.Addin.Entities.ErrorLevelEnum.Warning: myErrorLevel = CommonEntities.ErrorLevelEnum.Warning; break;
                    case SII.HCD.Addin.Entities.ErrorLevelEnum.Error: myErrorLevel = CommonEntities.ErrorLevelEnum.Error; break;
                    case SII.HCD.Addin.Entities.ErrorLevelEnum.CriticalError: myErrorLevel = CommonEntities.ErrorLevelEnum.CriticalError; break;
                    default: myErrorLevel = CommonEntities.ErrorLevelEnum.Unknown; break;
                }
                if (myErrorLevel == CommonEntities.ErrorLevelEnum.CriticalError)
                {
                    myErrors.Add(new CommonEntities.GenericErrorLogEntity(myErrorLevel,
                        string.Format(Properties.Resources.MSG_Error_AddinFault, item.ErrorMessageSourceDescription, item.ErrorMessage),
                        item.ErrorMessageSourceName, item.ErrorMessageSourceDescription));
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format(Properties.Resources.MSG_Error_AddinFault, item.ErrorMessageSourceDescription, item.ErrorMessage),
                        SII.Framework.Logging.Category.CriticalError, SII.Framework.Logging.Priority.High, 0, System.Diagnostics.TraceEventType.Critical, item.ErrorMessageSourceDescription);
                }
                else
                    myErrors.Add(new CommonEntities.GenericErrorLogEntity(myErrorLevel, item.ErrorMessage, item.ErrorMessageSourceName, item.ErrorMessageSourceDescription));
            }
            CommonEntities.GenericErrorLogEntity[] errors = myErrors.Count > 0 ? myErrors.ToArray() : null;
            return errors;
        }

        private void AddCustomersToPersons(Addin.Entities.Customer[] customers, List<PersonLookupDTO> persons, int maxRows, string customerLookupAddin, string addinDescription)
        {
            if ((customers == null) || (customers.Length == 0) || (persons.Count >= maxRows))
                return;

            foreach (Addin.Entities.Customer customer in customers)
            {
                List<IdentifierEntity> identifiers = new List<IdentifierEntity>();
                if (customer.Identifiers != null)
                {
                    foreach (Addin.Entities.Identifier identifier in customer.Identifiers)
                    {
                        IdentifierEntity myIdentifier = new IdentifierEntity(
                            0,
                            identifier.IDNumber ?? string.Empty,
                            new IdentifierTypeEntity(0,
                                                     identifier.IdentifierTypeName ?? string.Empty,
                                                     identifier.IdentifierTypeName ?? string.Empty,
                                                     identifier.RequiredValidation,
                                                     string.Empty,
                                                     identifier.ValidationMask ?? string.Empty,
                                                     CommonEntities.StatusEnum.Active,
                                                     CategoryTypeEnum.Person,
                                                     0),
                            0);
                        identifiers.Add(myIdentifier);
                    }
                }
                List<TelephoneEntity> telephones = new List<TelephoneEntity>();
                if (customer.Telephones != null)
                {
                    foreach (Addin.Entities.Telephone telephone in customer.Telephones)
                    {
                        TelephoneEntity myTelephone = new TelephoneEntity(0,
                                                                          telephone.Phone ?? string.Empty,
                                                                          telephone.Comments ?? string.Empty,
                                                                          telephone.TelephoneType ?? string.Empty,
                                                                          telephone.Emergency,
                                                                          0);
                        telephones.Add(myTelephone);
                    }
                }

                SexEnum sex = SexEnum.Unknown;
                switch (customer.Sex)
                {
                    case SII.HCD.Addin.Entities.SexEnum.Unknown:
                        sex = SexEnum.Unknown;
                        break;
                    case SII.HCD.Addin.Entities.SexEnum.Male:
                        sex = SexEnum.Male;
                        break;
                    case SII.HCD.Addin.Entities.SexEnum.Female:
                        sex = SexEnum.Female;
                        break;
                    case SII.HCD.Addin.Entities.SexEnum.Transexual:
                        sex = SexEnum.Transexual;
                        break;
                    case SII.HCD.Addin.Entities.SexEnum.Other:
                        sex = SexEnum.Other;
                        break;
                    default:
                        sex = SexEnum.Unknown;
                        break;
                }

                PersonLookupDTO person = new PersonLookupDTO(
                    0,
                    customer.FirstName ?? string.Empty,
                    customer.LastName ?? string.Empty,
                    customer.LastName2 ?? string.Empty,
                    CommonEntities.StatusEnum.None,
                    0,
                    customer.EmailAddress ?? string.Empty,
                    ((customer.Identifiers != null) && (customer.Identifiers.Length > 0)) ? customer.Identifiers[0].IDNumber ?? string.Empty : string.Empty,
                    ((customer.Telephones != null) && (customer.Telephones.Length > 0)) ? customer.Telephones[0].Phone ?? string.Empty : string.Empty,
                    DateTime.Now,
                    customer.CustomerAddress != null ? customer.CustomerAddress.StAddress ?? string.Empty : string.Empty,
                    customer.CustomerAddress != null ? customer.CustomerAddress.ZipCode ?? string.Empty : string.Empty,
                    customer.CustomerAddress != null ? customer.CustomerAddress.City ?? string.Empty : string.Empty,
                    customer.CustomerAddress != null ? customer.CustomerAddress.State ?? string.Empty : string.Empty,
                    customer.CustomerAddress != null ? customer.CustomerAddress.Province ?? string.Empty : string.Empty,
                    customer.CustomerAddress != null ? customer.CustomerAddress.Country ?? string.Empty : string.Empty,
                    customer.CHNumber ?? string.Empty,
                    string.Empty,
                    customer.DeathDateTime,
                    PersonMatchFlagEnum.AddinMatch,
                    customerLookupAddin,
                    addinDescription,
                    customer.IdentificationNumber ?? string.Empty,
                    customer.ShortIDNumber ?? string.Empty,
                    customer.PolicyNumber ?? string.Empty,
                    customer.BirthDate,
                    sex,
                    identifiers.Count > 0 ? identifiers.ToArray() : null,
                    telephones.Count > 0 ? telephones.ToArray() : null,
                    0,
                    0,
                    false
                    );

                persons.Add(person);

                if (persons.Count >= maxRows)
                    break;
            }
        }

        private Addin.Entities.CustomerLookupParameters SetCustomerLookupParameters(PersonSpecification specification)
        {
            Addin.Entities.CustomerLookupParameters param = new Addin.Entities.CustomerLookupParameters();

            if (specification.IsFilteredByAny(PersonSearchOptionEnum.ProcessChartAndCareCenter))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.ProcessChartAndCareCenter;
                param.ProcessChartID = specification.ProcessChartID;
                param.CareCenterID = specification.CareCenterID;
            }
            //if (specification.IsFilteredByAny(PersonSearchOptionEnum.CareCenter))
            //{
            //    param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.CareCenter;
            //    param.CareCenterID = specification.CareCenterID;
            //}
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.FirstName))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.FirstName;
                param.FirstName = specification.FirstName;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.LastName))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.LastName;
                param.LastName = specification.LastName;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.LastName2))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.LastName2;
                param.LastName2 = specification.LastName2;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.IdentifierType))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.IdentifierType;
                param.IdentifierTypeID = specification.IdentifierTypeID;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.IdentifierNumber))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.IdentifierNumber;
                param.IdentifierNumber = specification.IdentifierNumber;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.Category))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.Category;
                param.CategoryID = specification.CategoryID;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.Profile))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.Profile;
                param.ProfileID = specification.ProfileID;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.CHNumber))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.CHNumber;
                param.CHNumber = specification.CHNumber;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.CardBand))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.CardBand;
                param.CardBand = specification.CardBand;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.CardNumber))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.CardNumber;
                param.CardPAN = specification.CardNumber;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.PoorlyIdentified))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.PoorlyIdentified;
                param.PoorlyIdentified = specification.PoorlyIdentified;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.Insurer))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.Insurer;
                param.InsurerID = specification.InsurerID;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.EpisodeNumber))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.EpisodeNumber;
                param.EpisodeNumber = specification.EpisodeNumber;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.ExternalIdentifierNumber))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.ExternalIdentifierNumber;
                param.ExternalIdentifierNumber = specification.ExternalIdentifierNumber;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.ExternalEpisodeNumber))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.ExternalEpisodeNumber;
                param.ExternalEpisodeNumber = specification.ExternalEpisodeNumber;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByNameParts))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.PhoneticLookupByNameParts;
                param.PhoneticLookupByNameParts = specification.PhoneticLookupByNameParts;
            }
            if (specification.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByFullName))
            {
                param.Options = param.Options | Addin.Entities.PersonSearchOptionEnum.PhoneticLookupByFullName;
                param.PhoneticLookupByFullName = specification.PhoneticLookupByFullName;
                param.PhoneticLookupFullName = specification.PhoneticLookupFullName;
            }
            return param;
        }

        private PersonLookupDTO[] FindCustomerProcessInfoByCHNumber(int maxRows, PersonLookupDTO[] allPersons, PersonLookupDTOAdvancedAdapter personLookupDTOAdvancedAdapter, PersonSpecification specification, int chNumberCareCenterID)
        {
            if (IsCHNumberDuplicated(allPersons))
            {
                int myPersonID = allPersons[0].RecordMergedID;
                PersonSpecification spec = new PersonSpecification().ByPersonID(myPersonID);
                if (specification.IsFilteredByAny(PersonSearchOptionEnum.RetrieveExclusiveInformation))
                    spec.RetrieveExclusiveInformation(specification.ExclusiveProcessChartID, specification.ExclusiveCareCenterID);

                DataSet ds = _personDA.GetPersons(spec, chNumberCareCenterID, maxRows);
                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonLookupDTOTable)))
                {
                    allPersons = personLookupDTOAdvancedAdapter.GetData(ds);
                    allPersons = AddCustomerProcessInfo(allPersons, spec, maxRows);
                }
            }
            else
                allPersons = AddCustomerProcessInfo(allPersons, specification, maxRows);

            return allPersons;
        }

        private static bool IsCHNumberDuplicated(PersonLookupDTO[] allPersons)
        {
            return (allPersons != null) && (allPersons.Length == 1) &&
                   (allPersons[0].Status == CommonEntities.StatusEnum.Superceded) &&
                   (allPersons[0].RecordMergedID > 0);
        }

        private void SavePersonWithTrim(PersonEntity person, GlobalConfigurationSection globalConfig)
        {
            if ((globalConfig.Parameters["SavePersonWithTrim"] != null) && (globalConfig.Parameters["SavePersonWithTrim"].Active))
            {
                person.FirstName = person.FirstName != null ? person.FirstName.Trim() : person.FirstName;
                person.LastName = person.LastName != null ? person.LastName.Trim() : person.LastName;
                person.LastName2 = person.LastName2 != null ? person.LastName2.Trim() : person.LastName2;
                if (person.Address != null)
                {
                    person.Address.Address1 = person.Address.Address1 != null ? person.Address.Address1.Trim() : person.Address.Address1;
                    person.Address.Address2 = person.Address.Address2 != null ? person.Address.Address2.Trim() : person.Address.Address2;
                    person.Address.AddressType = person.Address.AddressType != null ? person.Address.AddressType.Trim() : person.Address.AddressType;
                    person.Address.City = person.Address.City != null ? person.Address.City.Trim() : person.Address.City;
                    person.Address.Province = person.Address.Province != null ? person.Address.Province.Trim() : person.Address.Province;
                    person.Address.State = person.Address.State != null ? person.Address.State.Trim() : person.Address.State;
                    person.Address.Country = person.Address.Country != null ? person.Address.Country.Trim() : person.Address.Country;
                    person.Address.ZipCode = person.Address.ZipCode != null ? person.Address.ZipCode.Trim() : person.Address.ZipCode;
                }
                if (person.Address2 != null)
                {
                    person.Address2.Address1 = person.Address2.Address1 != null ? person.Address2.Address1.Trim() : person.Address2.Address1;
                    person.Address2.Address2 = person.Address2.Address2 != null ? person.Address2.Address2.Trim() : person.Address2.Address2;
                    person.Address2.AddressType = person.Address2.AddressType != null ? person.Address2.AddressType.Trim() : person.Address2.AddressType;
                    person.Address2.City = person.Address2.City != null ? person.Address2.City.Trim() : person.Address2.City;
                    person.Address2.Province = person.Address2.Province != null ? person.Address2.Province.Trim() : person.Address2.Province;
                    person.Address2.State = person.Address2.State != null ? person.Address2.State.Trim() : person.Address2.State;
                    person.Address2.Country = person.Address2.Country != null ? person.Address2.Country.Trim() : person.Address2.Country;
                    person.Address2.ZipCode = person.Address2.ZipCode != null ? person.Address2.ZipCode.Trim() : person.Address2.ZipCode;
                }
            }
        }

        private void SavePersonWithUpperCase(PersonEntity person, GlobalConfigurationSection globalConfig)
        {
            if ((globalConfig.Parameters["SavePersonWithUpperCase"] != null) && (globalConfig.Parameters["SavePersonWithUpperCase"].Active))
            {
                person.FirstName = person.FirstName != null ? person.FirstName.ToUpper() : person.FirstName;
                person.LastName = person.LastName != null ? person.LastName.ToUpper() : person.LastName;
                person.LastName2 = person.LastName2 != null ? person.LastName2.ToUpper() : person.LastName2;
                if (person.Address != null)
                {
                    person.Address.Address1 = person.Address.Address1 != null ? person.Address.Address1.ToUpper() : person.Address.Address1;
                    person.Address.Address2 = person.Address.Address2 != null ? person.Address.Address2.ToUpper() : person.Address.Address2;
                    person.Address.AddressType = person.Address.AddressType != null ? person.Address.AddressType.ToUpper() : person.Address.AddressType;
                    person.Address.City = person.Address.City != null ? person.Address.City.ToUpper() : person.Address.City;
                    person.Address.Province = person.Address.Province != null ? person.Address.Province.ToUpper() : person.Address.Province;
                    person.Address.State = person.Address.State != null ? person.Address.State.ToUpper() : person.Address.State;
                    person.Address.Country = person.Address.Country != null ? person.Address.Country.ToUpper() : person.Address.Country;
                    person.Address.ZipCode = person.Address.ZipCode != null ? person.Address.ZipCode.ToUpper() : person.Address.ZipCode;
                }
                if (person.Address2 != null)
                {
                    person.Address2.Address1 = person.Address2.Address1 != null ? person.Address2.Address1.ToUpper() : person.Address2.Address1;
                    person.Address2.Address2 = person.Address2.Address2 != null ? person.Address2.Address2.ToUpper() : person.Address2.Address2;
                    person.Address2.AddressType = person.Address2.AddressType != null ? person.Address2.AddressType.ToUpper() : person.Address2.AddressType;
                    person.Address2.City = person.Address2.City != null ? person.Address2.City.ToUpper() : person.Address2.City;
                    person.Address2.Province = person.Address2.Province != null ? person.Address2.Province.ToUpper() : person.Address2.Province;
                    person.Address2.State = person.Address2.State != null ? person.Address2.State.ToUpper() : person.Address2.State;
                    person.Address2.Country = person.Address2.Country != null ? person.Address2.Country.ToUpper() : person.Address2.Country;
                    person.Address2.ZipCode = person.Address2.ZipCode != null ? person.Address2.ZipCode.ToUpper() : person.Address2.ZipCode;
                }
            }
        }
        #endregion

        #region Protected methods
        protected virtual PersonEntity Update(PersonEntity person)
        {
            if (person == null) throw new ArgumentNullException("person");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                person = this.InnerUpdate(person, userName, true);

                scope.Complete();
            }

            this.ResetPerson(person);
            LOPDLogger.Write(EntityNames.PersonEntityName, person.ID, ActionType.Modify);
            return person;
        }

        protected virtual CommonEntities.ElementEntity GetElementByName(string entityName, ElementBL elementBL)
        {
            try
            {
                CommonEntities.ElementEntity result = elementBL.GetElementByName(entityName);
                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        protected virtual void ValidatePerson(PersonEntity person, bool ignoreIdentifier, string configurationEntityName, ElementBL elementBL)
        {
            if (person == null) throw new ArgumentNullException("person");

            CommonEntities.ElementEntity _personMetadata = this.GetElementByName(EntityNames.PersonEntityName, elementBL);
            PersonHelper personHelper = new PersonHelper(_personMetadata);

            ValidationResults result = personHelper.Validate(person);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_personValidationError, sb));
            }

            if (_personMetadata.GetAttribute("Address").Visible)
            {
                if (person.Address != null)
                {
                    ValidateAddress(person.Address, elementBL);
                }
                else
                {
                    ValidateAddress(new AddressEntity(), elementBL);
                }
            }

            if (_personMetadata.GetAttribute("Address2").Visible)
            {
                if (person.Address2 != null)
                {
                    ValidateAddress(person.Address2, elementBL);
                }
                else
                {
                    ValidateAddress(new AddressEntity(), elementBL);
                }
            }

            if (person.Telephones != null)
            {
                foreach (PersonTelephoneEntity telephone in person.Telephones)
                {
                    if (telephone.Telephone != null)
                    {
                        ValidateTelephone(telephone.Telephone, elementBL);
                    }
                }
            }

            if (person.SensitiveData != null)
            {
                ValidateSensitiveData(person.SensitiveData, elementBL);
            }

            CommonEntities.AttributeEntity _personIdentifiersAttr = _personMetadata.GetAttribute("Identifiers");
            if (!ignoreIdentifier
                && _personIdentifiersAttr != null
                && _personIdentifiersAttr.HasOptions
                && _personIdentifiersAttr.AttributeOptions.Any(at => at.Value == configurationEntityName
                                                                    && at.Status == CommonEntities.AttributeStatusEnum.InUse))
            {
                CommonEntities.ElementEntity _personIdentifierMetadata = this.GetElementByName(EntityNames.PersonIdentifierEntityName, elementBL);
                CommonEntities.AttributeEntity _personIdentifierTypeAttr = _personIdentifierMetadata.GetAttribute("IdentifierType");
                if (_personIdentifierTypeAttr != null
                    && _personIdentifierTypeAttr.Required)
                {
                    if (person.Identifiers == null
                        || !person.Identifiers.Any(pi => pi.EditStatus.Value != StatusEntityValue.Deleted
                                                        && pi.EditStatus.Value != StatusEntityValue.NewAndDeleted
                                                        && ((!_personIdentifierTypeAttr.HasOptions && pi.IdentifierType.Name == _personIdentifierTypeAttr.DefaultValue)
                                                                || (_personIdentifierTypeAttr.HasOptions && _personIdentifierTypeAttr.AttributeOptions.Any(opt => opt.Value == pi.IdentifierType.Name)))))
                    {
                        IdentifierTypeEntity[] activeIdentifierTypesFromPerson = IdentifierTypeBL.GetIdentifierTypes(CategoryTypeEnum.Person);
                        if (_personIdentifierTypeAttr.HasOptions)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine();
                            string resultPersonIdentifierMsg = !string.IsNullOrWhiteSpace(_personIdentifierTypeAttr.DefaultValue)
                                                                ? _personIdentifierTypeAttr.DefaultValue
                                                                : string.Empty;
                            foreach (CommonEntities.AttributeOptionEntity option in _personIdentifierTypeAttr.AttributeOptions)
                            {
                                if (option.Value != _personIdentifierTypeAttr.DefaultValue
                                    && activeIdentifierTypesFromPerson != null
                                    && activeIdentifierTypesFromPerson.Any(it => it.Name == option.Value))
                                {
                                    resultPersonIdentifierMsg = resultPersonIdentifierMsg + " " + Properties.Resources.or + " " + option.Value;
                                }
                            }
                            sb.AppendFormat("■ {0}", string.Concat(resultPersonIdentifierMsg, " ", Properties.Resources.MSG_IsRequired));
                            throw new Exception(string.Format(Properties.Resources.ERROR_personValidationError, sb));
                        }
                        else if (!string.IsNullOrWhiteSpace(_personIdentifierTypeAttr.DefaultValue))
                        {
                            if (activeIdentifierTypesFromPerson != null
                                && activeIdentifierTypesFromPerson.Any(it => it.Name == _personIdentifierTypeAttr.DefaultValue))
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine();
                                sb.AppendFormat("■ {0}", string.Concat(_personIdentifierTypeAttr.DefaultValue, " ", Properties.Resources.MSG_IsRequired));
                                throw new Exception(string.Format(Properties.Resources.ERROR_personValidationError, sb));
                            }
                        }
                    }
                }
            }

            if (person.Identifiers != null)
            {
                foreach (PersonIdentifierEntity identifier in person.Identifiers)
                {
                    ValidateIdentifier(identifier, ignoreIdentifier, elementBL);
                }
            }
        }

        protected virtual void ResetPerson(PersonEntity person)
        {
            if (person.Address != null)
            {
                if (person.Address.AddressIsNullOrEmpty())
                {
                    person.Address.EditStatus.Reset();
                    person.Address.EditStatus.New();
                }
                else
                {
                    person.Address.EditStatus.Reset();
                }
            }
            if (person.Address2 != null)
            {
                if (person.Address2.AddressIsNullOrEmpty())
                {
                    person.Address2.EditStatus.Reset();
                    person.Address2.EditStatus.New();
                }
                else
                {
                    person.Address2.EditStatus.Reset();
                }
            }
            if (person.SensitiveData != null)
            {
                if (person.SensitiveData.SensitiveDataIsNullOrEmpty())
                {
                    person.SensitiveData.EditStatus.Reset();
                    person.SensitiveData.EditStatus.New();
                }
                else
                {
                    person.SensitiveData.EditStatus.Reset();
                }
            }
            if (person.Categories != null)
            {
                List<PersonCategoryEntity> Categories = new List<PersonCategoryEntity>();
                foreach (PersonCategoryEntity category in person.Categories)
                {
                    if (!((category.EditStatus.Value == StatusEntityValue.Deleted) || (category.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        category.EditStatus.Reset();
                        Categories.Add(category);
                    }
                }
                person.Categories = Categories.ToArray();
            }
            if (person.Telephones != null)
            {
                List<PersonTelephoneEntity> Telephones = new List<PersonTelephoneEntity>();
                foreach (PersonTelephoneEntity telephone in person.Telephones)
                {
                    if (!((telephone.EditStatus.Value == StatusEntityValue.Deleted) || (telephone.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        telephone.EditStatus.Reset();
                        Telephones.Add(telephone);
                    }
                }
                person.Telephones = Telephones.ToArray();
            }
            if (person.Identifiers != null)
            {
                List<PersonIdentifierEntity> Identifiers = new List<PersonIdentifierEntity>();
                foreach (PersonIdentifierEntity identifier in person.Identifiers)
                {
                    if (!((identifier.EditStatus.Value == StatusEntityValue.Deleted) || (identifier.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        identifier.EditStatus.Reset();
                        Identifiers.Add(identifier);
                    }
                }
                person.Identifiers = Identifiers.ToArray();
            }

            person.EditStatus.Reset();
        }

        protected virtual PersonEntity InnerInsert(PersonEntity person, string userName)
        {
            #region Person.Address
            if (person.Address != null)
            {
                switch (person.Address.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        break;
                    case StatusEntityValue.New:
                        person.Address.ID = _addressDA.Insert(person.Address.Address1, person.Address.Address2, person.Address.AddressType, person.Address.City, person.Address.Country,
                            person.Address.Province, person.Address.State, person.Address.ZipCode, userName);
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

            #region Person.Address2
            if (person.Address2 != null)
            {
                switch (person.Address2.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        break;
                    case StatusEntityValue.New:
                        person.Address2.ID = _addressDA.Insert(person.Address2.Address1, person.Address2.Address2, person.Address2.AddressType, person.Address2.City, person.Address2.Country,
                            person.Address2.Province, person.Address2.State, person.Address2.ZipCode, userName);
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

            #region Person
            person.ImageID = ((person.ImageData != null) && (person.ImageData.Length > 0))
                ? _dbImageStorageDA.Insert(person.ImageData, userName)
                : 0;


            person.ID = _personDA.Insert(person.FirstName, person.LastName, person.AsUser, person.LastName2,
                person.EmailAddress, person.ImageID,
                person.DuplicateGroupID, person.RecordMerged, person.HasMergedRegisters,
                (person.Address != null) ? person.Address.ID : 0,
                (person.Address2 != null) ? person.Address2.ID : 0, userName, (int)person.Status);
            #endregion

            #region Categories
            if ((person.Categories != null) && (person.Categories.Length > 0))
            {
                foreach (PersonCategoryEntity category in person.Categories)
                {
                    if (category.Category != null)
                    {
                        switch (category.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted: _personCatRelDA.Delete(category.ID); break;
                            case StatusEntityValue.New:
                                if (category.Category != null)
                                {
                                    _personCatRelDA.Insert(person.ID, category.Category.ID, userName);
                                }
                                break;
                            case StatusEntityValue.NewAndDeleted: break;
                            case StatusEntityValue.None: break;
                            case StatusEntityValue.Updated: _personCatRelDA.Update(category.ID, person.ID, category.Category.ID, userName); break;
                            default: break;
                        }
                    }
                }
            }
            #endregion

            #region Telephones
            if ((person.Telephones != null) && (person.Telephones.Length > 0))
            {
                foreach (PersonTelephoneEntity personTelephone in person.Telephones)
                {
                    if (personTelephone.Telephone != null)
                    {
                        switch (personTelephone.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted: break;
                            case StatusEntityValue.New:
                                if (personTelephone.Telephone != null)
                                {
                                    personTelephone.Telephone.ID = _telephoneDA.Insert(personTelephone.Telephone.Telephone, personTelephone.Telephone.Comments,
                                        personTelephone.Telephone.TelephoneType, personTelephone.Telephone.EmergencyContactPhone, userName);
                                    _personTelephoneRelDA.Insert(person.ID, personTelephone.Telephone.ID, userName);
                                }
                                break;
                            case StatusEntityValue.NewAndDeleted: break;
                            case StatusEntityValue.None: break;
                            case StatusEntityValue.Updated: break;
                            default: break;
                        }
                    }
                }
            }
            #endregion

            #region Identifiers
            if ((person.Identifiers != null) && (person.Identifiers.Length > 0))
            {
                foreach (PersonIdentifierEntity identifier in person.Identifiers)
                {
                    switch (identifier.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted: break;
                        case StatusEntityValue.New:
                            if (identifier.IdentifierType != null)
                            {
                                _personIdentifierRelDA.Insert(person.ID, identifier.IdentifierType.ID, identifier.IDNumber, userName);
                            }
                            break;
                        case StatusEntityValue.NewAndDeleted: break;
                        case StatusEntityValue.None: break;
                        case StatusEntityValue.Updated: break;
                        default: break;
                    }
                }
            }
            #endregion

            #region Person.SensitiveData
            if (person.SensitiveData != null)
            {
                switch (person.SensitiveData.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted: break;
                    case StatusEntityValue.New:
                        person.SensitiveData.ID = _sensitiveDataDA.Insert(person.SensitiveData.BirthDate, person.ID, (short)person.SensitiveData.Sex,
                            (short)person.SensitiveData.ReligiousPreference, (short)person.SensitiveData.Language, (short)person.SensitiveData.EducationLevel,
                            (short)person.SensitiveData.MaritalStatus, person.SensitiveData.BirthPlace, person.SensitiveData.Citizenship,
                            person.SensitiveData.CitizenshipComments, person.SensitiveData.DeathDateTime, person.SensitiveData.DeathReason, userName);
                        person.SensitiveData.DBTimeStamp = _sensitiveDataDA.GetDBTimeStamp(person.SensitiveData.ID);

                        break;
                    case StatusEntityValue.NewAndDeleted: break;
                    case StatusEntityValue.None: break;
                    case StatusEntityValue.Updated: break;
                    default: break;
                }
            }
            #endregion

            #region Update Related Caches
            _personDA.MarkRelatedPhysiciansUpdated(person.ID, userName);
            #endregion

            person.DBTimeStamp = _personDA.GetDBTimeStamp(person.ID);

            UpdatePhoneticInfo(person.ID);

            return person;
        }

        protected virtual PersonEntity InnerUpdate(PersonEntity person, string userName, bool fullPersonUpdate)
        {
            Int64 dbTimeStamp = _personDA.GetDBTimeStamp(person.ID);
            if (dbTimeStamp != person.DBTimeStamp)
                throw new FaultException<DBConcurrencyException>(
                    new DBConcurrencyException(string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, person.ID)), string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, person.ID));

            if (fullPersonUpdate)
            {
                #region Person.Address
                if (person.Address != null)
                {
                    switch (person.Address.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _addressDA.Delete(person.Address.ID);
                            break;
                        case StatusEntityValue.New:
                            person.Address.ID = _addressDA.Insert(person.Address.Address1, person.Address.Address2, person.Address.AddressType, person.Address.City, person.Address.Country,
                                person.Address.Province, person.Address.State, person.Address.ZipCode, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _addressDA.Update(person.Address.ID, person.Address.Address1, person.Address.Address2, person.Address.AddressType, person.Address.City,
                                person.Address.Country, person.Address.Province, person.Address.State, person.Address.ZipCode, userName);
                            break;
                        default:
                            break;
                    }
                    person.Address.EditStatus.Reset();
                }
                #endregion

                #region Person.Address2
                if (person.Address2 != null)
                {
                    switch (person.Address2.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _addressDA.Delete(person.Address2.ID);
                            break;
                        case StatusEntityValue.New:
                            person.Address2.ID = _addressDA.Insert(person.Address2.Address1, person.Address2.Address2, person.Address2.AddressType, person.Address2.City, person.Address2.Country,
                                person.Address2.Province, person.Address2.State, person.Address2.ZipCode, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _addressDA.Update(person.Address2.ID, person.Address2.Address1, person.Address2.Address2, person.Address2.AddressType, person.Address2.City,
                                person.Address2.Country, person.Address2.Province, person.Address2.State, person.Address2.ZipCode, userName);
                            break;
                        default:
                            break;
                    }
                    person.Address2.EditStatus.Reset();
                }
                #endregion

                #region Person

                int oldImageID = _personDA.GetImageIDByPersonID(person.ID);

                if (person.ImageID == 0)
                {
                    if (oldImageID > 0)
                    {
                        _dbImageStorageDA.Delete(oldImageID);
                    }

                    person.ImageID = ((person.ImageData != null) && (person.ImageData.Length > 0))
                        ? person.ImageID = _dbImageStorageDA.Insert(person.ImageData, userName)
                        : 0;
                }


                //int oldImageID = _personDA.GetImageIDByPersonID(person.ID);
                //if (person.ImageID == 0)
                //{
                //    if (oldImageID > 0)
                //        _dbImageStorageDA.Delete(oldImageID);

                //    person.ImageID = _dbImageStorageDA.Insert(person.ImageData, userName);
                //}
                //int recordMerged = _personDA.GetRecordMerged(person.ID);
                //if (recordMerged > 0 && !person.HasDuplicate)
                //{
                //    _personDA.UpdateSecondDuplicate(recordMerged, false, 0, userName);
                //}

                //if (person.HasDuplicate && person.RecordMerged <= 0)
                //{
                //    person.HasDuplicate = false;
                //}

                bool result = _personDA.Update(person.ID, person.FirstName, person.LastName, person.AsUser,
                    person.LastName2, person.EmailAddress, person.ImageID,
                    person.DuplicateGroupID, person.RecordMerged, person.HasMergedRegisters,
                    (person.Address != null) ? person.Address.ID : 0,
                    (person.Address2 != null) ? person.Address2.ID : 0,
                    userName, (int)person.Status) > 0;

                //if (recordMerged <= 0 && person.HasDuplicate && person.RecordMerged > 0
                //    && person.Status == CommonEntities.StatusEnum.Active)
                //{
                //    _personDA.UpdateSecondDuplicate(person.RecordMerged, true, person.ID, userName);
                //}
                #endregion

                #region Categories
                if ((person.Categories != null) && (person.Categories.Length > 0))
                {
                    foreach (PersonCategoryEntity category in person.Categories)
                    {
                        switch (category.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted: _personCatRelDA.Delete(category.ID); break;
                            case StatusEntityValue.New:
                                if (category.Category != null)
                                {
                                    _personCatRelDA.Insert(person.ID, category.Category.ID, userName);
                                }
                                break;
                            case StatusEntityValue.NewAndDeleted: break;
                            case StatusEntityValue.None: break;
                            case StatusEntityValue.Updated: _personCatRelDA.Update(category.ID, person.ID, category.Category.ID, userName); break;
                            default: break;
                        }
                    }
                }
                #endregion

                #region Telephones
                if ((person.Telephones != null) && (person.Telephones.Length > 0))
                {
                    foreach (PersonTelephoneEntity personTelephone in person.Telephones)
                    {
                        switch (personTelephone.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                if (personTelephone.Telephone != null)
                                {
                                    _telephoneDA.Delete(personTelephone.Telephone.ID);
                                    _personTelephoneRelDA.Delete(personTelephone.ID);
                                }
                                break;
                            case StatusEntityValue.New:
                                if (personTelephone.Telephone != null)
                                {
                                    personTelephone.Telephone.ID = _telephoneDA.Insert(personTelephone.Telephone.Telephone, personTelephone.Telephone.Comments,
                                        personTelephone.Telephone.TelephoneType, personTelephone.Telephone.EmergencyContactPhone, userName);
                                    _personTelephoneRelDA.Insert(person.ID, personTelephone.Telephone.ID, userName);
                                }
                                break;
                            case StatusEntityValue.NewAndDeleted: break;
                            case StatusEntityValue.None: break;
                            case StatusEntityValue.Updated:
                                if (personTelephone.Telephone != null)
                                {
                                    _personTelephoneRelDA.Update(personTelephone.ID, person.ID, personTelephone.Telephone.ID, userName);
                                    _telephoneDA.Update(personTelephone.Telephone.ID, personTelephone.Telephone.Telephone, personTelephone.Telephone.Comments,
                                        personTelephone.Telephone.TelephoneType, personTelephone.Telephone.EmergencyContactPhone, userName);
                                }
                                break;
                            default: break;
                        }
                    }
                }
                #endregion

                #region Identifiers
                if ((person.Identifiers != null) && (person.Identifiers.Length > 0))
                {
                    foreach (PersonIdentifierEntity identifier in person.Identifiers)
                    {
                        switch (identifier.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted: _personIdentifierRelDA.Delete(identifier.ID); break;
                            case StatusEntityValue.New:
                                if (identifier.IdentifierType != null)
                                {
                                         
                                   identifier.ID = _personIdentifierRelDA.Insert(person.ID, identifier.IdentifierType.ID, identifier.IDNumber, userName);
                                   
                                   identifier.EditStatus.Reset();
                                }
                                break;
                            case StatusEntityValue.NewAndDeleted: break;
                            case StatusEntityValue.None: break;
                            case StatusEntityValue.Updated:
                                if (identifier.IdentifierType != null)
                                {
                                    _personIdentifierRelDA.Update(identifier.ID, person.ID, identifier.IdentifierType.ID, identifier.IDNumber, userName);
                                    identifier.EditStatus.Reset();
                                }
                                break;
                            default: break;
                        }
                    }
                }
                #endregion

                #region Person.SensitiveData
                if (person.SensitiveData != null)
                {
                    switch (person.SensitiveData.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted: _sensitiveDataDA.Delete(person.SensitiveData.ID); break;
                        case StatusEntityValue.New:
                            person.SensitiveData.ID = _sensitiveDataDA.Insert(person.SensitiveData.BirthDate, person.ID, (short)person.SensitiveData.Sex,
                                (short)person.SensitiveData.ReligiousPreference, (short)person.SensitiveData.Language, (short)person.SensitiveData.EducationLevel,
                                (short)person.SensitiveData.MaritalStatus, person.SensitiveData.BirthPlace, person.SensitiveData.Citizenship,
                                person.SensitiveData.CitizenshipComments, person.SensitiveData.DeathDateTime, person.SensitiveData.DeathReason, userName);
                            person.SensitiveData.DBTimeStamp = _sensitiveDataDA.GetDBTimeStamp(person.SensitiveData.ID);

                            break;
                        case StatusEntityValue.NewAndDeleted: break;
                        case StatusEntityValue.None: break;
                        case StatusEntityValue.Updated:
                            _sensitiveDataDA.Update(person.SensitiveData.ID, person.SensitiveData.BirthDate, person.ID, (short)person.SensitiveData.Sex,
                                (short)person.SensitiveData.ReligiousPreference, (short)person.SensitiveData.Language, (short)person.SensitiveData.EducationLevel,
                                (short)person.SensitiveData.MaritalStatus, person.SensitiveData.BirthPlace, person.SensitiveData.Citizenship,
                                person.SensitiveData.CitizenshipComments, person.SensitiveData.DeathDateTime, person.SensitiveData.DeathReason, userName);
                            person.SensitiveData.DBTimeStamp = _sensitiveDataDA.GetDBTimeStamp(person.SensitiveData.ID);

                            break;
                        default: break;
                    }
                }
                #endregion
            }
            else
            {
                _personDA.Update(person.ID, userName);
            }

            person.DBTimeStamp = _personDA.GetDBTimeStamp(person.ID);

            UpdatePhoneticInfo(person.ID);

            #region Update Related Caches
            //_personDA.MarkRelatedPhysiciansUpdated(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromAdmissionConfigMedEpProcessPhysicianRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromReceptionConfigMedEpProcessPhysicianRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromReceptionConfigReceptionResourceRelHHRR(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromReceptionConfigReceptionResourceRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromCitationConfigMedEpProcessPhysicianRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromCitationConfigCitationResourceRelHHRR(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromCitationConfigCitationResourceRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromWaitingListConfigMedEpProcessPhysicianRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromWaitingListConfigWLResourceRelHHRR(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromWaitingListConfigWLResourceRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromInterviewConfigMedEpProcessPhysicianRelPhysician(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromReservationConfigResourceReservedRel(person.ID, userName);
            _personDA.MarkUpdatedRelatedPhysician(person.ID, userName);
            //_personDA.MarkUpdatedRelatedPhysicianFromOrganizationContactPerson(person.ID, userName);
            _personDA.MarkRelatedUpdatedObservationsFromObservationNotificationCriterionNotificationToPerson(person.ID, userName);
            _personDA.MarkRelatedUpdatedObservationTypesFromObservationObservationNotificationCriterionNotificationToPerson(person.ID, userName);
            _personDA.MarkRelatedUpdatedObservationBlocksFromObservationObservationNotificationCriterionNotificationToPerson(person.ID, userName);
            _personDA.MarkRelatedUpdatedObservationTemplatesFromObservationObservationNotificationCriterionNotificationToPerson(person.ID, userName);
            _personDA.MarkRelatedUpdatedObservationTemplatesFromObservationBlockObservationNotificationCriterionNotificationToPerson(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromProcessChartCareCenterRelProcessChartHierarchyRelNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromAdmissionConfigNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromTransferConfigNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromLeaveConfigNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromInterviewConfigNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromPreAssessmentConfigNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromReportConfigResultRejectNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromReportConfigReportAbortCancelNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromReportConfigReportSignedNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromCoverConfigNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromDeliveryNoteConfigNotification(person.ID, userName);
            _personDA.MarkUpdatedRelatedProcessChartFromInvoiceConfigNotification(person.ID, userName);
            #endregion

            return person;
        }

        #region CheckPreconditions and alternative methods to validation.
        protected virtual void CheckInsertPreconditions(PersonEntity person, int customerID, CategoryPersonKeyEnum categoryPerson, bool forceSave,
            bool ignoreIdentifier, bool validate, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (person == null) throw new ArgumentNullException("person");

            //Añadirle la categoría de donde venga
            PersonFindTypeEnum findType = PersonFindTypeEnum.Person;
            string configurationEntityName = string.Empty;
            int categoryID = 0;
            switch (categoryPerson)
            {
                case CategoryPersonKeyEnum.Customer:
                    configurationEntityName = EntityNames.CustomerEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.NOK:
                    configurationEntityName = EntityNames.NOKEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    findType = PersonFindTypeEnum.NOK;
                    break;
                case CategoryPersonKeyEnum.CustContactPerson:
                    configurationEntityName = EntityNames.CustomerContactPersonEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    findType = PersonFindTypeEnum.ContactPerson;
                    break;
                case CategoryPersonKeyEnum.HHRR:
                    configurationEntityName = EntityNames.HumanResourceEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.OrgContactPerson:
                    configurationEntityName = EntityNames.OrganizationContactPersonEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.Physician:
                    configurationEntityName = EntityNames.PhysicianEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.PotentialCustomer:
                case CategoryPersonKeyEnum.LegalPerson:
                case CategoryPersonKeyEnum.ReferringPhysician:
                case CategoryPersonKeyEnum.Nurse:
                case CategoryPersonKeyEnum.ReferringNurse:
                    configurationEntityName = EntityNames.PersonEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.None:
                    configurationEntityName = EntityNames.PersonEntityName;
                    break;
                default:
                    break;
            }

            if (validate)
                ValidatePerson(person, ignoreIdentifier, configurationEntityName, elementBL);

            GlobalConfigurationSection globalConfig = FrameworkConfigurationService<GlobalConfigurationSection>.GetSection("global") as GlobalConfigurationSection;
            SavePersonWithUpperCase(person, globalConfig);
            SavePersonWithTrim(person, globalConfig);

            //TODO Comprobar que no tenga esa categoria
            if ((categoryID > 0) && ((person.Categories == null) || !(Array.Exists(person.Categories, pce => ((pce.Category != null) ? pce.Category.ID : 0) == categoryID))))
            {
                CategoryBL _categoryBL = new CategoryBL();
                List<PersonCategoryEntity> listCat = (person.Categories != null) ? new List<PersonCategoryEntity>(person.Categories) : new List<PersonCategoryEntity>();

                PersonCategoryEntity personCategory = new PersonCategoryEntity();
                personCategory.Category = _categoryBL.GetCategory(categoryID);
                personCategory.EditStatus.New();
                listCat.Add(personCategory);

                person.Categories = listCat.ToArray();
            }

            homonymPersons = null;
            if (!forceSave)
            {
                int requiredDefaultIdentifierTypeID;
                string requiredIDNumber;

                List<StoredProcInParam> sqlParams = new List<StoredProcInParam>();
                String queryResult = String.Empty;
                this.SetPersonConfiguration(ref queryResult, ref sqlParams, person, configurationEntityName, ignoreIdentifier,
                    out requiredDefaultIdentifierTypeID, out requiredIDNumber, elementBL);

                bool maxRecordsExceded = false;
                string firstName = (sqlParams.Count > 0)
                ? ((from param in sqlParams where param.Name == "FirstName" select param).Count() > 0) ? person.FirstName : string.Empty
                : string.Empty;
                string lastName = (sqlParams.Count > 0)
                    ? ((from param in sqlParams where param.Name == "LastName" select param).Count() > 0) ? person.LastName : string.Empty
                    : string.Empty;
                string lastName2 = (sqlParams.Count > 0)
                    ? ((from param in sqlParams where param.Name == "LastName2" select param).Count() > 0) ? person.LastName2 : string.Empty
                    : string.Empty;

                PersonAddressListDTO[] findPersons = this.GetPersons(firstName, lastName, lastName2, requiredDefaultIdentifierTypeID,
                    requiredIDNumber, findType, customerID, out maxRecordsExceded);

                int[] ids = (queryResult.Contains("WHERE")) ? _personDA.GetPersonIDs(queryResult, sqlParams.ToArray()) : null;

                if ((ids != null) && (ids.Length > 0))
                {
                    bool hasIdentifierFilter = (sqlParams.Count > 0)
                        ? ((from param in sqlParams where param.Name == "IDNumber" select param).Count() > 0)
                        : false;

                    List<int> filterIDs = new List<int>();
                    foreach (int id in ids)
                    {
                        if (findPersons != null)
                        {
                            if ((from per in findPersons where per.ID == id select per).Count() > 0)
                                filterIDs.Add(id);
                        }
                        else
                            filterIDs.Add(id);
                    }

                    if (filterIDs.Count == 1)
                    {
                        if (hasIdentifierFilter)
                            throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(person.FirstName, " ", person.LastName)));
                        else
                            homonymPersons = ((findPersons != null) && (findPersons.Length > 0)) ? findPersons : null;
                    }
                    else
                        homonymPersons = ((findPersons != null) && (findPersons.Length > 0)) ? findPersons : null;
                }
                else homonymPersons = ((findPersons != null) && (findPersons.Length > 0)) ? findPersons : null;
            }

            if ((person.Identifiers != null) && (person.Identifiers.Length > 0))
            {
                Dictionary<int, PersonIdentifierEntity> identifiers = new Dictionary<int, PersonIdentifierEntity>();

                foreach (PersonIdentifierEntity identifier in person.Identifiers)
                {
                    if ((identifier.EditStatus.Value == StatusEntityValue.New) || (identifier.EditStatus.Value == StatusEntityValue.None) || (identifier.EditStatus.Value == StatusEntityValue.Updated))
                    {
                        if (identifier.IdentifierType != null)
                        {
                            if (identifiers.ContainsKey(identifier.IdentifierType.ID))
                            {
                                throw new Exception(string.Format(Properties.Resources.MSG_IdentifierDuplicated, identifier.IdentifierType.Name));
                            }
                            else
                            {
                                identifiers.Add(identifier.IdentifierType.ID, identifier);
                            }
                        }
                    }
                }
            }
        }

        protected virtual void CheckUpdatePreconditions(PersonEntity person, int customerID, CategoryPersonKeyEnum categoryPerson, bool forceSave,
            bool ignoreIdentifier, bool validate, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (person == null) throw new ArgumentNullException("person");

            PersonFindTypeEnum findType = PersonFindTypeEnum.Person;
            string configurationEntityName = string.Empty;
            int categoryID = 0;
            switch (categoryPerson)
            {
                case CategoryPersonKeyEnum.Customer:
                    configurationEntityName = EntityNames.CustomerEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.NOK:
                    configurationEntityName = EntityNames.NOKEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    findType = PersonFindTypeEnum.NOK;
                    break;
                case CategoryPersonKeyEnum.CustContactPerson:
                    configurationEntityName = EntityNames.CustomerContactPersonEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    findType = PersonFindTypeEnum.ContactPerson;
                    break;
                case CategoryPersonKeyEnum.HHRR:
                    configurationEntityName = EntityNames.HumanResourceEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.OrgContactPerson:
                    configurationEntityName = EntityNames.OrganizationContactPersonEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.Physician:
                    configurationEntityName = EntityNames.PhysicianEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.PotentialCustomer:
                case CategoryPersonKeyEnum.LegalPerson:
                case CategoryPersonKeyEnum.ReferringPhysician:
                case CategoryPersonKeyEnum.Nurse:
                case CategoryPersonKeyEnum.ReferringNurse:
                    configurationEntityName = EntityNames.PersonEntityName;
                    categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)categoryPerson);
                    break;
                case CategoryPersonKeyEnum.None:
                    configurationEntityName = EntityNames.PersonEntityName;
                    break;
                default:
                    break;
            }

            if (validate)
                ValidatePerson(person, ignoreIdentifier, configurationEntityName, elementBL);

            GlobalConfigurationSection globalConfig = FrameworkConfigurationService<GlobalConfigurationSection>.GetSection("global") as GlobalConfigurationSection;
            SavePersonWithUpperCase(person, globalConfig);
            SavePersonWithTrim(person, globalConfig);

            //TODO Comprobar que no tenga esa categoria
            if ((categoryID > 0) && ((person.Categories == null) || !(Array.Exists(person.Categories, pce => ((pce.Category != null) ? pce.Category.ID : 0) == categoryID))))
            {
                CategoryBL _categoryBL = new CategoryBL();
                List<PersonCategoryEntity> listCat = (person.Categories != null) ? new List<PersonCategoryEntity>(person.Categories) : new List<PersonCategoryEntity>();

                PersonCategoryEntity personCategory = new PersonCategoryEntity();
                personCategory.Category = _categoryBL.GetCategory(categoryID);
                personCategory.EditStatus.New();
                listCat.Add(personCategory);

                person.Categories = listCat.ToArray();
            }

            homonymPersons = null;
            if (!forceSave)
            {
                int requiredDefaultIdentifierTypeID;
                string requiredIDNumber;

                List<StoredProcInParam> sqlParams = new List<StoredProcInParam>();
                String queryResult = String.Empty;
                this.SetPersonConfiguration(ref queryResult, ref sqlParams, person, configurationEntityName, ignoreIdentifier,
                    out requiredDefaultIdentifierTypeID, out requiredIDNumber, elementBL);

                bool maxRecordsExceded = false;
                string firstName = (sqlParams.Count > 0)
                    ? ((from param in sqlParams where param.Name == "FirstName" select param).Count() > 0) ? person.FirstName : string.Empty
                    : string.Empty;
                string lastName = (sqlParams.Count > 0)
                    ? ((from param in sqlParams where param.Name == "LastName" select param).Count() > 0) ? person.LastName : string.Empty
                    : string.Empty;
                string lastName2 = (sqlParams.Count > 0)
                    ? ((from param in sqlParams where param.Name == "LastName2" select param).Count() > 0) ? person.LastName2 : string.Empty
                    : string.Empty;

                PersonAddressListDTO[] findPersons = this.GetPersons(firstName, lastName, lastName2, requiredDefaultIdentifierTypeID, requiredIDNumber, findType, customerID, out maxRecordsExceded);
                PersonAddressListDTO[] filterPersons = (findPersons != null)
                            ? (from per in findPersons where per.ID != person.ID select per).ToArray()
                            : null;

                int[] ids = (queryResult.Contains("WHERE")) ? _personDA.GetPersonIDs(queryResult, sqlParams.ToArray()) : null;

                if ((ids != null) && (ids.Length > 0))
                {
                    List<int> filterIDs = new List<int>();
                    foreach (int id in ids)
                    {
                        if (filterPersons != null)
                        {
                            if ((from per in filterPersons where per.ID == id select per).Count() > 0)
                                filterIDs.Add(id);
                        }
                        else
                            filterIDs.Add(id);
                    }

                    if (filterIDs.Count == 1)
                    {
                        foreach (int personID in filterIDs)
                        {
                            if (personID != person.ID)
                                throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(person.FirstName, " ", person.LastName)));
                        }
                    }
                    else homonymPersons = ((filterPersons != null) && (filterPersons.Length > 0)) ? filterPersons : null;
                }
                else homonymPersons = ((filterPersons != null) && (filterPersons.Length > 0)) ? filterPersons : null;

                //if ((ids != null) && (ids.Length > 0))
                //{
                //    List<int> filterIDs = new List<int>();
                //    foreach (int id in ids)
                //    {
                //        if (filterPersons != null)
                //        {
                //            if ((from per in filterPersons where per.ID == id select per).Count() > 0)
                //                filterIDs.Add(id);
                //        }
                //        else
                //            filterIDs.Add(id);
                //    }

                //    if (filterIDs.Count == 1)
                //    {
                //        foreach (int personID in filterIDs)
                //        {
                //            if (personID == person.ID)
                //                throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(person.FirstName, " ", person.LastName)));
                //        }
                //    }
                //    else
                //    {
                //        homonymPersons = ((filterPersons != null) && (filterPersons.Length > 0)) ? filterPersons : null;
                //    }
                //}
            }
        }

        private void SetPersonConfiguration(ref string queryResult, ref List<StoredProcInParam> sqlParams, PersonEntity person, string parentEntityName,
            bool ignoreIdentifier, out int requiredDefaultIdentifierTypeID, out string requiredIDNumber, ElementBL elementBL)
        {
            requiredDefaultIdentifierTypeID = 0;
            requiredIDNumber = string.Empty;

            string selectCommand = "SELECT DISTINCT Person.[ID] ";
            string fromCommand = "FROM Person ";
            string whereCommand = String.Empty;

            this.SetDataValidator(ref sqlParams, ref fromCommand, ref whereCommand, person, parentEntityName, ignoreIdentifier,
                out requiredDefaultIdentifierTypeID, out requiredIDNumber, elementBL);

            queryResult = String.Concat(selectCommand, fromCommand, !String.IsNullOrEmpty(whereCommand) ? String.Concat("WHERE ", whereCommand) : String.Empty);
        }

        private void SetDataValidator(ref List<StoredProcInParam> sqlParams, ref string fromCommand, ref string whereCommand, PersonEntity person, string parentEntityName,
            bool ignoreIdentifier, out int requiredDefaultIdentifierTypeID, out string requiredIDNumber, ElementBL elementBL)
        {
            requiredDefaultIdentifierTypeID = 0;
            requiredIDNumber = string.Empty;

            CommonEntities.ElementEntity _personMetadata = this.GetElementByName(EntityNames.PersonEntityName, elementBL);

            if (_personMetadata == null)
                return;

            string fromConfigurationaux = string.Empty;
            string whereConfigurationaux = string.Empty;
            foreach (CommonEntities.AttributeEntity attribute in _personMetadata.Attributes)
            {
                if ((attribute.AttributeType != CommonEntities.AttributeTypeEnum.Entity)
                    && (attribute.Name != "ID")
                    && (attribute.DesignRequired)
                    && ((attribute.AttributeOptions == null) || (attribute.AttributeOptions.Length <= 0)))
                {
                    sqlParams.Add(SetData(out whereConfigurationaux, person, attribute));
                    if (whereCommand != String.Empty)
                        whereCommand = String.Concat(whereCommand, " AND ", whereConfigurationaux);
                    else
                        whereCommand = whereConfigurationaux;
                }
                if ((attribute.AttributeType == CommonEntities.AttributeTypeEnum.Entity)
                    && (attribute.AttributeOptions != null) && (attribute.AttributeOptions.Length > 0)
                    && (!string.IsNullOrEmpty(parentEntityName)) && (attribute.AttributeOptions.Any(value => (value.Value == parentEntityName)
                    && (value.Status == CommonEntities.AttributeStatusEnum.InUse))))
                {
                    string entityName = this.GetEntityNameByAttributeTypeName(attribute.TypeName);
                    switch (entityName)
                    {
                        case "PersonIdentifierEntity":
                            if (!ignoreIdentifier)
                            {
                                fromCommand = String.Concat(fromCommand, Environment.NewLine, "LEFT JOIN PersonIdentifierRel ON Person.[ID]=PersonIdentifierRel.PersonID ");
                                SetDataIdentifierValidator(ref sqlParams, ref  whereCommand, person.Identifiers, parentEntityName, out requiredDefaultIdentifierTypeID, out requiredIDNumber, elementBL);
                            }
                            break;
                        case "PersonCategoryEntity":
                            fromCommand = String.Concat(fromCommand, Environment.NewLine, "LEFT JOIN PersonCatRel ON Person.[ID]=PersonCatRel.PersonID ",
                                 Environment.NewLine, "LEFT JOIN Category ON PersonCatRel.[CategoryID]=Category.[ID] ");
                            SetDataCategoryValidator(ref sqlParams, ref  whereCommand, person.Categories, parentEntityName, elementBL);
                            break;
                        case "PersonTelephoneEntity":
                            fromCommand = String.Concat(fromCommand, Environment.NewLine, "LEFT JOIN PersonTelephoneRel ON Person.[ID]=PersonTelephoneRel.PersonID ",
                                 Environment.NewLine, "LEFT JOIN Telephone ON PersonTelephoneRel.[TelephoneID]=Telephone.[ID] ");
                            SetDataTelephoneValidator(ref sqlParams, ref  whereCommand, person.Telephones, parentEntityName, elementBL);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void SetDataIdentifierValidator(ref List<StoredProcInParam> sqlParams, ref string whereCommand, PersonIdentifierEntity[] identifiers,
            string parentEntityName, out int requiredDefaultIdentifierTypeID, out string requiredIDNumber, ElementBL elementBL)
        {
            requiredDefaultIdentifierTypeID = 0;
            requiredIDNumber = string.Empty;

            CommonEntities.ElementEntity _personIdentifierMetadata = this.GetElementByName(EntityNames.PersonIdentifierEntityName, elementBL);

            if (_personIdentifierMetadata == null)
                return;

            CommonEntities.AttributeEntity attr = (_personIdentifierMetadata.Attributes != null)
                ? (from att in _personIdentifierMetadata.Attributes where att.Name == "IdentifierType" select att).FirstOrDefault()
                : null;

            string defaultIdentifierType = (attr != null) ? attr.DefaultValue : string.Empty;
            int identifierTypeID = 0;
            if (!string.IsNullOrEmpty(defaultIdentifierType))
            {
                IdentifierTypeDA identifierTypeDA = new IdentifierTypeDA();
                identifierTypeID = identifierTypeDA.GetIdentifierTypeID(defaultIdentifierType);
            }

            #region Check Default Identifier
            if ((identifiers != null) && (identifiers.Length > 0))
            {
                PersonIdentifierEntity identifier = (from idt in identifiers
                                                     where (idt.EditStatus.Value != StatusEntityValue.Deleted)
                                                        && (idt.EditStatus.Value != StatusEntityValue.NewAndDeleted)
                                                        && (idt.IdentifierType != null) && (idt.IdentifierType.Name == defaultIdentifierType)
                                                     select idt).FirstOrDefault();
                if (identifier != null)
                {
                    requiredDefaultIdentifierTypeID = identifierTypeID;
                    requiredIDNumber = identifier.IDNumber;

                    sqlParams.Add(new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID));
                    sqlParams.Add(new StoredProcInParam("IDNumber", DbType.String, identifier.IDNumber));
                    if (whereCommand != String.Empty)
                        whereCommand = String.Concat(whereCommand, Environment.NewLine, "AND PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID");
                    else
                        whereCommand = "PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID";
                    return;
                }
            }
            #endregion

            if (!CheckAlternatives(ref sqlParams, ref whereCommand, identifiers, defaultIdentifierType, attr, out requiredDefaultIdentifierTypeID, out requiredIDNumber))
            {
                throw new Exception(String.Format(Properties.Resources.MSG_CustomerIdentifierRequired, defaultIdentifierType));
            }
        }

        private bool CheckAlternatives(ref List<StoredProcInParam> sqlParams, ref string whereCommand, PersonIdentifierEntity[] identifiers, string defaultIdentifierType,
            CommonEntities.AttributeEntity attr, out int requiredDefaultIdentifierTypeID, out string requiredIDNumber)
        {
            requiredDefaultIdentifierTypeID = 0;
            requiredIDNumber = string.Empty;

            if ((attr != null) && (attr.AttributeOptions != null))
            {
                foreach (CommonEntities.AttributeOptionEntity option in attr.AttributeOptions)
                {
                    if ((identifiers != null) && (identifiers.Length > 0))
                    {
                        PersonIdentifierEntity identifier = (from idt in identifiers
                                                             where (idt.EditStatus.Value != StatusEntityValue.Deleted)
                                                                && (idt.EditStatus.Value != StatusEntityValue.NewAndDeleted)
                                                                && (idt.IdentifierType != null) && (idt.IdentifierType.Name == option.Value) && (idt.IdentifierType.Name != defaultIdentifierType)
                                                             select idt).FirstOrDefault();
                        if (identifier != null)
                        {
                            requiredDefaultIdentifierTypeID = identifier.IdentifierType.ID;
                            requiredIDNumber = identifier.IDNumber;

                            sqlParams.Add(new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifier.IdentifierType.ID));
                            sqlParams.Add(new StoredProcInParam("IDNumber", DbType.String, identifier.IDNumber));
                            if (whereCommand != String.Empty)
                                whereCommand = String.Concat(whereCommand, Environment.NewLine, "AND PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID");
                            else
                                whereCommand = "PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID";
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void SetDataCategoryValidator(ref List<StoredProcInParam> sqlParams, ref string whereCommand, PersonCategoryEntity[] categories, string parentEntityName, ElementBL elementBL)
        {
            CommonEntities.ElementEntity _personCategoryMetadata = this.GetElementByName(EntityNames.PersonCategoryEntityName, elementBL);
            if (_personCategoryMetadata == null) return;
            CommonEntities.AttributeEntity attr = (_personCategoryMetadata.Attributes != null)
                ? (from att in _personCategoryMetadata.Attributes where att.Name == "Category" select att).FirstOrDefault()
                : null;
            if ((attr != null)
                && (attr.AttributeOptions != null)
                && (attr.AttributeOptions.Length > 0))
            {
                CommonEntities.AttributeOptionEntity attrOption = (from attOption in attr.AttributeOptions
                                                                   where (attOption.Description == parentEntityName)
                                                                   select attOption).FirstOrDefault();
                if (attrOption != null)
                {
                    sqlParams.Add(new StoredProcInParam("CategoryKey", DbType.Int32, SIIConvert.ToInteger(attrOption.Value)));
                    if (whereCommand != String.Empty)
                        whereCommand = String.Concat(whereCommand, Environment.NewLine, "AND ", "Category.CategoryKey=@CategoryKey");
                    else
                        whereCommand = "Category.CategoryKey=@CategoryKey";
                    return;
                }
            }
            throw new Exception(String.Concat(Properties.Resources.MSG_CategoryIsRequired));
        }

        private void SetDataTelephoneValidator(ref List<StoredProcInParam> sqlParams, ref string whereCommand, PersonTelephoneEntity[] telephones, string parentEntityName, ElementBL elementBL)
        {
            CommonEntities.ElementEntity _personTelephoneMetadata = this.GetElementByName(EntityNames.PersonTelephoneEntityName, elementBL);
            if (_personTelephoneMetadata == null) return;
            if ((telephones != null) && (telephones.Length > 0))
            {
                String whereFilterByTelephones = String.Empty;
                String[] telephoneNumbers = (from tlf in telephones
                                             where (tlf.Telephone != null)
                                             select tlf.Telephone.Telephone).ToArray();
                if ((telephoneNumbers != null)
                    && (telephoneNumbers.Length > 0))
                {
                    whereFilterByTelephones = String.Join(",", Array.ConvertAll(telephoneNumbers, new Converter<string, string>(m => String.Concat("'", m.ToString(), "'"))));
                }
                if (!String.IsNullOrEmpty(whereFilterByTelephones))
                {
                    if (whereCommand != String.Empty)
                        whereCommand = String.Format(String.Concat(whereCommand, Environment.NewLine, "AND ", "Telephone.Telephone IN ({0})"), whereFilterByTelephones);
                    else
                        whereCommand = String.Format("Telephone.Telephone IN ({0})", whereFilterByTelephones);
                    return;
                }
            }
            throw new Exception(String.Concat(Properties.Resources.MSG_TelephoneIsRequired));
        }

        private string GetEntityNameByAttributeTypeName(string typeName)
        {
            if (String.IsNullOrEmpty(typeName))
                throw new Exception(string.Format(Properties.Resources.ERROR_personValidationError));//Cambiar mensaje.

            int index = typeName.IndexOf(",");
            typeName = typeName.Remove(index);
            string[] cadenas = typeName.Split('.');
            return cadenas[cadenas.Length - 1].Trim('[', ']');
        }

        private StoredProcInParam SetData(out string whereConfigurationaux, PersonEntity person, CommonEntities.AttributeEntity attribute)
        {
            switch (attribute.AttributeType)
            {
                case SII.HCD.Common.Entities.AttributeTypeEnum.Double:
                    double doubleValue = (double)this.GetValueByAttributeName(person, attribute.Name);
                    if (doubleValue == 0.0)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, attribute.Name));
                    }
                    else
                    {
                        whereConfigurationaux = String.Concat("Person.", attribute.Name, "=@", doubleValue.ToString());
                        return new StoredProcInParam(attribute.Name, DbType.Double, doubleValue);
                    }
                case SII.HCD.Common.Entities.AttributeTypeEnum.Decimal:
                    decimal decimalValue = (decimal)this.GetValueByAttributeName(person, attribute.Name);
                    if (decimalValue == 0.0m)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, attribute.Name));
                    }
                    else
                    {
                        whereConfigurationaux = String.Concat("Person.", attribute.Name, "=@", decimalValue.ToString());
                        return new StoredProcInParam(attribute.Name, DbType.Decimal, decimalValue);
                    }
                case SII.HCD.Common.Entities.AttributeTypeEnum.Integer:
                    int intValue = (int)this.GetValueByAttributeName(person, attribute.Name);
                    if (intValue == 0)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, attribute.Name));
                    }
                    else
                    {
                        whereConfigurationaux = String.Concat("Person.", attribute.Name, "=@", intValue.ToString());
                        return new StoredProcInParam(attribute.Name, DbType.Int32, intValue);
                    }
                case SII.HCD.Common.Entities.AttributeTypeEnum.String:
                    String stringValue = this.GetValueByAttributeName(person, attribute.Name).ToString();
                    if (String.IsNullOrEmpty(stringValue))
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, attribute.Name));
                    }
                    else
                    {
                        whereConfigurationaux = String.Concat("Person.", attribute.Name, "=@", attribute.Name);
                        return new StoredProcInParam(attribute.Name, DbType.String, stringValue);
                    }
                case SII.HCD.Common.Entities.AttributeTypeEnum.DateTime:
                    DateTime dtValue = (DateTime)this.GetValueByAttributeName(person, attribute.Name);
                    if (dtValue == DateTime.MinValue)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, attribute.Name));
                    }
                    else
                    {
                        whereConfigurationaux = String.Concat("Person.", attribute.Name, "=@", dtValue.ToString());
                        return new StoredProcInParam(attribute.Name, DbType.DateTime, dtValue);
                    }
                case SII.HCD.Common.Entities.AttributeTypeEnum.Boolean:
                    bool boolValue = (bool)this.GetValueByAttributeName(person, attribute.Name);
                    if (boolValue)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, attribute.Name));
                    }
                    else
                    {
                        whereConfigurationaux = String.Concat("Person.", attribute.Name, "=@", boolValue.ToString());
                        return new StoredProcInParam(attribute.Name, DbType.Boolean, boolValue);
                    }
                case SII.HCD.Common.Entities.AttributeTypeEnum.Entity:
                    //?
                    whereConfigurationaux = string.Empty;
                    return null;
                case SII.HCD.Common.Entities.AttributeTypeEnum.Enum:
                    //?
                    whereConfigurationaux = string.Empty;
                    return null;
                default:
                    whereConfigurationaux = string.Empty;
                    return null;
            }
        }

        private object GetValueByAttributeName(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        /*
        private void SetPersonConfigurationValues(PersonEntity person, CategoryPersonKeyEnum firstConfigurationWithMoreRestriction, CustomerFindRequest personConfiguration, bool ignoreIdentifier)
        {
            if (personConfiguration.MandatoryFirstName)
                personConfiguration.FirstName = person.FirstName;
            if (personConfiguration.MandatoryLastName)
                personConfiguration.LastName = person.LastName;

            switch (firstConfigurationWithMoreRestriction)
            {
                case CategoryPersonKeyEnum.Customer:
                    this.SetCustomerAlternativeOptions(person, personConfiguration, ignoreIdentifier);
                    break;
                case CategoryPersonKeyEnum.NOK:
                    this.SetNOKAlternativeOptions(person, personConfiguration, ignoreIdentifier);
                    break;
                case CategoryPersonKeyEnum.CustContactPerson:
                    this.SetCustomerContactPersonAlternativeOptions(person, personConfiguration, ignoreIdentifier);
                    break;
                case CategoryPersonKeyEnum.HHRR:
                    this.SetHHRRAlternativeOptions(person, personConfiguration, ignoreIdentifier);
                    break;
                case CategoryPersonKeyEnum.OrgContactPerson:
                    this.SetOrganizationContactPersonAlternativeOptions(person, personConfiguration, ignoreIdentifier);
                    break;
                case CategoryPersonKeyEnum.Physician:
                    this.SetPhysicianAlternativeOptions(person, personConfiguration, ignoreIdentifier);
                    break;
                case CategoryPersonKeyEnum.PotentialCustomer:
                case CategoryPersonKeyEnum.LegalPerson:
                case CategoryPersonKeyEnum.ReferringPhysician:
                case CategoryPersonKeyEnum.Nurse:
                case CategoryPersonKeyEnum.ReferringNurse:
                case CategoryPersonKeyEnum.None:
                    break;
                default:
                    break;
            }
        }

        private void SetPhysicianAlternativeOptions(PersonEntity person, CustomerFindRequest personConfiguration, bool ignoreIdentifier)
        {
            BackofficeConfigurationSection backofficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            if (backofficeConfig.EntitySettings.PhysicianEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backofficeConfig.EntitySettings.PhysicianEntity.Attributes)
                {
                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(ignoreIdentifier))
                    {
                        personConfiguration.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        personConfiguration.MandatoryIdentifierType = true;
                        personConfiguration.IdentifierIDNumber = this.GetIDNumber(person.Identifiers, attrib.DefaultValue);
                    }
                }
            }
            if (String.IsNullOrEmpty(personConfiguration.IdentifierIDNumber) && (personConfiguration.MandatoryIdentifierType))
            {
                if ((backofficeConfig.EntitySettings.PhysicianEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
                    (backofficeConfig.EntitySettings.PhysicianEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
                {
                    Boolean alternativeFound = false;
                    String alternatives = personConfiguration.MandatoryIdentifierTypeDefaultValue;
                    foreach (EntityAttributeOptionElement alternative in backofficeConfig.EntitySettings.PhysicianEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
                    {
                        string idNumber = GetIDNumber(person.Identifiers, alternative.Value);
                        if (!String.IsNullOrEmpty(idNumber))
                        {
                            alternativeFound = true;
                            personConfiguration.IdentifierIDNumber = idNumber;
                            personConfiguration.MandatoryIdentifierTypeDefaultValue = alternative.Value;
                            break;
                        }
                        else
                        {
                            alternatives = String.Concat(alternatives, " ", Properties.Resources.MSG_or, " ", alternative.Value);
                        }
                    }
                    if (!alternativeFound)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_PhysicianIdentifierRequired, alternatives));
                    }
                }
                else
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_PhysicianIdentifierRequired, personConfiguration.MandatoryIdentifierTypeDefaultValue));
                }
            }
        }

        private void SetOrganizationContactPersonAlternativeOptions(PersonEntity person, CustomerFindRequest personConfiguration, bool ignoreIdentifier)
        {
            //DO SALVA: Lo comento porque esta entidad no está metida en configuración. Dejo el método construido por si decidimos introducirla en configuración.

            //BackofficeConfigurationSection backofficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            //if (backofficeConfig.EntitySettings.OrganizationContactPerson.Attributes != null)
            //{
            //    foreach (EntityAttributeElement attrib in backofficeConfig.EntitySettings.OrganizationContactPerson.Attributes)
            //    {
            //        if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory)  && !(ignoreIdentifier))
            //        {
            //            personConfiguration.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
            //            personConfiguration.MandatoryIdentifierType = true;
            //            personConfiguration.IdentifierIDNumber = this.GetIDNumber(person.Identifiers, attrib.DefaultValue);
            //        }
            //    }
            //}
            //if (String.IsNullOrEmpty(personConfiguration.IdentifierIDNumber) && (personConfiguration.MandatoryIdentifierType))
            //{
            //    if ((backofficeConfig.EntitySettings.OrganizationContactPerson.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
            //        (backofficeConfig.EntitySettings.OrganizationContactPerson.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
            //    {
            //        Boolean alternativeFound = false;
            //        String alternatives = personConfiguration.MandatoryIdentifierTypeDefaultValue;
            //        foreach (EntityAttributeOptionElement alternative in backofficeConfig.EntitySettings.OrganizationContactPerson.Attributes["Identifier.IdentifierType"].AlternativeOptions)
            //        {
            //            string idNumber = GetIDNumber(person.Identifiers, alternative.Value);
            //            if (!String.IsNullOrEmpty(idNumber))
            //            {
            //                alternativeFound = true;
            //                personConfiguration.IdentifierIDNumber = idNumber;
            //                personConfiguration.MandatoryIdentifierTypeDefaultValue = alternative.Value;
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
            //    }
            //    else
            //    {
            //        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, personConfiguration.MandatoryIdentifierTypeDefaultValue));
            //    }
            //}
        }

        private void SetHHRRAlternativeOptions(PersonEntity person, CustomerFindRequest personConfiguration, bool ignoreIdentifier)
        {
            BackofficeConfigurationSection backofficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            if (backofficeConfig.EntitySettings.HumanResourceEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in backofficeConfig.EntitySettings.HumanResourceEntity.Attributes)
                {
                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(ignoreIdentifier))
                    {
                        personConfiguration.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        personConfiguration.MandatoryIdentifierType = true;
                        personConfiguration.IdentifierIDNumber = this.GetIDNumber(person.Identifiers, attrib.DefaultValue);
                    }
                }
            }
            if (String.IsNullOrEmpty(personConfiguration.IdentifierIDNumber) && (personConfiguration.MandatoryIdentifierType))
            {
                if ((backofficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
                    (backofficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
                {
                    Boolean alternativeFound = false;
                    String alternatives = personConfiguration.MandatoryIdentifierTypeDefaultValue;
                    foreach (EntityAttributeOptionElement alternative in backofficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
                    {
                        string idNumber = GetIDNumber(person.Identifiers, alternative.Value);
                        if (!String.IsNullOrEmpty(idNumber))
                        {
                            alternativeFound = true;
                            personConfiguration.IdentifierIDNumber = idNumber;
                            personConfiguration.MandatoryIdentifierTypeDefaultValue = alternative.Value;
                            break;
                        }
                        else
                        {
                            alternatives = String.Concat(alternatives, " ", Properties.Resources.MSG_or, " ", alternative.Value);
                        }
                    }
                    if (!alternativeFound)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_HumanResourceIdentifierRequired, alternatives));
                    }
                }
                else
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_HumanResourceIdentifierRequired, personConfiguration.MandatoryIdentifierTypeDefaultValue));
                }
            }
        }

        private void SetCustomerContactPersonAlternativeOptions(PersonEntity person, CustomerFindRequest personConfiguration, bool ignoreIdentifier)
        {
            AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
            if (administrativeConfig.EntitySettings.CustomerContactEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.CustomerContactEntity.Attributes)
                {
                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(ignoreIdentifier))
                    {
                        personConfiguration.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        personConfiguration.MandatoryIdentifierType = true;
                        personConfiguration.IdentifierIDNumber = this.GetIDNumber(person.Identifiers, attrib.DefaultValue);
                    }
                }
            }
            if (String.IsNullOrEmpty(personConfiguration.IdentifierIDNumber) && (personConfiguration.MandatoryIdentifierType))
            {
                if ((administrativeConfig.EntitySettings.CustomerContactEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
                    (administrativeConfig.EntitySettings.CustomerContactEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
                {
                    Boolean alternativeFound = false;
                    String alternatives = personConfiguration.MandatoryIdentifierTypeDefaultValue;
                    foreach (EntityAttributeOptionElement alternative in administrativeConfig.EntitySettings.CustomerContactEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
                    {
                        string idNumber = GetIDNumber(person.Identifiers, alternative.Value);
                        if (!String.IsNullOrEmpty(idNumber))
                        {
                            alternativeFound = true;
                            personConfiguration.IdentifierIDNumber = idNumber;
                            personConfiguration.MandatoryIdentifierTypeDefaultValue = alternative.Value;
                            break;
                        }
                        else
                        {
                            alternatives = String.Concat(alternatives, " ", Properties.Resources.MSG_or, " ", alternative.Value);
                        }
                    }
                    if (!alternativeFound)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, alternatives));
                    }
                }
                else
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, personConfiguration.MandatoryIdentifierTypeDefaultValue));
                }
            }
        }

        private void SetNOKAlternativeOptions(PersonEntity person, CustomerFindRequest personConfiguration, bool ignoreIdentifier)
        {
            //DO SALVA: Lo comento porque esta entidad no está metida en configuración. Dejo el método construido por si decidimos introducirla en configuración.

            //AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
            //if (administrativeConfig.EntitySettings.NOKEntity.Attributes != null)
            //{
            //    foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.NOKEntity.Attributes)
            //    {
            //        if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory)  && !(ignoreIdentifier))
            //        {
            //            personConfiguration.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
            //            personConfiguration.MandatoryIdentifierType = true;
            //            personConfiguration.IdentifierIDNumber = this.GetIDNumber(person.Identifiers, attrib.DefaultValue);
            //        }
            //    }
            //}
            //if (String.IsNullOrEmpty(personConfiguration.IdentifierIDNumber) && (personConfiguration.MandatoryIdentifierType))
            //{
            //    if ((administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
            //        (administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
            //    {
            //        Boolean alternativeFound = false;
            //        String alternatives = personConfiguration.MandatoryIdentifierTypeDefaultValue;
            //        foreach (EntityAttributeOptionElement alternative in administrativeConfig.EntitySettings.NOKEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
            //        {
            //            string idNumber = GetIDNumber(person.Identifiers, alternative.Value);
            //            if (!String.IsNullOrEmpty(idNumber))
            //            {
            //                alternativeFound = true;
            //                personConfiguration.IdentifierIDNumber = idNumber;
            //                personConfiguration.MandatoryIdentifierTypeDefaultValue = alternative.Value;
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
            //    }
            //    else
            //    {
            //        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, personConfiguration.MandatoryIdentifierTypeDefaultValue));
            //    }
            //}
        }

        private void SetCustomerAlternativeOptions(PersonEntity person, CustomerFindRequest personConfiguration, bool ignoreIdentifier)
        {
            AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
            if (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null)
            {
                foreach (EntityAttributeElement attrib in administrativeConfig.EntitySettings.CustomerEntity.Attributes)
                {
                    if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(ignoreIdentifier))
                    {
                        personConfiguration.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
                        personConfiguration.MandatoryIdentifierType = true;
                        personConfiguration.IdentifierIDNumber = this.GetIDNumber(person.Identifiers, attrib.DefaultValue);
                    }
                }
            }
            if (String.IsNullOrEmpty(personConfiguration.IdentifierIDNumber) && (personConfiguration.MandatoryIdentifierType))
            {
                if ((administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
                    (administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
                {
                    Boolean alternativeFound = false;
                    String alternatives = personConfiguration.MandatoryIdentifierTypeDefaultValue;
                    foreach (EntityAttributeOptionElement alternative in administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
                    {
                        string idNumber = GetIDNumber(person.Identifiers, alternative.Value);
                        if (!String.IsNullOrEmpty(idNumber))
                        {
                            alternativeFound = true;
                            personConfiguration.IdentifierIDNumber = idNumber;
                            personConfiguration.MandatoryIdentifierTypeDefaultValue = alternative.Value;
                            break;
                        }
                        else
                        {
                            alternatives = String.Concat(alternatives, " ", Properties.Resources.MSG_or, " ", alternative.Value);
                        }
                    }
                    if (!alternativeFound)
                    {
                        throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, alternatives));
                    }
                }
                else
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_CustomerIdentifierRequired, personConfiguration.MandatoryIdentifierTypeDefaultValue));
                }
            }
        }

        private string GetIDNumber(PersonIdentifierEntity[] identifiers, string identifierTypeName)
        {
            String IDNumber = String.Empty;
            if (identifiers != null)
            {
                foreach (PersonIdentifierEntity identifier in identifiers)
                {
                    if ((identifier.IdentifierType != null)
                        && (identifier.IdentifierType.Name == identifierTypeName)
                        && (identifier.EditStatus.Value != StatusEntityValue.Deleted)
                        && (identifier.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        IDNumber = identifier.IDNumber;
                    }
                }
            }
            return IDNumber;
        }
        */
        private void ComparePerson(PersonEntity person, int id)
        {
            PersonEntity oldPerson = this.GetPerson(id);
            if (oldPerson != null)
            {
                //Si tiene identificador entrante
                if ((person.Identifiers != null) && (oldPerson.Identifiers != null))
                {
                    foreach (PersonIdentifierEntity identifier in person.Identifiers)
                    {
                        if ((identifier.IdentifierType != null)
                            && ((identifier.EditStatus.Value == StatusEntityValue.None) || (identifier.EditStatus.Value == StatusEntityValue.New) || (identifier.EditStatus.Value == StatusEntityValue.Updated)))
                        {
                            bool existIdentifier = (from ident in oldPerson.Identifiers
                                                    where (ident.IdentifierType.Name == identifier.IdentifierType.Name)
                                                        && (ident.IDNumber == identifier.IDNumber)
                                                    select ident).Count() > 0;
                            if (existIdentifier)//Meter en recursos.
                                throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(person.FirstName, " ", person.LastName, "with Identifier:", identifier.IDNumber)));
                        }
                    }
                }
                //Si tiene categoría entrante
                bool sameCategories = false;
                if ((person.Categories != null) && (oldPerson.Categories != null))
                {
                    int countEqualCategories = 0;
                    foreach (PersonCategoryEntity category in person.Categories)
                    {
                        if ((category.Category != null)
                            && ((category.EditStatus.Value == StatusEntityValue.None) || (category.EditStatus.Value == StatusEntityValue.New) || (category.EditStatus.Value == StatusEntityValue.Updated)))
                        {
                            bool existCategory = (from ident in oldPerson.Categories
                                                  where (ident.Category != null)
                                                      && (ident.Category.ID == category.Category.ID)
                                                  select ident).Count() > 0;
                            if (existCategory)
                                countEqualCategories++;
                        }
                    }
                    sameCategories = (countEqualCategories == person.Categories.Length);
                }
                //Si tiene teléfono entrante
                bool sameTelephones = false;
                if ((person.Telephones != null) && (oldPerson.Telephones != null))
                {
                    int countEqualTelephones = 0;
                    foreach (PersonTelephoneEntity telephone in person.Telephones)
                    {
                        if ((telephone.Telephone != null)
                            && ((telephone.EditStatus.Value == StatusEntityValue.None) || (telephone.EditStatus.Value == StatusEntityValue.New) || (telephone.EditStatus.Value == StatusEntityValue.Updated)))
                        {
                            bool existTelephone = (from tlf in oldPerson.Telephones
                                                   where (tlf.Telephone != null)
                                                     && (tlf.Telephone.TelephoneType == telephone.Telephone.TelephoneType)
                                                     && (tlf.Telephone.Telephone == telephone.Telephone.Telephone)
                                                   select tlf).Count() > 0;
                            if (existTelephone)
                                countEqualTelephones++;
                        }
                    }
                    sameTelephones = (countEqualTelephones == person.Telephones.Length);
                }

                //Aqui habrá que ver si tiene que coincidir todo o no, y que mensaje mostrar.
                if ((sameTelephones) && (sameCategories))
                    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(person.FirstName, " ", person.LastName)));
            }
        }
        #endregion
        #endregion

        #region Public methods
        public PersonEntity Save(PersonEntity person, bool forceSave, out PersonAddressListDTO[] homonymPersons)
        {
            try
            {
                if (person == null) throw new ArgumentNullException("person");

                ElementBL _elementBL = new ElementBL();
                homonymPersons = null;

                switch (person.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return person;
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(person, 0, CategoryPersonKeyEnum.None, forceSave, false, true, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return person;
                        return Insert(person);
                    case StatusEntityValue.NewAndDeleted:
                        return person;
                    case StatusEntityValue.None:
                        CheckUpdatePreconditions(person, 0, CategoryPersonKeyEnum.None, forceSave, false, true, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return person;
                        return person;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(person, 0, CategoryPersonKeyEnum.None, forceSave, false, true, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return person;
                        return Update(person);
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

        //public void SetPersonAsDuplicate(int sourcePersonID, int targetPersonID)
        //{
        //    try
        //    {
        //        string userName = IdentityUser.GetIdentityUserName();

        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            _personDA.UpdateSecondDuplicate(sourcePersonID, true, targetPersonID, userName);
        //            _personDA.UpdateSecondDuplicate(targetPersonID, true, sourcePersonID, userName);

        //            scope.Complete();
        //        }

        //        LOPDLogger.Write(EntityNames.PersonEntityName, sourcePersonID, ActionType.Modify);
        //        LOPDLogger.Write(EntityNames.PersonEntityName, targetPersonID, ActionType.Modify);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //    }
        //}

        public void SetPersonsAsDuplicate(int[] personIDs)
        {
            try
            {
                string userName = IdentityUser.GetIdentityUserName();
                DataSet ds = _personDA.GetPersonDuplicateGroups(personIDs);
                if ((ds != null) && (ds.Tables.Contains(TableNames.PersonTable)) && (ds.Tables[TableNames.PersonTable].Rows.Count > 0))
                {
                    var ids = ds.Tables[TableNames.PersonTable].AsEnumerable()
                                   .Select(r => new { ID = r.Field<int>("ID"), DuplicateGroupID = r.Field<int>("DuplicateGroupID") });

                    //Si todos ya estan en un grupo, no hacer nada.
                    if (ids.All(r => r.DuplicateGroupID > 0))
                    {
                        return;
                    }

                    //Si hay varios grupos distintos, no hacer nada
                    if (ids.Where(r => r.DuplicateGroupID > 0).Select(r => r.DuplicateGroupID).Distinct().Count() > 1)
                    {
                        return;
                    }

                    //Si hay algun grupo, tomar el primero
                    int[] nonGroupPersons = ids.Where(r => r.DuplicateGroupID == 0).Select(r => r.ID).ToArray();
                    int duplicateGroupID = ids.Where(r => r.DuplicateGroupID > 0).Select(r => r.DuplicateGroupID).FirstOrDefault();
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (duplicateGroupID <= 0)
                        {
                            duplicateGroupID = _duplicateGroupDA.Insert(0, userName);
                        }
                        _personDA.UpdatePersonDuplicateGroups(nonGroupPersons, duplicateGroupID, userName);

                        scope.Complete();
                    }

                    if (nonGroupPersons != null)
                    {
                        foreach (int personID in nonGroupPersons)
                        {
                            LOPDLogger.Write(EntityNames.PersonEntityName, personID, ActionType.Modify);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public void RemovePersonsAsDuplicate(int[] personIDs)
        {
            try
            {
                string userName = IdentityUser.GetIdentityUserName();
                DataSet ds = _personDA.GetPersonDuplicateGroups(personIDs);
                if ((ds != null) && (ds.Tables.Contains(TableNames.PersonTable)) && (ds.Tables[TableNames.PersonTable].Rows.Count > 0))
                {
                    var ids = ds.Tables[TableNames.PersonTable].AsEnumerable()
                                .Select(r => new { ID = r.Field<int>("ID"), DuplicateGroupID = r.Field<int>("DuplicateGroupID") })
                                .ToList();

                    //Buscar las personas de todos los grupos > 0
                    var personsInGroups = Enumerable.Empty<object>()
                                                    .Select(o => new { ID = 0, DuplicateGroupID = 0 })
                                                    .ToList();
                    personsInGroups.Clear();

                    int[] distinctGroupIDs = ids.Where(gr => gr.DuplicateGroupID > 0).Select(gr => gr.DuplicateGroupID).Distinct().ToArray();
                    if ((distinctGroupIDs != null) && (distinctGroupIDs.Length > 0))
                    {
                        DataSet ds2 = _personDA.GetPersonsInDuplicateGroups(distinctGroupIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(TableNames.PersonTable)) && (ds2.Tables[TableNames.PersonTable].Rows.Count > 0))
                        {
                            personsInGroups = ds2.Tables[TableNames.PersonTable].AsEnumerable()
                                                 .Select(r => new { ID = r.Field<int>("ID"), DuplicateGroupID = r.Field<int>("DuplicateGroupID") })
                                                 .ToList();
                        }
                    }

                    //Para cada persona seleccionada, que está en un grupo, quitarla del grupo y comprobar si es necesario borrar el grupo
                    List<int> personsToRemove = new List<int>();
                    List<int> groupsToRemove = new List<int>();
                    foreach (var person in ids)
                    {
                        if (person.DuplicateGroupID > 0)
                        {
                            var myPerson = personsInGroups.Find(per => per.ID == person.ID);
                            if (myPerson != null)
                            {
                                personsToRemove.Add(person.ID);
                                personsInGroups.Remove(myPerson);
                                int personsLeftInGroup = personsInGroups.Where(per => per.DuplicateGroupID == person.DuplicateGroupID).Count();
                                if (personsLeftInGroup <= 1)
                                {
                                    if (personsLeftInGroup == 1)
                                    {
                                        myPerson = personsInGroups.Find(per => per.DuplicateGroupID == person.DuplicateGroupID);
                                        personsToRemove.Add(myPerson.ID);
                                        personsInGroups.Remove(myPerson);
                                    }
                                    groupsToRemove.Add(person.DuplicateGroupID);
                                }
                            }
                        }
                    }

                    using (TransactionScope scope = new TransactionScope())
                    {
                        _personDA.UpdatePersonDuplicateGroups(personsToRemove.ToArray(), 0, userName);
                        _duplicateGroupDA.Delete(groupsToRemove.ToArray());

                        scope.Complete();
                    }

                    if (personsToRemove != null)
                    {
                        foreach (int personID in personsToRemove)
                        {
                            LOPDLogger.Write(EntityNames.PersonEntityName, personID, ActionType.Modify);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public PersonAddressListDTO[] GetPersons(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber,
            PersonFindTypeEnum findType, int customerID, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();
                DataSet ds = null;
                switch (findType)
                {
                    case PersonFindTypeEnum.Person:
                        ds = _personDA.GetPersonsAnyCategory(firstName, lastName, lastName2, identifierTypeID, idNumber, maxRows);
                        break;
                    case PersonFindTypeEnum.Customer:
                        break;
                    case PersonFindTypeEnum.HHRR:
                        break;
                    case PersonFindTypeEnum.Physician:
                        break;
                    //case PersonFindTypeEnum.RequestingPhysician:
                    //    break;
                    case PersonFindTypeEnum.NOK:
                        ds = _personDA.GetPersonsExcludeNOK(firstName, lastName, identifierTypeID, idNumber, 0, maxRows, customerID);
                        break;
                    case PersonFindTypeEnum.ContactPerson:
                        if (customerID > 0)
                        {
                            ds = _personDA.GetPersonsExcludeCCP(firstName, lastName, identifierTypeID, idNumber, 0, maxRows, customerID);
                        }
                        else
                        {
                            ds = _personDA.GetPersonsExcludeOrg(firstName, lastName, identifierTypeID, idNumber, 0, maxRows, 0);
                        }
                        break;
                    default:
                        break;
                }

                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAddressListDTOTable)))
                {
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);

                        #region Person Categories
                        if (persons.Length > 0)
                        {
                            DataSet dsPersonCatRels = _categoryDA.GetListCategoryWithPersonCatRelByPersonIDs((from per in persons select per.ID).ToArray());
                            if ((dsPersonCatRels != null)
                                && (dsPersonCatRels.Tables != null)
                                && (dsPersonCatRels.Tables.Contains(BackOffice.Entities.TableNames.CategoryTable))
                                && (dsPersonCatRels.Tables[BackOffice.Entities.TableNames.CategoryTable].Rows.Count > 0))
                            {
                                foreach (PersonAddressListDTO person in persons)
                                {
                                    String categories = String.Empty;
                                    foreach (DataRow row in dsPersonCatRels.Tables[BackOffice.Entities.TableNames.CategoryTable].Rows)
                                    {
                                        int personID = SIIConvert.ToInteger(row["PersonID"].ToString(), 0);
                                        if (person.ID == personID)
                                        {
                                            if (String.IsNullOrEmpty(categories))
                                                categories = String.Concat(categories, row["Name"].ToString());
                                            else
                                                categories = String.Concat(categories, ",", Environment.NewLine, row["Name"].ToString());
                                        }
                                    }
                                    person.Categories = categories;
                                }
                            }
                        }
                        #endregion
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public SensitiveDataEntity GetSensitiveData(int personID)
        {
            try
            {
                DataSet ds = _sensitiveDataDA.GetSensitiveDataFromPerson(personID);
                if ((ds.Tables != null) && (ds.Tables.Contains(BackOffice.Entities.TableNames.SensitiveDataTable))
                    && (ds.Tables[BackOffice.Entities.TableNames.SensitiveDataTable].Rows.Count > 0))
                {
                    SensitiveDataAdvancedAdapter sensitiveDataAdapter = new SensitiveDataAdvancedAdapter();
                    return sensitiveDataAdapter.GetFirstOrDefault((row => (row["PersonID"] as int? ?? 0) == personID), ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonListDTO[] GetPersons(DateTime fromDate, DateTime toDate, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonListDTOAdapter personListDTOAdapter = new PersonListDTOAdapter();

                DataSet ds = _personDA.GetPersons(fromDate, toDate, maxRows);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonListDTOTable)))
                {
                    PersonListDTO[] persons = personListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonAddressListDTO[] GetPersons(int processChartID, int careCenterID, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();
                DataSet ds = _personDA.GetPersons(processChartID, careCenterID, maxRows);
                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAddressListDTOTable)))
                {
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonAddressListDTO[] GetPersons(int processChartID, int careCenterID, CommonEntities.StatusEnum status, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();
                DataSet ds = _personDA.GetPersons(processChartID, careCenterID, (int)status, maxRows);
                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAddressListDTOTable)))
                {
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonAddressListDTO[] GetPersons(int processChartID, int careCenterID, CommonEntities.StatusEnum customerProcessStatus, CommonEntities.StatusEnum customerEpisodeStatus, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();
                DataSet ds = _personDA.GetPersonsByStatus(processChartID, careCenterID, (int)customerProcessStatus, (int)customerEpisodeStatus, maxRows);
                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAddressListDTOTable)))
                {
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonAddressListDTO[] GetPersons(int processChartID, int careCenterID, CommonEntities.StatusEnum status, BasicProcessStepsEnum step, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();
                DataSet ds = _personDA.GetPersons(processChartID, careCenterID, (int)status, (long)step, maxRows);
                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAddressListDTOTable)))
                {
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonAddressListDTO[] GetPersons(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID,
            out Boolean maxRecordsExceded)
        {
            return this.GetPersons(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, out maxRecordsExceded, PersonFindTypeEnum.Person, 0, 0);
        }

        public PersonAddressListDTO[] GetPersons(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID,
            out Boolean maxRecordsExceded, PersonFindTypeEnum findType, int customerID, int organizationID)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();

                DataSet ds = null;
                CategoryAdapter myCategoryAdapter = new CategoryAdapter();
                CategoryPersonKeyEnum myCategoryKeyEnum = CategoryPersonKeyEnum.None;
                ds = _categoryDA.GetCategoryByID(categoryID);
                if ((ds != null) && (ds.Tables != null)
                    && ds.Tables.Contains(BackOffice.Entities.TableNames.CategoryTable) && (ds.Tables[BackOffice.Entities.TableNames.CategoryTable].Rows.Count > 0))
                {
                    CategoryEntity myCategory = myCategoryAdapter.GetInfo(ds.Tables[BackOffice.Entities.TableNames.CategoryTable].Rows[0], ds);
                    myCategoryKeyEnum = (CategoryPersonKeyEnum)myCategory.CategoryKey;
                }

                switch (findType)
                {
                    case PersonFindTypeEnum.Person:
                        if (myCategoryKeyEnum == CategoryPersonKeyEnum.Customer)
                            ds = _personDA.GetPersonsCustomerCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                        else
                            if (myCategoryKeyEnum == CategoryPersonKeyEnum.HHRR)
                                ds = _personDA.GetPersonsHHRRCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                            else
                                ds = _personDA.GetPersons(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows);
                        break;
                    case PersonFindTypeEnum.Customer:
                        if (myCategoryKeyEnum == CategoryPersonKeyEnum.Customer)
                            ds = _personDA.GetPersonsExcludeCustomerCustomerCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                        else
                            if (myCategoryKeyEnum == CategoryPersonKeyEnum.HHRR)
                                ds = _personDA.GetPersonsExcludeCustomerHHRRCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                            else
                                ds = _personDA.GetPersonsExcludeCustomer(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows);
                        break;
                    case PersonFindTypeEnum.HHRR:
                        if (myCategoryKeyEnum == CategoryPersonKeyEnum.Customer)
                            ds = _personDA.GetPersonsExcludeHHRRCustomerCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                        else
                            if (myCategoryKeyEnum == CategoryPersonKeyEnum.HHRR)
                                ds = _personDA.GetPersonsExcludeHHRRHHRRCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                            else
                                ds = _personDA.GetPersonsExcludeHHRR(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows);
                        break;
                    case PersonFindTypeEnum.NOK:
                        ds = _personDA.GetPersonsExcludeNOK(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows, customerID);
                        break;
                    case PersonFindTypeEnum.ContactPerson:
                        if (customerID > 0)
                            ds = _personDA.GetPersonsExcludeCCP(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows, customerID);
                        else
                            ds = _personDA.GetPersonsExcludeOrg(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows, organizationID);
                        break;
                    case PersonFindTypeEnum.Physician:
                        if (myCategoryKeyEnum == CategoryPersonKeyEnum.Customer)
                            ds = _personDA.GetPersonsExcludePhysicianCustomerCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                        else
                            if (myCategoryKeyEnum == CategoryPersonKeyEnum.HHRR)
                                ds = _personDA.GetPersonsExcludePhysicianHHRRCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                            else
                                ds = _personDA.GetPersonsExcludePhysician(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows);
                        break;
                    //case PersonFindTypeEnum.RequestingPhysician:

                    //    break;
                    default:
                        if (myCategoryKeyEnum == CategoryPersonKeyEnum.Customer)
                            ds = _personDA.GetPersonsCustomerCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                        else
                            if (myCategoryKeyEnum == CategoryPersonKeyEnum.HHRR)
                                ds = _personDA.GetPersonsHHRRCat(firstName, lastName, identifierTypeID, idNumber, categoryID, profileID, maxRows);
                            else
                                ds = _personDA.GetPersons(firstName, lastName, identifierTypeID, idNumber, categoryID, maxRows);
                        break;
                }

                if ((ds != null) && (ds.Tables != null) && ds.Tables.Contains(BackOffice.Entities.TableNames.PersonAddressListDTOTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.PersonAddressListDTOTable].Rows.Count > 0))
                {
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);

                        if (persons.Length > 0)
                        {
                            DataSet dsPersonCatRels = _categoryDA.GetListCategoryWithPersonCatRelByPersonIDs((from per in persons select per.ID).ToArray());
                            if ((dsPersonCatRels != null) && (dsPersonCatRels.Tables != null)
                                && dsPersonCatRels.Tables.Contains(BackOffice.Entities.TableNames.CategoryTable)
                                && (dsPersonCatRels.Tables[BackOffice.Entities.TableNames.CategoryTable].Rows.Count > 0))
                            {
                                foreach (PersonAddressListDTO person in persons)
                                {
                                    String categories = String.Empty;
                                    foreach (DataRow row in dsPersonCatRels.Tables[BackOffice.Entities.TableNames.CategoryTable].Rows)
                                    {
                                        int personID = SIIConvert.ToInteger(row["PersonID"].ToString(), 0);
                                        if (person.ID == personID)
                                        {
                                            if (String.IsNullOrEmpty(categories))
                                                categories = String.Concat(categories, row["Name"].ToString());
                                            else
                                                categories = String.Concat(categories, ",", Environment.NewLine, row["Name"].ToString());
                                        }
                                    }
                                    person.Categories = categories;
                                }
                            }
                        }
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonAddressListDTO[] GetPersons(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber, int categoryID, int profileID,
            int processChartID, int careCenterID, CommonEntities.StatusEnum status, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();

                DataSet ds = null;
                CategoryAdapter myCategoryAdapter = new CategoryAdapter();
                CategoryPersonKeyEnum myCategoryKeyEnum = CategoryPersonKeyEnum.None;
                ds = _categoryDA.GetCategoryByID(categoryID);
                if ((ds != null) && (ds.Tables != null) && ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CategoryTable) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.CategoryTable].Rows.Count > 0))
                {
                    CategoryEntity myCategory = myCategoryAdapter.GetInfo(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.CategoryTable].Rows[0], ds);
                    myCategoryKeyEnum = (CategoryPersonKeyEnum)myCategory.CategoryKey;
                }

                if (myCategoryKeyEnum == CategoryPersonKeyEnum.Customer)
                {
                    ds = _personDA.GetPersonsCustomerNotInProcessChart(firstName, lastName, lastName2, identifierTypeID, idNumber, categoryID, profileID, processChartID, careCenterID, status, maxRows);
                }
                else if (myCategoryKeyEnum == CategoryPersonKeyEnum.HHRR)
                {
                    ds = _personDA.GetPersonsHHRRNotInProcessChart(firstName, lastName, lastName2, identifierTypeID, idNumber, categoryID, profileID, processChartID, careCenterID, status, maxRows);
                }
                else
                {
                    ds = _personDA.GetPersonsNotInProcessChart(firstName, lastName, lastName2, identifierTypeID, idNumber, categoryID, processChartID, careCenterID, status, maxRows);
                }

                if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAddressListDTOTable)))
                {
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonAddressListDTO[] GetPersons(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber, int categoryID, int profileID,
            CommonEntities.StatusEnum status, out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                DataSet ds = _personDA.GetPersonAddressListDTOsByFilters(firstName, lastName, lastName2, identifierTypeID, idNumber, categoryID, profileID, status, maxRows);
                if ((ds != null) && ds.Tables.Contains(BackOffice.Entities.TableNames.PersonAddressListDTOTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.PersonAddressListDTOTable].Rows.Count > 0))
                {

                    PersonAddressListDTOAdapter personAddressListDTOAdapter = new PersonAddressListDTOAdapter();
                    PersonAddressListDTO[] persons = personAddressListDTOAdapter.GetData(ds);
                    if (persons != null)
                    {
                        maxRecordsExceded = (persons.Length >= maxRows);
                    }
                    return persons;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonLookupDTO[] GetPersons(PersonSpecification specification, out Boolean maxRecordsExceded, out CommonEntities.GenericErrorLogEntity[] errors)
        {
            return GetPersons(specification, 0, out maxRecordsExceded, out errors);
        }

        public PersonLookupDTO[] GetPersons(PersonSpecification specification, int chNumberCareCenterID, out Boolean maxRecordsExceded,
            out CommonEntities.GenericErrorLogEntity[] errors)
        {
            maxRecordsExceded = false;
            errors = null;

            if (specification == null)
                return null;

            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.PersonEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                CommonEntities.AddInTokenBaseEntity[] phoneticAddinNames = null;
                DataSet ds = null;
                PersonLookupDTO[] allPersons = null;
                PersonLookupDTO[] persons = null;
                bool showCHNumberInCareCenter = false;

                if (!specification.OnlyAddinLookup)
                {
                    if (specification.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByFullName | PersonSearchOptionEnum.PhoneticLookupByNameParts))
                    {
                        phoneticAddinNames = GetAvailablePhoneticAddins();
                        if ((phoneticAddinNames == null) || (phoneticAddinNames.Length == 0))
                            throw new Exception(Properties.Resources.MSG_NoPhoneticAddins);

                        PersonSpecification mySpecification = specification.Clone() as PersonSpecification;
                        foreach (CommonEntities.AddInTokenBaseEntity phoneticAddinName in phoneticAddinNames)
                        {
                            PhoneticTranslatorHostView host = AddInRepository.GetAddIn<PhoneticTranslatorHostView>(phoneticAddinName.AddinName);
                            if (host != null)
                            {
                                if (mySpecification.IsFilteredByAny(PersonSearchOptionEnum.FirstName))
                                    mySpecification.FirstName = host.Translate(mySpecification.FirstName);
                                if (mySpecification.IsFilteredByAny(PersonSearchOptionEnum.LastName))
                                    mySpecification.LastName = host.Translate(mySpecification.LastName);
                                if (mySpecification.IsFilteredByAny(PersonSearchOptionEnum.LastName2))
                                    mySpecification.LastName2 = host.Translate(mySpecification.LastName2);
                                if (mySpecification.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByFullName))
                                    mySpecification.PhoneticLookupFullName = host.Translate(mySpecification.PhoneticLookupFullName);
                            }

                            showCHNumberInCareCenter = CheckCHNumberSpecification(mySpecification, chNumberCareCenterID);
                            if (!showCHNumberInCareCenter)
                                chNumberCareCenterID = 0;

                            ds = _personDA.GetPersons(mySpecification, chNumberCareCenterID, maxRows, phoneticAddinName.AddinName);
                            if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonLookupDTOTable)))
                            {
                                PersonLookupDTOAdvancedAdapter personLookupDTOAdvancedAdapter = new PersonLookupDTOAdvancedAdapter();
                                persons = personLookupDTOAdvancedAdapter.GetData(ds);
                                allPersons = MergePhoneticPersons(persons, allPersons, maxRows, phoneticAddinName, mySpecification);

                                if (allPersons != null)
                                {
                                    maxRecordsExceded = (allPersons.Length >= maxRows);
                                    if (mySpecification.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter) || mySpecification.IsFilteredByAny(PersonSearchOptionEnum.CHNumber))
                                        allPersons = FindCustomerProcessInfoByCHNumber(maxRows, allPersons, personLookupDTOAdvancedAdapter, mySpecification, chNumberCareCenterID);
                                    else
                                        allPersons = AddCustomerProcessInfo(allPersons, mySpecification, maxRows, phoneticAddinName.AddinName);

                                    allPersons = AddCustomerCategoriesInfo(allPersons, specification, maxRows, phoneticAddinName.AddinName);
                                }
                            }
                        }
                    }
                    else
                    {
                        showCHNumberInCareCenter = CheckCHNumberSpecification(specification, chNumberCareCenterID);
                        if (!showCHNumberInCareCenter)
                            chNumberCareCenterID = 0;

                        ds = _personDA.GetPersons(specification, chNumberCareCenterID, maxRows);
                        if ((ds != null) && (ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonLookupDTOTable)))
                        {
                            PersonLookupDTOAdvancedAdapter personLookupDTOAdvancedAdapter = new PersonLookupDTOAdvancedAdapter();
                            allPersons = personLookupDTOAdvancedAdapter.GetData(ds);

                            if (allPersons != null)
                            {
                                maxRecordsExceded = (allPersons.Length >= maxRows);
                                if (specification.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter) || specification.IsFilteredByAny(PersonSearchOptionEnum.CHNumber))
                                    allPersons = FindCustomerProcessInfoByCHNumber(maxRows, allPersons, personLookupDTOAdvancedAdapter, specification, chNumberCareCenterID);
                                else
                                    allPersons = AddCustomerProcessInfo(allPersons, specification, maxRows);

                                allPersons = AddCustomerCategoriesInfo(allPersons, specification, maxRows);
                            }
                        }
                    }
                }

                allPersons = AddCustomerLookupAddins(allPersons, specification, maxRows, out errors);

                return allPersons;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public int GetPersonIDByIdentifier(string identifierType, string identifier)
        {
            try
            {
                IdentifierTypeBL identifierTypeBL = new IdentifierTypeBL();
                int identifierTypeID = identifierTypeBL.GetIdentifierTypeID(identifierType);
                if (identifierTypeID <= 0)
                    return 0;

                return _personDA.GetPersonIDByIDNumber(identifierTypeID, identifier);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }
/*
        public PersonEntity GetPerson(int personID)
        {
            try
            {
                int imageID = 0;


                #region Person
                DataSet ds = _personDA.GetPerson(personID);
                #endregion

                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0))
                {
                    #region Person Telephones
                    DataSet ds2 = _personTelephoneRelDA.GetPersonTelephone(personID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTelephoneTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTelephoneTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _telephoneDA.GetTelephoneFromPerson(personID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.TelephoneTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.TelephoneTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Address1
                    int address1 = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["AddressID"] as int? ?? 0;
                    if (address1 > 0)
                    {
                        ds2 = _addressDA.GetAddress(address1);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
                            dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AddressTable;
                            ds.Tables.Add(dt);
                        }
                    }
                    #endregion

                    #region Address2
                    int address2 = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["Address2ID"] as int? ?? 0;
                    if (address2 > 0)
                    {
                        ds2 = _addressDA.GetAddress(address2);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
                            dt.TableName = SII.HCD.BackOffice.Entities.TableNames.Address2Table;
                            ds.Tables.Add(dt);
                        }
                    }
                    #endregion

                    #region Categories
                    ds2 = _personCatRelDA.GetPersonCategory(personID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = _categoryDA.GetCategoryFromPerson(personID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.CategoryTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.CategoryTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region SensitiveData
                    ds2 = _sensitiveDataDA.GetSensitiveDataFromPerson(personID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Identifiers
                    ds2 = _personIdentifierRelDA.GetPersonIdentifier(personID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.IdentifierTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.IdentifierTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PersonIdentifierRelTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = _identifierTypeDA.GetIdentifierFromPerson(personID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion


                    PersonAdvancedAdapter personAdapter = new PersonAdvancedAdapter();
                    PersonEntity result = personAdapter.GetByID(personID, ds);

                    imageID = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["ImageID"] as int? ?? 0;
                    if (imageID > 0)
                    {
                        result.ImageData = _dbImageStorageDA.Get(imageID);
                    }

                    LOPDLogger.Write(EntityNames.PersonEntityName, personID, ActionType.View);
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
 */
        public int obtenerPersonID_From_HumanResource(int HumanResourceId)
        {
            return _personDA.obtenerPersonID_From_HumanResource(HumanResourceId);
        }
        public int obtenerPersonID_From_Customer(int Id)
        {
            return _personDA.obtenerPersonID_From_Customer(Id);
        }
        public int obtenerPersonID_From_NOK(int Id)
        {
            return _personDA.obtenerPersonID_From_NOK(Id);
        }
        public int obtenerPersonID_From_CustomerContactPerson(int Id)
        {
            return _personDA.obtenerPersonID_From_CustomerContactPerson(Id);
        }
        public PersonEntity GetPerson(int personID)
        {
            try
            {
                int imageID = 0;


                #region Person
                DataSet ds = _personDA.GetPerson(personID);
                #endregion

                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.PersonTable)) && (ds.Tables[TableNames.PersonTable].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();

                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());
                       
                    PersonAdvancedAdapter personAdapter = new PersonAdvancedAdapter();
                    PersonEntity result = personAdapter.GetByID(personID, ds2);

                    imageID = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["ImageID"] as int? ?? 0;
                    
                    if (imageID > 0) result.ImageData = _dbImageStorageDA.Get(imageID);

                    LOPDLogger.Write(EntityNames.PersonEntityName, personID, ActionType.View);

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
        public PersonEntity[] GetPersonByCustomerIDs(int[] customerIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) return null;
                customerIDs = customerIDs.OrderBy(c => c).Distinct().ToArray();

                #region Person
                DataSet ds = _personDA.GetPersonByCustomerIDs(customerIDs);

                List<int> personIDs = new List<int>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    personIDs.Add(Convert.ToInt32(row["ID"]));
                }

                #endregion

                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0))
                {
                    #region Person Telephones
                    DataSet ds2 = _personTelephoneRelDA.GetPersonTelephone(personIDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTelephoneTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTelephoneTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _telephoneDA.GetTelephoneFromPerson(personIDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.TelephoneTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.TelephoneTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Address1

                    List<int> address1IDs = new List<int>();
                    List<int> address2IDs = new List<int>();
                    foreach (DataRow row in ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows)
                    {
                        int address1ID = row["AddressID"] as int? ?? 0;
                        if (address1ID > 0)
                            address1IDs.Add(address1ID);

                        int address2ID = row["Address2ID"] as int? ?? 0;
                        if (address2ID > 0)
                            address2IDs.Add(address2ID);
                    }

                    //int address1 = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["AddressID"] as int? ?? 0;
                    ds2 = _addressDA.GetAddress(address1IDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AddressTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Address2
                    //int address2 = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["Address2ID"] as int? ?? 0;
                    ds2 = _addressDA.GetAddress(address2IDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.Address2Table;
                        ds.Tables.Add(dt);
                    }

                    #endregion

                    #region Categories
                    ds2 = _personCatRelDA.GetPersonCategory(personIDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = _categoryDA.GetCategoryFromPerson(personIDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.CategoryTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.CategoryTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region SensitiveData
                    ds2 = _sensitiveDataDA.GetSensitiveDataFromPerson(personIDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Identifiers
                    ds2 = _personIdentifierRelDA.GetPersonIdentifier(personIDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.IdentifierTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.IdentifierTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PersonIdentifierRelTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = _identifierTypeDA.GetIdentifierFromPerson(personIDs.ToArray());
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion


                    PersonAdvancedAdapter personAdapter = new PersonAdvancedAdapter();
                    PersonEntity[] result = personAdapter.GetData(ds);

                    //imageID = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["ImageID"] as int? ?? 0;
                    //if (imageID > 0)
                    //{
                    //    result.ImageData = _dbImageStorageDA.Get(imageID);
                    //}

                    foreach (var p in result)
                        LOPDLogger.Write(EntityNames.PersonEntityName, p.ID, ActionType.View);

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

        //public PersonEntity[] GetPersonByCustomerIDs(int[] customerIDs)
        //{
        //    try
        //    {
        //        if (customerIDs == null || customerIDs.Length <=0) return null;
        //        customerIDs = customerIDs.OrderBy(c => c).Distinct().ToArray();
        //        #region Person
        //        DataSet ds = _personDA.GetPersonByCustomerIDs(customerIDs);
        //        #endregion

        //        if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0))
        //        {
        //            //aqui los PersonIDs para el TVPTable



        //            //#region Person Telephones
        //            //DataSet ds2 = _personTelephoneRelDA.GetPersonTelephone(personID);
        //            //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonTelephoneTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTelephoneTable].Copy();
        //            //    ds.Tables.Add(dt);
        //            //}

        //            //ds2 = _telephoneDA.GetTelephoneFromPerson(personID);
        //            //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.TelephoneTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.TelephoneTable].Copy();
        //            //    ds.Tables.Add(dt);
        //            //}
        //            //#endregion

        //            //#region Address1
        //            //int address1 = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["AddressID"] as int? ?? 0;
        //            //if (address1 > 0)
        //            //{
        //            //    ds2 = _addressDA.GetAddress(address1);
        //            //    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
        //            //    {
        //            //        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
        //            //        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AddressTable;
        //            //        ds.Tables.Add(dt);
        //            //    }
        //            //}
        //            //#endregion

        //            //#region Address2
        //            //int address2 = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows[0]["Address2ID"] as int? ?? 0;
        //            //if (address2 > 0)
        //            //{
        //            //    ds2 = _addressDA.GetAddress(address2);
        //            //    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
        //            //    {
        //            //        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
        //            //        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.Address2Table;
        //            //        ds.Tables.Add(dt);
        //            //    }
        //            //}
        //            //#endregion

        //            //#region Categories
        //            //ds2 = _personCatRelDA.GetPersonCategory(personID);
        //            //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable].Copy();
        //            //    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PersonCategoryTable;
        //            //    ds.Tables.Add(dt);
        //            //}
        //            //ds2 = _categoryDA.GetCategoryFromPerson(personID);
        //            //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CategoryTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.CategoryTable].Copy();
        //            //    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.CategoryTable;
        //            //    ds.Tables.Add(dt);
        //            //}
        //            //#endregion

        //            //#region SensitiveData
        //            //ds2 = _sensitiveDataDA.GetSensitiveDataFromPerson(personID);
        //            //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable].Copy();
        //            //    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.SensitiveDataTable;
        //            //    ds.Tables.Add(dt);
        //            //}
        //            //#endregion

        //            //#region Identifiers
        //            //ds2 = _personIdentifierRelDA.GetPersonIdentifier(personID);
        //            //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.IdentifierTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.IdentifierTable].Copy();
        //            //    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PersonIdentifierRelTable;
        //            //    ds.Tables.Add(dt);
        //            //}
        //            //ds2 = _identifierTypeDA.GetIdentifierFromPerson(personID);
        //            //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable].Copy();
        //            //    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.IdentifierTypeTable;
        //            //    ds.Tables.Add(dt);
        //            //}
        //            //#endregion


        //            PersonAdvancedAdapter personAdapter = new PersonAdvancedAdapter();
        //            return personAdapter.GetData(ds);

        //        }
        //        else return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}


        public CategoryProfileDTO[] GetPersonCategoryProfile(int personID)
        {
            try
            {
                CategoryProfileDTOAdapter categoryProfileDTOAdapter = new CategoryProfileDTOAdapter();

                DataSet ds = _personCatRelDA.GetPersonCategoryProfile(personID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CategoryProfileTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.CategoryProfileTable].Rows.Count > 0))
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

        public Int64 GetPersonDBTimeStamp(int personID)
        {
            try
            {
                return _personDA.GetDBTimeStamp(personID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public PersonBaseEntity[] GetPersonsByType(int customerID, int type)
        {
            try
            {
                PersonBaseAdapter personAdapter = new PersonBaseAdapter();

                DataSet ds = _personDA.GetPersonsByType(customerID, type);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
                {
                    return personAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity GetPersonBaseByID(int personID)
        {
            try
            {
                PersonBaseAdapter personAdapter = new PersonBaseAdapter();

                DataSet ds = _personDA.GetPersonBaseByID(personID);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
                {
                    return personAdapter.GetInfo(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows[0], ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity[] GetPersonsBaseByIDs(int[] personIDs)
        {
            try
            {
                PersonBaseAdapter personAdapter = new PersonBaseAdapter();

                DataSet ds = _personDA.GetPersonsBaseByIDs(personIDs);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
                {
                    return personAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity GetPersonBaseByCustomerID(int customerID)
        {
            try
            {
                PersonBaseAdapter personAdapter = new PersonBaseAdapter();

                DataSet ds = _personDA.GetPersonBaseByCustomerID(customerID);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
                {
                    return personAdapter.GetInfo(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows[0], ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity[] GetPersonsRRHH(int careCenterID, int processChartID, int processStep)
        {
            try
            {
                PersonBaseAdapter personAdapter = new PersonBaseAdapter();

                DataSet ds = _personDA.GetPersonsRRHH(careCenterID, processChartID, processStep);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
                {
                    return personAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity GetPersonByUserName(string userName)
        {
            try
            {
                PersonBaseAdapter personAdapter = new PersonBaseAdapter();

                DataSet ds = _personDA.GetPersonByUserName(userName);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows.Count > 0))
                {
                    return personAdapter.GetInfo(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows[0], ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.IDDescriptionEntity[] GetIDDescriptionByCategory(string firstName, string lastName, string lastName2, int categoryID)
        {
            try
            {
                if (categoryID > 0)
                {
                    CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                    DataSet ds = _personDA.GetIDDescriptionByCategory(firstName, lastName, lastName2, categoryID);
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

        public CommonEntities.IDDescriptionEntity[] GetIDDescriptionByCategoryWithLike(string firstName, string lastName,
            string lastName2, CategoryPersonKeyEnum categoryKey)
        {
            try
            {
                if (categoryKey != CategoryPersonKeyEnum.None)
                {
                    CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                    DataSet ds = _personDA.GetIDDescriptionByCategoryWithLike(firstName, lastName, lastName2, categoryKey);
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


        public CommonEntities.CodeDescriptionEntity[] GetCodeDescriptionUserNames()
        {
            try
            {
                CommonEntities.CodePersonNameAdapter codeDescriptionAdapter = new CommonEntities.CodePersonNameAdapter();
                DataSet ds = _personDA.GetPersonsUserNames((int)SII.HCD.Common.Entities.StatusEnum.Active);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.CodeDescriptionTable)))
                {
                    CommonEntities.CodeDescriptionEntity[] persons = codeDescriptionAdapter.GetData(ds);
                    return persons;
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
        /// Devuelve a partir de un cliente una lista de ID de persona con sus nombres del cliente proporcionado junto con sus parientes y personas de contacto.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>IDDescriptionEntity</returns>
        public CommonEntities.IDDescriptionEntity[] GetCustomerNoksAndContactPersons(int customerID)
        {
            try
            {
                if (customerID > 0)
                {
                    List<CommonEntities.IDDescriptionEntity> myPersonList = new List<CommonEntities.IDDescriptionEntity>();

                    CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                    DataSet ds = _personDA.GetPersonCustomer(customerID);
                    if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                    {
                        CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                        if ((persons != null) && (persons.Length > 0))
                        {
                            myPersonList.AddRange(persons);
                        }
                    }
                    ds = _personDA.GetPersonNOKsByCustomer(customerID);
                    if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                    {
                        CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                        if ((persons != null) && (persons.Length > 0))
                        {
                            myPersonList.AddRange(persons);
                        }
                    }
                    ds = _personDA.GetPersonContactPersonsByCustomer(customerID);
                    if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                    {
                        CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                        if ((persons != null) && (persons.Length > 0))
                        {
                            myPersonList.AddRange(persons);
                        }
                    }

                    if (myPersonList.Count > 0)
                    {
                        return myPersonList.Distinct(new CommonEntities.IDDescriptionComparer()).ToArray();
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
        /// Devuelve a partir de un mapa de proceso y un paso de proceso una lista de ID de persona con sus nombres que tienen el rol asignado.
        /// </summary>
        /// <param name="ProcessChartID"></param>
        /// <param name="ProcessStep"></param>
        /// <returns>IDDescriptionEntity</returns>
        public CommonEntities.IDDescriptionEntity[] GetProcessChartStepProfilePersons(int processChartID, BasicProcessStepsEnum processStep)
        {
            try
            {
                List<CommonEntities.IDDescriptionEntity> myPersonList = new List<CommonEntities.IDDescriptionEntity>();

                CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                DataSet ds = _personDA.GetProcessChartStepProfilePersons(processChartID, (long)processStep);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable))
                    && (ds.Tables[SII.HCD.Common.Entities.TableNames.IDDescriptionTable].Rows.Count > 0))
                {
                    CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                    if ((persons != null) && (persons.Length > 0))
                    {
                        myPersonList.AddRange(persons);
                    }
                }

                if (myPersonList.Count > 0)
                {
                    return myPersonList.Distinct(new CommonEntities.IDDescriptionComparer()).ToArray();
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        public CommonEntities.IDDescriptionEntity[] GetProcessChartsStepProfilesPersons(int[] processChartIDs, BasicProcessStepsEnum processStep)
        {
            try
            {
                List<CommonEntities.IDDescriptionEntity> myPersonList = new List<CommonEntities.IDDescriptionEntity>();

                CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                DataSet ds = _personDA.GetProcessChartStepProfilePersons(processChartIDs, (long)processStep);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable))
                    && (ds.Tables[SII.HCD.Common.Entities.TableNames.IDDescriptionTable].Rows.Count > 0))
                {
                    CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                    if ((persons != null) && (persons.Length > 0))
                    {
                        myPersonList.AddRange(persons);
                    }
                }

                if (myPersonList.Count > 0)
                {
                    return myPersonList.Distinct(new CommonEntities.IDDescriptionComparer()).ToArray();
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        public CommonEntities.IDDescriptionEntity[] GetPersonsAsReportActorByRoutineActID(int routineActID)
        {
            try
            {
                List<CommonEntities.IDDescriptionEntity> myPersonList = new List<CommonEntities.IDDescriptionEntity>();

                CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                DataSet ds = _personDA.GetPersonsAsReportActorByRoutineActID(routineActID, (int)ParticipateAsEnum.ReportActor, (int)CommonEntities.StatusEnum.Active);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                {
                    CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                    if ((persons != null) && (persons.Length > 0))
                    {
                        myPersonList.AddRange(persons);
                    }
                }

                if (myPersonList.Count > 0)
                {
                    return myPersonList.Distinct(new CommonEntities.IDDescriptionComparer()).ToArray();
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.IDDescriptionEntity[] GetPersonsAsReportActorByProcedureActID(int procedureActID)
        {
            try
            {
                List<CommonEntities.IDDescriptionEntity> myPersonList = new List<CommonEntities.IDDescriptionEntity>();

                CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                DataSet ds = _personDA.GetPersonsAsReportActorByProcedureActID(procedureActID, (int)ParticipateAsEnum.ReportActor, (int)CommonEntities.StatusEnum.Active);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                {
                    CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                    if ((persons != null) && (persons.Length > 0))
                    {
                        myPersonList.AddRange(persons);
                    }
                }

                if (myPersonList.Count > 0)
                {
                    return myPersonList.Distinct(new CommonEntities.IDDescriptionComparer()).ToArray();
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.IDDescriptionEntity[] GetPersonsAsReportActorByCustomerOrderRequestID(int customerOrderRequestID)
        {
            try
            {
                List<CommonEntities.IDDescriptionEntity> myPersonList = new List<CommonEntities.IDDescriptionEntity>();

                CommonEntities.IDPersonNameAdapter idDescriptionAdapter = new CommonEntities.IDPersonNameAdapter();
                DataSet ds = _personDA.GetPersonsAsReportActorByCustomerOrderRequestID(customerOrderRequestID, (int)ParticipateAsEnum.ReportActor, (int)CommonEntities.StatusEnum.Active);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)))
                {
                    CommonEntities.IDDescriptionEntity[] persons = idDescriptionAdapter.GetData(ds);
                    if ((persons != null) && (persons.Length > 0))
                    {
                        myPersonList.AddRange(persons);
                    }
                }

                if (myPersonList.Count > 0)
                {
                    return myPersonList.Distinct(new CommonEntities.IDDescriptionComparer()).ToArray();
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public IDCategoryPersonEntity[] GetPersonByCategories(CategoryPersonKeyEnum[] categories)
        {
            try
            {
                IDCategoryPersonAdapter idCategoryPersonAdapter = new IDCategoryPersonAdapter();
                DataSet ds = _personDA.GetPersonByCategories(categories);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(Common.Entities.TableNames.IDDescriptionTable))
                    && (ds.Tables[Common.Entities.TableNames.IDDescriptionTable].Rows.Count > 0))
                {
                    IDCategoryPersonEntity[] persons = idCategoryPersonAdapter.GetData(ds);
                    return persons;
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonCareCenterAccessEntity[] GetPersonCareCenterAccessByPersonID(int personID)
        {
            try
            {
                PersonCareCenterAccessAdapter myAdapter = new PersonCareCenterAccessAdapter();
                PersonCareCenterAccessDA _personCareCenterAccessDA = new PersonCareCenterAccessDA();

                DataSet ds = _personCareCenterAccessDA.GetListPersonCareCenterAccessByPersonID(personID);
                if ((ds != null) && ds.Tables.Contains(BackOffice.Entities.TableNames.PersonCareCenterAccessTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.PersonCareCenterAccessTable].Rows.Count > 0))
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

        public PersonBaseEntity[] GetPersonsHHRR(int careCenterID)
        {
            try
            {
                DataSet ds = _personDA.GetPersonsHHRRByCareCenter(careCenterID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows.Count > 0))
                {
                    PersonBaseAdapter personAdapter = new PersonBaseAdapter();
                    return personAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity[] GetPersonsHHRR(int careCenterID, string[] profileCodes)
        {
            try
            {
                int[] profileIDs = null;
                ProfileDA _profileDA = new ProfileDA();
                DataSet ds = _profileDA.GetProfileIDsFromCodes(profileCodes);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ProfileTable)) &&
                    (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ProfileTable].Rows.Count > 0))
                {
                    List<int> profileIDList = new List<int>();
                    foreach (DataRow row in ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ProfileTable].Rows)
                    {
                        int myID = row["ID"] as int? ?? 0;
                        profileIDList.Add(myID);
                    }
                    profileIDs = profileIDList.ToArray();
                }
                ds = _personDA.GetPersonsHHRRByProfilesCareCenter(profileIDs, careCenterID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)) &&
                    (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows.Count > 0))
                {
                    PersonBaseAdapter personAdapter = new PersonBaseAdapter();
                    return personAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity[] GetPersonsHHRRByLocation(int locationID)
        {
            try
            {
                LocationDA locationDA = new LocationDA();
                int[] careCenterIDs = locationDA.GetCareCenterIDByLocationID(locationID);
                List<PersonBaseEntity> persons = new List<PersonBaseEntity>();
                if (careCenterIDs == null) return null;
                foreach (int ccID in careCenterIDs)
                {
                    PersonBaseEntity[] thisp = GetPersonsHHRR(ccID);
                    if (thisp != null && thisp.Length > 0) persons.AddRange(thisp);
                }
                return persons.Count > 0 ? persons.ToArray() : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonBaseEntity[] GetPersonsHHRRByLocation(int locationID, string[] profileCodes)
        {
            try
            {
                LocationDA locationDA = new LocationDA();
                int[] careCenterIDs = locationDA.GetCareCenterIDByLocationID(locationID);
                List<PersonBaseEntity> persons = new List<PersonBaseEntity>();
                if (careCenterIDs == null) return null;
                foreach (int ccID in careCenterIDs)
                {
                    PersonBaseEntity[] thisp = GetPersonsHHRR(ccID, profileCodes);
                    if (thisp != null && thisp.Length > 0) persons.AddRange(thisp);
                }
                return persons.Count > 0 ? persons.ToArray() : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public byte[] GetImage(int personID)
        {
            try
            {
                int imageID = _personDA.GetImageIDByPersonID(personID);
                return (imageID > 0)
                    ? _dbImageStorageDA.Get(imageID)
                    : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public PersonImageDTO[] GetImages(int[] personIDs)
        {
            try
            {
                if (personIDs == null || personIDs.Length <= 0) return null;
                personIDs = personIDs.Distinct().ToArray();
                List<PersonImageDTO> result = new List<PersonImageDTO>();
                DataSet ds = _personDA.GetImageIDsByPersonIDs(personIDs);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)) &&
                    (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows.Count > 0))
                {
                    foreach (DataRow row in ds.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Rows)
                    {
                        PersonImageDTO item = new PersonImageDTO()
                        {
                            PersonID = row.Field<int>("PersonID"),
                            ImageID = row.Field<int>("ImageID"),
                            ImageData = null,
                        };

                        if (item.ImageID > 0)
                        {
                            item.ImageData = _dbImageStorageDA.Get(item.ImageID);
                        }

                        result.Add(item);
                    }
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public void SavePhoneticInfo(int personID)
        {
            UpdatePhoneticInfo(personID);
        }

        public void SavePhoneticInfo(int personID, string[] addinNames)
        {
            UpdatePhoneticInfo(personID, addinNames);
        }

        public CommonEntities.AddInTokenBaseEntity[] GetAvailableCustomerLookupAddins(PersonSearchOptionEnum supportedLookupModes)
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
                        CustomerLookupHostView host = AddInRepository.GetAddIn<CustomerLookupHostView>(token.Name);
                        if (host != null)
                        {
                            try
                            {
                                int addinSupportedLookupModes = host.GetSupportedLookupModes();
                                if ((addinSupportedLookupModes & ((int)supportedLookupModes)) != 0)
                                {
                                    result.Add(new CommonEntities.AddInTokenBaseEntity(token.Name, token.Description, token.Publisher, token.Version));
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service);
                            }
                        }
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

        public CommonEntities.AddInTokenBaseEntity[] GetAvailablePhoneticAddins()
        {
            try
            {
                Collection<AddInToken> tokens = AddInRepository.GetAddInTokens<PhoneticTranslatorHostView>();
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

        public void InsertOrUpdatePhoneticInfo(int personID)
        {
            try
            {
                if (personID <= 0) throw new ArgumentNullException("personID");
                this.UpdatePhoneticInfo(personID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return;
            }
        }



        public void InsertOrUpdatePhoneticInfo(List<Tuple<int, string, string, string>> personlist)
        {
            try
            {
                if (personlist == null || personlist.Count <= 0) return;
                PersonBaseEntity[] persons = (from item in personlist
                                              select
                                              new PersonBaseEntity(
                                                     item.Item1, item.Item2, item.Item3, item.Item4,
                                                     CommonEntities.StatusEnum.Active, 0)
                                                 ).ToArray();

                this.UpdatePhoneticInfo(persons);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return;
            }
        }

        #endregion

        #region Public temporal methods
        //// estos métodos deberán desaparecer cuando se implemente correctamente la cache de cliente
        public void NotifyAllUsersTypeModified(MasterDataTypeEnum typeModified)
        {
            //try
            //{
            //   int allusers[] = _personDA.GetAllUsers();

            //    _personDA.NotifyAllUsersTypeModified(typeModified);
            //}
            //catch (Exception ex)
            //{
            //    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            //}
        }

        public int GetModifiedTypesByUserName(string userName)
        {
            return 0;
        }


        #endregion
    }
}
