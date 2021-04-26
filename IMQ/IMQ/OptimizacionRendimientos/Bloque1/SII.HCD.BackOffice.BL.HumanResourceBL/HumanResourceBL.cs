using System;
using System.Collections.Generic;
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
using SII.Framework.Logging.LOPD;
using SII.HCD.BackOffice.BL.CodeProvider;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.BackOffice.Services;
using SII.HCD.Common.BL;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Configuration;
using SII.HCD.Misc;
using SII.SIFP.Configuration;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.BackOffice.BL
{
    public class HumanResourceBL : PersonBL, IHumanResourceService
    {
        #region Constants
        //private const string HumanResourceEntityName = "HumanResourceEntity";
        #endregion

        #region DA Definitions
        private HumanResourceDA _humanResourceDA;
        private ResourceDeviceRelDA _resourceDeviceRelDA;
        private DeviceDA _deviceDA;
        private DeviceTypeDA _deviceTypeDA;
        private HumanResourceProfileRelDA _humanResourceProfileRelDA;
        private PersonAvailPatternDA _personAvailPatternDA;
        private PersonCareCenterAccessDA _personCareCenterAccessDA;
        private AvailPatternDA _availPatternDA;
        private TimePatternDA _timePatternDA;
        private ProfileDA _profileDA;
        private CategoryDA _categoryDA;
        private PersonDA _personDA;
        private PersonCatRelDA _personCatRelDA;
        private ParticipateAsProfileRelDA _participateAsProfileRelDA;
        private ParticipateAsDA _participateAsDA;
        #endregion

        #region Constructor
        public HumanResourceBL()
        {
            _humanResourceDA = new HumanResourceDA();
            _resourceDeviceRelDA = new ResourceDeviceRelDA();
            _deviceDA = new DeviceDA();
            _deviceTypeDA = new DeviceTypeDA();
            _humanResourceProfileRelDA = new HumanResourceProfileRelDA();
            _personAvailPatternDA = new PersonAvailPatternDA();
            _personCareCenterAccessDA = new PersonCareCenterAccessDA();
            _availPatternDA = new AvailPatternDA();
            _timePatternDA = new TimePatternDA();
            _profileDA = new ProfileDA();
            _categoryDA = new CategoryDA();
            _personDA = new PersonDA();
            _personCatRelDA = new PersonCatRelDA();
            _participateAsProfileRelDA = new ParticipateAsProfileRelDA();
            _participateAsDA = new ParticipateAsDA();
        }
        #endregion

        #region private methods
        private void ResetHHRR(HumanResourceEntity hhrr)
        {
            hhrr.EditStatus.Reset();

            if (hhrr.Profiles != null)
            {
                List<HHRRProfileRelEntity> Profiles = new List<HHRRProfileRelEntity>();
                foreach (HHRRProfileRelEntity profileRel in hhrr.Profiles)
                {
                    if (!((profileRel.EditStatus.Value == StatusEntityValue.Deleted) || (profileRel.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        profileRel.EditStatus.Reset();
                        Profiles.Add(profileRel);
                    }
                }
                hhrr.Profiles = Profiles.ToArray();
            }

            if (hhrr.AllocatedDevices != null)
            {
                List<ResourceDeviceEntity> Devices = new List<ResourceDeviceEntity>();
                foreach (ResourceDeviceEntity device in hhrr.AllocatedDevices)
                {
                    if (!((device.EditStatus.Value == StatusEntityValue.Deleted) || (device.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        device.EditStatus.Reset();
                        Devices.Add(device);
                    }
                }
                hhrr.AllocatedDevices = Devices.ToArray();
            }

            if (hhrr.AvailPatterns != null)
            {
                List<PersonAvailPatternEntity> AvailPatterns = new List<PersonAvailPatternEntity>();
                foreach (PersonAvailPatternEntity availPattern in hhrr.AvailPatterns)
                {
                    if (!((availPattern.EditStatus.Value == StatusEntityValue.Deleted) || (availPattern.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        availPattern.EditStatus.Reset();
                        AvailPatterns.Add(availPattern);
                    }
                }
                hhrr.AvailPatterns = AvailPatterns.ToArray();
            }

            if (hhrr.CareCentersAccess != null)
            {
                List<PersonCareCenterAccessEntity> careCentersAccess = new List<PersonCareCenterAccessEntity>();
                foreach (PersonCareCenterAccessEntity careCenterAccess in hhrr.CareCentersAccess)
                {
                    if (!((careCenterAccess.EditStatus.Value == StatusEntityValue.Deleted) || (careCenterAccess.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        careCenterAccess.EditStatus.Reset();
                        careCentersAccess.Add(careCenterAccess);
                    }
                }
                hhrr.CareCentersAccess = careCentersAccess.ToArray();
            }

            base.ResetPerson(hhrr.Person);
        }

        private HumanResourceEntity Insert(HumanResourceEntity hhrr, ElementBL elementBL)
        {
            if (hhrr == null) throw new ArgumentNullException("human resource");

            string userName = IdentityUser.GetIdentityUserName();
            int categoryID = _categoryDA.GetCategoryIDByCategoryKey((int)CategoryPersonKeyEnum.HHRR);
            if (categoryID <= 0)
            {
                throw new Exception(Properties.Resources.ERROR_NoCategoryDefinedForHumanResources);
            }

            string hhrrFileNumber = String.Empty;

            CommonEntities.ElementEntity _hhrrMetadata = base.GetElementByName(EntityNames.HumanResourceEntityName, elementBL);
            if (_hhrrMetadata != null)
                hhrrFileNumber = _hhrrMetadata.GetCodeGeneratorName("FileNumber");

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerInsert(hhrr, userName, categoryID, hhrrFileNumber);
                scope.Complete();
            }

            this.ResetHHRR(hhrr);
            LOPDLogger.Write(EntityNames.HumanResourceEntityName, hhrr.ID, ActionType.Create);
            return hhrr;
        }

        private HumanResourceEntity Update(HumanResourceEntity hhrr)
        {
            if (hhrr == null) throw new ArgumentNullException("human resource");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                if (hhrr.Person.EditStatus.Value == StatusEntityValue.Updated)
                {
                    hhrr.Person = base.InnerUpdate(hhrr.Person, userName, true);
                }

                if (hhrr.EditStatus.Value == StatusEntityValue.Updated)
                {
                    this.InnerUpdate(hhrr, userName, true);
                }
                scope.Complete();
            }

            this.ResetHHRR(hhrr);
            LOPDLogger.Write(EntityNames.HumanResourceEntityName, hhrr.ID, ActionType.Modify);
            return hhrr;
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

        /// <summary>
        /// Recupera todos los recursos humanos que son empleados con sus perfiles (es decir un recurso humano aparecera en el listado tantas veces como perfiles tenga asociado) y que tengan permisos para el centro selecionado.
        /// Los recursos humanos que no tengan perfil solo apareceran una vez y con ProfileID = 0 (Es decir sin perfil).
        /// </summary>
        /// <param name="careCenterID">Id del centro donde se realiza la rutina o procedimiento</param>
        /// <returns>Listado de recurso humanos con sus perfiles</returns>
        private HHRRListDTO[] GetHHRRsWithProfileByEmployeeIsActiveCareCenterID(int careCenterID, int identifierTypeID)
        {
            try
            {
                HHRRListDTOAdapter adapter = new HHRRListDTOAdapter();

                DataSet ds = _humanResourceDA.GetHHRRsWithProfileByEmployeeIsActiveCareCenterID(identifierTypeID, careCenterID);
                if ((ds.Tables != null)
                    && ds.Tables.Contains(BackOffice.Entities.TableNames.HHRRListDTOTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.HHRRListDTOTable].Rows.Count > 0))
                {
                    return adapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        #endregion

        #region public methods with service
        public int GetHumanResourcePersonID(int humanResourceID)
        {
            try
            {
                return _humanResourceDA.GetPersonID(humanResourceID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public void CheckInsertPreconditions(HumanResourceEntity hhrr, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (hhrr == null) throw new ArgumentNullException("human resource");

            ValidateHHRR(hhrr, elementBL);

            #region Comentado por SALVA
            //HumanResourceFindRequest humanResourceFind = new HumanResourceFindRequest();
            //BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            //AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;

            //if (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null)
            //{
            //    foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes)
            //    {
            //        if ((attrib.Name == "FirstName") && (attrib.Mandatory))
            //        {
            //            humanResourceFind.FirstName = hhrr.Person.FirstName;
            //            humanResourceFind.MandatoryFirstName = true;
            //        }

            //        if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //        {
            //            humanResourceFind.LastName = hhrr.Person.LastName;
            //            humanResourceFind.MandatoryLastName = true;
            //        }

            //        if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(hhrr.AllowNoDefaultIdentifier))
            //        {
            //            humanResourceFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
            //            humanResourceFind.MandatoryIdentifierType = true;
            //            humanResourceFind.IdentifierIDNumber = GetIDNumber(hhrr.Person.Identifiers, attrib.DefaultValue);
            //        }
            //    }
            //}

            //if (String.IsNullOrEmpty(humanResourceFind.IdentifierIDNumber) && (humanResourceFind.MandatoryIdentifierType))
            //{
            //    //checking alternatives
            //    if ((backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
            //        (backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
            //    {
            //        Boolean alternativeFound = false;
            //        String alternatives = humanResourceFind.MandatoryIdentifierTypeDefaultValue;
            //        foreach (EntityAttributeOptionElement alternative in backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
            //        {
            //            if (!String.IsNullOrEmpty(GetIDNumber(hhrr.Person.Identifiers, alternative.Value)))
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
            //            throw new Exception(string.Format(Properties.Resources.MSG_HumanResourceIdentifierRequired, alternatives));
            //        }
            //        else
            //        {
            //            humanResourceFind.MandatoryIdentifierType = false;
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception(string.Format(Properties.Resources.MSG_HumanResourceIdentifierRequired, humanResourceFind.MandatoryIdentifierTypeDefaultValue));
            //    }
            //}
            #endregion

            homonymPersons = null;

            switch (hhrr.Person.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    //int id = _personDA.GetPerson(humanResourceFind);
                    //if (id > 0)
                    //{
                    //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(hhrr.Person.FirstName, " ", hhrr.Person.LastName)));
                    //}
                    //DO SALVA: Llamamos al algoritmo de validación de Persona.
                    base.CheckInsertPreconditions(hhrr.Person, 0, CategoryPersonKeyEnum.HHRR, forceSave, hhrr.AllowNoDefaultIdentifier, true, out homonymPersons, elementBL);
                    break;
                case StatusEntityValue.Updated:
                    //int id2 = _personDA.GetPerson(humanResourceFind);
                    //if ((id2 > 0) && (id2 != hhrr.Person.ID))
                    //{
                    //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(hhrr.Person.FirstName, " ", hhrr.Person.LastName)));
                    //}
                    //DO SALVA
                    base.CheckUpdatePreconditions(hhrr.Person, 0, CategoryPersonKeyEnum.HHRR, forceSave, hhrr.AllowNoDefaultIdentifier, true, out homonymPersons, elementBL);
                    break;
            }


            if ((!String.IsNullOrEmpty(hhrr.FileNumber)) && (_humanResourceDA.FindFileNumber(hhrr.FileNumber) > 0))
            {
                throw new Exception(string.Format(Properties.Resources.MSG_FileNumberAlreadyExists, hhrr.FileNumber));
            }
        }

        public void CheckUpdatePreconditions(HumanResourceEntity hhrr, bool forceSave, out PersonAddressListDTO[] homonymPersons, ElementBL elementBL)
        {
            if (hhrr == null) throw new ArgumentNullException("human resource");

            ValidateHHRR(hhrr, elementBL);

            #region Comentado por SALVA
            //HumanResourceFindRequest humanResourceFind = new HumanResourceFindRequest();
            //BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
            //if (backOfficeConfig != null)
            //{
            //    if (backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes != null)
            //    {
            //        foreach (EntityAttributeElement attrib in backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes)
            //        {
            //            if ((attrib.Name == "FirstName") && (attrib.Mandatory))
            //            {
            //                humanResourceFind.FirstName = hhrr.Person.FirstName;
            //                humanResourceFind.MandatoryFirstName = true;
            //            }

            //            if ((attrib.Name == "LastName") && (attrib.Mandatory))
            //            {
            //                humanResourceFind.LastName = hhrr.Person.LastName;
            //                humanResourceFind.MandatoryLastName = true;
            //            }

            //            if ((attrib.Name == "Identifier.IdentifierType") && (attrib.Mandatory) && !(hhrr.AllowNoDefaultIdentifier))
            //            {
            //                humanResourceFind.MandatoryIdentifierTypeDefaultValue = attrib.DefaultValue;
            //                humanResourceFind.MandatoryIdentifierType = true;
            //                humanResourceFind.IdentifierIDNumber = GetIDNumber(hhrr.Person.Identifiers, attrib.DefaultValue);
            //            }
            //        }
            //    }
            //}

            //if (String.IsNullOrEmpty(humanResourceFind.IdentifierIDNumber) && (humanResourceFind.MandatoryIdentifierType))
            //{
            //    //checking alternatives
            //    if ((backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions != null) &&
            //        (backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions.Count > 0))
            //    {
            //        Boolean alternativeFound = false;
            //        String alternatives = humanResourceFind.MandatoryIdentifierTypeDefaultValue;
            //        foreach (EntityAttributeOptionElement alternative in backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].AlternativeOptions)
            //        {
            //            if (!String.IsNullOrEmpty(GetIDNumber(hhrr.Person.Identifiers, alternative.Value)))
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
            //            throw new Exception(string.Format(Properties.Resources.MSG_HumanResourceIdentifierRequired, alternatives));
            //        }
            //        else
            //        {
            //            humanResourceFind.MandatoryIdentifierType = false;
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception(string.Format(Properties.Resources.MSG_HumanResourceIdentifierRequired, humanResourceFind.MandatoryIdentifierTypeDefaultValue));
            //    }
            //}

            //int id = _personDA.GetPerson(humanResourceFind);
            //if ((id > 0) && (id != hhrr.Person.ID))
            //{
            //    throw new Exception(string.Format(Properties.Resources.MSG_PersonAlreadyExists, string.Concat(hhrr.Person.FirstName, " ", hhrr.Person.LastName)));
            //}
            #endregion

            homonymPersons = null;

            //DO SALVA
            base.CheckUpdatePreconditions(hhrr.Person, 0, CategoryPersonKeyEnum.HHRR, forceSave, hhrr.AllowNoDefaultIdentifier, true, out homonymPersons, elementBL);

            int hhrrID = 0;

            if (!String.IsNullOrEmpty(hhrr.FileNumber))
            {
                hhrrID = _humanResourceDA.FindFileNumber(hhrr.FileNumber);
                if ((hhrrID > 0) && (hhrrID != hhrr.ID))
                {
                    throw new Exception(string.Format(Properties.Resources.MSG_FileNumberAlreadyExists, hhrr.FileNumber));
                }
            }
        }

        public void ValidateHHRR(HumanResourceEntity hhrr, ElementBL elementBL)
        {
            if (hhrr == null) throw new ArgumentNullException("human resource");

            CommonEntities.ElementEntity _humanResourceMetadata = base.GetElementByName(EntityNames.HumanResourceEntityName, elementBL);
            HumanResourceHelper humanResourceHelper = new HumanResourceHelper(_humanResourceMetadata);

            ValidationResults result = humanResourceHelper.Validate(hhrr);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_humanResourceValidationError, sb));
            }

            //base.ValidatePerson(hhrr.Person, elementBL);
        }

        public HumanResourceEntity InnerInsert(HumanResourceEntity hhrr, string userName, int categoryID, string hhrrFileNumber)
        {
            #region Code generation
            CodeGenerator codeGenerator = new CodeGenerator();
            if (!String.IsNullOrEmpty(hhrrFileNumber))
            {
                hhrr.FileNumber = codeGenerator.Generate(String.Empty, hhrrFileNumber);
            }
            #endregion

            #region Person
            switch (hhrr.Person.EditStatus.Value)
            {
                case StatusEntityValue.New: hhrr.Person = base.InnerInsert(hhrr.Person, userName); break;
                case StatusEntityValue.Updated: hhrr.Person = base.InnerUpdate(hhrr.Person, userName, true); break;
                default: break;
            }
            #endregion

            #region Human Resource
            hhrr.ID = _humanResourceDA.Insert(hhrr.Person.ID, hhrr.FileNumber, hhrr.HasAvailability, hhrr.AdmitNotification, hhrr.IncludingEmail, userName);
            hhrr.DBTimeStamp = _humanResourceDA.GetDBTimeStamp(hhrr.ID);

            //TODO:: No insertar las categorias ya que se añaden en el sistema de validadcion de homonimos
            //_personCatRelDA.Insert(hhrr.Person.ID, categoryID, userName);
            #endregion

            #region Profile Rel
            if (hhrr.Profiles != null)
            {
                foreach (HHRRProfileRelEntity profileRel in hhrr.Profiles)
                {
                    switch (profileRel.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _humanResourceProfileRelDA.Delete(profileRel.ID);
                            break;
                        case StatusEntityValue.New:
                            if (profileRel.Profile != null)
                            {
                                profileRel.ID = _humanResourceProfileRelDA.Insert(hhrr.ID, profileRel.Profile.ID, profileRel.DefaultProfile, (int)profileRel.Status, userName);
                            }
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            if (profileRel.Profile != null)
                            {
                                _humanResourceProfileRelDA.Update(profileRel.ID, hhrr.ID, profileRel.Profile.ID, profileRel.DefaultProfile, (int)profileRel.Status, userName);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Resource Device Rel
            if (hhrr.AllocatedDevices != null)
            {
                foreach (ResourceDeviceEntity device in hhrr.AllocatedDevices)
                {
                    switch (device.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _resourceDeviceRelDA.Delete(device.ID);
                            break;
                        case StatusEntityValue.New:
                            if (device.Device != null)
                            {
                                device.ID = _resourceDeviceRelDA.Insert(hhrr.ID, device.Device.ID, device.ActiveAt, (int)device.Status, userName);
                            }
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            if (device.Device != null)
                            {
                                _resourceDeviceRelDA.Update(device.ID, hhrr.ID, device.Device.ID, device.ActiveAt, (int)device.Status, userName);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Avail Pattern Rel
            if (hhrr.AvailPatterns != null)
            {
                foreach (PersonAvailPatternEntity availPattern in hhrr.AvailPatterns)
                {
                    switch (availPattern.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _personAvailPatternDA.Delete(availPattern.ID);
                            break;
                        case StatusEntityValue.New:
                            if (availPattern.AvailabilityPattern != null)
                            {
                                availPattern.ID = _personAvailPatternDA.Insert(hhrr.Person.ID, availPattern.CareCenterID,
                                    availPattern.AvailabilityPattern.ID, (int)availPattern.Status, availPattern.IsDefault,
                                    availPattern.StartAt, availPattern.EndingIn, userName);
                                _availPatternDA.UpdateInUse(availPattern.AvailabilityPattern.ID, true, userName);
                            }
                            break;
                        case StatusEntityValue.Updated:
                            if (availPattern.AvailabilityPattern != null)
                            {
                                _personAvailPatternDA.Update(availPattern.ID, hhrr.Person.ID, availPattern.CareCenterID,
                                    availPattern.AvailabilityPattern.ID, (int)availPattern.Status, availPattern.IsDefault,
                                    availPattern.StartAt, availPattern.EndingIn, userName);
                                _availPatternDA.UpdateInUse(availPattern.AvailabilityPattern.ID, true, userName);
                            }
                            break;
                        default: break;
                    }
                }
            }
            #endregion

            #region Care Center Access
            if (hhrr.CareCentersAccess != null)
            {
                foreach (PersonCareCenterAccessEntity careCenterAccess in hhrr.CareCentersAccess)
                {
                    switch (careCenterAccess.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted: _personCareCenterAccessDA.Delete(careCenterAccess.ID); break;
                        case StatusEntityValue.New:
                            careCenterAccess.ID = _personCareCenterAccessDA.Insert(hhrr.Person.ID, careCenterAccess.CareCenterID,
                                careCenterAccess.Workplace, careCenterAccess.StartAccessDate, careCenterAccess.EndAccessDate, userName);
                            break;
                        case StatusEntityValue.Updated:
                            _personCareCenterAccessDA.Update(careCenterAccess.ID, hhrr.Person.ID, careCenterAccess.CareCenterID,
                                careCenterAccess.Workplace, careCenterAccess.StartAccessDate, careCenterAccess.EndAccessDate, userName);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            return hhrr;
        }

        public HumanResourceEntity InnerUpdate(HumanResourceEntity hhrr, string userName, bool fullHumanResourceUpdate)
        {
            Int64 dbTimeStamp = _humanResourceDA.GetDBTimeStamp(hhrr.ID);
            if (dbTimeStamp != hhrr.DBTimeStamp)
                throw new FaultException<DBConcurrencyException>(
                    new DBConcurrencyException(string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, hhrr.ID)), string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, hhrr.ID));

            if (fullHumanResourceUpdate)
            {
                _humanResourceDA.Update(hhrr.ID, hhrr.FileNumber, hhrr.HasAvailability, hhrr.AdmitNotification, hhrr.IncludingEmail, userName);
            }
            else
            {
                _humanResourceDA.Update(hhrr.ID, userName);
            }

            hhrr.DBTimeStamp = _humanResourceDA.GetDBTimeStamp(hhrr.ID);

            #region Profile Rel
            if (hhrr.Profiles != null)
            {
                foreach (HHRRProfileRelEntity profileRel in hhrr.Profiles)
                {
                    switch (profileRel.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _humanResourceProfileRelDA.Delete(profileRel.ID);
                            break;
                        case StatusEntityValue.New:
                            if (profileRel.Profile != null)
                            {
                                profileRel.ID = _humanResourceProfileRelDA.Insert(profileRel.HumanResourceID, profileRel.Profile.ID, profileRel.DefaultProfile, (int)profileRel.Status, userName);
                            }
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            if (profileRel.Profile != null)
                            {
                                _humanResourceProfileRelDA.Update(profileRel.ID, profileRel.HumanResourceID, profileRel.Profile.ID, profileRel.DefaultProfile, (int)profileRel.Status, userName);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Resource Device Rel
            if (hhrr.AllocatedDevices != null)
            {
                foreach (ResourceDeviceEntity device in hhrr.AllocatedDevices)
                {
                    switch (device.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _resourceDeviceRelDA.Delete(device.ID);
                            break;
                        case StatusEntityValue.New:
                            if (device.Device != null)
                            {
                                device.ID = _resourceDeviceRelDA.Insert(device.HumanResourceID, device.Device.ID, device.ActiveAt, (int)device.Status, userName);
                            }
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            if (device.Device != null)
                            {
                                _resourceDeviceRelDA.Update(device.ID, device.HumanResourceID, device.Device.ID, device.ActiveAt, (int)device.Status, userName);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Avail Pattern Rel
            if (hhrr.AvailPatterns != null)
            {
                foreach (PersonAvailPatternEntity availPattern in hhrr.AvailPatterns)
                {
                    switch (availPattern.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _personAvailPatternDA.Delete(availPattern.ID);
                            break;
                        case StatusEntityValue.New:
                            if (availPattern.AvailabilityPattern != null)
                            {
                                availPattern.ID = _personAvailPatternDA.Insert(availPattern.PersonID, availPattern.CareCenterID,
                                    availPattern.AvailabilityPattern.ID, (int)availPattern.Status, availPattern.IsDefault,
                                    availPattern.StartAt, availPattern.EndingIn, userName);
                                _availPatternDA.UpdateInUse(availPattern.AvailabilityPattern.ID, true, userName);
                            }
                            break;
                        case StatusEntityValue.Updated:
                            if (availPattern.AvailabilityPattern != null)
                            {
                                _personAvailPatternDA.Update(availPattern.ID, availPattern.PersonID, availPattern.CareCenterID,
                                    availPattern.AvailabilityPattern.ID, (int)availPattern.Status, availPattern.IsDefault,
                                    availPattern.StartAt, availPattern.EndingIn, userName);
                                _availPatternDA.UpdateInUse(availPattern.AvailabilityPattern.ID, true, userName);
                            }
                            break;
                        default: break;
                    }
                }
            }
            #endregion

            #region Care Center Access
            if (hhrr.CareCentersAccess != null)
            {
                foreach (PersonCareCenterAccessEntity careCenterAccess in hhrr.CareCentersAccess)
                {
                    switch (careCenterAccess.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _personCareCenterAccessDA.Delete(careCenterAccess.ID);
                            break;
                        case StatusEntityValue.New:
                            careCenterAccess.ID = _personCareCenterAccessDA.Insert(hhrr.Person.ID, careCenterAccess.CareCenterID, careCenterAccess.Workplace, careCenterAccess.StartAccessDate, careCenterAccess.EndAccessDate, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _personCareCenterAccessDA.Update(careCenterAccess.ID, hhrr.Person.ID, careCenterAccess.CareCenterID, careCenterAccess.Workplace, careCenterAccess.StartAccessDate, careCenterAccess.EndAccessDate, userName);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion


            return hhrr;
        }

        public HumanResourceEntity InnerUpdate(HumanResourceEntity hhrr, string userName)
        {
            Int64 dbTimeStamp = _humanResourceDA.GetDBTimeStamp(hhrr.ID);
            if (dbTimeStamp != hhrr.DBTimeStamp)
                throw new FaultException<DBConcurrencyException>(
                    new DBConcurrencyException(string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, hhrr.ID)), string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, hhrr.ID));

            if (hhrr.Person.EditStatus.Value == StatusEntityValue.Updated)
            {
                hhrr.Person = base.InnerUpdate(hhrr.Person, userName, true);
            }
            _humanResourceDA.Update(hhrr.ID, hhrr.FileNumber, hhrr.HasAvailability, hhrr.AdmitNotification, hhrr.IncludingEmail, userName);
            hhrr.DBTimeStamp = _humanResourceDA.GetDBTimeStamp(hhrr.ID);

            #region Profile Rel
            if (hhrr.Profiles != null)
            {
                foreach (HHRRProfileRelEntity profileRel in hhrr.Profiles)
                {
                    switch (profileRel.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _humanResourceProfileRelDA.Delete(profileRel.ID);
                            break;
                        case StatusEntityValue.New:
                            if (profileRel.Profile != null)
                            {
                                profileRel.ID = _humanResourceProfileRelDA.Insert(profileRel.HumanResourceID, profileRel.Profile.ID, profileRel.DefaultProfile, (int)profileRel.Status, userName);
                            }
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            if (profileRel.Profile != null)
                            {
                                _humanResourceProfileRelDA.Update(profileRel.ID, profileRel.HumanResourceID, profileRel.Profile.ID, profileRel.DefaultProfile, (int)profileRel.Status, userName);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Resource Device Rel
            if (hhrr.AllocatedDevices != null)
            {
                foreach (ResourceDeviceEntity device in hhrr.AllocatedDevices)
                {
                    switch (device.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _resourceDeviceRelDA.Delete(device.ID);
                            break;
                        case StatusEntityValue.New:
                            if (device.Device != null)
                            {
                                device.ID = _resourceDeviceRelDA.Insert(device.HumanResourceID, device.Device.ID, device.ActiveAt, (int)device.Status, userName);
                            }
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            if (device.Device != null)
                            {
                                _resourceDeviceRelDA.Update(device.ID, device.HumanResourceID, device.Device.ID, device.ActiveAt, (int)device.Status, userName);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Avail Pattern Rel
            if (hhrr.AvailPatterns != null)
            {
                foreach (PersonAvailPatternEntity availPattern in hhrr.AvailPatterns)
                {
                    switch (availPattern.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _personAvailPatternDA.Delete(availPattern.ID);
                            break;
                        case StatusEntityValue.New:
                            if (availPattern.AvailabilityPattern != null)
                            {
                                availPattern.ID = _personAvailPatternDA.Insert(availPattern.PersonID, availPattern.CareCenterID, availPattern.AvailabilityPattern.ID, (int)availPattern.Status, availPattern.IsDefault,
                                    availPattern.StartAt, availPattern.EndingIn, userName);
                                _availPatternDA.UpdateInUse(availPattern.AvailabilityPattern.ID, true, userName);
                            }
                            break;
                        case StatusEntityValue.Updated:
                            if (availPattern.AvailabilityPattern != null)
                            {
                                _personAvailPatternDA.Update(availPattern.ID, availPattern.PersonID, availPattern.CareCenterID, availPattern.AvailabilityPattern.ID, (int)availPattern.Status, availPattern.IsDefault,
                                    availPattern.StartAt, availPattern.EndingIn, userName);
                                _availPatternDA.UpdateInUse(availPattern.AvailabilityPattern.ID, true, userName);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Care Center Access
            if (hhrr.CareCentersAccess != null)
            {
                foreach (PersonCareCenterAccessEntity careCenterAccess in hhrr.CareCentersAccess)
                {
                    switch (careCenterAccess.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _personCareCenterAccessDA.Delete(careCenterAccess.ID);
                            break;
                        case StatusEntityValue.New:
                            careCenterAccess.ID = _personCareCenterAccessDA.Insert(hhrr.Person.ID, careCenterAccess.CareCenterID, careCenterAccess.Workplace, careCenterAccess.StartAccessDate, careCenterAccess.EndAccessDate, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _personCareCenterAccessDA.Update(careCenterAccess.ID, hhrr.Person.ID, careCenterAccess.CareCenterID, careCenterAccess.Workplace, careCenterAccess.StartAccessDate, careCenterAccess.EndAccessDate, userName);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            return hhrr;
        }
        #endregion

        #region IHumanResourceService members
        public HumanResourceEntity Save(HumanResourceEntity hhrr, bool forceSave, out PersonAddressListDTO[] homonymPersons)
        {
            try
            {
                if (hhrr == null) throw new ArgumentNullException("Human Resource");

                ElementBL _elementBL = new ElementBL();
                homonymPersons = null;

                switch (hhrr.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return hhrr;
                    case StatusEntityValue.New:
                        this.CheckInsertPreconditions(hhrr, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return hhrr;
                        return this.Insert(hhrr, _elementBL);
                    case StatusEntityValue.NewAndDeleted:
                        return hhrr;
                    case StatusEntityValue.None:
                        if ((hhrr.Person != null) && (hhrr.Person.EditStatus.Value == StatusEntityValue.Updated))
                        {
                            this.CheckUpdatePreconditions(hhrr, forceSave, out homonymPersons, _elementBL);
                            if (homonymPersons != null)
                                return hhrr;
                            this.Update(hhrr);
                        }
                        return hhrr;
                    case StatusEntityValue.Updated:
                        this.CheckUpdatePreconditions(hhrr, forceSave, out homonymPersons, _elementBL);
                        if (homonymPersons != null)
                            return hhrr;
                        return this.Update(hhrr);
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                homonymPersons = null;
                return null;
            }
        }

        public HHRRListDTO[] GetHHRRs(out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                int maxRows = backOfficeConfig.EntitySettings.HumanResourceEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }
                string identifierTypeName = backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].DefaultValue;

                HHRRListDTOAdapter hhrrListDTOAdapter = new HHRRListDTOAdapter();

                DataSet ds = _humanResourceDA.GetHHRRs(identifierTypeName, maxRows);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HHRRListDTOTable)))
                {
                    HHRRListDTO[] hhrrs = hhrrListDTOAdapter.GetData(ds);
                    if (hhrrs != null)
                    {
                        maxRecordsExceded = (hhrrs.Length >= maxRows);
                    }
                    return hhrrs;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HHRRListDTO GetHHRRListDTO(int id)
        {
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                string identifierTypeName = backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].DefaultValue;

                HHRRListDTOAdapter hhrrListDTOAdapter = new HHRRListDTOAdapter();

                DataSet ds = _humanResourceDA.GetHHRRListDTOByID(id, identifierTypeName);
                if (ds.Tables.Contains(BackOffice.Entities.TableNames.HHRRListDTOTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.HHRRListDTOTable].Rows.Count > 0))
                {
                    HHRRListDTO[] hhrrs = hhrrListDTOAdapter.GetData(ds);
                    if ((hhrrs != null) && (hhrrs.Length > 0))
                        return hhrrs[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public Int64 GetHHRRTimeStamp(int hhrrID)
        {
            try
            {
                return _humanResourceDA.GetDBTimeStamp(hhrrID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

/*
                public HumanResourceEntity GetHHRR(int hhrrID)
                {
                    try
                    {
                        HumanResourceAdapter humanResourceAdapter = new HumanResourceAdapter();
                        PersonBL personBL = new PersonBL();

                        DataSet ds = _humanResourceDA.GetByID(hhrrID);
                        if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HumanResourceTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.HumanResourceTable].Rows.Count > 0))
                        {
                            int personID = SIIConvert.ToInteger(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.HumanResourceTable].Rows[0]["PersonID"].ToString(), 0);
                            //int profileID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["ProfileID"].ToString(), 0);

                            DataSet ds2;

                            #region Profiles
                            #region Human Resource Profile Rel
                            ds2 = _humanResourceProfileRelDA.GetHHRRProfileRelByHumanResource(hhrrID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HHRRProfileRelTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.HHRRProfileRelTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion

                            #region Profiles
                            ds2 = _profileDA.GetListProfileByHumanResourceID(hhrrID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ProfileTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ProfileTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            ds2 = _participateAsProfileRelDA.GetParticipateAsProfileRelsByHumanResourceID(hhrrID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ParticipateAsProfileRelTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ParticipateAsProfileRelTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            ds2 = _participateAsDA.GetParticipatesAsByHumanResourceID(hhrrID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ParticipateAsTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ParticipateAsTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion
                            #endregion

                            #region Devices
                            #region ResourceDeviceRel Table
                            ds2 = _resourceDeviceRelDA.GetResourceRelsByHumanResource(hhrrID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ResourceDeviceRelTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ResourceDeviceRelTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion

                            #region Devices By ResourceDeviceRel
                            ds2 = _deviceDA.GetByHumanResource(hhrrID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DeviceTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DeviceTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion

                            #region Device Types By Human Resource
                            ds2 = _deviceTypeDA.GetListDeviceTypeByHumanResource(hhrrID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DeviceTypeTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DeviceTypeTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion
                            #endregion

                            #region Avail Patterns
                            #region HHRR Avail Patterns
                            ds2 = _personAvailPatternDA.GetPersonAvailPattern(personID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAvailPatternTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonAvailPatternTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion

                            #region AvailPatterns
                            ds2 = _availPatternDA.GetAvailPatternsByPerson(personID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AvailPatternTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AvailPatternTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion

                            #region TimePatterns
                            ds2 = _timePatternDA.GetAvailabilityBaseByPerson(personID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AvailabilityTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AvailabilityTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion
                            #endregion

                            #region Care centers access
                            ds2 = _personCareCenterAccessDA.GetListPersonCareCenterAccessByPersonID(personID);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonCareCenterAccessTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonCareCenterAccessTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion

                            #region Person
                            if (personID <= 0)
                            {
                                throw new Exception(Properties.Resources.ERROR_HumanResourcePersonNotFound);
                            }
                            SII.HCD.BackOffice.Entities.PersonEntity myPerson = personBL.GetPerson(personID);
                            #endregion

                            HumanResourceEntity result = humanResourceAdapter.GetInfo(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.HumanResourceTable].Rows[0], ds);
                            result.Person = myPerson;
                            LOPDLogger.Write(EntityNames.HumanResourceEntityName, hhrrID, ActionType.View);
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
        public HumanResourceEntity GetHHRR(int hhrrID)
        {
            try
            {
                PersonBL personBL = new PersonBL();

                //Obtenemos el PersonId para poder lanzar la parte myPerson fuera del hilo principal
                int personID = personBL.obtenerPersonID_From_HumanResource(hhrrID);
                if (personID == 0) throw new Exception(Properties.Resources.ERROR_HumanResourcePersonNotFound);

                PersonEntity myPerson = null;

                var HiloPerson = System.Threading.Tasks.Task.Factory.StartNew(() => myPerson = personBL.GetPerson(personID));
                DataSet ds = _humanResourceDA.GetByID(hhrrID);

                if ((ds.Tables != null) && (ds.Tables.Contains(TableNames.HumanResourceTable)) && (ds.Tables[TableNames.HumanResourceTable].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());

                    HumanResourceAdapter humanResourceAdapter = new HumanResourceAdapter();   
                    HumanResourceEntity result = humanResourceAdapter.GetInfo(ds.Tables[TableNames.HumanResourceTable].Rows[0], ds2);
                    HiloPerson.Wait();
                    result.Person = myPerson;
                    LOPDLogger.Write(EntityNames.HumanResourceEntityName, hhrrID, ActionType.View);

                    ds2.Dispose();
                    ds.Dispose();
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
        public HumanResourceEntity GetHHRRByUserName(string userName)
        {
            try
            {
                try
                {
                    int hhrrID = _humanResourceDA.GetIDByUserName(userName);
                    if (hhrrID > 0)
                    {
                        return this.GetHHRR(hhrrID);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceEntity GetHHRRByFileNumber(string filenumber)
        {
            try
            {
                try
                {
                    int hhrrID = _humanResourceDA.GetIDByFileNumber(filenumber);
                    if (hhrrID > 0)
                    {
                        return this.GetHHRR(hhrrID);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceEntity GetHHRRByPersonID(int personID)
        {
            try
            {
                int hhrrID = _humanResourceDA.GetIDByPersonID(personID);
                if (hhrrID > 0)
                {
                    return this.GetHHRR(hhrrID);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceDTO[] GetHumanResources()
        {
            try
            {
                HumanResourceDTOAdapter hmAdapter = new HumanResourceDTOAdapter();

                DataSet ds = _humanResourceDA.GetHumanResources();
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HumanResourceDTOTable)))
                {
                    HumanResourceDTO[] humanResources = hmAdapter.GetData(ds);
                    return humanResources;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceDTO[] GetHumanResourcesByProfileID(int profileID)
        {
            try
            {
                HumanResourceDTOAdapter hmAdapter = new HumanResourceDTOAdapter();

                DataSet ds = _humanResourceDA.GetByProfile(profileID);
                if ((ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceDTOTable))
                    && (ds.Tables[BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows.Count > 0))
                {
                    return hmAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceWithFileNumberDTO[] GetHumanResourcesWithFileNumberByProfileID(int profileID)
        {
            try
            {
                DataSet ds = _humanResourceDA.GetWithFileNumberByProfile(profileID);
                if ((ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceDTOTable))
                    && (ds.Tables[BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows.Count > 0))
                {
                    HumanResourceWithFileNumberDTOAdvancedAdapter hmAdapter = new HumanResourceWithFileNumberDTOAdvancedAdapter();
                    return hmAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceDTO[] GetHumanResourcesWithAdmitNotificationByProfile(int profileID)
        {
            try
            {
                HumanResourceDTOAdapter hmAdapter = new HumanResourceDTOAdapter();

                DataSet ds = _humanResourceDA.GetByAdmitNotificationAndProfile(profileID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HumanResourceDTOTable)))
                {
                    HumanResourceDTO[] humanResources = hmAdapter.GetData(ds);
                    return humanResources;
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceDTO[] GetHHRRWithAdminNotification()
        {
            try
            {
                HumanResourceDTOAdapter hmAdapter = new HumanResourceDTOAdapter();

                DataSet ds = _humanResourceDA.GetByAdmitNotification();
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HumanResourceDTOTable)))
                {
                    HumanResourceDTO[] humanResources = hmAdapter.GetData(ds);
                    return humanResources;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceDTO[] GetHHRRWithDevice()
        {
            try
            {
                HumanResourceDTOAdapter hmAdapter = new HumanResourceDTOAdapter();

                DataSet ds = _humanResourceDA.GetByDevice();
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HumanResourceDTOTable)))
                {
                    HumanResourceDTO[] humanResources = hmAdapter.GetData(ds);
                    return humanResources;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public HumanResourceDTO[] GetHumanResourcesByUnifiedAppConfig(UnifiedAppointmentsConfigDTO unifiedAppConfig, int resourceTypeID)
        {
            try
            {
                if (unifiedAppConfig == null) throw new ArgumentNullException("unifiedAppConfig");
                HumanResourceDTOAdapter myAdapter = new HumanResourceDTOAdapter();

                if (unifiedAppConfig.UnifiedAppResourceRel != null)
                {
                    if (unifiedAppConfig.IsCitation)
                    {
                        DataSet ds = _humanResourceDA.GetHumanResourcesByCitationConfig(unifiedAppConfig.ID, resourceTypeID);
                        if ((ds.Tables != null) && (ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceDTOTable)) &&
                            (ds.Tables[BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows.Count > 0))
                        {
                            return myAdapter.GetData(ds);
                        }
                    }
                    else
                    {
                        DataSet ds = _humanResourceDA.GetHumanResourcesByWLConfig(unifiedAppConfig.ID, resourceTypeID);
                        if ((ds.Tables != null) && (ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceDTOTable)) &&
                            (ds.Tables[BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows.Count > 0))
                        {
                            return myAdapter.GetData(ds);
                        }
                    }
                    return null;
                }
                else return (resourceTypeID != 0) ? this.GetHumanResourcesByProfileID(resourceTypeID) : this.GetHumanResources();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        //PENDIENTE de ir complentando con las subentidades que vayan haciendo falta.
        //AHORA sólo se cargan Persons y Profiles que es lo que se está consultando por LINQ en las vistas.
        //Se ha eliminado la recursividad que hacía que el tiempo del servicio no fuera óptimo.(Está comentado el código en la region 'Old code')
        //public HumanResourceEntity[] GetHumanResourcesByCareCenter(int careCenterID, int[] profileIDs)
        //{
        //    if (careCenterID <= 0) return null;

        //    try
        //    {
        //        DataSet ds = _humanResourceDA.GetHumanResourcesByCareCenter(careCenterID, profileIDs);
        //        if ((ds != null)
        //            && (ds.Tables != null)
        //            && (ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceTable))
        //            && (ds.Tables[BackOffice.Entities.TableNames.HumanResourceTable].Rows.Count > 0))
        //        {
        //            DataSet ds2;
        //            #region Persons
        //            ds2 = _personDA.GetPersonsByCareCenter(careCenterID, profileIDs);
        //            if ((ds2 != null)
        //                && (ds2.Tables.Contains(BackOffice.Entities.TableNames.PersonTable)))
        //            {
        //                DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.PersonTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion
        //            #region Profiles
        //            ds2 = _humanResourceProfileRelDA.GetHHRRProfileRelsByCareCenter(careCenterID, profileIDs);
        //            if ((ds2 != null)
        //                && (ds2.Tables.Contains(BackOffice.Entities.TableNames.HHRRProfileRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.HHRRProfileRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            //ds2 = _profileDA.GetProfilesByCareCenter(careCenterID, profileIDs);
        //            //if ((ds2 != null)
        //            //    && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ProfileTable)))
        //            //{
        //            //    DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ProfileTable].Copy();
        //            //    ds.Tables.Add(dt);
        //            //}
        //            ds2 = _profileDA.GetProfiles();
        //            if ((ds2 != null)
        //                && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ProfileTable)))
        //            {
        //                DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ProfileTable].Copy();
        //                ds.Tables.Add(dt);
        //            }

        //            ds2 = _participateAsProfileRelDA.GetParticipateAsProfileRelsByCareCenter(careCenterID, profileIDs);
        //            if ((ds2 != null)
        //                && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ParticipateAsProfileRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ParticipateAsProfileRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            ds2 = _participateAsDA.GetParticipatesAsByCareCenter(careCenterID, profileIDs);
        //            if ((ds2 != null)
        //                && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ParticipateAsTable)))
        //            {
        //                DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ParticipateAsTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion
        //            HumanResourceAdvancedAdapter hhrraa = new HumanResourceAdvancedAdapter();
        //            return hhrraa.GetData(ds);
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

        //PENDIENTE de ir complentando con las subentidades que vayan haciendo falta.
        //AHORA sólo se cargan Persons y Profiles que es lo que se está consultando por LINQ en las vistas.
        public HumanResourceEntity[] GetHumanResourcesByCareCenter(int careCenterID, int[] profileIDs)
        {
            if (careCenterID <= 0) return null;

            try
            {
                DataSet ds = _humanResourceDA.GetHumanResourcesByCareCenter(careCenterID, profileIDs);
                if ((ds != null)
                    && (ds.Tables != null)
                    && (ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceTable))
                    && (ds.Tables[BackOffice.Entities.TableNames.HumanResourceTable].Rows.Count > 0))
                {
                    IEnumerable<int> hhrrIDs = ds.Tables[BackOffice.Entities.TableNames.HumanResourceTable].AsEnumerable()
                                                .Select(r => r.Field<int>("ID"))
                                                .Distinct()
                                                .OrderBy(i => i);

                    #region Persons
                    MergeTable(_personDA.GetPersonsByHHRR(hhrrIDs),
                               ds, BackOffice.Entities.TableNames.PersonTable);
                    #endregion

                    #region Profiles
                    MergeTable(_humanResourceProfileRelDA.GetHHRRProfileRelsByHHRR(hhrrIDs),
                               ds, BackOffice.Entities.TableNames.HHRRProfileRelTable);

                    MergeTable(_profileDA.GetProfilesByHHRR(hhrrIDs),
                               ds, BackOffice.Entities.TableNames.ProfileTable);

                    MergeTable(_participateAsProfileRelDA.GetParticipateAsProfileRelsByHHRR(hhrrIDs),
                               ds, BackOffice.Entities.TableNames.ParticipateAsProfileRelTable);

                    MergeTable(_participateAsDA.GetParticipatesAsByHHRR(hhrrIDs),
                               ds, BackOffice.Entities.TableNames.ParticipateAsTable);
                    #endregion

                    HumanResourceAdvancedAdapter hhrraa = new HumanResourceAdvancedAdapter();
                    return hhrraa.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        //Evaluar si se descomenta la llamada al servicio que tenemos justo encima con las subentidades que trae ahora mismo.
        public HumanResourceEntity[] GetHumanResourcesByProfilesAndCareCenter(int[] profileIDs, int careCenterID)
        {
            try
            {
                if (profileIDs == null || profileIDs.Length <= 0 || careCenterID <= 0) return null;

                return GetHumanResourcesByCareCenter(careCenterID, profileIDs); //Esta sólo traería los HumanResourceEntity con las subentidades: Person y Profiles

                //List<HumanResourceEntity> myHHRRs = new List<HumanResourceEntity>();
                //DataSet ds = _humanResourceDA.GetByProfilesAndCareCenter(profileIDs, careCenterID);
                //if ((ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceDTOTable))
                //    && (ds.Tables[BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows.Count > 0))
                //{
                //    foreach (DataRow row in ds.Tables[SII.HCD.BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows)
                //    {
                //        int hhrrID = row["HumanResourceID"] as int? ?? 0;
                //        if (hhrrID > 0)
                //        {
                //            HumanResourceEntity hhrr = this.GetHHRR(hhrrID);
                //            if (hhrr != null) myHHRRs.Add(hhrr);
                //        }
                //    }
                //    return (myHHRRs.Count > 0) ? myHHRRs.ToArray() : null;
                //}
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// ES UNA BL RECURSIVA HABRÍA QUE CAMBIARLA SI FUESE EL CASO
        /// </summary>
        public HumanResourceEntity[] GetHumanResourcesByAssistanceService(int assistanceServiceID, int careCenterID)
        {
            try
            {
                List<HumanResourceEntity> myHHRRs = new List<HumanResourceEntity>();
                DataSet ds = _humanResourceDA.GetByCareCenterAndAssistanceService(assistanceServiceID, careCenterID);
                if ((ds.Tables.Contains(BackOffice.Entities.TableNames.HumanResourceDTOTable))
                    && (ds.Tables[BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows.Count > 0))
                {
                    foreach (DataRow row in ds.Tables[SII.HCD.BackOffice.Entities.TableNames.HumanResourceDTOTable].Rows)
                    {
                        int hhrrID = row["HumanResourceID"] as int? ?? 0;
                        if (hhrrID > 0)
                        {
                            HumanResourceEntity hhrr = this.GetHHRR(hhrrID);
                            if (hhrr != null) myHHRRs.Add(hhrr);
                        }
                    }
                    return (myHHRRs.Count > 0) ? myHHRRs.ToArray() : null;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// Recupera todos los recursos humanos que son empleados con sus perfiles (es decir un recurso humano aparecera en el listado tantas veces como perfiles tenga asociado) y que tengan permisos para el centro selecionado.
        /// Los recursos humanos que no tengan perfil solo apareceran una vez y con ProfileID = 0 (Es decir sin perfil).
        /// </summary>
        /// <param name="careCenterID">Id del centro donde se realiza la rutina o procedimiento</param>
        /// <param name="element">Enumerado que indica si queremos HHRR de rutina o procedimientos</param>
        /// <param name="serviceID">Id de la rutina o procedimiento de los que queremos us HHRR</param>
        /// <returns>Listado de recurso humanos con sus perfiles definidos en las rutinas o procedimientos</returns>
        public HHRRListDTO[] GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDAppointmentElementServiceID(int careCenterID, AppointmentElementEnum element, int serviceID)
        {
            try
            {
                BackofficeConfigurationSection backOfficeConfig = FrameworkConfigurationService<BackofficeConfigurationSection>.GetSection("backoffice") as BackofficeConfigurationSection;
                string identifierTypeName = backOfficeConfig.EntitySettings.HumanResourceEntity.Attributes["Identifier.IdentifierType"].DefaultValue;
                IdentifierTypeDA _identifierTypeDA = new IdentifierTypeDA();
                int identifierTypeID = _identifierTypeDA.GetIdentifierTypeID(identifierTypeName);
                HHRRListDTOAdapter adapter = new HHRRListDTOAdapter();

                DataSet ds = null;
                switch (element)
                {
                    case AppointmentElementEnum.None: return this.GetHHRRsWithProfileByEmployeeIsActiveCareCenterID(careCenterID, identifierTypeID);
                    case AppointmentElementEnum.Procedure: ds = _humanResourceDA.GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDProcedureID(identifierTypeID, careCenterID, serviceID); break;
                    case AppointmentElementEnum.Routine: ds = _humanResourceDA.GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDRoutineID(identifierTypeID, careCenterID, serviceID); break;

                    //De momento no tienen sentido para estas entidades, ya que estas no tienen definido los recursos humanos directamente.
                    case AppointmentElementEnum.MedicalOrder: break;
                    case AppointmentElementEnum.CustomerProcedure: break;
                    case AppointmentElementEnum.CustomerRoutine: break;
                    default: break;
                }

                if ((ds.Tables != null) && ds.Tables.Contains(BackOffice.Entities.TableNames.HHRRListDTOTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.HHRRListDTOTable].Rows.Count > 0))
                {
                    return adapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CommonEntities.IDDescriptionEntity[] GetHHRRByProfiles(int[] profileIDs)
        {
            try
            {
                List<CommonEntities.IDDescriptionEntity> myHHRR = new List<CommonEntities.IDDescriptionEntity>();
                DataSet ds = null;
                ds = _humanResourceDA.GetIDDescriptionsByProfiles(profileIDs);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(CommonEntities.TableNames.IDDescriptionTable))
                    && (ds.Tables[CommonEntities.TableNames.IDDescriptionTable].Rows.Count > 0))
                {
                    foreach (DataRow row in ds.Tables[CommonEntities.TableNames.IDDescriptionTable].Rows)
                    {
                        myHHRR.Add(new CommonEntities.IDDescriptionEntity(SIIConvert.ToInteger(row["ID"].ToString(), 0),
                            CommonEntities.DescriptionBuilder.PersonBuildName(row["FirstName"].ToString(), row["LastName"].ToString(), row["LastName2"].ToString())));
                    }
                    return myHHRR.ToArray();
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public bool DeleteHHRRProfileIsPossible(HHRRProfileRelEntity hhrrProfile)
        {
            try
            {
                if (hhrrProfile == null || hhrrProfile.Profile == null) return false;
                return _humanResourceDA.DeleteHHRRProfileIsPossible(hhrrProfile.HumanResourceID, hhrrProfile.Profile.ID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return true;
            }
        }

        #endregion
    }
}
