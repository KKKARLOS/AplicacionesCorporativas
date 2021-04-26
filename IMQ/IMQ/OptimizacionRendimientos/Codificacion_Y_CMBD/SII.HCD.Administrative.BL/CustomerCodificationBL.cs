using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using SII.HCD.Addin.Entities;
using SII.HCD.Administrative.DA;
using SII.HCD.Administrative.Entities;
using SII.HCD.Administrative.Services;
using SII.HCD.BackOffice.BL;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Common.BL;
using SII.HCD.Common.DA;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

using SII.HCD.Common.Entities;


namespace SII.HCD.Administrative.BL
{
    public class CustomerCodificationBL : CustomerProcessBL, ICustomerCodificationService
    {
        #region Fields
        private CustomerCodificationDA _customerCodificationDA;
        private CodedDiagnosisDA _codedDiagnosisDA;
        private CodedDRGDA _codedDRGDA;
        private CodedMDCDA _codedMDCDA;
        private CodedMorphologyDA _codedMorphologyDA;
        private CodedProcedureDA _codedProcedureDA;
        private OtherCodedCategoryDA _otherCodedCategoryDA;
        private CustomerEpisodeServiceRelDA _customerEpisodeServiceRelDA;
        private RecordDeletedLogDA _recordDeletedLogDA;
        private CustomerProcessDA _customerProcessDA;

        private DiagnosisDA _diagnosisDA;
        private DRGDA _drgDA;
        private MDCDA _mdcDA;
        private MorphologyDA _morphologyDA;
        private EpisodeProcedureDA _procedureDA;
        private CodedCategoryDA _codedCategoryDA;
        private ConceptDA _conceptDA;
        private CustomerEpisodeDA _customerEpisodeDA;
        private CustomerAssistancePlanDA _customerAssistancePlanDA;

        private PersonBL _personBL = null;
        private CustomerEpisodeBL _episodeBL = null;
        private ElementBL _elementBL = null;
        private DeliveryNoteBL _deliveryNoteBL = null;
        private CustomerObservationBL _customerObservationBL = null;
        private ProcessChartBL _processChartBL = null;
        private DiagnosisBL _diagnosisBL = null;
        private EpisodeProcedureBL _episodeProcedureBL = null;
        private ConceptBL _conceptBL = null;
        #endregion

        #region properties
        private DeliveryNoteBL DeliveryNoteBL
        {
            get
            {
                if (_deliveryNoteBL == null) _deliveryNoteBL = new DeliveryNoteBL();
                return _deliveryNoteBL;
            }
        }
        private PersonBL PersonBL
        {
            get
            {
                if (_personBL == null) _personBL = new PersonBL();
                return _personBL;
            }
        }
        private CustomerEpisodeBL CustomerEpisodeBL
        {
            get
            {
                if (_episodeBL == null) _episodeBL = new CustomerEpisodeBL();
                return _episodeBL;
            }
        }
        private ElementBL ElementBL
        {
            get
            {
                if (_elementBL == null) _elementBL = new ElementBL();
                return _elementBL;
            }
        }
        private CustomerObservationBL CustomerObservationBL
        {
            get
            {
                if (_customerObservationBL == null) _customerObservationBL = new CustomerObservationBL();
                return _customerObservationBL;
            }
        }
        private ProcessChartBL ProcessChartBL
        {
            get
            {
                if (_processChartBL == null) _processChartBL = new ProcessChartBL();
                return _processChartBL;
            }
        }
        private DiagnosisBL DiagnosisBL
        {
            get
            {
                if (_diagnosisBL == null) _diagnosisBL = new DiagnosisBL();
                return _diagnosisBL;
            }
        }
        private EpisodeProcedureBL EpisodeProcedureBL
        {
            get
            {
                if (_episodeProcedureBL == null) _episodeProcedureBL = new EpisodeProcedureBL();
                return _episodeProcedureBL;
            }
        }
        private ConceptBL ConceptBL
        {
            get
            {
                if (_conceptBL == null) _conceptBL = new ConceptBL();
                return _conceptBL;
            }
        }
        #endregion

        #region Constructors
        public CustomerCodificationBL()
        {
            _customerCodificationDA = new CustomerCodificationDA();
            _codedDiagnosisDA = new CodedDiagnosisDA();
            _codedDRGDA = new CodedDRGDA();
            _codedMDCDA = new CodedMDCDA();
            _codedMorphologyDA = new CodedMorphologyDA();
            _codedProcedureDA = new CodedProcedureDA();
            _otherCodedCategoryDA = new OtherCodedCategoryDA();
            _customerEpisodeServiceRelDA = new CustomerEpisodeServiceRelDA();
            _recordDeletedLogDA = new RecordDeletedLogDA();
            _customerProcessDA = new CustomerProcessDA();

            _diagnosisDA = new DiagnosisDA();
            _drgDA = new DRGDA();
            _mdcDA = new MDCDA();
            _morphologyDA = new MorphologyDA();
            _procedureDA = new EpisodeProcedureDA();
            _codedCategoryDA = new CodedCategoryDA();
            _customerEpisodeDA = new CustomerEpisodeDA();
            _customerAssistancePlanDA = new CustomerAssistancePlanDA();

            _conceptDA = new ConceptDA();

        }
        #endregion

        #region Private methods

        //private string IdentityUser.GetIdentityUserName()
        //{
        //    if ((ServiceSecurityContext.Current != null) && (ServiceSecurityContext.Current.PrimaryIdentity != null))
        //        return ServiceSecurityContext.Current.PrimaryIdentity.Name;

        //    return string.Empty;
        //}

