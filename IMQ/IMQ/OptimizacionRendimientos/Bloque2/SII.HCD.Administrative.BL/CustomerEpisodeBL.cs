using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using SII.HCD.Administrative.DA;
using SII.HCD.Administrative.Entities;
using SII.HCD.Administrative.Entities.CoverAnalysis;
using SII.HCD.Administrative.Services;
using SII.HCD.Assistance.DA;
using SII.HCD.BackOffice.BL;
using SII.HCD.BackOffice.BL.CodeProvider;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.BackOffice.Services;
using SII.HCD.Common.BL;
using SII.HCD.Common.DA;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Configuration;
using SII.HCD.Misc;
using SII.HCD.Misc.IoC;
using SII.HCD.Misc.Validators;
using SII.SIFP.Configuration;
using AdministrativeEntities = SII.HCD.Administrative.Entities;
using CommonEntities = SII.HCD.Common.Entities;
using System.Threading.Tasks;

namespace SII.HCD.Administrative.BL
{
    public class CustomerEpisodeBL : CustomerAdmissionBL, ICustomerEpisodeService
    {
        #region DA definition
        private CustomerDA _customerDA;
        private CustomerEpisodeDA _customerEpisodeDA;
        private CustomerRoutineDA _customerRoutineDA;
        private CustomerProcedureDA _customerProcedureDA;
        private RoutineActDA _routineActDA;
        private ProcedureActDA _procedureActDA;
        private ProcedureActResourceRelDA _procedureActResourceRelDA;
        private CustomerOrderRequestDA _customerOrderRequestDA;
        private CustomerOrderRealizationDA _customerOrderRealizationDA;
        private CustomerMedEpisodeActDA _customerMedEpisodeActDA;

        private CustomerAccountChargeDA _customerAccountChargeDA;
        protected LocationAvailabilityDA _locationAvailabilityDA;
        private EpisodeTypeDA _episodeTypeDA;
        private AuthorizationTypeDA _authorizationTypeDA;
        private EpisodeCloseModeDA _episodeCloseMode;
        private CustomerPolicyDA _customerPolicyDA;
        private CustomerCardDA _customerCardDA;
        private PolicyTypeDA _policyTypeDA;

        private AssistanceAgreementDA _assistanceAgreementDA;
        private AgreementDA _agreementDA;
        private InsurerCoverAgreementDA _insurerCoverAgreementDA;
        private InsurerAgreementDA _insurerAgreementDA;

        private CustomerAssistAgreeRelDA _customerAssistAgreeRelDA;
        private CustomerAgreeRelDA _customerAgreeRelDA;
        private CustomerCoverAgreeRelDA _customerCoverAgreeRelDA;
        private CustomerInsurerAgreeRelDA _customerInsurerAgreeRelDA;
        private CustomerEpisodeReasonRelDA _customerEpisodeReasonRelDA;
        private CustomerEpisodeLeaveReasonRelDA _customerEpisodeLeaveReasonRelDA;
        private CustomerEpisodeAuthorizationDA _customerEpisodeAuthorizationDA;
        private CustomerEpisodeAuthorizationEntryDA _customerEpisodeAuthorizationEntryDA;
        private CustomerEpisodeAuthorizationOpsDA _customerEpisodeAuthorizationOpsDA;
        private CustomerEpInteropInfoDA _customerEpInteropInfoDA;
        private CustomerEpisodeReferencedPhysicianRelDA _customerEpisodeReferencedPhysicianRelDA;
        private CustomerAccountDA _customerAccountDA;
        private CustomerEpisodeServiceRelDA _customerEpisodeServiceRelDA;
        private CustomerReservationDA _customerReservationDA;
        private LocationAvailPatternDA _locationAvailPatternDA;

        private CustomerTransferEntryDA _customerTransferEntryDA;

        //MT: AÑADIDO PORQUE DESCONOZCO PORQUE NO SE ESTÁN CARGANDO LAS RAZONES EN LOS EPISODIOS
        //EL CÓDIGO ESTA COMENTADO EN LOS MÉTODO Y NO TENGO NI IDEA POR QUÉ
        private EpisodeReasonDA _episodeReasonDA;
        private CustomerEpisodeGuarantorDA _customerEpisodeGuarantorDA;
        private CustomerGuaranteeDA _customerGuaranteeDA;

        private string _tableName;
        #endregion

        #region Fields
        private Dictionary<string, object> _entities = null;
        private Dictionary<string, object> _bls = null;
        private HL7MessagingProcessor _hl7processor = null;

        private CustomerObservationBL _customerObservationBL = null;
        private CustomerReservationBL _customerReservationBL = null;
        private CustomerTransferBL _customerTransferBL = null;
        private CustomerTransferEntryBL _customerTransferEntryBL = null;
        private CustomerGuaranteeBL _customerGuaranteeBL = null;
        private CustomerAccountChargeBL _customerAccountChargeBL = null;
        private ProcessChartBL _processChartBL = null;
        private ElementBL _elementBL = null;
        private CustomerBL _customerBL = null;
        private AssistanceProcessChartBL _assistanceProcessChartBL = null;
        private CustomerAssistancePlanBL _customerAssistancePlanBL = null;
        private LocationBL _locationBL = null;
        private EpisodeTypeBL _episodeTypeBL = null;
        private LocationTypeBL _locationTypeBL = null;
        private CustomerProcessWaitingListBL _cpwlBL = null;
        private PhysicianBL _physicianBL = null;
        private CustomerOrderRequestBL _customerOrderRequestBL = null;
        private CustomerProcessBL _customerProcessBL = null;
        private HistoryAssistanceAgreementBL _haaBL = null;
        private AssistanceAgreementBL _aaBL = null;
        private InsurerBL _insureBL = null;
        private MedicalEpisodeBL _medicalEpisodeBL = null;
        //private MedicalMessagingBL _medicalMessagingBL = null;
        //private CustomerTemporalLeaveBL _customerTemporalLeaveBL = null;
        private LocationAvailabilityBL _locationAvailabilityBL = null;

        private IPhysicianCacheService _physicianCache = null;

        #endregion

        #region Properties
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

        //private MedicalMessagingBL MedicalMessagingBL
        //{
        //    get
        //    {
        //        if (_medicalMessagingBL == null) _medicalMessagingBL = new MedicalMessagingBL();
        //        return _medicalMessagingBL;
        //    }
        //}

        private CustomerObservationBL CustomerObservationBL
        {
            get
            {
                if (_customerObservationBL == null)
                {
                    _customerObservationBL = new CustomerObservationBL();
                    if (!BLs.ContainsKey("CustomerObservationBL"))
                        BLs.Add("CustomerObservationBL", _customerObservationBL);
                }
                return _customerObservationBL;
            }
        }
        private CustomerReservationBL CustomerReservationBL
        {
            get
            {
                if (_customerReservationBL == null)
                {
                    _customerReservationBL = new CustomerReservationBL();
                    if (!BLs.ContainsKey("CustomerReservationBL"))
                        BLs.Add("CustomerReservationBL", _customerReservationBL);
                }
                return _customerReservationBL;
            }
        }
        private CustomerTransferBL CustomerTransferBL
        {
            get
            {
                if (_customerTransferBL == null)
                {
                    _customerTransferBL = new CustomerTransferBL();
                    if (!BLs.ContainsKey("CustomerTransferBL"))
                        BLs.Add("CustomerTransferBL", _customerTransferBL);
                }
                return _customerTransferBL;
            }
        }
        private CustomerTransferEntryBL CustomerTransferEntryBL
        {
            get
            {
                if (_customerTransferEntryBL == null)
                {
                    _customerTransferEntryBL = new CustomerTransferEntryBL();
                    if (!BLs.ContainsKey("CustomerTransferEntryBL"))
                        BLs.Add("CustomerTransferEntryBL", _customerTransferEntryBL);
                }
                return _customerTransferEntryBL;
            }
        }
        private CustomerGuaranteeBL CustomerGuaranteeBL
        {
            get
            {
                if (_customerGuaranteeBL == null)
                {
                    _customerGuaranteeBL = new CustomerGuaranteeBL();
                    if (!BLs.ContainsKey("CustomerGuaranteeBL"))
                        BLs.Add("CustomerGuaranteeBL", _customerGuaranteeBL);
                }
                return _customerGuaranteeBL;
            }
        }
        private CustomerAccountChargeBL CustomerAccountChargeBL
        {
            get
            {
                if (_customerAccountChargeBL == null)
                {
                    _customerAccountChargeBL = new CustomerAccountChargeBL();
                    if (!BLs.ContainsKey("CustomerAccountChargeBL"))
                        BLs.Add("CustomerAccountChargeBL", _customerAccountChargeBL);
                }
                return _customerAccountChargeBL;
            }
        }
        private ProcessChartBL ProcessChartBL
        {
            get
            {
                if (_processChartBL == null)
                {
                    _processChartBL = new ProcessChartBL();
                    if (!BLs.ContainsKey("ProcessChartBL"))
                        BLs.Add("ProcessChartBL", _processChartBL);
                }
                return _processChartBL;
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
        private CustomerBL CustomerBL
        {
            get
            {
                if (_customerBL == null)
                {
                    _customerBL = new CustomerBL();
                    if (!BLs.ContainsKey("CustomerBL"))
                        BLs.Add("CustomerBL", _customerBL);
                }
                return _customerBL;
            }
        }
        private AssistanceProcessChartBL AssistanceProcessChartBL
        {
            get
            {
                if (_assistanceProcessChartBL == null)
                {
                    _assistanceProcessChartBL = new AssistanceProcessChartBL();
                    if (!BLs.ContainsKey("AssistanceProcessChartBL"))
                        BLs.Add("AssistanceProcessChartBL", _assistanceProcessChartBL);
                }
                return _assistanceProcessChartBL;
            }
        }
        private CustomerAssistancePlanBL CustomerAssistancePlanBL
        {
            get
            {
                if (_customerAssistancePlanBL == null)
                {
                    _customerAssistancePlanBL = new CustomerAssistancePlanBL();
                    if (!BLs.ContainsKey("CustomerAssistancePlanBL"))
                        BLs.Add("CustomerAssistancePlanBL", _customerAssistancePlanBL);
                }
                return _customerAssistancePlanBL;
            }
        }
        private LocationBL LocationBL
        {
            get
            {
                if (_locationBL == null)
                {
                    _locationBL = new LocationBL();
                    if (!BLs.ContainsKey("LocationBL"))
                        BLs.Add("LocationBL", _locationBL);
                }
                return _locationBL;
            }
        }
        private EpisodeTypeBL EpisodeTypeBL
        {
            get
            {
                if (_episodeTypeBL == null)
                {
                    _episodeTypeBL = new EpisodeTypeBL();
                    if (!BLs.ContainsKey("EpisodeTypeBL"))
                        BLs.Add("EpisodeTypeBL", _episodeTypeBL);
                }
                return _episodeTypeBL;
            }
        }
        private LocationTypeBL LocationTypeBL
        {
            get
            {
                if (_locationTypeBL == null)
                {
                    _locationTypeBL = new LocationTypeBL();
                    if (!BLs.ContainsKey("LocationTypeBL"))
                        BLs.Add("LocationTypeBL", _locationTypeBL);
                }
                return _locationTypeBL;
            }
        }
        private CustomerProcessWaitingListBL CustomerProcessWaitingListBL
        {
            get
            {
                if (_cpwlBL == null)
                {
                    _cpwlBL = new CustomerProcessWaitingListBL();
                    if (!BLs.ContainsKey("CustomerProcessWaitingListBL"))
                        BLs.Add("CustomerProcessWaitingListBL", _cpwlBL);
                }
                return _cpwlBL;
            }
        }
        private PhysicianBL PhysicianBL
        {
            get
            {
                if (_physicianBL == null)
                {
                    _physicianBL = new PhysicianBL();
                    if (!BLs.ContainsKey("PhysicianBL"))
                        BLs.Add("PhysicianBL", _physicianBL);
                }
                return _physicianBL;
            }
        }
        private CustomerOrderRequestBL CustomerOrderRequestBL
        {
            get
            {
                if (_customerOrderRequestBL == null)
                {
                    _customerOrderRequestBL = new CustomerOrderRequestBL();
                    if (!BLs.ContainsKey("CustomerOrderRequestBL"))
                        BLs.Add("CustomerOrderRequestBL", _customerOrderRequestBL);
                }
                return _customerOrderRequestBL;
            }
        }

        private CustomerProcessBL CustomerProcessBL
        {
            get
            {
                if (_customerProcessBL == null)
                {
                    _customerProcessBL = new CustomerProcessBL();
                    if (!BLs.ContainsKey("CustomerProcessBL"))
                        BLs.Add("CustomerProcessBL", _customerProcessBL);
                }
                return _customerProcessBL;
            }
        }




        private HistoryAssistanceAgreementBL HistoryAssistanceAgreementBL
        {
            get
            {
                if (_haaBL == null)
                {
                    _haaBL = new HistoryAssistanceAgreementBL();
                    if (!BLs.ContainsKey("HistoryAssistanceAgreementBL"))
                        BLs.Add("HistoryAssistanceAgreementBL", _haaBL);
                }
                return _haaBL;
            }
        }

        private AssistanceAgreementBL AssistanceAgreementBL
        {
            get
            {
                if (_aaBL == null)
                {
                    _aaBL = new AssistanceAgreementBL();
                    if (!BLs.ContainsKey("AssistanceAgreementBL"))
                        BLs.Add("AssistanceAgreementBL", _aaBL);
                }
                return _aaBL;
            }
        }

        private InsurerBL InsurerBL
        {
            get
            {
                if (_insureBL == null)
                {
                    _insureBL = new InsurerBL();
                    if (!BLs.ContainsKey("InsurerBL"))
                        BLs.Add("InsurerBL", _insureBL);
                }
                return _insureBL;
            }
        }

        private MedicalEpisodeBL MedicalEpisodeBL
        {
            get
            {
                if (_medicalEpisodeBL == null)
                {
                    _medicalEpisodeBL = new MedicalEpisodeBL();
                    if (!BLs.ContainsKey("MedicalEpisodeBL"))
                        BLs.Add("MedicalEpisodeBL", _medicalEpisodeBL);
                }
                return _medicalEpisodeBL;
            }
        }

        private LocationAvailabilityBL LocationAvailabilityBL
        {
            get
            {
                if (_locationAvailabilityBL == null)
                {
                    _locationAvailabilityBL = new LocationAvailabilityBL();
                }
                return _locationAvailabilityBL;
            }
        }

        //private CustomerTemporalLeaveBL CustomerTemporalLeaveBL
        //{
        //    get
        //    {
        //        if (_customerTemporalLeaveBL == null) _customerTemporalLeaveBL = new CustomerTemporalLeaveBL();
        //        return _customerTemporalLeaveBL;
        //    }
        //}
        #endregion

        #region Constructors
        public CustomerEpisodeBL()
        {
            _customerDA = new CustomerDA();
            _customerEpisodeDA = new CustomerEpisodeDA();
            _customerRoutineDA = new CustomerRoutineDA();
            _customerProcedureDA = new CustomerProcedureDA();
            _routineActDA = new RoutineActDA();
            _procedureActDA = new ProcedureActDA();
            _procedureActResourceRelDA = new ProcedureActResourceRelDA();
            _customerOrderRequestDA = new CustomerOrderRequestDA();
            _customerOrderRealizationDA = new CustomerOrderRealizationDA();
            _customerMedEpisodeActDA = new CustomerMedEpisodeActDA();

            _customerAccountChargeDA = new CustomerAccountChargeDA();
            _locationAvailabilityDA = new LocationAvailabilityDA();
            _episodeTypeDA = new EpisodeTypeDA();
            _authorizationTypeDA = new AuthorizationTypeDA();
            _episodeCloseMode = new EpisodeCloseModeDA();
            _customerPolicyDA = new CustomerPolicyDA();
            _customerCardDA = new CustomerCardDA();
            _policyTypeDA = new PolicyTypeDA();

            _assistanceAgreementDA = new AssistanceAgreementDA();
            _agreementDA = new AgreementDA();
            _insurerCoverAgreementDA = new InsurerCoverAgreementDA();
            _insurerAgreementDA = new InsurerAgreementDA();

            _customerAssistAgreeRelDA = new CustomerAssistAgreeRelDA();
            _customerAgreeRelDA = new CustomerAgreeRelDA();
            _customerCoverAgreeRelDA = new CustomerCoverAgreeRelDA();
            _customerInsurerAgreeRelDA = new CustomerInsurerAgreeRelDA();
            _customerEpisodeReasonRelDA = new CustomerEpisodeReasonRelDA();
            _customerEpisodeLeaveReasonRelDA = new CustomerEpisodeLeaveReasonRelDA();
            _customerEpisodeAuthorizationDA = new CustomerEpisodeAuthorizationDA();
            _customerEpisodeAuthorizationEntryDA = new CustomerEpisodeAuthorizationEntryDA();
            _customerEpisodeAuthorizationOpsDA = new CustomerEpisodeAuthorizationOpsDA();
            _customerEpInteropInfoDA = new CustomerEpInteropInfoDA();
            _customerEpisodeReferencedPhysicianRelDA = new CustomerEpisodeReferencedPhysicianRelDA();
            _customerAccountDA = new CustomerAccountDA();
            _customerEpisodeServiceRelDA = new CustomerEpisodeServiceRelDA();
            _customerReservationDA = new CustomerReservationDA();
            _locationAvailPatternDA = new LocationAvailPatternDA();

            _customerTransferEntryDA = new CustomerTransferEntryDA();

            _tableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable;

            //MT: AÑADIDO PORQUE DESCONOZCO PORQUE NO SE ESTÁN CARGANDO LAS RAZONES, guarantor y guarantee EN LOS EPISODIOS
            _episodeReasonDA = new EpisodeReasonDA();
            _customerEpisodeGuarantorDA = new CustomerEpisodeGuarantorDA();
            _customerGuaranteeDA = new CustomerGuaranteeDA();

            InitializeCache();
        }
        #endregion

        #region Private methods
        private void InitializeCache()
        {
            _physicianCache = IoCFactory.CurrentContainer.Resolve<IPhysicianCacheService>();
        }

        //private CustomerEpisodeEntity Insert(CustomerEpisodeEntity episode, ElementBL elementBL, CustomerBL customerBL, AssistanceProcessChartBL assistanceProcessChartBL, ProcessChartBL processChartBL,
        //    CustomerAssistancePlanBL customerAssistancePlanBL, LocationBL locationBL, EpisodeTypeBL episodeTypeBL, LocationTypeBL locationTypeBL, CustomerProcessWaitingListBL cpwlBL, out StepResponse[] response)
        //{
        //    if (episode == null) throw new ArgumentNullException("Episode");

        //    CustomerProcessEntity customerProcess = base.GetActive(episode.CustomerID, episode.ProcessChartID, 0);  // "0" porque se supone que el episodio es nuevo y aún no está creado

        //    if (customerProcess == null) throw new ArgumentNullException("Customer Process");

        //    string userName = IdentityUser.GetIdentityUserName();

        //    string customerEpisodeNumber;
        //    string admissionEventNumber;
        //    string customerAccountNumber;

        //    ProcessChartEntity processChart = processChartBL.GetByID(episode.ProcessChartID);
        //    CustomerAdmissionEntity admission;
        //    CustomerAssistancePlanEntity customerAssistancePlan;
        //    CustomerProcessWaitingListEntity cpwl;

        //    this.PreInsertCustomerEpisode(episode, customerProcess, userName, out customerEpisodeNumber, out admissionEventNumber, out customerAccountNumber,
        //        processChart, out admission, out customerAssistancePlan, out cpwl, out response, elementBL, assistanceProcessChartBL, locationBL,
        //        episodeTypeBL, locationTypeBL, cpwlBL, customerAssistancePlanBL /*, historyAssistanceAgreementBL, historyAgreementBL,
        //        historyAgreeConditionBL, insurerAgreementBL, historyInsurerCoverAgreementBL, historyInsurerAgreementBL, historyInsurerConditionBL*/);

        //    int insurerID = (episode.AdmissionInsurer != null) ? episode.AdmissionInsurer.ID : 0;

        //    this.InsertCustomerEpisode(episode, customerProcess, userName, customerEpisodeNumber, admissionEventNumber, customerAccountNumber, insurerID,
        //        processChart, admission, customerAssistancePlan, cpwl, customerBL, customerAssistancePlanBL, cpwlBL);

        //    this.PostInsertCustomerEpisode(episode, ref response, processChart, admission);

        //    Entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);

        //    return episode;
        //}

        private void PostInsertCustomerEpisode(CustomerEpisodeEntity episode, ref StepResponse[] response, ProcessChartEntity processChart, CustomerAdmissionEntity admission)
        {
            #region IReceptionStepContract AfterStepAction
            StepResponse[] auxMessages = this.CallAfterStepAction(episode, admission, processChart);
            if ((auxMessages != null) && (auxMessages.Length > 0))
            {
                List<StepResponse> listRespose = (response != null) ? new List<StepResponse>(response) : new List<StepResponse>();
                listRespose.AddRange(auxMessages);
                response = listRespose.ToArray();
            }
            #endregion

            #region Registro de los mensajes del IReceptionStep en el logger
            if ((response != null) && (response.Length > 0))
            {
                this.SaveMessageStepActionInLogger(response);
            }
            #endregion
        }

        //private CustomerEpisodeEntity Update(CustomerEpisodeEntity episode, ElementBL elementBL)
        //{
        //    if (episode == null) throw new ArgumentNullException("Episode");

        //    CustomerProcessEntity customerProcess = base.GetActive(episode.CustomerID, episode.ProcessChartID, episode.ID);
        //    if (customerProcess == null) throw new ArgumentNullException("Customer Process");

        //    string userName = IdentityUser.GetIdentityUserName();

        //    //No es el mejor sitio para validar esto, pero tal y como está planteada esta BL es lo mejor para no modificar métodos pasando el ProcessChart
        //    ProcessChartEntity processChart = ProcessChartBL.GetByID(episode.ProcessChartID);
        //    if ((processChart != null) && (processChart.AdmissionConfig != null) && (processChart.AdmissionConfig.AdmissionOrderRequired) && (episode.ADTOrder == null))
        //    {
        //        throw new Exception(string.Format(Properties.Resources.MSG_ADTOrderRequiredByAdmissionConfiguration, episode.EpisodeNumber));
        //    }

        //    this.PreUpdateCustomerEpisode(episode, customerProcess, elementBL /*, historyAssistanceAgreementBL, historyAgreementBL,
        //        historyAgreeConditionBL, insurerAgreementBL, historyInsurerCoverAgreementBL, historyInsurerAgreementBL, historyInsurerConditionBL*/);

        //    int insurerID = (episode.AdmissionInsurer != null) ? episode.AdmissionInsurer.ID : 0;

        //    episode = this.UpdateCustomerEpisode(episode, customerProcess, insurerID, userName);

        //    Entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);

        //    return episode;
        //}

        private void ValidateCoverAgreements(CustomerCoverAgreeRelEntity[] customerCoverAgreeRelDTO)
        {
            if ((customerCoverAgreeRelDTO == null) || (customerCoverAgreeRelDTO.Length <= 0)) return;
            foreach (CustomerCoverAgreeRelEntity item in customerCoverAgreeRelDTO)
            {
                if (item.HistoryInsurerCoverAgreementID <= 0)
                {
                    throw new NullReferenceException("HistoryInsurerCoverAgreementID");
                }
                if ((item.InsurerAgreements != null) && (item.InsurerAgreements.Length > 0))
                {
                    foreach (CustomerInsurerAgreeRelEntity iadto in item.InsurerAgreements)
                    {
                        if (iadto.HistoryInsurerAgreementID <= 0) throw new NullReferenceException("HistoryInsurerAgreementID");
                    }
                }
            }
        }

        private void ValidateAssisanceAgreements(CustomerAssistAgreeRelEntity[] customerAssistAgreeRelDTO)
        {
            if ((customerAssistAgreeRelDTO == null) || (customerAssistAgreeRelDTO.Length <= 0)) return;
            foreach (CustomerAssistAgreeRelEntity item in customerAssistAgreeRelDTO)
            {
                if ((item.HistoryAssistanceAgreementID <= 0) || (item.HistoryCareCenterID <= 0))
                {
                    throw new NullReferenceException("HistoryAssistanceAgreementID");
                }
                if ((item.Agreements != null) && (item.Agreements.Length > 0))
                {
                    foreach (CustomerAgreeRelEntity adto in item.Agreements)
                    {
                        if (adto.HistoryAgreementID <= 0) throw new NullReferenceException("HistoryAgreementID");
                    }
                }
            }
        }

        private void ValidateAuthorizations(CustomerEpisodeAuthorizationEntity[] authorizations)
        {
        }

        private void ValidateAuthorizationEntries(CustomerEpisodeAuthorizationEntryEntity[] authorizationEntries)
        {
        }

        private StepResponse[] CallBeforeStepAction(CustomerEpisodeEntity customerEpisode, CustomerAdmissionEntity customerAdmission, ProcessChartEntity processChart)
        {
            List<StepResponse> messages = new List<StepResponse>();
            //addins de intercambio
            return messages.ToArray();
        }

        private StepResponse[] CallAfterStepAction(CustomerEpisodeEntity customerEpisode, CustomerAdmissionEntity customerAdmission, ProcessChartEntity processChart)
        {
            List<StepResponse> messages = new List<StepResponse>();
            //addins de intercambio
            return messages.ToArray();
        }

        private void SaveMessageStepActionInLogger(StepResponse[] response)
        {
            foreach (StepResponse sr in response)
            {
                switch (sr.Result)
                {
                    case AddinActionResultEnum.OK:
                        Logger.Write(sr.Message, Category.Information, Priority.Normal, 0, System.Diagnostics.TraceEventType.Information, EntityNames.ReceptionStepActionName);
                        break;
                    case AddinActionResultEnum.Warning:
                        Logger.Write(sr.Message, Category.Warning, Priority.Normal, 0, System.Diagnostics.TraceEventType.Warning, EntityNames.ReceptionStepActionName);
                        break;
                    case AddinActionResultEnum.Error:
                        Logger.Write(sr.Message, Category.Error, Priority.Normal, 0, System.Diagnostics.TraceEventType.Error, EntityNames.ReceptionStepActionName);
                        break;
                    case AddinActionResultEnum.CriticalError:
                        Logger.Write(sr.Message, Category.CriticalError, Priority.Normal, 0, System.Diagnostics.TraceEventType.Critical, EntityNames.ReceptionStepActionName);
                        break;
                    default: break;
                }
            }
        }

        private string GetStringListIDForQuerySQL(int[] listIDs)
        {
            if ((listIDs == null) || (listIDs.Length <= 0))
                return string.Empty;

            bool first = true;
            string result = string.Empty;
            foreach (int id in listIDs)
            {
                if (first)
                {
                    result += id.ToString();
                    first = false;
                }
                else result += string.Concat(", ", id.ToString());
            }
            return result;
        }

        private static List<int> GetEpisodeIDsFromDataset(DataSet ds)
        {
            List<int> result = new List<int>();
            if ((ds.Tables != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeTable)) &&
                (ds.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0))
            {
                foreach (DataRow row in ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows)
                {
                    if ((row != null) && (row.Table.Columns.Contains("ID")))
                    {
                        int epID = row["ID"] as int? ?? 0;
                        if (epID > 0) result.Add(epID);
                    }
                }
            }
            return result;
        }

        private string GetEpisodeTypeIDsStringWhereIn(CareCenterEpisodeTypeDTO[] episodeTypes)
        {
            if ((episodeTypes == null) || (episodeTypes.Length <= 0))
                return string.Empty;

            string stringEpisodeTypes = "(";
            bool _firstEpType = false;
            foreach (CareCenterEpisodeTypeDTO ept in episodeTypes)
            {
                if ((ept != null) && (ept.ID > 0))
                    stringEpisodeTypes += string.Concat((_firstEpType) ? ", " : string.Empty, ept.ID.ToString());
                _firstEpType = true;
            }
            stringEpisodeTypes += ")";
            return stringEpisodeTypes;
        }

        private string GetCoverAgreementCodesStringWhereIn(CoverAgreementCodeDTO[] coverAgreementCodes)
        {
            if ((coverAgreementCodes == null) || (coverAgreementCodes.Length <= 0))
                return string.Empty;

            string stringCoverAgreementCodes = "(";
            bool _firstCACodes = false;
            foreach (CoverAgreementCodeDTO cover in coverAgreementCodes)
            {
                if ((cover != null) && (cover.ID > 0))
                    stringCoverAgreementCodes += string.Concat((_firstCACodes) ? ", " : string.Empty, "'", cover.ID.ToString(), "'");
                _firstCACodes = true;
            }
            stringCoverAgreementCodes += ")";
            return stringCoverAgreementCodes;
        }

        private string GetAgreementCodesStringWhereIn(AgreementCodeDTO[] agreementCodes)
        {
            if ((agreementCodes == null) || (agreementCodes.Length <= 0))
                return string.Empty;

            string stringAgreementCodes = "(";
            bool _firstAgreementCodes = false;
            foreach (AgreementCodeDTO agree in agreementCodes)
            {
                if ((agree != null) && (agree.ID > 0))
                    stringAgreementCodes += string.Concat((_firstAgreementCodes) ? ", " : string.Empty, "'", agree.ID.ToString(), "'");
                _firstAgreementCodes = true;
            }
            stringAgreementCodes += ")";
            return stringAgreementCodes;
        }

        private void AssignCustomerAssistAgree(CustomerEpisodeEntity episode, int historyCareCenterID, HistoryAssistanceAgreementEntity haa, string userName)
        {
            if ((haa == null) || (episode == null) || (episode.AssistanceAgreements != null) || (historyCareCenterID <= 0)) return;
            CustomerAssistAgreeRelEntity caa = new CustomerAssistAgreeRelEntity(0, 0, 0, episode.ID, haa.AmountQty, historyCareCenterID, haa.ID, 0,
                CommonEntities.StatusEnum.Active, null, DateTime.Now, userName, 0);
            caa.EditStatus.New();
            episode.AssistanceAgreements = new CustomerAssistAgreeRelEntity[] { caa };
        }

        private int GetHistoryCareCenter(CustomerProcessEntity customerProcess)
        {
            CareCenterDA _careCenterDA = new CareCenterDA();
            return (customerProcess.CareCenterID > 0) ? _careCenterDA.GetHistoryCareCenterID(customerProcess.CareCenterID) : 0;
        }

        private void ValidateSetDefaultAssistanceAgreements(CustomerEpisodeEntity episode, CustomerProcessEntity customerProcess, ProcessChartEntity processChart, string userName)
        {
            if ((episode == null) || (processChart == null) || (customerProcess == null)
                || (processChart.StepsInProcess == null)
                || (!Array.Exists(processChart.StepsInProcess, (BasicStepsInProcessEntity bsp) => bsp.ProcessStep == BasicProcessStepsEnum.CoverAnalysis))
                || (processChart.CoverConfig == null)) return;
            if (processChart.CoverConfig.DefaultAssistanceAgree != null)
            {
                int hccID = GetHistoryCareCenter(customerProcess);
                //HistoryAssistanceAgreementBL haaBL = new HistoryAssistanceAgreementBL();
                HistoryAssistanceAgreementEntity haa = (hccID > 0) ? HistoryAssistanceAgreementBL.GetSimpleByCode(processChart.CoverConfig.DefaultAssistanceAgree.AssignedCode, hccID) : null;
                AssignCustomerAssistAgree(episode, hccID, haa, userName);
            }
            if (((processChart.CoverConfig.CoverAnalysisAtDischarge) || (processChart.CoverConfig.DefaultAssistanceAgree == null) ||
                    (processChart.CoverConfig.ChargesNotCovered == ChargesNotCoveredEnum.ToCustomer))
                && (episode.AssistanceAgreements == null) && (episode.CoverAgreements != null) && (episode.CoverAgreements.Length > 0))
            {
                //AssistanceAgreementBL aaBL = new AssistanceAgreementBL();
                int haaID = AssistanceAgreementBL.GetRelatedHistoryAssistanceAgreementID(episode.CoverAgreements[0].HistoryInsurerCoverAgreementID);
                //HistoryAssistanceAgreementBL haaBL = new HistoryAssistanceAgreementBL();
                HistoryAssistanceAgreementEntity haa = (haaID > 0) ? HistoryAssistanceAgreementBL.Get(haaID) : null;
                AssignCustomerAssistAgree(episode, GetHistoryCareCenter(customerProcess), haa, userName);
            }
        }

        private void AddCoverConfigAssistanceAgreement(CustomerEpisodeEntity episode, ProcessChartEntity processChart, CustomerProcessEntity customerProcess)
        {
            HistoryAssistanceAgreementEntity[] samplesHAA = GetSimplesHAA(episode.AssistanceAgreements);
            if (processChart.CoverConfig != null && processChart.CoverConfig.DefaultAssistanceAgree != null &&
                (samplesHAA == null || !Array.Exists(samplesHAA, haa => haa.AssignedCode == processChart.CoverConfig.DefaultAssistanceAgree.AssignedCode)))
            {
                int historyCareCenterID = this.GetHistoryCareCenter(customerProcess);
                HistoryAssistanceAgreementEntity haa = this.GetHistoryAssistanceAgreement(processChart.CoverConfig.DefaultAssistanceAgree.AssignedCode, historyCareCenterID);
                if (haa != null)
                {
                    CustomerAssistAgreeRelEntity caar = new CustomerAssistAgreeRelEntity();
                    caar.CustomerEpisodeID = episode.ID;
                    caar.HistoryCareCenterID = historyCareCenterID;
                    caar.HistoryAssistanceAgreementID = haa.ID;
                    caar.RequiredAutorization = haa.AuthorizationRequired;
                    caar.Status = CommonEntities.StatusEnum.Active;
                    caar.EditStatus.New();
                    List<CustomerAssistAgreeRelEntity> celist = new List<CustomerAssistAgreeRelEntity>();
                    if (episode.AssistanceAgreements != null && episode.AssistanceAgreements.Length > 0) celist.AddRange(episode.AssistanceAgreements);
                    celist.Add(caar);
                    episode.AssistanceAgreements = celist.ToArray();
                }
            }
        }

        private void LoadPredecessorIDsInList(int episodeID, DataSet dataset, ref List<int> customerEpisodeIDs)
        {
            if (episodeID <= 0 || !dataset.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeTable)
                || dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows == null
                || dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count <= 0)
                return;

            int colPosition = dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable]
                .Columns["ID"].Ordinal;

            if (colPosition < 0)
                return;

            var result = (from row in dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].AsEnumerable()
                          where (row[colPosition] as int? == episodeID)
                          select new
                          {
                              CustomerEpisodeID = row["ID"] as int? ?? 0,
                              CustomerID = row["CustomerID"] as int? ?? 0,
                              PredecessorID = row["PredecessorID"] as int? ?? 0
                          }).FirstOrDefault();

