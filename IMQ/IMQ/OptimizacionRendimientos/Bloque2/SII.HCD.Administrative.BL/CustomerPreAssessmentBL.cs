using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Validation;
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
    public class CustomerPreAssessmentBL : CustomerProcessBL, ICustomerPreAssessmentService
    {
        #region Consts
        //private const string CustomerPreAssessmentEntityName = "CustomerPreAssessmentEntity";
        //private const string TimePatternBaseEntityName = "TimePatternBaseEntity";
        #endregion

        #region DA definitions

        private CustomerPreAssessmentDA _customerPreAssessmentDA;
        private CustomerProcessDA _customerProcessDA;
        private SII.HCD.BackOffice.DA.TimePatternDA _timePatternDA;
        private readonly CustomerProcessStepsRelDA _customerProcessStepsRelDA;
        private CustomerPreAssessmentTypeRelDA _customerPreAssessmentTypeRelDA;
        private CustomerPreAssessmentReasonRelDA _customerPreAssessmentReasonRelDA;
        private CustomerPreAssessmentCustomerTemplateRelDA _customerPreAssessmentCustomerTemplateRelDA;
        private PreAssessmentTypeDA _preAssessmentTypeDA;
        private ProcessChartBL _processChartBL;
        private ElementBL _elementBL;

        private AssistanceDegreeDA _assistanceDegreeDA;

        private EpisodeReasonTypeDA _episodeReasonTypeDA;
        private EpisodeReasonElementRelDA _episodeReasonElementRelDA;
        private EpisodeReasonDA _episodeReasonDA;

        #endregion

        #region TableNames
        private string _tableName;
        #endregion

        #region Constructor
        public CustomerPreAssessmentBL()
        {
            _customerPreAssessmentDA = new CustomerPreAssessmentDA();
            _customerProcessDA = new CustomerProcessDA();
            _timePatternDA = new SII.HCD.BackOffice.DA.TimePatternDA();
            _customerProcessStepsRelDA = new CustomerProcessStepsRelDA();
            _customerPreAssessmentTypeRelDA = new CustomerPreAssessmentTypeRelDA();
            _customerPreAssessmentReasonRelDA = new CustomerPreAssessmentReasonRelDA();
            _customerPreAssessmentCustomerTemplateRelDA = new CustomerPreAssessmentCustomerTemplateRelDA();
            _preAssessmentTypeDA = new PreAssessmentTypeDA();
            _elementBL = new ElementBL();
            _processChartBL = new ProcessChartBL();

            _assistanceDegreeDA = new AssistanceDegreeDA();

            _episodeReasonDA = new EpisodeReasonDA();
            _episodeReasonTypeDA = new EpisodeReasonTypeDA();
            _episodeReasonElementRelDA = new EpisodeReasonElementRelDA();

            _tableName = SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentTable;
        }
        #endregion

        #region Private methods
        protected virtual CustomerPreAssessmentEntity Insert(CustomerPreAssessmentEntity customerPreAssessment,
            CustomerPreAssessmentEntity customerPreAssessment_old, CustomerProcessEntity customerProcess)
        {
            if (customerPreAssessment == null) throw new ArgumentNullException("customerPreAssessment");

            string userName = IdentityUser.GetIdentityUserName();

            ProcessChartBL _processChartBL = new ProcessChartBL();
            ProcessChartEntity processChart = _processChartBL.GetByID(customerProcess.ProcessChartID);

            using (TransactionScope scope = new TransactionScope())
            {
                if (customerPreAssessment_old != null)
                {
                    customerPreAssessment_old = this.InnerUpdate(customerPreAssessment_old, customerProcess, processChart, userName);
                    customerPreAssessment.AncestorID = customerPreAssessment_old.ID;
                }

                this.InnerInsert(customerPreAssessment, customerProcess, processChart, userName);

                scope.Complete();
            }

            this.ResetCustomerPreAssessment(customerPreAssessment);
            LOPDLogger.Write(EntityNames.CustomerPreAssessmentEntityName, customerPreAssessment.ID, ActionType.Create);
            return customerPreAssessment;
        }

        protected virtual CustomerPreAssessmentEntity Update(CustomerPreAssessmentEntity customerPreAssessment, CustomerProcessEntity customerProcess)
        {
            if (customerPreAssessment == null) throw new ArgumentNullException("customerPreAssessment");

            ProcessChartBL _processChartBL = new ProcessChartBL();
            ProcessChartEntity processChart = _processChartBL.GetByID(customerProcess.ProcessChartID);

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerUpdate(customerPreAssessment, customerProcess, processChart, userName);

                scope.Complete();
            }

            this.ResetCustomerPreAssessment(customerPreAssessment);
            LOPDLogger.Write(EntityNames.CustomerPreAssessmentEntityName, customerPreAssessment.ID, ActionType.Modify);
            return customerPreAssessment;
        }

        protected virtual CustomerPreAssessmentEntity Delete(CustomerPreAssessmentEntity customerPreAssessment)
        {
            if (customerPreAssessment == null) throw new ArgumentNullException("customerPreAssessment");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                _customerPreAssessmentReasonRelDA.DeleteByCustomerPreAssessment(customerPreAssessment.ID);
                _customerPreAssessmentDA.DeleteCustomerPreAssessment(customerPreAssessment.ID);

                scope.Complete();
            }

            return customerPreAssessment;
        }
        #endregion

        #region Protected Methods

        #region PTE OBTENER ESTA INFO DE CUSTOMER PROCESSBL

        private bool PreviousStepAreCompleted(CustomerProcessEntity customerProcess, ProcessChartEntity processChart, BasicStepsInProcessEntity step)
        {
            int pos = step.Position;
            bool required = false;
            for (int i = pos - 1; i >= 0; i--)
            {
                if (processChart.StepsInProcess[i].StepRequired)
                {
                    required = customerProcess.GetStepID(processChart.StepsInProcess[i].ProcessStep) == 0;
                    //#region switch BasicProcessStepsEnum
                    //switch (processChart.StepsInProcess[i].ProcessStep)
                    //{
                    //    case BasicProcessStepsEnum.Contact:
                    //        if (customerProcess.CurrentContactID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Visit:
                    //        if (customerProcess.CurrentVisitID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.WaitingList:
                    //        if (customerProcess.CurrentWaitingListID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Budget:
                    //        if (customerProcess.CurrentBudgetID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Contract:
                    //        if (customerProcess.CurrentContractID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Guarantee:
                    //        if (customerProcess.CurrentGuaranteeID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Reservation:
                    //        if (customerProcess.CurrentReservationID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Citation:
                    //        if (customerProcess.CurrentCitationID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Reception:
                    //        if (customerProcess.CurrentReceptionID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Interview:
                    //        if (customerProcess.CurrentInterviewID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.PreAssessment:
                    //        if (customerProcess.CurrentPreAssessmentID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Admission:
                    //        if (customerProcess.CurrentAdmissionID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.TemporalLeave:
                    //        if (customerProcess.CurrentTemporalLeaveID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Transfer:
                    //        if (customerProcess.CurrentTransferID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Leave:
                    //        if (customerProcess.CurrentLeaveID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Realization:
                    //        if (customerProcess.CurrentRealizationID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Reports:
                    //        if (customerProcess.CurrentReportsID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.DeliveryResults:
                    //        if (customerProcess.CurrentDeliveryResultsID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Charges:
                    //        if (customerProcess.CurrentChargesID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.CoverAnalysis:
                    //        if (customerProcess.CurrentCoverAnalysisID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.DeliveryNote:
                    //        if (customerProcess.CurrentDeliveryNoteID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Invoice:
                    //        if (customerProcess.CurrentInvoiceID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Remittance:
                    //        if (customerProcess.CurrentRemittanceID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;
                    //    case BasicProcessStepsEnum.Codification:
                    //        if (customerProcess.CurrentCodificationID == 0)
                    //        {
                    //            required = true;
                    //        }
                    //        break;

                    //}
                    //#endregion
                }
                if (required) break;
            }
            return required;
        }

        private long GetAvailableCustomerProcessSteps(CustomerProcessEntity customerProcess)
        {
            long result = 0;
            ProcessChartEntity processChart = _processChartBL.GetByID(customerProcess.ProcessChartID);

            if (processChart.StepsInProcess != null)
            {

                foreach (BasicStepsInProcessEntity step in processChart.StepsInProcess)
                {
                    if ((result != 0) && (step.Position != 0))
                    {
                        if (!PreviousStepAreCompleted(customerProcess, processChart, step))
                        {
                            result += (long)step.ProcessStep;
                        }
                    }
                    else result += (long)step.ProcessStep;
                }
            }

            return result;
        }

        #endregion

        //protected virtual string IdentityUser.GetIdentityUserName()
        //{
        //    if ((ServiceSecurityContext.Current != null) && (ServiceSecurityContext.Current.PrimaryIdentity != null))
        //        return ServiceSecurityContext.Current.PrimaryIdentity.Name;

        //    return string.Empty;
        //}

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

        protected virtual void ValidateCustomerPreAssessment(CustomerPreAssessmentEntity customerPreAssessment)
        {
            if (customerPreAssessment == null) throw new ArgumentNullException("customerPreAssessment");

            CommonEntities.ElementEntity _customerPreAssessmentMetadata = this.GetElementByName(EntityNames.CustomerPreAssessmentEntityName);
            CustomerPreAssessmentHelper customerPreAssessmentHelper = new CustomerPreAssessmentHelper(_customerPreAssessmentMetadata);

            ValidationResults result = customerPreAssessmentHelper.Validate(customerPreAssessment);
            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                /* PTE_PANEL ERROR_CustomerVisitValidationError
                throw new Exception(
                    string.Format(Properties.Resources.ERROR_CustomerVisitValidationError, sb));
                */
            }
        }

        protected virtual void ResetCustomerPreAssessment(CustomerPreAssessmentEntity customerPreAssessment)
        {
            customerPreAssessment.EditStatus.Reset();
        }

        protected virtual void InnerUpdateCustomerProcess(CustomerPreAssessmentEntity customerPreAssessment, CustomerProcessEntity customerProcess, ProcessChartEntity processChart, string userName)
        {
            DateTime? stepDateTime = customerPreAssessment.InitDate;
            base.InnerSaveBasicProcessStep(customerProcess.ID, BasicProcessStepsEnum.PreAssessment, customerPreAssessment.ID, customerPreAssessment.Status, stepDateTime, null, userName);
            customerProcess = base.RefreshStepCustomerProcess(customerProcess);
            long availableProcessSteps = base.InnerGetAvailableProcessSteps(customerProcess, processChart);
            _customerProcessDA.UpdateAvailableProcessStep(customerProcess.ID, availableProcessSteps, userName);
        }

        protected virtual CustomerPreAssessmentEntity InnerInsert(CustomerPreAssessmentEntity customerPreAssessment, CustomerProcessEntity customerProcess, ProcessChartEntity processChart, string userName)
        {
            customerPreAssessment.LastUpdated = DateTime.Now;
            customerPreAssessment.ID = _customerPreAssessmentDA.Insert(customerPreAssessment.CustomerID, customerPreAssessment.PersonID,
                customerPreAssessment.CustomerProcessID, customerPreAssessment.ProcessChartID,
                customerPreAssessment.InitDate, customerPreAssessment.ConfirmedDate, (int)customerPreAssessment.InformationProvidedPersonType, customerPreAssessment.InformationProvidedByID,
                customerPreAssessment.InterviewMadeIn, customerPreAssessment.InterviewMadeByID,
                customerPreAssessment.AssistanceDegreeRequested != null ? customerPreAssessment.AssistanceDegreeRequested.ID : 0,
                customerPreAssessment.AssistanceDegreeRecommended != null ? customerPreAssessment.AssistanceDegreeRecommended.ID : 0,
                customerPreAssessment.PreAssessmentDocument, customerPreAssessment.Recommendations, customerPreAssessment.Explanations,
                customerPreAssessment.AncestorID, customerPreAssessment.ADTOrder != null ? customerPreAssessment.ADTOrder.ID : 0,
                (int)customerPreAssessment.Status, customerPreAssessment.LastUpdated, userName, customerPreAssessment.DBTimeStamp);

            if (customerPreAssessment.PreAssessmentTemplate != null
                && customerPreAssessment.PreAssessmentTemplate.Templates != null
                && customerPreAssessment.PreAssessmentTemplate.Templates.Length > 0)
            {
                List<RegisteredObservationTemplateEntity> newList = new List<RegisteredObservationTemplateEntity>();
                List<RegisteredObservationTemplateEntity> deletedList = new List<RegisteredObservationTemplateEntity>();
                foreach (RegisteredObservationTemplateEntity rot in customerPreAssessment.PreAssessmentTemplate.Templates)
                {
                    switch (rot.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            deletedList.Add(rot);
                            break;
                        case StatusEntityValue.New:
                            newList.Add(rot);
                            break;
                        default:
                            break;
                    }
                }

                CustomerObservationBL cobsBL = new CustomerObservationBL();
                RegisteredLayoutEntity rle = cobsBL.Save(customerPreAssessment.PreAssessmentTemplate);
                if (rle != null
                    && rle.Templates != null
                    && rle.Templates.Length > 0)
                {
                    if (newList.Count > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in newList)
                        {
                            if (customerPreAssessment.ID > 0
                                && rot.ID > 0)
                                _customerPreAssessmentCustomerTemplateRelDA.Insert(customerPreAssessment.ID, rot.ID, userName);
                        }
                    }
                    if (deletedList.Count > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in deletedList)
                        {
                            if (customerPreAssessment.ID > 0
                                && rot.ID > 0)
                                _customerPreAssessmentCustomerTemplateRelDA.Delete(customerPreAssessment.ID, rot.ID);
                        }
                    }

                    customerPreAssessment.CustomerTemplateIDs = (from rot in rle.Templates
                                                                 where rot.ID > 0
                                                                 && (deletedList.Count == 0
                                                                     || !Array.Exists(deletedList.ToArray(), rt => rt.ID == rot.ID))
                                                                 select rot.ID).ToArray();
                }
                customerPreAssessment.PreAssessmentTemplate = rle;
            }

            if (customerPreAssessment.CustomerPreAssessmentTypeRels != null
                && customerPreAssessment.CustomerPreAssessmentTypeRels.Length > 0)
            {
                customerPreAssessment.CustomerPreAssessmentTypeRels = SaveCustomerPreAssessmentTypeRels(customerPreAssessment.ID, customerPreAssessment.CustomerPreAssessmentTypeRels, userName);
            }

            customerPreAssessment.DBTimeStamp = _customerPreAssessmentDA.GetDBTimeStamp(customerPreAssessment.ID);

            InnerUpdateCustomerProcess(customerPreAssessment, customerProcess, processChart, userName);

            return customerPreAssessment;
        }

        protected virtual CustomerPreAssessmentEntity InnerUpdate(CustomerPreAssessmentEntity customerPreAssessment, CustomerProcessEntity customerProcess, ProcessChartEntity processChart, string userName)
        {
            Int64 dbTimeStamp = _customerPreAssessmentDA.GetDBTimeStamp(customerPreAssessment.ID);
            if (dbTimeStamp != customerPreAssessment.DBTimeStamp)
                throw new Exception(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customerPreAssessment.ID));

            if (customerPreAssessment.PreAssessmentTemplate != null
                && customerPreAssessment.PreAssessmentTemplate.Templates != null
                && customerPreAssessment.PreAssessmentTemplate.Templates.Length > 0)
            {
                List<RegisteredObservationTemplateEntity> newList = new List<RegisteredObservationTemplateEntity>();
                List<RegisteredObservationTemplateEntity> deletedList = new List<RegisteredObservationTemplateEntity>();
                foreach (RegisteredObservationTemplateEntity rot in customerPreAssessment.PreAssessmentTemplate.Templates)
                {
                    switch (rot.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            deletedList.Add(rot);
                            break;
                        case StatusEntityValue.New:
                            newList.Add(rot);
                            break;
                        default:
                            break;
                    }
                }

                CustomerObservationBL cobsBL = new CustomerObservationBL();
                RegisteredLayoutEntity rle = cobsBL.Save(customerPreAssessment.PreAssessmentTemplate);
                if (rle != null
                    && rle.Templates != null
                    && rle.Templates.Length > 0)
                {
                    if (newList.Count > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in newList)
                        {
                            _customerPreAssessmentCustomerTemplateRelDA.Insert(customerPreAssessment.ID, rot.ID, userName);
                        }
                    }
                    if (deletedList.Count > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in deletedList)
                        {
                            _customerPreAssessmentCustomerTemplateRelDA.Delete(customerPreAssessment.ID, rot.ID);
                        }
                    }

                    customerPreAssessment.CustomerTemplateIDs = (from rot in rle.Templates
                                                                 where rot.ID > 0
                                                                    && !Array.Exists(deletedList.ToArray(), rt => rt.ID == rot.ID)
                                                                 select rot.ID).ToArray();
                }
                customerPreAssessment.PreAssessmentTemplate = rle;
            }

            switch (customerPreAssessment.Status)
            {
                case CommonEntities.StatusEnum.None:
                    break;
                case CommonEntities.StatusEnum.Active:
                case CommonEntities.StatusEnum.Pending:
                    _customerPreAssessmentDA.Update(customerPreAssessment.ID, customerPreAssessment.CustomerID, customerPreAssessment.PersonID,
                        customerPreAssessment.CustomerProcessID, customerPreAssessment.ProcessChartID,
                        customerPreAssessment.InitDate, customerPreAssessment.ConfirmedDate, (int)customerPreAssessment.InformationProvidedPersonType, customerPreAssessment.InformationProvidedByID,
                        customerPreAssessment.InterviewMadeIn, customerPreAssessment.InterviewMadeByID,
                        customerPreAssessment.AssistanceDegreeRequested != null ? customerPreAssessment.AssistanceDegreeRequested.ID : 0,
                        customerPreAssessment.AssistanceDegreeRecommended != null ? customerPreAssessment.AssistanceDegreeRecommended.ID : 0,
                        customerPreAssessment.PreAssessmentDocument, customerPreAssessment.Recommendations, customerPreAssessment.Explanations,
                        customerPreAssessment.AncestorID, customerPreAssessment.ADTOrder != null ? customerPreAssessment.ADTOrder.ID : 0,
                        (int)customerPreAssessment.Status, customerPreAssessment.LastUpdated, userName, customerPreAssessment.DBTimeStamp);

                    if (customerPreAssessment.ID == customerProcess.GetStepID(BasicProcessStepsEnum.PreAssessment))
                        InnerUpdateCustomerProcess(customerPreAssessment, customerProcess, processChart, userName);

                    break;
                case CommonEntities.StatusEnum.Cancelled:
                    _customerPreAssessmentDA.Update(customerPreAssessment.ID, customerPreAssessment.CustomerID, customerPreAssessment.PersonID,
                        customerPreAssessment.CustomerProcessID, customerPreAssessment.ProcessChartID,
                        customerPreAssessment.InitDate, customerPreAssessment.ConfirmedDate, (int)customerPreAssessment.InformationProvidedPersonType, customerPreAssessment.InformationProvidedByID,
                        customerPreAssessment.InterviewMadeIn, customerPreAssessment.InterviewMadeByID,
                        customerPreAssessment.AssistanceDegreeRequested != null ? customerPreAssessment.AssistanceDegreeRequested.ID : 0,
                        customerPreAssessment.AssistanceDegreeRecommended != null ? customerPreAssessment.AssistanceDegreeRecommended.ID : 0,
                        customerPreAssessment.PreAssessmentDocument, customerPreAssessment.Recommendations, customerPreAssessment.Explanations,
                        customerPreAssessment.AncestorID, customerPreAssessment.ADTOrder != null ? customerPreAssessment.ADTOrder.ID : 0,
                        (int)customerPreAssessment.Status, customerPreAssessment.LastUpdated, userName, customerPreAssessment.DBTimeStamp);

                    if (customerPreAssessment.ID == customerProcess.GetStepID(BasicProcessStepsEnum.PreAssessment))
                        InnerUpdateCustomerProcess(customerPreAssessment, customerProcess, processChart, userName);

                    break;
                case CommonEntities.StatusEnum.Confirmed:
                    _customerPreAssessmentDA.Update(customerPreAssessment.ID, customerPreAssessment.CustomerID, customerPreAssessment.PersonID,
                        customerPreAssessment.CustomerProcessID, customerPreAssessment.ProcessChartID,
                        customerPreAssessment.InitDate, customerPreAssessment.ConfirmedDate, (int)customerPreAssessment.InformationProvidedPersonType, customerPreAssessment.InformationProvidedByID,
                        customerPreAssessment.InterviewMadeIn, customerPreAssessment.InterviewMadeByID,
                        customerPreAssessment.AssistanceDegreeRequested != null ? customerPreAssessment.AssistanceDegreeRequested.ID : 0,
                        customerPreAssessment.AssistanceDegreeRecommended != null ? customerPreAssessment.AssistanceDegreeRecommended.ID : 0,
                        customerPreAssessment.PreAssessmentDocument, customerPreAssessment.Recommendations, customerPreAssessment.Explanations,
                        customerPreAssessment.AncestorID, customerPreAssessment.ADTOrder != null ? customerPreAssessment.ADTOrder.ID : 0,
                        (int)customerPreAssessment.Status, customerPreAssessment.LastUpdated, userName, customerPreAssessment.DBTimeStamp);

                    if (customerPreAssessment.ID == customerProcess.GetStepID(BasicProcessStepsEnum.PreAssessment))
                        InnerUpdateCustomerProcess(customerPreAssessment, customerProcess, processChart, userName);

                    break;
                case CommonEntities.StatusEnum.Closed:
                    _customerPreAssessmentDA.Update(customerPreAssessment.ID, customerPreAssessment.CustomerID, customerPreAssessment.PersonID,
                        customerPreAssessment.CustomerProcessID, customerPreAssessment.ProcessChartID,
                        customerPreAssessment.InitDate, customerPreAssessment.ConfirmedDate, (int)customerPreAssessment.InformationProvidedPersonType, customerPreAssessment.InformationProvidedByID,
                        customerPreAssessment.InterviewMadeIn, customerPreAssessment.InterviewMadeByID,
                        customerPreAssessment.AssistanceDegreeRequested != null ? customerPreAssessment.AssistanceDegreeRequested.ID : 0,
                        customerPreAssessment.AssistanceDegreeRecommended != null ? customerPreAssessment.AssistanceDegreeRecommended.ID : 0,
                        customerPreAssessment.PreAssessmentDocument, customerPreAssessment.Recommendations, customerPreAssessment.Explanations,
                        customerPreAssessment.AncestorID, customerPreAssessment.ADTOrder != null ? customerPreAssessment.ADTOrder.ID : 0,
                        (int)customerPreAssessment.Status, customerPreAssessment.LastUpdated, userName, customerPreAssessment.DBTimeStamp);

                    if (customerPreAssessment.ID == customerProcess.GetStepID(BasicProcessStepsEnum.PreAssessment))
                        InnerUpdateCustomerProcess(customerPreAssessment, customerProcess, processChart, userName);

                    break;
                case CommonEntities.StatusEnum.Superceded:
                    _customerPreAssessmentDA.Update(customerPreAssessment.ID, customerPreAssessment.CustomerID, customerPreAssessment.PersonID,
                        customerPreAssessment.CustomerProcessID, customerPreAssessment.ProcessChartID,
                        customerPreAssessment.InitDate, customerPreAssessment.ConfirmedDate, (int)customerPreAssessment.InformationProvidedPersonType, customerPreAssessment.InformationProvidedByID,
                        customerPreAssessment.InterviewMadeIn, customerPreAssessment.InterviewMadeByID,
                        customerPreAssessment.AssistanceDegreeRequested != null ? customerPreAssessment.AssistanceDegreeRequested.ID : 0,
                        customerPreAssessment.AssistanceDegreeRecommended != null ? customerPreAssessment.AssistanceDegreeRecommended.ID : 0,
                        customerPreAssessment.PreAssessmentDocument, customerPreAssessment.Recommendations, customerPreAssessment.Explanations,
                        customerPreAssessment.AncestorID, customerPreAssessment.ADTOrder != null ? customerPreAssessment.ADTOrder.ID : 0,
                        (int)customerPreAssessment.Status, customerPreAssessment.LastUpdated, userName, customerPreAssessment.DBTimeStamp);

                    if (customerPreAssessment.ID == customerProcess.GetStepID(BasicProcessStepsEnum.PreAssessment))
                        InnerUpdateCustomerProcess(customerPreAssessment, customerProcess, processChart, userName);

                    break;
                default:
                    break;
            }

            if (customerPreAssessment.CustomerPreAssessmentTypeRels != null
                && customerPreAssessment.CustomerPreAssessmentTypeRels.Length > 0)
            {
                customerPreAssessment.CustomerPreAssessmentTypeRels = SaveCustomerPreAssessmentTypeRels(customerPreAssessment.ID, customerPreAssessment.CustomerPreAssessmentTypeRels, userName);
            }

            customerPreAssessment.DBTimeStamp = _customerPreAssessmentDA.GetDBTimeStamp(customerPreAssessment.ID);

            return customerPreAssessment;
        }

        private CustomerPreAssessmentTypeRelEntity[] SaveCustomerPreAssessmentTypeRels(int customerPreAssessmentID, CustomerPreAssessmentTypeRelEntity[] customerPreAssessmentTypeRels, string userName)
        {
            List<CustomerPreAssessmentTypeRelEntity> cpatrList = new List<CustomerPreAssessmentTypeRelEntity>();
            foreach (CustomerPreAssessmentTypeRelEntity cpatr in customerPreAssessmentTypeRels)
            {
                switch (cpatr.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        _customerPreAssessmentTypeRelDA.Delete(cpatr.ID);
                        if (cpatr.CustomerPreAssessmentReasons != null
                            && cpatr.CustomerPreAssessmentReasons.Length > 0)
                        {
                            cpatr.CustomerPreAssessmentReasons = SaveCustomerPreAssessmentReasons(customerPreAssessmentID, cpatr.ID, cpatr.CustomerPreAssessmentReasons, userName);
                        }
                        cpatr.EditStatus.Reset();
                        break;
                    case StatusEntityValue.New:
                        cpatr.ID = _customerPreAssessmentTypeRelDA.Insert(customerPreAssessmentID, cpatr.PreAssessmentType.ID, userName);
                        if (cpatr.CustomerPreAssessmentReasons != null
                            && cpatr.CustomerPreAssessmentReasons.Length > 0)
                        {
                            cpatr.CustomerPreAssessmentReasons = SaveCustomerPreAssessmentReasons(customerPreAssessmentID, cpatr.ID, cpatr.CustomerPreAssessmentReasons, userName);
                        }
                        cpatr.EditStatus.Reset();
                        cpatrList.Add(cpatr);
                        break;
                    case StatusEntityValue.None:
                        cpatrList.Add(cpatr);
                        break;
                    case StatusEntityValue.Updated:
                        _customerPreAssessmentTypeRelDA.Update(cpatr.ID, customerPreAssessmentID, cpatr.PreAssessmentType.ID, userName);
                        if (cpatr.CustomerPreAssessmentReasons != null
                            && cpatr.CustomerPreAssessmentReasons.Length > 0)
                        {
                            cpatr.CustomerPreAssessmentReasons = SaveCustomerPreAssessmentReasons(customerPreAssessmentID, cpatr.ID, cpatr.CustomerPreAssessmentReasons, userName);
                        }
                        cpatr.EditStatus.Reset();
                        cpatrList.Add(cpatr);
                        break;
                    default:
                        break;
                }
            }
            return (cpatrList.Count > 0) ? cpatrList.ToArray() : null;
        }

        private CustomerPreAssessmentReasonRelEntity[] SaveCustomerPreAssessmentReasons(int customerPreAssessmentID, int customerPreAssessmentTypeRelID, CustomerPreAssessmentReasonRelEntity[] customerPreAssessmentReasonRels, string userName)
        {
            List<CustomerPreAssessmentReasonRelEntity> cparrList = new List<CustomerPreAssessmentReasonRelEntity>();
            foreach (CustomerPreAssessmentReasonRelEntity cparr in customerPreAssessmentReasonRels)
            {
                switch (cparr.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        _customerPreAssessmentReasonRelDA.Delete(cparr.ID);
                        break;
                    case StatusEntityValue.New:
                        cparr.ID = _customerPreAssessmentReasonRelDA.Insert(customerPreAssessmentID, customerPreAssessmentTypeRelID, cparr.EpisodeReason.ID, userName);
                        cparr.EditStatus.Reset();
                        cparrList.Add(cparr);
                        break;
                    case StatusEntityValue.None:
                        cparrList.Add(cparr);
                        break;
                    case StatusEntityValue.Updated:
                        _customerPreAssessmentReasonRelDA.Update(cparr.ID, customerPreAssessmentID, customerPreAssessmentTypeRelID, cparr.EpisodeReason.ID, userName);
                        cparr.EditStatus.Reset();
                        cparrList.Add(cparr);
                        break;
                    default:
                        break;
                }
            }
            return (cparrList.Count > 0) ? cparrList.ToArray() : null;
        }

        #region CheckPreconditions

        protected virtual void CheckInsertPreconditions(CustomerPreAssessmentEntity customerPreAssessment)
        {
            if (customerPreAssessment == null) throw new ArgumentNullException("customerPreAssessment");

            ValidateCustomerPreAssessment(customerPreAssessment);
        }

        protected virtual void CheckUpdatePreconditions(CustomerPreAssessmentEntity customerPreAssessment)
        {
            if (customerPreAssessment == null) throw new ArgumentNullException("customerPreAssessment");

            ValidateCustomerPreAssessment(customerPreAssessment);
        }

        protected virtual void CheckDeletePreconditions(int customerPreAssessmentID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Public Methods
        public CustomerPreAssessmentEntity Save(CustomerPreAssessmentEntity customerPreAssessment,
            CustomerPreAssessmentEntity customerPreAssessment_old,
            ref CustomerProcessEntity customerProcess)
        {
            try
            {
                if (customerPreAssessment == null)
                    throw new ArgumentNullException("customerPreAssessment");

                if (customerProcess.DBTimeStamp != base.GetCustomerProcessTimeStamp(customerProcess.ID))
                {
                    customerProcess = base.Get(customerProcess.ID);
                }

                switch (customerPreAssessment.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        CheckDeletePreconditions(customerPreAssessment.ID);
                        return Delete(customerPreAssessment);
                    case StatusEntityValue.New:
                        CheckInsertPreconditions(customerPreAssessment);
                        if (customerPreAssessment_old != null)
                            CheckUpdatePreconditions(customerPreAssessment_old);
                        return Insert(customerPreAssessment, customerPreAssessment_old, customerProcess);
                    case StatusEntityValue.NewAndDeleted:
                        return customerPreAssessment;
                    case StatusEntityValue.None:
                        return customerPreAssessment;
                    case StatusEntityValue.Updated:
                        CheckUpdatePreconditions(customerPreAssessment);
                        return Update(customerPreAssessment, customerProcess);
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

        public CustomerPreAssessmentEntity CopyInfoPreAssessment(int customerPreAssessmentID)
        {
            if (customerPreAssessmentID != 0)
            {
                string userName = IdentityUser.GetIdentityUserName();

                CustomerPreAssessmentEntity customerPreAssessment = this.GetCustomerPreAssessment(customerPreAssessmentID);

                //Hay que controlar que campos queremos recuperar del PreAssesment, por ahora recuperamos todo
                //mas adelante los que no se deban recuperar se deben vaciar aqui.


                return customerPreAssessment;
            }
            else
                return null;
        }

        public CustomerPreAssessmentEntity CopyInfo(CustomerPreAssessmentEntity customerPreAssessment, CustomerProcessEntity customerProcess)
        {
            try
            {
                if (customerPreAssessment == null)
                    throw new ArgumentNullException("customerPreAssessment");

                CustomerPreAssessmentEntity result = (CustomerPreAssessmentEntity)customerPreAssessment.Clone();
                result.AncestorID = 0;
                result.Status = CommonEntities.StatusEnum.Pending;
                if (customerPreAssessment.ID != 0)
                    result.EditStatus.Update();
                result.ConfirmedDate = null;
                result.ModifiedBy = "";

                // PTE preguntar a Miguel
                result.PreAssessmentDocument = "";

                // PTE Comprobar si el contacto copiado está 
                // entre los personas de contacto del proceso
                // en caso contrario:
                //result.InformationProvidedPersonType = CustomerRelTypeEnum.None;
                //result.InformationProvidedByID = 0;


                // PTE Comprobar si el recurso humano copiado está 
                // entre los recursos del proceso
                // en caso contrario:
                //result.InterviewMadeByID = 0;

                return result;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        //public CustomerPreAssessmentEntity GetCustomerPreAssessment_old(int id)
        //{
        //    try
        //    {
        //        DataSet ds = _customerPreAssessmentDA.GetCustomerPreAssessment(id);
        //        if ((ds.Tables != null) && ds.Tables.Contains(_tableName) && (ds.Tables[_tableName].Rows.Count > 0))
        //        {
        //            //CustomerPreAssessmentTypeRel
        //            DatasetUtils.MergeTable(_customerPreAssessmentTypeRelDA.GetCustomerPreAssessmentTypeRelsByCustomerPreAssessmentID(id),
        //                ds, Administrative.Entities.TableNames.CustomerPreAssessmentTypeRelTable);

        //            if (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerPreAssessmentTypeRelTable)
        //                && (ds.Tables[Administrative.Entities.TableNames.CustomerPreAssessmentTypeRelTable].Rows.Count > 0))
        //            {
        //                //CustomerPreAssessmentReasonRel
        //                DatasetUtils.MergeTable(_customerPreAssessmentReasonRelDA.GetAllCustomerPreAssessmentReasonRelByID(id),
        //                    ds, Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable);

        //                if (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable)
        //                    && (ds.Tables[Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable].Rows.Count > 0))
        //                {
        //                    //EpisodeReasonTypes
        //                    DatasetUtils.MergeTable(_episodeReasonTypeDA.GetAllEpisodeReasonType(ElementReasonTypeEnum.PreAssessment),
        //                        ds, BackOffice.Entities.TableNames.EpisodeReasonTypeTable);

        //                    //EpisodeReasons
        //                    DatasetUtils.MergeTable(_episodeReasonDA.GetAllEpisodeReasonByType(ElementReasonTypeEnum.PreAssessment),
        //                        ds, Administrative.Entities.TableNames.EpisodeReasonTable);

        //                    // EpisodeReasonelementRel
        //                    DatasetUtils.MergeTable(_episodeReasonElementRelDA.GetAllEpisodeReasonElementRelByType(ElementReasonTypeEnum.PreAssessment),
        //                        ds, BackOffice.Entities.TableNames.EpisodeReasonElementRelTable);
        //                }

        //                //PreAssessmentType
        //                DatasetUtils.MergeTable(_preAssessmentTypeDA.GetAllPreAssessmentType(), ds, BackOffice.Entities.TableNames.PreAssessmentTypeTable);
        //            }

        //            //AssistanceDegree
        //            DatasetUtils.MergeTable(_assistanceDegreeDA.GetAllAssistanceDegreeOfPreAssessmentConfig(),
        //                ds, BackOffice.Entities.TableNames.AssistanceDegreeTable);

        //            //CustomerPreAssessmentCustomerTemplateRel
        //            DatasetUtils.MergeTable(_customerPreAssessmentCustomerTemplateRelDA.GetCustomerPreAssessmentCustomerTemplateRelsByCustomerPreAssessmentID(id),
        //                ds, Administrative.Entities.TableNames.CustomerPreAssessmentCustomerTemplateRelTable);

        //            CustomerPreAssessmentAdvancedAdapter adapter = new CustomerPreAssessmentAdvancedAdapter();
        //            CustomerPreAssessmentEntity result = adapter.GetByID(id, ds);

        //            LOPDLogger.Write(EntityNames.CustomerPreAssessmentEntityName, id, ActionType.View);
        //            return result;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}
        //public CustomerPreAssessmentEntity GetCustomerPreAssessment(int id)
        //{
        //    try
        //    {
        //        CustomerPreAssessmentEntity oldCustomerPreAssessmentEntity = GetCustomerPreAssessment_old(id);
        //        CustomerPreAssessmentEntity newCustomerPreAssessmentEntity = GetCustomerPreAssessment_new(id);

        //        bool res = newCustomerPreAssessmentEntity.CompareEquals(oldCustomerPreAssessmentEntity);
        //        return newCustomerPreAssessmentEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}
        public CustomerPreAssessmentEntity GetCustomerPreAssessment(int id)
        {
            try
            {
                DataSet ds = _customerPreAssessmentDA.GetCustomerPreAssessment(id, ElementReasonTypeEnum.PreAssessment);
                if ((ds.Tables != null) && ds.Tables.Contains(_tableName) && (ds.Tables[_tableName].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());
                   
                    CustomerPreAssessmentAdvancedAdapter adapter = new CustomerPreAssessmentAdvancedAdapter();
                    CustomerPreAssessmentEntity result = adapter.GetByID(id, ds2);

                    LOPDLogger.Write(EntityNames.CustomerPreAssessmentEntityName, id, ActionType.View);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        public CustomerPreAssessmentEntity Create(int customerID, int processChartID)
        {
            try
            {
                CustomerPreAssessmentEntity customerPreAssessment = null;
                CommonEntities.ElementEntity _customerPreAssessmentMetadata = this.GetElementByName(EntityNames.CustomerPreAssessmentEntityName);
                CustomerPreAssessmentHelper customerPreAssessmentHelper = new CustomerPreAssessmentHelper(_customerPreAssessmentMetadata);
                customerPreAssessment = customerPreAssessmentHelper.Create();
                customerPreAssessment.EditStatus.New();
                customerPreAssessment.InitDate = DateTime.Now;
                customerPreAssessment.ConfirmedDate = DateTime.Now;
                customerPreAssessment.ProcessChartID = processChartID;
                customerPreAssessment.CustomerID = customerID;
                customerPreAssessment.Status = CommonEntities.StatusEnum.Pending;

                return customerPreAssessment;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public Int64 GetCustomerPreAssessmentTimeStamp(int customerPreAssessmentID)
        {
            try
            {
                return _customerPreAssessmentDA.GetDBTimeStamp(customerPreAssessmentID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public void SetCustomerPreAssessmentStatus(int customerPreAssessmentID, CommonEntities.StatusEnum status)
        {
            try
            {
                string userName = IdentityUser.GetIdentityUserName();
                _customerPreAssessmentDA.SetCustomerPreAssessmentStatus(customerPreAssessmentID, (int)status, userName);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public int DeleteCustomerPreAssessment(int customerPreAssessmentID)
        {
            try
            {
                CheckDeletePreconditions(customerPreAssessmentID);
                return _customerPreAssessmentDA.DeleteCustomerPreAssessment(customerPreAssessmentID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public bool CheckDisablePreconditions(int customerPreAssessmentID)
        {
            return true;
        }

        public CustomerPreAssessmentListDTO[] GetPreviousCustomerPreAssessment(int processChartID, int customerID)
        {
            try
            {
                DataSet ds = _customerPreAssessmentDA.GetPreviousCustomerPreAssessment(processChartID, customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentListDTOTable)))
                {
                    CustomerPreAssessmentListAdapter customerPreAssessmentListDTOAdapter = new CustomerPreAssessmentListAdapter();
                    return customerPreAssessmentListDTOAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerPreAssessmentListDTO[] GetPreviousCustomerPreAssessmentFromEpisode(int id, int customerEpisodeID, int customerID)
        {
            try
            {
                DataSet ds = _customerPreAssessmentDA.GetPreviousCustomerPreAssessmentFromEpisode(id, customerEpisodeID, customerID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentListDTOTable)))
                {
                    CustomerPreAssessmentListAdapter customerPreAssessmentListDTOAdapter = new CustomerPreAssessmentListAdapter();
                    return customerPreAssessmentListDTOAdapter.GetData(ds);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        //public CustomerPreAssessmentEntity GetCustomerPreAssessmentByCustomerID(int customerID)
        //{
        //    try
        //    {
        //        DataSet ds = _customerPreAssessmentDA.GetCustomerPreAssessmentByCustomerID(customerID);
        //        if ((ds.Tables != null) && (ds.Tables.Contains(_tableName))
        //           && (ds.Tables[_tableName].Rows.Count > 0))
        //        {

        //            #region EpisodeReasonsRel
        //            DataSet ds2 = _customerPreAssessmentReasonRelDA.GetAllCustomerPreAssessmentReasonRelByCustomerID(customerID);
        //            if (ds2 != null && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable].Copy();
        //                dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable;
        //                ds.Tables.Add(dt);

        //                #region Episode reason types
        //                ds2 = _episodeReasonTypeDA.GetAllEpisodeReasonType(ElementReasonTypeEnum.PreAssessment);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }

        //                ds2 = _episodeReasonDA.GetAllEpisodeReasonByType(ElementReasonTypeEnum.PreAssessment);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }

        //                #region EpisodeReasonelementRel
        //                ds2 = _episodeReasonElementRelDA.GetAllEpisodeReasonElementRelByType(ElementReasonTypeEnum.PreAssessment);
        //                if (ds2 != null && ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonElementRelTable))
        //                {
        //                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonElementRelTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //                #endregion

        //                #endregion
        //            }
        //            #endregion

        //            ds2 = _assistanceDegreeDA.GetAllAssistanceDegreeOfPreAssessmentConfig();
        //            if (ds2 != null && ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AssistanceDegreeTable))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AssistanceDegreeTable].Copy();
        //                dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AssistanceDegreeTable;
        //                ds.Tables.Add(dt);
        //            }

        //            ds2 = _preAssessmentTypeDA.GetAllPreAssessmentType();
        //            if (ds2 != null && ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PreAssessmentTypeTable))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PreAssessmentTypeTable].Copy();
        //                dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PreAssessmentTypeTable;
        //                ds.Tables.Add(dt);
        //            }

        //            CustomerPreAssessmentEntity customerPreAssessment;
        //            CustomerPreAssessmentAdvancedAdapter customerPreAssessmentAdapter = new CustomerPreAssessmentAdvancedAdapter();
        //            customerPreAssessment = customerPreAssessmentAdapter.GetInfo(ds.Tables[_tableName].Rows[0], ds);
        //            return customerPreAssessment;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}

        public CustomerPreAssessmentEntity ModifyPreAssessment(int preAssessmentID, CustomerProcessEntity process)
        {
            if (preAssessmentID != 0)
            {
                string userName = IdentityUser.GetIdentityUserName();

                CustomerPreAssessmentEntity customerPreAssessment = this.GetCustomerPreAssessment(preAssessmentID);

                ProcessChartBL _processChartBL = new ProcessChartBL();
                ProcessChartEntity processChart = _processChartBL.GetByID(process.ProcessChartID);

                if (customerPreAssessment != null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        this.InnerUpdateCustomerProcess(customerPreAssessment, process, processChart, userName);

                        scope.Complete();
                    }

                    this.ResetCustomerPreAssessment(customerPreAssessment);
                }

                return customerPreAssessment;
            }
            else
                return null;
        }

        //public CustomerPreAssessmentEntity[] GetByLocation(int locationID)
        //{
        //    try
        //    {
        //        DataSet ds = _customerPreAssessmentDA.GetCustomerPreAssessmentByLocation(locationID);
        //        if ((ds.Tables != null) && (ds.Tables.Contains(_tableName))
        //           && (ds.Tables[_tableName].Rows.Count > 0))
        //        {

        //            #region EpisodeReasonsRel
        //            DataSet ds2 = _customerPreAssessmentReasonRelDA.GetAllCustomerPreAssessmentReasonRelByCustomerID(locationID);
        //            if (ds2 != null && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable].Copy();
        //                dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable;
        //                ds.Tables.Add(dt);


        //                #region Episode reason types
        //                ds2 = _episodeReasonTypeDA.GetAllEpisodeReasonType(ElementReasonTypeEnum.PreAssessment);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }

        //                ds2 = _episodeReasonDA.GetAllEpisodeReasonByType(ElementReasonTypeEnum.PreAssessment);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }

        //                #region EpisodeReasonelementRel
        //                ds2 = _episodeReasonElementRelDA.GetAllEpisodeReasonElementRelByType(ElementReasonTypeEnum.PreAssessment);
        //                if (ds2 != null && ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonElementRelTable))
        //                {
        //                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonElementRelTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //                #endregion
        //                #endregion
        //            }
        //            #endregion

        //            ds2 = _assistanceDegreeDA.GetAllAssistanceDegreeOfPreAssessmentConfig();
        //            if (ds2 != null && ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AssistanceDegreeTable))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AssistanceDegreeTable].Copy();
        //                dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AssistanceDegreeTable;
        //                ds.Tables.Add(dt);
        //            }

        //            ds2 = _preAssessmentTypeDA.GetAllPreAssessmentType();
        //            if (ds2 != null && ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PreAssessmentTypeTable))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PreAssessmentTypeTable].Copy();
        //                dt.TableName = SII.HCD.BackOffice.Entities.TableNames.PreAssessmentTypeTable;
        //                ds.Tables.Add(dt);
        //            }

        //            CustomerPreAssessmentAdvancedAdapter customerPreAssessmentAdapter = new CustomerPreAssessmentAdvancedAdapter();
        //            return customerPreAssessmentAdapter.GetData(ds);
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}

        public bool ExistsCustomerPreAssessmentReasonRelByEpisodeReason(int episodeReasonID)
        {
            try
            {
                return _customerPreAssessmentReasonRelDA.ExistsCustomerPreAssessmentReasonRelByEpisodeReason(episodeReasonID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }


        public bool AssistanceDegreeRelInUse(int assistanceDegreeID, int preAssessmentTypeID, int processChartID)
        {
            try
            {
                return (assistanceDegreeID > 0 && processChartID > 0) 
                    ? _customerPreAssessmentDA.AssistanceDegreeRelInUse(assistanceDegreeID, preAssessmentTypeID, processChartID)
                    : false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool PreAssessmentTypeRelInUse(int preAssessmentTypeID, int processChartID)
        {
            try
            {
                return (preAssessmentTypeID > 0 && processChartID > 0)
                    ? _customerPreAssessmentDA.PreAssessmentTypeRelInUse(preAssessmentTypeID, processChartID)
                    : false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }


        #endregion
    }
}