        protected virtual CommonEntities.ElementEntity GetElementByName(string entityName)
        {
            try
            {
                CommonEntities.ElementEntity result = ElementBL.GetElementByName(entityName);
                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private string GetDefaultIdentiferType()
        {
            CommonEntities.ElementEntity personIdentifierType = this.GetElementByName(CommonEntities.Constants.EntityNames.PersonIdentifierEntityName);
            CommonEntities.AttributeEntity identifierType = (personIdentifierType != null) ? personIdentifierType.GetAttribute("IdentifierType") : null;
            return (identifierType != null)
                ? identifierType.DefaultValue
                : string.Empty;
        }

        protected void ResetCustomerCodification(CustomerCodificationEntity customerCodification)
        {
            customerCodification.EditStatus.Reset();

            if (customerCodification.Diagnosis != null)
            {
                List<CodedDiagnosisEntity> diagnosiss = new List<CodedDiagnosisEntity>();
                foreach (CodedDiagnosisEntity diagnosis in customerCodification.Diagnosis)
                {
                    if (!((diagnosis.EditStatus.Value == StatusEntityValue.Deleted) || (diagnosis.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        diagnosis.EditStatus.Reset();
                        diagnosiss.Add(diagnosis);
                    }
                }
                customerCodification.Diagnosis = diagnosiss.ToArray();
            }

            if (customerCodification.MDCs != null)
            {
                List<CodedMDCEntity> MDCs = new List<CodedMDCEntity>();
                foreach (CodedMDCEntity mdc in customerCodification.MDCs)
                {
                    if (!((mdc.EditStatus.Value == StatusEntityValue.Deleted) || (mdc.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        mdc.EditStatus.Reset();
                        MDCs.Add(mdc);
                    }
                }
                customerCodification.MDCs = MDCs.ToArray();
            }

            if (customerCodification.DRGs != null)
            {
                List<CodedDRGEntity> DRGs = new List<CodedDRGEntity>();
                foreach (CodedDRGEntity drg in customerCodification.DRGs)
                {
                    if (!((drg.EditStatus.Value == StatusEntityValue.Deleted) || (drg.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        drg.EditStatus.Reset();
                        DRGs.Add(drg);
                    }
                }
                customerCodification.DRGs = DRGs.ToArray();
            }

            if (customerCodification.Morphologies != null)
            {
                List<CodedMorphologyEntity> Morphologies = new List<CodedMorphologyEntity>();
                foreach (CodedMorphologyEntity morphology in customerCodification.Morphologies)
                {
                    if (!((morphology.EditStatus.Value == StatusEntityValue.Deleted) || (morphology.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        morphology.EditStatus.Reset();
                        Morphologies.Add(morphology);
                    }
                }
                customerCodification.Morphologies = Morphologies.ToArray();
            }

            if (customerCodification.Procedures != null)
            {
                List<CodedProcedureEntity> Procedures = new List<CodedProcedureEntity>();
                foreach (CodedProcedureEntity procedure in customerCodification.Procedures)
                {
                    if (!((procedure.EditStatus.Value == StatusEntityValue.Deleted) || (procedure.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        procedure.EditStatus.Reset();
                        Procedures.Add(procedure);
                    }
                }
                customerCodification.Procedures = Procedures.ToArray();
            }

            if (customerCodification.OtherCategories != null)
            {
                List<OtherCodedCategoryEntity> Others = new List<OtherCodedCategoryEntity>();
                foreach (OtherCodedCategoryEntity other in customerCodification.OtherCategories)
                {
                    if (!((other.EditStatus.Value == StatusEntityValue.Deleted) || (other.EditStatus.Value == StatusEntityValue.NewAndDeleted)))
                    {
                        other.EditStatus.Reset();
                        Others.Add(other);
                    }
                }
                customerCodification.OtherCategories = Others.ToArray();
            }
        }

        private void Validate(CustomerCodificationEntity customerCodification, EpisodeTypeEntity episodeType)
        {
            if (customerCodification == null) throw new ArgumentNullException("customerCodification");

            CommonEntities.ElementEntity _customerCodificationMetadata = GetElementByName(EntityNames.CustomerCodificationEntityName);
            CustomerCodificationHelper customerCodificationHelper = new CustomerCodificationHelper(_customerCodificationMetadata);
            ValidationResults result = customerCodificationHelper.Validate(customerCodification);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }
                throw new Exception(
                    string.Format(Properties.Resources.ERROR_CustomerCodificationValidationError, sb));
            }
        }

        private void CheckDeletePreconditions(CustomerCodificationEntity customerCodification)
        {
            if (customerCodification == null) throw new ArgumentNullException("customerCodification");
        }

        private void CheckInsertPreconditions(CustomerCodificationEntity customerCodification, EpisodeTypeEntity episodeType)
        {
            if (customerCodification == null) throw new ArgumentNullException("customerCodification");
            Validate(customerCodification, episodeType);
        }

        private void CheckUpdatePreconditions(CustomerCodificationEntity customerCodification, EpisodeTypeEntity episodeType)
        {
            if (customerCodification == null) throw new ArgumentNullException("customerCodification");
            Validate(customerCodification, episodeType);
        }

        private CustomerCodificationEntity Insert(CustomerCodificationEntity customerCodification, CustomerProcessEntity customerProcess)
        {
            if (customerCodification == null) throw new ArgumentNullException("customerCodification");

            ProcessChartBL _processChartBL = new ProcessChartBL();
            ProcessChartEntity processChart = _processChartBL.GetByID(customerProcess.ProcessChartID);

            string userName = IdentityUser.GetIdentityUserName();
            PersonBaseEntity person = PersonBL.GetPersonByUserName(userName);
            int codifiedByID = 0;
            if (person != null)
            {
                codifiedByID = person.ID;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                customerCodification.ID = _customerCodificationDA.Insert(customerCodification.CustomerID, customerCodification.ProcessChartID,
                    customerCodification.CustomerEpisodeID, customerCodification.CaseMixGroupID,
                    customerCodification.CaseMixGroupConceptID,
                    customerCodification.EstimatedCost, customerCodification.RelatedWeight, customerCodification.RelatedCost,
                    customerCodification.Explanation, customerCodification.RegistrationDateTime, customerCodification.ConfirmedDate,
                    codifiedByID, customerCodification.ConfirmedByID,
                    customerCodification.Exported, customerCodification.ExportedDateTime, customerCodification.ExportedBy,
                    customerCodification.Status, userName);

                #region Update Customer Process
                //customerProcess.CurrentCodificationID = customerCodification.ID;
                //customerProcess.CodificationDate = customerCodification.RegistrationDateTime;
                //customerProcess.CodificationStatus = customerCodification.Status;
                //_customerProcessBL.InnerUpdate(customerProcess, processChart, userName);
                InnerUpdateCustomerProcess(customerCodification, processChart, customerProcess, userName);
                #endregion

                customerCodification.DBTimeStamp = _customerCodificationDA.GetDBTimeStamp(customerCodification.ID);

                if (customerCodification.Diagnosis != null)
                {
                    foreach (CodedDiagnosisEntity diagnosis in customerCodification.Diagnosis)
                    {
                        switch (diagnosis.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                break;
                            case StatusEntityValue.New:
                                diagnosis.ID = _codedDiagnosisDA.Insert(customerCodification.ID, diagnosis.DiagnosisID, diagnosis.ConceptID, diagnosis.PrimaryDiagnosis,
                                    diagnosis.EncodedExplanation, (int)diagnosis.PresentOnAdmission, (int)diagnosis.POAOrigin,
                                    diagnosis.RegistrationDateTime, codifiedByID, userName);
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
                }

                if (customerCodification.DRGs != null)
                {
                    foreach (CodedDRGEntity drg in customerCodification.DRGs)
                    {
                        switch (drg.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                break;
                            case StatusEntityValue.New:
                                drg.ID = _codedDRGDA.Insert(customerCodification.ID, drg.DRGID, drg.ConceptID, drg.PrimaryDRG, drg.EstimatedWeight, drg.EstimatedCost,
                                    drg.RelatedWeight, drg.RelatedCost, drg.EncodedExplanation, drg.RegistrationDateTime, codifiedByID, userName);
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
                }

                if (customerCodification.MDCs != null)
                {
                    foreach (CodedMDCEntity mdc in customerCodification.MDCs)
                    {
                        switch (mdc.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                break;
                            case StatusEntityValue.New:
                                mdc.ID = _codedMDCDA.Insert(customerCodification.ID, mdc.MDCID, mdc.ConceptID, mdc.PrimaryMDC, mdc.EncodedExplanation,
                                    mdc.RegistrationDateTime, codifiedByID, userName);
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
                }

                if (customerCodification.Morphologies != null)
                {
                    foreach (CodedMorphologyEntity morphology in customerCodification.Morphologies)
                    {
                        switch (morphology.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                break;
                            case StatusEntityValue.New:
                                morphology.ID = _codedMorphologyDA.Insert(customerCodification.ID, morphology.MorphologyID, morphology.ConceptID,
                                    morphology.EncodedExplanation, morphology.RegistrationDateTime, codifiedByID, userName);
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
                }

                if (customerCodification.Procedures != null)
                {
                    foreach (CodedProcedureEntity procedure in customerCodification.Procedures)
                    {
                        switch (procedure.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                break;
                            case StatusEntityValue.New:
                                procedure.ID = _codedProcedureDA.Insert(customerCodification.ID, procedure.ProcedureID, procedure.ConceptID, procedure.PrimaryProcedure,
                                    procedure.EncodedExplanation, procedure.RealizationDateTime, procedure.RegistrationDateTime, codifiedByID, userName);
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
                }

                if (customerCodification.OtherCategories != null)
                {
                    foreach (OtherCodedCategoryEntity other in customerCodification.OtherCategories)
                    {
                        switch (other.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                break;
                            case StatusEntityValue.New:
                                other.ID = _otherCodedCategoryDA.Insert(customerCodification.ID, other.CodedCategoryID, other.ConceptID,
                                    other.EncodedExplanation, other.RegistrationDateTime, codifiedByID, userName);
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
                }

                scope.Complete();
            }

            this.ResetCustomerCodification(customerCodification);
            LOPDLogger.Write(EntityNames.CustomerCodificationEntityName, customerCodification.ID, ActionType.Modify);
            return customerCodification;
        }

        private CustomerCodificationEntity Update(CustomerCodificationEntity customerCodification, CustomerProcessEntity customerProcess)
        {
            if (customerCodification == null) throw new ArgumentNullException("customerCodification");

            //Int64 dbTimeStamp = _customerCodificationDA.GetDBTimeStamp(customerCodification.ID);
            //if (dbTimeStamp != customerCodification.DBTimeStamp)
            //    throw new FaultException<DBConcurrencyException>(
            //        new DBConcurrencyException(string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customerCodification.ID)), string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customerCodification.ID));

            string userName = IdentityUser.GetIdentityUserName();
            PersonBaseEntity person = PersonBL.GetPersonByUserName(userName);

            ProcessChartBL _processChartBL = new ProcessChartBL();
            ProcessChartEntity processChart = _processChartBL.GetByID(customerProcess.ProcessChartID);

            int codifiedByID = 0;
            if (PersonBL != null)
            {
                codifiedByID = person.ID;
            }
            if ((customerCodification.ConfirmedByID == 0) && (customerCodification.Status == Common.Entities.StatusEnum.Confirmed))
            {
                if (person != null)
                {
                    customerCodification.ConfirmedByID = person.ID;
                }
            }

            using (TransactionScope scope = new TransactionScope())
            {
                _customerCodificationDA.Update(customerCodification.ID, customerCodification.CustomerID, customerCodification.ProcessChartID,
                                    customerCodification.CustomerEpisodeID, customerCodification.CaseMixGroupID,
                                    customerCodification.CaseMixGroupConceptID,
                                    customerCodification.EstimatedCost, customerCodification.RelatedWeight, customerCodification.RelatedCost,
                                    customerCodification.Explanation, customerCodification.RegistrationDateTime, customerCodification.ConfirmedDate,
                                    customerCodification.CodifiedByID, customerCodification.ConfirmedByID,
                                    customerCodification.Exported, customerCodification.ExportedDateTime, customerCodification.ExportedBy,
                                    customerCodification.Status, userName);

                customerCodification.DBTimeStamp = _customerCodificationDA.GetDBTimeStamp(customerCodification.ID);

                if (customerCodification.Diagnosis != null)
                {
                    foreach (CodedDiagnosisEntity diagnosis in customerCodification.Diagnosis)
                    {
                        switch (diagnosis.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                _codedDiagnosisDA.Delete(diagnosis.ID);
                                break;
                            case StatusEntityValue.New:
                                diagnosis.ID = _codedDiagnosisDA.Insert(customerCodification.ID, diagnosis.DiagnosisID, diagnosis.ConceptID, diagnosis.PrimaryDiagnosis,
                                    diagnosis.EncodedExplanation, (int)diagnosis.PresentOnAdmission, (int)diagnosis.POAOrigin,
                                    diagnosis.RegistrationDateTime, codifiedByID, userName);
                                break;
                            case StatusEntityValue.NewAndDeleted:
                                break;
                            case StatusEntityValue.None:
                                break;
                            case StatusEntityValue.Updated:
                                _codedDiagnosisDA.Update(diagnosis.ID, customerCodification.ID, diagnosis.DiagnosisID, diagnosis.ConceptID, diagnosis.PrimaryDiagnosis,
                                    diagnosis.EncodedExplanation, (int)diagnosis.PresentOnAdmission, (int)diagnosis.POAOrigin,
                                    diagnosis.RegistrationDateTime, diagnosis.CodifiedByID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (customerCodification.DRGs != null)
                {
                    foreach (CodedDRGEntity drg in customerCodification.DRGs)
                    {
                        switch (drg.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                _codedDRGDA.Delete(drg.ID);
                                break;
                            case StatusEntityValue.New:
                                drg.ID = _codedDRGDA.Insert(customerCodification.ID, drg.DRGID, drg.ConceptID, drg.PrimaryDRG, drg.EstimatedWeight, drg.EstimatedCost,
                                    drg.RelatedWeight, drg.RelatedCost, drg.EncodedExplanation, drg.RegistrationDateTime, codifiedByID, userName);
                                break;
                            case StatusEntityValue.NewAndDeleted:
                                break;
                            case StatusEntityValue.None:
                                break;
                            case StatusEntityValue.Updated:
                                _codedDRGDA.Update(drg.ID, customerCodification.ID, drg.DRGID, drg.ConceptID, drg.PrimaryDRG, drg.EstimatedWeight, drg.EstimatedCost,
                                    drg.RelatedWeight, drg.RelatedCost, drg.EncodedExplanation, drg.RegistrationDateTime, drg.CodifiedByID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (customerCodification.MDCs != null)
                {
                    foreach (CodedMDCEntity mdc in customerCodification.MDCs)
                    {
                        switch (mdc.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                _codedMDCDA.Delete(mdc.ID);
                                break;
                            case StatusEntityValue.New:
                                mdc.ID = _codedMDCDA.Insert(customerCodification.ID, mdc.MDCID, mdc.ConceptID, mdc.PrimaryMDC, mdc.EncodedExplanation,
                                    mdc.RegistrationDateTime, codifiedByID, userName);
                                break;
                            case StatusEntityValue.NewAndDeleted:
                                break;
                            case StatusEntityValue.None:
                                break;
                            case StatusEntityValue.Updated:
                                _codedMDCDA.Update(mdc.ID, customerCodification.ID, mdc.MDCID, mdc.ConceptID, mdc.PrimaryMDC, mdc.EncodedExplanation,
                                    mdc.RegistrationDateTime, mdc.CodifiedByID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (customerCodification.Morphologies != null)
                {
                    foreach (CodedMorphologyEntity morphology in customerCodification.Morphologies)
                    {
                        switch (morphology.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                _codedMorphologyDA.Delete(morphology.ID);
                                break;
                            case StatusEntityValue.New:
                                morphology.ID = _codedMorphologyDA.Insert(customerCodification.ID, morphology.MorphologyID, morphology.ConceptID,
                                    morphology.EncodedExplanation, morphology.RegistrationDateTime, codifiedByID, userName);
                                break;
                            case StatusEntityValue.NewAndDeleted:
                                break;
                            case StatusEntityValue.None:
                                break;
                            case StatusEntityValue.Updated:
                                morphology.ID = _codedMorphologyDA.Update(morphology.ID, customerCodification.ID, morphology.MorphologyID, morphology.ConceptID,
                                    morphology.EncodedExplanation, morphology.RegistrationDateTime, morphology.CodifiedByID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (customerCodification.Procedures != null)
                {
                    foreach (CodedProcedureEntity procedure in customerCodification.Procedures)
                    {
                        switch (procedure.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                _codedProcedureDA.Delete(procedure.ID);
                                break;
                            case StatusEntityValue.New:
                                procedure.ID = _codedProcedureDA.Insert(customerCodification.ID, procedure.ProcedureID, procedure.ConceptID, procedure.PrimaryProcedure,
                                    procedure.EncodedExplanation, procedure.RealizationDateTime, procedure.RegistrationDateTime, codifiedByID, userName);
                                break;
                            case StatusEntityValue.NewAndDeleted:
                                break;
                            case StatusEntityValue.None:
                                break;
                            case StatusEntityValue.Updated:
                                _codedProcedureDA.Update(procedure.ID, customerCodification.ID, procedure.ProcedureID, procedure.ConceptID, procedure.PrimaryProcedure,
                                    procedure.EncodedExplanation, procedure.RealizationDateTime, procedure.RegistrationDateTime, procedure.CodifiedByID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (customerCodification.OtherCategories != null)
                {
                    foreach (OtherCodedCategoryEntity other in customerCodification.OtherCategories)
                    {
                        switch (other.EditStatus.Value)
                        {
                            case StatusEntityValue.Deleted:
                                _otherCodedCategoryDA.Delete(other.ID);
                                break;
                            case StatusEntityValue.New:
                                other.ID = _otherCodedCategoryDA.Insert(customerCodification.ID, other.CodedCategoryID, other.ConceptID,
                                    other.EncodedExplanation, other.RegistrationDateTime, codifiedByID, userName);
                                break;
                            case StatusEntityValue.NewAndDeleted:
                                break;
                            case StatusEntityValue.None:
                                break;
                            case StatusEntityValue.Updated:
                                _otherCodedCategoryDA.Update(other.ID, customerCodification.ID, other.CodedCategoryID, other.ConceptID,
                                    other.EncodedExplanation, other.RegistrationDateTime, other.CodifiedByID, userName);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (customerProcess.AvailableProcessSteps > (long)BasicProcessStepsEnum.Codification && customerCodification.Diagnosis == null)
                {
                    this.InnerUpdateCustomerProcessUndoCodification(customerCodification, processChart, customerProcess, userName);
                }
                else
                {
                    this.InnerUpdateCustomerProcess(customerCodification, processChart, customerProcess, userName);
                }

                scope.Complete();
            }

            this.ResetCustomerCodification(customerCodification);
            LOPDLogger.Write(EntityNames.CustomerCodificationEntityName, customerCodification.ID, ActionType.Modify);
            return customerCodification;
        }

        private CustomerCodificationEntity Delete(CustomerCodificationEntity customerCodification)
        {
            if (customerCodification == null) throw new ArgumentNullException("customerCodification");

            string userName = IdentityUser.GetIdentityUserName();
            using (TransactionScope scope = new TransactionScope())
            {
                scope.Complete();
                return customerCodification;
            }
        }

        private void InnerUpdateCustomerProcess(CustomerCodificationEntity customerCodification,
            ProcessChartEntity processChart, CustomerProcessEntity activeCustomerProcess, string userName)
        {
            if (activeCustomerProcess == null)
                throw new ArgumentNullException("activeCustomerProcess");

            DateTime? stepDateTime = customerCodification.RegistrationDateTime;
            base.InnerSaveBasicProcessStep(activeCustomerProcess.ID, BasicProcessStepsEnum.Codification, customerCodification.ID,
                customerCodification.Status, stepDateTime, null, userName);
            activeCustomerProcess = base.RefreshStepCustomerProcess(activeCustomerProcess);
            long availableProcessSteps = base.InnerGetAvailableProcessSteps(activeCustomerProcess, processChart) | (long)BasicProcessStepsEnum.Reports;
            _customerProcessDA.UpdateAvailableProcessStep(activeCustomerProcess.ID, availableProcessSteps, userName);
        }

        private void InnerUpdateCustomerProcessUndoCodification(CustomerCodificationEntity customerCodification,
            ProcessChartEntity processChart, CustomerProcessEntity activeCustomerProcess, string userName)
        {
            if (activeCustomerProcess == null)
                throw new ArgumentNullException("activeCustomerProcess");

            DateTime? stepDateTime = customerCodification.RegistrationDateTime;
            base.InnerSaveBasicProcessStep(activeCustomerProcess.ID, BasicProcessStepsEnum.Codification, customerCodification.ID,
                0, null, null, userName);
            activeCustomerProcess = base.RefreshStepCustomerProcess(activeCustomerProcess);
            long availableProcessSteps = base.InnerGetAvailableProcessSteps(activeCustomerProcess, processChart) | (long)BasicProcessStepsEnum.Reports;
            _customerProcessDA.UpdateAvailableProcessStep(activeCustomerProcess.ID, 12118080, userName);
        }

        //private void CheckPreconditions(CustomerCodificationEntity customerCodification)
        //{
        //    if (customerCodification == null)
        //        throw new ArgumentNullException("customerCodification");

        //    switch (customerCodification.EditStatus.Value)
        //    {
        //        case StatusEntityValue.New:
        //            CheckInsertPreconditions(customerCodification);
        //            break;
        //        case StatusEntityValue.Updated:
        //            CheckUpdatePreconditions(customerCodification);
        //            break;
        //        case StatusEntityValue.Deleted:
        //            CheckDeletePreconditions(customerCodification);
        //            break;
        //    }
        //}
        #endregion

        #region ICustomerCodificationService members
        public CustomerCodificationEntity GetCustomerCodificationByCustomerEpisode(int customerEpisodeID)
        {
            try
            {
                DataSet ds = _customerCodificationDA.GetCustomerCodificationByCustomerEpisode(customerEpisodeID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows.Count > 0))
                {
                    int customerCodificationID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows[0]["ID"].ToString(), 0);

                    DataSet ds2;

                    #region Coded Diagnosis
                    ds2 = _codedDiagnosisDA.GetCodedDiagnosisByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _diagnosisDA.GetDiagnosisByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesDiagnosisByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded DRG
                    ds2 = _codedDRGDA.GetCodedDRGByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDRGTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDRGTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _drgDA.GetDRGByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DRGTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DRGTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesDRGByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded MDC
                    ds2 = _codedMDCDA.GetCodedMDCByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMDCTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMDCTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _mdcDA.GetMDCByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDCTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDCTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesMDCByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded Morphology
                    ds2 = _codedMorphologyDA.GetCodedMorphologyByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _morphologyDA.GetMorphologyByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesMorphologyByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded Procedure
                    ds2 = _codedProcedureDA.GetCodedProcedureByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _procedureDA.GetEpisodeProcedureByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesProcedureByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Other Coded Category
                    ds2 = _otherCodedCategoryDA.GetOtherCodedCategoryByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    ds2 = _codedCategoryDA.GetCodedCategoryByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesOtherCategoryByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Customer Episode Service relation
                    ds2 = _customerEpisodeServiceRelDA.GetAssistanceServiceByCustomerEpisodeIDAndStep(customerEpisodeID, (Int64)BasicProcessStepsEnum.Leave);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    CustomerCodificationAdvancedAdapter customerCodificationAdapter = new CustomerCodificationAdvancedAdapter();

                    CustomerCodificationEntity[] customerCodifications = customerCodificationAdapter.GetData(ds);
                    CustomerCodificationHelper ccHeleper = new CustomerCodificationHelper(GetElementByName(CommonEntities.Constants.EntityNames.CustomerCodificationEntityName));
                    CustomerCodificationEntity result = ccHeleper.Create();
                    if ((customerCodifications != null) && (customerCodifications.Length > 0))
                    {
                        result = customerCodifications[0];
                    }

                    LOPDLogger.Write(EntityNames.CustomerCodificationEntityName, result.ID, ActionType.View);

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

        public CustomerCodificationEntity GetCustomerCodification(int customerCodificationID)
        {
            try
            {

                DataSet ds = _customerCodificationDA.Get(customerCodificationID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable)) && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows.Count > 0))
                {
                    int customerEpisodeID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows[0]["CustomerEpisodeID"].ToString(), 0);
                    DataSet ds2;

                    #region Coded Diagnosis
                    ds2 = _codedDiagnosisDA.GetCodedDiagnosisByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _diagnosisDA.GetDiagnosisByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesDiagnosisByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded DRG
                    ds2 = _codedDRGDA.GetCodedDRGByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDRGTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDRGTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _drgDA.GetDRGByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DRGTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DRGTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesDRGByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded MDC
                    ds2 = _codedMDCDA.GetCodedMDCByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMDCTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMDCTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _mdcDA.GetMDCByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDCTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDCTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesMDCByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded Morphology
                    ds2 = _codedMorphologyDA.GetCodedMorphologyByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _morphologyDA.GetMorphologyByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesMorphologyByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded Procedure
                    ds2 = _codedProcedureDA.GetCodedProcedureByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _procedureDA.GetEpisodeProcedureByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesProcedureByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Other Coded Category
                    ds2 = _otherCodedCategoryDA.GetOtherCodedCategoryByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    ds2 = _codedCategoryDA.GetCodedCategoryByCustomerCodification(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesOtherCategoryByCustomerCodificationID(customerCodificationID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Customer Episode Service relation
                    ds2 = _customerEpisodeServiceRelDA.GetAssistanceServiceByCustomerEpisodeIDAndStep(customerEpisodeID, (Int64)BasicProcessStepsEnum.Leave);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    CustomerCodificationAdvancedAdapter customerCodificationAdapter = new CustomerCodificationAdvancedAdapter();

                    CustomerCodificationEntity[] customerCodifications = customerCodificationAdapter.GetData(ds);
                    CustomerCodificationHelper ccHeleper = new CustomerCodificationHelper(GetElementByName(CommonEntities.Constants.EntityNames.CustomerCodificationEntityName));
                    CustomerCodificationEntity result = ccHeleper.Create();
                    if ((customerCodifications != null) && (customerCodifications.Length > 0))
                    {
                        result = customerCodifications[0];
                    }

                    LOPDLogger.Write(EntityNames.CustomerCodificationEntityName, result.ID, ActionType.View);

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

        public CustomerCodificationEntity[] GetCustomerCodificationToConfirm(int[] customerCodificationIDs)
        {
            try
            {
                if (customerCodificationIDs == null
                    || customerCodificationIDs.Length == 0) return null;

                DataSet ds = _customerCodificationDA.GetByIDs(customerCodificationIDs);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows.Count > 0))
                {
                    DataSet ds2;
                    #region Coded Diagnosis
                    ds2 = _codedDiagnosisDA.GetCodedDiagnosisByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _diagnosisDA.GetDiagnosisByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesDiagnosisByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    //#region Coded DRG
                    //ds2 = _codedDRGDA.GetCodedDRGByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDRGTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDRGTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _drgDA.GetDRGByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DRGTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DRGTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _conceptDA.GetConceptBasesDRGByCustomerCodificationID(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Coded MDC
                    //ds2 = _codedMDCDA.GetCodedMDCByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMDCTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMDCTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _mdcDA.GetMDCByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDCTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDCTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _conceptDA.GetConceptBasesMDCByCustomerCodificationID(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Coded Morphology
                    //ds2 = _codedMorphologyDA.GetCodedMorphologyByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _morphologyDA.GetMorphologyByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MorphologyTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MorphologyTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _conceptDA.GetConceptBasesMorphologyByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Coded Procedure
                    //ds2 = _codedProcedureDA.GetCodedProcedureByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _procedureDA.GetEpisodeProcedureByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeProcedureTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeProcedureTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _conceptDA.GetConceptBasesProcedureByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Other Coded Category
                    //ds2 = _otherCodedCategoryDA.GetOtherCodedCategoryByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //ds2 = _codedCategoryDA.GetCodedCategoryByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _conceptDA.GetConceptBasesOtherCategoryByCustomerCodificationIDs(customerCodificationIDs);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Customer Episode Service relation
                    //ds2 = _customerEpisodeServiceRelDA.GetAssistanceServiceByCustomerEpisodeIDAndStep(customerEpisodeID, (Int64)BasicProcessStepsEnum.Leave);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    CustomerCodificationAdvancedAdapter customerCodificationAdapter = new CustomerCodificationAdvancedAdapter();

                    return customerCodificationAdapter.GetData(ds);
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

        public CustomerCodificationEntity[] GetCustomerCodificationByCustomerID(int customerID)
        {
            try
            {
                DataSet ds = _customerCodificationDA.GetCustomerCodificationByCustomerID(customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows.Count > 0))
                {
                    int[] customercodificationIDs = (from row in ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].AsEnumerable()
                                                     where (row["ID"] as int? ?? 0) > 0
                                                     select (row["ID"] as int? ?? 0)).ToArray();
                    return GetCustomerCodificationForProcessChats(customercodificationIDs);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        //de uso interno
        public CustomerCodificationEntity[] GetCustomerCodificationForProcessChats(int[] customerCodificationIDs)
        {
            try
            {

                DataSet ds = _customerCodificationDA.GetByIDs(customerCodificationIDs);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows.Count > 0))
                {
                    DataSet ds2;
                    #region Coded Diagnosis
                    ds2 = _codedDiagnosisDA.GetCodedDiagnosisByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _diagnosisDA.GetDiagnosisByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesDiagnosisByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDiagnosisTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    //#region Coded DRG
                    //ds2 = _codedDRGDA.GetCodedDRGByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedDRGTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedDRGTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _drgDA.GetDRGByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.DRGTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.DRGTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _conceptDA.GetConceptBasesDRGByCustomerCodificationID(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptDRGTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Coded MDC
                    //ds2 = _codedMDCDA.GetCodedMDCByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMDCTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMDCTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _mdcDA.GetMDCByCustomerCodification(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDCTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDCTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //ds2 = _conceptDA.GetConceptBasesMDCByCustomerCodificationID(customerCodificationID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMDCTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    #region Coded Morphology
                    ds2 = _codedMorphologyDA.GetCodedMorphologyByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedMorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _morphologyDA.GetMorphologyByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesMorphologyByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptMorphologyTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Coded Procedure
                    ds2 = _codedProcedureDA.GetCodedProcedureByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CodedProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _procedureDA.GetEpisodeProcedureByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesProcedureByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptProcedureTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Other Coded Category
                    ds2 = _otherCodedCategoryDA.GetOtherCodedCategoryByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OtherCodedCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    ds2 = _codedCategoryDA.GetCodedCategoryByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.CodedCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }

                    ds2 = _conceptDA.GetConceptBasesOtherCategoryByCustomerCodificationIDs(customerCodificationIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptOtherCategoryTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    //#region Customer Episode Service relation
                    //ds2 = _customerEpisodeServiceRelDA.GetAssistanceServiceByCustomerEpisodeIDAndStep(customerEpisodeID, (Int64)BasicProcessStepsEnum.Leave);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    CustomerCodificationAdvancedAdapter customerCodificationAdapter = new CustomerCodificationAdvancedAdapter();

                    return customerCodificationAdapter.GetData(ds);
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


        public CustomerCodificationEntity Save(CustomerCodificationEntity customerCodification, CustomerProcessEntity customerProcess)
        {
            try
            {
                if (customerCodification == null)
                    throw new ArgumentNullException("customerCodification");

                CustomerEpisodeEntity episode = CustomerEpisodeBL.GetFullCustomerEpisode(customerCodification.CustomerEpisodeID);
                if (episode == null) throw new ArgumentNullException("customerEpisode");

                EpisodeTypeBL _episodeTypeBL = new EpisodeTypeBL();
                EpisodeTypeEntity episodeType = _episodeTypeBL.GetEpisodeType(episode.EpisodeTypeID);

                switch (customerCodification.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        return customerCodification;
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(customerCodification, episodeType);
                        return this.Insert(customerCodification, customerProcess);
                    case StatusEntityValue.NewAndDeleted:
                        return customerCodification;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(customerCodification, episodeType);
                        return this.Update(customerCodification, customerProcess);
                    case StatusEntityValue.None:
                        return customerCodification;
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

        public CustomerCodificationEntity AnalizeCreateNewEpisodeCodificationByEpisode(int customerEpisodeID, out ValidationResults validationResults)
        {
            validationResults = null;

            try
            {
                if (customerEpisodeID <= 0) return null;
                int customerProcessID = _customerProcessDA.GetCustomerProcessID(customerEpisodeID);
                return AnalizeCreateNewEpisodeCodification(customerProcessID, out validationResults);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        #region obtencion de CodedDiagnosisEntity
        private CodedDiagnosisEntity GetPrivateCodedDiagnosis(ConceptEntity concept)
        {
            if (concept == null) return null;
            DiagnosisEntity diagnosis = null;
            diagnosis = DiagnosisBL.Get(DiagnosisBL.GetIDByName(concept.Code));  //dice name pero es CODE. hat que modificarlo en las BL
            if (diagnosis == null)
            {
                //aqui registro el nuevo diagnosis
                diagnosis = new DiagnosisEntity(0, concept.Code, concept.Name, concept.Description, concept.Version, concept.Status, DateTime.Now, concept.ModifiedBy, 0);
                diagnosis.EditStatus.New();
                diagnosis = DiagnosisBL.Save(diagnosis);
            }
            CodedDiagnosisHelper cdHelper = new CodedDiagnosisHelper(GetElementByName(CommonEntities.Constants.EntityNames.CodedDiagnosisEntityName));
            CodedDiagnosisEntity codeDiag = cdHelper.Create();
            codeDiag.Diagnosis = diagnosis;
            codeDiag.DiagnosisID = diagnosis.ID;
            codeDiag.EditStatus.New();
            return codeDiag;
        }

        private CodedDiagnosisEntity GetCodedDiagnosis(string code)
        {
            if (string.IsNullOrEmpty(code)) return null;
            DiagnosisEntity diagnosis = null;
            diagnosis = DiagnosisBL.Get(DiagnosisBL.GetIDByName(code));  //dice name pero es CODE. hat que modificarlo en las BL
            if (diagnosis == null) return null;

            CodedDiagnosisHelper cdHelper = new CodedDiagnosisHelper(GetElementByName(CommonEntities.Constants.EntityNames.CodedDiagnosisEntityName));
            CodedDiagnosisEntity codeDiag = cdHelper.Create();
            codeDiag.Diagnosis = diagnosis;
            codeDiag.DiagnosisID = diagnosis.ID;
            codeDiag.EditStatus.New();
            return codeDiag;
        }

        private CodedDiagnosisEntity GetCodedDiagnosis(ConceptEntity concept)
        {
            if (concept == null) return null;
            CodedDiagnosisHelper cdHelper = new CodedDiagnosisHelper(GetElementByName(CommonEntities.Constants.EntityNames.CodedDiagnosisEntityName));
            CodedDiagnosisEntity codeDiag = cdHelper.Create();
            codeDiag.Concept = concept;
            codeDiag.ConceptID = concept.ID;
            codeDiag.EditStatus.New();
            return codeDiag;
        }

        private CodedDiagnosisEntity GetCodedDiagnosis(int id, ObservationOptionEntity[] observationOptions)
        {
            if (id <= 0 || observationOptions == null || observationOptions.Length <= 0) return null;
            ObservationOptionEntity codeopt = observationOptions.FirstOrDefault(opt => opt.ID == id);
            if (codeopt == null) return null;
            return GetCodedDiagnosis(codeopt.Value);
        }

        private CodedDiagnosisEntity GetCodedDiagnosis(int id, ObservationEncodingEntity observationEncoding, bool privateCodes)
        {
            if (id <= 0 || observationEncoding == null || observationEncoding.ObservationConcepts == null
                || observationEncoding.ObservationConcepts.Length <= 0) return null;
            ObservationConceptRelEntity codeopt = observationEncoding.ObservationConcepts.FirstOrDefault(opt => opt.Concept != null && opt.Concept.ID == id);
            if (codeopt == null || codeopt.Concept == null) return null;
            if (privateCodes)
                return GetPrivateCodedDiagnosis(codeopt.Concept);
            else
                return GetCodedDiagnosis(codeopt.Concept);
        }

        private CodedDiagnosisEntity[] GetCodedDiagnosis(int numbers, ExtObservationValueEntity extObservationValue,
            ObservationEncodingEntity observationEncoding, bool privateCodes)
        {
            if (numbers <= 0 || extObservationValue == null || extObservationValue.Value == null ||
                extObservationValue.Value.Length <= 0 || observationEncoding == null ||
                observationEncoding.ObservationConcepts == null || observationEncoding.ObservationConcepts.Length <= 0) return null;
            //aqui tengo que desglosar los multiples y por cada uno devolver un CodedDiagnosisEntity
            FakeTupleSerializer target = new FakeTupleSerializer();
            FakeTupleDTO[] multiples = target.Deserialize(extObservationValue.Value, numbers);
            if (multiples == null || multiples.Length <= 0) return null;
            List<CodedDiagnosisEntity> codes = new List<CodedDiagnosisEntity>();
            CodedDiagnosisEntity dianogsis = null;
            foreach (FakeTupleDTO ft in multiples)
            {
                ObservationConceptRelEntity ocr = observationEncoding.ObservationConcepts.FirstOrDefault(op => op.Concept != null && op.Concept.ID == ft.Item2);
                if (ocr != null)
                {

                    if (privateCodes)
                        dianogsis = GetPrivateCodedDiagnosis(ocr.Concept);
                    else
                        dianogsis = GetCodedDiagnosis(ocr.Concept);
                    if (dianogsis != null)
                        codes.Add(dianogsis);
                }
            }
            return codes.Count > 0 ? codes.ToArray() : null;
        }

        private POAOriginEnum GetPOAOrigin(RegisteredObservationValueEntity rov)
        {
            if (rov == null || rov.EpisodeID <= 0) return POAOriginEnum.None;
            SII.HCD.BackOffice.Entities.EpisodeCaseEnum episodeCase = _customerEpisodeDA.GetEpisodeCase(rov.EpisodeID);
            switch (episodeCase)
            {
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.RoutineOutPatient:
                    return POAOriginEnum.MedicalConsultation;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.EmergencyOutPatient:
                    return POAOriginEnum.PreviousEmergency;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.InPatient:
                    return POAOriginEnum.PreviousHospitalization;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.DayTreatment:
                    return POAOriginEnum.PreviousSurgicalDayTreatment;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.AtHome:
                    return POAOriginEnum.PrimaryCare;
                default:
                    return POAOriginEnum.None;
            }
        }

        private POAOriginEnum GetPOAOrigin(int episodeID)
        {
            if (episodeID <= 0) return POAOriginEnum.None;
            SII.HCD.BackOffice.Entities.EpisodeCaseEnum episodeCase = _customerEpisodeDA.GetEpisodeCase(episodeID);
            switch (episodeCase)
            {
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.RoutineOutPatient:
                    return POAOriginEnum.MedicalConsultation;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.EmergencyOutPatient:
                    return POAOriginEnum.PreviousEmergency;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.InPatient:
                    return POAOriginEnum.PreviousHospitalization;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.DayTreatment:
                    return POAOriginEnum.PreviousSurgicalDayTreatment;
                case SII.HCD.BackOffice.Entities.EpisodeCaseEnum.AtHome:
                    return POAOriginEnum.PrimaryCare;
                default:
                    return POAOriginEnum.None;
            }
        }

        private void AddToList(CodedDiagnosisEntity cde, List<CodedDiagnosisEntity> codedDiagnosisList, RegisteredObservationValueEntity episoderov,
            CustomerCodificationEntity[] previousCC)
        {
            if (codedDiagnosisList == null) return;
            if (cde != null && (codedDiagnosisList.Count <= 0 ||
                !Array.Exists(codedDiagnosisList.ToArray(), cd => cd.AssignedCode == cde.AssignedCode)))
            {
                if (episoderov != null)
                {
                    cde.PresentOnAdmission = PresentOnAdmissionEnum.Yes;
                    cde.POAOrigin = GetPOAOrigin(episoderov);
                }
                if (previousCC != null && previousCC.Any(cc => cc.Diagnosis != null && cc.Diagnosis.Any(d => d.AssignedCode == cde.AssignedCode)))
                {
                    CustomerCodificationEntity ccod = previousCC.FirstOrDefault(cc => cc.Diagnosis != null && cc.Diagnosis.Any(d => d.AssignedCode == cde.AssignedCode));
                    int episodeID = (codedDiagnosisList != null) ? ccod.CustomerEpisodeID : 0;
                    cde.PresentOnAdmission = PresentOnAdmissionEnum.Yes;
                    cde.POAOrigin = GetPOAOrigin(episodeID);
                }
                codedDiagnosisList.Add(cde);
            }
        }

        private CodedDiagnosisEntity[] GetCodedDiagnosis(RegisteredObservationValueEntity[] rovs, int customerEpisodeID,
            bool privateCodes, int customerID)
        {
            if (rovs == null || rovs.Length <= 0 || customerEpisodeID <= 0) return null;
            List<CodedDiagnosisEntity> codedDiagnosisList = new List<CodedDiagnosisEntity>();
            //separo en dos grupos
            //las del episodio y las otras
            RegisteredObservationValueEntity[] episoderovs = (from rov in rovs
                                                              where rov.EpisodeID == customerEpisodeID
                                                                  && rov.Observation != null
                                                              select rov).ToArray();
            if (episoderovs == null || episoderovs.Length <= 0) return null;
            //las previas iguales a las del episodio
            RegisteredObservationValueEntity[] noepisoderovs = (from rov in rovs
                                                                where rov.EpisodeID != customerEpisodeID
                                                                    && rov.Observation != null
                                                                    && Array.Exists(episoderovs, rv => rv.ObservationID == rov.ObservationID)
                                                                select rov).ToArray();
            CustomerCodificationEntity[] previousCC = this.GetCustomerCodificationByCustomerID(customerID);
            //aqui analizo los diagnosis
            foreach (RegisteredObservationValueEntity rov in episoderovs)
            {
                RegisteredObservationValueEntity episoderov = (noepisoderovs != null && noepisoderovs.Length > 0)
                    ? noepisoderovs.FirstOrDefault(rv => rv.ObservationID == rov.ObservationID)
                    : null;

                if (rov.Observation.KindOf == ObservationTypeEnum.GenericObservation && rov.Observation.BasicType == BasicObservationTypeEnum.String)
                {
                    AddToList(GetCodedDiagnosis(rov.ObservationValue.StValue), codedDiagnosisList, episoderov, previousCC);
                }
                if (rov.Observation.KindOf == ObservationTypeEnum.CodeObservation && rov.ObservationValue.IntValue != null)
                {
                    AddToList(GetCodedDiagnosis(rov.ObservationValue.IntValue.Value, rov.Observation.Options), codedDiagnosisList, episoderov, previousCC);
                }
                if (rov.Observation.KindOf == ObservationTypeEnum.CodifiedCodeObservation && rov.ObservationValue.IntValue != null)
                {
                    AddToList(GetCodedDiagnosis(rov.ObservationValue.IntValue.Value, rov.Observation.ObservationEncoding, privateCodes), codedDiagnosisList, episoderov, previousCC);
                }
                if (rov.Observation.KindOf == ObservationTypeEnum.CodifiedMultiSelectObservation && rov.ObservationValue.IntValue != null)
                {
                    CodedDiagnosisEntity[] cdes = GetCodedDiagnosis(rov.ObservationValue.IntValue.Value, rov.ExtObservationValue, rov.Observation.ObservationEncoding, privateCodes);
                    if (cdes != null && cdes.Length > 0)
                    {
                        foreach (CodedDiagnosisEntity cde in cdes)
                        {
                            AddToList(cde, codedDiagnosisList, episoderov, previousCC);
                        }
                    }
                }
            }
            return codedDiagnosisList.Count > 0 ? codedDiagnosisList.ToArray() : null;
        }

        private CodedDiagnosisEntity[] GetDiagnosisByObservations(int customerID, int customerEpisodeID, bool privateCodes, ValidationResults vr)
        {
            if (customerID <= 0 && customerEpisodeID <= 0) return null;
            CommonEntities.ElementEntity cdMetadata = GetElementByName(CommonEntities.Constants.EntityNames.CodedDiagnosisEntityName);
            if (cdMetadata != null && cdMetadata.Attributes != null && cdMetadata.Attributes.Length > 0)
            {
                CommonEntities.AttributeEntity diagnosisID = cdMetadata.GetAttribute("DiagnosisID");
                if (diagnosisID != null && diagnosisID.AttributeOptions != null && diagnosisID.AttributeOptions.Length > 0)
                {
                    string[] obsCodes = (from opt in diagnosisID.AttributeOptions
                                         where opt.Status == CommonEntities.AttributeStatusEnum.InUse
                                         select opt.Value).ToArray();
                    RegisteredObservationValueEntity[] rovs = CustomerObservationBL.GetRegisteredObservationValuesByObservationID(customerID, obsCodes);
                    try
                    {
                        return GetCodedDiagnosis(rovs, customerEpisodeID, privateCodes, customerID);
                    }
                    catch (Exception ex)
                    {
                        vr.AddResult(new ValidationResult(ex.Message, null, null, null, null));
                    }
                }
            }
            return null;
        }
        #endregion

        #region obtencion de CodedProcedureEntity
        private void GetCodedProcedures(RegisteredObservationValueEntity[] rovs, int customerEpisodeID, bool privateCodes, List<CodedProcedureEntity> codedProcedureList)
        {

        }

        private CodedProcedureEntity GetCodedProcedure(Tuple<string, DateTime> code, bool privateCodes)
        {
            if (code == null || string.IsNullOrEmpty(code.Item1)) return null;
            if (privateCodes)
            {
                EpisodeProcedureEntity procedure = EpisodeProcedureBL.Get(EpisodeProcedureBL.GetIDByCode(code.Item1));
                if (procedure != null)
                {
                    CodedProcedureEntity codeProc = new CodedProcedureEntity();
                    codeProc.Procedure = procedure;
                    codeProc.ProcedureID = procedure.ID;
                    codeProc.RealizationDateTime = code.Item2;
                    codeProc.EditStatus.New();
                    return codeProc;
                }
            }
            else
            {
                ConceptEntity concept = ConceptBL.Get(1, code.Item1);
                if (concept != null)
                {
                    CodedProcedureEntity codeProc = new CodedProcedureEntity();
                    codeProc.Concept = concept;
                    codeProc.ConceptID = concept.ID;
                    codeProc.RealizationDateTime = code.Item2;
                    codeProc.EditStatus.New();
                    return codeProc;
                }
            }
            return null;
        }

        private void GetCodedProcedures(Tuple<string, DateTime>[] codes, bool privateCodes, List<CodedProcedureEntity> codedProcedureList, ValidationResults vr)
        {
            if (codes == null || codes.Length <= 0 || codedProcedureList == null) return;
            foreach (Tuple<string, DateTime> code in codes)
            {
                try
                {
                    CodedProcedureEntity codeproc = GetCodedProcedure(code, privateCodes);
                    if (codeproc != null)
                        codedProcedureList.Add(codeproc);
                }
                catch (Exception ex)
                {
                    vr.AddResult(new ValidationResult(ex.Message, null, null, null, null));
                }
            }
        }

        private CodedProcedureEntity[] GetEpisodeProcedures(int customerID, int customerEpisodeID, bool privateCodes, ValidationResults vr)
        {
            if (customerID <= 0 && customerEpisodeID <= 0) return null;
            List<CodedProcedureEntity> codedProcedureList = new List<CodedProcedureEntity>();
            CommonEntities.ElementEntity cpMetadata = GetElementByName(CommonEntities.Constants.EntityNames.CodedProcedureEntityName);
            if (cpMetadata != null && cpMetadata.Attributes != null && cpMetadata.Attributes.Length > 0)
            {
                CommonEntities.AttributeEntity procedureID = cpMetadata.GetAttribute("ProcedureID");
                if (procedureID != null && procedureID.AttributeOptions != null && procedureID.AttributeOptions.Length > 0)
                {
                    string[] obsCodes = (from opt in procedureID.AttributeOptions
                                         where opt.Status == CommonEntities.AttributeStatusEnum.InUse
                                         select opt.Value).ToArray();
                    RegisteredObservationValueEntity[] rovs = CustomerObservationBL.GetRegisteredObservationValuesByObservationID(customerID, obsCodes);
                    GetCodedProcedures(rovs, customerEpisodeID, privateCodes, codedProcedureList);
                }
            }
            Tuple<string, DateTime>[] codes = _customerAssistancePlanDA.GetAllActivityCodes(customerEpisodeID);

            GetCodedProcedures(codes, privateCodes, codedProcedureList, vr);

            return codedProcedureList.Count > 0 ? codedProcedureList.ToArray() : null;
        }

        #endregion

        public CustomerCodificationEntity AnalizeCreateNewEpisodeCodification(int customerProcessID, out ValidationResults validationResults)
        {
            validationResults = new ValidationResults();
            try
            {
                if (customerProcessID <= 0) return null;
                CustomerProcessEntity customerProcess = base.Get(customerProcessID);
                if (customerProcess == null) return null;
                ProcessChartEntity pc = ProcessChartBL.GetByID(customerProcess.ProcessChartID);
                if (pc == null) return null;
                if (customerProcess.GetStepID(BasicProcessStepsEnum.Codification) > 0)
                    return GetCustomerCodification(customerProcess.GetStepID(BasicProcessStepsEnum.Codification));
                //aqui creamos uno nuevo.
                CustomerCodificationHelper ccHeleper = new CustomerCodificationHelper(GetElementByName(CommonEntities.Constants.EntityNames.CustomerCodificationEntityName));
                CustomerCodificationEntity ccData = ccHeleper.Create();
                ccData.EditStatus.New();
                ccData.CustomerID = customerProcess.CustomerID;
                ccData.CustomerEpisodeID = customerProcess.CustomerEpisodeID;
                ccData.CodifiedByID = 0;
                ccData.ProcessChartID = customerProcess.ProcessChartID;
                ccData.EstimatedCost = DeliveryNoteBL.GetTotalCalculatedCostByCustomerEpisodeID(customerProcess.CustomerEpisodeID);


                bool privateCodes = (pc.CodificationConfig != null && pc.CodificationConfig.CodedDiagnosisEncoder == null) || pc.CodificationConfig == null;
                ccData.Diagnosis = this.GetDiagnosisByObservations(customerProcess.CustomerID, customerProcess.CustomerEpisodeID, privateCodes, validationResults);
                privateCodes = (pc.CodificationConfig != null && pc.CodificationConfig.CodedProcedureEncoder == null) || pc.CodificationConfig == null;
                ccData.Procedures = this.GetEpisodeProcedures(customerProcess.CustomerID, customerProcess.CustomerEpisodeID, privateCodes, validationResults);
                /*INICIO - PARA QUE NO SE MUESTREN DE FORMA AUTOMÁTICA LOS PROCEDIMIENTOS ASOCIADOS CON LAS INTERVENCIONES*/
                /*Si se quiere que aparezcan, los procedmientos generados automáticamente, asociados a una intervención, 
                 * habría que eliminar la siguiente línea. 
                 * Se ha quitado la generación automática, para que añadan de forma manual los procedimientos que haya que codifcar.*/
                ccData.Procedures = null;
                /*FIN - PARA QUE NO SE MUESTREN DE FORMA AUTOMÁTICA LOS PROCEDIMIENTOS ASOCIADOS CON LAS INTERVENCIONES*/

                int previousEpisodeID = _customerEpisodeDA.GetPredecessorEpisodeID(customerProcess.CustomerEpisodeID);
                if (previousEpisodeID > 0)
                {
                    CodedDiagnosisEntity[] predDiagnosis = this.GetDiagnosisByObservations(customerProcess.CustomerID, previousEpisodeID, privateCodes, validationResults);
                    if (predDiagnosis != null && predDiagnosis.Length > 0)
                    {
                    if (ccData.Diagnosis != null && ccData.Diagnosis.Length > 0)
                    {
                        List<CodedDiagnosisEntity> diagList = new List<CodedDiagnosisEntity>();
                        diagList.AddRange(ccData.Diagnosis);
                        foreach (CodedDiagnosisEntity item in predDiagnosis)
                        {
                            if (!Array.Exists(diagList.ToArray(), cd => (cd.DiagnosisID == item.DiagnosisID && privateCodes) || (cd.ConceptID == item.ConceptID && !privateCodes)))
                                diagList.Add(item);
                        }
                        ccData.Diagnosis = diagList.ToArray();
                    }
                    else 
                        ccData.Diagnosis = predDiagnosis;
                    }
                    CodedProcedureEntity[] predProceudres = this.GetEpisodeProcedures(customerProcess.CustomerID, previousEpisodeID, privateCodes, validationResults);
                    if (predProceudres != null && predProceudres.Length > 0)
                    {
                        if (ccData.Procedures != null && ccData.Procedures.Length > 0)
                        {
                            List<CodedProcedureEntity> procList = new List<CodedProcedureEntity>();
                            procList.AddRange(ccData.Procedures);
                            foreach (CodedProcedureEntity item in predProceudres)
                            {
                                if (!Array.Exists(procList.ToArray(), cd => (cd.ProcedureID == item.ProcedureID && privateCodes) || (cd.ConceptID == item.ConceptID && !privateCodes)))
                                    procList.Add(item);
                            }
                            ccData.Procedures = procList.ToArray();
                        }
                        else
                            ccData.Procedures = predProceudres;
                    }
                }

                return ccData;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public void SetAllExported(int[] ccIDs)
        {
            try
            {
                _customerCodificationDA.SetAllExported(ccIDs, DateTime.Now, IdentityUser.GetIdentityUserName());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return;
            }
        }

/*
        public CustomerCodificationDTO[] GetMDSCustomerCodification(int[] processChartIDs,
            BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
            int[] locations, int[] careCenterIDs, int assistanceServiceID,
            DateTime? startDateTime, DateTime? endDateTime,
            string transferReason, MixedCodificationStatusEnum codificationStatus,
            out Boolean maxRecordsExceded)
        {
            maxRecordsExceded = false;
            try
            {
                //AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
                //int maxRows = administrativeConfig.EntitySettings.CustomerCodificationEntity.MaxRows;
                //if (maxRows == 0) { maxRows = Int32.MaxValue; }

                ////////////////////////////////////////////////////////////////////////////
                ///
                /// AQUI REVISAR CON ALBERTO PARA OPTMIZAR TODO LO QUE SE PUEDA
                ///
                ////////////////////////////////////////////////////////////////////////////

                DataSet ds = _customerCodificationDA.GetMDSCustomerCodification(0, processChartIDs, step, status, locations,
                    careCenterIDs, assistanceServiceID, startDateTime, endDateTime, transferReason, codificationStatus);
                if ((ds != null)
                    && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable)
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows.Count > 0))
                {
                    CustomerCodificationDTOAdvancedAdapter ccaa = new CustomerCodificationDTOAdvancedAdapter();
                    return ccaa.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
*/
        public CustomerCodificationDTO[] GetMDSCustomerCodification(int[] processChartIDs,
            BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
            int[] locations, int[] careCenterIDs, int assistanceServiceID,
            DateTime? startDateTime, DateTime? endDateTime,
            string transferReason, MixedCodificationStatusEnum codificationStatus,
            out Boolean maxRecordsExceded)
        {
            SqlDataReader dr = null;
            maxRecordsExceded = false;
            try
            {
                dr = _customerCodificationDA.GetMDSCustomerCodificationDR(0, processChartIDs, step, status, locations,
                    careCenterIDs, assistanceServiceID, startDateTime, endDateTime, transferReason, codificationStatus);

                CustomerCodificationDTOAdvancedAdapter ccaa = new CustomerCodificationDTOAdvancedAdapter();
                return ccaa.ConversionFromDataReader(dr);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }
        public bool CanBePrincipalDiagnosis(string strAssignedCode)
        {
            try
            {
                return _customerCodificationDA.CanBePrincipalDiagnosis(strAssignedCode);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool CanBePrincipalEpisodeProcedure(string strAssignedCode)
        {
            try
            {
                return _customerCodificationDA.CanBePrincipalEpisodeProcedure(strAssignedCode);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }


        #endregion

        #region Public Methods ONLY Related MDS and DRG extraction and export info
        //public MDSReportLine[] GetMDSReportLines(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        MDSReportLineAdapter mdsReportLineAdapter = new MDSReportLineAdapter();
        //        DataSet ds = _customerCodificationDA.GetMDSReportLines(episodeTypeID, step, fromDate, toDate);
        //        if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSReportLineTable))
        //            && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSReportLineTable].Rows.Count > 0))
        //        {
        //            DataSet ds2;

        //            #region MDSHeadLineInfo
        //            ds2 = _customerCodificationDA.GetMDSHeadLineInfo(episodeTypeID, step, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSHeadLineInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSHeadLineInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSFacilityInfo
        //            ///// TODO:
        //            ///// por ahora lo he puesto a capon, pero esto deberá ser una opcion de configuración
        //            string facilityServiceType = "CAM";

        //            ds2 = _customerCodificationDA.GetMDSFacilityInfo(episodeTypeID, step, fromDate, toDate, facilityServiceType);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSFacilityInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSFacilityInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSAddressInfo
        //            ds2 = _customerCodificationDA.GetMDSAddressInfo(episodeTypeID, step, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSAddressInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSAddressInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSRequestFacilityInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion

        //            #region MDSCustomerInfo
        //            #region MDSCHSummaryInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion

        //            AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
        //            string defaultIdentifier = string.Empty;
        //            if ((administrativeConfig != null) && (administrativeConfig.EntitySettings != null) && (administrativeConfig.EntitySettings.CustomerEntity != null)
        //                && (administrativeConfig.EntitySettings.CustomerEntity.Attributes != null) && (administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"] != null))
        //                defaultIdentifier = administrativeConfig.EntitySettings.CustomerEntity.Attributes["Identifier.IdentifierType"].DefaultValue;
        //            ds2 = _customerCodificationDA.GetMDSCustomerInfo(episodeTypeID, step, fromDate, toDate, defaultIdentifier);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSCustomerInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSCustomerInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSAssistanceInfo
        //            #region EpisodeLeaveReason
        //            ds2 = _customerCodificationDA.GetEpisodeLeaveReason(episodeTypeID, step, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion
        //            #region EpisodeReason
        //            ds2 = _customerCodificationDA.GetEpisodeReason(episodeTypeID, step, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSCHEpisodeSummaryInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion

        //            #region MDSAssistanceInfoEncoders
        //            ds2 = _customerCodificationDA.GetEpisodeEncoders(episodeTypeID, step, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeEncodersTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeEncodersTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSAssistanceInfoAutorizedActs
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion

        //            #region MDSTransferFacilityInfo
        //            ds2 = _customerCodificationDA.GetMDSTransferFacilityInfo(episodeTypeID, step, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSTransferFacilityInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSTransferFacilityInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSImagingDiagnosisInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion
        //            #region MDSSpecimenInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion
        //            #region MDSNurseEpisodeInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion
        //            #region MDSMedicalEpisodeInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion


        //            ds2 = _customerCodificationDA.GetMDSAssistanceInfo(episodeTypeID, step, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSAssistanceInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSAssistanceInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            MDSReportLine[] mdsReportLine = mdsReportLineAdapter.GetData(ds);
        //            return mdsReportLine;
        //        }
        //        else return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }


        //}

        // facilityServiceType = "CAM", "PAIS VASCO", etc...

        public MDSReportLine[] GetMDSReportLines(int[] customerEpisodeIDs /*, string facilityServiceType*/)
        {
            try
            {
                CommonEntities.ElementEntity commonInteropEntity = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.MDSExportListViewDTOName);
                CommonEntities.AttributeEntity specializedCareAttribute = commonInteropEntity.GetAttribute("ICULocation");
                CommonEntities.AttributeEntity facilityCareAttribute = commonInteropEntity.GetAttribute("FacilityCare");

                DataSet ds = _customerCodificationDA.GetMDSReportLines(customerEpisodeIDs);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSReportLineTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSReportLineTable].Rows.Count > 0))
                {
                    DataSet ds2;

                    #region MDSHeadLineInfo
                    ds2 = _customerCodificationDA.GetMDSHeadLineInfo(customerEpisodeIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSHeadLineInfoTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSHeadLineInfoTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region MDSFacilityInfo
                    string facilityServiceType = (facilityCareAttribute != null && !string.IsNullOrEmpty(facilityCareAttribute.DefaultValue))
                        ? facilityCareAttribute.DefaultValue : string.Empty; //esto viene del metadato

                    ds2 = _customerCodificationDA.GetMDSFacilityInfo(customerEpisodeIDs, facilityServiceType);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSFacilityInfoTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSFacilityInfoTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region MDSAddressInfo
                    ds2 = _customerCodificationDA.GetMDSAddressInfo(customerEpisodeIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSAddressInfoTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSAddressInfoTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region MDSRequestFacilityInfo
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
                    #endregion

                    #region MDSCustomerInfo
                    #region MDSCHSummaryInfo
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

                    //las tablas que han de aparecer aquí son: 
                    //SII.HCD.BackOffice.Entities.TableNames.MDSCHSummaryOfCustomerInfoTable
                    #endregion
                    string defaultIdentifier = this.GetDefaultIdentiferType();
                    ds2 = _customerCodificationDA.GetMDSCustomerInfo(customerEpisodeIDs, defaultIdentifier);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSCustomerInfoTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSCustomerInfoTable].Copy();
                        ds.Tables.Add(dt);

                        #region MDSFacilityNumberInfo
                        ds2 = _customerCodificationDA.GetMDSFacilityNumberInfo(customerEpisodeIDs, defaultIdentifier);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSFacilityNumberInfoTable)))
                        {
                            dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSFacilityNumberInfoTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region MDSCHFacilityNumberInfo
                        ds2 = _customerCodificationDA.GetMDSCHFacilityNumberInfo(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSCHFacilityNumberInfoTable)))
                        {
                            dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSCHFacilityNumberInfoTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                    }
                    #endregion

                    #region MDSAssistanceInfo
                    #region EpisodeLeaveReason
                    ds2 = _customerCodificationDA.GetEpisodeLeaveReason(customerEpisodeIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region EpisodeReason
                    ds2 = _customerCodificationDA.GetEpisodeReason(customerEpisodeIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region EpisodeLocation
                    ds2 = _customerCodificationDA.GetEpisodeLocation(customerEpisodeIDs, (specializedCareAttribute != null) ? specializedCareAttribute.ID : 0);
                    if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.LocationTable)))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.LocationTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region MDSCHEpisodeSummaryInfo
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

                    //las tablas que han de aparecer aquí son: 
                    //SII.HCD.BackOffice.Entities.TableNames.MDSCHEpisodeSummaryInfoTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSCHSummaryOfEpisodeInfoTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSTreatmentOfEpisodeInfoTable

                    #endregion

                    //#region MDSAssistanceInfoEncoders
                    #region EpisodeEncoders
                    ds2 = _customerCodificationDA.GetEpisodeEncoders(customerEpisodeIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeEncodersTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeEncodersTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region MDSAssistanceInfoAutorizedActs
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
                    #endregion

                    #region MDSTransferFacilityInfo
                    ds2 = _customerCodificationDA.GetMDSTransferFacilityInfo(customerEpisodeIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSTransferFacilityInfoTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSTransferFacilityInfoTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region MDSImagingDiagnosisInfo
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

                    //las tablas que han de aparecer aquí son: 
                    //SII.HCD.BackOffice.Entities.TableNames.MDSImagingDiagnosisInfoTable
                    //ordenes padres en SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderClinicalInfoTable
                    //las ordenes deben llevar el CustomerOrderRequestID
                    //las notas clinicas de los resultados con SII.HCD.BackOffice.Entities.TableNames.MDSClinicalNoteInfoResultTable
                    //las notas clinicas de los resultados con SII.HCD.BackOffice.Entities.TableNames.MDSClinicalNoteInfoRecomendationTable
                    #endregion

                    #region MDSSpecimenInfo
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
                    //las tablas que han de aparecer aquí son: 
                    //SII.HCD.BackOffice.Entities.TableNames.MDSSpecimenInfoTable
                    //ordenes padres en SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderClinicalInfoTable
                    //las ordenes deben llevar el CustomerOrderRequestID
                    //las notas clinicas de los resultados con SII.HCD.BackOffice.Entities.TableNames.MDSClinicalNoteInfoResultTable
                    #endregion

                    #region MDSNurseEpisodeInfo
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

                    //los nurseepisodeinfo con los CustomerAssistencePLan pero del CustomerEpisodeID
                    //SII.HCD.BackOffice.Entities.TableNames.MDSNurseEpisodeReasonInfoTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSCHSummaryInfoTable tienen que tener el 
                    //SII.HCD.BackOffice.Entities.TableNames.MDSPreAssessmentClinicalInfoTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSOtherNurseClinicalInfoTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSNOCClinicalInfoTable
                    //SII.HCD.BackOffice.Entities.TableNames.MDSNurseCHSummaryInfoTable

                    #endregion

                    #region MDSMedicalEpisodeInfo
                    //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
                    #endregion

                    ds2 = _customerCodificationDA.GetMDSAssistanceInfo(customerEpisodeIDs);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSAssistanceInfoTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSAssistanceInfoTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    MDSAdvancedAdapter mdsReportLineAdapter = new MDSAdvancedAdapter();
                    return mdsReportLineAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }


        }


        //public MDSReportLine[] GetMDSReportLinesCodification(int[] customerEpisodeIDs /*, string facilityServiceType*/)
        //{
        //    try
        //    {
        //        CommonEntities.ElementEntity commonInteropEntity = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.MDSExportListViewDTOName);
        //        CommonEntities.AttributeEntity specializedCareAttribute = commonInteropEntity.GetAttribute("ICULocation");
        //        CommonEntities.AttributeEntity facilityCareAttribute = commonInteropEntity.GetAttribute("FacilityCare");

        //        DataSet ds = _customerCodificationDA.GetMDSReportLines(customerEpisodeIDs);
        //        if ((ds.Tables != null)
        //            && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSReportLineTable))
        //            && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSReportLineTable].Rows.Count > 0))
        //        {
        //            DataSet ds2;

        //            #region MDSHeadLineInfo
        //            ds2 = _customerCodificationDA.GetMDSHeadLineInfo(customerEpisodeIDs);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSHeadLineInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSHeadLineInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSFacilityInfo
        //            string facilityServiceType = (facilityCareAttribute != null && !string.IsNullOrEmpty(facilityCareAttribute.DefaultValue))
        //                ? facilityCareAttribute.DefaultValue : string.Empty; //esto viene del metadato

        //            ds2 = _customerCodificationDA.GetMDSFacilityInfo(customerEpisodeIDs, facilityServiceType);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSFacilityInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSFacilityInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSAddressInfo
        //            ds2 = _customerCodificationDA.GetMDSAddressInfo(customerEpisodeIDs);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSAddressInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSAddressInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSRequestFacilityInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion

        //            #region MDSCustomerInfo
        //            #region MDSCHSummaryInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

        //            //las tablas que han de aparecer aquí son: 
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSCHSummaryOfCustomerInfoTable
        //            #endregion
        //            string defaultIdentifier = this.GetDefaultIdentiferType();
        //            ds2 = _customerCodificationDA.GetMDSCustomerInfo(customerEpisodeIDs, defaultIdentifier);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSCustomerInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSCustomerInfoTable].Copy();
        //                ds.Tables.Add(dt);

        //                #region MDSFacilityNumberInfo
        //                ds2 = _customerCodificationDA.GetMDSFacilityNumberInfo(customerEpisodeIDs, defaultIdentifier);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSFacilityNumberInfoTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSFacilityNumberInfoTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //                #endregion

        //                #region MDSCHFacilityNumberInfo
        //                ds2 = _customerCodificationDA.GetMDSCHFacilityNumberInfo(customerEpisodeIDs);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSCHFacilityNumberInfoTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSCHFacilityNumberInfoTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //                #endregion

        //            }
        //            #endregion

        //            #region MDSAssistanceInfo
        //            #region EpisodeLeaveReason
        //            ds2 = _customerCodificationDA.GetEpisodeLeaveReason(customerEpisodeIDs);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region EpisodeReason
        //            ds2 = _customerCodificationDA.GetEpisodeReason(customerEpisodeIDs);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region EpisodeLocation
        //            ds2 = _customerCodificationDA.GetEpisodeLocation(customerEpisodeIDs, (specializedCareAttribute != null) ? specializedCareAttribute.ID : 0);
        //            if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.LocationTable)))
        //            {
        //                DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.LocationTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSCHEpisodeSummaryInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

        //            //las tablas que han de aparecer aquí son: 
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSCHEpisodeSummaryInfoTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSCHSummaryOfEpisodeInfoTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSTreatmentOfEpisodeInfoTable

        //            #endregion

        //            //#region MDSAssistanceInfoEncoders
        //            #region EpisodeEncoders
        //            ds2 = _customerCodificationDA.GetEpisodeEncoders(customerEpisodeIDs);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeEncodersTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeEncodersTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSAssistanceInfoAutorizedActs
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion

        //            #region MDSTransferFacilityInfo
        //            ds2 = _customerCodificationDA.GetMDSTransferFacilityInfo(customerEpisodeIDs);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSTransferFacilityInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSTransferFacilityInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region MDSImagingDiagnosisInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

        //            //las tablas que han de aparecer aquí son: 
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSImagingDiagnosisInfoTable
        //            //ordenes padres en SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderClinicalInfoTable
        //            //las ordenes deben llevar el CustomerOrderRequestID
        //            //las notas clinicas de los resultados con SII.HCD.BackOffice.Entities.TableNames.MDSClinicalNoteInfoResultTable
        //            //las notas clinicas de los resultados con SII.HCD.BackOffice.Entities.TableNames.MDSClinicalNoteInfoRecomendationTable
        //            #endregion

        //            #region MDSSpecimenInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            //las tablas que han de aparecer aquí son: 
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSSpecimenInfoTable
        //            //ordenes padres en SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSCommonOrderClinicalInfoTable
        //            //las ordenes deben llevar el CustomerOrderRequestID
        //            //las notas clinicas de los resultados con SII.HCD.BackOffice.Entities.TableNames.MDSClinicalNoteInfoResultTable
        //            #endregion

        //            #region MDSNurseEpisodeInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD

        //            //los nurseepisodeinfo con los CustomerAssistencePLan pero del CustomerEpisodeID
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSNurseEpisodeReasonInfoTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSCHSummaryInfoTable tienen que tener el 
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSPreAssessmentClinicalInfoTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSOtherNurseClinicalInfoTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSNOCClinicalInfoTable
        //            //SII.HCD.BackOffice.Entities.TableNames.MDSNurseCHSummaryInfoTable

        //            #endregion

        //            #region MDSMedicalEpisodeInfo
        //            //TODO: Hasta disponer de nuevas versiones que impliquen la necesidad de ampliar los datos del CMBD
        //            #endregion

        //            ds2 = _customerCodificationDA.GetMDSAssistanceInfo(customerEpisodeIDs);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.MDSAssistanceInfoTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.MDSAssistanceInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            MDSAdvancedAdapter mdsReportLineAdapter = new MDSAdvancedAdapter();
        //            return mdsReportLineAdapter.GetData(ds);
        //        }
        //        else return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }


        //}

        #endregion
    }
}