            if (result != null)
            {
                if (!customerEpisodeIDs.Contains(episodeID))
                    customerEpisodeIDs.Add(episodeID);

                if (result.PredecessorID > 0)
                {
                    this.LoadPredecessorIDsInList(result.PredecessorID, dataset, ref customerEpisodeIDs);
                }
            }
        }

        private void LoadCustomerEpisodeWithPredecessorIDInList(int predecessorID, DataSet dataset, ref List<int> customerEpisodeIDs)
        {
            if (predecessorID <= 0 || !dataset.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeTable)
                || dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows == null
                || dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count <= 0)
                return;

            int colPosition = dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable]
                .Columns["PredecessorID"].Ordinal;

            if (colPosition < 0)
                return;

            var result = (from row in dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].AsEnumerable()
                          where (row[colPosition] as int? == predecessorID)
                          select new
                          {
                              CustomerEpisodeID = row["ID"] as int? ?? 0,
                              CustomerID = row["CustomerID"] as int? ?? 0,
                              PredecessorID = row["PredecessorID"] as int? ?? 0
                          }).FirstOrDefault();

            if ((result != null) && (result.CustomerEpisodeID > 0))
            {
                if (!customerEpisodeIDs.Contains(result.CustomerEpisodeID))
                    customerEpisodeIDs.Add(result.CustomerEpisodeID);
                this.LoadCustomerEpisodeWithPredecessorIDInList(result.CustomerEpisodeID, dataset, ref customerEpisodeIDs);
            }
        }

        private void LoadEpisodeIDAndRelatedEpisodeIDsInList(int episodeID, DataSet dataset, ref List<int> customerEpisodeIDs)
        {
            if (episodeID <= 0 || !dataset.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeTable)
                || dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows == null
                || dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count <= 0)
                return;

            int colPosition = dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable]
                .Columns["ID"].Ordinal;

            if (colPosition < 0)
                return;

            var result = (from row in dataset.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].AsEnumerable()
                          where (row[colPosition] as int? == episodeID)
                          select new
                          {
                              CustomerEpisodeID = row["ID"] as int? ?? 0,
                              CustomerID = row["CustomerID"] as int? ?? 0,
                              PredecessorID = row["PredecessorID"] as int? ?? 0
                          }).FirstOrDefault();

            if (result != null)
            {
                if (!customerEpisodeIDs.Contains(result.CustomerEpisodeID))
                    customerEpisodeIDs.Add(result.CustomerEpisodeID);
                this.LoadPredecessorIDsInList(result.PredecessorID, dataset, ref customerEpisodeIDs);
                this.LoadCustomerEpisodeWithPredecessorIDInList(result.CustomerEpisodeID, dataset, ref customerEpisodeIDs);
            }
        }

        public CustomerEpisodeWithChargesDTO[] GetCustomerEpisodeWithChargesDTOByCustomerEpisodeIDs(int[] customerEpisodeIDs, bool auditAllowed)
        {
            if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;

            try
            {
                CommonEntities.ElementEntity agreeConditionElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.AgreeConditionEntityName, true);
                CommonEntities.ElementEntity insurerConditionElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.InsurerConditionEntityName, false);

                string result = this.GetListCustomerEpisodeIDsForSQL(this.GetDistinctCustomerEpisodeIDs(customerEpisodeIDs));

                DataSet ds = _customerEpisodeDA.GetCustomerEpisodeWithChargesDTOByStringListCustomerEpisodesID(result);
                if ((ds != null)
                    && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeWithChargesDTOTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerEpisodeWithChargesDTOTable].Rows.Count > 0))
                {
                    DataSet ds2;

                    #region Customer Account Charges
                    ds2 = _customerAccountChargeDA.GetCustomerAccountChargeByStringListCustomerEpisodeID(result, (agreeConditionElement != null) ? agreeConditionElement.ID : 0,
                        (insurerConditionElement != null) ? insurerConditionElement.ID : 0,
                        auditAllowed);

                    if ((ds2 != null)
                        && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerAccountChargeTable)
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerAccountChargeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerAccountChargeTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    CustomerEpisodeWithChargesDTOAdvancedAdapter adapter = new CustomerEpisodeWithChargesDTOAdvancedAdapter();
                    return SuppressDuplicatedByCustomerEpisode(adapter.GetData(ds));
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private string GetListCustomerEpisodeIDsForSQL(int[] customerEpisodeIDs)
        {
            if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return string.Empty;

            bool first = true;
            string result = string.Empty;
            foreach (int ceID in customerEpisodeIDs)
            {
                if (first)
                {
                    result += ceID.ToString();
                    first = false;
                }
                else result += string.Concat(", ", ceID.ToString());
            }
            return result;
        }

        private int[] GetDistinctCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;

            return (from ceID in customerEpisodeIDs
                    select ceID).Distinct().ToArray();
        }

        private string GetStatKey(int routineTypeID, ActionStatusEnum status)
        {
            return string.Format("{0}.{1}", routineTypeID, status);
        }

        private RoutineTypeStatusSummary FindEpisodeStat(
            CustomerEpisodeStatistic customerEpisode, int routineTypeID, ActionStatusEnum status)
        {
            if (customerEpisode == null || customerEpisode.EpisodeStats == null || customerEpisode.EpisodeStats.Length <= 0)
                return null;

            return customerEpisode.EpisodeStats.FirstOrDefault(es => es.RoutineTypeID == routineTypeID && es.Status == status);
        }

        private int CalculateNumberOfActs(string meaning, DateTime start, DateTime end)
        {
            if (string.IsNullOrWhiteSpace(meaning) || start > end)
                return 0;

            TimePatternEditionEntity tpe = new TimePatternEditionEntity(meaning);
            return tpe.GetDateTimes(start, end).Count();
        }

        private void ProcessEpisodes(DataSet stats, Dictionary<int, CustomerEpisodeStatistic> result)
        {
            if (stats == null || !stats.Tables.Contains(AdministrativeEntities.TableNames.CustomerEpisodeTable))
                return;

            foreach (DataRow row in stats.Tables[AdministrativeEntities.TableNames.CustomerEpisodeTable].Rows)
            {
                CustomerEpisodeStatistic item = new CustomerEpisodeStatistic();
                item.CustomerEpisodeID = row["CustomerEpisodeID"] as int? ?? 0;
                item.CustomerID = row["CustomerID"] as int? ?? 0;
                item.PersonID = row["PersonID"] as int? ?? 0;
                item.IdentificationNumber = row["IdentificationNumber"] as string ?? string.Empty;
                item.FirstName = row["FirstName"] as string ?? string.Empty;
                item.LastName = row["LastName"] as string ?? string.Empty;
                item.LastName2 = row["LastName2"] as string ?? string.Empty;
                item.Sex = EnumUtils.GetEnum<SII.HCD.BackOffice.Entities.SexEnum>(row["Sex"]);
                item.SexDescription = SexEnumNames.GetName(item.Sex);
                item.BirthDate = row["BirthDate"] as DateTime? ?? DateTime.MinValue;
                item.CHNumber = row["CHNumber"] as string ?? string.Empty;
                item.EpisodeNumber = row["EpisodeNumber"] as string ?? string.Empty;
                item.EpisodeType = row["EpisodeTypeName"] as string ?? string.Empty;
                result.Add(item.CustomerEpisodeID, item);
            }
        }

        private void ProcessReasons(DataSet reasons, Dictionary<int, CustomerEpisodeStatistic> result)
        {
            if (reasons == null || !reasons.Tables.Contains(AdministrativeEntities.TableNames.CustomerEpisodeReasonRelTable))
                return;

            foreach (DataRow row in reasons.Tables[AdministrativeEntities.TableNames.CustomerEpisodeReasonRelTable].Rows)
            {
                int customerEpisodeID = row["CustomerEpisodeID"] as int? ?? 0;
                CustomerEpisodeStatistic customerEpisode;
                if (!result.TryGetValue(customerEpisodeID, out customerEpisode))
                    continue;

                List<EpisodeReasonEntity> items = new List<EpisodeReasonEntity>();
                if (customerEpisode.Reasons != null)
                    items.AddRange(customerEpisode.Reasons);

                EpisodeReasonEntity item = new EpisodeReasonEntity();
                item.ID = row["EpisodeReasonID"] as int? ?? 0;
                item.AssignedCode = row["EpisodeReasonCode"] as string ?? string.Empty;
                item.FullySpecifiedName = row["EpisodeReasonName"] as string ?? string.Empty;
                item.Description = row["EpisodeReasonDescription"] as string ?? string.Empty;
                item.EpisodeReasonType = new EpisodeReasonTypeEntity();
                item.EpisodeReasonType.ID = row["EpisodeReasonTypeID"] as int? ?? 0;
                item.EpisodeReasonType.AssignedCode = row["EpisodeReasonTypeCode"] as string ?? string.Empty;
                item.EpisodeReasonType.Name = row["EpisodeReasonTypeName"] as string ?? string.Empty;
                item.EpisodeReasonType.Description = row["EpisodeReasonTypeDescription"] as string ?? string.Empty;
                items.Add(item);

                customerEpisode.Reasons = items.ToArray();
            }
        }

        private void ProcessEpisodeStats(DataSet episodeStats, Dictionary<int, CustomerEpisodeStatistic> result)
        {
            if (episodeStats == null || !episodeStats.Tables.Contains(AdministrativeEntities.TableNames.CustomerEpisodeTable))
                return;

            foreach (DataRow row in episodeStats.Tables[AdministrativeEntities.TableNames.CustomerEpisodeTable].Rows)
            {
                int customerEpisodeID = row["CustomerEpisodeID"] as int? ?? 0;
                CustomerEpisodeStatistic customerEpisode;
                if (!result.TryGetValue(customerEpisodeID, out customerEpisode))
                    continue;

                int routineTypeID = row["RoutineTypeID"] as int? ?? 0;
                ActionStatusEnum status = EnumUtils.GetEnum<ActionStatusEnum>(row["Status"]);
                RoutineTypeStatusSummary stat = FindEpisodeStat(customerEpisode, routineTypeID, status);
                if (stat == null)
                {
                    stat = new RoutineTypeStatusSummary();
                    stat.RoutineTypeID = routineTypeID;
                    stat.RoutineTypeCode = row["RoutineTypeCode"] as string ?? string.Empty;
                    stat.RoutineTypeName = row["RoutineTypeName"] as string ?? string.Empty;
                    stat.Status = status;
                    stat.StatusDescription = ActionStatusEnumNames.GetName(status);

                    List<RoutineTypeStatusSummary> stats = new List<RoutineTypeStatusSummary>();
                    if (customerEpisode.EpisodeStats != null)
                        stats.AddRange(customerEpisode.EpisodeStats);
                    stats.Add(stat);
                    customerEpisode.EpisodeStats = stats.ToArray();
                }

                stat.Count += row["Total"] as int? ?? 0;
            }
        }

        private void ProcessEpisodeProgrammings(DataSet programmings, DateTime startDateTime,
            DateTime endDateTime, Dictionary<int, CustomerEpisodeStatistic> result)
        {
            if (programmings == null || !programmings.Tables.Contains(AdministrativeEntities.TableNames.CustomerRoutineTable))
                return;

            foreach (DataRow row in programmings.Tables[AdministrativeEntities.TableNames.CustomerRoutineTable].Rows)
            {
                int customerEpisodeID = row["CustomerEpisodeID"] as int? ?? 0;
                CustomerEpisodeStatistic customerEpisode;
                if (!result.TryGetValue(customerEpisodeID, out customerEpisode))
                    continue;

                DateTime start = row["LatestSchedule"] as DateTime? ?? DateTime.MinValue;
                if (start == DateTime.MinValue)
                    start = row["StartAt"] as DateTime? ?? DateTime.MinValue;
                if (start < startDateTime)
                    start = startDateTime;

                DateTime end = row["EndingTo"] as DateTime? ?? DateTime.MinValue;
                if (end == DateTime.MinValue || end > endDateTime)
                    end = endDateTime;

                int numberOfActs = CalculateNumberOfActs(row["Meaning"] as string ?? string.Empty, start, end);
                if (numberOfActs > 0)
                {
                    int routineTypeID = row["RoutineTypeID"] as int? ?? 0;
                    ActionStatusEnum status = ActionStatusEnum.Scheduled;
                    RoutineTypeStatusSummary stat = FindEpisodeStat(customerEpisode, routineTypeID, status);
                    if (stat == null)
                    {
                        stat = new RoutineTypeStatusSummary();
                        stat.RoutineTypeID = routineTypeID;
                        stat.RoutineTypeCode = row["RoutineTypeCode"] as string ?? string.Empty;
                        stat.RoutineTypeName = row["RoutineTypeName"] as string ?? string.Empty;
                        stat.Status = status;
                        stat.StatusDescription = ActionStatusEnumNames.GetName(status);

                        List<RoutineTypeStatusSummary> stats = new List<RoutineTypeStatusSummary>();
                        if (customerEpisode.EpisodeStats != null)
                            stats.AddRange(customerEpisode.EpisodeStats);
                        stats.Add(stat);
                        customerEpisode.EpisodeStats = stats.ToArray();
                    }

                    stat.Count += numberOfActs;
                }
            }
        }

        private void ProcessStats(DataSet stats, Dictionary<string, RoutineTypeStatusSummary> result)
        {
            if (stats == null || !stats.Tables.Contains(AdministrativeEntities.TableNames.CustomerEpisodeTable))
                return;

            foreach (DataRow row in stats.Tables[AdministrativeEntities.TableNames.CustomerEpisodeTable].Rows)
            {
                int routineTypeID = row["RoutineTypeID"] as int? ?? 0;
                ActionStatusEnum status = EnumUtils.GetEnum<ActionStatusEnum>(row["Status"]);
                string key = GetStatKey(routineTypeID, status);

                RoutineTypeStatusSummary stat;
                if (result.ContainsKey(key))
                {
                    stat = result[key];
                }
                else
                {
                    stat = new RoutineTypeStatusSummary();
                    stat.RoutineTypeID = routineTypeID;
                    stat.RoutineTypeCode = row["RoutineTypeCode"] as string ?? string.Empty;
                    stat.RoutineTypeName = row["RoutineTypeName"] as string ?? string.Empty;
                    stat.Status = status;
                    stat.StatusDescription = ActionStatusEnumNames.GetName(status);
                    result.Add(key, stat);
                }

                stat.Count += row["Total"] as int? ?? 0;
            }
        }

        private void ProcessProgrammings(DataSet programmings, DateTime startDateTime, DateTime endDateTime,
            Dictionary<string, RoutineTypeStatusSummary> result)
        {
            if (programmings == null || !programmings.Tables.Contains(AdministrativeEntities.TableNames.CustomerRoutineTable))
                return;

            foreach (DataRow row in programmings.Tables[AdministrativeEntities.TableNames.CustomerRoutineTable].Rows)
            {
                DateTime start = row["LatestSchedule"] as DateTime? ?? DateTime.MinValue;
                if (start == DateTime.MinValue)
                    start = row["StartAt"] as DateTime? ?? DateTime.MinValue;
                if (start < startDateTime)
                    start = startDateTime;

                DateTime end = row["EndingTo"] as DateTime? ?? DateTime.MinValue;
                if (end == DateTime.MinValue || end > endDateTime)
                    end = endDateTime;

                int numberOfActs = CalculateNumberOfActs(row["Meaning"] as string ?? string.Empty, start, end);
                if (numberOfActs > 0)
                {
                    int routineTypeID = row["RoutineTypeID"] as int? ?? 0;
                    ActionStatusEnum status = ActionStatusEnum.Scheduled;
                    string key = GetStatKey(routineTypeID, status);

                    RoutineTypeStatusSummary stat;
                    if (result.ContainsKey(key))
                    {
                        stat = result[key];
                    }
                    else
                    {
                        stat = new RoutineTypeStatusSummary();
                        stat.RoutineTypeID = routineTypeID;
                        stat.RoutineTypeCode = row["RoutineTypeCode"] as string ?? string.Empty;
                        stat.RoutineTypeName = row["RoutineTypeName"] as string ?? string.Empty;
                        stat.Status = status;
                        stat.StatusDescription = ActionStatusEnumNames.GetName(status);
                        result.Add(key, stat);
                    }

                    stat.Count += numberOfActs;
                }
            }
        }

        //private CustomerEpisodeEntity AbortAdmission(CustomerEpisodeEntity customerEpisode, ReasonChangeEntity reasonChange, string explanation, ProcessChartEntity processChart)
        //{
        //    string userName = IdentityUser.GetIdentityUserName();

        //    int customerProcessID = 0;
        //    CustomerProcessEntity customerProcess = GetCustomerProcessByCustomerEpisodeID(customerEpisode.ID);
        //    if (customerProcess != null)
        //        customerProcessID = customerProcess.ID;

        //    CustomerProcessReasonRelDA _customerProcessReasonRelDA = new CustomerProcessReasonRelDA();
        //    MedicalEpisodeDA _medicalEpisodeDA = new MedicalEpisodeDA();
        //    CustomerProcessDA _customerProcessDA = new CustomerProcessDA();
        //    NotificationActBL _notificationActBL = new NotificationActBL();
        //    CustomerEpisodeAction cea = new CustomerEpisodeAction(
        //        customerEpisode,
        //        CustomerEpisodeActionEnum.Abort,
        //        reasonChange,
        //        explanation);

        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        //Save reason change of customer process
        //        _customerProcessReasonRelDA.Insert(customerProcessID, BasicProcessStepsEnum.Admission, customerEpisode.CustomerAdmissionID, reasonChange.ID, explanation, userName);

        //        //Episode Cancellation
        //        _customerEpisodeDA.UpdateCancelled(customerEpisode.ID, DateTime.Now, userName);

        //        //Admission Cancellation
        //        if (customerEpisode.CustomerAdmissionID != 0)
        //        {
        //            _customerAdmissionDA.UpdateStatus(customerEpisode.CustomerAdmissionID, DateTime.Now, userName, CommonEntities.StatusEnum.Cancelled);
        //            base.InnerSaveBasicProcessStep(customerProcessID, BasicProcessStepsEnum.Admission, customerEpisode.CustomerAdmissionID, CommonEntities.StatusEnum.Cancelled,
        //                customerEpisode.StartDateTime, null, userName);
        //            customerProcess = base.RefreshStepCustomerProcess(customerProcess);
        //        }

        //        //Location Availability Cancellation
        //        if ((customerEpisode.CurrentLocationAvail != null) && (customerEpisode.CurrentLocationAvail.ID > 0))
        //            _locationAvailabilityDA.Update(customerEpisode.CurrentLocationAvail.ID, DateTime.Now, AvailStatusEnum.Cancelled, userName);

        //        //Customer Routine Cancellation
        //        _customerRoutineDA.UpdateStatusByCustomerEpisodeID(customerEpisode.ID, userName, ActionStatusEnum.Cancelled);

        //        //Customer Procedure Cancellation
        //        _customerProcedureDA.UpdateStatusByCustomerEpisodeID(customerEpisode.ID, userName, ActionStatusEnum.Cancelled);

        //        //Routine Act Cancellation
        //        _routineActDA.UpdateStatusByCustomerEpisodeID(customerEpisode.ID, userName, ActionStatusEnum.Cancelled);

        //        //Procedure Act Cancellation
        //        _procedureActDA.UpdateStatusByCustomerEpisodeID(customerEpisode.ID, userName, ActionStatusEnum.Cancelled);

        //        //Customer Order Request Cancellation
        //        _customerOrderRequestDA.UpdateStatusByCustomerEpisodeID(customerEpisode.ID, userName, ActionStatusEnum.Cancelled);

        //        //Customer Medical Episode Act Cancellation
        //        _customerMedEpisodeActDA.UpdateStatusByCustomerEpisodeID(customerEpisode.ID, userName, ActionStatusEnum.Cancelled);

        //        //Medical Episode Cancellation
        //        _medicalEpisodeDA.UpdateStatusByCustomerEpisodeID(customerEpisode.ID, userName, DateTime.Now, ActionStatusEnum.Cancelled);

        //        //Customer Medical Process Cancellation
        //        _medicalEpisodeDA.UpdateCustomerMedProcessStatusByCustomerEpisodeID(customerEpisode.ID, userName, DateTime.Now, ActionStatusEnum.Cancelled);

        //        long availableProcessSteps = base.InnerGetAvailableProcessSteps(customerProcess, processChart);
        //        _customerProcessDA.UpdateAvailableProcessStep(customerProcess.ID, availableProcessSteps, userName);

        //        _notificationActBL.HandleNotifications(cea);

        //        scope.Complete();
        //    }

        //    _notificationActBL.SendNotifications();
        //    _notificationActBL.ResetNotifications();

        //    return GetFullCustomerEpisode(customerEpisode.ID);
        //}

        //private bool ValidateAbortAdmission(CustomerEpisodeEntity customerEpisode, CustomerProcessEntity customerProcess, ProcessChartEntity processChart)
        //{
        //    if (customerEpisode == null) throw new Exception(Properties.Resources.MSG_CustomerEpisodeNotExists);
        //    if (customerProcess == null) throw new Exception(Properties.Resources.MSG_CustomerProcessNotExists);
        //    if (processChart == null || processChart.StepsInProcess == null) throw new ArgumentNullException("processChart");

        //    //ProcessChartBL _processChartBL = new ProcessChartBL();
        //    //CustomerAccountChargeBL _customerAccountChargeBL = new CustomerAccountChargeBL();

        //    DeliveryNoteDA _deliveryNoteDA = new DeliveryNoteDA();
        //    InvoiceDA _invoiceDA = new InvoiceDA();
        //    RemittanceContentDA _remittanceContentDA = new RemittanceContentDA();

        //    switch (customerEpisode.Status)
        //    {
        //        case SII.HCD.Common.Entities.StatusEnum.Cancelled:
        //            throw new Exception(Properties.Resources.ERROR_CustomerEpisodeAlreadyCancelled);
        //        case SII.HCD.Common.Entities.StatusEnum.Closed:
        //            throw new Exception(Properties.Resources.ERROR_CustomerEpisodeClosed);
        //        case SII.HCD.Common.Entities.StatusEnum.Held:
        //            throw new Exception(Properties.Resources.ERROR_CustomerEpisodeHeld);
        //        default:
        //            break;
        //    }

        //    #region validar si existen otros pasos de proceso invalidantes
        //    //// Si existe Reserva hay que liberar los recursos de la reserva y cancelar la resereva
        //    //if (Array.Exists(processChart.StepsInProcess, step => step.ProcessStep == BasicProcessStepsEnum.Reservation &&
        //    //    customerProcess.GetStepID(BasicProcessStepsEnum.Reservation) > 0))
        //    //{
        //    //    CustomerReservationEntity cre = CustomerReservationBL.GetCustomerReservation(customerProcess.GetStepID(BasicProcessStepsEnum.Reservation));
        //    //    if (cre.Status != CommonEntities.StatusEnum.Cancelled && cre.Status != CommonEntities.StatusEnum.Superceded)
        //    //        throw new Exception(Properties.Resources.MSG_ExistsActiveCustomerReservation);
        //    //}

        //    //// Si existe Fianza hay que devolver la fianza.
        //    if (Array.Exists(processChart.StepsInProcess, step => step.ProcessStep == BasicProcessStepsEnum.Guarantee && customerProcess.GetStepID(BasicProcessStepsEnum.Guarantee) > 0))
        //    {
        //        CustomerGuaranteeEntity cge = CustomerGuaranteeBL.GetCustomerGuarantee(customerProcess.GetStepID(BasicProcessStepsEnum.Guarantee));
        //        if ((cge.Status != CommonEntities.StatusEnum.Cancelled && cge.Status != CommonEntities.StatusEnum.Superceded) ||
        //            cge.TotalDepositAmount > (cge.TotalReturnedAmount + cge.TotalCompensationAmount))
        //            throw new Exception(Properties.Resources.MSG_ExistsActiveCustomerGuarantee);
        //    }

        //    //// Si exsite transfer interno hay que cancelar los Transfer
        //    if (Array.Exists(processChart.StepsInProcess, step => step.ProcessStep == BasicProcessStepsEnum.Transfer && customerProcess.GetStepID(BasicProcessStepsEnum.Transfer) > 0))
        //    {
        //        CustomerTransferEntity cte = CustomerTransferBL.GetCustomerTransfer(customerProcess.GetStepID(BasicProcessStepsEnum.Transfer));
        //        if ((cte.Status != CommonEntities.StatusEnum.Cancelled && cte.Status != CommonEntities.StatusEnum.Superceded)
        //            || (cte.Entries != null && Array.Exists(cte.Entries, entry => entry.Status != CommonEntities.StatusEnum.Cancelled && entry.Status != CommonEntities.StatusEnum.Superceded)))
        //            throw new Exception(Properties.Resources.MSG_ExistsActiveCustomerTransfer);
        //        //// Si existe transfer temporal hay que cancelar los transfer temporales.
        //    }
        //    //// Si existe temporalleave  hay que cancelar el alta temporal.
        //    // pendiente de desarrollar
        //    #endregion

        //    #region validar si existe CustomerRoutine con episodio
        //    if (_customerRoutineDA.ExistsByCustomerEpisodeID(customerEpisode.ID, new int[] { (int)ActionStatusEnum.Initiated, (int)ActionStatusEnum.Completed }))
        //        throw new Exception(Properties.Resources.ERROR_ExistsCustomerRoutineByCustomerEpisode);
        //    #endregion

        //    #region validar si existe CustomerProcedure con episodio
        //    if (_customerProcedureDA.ExistsByCustomerEpisodeID(customerEpisode.ID, new int[] { (int)ActionStatusEnum.Initiated, (int)ActionStatusEnum.Completed }))
        //        throw new Exception(Properties.Resources.ERROR_ExistsCustomerProcedureByCustomerEpisode);
        //    #endregion

        //    #region validar si existe RoutineAct con episodio
        //    if (_routineActDA.ExistsByCustomerEpisodeID(customerEpisode.ID, new int[] { (int)ActionStatusEnum.Initiated, (int)ActionStatusEnum.Completed, (int)ActionStatusEnum.Confirmed }))
        //        throw new Exception(Properties.Resources.ERROR_ExistsRoutineActByCustomerEpisode);
        //    #endregion

        //    #region validar si existe ProcedureAct con episodio
        //    if (_procedureActDA.ExistsByCustomerEpisodeID(customerEpisode.ID, new int[] { (int)ActionStatusEnum.Initiated, (int)ActionStatusEnum.Completed, (int)ActionStatusEnum.Confirmed }))
        //        throw new Exception(Properties.Resources.ERROR_ExistsProcedureActByCustomerEpisode);
        //    #endregion

        //    #region validar si existe CustomerOrderRequest con episodio
        //    if (_customerOrderRequestDA.ExistsByCustomerEpisodeID(customerEpisode.ID, new int[] { (int)ActionStatusEnum.Confirmed }))
        //        throw new Exception(Properties.Resources.ERROR_ExistsCustomerOrderRequestByCustomerEpisode);
        //    #endregion

        //    #region validar si existe CustomerMedEpisodeAct con episodio
        //    if (_customerMedEpisodeActDA.ExistsByCustomerEpisodeID(customerEpisode.ID, new int[] { (int)ActionStatusEnum.Initiated, (int)ActionStatusEnum.Waiting, (int)ActionStatusEnum.Held, (int)ActionStatusEnum.Confirmed }))
        //        throw new Exception(Properties.Resources.ERROR_ExistsCustomerMedEpisodeActByCustomerEpisode);
        //    #endregion

        //    #region validar si existen customer account charges (Covered o Pending)
        //    if (CustomerAccountChargeBL.ExistsChargesPendingOrCoveredByCustomerEpisodeAndStatus(customerEpisode.ID))
        //        throw new Exception(Properties.Resources.MSG_ExistsPendingCharges);
        //    #endregion

        //    return true;
        //}

        private HistoryAssistanceAgreementEntity[] GetSimplesHAA(CustomerAssistAgreeRelEntity[] customerAssistAgreeRels)
        {
            if (customerAssistAgreeRels == null) return null;
            HistoryAssistanceAgreementBL _historyAssistanceAgreementBL = new BackOffice.BL.HistoryAssistanceAgreementBL();
            List<HistoryAssistanceAgreementEntity> haalist = new List<HistoryAssistanceAgreementEntity>();
            foreach (CustomerAssistAgreeRelEntity item in customerAssistAgreeRels)
            {
                HistoryAssistanceAgreementEntity haa = _historyAssistanceAgreementBL.GetSimpleByID(item.HistoryAssistanceAgreementID);
                if (haa != null) haalist.Add(haa);
            }
            return (haalist.Count > 0) ? haalist.ToArray() : null;
        }

        private HistoryAssistanceAgreementEntity GetHistoryAssistanceAgreement(string assignedCode, int historyCareCenterID)
        {
            if (string.IsNullOrEmpty(assignedCode)) return null;
            HistoryAssistanceAgreementBL _historyAssistanceAgreementBL = new BackOffice.BL.HistoryAssistanceAgreementBL();
            return _historyAssistanceAgreementBL.GetSimpleByCode(assignedCode, historyCareCenterID);
        }

        private CustomerEpisodeDTO[] SuppressDuplicatedByCustomerEpisode(CustomerEpisodeDTO[] episodes)
        {
            if ((episodes != null) && (episodes.Length > 0))
            {
                List<CustomerEpisodeDTO> episodeList = new List<CustomerEpisodeDTO>();
                foreach (CustomerEpisodeDTO item in episodes)
                {
                    if (!episodeList.Exists(ep => ep.ID == item.ID))
                        episodeList.Add(item);
                }
                return episodeList.Count > 0 ? episodeList.ToArray() : null;
            }
            return null;
        }

        private CustomerEpisodeWithChargesDTO[] SuppressDuplicatedByCustomerEpisode(CustomerEpisodeWithChargesDTO[] episodes)
        {
            if ((episodes != null) && (episodes.Length > 0))
            {
                List<CustomerEpisodeWithChargesDTO> episodeList = new List<CustomerEpisodeWithChargesDTO>();
                foreach (CustomerEpisodeWithChargesDTO item in episodes)
                {
                    if (!episodeList.Exists(ep => ep.ID == item.ID))
                        episodeList.Add(item);
                }
                return episodeList.Count > 0 ? episodeList.ToArray() : null;
            }
            return null;
        }

        private CustomerBaseEpisodeWithChargesDTO[] SuppressDuplicatedByCustomerEpisode(CustomerBaseEpisodeWithChargesDTO[] episodes)
        {
            if ((episodes != null) && (episodes.Length > 0))
            {
                foreach (CustomerBaseEpisodeWithChargesDTO item in episodes)
                {
                    if ((item.CustomerEpisodes != null) && (item.CustomerEpisodes.Length > 1))
                        item.CustomerEpisodes = SuppressDuplicatedByCustomerEpisode(item.CustomerEpisodes);
                }
            }

            return episodes;
        }

        private bool LocationHasAvailPatterns(int locationID)
        {
            if (locationID <= 0)
                return false;

            return LocationBL.HasAvailPatterns(locationID);
        }

        public LocationAvailabilityEntity GetFirstEpisodeLocationAvailability(int customerEpisodeID)
        {
            LocationAvailabilityEntity[] avails =
                LocationAvailabilityBL.GetEpisodeLocationAvailabilities(customerEpisodeID);
            if (avails == null || avails.Length <= 0)
                return null;

            return avails.OrderBy(la => la.StartDateTime)
                         .FirstOrDefault();
        }

        private LocationAvailabilityEntity GetPreviousLocationAvailability(
            int locationID, DateTime referenceDateTime, bool ignoreCleaningStatus)
        {
            return LocationAvailabilityBL.GetPreviousLocationAvailability(
                    locationID, referenceDateTime, ignoreCleaningStatus);
        }

        private LocationAvailabilityEntity GetNextLocationAvailability(
            int locationID, int currentLocationAvailID, DateTime referenceDateTime, bool ignoreCleaningStatus)
        {
            return LocationAvailabilityBL.GetNextLocationAvailability(
                    locationID, currentLocationAvailID, referenceDateTime, ignoreCleaningStatus);
        }

        private LocationAvailabilityEntity[] GetCurrentLocationAvailabilities(
            int locationID, DateTime referenceDateTime)
        {
            return LocationAvailabilityBL.GetLocationAvailabilities(locationID, referenceDateTime, referenceDateTime);
        }

        private LocationAvailabilityEntity[] GetCurrentLocationAvailabilities(
            int locationID, DateTime referenceDateTime, int customerEpisodeID)
        {
            return LocationAvailabilityBL.GetLocationAvailabilities(locationID, referenceDateTime, referenceDateTime, customerEpisodeID);
        }

        private LocationAvailabilityEntity[] GetCurrentLocationAvailabilities(
           int locationID, DateTime startDateTime, DateTime endDateTime, int customerEpisodeID)
        {
            return LocationAvailabilityBL.GetLocationAvailabilities(locationID, startDateTime, endDateTime, customerEpisodeID);
        }

        private DateTimeRange GetActiveRoutineActsDateTimeRange(int customerEpisodeID, DateTimeRange dateRange)
        {
            DateTimeRange result = new DateTimeRange();
            if (customerEpisodeID > 0)
            {
                DataSet ds = _routineActDA.GetRoutineActByCustomerEpisodeAndDateRange(
                    customerEpisodeID, dateRange.Start, dateRange.End);
                if (ds != null && ds.Tables.Contains(SII.HCD.Assistance.Entities.TableNames.RoutineActTable))
                {
                    result.Start = ds.Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActTable]
                                 .AsEnumerable()
                                 .Where(r => EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Initiated
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Confirmed
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Completed)
                                 .Min(r => r.Field<DateTime?>("StartDateTime"));
                    result.End = ds.Tables[SII.HCD.Assistance.Entities.TableNames.RoutineActTable]
                                 .AsEnumerable()
                                 .Where(r => EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Initiated
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Confirmed
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Completed)
                                 .Max(r => r.Field<DateTime?>("StartDateTime"));
                }
            }

            return result;
        }

        private DateTimeRange GetActiveProcedureActsDateTimeRange(int customerEpisodeID, DateTimeRange dateRange)
        {
            DateTimeRange result = new DateTimeRange();
            if (customerEpisodeID > 0)
            {
                DataSet ds = _procedureActDA.GetNoPrescriptionProcedureActByCustomerEpisodeAndDateRange(
                    customerEpisodeID, dateRange.Start, dateRange.End);
                if (ds != null && ds.Tables.Contains(SII.HCD.Assistance.Entities.TableNames.ProcedureActTable))
                {
                    result.Start = ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActTable]
                                 .AsEnumerable()
                                 .Where(r => EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Initiated
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Confirmed
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Completed)
                                 .Min(r => r.Field<DateTime?>("StartDateTime"));
                    result.End = ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActTable]
                                 .AsEnumerable()
                                 .Where(r => EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Initiated
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Confirmed
                                            || EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Completed)
                                 .Max(r => r.Field<DateTime?>("StartDateTime"));
                }
            }

            return result;
        }

        private DateTimeRange GetActiveProcedureActsResourceRelDateTimeRange(int customerEpisodeID, DateTimeRange dateRange)
        {
            DateTimeRange result = new DateTimeRange();
            if (customerEpisodeID > 0)
            {
                List<int> prescriptionIDs = new List<int>();

                DataSet ds = _procedureActResourceRelDA.GetProcedureActResourceRelByCustomerEpisodeAndDateRange(
                    customerEpisodeID, dateRange.Start, dateRange.End);
                if (ds != null && ds.Tables.Contains(SII.HCD.Assistance.Entities.TableNames.ProcedureActResourceRelTable))
                {
                    result.Start = ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActResourceRelTable]
                                 .AsEnumerable()
                                 .Where(r => EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Completed)
                                 .Min(r => r.Field<DateTime?>("RealizationDateTime"));
                    result.End = ds.Tables[SII.HCD.Assistance.Entities.TableNames.ProcedureActResourceRelTable]
                                 .AsEnumerable()
                                 .Where(r => EnumUtils.GetEnum<ActionStatusEnum>(r.Field<short>("Status")) == ActionStatusEnum.Completed)
                                 .Max(r => r.Field<DateTime?>("RealizationDateTime"));
                }
            }

            return result;
        }

        private DateTimeRange GetActivityRange(int customerEpisodeID, DateTimeRange dateRange)
        {
            DateTimeRange result = new DateTimeRange();

            DateTimeRangeCollection dts = new DateTimeRangeCollection();
            //Buscamos actos de rutinas en estado iniciado, confirmado y completado
            dts.Add(GetActiveRoutineActsDateTimeRange(customerEpisodeID, dateRange));
            //Buscamos actos de procedimientos (no prescripcion) en estado iniciado, confirmado y completado
            dts.Add(GetActiveProcedureActsDateTimeRange(customerEpisodeID, dateRange));
            //Buscamos tomas de prescripcionesen estado completado
            //dts.Add(GetActiveProcedureActsResourceRelDateTimeRange(customerEpisodeID, dateRange));

            result.Start = dts.Min(d => d.Start);
            result.End = dts.Max(d => d.End);

            return result;
        }

        private void GetCustomerAdmissionDateTime(int customerEpisodeID, out DateTime? admissionDate, out int currentLocationAvailID)
        {
            admissionDate = null;
            currentLocationAvailID = 0;
            if (customerEpisodeID <= 0)
                return;

            DataSet ds = _customerEpisodeDA.GetCustomerEpisode(customerEpisodeID);
            if (ds != null && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable)
                && ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0)
            {
                DataRow row = ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows[0];
                admissionDate = row.Field<DateTime>("StartDateTime");
                currentLocationAvailID = row.Field<int>("CurrentLocationAvailID");
            }
        }

        private DateTimeRange InnerGetValidAdmissionDateRange(int customerEpisodeID, int locationID, bool ignoreCleaningStatus)
        {
            DateTimeRange result = new DateTimeRange();

            //Obtener episodio
            DateTime? admissionDateTime;
            int currentLocationAvailID;
            GetCustomerAdmissionDateTime(customerEpisodeID, out admissionDateTime, out currentLocationAvailID);

            //Obtener la primera ocupación del episodio y su ubicación
            LocationAvailabilityEntity firstEpisodeLocationAvailability = GetFirstEpisodeLocationAvailability(customerEpisodeID);
            int firstLocationID = (firstEpisodeLocationAvailability != null)
                                    ? firstEpisodeLocationAvailability.LocationID
                                    : locationID;

            //Verificamos si la ubicación tiene control de disponibilidad.
            bool hasAvailability = LocationHasAvailPatterns(firstLocationID);
            bool firstLocationNotAvailPattern = false;

            if (hasAvailability)
            {
                //Verificamos si ha habido un transfer y en ese caso si el origen
                //es una ubicación sin control de ocupación
                CustomerTransferEntryEntity[] transfers = CustomerTransferEntryBL.GetCustomerTransferEntriesByEpisodeID(customerEpisodeID);
                

                if (transfers != null && transfers.Any(t => firstEpisodeLocationAvailability != null
                                                        && firstEpisodeLocationAvailability.EndDateTime.HasValue
                                                        && t.TransferAtDateTime == firstEpisodeLocationAvailability.EndDateTime
                                                        && LocationHasAvailPatterns(t.TargetLocationID)))
                    firstLocationNotAvailPattern = true;

                //Obtener ocupación previa a fecha de admisión para establecerlo como fecha mínima
                DateTime referenceDateTime = new DateTime();
                DateTime referenceNotAvailPatternDateTime = new DateTime();

                if (firstEpisodeLocationAvailability != null && firstEpisodeLocationAvailability.LocationID > 0 &&
                            firstEpisodeLocationAvailability.LocationID == locationID)
                {
                    referenceDateTime = firstEpisodeLocationAvailability.StartDateTime;
                    referenceNotAvailPatternDateTime = referenceDateTime;
                }
                else if (firstLocationNotAvailPattern && firstEpisodeLocationAvailability != null && firstEpisodeLocationAvailability.EndDateTime.HasValue)
                {
                    referenceDateTime = firstEpisodeLocationAvailability.StartDateTime;
                    referenceNotAvailPatternDateTime = admissionDateTime ?? DateTime.Now;
                }
                else
                {
                    referenceDateTime = admissionDateTime ?? DateTime.Now;
                    referenceNotAvailPatternDateTime = referenceDateTime;
                }

                LocationAvailabilityEntity previousAvail = GetPreviousLocationAvailability(
                    firstLocationID, referenceDateTime, ignoreCleaningStatus);

                result.Start = (previousAvail != null)
                                        ? previousAvail.EndDateTime
                                        : null;

                //Obtenemos la ocupación que delimita detrás de la fecha de referencia
                if (customerEpisodeID > 0 && firstEpisodeLocationAvailability != null)
                {
                    //Si hay episodio, estará definido por la primera ubicación en la que estuvo asignado
                    result.End = firstEpisodeLocationAvailability.EndDateTime;
                }
                else
                {
                    //Si no hay episodio, estará definido por la primera ocupación posterior a la fecha
                    LocationAvailabilityEntity nextAvail = GetNextLocationAvailability(
                        firstLocationID,
                        (previousAvail != null)
                            ? previousAvail.ID
                            : 0,
                        referenceDateTime,
                        ignoreCleaningStatus);

                    result.End = (nextAvail != null)
                                        ? (DateTime?)nextAvail.StartDateTime
                                        : null;

                }

                //Verificamos las ocupaciones que contengan la fecha de referencia
                //LocationAvailabilityEntity[] avails = GetCurrentLocationAvailabilities(
                //        firstLocationID, referenceDateTime);
                //LocationAvailabilityEntity[] avails = GetCurrentLocationAvailabilities(
                //        firstLocationID, referenceDateTime, customerEpisodeID);

                LocationAvailabilityEntity[] avails = GetCurrentLocationAvailabilities(
                        firstLocationID, referenceNotAvailPatternDateTime, customerEpisodeID);
                if (avails != null && avails.Length > 0)
                {
                    foreach (LocationAvailabilityEntity item in avails)
                    {
                        if (item.ID != currentLocationAvailID
                            && item.Status != AvailStatusEnum.Free
                            && item.Status != AvailStatusEnum.Reserved
                            && (item.Status != AvailStatusEnum.Cleaning || !ignoreCleaningStatus))
                        {
                            //DateTimeRange intersection = result.GetIntersection(new DateTimeRange(item.StartDateTime, item.EndDateTime));
                            //if (intersection != null)
                            //{
                            DateTimeRange itemRange = new DateTimeRange(item.StartDateTime, item.EndDateTime,
                                DateRangeBoundaryType.Excluded, DateRangeBoundaryType.Excluded);
                            DateTimeRangeCollection obverseIntersection = itemRange.GetObverse(result);
                            if (obverseIntersection == null)
                                return null;

                            var items = obverseIntersection.Contains(referenceDateTime);
                            if (items == null)
                                return null;

                            result = items.FirstOrDefault();
                            if (result == null)
                                return null;
                            //}
                        }
                    }
                }
            }

            //Obtenermos la fecha del último traslado
            DateTime? firstTransferDateTime = CustomerTransferBL.GetFirstTransferDateTime(customerEpisodeID);

            //////////////////////////////////

            CustomerEpisodeEntity EpisodioOrigen = new CustomerEpisodeEntity();

            bool TrasladoUrgenciasHospi = false;

            CustomerEpisodeEntity CustomerEpisodeDestino = GetFullCustomerEpisode(customerEpisodeID);

            if (CustomerEpisodeDestino != null && CustomerEpisodeDestino.PredecessorID > 0)
            {
                EpisodioOrigen = GetFullCustomerEpisode(CustomerEpisodeDestino.PredecessorID);
                if (EpisodioOrigen.ProcessChartID == 1 && CustomerEpisodeDestino.ProcessChartID == 2)
                    TrasladoUrgenciasHospi = true;

            }

            ////////////////////////////////////////////////////////////////////////////////

            if (firstTransferDateTime.HasValue && (!result.End.HasValue || firstTransferDateTime < result.End) && TrasladoUrgenciasHospi == false)
            //if (firstTransferDateTime.HasValue && (!result.End.HasValue))
            {
                result.End = firstTransferDateTime;
            }

            //DateTime? lastTransferDateTime = CustomerTransferBL.GetLastTransferDateTime(customerEpisodeID);
            //if (lastTransferDateTime.HasValue && (!result.End.HasValue || lastTransferDateTime < result.End))
            //{
            //    result.End = lastTransferDateTime;
            //}

            //Verificamos que la fecha final siempre es anterior a la actual
            DateTime current = DateTime.Now;
            if (!result.End.HasValue || result.End.Value > DateTime.Now)
            {
                result.End = current;
            }

            //Analizamos si hay actividad en el rango de fechas obtenido para el episodio
            if (customerEpisodeID > 0 && !firstLocationNotAvailPattern)
            {
                DateTimeRange activityRange = GetActivityRange(customerEpisodeID, result);
                //if (activityRange.Start.HasValue && activityRange.Start.Value < result.End.Value)
                if (activityRange.Start.HasValue && activityRange.End.Value > result.End.Value)
                {
                    result.End = activityRange.End;
                }
            }

            return result;
        }

        #endregion

        #region Private methods related to customer episode guarantor
        protected virtual CustomerEpisodeGuarantorEntity InnerInsertCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor, string userName)
        {
            customerEpisodeGuarantor.ID = _customerEpisodeGuarantorDA.Insert(customerEpisodeGuarantor.CustomerID, customerEpisodeGuarantor.CustomerEpisodeID, (int)customerEpisodeGuarantor.GuarantorType,
                customerEpisodeGuarantor.Person.ID, customerEpisodeGuarantor.PersonRelationship, customerEpisodeGuarantor.Priority, customerEpisodeGuarantor.InvAddress.ID,
                customerEpisodeGuarantor.CoverAmountQty, customerEpisodeGuarantor.GoesDate, customerEpisodeGuarantor.ExpirationDate, (int)customerEpisodeGuarantor.Status, userName);
            return customerEpisodeGuarantor;
        }

        protected virtual CustomerEpisodeGuarantorEntity InnerUpdateCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor, string userName)
        {
            Int64 dbTimeStamp = _customerEpisodeGuarantorDA.GetDBTimeStamp(customerEpisodeGuarantor.ID);
            if (dbTimeStamp != customerEpisodeGuarantor.DBTimeStamp)
                throw new Exception(string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, customerEpisodeGuarantor.ID));

            _customerEpisodeGuarantorDA.Update(customerEpisodeGuarantor.ID, customerEpisodeGuarantor.CustomerID, customerEpisodeGuarantor.CustomerEpisodeID, (int)customerEpisodeGuarantor.GuarantorType,
                customerEpisodeGuarantor.Person.ID, customerEpisodeGuarantor.PersonRelationship, customerEpisodeGuarantor.Priority, customerEpisodeGuarantor.InvAddress.ID,
                customerEpisodeGuarantor.CoverAmountQty, customerEpisodeGuarantor.GoesDate, customerEpisodeGuarantor.ExpirationDate, (int)customerEpisodeGuarantor.Status, userName);
            customerEpisodeGuarantor.DBTimeStamp = _customerEpisodeGuarantorDA.GetDBTimeStamp(customerEpisodeGuarantor.ID);
            return customerEpisodeGuarantor;
        }

        protected int InnerDeleteCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor, string userName)
        {
            RecordDeletedLogDA _recordDeletedLogDA = new RecordDeletedLogDA();
            _recordDeletedLogDA.NewRecordDeletedLogIDBased(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable, "ID", customerEpisodeGuarantor.ID,
                "Person", customerEpisodeGuarantor.Person.FullName, userName);

            int result = _customerEpisodeGuarantorDA.DeleteCustomerEpisodeGuarantor(customerEpisodeGuarantor.ID);
            return result;
        }

        private bool ExistCustomerEpisodeGuarantor(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor)
        {
            return (_customerEpisodeGuarantorDA.ExistCustomerEpisodeGuarantor(customerEpisodeGuarantor.CustomerID, customerEpisodeGuarantor.CustomerEpisodeID, customerEpisodeGuarantor.Person.ID));
        }

        private void ValidateCustomerEpisodeGuarantor(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor, ElementBL elementBL)
        {
            if (customerEpisodeGuarantor == null) throw new ArgumentNullException("customerEpisodeGuarantor");

            CommonEntities.ElementEntity entityMetadata = ElementBL.GetElementByName(EntityNames.CustomerEpisodeGuarantorEntityName);
            CustomerEpisodeGuarantorHelper helper = new CustomerEpisodeGuarantorHelper(entityMetadata);

            ValidationResults result = helper.Validate(customerEpisodeGuarantor);

            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in result)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_ProcedureClassificationValidationError, sb));
            }
        }

        private void CheckInsertPreconditionsCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor, ElementBL elementBL)
        {
            if (customerEpisodeGuarantor == null) throw new ArgumentNullException("customerEpisodeGuarantor");

            ValidateCustomerEpisodeGuarantor(customerEpisodeGuarantor, elementBL);

            bool existCEG = this.ExistCustomerEpisodeGuarantor(customerEpisodeGuarantor);

            if (existCEG) throw new Exception(Properties.Resources.MSG_CustomerEpisodeGuarantorFound);
        }

        private void CheckUpdatePreconditionsCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor, ElementBL elementBL)
        {
            if (customerEpisodeGuarantor == null) throw new ArgumentNullException("customerEpisodeGuarantor");

            ValidateCustomerEpisodeGuarantor(customerEpisodeGuarantor, elementBL);

            int id = _customerEpisodeGuarantorDA.GetCustomerEpisodeGuarantorID(customerEpisodeGuarantor.CustomerID, customerEpisodeGuarantor.CustomerEpisodeID, customerEpisodeGuarantor.Person.ID);
            if ((id > 0) && (id != customerEpisodeGuarantor.ID))
                throw new Exception(Properties.Resources.MSG_CustomerEpisodeGuarantorFound);
        }

        private void CheckDeletePreconditionsCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor)
        {
            if (customerEpisodeGuarantor == null) throw new ArgumentNullException("customerEpisodeGuarantor");
        }

        private CustomerEpisodeGuarantorEntity InsertCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor)
        {
            if (customerEpisodeGuarantor == null) throw new ArgumentNullException("customerEpisodeGuarantor");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerInsertCEG(customerEpisodeGuarantor, userName);

                scope.Complete();
            }

            customerEpisodeGuarantor.EditStatus.Reset();
            LOPDLogger.Write(EntityNames.CustomerEpisodeGuarantorEntityName, customerEpisodeGuarantor.ID, ActionType.Create);
            return customerEpisodeGuarantor;
        }

        private CustomerEpisodeGuarantorEntity UpdateCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor)
        {
            if (customerEpisodeGuarantor == null) throw new ArgumentNullException("customerEpisodeGuarantor");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerUpdateCEG(customerEpisodeGuarantor, userName);

                scope.Complete();
            }

            customerEpisodeGuarantor.EditStatus.Reset();
            LOPDLogger.Write(EntityNames.CustomerEpisodeGuarantorEntityName, customerEpisodeGuarantor.ID, ActionType.Modify);
            return customerEpisodeGuarantor;
        }

        private CustomerEpisodeGuarantorEntity DeleteCEG(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor)
        {
            if (customerEpisodeGuarantor == null) throw new ArgumentNullException("customerEpisodeGuarantor");

            string userName = IdentityUser.GetIdentityUserName();

            using (TransactionScope scope = new TransactionScope())
            {
                this.InnerDeleteCEG(customerEpisodeGuarantor, userName);

                scope.Complete();
            }
            return customerEpisodeGuarantor;
        }

        private int[] GetProcessChartIDsContainsStepBillingByEpisodeType(CareCenterEpisodeTypeDTO[] episodeTypes)
        {
            if ((episodeTypes == null) || (episodeTypes.Length <= 0))
                return null;

            ProcessChartEntity[] processCharts = ProcessChartBL.GetAllProcessCharts();

            if ((processCharts == null) || (processCharts.Length <= 0))
                return null;

            return (from pc in processCharts
                    where (pc.StepsInProcess != null) && (pc.Status == CommonEntities.StatusEnum.Active)
                        && (pc.EpisodeConfig != null) && Array.Exists(episodeTypes, et => et.ID == pc.EpisodeConfig.ID)
                        && (Array.Exists(pc.StepsInProcess, (BasicStepsInProcessEntity bsp) => bsp.ProcessStep == BasicProcessStepsEnum.Charges)
                            || Array.Exists(pc.StepsInProcess, (BasicStepsInProcessEntity bsp) => bsp.ProcessStep == BasicProcessStepsEnum.CoverAnalysis)
                            || Array.Exists(pc.StepsInProcess, (BasicStepsInProcessEntity bsp) => bsp.ProcessStep == BasicProcessStepsEnum.DeliveryNote)
                            || Array.Exists(pc.StepsInProcess, (BasicStepsInProcessEntity bsp) => bsp.ProcessStep == BasicProcessStepsEnum.Invoice)
                            || Array.Exists(pc.StepsInProcess, (BasicStepsInProcessEntity bsp) => bsp.ProcessStep == BasicProcessStepsEnum.Remittance))
                    select pc.ID).ToArray();
        }
        #endregion

        #region Public methods without service
        public void ResetCustomerEpisode(CustomerEpisodeEntity customerEpisode)
        {
            customerEpisode.EditStatus.Reset();

            if (customerEpisode.CurrentLocationAvail != null)
            {
                customerEpisode.CurrentLocationAvail.EditStatus.Reset();
            }

            if (customerEpisode.CurrentEquipmentAvail != null)
            {
                customerEpisode.CurrentEquipmentAvail.EditStatus.Reset();
            }

            if (customerEpisode.EpisodeReasons != null)
            {
                List<CustomerEpisodeReasonRelEntity> episodeReasons = new List<CustomerEpisodeReasonRelEntity>();
                foreach (CustomerEpisodeReasonRelEntity episodeReason in customerEpisode.EpisodeReasons)
                {
                    if ((episodeReason.EditStatus.Value != StatusEntityValue.Deleted) && (episodeReason.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        episodeReason.EditStatus.Reset();
                        episodeReasons.Add(episodeReason);
                    }
                }
                customerEpisode.EpisodeReasons = episodeReasons.ToArray();
            }

            if (customerEpisode.EpisodeLeaveReasons != null)
            {
                List<CustomerEpisodeLeaveReasonRelEntity> episodeLeaveReasons = new List<CustomerEpisodeLeaveReasonRelEntity>();
                foreach (CustomerEpisodeLeaveReasonRelEntity episodeLeaveReason in customerEpisode.EpisodeLeaveReasons)
                {
                    if ((episodeLeaveReason.EditStatus.Value != StatusEntityValue.Deleted) && (episodeLeaveReason.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        episodeLeaveReason.EditStatus.Reset();
                        episodeLeaveReasons.Add(episodeLeaveReason);
                    }
                }
                customerEpisode.EpisodeLeaveReasons = episodeLeaveReasons.ToArray();
            }

            if (customerEpisode.ContractCoverAgreements != null)
            {
                List<ContractCoverAgreementEntity> contractCoverAgreements = new List<ContractCoverAgreementEntity>();
                foreach (ContractCoverAgreementEntity contractCoverAgreement in customerEpisode.ContractCoverAgreements)
                {
                    if ((contractCoverAgreement.EditStatus.Value != StatusEntityValue.Deleted) && (contractCoverAgreement.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        contractCoverAgreement.EditStatus.Reset();
                        contractCoverAgreements.Add(contractCoverAgreement);
                    }
                }
                customerEpisode.ContractCoverAgreements = contractCoverAgreements.ToArray();
            }

            if (customerEpisode.AssistanceAgreements != null)
            {
                List<CustomerAssistAgreeRelEntity> assistanceAgreements = new List<CustomerAssistAgreeRelEntity>();
                foreach (CustomerAssistAgreeRelEntity assistanceAgreement in customerEpisode.AssistanceAgreements)
                {
                    if ((assistanceAgreement.EditStatus.Value != StatusEntityValue.Deleted) && (assistanceAgreement.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        assistanceAgreement.EditStatus.Reset();
                        assistanceAgreements.Add(assistanceAgreement);
                    }
                }
                customerEpisode.AssistanceAgreements = assistanceAgreements.ToArray();
            }

            if (customerEpisode.CoverAgreements != null)
            {
                List<CustomerCoverAgreeRelEntity> coverAgreements = new List<CustomerCoverAgreeRelEntity>();
                foreach (CustomerCoverAgreeRelEntity coverAgreement in customerEpisode.CoverAgreements)
                {
                    if ((coverAgreement.EditStatus.Value != StatusEntityValue.Deleted) && (coverAgreement.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        coverAgreement.EditStatus.Reset();
                        coverAgreements.Add(coverAgreement);
                    }
                }
                customerEpisode.CoverAgreements = coverAgreements.ToArray();
            }

            if (customerEpisode.CustomerPolicy != null)
            {
                customerEpisode.CustomerPolicy.EditStatus.Reset();
            }

            if (customerEpisode.CustomerAuthorizations != null)
            {
                List<CustomerEpisodeAuthorizationEntity> customerEpisodeAuthorizations = new List<CustomerEpisodeAuthorizationEntity>();
                foreach (CustomerEpisodeAuthorizationEntity customerEpisodeAuthorization in customerEpisode.CustomerAuthorizations)
                {
                    if ((customerEpisodeAuthorization.EditStatus.Value != StatusEntityValue.Deleted) && (customerEpisodeAuthorization.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        customerEpisodeAuthorization.EditStatus.Reset();
                        customerEpisodeAuthorizations.Add(customerEpisodeAuthorization);
                    }

                    if (customerEpisodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                    {
                        List<CustomerEpisodeAuthorizationEntryEntity> customerEpisodeAuthorizationEntries = new List<CustomerEpisodeAuthorizationEntryEntity>();
                        foreach (CustomerEpisodeAuthorizationEntryEntity customerEpisodeAuthorizationEntry in customerEpisodeAuthorization.CustomerEpisodeAuthorizationEntries)
                        {
                            if ((customerEpisodeAuthorizationEntry.EditStatus.Value != StatusEntityValue.Deleted) && (customerEpisodeAuthorizationEntry.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                            {
                                customerEpisodeAuthorizationEntry.EditStatus.Reset();
                                customerEpisodeAuthorizationEntries.Add(customerEpisodeAuthorizationEntry);
                            }
                        }
                        customerEpisodeAuthorization.CustomerEpisodeAuthorizationEntries = customerEpisodeAuthorizationEntries.ToArray();
                    }

                    if (customerEpisodeAuthorization.CustomerEpisodeAuthorizationOps != null)
                    {
                        List<CustomerEpisodeAuthorizationOpsEntity> customerEpisodeAuthorizationOps = new List<CustomerEpisodeAuthorizationOpsEntity>();
                        foreach (CustomerEpisodeAuthorizationOpsEntity customerEpisodeAuthorizationOp in customerEpisodeAuthorization.CustomerEpisodeAuthorizationOps)
                        {
                            if ((customerEpisodeAuthorizationOp.EditStatus.Value != StatusEntityValue.Deleted) && (customerEpisodeAuthorizationOp.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                            {
                                customerEpisodeAuthorizationOp.EditStatus.Reset();
                                customerEpisodeAuthorizationOps.Add(customerEpisodeAuthorizationOp);
                            }
                        }
                        customerEpisodeAuthorization.CustomerEpisodeAuthorizationOps = customerEpisodeAuthorizationOps.ToArray();
                    }

                }
                customerEpisode.CustomerAuthorizations = customerEpisodeAuthorizations.ToArray();
            }

            if (customerEpisode.CustomerEpisodeServices != null && customerEpisode.CustomerEpisodeServices.Length > 0)
            {
                customerEpisode.CustomerEpisodeServices[0].EditStatus.Reset();
            }

            if ((customerEpisode.InteropInformations != null) && (customerEpisode.InteropInformations.Length > 0))
            {
                List<CustomerEpInteropInfoEntity> interopInformations = new List<CustomerEpInteropInfoEntity>();
                foreach (CustomerEpInteropInfoEntity interop in customerEpisode.InteropInformations)
                {
                    if ((interop.EditStatus.Value != StatusEntityValue.Deleted) && (interop.EditStatus.Value != StatusEntityValue.NewAndDeleted))
                    {
                        interop.EditStatus.Reset();
                        interopInformations.Add(interop);
                    }
                }
                customerEpisode.InteropInformations = interopInformations.ToArray();
            }
        }

        public void Validate(CustomerEpisodeEntity episode, CustomerAdmissionEntity admission, CustomerAssistancePlanEntity customerAssistancePlan, ElementBL elementBL)
        {
            if (episode == null) throw new ArgumentNullException("Episode");

            CommonEntities.ElementEntity _customerEpisodeMetadata = ElementBL.GetElementByName(EntityNames.CustomerEpisodeEntityName, true);
            CommonEntities.ElementEntity _customerAdmissionMetadata = ElementBL.GetElementByName(EntityNames.CustomerAdmissionEntityName, false);
            CommonEntities.ElementEntity _customerAssistancePlanMetadata = ElementBL.GetElementByName(EntityNames.CustomerAssistancePlanEntityName, false);
            CommonEntities.ElementEntity _customerPolicyMetadata = ElementBL.GetElementByName(EntityNames.CustomerPolicyEntityName, false);
            CommonEntities.ElementEntity _customerEpisodeAuthorizationMetadata = ElementBL.GetElementByName(EntityNames.CustomerEpisodeAuthorizationEntityName, false);
            CommonEntities.ElementEntity _customerEpisodeAuthorizationEntryMetadata = ElementBL.GetElementByName(EntityNames.CustomerEpisodeAuthorizationEntryEntityName, false);
            CommonEntities.ElementEntity _customerEpisodeAuthorizationOpsMetadata = ElementBL.GetElementByName(EntityNames.CustomerEpisodeAuthorizationOpsEntityName, false);
            CommonEntities.ElementEntity _customerEpisodeReasonRelMetadata = ElementBL.GetElementByName(EntityNames.CustomerEpisodeReasonRelEntityName, false);

            CustomerEpisodeHelper _customerEpisodeHelper = new CustomerEpisodeHelper(_customerEpisodeMetadata);

            ValidationResults episodeResult = _customerEpisodeHelper.Validate(episode);
            if (!episodeResult.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ValidationResult vr in episodeResult)
                {
                    sb.AppendLine();
                    sb.AppendFormat("■ {0}", vr.Message);
                }

                throw new Exception(
                    string.Format(Properties.Resources.ERROR_EpisodeValidationError, sb));
            }

            if (admission != null)
            {
                CustomerAdmissionHelper _customerAdmissionHelper = new CustomerAdmissionHelper(_customerAdmissionMetadata);

                ValidationResults admissionResult = _customerAdmissionHelper.Validate(admission);
                if (!admissionResult.IsValid)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ValidationResult vr in admissionResult)
                    {
                        sb.AppendLine();
                        sb.AppendFormat("■ {0}", vr.Message);
                    }

                    throw new Exception(
                        string.Format(Properties.Resources.ERROR_AdmissionValidationError, sb));
                }
            }

            if (customerAssistancePlan != null)
            {
                CustomerAssistancePlanHelper _customerAssistancePlanHelper = new CustomerAssistancePlanHelper(_customerAssistancePlanMetadata);

                ValidationResults assistancePlanResult = _customerAssistancePlanHelper.Validate(customerAssistancePlan);
                if (!assistancePlanResult.IsValid)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ValidationResult vr in assistancePlanResult)
                    {
                        sb.AppendLine();
                        sb.AppendFormat("■ {0}", vr.Message);
                    }

                    throw new Exception(
                        string.Format(Properties.Resources.ERROR_CustomerAssistancePlanValidationError, sb));
                }
            }

            if ((episode != null) && (episode.CustomerPolicy != null))
            {
                CustomerPolicyHelper _customerPolicyHelper = new CustomerPolicyHelper(_customerPolicyMetadata);

                ValidationResults customerPolicyResult = _customerPolicyHelper.Validate(episode.CustomerPolicy);
                if (!customerPolicyResult.IsValid)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ValidationResult vr in customerPolicyResult)
                    {
                        sb.AppendLine();
                        sb.AppendFormat("■ {0}", vr.Message);
                    }

                    throw new Exception(
                        string.Format(Properties.Resources.ERROR_CustomerPolicyValidationError, sb));
                }

                if ((episode.CustomerPolicy.EditStatus.Value == StatusEntityValue.New) || (episode.CustomerPolicy.EditStatus.Value == StatusEntityValue.Updated))
                {
                    if (!String.IsNullOrEmpty(episode.CustomerPolicy.PolicyType.ValidationClass))
                    {
                        Type validatorType = Type.GetType(episode.CustomerPolicy.PolicyType.ValidationClass);
                        ICustomValidator<string, string> validator = Activator.CreateInstance(validatorType) as ICustomValidator<string, string>;
                        if (!validator.Validate(episode.CustomerPolicy.PolicyNumber))
                        {
                            throw new Exception(String.Format(Properties.Resources.MSG_CannotValidatePolicy, episode.CustomerPolicy.PolicyNumber));
                        }
                    }
                }
            }

            if ((episode != null) && (episode.CustomerAuthorizations != null))
            {
                CustomerEpisodeAuthorizationHelper _customerEpisodeAuthorizationHelper = new CustomerEpisodeAuthorizationHelper(_customerEpisodeAuthorizationMetadata);

                CustomerEpisodeAuthorizationEntryHelper _customerEpisodeAuthorizationEntryHelper = new CustomerEpisodeAuthorizationEntryHelper(_customerEpisodeAuthorizationEntryMetadata);

                CustomerEpisodeAuthorizationOpsHelper _customerEpisodeAuthorizationOpsHelper = new CustomerEpisodeAuthorizationOpsHelper(_customerEpisodeAuthorizationOpsMetadata);

                foreach (CustomerEpisodeAuthorizationEntity authorization in episode.CustomerAuthorizations)
                {
                    ValidationResults customerEpisodeAuthorizationResult = _customerEpisodeAuthorizationHelper.Validate(authorization);
                    if (!customerEpisodeAuthorizationResult.IsValid)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (ValidationResult vr in customerEpisodeAuthorizationResult)
                        {
                            sb.AppendLine();
                            sb.AppendFormat("■ {0}", vr.Message);
                        }

                        throw new Exception(
                            string.Format(Properties.Resources.ERROR_CustomerEpisodeAuthorizationValidationError, sb));
                    }

                    if (authorization.CustomerEpisodeAuthorizationEntries != null)
                    {
                        foreach (CustomerEpisodeAuthorizationEntryEntity authorizationEntry in authorization.CustomerEpisodeAuthorizationEntries)
                        {
                            ValidationResults customerEpisodeAuthorizationEntryResult = _customerEpisodeAuthorizationEntryHelper.Validate(authorizationEntry);
                            if (!customerEpisodeAuthorizationEntryResult.IsValid)
                            {
                                StringBuilder sb = new StringBuilder();
                                foreach (ValidationResult vr in customerEpisodeAuthorizationEntryResult)
                                {
                                    sb.AppendLine();
                                    sb.AppendFormat("■ {0}", vr.Message);
                                }

                                throw new Exception(
                                    string.Format(Properties.Resources.ERROR_CustomerEpisodeAuthorizationEntryValidationError, sb));
                            }
                        }
                    }

                    if (authorization.CustomerEpisodeAuthorizationOps != null)
                    {
                        foreach (CustomerEpisodeAuthorizationOpsEntity authorizationOp in authorization.CustomerEpisodeAuthorizationOps)
                        {
                            ValidationResults customerEpisodeAuthorizationOpResult = _customerEpisodeAuthorizationOpsHelper.Validate(authorizationOp);
                            if (!customerEpisodeAuthorizationOpResult.IsValid)
                            {
                                StringBuilder sb = new StringBuilder();
                                foreach (ValidationResult vr in customerEpisodeAuthorizationOpResult)
                                {
                                    sb.AppendLine();
                                    sb.AppendFormat("■ {0}", vr.Message);
                                }

                                throw new Exception(
                                    string.Format(Properties.Resources.ERROR_CustomerEpisodeAuthorizationOpValidationError, sb));
                            }
                        }
                    }
                }
            }

            if ((episode != null) && (episode.EpisodeReasons != null))
            {
                CustomerEpisodeReasonRelHelper _customerEpisodeReasonRelHelper = new CustomerEpisodeReasonRelHelper(_customerEpisodeReasonRelMetadata);
                foreach (CustomerEpisodeReasonRelEntity episodeReason in episode.EpisodeReasons)
                {
                    ValidationResults customerEpisodeReasonRelResult = _customerEpisodeReasonRelHelper.Validate(episodeReason);
                    if (!customerEpisodeReasonRelResult.IsValid)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (ValidationResult vr in customerEpisodeReasonRelResult)
                        {
                            sb.AppendLine();
                            sb.AppendFormat("■ {0}", vr.Message);
                        }

                        throw new Exception(
                            string.Format(Properties.Resources.ERROR_CustomerEpisodeReasonRelValidationError, sb));
                    }
                }
            }
        }

        public void InnerInsertCustomerEpisodeAuthorization(CustomerEpisodeAuthorizationEntity episodeAuthorization,
            int episodeID, string userName)
        {
            if (episodeAuthorization == null) return;
            switch (episodeAuthorization.EditStatus.Value)
            {
                case StatusEntityValue.Deleted:
                    _customerEpisodeAuthorizationDA.Delete(episodeAuthorization.ID);
                    break;
                case StatusEntityValue.New:
                    episodeAuthorization.CustomerEpisodeID = episodeID;
                    episodeAuthorization.ID = _customerEpisodeAuthorizationDA.Insert(episodeID, (episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0,
                        (episodeAuthorization.AuthorizationType != null) ? episodeAuthorization.AuthorizationType.ID : 0,
                        episodeAuthorization.ValidFromDate, episodeAuthorization.ValidToDate, episodeAuthorization.AuthorizationDocumentNumber,
                        episodeAuthorization.Comments, episodeAuthorization.AuthorizedBy, episodeAuthorization.IsChipCard, (int)episodeAuthorization.Status, userName);
                    if (episodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                    {
                        foreach (CustomerEpisodeAuthorizationEntryEntity customerEpisodeAuthorizationEntry in episodeAuthorization.CustomerEpisodeAuthorizationEntries)
                        {
                            switch (customerEpisodeAuthorizationEntry.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Insert(customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);

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

                    if (episodeAuthorization.CustomerEpisodeAuthorizationOps != null)
                    {
                        foreach (CustomerEpisodeAuthorizationOpsEntity customerEpisodeAuthorizationOp in episodeAuthorization.CustomerEpisodeAuthorizationOps)
                        {
                            switch (customerEpisodeAuthorizationOp.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Insert(customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
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

                    break;
                case StatusEntityValue.NewAndDeleted:
                    break;
                case StatusEntityValue.None:
                    break;
                case StatusEntityValue.Updated:
                    episodeAuthorization.CustomerEpisodeID = episodeID;
                    _customerEpisodeAuthorizationDA.Update(episodeAuthorization.ID, episodeID, (episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0,
                        (episodeAuthorization.AuthorizationType != null) ? episodeAuthorization.AuthorizationType.ID : 0,
                         episodeAuthorization.ValidFromDate, episodeAuthorization.ValidToDate, episodeAuthorization.AuthorizationDocumentNumber,
                        episodeAuthorization.Comments, episodeAuthorization.AuthorizedBy, episodeAuthorization.IsChipCard, (int)episodeAuthorization.Status, userName);
                    if (episodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                    {
                        foreach (CustomerEpisodeAuthorizationEntryEntity customerEpisodeAuthorizationEntry in episodeAuthorization.CustomerEpisodeAuthorizationEntries)
                        {
                            switch (customerEpisodeAuthorizationEntry.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationEntryDA.Delete(customerEpisodeAuthorizationEntry.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Insert(customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Update(customerEpisodeAuthorizationEntry.ID, customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (episodeAuthorization.CustomerEpisodeAuthorizationOps != null)
                    {
                        foreach (CustomerEpisodeAuthorizationOpsEntity customerEpisodeAuthorizationOp in episodeAuthorization.CustomerEpisodeAuthorizationOps)
                        {
                            switch (customerEpisodeAuthorizationOp.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationOpsDA.Delete(customerEpisodeAuthorizationOp.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Insert(customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Update(customerEpisodeAuthorizationOp.ID, customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    break;
                default:
                    break;
            }

        }

        public void InnerUpdateCustomerEpisodeAuthorization(CustomerEpisodeAuthorizationEntity episodeAuthorization,
            int episodeID, string userName)
        {
            switch (episodeAuthorization.EditStatus.Value)
            {
                case StatusEntityValue.Deleted:
                    _customerEpisodeAuthorizationDA.Delete(episodeAuthorization.ID);
                    if (episodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                    {
                        foreach (CustomerEpisodeAuthorizationEntryEntity customerEpisodeAuthorizationEntry in episodeAuthorization.CustomerEpisodeAuthorizationEntries)
                        {
                            switch (customerEpisodeAuthorizationEntry.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationEntryDA.Delete(customerEpisodeAuthorizationEntry.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Insert(customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Update(customerEpisodeAuthorizationEntry.ID, customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (episodeAuthorization.CustomerEpisodeAuthorizationOps != null)
                    {
                        foreach (CustomerEpisodeAuthorizationOpsEntity customerEpisodeAuthorizationOp in episodeAuthorization.CustomerEpisodeAuthorizationOps)
                        {
                            switch (customerEpisodeAuthorizationOp.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationOpsDA.Delete(customerEpisodeAuthorizationOp.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Insert(customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Update(customerEpisodeAuthorizationOp.ID, customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case StatusEntityValue.New:
                    episodeAuthorization.CustomerEpisodeID = episodeID;
                    episodeAuthorization.ID = _customerEpisodeAuthorizationDA.Insert(episodeID, (episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0,
                        (episodeAuthorization.AuthorizationType != null) ? episodeAuthorization.AuthorizationType.ID : 0,
                        episodeAuthorization.ValidFromDate, episodeAuthorization.ValidToDate, episodeAuthorization.AuthorizationDocumentNumber,
                        episodeAuthorization.Comments, episodeAuthorization.AuthorizedBy, episodeAuthorization.IsChipCard, (int)episodeAuthorization.Status, userName);
                    if (episodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                    {
                        foreach (CustomerEpisodeAuthorizationEntryEntity customerEpisodeAuthorizationEntry in episodeAuthorization.CustomerEpisodeAuthorizationEntries)
                        {
                            switch (customerEpisodeAuthorizationEntry.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationEntryDA.Delete(customerEpisodeAuthorizationEntry.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Insert(customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);

                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Update(customerEpisodeAuthorizationEntry.ID, customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (episodeAuthorization.CustomerEpisodeAuthorizationOps != null)
                    {
                        foreach (CustomerEpisodeAuthorizationOpsEntity customerEpisodeAuthorizationOp in episodeAuthorization.CustomerEpisodeAuthorizationOps)
                        {
                            switch (customerEpisodeAuthorizationOp.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationOpsDA.Delete(customerEpisodeAuthorizationOp.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Insert(customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Update(customerEpisodeAuthorizationOp.ID, customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    break;
                case StatusEntityValue.NewAndDeleted:
                    break;
                case StatusEntityValue.None:
                    break;
                case StatusEntityValue.Updated:
                    episodeAuthorization.CustomerEpisodeID = episodeID;
                    _customerEpisodeAuthorizationDA.Update(episodeAuthorization.ID, episodeID, (episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0,
                        (episodeAuthorization.AuthorizationType != null) ? episodeAuthorization.AuthorizationType.ID : 0,
                        episodeAuthorization.ValidFromDate, episodeAuthorization.ValidToDate, episodeAuthorization.AuthorizationDocumentNumber,
                        episodeAuthorization.Comments, episodeAuthorization.AuthorizedBy, episodeAuthorization.IsChipCard, (int)episodeAuthorization.Status, userName);
                    if (episodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                    {
                        foreach (CustomerEpisodeAuthorizationEntryEntity customerEpisodeAuthorizationEntry in episodeAuthorization.CustomerEpisodeAuthorizationEntries)
                        {
                            switch (customerEpisodeAuthorizationEntry.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationEntryDA.Delete(customerEpisodeAuthorizationEntry.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Insert(customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationEntry.ID = _customerEpisodeAuthorizationEntryDA.Update(customerEpisodeAuthorizationEntry.ID, customerEpisodeAuthorizationEntry.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationEntry.AuthorizedActID, customerEpisodeAuthorizationEntry.AuthorizedElementName,
                                        customerEpisodeAuthorizationEntry.AuthorizedElementID,
                                        customerEpisodeAuthorizationEntry.IsChipCard, customerEpisodeAuthorizationEntry.AuthorizationDocumentNumber, customerEpisodeAuthorizationEntry.AuthorizationTypeID,
                                        (int)customerEpisodeAuthorizationEntry.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (episodeAuthorization.CustomerEpisodeAuthorizationOps != null)
                    {
                        foreach (CustomerEpisodeAuthorizationOpsEntity customerEpisodeAuthorizationOp in episodeAuthorization.CustomerEpisodeAuthorizationOps)
                        {
                            switch (customerEpisodeAuthorizationOp.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerEpisodeAuthorizationOpsDA.Delete(customerEpisodeAuthorizationOp.ID);
                                    break;
                                case StatusEntityValue.New:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Insert(customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID = episodeAuthorization.ID;
                                    customerEpisodeAuthorizationOp.ID = _customerEpisodeAuthorizationOpsDA.Update(customerEpisodeAuthorizationOp.ID, customerEpisodeAuthorizationOp.CustomerEpisodeAuthorizationID,
                                        customerEpisodeAuthorizationOp.NativeResultCode, customerEpisodeAuthorizationOp.NativeRequest, customerEpisodeAuthorizationOp.NativeResponse,
                                        customerEpisodeAuthorizationOp.NativeResultMessage, customerEpisodeAuthorizationOp.NativeAuthorizationIdentifier,
                                        (int)customerEpisodeAuthorizationOp.Status, userName);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

        }


        public CustomerEpisodeEntity InnerInsert(CustomerEpisodeEntity episode, ProcessChartEntity processChart, CustomerProcessEntity process, CustomerAdmissionEntity admission, CustomerAssistancePlanEntity customerAssistancePlan,
            int[] customerRoutineIDs, int[] routineActIDs, int[] customerProcedureIDs, int[] procedureActIDs, int[] customerOrderRequestIDs,
            string userName, string customerEpisodeNumber, string admissionEventNumber, string customerAccountNumber,
            CustomerProcessWaitingListEntity cpwl, CustomerBL customerBL, CustomerAssistancePlanBL customerAssistancePlanBL, CustomerProcessWaitingListBL cpwlBL, MedicalEpisodeBL _medicalEpisodeBL)
        {
            if (processChart == null) throw new NullReferenceException("Process Chart");

            CodeGenerator codeGenerator = new CodeGenerator();
            episode.EpisodeNumber = codeGenerator.Generate(string.Empty, customerEpisodeNumber);

            #region Actualizar o crear la admission correspondiente
            switch (admission.EditStatus.Value)
            {
                case StatusEntityValue.Deleted:
                    break;
                case StatusEntityValue.New:
                    admission = base.InnerInsert(admission, process, processChart, userName, admissionEventNumber);
                    if (admission != null)
                    {
                        episode.CustomerAdmissionID = admission.ID;
                        //process.CurrentAdmissionID = admission.ID;
                    }
                    break;
                case StatusEntityValue.NewAndDeleted:
                    break;
                case StatusEntityValue.None:
                    break;
                case StatusEntityValue.Updated:
                    base.InnerUpdate(admission, process, processChart, userName);
                    if (admission != null)
                    {
                        episode.CustomerAdmissionID = admission.ID;
                        //process.CurrentAdmissionID = admission.ID;
                    }
                    break;
                default:
                    break;
            }

            episode.CustomerAdmissionID = admission.ID;
            #endregion

            #region Actualizar Location Availability
            if ((episode.CurrentLocationAvail != null) && (episode.CurrentLocationAvail.LocationID > 0))
            {
                switch (episode.CurrentLocationAvail.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        break;
                    case StatusEntityValue.New:
                        episode.CurrentLocationAvail.ID = _locationAvailabilityDA.Insert(episode.CurrentLocationAvail.LocationID, episode.CurrentLocationAvail.AvailPatternID,
                            episode.CurrentLocationAvail.StartDateTime, episode.CurrentLocationAvail.EndDateTime, episode.CurrentLocationAvail.Status, userName);
                        episode.CurrentLocationAvail.DBTimeStamp = _locationAvailabilityDA.GetDBTimeStamp(episode.CurrentLocationAvail.ID);
                        break;
                    case StatusEntityValue.NewAndDeleted:
                        break;
                    case StatusEntityValue.None:
                        break;
                    case StatusEntityValue.Updated:
                        _locationAvailabilityDA.Update(episode.CurrentLocationAvail.ID, episode.CurrentLocationAvail.LocationID, episode.CurrentLocationAvail.AvailPatternID,
                            episode.CurrentLocationAvail.StartDateTime, episode.CurrentLocationAvail.EndDateTime, AvailStatusEnum.Busy, userName);
                        episode.CurrentLocationAvail.DBTimeStamp = _locationAvailabilityDA.GetDBTimeStamp(episode.CurrentLocationAvail.ID);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Actualizar Equipment Availability
            //TODO: pendiente de que esta parte quede finalizada.
            #endregion

            #region Crear o actualizar Customer Policy
            if (episode.CustomerPolicy != null)
            {
                switch (episode.CustomerPolicy.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        break;
                    case StatusEntityValue.New:
                        episode.CustomerPolicy.ID = _customerPolicyDA.Insert(episode.CustomerPolicy.CustomerID, episode.CustomerPolicy.InsurerID,
                            (episode.CustomerPolicy.PolicyType != null) ? episode.CustomerPolicy.PolicyType.ID : 0,
                            episode.CustomerPolicy.PolicyNumber, episode.CustomerPolicy.ActiveAt, episode.CustomerPolicy.ActiveTo,
                            episode.CustomerPolicy.CoverageQty, (int)episode.CustomerPolicy.Status, userName);
                        episode.CustomerPolicy.DBTimeStamp = _customerPolicyDA.GetDBTimeStamp(episode.CustomerPolicy.ID);
                        break;
                    case StatusEntityValue.NewAndDeleted:
                        break;
                    case StatusEntityValue.None:
                        break;
                    case StatusEntityValue.Updated:
                        _customerPolicyDA.Update(episode.CustomerPolicy.ID, episode.CustomerPolicy.CustomerID, episode.CustomerPolicy.InsurerID,
                            (episode.CustomerPolicy.PolicyType != null) ? episode.CustomerPolicy.PolicyType.ID : 0,
                            episode.CustomerPolicy.PolicyNumber, episode.CustomerPolicy.ActiveAt, episode.CustomerPolicy.ActiveTo,
                            episode.CustomerPolicy.CoverageQty, (int)episode.CustomerPolicy.Status, userName);
                        episode.CustomerPolicy.DBTimeStamp = _customerPolicyDA.GetDBTimeStamp(episode.CustomerPolicy.ID);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Asociar la tarjeta si procede
            if ((episode.CustomerPolicy != null) && (episode.CustomerPolicy.CustomerCard != null))
            {
                int customerCardID = _customerCardDA.GetCustomerCardIDByCustomerAndInsurer(episode.CustomerPolicy.CustomerID, episode.CustomerPolicy.InsurerID);
                if (customerCardID > 0)
                    _customerCardDA.UpdateCustomerPolicyID(customerCardID, episode.CustomerPolicy.ID, userName);
            }
            #endregion

            #region Registrar nuevo episodio
            episode.ID = _customerEpisodeDA.Insert(episode.CustomerID, episode.ProcessChartID, episode.CustomerProfileID, episode.CustomerClassificationID,
                episode.TariffID, episode.EpisodeTypeID, (episode.AdmissionInsurer != null) ? episode.AdmissionInsurer.ID : 0,
                (episode.Physician != null) ? episode.Physician.ID : 0, (episode.CustomerPolicy != null) ? episode.CustomerPolicy.ID : 0,
                admission.ID, episode.EpisodeNumber, episode.Comments, (episode.CurrentLocationAvail != null) ? episode.CurrentLocationAvail.ID : 0,
                (episode.CurrentEquipmentAvail != null) ? episode.CurrentEquipmentAvail.ID : 0, episode.StartDateTime, episode.EndDateTime,
                episode.Origin, (episode.Predecessor != null) ? episode.Predecessor.ID : 0, episode.ADTOrder != null ? episode.ADTOrder.ID : 0, episode.Status, userName);

            episode.DBTimeStamp = _customerEpisodeDA.GetDBTimeStamp(episode.ID);
            #endregion

            #region Actualizar Cliente
            this.InnerUpdateCustomerData(episode.CustomerID, episode.CustomerProfileID, episode.CustomerClassificationID, episode.CustomerAdmissionID, episode.ID, customerBL);
            #endregion

            #region Crear numero de cuenta de cliente si no existe
            if (_customerAccountDA.GetCustomerAccountIDByCustomerID(episode.CustomerID) == 0)
            {
                _customerAccountDA.Insert(episode.CustomerID, codeGenerator.Generate(string.Empty, customerAccountNumber), 0.0m, DateTime.Now, (int)CommonEntities.StatusEnum.Active, userName);
            }
            #endregion

            #region Assistance Agreements
            if ((episode.AssistanceAgreements != null) && (episode.AssistanceAgreements.Length > 0))
            {
                foreach (CustomerAssistAgreeRelEntity assistanceAgreement in episode.AssistanceAgreements)
                {
                    switch (assistanceAgreement.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _customerAssistAgreeRelDA.DeleteCustomerAssistAgreeRel(assistanceAgreement.ID);
                            break;
                        case StatusEntityValue.New:
                            assistanceAgreement.ID = _customerAssistAgreeRelDA.Insert(assistanceAgreement.CustomerBudgetID, assistanceAgreement.CustomerContractID,
                                episode.ID, assistanceAgreement.HistoryCareCenterID, assistanceAgreement.HistoryAssistanceAgreementID, assistanceAgreement.AmountQty,
                                (int)assistanceAgreement.Status, assistanceAgreement.ContractCoverAgreementID, userName);
                            assistanceAgreement.DbTimeStamp = _customerAssistAgreeRelDA.GetDBTimeStamp(assistanceAgreement.ID);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _customerAssistAgreeRelDA.Update(assistanceAgreement.ID, assistanceAgreement.CustomerBudgetID, assistanceAgreement.CustomerContractID,
                                episode.ID, assistanceAgreement.HistoryCareCenterID, assistanceAgreement.HistoryAssistanceAgreementID, assistanceAgreement.AmountQty,
                                (int)assistanceAgreement.Status, assistanceAgreement.ContractCoverAgreementID, userName);
                            assistanceAgreement.DbTimeStamp = _customerAssistAgreeRelDA.GetDBTimeStamp(assistanceAgreement.ID);
                            break;
                        default:
                            break;
                    }

                    #region Agreements
                    if ((assistanceAgreement.Agreements != null) && (assistanceAgreement.Agreements.Length > 0))
                    {
                        foreach (CustomerAgreeRelEntity agreement in assistanceAgreement.Agreements)
                        {
                            switch (agreement.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerAgreeRelDA.DeleteCustomerAgreeRel(agreement.ID);
                                    break;
                                case StatusEntityValue.New:
                                    agreement.ID = _customerAgreeRelDA.Insert(agreement.CustomerAssistanceAgreeRelID, agreement.HistoryAgreementID, agreement.AmountQty,
                                        agreement.Units, agreement.TotalQty, (int)agreement.Status, userName);
                                    agreement.DbTimeStamp = _customerAgreeRelDA.GetDBTimeStamp(agreement.ID);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    _customerAgreeRelDA.Update(agreement.ID, agreement.CustomerAssistanceAgreeRelID, agreement.HistoryAgreementID, agreement.AmountQty,
                                        agreement.Units, agreement.TotalQty, (int)agreement.Status, userName);
                                    agreement.DbTimeStamp = _customerAgreeRelDA.GetDBTimeStamp(agreement.ID);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Insurer Cover Agreements
            if ((episode.CoverAgreements != null) && (episode.CoverAgreements.Length > 0))
            {
                foreach (CustomerCoverAgreeRelEntity coverAgreement in episode.CoverAgreements)
                {
                    switch (coverAgreement.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _customerCoverAgreeRelDA.DeleteCustomerCoverAgreeRel(coverAgreement.ID);
                            break;
                        case StatusEntityValue.New:
                            coverAgreement.ID = _customerCoverAgreeRelDA.Insert(coverAgreement.CustomerBudgetID, coverAgreement.CustomerContractID, episode.ID,
                                coverAgreement.AmountQty, coverAgreement.HistoryInsurerCoverAgreementID, (int)coverAgreement.Status, userName);
                            coverAgreement.DbTimeStamp = _customerCoverAgreeRelDA.GetDBTimeStamp(coverAgreement.ID);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _customerCoverAgreeRelDA.Update(coverAgreement.ID, coverAgreement.CustomerBudgetID, coverAgreement.CustomerContractID, episode.ID,
                                coverAgreement.AmountQty, coverAgreement.HistoryInsurerCoverAgreementID, (int)coverAgreement.Status, userName);
                            coverAgreement.DbTimeStamp = _customerCoverAgreeRelDA.GetDBTimeStamp(coverAgreement.ID);
                            break;
                        default:
                            break;
                    }

                    #region Insurer Agreements
                    if ((coverAgreement.InsurerAgreements != null) && (coverAgreement.InsurerAgreements.Length > 0))
                    {
                        foreach (CustomerInsurerAgreeRelEntity insurerAgreement in coverAgreement.InsurerAgreements)
                        {
                            switch (insurerAgreement.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerInsurerAgreeRelDA.DeleteCustomerInsurerAgreeRel(insurerAgreement.ID);
                                    break;
                                case StatusEntityValue.New:
                                    insurerAgreement.ID = _customerInsurerAgreeRelDA.Insert(insurerAgreement.CustomerCoverAgreeRelID, insurerAgreement.HistoryInsurerAgreementID,
                                        insurerAgreement.AmountQty, insurerAgreement.Units, insurerAgreement.TotalQty, (int)insurerAgreement.Status, userName);
                                    insurerAgreement.DbTimeStamp = _customerInsurerAgreeRelDA.GetDBTimeStamp(insurerAgreement.ID);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    _customerInsurerAgreeRelDA.Update(insurerAgreement.ID, insurerAgreement.CustomerCoverAgreeRelID, insurerAgreement.HistoryInsurerAgreementID,
                                        insurerAgreement.AmountQty, insurerAgreement.Units, insurerAgreement.TotalQty, (int)insurerAgreement.Status, userName);
                                    insurerAgreement.DbTimeStamp = _customerInsurerAgreeRelDA.GetDBTimeStamp(insurerAgreement.ID);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                }
                //process.FirstInsurerID = (episode.AdmissionInsurer != null) ? episode.AdmissionInsurer.ID : 0;
            }
            #endregion

            #region Customer Assistance Plan
            if (customerAssistancePlan != null)
            {
                customerAssistancePlan.AdmissionID = episode.CustomerAdmissionID;
                customerAssistancePlan.EpisodeID = episode.ID;

                if (customerAssistancePlan.Procedures != null)
                {
                    foreach (CustomerProcedureEntity procedure in customerAssistancePlan.Procedures)
                    {
                        procedure.EpisodeID = episode.ID;
                    }
                }

                if (customerAssistancePlan.Routines != null)
                {
                    foreach (CustomerRoutineEntity routine in customerAssistancePlan.Routines)
                    {
                        routine.EpisodeID = episode.ID;
                    }
                }

                //customerAssistancePlan = customerAssistancePlanBL.InnerInsert(customerAssistancePlan, userName);
                customerAssistancePlan = customerAssistancePlanBL.Save(customerAssistancePlan, true, null);
            }
            #endregion

            #region Razones
            if (episode.EpisodeReasons != null)
            {
                foreach (CustomerEpisodeReasonRelEntity episodeReason in episode.EpisodeReasons)
                {
                    switch (episodeReason.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _customerEpisodeReasonRelDA.Delete(episodeReason.ID);
                            break;
                        case StatusEntityValue.New:
                            episodeReason.ID = _customerEpisodeReasonRelDA.Insert(episode.ID, episodeReason.EpisodeReasonID, episodeReason.ConceptID, (episodeReason.EpisodeReasonType != null) ? episodeReason.EpisodeReasonType.ID : 0,
                                userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            episodeReason.CustomerEpisodeID = episode.ID;
                            _customerEpisodeReasonRelDA.Update(episodeReason.ID, episodeReason.CustomerEpisodeID, episodeReason.EpisodeReasonID, episodeReason.ConceptID, (episodeReason.EpisodeReasonType != null) ? episodeReason.EpisodeReasonType.ID : 0,
                                userName);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Autorizaciones
            if (episode.CustomerAuthorizations != null)
            {
                foreach (CustomerEpisodeAuthorizationEntity episodeAuthorization in episode.CustomerAuthorizations)
                {
                    InnerInsertCustomerEpisodeAuthorization(episodeAuthorization, episode.ID, userName);
                }
            }
            #endregion

            #region Custome Episode Service Rel
            if (episode.CustomerEpisodeServices != null && episode.CustomerEpisodeServices.Length > 0)
            {
                foreach (CustomerEpisodeServiceRelEntity cesr in episode.CustomerEpisodeServices)
                {
                    switch (cesr.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            break;
                        case StatusEntityValue.New:
                            cesr.ID = _customerEpisodeServiceRelDA.Insert(episode.ID,
                                cesr.AssistanceServiceID, (long)cesr.Step,
                                cesr.UnitServiceID,
                                cesr.StartAt, cesr.EndingTo,
                                cesr.PhysicianID, userName);
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
            #endregion

            #region refequestingPhysician
            if (episode.ReferencedPhysicians != null)
            {
                foreach (CustomerEpisodeReferencedPhysicianRelEntity cerp in episode.ReferencedPhysicians)
                {
                    switch (cerp.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            break;
                        case StatusEntityValue.New:
                            _customerEpisodeReferencedPhysicianRelDA.Insert(episode.ID, cerp.ReferencedPhysician.MasterID, userName);
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
            #endregion

            #region update status cpwl
            if (cpwl != null)
            {
                cpwl.Status = CommonEntities.StatusEnum.Closed;
                if (cpwl.WLAppointmentsInformation != null)
                {
                    cpwl.WLAppointmentsInformation.Status = AvailStatusEnum.Blocked;
                    cpwl.WLAppointmentsInformation.EditStatus.Update();
                    cpwl.WLAppointmentsInformation.ResourceAvail.Status = AvailStatusEnum.Busy;
                    cpwl.WLAppointmentsInformation.ResourceAvail.EditStatus.Update();
                }
                cpwl.EditStatus.Update();
                cpwlBL.InnerUpdate(cpwl, true, userName);
            }
            #endregion


            #region Custome Episode Interop information
            if ((episode.InteropInformations != null) && (episode.InteropInformations.Length > 0))
            {
                foreach (CustomerEpInteropInfoEntity interop in episode.InteropInformations)
                {
                    switch (interop.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted: _customerEpInteropInfoDA.DeleteByID(interop.ID); break;
                        case StatusEntityValue.New:
                            interop.ID = _customerEpInteropInfoDA.Insert(episode.CustomerID, episode.ID, interop.IdentifierTypeProvider, interop.IdentifierTypeName,
                                interop.VisitNumber, interop.VisitPriorityCode, interop.VisitDescription, interop.PatientStatusCode, interop.AdmitReason,
                                interop.TransferReason, (int)interop.PatientLocationType, interop.PatientLocation, interop.ExpectedAdmitDateTime, interop.ExpectedDischargeDateTime, userName);
                            break;
                        case StatusEntityValue.Updated:
                            _customerEpInteropInfoDA.Update(interop.ID, interop.CustomerID, interop.CustomerEpisodeID, interop.IdentifierTypeProvider, interop.IdentifierTypeName,
                                interop.VisitNumber, interop.VisitPriorityCode, interop.VisitDescription, interop.PatientStatusCode, interop.AdmitReason,
                                interop.TransferReason, (int)interop.PatientLocationType, interop.PatientLocation, interop.ExpectedAdmitDateTime, interop.ExpectedDischargeDateTime, userName);
                            break;
                        default: break;
                    }
                }
            }
            #endregion

            #region Actualizar Episode in CustomerProcess
            if (episode.ID > 0)
            {
                CustomerProcessDA _customerProcessDA = new CustomerProcessDA();
                _customerProcessDA.Update(process.ID, episode.ID, userName);

            }
            #endregion

            #region registrar episodio medico
            if (episode.Physician != null && _medicalEpisodeBL != null &&
               ((processChart.AdmissionConfig != null && processChart.AdmissionConfig.OnAdmitCreateMedicalEpisode && processChart.AdmissionConfig.MedEpisodeProcessChart != null)))
                _medicalEpisodeBL.InnerCreateFirstEpisodeByAdmission(processChart.AdmissionConfig.MedEpisodeProcessChart, process, episode.Physician.ID, episode.ID, userName);
            else
            {
                if (episode.Physician != null && _medicalEpisodeBL != null &&
                   (processChart.ReceptionConfig != null && processChart.ReceptionConfig.OnAdmitCreateMedicalEpisode && processChart.ReceptionConfig.MedEpisodeProcessChart != null))
                    _medicalEpisodeBL.InnerCreateFirstEpisodeByAdmission(processChart.ReceptionConfig.MedEpisodeProcessChart, process, episode.Physician.ID, episode.ID, userName);
            }
            #endregion

            #region buscar el customerReservation y asignar a los actos y customerroutine/procedure el episodio y el asistance plan
            if (customerRoutineIDs != null && customerRoutineIDs.Length > 0)
            {
                foreach (int item in customerRoutineIDs)
                {
                    _customerRoutineDA.Update(item, customerAssistancePlan.ID, episode.ID, userName);
                }
            }
            if (routineActIDs != null && routineActIDs.Length > 0)
            {
                foreach (int item in routineActIDs)
                {
                    _routineActDA.Update(item, customerAssistancePlan.ID, episode.ID, userName);
                }
            }
            if (customerProcedureIDs != null && customerProcedureIDs.Length > 0)
            {
                foreach (int item in customerProcedureIDs)
                {
                    _customerProcedureDA.Update(item, customerAssistancePlan.ID, episode.ID, userName);
                }
            }
            if (procedureActIDs != null && procedureActIDs.Length > 0)
            {
                foreach (int item in customerProcedureIDs)
                {
                    _procedureActDA.Update(item, customerAssistancePlan.ID, episode.ID, userName);
                }
            }
            //int[] , int[] ,
            if (customerOrderRequestIDs != null && customerOrderRequestIDs.Length > 0)
            {
                foreach (int item in customerOrderRequestIDs)
                {
                    _customerOrderRequestDA.UpdateCustomerEpisodeID(item, episode.ID, userName);
                    _customerOrderRealizationDA.UpdateCustomerEpisodeID(item, episode.ID, userName);
                }
            }
            #endregion
            return episode;
        }

        public CustomerEpisodeEntity InnerUpdate(CustomerEpisodeEntity episode, string userName)
        {
            #region Actualizar episodio
            Int64 dbTimeStamp = _customerEpisodeDA.GetDBTimeStamp(episode.ID);
            if (dbTimeStamp != episode.DBTimeStamp)
                throw new Exception(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, episode.ID));

            #region Crear o actualizar Customer Policy
            if (episode.CustomerPolicy != null)
            {
                switch (episode.CustomerPolicy.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        break;
                    case StatusEntityValue.New:
                        episode.CustomerPolicy.ID = _customerPolicyDA.Insert(episode.CustomerPolicy.CustomerID, episode.CustomerPolicy.InsurerID,
                            (episode.CustomerPolicy.PolicyType != null) ? episode.CustomerPolicy.PolicyType.ID : 0,
                            episode.CustomerPolicy.PolicyNumber, episode.CustomerPolicy.ActiveAt, episode.CustomerPolicy.ActiveTo,
                            episode.CustomerPolicy.CoverageQty, (int)episode.CustomerPolicy.Status, userName);
                        episode.CustomerPolicy.DBTimeStamp = _customerPolicyDA.GetDBTimeStamp(episode.CustomerPolicy.ID);
                        break;
                    case StatusEntityValue.NewAndDeleted:
                        break;
                    case StatusEntityValue.None:
                        break;
                    case StatusEntityValue.Updated:
                        _customerPolicyDA.Update(episode.CustomerPolicy.ID, episode.CustomerPolicy.CustomerID, episode.CustomerPolicy.InsurerID,
                            (episode.CustomerPolicy.PolicyType != null) ? episode.CustomerPolicy.PolicyType.ID : 0,
                            episode.CustomerPolicy.PolicyNumber, episode.CustomerPolicy.ActiveAt, episode.CustomerPolicy.ActiveTo,
                            episode.CustomerPolicy.CoverageQty, (int)episode.CustomerPolicy.Status, userName);
                        episode.CustomerPolicy.DBTimeStamp = _customerPolicyDA.GetDBTimeStamp(episode.CustomerPolicy.ID);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            _customerEpisodeDA.Update(episode.ID, episode.CustomerID, episode.ProcessChartID, episode.CustomerProfileID, episode.CustomerClassificationID,
                episode.TariffID, episode.EpisodeTypeID, (episode.AdmissionInsurer != null) ? episode.AdmissionInsurer.ID : 0,
                (episode.Physician != null) ? episode.Physician.ID : 0, (episode.CustomerPolicy != null) ? episode.CustomerPolicy.ID : 0,
                episode.CustomerAdmissionID, episode.EpisodeNumber, episode.Comments, (episode.CurrentLocationAvail != null) ? episode.CurrentLocationAvail.ID : 0,
                (episode.CurrentEquipmentAvail != null) ? episode.CurrentEquipmentAvail.ID : 0, episode.StartDateTime, episode.EndDateTime,
                episode.Origin, (episode.Predecessor != null) ? episode.Predecessor.ID : 0, episode.ADTOrder != null ? episode.ADTOrder.ID : 0, episode.Status, userName);

            episode.DBTimeStamp = _customerEpisodeDA.GetDBTimeStamp(episode.ID);
            #endregion

            #region Assistance Agreements
            if ((episode.AssistanceAgreements != null) && (episode.AssistanceAgreements.Length > 0))
            {
                foreach (CustomerAssistAgreeRelEntity assistanceAgreement in episode.AssistanceAgreements)
                {
                    switch (assistanceAgreement.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _customerAgreeRelDA.DeleteCustomerAgreeRel(assistanceAgreement.HistoryAssistanceAgreementID, episode.ID);
                            _customerAssistAgreeRelDA.DeleteCustomerAssistAgreeRel(assistanceAgreement.ID);
                            break;
                        case StatusEntityValue.New:
                            assistanceAgreement.ID = _customerAssistAgreeRelDA.Insert(assistanceAgreement.CustomerBudgetID, assistanceAgreement.CustomerContractID,
                                episode.ID, assistanceAgreement.HistoryCareCenterID, assistanceAgreement.HistoryAssistanceAgreementID, assistanceAgreement.AmountQty,
                                (int)assistanceAgreement.Status, assistanceAgreement.ContractCoverAgreementID, userName);
                            assistanceAgreement.DbTimeStamp = _customerAssistAgreeRelDA.GetDBTimeStamp(assistanceAgreement.ID);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _customerAssistAgreeRelDA.Update(assistanceAgreement.ID, assistanceAgreement.CustomerBudgetID, assistanceAgreement.CustomerContractID,
                                episode.ID, assistanceAgreement.HistoryCareCenterID, assistanceAgreement.HistoryAssistanceAgreementID, assistanceAgreement.AmountQty,
                                (int)assistanceAgreement.Status, assistanceAgreement.ContractCoverAgreementID, userName);
                            assistanceAgreement.DbTimeStamp = _customerAssistAgreeRelDA.GetDBTimeStamp(assistanceAgreement.ID);
                            break;
                        default:
                            break;
                    }

                    #region Agreements
                    if ((assistanceAgreement.Agreements != null) && (assistanceAgreement.Agreements.Length > 0))
                    {
                        foreach (CustomerAgreeRelEntity agreement in assistanceAgreement.Agreements)
                        {
                            switch (agreement.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerAgreeRelDA.DeleteCustomerAgreeRel(agreement.ID);
                                    break;
                                case StatusEntityValue.New:
                                    agreement.ID = _customerAgreeRelDA.Insert(assistanceAgreement.ID, agreement.HistoryAgreementID, agreement.AmountQty,
                                        agreement.Units, agreement.TotalQty, (int)agreement.Status, userName);
                                    agreement.DbTimeStamp = _customerAgreeRelDA.GetDBTimeStamp(agreement.ID);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    _customerAgreeRelDA.Update(agreement.ID, assistanceAgreement.ID, agreement.HistoryAgreementID, agreement.AmountQty,
                                        agreement.Units, agreement.TotalQty, (int)agreement.Status, userName);
                                    agreement.DbTimeStamp = _customerAgreeRelDA.GetDBTimeStamp(agreement.ID);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Insurer Cover Agreements
            if ((episode.CoverAgreements != null) && (episode.CoverAgreements.Length > 0))
            {
                foreach (CustomerCoverAgreeRelEntity coverAgreement in episode.CoverAgreements)
                {
                    switch (coverAgreement.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _customerCoverAgreeRelDA.DeleteCustomerCoverAgreeRel(coverAgreement.ID);
                            break;
                        case StatusEntityValue.New:
                            coverAgreement.ID = _customerCoverAgreeRelDA.Insert(coverAgreement.CustomerBudgetID, coverAgreement.CustomerContractID, episode.ID,
                                coverAgreement.AmountQty, coverAgreement.HistoryInsurerCoverAgreementID, (int)coverAgreement.Status, userName);
                            coverAgreement.DbTimeStamp = _customerCoverAgreeRelDA.GetDBTimeStamp(coverAgreement.ID);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _customerCoverAgreeRelDA.Update(coverAgreement.ID, coverAgreement.CustomerBudgetID, coverAgreement.CustomerContractID, episode.ID,
                                coverAgreement.AmountQty, coverAgreement.HistoryInsurerCoverAgreementID, (int)coverAgreement.Status, userName);
                            coverAgreement.DbTimeStamp = _customerCoverAgreeRelDA.GetDBTimeStamp(coverAgreement.ID);
                            break;
                        default:
                            break;
                    }

                    #region Insurer Agreements
                    if ((coverAgreement.InsurerAgreements != null) && (coverAgreement.InsurerAgreements.Length > 0))
                    {
                        foreach (CustomerInsurerAgreeRelEntity insurerAgreement in coverAgreement.InsurerAgreements)
                        {
                            switch (insurerAgreement.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    _customerInsurerAgreeRelDA.DeleteCustomerInsurerAgreeRel(insurerAgreement.ID);
                                    break;
                                case StatusEntityValue.New:
                                    insurerAgreement.ID = _customerInsurerAgreeRelDA.Insert(coverAgreement.ID, insurerAgreement.HistoryInsurerAgreementID,
                                        insurerAgreement.AmountQty, insurerAgreement.Units, insurerAgreement.TotalQty, (int)insurerAgreement.Status, userName);
                                    insurerAgreement.DbTimeStamp = _customerInsurerAgreeRelDA.GetDBTimeStamp(insurerAgreement.ID);
                                    break;
                                case StatusEntityValue.NewAndDeleted:
                                    break;
                                case StatusEntityValue.None:
                                    break;
                                case StatusEntityValue.Updated:
                                    _customerInsurerAgreeRelDA.Update(insurerAgreement.ID, coverAgreement.ID, insurerAgreement.HistoryInsurerAgreementID,
                                        insurerAgreement.AmountQty, insurerAgreement.Units, insurerAgreement.TotalQty, (int)insurerAgreement.Status, userName);
                                    insurerAgreement.DbTimeStamp = _customerInsurerAgreeRelDA.GetDBTimeStamp(insurerAgreement.ID);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Razones y Razones de alta
            if (episode.EpisodeLeaveReasons != null)
            {
                foreach (CustomerEpisodeLeaveReasonRelEntity celrl in episode.EpisodeLeaveReasons)
                {
                    if (celrl.EditStatus.Value == StatusEntityValue.Deleted)
                    {
                        _customerEpisodeLeaveReasonRelDA.Delete(celrl.ID);
                    }
                }
                foreach (CustomerEpisodeLeaveReasonRelEntity celrl in episode.EpisodeLeaveReasons)
                {
                    switch (celrl.EditStatus.Value)
                    {
                        case StatusEntityValue.New:
                            celrl.CustomerEpisodeID = episode.ID;
                            celrl.ID = _customerEpisodeLeaveReasonRelDA.Insert(celrl.CustomerEpisodeID, celrl.EpisodeReasonID,
                                celrl.ConceptID, (celrl.EpisodeReasonType != null) ? celrl.EpisodeReasonType.ID : 0, userName);
                            break;
                        case StatusEntityValue.Updated:
                            _customerEpisodeLeaveReasonRelDA.Update(celrl.ID, celrl.CustomerEpisodeID, celrl.EpisodeReasonID,
                                celrl.ConceptID, (celrl.EpisodeReasonType != null) ? celrl.EpisodeReasonType.ID : 0, userName);
                            break;
                    }
                }
            }
            if (episode.EpisodeReasons != null)
            {
                foreach (CustomerEpisodeReasonRelEntity cerl in episode.EpisodeReasons)
                {
                    if (cerl.EditStatus.Value == StatusEntityValue.Deleted)
                    {
                        _customerEpisodeReasonRelDA.Delete(cerl.ID);
                    }
                }
                foreach (CustomerEpisodeReasonRelEntity cerl in episode.EpisodeReasons)
                {
                    switch (cerl.EditStatus.Value)
                    {
                        case StatusEntityValue.New:
                            cerl.CustomerEpisodeID = episode.ID;
                            cerl.ID = _customerEpisodeReasonRelDA.Insert(cerl.CustomerEpisodeID, cerl.EpisodeReasonID,
                                cerl.ConceptID, (cerl.EpisodeReasonType != null) ? cerl.EpisodeReasonType.ID : 0, userName);
                            break;
                        case StatusEntityValue.Updated:
                            cerl.CustomerEpisodeID = episode.ID;
                            _customerEpisodeReasonRelDA.Update(cerl.ID, cerl.CustomerEpisodeID, cerl.EpisodeReasonID,
                                cerl.ConceptID, (cerl.EpisodeReasonType != null) ? cerl.EpisodeReasonType.ID : 0, userName);
                            break;
                    }
                }
            }
            #endregion

            #region Autorizaciones
            if (episode.CustomerAuthorizations != null)
            {
                foreach (CustomerEpisodeAuthorizationEntity episodeAuthorization in episode.CustomerAuthorizations)
                {
                    InnerUpdateCustomerEpisodeAuthorization(episodeAuthorization, episode.ID, userName);
                }
            }
            #endregion

            #region Custome Episode Service Rel
            if (episode.CustomerEpisodeServices != null && episode.CustomerEpisodeServices.Length > 0)
            {
                foreach (CustomerEpisodeServiceRelEntity cesr in episode.CustomerEpisodeServices)
                {
                    switch (cesr.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            break;
                        case StatusEntityValue.New:
                            cesr.ID = _customerEpisodeServiceRelDA.Insert(
                                episode.ID, cesr.AssistanceServiceID,
                                (long)cesr.Step, cesr.UnitServiceID,
                                cesr.StartAt, cesr.EndingTo,
                                cesr.PhysicianID, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _customerEpisodeServiceRelDA.Update(cesr.ID,
                                cesr.CustomerEpisodeID, cesr.AssistanceServiceID,
                                (long)cesr.Step, cesr.UnitServiceID,
                                cesr.StartAt, cesr.EndingTo,
                                cesr.PhysicianID, userName);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region refequestingPhysician
            if (episode.ReferencedPhysicians != null)
            {
                foreach (CustomerEpisodeReferencedPhysicianRelEntity cerp in episode.ReferencedPhysicians)
                {
                    switch (cerp.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            _customerEpisodeReferencedPhysicianRelDA.Delete(cerp.ID);
                            break;
                        case StatusEntityValue.New:
                            _customerEpisodeReferencedPhysicianRelDA.Insert(episode.ID, cerp.ReferencedPhysician.MasterID, userName);
                            break;
                        case StatusEntityValue.NewAndDeleted:
                            break;
                        case StatusEntityValue.None:
                            break;
                        case StatusEntityValue.Updated:
                            _customerEpisodeReferencedPhysicianRelDA.Update(cerp.ID, episode.ID, cerp.ReferencedPhysician.MasterID, userName);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            #region Custome Episode Interop information
            if ((episode.InteropInformations != null) && (episode.InteropInformations.Length > 0))
            {
                foreach (CustomerEpInteropInfoEntity interop in episode.InteropInformations)
                {
                    switch (interop.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted: _customerEpInteropInfoDA.DeleteByID(interop.ID); break;
                        case StatusEntityValue.New:
                            interop.ID = _customerEpInteropInfoDA.Insert(episode.CustomerID, episode.ID, interop.IdentifierTypeProvider, interop.IdentifierTypeName,
                                interop.VisitNumber, interop.VisitPriorityCode, interop.VisitDescription, interop.PatientStatusCode, interop.AdmitReason,
                                interop.TransferReason, (int)interop.PatientLocationType, interop.PatientLocation, interop.ExpectedAdmitDateTime, interop.ExpectedDischargeDateTime, userName);
                            break;
                        case StatusEntityValue.Updated:
                            _customerEpInteropInfoDA.Update(interop.ID, interop.CustomerID, interop.CustomerEpisodeID, interop.IdentifierTypeProvider, interop.IdentifierTypeName,
                                interop.VisitNumber, interop.VisitPriorityCode, interop.VisitDescription, interop.PatientStatusCode, interop.AdmitReason,
                                interop.TransferReason, (int)interop.PatientLocationType, interop.PatientLocation, interop.ExpectedAdmitDateTime, interop.ExpectedDischargeDateTime, userName);
                            break;
                        default: break;
                    }
                }
            }
            #endregion

            return episode;
        }

        public void CheckInsertPreconditions(CustomerEpisodeEntity customerEpisode, CustomerAdmissionEntity admission, CustomerAssistancePlanEntity customerAssistancePlan, ElementBL elementBL)
        {
            if (customerEpisode == null) throw new ArgumentNullException("customerEpisode");

            this.Validate(customerEpisode, admission, customerAssistancePlan, elementBL);

            int idCE = _customerEpisodeDA.GetEpisodeIDByEpisodeNumber(customerEpisode.EpisodeNumber, customerEpisode.EpisodeTypeID);
            if (idCE > 0) throw new Exception(string.Format(Properties.Resources.MSG_EpisodeWithNumberOfEpisodeTypeFound, customerEpisode.EpisodeNumber));
        }

        public void CheckUpdatePreconditions(CustomerEpisodeEntity customerEpisode, CustomerAdmissionEntity admission, CustomerAssistancePlanEntity customerAssistancePlan, ElementBL elementBL)
        {
            if (customerEpisode == null) throw new ArgumentNullException("customerEpisode");

            this.Validate(customerEpisode, admission, customerAssistancePlan, elementBL);

            bool exists = _customerEpisodeDA.Exists(customerEpisode.ID);

            if (!exists) throw new Exception(string.Format(Properties.Resources.ERROR_CustomerEpisodeOfEpisodeTypeNotFound, customerEpisode.EpisodeNumber));
        }

        public void CheckDeletePreconditions(CustomerEpisodeEntity customerEpisode, CustomerAdmissionEntity admission, CustomerAssistancePlanEntity customerAssistancePlan, ElementBL elementBL)
        {
            if (customerEpisode == null) throw new ArgumentNullException("customerEpisode");

            this.Validate(customerEpisode, admission, customerAssistancePlan, elementBL);

            bool exists = _customerEpisodeDA.Exists(customerEpisode.ID);

            if (!exists) throw new Exception(string.Format(Properties.Resources.ERROR_CustomerEpisodeOfEpisodeTypeNotFound, customerEpisode.EpisodeNumber));
        }

        public void PreInsertCustomerEpisode(CustomerEpisodeEntity episode, CustomerProcessEntity customerProcess, string userName,
            out string customerEpisodeNumber, out string admissionEventNumber, out string customerAccountNumber,
            ProcessChartEntity processChart, out CustomerAdmissionEntity admission, out CustomerAssistancePlanEntity customerAssistancePlan, out CustomerProcessWaitingListEntity cpwl, out StepResponse[] response,
            ElementBL elementBL, AssistanceProcessChartBL assistanceProcessChartBL, LocationBL locationBL, EpisodeTypeBL episodeTypeBL, LocationTypeBL locationTypeBL,
            CustomerProcessWaitingListBL cpwlBL, CustomerAssistancePlanBL customerAssistancePlanBL)
        {
            if (processChart.EpisodeConfig == null) throw new ArgumentNullException("EpisodeConfig");

            episode.StartDateTime = DateUtils.RoundDown(episode.StartDateTime, TimeSpan.FromMinutes(1));

            CommonEntities.ElementEntity _customerEpisodeMetadata = ElementBL.GetElementByName(EntityNames.CustomerEpisodeEntityName, true);
            CommonEntities.ElementEntity _admissionMetadata = ElementBL.GetElementByName(EntityNames.CustomerAdmissionEntityName, false);
            CommonEntities.ElementEntity _customerAssistancePlanMetadata = ElementBL.GetElementByName(EntityNames.CustomerAssistancePlanEntityName, false);
            CommonEntities.ElementEntity _customerProcedureMetadata = ElementBL.GetElementByName(EntityNames.CustomerProcedureEntityName, false);
            CommonEntities.ElementEntity _customerRoutineMetadata = ElementBL.GetElementByName(EntityNames.CustomerRoutineEntityName, false);
            CommonEntities.ElementEntity _customerAdmissionMetadata = ElementBL.GetElementByName(EntityNames.CustomerAdmissionEntityName, false);
            CommonEntities.ElementEntity _insurerCoverAgreementMetadata = ElementBL.GetElementByName(EntityNames.InsurerCoverAgreementEntityName, false);
            CommonEntities.ElementEntity _insurerAgreementMetadata = ElementBL.GetElementByName(EntityNames.InsurerAgreementEntityName, false);
            CommonEntities.ElementEntity _insurerConditionMetadata = ElementBL.GetElementByName(EntityNames.InsurerConditionEntityName, false);

            #region Masks and metadata
            int careCenterID = customerProcess.CareCenterID;

            customerEpisodeNumber = string.Empty;
            if (_customerEpisodeMetadata != null)
            {
                int elementID = _customerEpisodeMetadata.ID;
                CommonEntities.AttributeEntity attribute = _customerEpisodeMetadata.GetAttribute("EpisodeNumber");
                int elementAttributeID = (attribute != null) ? attribute.ID : 0;
                customerEpisodeNumber = _processChartBL.GetCodeGeneratorName(careCenterID, elementID, elementAttributeID, processChart.ID, ProcessChartCodeGeneratorsEnum.EpisodeNumber);
            }

            customerAccountNumber = _processChartBL.GetCodeGeneratorName(careCenterID, 0, 0, processChart.ID, ProcessChartCodeGeneratorsEnum.AccountNumber);

            admissionEventNumber = string.Empty;
            if (_admissionMetadata != null)
            {
                int elementID = _admissionMetadata.ID;
                CommonEntities.AttributeEntity attribute = _admissionMetadata.GetAttribute("EventNumber");
                int elementAttributeID = (attribute != null) ? attribute.ID : 0;
                admissionEventNumber = _processChartBL.GetCodeGeneratorName(careCenterID, elementID, elementAttributeID, 0, ProcessChartCodeGeneratorsEnum.None);
            }

            AssistanceProcessChartEntity assistanceProcessChart = base.GetByProcessChart(processChart, (customerProcess != null) ? customerProcess.CareCenterID : 0, episode.AdmissionServiceID);
            #endregion

            #region Admission data
            CustomerAdmissionHelper _customerAdmissionHelper = new CustomerAdmissionHelper(_customerAdmissionMetadata);
            admission = null;
            if (customerProcess.GetStepID(BasicProcessStepsEnum.Admission) > 0)
            {
                admission = base.GetCustomerAdmission(customerProcess.GetStepID(BasicProcessStepsEnum.Admission));
            }

            switch (processChart.EpisodeMandatoryConfig)
            {
                case EpisodeMandatoryConfigEnum.None:
                    break;
                case EpisodeMandatoryConfigEnum.AdmissionMandatory:
                    admission = _customerAdmissionHelper.Create();
                    admission.EditStatus.Value = StatusEntityValue.New;
                    admission.CustomerID = episode.CustomerID;
                    admission.ProcessChartID = episode.ProcessChartID;
                    admission.StartDateTime = episode.StartDateTime;
                    admission.EndDateTime = episode.EndDateTime;
                    admission.Status = CommonEntities.StatusEnum.Active;
                    if (episode.CurrentLocationAvail != null)
                    {
                        admission.CurrentLocation = locationBL.GetLocation(episode.CurrentLocationAvail.LocationID, false, false);
                    }
                    break;
                case EpisodeMandatoryConfigEnum.AssistanceMandatory:
                    if (customerProcess.GetStepID(BasicProcessStepsEnum.Admission) > 0)
                    {
                        admission = base.GetCustomerAdmission(customerProcess.GetStepID(BasicProcessStepsEnum.Admission));
                        admission.EditStatus.Value = StatusEntityValue.Updated;
                        admission.CustomerID = episode.CustomerID;
                        admission.ProcessChartID = episode.ProcessChartID;
                        admission.StartDateTime = episode.StartDateTime;
                        admission.EndDateTime = episode.EndDateTime;
                        admission.Status = CommonEntities.StatusEnum.Active;
                        if (episode.CurrentLocationAvail != null)
                        {
                            admission.CurrentLocation = locationBL.GetLocation(episode.CurrentLocationAvail.LocationID, false, false);
                        }
                    }
                    else
                    {
                        admission = _customerAdmissionHelper.Create();
                        admission.EditStatus.Value = StatusEntityValue.New;
                        admission.CustomerID = episode.CustomerID;
                        admission.ProcessChartID = episode.ProcessChartID;
                        admission.StartDateTime = episode.StartDateTime;
                        admission.EndDateTime = episode.EndDateTime;
                        admission.Status = CommonEntities.StatusEnum.Active;
                        if (episode.CurrentLocationAvail != null)
                        {
                            admission.CurrentLocation = locationBL.GetLocation(episode.CurrentLocationAvail.LocationID, false, false);
                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion

            #region Assistance plan
            customerAssistancePlan = null;
            if ((assistanceProcessChart != null) && (assistanceProcessChart.OpenAssistancePlan))
            {
                List<CustomerRoutineEntity> routines = new List<CustomerRoutineEntity>();
                List<CustomerProcedureEntity> procedures = new List<CustomerProcedureEntity>();

                //PrescriptionSupplyConfigEntity psc = assistanceProcessChartBL.GetPrescriptionSupplyConfig(assistanceProcessChart,
                //    (admission != null) ? admission.CurrentLocation : null);

                PrescriptionSupplyConfigEntity psc = ((assistanceProcessChart != null) && (assistanceProcessChart.PrescriptionSupplyConfig != null))
                    ? assistanceProcessChart.PrescriptionSupplyConfig : null;

                if (admission.CurrentLocation != null)
                {
                    LocationEntity storageloc = LocationBL.GetStorageLocationOfLocation(admission.CurrentLocation.ID);
                    if (storageloc != null)
                    {
                        if (psc != null)
                        {
                            CustomerRoutineEntity cr = customerAssistancePlanBL.GetCustomerRoutineByPrescriptionSupplyConfig(psc, episode,
                                storageloc.ID, _customerRoutineMetadata);
                            if (cr != null) routines.Add(cr);
                        }

                        if (assistanceProcessChart.CareProcessElements != null)
                        {
                            foreach (CareProcessElementConfigEntity cpe in assistanceProcessChart.CareProcessElements)
                            {
                                if (cpe.Element == AppointmentElementEnum.Routine && cpe.IncludingInCarePlan)
                                {
                                    CustomerRoutineEntity cr = customerAssistancePlanBL.GetCustomerRoutineByCareProcessElement(cpe, episode,
                                        storageloc.ID, _customerRoutineMetadata);
                                    if (cr != null) routines.Add(cr);
                                }
                                if (cpe.Element == AppointmentElementEnum.Procedure && cpe.IncludingInCarePlan)
                                {
                                    CustomerProcedureEntity cp = customerAssistancePlanBL.GetCustomerProcedureByCareProcessElement(cpe, episode,
                                        storageloc.ID, _customerProcedureMetadata);
                                    if (cp != null) procedures.Add(cp);
                                }
                            }
                        }
                    }
                }
                CustomerAssistancePlanHelper customerAssistancePlanHelper = new CustomerAssistancePlanHelper(_customerAssistancePlanMetadata);
                customerAssistancePlan = customerAssistancePlanHelper.Create();
                customerAssistancePlan.CustomerID = episode.CustomerID;
                customerAssistancePlan.AssistanceProcessChartID = assistanceProcessChart.ID;
                customerAssistancePlan.InitDateTime = episode.StartDateTime;
                customerAssistancePlan.EndDateTime = episode.EndDateTime;
                customerAssistancePlan.Status = CommonEntities.StatusEnum.Active;
                customerAssistancePlan.Routines = routines.ToArray();
                customerAssistancePlan.Procedures = procedures.ToArray();
                customerAssistancePlan.EditStatus.New();
            }
            #endregion

            #region Location Availability

            bool _episodeHasLocation = processChart.EpisodeConfig.HasLocation;

            if (((_episodeHasLocation) && (episode.CurrentLocationAvail != null) && (episode.CurrentLocationAvail.LocationID <= 0))
                || ((_episodeHasLocation) && (episode.CurrentLocationAvail == null)))
                throw new Exception(Properties.Resources.ERROR_LocationRequired);

            if (episode.CurrentLocationAvail != null)
            {
                LocationTypeEntity locationType = locationTypeBL.GetLocationTypeByLocationID(episode.CurrentLocationAvail.LocationID);
                if ((locationType.HasAvailability) && (_locationAvailPatternDA.HasAvailPatternsWithFillPattern(episode.CurrentLocationAvail.LocationID, episode.StartDateTime, episode.EndDateTime,
                    (int)FillPatternEnum.WithoutFillPattern, (int)CommonEntities.StatusEnum.Active)))
                {
                    if (_locationAvailabilityDA.IsLocationAvailable(episode.CurrentLocationAvail.LocationID, episode.StartDateTime, episode.EndDateTime, (int)CommonEntities.StatusEnum.Active))
                    {
                        int availPatternID = _locationAvailPatternDA.GetAvailPatternIDFirstWithFillPattern(episode.CurrentLocationAvail.LocationID, episode.StartDateTime, episode.EndDateTime,
                            (int)FillPatternEnum.WithoutFillPattern, (int)CommonEntities.StatusEnum.Active);
                        episode.CurrentLocationAvail = new LocationAvailabilityEntity(0, episode.CurrentLocationAvail.LocationID, availPatternID,
                            episode.StartDateTime, episode.EndDateTime, AvailStatusEnum.Busy, 0);
                        episode.CurrentLocationAvail.EditStatus.Value = StatusEntityValue.New;
                    }
                    else throw new Exception(Properties.Resources.ERROR_LocationIsBusy);
                }
                else
                {
                    if (processChart.EpisodeConfig.CalculationOfStayIntervals)
                    {
                        episode.CurrentLocationAvail.StartDateTime = episode.StartDateTime;
                        episode.CurrentLocationAvail.EndDateTime = episode.EndDateTime;
                        episode.CurrentLocationAvail.AvailPatternID = processChart.EpisodeConfig.DefaultAvailPatternID;
                        episode.CurrentLocationAvail.Status = AvailStatusEnum.Busy;
                        episode.CurrentLocationAvail.EditStatus.Value = StatusEntityValue.New;
                    }
                    else episode.CurrentLocationAvail = null;
                }
            }

            #endregion

            #region Customer Policy
            if (episode.CustomerPolicy != null)
            {
                episode.CustomerPolicy.CustomerID = episode.CustomerID;
            }
            #endregion

            #region IStepContract BeforeStepAction
            response = this.CallBeforeStepAction(episode, admission, processChart);
            #endregion

            // Cuando es NEW se deberán borrar los codigos que se van a generar
            if (!String.IsNullOrEmpty(customerEpisodeNumber)) episode.EpisodeNumber = String.Empty;
            if (!String.IsNullOrEmpty(admissionEventNumber)) admission.EventNumber = String.Empty;

            //this.Validate(episode, admission, customerAssistancePlan);
            this.CheckInsertPreconditions(episode, admission, customerAssistancePlan, elementBL);

            //No es el mejor sitio para validar esto, pero tal y como está planteada esta BL es lo mejor para no modificar métodos pasando el ProcessChart
            if ((processChart != null) && (processChart.AdmissionConfig != null) && (processChart.AdmissionConfig.AdmissionOrderRequired) && (episode.ADTOrder == null))
            {
                throw new Exception(string.Format(Properties.Resources.MSG_ADTOrderRequiredByAdmissionConfiguration, episode.EpisodeNumber));
            }

            #region Assistance Agreements
            ValidateSetDefaultAssistanceAgreements(episode, customerProcess, processChart, userName);
            AddCoverConfigAssistanceAgreement(episode, processChart, customerProcess);
            if (episode.AssistanceAgreements != null)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                //DO MIGUEL
                //AQUI DEBEMOS PONER UN CHECKPRECONDITIONS
                //
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                ValidateAssisanceAgreements(episode.AssistanceAgreements);
            }
            #endregion

            #region Insurer Cover Agreements
            if (episode.CoverAgreements != null)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                //DO MIGUEL
                //AQUI DEBEMOS PONER UN CHECKPRECONDITIONS
                //
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                ValidateCoverAgreements(episode.CoverAgreements);
            }
            #endregion

            #region History authorization entries
            if (episode.CustomerAuthorizations != null)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //
                //DO MIGUEL
                //AQUI DEBEMOS PONER UN CHECKPRECONDITIONS
                //
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                ValidateAuthorizations(episode.CustomerAuthorizations);



                //foreach (CustomerEpisodeAuthorizationEntity episodeAuthorization in episode.CustomerAuthorizations)
                //{
                //    if (episodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                //    {
                //        foreach (CustomerEpisodeAuthorizationEntryEntity authorizationEntry in episodeAuthorization.CustomerEpisodeAuthorizationEntries)
                //        {
                //            switch (authorizationEntry.AuthorizedElementName)
                //            {
                //                case EntityNames.AssistanceAgreementEntityName:
                //                    authorizationEntry.AuthorizedActID = historyAssistanceAgreementBL.Save(authorizationEntry.AuthorizedActID, customerProcess.CareCenterID).ID;
                //                    break;
                //                case EntityNames.AgreementEntityName:
                //                    authorizationEntry.AuthorizedActID = historyAgreementBL.Save(authorizationEntry.AuthorizedActID).ID;
                //                    break;
                //                case EntityNames.AgreeConditionEntityName:
                //                    authorizationEntry.AuthorizedActID = historyAgreeConditionBL.Save(authorizationEntry.AuthorizedActID).ID;
                //                    break;
                //                case EntityNames.InsurerCoverAgreementEntityName:
                //                    if (authorizationEntry.AuthorizedActID <= 0)
                //                    {
                //                        authorizationEntry.AuthorizedActID = _insurerCoverAgreementDA.FindInsurerCoverAgreement((episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0, authorizationEntry.AuthorizedActCode);
                //                    }
                //                    if (authorizationEntry.AuthorizedActID > 0)
                //                    {
                //                        authorizationEntry.AuthorizedActID = historyInsurerCoverAgreementBL.Save(authorizationEntry.AuthorizedActID).ID;
                //                    }
                //                    if (authorizationEntry.AuthorizedElementID <= 0)
                //                    {
                //                        authorizationEntry.AuthorizedElementID = _insurerCoverAgreementMetadata.ID;
                //                    }
                //                    break;
                //                case EntityNames.InsurerAgreementEntityName:
                //                    if (authorizationEntry.AuthorizedActID > 0)
                //                    {
                //                        int myHistoryInsureCoverAgreementID = historyInsurerCoverAgreementBL.Save(insurerAgreementBL.GetInsurerAgreement(authorizationEntry.AuthorizedActID).InsurerCoverAgreementID).ID;
                //                        authorizationEntry.AuthorizedActID = historyInsurerAgreementBL.Save(authorizationEntry.AuthorizedActID).ID;
                //                    }
                //                    else
                //                    {
                //                        authorizationEntry.AuthorizedActID = _insurerAgreementDA.FindInsurerAgreementByCodeAndInsurerID((episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0, authorizationEntry.AuthorizedActCode);
                //                        if (authorizationEntry.AuthorizedActID > 0)
                //                        {
                //                            authorizationEntry.AuthorizedActID = historyInsurerAgreementBL.Save(authorizationEntry.AuthorizedActID).ID;
                //                        }
                //                    }
                //                    if (authorizationEntry.AuthorizedElementID <= 0)
                //                    {
                //                        authorizationEntry.AuthorizedElementID = _insurerAgreementMetadata.ID;
                //                    }
                //                    break;
                //                case EntityNames.InsurerConditionEntityName:
                //                    if (authorizationEntry.AuthorizedActID > 0)
                //                    {
                //                        authorizationEntry.AuthorizedActID = historyInsurerConditionBL.Save(authorizationEntry.AuthorizedActID).ID;
                //                    }
                //                    else
                //                    {
                //                        authorizationEntry.AuthorizedActID = historyInsurerConditionBL.Save(authorizationEntry.AuthorizedActCode).ID;
                //                    }
                //                    if (authorizationEntry.AuthorizedElementID <= 0)
                //                    {
                //                        authorizationEntry.AuthorizedElementID = _insurerConditionMetadata.ID;
                //                    }
                //                    break;
                //                default:
                //                    break;
                //            }
                //        }
                //    }
                //}
            }
            #endregion

            #region refequestingPhysician
            if (episode.ReferencedPhysicians != null)
            {
                List<CustomerEpisodeReferencedPhysicianRelEntity> cerpList = new List<CustomerEpisodeReferencedPhysicianRelEntity>();
                foreach (CustomerEpisodeReferencedPhysicianRelEntity cerp in episode.ReferencedPhysicians)
                {
                    if ((cerp.ReferencedPhysician != null) && (cerp.ReferencedPhysician.MasterID > 0))
                    {
                        cerpList.Add(cerp);
                    }
                    episode.ReferencedPhysicians = cerpList.ToArray();
                }
            }
            #endregion

            #region cpwl
            cpwl = (customerProcess.GetStepID(BasicProcessStepsEnum.WaitingList) > 0)
                ? cpwlBL.GetCustomerProcessWaitingList(customerProcess.GetStepID(BasicProcessStepsEnum.WaitingList))
                : null;
            #endregion
        }

        //public void InsertCustomerEpisode(CustomerEpisodeEntity episode, CustomerProcessEntity customerProcess, string userName,
        //    string customerEpisodeNumber, string admissionEventNumber, string customerAccountNumber,
        //    int insurerID, ProcessChartEntity processChart, CustomerAdmissionEntity admission, CustomerAssistancePlanEntity customerAssistancePlan,
        //    CustomerProcessWaitingListEntity cpwl, CustomerBL customerBL, CustomerAssistancePlanBL customerAssistancePlanBL, CustomerProcessWaitingListBL cpwlBL)
        //{
        //    MedicalEpisodeDA _medicalEpisodeDA = new MedicalEpisodeDA();
        //    if (episode.Physician != null &&
        //       ((processChart.AdmissionConfig != null && processChart.AdmissionConfig.OnAdmitCreateMedicalEpisode && processChart.AdmissionConfig.MedEpisodeProcessChart != null) ||
        //        (processChart.ReceptionConfig != null && processChart.ReceptionConfig.OnAdmitCreateMedicalEpisode && processChart.ReceptionConfig.MedEpisodeProcessChart != null)) &&
        //        (processChart.AdmissionConfig.MedEpisodeProcessChart.PhysicianRels == null || processChart.AdmissionConfig.MedEpisodeProcessChart.PhysicianRels.Length <= 0 ||
        //            _medicalEpisodeDA.ValidatePhysician(episode.Physician.ID, (processChart.AdmissionConfig.MedEpisodeProcessChart != null)
        //                    ? processChart.AdmissionConfig.MedEpisodeProcessChart.ID : processChart.ReceptionConfig.MedEpisodeProcessChart.ID)))

        //        if (episode.ADTOrder != null)
        //        {
        //            episode.ADTOrder.Status = ActionStatusEnum.Completed;
        //            episode.ADTOrder.EditStatus.Update();
        //        }

        //    int[] customerRoutineIDs = null;
        //    int[] routineActIDs = null;
        //    int[] customerProcedureIDs = null;
        //    int[] procedureActIDs = null;
        //    int[] customerOrderRequestIDs = null;
        //    int customerReservationID = customerProcess.GetStepID(BasicProcessStepsEnum.Reservation);
        //    if (customerReservationID > 0)
        //    {
        //        //esto queda pendiente para cuando la reserva disponga de estos datos.
        //        //CustomerReservationEntity cre = CustomerReservationBL.GetCustomerReservation(customerProcess.GetStepID(BasicProcessStepsEnum.Reservation));
        //        //if (cre != null)
        //        //{
        //        List<int> coreIDs = new List<int>();
        //        //if (cre.ReservedProcedureActs != null && cre.ReservedProcedureActs.Length > 0)
        //        //{
        //        //    procedureActIDs = (from rpa in cre.ReservedProcedureActs
        //        //                       where rpa.Element == AppointmentElementEnum.Procedure && rpa.ElementID > 0
        //        //                       select rpa.ElementID).ToArray();
        //        //    customerProcedureIDs = (from rpa in cre.ReservedProcedureActs
        //        //                            where rpa.Element == AppointmentElementEnum.CustomerProcedure && rpa.ElementID > 0
        //        //                            select rpa.ElementID).ToArray();
        //        //    if (procedureActIDs != null && procedureActIDs.Length > 0)
        //        //    {
        //        //        List<int> cprids = new List<int>();
        //        //        if (customerProcedureIDs != null && customerProcedureIDs.Length > 0)
        //        //            cprids.AddRange(customerProcedureIDs);
        //        //        foreach (int item in routineActIDs)
        //        //        {
        //        //            int cpid = _procedureActDA.GetCustomerProcedureIDByID(item);
        //        //            if (cpid > 0) cprids.Add(cpid);
        //        //            cpid = _procedureActDA.GetCustomerOrderRequestIDByID(item);
        //        //            if (cpid > 0 && (coreIDs.Count <= 0 || !Array.Exists(coreIDs.ToArray(), core => core == cpid))) coreIDs.Add(cpid);
        //        //        }
        //        //        customerProcedureIDs = cprids.Count > 0 ? cprids.ToArray() : null;
        //        //    }
        //        //}
        //        //if (cre.ReservedRoutineActs != null && cre.ReservedRoutineActs.Length > 0)
        //        //{
        //        //    routineActIDs = (from rra in cre.ReservedRoutineActs
        //        //                     where rra.Element == AppointmentElementEnum.Routine && rra.ElementID > 0
        //        //                     select rra.ElementID).ToArray();
        //        //    customerRoutineIDs = (from rra in cre.ReservedRoutineActs
        //        //                          where rra.Element == AppointmentElementEnum.CustomerRoutine && rra.ElementID > 0
        //        //                          select rra.ElementID).ToArray();
        //        //    if (routineActIDs != null && routineActIDs.Length > 0)
        //        //    {
        //        //        List<int> crrids = new List<int>();
        //        //        if (customerRoutineIDs != null && customerRoutineIDs.Length > 0)
        //        //            crrids.AddRange(customerRoutineIDs);
        //        //        foreach (int item in routineActIDs)
        //        //        {
        //        //            int crid = _routineActDA.GetCustomerRoutineIDByID(item);
        //        //            if (crid > 0) crrids.Add(crid);
        //        //            crid = _routineActDA.GetCustomerOrderRequestIDByID(item);
        //        //            if (crid > 0 && (coreIDs.Count <= 0 || !Array.Exists(coreIDs.ToArray(), core => core == crid))) coreIDs.Add(crid);
        //        //        }
        //        //        customerRoutineIDs = crrids.Count > 0 ? crrids.ToArray() : null;
        //        //    }
        //        //}
        //        DataSet ds = _routineActDA.GetRoutineActIDsByCustomerReservationIDWithoutCustomerEpisodeID(customerReservationID);
        //        if (ds != null && ds.Tables != null && ds.Tables.Contains(Assistance.Entities.TableNames.RoutineActTable)
        //            && ds.Tables[Assistance.Entities.TableNames.RoutineActTable].Rows != null
        //            && ds.Tables[Assistance.Entities.TableNames.RoutineActTable].Rows.Count > 0)
        //            routineActIDs = (from row in ds.Tables[Assistance.Entities.TableNames.RoutineActTable].AsEnumerable()
        //                             where (row["ID"] as int? ?? 0) > 0
        //                             select (row["ID"] as int? ?? 0)).ToArray();
        //        ds = _customerRoutineDA.GetCustomerRoutineIDsByCustomerReservationIDWithoutCustomerEpisodeID(customerReservationID);
        //        if (ds != null && ds.Tables != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerRoutineTable)
        //            && ds.Tables[Administrative.Entities.TableNames.CustomerRoutineTable].Rows != null
        //            && ds.Tables[Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0)
        //            customerRoutineIDs = (from row in ds.Tables[Administrative.Entities.TableNames.CustomerRoutineTable].AsEnumerable()
        //                                  where (row["ID"] as int? ?? 0) > 0
        //                                  select (row["ID"] as int? ?? 0)).ToArray();

        //        ds = _procedureActDA.GetProcedureActIDsByCustomerReservationIDWithoutCustomerEpisodeID(customerReservationID);
        //        if (ds != null && ds.Tables != null && ds.Tables.Contains(Assistance.Entities.TableNames.ProcedureActTable)
        //            && ds.Tables[Assistance.Entities.TableNames.ProcedureActTable].Rows != null
        //            && ds.Tables[Assistance.Entities.TableNames.ProcedureActTable].Rows.Count > 0)
        //            procedureActIDs = (from row in ds.Tables[Assistance.Entities.TableNames.ProcedureActTable].AsEnumerable()
        //                               where (row["ID"] as int? ?? 0) > 0
        //                               select (row["ID"] as int? ?? 0)).ToArray();
        //        ds = _customerProcedureDA.GetCustomerProcedureIDsByCustomerReservationIDWithoutCustomerEpisodeID(customerReservationID);
        //        if (ds != null && ds.Tables != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerProcedureTable)
        //            && ds.Tables[Administrative.Entities.TableNames.CustomerProcedureTable].Rows != null
        //            && ds.Tables[Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0)
        //            customerProcedureIDs = (from row in ds.Tables[Administrative.Entities.TableNames.CustomerProcedureTable].AsEnumerable()
        //                                    where (row["ID"] as int? ?? 0) > 0
        //                                    select (row["ID"] as int? ?? 0)).ToArray();

        //        ds = _customerOrderRequestDA.GetCustomerOrderRequestIDsByCustomerReservationIDWithoutCustomerEpisodeID(customerReservationID);
        //        if (ds != null && ds.Tables != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
        //            && ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows != null
        //            && ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0)
        //            customerOrderRequestIDs = (from row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
        //                                       where (row["ID"] as int? ?? 0) > 0
        //                                       select (row["ID"] as int? ?? 0)).ToArray();
        //        //customerOrderRequestIDs = coreIDs.Count > 0 ? coreIDs.ToArray() : null;
        //        //}
        //    }
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        this.InnerInsertCustomerEpisode(episode, customerProcess, userName, customerEpisodeNumber, admissionEventNumber, customerAccountNumber,
        //            insurerID, processChart, admission, customerAssistancePlan,
        //            customerRoutineIDs, routineActIDs, customerProcedureIDs, procedureActIDs, customerOrderRequestIDs,

        //            cpwl, customerBL, customerAssistancePlanBL, cpwlBL, MedicalEpisodeBL);

        //        if ((episode.ADTOrder != null))
        //        {
        //            episode.ADTOrder.CustomerEpisodeID = episode.ID;
        //            episode.ADTOrder = CustomerOrderRequestBL.Save(episode.ADTOrder);
        //            _customerEpisodeDA.Update(episode.ID, episode.ADTOrder.ID, userName);
        //        }

        //        scope.Complete();
        //    }

        //    LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, episode.ID, ActionType.Create);
        //}

        public void InnerInsertCustomerEpisode(CustomerEpisodeEntity episode, CustomerProcessEntity customerProcess,
            string userName, string customerEpisodeNumber, string admissionEventNumber, string customerAccountNumber,
            int insurerID, ProcessChartEntity processChart, CustomerAdmissionEntity admission,
            CustomerAssistancePlanEntity customerAssistancePlan,
            int[] customerRoutineIDs, int[] routineActIDs, int[] customerProcedureIDs, int[] procedureActIDs, int[] customerOrderRequestIDs,
            CustomerProcessWaitingListEntity cpwl,
            CustomerBL customerBL, CustomerAssistancePlanBL customerAssistancePlanBL, CustomerProcessWaitingListBL cpwlBL, MedicalEpisodeBL medicalEpisodeBL)
        {
            this.InnerInsert(episode, processChart, customerProcess, admission, customerAssistancePlan,
                customerRoutineIDs, routineActIDs, customerProcedureIDs, procedureActIDs, customerOrderRequestIDs,
                userName, customerEpisodeNumber, admissionEventNumber, customerAccountNumber, cpwl,
                customerBL, customerAssistancePlanBL, cpwlBL, medicalEpisodeBL);
            if (customerProcess.GetStepID(BasicProcessStepsEnum.Reservation) > 0)
            {
                _customerReservationDA.Update(customerProcess.GetStepID(BasicProcessStepsEnum.Reservation),
                    episode.StartDateTime, (episode.Physician != null) ? episode.Physician.ID : 0, insurerID, (int)CommonEntities.StatusEnum.Confirmed, userName);
            }
        }

        public void PreUpdateCustomerEpisode(CustomerEpisodeEntity episode, CustomerProcessEntity customerProcess, ElementBL elementBL)
        {
            this.CheckUpdatePreconditions(episode, null, null, elementBL);

            #region History authorization entries


            if (episode.CustomerAuthorizations != null)
            {
                ValidateAuthorizations(episode.CustomerAuthorizations);
                //    foreach (CustomerEpisodeAuthorizationEntity episodeAuthorization in episode.CustomerAuthorizations)
                //    {
                //        if (episodeAuthorization.CustomerEpisodeAuthorizationEntries != null)
                //        {
                //            switch (episodeAuthorization.EditStatus.Value)
                //            {
                //                case StatusEntityValue.None: break;
                //                case StatusEntityValue.Deleted: break;
                //                case StatusEntityValue.NewAndDeleted: break;
                //                case StatusEntityValue.New:
                //                    episodeAuthorization.CustomerEpisodeAuthorizationEntries = this.SaveHistoryAuthorizationEntries((episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0,
                //                        episodeAuthorization.CustomerEpisodeAuthorizationEntries, customerProcess.CareCenterID, elementBL, historyAssistanceAgreementBL, historyAgreementBL,
                //                        historyAgreeConditionBL, insurerAgreementBL, historyInsurerCoverAgreementBL, historyInsurerAgreementBL, historyInsurerConditionBL);
                //                    break;
                //                case StatusEntityValue.Updated:
                //                    episodeAuthorization.CustomerEpisodeAuthorizationEntries = this.SaveHistoryAuthorizationEntries((episodeAuthorization.Insurer != null) ? episodeAuthorization.Insurer.ID : 0,
                //                        episodeAuthorization.CustomerEpisodeAuthorizationEntries, customerProcess.CareCenterID, elementBL, historyAssistanceAgreementBL, historyAgreementBL,
                //                        historyAgreeConditionBL, insurerAgreementBL, historyInsurerCoverAgreementBL, historyInsurerAgreementBL, historyInsurerConditionBL);
                //                    break;
                //                default: break;
                //            }
                //        }
                //    }
            }
            #endregion

            #region refequestingPhysician
            if (episode.ReferencedPhysicians != null)
            {
                List<CustomerEpisodeReferencedPhysicianRelEntity> cerpList = new List<CustomerEpisodeReferencedPhysicianRelEntity>();
                foreach (CustomerEpisodeReferencedPhysicianRelEntity cerp in episode.ReferencedPhysicians)
                {
                    if ((cerp.ReferencedPhysician != null) && (cerp.ReferencedPhysician.MasterID > 0))
                    {
                        cerpList.Add(cerp);
                    }
                    episode.ReferencedPhysicians = cerpList.ToArray();
                }
            }
            #endregion
        }

        public CustomerEpisodeEntity UpdateCustomerEpisode(CustomerEpisodeEntity episode, CustomerProcessEntity customerProcess, int insurerID, string userName)
        {
            CustomerOrderRequestBL customerOrderRequestBL = null;
            if (episode.ADTOrder != null)
            {
                if (episode.ADTOrder.Status != ActionStatusEnum.Completed)
                {
                    episode.ADTOrder.Status = ActionStatusEnum.Completed;
                    episode.ADTOrder.EditStatus.Update();
                }
                customerOrderRequestBL = new CustomerOrderRequestBL();
            }

            using (TransactionScope scope = new TransactionScope())
            {
                if ((episode.ADTOrder != null) && (customerOrderRequestBL != null))
                {
                    episode.ADTOrder.CustomerEpisodeID = episode.ID;
                    episode.ADTOrder = customerOrderRequestBL.Save(episode.ADTOrder);
                }

                this.InnerUpdateCustomerEpisode(episode, customerProcess, insurerID, userName);
                scope.Complete();
            }

            LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, episode.ID, ActionType.Modify);
            return episode;
        }

        public void InnerUpdateCustomerEpisode(CustomerEpisodeEntity episode, CustomerProcessEntity customerProcess, int insurerID, string userName)
        {
            this.InnerUpdate(episode, userName);
            if (customerProcess.GetStepID(BasicProcessStepsEnum.Reservation) > 0)
            {
                _customerReservationDA.Update(customerProcess.GetStepID(BasicProcessStepsEnum.Reservation),
                    episode.StartDateTime, (episode.Physician != null) ? episode.Physician.ID : 0, insurerID, (int)CommonEntities.StatusEnum.Confirmed, userName);
            }
        }

        public int[] GetRelatedEpisodeIDsByCustomerIDsEpisodeIDs(int[] customerIDs, int[] episodeIDs)
        {
            if ((customerIDs == null) || (customerIDs.Length <= 0) || (episodeIDs == null) || (episodeIDs.Length <= 0))
                return null;

            List<int> result = new List<int>();

            //Traerse todos los filas con customerID, CustomerEpisodeID, PredeccesorID.
            DataSet dataset = null;
            if (customerIDs.Length > 1)
            {
                string listCustomerIDs = this.GetStringListIDForQuerySQL(customerIDs);
                dataset = _customerEpisodeDA.GetCustomerEpisodeIDAndPredecessorIDsByStringListCustomerEpisodeIDs(listCustomerIDs);
            }
            else dataset = _customerEpisodeDA.GetCustomerEpisodeIDAndPredecessorIDsByCustomerID(customerIDs[0]);

            //Por cada episodeIDs buscar los episodios relacionados (predecesores y descendientes).
            foreach (int episodeID in episodeIDs)
            {
                this.LoadEpisodeIDAndRelatedEpisodeIDsInList(episodeID, dataset, ref result);
            }

            return result.ToArray();
        }

        public string GetCustomerEpisodeNumberByID(int customerEpisodeID)
        {
            try
            {
                return _customerEpisodeDA.GetCustomerEpisodeNumber(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return string.Empty;
            }
        }

        public void GetDatesOfCustomerEpisodeByID(int id, out DateTime? startDateTime, out DateTime? endDatetime)
        {
            startDateTime = null;
            endDatetime = null;
            if (id <= 0)
                return;

            DataSet ds = _customerEpisodeDA.GetStartDateTimeAndEndDateTimeOfCustomerEpisodeByID(id);
            if (ds != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeTable)
                && ds.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0)
            {
                DataRow row = ds.Tables[Administrative.Entities.TableNames.CustomerEpisodeTable].Rows[0];
                startDateTime = row["StartDateTime"] as DateTime? ?? null;
                endDatetime = row["EndDateTime"] as DateTime? ?? null;
            }
        }
        #endregion

        #region Public methods
        //public CustomerEpisodeEntity Save(CustomerEpisodeEntity episode, out StepResponse[] responses)
        //{
        //    responses = null;
        //    try
        //    {
        //        if (episode == null) throw new ArgumentNullException("Episode");

        //        LocationBL _locationBL = new LocationBL();
        //        EpisodeTypeBL _episodeTypeBL = new EpisodeTypeBL();
        //        LocationTypeBL _locationTypeBL = new LocationTypeBL();
        //        CustomerProcessWaitingListBL cpwlBL = new CustomerProcessWaitingListBL();

        //        CustomerEpisodeEntity result = null;
        //        CustomerProcessEntity customerProcess = null;
        //        Entities.Clear();
        //        switch (episode.EditStatus.Value)
        //        {
        //            case StatusEntityValue.Deleted: /*result = this.Delete(admission);*/ break;
        //            case StatusEntityValue.New:
        //                result = this.Insert(episode, ElementBL, CustomerBL, AssistanceProcessChartBL, ProcessChartBL, CustomerAssistancePlanBL, _locationBL, _episodeTypeBL,
        //                    _locationTypeBL, cpwlBL, out responses);
        //                //// envia mensaje HL7  ADT^A01 or ADT^A04 si el addin existe
        //                if (!Entities.ContainsKey(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName))
        //                    Entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, result);
        //                if (!Entities.ContainsKey(CommonEntities.Constants.EntityNames.CustomerProcessEntityName))
        //                {
        //                    customerProcess = GetCustomerProcessByEpisodeID(result.ID);
        //                    Entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, customerProcess);
        //                }
        //                HL7MessagingProcessor.ResetEntities(Entities);
        //                HL7MessagingProcessor.ResetBLs(BLs);
        //                HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT01);
        //                /////////////////////////////////////////////////////
        //                break;
        //            case StatusEntityValue.None: result = episode; break;
        //            case StatusEntityValue.NewAndDeleted: result = null; break;
        //            case StatusEntityValue.Updated:
        //                result = this.Update(episode, ElementBL);
        //                //// envia mensaje HL7  ADT^A08 si el addin existe
        //                if (!Entities.ContainsKey(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName))
        //                    Entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, result);
        //                if (!Entities.ContainsKey(CommonEntities.Constants.EntityNames.CustomerProcessEntityName))
        //                {
        //                    customerProcess = GetCustomerProcessByEpisodeID(result.ID);
        //                    Entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, customerProcess);
        //                }

        //                HL7MessagingProcessor.ResetEntities(Entities);
        //                HL7MessagingProcessor.ResetBLs(BLs);
        //                HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT08);
        //                /////////////////////////////////////////////////////
        //                break;
        //            default: throw new ArgumentOutOfRangeException();
        //        }
        //        this.ResetCustomerEpisode(result);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}

        public CustomerEpisodeEntity Save(CustomerEpisodeEntity episode, out StepResponse[] responses)
        {
            CustomerEpisodeUoWBL service = new CustomerEpisodeUoWBL(this);
            return service.Save(episode, out responses);
        }

        public CustomerEpisodeEntity Save(CustomerEpisodeEntity episode,
            out StepResponse[] responses, bool ignoreCleaningStatus)
        {
            CustomerEpisodeUoWBL service = new CustomerEpisodeUoWBL(this);
            return service.Save(episode, out responses, ignoreCleaningStatus);
        }

        public CustomerEpisodeEntity Save(CustomerEpisodeEntity episode,
            DateTime overwrittenAdmissionDateTime, out StepResponse[] responses, bool ignoreCleaningStatus)
        {
            CustomerEpisodeUoWBL service = new CustomerEpisodeUoWBL(this);            
            return service.Save(episode, overwrittenAdmissionDateTime, out responses, ignoreCleaningStatus);
        }

        public CustomerEpisodeEntity Save(CustomerEpisodeEntity episode,
            DateTime overwrittenAdmissionDateTime, DateTime formerAdmissionDate, out StepResponse[] responses, bool ignoreCleaningStatus)
        {
            CustomerEpisodeUoWBL service = new CustomerEpisodeUoWBL(this);
            return service.Save(episode, overwrittenAdmissionDateTime, formerAdmissionDate, out responses, ignoreCleaningStatus);
        }

        public bool SaveCustomerEpisodeAuthorizations(CustomerEpisodeAuthorizationEntity custAuth, CustomerEpisodeAuthorizationEntryEntity custEpAuthEntrie)
        {
            bool customerEpisodeAuthorizationNEW = custAuth.EditStatus.Value == StatusEntityValue.New ? true : false;
            bool customerEpisodeAuthorizationUPD = custAuth.EditStatus.Value == StatusEntityValue.Updated ? true : false;
            bool customerEpisodeAuthorizationEntryNEW = custEpAuthEntrie.EditStatus.Value == StatusEntityValue.New ? true : false;

            _customerEpisodeDA.SaveCustomerEpisodeAuthorizations(custAuth.CustomerEpisodeID, custAuth.ID, custAuth.Insurer != null ? custAuth.Insurer.ID : 0, custAuth.AuthorizationType != null ? custAuth.AuthorizationType.ID : 0,
                custAuth.AuthorizationDocumentNumber, custAuth.AuthorizedBy, (Int16)custAuth.Status, custAuth.LastUpdated, IdentityUser.GetIdentityUserName(), custAuth.IsChipCard,
                custEpAuthEntrie.ID, custEpAuthEntrie.AuthorizedActID, custEpAuthEntrie.AuthorizedElementName, custEpAuthEntrie.AuthorizedElementID,
                (Int16)custEpAuthEntrie.Status, custEpAuthEntrie.LastUpdated, IdentityUser.GetIdentityUserName(), custEpAuthEntrie.IsChipCard,
                custEpAuthEntrie.AuthorizationTypeID, custEpAuthEntrie.AuthorizationDocumentNumber,
                customerEpisodeAuthorizationNEW, customerEpisodeAuthorizationUPD, customerEpisodeAuthorizationEntryNEW);

            return true;
        }

        public DateTimeRange GetValidAdmissionDateTimeRange(int customerEpisodeID, int locationID, bool ignoreCleaningStatus)
        {
            try
            {
                return InnerGetValidAdmissionDateRange(customerEpisodeID, locationID, ignoreCleaningStatus);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public bool ValidateAbortAdmission(int customerEpisodeID)
        {
            //CustomerEpisodeEntity customerEpisode = GetFullCustomerEpisode(customerEpisodeID);
            //CustomerProcessEntity customerProcess = GetCustomerProcessByCustomerEpisodeID(customerEpisodeID);
            //ProcessChartBL _processChartBL = new ProcessChartBL();
            //ProcessChartEntity processChart = _processChartBL.GetByID(customerEpisode.ProcessChartID);

            //return ValidateAbortAdmission(customerEpisode, customerProcess, processChart);
            CustomerEpisodeUoWAbortBL service = new CustomerEpisodeUoWAbortBL(this);
            return service.IsAbortable(customerEpisodeID);
        }

        public CustomerEpisodeEntity AbortAdmission(int customerEpisodeID, int reasonChangeID, string explanation)
        {
            //if (customerEpisodeID <= 0) throw new ArgumentNullException("Episode");

            //CustomerEpisodeEntity customerEpisode = GetFullCustomerEpisode(customerEpisodeID);
            //CustomerProcessEntity customerProcess = GetCustomerProcessByCustomerEpisodeID(customerEpisodeID);
            ////ProcessChartBL _processChartBL = new ProcessChartBL();
            //ProcessChartEntity processChart = ProcessChartBL.GetByID(customerEpisode.ProcessChartID);

            //if (ValidateAbortAdmission(customerEpisode, customerProcess, processChart))
            //{
            //    customerEpisode = AbortAdmission(customerEpisode, reasonChange, explanation, processChart);

            //    //// envia mensaje HL7  ADT^A011 si el addin existe
            //    Entities.Clear();
            //    Entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);
            //    Entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, customerEpisode);
            //    CustomerProcessEntity cpe = GetCustomerProcess(customerProcess.ID);
            //    Entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, cpe);
            //    HL7MessagingProcessor.ResetEntities(Entities);
            //    HL7MessagingProcessor.ResetBLs(BLs);
            //    HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT11);
            //    /////////////////////////////////////////////////////

            //    return customerEpisode;
            //}
            //return null;
            if ((customerEpisodeID <= 0) || (reasonChangeID <= 0))
                return null;

            CustomerEpisodeUoWAbortBL service = new CustomerEpisodeUoWAbortBL(this);
            return service.Abort(customerEpisodeID, reasonChangeID, explanation);
        }

        public CustomerEpisodeEntity AbortAdmission(int customerEpisodeID, ReasonChangeEntity reasonChange, string explanation)
        {
            //if (customerEpisodeID <= 0) throw new ArgumentNullException("Episode");

            //CustomerEpisodeEntity customerEpisode = GetFullCustomerEpisode(customerEpisodeID);
            //CustomerProcessEntity customerProcess = GetCustomerProcessByCustomerEpisodeID(customerEpisodeID);
            ////ProcessChartBL _processChartBL = new ProcessChartBL();
            //ProcessChartEntity processChart = ProcessChartBL.GetByID(customerEpisode.ProcessChartID);

            //if (ValidateAbortAdmission(customerEpisode, customerProcess, processChart))
            //{
            //    customerEpisode = AbortAdmission(customerEpisode, reasonChange, explanation, processChart);

            //    //// envia mensaje HL7  ADT^A011 si el addin existe
            //    Entities.Clear();
            //    Entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);
            //    Entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, customerEpisode);
            //    CustomerProcessEntity cpe = GetCustomerProcess(customerProcess.ID);
            //    Entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, cpe);
            //    HL7MessagingProcessor.ResetEntities(Entities);
            //    HL7MessagingProcessor.ResetBLs(BLs);
            //    HL7MessagingProcessor.SendADTMessages(MessageTypeEnum.HL7_ADT11);
            //    /////////////////////////////////////////////////////

            //    return customerEpisode;
            //}
            //return null;
            if ((customerEpisodeID <= 0) || (reasonChange == null))
                return null;

            CustomerEpisodeUoWAbortBL service = new CustomerEpisodeUoWAbortBL(this);
            return service.Abort(customerEpisodeID, reasonChange.ID, explanation);
        }

        public CustomerEpisodeDTO[] GetCustomerEpisodeWithExtenderChargeInfo(int careCenterID, CareCenterEpisodeTypeDTO[] episodeTypes, int insurerID, int authorizationTypeID,
            CoverAgreementCodeDTO[] coverAgreementCodes, AgreementCodeDTO[] agreementCodes, DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, out bool maxRecordsExceeded)
        {
            maxRecordsExceeded = false;
            try
            {
                int maxRows = ServiceRestrictionHelper.GetMaxRows(EntityNames.CustomerEpisodeDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                DataSet ds = null;

                if ((episodeTypes == null) || (episodeTypes.Length <= 0))
                    return null;

                int[] processChartIDs = GetProcessChartIDsContainsStepBillingByEpisodeType(episodeTypes);

                if ((processChartIDs == null) || (processChartIDs.Length <= 0))
                    return null;

                string concatEpisodeTypes = this.GetEpisodeTypeIDsStringWhereIn(episodeTypes);

                switch (insurerID)
                {
                    case -2:
                        //Todos los episodios que cumplan los filtros selccionados.
                        ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByAllEpisode((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes,
                                (fromDate == null) ? (DateTime?)null : fromDate.Value.Date, (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1),
                                status, processChartIDs, maxRows);
                        break;
                    case -1:
                        if ((coverAgreementCodes != null) && (coverAgreementCodes.Length > 0))
                        {
                            string concatCoverAgreementCodes = this.GetCoverAgreementCodesStringWhereIn(coverAgreementCodes);

                            if ((agreementCodes != null) && (agreementCodes.Length > 0))
                            {
                                string concatAgreementCodes = this.GetAgreementCodesStringWhereIn(agreementCodes);

                                //Todos los episodios que tengan asociado un acuerdo assistencial y no tiene ningun acuerdo de covertura de aseguradora 
                                //y que tenga alguno de los seleccionados o el acuerdo assitenciales padre de los acuerdos seleccioandos.
                                ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByAgreementsCoveragePrivate((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes,
                                    concatCoverAgreementCodes, concatAgreementCodes, (fromDate == null) ? (DateTime?)null : fromDate.Value.Date,
                                    (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1), status, processChartIDs, maxRows);
                            }
                            else
                            {
                                //Todos los episodios que tengan asociado un acuerdo assistencial y no tiene ningun acuerdo de covertura de aseguradora y que tenga alguno de los acuerdos assitenciales seleccionados. 
                                ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByAssistanceAgreementsCoveragePrivate((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes,
                                    concatCoverAgreementCodes, (fromDate == null) ? (DateTime?)null : fromDate.Value.Date, (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1),
                                    status, processChartIDs, maxRows);
                            }
                        }
                        else
                        {
                            //Todos los episodios que tengan asociado un acuerdo de privado y no tiene ningun acuerdo de aseguradora. 
                            ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByAnyCoveragePrivate((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes,
                                (fromDate == null) ? (DateTime?)null : fromDate.Value.Date, (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1),
                                status, processChartIDs, maxRows);
                        }
                        break;
                    case 0:
                        //Todos los episodios que tengan asociado un acuerdo de sociedad
                        ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByAnyCoverageInsurer((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes,
                            (fromDate == null) ? (DateTime?)null : fromDate.Value.Date, (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1),
                            status, processChartIDs, maxRows);
                        break;
                    default:
                        if ((coverAgreementCodes != null) && (coverAgreementCodes.Length > 0))
                        {
                            string concatCoverAgreementCodes = this.GetCoverAgreementCodesStringWhereIn(coverAgreementCodes);

                            if ((agreementCodes != null) && (agreementCodes.Length > 0))
                            {
                                string concatAgreementCodes = this.GetAgreementCodesStringWhereIn(agreementCodes);

                                //Todos los episodios que tengan asociado un acuerdo de covertura de aseguradora y que tenga alguno de los acuerdos de aseguradora o 
                                //que tenga el acuerdo de covertura padre de los acuerdos de aseguradoa seleccionados. 
                                ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByAgreementsCoverageInsurer((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes, insurerID,
                                    concatCoverAgreementCodes, concatAgreementCodes, (fromDate == null) ? (DateTime?)null : fromDate.Value.Date,
                                    (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1), status, processChartIDs, maxRows);
                            }
                            else
                            {
                                //Todos los episodios que tengan asociado un acuerdo de covertura de aseguradora y que tenga alguno de los acuerdos covertura seleccionados. 
                                ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByCoverAgreementsCoverageInsurer((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes, insurerID,
                                    concatCoverAgreementCodes, (fromDate == null) ? (DateTime?)null : fromDate.Value.Date, (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1),
                                    status, processChartIDs, maxRows);
                            }
                        }
                        else
                        {
                            //Todos los episodios que tengan asociado un acuerdo de covertura de sociedad para la aseguradora seleccionada. 
                            ds = _customerEpisodeDA.GetCustomerEpisodeWithChargeExtendedInfoByCoverageInsurerOfInsurerID((careCenterID > 0) ? careCenterID : -1, authorizationTypeID, concatEpisodeTypes, insurerID,
                                (fromDate == null) ? (DateTime?)null : fromDate.Value.Date, (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1), status, processChartIDs, maxRows);
                        }
                        break;
                }

                if ((ds != null)
                    && (ds.Tables != null)
                    && ds.Tables.Contains(AdministrativeEntities.TableNames.CustomerEpisodeTable)
                    && (ds.Tables[AdministrativeEntities.TableNames.CustomerEpisodeTable].Rows.Count > 0))
                {
                    CustomerEpisodeDTOAdvancedAdapter adapter = new CustomerEpisodeDTOAdvancedAdapter();
                    CustomerEpisodeDTO[] result = adapter.GetData(ds);
                    maxRecordsExceeded = ((result != null) && (result.Length >= maxRows));

                    result = SuppressDuplicatedByCustomerEpisode(result);

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

        public CommonEntities.IDDescriptionEntity[] GetCustomerEpisodesPrincipalGuarantor(int careCenterID, CareCenterEpisodeTypeDTO[] episodeTypes,
            DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status)
        {
            try
            {
                CommonEntities.IDPersonNameAdapter adapter = new CommonEntities.IDPersonNameAdapter();
                DataSet ds = null;
                string concatEpisodeTypes = "DECLARE @EpisodeTypeCodes Table([ID] int,[Code] nvarchar(50))";
                if ((episodeTypes != null) && (episodeTypes.Length > 0))
                {
                    foreach (var ept in episodeTypes)
                    {
                        if ((ept != null) && (ept.ID > 0) && (!string.IsNullOrEmpty(ept.Code)))
                            concatEpisodeTypes = string.Concat(concatEpisodeTypes, "INSERT INTO @EpisodeTypeCodes([ID],[Code]) VALUES(", ept.ID.ToString(), ",'", ept.Code, "')", Environment.NewLine);
                    }
                }

                ds = _customerEpisodeDA.GetCustomerEpisodesPrincipalGuarantor((careCenterID > 0) ? careCenterID : -1,
                        concatEpisodeTypes, (fromDate == null) ? (DateTime?)null : fromDate.Value.Date,
                        (toDate == null) ? (DateTime?)null : toDate.Value.Date.AddDays(1).AddSeconds(-1), status);
                if ((ds != null) && (ds.Tables != null) && ds.Tables.Contains(CommonEntities.TableNames.IDDescriptionTable)
                    && (ds.Tables[CommonEntities.TableNames.IDDescriptionTable].Rows.Count > 0))
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

        public CustomerEpisodeDTO GetCustomerEpisodeDTO(int id)
        {
            try
            {
                DataSet ds = _customerEpisodeDA.GetCustomerEpisodeDTOByID(id);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable))
                   && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0))
                {
                    CustomerEpisodeDTOAdvancedAdapter myAdapter = new CustomerEpisodeDTOAdvancedAdapter();
                    CustomerEpisodeDTO[] customerEpisodeDTOs = myAdapter.GetData(ds);
                    if ((customerEpisodeDTOs != null) && (customerEpisodeDTOs.Length > 0))
                        return customerEpisodeDTOs[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEpisodeDTO[] GetCustomerEpisodeDTOByInvoice(int invoiceID)
        {
            try
            {
                DataSet ds = _customerEpisodeDA.GetCustomerEpisodeDTOByInvoiceID(invoiceID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable))
                   && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0))
                {
                    CustomerEpisodeDTOAdvancedAdapter myAdapter = new CustomerEpisodeDTOAdvancedAdapter();
                    return myAdapter.GetData(ds);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEpisodeEntity GetFullCustomerEpisodeByCustomerMedEpisodeAct(int customerMedEpisodeActID)
        {
            try
            {
                int customerEpisodeID = _customerEpisodeDA.GetCustomerEpisodeIDByCustomerMedEpisodeActID(customerMedEpisodeActID);

                return (customerEpisodeID > 0) ? this.GetFullCustomerEpisode(customerEpisodeID) : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="admissionID">id del episodio</param>
        /// <returns>CustomerEpisodeEntity</returns>
        public CustomerEpisodeEntity GetFullCustomerEpisode(int id)
        {
            return GetFullCustomerEpisode(id, true);
        }
        //public CustomerEpisodeEntity GetFullCustomerEpisode(int id, bool showOutdatedCoverAgrees)
        //{
        //    try
        //    {
        //        CustomerEpisodeEntity oldCustomerEpisodeEntity = GetFullCustomerEpisode_old(id, showOutdatedCoverAgrees);
        //        CustomerEpisodeEntity newCustomerEpisodeEntity = GetFullCustomerEpisode_new(id, showOutdatedCoverAgrees);

        //        bool res = newCustomerEpisodeEntity.CompareEquals(oldCustomerEpisodeEntity);
        //        return newCustomerEpisodeEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}
        public CustomerEpisodeEntity GetFullCustomerEpisode(int id, bool showOutdatedCoverAgrees)
        {
            try
            {
                DataSet ds = _customerEpisodeDA.GetCustomerEpisode(id, showOutdatedCoverAgrees);
                if ((ds.Tables != null) && ds.Tables.Contains(_tableName) && (ds.Tables[_tableName].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());

                    int customerADTOrderID = ds.Tables[_tableName].Rows[0]["CustomerADTOrderID"] as int? ?? 0;

                    CustomerEpisodeAdvancedAdapter customerEpisodeAdapter = new CustomerEpisodeAdvancedAdapter();
                    CustomerEpisodeEntity[] customerEpisodes = customerEpisodeAdapter.GetData(ds2);
                    CustomerEpisodeEntity myCustomerEpisode = customerEpisodes[0];

                    if (customerADTOrderID > 0)
                    {
                        myCustomerEpisode.ADTOrder = CustomerOrderRequestBL.GetByID(customerADTOrderID);
                    }

                    if (myCustomerEpisode.CustomerAuthorizations != null && myCustomerEpisode.CustomerAuthorizations.Length < 0)
                    {
                        int notAuthorizationTypeID = _authorizationTypeDA.GetAuthorizationTypeNotInvoiceByCustomerEpisode(myCustomerEpisode.ID);
                        if (notAuthorizationTypeID > 0 &&
                            myCustomerEpisode.CustomerAuthorizations.Any(auth => auth.AuthorizationTypeID == notAuthorizationTypeID))
                        {
                            List<CustomerEpisodeAuthorizationEntity> authors = new List<CustomerEpisodeAuthorizationEntity>();
                            authors.AddRange(myCustomerEpisode.CustomerAuthorizations
                                                    .Where(auth => auth.AuthorizationTypeID != notAuthorizationTypeID)
                                                    .ToArray());
                            authors.Add(myCustomerEpisode.CustomerAuthorizations
                                                    .Where(auth => auth.AuthorizationTypeID == notAuthorizationTypeID)
                                                    .FirstOrDefault());
                            myCustomerEpisode.CustomerAuthorizations = authors.ToArray();
                        }
                    }
                    ds2.Dispose();
                    ds.Dispose();
                    LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, id, ActionType.View);
                    return myCustomerEpisode;
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
        /// 
        /// </summary>
        /// <param name="admissionID">id del episodio</param>
        /// <returns>CustomerEpisodeEntity</returns>
        //public CustomerEpisodeEntity GetFullCustomerEpisode_old(int id, bool showOutdatedCoverAgrees)
        //{
        //    try
        //    {
        //        CustomerEpisodeEntity customerEpisode = null;
        //        CustomerEpisodeAdvancedAdapter customerEpisodeAdapter = new CustomerEpisodeAdvancedAdapter();
        //        DataSet ds = _customerEpisodeDA.GetCustomerEpisode(id);
        //        if ((ds.Tables != null)
        //            && (ds.Tables.Contains(_tableName))
        //            && (ds.Tables[_tableName].Rows.Count > 0))
        //        {
        //            DataSet ds2;

        //            #region Current location avail
        //            int currentLocationAvailID = 0;
        //            if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable))
        //                && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0))
        //            {
        //                currentLocationAvailID = ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows[0]["CurrentLocationAvailID"] as int? ?? 0;
        //            }

        //            if (currentLocationAvailID > 0)
        //            {
        //                ds2 = _locationAvailabilityDA.GetLocationAvailabilityByID(currentLocationAvailID);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable].Copy();
        //                    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable;
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region Episode reason types
        //            EpisodeReasonTypeDA _episodeReasonTypeDA = new EpisodeReasonTypeDA();
        //            ds2 = _episodeReasonTypeDA.GetAllEpisodeReasonType();
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region CustomerEpisodeReasonRels
        //            CustomerEpisodeReasonRelDA _customerEpisodeReasonRelDA = new CustomerEpisodeReasonRelDA();
        //            ds2 = _customerEpisodeReasonRelDA.GetCustomerEpisodeReasonRels(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }

        //            CustomerEpisodeLeaveReasonRelDA _customerEpisodeLeaveReasonRelDA = new CustomerEpisodeLeaveReasonRelDA();
        //            ds2 = _customerEpisodeLeaveReasonRelDA.GetCustomerEpisodeLeaveReasonRels(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }

        //            EpisodeReasonDA _episodeReasonDA = new EpisodeReasonDA();
        //            ds2 = _episodeReasonDA.GetEpisodeReasonByCustomerEpisode(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
        //                ds.Tables.Add(dt);
        //            }

        //            ds2 = _episodeReasonDA.GetEpisodeLeaveReasonByCustomerEpisode(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable].Copy();
        //                ds.Tables.Add(dt);
        //            }

        //            ConceptDA _conceptDA = new ConceptDA();
        //            ds2 = _conceptDA.GetConceptsByCustomerEpisode(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptTable].Copy();
        //                ds.Tables.Add(dt);
        //            }

        //            ds2 = _conceptDA.GetConceptsLeaveByCustomerEpisode(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptLeaveTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptLeaveTable].Copy();
        //                ds.Tables.Add(dt);
        //            }


        //            #endregion

        //            #region CustomerEpisodeAuthorizations
        //            //CustomerEpisodeAuthorizationDA _customerEpisodeAuthorizationDA = new CustomerEpisodeAuthorizationDA();
        //            ds2 = _customerEpisodeAuthorizationDA.GetByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable].Copy();
        //                ds.Tables.Add(dt);

        //                ds2 = _authorizationTypeDA.GetAuthorizationTypesByEpisode(id);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable].Copy();
        //                    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable;
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region CustomerEpisodeAuthorizationEntries
        //            //CustomerEpisodeAuthorizationEntryDA _customerEpisodeAuthorizationEntryDA = new CustomerEpisodeAuthorizationEntryDA();
        //            ds2 = _customerEpisodeAuthorizationEntryDA.GetByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region CustomerEpisodeAuthorizationOps
        //            //CustomerEpisodeAuthorizationOpsDA _customerEpisodeAuthorizationOpsDA = new CustomerEpisodeAuthorizationOpsDA();
        //            ds2 = _customerEpisodeAuthorizationOpsDA.GetByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable].Copy();
        //                dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable;
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Insurer Cover Agreements Base
        //            if (showOutdatedCoverAgrees)
        //            {
        //                ds2 = _insurerCoverAgreementDA.GetInsurerCoverAgreements(Int32.MaxValue);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.InsurerCoverAgreementDTOTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.InsurerCoverAgreementDTOTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            else
        //            {
        //                ds2 = _insurerCoverAgreementDA.GetInsurerCoverAgreements(Int32.MaxValue, false);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.InsurerCoverAgreementDTOTable)))
        //                {
        //                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.InsurerCoverAgreementDTOTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }
        //            #endregion

        //            #region Insurer Cover Agreements
        //            ds2 = _customerCoverAgreeRelDA.GetInsurerCoverAgreementsByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCoverAgreeRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCoverAgreeRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Insurer Agreements
        //            ds2 = _customerInsurerAgreeRelDA.GetInsurerAgreementsByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerInsurerAgreeRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerInsurerAgreeRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Care Center Assistance Agreements
        //            ds2 = _customerAssistAgreeRelDA.GetCustomerAssistAgreeRelByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAssistAgreeRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAssistAgreeRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Care Center Agreements
        //            ds2 = _customerAgreeRelDA.GetCustomerAgreeRelByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAgreeRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAgreeRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region contractcoverAgreements
        //            #endregion

        //            #region Customer Episode Service Rel
        //            ds2 = _customerEpisodeServiceRelDA.GetByCustomerEpisodeID(id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Customer Episode Interop Information
        //            ds2 = _customerEpInteropInfoDA.GetByCustomerEpisodeID(id);
        //            if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerEpInteropInfoTable)
        //                && (ds2.Tables[Administrative.Entities.TableNames.CustomerEpInteropInfoTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerEpInteropInfoTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Customer Episode Referenced Physicians
        //            ds2 = _customerEpisodeReferencedPhysicianRelDA.GetCustomerEpisodeReferencedPhysicianRels(id);
        //            if ((ds2 != null)
        //                && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeReferencedPhysicianRelTable))
        //                && (ds2.Tables[Administrative.Entities.TableNames.CustomerEpisodeReferencedPhysicianRelTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerEpisodeReferencedPhysicianRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Customer Policy
        //            ds2 = _customerPolicyDA.GetCustomerPolicyByCustomerEpisodeID(id);
        //            if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerPolicyTable)
        //                && (ds2.Tables[Administrative.Entities.TableNames.CustomerPolicyTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerPolicyTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region Policy Type
        //            ds2 = _policyTypeDA.GetPolicyTypeByCustomerEpisodeID(id);
        //            if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.PolicyTypeTable)
        //                && (ds2.Tables[BackOffice.Entities.TableNames.PolicyTypeTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.PolicyTypeTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region CustomerGuarantee
        //            ds2 = _customerGuaranteeDA.GetByCustomerEpisode(id);

        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerGuaranteeTable))
        //                && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerGuaranteeTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerGuaranteeTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region CustomerEpisodeGuarantor
        //            ds2 = _customerEpisodeGuarantorDA.Get(0, id);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable))
        //               && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable].Rows.Count > 0))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable].Copy();
        //                ds.Tables.Add(dt);
        //                #region Persons
        //                DataSet ds3 = _customerEpisodeGuarantorDA.GetRelatedPersons(0, id);
        //                if ((ds3 != null) && (ds3.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
        //                {
        //                    dt = ds3.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //                #endregion
        //                #region Address
        //                ds3 = _customerEpisodeGuarantorDA.GetRelatedAddress(0, id);
        //                if ((ds3 != null) && (ds3.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
        //                {
        //                    dt = ds3.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //                #endregion
        //            }
        //            #endregion

        //            int customerADTOrderID = ds.Tables[_tableName].Rows[0]["CustomerADTOrderID"] as int? ?? 0;

        //            CustomerEpisodeEntity[] customerEpisodes = customerEpisodeAdapter.GetData(ds);
        //            CustomerEpisodeEntity myCustomerEpisode = customerEpisodes[0];

        //            if (customerADTOrderID > 0)
        //            {
        //                myCustomerEpisode.ADTOrder = CustomerOrderRequestBL.GetByID(customerADTOrderID);
        //            }

        //            if (myCustomerEpisode.CustomerAuthorizations != null && myCustomerEpisode.CustomerAuthorizations.Length < 0)
        //            {
        //                int notAuthorizationTypeID = _authorizationTypeDA.GetAuthorizationTypeNotInvoiceByCustomerEpisode(myCustomerEpisode.ID);
        //                if (notAuthorizationTypeID > 0 &&
        //                    myCustomerEpisode.CustomerAuthorizations.Any(auth => auth.AuthorizationTypeID == notAuthorizationTypeID))
        //                {
        //                    List<CustomerEpisodeAuthorizationEntity> authors = new List<CustomerEpisodeAuthorizationEntity>();
        //                    authors.AddRange(myCustomerEpisode.CustomerAuthorizations
        //                                            .Where(auth => auth.AuthorizationTypeID != notAuthorizationTypeID)
        //                                            .ToArray());
        //                    authors.Add(myCustomerEpisode.CustomerAuthorizations
        //                                            .Where(auth => auth.AuthorizationTypeID == notAuthorizationTypeID)
        //                                            .FirstOrDefault());
        //                    myCustomerEpisode.CustomerAuthorizations = authors.ToArray();
        //                }
        //            }

        //            return myCustomerEpisode;
        //        }
        //        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, id, ActionType.View);
        //        return customerEpisode;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}

        public CustomerEpisodeEntity GetFullCustomerEpisode(string episodeNumber, string processChartName)
        {
            try
            {
                int episodeID = 0;
                if (string.IsNullOrEmpty(processChartName))
                    episodeID = _customerEpisodeDA.GetEpisodeIDByEpisodeNumber(episodeNumber);
                else
                    episodeID = _customerEpisodeDA.GetCustomerEpisodeID(episodeNumber, processChartName);
                if (episodeID > 0)
                {
                    CustomerEpisodeEntity customerEpisode = this.GetFullCustomerEpisode(episodeID);
                    if (customerEpisode != null)
                        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, customerEpisode.ID, ActionType.View);
                    return customerEpisode;
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

        public CustomerEpisodeEntity GetFullCustomerEpisode(string episodeNumber, int processChartID, int careCenterID)
        {
            try
            {
                int episodeID = _customerEpisodeDA.GetCustomerEpisodeIDByPCIDAndCCID(episodeNumber, processChartID, careCenterID);
                if (episodeID > 0)
                {
                    CustomerEpisodeEntity customerEpisode = this.GetFullCustomerEpisode(episodeID);
                    if (customerEpisode != null)
                        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, customerEpisode.ID, ActionType.View);
                    return customerEpisode;
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

        public CustomerEpisodeEntity GetFullCustomerEpisode(int customerID, string episodeNumber, int careCenterID, int status)
        {
            try
            {
                int episodeID = _customerEpisodeDA.GetCustomerEpisodeIDByNumberCCIDANDStatus(customerID, episodeNumber,
                                                careCenterID, status);
                if (episodeID > 0)
                {
                    CustomerEpisodeEntity customerEpisode = this.GetFullCustomerEpisode(episodeID);
                    if (customerEpisode != null)
                        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, customerEpisode.ID, ActionType.View);
                    return customerEpisode;
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

        public CustomerEpisodeEntity GetFullCustomerEpisode(int customerID, string episodeNumber, int status)
        {
            try
            {
                int episodeID = _customerEpisodeDA.GetCustomerEpisodeIDByNumberANDStatus(customerID, episodeNumber, status);
                if (episodeID > 0)
                {
                    CustomerEpisodeEntity customerEpisode = this.GetFullCustomerEpisode(episodeID);
                    if (customerEpisode != null)
                        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, customerEpisode.ID, ActionType.View);
                    return customerEpisode;
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

        public CustomerEpisodeEntity GetFullCustomerEpisode(int customerID, int careCenterID, int status)
        {
            try
            {
                int episodeID = _customerEpisodeDA.GetCustomerEpisodeIDByCCIDANDStatus(customerID, careCenterID, status);
                if (episodeID > 0)
                {
                    CustomerEpisodeEntity customerEpisode = this.GetFullCustomerEpisode(episodeID);
                    if (customerEpisode != null)
                        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, customerEpisode.ID, ActionType.View);
                    return customerEpisode;
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

        public CustomerEpisodeEntity GetCustomerEpisode(int customerID, string episodeNumber, int careCenterID, string careCenterName)
        {
            try
            {
                if (customerID <= 0 || string.IsNullOrEmpty(episodeNumber) || (careCenterID <= 0 && string.IsNullOrEmpty(careCenterName)))
                    return null;
                int episodeID = _customerEpisodeDA.GetEpisodeIDByEpisodeNumber(customerID, episodeNumber, careCenterID, careCenterName);
                if (episodeID > 0)
                {
                    CustomerEpisodeEntity customerEpisode = this.GetFullCustomerEpisode(episodeID);
                    if (customerEpisode != null)
                        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, customerEpisode.ID, ActionType.View);
                    return customerEpisode;
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

        public CustomerEpisodeEntity GetCustomerEpisode(int customerID, string episodeNumber, int episodeCase, int careCenterID, string careCenterName = null)
        {
            try
            {
                if (customerID <= 0 || string.IsNullOrEmpty(episodeNumber) || (careCenterID <= 0 && string.IsNullOrEmpty(careCenterName)))
                    return null;
                int episodeID = 0;
                //if (episodeCase <= 0)
                episodeID = _customerEpisodeDA.GetEpisodeIDByEpisodeNumber(customerID, episodeNumber, episodeCase, careCenterID, careCenterName);
                if (episodeID <= 0)
                    episodeID = _customerEpisodeDA.GetEpisodeIDByEpisodeNumber(customerID, episodeNumber, careCenterID, careCenterName);
                if (episodeID > 0)
                {
                    CustomerEpisodeEntity customerEpisode = this.GetFullCustomerEpisode(episodeID);
                    if (customerEpisode != null)
                        LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, customerEpisode.ID, ActionType.View);
                    return customerEpisode;
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                                                       //
        //     ESTOS DOS MÉTODOS DEBERÁN SER SERIAMENTE REVISADOS. EL PRIMERO DEBERÍA DEVOLVER BASE Y EL SEGUN NO DEBERÍA HACER  //
        //                                               EL BUCLE QUE ESTÉ HACIEND                                               //
        //                                                                                                                       //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public CustomerEpisodeEntity[] GetAllCustomerEpisodes(int customerID)
        {
            try
            {

                CustomerEpisodeAdvancedAdapter customerEpisodeAdapter = new CustomerEpisodeAdvancedAdapter();

                DataSet ds = _customerEpisodeDA.GetAllCustomerEpisodes(customerID);

                if ((ds.Tables != null) && (ds.Tables.Contains(_tableName))
                   && (ds.Tables[_tableName].Rows.Count > 0))
                {
                    //#region Current location avail
                    //int currentLocationAvailID = 0;
                    //if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable))
                    //    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0))
                    //{
                    //    currentLocationAvailID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows[0]["CurrentLocationAvailID"].ToString(), 0);
                    //}

                    //DataSet ds2 = _locationAvailabilityDA.GetLocationAvailabilityByID(currentLocationAvailID);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable].Copy();
                    //    dt.TableName = SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable;
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Episode reason types
                    //EpisodeReasonTypeDA _episodeReasonTypeDA = new EpisodeReasonTypeDA();
                    //ds2 = _episodeReasonTypeDA.GetAllEpisodeReasonType();
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region CustomerEpisodeReasonRels
                    //CustomerEpisodeReasonRelDA _customerEpisodeReasonRelDA = new CustomerEpisodeReasonRelDA();
                    //ds2 = _customerEpisodeReasonRelDA.GetCustomerEpisodeReasonRels(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}

                    //CustomerEpisodeLeaveReasonRelDA _customerEpisodeLeaveReasonRelDA = new CustomerEpisodeLeaveReasonRelDA();
                    //ds2 = _customerEpisodeLeaveReasonRelDA.GetCustomerEpisodeLeaveReasonRels(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}


                    //#endregion

                    //#region CustomerEpisodeAuthorizations
                    //CustomerEpisodeAuthorizationDA _customerEpisodeAuthorizationDA = new CustomerEpisodeAuthorizationDA();
                    //ds2 = _customerEpisodeAuthorizationDA.GetByCustomerEpisodeID(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region CustomerEpisodeAuthorizationEntries
                    //CustomerEpisodeAuthorizationEntryDA _customerEpisodeAuthorizationEntryDA = new CustomerEpisodeAuthorizationEntryDA();
                    //ds2 = _customerEpisodeAuthorizationEntryDA.GetByCustomerEpisodeID(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region CustomerEpisodeAuthorizationOps
                    //CustomerEpisodeAuthorizationOpsDA _customerEpisodeAuthorizationOpsDA = new CustomerEpisodeAuthorizationOpsDA();
                    //ds2 = _customerEpisodeAuthorizationOpsDA.GetByCustomerEpisodeID(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable].Copy();
                    //    dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable;
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Insurer Cover Agreements Base
                    //ds2 = _insurerCoverAgreementDA.GetInsurerCoverAgreements(Int32.MaxValue);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.InsurerCoverAgreementDTOTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.InsurerCoverAgreementDTOTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Insurer Cover Agreements
                    //ds2 = _customerCoverAgreeRelDA.GetInsurerCoverAgreementsByCustomerEpisodeID(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCoverAgreeRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCoverAgreeRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Insurer Agreements Base
                    //ds2 = _insurerAgreementDA.GetInsurerAgreementsBase();
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.InsurerAgreementBaseDTOTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.InsurerAgreementBaseDTOTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Insurer Agreements
                    //ds2 = _customerInsurerAgreeRelDA.GetInsurerAgreementsByCustomerEpisodeID(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerInsurerAgreeRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerInsurerAgreeRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Care Center Assistance Agreement Bases
                    //ds2 = _assistanceAgreementDA.GetAssistanceAgreementsBase();
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AssistanceAgreementBaseDTOTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AssistanceAgreementBaseDTOTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Care Center Assistance Agreements
                    //ds2 = _customerAssistAgreeRelDA.GetCustomerAssistAgreeRelByCustomerEpisodeID(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAssistAgreeRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAssistAgreeRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Care Center Agreements
                    //ds2 = _customerAgreeRelDA.GetCustomerAgreeRelByCustomerEpisodeID(id);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAgreeRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAgreeRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    //#region Customer Episode Service Rel
                    //ds2 = _customerEpisodeServiceRelDA.GetByCustomerEpisodeID(id, (long)currentProcess);
                    //if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
                    //{
                    //    DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    //#endregion

                    LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, 0, ActionType.View);

                    return customerEpisodeAdapter.GetData(ds);
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

        public CustomerEpisodeEntity[] GetAllCustomerEpisodes(int customerID, bool onlyActivesOrLastIfNoActivesFound)
        {
            try
            {
                CustomerEpisodeEntity[] customerEpisodes = null;
                List<CustomerEpisodeEntity> myCustomerEpisodes = new List<CustomerEpisodeEntity>();
                CustomerEpisodeAdvancedAdapter customerEpisodeAdapter = new CustomerEpisodeAdvancedAdapter();

                DataSet ds = _customerEpisodeDA.GetAllCustomerEpisodes(customerID);

                if ((ds.Tables != null) && (ds.Tables.Contains(_tableName))
                   && (ds.Tables[_tableName].Rows.Count > 0))
                {
                    LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, 0, ActionType.View);

                    customerEpisodes = customerEpisodeAdapter.GetData(ds);

                    if (onlyActivesOrLastIfNoActivesFound)
                    {
                        if ((customerEpisodes != null) && (customerEpisodes.Length > 0))
                        {
                            for (int i = customerEpisodes.Length - 1; i >= 0; i--)
                            {
                                if (customerEpisodes[i].Status == CommonEntities.StatusEnum.Active)
                                {
                                    customerEpisodes[i] = this.GetFullCustomerEpisode(customerEpisodes[i].ID);
                                    myCustomerEpisodes.Add(customerEpisodes[i]);
                                }
                            }

                            if (myCustomerEpisodes.Count == 0)
                            {
                                Array.Sort(customerEpisodes, delegate(CustomerEpisodeEntity ce1, CustomerEpisodeEntity ce2) { return ce1.StartDateTime.CompareTo(ce2.StartDateTime); });
                                myCustomerEpisodes.Add(customerEpisodes[customerEpisodes.Length - 1]);
                            }
                        }
                    }
                    else
                    {
                        if ((customerEpisodes != null) && (customerEpisodes.Length > 0))
                        {
                            for (int i = customerEpisodes.Length - 1; i >= 0; i--)
                            {
                                customerEpisodes[i] = this.GetFullCustomerEpisode(customerEpisodes[i].ID);
                                myCustomerEpisodes.Add(customerEpisodes[i]);
                            }
                        }
                    }

                    if (myCustomerEpisodes.Count > 0)
                    {
                        return myCustomerEpisodes.ToArray();
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

        public CustomerEpisodeEntity GetLastCustomerEpisode(int admissionID, int processChartID)
        {
            try
            {
                DataSet ds = _customerEpisodeDA.GetLastCustomerEpisode(admissionID, processChartID);
                int customerEpisodeID = 0;
                customerEpisodeID = SIIConvert.ToInteger(ds.Tables[_tableName].Rows[0]["ID"].ToString(), 0);

                return (customerEpisodeID > 0) ? GetFullCustomerEpisode(customerEpisodeID) : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public bool HasCustomerEpisodes(int customerID, CommonEntities.StatusEnum status)
        {
            try
            {
                return _customerEpisodeDA.HasCustomerEpisodes(customerID, status);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        /// <summary>
        /// esto son episodios previos de un cliente sin importar la relacio´n entre episodios
        /// </summary>
        public CustomerRelatedEpisodeInfoDTO[] GetCustomerRelatedEpisodeInfoDTOs(int customerID, DateTime leaveDateTime, CommonEntities.StatusEnum status)
        {
            try
            {
                DataSet ds = _customerEpisodeDA.GetCustomerPreviousEpisodeByCustomerID(customerID, leaveDateTime, (int)status);

                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable))
                   && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable].Rows.Count > 0))
                {
                    CustomerRelatedEpisodeInfoDTOAdvancedAdapter CustomerRelatedEpisodeInfoDTOAdapter = new CustomerRelatedEpisodeInfoDTOAdvancedAdapter();
                    return CustomerRelatedEpisodeInfoDTOAdapter.GetData(ds);
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
        /// esto son episodios Activos de un cliente
        /// </summary>
        public bool TieneEpisodiosActivos(int customerID)
        {
            try
            {
                DataSet ds = _customerEpisodeDA.GetCustomerEpisodesByCustomerID(customerID);

                if ((ds.Tables != null))
                {
                    //CustomerRelatedEpisodeInfoDTOAdvancedAdapter CustomerRelatedEpisodeInfoDTOAdapter = new CustomerRelatedEpisodeInfoDTOAdvancedAdapter();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }


        /// <summary>
        /// esto son todos los episodios un cliente sin importar la relación entre episodios
        /// </summary>
        public CustomerRelatedEpisodeInfoDTO[] GetCustomerRelatedEpisodeInfoDTOsByCustomerID(int customerID)
        {
            try
            {
                DataSet ds = _customerEpisodeDA.GetRelatedEpisodesByCustomerID(customerID);
                if (ds != null && ds.Tables != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeBasicTable)
                    && ds.Tables[Administrative.Entities.TableNames.CustomerEpisodeBasicTable].Rows.Count > 0)
                {
                    int[] episodes = (from row in ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable].AsEnumerable()
                                      where (row["ID"] as int? ?? 0) > 0
                                      select (row["ID"] as int? ?? 0)).ToArray();
                    DataSet ds1 = _customerEpisodeDA.GetCustomerEpisodeSimpleBase(episodes);
                    if (ds1 != null
                        && ds1.Tables != null
                        && ds1.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable)
                        && ds1.Tables[Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable;
                        ds.Tables.Add(dt);
                    }
                    ds1 = _customerEpisodeServiceRelDA.GetAssistanceServiceByCustomerEpisodeIDs(episodes);
                    if (ds1 != null
                        && ds1.Tables != null
                        && ds1.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)
                        && ds1.Tables[Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable;
                        ds.Tables.Add(dt);
                    }
                    ds1 = _customerTransferEntryDA.GetAssistanceServiceByCustomerEpisodeIDs(episodes);
                    if (ds1 != null
                        && ds1.Tables != null
                        && ds1.Tables.Contains(Administrative.Entities.TableNames.CustomerTransferEntryTable)
                        && ds1.Tables[Administrative.Entities.TableNames.CustomerTransferEntryTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTransferEntryTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerTransferEntryTable;
                        ds.Tables.Add(dt);
                    }
                    CustomerRelatedEpisodeInfoDTOAdvancedAdapter CustomerRelatedEpisodeInfoDTOAdapter = new CustomerRelatedEpisodeInfoDTOAdvancedAdapter();
                    CustomerRelatedEpisodeInfoDTO[] myData = CustomerRelatedEpisodeInfoDTOAdapter.GetData(ds);
                    if (myData != null)
                    {
                        foreach (CustomerRelatedEpisodeInfoDTO item in myData)
                        {
                            if ((item.RelatedTransferEntries != null) && (item.RelatedTransferEntries.Length > 0))
                            {
                                _physicianCache.PhysicianCache.UpdateCache();
                                foreach (CustomerTransferEntryEntity te in item.RelatedTransferEntries)
                                {
                                    if (te.SourcePhysicianID > 0)
                                    {
                                        PhysicianEntity myPhysician = _physicianCache.PhysicianCache.Get(te.SourcePhysicianID, false);
                                        if (myPhysician != null)
                                            te.SourcePhysicianName = CommonEntities.DescriptionBuilder.PersonBuildName(myPhysician.Person.FirstName, myPhysician.Person.LastName, myPhysician.Person.LastName2);
                                    }
                                    if (te.TargetPhysicianID > 0)
                                    {
                                        PhysicianEntity myPhysician = _physicianCache.PhysicianCache.Get(te.TargetPhysicianID, false);
                                        if (myPhysician != null)
                                            te.TargetPhysicianName = CommonEntities.DescriptionBuilder.PersonBuildName(myPhysician.Person.FirstName, myPhysician.Person.LastName, myPhysician.Person.LastName2);
                                    }
                                }
                            }
                        }
                    }
                    return myData;
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
        /// esto son episodios previo y post concatenados con un episodio de cliente
        /// </summary>
        public CustomerRelatedEpisodeInfoDTO[] GetRelatedEpisodesByCustomerEpisodeID(int customerEpisodeID, bool includeThis, bool includeRestInfo)
        {
            try
            {
                if (customerEpisodeID <= 0)
                    return null;
                int[] episodes = _customerEpisodeDA.GetRelatedEpisodesByCustomerEpisodeID(customerEpisodeID, includeThis);
                if (episodes == null || episodes.Length <= 0) return null;

                DataSet ds = _customerEpisodeDA.GetRelatedEpisodesByListOfEpisodeID(episodes);
                if (ds != null && ds.Tables != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeBasicTable)
                    && ds.Tables[Administrative.Entities.TableNames.CustomerEpisodeBasicTable].Rows.Count > 0)
                {
                    DataSet ds1 = _customerEpisodeDA.GetCustomerEpisodeSimpleBase(episodes);
                    if (ds1 != null && ds1.Tables != null && ds1.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable)
                        && ds1.Tables[Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable;
                        ds.Tables.Add(dt);
                    }
                    ds1 = _customerEpisodeServiceRelDA.GetAssistanceServiceByCustomerEpisodeIDs(episodes);
                    if (ds1 != null && ds1.Tables != null && ds1.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)
                        && ds1.Tables[Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable;
                        ds.Tables.Add(dt);
                    }
                    ds1 = _customerTransferEntryDA.GetAssistanceServiceByCustomerEpisodeIDs(episodes);
                    if (ds1 != null && ds1.Tables != null && ds1.Tables.Contains(Administrative.Entities.TableNames.CustomerTransferEntryTable)
                        && ds1.Tables[Administrative.Entities.TableNames.CustomerTransferEntryTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTransferEntryTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerTransferEntryTable;
                        ds.Tables.Add(dt);
                    }
                    CustomerRelatedEpisodeInfoDTOAdvancedAdapter CustomerRelatedEpisodeInfoDTOAdapter = new CustomerRelatedEpisodeInfoDTOAdvancedAdapter();
                    CustomerRelatedEpisodeInfoDTO[] myData = CustomerRelatedEpisodeInfoDTOAdapter.GetData(ds);
                    if (myData != null)
                    {
                        foreach (CustomerRelatedEpisodeInfoDTO item in myData)
                        {
                            if ((item.RelatedTransferEntries != null) && (item.RelatedTransferEntries.Length > 0))
                            {
                                _physicianCache.PhysicianCache.UpdateCache();
                                foreach (CustomerTransferEntryEntity te in item.RelatedTransferEntries)
                                {
                                    if (te.SourcePhysicianID > 0)
                                    {
                                        PhysicianEntity myPhysician = _physicianCache.PhysicianCache.Get(te.SourcePhysicianID, false);
                                        if (myPhysician != null)
                                            te.SourcePhysicianName = myPhysician.Person.FullName;
                                    }
                                    if (te.TargetPhysicianID > 0)
                                    {
                                        PhysicianEntity myPhysician = _physicianCache.PhysicianCache.Get(te.TargetPhysicianID, false);
                                        if (myPhysician != null)
                                            te.TargetPhysicianName = myPhysician.Person.FullName;
                                    }
                                }
                            }
                        }
                    }
                    return myData;
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEpisodeAuthorizationEntity[] GetCustomerEpisodeAuthorizations(int customerEpisodeID)
        {
            try
            {
                CustomerEpisodeAuthorizationEntity[] customerEpisodeAuthorizations = null;
                CustomerEpisodeAuthorizationAdvancedAdapter CustomerEpisodeAuthorizationAdvancedAdapter = new CustomerEpisodeAuthorizationAdvancedAdapter();

                DataSet ds = _customerEpisodeAuthorizationDA.GetByCustomerEpisodeID(customerEpisodeID);
                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable))
                   && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable].Rows.Count > 0))
                {

                    #region Authorization Type
                    //int authorizationTypeID = 0;
                    //if ((ds != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationDTOTable))
                    //    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationDTOTable].Rows.Count > 0))
                    //{
                    //    authorizationTypeID = SIIConvert.ToInteger(ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationDTOTable].Rows[0]["AuthorizationTypeID"].ToString(), 0);
                    //}

                    DataSet ds2 = _authorizationTypeDA.GetAuthorizationTypes();//_authorizationTypeDA.GetAuthorizationType(authorizationTypeID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable].Copy();
                        dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region CustomerEpisodeAuthorizationEntries
                    //CustomerEpisodeAuthorizationEntryDA _customerEpisodeAuthorizationEntryDA = new CustomerEpisodeAuthorizationEntryDA();
                    ds2 = _customerEpisodeAuthorizationEntryDA.GetByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region CustomerEpisodeAuthorizationOps
                    //CustomerEpisodeAuthorizationOpsDA _customerEpisodeAuthorizationOpsDA = new CustomerEpisodeAuthorizationOpsDA();
                    ds2 = _customerEpisodeAuthorizationOpsDA.GetByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    customerEpisodeAuthorizations = CustomerEpisodeAuthorizationAdvancedAdapter.GetData(ds);

                    if (customerEpisodeAuthorizations != null && customerEpisodeAuthorizations.Length < 0)
                    {
                        int notAuthorizationTypeID = _authorizationTypeDA.GetAuthorizationTypeNotInvoiceByCustomerEpisode(customerEpisodeID);
                        if (notAuthorizationTypeID > 0 &&
                            customerEpisodeAuthorizations.Any(auth => auth.AuthorizationTypeID == notAuthorizationTypeID))
                        {
                            List<CustomerEpisodeAuthorizationEntity> authors = new List<CustomerEpisodeAuthorizationEntity>();
                            authors.AddRange(customerEpisodeAuthorizations
                                                    .Where(auth => auth.AuthorizationTypeID != notAuthorizationTypeID)
                                                    .ToArray());
                            authors.Add(customerEpisodeAuthorizations
                                                    .Where(auth => auth.AuthorizationTypeID == notAuthorizationTypeID)
                                                    .FirstOrDefault());
                            customerEpisodeAuthorizations = authors.ToArray();
                        }
                    }
                }
                return customerEpisodeAuthorizations;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEpisodeEntity GetCurrentCustomerEpisodeByCustomerID(int customerID)
        {
            try
            {
                int customerEpisodeID = _customerDA.GetCurrentEpisodeID(customerID);
                return GetFullCustomerEpisode(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public bool ExistCustomerEpisodeActive(int customerID, int processChartID, int careCenterID)
        {
            try
            {
                return _customerEpisodeDA.ExistsCustomerEpisodeActive(customerID, processChartID, careCenterID, (int)CommonEntities.StatusEnum.Active);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ExistsCustomerEpisodeByStatuses(string episodeNumber, CommonEntities.StatusEnum[] statuses, int customerID, int processChartID)
        {
            try
            {
                return _customerEpisodeDA.ExistsCustomerEpisodeByStatuses(episodeNumber, statuses, customerID, processChartID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ExistNextCustomerEpisode(int customerEpisodeID)
        {
            try
            {
                return _customerEpisodeDA.ExistNextCustomerEpisode(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }



        public CustomerBaseEpisodeWithChargesDTO[] GetCustomerBaseEpisodeWithChargesDTOByListCustomerEpisodeID(int[] customerEpisodeIDs, bool auditAllowed)
        {
            if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;

            try
            {
                CommonEntities.ElementEntity agreeConditionElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.AgreeConditionEntityName, true);
                CommonEntities.ElementEntity insurerConditionElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.InsurerConditionEntityName, false);

                string result = this.GetListCustomerEpisodeIDsForSQL(this.GetDistinctCustomerEpisodeIDs(customerEpisodeIDs));

                DataSet ds = _customerDA.GetCustomerBaseEpisodeWithChargesDTOByStringListCustomerEpisodesID(result);
                if ((ds != null)
                    && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerBaseEpisodeWithChargesDTOTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerBaseEpisodeWithChargesDTOTable].Rows.Count > 0))
                {
                    DataSet ds2;

                    #region Customer Episode with Charges DTO
                    ds2 = _customerEpisodeDA.GetCustomerEpisodeWithChargesDTOByStringListCustomerEpisodesID(result);
                    if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeWithChargesDTOTable)
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerEpisodeWithChargesDTOTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerEpisodeWithChargesDTOTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region Customer Account Charges
                    ds2 = _customerAccountChargeDA.GetCustomerAccountChargeByStringListCustomerEpisodeID(result, (agreeConditionElement != null) ? agreeConditionElement.ID : 0,
                        (insurerConditionElement != null) ? insurerConditionElement.ID : 0,
                        auditAllowed);
                    if ((ds2 != null)
                        && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerAccountChargeTable)
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerAccountChargeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerAccountChargeTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    CustomerBaseEpisodeWithChargesDTOAdvancedAdapter adapter = new CustomerBaseEpisodeWithChargesDTOAdvancedAdapter();
                    return SuppressDuplicatedByCustomerEpisode(adapter.GetData(ds));
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerBaseEpisodeWithChargesDTO[] GetCustomerBaseEpisodeWithChargesDTOByListCustomerEpisodeIDAndRelatedEpisodes(int[] customerIDs, int[] customerEpisodeIDs, bool auditAllowed)
        {
            if ((customerIDs == null) || (customerIDs.Length <= 0) || (customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;

            int[] episodeIDs = this.GetRelatedEpisodeIDsByCustomerIDsEpisodeIDs(customerIDs, customerEpisodeIDs);

            if ((episodeIDs != null) && (episodeIDs.Length > 0))
            {
                return this.GetCustomerBaseEpisodeWithChargesDTOByListCustomerEpisodeID(episodeIDs, auditAllowed);
            }
            else return null;
        }

        public CustomerEpisodeWithChargesDTO[] GetCustomerEpisodeWithChargesDTOByCustomerEpisodeID(int customerEpisodeID, bool auditAllowed)
        {
            if (customerEpisodeID <= 0)
                return null;

            return GetCustomerEpisodeWithChargesDTOByCustomerEpisodeIDs(new int[] { customerEpisodeID }, auditAllowed);
        }

        public CustomerEpisodeWithChargesDTO[] GetCustomerEpisodeWithChargesDTOByListCustomerEpisodeIDAndRelatedEpisodes(int customerID, int customerEpisodeID, bool auditAllowed)
        {
            if ((customerID <= 0) || (customerEpisodeID <= 0))
                return null;

            int[] episodeIDs = this.GetRelatedEpisodeIDsByCustomerIDsEpisodeIDs(new int[] { customerID }, new int[] { customerEpisodeID });

            if ((episodeIDs != null) && (episodeIDs.Length > 0))
            {
                return GetCustomerEpisodeWithChargesDTOByCustomerEpisodeIDs(episodeIDs, auditAllowed);
            }
            else return null;
        }

        public CustomerEpisodeWithChargesDTO GetCustomerEpisodesWithChargesByEpisodeID(int episodeID, bool auditAllowed)
        {
            if (episodeID <= 0)
                return null;

            CustomerEpisodeWithChargesDTO[] result = GetCustomerEpisodeWithChargesDTOByCustomerEpisodeIDs(new int[] { episodeID }, auditAllowed);

            return ((result != null) && (result.Length > 0)) ? result[0] : null;
        }

        public AddinAttribute[] GetCustomerEpisodeAttributes(int customerEpisodeID)
        {
            try
            {
                AddinAttributeAdapter adapter = new AddinAttributeAdapter();

                DataSet ds = _customerEpisodeDA.GetCustomerEpisodeAttributes(customerEpisodeID);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.Common.Entities.TableNames.EACAttributeTable))
                    && (ds.Tables[SII.HCD.Common.Entities.TableNames.EACAttributeTable].Rows.Count > 0))
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

        public bool ValidateAnyEpisodeInHIS(CustomerEpisodeEntity customerEpisode)
        {
            //addins de intercambio
            return false;
        }

        public CustomerEpisodeDTO[] GetCustomerEpisodeDTOByCustomerEpisodeIDsAndRelatedEpisodes(int[] customerIDs, int[] customerEpisodeIDs)
        {
            if ((customerIDs == null) || (customerIDs.Length <= 0) || (customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;

            try
            {
                int[] episodeIDs = this.GetRelatedEpisodeIDsByCustomerIDsEpisodeIDs(customerIDs, customerEpisodeIDs);

                DataSet ds = _customerEpisodeDA.GetCustomerEpisodeDTOByCustomerEpisodesIDs(episodeIDs);
                if ((ds != null)
                    && (ds.Tables != null)
                    && ds.Tables.Contains(AdministrativeEntities.TableNames.CustomerEpisodeTable)
                    && (ds.Tables[AdministrativeEntities.TableNames.CustomerEpisodeTable].Rows.Count > 0))
                {
                    CustomerEpisodeDTOAdvancedAdapter adapter = new CustomerEpisodeDTOAdvancedAdapter();
                    CustomerEpisodeDTO[] result = adapter.GetData(ds);

                    result = SuppressDuplicatedByCustomerEpisode(result);

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

        //DO ALBERTO
        //ESTOS MÉTODOS DEVUELVEN UNA LISTA DE [ID] DE LOS EPISODIOS QUE TENGAN UNA POLIZA DE LOS TIPOS SELECCIONADOS
        //public int[] GetEpisodesByPolicyTypes(AddinElementDTO[] entities, int[] episodeIDs)
        public int[] GetEpisodesByPolicyTypes(int[] entities, int[] episodeIDs)
        {
            try
            {
                if ((entities == null) || (entities.Length <= 0) || (episodeIDs == null) || (episodeIDs.Length <= 0))
                    return null;

                //string episodes = this.GetEpisodeIDsStringWhereIn(episodeIDs);
                //string entityIDs = this.GetEntityIDsStringWhereIn(entities);

                string episodes = this.GetStringListIDForQuerySQL(episodeIDs);
                string entityIDs = this.GetStringListIDForQuerySQL(entities);

                DataSet ds = _customerEpisodeDA.GetEpisodesByPolicyTypes(entityIDs, episodes);

                List<int> result = GetEpisodeIDsFromDataset(ds);
                return (result.Count > 0) ? result.ToArray() : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        //PARA QUE SE UTILICE EN LA VISTA DE ADMISION PARA VALIDAD POSIBLE BORRADO DE ACUERDOS ASOCIADOS AL CUSTOMEREPISODE
        public bool ValidateDeleteHistoryAssistanceAgreementFromEpisode(int customerEpisodeID, int historyAssistanceAgreementID)
        {
            try
            {
                if (customerEpisodeID <= 0)
                    throw new ArgumentNullException("customerEpisodeID");
                if (historyAssistanceAgreementID <= 0)
                    throw new ArgumentNullException("historyAssistanceAgreementID");

                CommonEntities.ElementEntity element = ElementBL.GetElementByName(SII.HCD.Common.Entities.Constants.EntityNames.AssistanceAgreementEntityName, true);
                if (element == null)
                    return true;

                return _customerAssistAgreeRelDA.ValidateDeleteHistoryAssistanceAgreementFromEpisode(customerEpisodeID, historyAssistanceAgreementID, element.ID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ValidateDeleteHistoryAgreementFromEpisode(int customerEpisodeID, int historyAgreementID)
        {
            try
            {
                if (customerEpisodeID <= 0)
                    throw new ArgumentNullException("customerEpisodeID");
                if (historyAgreementID <= 0)
                    throw new ArgumentNullException("historyAgreementID");

                CommonEntities.ElementEntity element = ElementBL.GetElementByName(SII.HCD.Common.Entities.Constants.EntityNames.AgreementEntityName, true);
                if (element == null) return true;
                return _customerAgreeRelDA.ValidateDeleteHistoryAgreementFromEpisode(customerEpisodeID, historyAgreementID, element.ID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ValidateDeleteHistoryInsureCoverAgreementFromEpisode(int customerEpisodeID, int historyInsurerCoverAgreementID)
        {
            try
            {
                if (customerEpisodeID <= 0)
                    throw new ArgumentNullException("customerEpisodeID");
                if (historyInsurerCoverAgreementID <= 0)
                    throw new ArgumentNullException("historyInsurerCoverAgreementID");

                CommonEntities.ElementEntity element = ElementBL.GetElementByName(SII.HCD.Common.Entities.Constants.EntityNames.InsurerCoverAgreementEntityName, true);
                if (element == null)
                    return true;

                return _customerCoverAgreeRelDA.ValidateDeleteHistoryInsurerCoverAgreementFromEpisode(customerEpisodeID, historyInsurerCoverAgreementID, element.ID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ValidateDeleteHistoryInsurerAgreementFromEpisode(int customerEpisodeID, int historyInsurerAgreementID)
        {
            try
            {
                if (customerEpisodeID <= 0)
                    throw new ArgumentNullException("customerEpisodeID");
                if (historyInsurerAgreementID <= 0)
                    throw new ArgumentNullException("historyInsurerAgreementID");

                CommonEntities.ElementEntity element = ElementBL.GetElementByName(SII.HCD.Common.Entities.Constants.EntityNames.InsurerAgreementEntityName, true);
                if (element == null)
                    return true;

                return _customerInsurerAgreeRelDA.ValidateDeleteHistoryInsurerAgreementFromEpisode(customerEpisodeID, historyInsurerAgreementID, element.ID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public string ValidateDeleteHistoryXAgreementsFromEpisode(int customerEpisodeID,
            int[] historyAssistanceAgreementIDs, int[] historyAgreementIDs,
            int[] historyInsurerCoverAgreementIDs, int[] historyInsurerAgreementIDs)
        {
            try
            {
                if (customerEpisodeID <= 0)
                    throw new ArgumentNullException("customerEpisodeID");

                string message = string.Empty;

                bool validateAssistanceAgreements = false;
                CommonEntities.ElementEntity aaElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.AssistanceAgreementEntityName, true);
                if (aaElement != null
                    && historyAssistanceAgreementIDs != null
                    && historyAssistanceAgreementIDs.Length > 0)
                {
                    validateAssistanceAgreements = _customerAssistAgreeRelDA.ValidateDeleteHistoryAssistanceAgreementsFromEpisode(customerEpisodeID, historyAssistanceAgreementIDs, aaElement.ID);
                    if (!validateAssistanceAgreements)
                        message = string.Concat(message, Environment.NewLine, Properties.Resources.MSG_ExistsDeliveryNotesRelatedToAssistanceAgreements);
                }
                bool validateAgreements = false;
                CommonEntities.ElementEntity aElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.AgreementEntityName, true);
                if (aElement != null
                    && historyAgreementIDs != null
                    && historyAgreementIDs.Length > 0)
                {
                    validateAgreements = _customerAgreeRelDA.ValidateDeleteHistoryAgreementsFromEpisode(customerEpisodeID, historyAgreementIDs, aElement.ID);
                    if (!validateAgreements)
                        message = string.Concat(message, Environment.NewLine, Properties.Resources.MSG_ExistsDeliveryNotesRelatedToAgreements);
                }
                bool validateInsurerCoverAgreements = false;
                CommonEntities.ElementEntity icaElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.InsurerCoverAgreementEntityName, true);
                if (icaElement != null
                    && historyInsurerCoverAgreementIDs != null
                    && historyInsurerCoverAgreementIDs.Length > 0)
                {
                    validateInsurerCoverAgreements = _customerCoverAgreeRelDA.ValidateDeleteHistoryInsurerCoverAgreementsFromEpisode(customerEpisodeID, historyInsurerCoverAgreementIDs, icaElement.ID);
                    if (!validateInsurerCoverAgreements)
                        message = string.Concat(message, Environment.NewLine, Properties.Resources.MSG_ExistsDeliveryNotesRelatedToInsurerCoverAgreements);
                }
                bool validateInsurerAgreements = false;
                CommonEntities.ElementEntity iaElement = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.InsurerAgreementEntityName, true);
                if (iaElement != null
                    && historyInsurerAgreementIDs != null
                    && historyInsurerAgreementIDs.Length > 0)
                {
                    validateInsurerAgreements = _customerInsurerAgreeRelDA.ValidateDeleteHistoryInsurerAgreementsFromEpisode(customerEpisodeID, historyInsurerAgreementIDs, iaElement.ID);
                    if (!validateInsurerAgreements)
                        message = string.Concat(message, Environment.NewLine, Properties.Resources.MSG_ExistsDeliveryNotesRelatedToInsurerAgreements);
                }
                return message;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return ex.Message;
            }
        }

        public CustomerEpisodeStatistic[] GetCustomerEpisodeStatistics(DateTime startDateTime, DateTime endDateTime, int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber != 0)
                    throw new ArgumentOutOfRangeException("pageNumber");

                if (pageSize != 0)
                    throw new ArgumentOutOfRangeException("pageSize");

                if (startDateTime >= endDateTime)
                    throw new ArgumentException("startDateTime");

                Dictionary<int, CustomerEpisodeStatistic> result = new Dictionary<int, CustomerEpisodeStatistic>();

                DataSet episodes = _customerEpisodeDA.GetCustomerEpisodeInfoByDateRange(startDateTime, endDateTime);
                ProcessEpisodes(episodes, result);

                DataSet reasons = _customerEpisodeReasonRelDA.GetCustomerEpisodeReasonRelsByDateRange(startDateTime, endDateTime);
                ProcessReasons(reasons, result);

                DataSet stats = _customerEpisodeDA.GetEpisodeStatsByEpisodeAndRoutineTypeAndStatus(startDateTime, endDateTime);
                ProcessEpisodeStats(stats, result);

                DataSet programmings = _customerRoutineDA.GetCustomerRoutineProgrammingByDateTimeRange(startDateTime, endDateTime);
                ProcessEpisodeProgrammings(programmings, startDateTime, endDateTime, result);

                return result.Values.ToArray();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RoutineTypeStatusSummary[] GetCustomerEpisodeSummaries(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                if (startDateTime >= endDateTime)
                    throw new ArgumentException("startDateTime");

                Dictionary<string, RoutineTypeStatusSummary> result = new Dictionary<string, RoutineTypeStatusSummary>();

                //Cargamos las summary
                DataSet stats = _customerEpisodeDA.GetEpisodeStatsByRoutineTypeAndStatus(startDateTime, endDateTime);
                ProcessStats(stats, result);

                DataSet programmings = _customerRoutineDA.GetCustomerRoutineProgrammingByDateTimeRange(startDateTime, endDateTime);
                ProcessProgrammings(programmings, startDateTime, endDateTime, result);

                return result.Values.ToArray();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public bool GetPostRelatedEpisodes(int customerEpisodeID)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;
                return _customerEpisodeDA.GetPostRelatedEpisodes(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool GetEpisodeIsInvoiced(int customerEpisodeID)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;
                return _customerEpisodeDA.GetEpisodeIsInvoiced(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool GetEpisodeIsCodified(int customerEpisodeID)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;
                return _customerEpisodeDA.GetEpisodeIsCodified(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public int ObtenerPredecessorID(int customerEpisodeID)
        {
            try
            {
                if (customerEpisodeID <= 0) return 0;
                return _customerEpisodeDA.ObtenerPredecessorID(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public int ObtenerEpisodioDestinoID(int PredecessorID)
        {
            try
            {
                if (PredecessorID <= 0) return 0;
                return _customerEpisodeDA.ObtenerEpisodioDestinoID(PredecessorID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public bool ActualizaFechaFinEpisodio(int customerEpisodeID, DateTime fechaFinEpisodio, string userName)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;
                return _customerEpisodeDA.ActualizaFechaFinEpisodio(customerEpisodeID, fechaFinEpisodio, userName);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ActualizaCustomerLeave(int customerEpisodeID, DateTime fechaFinEpisodio, string userName)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;
                return _customerEpisodeDA.ActualizaCustomerLeave(customerEpisodeID, fechaFinEpisodio, userName);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ActualizaFechaInicioEpisodio(int customerEpisodeID, DateTime fechaInicioEpisodio, string userName)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;
                return _customerEpisodeDA.ActualizaFechaInicioEpisodio(customerEpisodeID, fechaInicioEpisodio, userName);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        /// <summary>
        /// Actualiza el estado de autorización de los delivery note asociados a un episodio en función de las autorizaciones disponibles.
        /// </summary>
        /// <param name="customerEpisodeID"></param>
        public void UpdateDeliveryNoteAuthorizationStatus(int customerEpisodeID)
        {
            CustomerEpisodeUoWBL service = new CustomerEpisodeUoWBL(this);
            service.UpdateDeliveryNoteAuthorizationStatus(customerEpisodeID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prescr"></param>
        /// <returns></returns>
        public List<Tuple<int, int>> GetCustomerEpisodeIDsByPrescriptionRequestIDs(List<int> prescr)
        {
            try
            {
                if (prescr == null || prescr.Count <= 0) return null;
                DataSet ds = _customerEpisodeDA.GetCustomerEpisodeIDByPrescRequest(prescr.ToArray());

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    List<Tuple<int, int>> lista = new List<Tuple<int, int>>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int prescrID = Convert.ToInt32(row["PrescriptionRequestID"]);
                        int corID = Convert.ToInt32(row["CustomerEpisodeID"]);

                        lista.Add(new Tuple<int, int>(prescrID, corID));
                    }

                    return lista;
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

        #region Public methods Customer Episode Guarantor
        public CustomerEpisodeGuarantorEntity SaveCustomerEpisodeGuarantor(CustomerEpisodeGuarantorEntity customerEpisodeGuarantor)
        {
            try
            {
                if (customerEpisodeGuarantor == null) throw new ArgumentNullException("CustomerEpisodeGuarantor");

                CustomerEpisodeGuarantorEntity result = null;
                switch (customerEpisodeGuarantor.EditStatus.Value)
                {
                    case StatusEntityValue.Deleted:
                        this.CheckDeletePreconditionsCEG(customerEpisodeGuarantor);
                        result = this.DeleteCEG(customerEpisodeGuarantor);
                        break;
                    case StatusEntityValue.New:
                        result = this.InsertCEG(customerEpisodeGuarantor);
                        result.DBTimeStamp = _customerEpisodeGuarantorDA.GetDBTimeStamp(result.ID);
                        break;
                    case StatusEntityValue.None:
                        result = customerEpisodeGuarantor;
                        break;
                    case StatusEntityValue.NewAndDeleted:
                        result = null;
                        break;
                    case StatusEntityValue.Updated:
                        result = this.UpdateCEG(customerEpisodeGuarantor);
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerEpisodeGuarantorEntity[] GetCustomerEpisodeGuarantor(int customerID, int customerEpisodeID)
        {
            try
            {
                CustomerEpisodeGuarantorEntity[] customerEpisodeGuarantors = null;
                CustomerEpisodeGuarantorAdvancedAdapter CustomerEpisodeGuarantorAdvancedAdapter = new CustomerEpisodeGuarantorAdvancedAdapter();

                DataSet ds = _customerEpisodeGuarantorDA.Get(customerID, customerEpisodeID);

                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable))
                   && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable].Rows.Count > 0))
                {
                    #region Persons
                    DataSet ds2 = _customerEpisodeGuarantorDA.GetRelatedPersons(customerID, customerEpisodeID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion
                    #region Address
                    ds2 = _customerEpisodeGuarantorDA.GetRelatedAddress(customerID, customerEpisodeID);
                    if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion
                    customerEpisodeGuarantors = CustomerEpisodeGuarantorAdvancedAdapter.GetData(ds);
                }
                return customerEpisodeGuarantors;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        #endregion

        #region Special methods sin publicar
        //public CustomerEpisodeEntity[] GetCustomerEpisodes(int processChartID, Common.Entities.StatusEnum status,
        //    DateTime? fromDate, DateTime? toDate, out bool maxRecordExceded)
        //{
        //    maxRecordExceded = false;
        //    try
        //    {
        //        AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
        //        int maxRows = administrativeConfig.EntitySettings.CustomerEpisodeEntity.MaxRows;
        //        if (maxRows == 0) { maxRows = Int32.MaxValue; }

        //        CustomerEpisodeAdapter customerEpisodeAdapter = new CustomerEpisodeAdapter();

        //        DataSet ds = _customerEpisodeDA.GetCustomerEpisodes(maxRows, processChartID, status, fromDate, toDate);

        //        if ((ds.Tables != null) && (ds.Tables.Contains(_tableName)) && (ds.Tables[_tableName].Rows.Count > 0))
        //        {
        //            #region Episode reason types
        //            EpisodeReasonTypeDA _episodeReasonTypeDA = new EpisodeReasonTypeDA();
        //            DataSet ds2 = _episodeReasonTypeDA.GetAllEpisodeReasonType();
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            #region CustomerEpisodeReasonRels
        //            CustomerEpisodeReasonRelDA _customerEpisodeReasonRelDA = new CustomerEpisodeReasonRelDA();
        //            ds2 = _customerEpisodeReasonRelDA.GetCustomerEpisodeReasonRels(processChartID, status, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable].Copy();
        //                ds.Tables.Add(dt);

        //                EpisodeReasonDA _episodeReasonDA = new EpisodeReasonDA();
        //                ds2 = _episodeReasonDA.GetEpisodeReasonByCustomerEpisodes(processChartID, status, fromDate, toDate);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }

        //            }
        //            CustomerEpisodeLeaveReasonRelDA _customerEpisodeLeaveReasonRelDA = new CustomerEpisodeLeaveReasonRelDA();
        //            ds2 = _customerEpisodeLeaveReasonRelDA.GetCustomerEpisodeLeaveReasonRels(processChartID, status, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable].Copy();
        //                ds.Tables.Add(dt);
        //                ds2 = _episodeReasonDA.GetEpisodeLeaveReasonByCustomerEpisodes(processChartID, status, fromDate, toDate);
        //                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable)))
        //                {
        //                    dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable].Copy();
        //                    ds.Tables.Add(dt);
        //                }
        //            }

        //            #endregion

        //            #region Customer Episode Service Rel
        //            ds2 = _customerEpisodeServiceRelDA.GetByCustomerEpisodes(processChartID, status, fromDate, toDate);
        //            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
        //            {
        //                DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
        //                ds.Tables.Add(dt);
        //            }
        //            #endregion

        //            CustomerEpisodeEntity[] result = customerEpisodeAdapter.GetData(ds);
        //            if (result != null)
        //            {
        //                maxRecordExceded = (result.Length >= maxRows);
        //            }
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

        public CustomerEpisodeEntity[] GetCustomerEpisodes(Common.Entities.StatusEnum status, int locationID,
            DateTime? fromDate, DateTime? toDate, out bool maxRecordExceded)
        {
            maxRecordExceded = false;
            try
            {
                AdministrativeConfigurationSection administrativeConfig = FrameworkConfigurationService<AdministrativeConfigurationSection>.GetSection("administrative") as AdministrativeConfigurationSection;
                int maxRows = administrativeConfig.EntitySettings.CustomerEpisodeEntity.MaxRows;
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                CustomerEpisodeAdvancedAdapter customerEpisodeAdapter = new CustomerEpisodeAdvancedAdapter();
                string RecursiveLocations = "DECLARE @RecursiveLocation Table([ID] int,[Name] nvarchar(100))";
                CommonEntities.IDDescriptionAdapter recursiveLocationAdapter = new CommonEntities.IDDescriptionAdapter();
                CareProcessRealizationDA _careProcessRealizationDA = new CareProcessRealizationDA();
                DataSet rlds = _careProcessRealizationDA.GetRecursiveLocations(locationID, null);
                if ((rlds.Tables != null) && rlds.Tables.Contains(SII.HCD.Common.Entities.TableNames.IDDescriptionTable)
                    && (rlds.Tables[SII.HCD.Common.Entities.TableNames.IDDescriptionTable].Rows.Count > 0))
                {
                    CommonEntities.IDDescriptionEntity[] recursiveLocations = recursiveLocationAdapter.GetData(rlds);
                    foreach (CommonEntities.IDDescriptionEntity item in recursiveLocations)
                    {
                        RecursiveLocations = string.Concat(RecursiveLocations, "INSERT INTO @RecursiveLocation([ID],[Name]) VALUES(", item.ID.ToString(), ",'", item.Description, "')", Environment.NewLine);
                    }
                }

                DataSet ds = _customerEpisodeDA.GetCustomerEpisodes(maxRows, RecursiveLocations, status, fromDate, toDate);

                if ((ds.Tables != null) && (ds.Tables.Contains(_tableName))
                   && (ds.Tables[_tableName].Rows.Count > 0))
                {
                    CustomerEpisodeEntity[] result = customerEpisodeAdapter.GetData(ds);
                    if (result != null)
                    {
                        maxRecordExceded = (result.Length >= maxRows);
                    }
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


        public bool UpdatePhysician(int customerEpisodeID, int physicianID, string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName)) userName = IdentityUser.GetIdentityUserName();
                DataSet ds = _customerEpisodeServiceRelDA.GetByCustomerEpisodeID(customerEpisodeID);
                if (ds != null && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)
                    && ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Rows.Count > 0)
                {
                    int cesrID = ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].AsEnumerable()
                                    .Where(row => (row["Step"] as long? ?? 0) == (long)BasicProcessStepsEnum.Admission || (row["Step"] as long? ?? 0) == (long)BasicProcessStepsEnum.Reception)
                                    .Select(row => row["ID"] as int? ?? 0)
                                    .FirstOrDefault();
                    if (cesrID > 0)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {

                            _customerEpisodeServiceRelDA.UpdatePhysician(cesrID, physicianID, userName);
                            _customerEpisodeDA.UpdatePhysician(customerEpisodeID, physicianID, userName);
                            scope.Complete();
                        }
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public CustomerEpisodeEntity[] GetCustomerEpisodes(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.OrderBy(id => id).Distinct().ToArray();

                try
                {
                    CustomerEpisodeEntity customerEpisode = null;
                    CustomerEpisodeAdvancedAdapter customerEpisodeAdapter = new CustomerEpisodeAdvancedAdapter();
                    DataSet ds = _customerEpisodeDA.GetCustomerEpisodeByIDs(customerEpisodeIDs);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(_tableName))
                        && (ds.Tables[_tableName].Rows.Count > 0))
                    {
                        List<int> currentLocationAvailIDs = new List<int>();
                        List<int> customerADTOrderIDs = new List<int>();

                        foreach (DataRow row in ds.Tables[_tableName].Rows)
                        {
                            currentLocationAvailIDs.Add(Convert.ToInt32(row["CurrentLocationAvailID"]));
                            customerADTOrderIDs.Add(Convert.ToInt32(row["CustomerADTOrderID"]));
                        }

                        currentLocationAvailIDs = currentLocationAvailIDs.OrderBy(id => id).Distinct().ToList();
                        customerADTOrderIDs = customerADTOrderIDs.OrderBy(id => id).Distinct().ToList();

                        DataSet ds2;

                        #region Current location avail
                        
                        if (currentLocationAvailIDs.Count > 0)
                        {
                            ds2 = _locationAvailabilityDA.GetLocationAvailabilityByID(currentLocationAvailIDs.ToArray());
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable)))
                            {
                                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable].Copy();
                                dt.TableName = SII.HCD.BackOffice.Entities.TableNames.LocationAvailabilityTable;
                                ds.Tables.Add(dt);
                            }
                        }
                        #endregion

                        #region Episode reason types
                        EpisodeReasonTypeDA _episodeReasonTypeDA = new EpisodeReasonTypeDA();
                        ds2 = _episodeReasonTypeDA.GetAllEpisodeReasonType();
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.EpisodeReasonTypeTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region CustomerEpisodeReasonRels
                        CustomerEpisodeReasonRelDA _customerEpisodeReasonRelDA = new CustomerEpisodeReasonRelDA();
                        ds2 = _customerEpisodeReasonRelDA.GetCustomerEpisodeReasonRels(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable].Copy();
                            ds.Tables.Add(dt);
                        }

                        CustomerEpisodeLeaveReasonRelDA _customerEpisodeLeaveReasonRelDA = new CustomerEpisodeLeaveReasonRelDA();
                        ds2 = _customerEpisodeLeaveReasonRelDA.GetCustomerEpisodeLeaveReasonRels(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable].Copy();
                            ds.Tables.Add(dt);
                        }

                        EpisodeReasonDA _episodeReasonDA = new EpisodeReasonDA();
                        ds2 = _episodeReasonDA.GetEpisodeReasonByCustomerEpisode(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable].Copy();
                            ds.Tables.Add(dt);
                        }

                        ds2 = _episodeReasonDA.GetEpisodeLeaveReasonByCustomerEpisode(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable].Copy();
                            ds.Tables.Add(dt);
                        }

                        ConceptDA _conceptDA = new ConceptDA();
                        ds2 = _conceptDA.GetConceptsByCustomerEpisode(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptTable].Copy();
                            ds.Tables.Add(dt);
                        }

                        ds2 = _conceptDA.GetConceptsLeaveByCustomerEpisode(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ConceptLeaveTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ConceptLeaveTable].Copy();
                            ds.Tables.Add(dt);
                        }


                        #endregion

                        #region CustomerEpisodeAuthorizations
                        //CustomerEpisodeAuthorizationDA _customerEpisodeAuthorizationDA = new CustomerEpisodeAuthorizationDA();
                        ds2 = _customerEpisodeAuthorizationDA.GetByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable].Copy();
                            ds.Tables.Add(dt);

                            ds2 = _authorizationTypeDA.GetAuthorizationTypesByEpisode(customerEpisodeIDs);
                            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable)))
                            {
                                dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable].Copy();
                                dt.TableName = SII.HCD.BackOffice.Entities.TableNames.AuthorizationTypeTable;
                                ds.Tables.Add(dt);
                            }
                        }
                        #endregion

                        #region CustomerEpisodeAuthorizationEntries
                        //CustomerEpisodeAuthorizationEntryDA _customerEpisodeAuthorizationEntryDA = new CustomerEpisodeAuthorizationEntryDA();
                        ds2 = _customerEpisodeAuthorizationEntryDA.GetByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region CustomerEpisodeAuthorizationOps
                        //CustomerEpisodeAuthorizationOpsDA _customerEpisodeAuthorizationOpsDA = new CustomerEpisodeAuthorizationOpsDA();
                        ds2 = _customerEpisodeAuthorizationOpsDA.GetByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable].Copy();
                            dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable;
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Insurer Cover Agreements
                        ds2 = _customerCoverAgreeRelDA.GetInsurerCoverAgreementsByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCoverAgreeRelTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCoverAgreeRelTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Insurer Agreements
                        ds2 = _customerInsurerAgreeRelDA.GetInsurerAgreementsByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerInsurerAgreeRelTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerInsurerAgreeRelTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Care Center Assistance Agreements
                        ds2 = _customerAssistAgreeRelDA.GetCustomerAssistAgreeRelByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAssistAgreeRelTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAssistAgreeRelTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Care Center Agreements
                        ds2 = _customerAgreeRelDA.GetCustomerAgreeRelByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAgreeRelTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAgreeRelTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Customer Episode Service Rel
                        ds2 = _customerEpisodeServiceRelDA.GetByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable)))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Customer Episode Interop Information
                        ds2 = _customerEpInteropInfoDA.GetByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerEpInteropInfoTable)
                            && (ds2.Tables[Administrative.Entities.TableNames.CustomerEpInteropInfoTable].Rows.Count > 0))
                        {
                            DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerEpInteropInfoTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Customer Episode Referenced Physicians
                        ds2 = _customerEpisodeReferencedPhysicianRelDA.GetCustomerEpisodeReferencedPhysicianRels(customerEpisodeIDs);
                        if ((ds2 != null)
                            && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerEpisodeReferencedPhysicianRelTable))
                            && (ds2.Tables[Administrative.Entities.TableNames.CustomerEpisodeReferencedPhysicianRelTable].Rows.Count > 0))
                        {
                            DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerEpisodeReferencedPhysicianRelTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Customer Policy
                        ds2 = _customerPolicyDA.GetCustomerPolicyByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerPolicyTable)
                            && (ds2.Tables[Administrative.Entities.TableNames.CustomerPolicyTable].Rows.Count > 0))
                        {
                            DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerPolicyTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region Policy Type
                        ds2 = _policyTypeDA.GetPolicyTypeByCustomerEpisodeID(customerEpisodeIDs);
                        if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.PolicyTypeTable)
                            && (ds2.Tables[BackOffice.Entities.TableNames.PolicyTypeTable].Rows.Count > 0))
                        {
                            DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.PolicyTypeTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region CustomerGuarantee
                        ds2 = _customerGuaranteeDA.GetByCustomerEpisode(customerEpisodeIDs);

                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerGuaranteeTable))
                            && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerGuaranteeTable].Rows.Count > 0))
                        {
                            DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerGuaranteeTable].Copy();
                            ds.Tables.Add(dt);
                        }
                        #endregion

                        #region CustomerEpisodeGuarantor
                        ds2 = _customerEpisodeGuarantorDA.GetByCustomerEpisodeIDs(customerEpisodeIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable))
                           && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable].Rows.Count > 0))
                        {
                            DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable].Copy();
                            ds.Tables.Add(dt);
                            #region Persons
                            DataSet ds3 = _customerEpisodeGuarantorDA.GetRelatedPersons(customerEpisodeIDs);
                            if ((ds3 != null) && (ds3.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable)))
                            {
                                dt = ds3.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion
                            #region Address
                            ds3 = _customerEpisodeGuarantorDA.GetRelatedAddress(customerEpisodeIDs);
                            if ((ds3 != null) && (ds3.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.AddressTable)))
                            {
                                dt = ds3.Tables[SII.HCD.BackOffice.Entities.TableNames.AddressTable].Copy();
                                ds.Tables.Add(dt);
                            }
                            #endregion
                        }
                        #endregion

                        CustomerEpisodeEntity[] customerEpisodes = customerEpisodeAdapter.GetData(ds);
                        CustomerOrderRequestEntity[] ADTOrders = null;

                        if (customerADTOrderIDs.Count > 0)
                        {
                            ADTOrders = CustomerOrderRequestBL.GetCustomerOrderRequests(customerADTOrderIDs.ToArray(), false, true);
                        }

                        foreach (var myCustomerEpisode in customerEpisodes)
                        {
                            if (ADTOrders != null)
                            {
                                CustomerOrderRequestEntity ADTOrder = ADTOrders.FirstOrDefault(adt => adt.CustomerEpisodeID == myCustomerEpisode.ID);
                                if (ADTOrder != null)
                                    myCustomerEpisode.ADTOrder = ADTOrder;
                            }

                            if (myCustomerEpisode.CustomerAuthorizations != null && myCustomerEpisode.CustomerAuthorizations.Length < 0)
                            {
                                int notAuthorizationTypeID = _authorizationTypeDA.GetAuthorizationTypeNotInvoiceByCustomerEpisode(myCustomerEpisode.ID);
                                if (notAuthorizationTypeID > 0 &&
                                    myCustomerEpisode.CustomerAuthorizations.Any(auth => auth.AuthorizationTypeID == notAuthorizationTypeID))
                                {
                                    List<CustomerEpisodeAuthorizationEntity> authors = new List<CustomerEpisodeAuthorizationEntity>();
                                    authors.AddRange(myCustomerEpisode.CustomerAuthorizations
                                                            .Where(auth => auth.AuthorizationTypeID != notAuthorizationTypeID)
                                                            .ToArray());
                                    authors.Add(myCustomerEpisode.CustomerAuthorizations
                                                            .Where(auth => auth.AuthorizationTypeID == notAuthorizationTypeID)
                                                            .FirstOrDefault());
                                    myCustomerEpisode.CustomerAuthorizations = authors.ToArray();
                                }
                            }
                        }

                        //Task tLog = Task.Factory.StartNew(() =>
                        //{
                            foreach (var ce in customerEpisodes)
                                LOPDLogger.Write(EntityNames.CustomerEpisodeEntityName, ce.ID, ActionType.View);
                        //});

                        return customerEpisodes;
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
        #endregion
    }
}
