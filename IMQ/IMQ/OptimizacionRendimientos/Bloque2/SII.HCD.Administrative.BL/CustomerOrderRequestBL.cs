using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using SII.Framework.Entities.Services;
using SII.Framework.ExceptionHandling;
using SII.Framework.Interfaces;
using SII.Framework.Logging.LOPD;
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
using SII.HCD.BackOffice.Services;
using SII.HCD.Common.BL;
using SII.HCD.Common.DA;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Common.Services;
using SII.HCD.Configuration;
using SII.HCD.Misc;
using SII.HCD.Misc.IoC;
using CommonEntities = SII.HCD.Common.Entities;
using System.Text;
using System.Text.RegularExpressions;

namespace SII.HCD.Administrative.BL
{
    public class CustomerOrderRequestBL : BusinessLayerBase<CustomerOrderRequestEntity>, ICustomerOrderRequestService
    {
        #region Const
        private const string routineCodeName = "Code";
        private const string procedureCodeName = "AssignedCode";
        private string MaxCharactersAllowedInQueryName = "MaxCharactersAllowedInQuery";
        private string MediaServerCommand = "MediaServerCommand";
        #endregion

        #region Fields
        private CustomerOrderRequestDataAccess _dataAccess = null;
        private CustomerOrderRequestHelpers _helpers = null;

        private ElementBL _elementBL = null;
        private CustomerOrderRealizationBL _customerOrderRealizationBL = null;
        private NotificationActBL _notificationActBL = null;
        private CustomerBL _customerBL = null;
        private CustomerRoutineBL _customerRoutineBL = null;
        private CustomerProcedureBL _customerProcedureBL = null;
        private RoutineActBL _routineActBL = null;
        private ProcedureActBL _procedureActBL = null;
        private RoutineBL _routineBL = null;
        private ProcedureBL _procedureBL = null;
        private CareCenterBL _careCenterBL = null;
        private LocationBL _locationBL = null;
        private TimePatternBL _timePatternBL = null;
        private ReasonChangeBL _reasonChangeBL = null;
        private ProcessChartBL _processChartBL = null;
        private CustomerEpisodeBL _customerEpisodeBL = null;
        private PhysicianBL _physicianBL = null;
        private PersonBL _personBL = null;
        private CustomerProcessBL _customerProcessBL = null;
        private CustomerReservationBL _customerReservationBL = null;

        private CustomerAssistancePlanBL _customerAssistancePlanBL = null;
        private CustomerMedEpisodeActBL _customerMedEpisodeActBL = null;
        private CustomerObservationBL _customerObservationBL = null;
        private OrderBL _orderBL = null;
        private CodeGenerator _codeGenerator = null;
        private IOrderCacheService _orderCache = null;
        private IPhysicianCacheService _physicianCache = null;
        private IRoutineCacheService _routineCache = null;
        private IProcedureCacheService _procedureCache = null;
        private IObservationCacheService _observationCache = null;

        private CommonEntities.ElementEntity _customerOrderRequestMetadata = null;

        private int[] _validRoutineIDs = null;
        private int[] _validProcedureIDs = null;
        private int[] _storageLocationIDs = null;

        private string[] group_columns_array = new string[] { "CustomerOrderRequestID", "Element", "FifthColumn", "SixColumn" };
        private List<CustomerOrderRequestEntity> _analyzedCustomerOrders;

        private IIDISDeviceInteropConfigManagement _isicms = null;
        #endregion

        #region Constructor
        public CustomerOrderRequestBL()
        {
            _orderCache = IoCFactory.CurrentContainer.Resolve<IOrderCacheService>();
            _physicianCache = IoCFactory.CurrentContainer.Resolve<IPhysicianCacheService>();
            _routineCache = IoCFactory.CurrentContainer.Resolve<IRoutineCacheService>();
            _procedureCache = IoCFactory.CurrentContainer.Resolve<IProcedureCacheService>();
            _observationCache = IoCFactory.CurrentContainer.Resolve<IObservationCacheService>();
        }
        #endregion

        #region Properties
        private CustomerOrderRequestDataAccess DataAccess
        {
            get
            {
                if (_dataAccess == null)
                    InitializeDataAccess();

                return _dataAccess;
            }
        }

        private CustomerOrderRequestHelpers Helpers
        {
            get
            {
                if (_helpers == null)
                    InitializeHelpers();

                return _helpers;
            }
        }

        private CustomerBL CustomerBL
        {
            get
            {
                if (_customerBL == null)
                    _customerBL = new CustomerBL();
                return _customerBL;
            }
        }

        private CustomerRoutineBL CustomerRoutineBL
        {
            get
            {
                if (_customerRoutineBL == null)
                    _customerRoutineBL =
                        CreateDependentBusinessLayer<CustomerRoutineBL, CustomerRoutineEntity>();

                return _customerRoutineBL;
            }
        }

        private RoutineActBL RoutineActBL
        {
            get
            {
                if (_routineActBL == null)
                    _routineActBL =
                        CreateDependentBusinessLayer<RoutineActBL, RoutineActEntity>();

                return _routineActBL;
            }
        }

        private CustomerProcedureBL CustomerProcedureBL
        {
            get
            {
                if (_customerProcedureBL == null)
                    _customerProcedureBL =
                        CreateDependentBusinessLayer<CustomerProcedureBL, CustomerProcedureEntity>();

                return _customerProcedureBL;
            }
        }

        private ProcedureActBL ProcedureActBL
        {
            get
            {
                if (_procedureActBL == null)
                    _procedureActBL =
                        CreateDependentBusinessLayer<ProcedureActBL, ProcedureActEntity>();

                return _procedureActBL;
            }
        }

        private CustomerOrderRealizationBL CustomerOrderRealizationBL
        {
            get
            {
                if (_customerOrderRealizationBL == null)
                    _customerOrderRealizationBL =
                        CreateDependentBusinessLayer<CustomerOrderRealizationBL, CustomerOrderRealizationEntity>();

                return _customerOrderRealizationBL;
            }
        }

        public NotificationActBL NotificationActBL
        {
            get
            {
                if (_notificationActBL == null)
                    _notificationActBL =
                        CreateDependentBusinessLayer<NotificationActBL, NotificationActEntity>();

                return _notificationActBL;
            }
        }

        private RoutineBL RoutineBL
        {
            get
            {
                if (_routineBL == null)
                    _routineBL = new RoutineBL();

                return _routineBL;
            }
        }

        private ProcedureBL ProcedureBL
        {
            get
            {
                if (_procedureBL == null)
                    _procedureBL = new ProcedureBL();

                return _procedureBL;
            }
        }

        private CareCenterBL CareCenterBL
        {
            get
            {
                if (_careCenterBL == null)
                    _careCenterBL = new CareCenterBL();

                return _careCenterBL;
            }
        }

        private LocationBL LocationBL
        {
            get
            {
                if (_locationBL == null)
                    _locationBL = new LocationBL();

                return _locationBL;
            }
        }

        private TimePatternBL TimePatternBL
        {
            get
            {
                if (_timePatternBL == null)
                    _timePatternBL = new TimePatternBL();

                return _timePatternBL;
            }
        }

        private ReasonChangeBL ReasonChangeBL
        {
            get
            {
                if (_reasonChangeBL == null)
                    _reasonChangeBL = new ReasonChangeBL();

                return _reasonChangeBL;
            }
        }

        private ProcessChartBL ProcessChartBL
        {
            get
            {
                if (_processChartBL == null)
                    _processChartBL = new ProcessChartBL();

                return _processChartBL;
            }
        }

        private CustomerEpisodeBL CustomerEpisodeBL
        {
            get
            {
                if (_customerEpisodeBL == null)
                    _customerEpisodeBL = new CustomerEpisodeBL();

                return _customerEpisodeBL;
            }
        }

        private PhysicianBL PhysicianBL
        {
            get
            {
                if (_physicianBL == null)
                    _physicianBL = new PhysicianBL();
                return _physicianBL;
            }
        }

        private PersonBL PersonBL
        {
            get
            {
                if (_personBL == null)
                    _personBL = new PersonBL();
                return _personBL;
            }
        }

        private CustomerAssistancePlanBL CustomerAssistancePlanBL
        {
            get
            {
                if (_customerAssistancePlanBL == null)
                    _customerAssistancePlanBL = new CustomerAssistancePlanBL();

                return _customerAssistancePlanBL;
            }
        }

        private CustomerMedEpisodeActBL CustomerMedEpisodeActBL
        {
            get
            {
                if (_customerMedEpisodeActBL == null)
                    _customerMedEpisodeActBL = new CustomerMedEpisodeActBL();

                return _customerMedEpisodeActBL;
            }
        }

        private CustomerObservationBL CustomerObservationBL
        {
            get
            {
                if (_customerObservationBL == null)
                    _customerObservationBL = CreateDependentBusinessLayer<CustomerObservationBL, RegisteredLayoutEntity>();

                return _customerObservationBL;
            }
        }

        private OrderBL OrderBL
        {
            get
            {
                if (_orderBL == null)
                    _orderBL = new OrderBL();

                return _orderBL;
            }
        }

        private ElementBL ElementBL
        {
            get
            {
                if (_elementBL == null)
                    _elementBL = new ElementBL();

                return _elementBL;
            }
        }

        private CodeGenerator CodeGenerator
        {
            get
            {
                if (_codeGenerator == null)
                    _codeGenerator = new CodeGenerator();

                return _codeGenerator;
            }
        }

        internal CommonEntities.ElementEntity CustomerOrderRequestMetadata
        {
            get
            {
                if (_helpers == null)
                    InitializeHelpers();

                return _customerOrderRequestMetadata;
            }
        }

        private int[] ValidRoutineIDs
        {
            get
            {
                if (_validRoutineIDs == null)
                    _validRoutineIDs = DataAccess.RoutineDA.GetRoutineIDsByStatus(
                        CommonEntities.StatusEnum.Confirmed);

                return _validRoutineIDs;
            }
        }

        private int[] ValidProcedureIDs
        {
            get
            {
                if (_validProcedureIDs == null)
                    _validProcedureIDs = DataAccess.ProcedureDA.GetProcedureIDsByStatus(
                        CommonEntities.StatusEnum.Confirmed);

                return _validProcedureIDs;
            }
        }

        private int[] StorageLocationIDs
        {
            get
            {
                if (_storageLocationIDs == null)
                    _storageLocationIDs = DataAccess.LocationDA.GetLocationIDsWithStorage(
                         CommonEntities.StatusEnum.Active);

                return _storageLocationIDs;
            }
        }

        private CustomerProcessBL CustomerProcessBL
        {
            get
            {
                if (_customerProcessBL == null)
                    _customerProcessBL = new CustomerProcessBL();

                return _customerProcessBL;
            }
        }

        private CustomerReservationBL CustomerReservationBL
        {
            get
            {
                if (_customerReservationBL == null)
                    _customerReservationBL = new CustomerReservationBL();

                return _customerReservationBL;
            }
        }

        internal List<CustomerOrderRequestEntity> AnalyzedCustomerOrders
        {
            get
            {
                if (_analyzedCustomerOrders == null)
                    _analyzedCustomerOrders = new List<CustomerOrderRequestEntity>();

                return _analyzedCustomerOrders;
            }
        }

        private IIDISDeviceInteropConfigManagement IDISMetadata
        {
            get
            {
                if (_isicms == null) _isicms = IoCFactory.CurrentContainer.Resolve<IIDISDeviceInteropConfigManagement>();
                return _isicms;
            }
        }

        #endregion

        #region Private methods
        private void InitializeDataAccess()
        {
            _dataAccess = new CustomerOrderRequestDataAccess
            {
                CustomerOrderRequestDA = new CustomerOrderRequestDA(),
                OrderRequestSchPlanningDA = new OrderRequestSchPlanningDA(),
                OrderRequestRoutineRelDA = new OrderRequestRoutineRelDA(),
                OrderRequestRoutineTimeDA = new OrderRequestRoutineTimeDA(),
                OrderRequestProcedureRelDA = new OrderRequestProcedureRelDA(),
                OrderRequestTimeDA = new OrderRequestTimeDA(),
                OrderDA = new OrderDA(),
                RoutineDA = new RoutineDA(),
                RoutineActDA = new RoutineActDA(),
                ProcedureDA = new ProcedureDA(),
                ProcedureActDA = new ProcedureActDA(),
                LocationDA = new LocationDA(),
                PrescriptionRequestDA = new PrescriptionRequestDA(),
                PrescriptionRequestTimeDA = new PrescriptionRequestTimeDA(),

                //PrescriptionRequestItemSequenceRelDA = new PrescriptionRequestItemSequenceRelDA(),

                //ItemTreatmentRouteDA = new ItemTreatmentRouteDA(),
                EquipmentDA = new EquipmentDA(),
                AdministrationMethodDA = new AdministrationMethodDA(),
                AdministrationRouteDA = new AdministrationRouteDA(),
                PharmaceuticalFormAdministrationRouteRelDA = new PharmaceuticalFormAdministrationRouteRelDA(),
                PharmaceuticalFormDA = new PharmaceuticalFormDA(),
                BodySiteDA = new BodySiteDA(),
                BodySiteConceptDA = new BodySiteConceptDA(),
                BodySiteClassificationDA = new BodySiteClassificationDA(),
                BodySiteParticipationDA = new BodySiteParticipationDA(),
                ItemTreatmentOrderSequenceDA = new ItemTreatmentOrderSequenceDA(),
                ItemDA = new ItemDA(),
                PhysUnitDA = new PhysUnitDA(),
                TimePatternDA = new TimePatternDA(),
                ProcessChartDA = new ProcessChartDA(),
                OrderRequestProcedureTimeDA = new OrderRequestProcedureTimeDA(),
                OrderRequestProcedureRoutineRelDA = new OrderRequestProcedureRoutineRelDA(),
                OrderRequestCustomerObservationRelDA = new OrderRequestCustomerObservationRelDA(),
                CustomerOrderRequestReasonRelDA = new CustomerOrderRequestReasonRelDA(),
                ReasonChangeDA = new ReasonChangeDA(),
                RecordDeletedLogDA = new RecordDeletedLogDA(),
                CustomerOrderRealizationDA = new CustomerOrderRealizationDA(),
                CustomerAccountDA = new CustomerAccountDA(),
                ProcedureActRoutineActRelDA = new ProcedureActRoutineActRelDA(),
                CustomerAssistancePlanDA = new CustomerAssistancePlanDA(),
                CustomerProcedureDA = new CustomerProcedureDA(),
                CustomerRoutineDA = new CustomerRoutineDA(),
                CustomerProcedureRoutineRelDA = new CustomerProcedureRoutineRelDA(),
                CustomerProcedureReasonRelDA = new CustomerProcedureReasonRelDA(),
                OrderRequestCustomerProcedureRelDA = new OrderRequestCustomerProcedureRelDA(),
                OrderRequestCustomerRoutineRelDA = new OrderRequestCustomerRoutineRelDA(),
                CustomerProcedureTimeDA = new CustomerProcedureTimeDA(),
                CustomerRoutineTimeDA = new CustomerRoutineTimeDA(),
                OrderRequestADTInfoDA = new OrderRequestADTInfoDA(),
                CustomerAdmissionDA = new CustomerAdmissionDA(),
                CustomerRoutineReasonRelDA = new CustomerRoutineReasonRelDA(),
                CareProcessRealizationDA = new CareProcessRealizationDA(),

                OrderRequestHumanResourceRelDA = new OrderRequestHumanResourceRelDA(),
                ParticipateAsDA = new ParticipateAsDA(),
                OrderRequestResourceRelDA = new OrderRequestResourceRelDA(),
                OrderRequestEquipmentRelDA = new OrderRequestEquipmentRelDA(),
                OrderRequestLocationRelDA = new OrderRequestLocationRelDA(),
                OrderRequestRequirementRelDA = new OrderRequestRequirementRelDA(),
                RequirementDA = new RequirementDA(),
                OrderRequestBodySiteRelDA = new OrderRequestBodySiteRelDA(),
                OrderRequestConsentRelDA = new OrderRequestConsentRelDA(),
                ConsentPreprintDA = new ConsentPreprintDA(),
                ConsentTypeDA = new ConsentTypeDA(),

                CareCenterRelatedCodeGeneratorDA = new CareCenterRelatedCodeGeneratorDA(),
                CustomerDA = new CustomerDA()
            };
        }

        private void InitializeHelpers()
        {
            _customerOrderRequestMetadata = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.CustomerOrderRequestEntityName, true);

            _helpers = new CustomerOrderRequestHelpers
            {
                CustomerOrderRequestHelper = new CustomerOrderRequestHelper(_customerOrderRequestMetadata),
                OrderRequestSchPlanningHelper = new OrderRequestSchPlanningHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestSchPlanningEntityName, false)),
                OrderRequestRoutineRelHelper = new OrderRequestRoutineRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestRoutineRelEntityName, false)),
                OrderRequestProcedureRelHelper = new OrderRequestProcedureRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestProcedureRelEntityName, false)),
                OrderRequestTimeHelper = new OrderRequestTimeHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestTimeEntityName, false)),
                OrderHelper = new OrderHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderEntityName, false)),
                RoutineHelper = new RoutineHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.RoutineEntityName, false)),
                RoutineActHelper = new RoutineActHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.RoutineActEntityName, false)),
                ProcedureHelper = new ProcedureHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.ProcedureEntityName, false)),
                ProcedureActHelper = new ProcedureActHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.ProcedureActEntityName, false)),
                PrescriptionRequestHelper = new PrescriptionRequestHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.PrescriptionRequestEntityName, false)),
                PrescriptionRequestTimeHelper = new PrescriptionRequestTimeHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.PrescriptionRequestTimeEntityName, false)),

                //PrescriptionRequestItemSequenceRelHelper = new PrescriptionRequestItemSequenceRelHelper(
                //    ElementBL.GetElementByName(
                //        CommonEntities.Constants.EntityNames.PrescriptionRequestItemSequenceRelEntityName, false)),
                ItemTreatmentOrderSequenceHelper = new ItemTreatmentOrderSequenceHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.ItemTreatmentOrderSequenceEntityName, false)),

                TimePatternHelper = new TimePatternHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.TimePatternEntityName, false)),
                OrderRequestProcedureRoutineRelHelper = new OrderRequestProcedureRoutineRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestProcedureRoutineRelEntityName, false)),
                CustomerOrderRequestReasonRelHelper = new CustomerOrderRequestReasonRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.CustomerOrderRequestEntityName, false)),
                ReasonChangeHelper = new ReasonChangeHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.ReasonChangeEntityName, false)),
                CustomerOrderRealizationHelper = new CustomerOrderRealizationHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.CustomerOrderRealizationEntityName, false)),
                CustomerProcedureHelper = new CustomerProcedureHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.CustomerProcedureEntityName, false)),
                CustomerRoutineHelper = new CustomerRoutineHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.CustomerRoutineEntityName, false)),
                CustomerProcedureRoutineRelHelper = new CustomerProcedureRoutineRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.CustomerProcedureRoutineRelEntityName, false)),
                CustomerProcedureTimeHelper = new CustomerProcedureTimeHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.CustomerProcedureTimeEntityName, false)),
                CustomerRoutineTimeHelper = new CustomerRoutineTimeHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.CustomerRoutineTimeEntityName, false)),
                OrderRequestADTInfoHelper = new OrderRequestADTInfoHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestADTInfoEntityName, false)),

                OrderRequestHumanResourceRelHelper = new OrderRequestHumanResourceRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestHumanResourceRelEntityName, false)),
                OrderRequestResourceRelHelper = new OrderRequestResourceRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestResourceRelEntityName, false)),
                OrderRequestEquipmentRelHelper = new OrderRequestEquipmentRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestEquipmentRelEntityName, false)),
                OrderRequestLocationRelHelper = new OrderRequestLocationRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestLocationRelEntityName, false)),
                OrderRequestRequirementRelHelper = new OrderRequestRequirementRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestRequirementRelEntityName, false)),
                OrderRequestBodySiteRelHelper = new OrderRequestBodySiteRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestBodySiteRelEntityName, false)),
                OrderRequestConsentRelHelper = new OrderRequestConsentRelHelper(
                    ElementBL.GetElementByName(
                        CommonEntities.Constants.EntityNames.OrderRequestConsentRelEntityName, false)),
            };
        }

        private bool ProcedureActHasChanges(ProcedureActEntity procedureAct)
        {
            if (procedureAct == null)
                return false;

            return procedureAct.EditStatus.Value != StatusEntityValue.None
                || (procedureAct.Resources != null
                    && procedureAct.Resources.Any(pr => pr.EditStatus.Value != StatusEntityValue.None));
        }

        private RoutineActEntity FindRoutineAct(int routineActID)
        {
            if (routineActID <= 0)
                return null;

            RoutineActEntity result = DataRepository.Find<RoutineActEntity>(ra => ra.ID == routineActID);
            if (result == null)
            {
                result = RoutineActBL.Get(routineActID, false);
                if (result != null)
                {
                    DataRepository.Add<RoutineActEntity>(result);
                }
            }

            return result;
        }

        private ProcedureActEntity FindProcedureAct(int procedureActID)
        {
            if (procedureActID <= 0)
                return null;

            ProcedureActEntity result = DataRepository.Find<ProcedureActEntity>(ra => ra.ID == procedureActID);
            if (result == null)
            {
                result = ProcedureActBL.Get(procedureActID, ProcedureActSubEntitiesFlagEnum.Resources);
                if (result != null)
                {
                    DataRepository.Add<ProcedureActEntity>(result);
                }
            }

            return result;
        }

        private RoutineActEntity[] FindRoutineActsByCustomerRoutineCheckingDataRepository(int customerRoutineID)
        {
            if (customerRoutineID <= 0)
                return null;

            int[] routineActIDs = DataAccess.RoutineActDA.GetIDsByCustomerRoutine(customerRoutineID);
            if (routineActIDs == null)
                return null;

            List<RoutineActEntity> result = new List<RoutineActEntity>();
            foreach (int id in routineActIDs)
            {
                RoutineActEntity routineAct = FindRoutineAct(id);
                if (routineAct != null)
                {
                    result.Add(routineAct);
                }
            }
            return result.ToArray();
        }

        private ProcedureActEntity[] FindProcedureActsByCustomerProcedureCheckingDataRepository(int customerProcedureID)
        {
            if (customerProcedureID <= 0)
                return null;

            int[] procedureActIDs = DataAccess.ProcedureActDA.GetIDsByCustomerProcedure(customerProcedureID);
            if (procedureActIDs == null)
                return null;

            List<ProcedureActEntity> result = new List<ProcedureActEntity>();
            foreach (int id in procedureActIDs)
            {
                ProcedureActEntity procedureAct = FindProcedureAct(id);
                if (procedureAct != null)
                {
                    result.Add(procedureAct);
                }
            }
            return result.ToArray();
        }

        private void AddReasonCustomerOrderRequest(CustomerOrderRequestEntity entity,
            int reasonChangeID, string explanation, ValidationResults validationResults)
        {
            if (entity == null)
                return;

            ReasonChangeEntity reasonChange = ReasonChangeBL.GetReasonChange(reasonChangeID);

            if (reasonChange == null)
                return;

            CustomerOrderRequestReasonRelEntity corReason = new CustomerOrderRequestReasonRelEntity()
            {
                CustomerOrderRequestID = entity.ID,
                Explanation = explanation,
                ReasonChange = reasonChange
            };
            corReason.EditStatus.New();

            entity.Reason = corReason;
        }

        private void AddReasonToCustomerOrderRequests(IEnumerable<CustomerOrderRequestEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            ReasonChangeEntity reasonChange = ReasonChangeBL.GetReasonChange(reasonChangeID);

            if (reasonChange == null)
                return;

            foreach (CustomerOrderRequestEntity entity in entities)
            {
                //if (entity.Status == ActionStatusEnum.Cancelled)
                //{
                CustomerOrderRequestReasonRelEntity crReason = new CustomerOrderRequestReasonRelEntity()
                {
                    CustomerOrderRequestID = entity.ID,
                    Explanation = explanation,
                    ReasonChange = reasonChange
                };
                crReason.EditStatus.New();

                entity.Reason = crReason;
                //}
            }
        }

        private void AddReasonToCustomerRoutines(IEnumerable<CustomerRoutineEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            ReasonChangeEntity reasonChange = ReasonChangeBL.GetReasonChange(reasonChangeID);

            if (reasonChange == null)
                return;

            foreach (CustomerRoutineEntity entity in entities)
            {
                CustomerRoutineReasonRelEntity crReason = new CustomerRoutineReasonRelEntity()
                {
                    CustomerRoutineID = entity.ID,
                    Explanation = explanation,
                    ReasonChange = reasonChange
                };
                crReason.EditStatus.New();

                entity.Reason = crReason;
            }
        }

        private void AddReasonToCustomerProcedures(IEnumerable<CustomerProcedureEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            ReasonChangeEntity reasonChange = ReasonChangeBL.GetReasonChange(reasonChangeID);

            if (reasonChange == null)
                return;

            foreach (CustomerProcedureEntity entity in entities)
            {
                CustomerProcedureReasonRelEntity crReason = new CustomerProcedureReasonRelEntity()
                {
                    CustomerProcedureID = entity.ID,
                    Explanation = explanation,
                    ReasonChange = reasonChange
                };
                crReason.EditStatus.New();

                entity.Reason = crReason;
            }
        }

        private void AddReasonToRoutinesActs(IEnumerable<RoutineActEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            ReasonChangeEntity reasonChange = ReasonChangeBL.GetReasonChange(reasonChangeID);

            if (reasonChange == null)
                return;

            foreach (RoutineActEntity entity in entities)
            {
                RoutineActReasonRelEntity crReason = new RoutineActReasonRelEntity()
                {
                    RoutineActID = entity.ID,
                    Explanation = explanation,
                    ReasonChange = reasonChange
                };
                crReason.EditStatus.New();

                entity.Reason = crReason;
            }
        }

        private void AddReasonToProcedureActs(IEnumerable<ProcedureActEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            ReasonChangeEntity reasonChange = ReasonChangeBL.GetReasonChange(reasonChangeID);

            if (reasonChange == null)
                return;

            foreach (ProcedureActEntity entity in entities)
            {
                ProcedureActReasonRelEntity crReason = new ProcedureActReasonRelEntity()
                {
                    ProcedureActID = entity.ID,
                    Explanation = explanation,
                    ReasonChange = reasonChange
                };
                crReason.EditStatus.New();

                entity.Reason = crReason;
            }
        }

        private void RemoveReasonCustomerOrderRequest(CustomerOrderRequestEntity entity,
            int reasonChangeID, string explanation, ValidationResults validationResults)
        {
            if (entity == null || entity.Reason == null || entity.Reason.ReasonChange != null
                || entity.Reason.ReasonChange.ID != reasonChangeID)
                return;

            entity.Reason.EditStatus.Delete();
        }

        private void RemoveReasonToCustomerOrderRequests(IEnumerable<CustomerOrderRequestEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            foreach (CustomerOrderRequestEntity entity in entities)
            {
                if (entity.Reason != null && entity.Reason.ReasonChange != null
                    && entity.Reason.ReasonChange.ID == reasonChangeID)
                {
                    entity.Reason.EditStatus.Delete();
                }
            }
        }

        private void RemoveReasonToCustomerRoutines(IEnumerable<CustomerRoutineEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            foreach (CustomerRoutineEntity entity in entities)
            {
                if (entity.Reason != null && entity.Reason.ReasonChange != null
                    && entity.Reason.ReasonChange.ID == reasonChangeID)
                {
                    entity.Reason.EditStatus.Delete();
                }
            }
        }

        private void RemoveReasonToCustomerProcedures(IEnumerable<CustomerProcedureEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            foreach (CustomerProcedureEntity entity in entities)
            {
                if (entity.Reason != null && entity.Reason.ReasonChange != null
                    && entity.Reason.ReasonChange.ID == reasonChangeID)
                {
                    entity.Reason.EditStatus.Delete();
                }
            }
        }

        private void RemoveReasonToRoutinesActs(IEnumerable<RoutineActEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            foreach (RoutineActEntity entity in entities)
            {
                if (entity.Reason != null && entity.Reason.ReasonChange != null
                    && entity.Reason.ReasonChange.ID == reasonChangeID)
                {
                    entity.Reason.EditStatus.Delete();
                }
            }
        }

        private void RemoveReasonToProcedureActs(IEnumerable<ProcedureActEntity> entities,
            int reasonChangeID, string explanation)
        {
            if (entities == null || reasonChangeID <= 0)
                return;

            foreach (ProcedureActEntity entity in entities)
            {
                if (entity.Reason != null && entity.Reason.ReasonChange != null
                    && entity.Reason.ReasonChange.ID == reasonChangeID)
                {
                    entity.Reason.EditStatus.Delete();
                }
            }
        }

        private void CancelActivityAfterDateTime(CustomerOrderRequestEntity customerOrderRequest,
            DateTime? dateTimeLimit, int reasonChangeID, string explanation,
            ValidationResults validationResults)
        {
            if (!dateTimeLimit.HasValue || customerOrderRequest == null
                || customerOrderRequest.OrderRequestSchPlanning == null)
                return;

            //Cancelar RoutineActs
            CancelRoutineActsAfterDateTime(customerOrderRequest, dateTimeLimit,
                reasonChangeID, explanation, validationResults);

            //Cancelar ProcedureActs
            CancelProcedureActsAfterDateTime(customerOrderRequest, dateTimeLimit,
                reasonChangeID, explanation, validationResults);
        }

        private void CancelRoutineActsAfterDateTime(CustomerOrderRequestEntity customerOrderRequest,
            DateTime? dateTimeLimit, int reasonChangeID, string explanation, ValidationResults validationResults)
        {
            if (!dateTimeLimit.HasValue || customerOrderRequest == null
                || customerOrderRequest.OrderRequestSchPlanning == null
                || customerOrderRequest.OrderRequestSchPlanning.CustomerRoutines == null)
                return;

            //Cancelar RoutineActs
            if (customerOrderRequest.OrderRequestSchPlanning.CustomerRoutines != null)
            {
                foreach (CustomerRoutineEntity cr in customerOrderRequest.OrderRequestSchPlanning.CustomerRoutines)
                {
                    //if (cr.Status == ActionStatusEnum.Pending
                    //    || cr.Status == ActionStatusEnum.Scheduled
                    //    || cr.Status == ActionStatusEnum.Initiated
                    //    || cr.Status == ActionStatusEnum.Completed)
                    //{
                    RoutineActEntity[] routineActs = FindRoutineActsByCustomerRoutineCheckingDataRepository(cr.ID);
                    if (routineActs != null && routineActs.Length > 0)
                    {
                        foreach (RoutineActEntity ra in routineActs)
                        {
                            if (ra.StartDateTime.HasValue && ra.StartDateTime > dateTimeLimit)
                            {
                                switch (ra.ActStatus)
                                {
                                    case ActionStatusEnum.Pending:
                                    case ActionStatusEnum.Waiting:
                                    case ActionStatusEnum.Scheduled:
                                        ra.ActStatus = ActionStatusEnum.Cancelled;
                                        ra.EditStatus.Update();
                                        break;
                                    case ActionStatusEnum.Initiated:
                                    case ActionStatusEnum.Completed:
                                        ra.ActStatus = ActionStatusEnum.Aborted;
                                        ra.EditStatus.Update();
                                        break;
                                    default:
                                        break;
                                }
                            }

                            //if (ra.EditStatus.Value != StatusEntityValue.None)
                            //{
                            //    RoutineActBL.Save(ra, false, validationResults);
                            //}
                        }
                    }
                    //}
                }
            }
        }

        private void CancelProcedureActsAfterDateTime(CustomerOrderRequestEntity customerOrderRequest,
            DateTime? dateTimeLimit, int reasonChangeID, string explanation, ValidationResults validationResults)
        {
            if (!dateTimeLimit.HasValue || customerOrderRequest == null
                || customerOrderRequest.OrderRequestSchPlanning == null
                || customerOrderRequest.OrderRequestSchPlanning.CustomerProcedures == null)
                return;

            //Cancelar ProcedureActs
            if (customerOrderRequest.OrderRequestSchPlanning.CustomerProcedures != null)
            {
                foreach (CustomerProcedureEntity cp in customerOrderRequest.OrderRequestSchPlanning.CustomerProcedures)
                {
                    //if (cp.Status == ActionStatusEnum.Pending
                    //    || cp.Status == ActionStatusEnum.Scheduled
                    //    || cp.Status == ActionStatusEnum.Initiated
                    //    || cp.Status == ActionStatusEnum.Cancelled
                    //    || cp.Status == ActionStatusEnum.Aborted
                    //    || cp.Status == ActionStatusEnum.Completed)
                    //{
                    ProcedureActEntity[] procedureActs = FindProcedureActsByCustomerProcedureCheckingDataRepository(cp.ID);
                    if (procedureActs != null && procedureActs.Length > 0)
                    {
                        foreach (ProcedureActEntity procedureAct in procedureActs)
                        {
                            //Cargamos el ProcedureAct completo solo si es una prescripción
                            //ProcedureActEntity procedureAct = pa.IsPrescription
                            //    ? GetProcedureActByID(pa.ID, ProcedureActSubEntitiesFlagEnum.Resources)
                            //    : pa;

                            if (procedureAct == null)
                                continue;

                            //Cancelar el acto si es posterior a la fecha
                            if (procedureAct.StartDateTime.HasValue && procedureAct.StartDateTime > dateTimeLimit)
                            {
                                switch (procedureAct.Status)
                                {
                                    case ActionStatusEnum.Pending:
                                    case ActionStatusEnum.Waiting:
                                    case ActionStatusEnum.Scheduled:
                                        procedureAct.Status = ActionStatusEnum.Cancelled;
                                        procedureAct.EditStatus.Update();
                                        break;
                                    case ActionStatusEnum.Initiated:
                                    case ActionStatusEnum.Completed:
                                        procedureAct.Status = ActionStatusEnum.Aborted;
                                        procedureAct.EditStatus.Update();
                                        break;
                                    default:
                                        break;
                                }
                            }

                            //Cancelar los recursos (tomas) si son posteriores a la fecha
                            if (procedureAct.IsPrescription)
                            {
                                if (procedureAct.Resources != null)
                                {
                                    foreach (ProcedureActResourceRelEntity resource in procedureAct.Resources)
                                    {
                                        if (resource.RealizationDateTime > dateTimeLimit)
                                        {
                                            switch (resource.Status)
                                            {
                                                case ActionStatusEnum.Confirmed:
                                                    throw new InvalidOperationException(
                                                        string.Format(
                                                            Properties.Resources.ERROR_PrescriptionResourceOutOfEpisodeDateRange,
                                                            procedureAct.ProcedureName));

                                                case ActionStatusEnum.Completed:
                                                    resource.Status = ActionStatusEnum.Cancelled;
                                                    resource.ReasonChangeID = reasonChangeID;
                                                    resource.Explanation = explanation;
                                                    resource.EditStatus.Update();

                                                    procedureAct.EditStatus.Update();
                                                    AddReasonToProcedureActs(new ProcedureActEntity[] { procedureAct }, reasonChangeID, explanation);
                                                    break;
                                                case ActionStatusEnum.Cancelled:
                                                    if (resource.EditStatus.Value == StatusEntityValue.Updated)
                                                    {
                                                        resource.Status = ActionStatusEnum.Cancelled;
                                                        resource.ReasonChangeID = reasonChangeID;
                                                        resource.Explanation = explanation;

                                                        procedureAct.EditStatus.Update();
                                                        AddReasonToProcedureActs(new ProcedureActEntity[] { procedureAct }, reasonChangeID, explanation);
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }

                            //if (ProcedureActHasChanges(procedureAct))
                            //{
                            //    ProcedureActBL.Save(procedureAct, false, validationResults);
                            //}

                        }
                    }
                    //}
                }
            }
        }

        private void UndoCancelRoutineActsAfterDateTime(CustomerOrderRequestEntity customerOrderRequest,
            DateTime? dateTimeLimit, int reasonChangeID, string explanation, ValidationResults validationResults)
        {
            if (!dateTimeLimit.HasValue || customerOrderRequest == null
                || customerOrderRequest.OrderRequestSchPlanning == null
                || customerOrderRequest.OrderRequestSchPlanning.CustomerRoutines == null)
                return;

            //Cancelar RoutineActs
            if (customerOrderRequest.OrderRequestSchPlanning.CustomerRoutines != null)
            {
                foreach (CustomerRoutineEntity cr in customerOrderRequest.OrderRequestSchPlanning.CustomerRoutines)
                {
                    if (cr.Status == ActionStatusEnum.Pending
                        || cr.Status == ActionStatusEnum.Scheduled
                        || cr.Status == ActionStatusEnum.Initiated
                        || cr.Status == ActionStatusEnum.Completed)
                    {
                        RoutineActEntity[] routineActs = RoutineActBL.GetByCustomerRoutineID(cr.ID, false);
                        if (routineActs != null && routineActs.Length > 0)
                        {
                            foreach (RoutineActEntity ra in routineActs)
                            {
                                if (ra.StartDateTime.HasValue && ra.StartDateTime > dateTimeLimit)
                                {
                                    switch (ra.ActStatus)
                                    {
                                        case ActionStatusEnum.Pending:
                                        case ActionStatusEnum.Waiting:
                                        case ActionStatusEnum.Scheduled:
                                            ra.ActStatus = ActionStatusEnum.Cancelled;
                                            ra.EditStatus.Update();
                                            break;
                                        case ActionStatusEnum.Initiated:
                                        case ActionStatusEnum.Completed:
                                            ra.ActStatus = ActionStatusEnum.Aborted;
                                            ra.EditStatus.Update();
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                if (ra.EditStatus.Value != StatusEntityValue.None)
                                {
                                    RoutineActBL.Save(ra, false, validationResults);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UndoCancelActivityAfterDateTime(CustomerOrderRequestEntity customerOrderRequest,
            DateTime? dateTimeLimit, int reasonChangeID, string explanation,
            ValidationResults validationResults)
        {
            if (!dateTimeLimit.HasValue || customerOrderRequest == null
                || customerOrderRequest.OrderRequestSchPlanning == null
                || customerOrderRequest.OrderRequestSchPlanning.CustomerRoutines == null)
                return;

            //Cancelar RoutineActs
            CancelRoutineActsAfterDateTime(customerOrderRequest, dateTimeLimit,
                reasonChangeID, explanation, validationResults);

            //Cancelar ProcedureActs
            CancelProcedureActsAfterDateTime(customerOrderRequest, dateTimeLimit,
                reasonChangeID, explanation, validationResults);
        }

        private int GetCurrentLocation(int customerAdmissionID)
        {
            DataSet ds = DataAccess.CustomerAdmissionDA.GetCustomerAdmission(customerAdmissionID);
            if (ds != null && ds.Tables != null && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable)
                && ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable].Rows.Count > 0)
                return ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerAdmissionTable].Rows[0]["CurrentLocationID"] as int? ?? 0;

            return 0;
        }

        private bool ProcessChartHasRealizationSteps(ProcessChartEntity processChart)
        {
            if ((processChart != null) && (processChart.StepsInProcess != null))
            {
                return Array.Exists(processChart.StepsInProcess, (BasicStepsInProcessEntity step) => step.ProcessStep == BasicProcessStepsEnum.Realization);
            }
            return false;
        }

        private bool IsAnalyzed(CustomerOrderRequestEntity cor)
        {
            if (_analyzedCustomerOrders == null)
                return false;

            return AnalyzedCustomerOrders.IndexOf(cor) >= 0;
        }

        private int[] GetCustomerOrderRequestIDsByParentCustomerOrderRequestID(int parentCustomerOrderRequestID)
        {
            try
            {
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByParentCustomerOrderRequestID(parentCustomerOrderRequestID);
                if ((ds.Tables != null) && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    return (from row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                            select (row["ID"] as int? ?? 0)).ToArray();
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }

        private bool HasChildrensNotCompleted(CustomerOrderRequestEntity[] entities)
        {
            if ((entities == null) || (entities.Length <= 0))
                return false;

            return ((from cor in entities
                     where (cor.Status != ActionStatusEnum.Completed) && (cor.Status != ActionStatusEnum.Cancelled)
                     select cor).Count() > 0);
        }

        private void AnalyzeCompletedCustomerOrderRequestParent(CustomerOrderRequestEntity entity, CustomerOrderRequestEntity[] childrens)
        {
            List<CustomerOrderRequestEntity> list = new List<CustomerOrderRequestEntity>();

            if ((entity == null) || (entity.OrderControlCode != OrderControlCodeEnum.ParentOrder))
                return;

            if ((childrens == null) || (childrens.Length <= 0))
                return;

            if (HasChildrensNotCompleted(childrens))
                return;

            int countChildrens = (childrens != null) ? childrens.Length : 0;

            int countChildrensCancelled = ((childrens != null) && (childrens.Length > 0))
                ? ((from children in childrens where children.Status == ActionStatusEnum.Cancelled select children).Count())
                : 0;

            if (countChildrens == countChildrensCancelled)
            {
                //Actualizar el CustomerOrderRealization.EndDateTime si es distinto
                entity.Status = ActionStatusEnum.Cancelled;
                entity.EditStatus.Update();
            }
            else
            {
                //Actualizar el CustomerOrderRealization.EndDateTime si es distinto
                entity.Status = ActionStatusEnum.Completed;
                entity.EditStatus.Update();

                if (entity != null)
                {
                    CustomerOrderRequestAction action = new CustomerOrderRequestAction(
                        entity, CustomerOrderRequestActionEnum.Completed, AppointmentElementEnum.MedicalOrder,
                        entity, null, string.Empty);

                    NotificationActBL.HandleNotifications(action);
                }
            }
        }

        private CustomerProcessSpecification PreProcessCustomerProcessSpecification(CustomerProcessSpecification filter)
        {
            if (filter == null)
                return null;

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.Location))
            {
                List<int> locationIDs = new List<int>();
                if (filter.LocationIDs != null)
                    locationIDs.AddRange(filter.LocationIDs);
                if ((filter.LocationNames != null) && (filter.LocationNames.Length > 0))
                {
                    LocationBL locationBL = new LocationBL();
                    LocationBaseEntity[] myLocations = locationBL.GetListLocationBase(filter.LocationNames);
                    if (myLocations != null)
                        locationIDs.AddRange(myLocations.Select(loc => loc.ID).ToArray());
                }
                filter.LocationIDs = locationIDs.ToArray();
            }

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.LocationType))
            {
                List<int> locationTypeIDs = new List<int>();
                if (filter.LocationTypeIDs != null)
                    locationTypeIDs.AddRange(filter.LocationTypeIDs);
                if ((filter.LocationTypeCodes != null) && (filter.LocationTypeCodes.Length > 0))
                {
                    LocationTypeBL locationTypeBL = new LocationTypeBL();
                    LocationTypeBaseEntity[] myLocationTypes = locationTypeBL.GetListLocationTypes();
                    if (myLocationTypes != null)
                        locationTypeIDs.AddRange(myLocationTypes
                                                    .Where(lt => filter.LocationTypeCodes.Contains(lt.Code))
                                                    .Select(lt => lt.ID).ToArray());
                }
                filter.LocationTypeIDs = locationTypeIDs.ToArray();
            }

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.Order))
            {
                List<int> orderIDs = new List<int>();
                if (filter.OrderIDs != null)
                    orderIDs.AddRange(filter.OrderIDs);
                if ((filter.OrderNames != null) && (filter.OrderNames.Length > 0))
                {
                    OrderEntity[] myOrders = _orderCache.OrderCache.FindAll(order => filter.OrderNames.Contains(order.Name), true);
                    if (myOrders != null)
                        orderIDs.AddRange(myOrders.Select(ord => ord.ID).ToArray());
                }
                filter.OrderIDs = orderIDs.ToArray();
            }

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.Procedure))
            {
                List<int> procedureIDs = new List<int>();
                if (filter.ProcedureIDs != null)
                    procedureIDs.AddRange(filter.ProcedureIDs);
                if ((filter.ProcedureCodes != null) && (filter.ProcedureCodes.Length > 0))
                {
                    ProcedureEntity[] myProcedures = _procedureCache.ProcedureCache.FindAll(procedure => filter.ProcedureCodes.Contains(procedure.AssignedCode), true);
                    if (myProcedures != null)
                        procedureIDs.AddRange(myProcedures.Select(proc => proc.ID).ToArray());
                }
                filter.ProcedureIDs = procedureIDs.ToArray();
            }

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.AssistanceService))
            {
                List<int> assistanceServiceIDs = new List<int>();
                if (filter.AssistanceServiceIDs != null)
                    assistanceServiceIDs.AddRange(filter.AssistanceServiceIDs);
                if ((filter.AssistanceServiceCodes != null) && (filter.AssistanceServiceCodes.Length > 0))
                {
                    AssistanceServiceBL assistanceServiceBL = new AssistanceServiceBL();
                    AssistanceServiceEntity[] myAssistanceServices = assistanceServiceBL.GetAssistanceServicesByCodes(filter.AssistanceServiceCodes);
                    if (myAssistanceServices != null)
                        assistanceServiceIDs.AddRange(myAssistanceServices.Select(ass => ass.ID).ToArray());
                }
                filter.AssistanceServiceIDs = assistanceServiceIDs.ToArray();
            }

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.RoutineType))
            {
                List<int> routineTypeIDs = new List<int>();
                if (filter.RoutineTypeIDs != null)
                    routineTypeIDs.AddRange(filter.RoutineTypeIDs);
                if ((filter.RoutineTypeCodes != null) && (filter.RoutineTypeCodes.Length > 0))
                {
                    RoutineTypeEntity[] myRoutineTypes = _routineCache.RoutineTypeCache.FindAll(routineType => filter.RoutineTypeCodes.Contains(routineType.Code), true);
                    if (myRoutineTypes != null)
                        routineTypeIDs.AddRange(myRoutineTypes.Select(routt => routt.ID).ToArray());
                }
                filter.RoutineTypeIDs = routineTypeIDs.ToArray();
            }

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.Routine))
            {
                List<int> routineIDs = new List<int>();
                if (filter.RoutineIDs != null)
                    routineIDs.AddRange(filter.RoutineIDs);
                if ((filter.RoutineCodes != null) && (filter.RoutineCodes.Length > 0))
                {
                    RoutineEntity[] myRoutines = _routineCache.RoutineCache.FindAll(routine => filter.RoutineCodes.Contains(routine.Code), true);
                    if (myRoutines != null)
                        routineIDs.AddRange(myRoutines.Select(rout => rout.ID).ToArray());
                }
                filter.RoutineIDs = routineIDs.ToArray();
            }

            if (filter.IsFilteredByAny(CustomerProcessOptionsEnum.WorkGroup))
            {
                List<int> workGroupIDs = new List<int>();
                if (filter.WorkGroupIDs != null)
                    workGroupIDs.AddRange(filter.WorkGroupIDs);
                if ((filter.WorkGroupNames != null) && (filter.WorkGroupNames.Length > 0))
                {
                    WorkGroupBL workGroupBL = new WorkGroupBL();
                    WorkGroupEntity[] myWorkGroups = workGroupBL.GetWorkGroupByNames(filter.WorkGroupNames);
                    if (myWorkGroups != null)
                        workGroupIDs.AddRange(myWorkGroups.Select(work => work.ID).ToArray());
                }
                filter.WorkGroupIDs = workGroupIDs.ToArray();
            }

            return filter;
        }

        private T LoadParameterFromAppConfig<T>(string parameterName, T defaultValue)
        {
            string stringValue = ConfigurationManager.AppSettings[parameterName];
            if (string.IsNullOrWhiteSpace(stringValue))
                return defaultValue;

            if (typeof(T) == typeof(string))
                return (T)((object)(stringValue));

            Type type = typeof(T);
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static;
            Type[] parameterTypes = new Type[] { typeof(string) };
            MethodInfo method = type.GetMethod("Parse", bindingFlags, null, parameterTypes, null);
            if (method == null)
                return defaultValue;

            try
            {
                object result = method.Invoke(null, new object[] { stringValue });
                return (result is T)
                    ? (T)result
                    : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        private bool ThereAreNCareCentersWithCodeGenerator()
        {
            CommonEntities.ElementEntity _customerMetadata = GetElementByName(EntityNames.CustomerEntityName, ElementBL);
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

        private CommonEntities.ElementEntity GetElementByName(string entityName, ElementBL elementBL)
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

        private CommonEntities.AddInTokenBaseEntity[] GetAvailablePhoneticAddins()
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

        private string SetLocationData(int requirementEntityID)
        {
            if (requirementEntityID > 0)
            {
                LocationBL locationBL = new LocationBL();
                LocationBaseEntity location = locationBL.GetLocationBase(requirementEntityID);
                if (location != null)
                {
                    return location.Name;
                }
            }
            return string.Empty;
        }

        private string SetEquipmentData(int requirementEntityID)
        {
            if (requirementEntityID > 0)
            {
                EquipmentBL equipmentBL = new EquipmentBL();
                EquipmentBaseEntity equipment = equipmentBL.GetEquipmentBase(requirementEntityID);
                if (equipment != null)
                {
                    return equipment.Name;
                }
            }
            return string.Empty;
        }

        private string SetDeviceData(int requirementEntityID)
        {
            if (requirementEntityID > 0)
            {
                DeviceBL deviceBL = new DeviceBL();
                DeviceBaseEntity device = deviceBL.GetDeviceBase(requirementEntityID);
                if (device != null)
                {
                    return string.IsNullOrWhiteSpace(device.Code) ? device.Name : string.Concat("(", device.Code, ") ", device.Name);
                }
            }
            return string.Empty;
        }

        private string SetItemData(int requirementEntityID)
        {
            if (requirementEntityID > 0)
            {
                IItemCacheService itemCache = IoCFactory.CurrentContainer.Resolve<IItemCacheService>();
                ItemEntity item = itemCache.ItemCache.Get(requirementEntityID);
                if (item != null)
                {
                    return string.IsNullOrWhiteSpace(item.Code) ? item.GenericName : string.Concat("(", item.Code, ") ", item.GenericName);
                }
            }
            return string.Empty;
        }

        private string SetMaterialSpecimenData(int requirementEntityID)
        {
            if (requirementEntityID > 0)
            {
                MaterialSpecimenSampleBL materialSpecimenSampleBL = new MaterialSpecimenSampleBL();
                MaterialSpecimenSampleBaseEntity materialSpecimenSample = materialSpecimenSampleBL.GetMaterialSpecimenSampleBase(requirementEntityID);
                if (materialSpecimenSample != null)
                {
                    return materialSpecimenSample.Name;
                }
            }
            return string.Empty;
        }

        private string SetRoutineData(int requirementEntityID)
        {
            IRoutineCacheService routineCache = IoCFactory.CurrentContainer.Resolve<IRoutineCacheService>();
            if (requirementEntityID > 0)
            {
                RoutineEntity routine = routineCache.RoutineCache.Get(requirementEntityID);
                if (routine != null)
                {
                    return string.IsNullOrWhiteSpace(routine.Code) ? routine.Name : string.Concat("(", routine.Code, ") ", routine.Name);
                }
            }
            return string.Empty;
        }

        private string SetProcedureData(int requirementEntityID)
        {
            IProcedureCacheService procedureCache = IoCFactory.CurrentContainer.Resolve<IProcedureCacheService>();
            if (requirementEntityID > 0)
            {
                ProcedureEntity procedure = procedureCache.ProcedureCache.Get(requirementEntityID);
                if (procedure != null)
                {
                    return string.IsNullOrWhiteSpace(procedure.AssignedCode) ? procedure.Name : string.Concat("(", procedure.AssignedCode, ") ", procedure.Name);
                }
            }
            return string.Empty;
        }

        private string SetHumanResourceData(int requirementEntityID)
        {
            if (requirementEntityID > 0)
            {
                HumanResourceBL humanResourceBL = new HumanResourceBL();
                HumanResourceEntity humanResource = humanResourceBL.GetHHRR(requirementEntityID);
                if (humanResource != null)
                {
                    return string.IsNullOrWhiteSpace(humanResource.FileNumber) ? humanResource.Person.FullName : string.Concat("(", humanResource.FileNumber, ") ", humanResource.Person.FullName);
                }
            }
            return string.Empty;
        }

        private string SetPhysicianData(int requirementEntityID)
        {
            if (requirementEntityID > 0)
            {
                IPhysicianCacheService physicianCache = IoCFactory.CurrentContainer.Resolve<IPhysicianCacheService>();
                PhysicianEntity physician = physicianCache.PhysicianCache.Get(requirementEntityID);
                if (physician != null)
                {
                    return string.IsNullOrWhiteSpace(physician.CollegiateNumber) ? physician.Person.FullName : string.Concat("(", physician.CollegiateNumber, ") ", physician.Person.FullName);
                }
            }
            return string.Empty;
        }

        private CustomerOrderRequestEntity[] GetCustomerOrderRequestsByIDs(int[] ids, bool applyLOPD)
        {
            return GetCustomerOrderRequestsByIDs(ids, applyLOPD, true);
        }

        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByIDs(int[] ids, bool applyLOPD, bool loadObservations)
        {
            try
            {
                if ((ids == null) || (ids.Length == 0) || (!ids.Any(id => id > 0)))
                {
                    return null;
                }

                IEnumerable<CommonEntities.IDGenericEntity<int>> orderIDs = null;

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByIDs(ids);
                if ((ds.Tables != null)
                    && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    orderIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                       .Select(r => new CommonEntities.IDGenericEntity<int>(r.Field<int>("ID"), r.Field<int>("OrderID")));

                    DataSet ds2 = null;

                    #region Reason
                    MergeTable(DataAccess.CustomerOrderRequestReasonRelDA.GetByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable);
                    MergeTable(DataAccess.ReasonChangeDA.GetAllReasonChange(), ds, BackOffice.Entities.TableNames.ReasonChangeTable);
                    #endregion

                    #region PrescriptionRequest
                    ds2 = DataAccess.PrescriptionRequestDA.GetByCustomerOrderRequestIDs(ids);
                    if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable)
                        && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
                    {
                        MergeTable(ds2, ds, Administrative.Entities.TableNames.PrescriptionRequestTable);
                        MergeTable(DataAccess.PrescriptionRequestTimeDA.GetByCustomerOrderRequestIDs(ids),
                            ds, Administrative.Entities.TableNames.PrescriptionRequestTimeTable);

                        IEnumerable<int> pharmaceuticalFormIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("PharmaceuticalFormID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);
                        IEnumerable<int> administrationRouteIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationRouteID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);
                        IEnumerable<int> administrationMethodIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationMethodID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);

                        IEnumerable<int> physicalUnitIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("PhysUnitID"))
                                            .Where(i => i > 0)
                                            .Distinct()
                                            .OrderBy(i => i);


                        if (pharmaceuticalFormIDs.Any())
                        {
                            MergeTable(DataAccess.PharmaceuticalFormDA.GetPharmaceuticalFormByIDs(pharmaceuticalFormIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.PharmaceuticalFormTable);
                        }

                        if (administrationRouteIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationRouteDA.GetAdministrationRouteByIDs(administrationRouteIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationRouteTable);
                        }

                        if (administrationMethodIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationMethodDA.GetAdministrationMethodByIDs(administrationMethodIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationMethodTable);
                        }
                        if (physicalUnitIDs.Any())
                        {
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByIDs(physicalUnitIDs.ToArray()),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }

                        //aqui hay que poner ItemTreatmentOrderSequenceTable
                        #region Item Treatment Order Sequences
                        int[] prIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable]
                            .AsEnumerable()
                            .Where(r => (r["ID"] as int? ?? 0) > 0)
                            .Select(r => (r["ID"] as int? ?? 0))
                            .Distinct()
                            .OrderBy(id => id)
                            .ToArray();


                        MergeTable(DataAccess.LocationDA.GetLocationBaseByPrescriptionRequestIDs(prIDs),
                            ds, BackOffice.Entities.TableNames.LocationBaseTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptByPrescriptionRequestIDs(prIDs),
                            ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.EquipmentDA.GetEquipmentBaseByPrescriptionRequestIDs(prIDs),
                            ds, BackOffice.Entities.TableNames.EquipmentBaseTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByGiveStrengthUnitsByPrescriptionRequestIDs(prIDs),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);


                        ds2 = DataAccess.ItemTreatmentOrderSequenceDA.GetItemTreatmentOrderSequenceByPrescriptionRequestIDs(prIDs);
                        if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable))
                            && (ds2.Tables[BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable);

                            MergeTable(DataAccess.ItemDA.GetItemBaseByPrescriptionRequestIDs(prIDs),
                                ds, BackOffice.Entities.TableNames.ItemBaseTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveUnitsByPrescriptionRequestIDs(prIDs),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                            MergeTable(DataAccess.TimePatternDA.GetTimePatternBaseByRequestedGivePerTimeUnitByPrescriptionRequestIDs(prIDs),
                                ds, BackOffice.Entities.TableNames.TimePatternTable);
                        }
                        #endregion


                    }
                    #endregion

                    #region OrderRequestSchPlanning
                    ds2 = DataAccess.OrderRequestSchPlanningDA.GetOrderRequestSchPlanningByCustomerOrderRequestIDs(ids);
                    if ((ds2 != null) && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable)
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
                    {
                        MergeTable(ds2, ds, Administrative.Entities.TableNames.OrderRequestSchPlanningTable);
                        MergeTable(DataAccess.OrderRequestTimeDA.GetListOrderRequestTimeByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestTimeTable);

                        MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes),
                                ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);

                        //CustomerProcedures
                        ds2 = DataAccess.CustomerProcedureDA.GetCustomerProceduresByCustomerOrderRequestIDs(ids);
                        if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerProcedureTable)
                            && (ds2.Tables[Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, Administrative.Entities.TableNames.CustomerProcedureTable);
                            MergeTable(DataAccess.ProcedureDA.GetProcedureBasesOfCustomerProceduresByCustomerOrderRequestIDs(ids), ds, BackOffice.Entities.TableNames.ProcedureBaseTable);
                            MergeTable(DataAccess.CustomerProcedureRoutineRelDA.GetCustomerProcedureRoutineRelByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.CustomerProcedureRoutineRelTable);
                            MergeTable(DataAccess.CustomerProcedureTimeDA.GetCustomerProcedureTimesByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.CustomerProcedureTimeTable);
                            MergeTable(DataAccess.CustomerProcedureReasonRelDA.GetCustomerProcedureReasonRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.CustomerProcedureReasonRelTable);
                        }

                        //CustomerRoutines
                        ds2 = DataAccess.CustomerRoutineDA.GetCustomerRoutinesByCustomerOrderRequestIDs(ids);
                        if ((ds2 != null) && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable)
                            && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, Administrative.Entities.TableNames.CustomerRoutineTable);
                            MergeTable(DataAccess.RoutineDA.GetRoutinesBasesOfCustomerRoutinesByCustomerOrderRequestIDs(ids), ds, BackOffice.Entities.TableNames.RoutineBaseTable);
                            MergeTable(DataAccess.CustomerRoutineTimeDA.GetCustomerRoutineTimesByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.CustomerRoutineTimeTable);
                            MergeTable(DataAccess.CustomerRoutineReasonRelDA.GetCustomerRoutineReasonRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.CustomerRoutineReasonRelTable);
                        }

                        MergeTable(DataAccess.TimePatternDA.GetByTimestamp(0), ds, BackOffice.Entities.TableNames.TimePatternTable);
                    }
                    #endregion

                    #region OrderRequestProcedureRels
                    MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
                        MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
                    }
                    #endregion

                    #region OrderRequestRoutineRels
                    MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
                    if ((ds != null)
                        && ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable)
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
                    }
                    #endregion

                    #region OrderRequestHumanResourceRels
                    MergeTable(DataAccess.OrderRequestHumanResourceRelDA.GetOrderRequestHumanResourceRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ParticipateAsDA.GetParticipatesAsByCustomerOrderRequestIDs(ids),
                                    ds,
                                    BackOffice.Entities.TableNames.ParticipateAsTable);
                    }
                    #endregion

                    #region OrderRequestResourceRels
                    MergeTable(DataAccess.OrderRequestResourceRelDA.GetOrderRequestResourceRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestResourceRelTable);
                    #endregion

                    #region OrderRequestEquipmentRels
                    MergeTable(DataAccess.OrderRequestEquipmentRelDA.GetOrderRequestEquipmentRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestEquipmentRelTable);
                    #endregion

                    #region OrderRequestLocationRels
                    MergeTable(DataAccess.OrderRequestLocationRelDA.GetOrderRequestLocationRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestLocationRelTable);
                    #endregion

                    #region OrderRequestADTInfo
                    MergeTable(DataAccess.OrderRequestADTInfoDA.GetOrderRequestADTInfoByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestADTInfoTable);
                    #endregion

                    #region OrderRequestBodySiteRels
                    MergeTable(DataAccess.OrderRequestBodySiteRelDA.GetOrderRequestBodySiteRelsByCustomerOrderRequestIDs(ids), ds, Administrative.Entities.TableNames.OrderRequestBodySiteRelTable);
                    if ((ds != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestBodySiteRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestBodySiteRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.BodySiteDA.GetBodySitesByCustomerOrderRequestIDs(ids), ds, BackOffice.Entities.TableNames.BodySiteTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptsByCustomerOrderRequestIDs(ids), ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.BodySiteParticipationDA.GetBodySiteParticipationsByCustomerOrderRequestIDs(ids), ds, BackOffice.Entities.TableNames.BodySiteParticipationTable);
                    }
                    #endregion

                    #region OrderRequestRequirementRels
                    MergeTable(DataAccess.OrderRequestRequirementRelDA.GetOrderRequestRequirementRelsByCustomerOrderRequestIDs(ids),
                        ds, Administrative.Entities.TableNames.OrderRequestRequirementRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRequirementRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRequirementRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.RequirementDA.GetRequirementsByOrderRequestRequirementRelCustomerOrderRequests(ids),
                            ds, BackOffice.Entities.TableNames.RequirementTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByOrderRequestRequirementRelCustomerOrderRequests(ids),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                    }
                    #endregion

                    #region OrderRequestConsentRels
                    MergeTable(DataAccess.OrderRequestConsentRelDA.GetOrderRequestConsentRelsByCustomerOrderRequestIDs(ids),
                        ds, Administrative.Entities.TableNames.OrderRequestConsentRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ConsentPreprintDA.GetConsentPreprintsByOrderRequestConsentRelCustomerOrderRequests(ids),
                            ds, BackOffice.Entities.TableNames.ConsentPreprintTable);
                        if ((ds.Tables != null)
                            && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                            && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                        {
                            MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelConsentPreprintCustomerOrderRequests(ids),
                                ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                        }
                        MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelCustomerOrderRequests(ids),
                            ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                    }
                    #endregion

                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] result = adapter.GetData(ds);
                    if ((result != null) && (result.Length > 0))
                    {
                        _orderCache.OrderCache.UpdateCache();
                        foreach (CustomerOrderRequestEntity cor in result)
                        {
                            int orderID = orderIDs.Where(ord => ord.ID == cor.ID).Select(ord => ord.Data).FirstOrDefault();
                            cor.Order = _orderCache.OrderCache.Get(orderID, false);
                        }
                    }
                    if (loadObservations)
                    {
                        if ((result != null) && (result.Length > 0))
                        {
                            
                            _observationCache.UpdateCaches();
                            foreach (CustomerOrderRequestEntity cor in result)
                            {
                                //TODO: ver como optimizar la carga de observaciones

                                cor.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(cor.CustomerID, cor.ID, false);
                            }
                        }
                    }

                    if (applyLOPD)
                    {
                        if (result != null)
                        {
                            foreach (CustomerOrderRequestEntity item in result)
                            {
                                LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, item.ID, ActionType.View);
                            }
                        }
                    }
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

        private string GetAccessionNumberByCOR(CustomerOrderRequestEntity cor)
        {
            return (cor != null)
                 ? (cor.RoutineRels != null && cor.RoutineRels.Length > 0)
                     ? cor.RoutineRels[0].AccessionNumber
                     : (cor.ProcedureRels != null && cor.ProcedureRels.Length > 0)
                          ? cor.ProcedureRels[0].AccessionNumber
                          : cor.PlaceOrderNumber
                 : string.Empty;
        }

        #endregion

        #region hl7ormsendmessage
        private bool FirstValidate(bool returnIsProcessing, HL7MessagingProcessor hl7processor, ValidationResults vr)
        {
            //si hay errores no sigue    
            if (!vr.IsValid) return true;
            //si no existe el processor no sigue
            if (hl7processor == null) return true;
            //si no hay que procesar no sigue
            if (returnIsProcessing && hl7processor.GetMessageInProgess()) return true;
            return false;
        }

        private Dictionary<string, object> SetEntities(CustomerEntity customer,
            CustomerOrderRequestEntity cor, CustomerOrderRequestEntity parentcor,
            CustomerEpisodeEntity customerEpisode)
        {
            Dictionary<string, object> entities = new Dictionary<string, object>();
            entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, customer);
            entities.Add(CommonEntities.Constants.EntityNames.CustomerOrderRequestEntityName, cor);
            if (parentcor != null)
                entities.Add(CommonEntities.Constants.EntityNames.ParentCustomerOrderRequestEntityName, parentcor);
            if (customerEpisode != null)
                entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, customerEpisode);
            return entities;
        }

        private bool AnalysisSendORMMessage(CustomerOrderRequestAnalyzer _corAnalyzer,
            HL7IDISEventEnum hL7IDISEvent, bool returnIsProcessing, HL7MessagingProcessor hl7processor, ValidationResults vr)
        {
            if (FirstValidate(returnIsProcessing, hl7processor, vr)) return true;

            // se pasa el corAnalyzer o la CustomerOrderRequest por si viene de otro sitio 
            if (_corAnalyzer.CustomerOrderRequest == null || _corAnalyzer.CustomerOrderRequest.Order == null) return true;
            // se valida si no hay errores
            if (!vr.IsValid) return true;
            //investigo si hay metadato y tiene actores
            if (IDISMetadata == null || IDISMetadata.ExpectedAppActors == null || IDISMetadata.ExpectedAppActors.Count <= 0
                || IDISMetadata.IncludeOrders == null || IDISMetadata.IncludeOrders.Count <= 0) return true;
            //el customer es requerido    
            CustomerEntity customer = CustomerBL.GetCustomer(_corAnalyzer.CustomerOrderRequest.CustomerID);
            if (customer == null) return true;
            bool result = true;
            // una sola orden
            if (_corAnalyzer.ChildrenCustomerOrderRequests == null || _corAnalyzer.ChildrenCustomerOrderRequests.Count <= 0)
            {
                //mensaje primario
                if (hL7IDISEvent == HL7IDISEventEnum.NewOrder && IDISMetadata.HCDISAsActor != null
                     && IDISMetadata.HCDISAsActor.Roles != null && IDISMetadata.HCDISAsActor.Roles.Any(rol => rol == HL7ActorRolEnum.ORDERPLACER))
                {
                    _corAnalyzer.CustomerOrderRequest.PlaceOrderNumber =
                        StringUtils.Left(
                            string.Format("{0}^{1}", CodeGenerator.Generate(String.Empty, IDISMetadata.PlacerOrderNumber), IDISMetadata.HCDISAsActor.ActorName),
                            50);
                    _corAnalyzer.CustomerOrderRequest.EditStatus.Update();
                }
                result = hl7processor.AnalysisSendORMMessage(hL7IDISEvent, vr,
                        SetEntities(customer, _corAnalyzer.CustomerOrderRequest, null, _corAnalyzer.CustomerEpisode),
                        returnIsProcessing, _corAnalyzer.CustomerOrderRequest.Reason != null ? _corAnalyzer.CustomerOrderRequest.Reason.ReasonChange : null);
            }
            // una orden con hijos
            if (_corAnalyzer.ChildrenCustomerOrderRequests != null && _corAnalyzer.ChildrenCustomerOrderRequests.Count > 0)
            {
                foreach (CustomerOrderRequestEntity childCor in _corAnalyzer.ChildrenCustomerOrderRequests)
                {
                    Dictionary<string, object> entities = new Dictionary<string, object>();
                    //mensaje primario
                    if (hL7IDISEvent == HL7IDISEventEnum.NewOrder && IDISMetadata.HCDISAsActor != null
                         && IDISMetadata.HCDISAsActor.Roles != null && IDISMetadata.HCDISAsActor.Roles.Any(rol => rol == HL7ActorRolEnum.ORDERPLACER))
                    {
                        childCor.PlaceOrderNumber =
                            StringUtils.Left(
                                string.Format("{0}^{1}", CodeGenerator.Generate(String.Empty, IDISMetadata.PlacerOrderNumber), IDISMetadata.HCDISAsActor.ActorName),
                                50);
                        childCor.EditStatus.Update();
                        _corAnalyzer.CustomerOrderRequest.EditStatus.Update();
                    }
                    result = hl7processor.AnalysisSendORMMessage(hL7IDISEvent, vr,
                        SetEntities(customer, childCor, _corAnalyzer.CustomerOrderRequest, _corAnalyzer.CustomerEpisode),
                        returnIsProcessing, childCor.Reason != null ? childCor.Reason.ReasonChange : null) && result;
                }
            }
            return result || hL7IDISEvent.PrimaryEvent;
        }
        #endregion

        #region Protected methods

        #endregion

        #region Internal methods
        internal CommonEntities.ElementEntity GetElementByName(string elementName)
        {
            return ElementBL.GetElementByName(elementName);
        }

        internal string GetFrequencyOfApplicationMeaning(CustomerOrderRequestEntity entity)
        {
            if (entity == null || entity.OrderRequestSchPlanning == null)
                return string.Empty;

            if (entity.OrderRequestSchPlanning.FrequencyOfApplicationID <= 0)
                return entity.OrderRequestSchPlanning.FrequencyOfApplicationMeaning;

            TimePatternEntity tp = GetTimePatternByID(entity.OrderRequestSchPlanning.FrequencyOfApplicationID);

            return (tp != null) ? tp.Meaning : string.Empty;
        }

        internal ProcessChartEntity GetProcessChart(int processChartID)
        {
            if (processChartID <= 0)
                return null;

            ProcessChartEntity result = DataRepository
                .Find<ProcessChartEntity>(c => c.ID == processChartID);

            if (result == null)
            {
                result = ProcessChartBL.GetByID(processChartID);
                if (result != null)
                {
                    DataRepository.Add<ProcessChartEntity>(result);
                }
            }

            return result;
        }

        internal ProcessChartEntity GetProcessChartByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            int processChartID = DataAccess.ProcessChartDA.GetIDByCustomerEpisodeID(customerEpisodeID);
            return GetProcessChart(processChartID);
        }

        internal CustomerEpisodeEntity GetCustomerEpisode(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            CustomerEpisodeEntity result = DataRepository
                .Find<CustomerEpisodeEntity>(c => c.ID == customerEpisodeID);

            if (result == null)
            {
                result = CustomerEpisodeBL.GetFullCustomerEpisode(customerEpisodeID);
                if (result != null)
                {
                    DataRepository.Add<CustomerEpisodeEntity>(result);
                }
            }

            return result;
        }

        internal CustomerAssistancePlanEntity GetCustomerAssistancePlan(int customerAssistancePlanID)
        {
            if (customerAssistancePlanID <= 0)
                return null;

            CustomerAssistancePlanEntity result = DataRepository
                .Find<CustomerAssistancePlanEntity>(ap => ap.ID == customerAssistancePlanID);

            if (result == null)
            {
                result = CustomerAssistancePlanBL.GetByID(customerAssistancePlanID);
                DataRepository.Add<CustomerAssistancePlanEntity>(result);
            }

            return result;
        }

        internal CustomerAssistancePlanEntity GetCustomerAssistancePlanByCustomerEpisode(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            int customerAssistancePlanID = DataAccess.CustomerAssistancePlanDA
                .GetIDCurrentCustomerAssistancePlanIDByEpisodeID(customerEpisodeID);

            return GetCustomerAssistancePlan(customerAssistancePlanID);
        }

        internal CustomerMedEpisodeActEntity GetCustomerMedEpisodeAct(int customerMedEpisodeActID)
        {
            if (customerMedEpisodeActID <= 0)
                return null;

            CustomerMedEpisodeActEntity result = DataRepository
                .Find<CustomerMedEpisodeActEntity>(ap => ap.ID == customerMedEpisodeActID);

            if (result == null)
            {
                result = CustomerMedEpisodeActBL.Get(customerMedEpisodeActID);
                DataRepository.Add<CustomerMedEpisodeActEntity>(result);
            }

            return result;
        }

        internal bool ExistsProcessChartWithRealizationStep()
        {
            ProcessChartEntity[] processChartList = ProcessChartBL.GetActiveProcessCharts();
            if (processChartList == null || processChartList.Length <= 0)
                return false;

            foreach (ProcessChartEntity pc in processChartList)
            {
                if (ProcessChartHasRealizationSteps(pc))
                    return true;
            }

            return false;
        }

        internal LocationBaseEntity GetStorageLocation(int customerAdmissionID)
        {
            int currentLocationID = GetCurrentLocation(customerAdmissionID);
            if (currentLocationID <= 0)
                return null;

            return LocationBL.GetParentLocationBaseWithStorage(currentLocationID);
        }

        internal LocationEntity GetLocation(int locationID)
        {
            if (locationID <= 0)
                return null;

            LocationEntity result = DataRepository.Find<LocationEntity>(l => l.ID == locationID);

            if (result == null)
            {
                result = LocationBL.GetLocation(locationID, false, false);
                if (result != null)
                    DataRepository.Add<LocationEntity>(result);
            }

            return result;
        }

        internal RoutineEntity GetRoutine(int routineID)
        {
            if (routineID <= 0)
                return null;

            RoutineEntity result = DataRepository.Find<RoutineEntity>(r => r.ID == routineID);

            if (result == null)
            {
                result = _routineCache.RoutineCache.Get(routineID);
                if (result != null)
                    DataRepository.Add<RoutineEntity>(result);
            }

            return result;
        }

        internal ProcedureEntity GetProcedure(int procedureID)
        {
            if (procedureID <= 0)
                return null;

            ProcedureEntity result = DataRepository.Find<ProcedureEntity>(r => r.ID == procedureID);
            if (result == null)
            {
                result = _procedureCache.ProcedureCache.Get(procedureID);
                if (result != null)
                    DataRepository.Add<ProcedureEntity>(result);
            }

            return result;
        }

        internal ProcedureActEntity GetProcedureActByID(int id, ProcedureActSubEntitiesFlagEnum subEntities)
        {
            if (id <= 0)
                return null;

            return ProcedureActBL.Get(id, subEntities);
        }

        internal TimePatternBaseEntity GetTimePatternBase(int timePatternID)
        {
            if (timePatternID <= 0)
                return null;

            TimePatternEntity result = DataRepository.Find<TimePatternEntity>(r => r.ID == timePatternID);

            if (result == null)
            {
                result = TimePatternBL.GetByID(timePatternID);
                if (result != null)
                    DataRepository.Add<TimePatternEntity>(result);
            }

            return result;
        }

        internal TimePatternEntity GetTimePattern(int timePatternID)
        {
            if (timePatternID <= 0)
                return null;

            TimePatternEntity result = DataRepository.Find<TimePatternEntity>(r => r.ID == timePatternID);

            if (result == null)
            {
                result = TimePatternBL.GetByID(timePatternID);
                if (result != null)
                    DataRepository.Add<TimePatternEntity>(result);
            }

            return result;
        }

        internal int[] GetStorageLocations()
        {
            return StorageLocationIDs;
        }

        internal CustomerOrderRealizationEntity[] GetCustomerOrderRealizations(int customerOrderRequestID, bool withSubEntitiesActs)
        {
            if (customerOrderRequestID <= 0)
                return null;

            return CustomerOrderRealizationBL.GetCustomerOrderRealizationsByCustomerOrderRequest(customerOrderRequestID, withSubEntitiesActs);
        }

        internal void AnalyzerCustomerOrderRealizations(CustomerOrderRealizationEntity[] entities,
            CustomerOrderRequestEntity customerOrderRequest, out CustomerOrderRequestEntity parentCustomerOrderRequest)
        {
            parentCustomerOrderRequest = null;

            if ((entities == null) || (entities.Length <= 0) || (customerOrderRequest == null))
                return;

            CustomerOrderRealizationBL.AnalyzerCustomerOrderRealizations(entities, customerOrderRequest, out parentCustomerOrderRequest);
        }

        internal RoutineActEntity CancelRoutineAct(int routineActID)
        {
            if (routineActID <= 0)
                return null;

            return RoutineActBL.CancelFromOtherEntity(routineActID, AppointmentElementEnum.MedicalOrder);
        }

        internal RoutineActEntity SupersedeRoutineAct(int routineActID)
        {
            if (routineActID <= 0)
                return null;

            return RoutineActBL.SupersedeFromOtherEntity(routineActID, AppointmentElementEnum.MedicalOrder);
        }

        internal RoutineActEntity ActivateRoutineAct(int routineActID)
        {
            if (routineActID <= 0)
                return null;

            return RoutineActBL.ActiveFromOtherEntity(routineActID, AppointmentElementEnum.MedicalOrder);
        }



        internal RoutineActEntity AbortRoutineAct(int routineActID)
        {
            if (routineActID <= 0)
                return null;

            return RoutineActBL.AbortFromOtherEntity(routineActID, AppointmentElementEnum.MedicalOrder);
        }

        internal ProcedureActEntity RejectProcedureAct(int procedureActID)
        {
            if (procedureActID <= 0)
                return null;

            return ProcedureActBL.RejectFromOtherEntity(procedureActID, AppointmentElementEnum.MedicalOrder);
        }

        internal ProcedureActEntity SupersedeProcedureAct(int procedureActID)
        {
            if (procedureActID <= 0)
                return null;

            return ProcedureActBL.SupersedeFromOtherEntity(procedureActID, AppointmentElementEnum.MedicalOrder);
        }

        internal ProcedureActEntity ActivateProcedureAct(int procedureActID)
        {
            if (procedureActID <= 0)
                return null;

            return ProcedureActBL.ActivateFromOtherEntity(procedureActID, AppointmentElementEnum.MedicalOrder);
        }


        
        internal ProcedureActEntity CancelProcedureAct(int procedureActID)
        {
            if (procedureActID <= 0)
                return null;

            return ProcedureActBL.CancelFromOtherEntity(procedureActID, AppointmentElementEnum.MedicalOrder);
        }

        internal ProcedureActEntity AbortProcedureAct(int procedureActID, DateTime? abortDateTime = null)
        {
            if (procedureActID <= 0)
                return null;

            return ProcedureActBL.AbortFromOtherEntity(procedureActID, AppointmentElementEnum.MedicalOrder, abortDateTime);
        }

        internal ProcedureActEntity FindProcedureAct(int customerProcedureID, DateTime estimatedDateTime)
        {
            if (customerProcedureID <= 0)
                return null;

            ProcedureActEntity result = DataRepository
                            .Find<ProcedureActEntity>(
                                    pa => pa.CustomerProcedureID == customerProcedureID
                                          && DateUtils.AreEqual(pa.EstimatedDateTime, estimatedDateTime));

            if (result == null)
            {
                result = ProcedureActBL.GetProcedureActByCustomerProcedure(customerProcedureID, estimatedDateTime);
                if (result != null)
                {
                    DataRepository.Add<ProcedureActEntity>(result);
                }
            }

            return result;
        }

        internal CustomerRoutineEntity CancelCustomerRoutine(CustomerRoutineEntity customerRoutine)
        {
            if (customerRoutine == null)
                return null;

            customerRoutine.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerRoutineBL.CancelEntity(customerRoutine);
        }

        internal CustomerRoutineEntity SupersedeCustomerRoutine(CustomerRoutineEntity customerRoutine)
        {
            if (customerRoutine == null)
                return null;

            customerRoutine.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerRoutineBL.SupersedeEntity(customerRoutine);
        }

        internal CustomerRoutineEntity ActivateCustomerRoutine(CustomerRoutineEntity customerRoutine)
        {
            if (customerRoutine == null)
                return null;

            customerRoutine.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerRoutineBL.ActivateEntity(customerRoutine);
        }


        internal CustomerRoutineEntity AbortCustomerRoutine(CustomerRoutineEntity customerRoutine)
        {
            if (customerRoutine == null)
                return null;

            customerRoutine.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerRoutineBL.AbortEntity(customerRoutine);
        }

        internal CustomerProcedureEntity CancelCustomerProcedure(CustomerProcedureEntity customerProcedure)
        {
            if (customerProcedure == null)
                return null;

            customerProcedure.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerProcedureBL.CancelEntity(customerProcedure);
        }

        internal CustomerProcedureEntity SupersedeCustomerProcedure(CustomerProcedureEntity customerProcedure)
        {
            if (customerProcedure == null)
                return null;

            customerProcedure.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerProcedureBL.SupersedeEntity(customerProcedure);
        }

        internal CustomerProcedureEntity ActivateCustomerProcedure(CustomerProcedureEntity customerProcedure)
        {
            if (customerProcedure == null)
                return null;

            customerProcedure.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerProcedureBL.ActivateEntity(customerProcedure);
        }


        internal CustomerProcedureEntity AbortCustomerProcedure(CustomerProcedureEntity customerProcedure)
        {
            if (customerProcedure == null)
                return null;

            customerProcedure.Source = AppointmentElementEnum.MedicalOrder;

            return CustomerProcedureBL.AbortEntity(customerProcedure);
        }

        internal int GetNumberOrRoutines(CustomerOrderRequestEntity entity)
        {
            if (entity == null || entity.RoutineRels == null || entity.RoutineRels.Length <= 0)
                return 0;

            return (from r in entity.RoutineRels
                    where r.EditStatus.Value != StatusEntityValue.Deleted
                        && r.EditStatus.Value != StatusEntityValue.NewAndDeleted
                    select r).Count();
        }

        internal int GetNumberOfProcedures(CustomerOrderRequestEntity entity)
        {
            if (entity == null || entity.ProcedureRels == null || entity.ProcedureRels.Length <= 0)
                return 0;

            return (from p in entity.ProcedureRels
                    where p.EditStatus.Value != StatusEntityValue.Deleted
                        && p.EditStatus.Value != StatusEntityValue.NewAndDeleted
                    select p).Count();
        }

        #region Find methods
        internal CustomerOrderRequestEntity FindCustomerOrderRequest(int customerOrderRequestID)
        {
            if (customerOrderRequestID <= 0)
                return null;

            CustomerOrderRequestEntity result = null;

            //Buscar si ya esta en el DataRepository.
            result = DataRepository.Find<CustomerOrderRequestEntity>(cor => cor.ID == customerOrderRequestID);

            //Si no esta en el DataRepository buscar en la base de Datos
            if (result == null)
            {
                result = GetCustomerOrderRequest(customerOrderRequestID, false);
                if (result != null)
                {
                    DataRepository.Add<CustomerOrderRequestEntity>(result);
                }
            }

            //Buscar en repositorio de datos los CustomerRoutines y CustomerProcedures
            if (result != null && result.OrderRequestSchPlanning != null)
            {
                if (result.OrderRequestSchPlanning.CustomerRoutines != null)
                {
                    for (int i = result.OrderRequestSchPlanning.CustomerRoutines.Length - 1; i >= 0; i--)
                    {
                        CustomerRoutineEntity cr = DataRepository
                            .Find<CustomerRoutineEntity>(
                                c => c.ID == result.OrderRequestSchPlanning.CustomerRoutines[i].ID);
                        if (cr != null)
                        {
                            result.OrderRequestSchPlanning.CustomerRoutines[i] = cr;
                        }
                        else
                        {
                            DataRepository.Add<CustomerRoutineEntity>(result.OrderRequestSchPlanning.CustomerRoutines[i]);
                        }
                    }
                }
                if (result.OrderRequestSchPlanning.CustomerProcedures != null)
                {
                    for (int i = result.OrderRequestSchPlanning.CustomerProcedures.Length - 1; i >= 0; i--)
                    {
                        CustomerProcedureEntity cr = DataRepository
                            .Find<CustomerProcedureEntity>(
                                c => c.ID == result.OrderRequestSchPlanning.CustomerProcedures[i].ID);
                        if (cr != null)
                        {
                            result.OrderRequestSchPlanning.CustomerProcedures[i] = cr;
                        }
                        else
                        {
                            DataRepository.Add<CustomerProcedureEntity>(result.OrderRequestSchPlanning.CustomerProcedures[i]);
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        internal CustomerOrderRequestEntity AnalyzeCompletedCustomerOrderRequestParent(int corParentID)
        {
            if (corParentID <= 0)
                return null;

            CustomerOrderRequestEntity parentCOR = FindCustomerOrderRequest(corParentID);

            if ((parentCOR == null) || (parentCOR.OrderControlCode != OrderControlCodeEnum.ParentOrder))
                return null;

            int[] corChildrenIds = GetCustomerOrderRequestIDsByParentCustomerOrderRequestID(parentCOR.ID);

            List<CustomerOrderRequestEntity> childrensCOR = new List<CustomerOrderRequestEntity>();

            if ((corChildrenIds != null) && (corChildrenIds.Length > 0))
            {
                foreach (int id in corChildrenIds)
                {
                    if (!childrensCOR.Exists(e => e.ID == id))
                    {
                        CustomerOrderRequestEntity aux = FindCustomerOrderRequest(id);
                        if (aux != null)
                            childrensCOR.Add(aux);
                    }
                }
            }

            CustomerOrderRequestEntity[] childrens = childrensCOR.ToArray();

            AnalyzeCompletedCustomerOrderRequestParent(parentCOR, childrens);

            return parentCOR;
        }

        internal int GetCareCenterIDByCustomerEpisode(int customerEpisodeID)
        {
            return CareCenterBL.GetCareCenterIDByCustomerEpisode(customerEpisodeID);
        }

        internal string PeekCodeGenerator(int careCenterID, int elementID, int elementAttributeID)
        {
            return ProcessChartBL.PeekCodeGenerator(careCenterID, elementID, elementAttributeID, 0, ProcessChartCodeGeneratorsEnum.None);
        }
        #endregion

        #region ORM handlers
        #region ORMHandler<CustomerOrderRequestEntity> implementation
        private void CustomerOrderRequestNew(CustomerOrderRequestEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.CustomerOrderRequestDA.Insert(
                            entity.Order.ID, //No se controla porque Order es obligatorio
                            entity.OrderNumber,
                            entity.PlaceOrderNumber,
                            entity.CustomerID,
                            entity.CustomerEpisodeID,
                            entity.CustomerMedEpisodeActID,
                            entity.RequestedPersonID,
                            entity.RequestedInsurerID,
                            entity.PolicyTypeID,
                            entity.RequestedLocationID,
                            entity.RequestedCareCenterID,
                            entity.AssistanceServiceID,
                            entity.MedicalSpecialtyID,
                            entity.RelevantClinicalInfo,
                            entity.PresumptiveDiagnosis,
                            entity.RequestDateTime,
                            entity.RequestEffectiveAtDateTime,
                            entity.EpisodeAssistanceServiceID,
                            entity.AttendingPhysicianID,
                            entity.RequestAppointmentID,
                            entity.RequestExplanation,
                            (int)entity.OrderControlCode,
                            (int)entity.ResponseFlag,
                            (int)entity.Priority,
                            entity.CriticalTimeID,
                            entity.OrderPrinted,
                            entity.ParentCustomerOrderRequestID,
                            entity.RegistrationDateTime,
                            entity.Placed,
                            (int)entity.Status,
                            entity.ModifiedBy != string.Empty ? entity.ModifiedBy : GetIdentityUserName());

            //Actualiza el ID en las subentidades dependientes
            if (entity.Reason != null)
                entity.Reason.CustomerOrderRequestID = entity.ID;

            if (entity.OrderRequestSchPlanning != null)
                entity.OrderRequestSchPlanning.CustomerOrderRequestID = entity.ID;

            if (entity.ProcedureRels != null && entity.ProcedureRels.Length > 0)
            {
                foreach (OrderRequestProcedureRelEntity item in entity.ProcedureRels)
                    item.CustomerOrderRequestID = entity.ID;
            }

            if (entity.RoutineRels != null && entity.RoutineRels.Length > 0)
            {
                foreach (OrderRequestRoutineRelEntity item in entity.RoutineRels)
                    item.CustomerOrderRequestID = entity.ID;
            }

            if (entity.Prescription != null)
                entity.Prescription.CustomerOrderRequestID = entity.ID;

            if (entity.ADTRequestInfo != null)
                entity.ADTRequestInfo.CustomerOrderRequestID = entity.ID;

            //Observations
            if (entity.RegisteredObservations != null)
            {
                RegisteredObservationValueEntity[] observations = entity.RegisteredObservations.GetAllRegisteredObservationValue();
                if ((observations != null) && (observations.Length > 0))
                {
                    foreach (RegisteredObservationValueEntity obs in observations)
                    {
                        obs.EntityType = ElementTypeEnum.RequestOrder;
                        obs.EntityActID = entity.ID;
                    }
                }
            }


            if (entity.HumanResourceRels != null && entity.HumanResourceRels.Length > 0)
            {
                foreach (OrderRequestHumanResourceRelEntity item in entity.HumanResourceRels)
                    item.CustomerOrderRequestID = entity.ID;
            }
            if (entity.ResourceRels != null && entity.ResourceRels.Length > 0)
            {
                foreach (OrderRequestResourceRelEntity item in entity.ResourceRels)
                    item.CustomerOrderRequestID = entity.ID;
            }
            if (entity.EquipmentRels != null && entity.EquipmentRels.Length > 0)
            {
                foreach (OrderRequestEquipmentRelEntity item in entity.EquipmentRels)
                    item.CustomerOrderRequestID = entity.ID;
            }
            if (entity.LocationRels != null && entity.LocationRels.Length > 0)
            {
                foreach (OrderRequestLocationRelEntity item in entity.LocationRels)
                    item.CustomerOrderRequestID = entity.ID;
            }

            if (entity.RequirementRels != null && entity.RequirementRels.Length > 0)
            {
                foreach (OrderRequestRequirementRelEntity item in entity.RequirementRels)
                    item.CustomerOrderRequestID = entity.ID;
            }

            if (entity.BodySiteRels != null && entity.BodySiteRels.Length > 0)
            {
                foreach (OrderRequestBodySiteRelEntity item in entity.BodySiteRels)
                    item.CustomerOrderRequestID = entity.ID;
            }

            if (entity.Consents != null && entity.Consents.Length > 0)
            {
                foreach (OrderRequestConsentRelEntity item in entity.Consents)
                    item.CustomerOrderRequestID = entity.ID;
            }

            DataAccess.OrderDA.SetOrderInUse(entity.Order.ID, GetIdentityUserName());

            //LOPD
            LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName,
                entity.ID, ActionType.Create);
        }

        private void CustomerOrderRequestUpdate(CustomerOrderRequestEntity entity)
        {
            if (entity == null)
                return;

            long currentDBTimestamp = GetDBTimestamp(entity.ID);
            if (currentDBTimestamp != entity.DBTimeStamp)
                throw new DBConcurrencyException(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, entity.ID));

            //Llama al método Update del DA
            DataAccess.CustomerOrderRequestDA.Update(
                            entity.ID,
                            entity.Order.ID, //No se controla porque Order es obligatorio
                            entity.OrderNumber,
                            entity.PlaceOrderNumber,
                            entity.CustomerID,
                            entity.CustomerEpisodeID,
                            entity.CustomerMedEpisodeActID,
                            entity.RequestedPersonID,
                            entity.RequestedInsurerID,
                            entity.PolicyTypeID,
                            entity.RequestedLocationID,
                            entity.RequestedCareCenterID,
                            entity.AssistanceServiceID,
                            entity.MedicalSpecialtyID,
                            entity.RelevantClinicalInfo,
                            entity.PresumptiveDiagnosis,
                            entity.RequestDateTime,
                            entity.RequestEffectiveAtDateTime,
                            entity.EpisodeAssistanceServiceID,
                            entity.AttendingPhysicianID,
                            entity.RequestAppointmentID,
                            entity.RequestExplanation,
                            (int)entity.OrderControlCode,
                            (int)entity.ResponseFlag,
                            (int)entity.Priority,
                            entity.CriticalTimeID,
                            entity.OrderPrinted,
                            entity.ParentCustomerOrderRequestID,
                            entity.RegistrationDateTime,
                            entity.Placed,
                            (int)entity.Status,
                            GetIdentityUserName());

            DataAccess.OrderDA.SetOrderInUse(entity.Order.ID, GetIdentityUserName());

            //LOPD
            LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName,
                entity.ID, ActionType.Modify);
        }

        private void CustomerOrderRequestDelete(CustomerOrderRequestEntity entity)
        {
            if (entity == null)
                return;

            long currentDBTimestamp = GetDBTimestamp(entity.ID);
            if (currentDBTimestamp != entity.DBTimeStamp)
                throw new DBConcurrencyException(
                    string.Format(Properties.Resources.MSG_ConcurrencyUpdateWarning, entity.ID));

            int numberOfRows = DataAccess.CustomerOrderRequestDA.Delete(entity.ID);
            if (numberOfRows <= 0)
                throw new ArgumentException(
                    string.Format(Properties.Resources.ERROR_CustomerOrderNotFoundOrNotPending,
                    entity.ID, entity.OrderNumber));

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable,
                "ID", entity.ID, "OrderNumber", entity.OrderNumber,
                GetIdentityUserName());

            //LOPD
            LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName,
                entity.ID, ActionType.Delete);
        }
        #endregion

        #region ORMHandler<BasicCodeGeneratorRelationship<CustomerOrderRequestEntity>> implementation
        private void BasicCodeGeneratorRelationshipCustomerOrderRequestNew(
            CommonEntities.BasicCodeGeneratorRelationship<CustomerOrderRequestEntity> entity)
        {
            if (entity == null || entity.CodeGenerators.Count <= 0)
                return;

            string codeGeneratorName;
            if (entity.CodeGenerators.TryGetValue("OrderNumber", out codeGeneratorName))
            {
                entity.Entity.OrderNumber = CodeGenerator.Generate(String.Empty, codeGeneratorName);
            }
        }
        #endregion

        #region ORMHandler<CustomerOrderRequestReasonRelEntity> implementation
        private void CustomerOrderRequestReasonRelNew(CustomerOrderRequestReasonRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.CustomerOrderRequestReasonRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.ReasonChange.ID, //No hay control de nulo por ser obligatorio
                            entity.Explanation,
                            GetIdentityUserName());
        }

        private void CustomerOrderRequestReasonRelUpdate(CustomerOrderRequestReasonRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            DataAccess.CustomerOrderRequestReasonRelDA.Update(
                    entity.ID,
                    entity.CustomerOrderRequestID,
                    entity.ReasonChange.ID, //No hay control de nulo por ser obligatorio
                    entity.Explanation,
                    GetIdentityUserName());
        }

        private void CustomerOrderRequestReasonRelDelete(CustomerOrderRequestReasonRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.CustomerOrderRequestReasonRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestSchPlanningEntity> implementation
        private void OrderRequestSchPlanningNew(OrderRequestSchPlanningEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestSchPlanningDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.CustomerID,
                            entity.OrderEffectiveAt,
                            entity.EstimatedFinalizeAt,
                            entity.EstimatedDurationID,
                            (int)entity.AvailabilityPattern,
                            entity.FrequencyOfApplicationID,
                            entity.FrequencyOfApplicationMeaning,
                            entity.RepeatPattern,
                            entity.RepeatDuringID,
                            entity.Recomendations,
                            entity.NoticeRequires,
                            entity.NoticePreviousTimeID,
                            entity.NoticeMessage,
                            (int)entity.Status,
                            entity.LastUpdated,
                            entity.ModifiedBy!=string.Empty ? entity.ModifiedBy : GetIdentityUserName());

            //Actualiza identificadores de entidades dependientes
            if (entity.OrderRequestTimes != null)
            {
                foreach (OrderRequestTimeEntity item in entity.OrderRequestTimes)
                    item.OrderRequestSchPlanningID = entity.ID;
            }

            if ((entity.CustomerProcedures != null) && (entity.CustomerProcedures.Length > 0))
            {
                foreach (CustomerProcedureEntity item in entity.CustomerProcedures)
                    item.CustomerOrderRequestID = entity.CustomerOrderRequestID;
            }

            if ((entity.CustomerRoutines != null) && (entity.CustomerRoutines.Length > 0))
            {
                foreach (CustomerRoutineEntity item in entity.CustomerRoutines)
                    item.CustomerOrderRequestID = entity.CustomerOrderRequestID;
            }
        }

        private void OrderRequestSchPlanningUpdate(OrderRequestSchPlanningEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            DataAccess.OrderRequestSchPlanningDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.CustomerID,
                entity.OrderEffectiveAt,
                entity.EstimatedFinalizeAt,
                entity.EstimatedDurationID,
                (int)entity.AvailabilityPattern,
                entity.FrequencyOfApplicationID,
                entity.FrequencyOfApplicationMeaning,
                entity.RepeatPattern,
                entity.RepeatDuringID,
                entity.Recomendations,
                entity.NoticeRequires,
                entity.NoticePreviousTimeID,
                entity.NoticeMessage,
                (int)entity.Status,
                entity.LastUpdated,
                entity.ModifiedBy != string.Empty ? entity.ModifiedBy : GetIdentityUserName());
        }

        private void OrderRequestSchPlanningDelete(OrderRequestSchPlanningEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestSchPlanningDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestTimeEntity> implementation
        private void OrderRequestTimeNew(OrderRequestTimeEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestTimeDA.Insert(
                            entity.OrderRequestSchPlanningID,
                            entity.Time,
                            GetIdentityUserName());
        }

        private void OrderRequestTimeUpdate(OrderRequestTimeEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestTimeDA.Update(
                entity.ID,
                entity.OrderRequestSchPlanningID,
                entity.Time,
                GetIdentityUserName());
        }

        private void OrderRequestTimeDelete(OrderRequestTimeEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable,
                "ID", entity.ID, "OrderRequestSchPlanningID", entity.OrderRequestSchPlanningID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestTimeDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestProcedureRelEntity> implementation
        private void OrderRequestProcedureRelNew(OrderRequestProcedureRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestProcedureRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.ProcedureID,
                            (int)entity.RelationType,
                            (int)entity.ResponseFlag,
                            entity.Order,
                            entity.Priority,
                            entity.HasCriterion,
                            entity.AccessionNumber,
                            entity.UniqueIdentifier,
                            (int)entity.AvailabilityPattern,
                            entity.FrequencyOfApplicationID,
                            entity.FrequencyOfApplicationMeaning,
                            entity.RepeatPattern,
                            entity.RepeatDuringID,
                            GetIdentityUserName());

            //Actualiza identificadores de entidades dependientes
            if (entity.Routines != null)
            {
                foreach (OrderRequestProcedureRoutineRelEntity item in entity.Routines)
                    item.OrderRequestProcedureRelID = entity.ID;
            }
        }

        private void OrderRequestProcedureRelUpdate(OrderRequestProcedureRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestProcedureRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.ProcedureID,
                (int)entity.RelationType,
                (int)entity.ResponseFlag,
                entity.Order,
                entity.Priority,
                entity.HasCriterion,
                entity.AccessionNumber,
                entity.UniqueIdentifier,
                (int)entity.AvailabilityPattern,
                entity.FrequencyOfApplicationID,
                entity.FrequencyOfApplicationMeaning,
                entity.RepeatPattern,
                entity.RepeatDuringID,
                GetIdentityUserName());
        }

        private void OrderRequestProcedureRelDelete(OrderRequestProcedureRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestProcedureRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestProcedureRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestProcedureRoutineRelEntity> implementation
        private void OrderRequestProcedureRoutineRelNew(OrderRequestProcedureRoutineRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestProcedureRoutineRelDA.Insert(
                            entity.OrderRequestProcedureRelID,
                            entity.RoutineID,
                            GetIdentityUserName());
        }

        private void OrderRequestProcedureRoutineRelUpdate(OrderRequestProcedureRoutineRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestProcedureRoutineRelDA.Update(
                entity.ID,
                entity.OrderRequestProcedureRelID,
                entity.RoutineID,
                GetIdentityUserName());
        }

        private void OrderRequestProcedureRoutineRelDelete(OrderRequestProcedureRoutineRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable,
                "ID", entity.ID, "OrderRequestProcedureRelID", entity.OrderRequestProcedureRelID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestProcedureRoutineRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestRoutineRelEntity> implementation
        private void OrderRequestRoutineRelNew(OrderRequestRoutineRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestRoutineRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.RoutineID,
                            (int)entity.RelationType,
                            (int)entity.ResponseFlag,
                            entity.Order,
                            entity.Priority,
                            entity.HasCriterion,
                            entity.AccessionNumber,
                            entity.UniqueIdentifier,
                            (int)entity.AvailabilityPattern,
                            entity.FrequencyOfApplicationID,
                            entity.FrequencyOfApplicationMeaning,
                            entity.RepeatPattern,
                            entity.RepeatDuringID,
                //(int)entity.Status,
                            GetIdentityUserName());
        }

        private void OrderRequestRoutineRelUpdate(OrderRequestRoutineRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestRoutineRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.RoutineID,
                (int)entity.RelationType,
                (int)entity.ResponseFlag,
                entity.Order,
                entity.Priority,
                entity.HasCriterion,
                entity.AccessionNumber,
                entity.UniqueIdentifier,
                (int)entity.AvailabilityPattern,
                entity.FrequencyOfApplicationID,
                entity.FrequencyOfApplicationMeaning,
                entity.RepeatPattern,
                entity.RepeatDuringID,
                //(int)entity.Status,
                GetIdentityUserName());
        }

        private void OrderRequestRoutineRelDelete(OrderRequestRoutineRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestRoutineRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestRoutineRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestCustomerRoutineRelationship> implementation
        private void OrderRequestCustomerRoutineRelationshipNew(OrderRequestCustomerRoutineRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Child.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_TransientState);

            //LLama al método Insert del DA
            DataAccess.OrderRequestCustomerRoutineRelDA.Insert(
                            entity.Parent.ID,
                            entity.Child.ID,
                            GetIdentityUserName());
        }

        private void OrderRequestCustomerRoutineRelationshipUpdate(OrderRequestCustomerRoutineRelationship entity)
        {
            //No se usa
        }

        private void OrderRequestCustomerRoutineRelationshipDelete(OrderRequestCustomerRoutineRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Child.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_TransientState);

            DataAccess.OrderRequestCustomerRoutineRelDA.Delete(entity.Parent.ID, entity.Child.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestCustomerProcedureRelationship> implementation
        private void OrderRequestCustomerProcedureRelationshipNew(OrderRequestCustomerProcedureRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Child.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_TransientState);

            //LLama al método Insert del DA
            DataAccess.OrderRequestCustomerProcedureRelDA.Insert(
                            entity.Parent.ID,
                            entity.Child.ID,
                            GetIdentityUserName());
        }

        private void OrderRequestCustomerProcedureRelationshipUpdate(OrderRequestCustomerProcedureRelationship entity)
        {
            //No se usa
        }

        private void OrderRequestCustomerProcedureRelationshipDelete(OrderRequestCustomerProcedureRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Child.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_TransientState);

            DataAccess.OrderRequestCustomerProcedureRelDA.Delete(entity.Parent.ID, entity.Child.ID);
        }
        #endregion

        #region ORMHandler<CustomerRoutineRoutineActRelationship> implementation
        private void CustomerRoutineRoutineActRelationshipNew(CustomerRoutineRoutineActRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Parent.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_TransientState);

            entity.Child.CustomerRoutineID = entity.Parent.ID;
        }

        private void CustomerRoutineRoutineActRelationshipUpdate(CustomerRoutineRoutineActRelationship entity)
        {
            //No se usa
        }

        private void CustomerRoutineRoutineActRelationshipDelete(CustomerRoutineRoutineActRelationship entity)
        {
            //No se usa
        }
        #endregion

        #region ORMHandler<CustomerProcedureProcedureActRelationship> implementation
        private void CustomerProcedureProcedureActRelationshipNew(CustomerProcedureProcedureActRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Parent.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_TransientState);

            entity.Child.CustomerProcedureID = entity.Parent.ID;
        }

        private void CustomerProcedureProcedureActRelationshipUpdate(CustomerProcedureProcedureActRelationship entity)
        {
            //No se usa
        }

        private void CustomerProcedureProcedureActRelationshipDelete(CustomerProcedureProcedureActRelationship entity)
        {
            //No se usa
        }
        #endregion

        #region ORMHandler<PrescriptionRequestCustomerProcedureRelationship> implementation
        private void PrescriptionRequestCustomerProcedureRelationshipNew(PrescriptionRequestCustomerProcedureRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Parent.ID <= 0
                || entity.Child.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_TransientState);

            DataAccess.PrescriptionRequestDA.UpdateByCustomerProcedure(entity.Parent.ID, entity.Child.ID, GetIdentityUserName());
            //TODO ALBERTO: No borrar (Si el PrescriptionRequest esta marcado como Udpated puede que resetee el CustomerProcedure al guardarse)
            //Esto puede suceder tras haber guardado una orden sin confirmar, luego modificar algo de la prescripcion y guardar confirmando.
            entity.Parent.CustomerProcedureID = entity.Child.ID;
        }

        private void PrescriptionRequestCustomerProcedureRelationshipUpdate(PrescriptionRequestCustomerProcedureRelationship entity)
        {
            //No se usa
        }

        private void PrescriptionRequestCustomerProcedureRelationshipDelete(PrescriptionRequestCustomerProcedureRelationship entity)
        {
            if (entity == null)
                return;

            entity.Parent.CustomerProcedureID = 0;
        }
        #endregion

        #region ORMHandler<PrescriptionRequestEntity> implementation
        private void PrescriptionRequestNew(PrescriptionRequestEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.PrescriptionRequestDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.CustomerProcedureID,
                            entity.RequestedPersonID,
                            entity.CurrentLocationID,
                            entity.LocationID,
                            entity.ItemID,
                            entity.IncludeInitialDose,
                            entity.InitialDoseUnits,
                            entity.InformativePrescription,
                            entity.UnitaryQuantity,
                            entity.UnitaryDose,
                            entity.DayDose,
                            entity.TotalDose,
                            entity.AdministrationRouteID,
                            entity.AdministrationMethodID,
                            (int)entity.PhysicianValidateStatus,
                            (int)entity.PharmacistValidateStatus,
                            entity.PredecessorID,
                            entity.AllowSubstitute,
                            entity.SupplySupervised,
                            entity.StartDateTime,
                            entity.EndDateTime,
                            (int)entity.Dispatchment,
                            entity.LastDispatchDateTime,
                            entity.EstimatedDurationLastDispatch,
                ////////////////////////////////////////////////////////////////////
                //VIENE DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                            entity.PharmacyOrderType,
                            entity.PharmacyTreatmentInstructions,
                            entity.AdministrationInstructions,
                            entity.DeliveryToLocation != null ? entity.DeliveryToLocation.ID : 0,
                            entity.RequiresPharmacistTreatmentVerifier,
                            entity.NeedsHumanReview,
                            entity.BodySite != null ? entity.BodySite.ID : 0,
                            entity.AdministrationDevice != null ? entity.AdministrationDevice.ID : 0,
                            entity.RequestedGiveStrength,
                            entity.RequestedGiveStrengthUnits != null ? entity.RequestedGiveStrengthUnits.ID : 0,
                            entity.FrequencyDescription,
                            entity.EFarmacoID,
                            //FIN DE LA LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                ////////////////////////////////////////////////////////////////////

                            entity.Status,
                            entity.ModifiedBy!=string.Empty ? entity.ModifiedBy : GetIdentityUserName());
            ////////////////////////////////////////////////////////////////////
            //VIENE DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS

            //if ((entity.TreatmentRoutes != null) && (entity.TreatmentRoutes.Length > 0))
            //{
            //    foreach (PrescriptionRequestItemSequenceRelEntity item in entity.TreatmentRoutes)
            //        item.PrescriptionRequestID = entity.ID;
            //}


            if ((entity.ItemSequence != null) && (entity.ItemSequence.Length > 0))
            {
                foreach (ItemTreatmentOrderSequenceEntity item in entity.ItemSequence)
                    item.PrescriptionRequestID = entity.ID;
            }

            //FIN DE LA LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
            ////////////////////////////////////////////////////////////////////
        }

        private void PrescriptionRequestUpdate(PrescriptionRequestEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.PrescriptionRequestDA.Updated(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.CustomerProcedureID,
                entity.RequestedPersonID,
                entity.CurrentLocationID,
                entity.LocationID,
                entity.ItemID,
                entity.IncludeInitialDose,
                entity.InitialDoseUnits,
                entity.InformativePrescription,
                entity.UnitaryQuantity,
                entity.UnitaryDose,
                entity.DayDose,
                entity.TotalDose,
                entity.AdministrationRouteID,
                entity.AdministrationMethodID,
                (int)entity.PhysicianValidateStatus,
                (int)entity.PharmacistValidateStatus,
                entity.PredecessorID,
                entity.AllowSubstitute,
                entity.SupplySupervised,
                entity.StartDateTime,
                entity.EndDateTime,
                (int)entity.Dispatchment,
                entity.LastDispatchDateTime,
                entity.EstimatedDurationLastDispatch,

                ////////////////////////////////////////////////////////////////////
                //VIENE DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                entity.PharmacyOrderType,
                entity.PharmacyTreatmentInstructions,
                entity.AdministrationInstructions,
                entity.DeliveryToLocation != null ? entity.DeliveryToLocation.ID : 0,
                entity.RequiresPharmacistTreatmentVerifier,
                entity.NeedsHumanReview,
                entity.BodySite != null ? entity.BodySite.ID : 0,
                entity.AdministrationDevice != null ? entity.AdministrationDevice.ID : 0,
                entity.RequestedGiveStrength,
                entity.RequestedGiveStrengthUnits != null ? entity.RequestedGiveStrengthUnits.ID : 0,
                entity.FrequencyDescription,
                entity.EFarmacoID,
                //FIN DE LA LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                ////////////////////////////////////////////////////////////////////

                entity.Status,
                entity.MeaningBeforeSuperceded,
                entity.ModifiedBy != string.Empty ? entity.ModifiedBy : GetIdentityUserName());
        }

        private void PrescriptionRequestDelete(PrescriptionRequestEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                Administrative.Entities.TableNames.PrescriptionRequestTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.PrescriptionRequestDA.Delete(entity.ID);
        }
        #endregion


        #region ORMHandler<ItemTreatmentOrderSequenceEntity> implementation
        private void ItemTreatmentOrderSequenceNew(ItemTreatmentOrderSequenceEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.ItemTreatmentOrderSequenceDA.Insert(
                            entity.ItemTreatmentRouteID,
                            entity.PrescriptionRequestID,
                            entity.OrderInSequence,
                            entity.ItemComponentType,
                            entity.Item.ID,
                            entity.AllowSubstitution,
                            entity.RequestedGiveAmountMinimum,
                            entity.RequestedGiveAmountMaximum,
                            entity.RequestedGiveUnits != null ? entity.RequestedGiveUnits.ID : 0,
                            entity.RequestedGivePerTimeUnit != null ? entity.RequestedGivePerTimeUnit.ID : 0,
                            entity.RequestedGivePerTimeUnit != null ? entity.RequestedGivePerTimeUnit.Meaning : string.Empty,
                            entity.RequestedGiveStrength,
                            entity.RequestedGiveStrengthUnits != null ? entity.RequestedGiveStrengthUnits.ID : 0,
                ////////////////////////////////////////////////////////////////////
                //VIENE DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                            entity.RequestedGiveStrengthVolume,
                            entity.RequestedGiveStrengthVolumeUnits != null ? entity.RequestedGiveStrengthVolumeUnits.ID : 0,
                            entity.GiveQtyToDispense,
                //FIN DE LA LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                ////////////////////////////////////////////////////////////////////
                            entity.Status,
                            entity.ModifiedBy != string.Empty ? entity.ModifiedBy : GetIdentityUserName());
        }

        private void ItemTreatmentOrderSequenceUpdate(ItemTreatmentOrderSequenceEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.ItemTreatmentOrderSequenceDA.Update(
                            entity.ID,
                            entity.ItemTreatmentRouteID,
                            entity.PrescriptionRequestID,
                            entity.OrderInSequence,
                            entity.ItemComponentType,
                            entity.Item.ID,
                            entity.AllowSubstitution,
                            entity.RequestedGiveAmountMinimum,
                            entity.RequestedGiveAmountMaximum,
                            entity.RequestedGiveUnits != null ? entity.RequestedGiveUnits.ID : 0,
                            entity.RequestedGivePerTimeUnit != null ? entity.RequestedGivePerTimeUnit.ID : 0,
                            entity.RequestedGivePerTimeUnit != null ? entity.RequestedGivePerTimeUnit.Meaning : string.Empty,
                            entity.RequestedGiveStrength,
                            entity.RequestedGiveStrengthUnits != null ? entity.RequestedGiveStrengthUnits.ID : 0,
                ////////////////////////////////////////////////////////////////////
                //VIENE DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                            entity.RequestedGiveStrengthVolume,
                            entity.RequestedGiveStrengthVolumeUnits != null ? entity.RequestedGiveStrengthVolumeUnits.ID : 0,
                            entity.GiveQtyToDispense,
                //FIN DE LA LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
                ////////////////////////////////////////////////////////////////////
                            entity.Status,
                            entity.ModifiedBy != string.Empty ? entity.ModifiedBy : GetIdentityUserName());
        }

        private void ItemTreatmentOrderSequenceDelete(ItemTreatmentOrderSequenceEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable,
                "ID", entity.ID, "PrescriptionRequestID", entity.PrescriptionRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.ItemTreatmentOrderSequenceDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestADTInfoEntity> implementation
        private void OrderRequestADTInfoNew(OrderRequestADTInfoEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestADTInfoDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.ADTRequestAction,
                            entity.CareCenterID,
                            entity.ProcessChartID,
                            entity.LocationTypeID,
                            entity.LocationClassID,
                            entity.LocationID,
                            GetIdentityUserName());
        }

        private void OrderRequestADTInfoUpdate(OrderRequestADTInfoEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestADTInfoDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.ADTRequestAction,
                entity.CareCenterID,
                entity.ProcessChartID,
                entity.LocationTypeID,
                entity.LocationClassID,
                entity.LocationID,
                GetIdentityUserName());
        }

        private void OrderRequestADTInfoDelete(OrderRequestADTInfoEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.OrderRequestADTInfoDA.Delete(entity.ID);
            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(Administrative.Entities.TableNames.OrderRequestADTInfoTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(), GetIdentityUserName());
        }
        #endregion

        #region ORMHandler<OrderRequestCustomerObservationRelationship> implementation
        private void OrderRequestCustomerObservationRelationshipNew(OrderRequestCustomerObservationRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Child.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_ObservationValueIsInTransientState);

            //LLama al método Insert del DA
            DataAccess.OrderRequestCustomerObservationRelDA.Insert(
                            entity.Parent.ID,
                            entity.Child.ID,
                            GetIdentityUserName());
        }

        private void OrderRequestCustomerObservationRelationshipUpdate(OrderRequestCustomerObservationRelationship entity)
        {
            //No se usa
        }

        private void OrderRequestCustomerObservationRelationshipDelete(OrderRequestCustomerObservationRelationship entity)
        {
            if (entity == null)
                return;

            if (entity.Child.ID <= 0)
                throw new InvalidOperationException(
                    Properties.Resources.ERROR_ObservationValueIsInTransientState);

            DataAccess.OrderRequestCustomerObservationRelDA.Delete(entity.Parent.ID, entity.Child.ID);
        }
        #endregion

        #region ORMHandler<CustomerOrderRequestRoutineActRelationship>
        private void CustomerOrderRequestRoutineActRelationshipInsert(
            CustomerOrderRequestRoutineActRelationship entity)
        {
            if (entity == null)
                return;

            entity.Child.CustomerOrderRequestID = entity.Parent.ID;
        }

        private void CustomerOrderRequestRoutineActRelationshipUpdate(
            CustomerOrderRequestRoutineActRelationship entity)
        {
            if (entity == null)
                return;

            entity.Child.CustomerOrderRequestID = entity.Parent.ID;
        }

        private void CustomerOrderRequestRoutineActRelationshipDelete(
            CustomerOrderRequestRoutineActRelationship entity)
        {
            return;
        }
        #endregion

        #region ORMHandler<CustomerOrderRequestProcedureActRelationship>
        private void CustomerOrderRequestProcedureActRelationshipInsert(
            CustomerOrderRequestProcedureActRelationship entity)
        {
            if (entity == null)
                return;

            entity.Child.CustomerOrderRequestID = entity.Parent.ID;
        }

        private void CustomerOrderRequestProcedureActRelationshipUpdate(
            CustomerOrderRequestProcedureActRelationship entity)
        {
            if (entity == null)
                return;

            entity.Child.CustomerOrderRequestID = entity.Parent.ID;
        }

        private void CustomerOrderRequestProcedureActRelationshipDelete(
            CustomerOrderRequestProcedureActRelationship entity)
        {
            return;
        }
        #endregion

        #region ORMHandler<OrderRequestHumanResourceRelEntity> implementation
        private void OrderRequestHumanResourceRelNew(OrderRequestHumanResourceRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestHumanResourceRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.HumanResourceID,
                            entity.PersonID,
                            entity.ProfileID,
                            (entity.ParticipateAs != null) ? entity.ParticipateAs.ID : 0,
                            (int)entity.EntityType,
                            entity.EntityID,
                            GetIdentityUserName());
        }

        private void OrderRequestHumanResourceRelUpdate(OrderRequestHumanResourceRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestHumanResourceRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.HumanResourceID,
                entity.PersonID,
                entity.ProfileID,
                (entity.ParticipateAs != null) ? entity.ParticipateAs.ID : 0,
                (int)entity.EntityType,
                entity.EntityID,
                GetIdentityUserName());
        }

        private void OrderRequestHumanResourceRelDelete(OrderRequestHumanResourceRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable,
                                                                    "ID", entity.ID,
                                                                    "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                                                                    GetIdentityUserName());
            DataAccess.OrderRequestHumanResourceRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestResourceRelEntity> implementation
        private void OrderRequestResourceRelNew(OrderRequestResourceRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestResourceRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.ItemID,
                            entity.Quantity,
                            (int)entity.EntityType,
                            entity.EntityID,
                            GetIdentityUserName());
        }

        private void OrderRequestResourceRelUpdate(OrderRequestResourceRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestResourceRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.ItemID,
                entity.Quantity,
                (int)entity.EntityType,
                entity.EntityID,
                GetIdentityUserName());
        }

        private void OrderRequestResourceRelDelete(OrderRequestResourceRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestResourceRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestResourceRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestEquipmentRelEntity> implementation
        private void OrderRequestEquipmentRelNew(OrderRequestEquipmentRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestEquipmentRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.EquipmentID,
                            (int)entity.EntityType,
                            entity.EntityID,
                            GetIdentityUserName());
        }

        private void OrderRequestEquipmentRelUpdate(OrderRequestEquipmentRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestEquipmentRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.EquipmentID,
                (int)entity.EntityType,
                entity.EntityID,

                GetIdentityUserName());
        }

        private void OrderRequestEquipmentRelDelete(OrderRequestEquipmentRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestEquipmentRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestEquipmentRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestLocationRelEntity> implementation
        private void OrderRequestLocationRelNew(OrderRequestLocationRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestLocationRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.LocationID,
                            GetIdentityUserName());
        }

        private void OrderRequestLocationRelUpdate(OrderRequestLocationRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestLocationRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.LocationID,
                GetIdentityUserName());
        }

        private void OrderRequestLocationRelDelete(OrderRequestLocationRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestLocationRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestLocationRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<ParentCustomerOrderRequestChildCustomerOrderRequestRelationship>
        private void ParentCustomerOrderRequestChildCustomerOrderRequestRelationshipNew(
            ParentCustomerOrderRequestChildCustomerOrderRequestRelationship entity)
        {
            if (entity == null)
                return;

            entity.Child.ParentCustomerOrderRequestID = entity.Parent.ID;
        }
        #endregion

        #region ORMHandler<OrderRequestBodySiteRelEntity> implementation
        private void OrderRequestBodySiteRelNew(OrderRequestBodySiteRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestBodySiteRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            (entity.BodySiteConcept != null) ? entity.BodySiteConcept.ID : 0,
                            (entity.BodySiteParticipation != null) ? entity.BodySiteParticipation.ID : 0,
                            (entity.BodySite != null) ? entity.BodySite.ID : 0,
                            (int)entity.EntityType,
                            entity.EntityID,
                            entity.RequestComments,
                            GetIdentityUserName());

            //Actualiza identificadores de entidades dependientes
        }

        private void OrderRequestBodySiteRelUpdate(OrderRequestBodySiteRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestBodySiteRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                (entity.BodySiteConcept != null) ? entity.BodySiteConcept.ID : 0,
                (entity.BodySiteParticipation != null) ? entity.BodySiteParticipation.ID : 0,
                (entity.BodySite != null) ? entity.BodySite.ID : 0,
                (int)entity.EntityType,
                entity.EntityID,
                entity.RequestComments,
                GetIdentityUserName());
        }

        private void OrderRequestBodySiteRelDelete(OrderRequestBodySiteRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                Administrative.Entities.TableNames.OrderRequestProcedureRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestBodySiteRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestRequirementRelEntity> implementation
        private void OrderRequestRequirementRelNew(OrderRequestRequirementRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestRequirementRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.Requirement != null ? entity.Requirement.ID : 0,
                            entity.RequirementEntityID,
                            entity.Required,
                            entity.EntityType,
                            entity.EntityID,
                            entity.Quantity,
                            entity.PhysUnit != null ? entity.PhysUnit.ID : 0,
                            entity.WhenToUse,
                            entity.RequestComments,
                            GetIdentityUserName());

            //Actualiza identificadores de entidades dependientes
        }

        private void OrderRequestRequirementRelUpdate(OrderRequestRequirementRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestRequirementRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.Requirement != null ? entity.Requirement.ID : 0,
                entity.RequirementEntityID,
                entity.Required,
                entity.EntityType,
                entity.EntityID,
                entity.Quantity,
                entity.PhysUnit != null ? entity.PhysUnit.ID : 0,
                entity.WhenToUse,
                entity.RequestComments,
                GetIdentityUserName());
        }

        private void OrderRequestRequirementRelDelete(OrderRequestRequirementRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestRequirementRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestRequirementRelDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<OrderRequestConsentRelEntity> implementation
        private void OrderRequestConsentRelNew(OrderRequestConsentRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Insert del DA
            entity.ID = DataAccess.OrderRequestConsentRelDA.Insert(
                            entity.CustomerOrderRequestID,
                            entity.ConsentType != null ? entity.ConsentType.ID : 0,
                            entity.Consent != null ? entity.Consent.ID : 0,
                            entity.EntityType,
                            entity.EntityID,
                            entity.ConsentMode,
                            entity.SignedByID,
                            entity.ReceivedByID,
                            entity.StepWasRegistered,
                            entity.ConsentRejected,
                            entity.ConsentRejectedExplanation,
                            entity.RescindedDate,
                            entity.RegistrationDateTime,
                            entity.Status,
                            GetIdentityUserName());

            //Actualiza identificadores de entidades dependientes
        }

        private void OrderRequestConsentRelUpdate(OrderRequestConsentRelEntity entity)
        {
            if (entity == null)
                return;

            //LLama al método Update del DA
            DataAccess.OrderRequestConsentRelDA.Update(
                entity.ID,
                entity.CustomerOrderRequestID,
                entity.ConsentType != null ? entity.ConsentType.ID : 0,
                entity.Consent != null ? entity.Consent.ID : 0,
                entity.EntityType,
                entity.EntityID,
                entity.ConsentMode,
                entity.SignedByID,
                entity.ReceivedByID,
                entity.StepWasRegistered,
                entity.ConsentRejected,
                entity.ConsentRejectedExplanation,
                entity.RescindedDate,
                entity.RegistrationDateTime,
                entity.Status,
                GetIdentityUserName());
        }

        private void OrderRequestConsentRelDelete(OrderRequestConsentRelEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.RecordDeletedLogDA.NewRecordDeletedLogIDBased(
                SII.HCD.Administrative.Entities.TableNames.OrderRequestConsentRelTable,
                "ID", entity.ID, "CustomerOrderRequestID", entity.CustomerOrderRequestID.ToString(),
                GetIdentityUserName());
            DataAccess.OrderRequestConsentRelDA.Delete(entity.ID);
        }
        #endregion
        #endregion

        #region Analysis methods
        private void ValidateDateTimeFinalization(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if ((entity == null) || (entity.OrderRequestSchPlanning == null) || !entity.OrderRequestSchPlanning.EstimatedFinalizeAt.HasValue)
                return;

            if (!entity.OrderRequestSchPlanning.OrderEffectiveAt.HasValue
                || (entity.OrderRequestSchPlanning.OrderEffectiveAt > entity.OrderRequestSchPlanning.EstimatedFinalizeAt))
                validationResults.AddResult(new ValidationResult(
                    string.Format(Properties.Resources.ERROR_OrderEndDateTimeBeforeStartDateTime, entity.OrderNumber),
                    this, null, null, null));
        }

        private void CheckLocation(CustomerOrderRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || entity.Prescription == null)
                return;

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    if (entity.CustomerEpisodeID > 0)
                    {
                        LocationBL _locationBL = new LocationBL();
                        int currentLocationID = _locationBL.GetLocationByCustomerEpisode(entity.CustomerEpisodeID);
                        if (entity.Prescription.CurrentLocationID != currentLocationID)
                        {
                            entity.Prescription.CurrentLocationID = currentLocationID;
                            entity.Prescription.EditStatus.Update();
                        }
                    }
                    break;
                default: break;
            }
        }

        private void CheckCodeGenerators(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity.EditStatus.Value != StatusEntityValue.New)
                return;

            if (CustomerOrderRequestMetadata != null)
            {
                int careCenterID = entity.RequestedCareCenterID;
                int elementID = CustomerOrderRequestMetadata.ID;
                CommonEntities.AttributeEntity attribute = CustomerOrderRequestMetadata.GetAttribute("OrderNumber");
                int elementAttributeID = (attribute != null)
                    ? attribute.ID
                    : 0;
                string codeGenerator = ProcessChartBL.GetCodeGeneratorName(
                    careCenterID, elementID, elementAttributeID, 0, ProcessChartCodeGeneratorsEnum.None);

                if (!string.IsNullOrWhiteSpace(codeGenerator))
                {
                    CommonEntities.BasicCodeGeneratorRelationship<CustomerOrderRequestEntity> rel =
                        new CommonEntities.BasicCodeGeneratorRelationship<CustomerOrderRequestEntity>(entity);
                    rel.AddCodeGenerator("OrderNumber", codeGenerator);
                    rel.EditStatus.New();
                    HandleBasicActions<CommonEntities.BasicCodeGeneratorRelationship<CustomerOrderRequestEntity>>(rel, validationResults);
                }
            }
        }

        private TimePatternEntity GetTimePatternByID(int timePatternID)
        {
            if (timePatternID <= 0)
                return null;

            TimePatternEntity result = DataRepository.Find<TimePatternEntity>(tp => tp.ID == timePatternID);

            if (result == null)
            {
                result = TimePatternBL.GetByID(timePatternID);
                if (result != null)
                    DataRepository.Add<TimePatternEntity>(result);
            }

            return result;
        }

        private int GetNumberOfTimes(OrderRequestSchPlanningEntity entity)
        {
            if ((entity == null) || (entity.OrderRequestTimes == null))
                return 0;

            return (from ot in entity.OrderRequestTimes
                    where ot.EditStatus.Value == StatusEntityValue.New ||
                          ot.EditStatus.Value == StatusEntityValue.Updated ||
                          ot.EditStatus.Value == StatusEntityValue.None
                    select ot).Count();
        }

        private void ValidateProcedureRel(OrderRequestProcedureRelEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null && (entity.EditStatus.Value == StatusEntityValue.New
                || entity.EditStatus.Value == StatusEntityValue.Updated))
                return;

            if (!ValidProcedureIDs.Contains(entity.ProcedureID))
                validationResults.AddResult(
                    new ValidationResult(
                        string.Format(
                            Properties.Resources.ERROR_InvalidStateInProcedureProgramming,
                            entity.ProcedureID, entity.ProcedureAssignedCode, entity.ProcedureName),
                        this, null, null, null));

            if (entity.Routines != null && entity.Routines.Length > 0)
            {
                foreach (OrderRequestProcedureRoutineRelEntity item in entity.Routines)
                    ValidateProcedureRoutineRel(item, validationResults);
            }
        }

        private void ValidateProcedureRoutineRel(OrderRequestProcedureRoutineRelEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null && (entity.EditStatus.Value == StatusEntityValue.New
                || entity.EditStatus.Value == StatusEntityValue.Updated))
                return;

            if (!ValidRoutineIDs.Contains(entity.RoutineID))
                validationResults.AddResult(
                    new ValidationResult(
                        string.Format(
                            Properties.Resources.ERROR_InvalidStateInProcedureRoutineProgramming,
                            entity.RoutineID, entity.RoutineCode, entity.RoutineName),
                        this, null, null, null));
        }

        private void ValidateRoutineRel(OrderRequestRoutineRelEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null && (entity.EditStatus.Value == StatusEntityValue.New
                || entity.EditStatus.Value == StatusEntityValue.Updated))
                return;

            if (!ValidRoutineIDs.Contains(entity.RoutineID))
                validationResults.AddResult(
                    new ValidationResult(
                        string.Format(
                            Properties.Resources.ERROR_InvalidStateInRoutineProgramming,
                            entity.RoutineID, entity.RoutineAssignedCode, entity.RoutineName),
                        this, null, null, null));
        }

        private void ValidatePriority(CustomerOrderRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null)
                return;

            switch (entity.Priority)
            {
                case OrderPriorityEnum.HighestPriority:
                case OrderPriorityEnum.ASAP:
                case OrderPriorityEnum.Routine:
                case OrderPriorityEnum.Preop:
                case OrderPriorityEnum.TimingCritical:
                    break;
                case OrderPriorityEnum.AsNeeded:
                    //if ((entity.OrderRequestSchPlanning != null) && ((entity.OrderRequestSchPlanning.FrequencyOfApplicationID > 0)
                    //    || !string.IsNullOrWhiteSpace(entity.OrderRequestSchPlanning.FrequencyOfApplicationMeaning)))
                    //    validationResults.AddResult(new ValidationResult(
                    //        string.Format(Properties.Resources.ERROR_InvalidCustomerOrderPriorityAsNeededWithScheduled,
                    //            entity.OrderNumber, OrderPriorityEnumNames.GetName(entity.Priority)),
                    //        this, null, null, null));
                    break;
                default: break;
            }
        }

        private void ValidateProgramming(CustomerOrderRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || entity.Order == null)
                return;

            ValidatePriority(entity, validationResults);

            //Si es prescripción, no se hacen verificaciones
            if (entity.Order.OrderCase == OrderClassTypeEnum.DrugPrescription)
                return;

            if ((entity.Order.OrderCase == OrderClassTypeEnum.ADTInstructions) && (entity.Order.ADTRequestAction != ADTRequestActionEnum.PreAssessmentRequest)
                && ((entity.OrderRequestSchPlanning.FrequencyOfApplicationID > 0)
                        || !string.IsNullOrWhiteSpace(entity.OrderRequestSchPlanning.FrequencyOfApplicationMeaning)))
                validationResults.AddResult(new ValidationResult(
                        string.Format(Properties.Resources.ERROR_InvalidCustomerOrderProgrammingADTInstruction, entity.OrderNumber,
                            ADTRequestActionEnumNames.GetName(entity.Order.ADTRequestAction)),
                        this, null, null, null));

            if ((entity.Order.Procedures != null && entity.Order.Procedures.Length > 0)
                || (entity.Order.Routines != null && entity.Order.Routines.Length > 0))
            {
                if (GetNumberOfProcedures(entity) <= 0 && GetNumberOrRoutines(entity) <= 0)
                    validationResults.AddResult(new ValidationResult(
                        Properties.Resources.ERROR_InvalidCustomerOrderProgrammingNoProgramming,
                        this, null, null, null));
            }

            if (entity.ProcedureRels != null && entity.ProcedureRels.Length > 0)
            {
                foreach (OrderRequestProcedureRelEntity procedure in entity.ProcedureRels)
                    ValidateProcedureRel(procedure, validationResults);
            }

            if (entity.RoutineRels != null && entity.RoutineRels.Length > 0)
            {
                foreach (OrderRequestRoutineRelEntity routine in entity.RoutineRels)
                    ValidateRoutineRel(routine, validationResults);
            }
        }

        private void ValidateCustomerOrderRequest(CustomerOrderRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || validationResults == null)
                return;

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    ValidateEntity<CustomerOrderRequestEntity>(entity, validationResults,
                        Helpers.CustomerOrderRequestHelper.Validate);
                    break;
                case StatusEntityValue.Updated:
                    ValidateEntity<CustomerOrderRequestEntity>(entity, validationResults,
                        Helpers.CustomerOrderRequestHelper.Validate);
                    break;
                case StatusEntityValue.Deleted:
                    //Se verificara en el ORM durante el borrado que el estado es Pending
                    break;
                default:
                    break;
            }
        }

        private void ValidateRequiredOrderFlags(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null || entity.Order == null)
            {
                return;
            }

            ToRequestOrderFlagsEnum requiredFlags = (ToRequestOrderFlagsEnum)entity.Order.ToRequestRequiredOrderFlags;

            foreach (ToRequestOrderFlagsEnum flag in Enum.GetValues(typeof(ToRequestOrderFlagsEnum)))
            {
                switch (requiredFlags & flag)
                {
                    case ToRequestOrderFlagsEnum.RequestingCareCenter:
                        if (entity.RequestedCareCenterID == 0)
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_RequestedCareCenterRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.RequestingAssistanceService:
                        if (entity.AssistanceServiceID == 0)
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_RequestedAssistanceServiceRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.RequestingMedicalSpecialty:
                        if (entity.MedicalSpecialtyID == 0)
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_RequestedMedicalSpecialtyRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.WithPresumptiveDiagnosis:
                        if (string.IsNullOrWhiteSpace(entity.PresumptiveDiagnosis))
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_PresumptiveDiagnosisRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.WithClinicalInfo:
                        if (string.IsNullOrWhiteSpace(entity.RelevantClinicalInfo))
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_ClinicalInfoRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.Insurer:
                        if (entity.RequestedInsurerID < 0)
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_RequestedInsurerRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.PolicyType:
                        if ((entity.PolicyTypeID <= 0) && (entity.RequestedInsurerID > 0))
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_PolicyTypeRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.AdmissionAssistanceService:
                        if (entity.EpisodeAssistanceServiceID == 0)
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_AdmissionAssistanceServiceRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.EstimatedAdmissionDateTime:
                        if ((entity.RequestEffectiveAtDateTime == null)
                            && (entity.Order != null)
                            && (entity.Order.OrderCase == OrderClassTypeEnum.ADTInstructions)
                            && (entity.Order.ADTRequestAction == ADTRequestActionEnum.AdmissionRequest))
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_EstimatedAdmissionDateTimeRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.ProcessStartAtDateTime:
                        if ((entity.RequestEffectiveAtDateTime == null) &&
                            (entity.Order != null) &&
                            (entity.Order.OrderCase != OrderClassTypeEnum.ADTInstructions))
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_ProcessStartAtDateTimeRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.AttendingPhysician:
                        if (entity.AttendingPhysicianID == 0)
                        {
                            validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_AttendingPhysicianRequired, entity.OrderNumber),
                                    this, null, null, null));
                        }
                        break;
                    case ToRequestOrderFlagsEnum.None:
                    case ToRequestOrderFlagsEnum.RequestingReferringPhysician:
                    default:
                        break;
                }
            }
        }

        private void ValidateFrequencyOfApplication(OrderRequestSchPlanningEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || validationResults == null)
                return;

            if (entity.EditStatus.Value != StatusEntityValue.New
                && entity.EditStatus.Value != StatusEntityValue.Updated)
                return;

            if (entity.FrequencyOfApplicationID > 0)
            {
                TimePatternEntity tp = GetTimePatternByID(entity.FrequencyOfApplicationID);
                if (tp != null)
                {
                    int times = GetNumberOfTimes(entity);

                    if (tp.NumberOfTimes != times)
                    {
                        validationResults.AddResult(
                            new ValidationResult(
                                string.Format(Properties.Resources.ERROR_InvalidNumberOfTimes, tp.NumberOfTimes, times),
                                this, null, null, null));
                    }

                    //Validar User Times distintos
                    if ((entity != null) &&
                        (entity.OrderRequestTimes != null) &&
                        (entity.OrderRequestTimes.Length > 0))
                    {
                        DateTime[] duplicatedDates = entity.OrderRequestTimes
                                                            .Where(i => i.EditStatus.Value != StatusEntityValue.Deleted && i.EditStatus.Value != StatusEntityValue.NewAndDeleted)
                                                            .GroupBy(i => DateUtils.RoundDown(i.Time, TimeSpan.FromMinutes(1)))
                                                            .Where(g => g.Count() > 1)
                                                            .Select(g => g.Key)
                                                            .ToArray();
                        if ((duplicatedDates != null) && (duplicatedDates.Length > 0))
                        {
                            string duplicatedTimes = string.Empty;
                            foreach (DateTime item in duplicatedDates)
                            {
                                if (string.IsNullOrWhiteSpace(duplicatedTimes))
                                {
                                    duplicatedTimes = item.ToString("t");
                                }
                                else
                                {
                                    duplicatedTimes = string.Concat(duplicatedTimes, "; ", item.ToString("t"));
                                }
                            }

                            validationResults.AddResult(
                                new ValidationResult(string.Format(Properties.Resources.MSG_CannotDuplicateTimes, duplicatedTimes),
                                    this, null, null, null));
                        }
                    }

                }
                else
                {
                    validationResults.AddResult(
                        new ValidationResult(Properties.Resources.ERROR_FrequencyOfApplicationPatternNotFound,
                            this, null, null, null));
                }
            }
            else
            {
                //TODO: Pendiente de decisión sobre el funcionamiento del OrderRequestSchPlanning
                //if (entity.OrderEffectiveAt != null && string.IsNullOrWhiteSpace(entity.FrequencyOfApplicationMeaning))
                //    validationResults.AddResult(
                //        new ValidationResult(Properties.Resources.ERROR_FrequencyOfApplicationPatternRequired,
                //            this, null, null, null));

                if (!string.IsNullOrWhiteSpace(entity.FrequencyOfApplicationMeaning))
                {
                    try
                    {
                        TimePatternEditionEntity tp = new TimePatternEditionEntity(entity.FrequencyOfApplicationMeaning);

                        if ((tp != null) &&
                            (tp.UserTimes != null) &&
                            (tp.UserTimes.Length > 0))
                        {
                            DateTime[] duplicatedDates = tp.UserTimes
                                                            .GroupBy(i => DateUtils.RoundDown(i.Time, TimeSpan.FromMinutes(1)))
                                                            .Where(g => g.Count() > 1)
                                                            .Select(g => g.Key)
                                                            .ToArray();
                            if ((duplicatedDates != null) && (duplicatedDates.Length > 0))
                            {
                                string duplicatedTimes = string.Empty;
                                foreach (DateTime item in duplicatedDates)
                                {
                                    if (string.IsNullOrWhiteSpace(duplicatedTimes))
                                    {
                                        duplicatedTimes = item.ToString("t");
                                    }
                                    else
                                    {
                                        duplicatedTimes = string.Concat(duplicatedTimes, "; ", item.ToString("t"));
                                    }
                                }

                                validationResults.AddResult(
                                    new ValidationResult(string.Format(Properties.Resources.MSG_CannotDuplicateTimes, duplicatedTimes),
                                        this, null, null, null));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        validationResults.AddResult(
                            new ValidationResult(
                                string.Format(
                                    Properties.Resources.ERROR_InvalidFrequencyOfApplicationPatternFormat,
                                    entity.FrequencyOfApplicationMeaning, ex.Message),
                                this, null, null, null));
                    }
                }
            }
        }

        private void SetPrescriptionLocation(PrescriptionRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null)
                return;

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.New:
                    if (entity.CurrentLocationID > 0)
                    {
                        LocationBaseEntity location = LocationBL.GetParentLocationBaseWithStorage(
                            entity.CurrentLocationID);
                        if (location != null)
                        {
                            if (location.ID != entity.LocationID)
                            {
                                entity.LocationID = location.ID;
                                entity.EditStatus.Update();
                            }
                        }
                    }
                    break;
                default: break;
            }
        }

        private void AnalyzeCustomerOrderRequest(
            CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null)
                return;

            //Preanalisis
            ValidateCustomerOrderRequest(entity, validationResults);
            ValidateRequiredOrderFlags(entity, validationResults);

            ValidateProgramming(entity, validationResults);

            CheckLocation(entity, validationResults);

            CheckCodeGenerators(entity, validationResults);

            //Análisis de entidades CustomerOrderRequestEntity -> Entities

            //Análisis de CustomerOrderRequestEntity
            if (entity.OrderControlCode == OrderControlCodeEnum.ParentOrder)
            {
                if (!entity.Placed)
                {
                    entity.Placed = true;
                    entity.EditStatus.Update();
                }
            }

            HandleBasicActions<CustomerOrderRequestEntity>(entity, validationResults);

            //Verificar identificadores de relación entre entidades
            if (entity.Reason != null)
            {
                if (entity.Reason.CustomerOrderRequestID != entity.ID)
                {
                    entity.Reason.CustomerOrderRequestID = entity.ID;
                    entity.Reason.EditStatus.Update();
                }
            }

            if (entity.OrderRequestSchPlanning != null)
            {
                if (entity.OrderRequestSchPlanning.CustomerOrderRequestID != entity.ID)
                {
                    entity.OrderRequestSchPlanning.CustomerOrderRequestID = entity.ID;
                    entity.OrderRequestSchPlanning.EditStatus.Update();
                }
            }

            if (entity.ProcedureRels != null && entity.ProcedureRels.Length > 0)
            {
                foreach (OrderRequestProcedureRelEntity item in entity.ProcedureRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            if (entity.RoutineRels != null && entity.RoutineRels.Length > 0)
            {
                foreach (OrderRequestRoutineRelEntity item in entity.RoutineRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            if (entity.HumanResourceRels != null && entity.HumanResourceRels.Length > 0)
            {
                foreach (OrderRequestHumanResourceRelEntity item in entity.HumanResourceRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }
            if (entity.ResourceRels != null && entity.ResourceRels.Length > 0)
            {
                foreach (OrderRequestResourceRelEntity item in entity.ResourceRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }
            if (entity.EquipmentRels != null && entity.EquipmentRels.Length > 0)
            {
                foreach (OrderRequestEquipmentRelEntity item in entity.EquipmentRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }
            if (entity.LocationRels != null && entity.LocationRels.Length > 0)
            {
                foreach (OrderRequestLocationRelEntity item in entity.LocationRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }


            if (entity.Prescription != null)
            {
                if (entity.Prescription.CustomerOrderRequestID != entity.ID)
                {
                    entity.Prescription.CustomerOrderRequestID = entity.ID;
                    entity.Prescription.EditStatus.Update();
                }
            }

            if (entity.ADTRequestInfo != null)
            {
                if (entity.ADTRequestInfo.CustomerOrderRequestID != entity.ID)
                {
                    entity.ADTRequestInfo.CustomerOrderRequestID = entity.ID;
                    entity.ADTRequestInfo.EditStatus.Update();
                }
            }

            if (entity.RequirementRels != null && entity.RequirementRels.Length > 0)
            {
                foreach (OrderRequestRequirementRelEntity item in entity.RequirementRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            if (entity.BodySiteRels != null && entity.BodySiteRels.Length > 0)
            {
                foreach (OrderRequestBodySiteRelEntity item in entity.BodySiteRels)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            if (entity.Consents != null && entity.Consents.Length > 0)
            {
                foreach (OrderRequestConsentRelEntity item in entity.Consents)
                {
                    if (item.CustomerOrderRequestID != entity.ID)
                    {
                        item.CustomerOrderRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            //Marcamos borradas las entidades dependientes si el estado es borrado
            if (entity.EditStatus.Value == StatusEntityValue.Deleted
                || entity.EditStatus.Value == StatusEntityValue.NewAndDeleted)
            {
                if (entity.Reason != null)
                    entity.Reason.EditStatus.Delete();

                if (entity.OrderRequestSchPlanning != null)
                    entity.OrderRequestSchPlanning.EditStatus.Delete();

                if (entity.ProcedureRels != null && entity.ProcedureRels.Length > 0)
                {
                    foreach (OrderRequestProcedureRelEntity item in entity.ProcedureRels)
                        item.EditStatus.Delete();
                }

                if (entity.RoutineRels != null && entity.RoutineRels.Length > 0)
                {
                    foreach (OrderRequestRoutineRelEntity item in entity.RoutineRels)
                        item.EditStatus.Delete();
                }

                if (entity.Prescription != null)
                    entity.Prescription.EditStatus.Delete();

                if (entity.ADTRequestInfo != null)
                    entity.ADTRequestInfo.EditStatus.Delete();



                if (entity.HumanResourceRels != null && entity.HumanResourceRels.Length > 0)
                {
                    foreach (OrderRequestHumanResourceRelEntity item in entity.HumanResourceRels)
                        item.EditStatus.Delete();
                }
                if (entity.ResourceRels != null && entity.ResourceRels.Length > 0)
                {
                    foreach (OrderRequestResourceRelEntity item in entity.ResourceRels)
                        item.EditStatus.Delete();
                }
                if (entity.EquipmentRels != null && entity.EquipmentRels.Length > 0)
                {
                    foreach (OrderRequestEquipmentRelEntity item in entity.EquipmentRels)
                        item.EditStatus.Delete();
                }
                if (entity.LocationRels != null && entity.LocationRels.Length > 0)
                {
                    foreach (OrderRequestLocationRelEntity item in entity.LocationRels)
                        item.EditStatus.Delete();
                }

                if (entity.RequirementRels != null && entity.RequirementRels.Length > 0)
                {
                    foreach (OrderRequestRequirementRelEntity item in entity.RequirementRels)
                        item.EditStatus.Delete();
                }

                if (entity.BodySiteRels != null && entity.BodySiteRels.Length > 0)
                {
                    foreach (OrderRequestBodySiteRelEntity item in entity.BodySiteRels)
                        item.EditStatus.Delete();
                }

                if (entity.Consents != null && entity.Consents.Length > 0)
                {
                    foreach (OrderRequestConsentRelEntity item in entity.Consents)
                        item.EditStatus.Delete();
                }
            }

            //Análisis de entidades Entities -> CustomerOrderRequestEntity
            //sólo si se han cambiado rutinas o procedimientos
            if (entity.Reason != null)
                AnalyzeCustomerOrderRequestReason(entity, validationResults);

            AnalyzeOrderRequestSchPlanning(entity.OrderRequestSchPlanning, entity, validationResults);
            AnalyzeProcedureRels(entity, validationResults);
            AnalyzeRoutineRels(entity, validationResults);

            AnalyzeHumanResourceRels(entity, validationResults);
            AnalyzeResourceRels(entity, validationResults);
            AnalyzeEquipmentRels(entity, validationResults);
            AnalyzeLocationRels(entity, validationResults);
            AnalyzeRequirementRels(entity, validationResults);
            AnalyzeBodySiteRels(entity, validationResults);
            AnalyzeConsentRels(entity, validationResults);

            AnalyzePrescription(entity.Prescription, entity, validationResults);
            HandleBasicActions<OrderRequestADTInfoEntity>(entity.ADTRequestInfo,
                validationResults, Helpers.OrderRequestADTInfoHelper.Validate);

            AnalyzeRegisteredObservations(entity, validationResults);
        }

        private void AnalyzeOrderRequestSchPlanning(OrderRequestSchPlanningEntity entity,
            CustomerOrderRequestEntity cor, ValidationResults validationResults)
        {
            if (entity == null)
                return;

            //Preanalisis
            ValidateFrequencyOfApplication(entity, validationResults);

            //Análisis de entidades OrderRequestSchPlanningEntity -> Entities
            AnalizeModifyProgrammingOrderRequestSchPlanningAfterConfirmed(entity, cor, validationResults);

            //Análisis de OrderRequestSchPlanningEntity
            HandleBasicActions<OrderRequestSchPlanningEntity>(entity,
                validationResults, Helpers.OrderRequestSchPlanningHelper.Validate);

            //Verificar identificadores de relación entre entidades
            if (entity.OrderRequestTimes != null)
            {
                foreach (OrderRequestTimeEntity item in entity.OrderRequestTimes)
                {
                    if (item.OrderRequestSchPlanningID != entity.ID)
                    {
                        item.OrderRequestSchPlanningID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            //Marcamos borradas las entidades dependientes si el estado es borrado
            if (entity.EditStatus.Value == StatusEntityValue.Deleted
                || entity.EditStatus.Value == StatusEntityValue.NewAndDeleted)
            {
                if (entity.OrderRequestTimes != null)
                {
                    foreach (OrderRequestTimeEntity item in entity.OrderRequestTimes)
                        item.EditStatus.Delete();
                }
            }

            //Análisis de entidades Entities -> OrderRequestSchPlanningEntity
            HandleBasicListActions<OrderRequestTimeEntity>(entity.OrderRequestTimes,
                validationResults, Helpers.OrderRequestTimeHelper.Validate);

            AnalyzeCustomerRoutines(entity, validationResults);
            AnalyzeCustomerProcedures(entity, validationResults);
        }

        private void AnalyzeCustomerRoutines(OrderRequestSchPlanningEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null
                || entity.CustomerRoutines == null
                || entity.CustomerRoutines.Length <= 0)
                return;

            foreach (CustomerRoutineEntity item in entity.CustomerRoutines)
            {
                item.Source = AppointmentElementEnum.MedicalOrder;
                CustomerRoutineBL.Save(item, false, validationResults);

                OrderRequestCustomerRoutineRelationship rel =
                    new OrderRequestCustomerRoutineRelationship(entity, item);
                switch (item.EditStatus.Value)
                {
                    case StatusEntityValue.New:
                        item.EditStatus.Value = StatusEntityValue.New;
                        UnitOfWork.New(rel);
                        break;
                    case StatusEntityValue.Updated:
                        item.EditStatus.Value = StatusEntityValue.Updated;
                        UnitOfWork.Update(rel);
                        break;
                    case StatusEntityValue.Deleted:
                        item.EditStatus.Value = StatusEntityValue.Deleted;
                        UnitOfWork.Remove(rel);
                        break;
                    default:
                        break;
                }
            }
        }

        private void AnalyzeCustomerProcedures(OrderRequestSchPlanningEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || entity.CustomerProcedures == null
                || entity.CustomerProcedures.Length <= 0)
                return;

            foreach (CustomerProcedureEntity item in entity.CustomerProcedures)
            {
                item.Source = AppointmentElementEnum.MedicalOrder;
                CustomerProcedureBL.Save(item, false, validationResults);

                OrderRequestCustomerProcedureRelationship rel =
                    new OrderRequestCustomerProcedureRelationship(entity, item);
                switch (item.EditStatus.Value)
                {
                    case StatusEntityValue.New:
                        rel.EditStatus.Value = StatusEntityValue.New;
                        UnitOfWork.New(rel);
                        break;
                    case StatusEntityValue.Updated:
                        rel.EditStatus.Value = StatusEntityValue.Updated;
                        UnitOfWork.Update(rel);
                        break;
                    case StatusEntityValue.Deleted:
                        rel.EditStatus.Value = StatusEntityValue.Deleted;
                        UnitOfWork.Remove(rel);
                        break;
                    default:
                        break;
                }
            }
        }

        private void AnalyzeProcedureRels(CustomerOrderRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || entity.ProcedureRels == null
                || entity.ProcedureRels.Length <= 0)
                return;

            foreach (OrderRequestProcedureRelEntity item in entity.ProcedureRels)
                AnalyzeProcedureRel(item, validationResults);
        }

        private void AnalyzeProcedureRel(OrderRequestProcedureRelEntity entity, ValidationResults validationResults)
        {
            //Preanalisis
            if (entity == null)
                return;

            //Verificar que tiene algun OrderRequestProcedureRel asociado a un Procedure en estado Confirmado
            //Veriricar que todas las rutinas de OrderRequestProcedureRel asociadas a un Routine en estado Confirmado

            //ValidateIsConfirmed(entity, validationResults);

            //Análisis de entidades OrderRequestProcedureRelEntity -> Entities

            //Análisis de OrderRequestProcedureRelEntity
            HandleBasicActions<OrderRequestProcedureRelEntity>(entity,
                validationResults, Helpers.OrderRequestProcedureRelHelper.Validate);

            //Verificar identificadores de relación entre entidades
            if (entity.Routines != null)
            {
                foreach (OrderRequestProcedureRoutineRelEntity item in entity.Routines)
                {
                    if (item.OrderRequestProcedureRelID != entity.ID)
                    {
                        item.OrderRequestProcedureRelID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            //Marcamos borradas las entidades dependientes si el estado es borrado
            if (entity.EditStatus.Value == StatusEntityValue.Deleted
                || entity.EditStatus.Value == StatusEntityValue.NewAndDeleted)
            {
                if (entity.Routines != null)
                {
                    foreach (OrderRequestProcedureRoutineRelEntity item in entity.Routines)
                        item.EditStatus.Delete();
                }
            }

            //Análisis de entidades Entities -> OrderRequestProcedureRelEntity
            HandleBasicListActions<OrderRequestProcedureRoutineRelEntity>(entity.Routines,
                validationResults, Helpers.OrderRequestProcedureRoutineRelHelper.Validate);
        }

        private void AnalyzeRoutineRels(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null || entity.RoutineRels == null || entity.RoutineRels.Length <= 0)
                return;

            HandleBasicListActions<OrderRequestRoutineRelEntity>(entity.RoutineRels,
                validationResults, Helpers.OrderRequestRoutineRelHelper.Validate);
        }

        private void AnalyzePrescription(PrescriptionRequestEntity entity, CustomerOrderRequestEntity cor, ValidationResults validationResults)
        {
            if (entity == null)
                return;

            //Preanálisis
            SetPrescriptionLocation(entity, validationResults);

            //Análisis de entidades PrescriptionRequestEntity -> Entities

            //Análisis de PrescriptionRequestEntity
            HandleBasicActions<PrescriptionRequestEntity>(entity,
                validationResults, Helpers.PrescriptionRequestHelper.Validate);

            ////////////////////////////////////////////////////////////////////
            //VIENE DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS

            ////Verificar identificadores de relación entre entidades
            //if (entity.TreatmentRoutes != null)
            //{
            //    //TODO: Implementar gestion de ItemTreatmentRouteEntity
            //    foreach (PrescriptionRequestItemSequenceRelEntity item in entity.TreatmentRoutes)
            //    {
            //        if (item.PrescriptionRequestID != entity.ID)
            //        {
            //            item.PrescriptionRequestID = entity.ID;
            //            item.EditStatus.Update();
            //        }
            //    }
            //}

            ////Marcamos borradas las entidades dependientes si el estado es borrado
            //if (entity.EditStatus.Value == StatusEntityValue.Deleted
            //    || entity.EditStatus.Value == StatusEntityValue.NewAndDeleted)
            //{
            //    if ((entity.TreatmentRoutes != null) && (entity.TreatmentRoutes.Length > 0))
            //    {
            //        foreach (PrescriptionRequestItemSequenceRelEntity item in entity.TreatmentRoutes)
            //            item.EditStatus.Delete();
            //    }
            //}

            ////Análisis de entidades Entities -> PrescriptionRequestEntity
            //AnalyzeTreatmentRoutes(entity.TreatmentRoutes, cor, validationResults);

            //Verificar identificadores de relación entre entidades
            if (entity.ItemSequence != null)
            {
                //TODO: Implementar gestion de ItemTreatmentOrderSequenceEntity
                foreach (ItemTreatmentOrderSequenceEntity item in entity.ItemSequence)
                {
                    if (item.PrescriptionRequestID != entity.ID)
                    {
                        item.PrescriptionRequestID = entity.ID;
                        item.EditStatus.Update();
                    }
                }
            }

            //Marcamos borradas las entidades dependientes si el estado es borrado
            if (entity.EditStatus.Value == StatusEntityValue.Deleted
                || entity.EditStatus.Value == StatusEntityValue.NewAndDeleted)
            {
                if ((entity.ItemSequence != null) && (entity.ItemSequence.Length > 0))
                {
                    foreach (ItemTreatmentOrderSequenceEntity item in entity.ItemSequence)
                        item.EditStatus.Delete();
                }
            }

            //Análisis de entidades Entities -> PrescriptionRequestEntity
            AnalyzeItemTreatmentOrderSequence(entity.ItemSequence, cor, validationResults);



            //FIN DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
            ////////////////////////////////////////////////////////////////////

        }


        ////////////////////////////////////////////////////////////////////
        //VIENE DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS

        //private void AnalyzeTreatmentRoutes(PrescriptionRequestItemSequenceRelEntity[] entities, CustomerOrderRequestEntity cor,
        //    ValidationResults validationResults)
        //{
        //    if ((entities == null) || (entities.Length <= 0) || (validationResults == null))
        //        return;

        //    //Validar que el orden de las secuencias es correcto
        //    ValidateOrderTreatmentRoutes(entities, cor, validationResults);

        //    HandleBasicListActions<PrescriptionRequestItemSequenceRelEntity>(entities,
        //        validationResults, Helpers.PrescriptionRequestItemSequenceRelHelper.Validate);
        //}

        //private void ValidateOrderTreatmentRoutes(PrescriptionRequestItemSequenceRelEntity[] entities, CustomerOrderRequestEntity cor, ValidationResults validationResults)
        //{
        //    if ((entities == null) || (entities.Length <= 0) || (validationResults == null))
        //        return;

        //    Array.Sort(entities, (PrescriptionRequestItemSequenceRelEntity prisr1, PrescriptionRequestItemSequenceRelEntity prisr2) => prisr1.OrderItemSequence.CompareTo(prisr2.OrderItemSequence));

        //    //Empieza en 1 y es consecutivo.
        //    for (int i = 0; i < entities.Length; i++)
        //    {
        //        if (entities[i].OrderItemSequence != i + 1)
        //            validationResults.AddResult(new ValidationResult(
        //                    string.Format(Properties.Resources.ERROR_ErrorOrderInSequenceInTreatmentRouteOfCustomerOrderRequest, cor.OrderNumber),
        //                    this, null, null, null));
        //    }
        //}



        private void AnalyzeItemTreatmentOrderSequence(ItemTreatmentOrderSequenceEntity[] entities, CustomerOrderRequestEntity cor,
            ValidationResults validationResults)
        {
            if ((entities == null) || (entities.Length <= 0) || (validationResults == null))
                return;

            //Validar que el orden de las secuencias es correcto
            ValidateItemTreatmentOrderSequence(entities, cor, validationResults);

            HandleBasicListActions<ItemTreatmentOrderSequenceEntity>(entities,
                validationResults, Helpers.ItemTreatmentOrderSequenceHelper.Validate);
        }
        //FIN DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
        ////////////////////////////////////////////////////////////////////


        private void ValidateItemTreatmentOrderSequence(ItemTreatmentOrderSequenceEntity[] entities, CustomerOrderRequestEntity cor, ValidationResults validationResults)
        {
            if ((entities == null) || (entities.Length <= 0) || (validationResults == null))
                return;

            Array.Sort(entities, (ItemTreatmentOrderSequenceEntity prisr1, ItemTreatmentOrderSequenceEntity prisr2) => prisr1.OrderInSequence.CompareTo(prisr2.OrderInSequence));

            //Empieza en 1 y es consecutivo.
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i].OrderInSequence != i + 1)
                    validationResults.AddResult(new ValidationResult(
                            string.Format(Properties.Resources.ERROR_ErrorOrderInSequenceInTreatmentRouteOfCustomerOrderRequest, cor.OrderNumber),
                            this, null, null, null));
            }
        }


        //FIN DE LA TAREA DE EFARMACO Y DE LAS PRESCRIPCIONES COMPLEJAS
        ////////////////////////////////////////////////////////////////////


        private void AnalyzeRegisteredObservations(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if ((entity == null) || (entity.RegisteredObservations == null) || (validationResults == null))
                return;

            entity.RegisteredObservations.CustomerID = entity.CustomerID;
            entity.RegisteredObservations = CustomerObservationBL.Save(entity.RegisteredObservations, false, validationResults);

            RegisteredObservationValueEntity[] observations = entity.RegisteredObservations.GetAllRegisteredObservationValue();

            List<OrderRequestCustomerObservationRelationship> observationsRelAct = new List<OrderRequestCustomerObservationRelationship>();
            if ((observations != null) && (observations.Length > 0))
            {
                foreach (RegisteredObservationValueEntity obs in observations)
                {
                    OrderRequestCustomerObservationRelationship rarovr
                        = new OrderRequestCustomerObservationRelationship(entity, obs);

                    switch (obs.EditStatus.Value)
                    {
                        case StatusEntityValue.Deleted:
                            rarovr.EditStatus.Delete();
                            observationsRelAct.Add(rarovr);
                            break;
                        case StatusEntityValue.New:
                            rarovr.EditStatus.New();
                            observationsRelAct.Add(rarovr);
                            break;
                        default: break;
                    }
                }
            }

            if (observationsRelAct.Count > 0)
                HandleBasicListActions<OrderRequestCustomerObservationRelationship>(observationsRelAct, validationResults);
        }

        private void AnalyzeCustomerOrderRequestReason(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null)
                return;

            switch (entity.Status)
            {
                case ActionStatusEnum.Pending:
                case ActionStatusEnum.Completed:
                case ActionStatusEnum.Confirmed:
                    //if ((entity.Reason != null)
                    //    && (entity.Reason.ReasonChange != null)
                    //    && (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.FinalizeOrder)
                    //    && (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.UndoOrder)
                    //    && (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.AbortOrder)
                    //    && (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.ChangeOrder)
                    //    && (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.CloseOrder)
                    //    && (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.CancelOrder)
                    //    && (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.RejectValidationPrescription))
                    if ((entity.Reason != null)
                        && (entity.Reason.ReasonChange != null)
                        //¿Compara también el ElementID?
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Finalize)
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Undo)
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Abort)
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Change)
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Close)
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Cancel)
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Reject)
                        && (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Miss))
                    {
                        validationResults.AddResult(new ValidationResult(
                            string.Format(Properties.Resources.ERROR_OrderNotChangeReasonChange, entity.OrderNumber),
                            this, null, null, null));
                    }
                    break;
                case ActionStatusEnum.Cancelled:
                    if (entity.Reason == null)
                    {
                        validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_OrderNotCancellationReasonChange, entity.OrderNumber),
                                this, null, null, null));
                    }
                    else
                    {
                        if (entity.Reason.ReasonChange == null)
                        {
                            validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_OrderNotReasonChange, entity.OrderNumber),
                                this, null, null, null));
                        }
                        else
                        {
                            //if (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.MissCitation
                            //    && entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.CancelCitation
                            //    && entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.CancelOrder
                            //    && entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.CloseOrder)
                            if (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Miss
                                && entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Cancel
                                && entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Finalize)
                            {
                                validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_OrderNotReasonChangeCancellation, entity.OrderNumber),
                                    this, null, null, null));
                            }
                        }
                    }
                    break;
                case ActionStatusEnum.Aborted:
                    if (entity.Reason == null)
                    {
                        validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_OrderNotCancellationReasonChange, entity.OrderNumber),
                                this, null, null, null));
                    }
                    else
                    {
                        if (entity.Reason.ReasonChange == null)
                        {
                            validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_OrderNotReasonChange, entity.OrderNumber),
                                this, null, null, null));
                        }
                        else
                        {
                            //if (entity.Reason.ReasonChange.ReasonChangeType != ReasonTypeEnum.AbortOrder)
                            if (entity.Reason.ReasonChange.ReasonChangeType != NewReasonTypeEnum.Abort)
                            {
                                validationResults.AddResult(new ValidationResult(
                                    string.Format(Properties.Resources.ERROR_OrderNotReasonChangeCancellation, entity.OrderNumber),
                                    this, null, null, null));
                            }
                        }
                    }
                    break;
                default: break;
            }

            HandleBasicActions<CustomerOrderRequestReasonRelEntity>(entity.Reason,
                validationResults, Helpers.CustomerOrderRequestReasonRelHelper.Validate);
        }

        private void AnalizeModifyProgrammingOrderRequestSchPlanningAfterConfirmed(OrderRequestSchPlanningEntity entity,
            CustomerOrderRequestEntity cor, ValidationResults validationResults)
        {
            if ((entity == null) || (((entity.CustomerRoutines == null) || (entity.CustomerRoutines.Length <= 0))
                    && ((entity.CustomerProcedures == null) || (entity.CustomerProcedures.Length <= 0)))
                || (validationResults == null))
                return;

            if (IsAnalyzed(cor) || (cor.Status != ActionStatusEnum.Confirmed))
                return;

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.None:
                    if ((entity.OrderRequestTimes == null) || (entity.OrderRequestTimes.Length <= 0))
                        return;

                    AnalizeModifyProgrammingOrderRequestTimesAfterConfirmed(entity, validationResults);
                    break;
                case StatusEntityValue.Updated:
                    AnalizeModifyProgrammingFrequencyOfApplicationAfterConfirmed(entity, validationResults);
                    AnalizeModifyProgrammingOrderRequestTimesAfterConfirmed(entity, validationResults);
                    break;
                default: break;
            }
        }

        private void AnalizeModifyProgrammingOrderRequestTimesAfterConfirmed(OrderRequestSchPlanningEntity entity,
            ValidationResults validationResults)
        {
            if ((entity == null) || (entity.OrderRequestTimes == null) || (entity.OrderRequestTimes.Length <= 0)
                || (((entity.CustomerRoutines == null) || (entity.CustomerRoutines.Length <= 0))
                    && ((entity.CustomerProcedures == null) || (entity.CustomerProcedures.Length <= 0)))
                || (validationResults == null))
                return;

            if (!Array.Exists(entity.OrderRequestTimes, ort => ort.EditStatus.Value != StatusEntityValue.None))
                return;

            OrderRequestTimeEntity[] times = Array.FindAll(entity.OrderRequestTimes,
                ort => ort.EditStatus.Value != StatusEntityValue.Deleted && ort.EditStatus.Value != StatusEntityValue.NewAndDeleted);


            if ((entity.CustomerRoutines != null) && (entity.CustomerRoutines.Length > 0))
            {
                foreach (CustomerRoutineEntity cr in entity.CustomerRoutines)
                {
                    List<CustomerRoutineTimeEntity> customerRoutineTimes = new List<CustomerRoutineTimeEntity>();
                    //Marcar las horas de cr para borrar 
                    if ((cr.CustomerRoutineTimes != null) && (cr.CustomerRoutineTimes.Length > 0))
                    {
                        foreach (CustomerRoutineTimeEntity crTimes in cr.CustomerRoutineTimes)
                        {
                            crTimes.EditStatus.Delete();
                            customerRoutineTimes.Add(crTimes);
                        }
                    }

                    //Generar CustomerRoutineTimes nuevos con los times (orderRequestTimes)
                    if ((times != null) && (times.Length > 0))
                    {
                        foreach (OrderRequestTimeEntity orTimes in times)
                        {
                            CustomerRoutineTimeEntity crt = new CustomerRoutineTimeEntity(0, cr.ID, orTimes.Time, DateTime.Now, string.Empty, 0);
                            crt.EditStatus.New();
                            customerRoutineTimes.Add(crt);
                        }
                    }

                    cr.CustomerRoutineTimes = customerRoutineTimes.ToArray();
                }
            }

            if ((entity.CustomerProcedures != null) && (entity.CustomerProcedures.Length > 0))
            {
                foreach (CustomerProcedureEntity cp in entity.CustomerProcedures)
                {
                    List<CustomerProcedureTimeEntity> customerProcedureTimes = new List<CustomerProcedureTimeEntity>();
                    //Marcar las horas de cr para borrar 
                    if ((cp.CustomerProcedureTimes != null) && (cp.CustomerProcedureTimes.Length > 0))
                    {
                        foreach (CustomerProcedureTimeEntity cpTimes in cp.CustomerProcedureTimes)
                        {
                            cpTimes.EditStatus.Delete();
                            customerProcedureTimes.Add(cpTimes);
                        }
                    }

                    //Generar CustomerProcedureTimes nuevos con los times (orderRequestTimes)
                    if ((times != null) && (times.Length > 0))
                    {
                        foreach (OrderRequestTimeEntity orTimes in times)
                        {
                            CustomerProcedureTimeEntity cpt = new CustomerProcedureTimeEntity(0, cp.ID, orTimes.Time, DateTime.Now, string.Empty, 0);
                            cpt.EditStatus.New();
                            customerProcedureTimes.Add(cpt);
                        }
                    }

                    cp.CustomerProcedureTimes = customerProcedureTimes.ToArray();
                }
            }
        }

        private void AnalizeModifyProgrammingFrequencyOfApplicationAfterConfirmed(OrderRequestSchPlanningEntity entity,
            ValidationResults validationResults)
        {
            if ((entity == null) || (((entity.CustomerRoutines == null) || (entity.CustomerRoutines.Length <= 0))
                    && ((entity.CustomerProcedures == null) || (entity.CustomerProcedures.Length <= 0)))
                || (validationResults == null))
                return;

            TimePatternBaseEntity _frequencyOfApplication = (entity.FrequencyOfApplicationID > 0)
                ? GetTimePatternBase(entity.FrequencyOfApplicationID)
                : null;

            if ((entity.CustomerRoutines != null) && (entity.CustomerRoutines.Length > 0))
            {
                foreach (CustomerRoutineEntity cr in entity.CustomerRoutines)
                {
                    if (cr.EstimatedDuration != null
                        && entity.EstimatedDurationID != cr.EstimatedDuration.ID)
                    {
                        cr.EstimatedDuration = TimePatternBL.GetByID(entity.EstimatedDurationID);
                        cr.EditStatus.Update();
                    }

                    if (entity.OrderEffectiveAt != cr.StartAt)
                    {
                        cr.StartAt = entity.OrderEffectiveAt;
                        cr.EditStatus.Update();
                    }

                    if (entity.EstimatedFinalizeAt != cr.EndingTo)
                    {
                        cr.EndingTo = entity.EstimatedFinalizeAt;
                        cr.EditStatus.Update();
                    }

                    if (cr.FrequencyOfApplication == null)
                    {
                        if (_frequencyOfApplication != null)
                        {
                            cr.FrequencyOfApplication = _frequencyOfApplication;
                            cr.EditStatus.Update();
                        }
                    }
                    else
                    {
                        if ((_frequencyOfApplication != null) && (_frequencyOfApplication.ID != cr.FrequencyOfApplication.ID))
                        {
                            cr.FrequencyOfApplication = _frequencyOfApplication;
                            cr.EditStatus.Update();
                        }
                    }

                    if (cr.Meaning != entity.FrequencyOfApplicationMeaning)
                    {
                        cr.Meaning = entity.FrequencyOfApplicationMeaning;
                        cr.EditStatus.Update();
                    }
                }
            }

            if ((entity.CustomerProcedures != null) && (entity.CustomerProcedures.Length > 0))
            {
                foreach (CustomerProcedureEntity cp in entity.CustomerProcedures)
                {
                    if (cp.EstimatedDuration != null
                        && entity.EstimatedDurationID != cp.EstimatedDuration.ID)
                    {
                        cp.EstimatedDuration = TimePatternBL.GetByID(entity.EstimatedDurationID);
                        cp.EditStatus.Update();
                    }

                    if (entity.OrderEffectiveAt != cp.StartAt)
                    {
                        cp.StartAt = entity.OrderEffectiveAt;
                        cp.EditStatus.Update();
                    }

                    if (entity.EstimatedFinalizeAt != cp.EndingTo)
                    {
                        cp.EndingTo = entity.EstimatedFinalizeAt;
                        cp.EditStatus.Update();
                    }

                    if (cp.FrequencyOfApplication == null)
                    {
                        if (_frequencyOfApplication != null)
                        {
                            cp.FrequencyOfApplication = _frequencyOfApplication;
                            cp.EditStatus.Update();
                        }
                    }
                    else
                    {
                        if ((_frequencyOfApplication == null) || (_frequencyOfApplication.ID != cp.FrequencyOfApplication.ID))
                        {
                            cp.FrequencyOfApplication = _frequencyOfApplication;
                            cp.EditStatus.Update();
                        }
                    }

                    if (cp.Meaning != entity.FrequencyOfApplicationMeaning)
                    {
                        cp.Meaning = entity.FrequencyOfApplicationMeaning;
                        cp.EditStatus.Update();
                    }
                }
            }
        }

        private void ValidateHumanResourceRel(OrderRequestHumanResourceRelEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null && (entity.EditStatus.Value == StatusEntityValue.New
                || entity.EditStatus.Value == StatusEntityValue.Updated))
                return;

            ValidateEntity<OrderRequestHumanResourceRelEntity>(entity, validationResults,
                Helpers.OrderRequestHumanResourceRelHelper.Validate);
        }

        private void AnalyzeHumanResourceRels(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null || entity.HumanResourceRels == null || entity.HumanResourceRels.Length <= 0)
                return;

            HandleBasicListActions<OrderRequestHumanResourceRelEntity>(entity.HumanResourceRels,
                validationResults, Helpers.OrderRequestHumanResourceRelHelper.Validate);
        }

        private void ValidateResourceRel(OrderRequestResourceRelEntity entity,
              ValidationResults validationResults)
        {
            if (entity == null && (entity.EditStatus.Value == StatusEntityValue.New
                || entity.EditStatus.Value == StatusEntityValue.Updated))
                return;

            ValidateEntity<OrderRequestResourceRelEntity>(entity, validationResults,
                Helpers.OrderRequestResourceRelHelper.Validate);
        }

        private void AnalyzeResourceRels(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null || entity.ResourceRels == null || entity.ResourceRels.Length <= 0)
                return;

            HandleBasicListActions<OrderRequestResourceRelEntity>(entity.ResourceRels,
                validationResults, Helpers.OrderRequestResourceRelHelper.Validate);
        }

        private void ValidateEquipmentRel(OrderRequestEquipmentRelEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null && (entity.EditStatus.Value == StatusEntityValue.New
                || entity.EditStatus.Value == StatusEntityValue.Updated))
                return;

            ValidateEntity<OrderRequestEquipmentRelEntity>(entity, validationResults,
                Helpers.OrderRequestEquipmentRelHelper.Validate);
        }

        private void AnalyzeEquipmentRels(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null || entity.EquipmentRels == null || entity.EquipmentRels.Length <= 0)
                return;

            HandleBasicListActions<OrderRequestEquipmentRelEntity>(entity.EquipmentRels,
                validationResults, Helpers.OrderRequestEquipmentRelHelper.Validate);
        }

        private void ValidateLocationRel(OrderRequestLocationRelEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null && (entity.EditStatus.Value == StatusEntityValue.New
                || entity.EditStatus.Value == StatusEntityValue.Updated))
                return;

            ValidateEntity<OrderRequestLocationRelEntity>(entity, validationResults,
                Helpers.OrderRequestLocationRelHelper.Validate);
        }

        private void AnalyzeLocationRels(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null || entity.LocationRels == null || entity.LocationRels.Length <= 0)
                return;

            HandleBasicListActions<OrderRequestLocationRelEntity>(entity.LocationRels,
                validationResults, Helpers.OrderRequestLocationRelHelper.Validate);
        }

        private void AnalyzeRequirementRels(CustomerOrderRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || entity.RequirementRels == null
                || entity.RequirementRels.Length <= 0)
                return;

            foreach (OrderRequestRequirementRelEntity item in entity.RequirementRels)
                AnalyzeRequirementRel(item, validationResults);
        }

        private void AnalyzeRequirementRel(OrderRequestRequirementRelEntity entity, ValidationResults validationResults)
        {
            //Preanalisis
            if (entity == null)
                return;

            //Análisis de entidades OrderRequestRequirementRelEntity -> Entities

            //Análisis de OrderRequestRequirementRelEntity
            HandleBasicActions<OrderRequestRequirementRelEntity>(entity,
                validationResults, Helpers.OrderRequestRequirementRelHelper.Validate);

            //Verificar identificadores de relación entre entidades

            //Marcamos borradas las entidades dependientes si el estado es borrado

            //Análisis de entidades Entities -> OrderRequestRequirementRelEntity
        }

        private void AnalyzeBodySiteRels(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            if (entity == null || entity.BodySiteRels == null || entity.BodySiteRels.Length <= 0)
                return;

            foreach (OrderRequestBodySiteRelEntity item in entity.BodySiteRels)
                AnalyzeBodySiteRel(item, validationResults);
        }

        private void AnalyzeBodySiteRel(OrderRequestBodySiteRelEntity entity, ValidationResults validationResults)
        {
            //Preanalisis
            if (entity == null)
                return;

            //Análisis de entidades OrderRequestBodySiteRelEntity -> Entities

            //Análisis de OrderRequestBodySiteRelEntity
            HandleBasicActions<OrderRequestBodySiteRelEntity>(entity,
                validationResults, Helpers.OrderRequestBodySiteRelHelper.Validate);

            //Verificar identificadores de relación entre entidades

            //Marcamos borradas las entidades dependientes si el estado es borrado

            //Análisis de entidades Entities -> OrderRequestBodySiteRelEntity
        }

        private void AnalyzeConsentRels(CustomerOrderRequestEntity entity,
            ValidationResults validationResults)
        {
            if (entity == null || entity.Consents == null
                || entity.Consents.Length <= 0)
                return;

            foreach (OrderRequestConsentRelEntity item in entity.Consents)
                AnalyzeConsentRel(item, validationResults);
        }

        private void AnalyzeConsentRel(OrderRequestConsentRelEntity entity, ValidationResults validationResults)
        {
            //Preanalisis
            if (entity == null)
                return;

            //Análisis de entidades OrderRequestConsentRelEntity -> Entities

            //Análisis de OrderRequestConsentRelEntity
            HandleBasicActions<OrderRequestConsentRelEntity>(entity,
                validationResults, Helpers.OrderRequestConsentRelHelper.Validate);

            //Verificar identificadores de relación entre entidades

            //Marcamos borradas las entidades dependientes si el estado es borrado

            //Análisis de entidades Entities -> OrderRequestConsentRelEntity
        }
        #endregion

        #region BusinessLayerBase<CustomerOrderRequestEntity> protected overriden methods
        protected override void RegisterHandlers()
        {
            UnitOfWork.RegisterHandler(typeof(CustomerOrderRequestEntity),
                new ORMHandler<CustomerOrderRequestEntity>(
                    CustomerOrderRequestNew, CustomerOrderRequestUpdate,
                    CustomerOrderRequestDelete));

            UnitOfWork.RegisterHandler(typeof(CommonEntities.BasicCodeGeneratorRelationship<CustomerOrderRequestEntity>),
                new ORMHandler<CommonEntities.BasicCodeGeneratorRelationship<CustomerOrderRequestEntity>>(
                    BasicCodeGeneratorRelationshipCustomerOrderRequestNew, null, null));

            UnitOfWork.RegisterHandler(typeof(CustomerOrderRequestReasonRelEntity),
                new ORMHandler<CustomerOrderRequestReasonRelEntity>(
                    CustomerOrderRequestReasonRelNew, CustomerOrderRequestReasonRelUpdate,
                    CustomerOrderRequestReasonRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestSchPlanningEntity),
                new ORMHandler<OrderRequestSchPlanningEntity>(
                    OrderRequestSchPlanningNew, OrderRequestSchPlanningUpdate,
                    OrderRequestSchPlanningDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestTimeEntity),
                new ORMHandler<OrderRequestTimeEntity>(
                    OrderRequestTimeNew, OrderRequestTimeUpdate,
                    OrderRequestTimeDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestProcedureRelEntity),
                new ORMHandler<OrderRequestProcedureRelEntity>(
                    OrderRequestProcedureRelNew, OrderRequestProcedureRelUpdate,
                    OrderRequestProcedureRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestProcedureRoutineRelEntity),
                new ORMHandler<OrderRequestProcedureRoutineRelEntity>(
                    OrderRequestProcedureRoutineRelNew, OrderRequestProcedureRoutineRelUpdate,
                    OrderRequestProcedureRoutineRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestCustomerRoutineRelationship),
                new ORMHandler<OrderRequestCustomerRoutineRelationship>(
                    OrderRequestCustomerRoutineRelationshipNew, OrderRequestCustomerRoutineRelationshipUpdate,
                    OrderRequestCustomerRoutineRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestCustomerProcedureRelationship),
                new ORMHandler<OrderRequestCustomerProcedureRelationship>(
                    OrderRequestCustomerProcedureRelationshipNew, OrderRequestCustomerProcedureRelationshipUpdate,
                    OrderRequestCustomerProcedureRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(CustomerRoutineRoutineActRelationship),
                new ORMHandler<CustomerRoutineRoutineActRelationship>(
                    CustomerRoutineRoutineActRelationshipNew, CustomerRoutineRoutineActRelationshipUpdate,
                    CustomerRoutineRoutineActRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(CustomerProcedureProcedureActRelationship),
                new ORMHandler<CustomerProcedureProcedureActRelationship>(
                    CustomerProcedureProcedureActRelationshipNew, CustomerProcedureProcedureActRelationshipUpdate,
                    CustomerProcedureProcedureActRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(PrescriptionRequestCustomerProcedureRelationship),
                new ORMHandler<PrescriptionRequestCustomerProcedureRelationship>(
                    PrescriptionRequestCustomerProcedureRelationshipNew,
                    PrescriptionRequestCustomerProcedureRelationshipUpdate,
                    PrescriptionRequestCustomerProcedureRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestRoutineRelEntity),
                new ORMHandler<OrderRequestRoutineRelEntity>(
                    OrderRequestRoutineRelNew, OrderRequestRoutineRelUpdate,
                    OrderRequestRoutineRelDelete));

            UnitOfWork.RegisterHandler(typeof(PrescriptionRequestEntity),
                new ORMHandler<PrescriptionRequestEntity>(
                    PrescriptionRequestNew, PrescriptionRequestUpdate,
                    PrescriptionRequestDelete));

            //UnitOfWork.RegisterHandler(typeof(PrescriptionRequestItemSequenceRelEntity),
            //    new ORMHandler<PrescriptionRequestItemSequenceRelEntity>(
            //        PrescriptionRequestItemSequenceRelNew, PrescriptionRequestItemSequenceRelUpdate,
            //        PrescriptionRequestItemSequenceRelDelete));


            UnitOfWork.RegisterHandler(typeof(ItemTreatmentOrderSequenceEntity),
                new ORMHandler<ItemTreatmentOrderSequenceEntity>(
                    ItemTreatmentOrderSequenceNew, ItemTreatmentOrderSequenceUpdate,
                    ItemTreatmentOrderSequenceDelete));


            UnitOfWork.RegisterHandler(typeof(OrderRequestADTInfoEntity),
                new ORMHandler<OrderRequestADTInfoEntity>(
                    OrderRequestADTInfoNew, OrderRequestADTInfoUpdate,
                    OrderRequestADTInfoDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestCustomerObservationRelationship),
                new ORMHandler<OrderRequestCustomerObservationRelationship>(
                    OrderRequestCustomerObservationRelationshipNew,
                    OrderRequestCustomerObservationRelationshipUpdate,
                    OrderRequestCustomerObservationRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(CustomerOrderRequestRoutineActRelationship),
                new ORMHandler<CustomerOrderRequestRoutineActRelationship>(
                    CustomerOrderRequestRoutineActRelationshipInsert,
                    CustomerOrderRequestRoutineActRelationshipUpdate,
                    CustomerOrderRequestRoutineActRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(CustomerOrderRequestProcedureActRelationship),
                new ORMHandler<CustomerOrderRequestProcedureActRelationship>(
                    CustomerOrderRequestProcedureActRelationshipInsert,
                    CustomerOrderRequestProcedureActRelationshipUpdate,
                    CustomerOrderRequestProcedureActRelationshipDelete));


            UnitOfWork.RegisterHandler(typeof(OrderRequestHumanResourceRelEntity),
                new ORMHandler<OrderRequestHumanResourceRelEntity>(
                    OrderRequestHumanResourceRelNew, OrderRequestHumanResourceRelUpdate,
                    OrderRequestHumanResourceRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestResourceRelEntity),
                new ORMHandler<OrderRequestResourceRelEntity>(
                    OrderRequestResourceRelNew, OrderRequestResourceRelUpdate,
                    OrderRequestResourceRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestEquipmentRelEntity),
                new ORMHandler<OrderRequestEquipmentRelEntity>(
                    OrderRequestEquipmentRelNew, OrderRequestEquipmentRelUpdate,
                    OrderRequestEquipmentRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestLocationRelEntity),
                new ORMHandler<OrderRequestLocationRelEntity>(
                    OrderRequestLocationRelNew, OrderRequestLocationRelUpdate,
                    OrderRequestLocationRelDelete));

            UnitOfWork.RegisterHandler(typeof(ParentCustomerOrderRequestChildCustomerOrderRequestRelationship),
                new ORMHandler<ParentCustomerOrderRequestChildCustomerOrderRequestRelationship>(
                    ParentCustomerOrderRequestChildCustomerOrderRequestRelationshipNew,
                    null
                    ));

            UnitOfWork.RegisterHandler(typeof(OrderRequestRequirementRelEntity),
                new ORMHandler<OrderRequestRequirementRelEntity>(
                    OrderRequestRequirementRelNew, OrderRequestRequirementRelUpdate,
                    OrderRequestRequirementRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestBodySiteRelEntity),
                new ORMHandler<OrderRequestBodySiteRelEntity>(
                    OrderRequestBodySiteRelNew,
                    OrderRequestBodySiteRelUpdate,
                    OrderRequestBodySiteRelDelete));

            UnitOfWork.RegisterHandler(typeof(OrderRequestConsentRelEntity),
                new ORMHandler<OrderRequestConsentRelEntity>(
                    OrderRequestConsentRelNew, OrderRequestConsentRelUpdate,
                    OrderRequestConsentRelDelete));
        }

        protected override void AnalyzeActions(CustomerOrderRequestEntity entity, ValidationResults validationResults)
        {
            base.AnalyzeActions(entity, validationResults);

            InitializeHelpers();
            AnalyzeCustomerOrderRequest(entity, validationResults);
        }
        #endregion

        #region BusinessLayerBase<CustomerOrderRequestEntity> members
        public override long GetDBTimestamp(int id)
        {
            try
            {
                return DataAccess.CustomerOrderRequestDA.GetDBTimeStamp(id);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public override CustomerOrderRequestEntity GetByID(int id)
        {
            return GetCustomerOrderRequest(id, false);
        }

        public void ActivateAndDeleteOrder(CustomerOrderRequestEntity corToActivate, CustomerOrderRequestEntity corToDelete,
            bool commitActions = true, ValidationResults validationResults = null, bool disablePostProcessActions = false,
            HL7MessagingProcessor hl7Processor = null, string hl7IDISEventName = "ChangeOrder",
            ReasonChangeEntity reason = null)
        {
            try
            {
                if (corToActivate == null || corToDelete == null)
                    throw new ArgumentNullException("entities");

                if (!commitActions && validationResults == null)
                    throw new ArgumentNullException("validationResults");

                ValidationResults vr = validationResults ?? new ValidationResults();
                try
                {
                    Undo(corToDelete, reason != null ? reason.ID : 0, string.Empty, false, vr, hl7Processor);

                    Activate(corToActivate, false, vr, hl7Processor);

                    if (commitActions)
                    {
                        Commit(vr);
                    }
                }
                catch (Exception)
                {
                    if (commitActions)
                        RollbackActions();
                    throw;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public CustomerOrderRequestEntity Save(CustomerOrderRequestEntity entity,
            bool commitActions = true, ValidationResults validationResults = null, bool disablePostProcessActions = false,
            HL7MessagingProcessor hl7Processor = null, string hl7IDISEventName = "ChangeOrder",
            ReasonChangeEntity reason = null)
        {

            if (entity == null)
                throw new ArgumentNullException("entity");

            if ((!commitActions) && (validationResults == null))
                throw new ArgumentNullException("validationResults");

            ValidationResults vr = validationResults ?? new ValidationResults();
            try
            {
                int numberOfPendingChanges = UnitOfWork.NumberOfPendingUpdates;

                AnalyzeActions(entity, vr);

                if (entity.EditStatus.Value == StatusEntityValue.None
                    && numberOfPendingChanges < UnitOfWork.NumberOfPendingUpdates)
                    UnitOfWork.MarkUpdated(entity);

                bool relizeCommit = true;
                if (entity.ID > 0 && commitActions)
                {
                    ////////////////////////////////////////////////////////////////////////////
                    //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                    ////////////////////////////////////////////////////////////////////////////
                    //// EL PROCESSOR SE CREA DENTRO DEL MÉTODO. NO VIENE DE NINGUN SITIO
                    //// se inicializan a null las entities y las bls del procesador
                    ////////////////////////////////////////////////////////////////////////////
                    bool returnIsProcessing = hl7Processor != null;
                    HL7MessagingProcessor thishl7processor = hl7Processor ?? new HL7MessagingProcessor(null, null);
                    if (!returnIsProcessing)
                        thishl7processor.SetMessageInProgess();

                    ////////////////////////////////////////////////////////////////////////////
                    // este necesita que se le pase HL7MessagingProcessor porque no es primario
                    // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de la cancelación de la solicitud                    
                    ////////////////////////////////////////////////////////////////////////////
                    Dictionary<string, object> entities = new Dictionary<string, object>();
                    //la orden y el paciente tienen que ir siempre
                    entities.Add(CommonEntities.Constants.EntityNames.CustomerOrderRequestEntityName, entity);
                    CustomerEntity customer = CustomerBL.GetCustomer(entity.CustomerID);
                    entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, customer);
                    HL7IDISEventEnum hl7IDISEvent = HL7IDISEventEnum.GetByName(hl7IDISEventName);
                    if (hl7IDISEvent == HL7IDISEventEnum.ChangeOrder && CustomerOrderRequestIsScheduled(entity.ID))
                        hl7IDISEvent = HL7IDISEventEnum.ChangeScheduledOrder;


                    relizeCommit = thishl7processor.AnalysisSendORMMessage(hl7IDISEvent, vr, entities, returnIsProcessing, reason);
                    if (!returnIsProcessing)
                        thishl7processor.ResetMessageInProgess();
                    relizeCommit = relizeCommit || hl7IDISEvent.PrimaryEvent;
                    ////////////////////////////////////////////////////////////////////////////
                    ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                    ////////////////////////////////////////////////////////////////////////////
                    if (!relizeCommit)
                    {
                        ProcessValidationResult(vr);
                    }
                }


                if (commitActions && relizeCommit)
                {
                    Commit(vr);
                    return GetByID(entity.ID);
                }

                return entity;

            }
            catch (Exception)
            {
                if (commitActions)
                    RollbackActions();
                throw;
            }

        }

        public void Save(CustomerOrderRequestEntity[] entities,
            bool commitActions = true, ValidationResults validationResults = null, bool disablePostProcessActions = false,
            HL7MessagingProcessor hl7Processor = null, string hl7IDISEventName = "ChangeOrder",
            ReasonChangeEntity reason = null)
        {

            if (entities == null || entities.Length <= 0)
                return;

            ValidationResults vr = validationResults ?? new ValidationResults();
            try
            {
                bool relizeCommit = true;
                foreach (CustomerOrderRequestEntity entity in entities)
                {

                    int numberOfPendingChanges = UnitOfWork.NumberOfPendingUpdates;

                    AnalyzeActions(entity, vr);

                    if (entity.EditStatus.Value == StatusEntityValue.None
                        && numberOfPendingChanges < UnitOfWork.NumberOfPendingUpdates)
                        UnitOfWork.MarkUpdated(entity);

                    if (entity.ID > 0 && commitActions)
                    {
                        ////////////////////////////////////////////////////////////////////////////
                        //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                        ////////////////////////////////////////////////////////////////////////////
                        //// EL PROCESSOR SE CREA DENTRO DEL MÉTODO. NO VIENE DE NINGUN SITIO
                        //// se inicializan a null las entities y las bls del procesador
                        ////////////////////////////////////////////////////////////////////////////
                        bool returnIsProcessing = hl7Processor != null;
                        HL7MessagingProcessor thishl7processor = hl7Processor ?? new HL7MessagingProcessor(null, null);
                        if (!returnIsProcessing)
                            thishl7processor.SetMessageInProgess();

                        ////////////////////////////////////////////////////////////////////////////
                        // este necesita que se le pase HL7MessagingProcessor porque no es primario
                        // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de la cancelación de la solicitud                    
                        ////////////////////////////////////////////////////////////////////////////
                        Dictionary<string, object> proccentities = new Dictionary<string, object>();
                        //la orden y el paciente tienen que ir siempre
                        proccentities.Add(CommonEntities.Constants.EntityNames.CustomerOrderRequestEntityName, entity);
                        CustomerEntity customer = CustomerBL.GetCustomer(entity.CustomerID);
                        proccentities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, customer);
                        HL7IDISEventEnum hl7IDISEvent = HL7IDISEventEnum.GetByName(hl7IDISEventName);
                        if (hl7IDISEvent == HL7IDISEventEnum.ChangeOrder && CustomerOrderRequestIsScheduled(entity.ID))
                            hl7IDISEvent = HL7IDISEventEnum.ChangeScheduledOrder;


                        relizeCommit = thishl7processor.AnalysisSendORMMessage(hl7IDISEvent, vr, proccentities, returnIsProcessing, reason) && relizeCommit;
                        if (!returnIsProcessing)
                            thishl7processor.ResetMessageInProgess();
                        ////////////////////////////////////////////////////////////////////////////
                        ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                        ////////////////////////////////////////////////////////////////////////////
                        if (!relizeCommit)
                        {
                            ProcessValidationResult(vr);
                        }
                    }
                }
                if (commitActions && relizeCommit)
                {
                    Commit(vr);
                }


            }
            catch (Exception)
            {
                if (commitActions)
                    RollbackActions();
                throw;
            }

        }
        #endregion

        #region Public methods
        //public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByParentCustomerOrderRequestID_old(int parentCustomerOrderRequestID, bool applyLOPD)
        //{
        //    try
        //    {
        //        Dictionary<int, int> dicOrderIDs = new Dictionary<int, int>();

        //        DataSet ds = DataAccess.CustomerOrderRequestDA.GetByParentCustomerOrderRequestID(parentCustomerOrderRequestID);
        //        if ((ds.Tables != null)
        //            && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
        //            && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
        //        {
        //            foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows)
        //            {
        //                int id = row["ID"] as int? ?? 0;
        //                int orderID = row["OrderID"] as int? ?? 0;
        //                dicOrderIDs.Add(id, orderID);
        //            }

        //            DataSet ds2 = null;

        //            #region Reason
        //            MergeTable(DataAccess.CustomerOrderRequestReasonRelDA.GetByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable);
        //            #endregion

        //            #region PrescriptionRequest
        //            ds2 = DataAccess.PrescriptionRequestDA.GetByParentCustomerOrderRequestID(parentCustomerOrderRequestID);
        //            if ((ds2 != null)
        //                && ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable)
        //                && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
        //            {
        //                MergeTable(ds2,
        //                    ds, Administrative.Entities.TableNames.PrescriptionRequestTable);
        //                MergeTable(DataAccess.PrescriptionRequestTimeDA.GetByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, Administrative.Entities.TableNames.PrescriptionRequestTimeTable);

        //                IEnumerable<int> pharmaceuticalFormIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
        //                                    .Select(r => r.Field<int>("PharmaceuticalFormID"))
        //                                    .Where(i => i > 0)
        //                                    .Distinct()
        //                                    .OrderBy(i => i);
        //                IEnumerable<int> administrationRouteIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
        //                                                            .Select(r => r.Field<int>("AdministrationRouteID"))
        //                                                            .Where(i => i > 0)
        //                                                            .Distinct()
        //                                                            .OrderBy(i => i);
        //                IEnumerable<int> administrationMethodIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
        //                                                            .Select(r => r.Field<int>("AdministrationMethodID"))
        //                                                            .Where(i => i > 0)
        //                                                            .Distinct()
        //                                                            .OrderBy(i => i);

        //                if (pharmaceuticalFormIDs.Any())
        //                {
        //                    MergeTable(DataAccess.PharmaceuticalFormDA.GetPharmaceuticalFormByIDs(pharmaceuticalFormIDs.ToArray()),
        //                            ds, BackOffice.Entities.TableNames.PharmaceuticalFormTable);
        //                }

        //                if (administrationRouteIDs.Any())
        //                {
        //                    MergeTable(DataAccess.AdministrationRouteDA.GetAdministrationRouteByIDs(administrationRouteIDs.ToArray()),
        //                            ds, BackOffice.Entities.TableNames.AdministrationRouteTable);
        //                }

        //                if (administrationMethodIDs.Any())
        //                {
        //                    MergeTable(DataAccess.AdministrationMethodDA.GetAdministrationMethodByIDs(administrationMethodIDs.ToArray()),
        //                            ds, BackOffice.Entities.TableNames.AdministrationMethodTable);
        //                }


        //                IEnumerable<int> physicalUnitIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
        //                    .Select(r => r.Field<int>("PhysUnitID"))
        //                    .Where(i => i > 0)
        //                    .Distinct()
        //                    .OrderBy(i => i);

        //                if (physicalUnitIDs.Any())
        //                {
        //                    MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByIDs(physicalUnitIDs.ToArray()),
        //                        ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
        //                }


        //                int[] ids = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable]
        //                    .AsEnumerable()
        //                    .Where(r => (r["ID"] as int? ?? 0) > 0)
        //                    .Select(r => (r["ID"] as int? ?? 0))
        //                    .Distinct()
        //                    .OrderBy(id => id)
        //                    .ToArray();

        //                MergeTable(DataAccess.LocationDA.GetLocationBaseByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.LocationBaseTable);
        //                MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
        //                MergeTable(DataAccess.EquipmentDA.GetEquipmentBaseByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.EquipmentBaseTable);
        //                MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByGiveStrengthUnitsByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.PhysicalUnitTable);



        //                //aqui hay que poner ItemTreatmentOrderSequenceTable
        //                #region Item Treatment Order Sequences
        //                ds2 = DataAccess.ItemTreatmentOrderSequenceDA.GetItemTreatmentOrderSequenceByPrescriptionRequestIDs(ids);
        //                if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable))
        //                    && (ds2.Tables[BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable].Rows.Count > 0))
        //                {
        //                    MergeTable(ds2, ds, BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable);

        //                    MergeTable(DataAccess.ItemDA.GetItemBaseByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.ItemBaseTable);
        //                    MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveUnitsByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
        //                    MergeTable(DataAccess.TimePatternDA.GetTimePatternBaseByRequestedGivePerTimeUnitByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.TimePatternTable);
        //                    MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveStrengthUnitsByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
        //                }
        //                #endregion

        //            }
        //            #endregion

        //            #region OrderRequestSchPlanning
        //            ds2 = DataAccess.OrderRequestSchPlanningDA.GetOrderRequestSchPlanningByParentCustomerOrderRequestID(parentCustomerOrderRequestID);
        //            if ((ds2 != null) && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable)
        //                && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
        //            {
        //                MergeTable(ds2,
        //                    ds, Administrative.Entities.TableNames.OrderRequestSchPlanningTable);
        //                MergeTable(DataAccess.OrderRequestTimeDA.GetListOrderRequestTimeByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, Administrative.Entities.TableNames.OrderRequestTimeTable);

        //                MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes),
        //                        ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);


        //                //CustomerProcedures
        //                ds2 = DataAccess.CustomerProcedureDA.GetCustomerProceduresByParentCustomerOrderRequestID(parentCustomerOrderRequestID);
        //                if ((ds2 != null)
        //                    && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerProcedureTable)
        //                    && (ds2.Tables[Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0))
        //                {
        //                    MergeTable(ds2,
        //                        ds, Administrative.Entities.TableNames.CustomerProcedureTable);
        //                    MergeTable(DataAccess.ProcedureDA.GetProcedureBasesOfCustomerProceduresByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, BackOffice.Entities.TableNames.ProcedureBaseTable);
        //                    MergeTable(DataAccess.CustomerProcedureRoutineRelDA.GetCustomerProcedureRoutineRelByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, Administrative.Entities.TableNames.CustomerProcedureRoutineRelTable);
        //                    MergeTable(DataAccess.CustomerProcedureTimeDA.GetCustomerProcedureTimesByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, Administrative.Entities.TableNames.CustomerProcedureTimeTable);
        //                    MergeTable(DataAccess.CustomerProcedureReasonRelDA.GetCustomerProcedureReasonRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, Administrative.Entities.TableNames.CustomerProcedureReasonRelTable);
        //                    MergeTable(DataAccess.ReasonChangeDA.GetReasonChangesCustomerProcedureByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, BackOffice.Entities.TableNames.ReasonChangeTable);
        //                }

        //                //CustomerRoutines
        //                ds2 = DataAccess.CustomerRoutineDA.GetCustomerRoutinesByParentCustomerOrderRequestID(parentCustomerOrderRequestID);
        //                if ((ds2 != null)
        //                    && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable)
        //                    && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0))
        //                {
        //                    MergeTable(ds2,
        //                        ds, Administrative.Entities.TableNames.CustomerRoutineTable);
        //                    MergeTable(DataAccess.RoutineDA.GetRoutinesBasesOfCustomerRoutinesByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, BackOffice.Entities.TableNames.RoutineBaseTable);
        //                    MergeTable(DataAccess.CustomerRoutineTimeDA.GetCustomerRoutineTimesByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, Administrative.Entities.TableNames.CustomerRoutineTimeTable);
        //                    MergeTable(DataAccess.CustomerRoutineReasonRelDA.GetCustomerRoutineReasonRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, Administrative.Entities.TableNames.CustomerRoutineReasonRelTable);
        //                    MergeTable(DataAccess.ReasonChangeDA.GetReasonChangesCustomerRoutineByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                        ds, BackOffice.Entities.TableNames.ReasonChangeTable);
        //                }

        //                MergeTable(DataAccess.TimePatternDA.GetByTimestamp(0), ds, BackOffice.Entities.TableNames.TimePatternTable);
        //            }
        //            #endregion

        //            #region OrderRequestProcedureRels
        //            MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
        //                MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
        //            }
        //            #endregion

        //            #region OrderRequestRoutineRels
        //            MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
        //            if ((ds != null)
        //                && ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable)
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
        //            }
        //            #endregion

        //            #region OrderRequestHumanResourceRels
        //            MergeTable(DataAccess.OrderRequestHumanResourceRelDA.GetOrderRequestHumanResourceRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.ParticipateAsDA.GetParticipatesAsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.ParticipateAsTable);
        //            }
        //            #endregion

        //            #region OrderRequestResourceRels
        //            MergeTable(DataAccess.OrderRequestResourceRelDA.GetOrderRequestResourceRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestResourceRelTable);
        //            #endregion

        //            #region OrderRequestEquipmentRels
        //            MergeTable(DataAccess.OrderRequestEquipmentRelDA.GetOrderRequestEquipmentRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestEquipmentRelTable);
        //            #endregion

        //            #region OrderRequestLocationRels
        //            MergeTable(DataAccess.OrderRequestLocationRelDA.GetOrderRequestLocationRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestLocationRelTable);
        //            #endregion

        //            #region OrderRequestADTInfo
        //            MergeTable(DataAccess.OrderRequestADTInfoDA.GetOrderRequestADTInfoByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestADTInfoTable);
        //            #endregion

        //            #region OrderRequestBodySiteRels
        //            MergeTable(DataAccess.OrderRequestBodySiteRelDA.GetOrderRequestBodySiteRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestBodySiteRelTable);
        //            if ((ds != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestBodySiteRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestBodySiteRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.BodySiteDA.GetBodySitesByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.BodySiteTable);
        //                MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
        //                MergeTable(DataAccess.BodySiteParticipationDA.GetBodySiteParticipationsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.BodySiteParticipationTable);
        //            }
        //            #endregion

        //            #region OrderRequestRequirementRels
        //            MergeTable(DataAccess.OrderRequestRequirementRelDA.GetOrderRequestRequirementRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestRequirementRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRequirementRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRequirementRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.RequirementDA.GetRequirementsByOrderRequestRequirementRelParentCustomerOrderRequest(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.RequirementTable);
        //                MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByOrderRequestRequirementRelParentCustomerOrderRequest(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
        //            }
        //            #endregion

        //            #region OrderRequestConsentRels
        //            MergeTable(DataAccess.OrderRequestConsentRelDA.GetOrderRequestConsentRelsByParentCustomerOrderRequestID(parentCustomerOrderRequestID),
        //                ds, Administrative.Entities.TableNames.OrderRequestConsentRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.ConsentPreprintDA.GetConsentPreprintsByOrderRequestConsentRelParentCustomerOrderRequest(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.ConsentPreprintTable);
        //                if ((ds.Tables != null)
        //                    && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
        //                    && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
        //                {
        //                    MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelConsentPreprintParentCustomerOrderRequest(parentCustomerOrderRequestID),
        //                        ds, BackOffice.Entities.TableNames.ConsentTypeTable);
        //                }
        //                MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelParentCustomerOrderRequest(parentCustomerOrderRequestID),
        //                    ds, BackOffice.Entities.TableNames.ConsentTypeTable);
        //            }
        //            #endregion

        //            CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
        //            CustomerOrderRequestEntity[] result = adapter.GetData(ds);

        //            if ((result != null) && (result.Length > 0))
        //            {
        //                _orderCache.OrderCache.UpdateCache();
        //                foreach (CustomerOrderRequestEntity cor in result)
        //                {
        //                    int orderID = dicOrderIDs[cor.ID];
        //                    cor.Order = _orderCache.OrderCache.Get(orderID, false);
        //                    cor.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(cor.CustomerID, cor.ID);

        //                    if (applyLOPD) LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, cor.ID, ActionType.View);
        //                }
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

        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByParentCustomerOrderRequestID(int parentCustomerOrderRequestID, bool applyLOPD)
        {
            try
            {
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByParentCustomerOrderRequestID(parentCustomerOrderRequestID, PatternTypeEnum.DurationTypes, 0);
                if ((ds.Tables != null)
                    && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    Dictionary<int, int> dicOrderIDs = new Dictionary<int, int>();

                    foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows)
                    {
                        int id = row["ID"] as int? ?? 0;
                        int orderID = row["OrderID"] as int? ?? 0;
                        dicOrderIDs.Add(id, orderID);
                    }

                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());
                    
                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] result = adapter.GetData(ds);

                    if ((result != null) && (result.Length > 0))
                    {
                        _orderCache.OrderCache.UpdateCache();
                        foreach (CustomerOrderRequestEntity cor in result)
                        {
                            int orderID = dicOrderIDs[cor.ID];
                            cor.Order = _orderCache.OrderCache.Get(orderID, false);
                            cor.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(cor.CustomerID, cor.ID);

                            if (applyLOPD) LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, cor.ID, ActionType.View);
                        }
                    }
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

        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByParentCustomerOrderRequestID(int parentCustomerOrderRequestID)
        {
            //try
            //{
            //    CustomerOrderRequestEntity[] oldCustomerOrderRequestEntity = GetCustomerOrderRequestsByParentCustomerOrderRequestID_old(parentCustomerOrderRequestID, true);
            //    CustomerOrderRequestEntity[] newCustomerOrderRequestEntity = GetCustomerOrderRequestsByParentCustomerOrderRequestID(parentCustomerOrderRequestID, true);

            //    bool res = newCustomerOrderRequestEntity.CompareEquals(oldCustomerOrderRequestEntity);
            //    return newCustomerOrderRequestEntity;
            //}
            //catch (Exception ex)
            //{
            //    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            //    return null;
            //}
            return GetCustomerOrderRequestsByParentCustomerOrderRequestID(parentCustomerOrderRequestID, true);
        }

        //public CustomerOrderRequestEntity GetCustomerOrderRequest(int id, bool applyLOPD)
        //{
        //    try
        //    {
        //        CustomerOrderRequestEntity oldCustomerOrderRequestEntity = GetCustomerOrderRequest_old(id, applyLOPD);
        //        CustomerOrderRequestEntity newCustomerOrderRequestEntity = GetCustomerOrderRequest_new(id, applyLOPD);

        //        bool res = newCustomerOrderRequestEntity.CompareEquals(oldCustomerOrderRequestEntity);
        //        return newCustomerOrderRequestEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //        return null;
        //    }
        //}
        public CustomerOrderRequestEntity GetCustomerOrderRequest(int id, bool applyLOPD)
        {
            try
            {
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByID(id, PatternTypeEnum.DurationTypes,0);
                if ((ds.Tables != null)
                    && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    DataSet ds2 = new DataSet();
                    foreach (DataTable oTabla in ds.Tables) if (oTabla.Rows.Count > 0) ds2.Tables.Add(oTabla.Copy());
                    int orderID = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows[0]["OrderID"] as int? ?? 0;

                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity result = adapter.GetByID(id, ds2);

                    result.Order = _orderCache.OrderCache.Get(orderID, true);
                    result.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(result.CustomerID, id);

                    ds2.Dispose();
                    ds.Dispose();
                    if (applyLOPD) LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, id, ActionType.View);

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
        //public CustomerOrderRequestEntity GetCustomerOrderRequest_old(int id, bool applyLOPD)
        //{
        //    try
        //    {
        //        int orderID = 0;

        //        DataSet ds = DataAccess.CustomerOrderRequestDA.GetByID(id);
        //        if ((ds.Tables != null)
        //            && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
        //            && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
        //        {
        //            orderID = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows[0]["OrderID"] as int? ?? 0;

        //            DataSet ds2 = null;

        //            #region Reason
        //            MergeTable(DataAccess.CustomerOrderRequestReasonRelDA.GetByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable);
        //            MergeTable(DataAccess.ReasonChangeDA.GetAllReasonChange(), ds, BackOffice.Entities.TableNames.ReasonChangeTable);

        //            #endregion

        //            #region PrescriptionRequest
        //            ds2 = DataAccess.PrescriptionRequestDA.GetByCustomerOrderRequestID(id);
        //            if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable)
        //                && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
        //            {
        //                MergeTable(ds2, ds, Administrative.Entities.TableNames.PrescriptionRequestTable);
        //                MergeTable(DataAccess.PrescriptionRequestTimeDA.GetByCustomerOrderRequestID(id),
        //                    ds, Administrative.Entities.TableNames.PrescriptionRequestTimeTable);

        //                int pharmaceuticalFormID = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows[0]["PharmaceuticalFormID"] as int? ?? 0;
        //                int administrationRouteID = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows[0]["AdministrationRouteID"] as int? ?? 0;
        //                int administrationMethodID = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows[0]["AdministrationMethodID"] as int? ?? 0;

        //                MergeTable(DataAccess.PharmaceuticalFormDA.GetPharmaceuticalFormByID(pharmaceuticalFormID),
        //                        ds, BackOffice.Entities.TableNames.PharmaceuticalFormTable);

        //                MergeTable(DataAccess.AdministrationRouteDA.GetAdministrationRouteByID(administrationRouteID),
        //                        ds, BackOffice.Entities.TableNames.AdministrationRouteTable);

        //                MergeTable(DataAccess.AdministrationMethodDA.GetAdministrationMethodByID(administrationMethodID),
        //                        ds, BackOffice.Entities.TableNames.AdministrationMethodTable);


        //                int physUnitID = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows[0]["PhysUnitID"] as int? ?? 0;
        //                MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByID(physUnitID),
        //                        ds, BackOffice.Entities.TableNames.PhysicalUnitTable);


        //                int[] ids = new int[] { (ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows[0]["ID"] as int? ?? 0) };

        //                MergeTable(DataAccess.LocationDA.GetLocationBaseByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.LocationBaseTable);
        //                MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
        //                MergeTable(DataAccess.EquipmentDA.GetEquipmentBaseByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.EquipmentBaseTable);
        //                MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByGiveStrengthUnitsByPrescriptionRequestIDs(ids),
        //                    ds, BackOffice.Entities.TableNames.PhysicalUnitTable);


        //                //aqui hay que poner ItemTreatmentOrderSequenceTable
        //                #region Item Treatment Order Sequences
        //                ds2 = DataAccess.ItemTreatmentOrderSequenceDA.GetItemTreatmentOrderSequenceByPrescriptionRequestIDs(ids);
        //                if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable))
        //                    && (ds2.Tables[BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable].Rows.Count > 0))
        //                {
        //                    MergeTable(ds2, ds, BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable);

        //                    MergeTable(DataAccess.ItemDA.GetItemBaseByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.ItemBaseTable);
        //                    MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveUnitsByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
        //                    MergeTable(DataAccess.TimePatternDA.GetTimePatternBaseByRequestedGivePerTimeUnitByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.TimePatternTable);
        //                    MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveStrengthUnitsByPrescriptionRequestIDs(ids),
        //                        ds, BackOffice.Entities.TableNames.PhysicalUnitTable);

        //                }
        //                #endregion

        //            }
        //            #endregion

        //            #region OrderRequestSchPlanning
        //            ds2 = DataAccess.OrderRequestSchPlanningDA.GetOrderRequestSchPlanningByCustomerOrderRequestID(id);
        //            if ((ds2 != null) && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable)
        //                && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
        //            {
        //                MergeTable(ds2, ds, Administrative.Entities.TableNames.OrderRequestSchPlanningTable);
        //                MergeTable(DataAccess.OrderRequestTimeDA.GetListOrderRequestTimeByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestTimeTable);

        //                MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes),
        //                        ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);

        //                //CustomerProcedures
        //                ds2 = DataAccess.CustomerProcedureDA.GetCustomerProceduresByCustomerOrderRequestID(id);
        //                if ((ds2 != null) && ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerProcedureTable)
        //                    && (ds2.Tables[Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0))
        //                {
        //                    MergeTable(ds2, ds, Administrative.Entities.TableNames.CustomerProcedureTable);
        //                    MergeTable(DataAccess.ProcedureDA.GetProcedureBasesOfCustomerProceduresByCustomerOrderRequestID(id), ds, BackOffice.Entities.TableNames.ProcedureBaseTable);
        //                    MergeTable(DataAccess.CustomerProcedureRoutineRelDA.GetCustomerProcedureRoutineRelByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.CustomerProcedureRoutineRelTable);
        //                    MergeTable(DataAccess.CustomerProcedureTimeDA.GetCustomerProcedureTimesByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.CustomerProcedureTimeTable);
        //                    MergeTable(DataAccess.CustomerProcedureReasonRelDA.GetCustomerProcedureReasonRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.CustomerProcedureReasonRelTable);
        //                    //MergeTable(DataAccess.ReasonChangeDA.GetReasonChangesCustomerProcedureByCustomerOrderRequestID(id), ds, BackOffice.Entities.TableNames.ReasonChangeTable);
        //                }

        //                //CustomerRoutines
        //                ds2 = DataAccess.CustomerRoutineDA.GetCustomerRoutinesByCustomerOrderRequestID(id);
        //                if ((ds2 != null) && ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable)
        //                    && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0))
        //                {
        //                    MergeTable(ds2, ds, Administrative.Entities.TableNames.CustomerRoutineTable);
        //                    MergeTable(DataAccess.RoutineDA.GetRoutinesBasesOfCustomerRoutinesByCustomerOrderRequestID(id), ds, BackOffice.Entities.TableNames.RoutineBaseTable);
        //                    MergeTable(DataAccess.CustomerRoutineTimeDA.GetCustomerRoutineTimesByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.CustomerRoutineTimeTable);
        //                    MergeTable(DataAccess.CustomerRoutineReasonRelDA.GetCustomerRoutineReasonRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.CustomerRoutineReasonRelTable);
        //                    //MergeTable(DataAccess.ReasonChangeDA.GetReasonChangesCustomerRoutineByCustomerOrderRequestID(id), ds, BackOffice.Entities.TableNames.ReasonChangeTable);
        //                }

        //                MergeTable(DataAccess.TimePatternDA.GetByTimestamp(0), ds, BackOffice.Entities.TableNames.TimePatternTable);
        //            }
        //            #endregion

        //            #region OrderRequestProcedureRels
        //            MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
        //                MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
        //            }
        //            #endregion

        //            #region OrderRequestRoutineRels
        //            MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
        //            if ((ds != null)
        //                && ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable)
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
        //            }
        //            #endregion

        //            #region OrderRequestHumanResourceRels
        //            MergeTable(DataAccess.OrderRequestHumanResourceRelDA.GetOrderRequestHumanResourceRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.ParticipateAsDA.GetParticipatesAsByCustomerOrderRequestID(id),
        //                            ds,
        //                            BackOffice.Entities.TableNames.ParticipateAsTable);
        //            }
        //            #endregion

        //            #region OrderRequestResourceRels
        //            MergeTable(DataAccess.OrderRequestResourceRelDA.GetOrderRequestResourceRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestResourceRelTable);
        //            #endregion

        //            #region OrderRequestEquipmentRels
        //            MergeTable(DataAccess.OrderRequestEquipmentRelDA.GetOrderRequestEquipmentRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestEquipmentRelTable);
        //            #endregion

        //            #region OrderRequestLocationRels
        //            MergeTable(DataAccess.OrderRequestLocationRelDA.GetOrderRequestLocationRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestLocationRelTable);
        //            #endregion

        //            #region OrderRequestADTInfo
        //            MergeTable(DataAccess.OrderRequestADTInfoDA.GetOrderRequestADTInfoByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestADTInfoTable);
        //            #endregion

        //            #region OrderRequestBodySiteRels
        //            MergeTable(DataAccess.OrderRequestBodySiteRelDA.GetOrderRequestBodySiteRelsByCustomerOrderRequestID(id), ds, Administrative.Entities.TableNames.OrderRequestBodySiteRelTable);
        //            if ((ds != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestBodySiteRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestBodySiteRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.BodySiteDA.GetBodySitesByCustomerOrderRequestID(id), ds, BackOffice.Entities.TableNames.BodySiteTable);
        //                MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptsByCustomerOrderRequestID(id), ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
        //                MergeTable(DataAccess.BodySiteParticipationDA.GetBodySiteParticipationsByCustomerOrderRequestID(id), ds, BackOffice.Entities.TableNames.BodySiteParticipationTable);
        //            }
        //            #endregion

        //            #region OrderRequestRequirementRels
        //            MergeTable(DataAccess.OrderRequestRequirementRelDA.GetOrderRequestRequirementRelsByCustomerOrderRequestID(id),
        //                ds, Administrative.Entities.TableNames.OrderRequestRequirementRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRequirementRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRequirementRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.RequirementDA.GetRequirementsByOrderRequestRequirementRelCustomerOrderRequest(id),
        //                    ds, BackOffice.Entities.TableNames.RequirementTable);
        //                MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByOrderRequestRequirementRelCustomerOrderRequest(id),
        //                    ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
        //            }
        //            #endregion

        //            #region OrderRequestConsentRels
        //            MergeTable(DataAccess.OrderRequestConsentRelDA.GetOrderRequestConsentRelsByCustomerOrderRequestID(id),
        //                ds, Administrative.Entities.TableNames.OrderRequestConsentRelTable);
        //            if ((ds.Tables != null)
        //                && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
        //                && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
        //            {
        //                MergeTable(DataAccess.ConsentPreprintDA.GetConsentPreprintsByOrderRequestConsentRelCustomerOrderRequest(id),
        //                    ds, BackOffice.Entities.TableNames.ConsentPreprintTable);
        //                if ((ds.Tables != null)
        //                    && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
        //                    && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
        //                {
        //                    MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelConsentPreprintCustomerOrderRequest(id),
        //                        ds, BackOffice.Entities.TableNames.ConsentTypeTable);
        //                }
        //                MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelCustomerOrderRequest(id),
        //                    ds, BackOffice.Entities.TableNames.ConsentTypeTable);
        //            }
        //            #endregion

        //            CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
        //            CustomerOrderRequestEntity result = adapter.GetByID(id, ds);
        //            result.Order = _orderCache.OrderCache.Get(orderID, true);
        //            result.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(result.CustomerID, id);

        //            if (applyLOPD)
        //                LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, id, ActionType.View);

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

        public CustomerOrderRequestEntity GetCustomerOrderRequestByPrescriptionReuqestID(int prescriptionID, bool applyLOPD)
        {
            try
            {
                int corID = DataAccess.CustomerOrderRequestDA.GetIDByPrescriptionRequestID(prescriptionID);

                if (corID > 0)
                    return this.GetCustomerOrderRequest(corID, applyLOPD);
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public string PeekOrderNumberGeneration(ValidationResults vr)
        {
            CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
            return corAnalyzer.PeekOrderNumberGeneration();
        }

        public bool ResetCustomerEpisodeIsPossible(int customerEpisodeID)
        {
            bool resetOK = false;
            int processChartID = DataAccess.ProcessChartDA.GetIDByCustomerEpisodeID(customerEpisodeID);
            if (processChartID > 0)
            {
                ProcessChartEntity processChart = ProcessChartBL.GetByID(processChartID);
                if (processChart != null
                    && processChart.EpisodeConfig != null)
                {
                    switch (processChart.EpisodeConfig.EpisodeCase)
                    {
                        case EpisodeCaseEnum.AtHome:
                        case EpisodeCaseEnum.Residential:
                            resetOK = true;
                            break;
                        case EpisodeCaseEnum.RoutineOutPatient:
                            if (processChart.CitationConfig != null)
                            {
                                if (processChart.CitationConfig.CitationType == CitationTypeEnum.MedicalOrder)
                                    resetOK = true;
                                else
                                    resetOK = false;
                            }
                            else
                            {
                                if (processChart.ReceptionConfig != null)
                                {
                                    if (processChart.CitationConfig.CitationType == CitationTypeEnum.MedicalOrder)
                                    {
                                        if (!processChart.CitationConfig.AllowMultipleAppointment)
                                            resetOK = true;
                                    }
                                    else
                                        resetOK = false;
                                }
                                else
                                    resetOK = true;
                            }
                            break;
                        case EpisodeCaseEnum.EmergencyOutPatient:
                        case EpisodeCaseEnum.InPatient:
                        case EpisodeCaseEnum.DayTreatment:
                        default:
                            break;
                    }
                }
            }
            return resetOK;
        }

        public string GetAccessionNumberByCORId(int customerOrderRequestId)
        {
            return DataAccess.CustomerOrderRequestDA.GetAccesionNumberByCORId(customerOrderRequestId);
        }

        #endregion

        #region ICustomerOrderRequestService members
        public CustomerOrderRequestEntity GetCustomerOrderRequestByOrderNumber(string orderNumber)
        {
            try
            {
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByOrderNumber(orderNumber);
                if ((ds.Tables != null) && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    int id = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows[0]["ID"] as int? ?? 0;
                    if (id > 0)
                        return this.GetCustomerOrderRequest(id, true);
                }

                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity[] GetCustomerOrderRequestByPlaceOrderNumber(string placeOrderNumber)
        {
            try
            {
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByPlaceOrderNumber(placeOrderNumber);
                if ((ds.Tables != null) && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    return (from row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                            where (row["ID"] as int? ?? 0) > 0
                            select GetCustomerOrderRequest((row["ID"] as int? ?? 0), true)).ToArray();
                }

                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity GetCustomerOrderRequestByOrderNumberAndPlaceOrderNumber(string orderNumber, string placeOrderNumber)
        {
            try
            {
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByOrderNumberAndPlaceOrderNumber(orderNumber, placeOrderNumber);
                if ((ds.Tables != null) && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    int id = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows[0]["ID"] as int? ?? 0;
                    if (id > 0)
                        return this.GetCustomerOrderRequest(id, true);
                }

                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity GetCustomerOrderRequestByCustomerOrderRealizationID(int customerOrderRealizationID, bool applyLOPD)
        {
            try
            {
                int corID = DataAccess.CustomerOrderRequestDA.GetIDByCustomerOrderRealizationID(customerOrderRealizationID);

                if (corID > 0)
                    return this.GetCustomerOrderRequest(corID, applyLOPD);
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity GetCustomerOrderRequestByRoutineActID(int routineActID, bool applyLOPD)
        {
            try
            {
                int corID = DataAccess.CustomerOrderRequestDA.GetIDByRoutineActID(routineActID);

                if (corID > 0)
                    return this.GetCustomerOrderRequest(corID, applyLOPD);
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity GetCustomerOrderRequestByProcedureActID(int procedureActID, bool applyLOPD)
        {
            try
            {
                int corID = DataAccess.CustomerOrderRequestDA.GetIDByProcedureActID(procedureActID);

                if (corID > 0)
                    return this.GetCustomerOrderRequest(corID, applyLOPD);
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity GetCustomerOrderRequestByCustomerRoutineID(int customerRoutineID, bool applyLOPD)
        {
            try
            {
                int corID = DataAccess.CustomerOrderRequestDA.GetIDByCustomerRoutineID(customerRoutineID);

                if (corID > 0)
                    return this.GetCustomerOrderRequest(corID, applyLOPD);
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity GetCustomerOrderRequestByCustomerProcedureID(int customerProcedureID, bool applyLOPD)
        {
            try
            {
                int corID = DataAccess.CustomerOrderRequestDA.GetIDByCustomerProcedureID(customerProcedureID);

                if (corID > 0)
                    return this.GetCustomerOrderRequest(corID, applyLOPD);
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestEntity[] GetCustomerOrderRequests(int[] ids, bool applyLOPD)
        {
            if ((ids != null) && (ids.Length > 0))
            {
                if (ids.Length == 1)
                {
                    List<CustomerOrderRequestEntity> cores = new List<CustomerOrderRequestEntity>();
                    CustomerOrderRequestEntity core = GetCustomerOrderRequest(ids[0], applyLOPD);
                    cores.Add(core);
                    return (cores.Count > 0) ? cores.ToArray() : null;
                }
                else
                {
                    return GetCustomerOrderRequestsByIDs(ids, applyLOPD);
                }
            }
            return null;
        }

        public CustomerOrderRequestEntity[] GetCustomerOrderRequests(int[] ids, bool applyLOPD, bool loadObservations)
        {
            if ((ids != null) && (ids.Length > 0))
            {
                return GetCustomerOrderRequestsByIDs(ids, applyLOPD, loadObservations);
            }
            return null;
        }

        private int[] GetChildCustomerOrderRequestIDsJoinedParentCustomerOrderRequestID(int parentCustomerOrderRequestID)
        {
            List<int> corIDs = new List<int>();
            corIDs.Add(parentCustomerOrderRequestID);
            DataSet ds = DataAccess.CustomerOrderRequestDA.GetByParentCustomerOrderRequestID(parentCustomerOrderRequestID);
            if ((ds.Tables != null)
                && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable)
                && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
            {
                foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows)
                {
                    int id = row["ID"] as int? ?? 0;
                    corIDs.Add(id);
                }
            }
            return (corIDs.Count > 0) ? corIDs.ToArray() : null;
        }

        /// <summary>
        /// Obtiene todas las Órdenes Médicas(CustomerOrderRequestBasicEntity[]) a partir de la Orden Padre.
        /// </summary>
        /// <param name="parentCustomerOrderRequestID"></param>
        /// <returns>La orden padre y todas sus hijas</returns>
        public CustomerOrderRequestBasicEntity[] GetChildCustomerOrderRequestBasicsJoinedParentCustomerOrderRequestBasic(int parentCustomerOrderRequestID)
        {
            int[] corIDs = GetChildCustomerOrderRequestIDsJoinedParentCustomerOrderRequestID(parentCustomerOrderRequestID);
            if (corIDs != null && corIDs.Any())
                return GetCustomerOrderRequestBasics(corIDs);
            return null;
        }

        /// <summary>
        /// Obtiene todas las Órdenes Médicas(CustomerOrderRequestEntity[]) a partir de la Orden Padre.
        /// </summary>
        /// <param name="parentCustomerOrderRequestID"></param>
        /// <returns>La orden padre y todas sus hijas</returns>
        public CustomerOrderRequestEntity[] GetChildCustomerOrderRequests(int parentCustomerOrderRequestID)
        {
            int[] corIDs = GetChildCustomerOrderRequestIDsJoinedParentCustomerOrderRequestID(parentCustomerOrderRequestID);
            if (corIDs != null && corIDs.Any())
                return GetCustomerOrderRequests(corIDs, false);

            return null;
        }

        /// <summary>
        /// Obtiene todas las Órdenes Médicas(CustomerOrderRequestBasicEntity[]) a partir de los IDs de las Órdenes.
        /// </summary>
        /// <param name="customerOrderRequestIDs"></param>
        /// <returns>Las ordenes que indicamos en el array de IDs</returns>
        public CustomerOrderRequestBasicEntity[] GetCustomerOrderRequestBasics(int[] customerOrderRequestIDs)
        {
            try
            {
                if ((customerOrderRequestIDs == null) || (customerOrderRequestIDs.Length <= 0))
                    return null;

                DataSet ds = new DataSet();
                //CustomerOrderRequestBasic
                MergeTable(DataAccess.CareProcessRealizationDA.GetCustomerOrderRequestsByIDs(customerOrderRequestIDs),
                    ds, Assistance.Entities.TableNames.CustomerOrderRequestBasicTable);
                //CustomerOrderRequestBasic -- OrderRequestTime
                MergeTable(DataAccess.CareProcessRealizationDA.GetCustomerOrderRequestsTimesByCustomerOrderRequestIDs(customerOrderRequestIDs),
                    ds, Administrative.Entities.TableNames.OrderRequestTimeTable);
                //CustomerOrderRequestBasic -- RequestAttendingPhysicians
                MergeTable(DataAccess.CareProcessRealizationDA.GetCustomerOrderRequestAttendingPhysicians(customerOrderRequestIDs),
                    ds, SII.HCD.Common.Entities.TableNames.IDDescriptionTable);

                #region OrderRequestProcedureRels
                MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByCustomerOrderRequestIDs(customerOrderRequestIDs), ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
                    && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
                {
                    MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByCustomerOrderRequestIDs(customerOrderRequestIDs), ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
                    MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByCustomerOrderRequestIDs(customerOrderRequestIDs), ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
                }
                #endregion

                #region OrderRequestRoutineRels
                MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByCustomerOrderRequestIDs(customerOrderRequestIDs), ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable))
                    && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
                {
                    MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByCustomerOrderRequestIDs(customerOrderRequestIDs), ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
                }
                #endregion

                #region Reason
                MergeTable(DataAccess.CustomerOrderRequestReasonRelDA.GetByCustomerOrderRequestIDs(customerOrderRequestIDs), ds, Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable);
                MergeTable(DataAccess.ReasonChangeDA.GetAllReasonChange(), ds, BackOffice.Entities.TableNames.ReasonChangeTable);
                #endregion

                CustomerOrderRequestBasicAdvancedAdapter adapter = new CustomerOrderRequestBasicAdvancedAdapter();
                CustomerOrderRequestBasicEntity[] result = adapter.GetData(ds);
                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }


        /// <summary>
        /// para la vista de medico
        /// </summary>
        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByCustomer(int customerID)
        {
            try
            {
                if (customerID <= 0)
                    return null;

                Dictionary<int, int> dicOrderIDs = new Dictionary<int, int>();

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByCustomerID(customerID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    #region Orders
                    foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows)
                    {
                        int id = row["ID"] as int? ?? 0;
                        int orderID = row["OrderID"] as int? ?? 0;
                        dicOrderIDs.Add(id, orderID);
                    }
                    #endregion

                    #region Reasons
                    DataSet ds2 = DataAccess.CustomerOrderRequestReasonRelDA.GetByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region OrderRequestSchPlannings
                    ds2 = DataAccess.OrderRequestSchPlanningDA.GetOrderSchPlanningsByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable;
                        ds.Tables.Add(dt);
                    }

                    MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes),
                            ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);


                    ds2 = DataAccess.OrderRequestTimeDA.GetOrderRequestTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.ProcedureDA.GetProcedureBasesOfCustomerProceduresByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ProcedureBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.ProcedureBaseTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureDA.GetCustomerProcedures(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureTimeDA.GetCustomerProcedureTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.RoutineDA.GetRoutinesBasesOfCustomerRoutinesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.RoutineBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.RoutineBaseTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.CustomerRoutineDA.GetCustomerRoutines(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerRoutineTimeDA.GetCustomerRoutineTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.TimePatternDA.GetByTimestamp(0);
                    if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.TimePatternTable)
                        && (ds2.Tables[BackOffice.Entities.TableNames.TimePatternTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.TimePatternTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region OrderRequestProcedureRels
                    MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
                        MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
                    }
                    #endregion

                    #region OrderRequestRoutineRels
                    MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
                    if ((ds != null)
                        && ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable)
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
                    }
                    #endregion

                    #region OrderRequestHumanResourceRels
                    MergeTable(DataAccess.OrderRequestHumanResourceRelDA.GetOrderRequestHumanResourceRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable].Rows.Count > 0))
                    {
                        DatasetUtils.MergeTable(DataAccess.ParticipateAsDA.GetParticipatesAsByCustomerOrderRequestCustomer(customerID),
                                            ds,
                                            BackOffice.Entities.TableNames.ParticipateAsTable);
                    }
                    #endregion

                    #region OrderRequestResourceRels
                    MergeTable(DataAccess.OrderRequestResourceRelDA.GetOrderRequestResourceRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestResourceRelTable);
                    #endregion

                    #region OrderRequestEquipmentRels
                    MergeTable(DataAccess.OrderRequestEquipmentRelDA.GetOrderRequestEquipmentRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestEquipmentRelTable);
                    #endregion

                    #region OrderRequestLocationRels
                    MergeTable(DataAccess.OrderRequestLocationRelDA.GetOrderRequestLocationRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestLocationRelTable);
                    #endregion

                    #region PrescriptionRequest
                    ds2 = DataAccess.PrescriptionRequestDA.GetByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Copy();
                        ds.Tables.Add(dt);

                        ds2 = DataAccess.PrescriptionRequestTimeDA.GetByCustomerID(customerID);
                        if ((ds2 != null)
                            && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTimeTable))
                            && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Rows.Count > 0))
                        {
                            DataTable dt1 = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Copy();
                            ds.Tables.Add(dt1);
                        }


                        IEnumerable<int> pharmaceuticalFormIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("PharmaceuticalFormID"))
                                            .Where(i => i > 0)
                                            .Distinct()
                                            .OrderBy(i => i);
                        IEnumerable<int> administrationRouteIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationRouteID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);
                        IEnumerable<int> administrationMethodIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationMethodID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);

                        if (pharmaceuticalFormIDs.Any())
                        {
                            MergeTable(DataAccess.PharmaceuticalFormDA.GetPharmaceuticalFormByIDs(pharmaceuticalFormIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.PharmaceuticalFormTable);
                        }

                        if (administrationRouteIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationRouteDA.GetAdministrationRouteByIDs(administrationRouteIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationRouteTable);
                        }

                        if (administrationMethodIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationMethodDA.GetAdministrationMethodByIDs(administrationMethodIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationMethodTable);
                        }

                        IEnumerable<int> physicalUnitIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                .Select(r => r.Field<int>("PhysUnitID"))
                                .Where(i => i > 0)
                                .Distinct()
                                .OrderBy(i => i);
                        if (physicalUnitIDs.Any())
                        {
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByIDs(physicalUnitIDs.ToArray()),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }

                        int[] ids = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable]
                                .AsEnumerable()
                                .Where(r => (r["ID"] as int? ?? 0) > 0)
                                .Select(r => (r["ID"] as int? ?? 0))
                                .Distinct()
                                .OrderBy(id => id)
                                .ToArray();

                        MergeTable(DataAccess.LocationDA.GetLocationBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.LocationBaseTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.EquipmentDA.GetEquipmentBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.EquipmentBaseTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        //aqui hay que poner ItemTreatmentOrderSequenceTable
                        #region Item Treatment Order Sequences
                        ds2 = DataAccess.ItemTreatmentOrderSequenceDA.GetItemTreatmentOrderSequenceByPrescriptionRequestIDs(ids);
                        if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable))
                            && (ds2.Tables[BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable);

                            MergeTable(DataAccess.ItemDA.GetItemBaseByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.ItemBaseTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                            MergeTable(DataAccess.TimePatternDA.GetTimePatternBaseByRequestedGivePerTimeUnitByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.TimePatternTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }
                        #endregion

                    }
                    #endregion

                    #region OrderRequestBodySiteRels
                    MergeTable(DataAccess.OrderRequestBodySiteRelDA.GetOrderRequestBodySiteRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestBodySiteRelTable);
                    if ((ds != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestBodySiteRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestBodySiteRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.BodySiteDA.GetBodySitesByCustomerOrderRequestCustomer(customerID), ds, BackOffice.Entities.TableNames.BodySiteTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptsByCustomerOrderRequestCustomer(customerID), ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.BodySiteParticipationDA.GetBodySiteParticipationsByCustomerOrderRequestCustomer(customerID), ds, BackOffice.Entities.TableNames.BodySiteParticipationTable);
                    }
                    #endregion

                    #region OrderRequestRequirementRels
                    MergeTable(DataAccess.OrderRequestRequirementRelDA.GetOrderRequestRequirementRelsByCustomer(customerID),
                        ds, Administrative.Entities.TableNames.OrderRequestRequirementRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRequirementRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRequirementRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.RequirementDA.GetRequirementsByOrderRequestRequirementRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.RequirementTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByOrderRequestRequirementRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                    }
                    #endregion

                    #region OrderRequestConsentRels
                    MergeTable(DataAccess.OrderRequestConsentRelDA.GetOrderRequestConsentRelsByCustomer(customerID),
                        ds, Administrative.Entities.TableNames.OrderRequestConsentRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ConsentPreprintDA.GetConsentPreprintsByOrderRequestConsentRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.ConsentPreprintTable);
                        if ((ds.Tables != null)
                            && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                            && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                        {
                            MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelConsentPreprintCustomer(customerID),
                                ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                        }
                        MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                    }
                    #endregion


                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] result = adapter.GetData(ds);

                    if ((result != null) && (result.Length > 0))
                    {
                        _orderCache.OrderCache.UpdateCache();
                        foreach (CustomerOrderRequestEntity cor in result)
                        {
                            int orderID = dicOrderIDs[cor.ID];
                            cor.Order = _orderCache.OrderCache.Get(orderID, false);
                            cor.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(cor.CustomerID, cor.ID);
                            LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, cor.ID, ActionType.View);
                        }
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

        public CustomerOrderRequestEntity[] GetCustomerOnlySchOrderRequestsByCustomer(int customerID)
        {
            try
            {
                if (customerID <= 0)
                    return null;

                Dictionary<int, int> dicOrderIDs = new Dictionary<int, int>();

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetOnlySchByCustomerID(customerID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {

                    #region Orders
                    foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows)
                    {
                        int id = row["ID"] as int? ?? 0;
                        int orderID = row["OrderID"] as int? ?? 0;
                        dicOrderIDs.Add(id, orderID);
                    }
                    #endregion

                    #region Reasons
                    DataSet ds2 = DataAccess.CustomerOrderRequestReasonRelDA.GetByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region OrderRequestSchPlannings
                    ds2 = DataAccess.OrderRequestSchPlanningDA.GetOrderSchPlanningsByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable;
                        ds.Tables.Add(dt);
                    }

                    MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes),
                            ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);


                    ds2 = DataAccess.OrderRequestTimeDA.GetOrderRequestTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.ProcedureDA.GetProcedureBasesOfCustomerProceduresByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ProcedureBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.ProcedureBaseTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureDA.GetCustomerProcedures(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureTimeDA.GetCustomerProcedureTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.RoutineDA.GetRoutinesBasesOfCustomerRoutinesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.RoutineBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.RoutineBaseTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.CustomerRoutineDA.GetCustomerRoutines(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerRoutineTimeDA.GetCustomerRoutineTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable;
                        ds.Tables.Add(dt);
                    }


                    MergeTable(DataAccess.TimePatternDA.GetByTimestamp(0),
                                ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);

                    //ds2 = DataAccess.TimePatternDA.GetByTimestamp(0);
                    //if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.TimePatternTable)
                    //    && (ds2.Tables[BackOffice.Entities.TableNames.TimePatternTable].Rows.Count > 0))
                    //{
                    //    DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.TimePatternTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    #endregion

                    #region OrderRequestProcedureRels
                    MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
                        MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
                    }
                    #endregion

                    #region OrderRequestRoutineRels
                    MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
                    if ((ds != null)
                        && ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable)
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
                    }
                    #endregion

                    #region OrderRequestHumanResourceRels
                    MergeTable(DataAccess.OrderRequestHumanResourceRelDA.GetOrderRequestHumanResourceRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ParticipateAsDA.GetParticipatesAsByCustomerOrderRequestCustomer(customerID),
                                    ds,
                                    BackOffice.Entities.TableNames.ParticipateAsTable);
                    }
                    #endregion

                    #region OrderRequestResourceRels
                    MergeTable(DataAccess.OrderRequestResourceRelDA.GetOrderRequestResourceRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestResourceRelTable);
                    #endregion

                    #region OrderRequestEquipmentRels
                    MergeTable(DataAccess.OrderRequestEquipmentRelDA.GetOrderRequestEquipmentRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestEquipmentRelTable);
                    #endregion

                    #region OrderRequestLocationRels
                    MergeTable(DataAccess.OrderRequestLocationRelDA.GetOrderRequestLocationRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestLocationRelTable);
                    #endregion

                    #region PrescriptionRequest
                    ds2 = DataAccess.PrescriptionRequestDA.GetByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Copy();
                        ds.Tables.Add(dt);

                        ds2 = DataAccess.PrescriptionRequestTimeDA.GetByCustomerID(customerID);
                        if ((ds2 != null)
                            && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTimeTable))
                            && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Rows.Count > 0))
                        {
                            DataTable dt1 = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Copy();
                            ds.Tables.Add(dt1);
                        }

                        IEnumerable<int> pharmaceuticalFormIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("PharmaceuticalFormID"))
                                            .Where(i => i > 0)
                                            .Distinct()
                                            .OrderBy(i => i);
                        IEnumerable<int> administrationRouteIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationRouteID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);
                        IEnumerable<int> administrationMethodIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationMethodID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);

                        if (pharmaceuticalFormIDs.Any())
                        {
                            MergeTable(DataAccess.PharmaceuticalFormDA.GetPharmaceuticalFormByIDs(pharmaceuticalFormIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.PharmaceuticalFormTable);
                        }

                        if (administrationRouteIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationRouteDA.GetAdministrationRouteByIDs(administrationRouteIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationRouteTable);
                        }

                        if (administrationMethodIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationMethodDA.GetAdministrationMethodByIDs(administrationMethodIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationMethodTable);
                        }

                        IEnumerable<int> physicalUnitIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                            .Select(r => r.Field<int>("PhysUnitID"))
                            .Where(i => i > 0)
                            .Distinct()
                            .OrderBy(i => i);
                        if (physicalUnitIDs.Any())
                        {
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByIDs(physicalUnitIDs.ToArray()),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }



                        int[] ids = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable]
                            .AsEnumerable()
                            .Where(r => (r["ID"] as int? ?? 0) > 0)
                            .Select(r => (r["ID"] as int? ?? 0))
                            .Distinct()
                            .OrderBy(id => id)
                            .ToArray();

                        MergeTable(DataAccess.LocationDA.GetLocationBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.LocationBaseTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.EquipmentDA.GetEquipmentBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.EquipmentBaseTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        //aqui hay que poner ItemTreatmentOrderSequenceTable
                        #region Item Treatment Order Sequences
                        ds2 = DataAccess.ItemTreatmentOrderSequenceDA.GetItemTreatmentOrderSequenceByPrescriptionRequestIDs(ids);
                        if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable))
                            && (ds2.Tables[BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable);

                            MergeTable(DataAccess.ItemDA.GetItemBaseByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.ItemBaseTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                            MergeTable(DataAccess.TimePatternDA.GetTimePatternBaseByRequestedGivePerTimeUnitByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.TimePatternTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }
                        #endregion

                    }
                    #endregion

                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] result = adapter.GetData(ds);

                    if ((result != null) && (result.Length > 0))
                    {
                        _orderCache.OrderCache.UpdateCache();
                        foreach (CustomerOrderRequestEntity cor in result)
                        {
                            int orderID = dicOrderIDs[cor.ID];
                            cor.Order = _orderCache.OrderCache.Get(orderID, false);
                            cor.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(cor.CustomerID, cor.ID);
                            LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, cor.ID, ActionType.View);
                        }
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

        public CustomerOrderRequestEntity GetLastCustomerOrderRequestsByCustomerAndOrderID(int customerID, int orderID)
        {
            try
            {
                if (customerID <= 0 || orderID <= 0) return null;
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetLastCustomerOrderRequestByCustomerIDAndOrderID(customerID, orderID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    int corID = ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows[0]["ID"] as int? ?? 0;
                    return (corID > 0) ? GetCustomerOrderRequest(corID, false) : null;
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

        public CustomerOrderRequestBaseEntity[] GetCustomerOrderRequestsByCustomerAndCOR(int customerID, int customerOrderRequestID)
        {
            try
            {
                if (customerID <= 0 || customerOrderRequestID <= 0)
                    return null;
                int orderID = DataAccess.OrderDA.GetOrderIDByCORID(customerOrderRequestID);
                return GetCustomerOrderRequestsByCustomerAndOrder(customerID, orderID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestBaseEntity[] GetCustomerOrderRequestsByCustomerAndOrder(int customerID, int orderID)
        {
            try
            {
                if (customerID <= 0 || orderID <= 0)
                    return null;

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByCustomerIDAndOrderID(customerID, orderID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    #region Orders
                    DataSet ds2 = DataAccess.OrderDA.GetOrdersByCustomer(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.OrderTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.OrderTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.OrderTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.OrderTable;
                        ds.Tables.Add(dt);

                        OrderObservationRelDA _orderObservationRelDA = new OrderObservationRelDA();
                        OrderObsNotificationCriterionDA _orderObsNotificationCriterionDA = new OrderObsNotificationCriterionDA();
                        OrderProcedureRelDA _orderProcedureRelDA = new OrderProcedureRelDA();
                        OrderProcedureRoutineRelDA _orderProcedureRoutineRelDA = new OrderProcedureRoutineRelDA();
                        OrderRoutineRelDA _orderRoutineRelDA = new OrderRoutineRelDA();
                        NotificationDA _notificationDA = new NotificationDA();
                        ObservationValueDA _observationValueDA = new ObservationValueDA();

                        #region Order Procedures
                        ds2 = _orderProcedureRelDA.GetOrderProcedureRelsByCustomerAndOrder(customerID, orderID);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.OrderProcedureRelTable)))
                        {
                            DataTable dt1 = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.OrderProcedureRelTable].Copy();
                            dt1.TableName = SII.HCD.BackOffice.Entities.TableNames.OrderProcedureRelTable;
                            ds.Tables.Add(dt1);
                        }

                        ds2 = _orderProcedureRoutineRelDA.GetOrderProcedureRoutineRelsByCustomerAndOrder(customerID, orderID);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.OrderProcedureRoutineRelTable)))
                        {
                            DataTable dt1 = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.OrderProcedureRoutineRelTable].Copy();
                            dt1.TableName = SII.HCD.BackOffice.Entities.TableNames.OrderProcedureRoutineRelTable;
                            ds.Tables.Add(dt1);
                        }
                        #endregion

                        #region Order Routines
                        ds2 = _orderRoutineRelDA.GetOrderRoutineRelsByCustomerAndOrder(customerID, orderID);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.OrderRoutineRelTable)))
                        {
                            DataTable dt1 = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.OrderRoutineRelTable].Copy();
                            dt1.TableName = SII.HCD.BackOffice.Entities.TableNames.OrderRoutineRelTable;
                            ds.Tables.Add(dt1);
                        }
                        #endregion

                        #region Order Observations
                        ds2 = _orderObservationRelDA.GetObservationRelsByCustomerAndOrder(customerID, orderID);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationRelTable)))
                        {
                            DataTable dt1 = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationRelTable].Copy();
                            dt1.TableName = SII.HCD.BackOffice.Entities.TableNames.ObservationRelTable;
                            ds.Tables.Add(dt1);
                        }

                        ds2 = _orderObsNotificationCriterionDA.GetObsNotificationCriterionsByCustomerAndOrder(customerID, orderID);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObsNotificationCriterionTable)))
                        {
                            DataTable dt1 = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObsNotificationCriterionTable].Copy();
                            dt1.TableName = SII.HCD.BackOffice.Entities.TableNames.ObsNotificationCriterionTable;
                            ds.Tables.Add(dt1);
                        }

                        ds2 = _notificationDA.GetNotificationsByCustomerAndOrder(customerID, orderID);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.NotificationTable)))
                        {
                            DataTable dt1 = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.NotificationTable].Copy();
                            dt1.TableName = SII.HCD.BackOffice.Entities.TableNames.ObsNotificationTable;
                            ds.Tables.Add(dt1);
                        }

                        ds2 = _observationValueDA.GetObservationValuesByCustomerAndOrder(customerID, orderID);
                        if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable)))
                        {
                            DataTable dt1 = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                            dt1.TableName = SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable;
                            ds.Tables.Add(dt1);
                        }
                        #endregion
                    }
                    #endregion

                    CustomerOrderRequestBaseAdvancedAdapter customerOrderRequestAdapter = new CustomerOrderRequestBaseAdvancedAdapter();
                    CustomerOrderRequestBaseEntity[] myOrderRequests = customerOrderRequestAdapter.GetData(ds);
                    return myOrderRequests;
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
        /// para la vista de medico
        /// </summary>
        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByCustomer(int customerID, int[] medicalEpisodeIDs)
        {
            try
            {
                if (customerID <= 0)
                    return null;

                if (medicalEpisodeIDs != null && medicalEpisodeIDs.Length < 0)
                    return GetCustomerOrderRequestsByCustomer(customerID);

                Dictionary<int, int> dicOrderIDs = new Dictionary<int, int>();

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByCustomerID(customerID, medicalEpisodeIDs);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    #region Orders
                    foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows)
                    {
                        int id = row["ID"] as int? ?? 0;
                        int orderID = row["OrderID"] as int? ?? 0;
                        dicOrderIDs.Add(id, orderID);
                    }
                    #endregion

                    #region Reasons
                    DataSet ds2 = DataAccess.CustomerOrderRequestReasonRelDA.GetByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region OrderRequestSchPlannings
                    ds2 = DataAccess.OrderRequestSchPlanningDA.GetOrderSchPlanningsByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable;
                        ds.Tables.Add(dt);
                    }

                    MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes),
                            ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);


                    ds2 = DataAccess.OrderRequestTimeDA.GetOrderRequestTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.ProcedureDA.GetProcedureBasesOfCustomerProceduresByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ProcedureBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.ProcedureBaseTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureDA.GetCustomerProcedures(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureTimeDA.GetCustomerProcedureTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.RoutineDA.GetRoutinesBasesOfCustomerRoutinesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.RoutineBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.RoutineBaseTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.CustomerRoutineDA.GetCustomerRoutines(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerRoutineTimeDA.GetCustomerRoutineTimesByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable;
                        ds.Tables.Add(dt);
                    }

                    MergeTable(DataAccess.TimePatternDA.GetByTimestamp(0), ds, BackOffice.Entities.TableNames.TimePatternTable);


                    //ds2 = DataAccess.TimePatternDA.GetByTimestamp(0);
                    //if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.TimePatternTable)
                    //    && (ds2.Tables[BackOffice.Entities.TableNames.TimePatternTable].Rows.Count > 0))
                    //{
                    //    DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.TimePatternTable].Copy();
                    //    ds.Tables.Add(dt);
                    //}
                    #endregion

                    #region OrderRequestProcedureRels
                    MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
                        MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
                    }
                    #endregion

                    #region OrderRequestRoutineRels
                    MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
                    if ((ds != null)
                        && ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable)
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
                    }
                    #endregion

                    #region PrescriptionRequest
                    ds2 = DataAccess.PrescriptionRequestDA.GetByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Copy();
                        ds.Tables.Add(dt);

                        ds2 = DataAccess.PrescriptionRequestTimeDA.GetByCustomerID(customerID);
                        if ((ds2 != null)
                            && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTimeTable))
                            && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Rows.Count > 0))
                        {
                            DataTable dt1 = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Copy();
                            ds.Tables.Add(dt1);
                        }

                        IEnumerable<int> pharmaceuticalFormIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("PharmaceuticalFormID"))
                                            .Where(i => i > 0)
                                            .Distinct()
                                            .OrderBy(i => i);
                        IEnumerable<int> administrationRouteIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationRouteID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);
                        IEnumerable<int> administrationMethodIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationMethodID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);

                        if (pharmaceuticalFormIDs.Any())
                        {
                            MergeTable(DataAccess.PharmaceuticalFormDA.GetPharmaceuticalFormByIDs(pharmaceuticalFormIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.PharmaceuticalFormTable);
                        }

                        if (administrationRouteIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationRouteDA.GetAdministrationRouteByIDs(administrationRouteIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationRouteTable);
                        }

                        if (administrationMethodIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationMethodDA.GetAdministrationMethodByIDs(administrationMethodIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationMethodTable);
                        }

                        IEnumerable<int> physicalUnitIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                            .Select(r => r.Field<int>("PhysUnitID"))
                            .Where(i => i > 0)
                            .Distinct()
                            .OrderBy(i => i);
                        if (physicalUnitIDs.Any())
                        {
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByIDs(physicalUnitIDs.ToArray()),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }

                        int[] ids = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable]
                            .AsEnumerable()
                            .Where(r => (r["ID"] as int? ?? 0) > 0)
                            .Select(r => (r["ID"] as int? ?? 0))
                            .Distinct()
                            .OrderBy(id => id)
                            .ToArray();

                        MergeTable(DataAccess.LocationDA.GetLocationBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.LocationBaseTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.EquipmentDA.GetEquipmentBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.EquipmentBaseTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        //aqui hay que poner ItemTreatmentOrderSequenceTable
                        #region Item Treatment Order Sequences
                        ds2 = DataAccess.ItemTreatmentOrderSequenceDA.GetItemTreatmentOrderSequenceByPrescriptionRequestIDs(ids);
                        if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable))
                            && (ds2.Tables[BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable);

                            MergeTable(DataAccess.ItemDA.GetItemBaseByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.ItemBaseTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                            MergeTable(DataAccess.TimePatternDA.GetTimePatternBaseByRequestedGivePerTimeUnitByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.TimePatternTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }
                        #endregion

                    }
                    #endregion

                    #region OrderRequestHumanResourceRels
                    MergeTable(DataAccess.OrderRequestHumanResourceRelDA.GetOrderRequestHumanResourceRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ParticipateAsDA.GetParticipatesAsByCustomerOrderRequestCustomer(customerID),
                                    ds,
                                    BackOffice.Entities.TableNames.ParticipateAsTable);
                    }
                    #endregion

                    #region OrderRequestResourceRels
                    MergeTable(DataAccess.OrderRequestResourceRelDA.GetOrderRequestResourceRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestResourceRelTable);
                    #endregion

                    #region OrderRequestEquipmentRels
                    MergeTable(DataAccess.OrderRequestEquipmentRelDA.GetOrderRequestEquipmentRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestEquipmentRelTable);
                    #endregion

                    #region OrderRequestLocationRels
                    MergeTable(DataAccess.OrderRequestLocationRelDA.GetOrderRequestLocationRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestLocationRelTable);
                    #endregion

                    #region OrderRequestRequirementRels
                    MergeTable(DataAccess.OrderRequestRequirementRelDA.GetOrderRequestRequirementRelsByCustomer(customerID),
                        ds, Administrative.Entities.TableNames.OrderRequestRequirementRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRequirementRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRequirementRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.RequirementDA.GetRequirementsByOrderRequestRequirementRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.RequirementTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByOrderRequestRequirementRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                    }
                    #endregion

                    #region OrderRequestBodySiteRels
                    MergeTable(DataAccess.OrderRequestBodySiteRelDA.GetOrderRequestBodySiteRelsByCustomer(customerID), ds, Administrative.Entities.TableNames.OrderRequestBodySiteRelTable);
                    if ((ds != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestBodySiteRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestBodySiteRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.BodySiteDA.GetBodySitesByCustomerOrderRequestCustomer(customerID), ds, BackOffice.Entities.TableNames.BodySiteTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptsByCustomerOrderRequestCustomer(customerID), ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.BodySiteParticipationDA.GetBodySiteParticipationsByCustomerOrderRequestCustomer(customerID), ds, BackOffice.Entities.TableNames.BodySiteParticipationTable);
                    }
                    #endregion

                    #region OrderRequestConsentRels
                    MergeTable(DataAccess.OrderRequestConsentRelDA.GetOrderRequestConsentRelsByCustomer(customerID),
                        ds, Administrative.Entities.TableNames.OrderRequestConsentRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ConsentPreprintDA.GetConsentPreprintsByOrderRequestConsentRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.ConsentPreprintTable);
                        if ((ds.Tables != null)
                            && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                            && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                        {
                            MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelConsentPreprintCustomer(customerID),
                                ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                        }
                        MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelCustomer(customerID),
                            ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                    }
                    #endregion

                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] result = adapter.GetData(ds);

                    if ((result != null) && (result.Length > 0))
                    {
                        _orderCache.OrderCache.UpdateCache();
                        foreach (CustomerOrderRequestEntity cor in result)
                        {
                            int orderID = dicOrderIDs[cor.ID];
                            cor.Order = _orderCache.OrderCache.Get(orderID, false);
                            cor.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(cor.CustomerID, cor.ID);

                            LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, cor.ID, ActionType.View);
                        }
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

        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByCustomerMedEpisodeAct(int customerMedEpisodeActID)
        {
            try
            {
                List<CustomerOrderRequestEntity> result = new List<CustomerOrderRequestEntity>();

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByCustomerMedEpisodeActID(customerMedEpisodeActID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] cors = adapter.GetData(ds);

                    if ((cors != null) && (cors.Length > 0))
                    {
                        foreach (CustomerOrderRequestEntity cOR in cors)
                        {
                            CustomerOrderRequestEntity myOrderRequest = this.GetCustomerOrderRequest(cOR.ID, false);
                            result.Add(myOrderRequest);
                        }
                        return result.ToArray();
                    }
                    return null;
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

        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsWithActivePrescriptionByCustomer(int customerID)
        {
            try
            {
                List<CustomerOrderRequestEntity> customerOrderRequestList = new List<CustomerOrderRequestEntity>();
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetActiveDrugPrescriptionByCustomerID(customerID, (int)OrderClassTypeEnum.DrugPrescription);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    #region OrderRequestSchPlannings
                    DataSet ds2 = DataAccess.OrderRequestSchPlanningDA.GetActiveOrderSchPlanningByCustomerID(customerID, (int)OrderClassTypeEnum.DrugPrescription);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable;
                        ds.Tables.Add(dt);
                    }

                    MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes),
                            ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);

                    ds2 = DataAccess.OrderRequestTimeDA.GetActiveOrderRequestTimesByCustomerID(customerID, (int)OrderClassTypeEnum.DrugPrescription);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.PrescriptionRequestDA.GetByCustomerID(customerID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Copy();
                        ds.Tables.Add(dt);

                        ds2 = DataAccess.PrescriptionRequestTimeDA.GetByCustomerID(customerID);
                        if ((ds2 != null)
                            && (ds2.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTimeTable))
                            && (ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Rows.Count > 0))
                        {
                            DataTable dt1 = ds2.Tables[Administrative.Entities.TableNames.PrescriptionRequestTimeTable].Copy();
                            ds.Tables.Add(dt1);
                        }

                        IEnumerable<int> pharmaceuticalFormIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("PharmaceuticalFormID"))
                                            .Where(i => i > 0)
                                            .Distinct()
                                            .OrderBy(i => i);
                        IEnumerable<int> administrationRouteIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationRouteID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);
                        IEnumerable<int> administrationMethodIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                                                                    .Select(r => r.Field<int>("AdministrationMethodID"))
                                                                    .Where(i => i > 0)
                                                                    .Distinct()
                                                                    .OrderBy(i => i);

                        if (pharmaceuticalFormIDs.Any())
                        {
                            MergeTable(DataAccess.PharmaceuticalFormDA.GetPharmaceuticalFormByIDs(pharmaceuticalFormIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.PharmaceuticalFormTable);
                        }

                        if (administrationRouteIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationRouteDA.GetAdministrationRouteByIDs(administrationRouteIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationRouteTable);
                        }

                        if (administrationMethodIDs.Any())
                        {
                            MergeTable(DataAccess.AdministrationMethodDA.GetAdministrationMethodByIDs(administrationMethodIDs.ToArray()),
                                    ds, BackOffice.Entities.TableNames.AdministrationMethodTable);
                        }

                        IEnumerable<int> physicalUnitIDs = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                            .Select(r => r.Field<int>("PhysUnitID"))
                            .Where(i => i > 0)
                            .Distinct()
                            .OrderBy(i => i);
                        if (physicalUnitIDs.Any())
                        {
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByIDs(physicalUnitIDs.ToArray()),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }

                        int[] ids = ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable]
                            .AsEnumerable()
                            .Where(r => (r["ID"] as int? ?? 0) > 0)
                            .Select(r => (r["ID"] as int? ?? 0))
                            .Distinct()
                            .OrderBy(id => id)
                            .ToArray();

                        MergeTable(DataAccess.LocationDA.GetLocationBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.LocationBaseTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.EquipmentDA.GetEquipmentBaseByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.EquipmentBaseTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitByGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        //aqui hay que poner ItemTreatmentOrderSequenceTable
                        #region Item Treatment Order Sequences
                        ds2 = DataAccess.ItemTreatmentOrderSequenceDA.GetItemTreatmentOrderSequenceByPrescriptionRequestIDs(ids);
                        if ((ds2 != null) && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable))
                            && (ds2.Tables[BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable);

                            MergeTable(DataAccess.ItemDA.GetItemBaseByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.ItemBaseTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                            MergeTable(DataAccess.TimePatternDA.GetTimePatternBaseByRequestedGivePerTimeUnitByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.TimePatternTable);
                            MergeTable(DataAccess.PhysUnitDA.GetPhysUnitOfRequestedGiveStrengthUnitsByPrescriptionRequestIDs(ids),
                                ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                        }
                        #endregion

                    }
                    #endregion

                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] result = adapter.GetData(ds);
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

        public CustomerOrderRequestEntity[] GetCustomerOrderRequestsByCustomerEpisode(int customerEpisodeID)
        {
            try
            {
                Dictionary<int, int> dicOrderIDs = new Dictionary<int, int>();

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByCustomerEpisodeID(customerEpisodeID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    DataSet ds2 = null;

                    #region Orders
                    foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows)
                    {
                        int id = row["ID"] as int? ?? 0;
                        int orderID = row["OrderID"] as int? ?? 0;
                        dicOrderIDs.Add(id, orderID);
                    }
                    #endregion

                    #region Reasons
                    ds2 = DataAccess.CustomerOrderRequestReasonRelDA.GetByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable))
                        && (ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region OrderRequestSchPlannings
                    ds2 = DataAccess.OrderRequestSchPlanningDA.GetOrderSchPlanningsByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestSchPlanningTable;
                        ds.Tables.Add(dt);
                    }

                    MergeTable(DataAccess.TimePatternDA.GetByPatternType(PatternTypeEnum.DurationTypes), ds, SII.HCD.BackOffice.Entities.TableNames.TimePatternTable);

                    ds2 = DataAccess.OrderRequestTimeDA.GetOrderRequestTimesByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.ProcedureDA.GetProcedureBasesByCustomerEpisode(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.ProcedureBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.ProcedureBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.ProcedureBaseTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureDA.GetCustomerEpisodeProcedures(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerProcedureTimeDA.GetCustomerProcedureTimesByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerProcedureTimeTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.RoutineDA.GetRoutinesBasesByCustomerEpisode(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(BackOffice.Entities.TableNames.RoutineBaseTable))
                        && (ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[BackOffice.Entities.TableNames.RoutineBaseTable].Copy();
                        dt.TableName = BackOffice.Entities.TableNames.RoutineBaseTable;
                        ds.Tables.Add(dt);
                    }

                    ds2 = DataAccess.CustomerRoutineDA.GetCustomerEpisodeRoutines(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTable;
                        ds.Tables.Add(dt);
                    }
                    ds2 = DataAccess.CustomerRoutineTimeDA.GetCustomerRoutineTimesByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2 != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable].Copy();
                        dt.TableName = SII.HCD.Administrative.Entities.TableNames.CustomerRoutineTimeTable;
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region OrderRequestProcedureRels
                    MergeTable(DataAccess.OrderRequestProcedureRelDA.GetOrderRequestProcedureRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestProcedureRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestProcedureRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestProcedureTimeDA.GetOrderRequestProcedureTimesByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestProcedureTimeTable);
                        MergeTable(DataAccess.OrderRequestProcedureRoutineRelDA.GetOrderRequestProcedureRoutineRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable);
                    }
                    #endregion

                    #region OrderRequestRoutineRels
                    MergeTable(DataAccess.OrderRequestRoutineRelDA.GetOrderRequestRoutineRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestRoutineRelTable);
                    if ((ds != null)
                        && ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRoutineRelTable)
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRoutineRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.OrderRequestRoutineTimeDA.GetOrderRequestRoutineTimesByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestRoutineTimeTable);
                    }
                    #endregion

                    #region OrderRequestHumanResourceRels
                    MergeTable(DataAccess.OrderRequestHumanResourceRelDA.GetOrderRequestHumanResourceRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ParticipateAsDA.GetParticipatesAsByCustomerOrderRequestCustomerEpisode(customerEpisodeID),
                                    ds,
                                    BackOffice.Entities.TableNames.ParticipateAsTable);
                    }
                    #endregion

                    #region OrderRequestResourceRels
                    MergeTable(DataAccess.OrderRequestResourceRelDA.GetOrderRequestResourceRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestResourceRelTable);
                    #endregion

                    #region OrderRequestEquipmentRels
                    MergeTable(DataAccess.OrderRequestEquipmentRelDA.GetOrderRequestEquipmentRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestEquipmentRelTable);
                    #endregion

                    #region OrderRequestLocationRels
                    MergeTable(DataAccess.OrderRequestLocationRelDA.GetOrderRequestLocationRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestLocationRelTable);
                    #endregion

                    #region ADTRequestInfo
                    ds2 = DataAccess.OrderRequestADTInfoDA.GetOrderRequestADTInfoByCustomerEpisodeID(customerEpisodeID);
                    if ((ds2.Tables != null)
                        && (ds2.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.OrderRequestADTInfoTable))
                        && (ds2.Tables[SII.HCD.Administrative.Entities.TableNames.OrderRequestADTInfoTable].Rows.Count > 0))
                    {
                        DataTable dt = ds2.Tables[Administrative.Entities.TableNames.OrderRequestADTInfoTable].Copy();
                        ds.Tables.Add(dt);
                    }
                    #endregion

                    #region OrderRequestRequirementRels
                    MergeTable(DataAccess.OrderRequestRequirementRelDA.GetOrderRequestRequirementRelsByCustomerEpisode(customerEpisodeID),
                        ds, Administrative.Entities.TableNames.OrderRequestRequirementRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestRequirementRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestRequirementRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.RequirementDA.GetRequirementsByOrderRequestRequirementRelCustomerEpisode(customerEpisodeID),
                            ds, BackOffice.Entities.TableNames.RequirementTable);
                        MergeTable(DataAccess.PhysUnitDA.GetPhysUnitsByOrderRequestRequirementRelCustomerEpisode(customerEpisodeID),
                            ds, BackOffice.Entities.TableNames.PhysicalUnitTable);
                    }
                    #endregion

                    #region OrderRequestBodySiteRels
                    MergeTable(DataAccess.OrderRequestBodySiteRelDA.GetOrderRequestBodySiteRelsByCustomerEpisode(customerEpisodeID), ds, Administrative.Entities.TableNames.OrderRequestBodySiteRelTable);
                    if ((ds != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestBodySiteRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestBodySiteRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.BodySiteDA.GetBodySitesByCustomerOrderRequestCustomerEpisode(customerEpisodeID), ds, BackOffice.Entities.TableNames.BodySiteTable);
                        MergeTable(DataAccess.BodySiteConceptDA.GetBodySiteConceptsByCustomerOrderRequestCustomerEpisode(customerEpisodeID), ds, BackOffice.Entities.TableNames.BodySiteConceptTable);
                        MergeTable(DataAccess.BodySiteParticipationDA.GetBodySiteParticipationsByCustomerOrderRequestCustomerEpisode(customerEpisodeID), ds, BackOffice.Entities.TableNames.BodySiteParticipationTable);
                    }
                    #endregion

                    #region OrderRequestConsentRels
                    MergeTable(DataAccess.OrderRequestConsentRelDA.GetOrderRequestConsentRelsByCustomerEpisode(customerEpisodeID),
                        ds, Administrative.Entities.TableNames.OrderRequestConsentRelTable);
                    if ((ds.Tables != null)
                        && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                        && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                    {
                        MergeTable(DataAccess.ConsentPreprintDA.GetConsentPreprintsByOrderRequestConsentRelCustomerEpisode(customerEpisodeID),
                            ds, BackOffice.Entities.TableNames.ConsentPreprintTable);
                        if ((ds.Tables != null)
                            && (ds.Tables.Contains(Administrative.Entities.TableNames.OrderRequestConsentRelTable))
                            && (ds.Tables[Administrative.Entities.TableNames.OrderRequestConsentRelTable].Rows.Count > 0))
                        {
                            MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelConsentPreprintCustomerEpisode(customerEpisodeID),
                                ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                        }
                        MergeTable(DataAccess.ConsentTypeDA.GetConsentTypesByOrderRequestConsentTypeRelCustomerEpisode(customerEpisodeID),
                            ds, BackOffice.Entities.TableNames.ConsentTypeTable);
                    }
                    #endregion

                    CustomerOrderRequestAdvancedAdapter adapter = new CustomerOrderRequestAdvancedAdapter();
                    CustomerOrderRequestEntity[] result = adapter.GetData(ds);

                    if ((result != null) && (result.Length > 0))
                    {
                        _orderCache.OrderCache.UpdateCache();
                        foreach (CustomerOrderRequestEntity cor in result)
                        {
                            int orderID = dicOrderIDs[cor.ID];
                            cor.Order = _orderCache.OrderCache.Get(orderID, false);
                            cor.RegisteredObservations = CustomerObservationBL.GetRegisteredLayoutByCustomerAndOrderRequest(cor.CustomerID, cor.ID);

                            LOPDLogger.Write(EntityNames.CustomerOrderRequestEntityName, cor.ID, ActionType.View);
                        }
                    }

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

        public bool CustomerOrderRequestIsScheduled(int customerOrderRequestID)
        {
            try
            {
                return (customerOrderRequestID > 0) ? DataAccess.CustomerOrderRequestDA.CustomerOrderRequestIsScheduled(customerOrderRequestID) : false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public void SetCustomerOrderRequestPrinted(int[] selectedCustomerOrderRequestIDs)
        {
            try
            {
                string userName = IdentityUser.GetIdentityUserName();
                if ((selectedCustomerOrderRequestIDs != null) && (selectedCustomerOrderRequestIDs.Length > 0))
                {
                    foreach (int id in selectedCustomerOrderRequestIDs)
                    {
                        DataAccess.CustomerOrderRequestDA.UpdateOrderPrinted(id, userName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public CustomerOrderRequestEntity Change(CustomerOrderRequestEntity newCustomerOrderRequest,
            CustomerOrderRequestEntity oldCustomerOrderRequest, int reasonChangeID, string explanation)
        {
            if (newCustomerOrderRequest == null)
                throw new ArgumentException("newCustomerOrderRequest");

            if (oldCustomerOrderRequest == null)
                throw new ArgumentException("oldCustomerOrderRequest");

            CustomerOrderRequestUoWChangeBL service = new CustomerOrderRequestUoWChangeBL();
            return service.Change(newCustomerOrderRequest, oldCustomerOrderRequest, reasonChangeID, explanation);
        }

        public CustomerOrderRequestEntity AddOrder(CustomerOrderRequestEntity newCustomerOrderRequest,
            CustomerOrderRequestEntity oldCustomerOrderRequest)
        {
            if (newCustomerOrderRequest == null)
                throw new ArgumentException("newCustomerOrderRequest");

            if (oldCustomerOrderRequest == null)
                throw new ArgumentException("oldCustomerOrderRequest");

            CustomerOrderRequestUoWChangeBL service = new CustomerOrderRequestUoWChangeBL();
            return service.AddOrder(newCustomerOrderRequest, oldCustomerOrderRequest);
        }

        public override CustomerOrderRequestEntity Save(CustomerOrderRequestEntity entity)
        {
            try
            {
                if (entity.ID <= 0)
                    base.Save(entity, true);
                else
                {
                    ValidationResults vr = new ValidationResults();
                    this.Save(entity, true, vr);
                }
                return entity;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public void Confirm(int customerOrderRequestID)
        {
            try
            {
                if (customerOrderRequestID <= 0)
                    throw new ArgumentException("customerOrderRequestID");

                CustomerOrderRequestEntity customerOrderRequest = GetByID(customerOrderRequestID);
                if (customerOrderRequest == null)
                    throw new NullReferenceException(
                        string.Format(
                        Properties.Resources.ERROR_CustomerOrderRequestNotFound, customerOrderRequestID));

                Confirm(customerOrderRequest);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public CustomerOrderRequestEntity Confirm(CustomerOrderRequestEntity customerOrderRequest)
        {
            Confirm(customerOrderRequest, true, null);

            return GetByID(customerOrderRequest.ID);
        }

        public void Confirm(CustomerOrderRequestEntity customerOrderRequest,
            bool commitActions = true, ValidationResults validationResults = null, HL7MessagingProcessor hl7processor = null)
        {
            try
            {
                if (customerOrderRequest == null)
                    throw new ArgumentException("customerOrderRequest");

                ValidationResults vr = (validationResults != null)
                    ? validationResults
                    : new ValidationResults();

                //Procesar
                CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
                CustomerOrderRequestEntity[] customerOrderRequestList =
                    corAnalyzer.Confirm(customerOrderRequest);

                //Verificamos que hay CustomerOrderRequest que confirmar
                //Esto no debería ocurrir nunca, pues si no hay ninguna debería ser 
                //por algun error procesado en la sentencia anterior
                if (customerOrderRequestList == null || customerOrderRequestList.Length <= 0)
                    throw new NullReferenceException("customerOrderRequestList");


                //Una vez completado el proceso de confirmación, guardamos orden principal
                Save(corAnalyzer.CustomerOrderRequest, false, vr);

                //si la orden principal era de tipo Parent a podido generar hijas estas hay quer relacionarla con su padre para que se guarde correctamente la relacion con su padre
                if (corAnalyzer.ChildrenCustomerOrderRequests != null
                    && corAnalyzer.ChildrenCustomerOrderRequests.Any())
                {
                    foreach (CustomerOrderRequestEntity childCOR in corAnalyzer.ChildrenCustomerOrderRequests)
                    {
                        ParentCustomerOrderRequestChildCustomerOrderRequestRelationship parentCORChildCORRel =
                            GetRelationship<CustomerOrderRequestEntity, CustomerOrderRequestEntity,
                                ParentCustomerOrderRequestChildCustomerOrderRequestRelationship>(corAnalyzer.CustomerOrderRequest, childCOR);
                        parentCORChildCORRel.Parent.EditStatus.New();
                        UnitOfWork.New(parentCORChildCORRel);
                    }

                    //Una vez relacionadas con su padre guardamos
                    Save(corAnalyzer.ChildrenCustomerOrderRequests.ToArray(), false, vr);
                }

                //Si hay relaciones con RoutineActs, los guardamos
                if (corAnalyzer.CustomerRoutineActRelationships != null
                    && corAnalyzer.CustomerRoutineActRelationships.Any())
                    HandleBasicListActions<CustomerRoutineRoutineActRelationship>(
                        corAnalyzer.CustomerRoutineActRelationships, vr);

                //Si hay relaciones con ProcedureActs, los guardamos
                if (corAnalyzer.CustomerProcedureActRelationships != null
                    && corAnalyzer.CustomerProcedureActRelationships.Any())
                    HandleBasicListActions<CustomerProcedureProcedureActRelationship>(
                        corAnalyzer.CustomerProcedureActRelationships, vr);

                //Si hay relaciones con Prescripciones, los guardamos ya que el PrescriptionRequest está guardado
                //Para ello, marcaremos como actualizado el PrescriptionRequest y lo añadiremos al UnitOfWork
                if (corAnalyzer.PrescriptionRequestCustomerProcedureRelationships != null
                    && corAnalyzer.PrescriptionRequestCustomerProcedureRelationships.Any())
                {
                    foreach (PrescriptionRequestCustomerProcedureRelationship item in
                        corAnalyzer.PrescriptionRequestCustomerProcedureRelationships)
                    {
                        HandleBasicActions<PrescriptionRequestCustomerProcedureRelationship>(
                            item, vr);
                        item.Parent.EditStatus.Update();
                        UnitOfWork.Update(item.Parent);
                    }
                }

                //Si hay RoutineActs, las guardamos
                if (corAnalyzer.RoutineActs != null && corAnalyzer.RoutineActs.Any())
                    RoutineActBL.Save(corAnalyzer.RoutineActs.ToArray(), false, vr);

                //Si hay ProcedureActs, los guardamos
                //Si hay relaciones con Prescripciones, los guardamos ya que el PrescriptionRequest está guardado
                //Para ello, marcaremos como actualizado el PrescriptionRequest y lo añadiremos al UnitOfWork
                if (corAnalyzer.CustomerOrderRequestProcedureActRelationships != null
                    && corAnalyzer.CustomerOrderRequestProcedureActRelationships.Any())
                {
                    foreach (CustomerOrderRequestProcedureActRelationship item in
                        corAnalyzer.CustomerOrderRequestProcedureActRelationships)
                    {
                        HandleBasicActions<CustomerOrderRequestProcedureActRelationship>(
                            item, vr);
                    }
                }

                if (corAnalyzer.ProcedureActs != null && corAnalyzer.ProcedureActs.Any())
                    ProcedureActBL.Save(corAnalyzer.ProcedureActs.ToArray(), false, vr);

                //Si hay un CustomerorderRealization, lo guardamos
                if (corAnalyzer.CustomerOrderRealizations != null && corAnalyzer.CustomerOrderRealizations.Any())
                    CustomerOrderRealizationBL.Save(corAnalyzer.CustomerOrderRealizations.ToArray(), false, vr);


                ////////////////////////////////////////////////////////////////////////////
                ///LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                ////////////////////////////////////////////////////////////////////////////
                // est no necesita que se le pase nigún HL7MessagingProcessor porque es primario
                // tampoco importa el retorno del método porque siempre se registrará la solicitud
                // si hay error dentro del mesnaje se devuelve en el vr por lo que no se haría el commit
                ////////////////////////////////////////////////////////////////////////////
                bool returnIsProcessing = hl7processor != null;
                HL7MessagingProcessor thishl7processor = hl7processor ?? new HL7MessagingProcessor(null, null);
                if (!returnIsProcessing)
                    thishl7processor.SetMessageInProgess();
                bool relizeCommit = AnalysisSendORMMessage(corAnalyzer, HL7IDISEventEnum.NewOrder, returnIsProcessing, thishl7processor, vr);
                if (!returnIsProcessing)
                    thishl7processor.ResetMessageInProgess();
                ////////////////////////////////////////////////////////////////////////////
                ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                ////////////////////////////////////////////////////////////////////////////

                //Realizamos el Commit de los cambios
                if (commitActions && relizeCommit)
                {
                    //Thread.Sleep(5000);

                    Commit(vr);
                }

                if (!relizeCommit)
                {
                    ProcessValidationResult(vr);
                }

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public void Cancel(int customerOrderRequestID, int reasonChangeID, string explanation)
        {
            if (customerOrderRequestID <= 0)
                throw new ArgumentException("customerOrderRequestID");

            CustomerOrderRequestCancelParameters parameters = new CustomerOrderRequestCancelParameters()
            {
                CustomerOrderRequestID = customerOrderRequestID,
                CustomerOrderRequestCancelReasonID = reasonChangeID,
                CustomerOrderRequestCancelReasonExplanation = explanation,

                CustomerProcedureCancelReasonID = reasonChangeID,
                CustomerProcedureCancelReasonExplanation = explanation,

                CustomerRoutineCancelReasonID = reasonChangeID,
                CustomerRoutineCancelReasonExplanation = explanation,

                ProcedureActCancelReasonID = reasonChangeID,
                ProcedureActCancelReasonExplanation = explanation,

                RoutineActCancelReasonID = reasonChangeID,
                RoutineActCancelReasonExplanation = explanation,
            };

            Cancel(parameters, true, null, null);
        }

        public void Cancel(CustomerOrderRequestEntity customerOrderRequest, int reasonChangeID, string explanation,
            HL7MessagingProcessor hl7processor = null)
        {
            if (customerOrderRequest == null)
                throw new ArgumentException("customerOrderRequest");

            CustomerOrderRequestCancelParameters parameters = new CustomerOrderRequestCancelParameters()
            {
                CustomerOrderRequestID = customerOrderRequest.ID,
                CustomerOrderRequestCancelReasonID = reasonChangeID,
                CustomerOrderRequestCancelReasonExplanation = explanation,

                CustomerProcedureCancelReasonID = reasonChangeID,
                CustomerProcedureCancelReasonExplanation = explanation,

                CustomerRoutineCancelReasonID = reasonChangeID,
                CustomerRoutineCancelReasonExplanation = explanation,

                ProcedureActCancelReasonID = reasonChangeID,
                ProcedureActCancelReasonExplanation = explanation,

                RoutineActCancelReasonID = reasonChangeID,
                RoutineActCancelReasonExplanation = explanation,
            };

            Cancel(parameters, true, null, hl7processor);
        }

        public void Cancel(int customerOrderRequestID, int reasonChangeID, string explanation,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null)
        {
            if (customerOrderRequestID <= 0)
                throw new ArgumentException("customerOrderRequestID");

            CustomerOrderRequestCancelParameters parameters = new CustomerOrderRequestCancelParameters()
            {
                CustomerOrderRequestID = customerOrderRequestID,
                CustomerOrderRequestCancelReasonID = reasonChangeID,
                CustomerOrderRequestCancelReasonExplanation = explanation,
            };

            Cancel(parameters, commitActions, validationResults, hl7processor);
        }

        public void Cancel(CustomerOrderRequestCancelParameters parameters,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null
            )
        {
            try
            {
                if (parameters == null)
                    throw new ArgumentNullException("customerOrderRequestID");

                if (parameters.CustomerOrderRequestID <= 0)
                    throw new ArgumentException("customerOrderRequestID");

                CustomerOrderRequestEntity customerOrderRequest = FindCustomerOrderRequest(parameters.CustomerOrderRequestID);
                if (customerOrderRequest == null)
                    throw new NullReferenceException(
                        string.Format(Properties.Resources.ERROR_CustomerOrderRequestNotFound, parameters.CustomerOrderRequestID));

                ValidationResults vr = validationResults ?? new ValidationResults();

                //Procesar
                CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
                CustomerOrderRequestEntity[] customerOrderRequestList =
                    corAnalyzer.Cancel(customerOrderRequest, parameters.DateTimeLimit);

                //Verificamos que hay CustomerOrderRequest que confirmar
                //Esto no debería ocurrir nunca, pues si no hay ninguna debería ser 
                //por algun error procesado en la sentencia anterior
                if (customerOrderRequestList == null || customerOrderRequestList.Length <= 0)
                    throw new NullReferenceException("customerOrderRequestList");

                //Cancelamos las tomas posteriores a la fecha de referencia de la cancelación
                if (parameters.DateTimeLimit.HasValue)
                {
                    CancelActivityAfterDateTime(customerOrderRequest, parameters.DateTimeLimit,
                        parameters.ProcedureActCancelReasonID,
                        parameters.ProcedureActCancelReasonExplanation,
                        validationResults);
                }

                //Añadimos razones a CustomerOrderRequests
                AddReasonToCustomerOrderRequests(customerOrderRequestList,
                    parameters.CustomerOrderRequestCancelReasonID,
                    parameters.CustomerOrderRequestCancelReasonExplanation);

                //Añadimos razones a CustomerRoutines
                AddReasonToCustomerRoutines(corAnalyzer.CustomerRoutines,
                    parameters.CustomerRoutineCancelReasonID,
                    parameters.CustomerRoutineCancelReasonExplanation);

                //Añadimos razones a CustomerProcedures
                AddReasonToCustomerProcedures(corAnalyzer.CustomerProcedures,
                    parameters.CustomerProcedureCancelReasonID,
                    parameters.CustomerProcedureCancelReasonExplanation);

                //Añadimos razones a RoutineAct
                AddReasonToRoutinesActs(corAnalyzer.RoutineActs,
                    parameters.RoutineActCancelReasonID,
                    parameters.RoutineActCancelReasonExplanation);

                //Añadimos razones a ProcedureAct
                AddReasonToProcedureActs(corAnalyzer.ProcedureActs,
                    parameters.ProcedureActCancelReasonID,
                    parameters.ProcedureActCancelReasonExplanation);

                //Marcamos con Placed (si aun no lo están) todas y cada una de las ordenes a cancelar
                foreach (CustomerOrderRequestEntity cor in customerOrderRequestList)
                {
                    if (!cor.Placed)
                    {
                        cor.Placed = true;
                        cor.EditStatus.Update();
                    }
                }

                //Una vez completado el proceso de confirmación, guardamos
                Save(customerOrderRequestList, false, vr);

                //Si hay RoutineActs, las guardamos
                if (corAnalyzer.RoutineActs != null && corAnalyzer.RoutineActs.Any())
                    RoutineActBL.Save(corAnalyzer.RoutineActs.ToArray(), false, vr);

                //Si hay ProcedureActs, los guardamos
                if (corAnalyzer.ProcedureActs != null && corAnalyzer.ProcedureActs.Any())
                    ProcedureActBL.Save(corAnalyzer.ProcedureActs.ToArray(), false, vr);

                //Si hay CustomerorderRealizations, lo guardamos
                if (corAnalyzer.CustomerOrderRealizations != null && corAnalyzer.CustomerOrderRealizations.Any())
                    CustomerOrderRealizationBL.Save(corAnalyzer.CustomerOrderRealizations.ToArray(), false, vr);


                ////////////////////////////////////////////////////////////////////////////
                //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                ////////////////////////////////////////////////////////////////////////////
                //// SI NO VIENE EL PROCESSOR LO CREO
                ////////////////////////////////////////////////////////////////////////////
                bool returnIsProcessing = hl7processor != null;
                HL7MessagingProcessor thishl7processor = hl7processor ?? new HL7MessagingProcessor(null, null);
                if (!returnIsProcessing)
                    thishl7processor.SetMessageInProgess();
                ////////////////////////////////////////////////////////////////////////////
                // este necesita que se le pase HL7MessagingProcessor porque no es primario
                // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de la cancelación de la solicitud
                ////////////////////////////////////////////////////////////////////////////
                bool relizeCommit = AnalysisSendORMMessage(corAnalyzer, HL7IDISEventEnum.CancelOrder, returnIsProcessing, thishl7processor, vr);
                if (!returnIsProcessing)
                    thishl7processor.ResetMessageInProgess();
                ////////////////////////////////////////////////////////////////////////////
                ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                ////////////////////////////////////////////////////////////////////////////

                //Realizamos el Commit de los cambios
                if (commitActions && relizeCommit)
                {
                    Commit(vr);
                }

                if (!relizeCommit)
                {
                    ProcessValidationResult(vr);
                }

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public void Reject(int customerOrderRequestID, int reasonChangeID, string explanation,
            bool commitActions = true, ValidationResults validationResults = null)
        {
            Reject(customerOrderRequestID, reasonChangeID, explanation, commitActions, validationResults, null);
        }

        public void Reject(int customerOrderRequestID, int reasonChangeID, string explanation,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null)
        {
            try
            {
                if (customerOrderRequestID <= 0)
                    throw new ArgumentException("customerOrderRequestID");

                CustomerOrderRequestEntity customerOrderRequest = GetByID(customerOrderRequestID);
                if (customerOrderRequest == null)
                    throw new NullReferenceException(
                        string.Format(Properties.Resources.ERROR_CustomerOrderRequestNotFound, customerOrderRequestID));

                ValidationResults vr = validationResults ?? new ValidationResults();

                AddReasonCustomerOrderRequest(customerOrderRequest, reasonChangeID, explanation, vr);

                //Procesar
                CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
                CustomerOrderRequestEntity[] customerOrderRequestList =
                    corAnalyzer.Reject(customerOrderRequest);

                //Verificamos que hay CustomerOrderRequest que confirmar
                //Esto no debería ocurrir nunca, pues si no hay ninguna debería ser 
                //por algun error procesado en la sentencia anterior
                if (customerOrderRequestList == null || customerOrderRequestList.Length <= 0)
                    throw new NullReferenceException("customerOrderRequestList");

                //Marcamos con Placed (si aun no lo están) todas y cada una de las ordenes a rechazar
                foreach (CustomerOrderRequestEntity cor in customerOrderRequestList)
                {
                    if (!cor.Placed)
                    {
                        cor.Placed = true;
                        cor.EditStatus.Update();
                    }
                }

                //Una vez completado el proceso de confirmación, guardamos
                Save(customerOrderRequestList, false, vr);

                //Si hay RoutineActs, las guardamos
                if (corAnalyzer.RoutineActs != null && corAnalyzer.RoutineActs.Any())
                    RoutineActBL.Save(corAnalyzer.RoutineActs.ToArray(), false, vr);

                //Si hay ProcedureActs, los guardamos
                if (corAnalyzer.ProcedureActs != null && corAnalyzer.ProcedureActs.Any())
                    ProcedureActBL.Save(corAnalyzer.ProcedureActs.ToArray(), false, vr);

                //Si hay CustomerorderRealizations, lo guardamos
                if (corAnalyzer.CustomerOrderRealizations != null && corAnalyzer.CustomerOrderRealizations.Any())
                    CustomerOrderRealizationBL.Save(corAnalyzer.CustomerOrderRealizations.ToArray(), false, vr);

                ////////////////////////////////////////////////////////////////////////////
                //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                ////////////////////////////////////////////////////////////////////////////
                //// SI NO VIENE EL PROCESSOR LO CREO
                ////////////////////////////////////////////////////////////////////////////
                bool returnIsProcessing = hl7processor != null;
                HL7MessagingProcessor thishl7processor = hl7processor ?? new HL7MessagingProcessor(null, null);
                if (!returnIsProcessing)
                    thishl7processor.SetMessageInProgess();
                ////////////////////////////////////////////////////////////////////////////
                // este necesita que se le pase HL7MessagingProcessor porque no es primario
                // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de descontinuar la solicitud
                //HAY QUE BUSCAR UN EVENTO PARA FINALIZAR ORDER
                ////////////////////////////////////////////////////////////////////////////
                bool relizeCommit = AnalysisSendORMMessage(corAnalyzer, HL7IDISEventEnum.DiscontinueOrder, false, thishl7processor, vr);
                if (!returnIsProcessing)
                    thishl7processor.ResetMessageInProgess();
                ////////////////////////////////////////////////////////////////////////////
                ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                ////////////////////////////////////////////////////////////////////////////

                //Realizamos el Commit de los cambios
                if (commitActions && relizeCommit)
                //if (commitActions)
                {
                    Commit(vr);
                }

                if (!relizeCommit)
                {
                    ProcessValidationResult(vr);
                }

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public void Undo(int customerOrderRequestID, int reasonChangeID, string explanation)//Todo pasará al estado Supercedeed
        {
            try
            {
                if (customerOrderRequestID <= 0)
                    throw new ArgumentException("customerOrderRequestID");

                CustomerOrderRequestEntity customerOrderRequest = GetByID(customerOrderRequestID);
                if (customerOrderRequest == null)
                    throw new NullReferenceException(
                        string.Format(Properties.Resources.ERROR_CustomerOrderRequestNotFound, customerOrderRequestID));
                UndoSaveProcess(customerOrderRequest, reasonChangeID, explanation);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public void Undo(CustomerOrderRequestEntity customerOrderRequest, int reasonChangeID, string explanation,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null)//Todo pasará al estado Supercedeed
        {
            try
            {
                if (customerOrderRequest == null)
                    throw new ArgumentException("customerOrderRequest");
                UndoSaveProcess(customerOrderRequest, reasonChangeID, explanation, commitActions, validationResults, hl7processor);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        private void UndoSaveProcess(CustomerOrderRequestEntity customerOrderRequest, int reasonChangeID, string explanation,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null
            )
        {
            CustomerOrderRequestEntity parentCustomerOrderRequest = GetByID(customerOrderRequest.ParentCustomerOrderRequestID);
            ValidationResults vr = validationResults ?? new ValidationResults();

            AddReasonCustomerOrderRequest(customerOrderRequest, reasonChangeID, explanation, vr);

            //Procesar
            CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
            CustomerOrderRequestEntity[] customerOrderRequestList =
                corAnalyzer.Undo(customerOrderRequest, parentCustomerOrderRequest);

            //Verificamos que hay CustomerOrderRequest que eliminar
            //Esto no debería ocurrir nunca, pues si no hay ninguna debería ser 
            //por algun error procesado en la sentencia anterior
            if (customerOrderRequestList == null || customerOrderRequestList.Length <= 0)
                throw new NullReferenceException("customerOrderRequestList");

            //Marcamos con Placed (si aun no lo están) todas y cada una de las ordenes a cancelar
            foreach (CustomerOrderRequestEntity cor in customerOrderRequestList)
            {
                if (!cor.Placed)
                {
                    cor.Placed = true;
                    cor.EditStatus.Update();
                }
            }

            //Una vez completado el proceso de elminación, guardamos
            Save(customerOrderRequestList, false, vr);

            //Si hay RoutineActs, las guardamos
            if (corAnalyzer.RoutineActs != null && corAnalyzer.RoutineActs.Any())
                RoutineActBL.Save(corAnalyzer.RoutineActs.ToArray(), false, vr);

            //Si hay ProcedureActs, los guardamos
            if (corAnalyzer.ProcedureActs != null && corAnalyzer.ProcedureActs.Any())
                ProcedureActBL.Save(corAnalyzer.ProcedureActs.ToArray(), false, vr);

            //Si hay CustomerorderRealizations, lo guardamos
            if (corAnalyzer.CustomerOrderRealizations != null && corAnalyzer.CustomerOrderRealizations.Any())
                CustomerOrderRealizationBL.Save(corAnalyzer.CustomerOrderRealizations.ToArray(), false, vr);

            ////////////////////////////////////////////////////////////////////////////
            //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
            ////////////////////////////////////////////////////////////////////////////
            //// SI NO VIENE EL PROCESSOR LO CREO
            ////////////////////////////////////////////////////////////////////////////
            bool returnIsProcessing = hl7processor != null;
            HL7MessagingProcessor thishl7processor = hl7processor ?? new HL7MessagingProcessor(null, null);
            if (!returnIsProcessing)
                thishl7processor.SetMessageInProgess();
            ////////////////////////////////////////////////////////////////////////////
            // este necesita que se le pase HL7MessagingProcessor porque no es primario
            // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de descontinuar la solicitud
            ////////////////////////////////////////////////////////////////////////////
            bool relizeCommit = AnalysisSendORMMessage(corAnalyzer, HL7IDISEventEnum.DiscontinueOrder, returnIsProcessing, thishl7processor, vr);
            if (!returnIsProcessing)
                thishl7processor.ResetMessageInProgess();
            ////////////////////////////////////////////////////////////////////////////
            ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
            ////////////////////////////////////////////////////////////////////////////

            //Realizamos el Commit de los cambios
            if (commitActions && relizeCommit)
            //if (commitActions)
            {
                //Realizamos el Commit de los cambios
                Commit(vr);
            }

            if (!relizeCommit)
            {
                ProcessValidationResult(vr);
            }

        }

        public void Finalize(int customerOrderRequestID, int reasonChangeID, string explanation)
        {
            try
            {
                if (customerOrderRequestID <= 0)
                    throw new ArgumentException("customerOrderRequestID");

                CustomerOrderRequestFinalizeParameters parameters = new CustomerOrderRequestFinalizeParameters()
                {
                    CustomerOrderRequestID = customerOrderRequestID,
                    DateTime = DateTime.Now,
                    CustomerOrderRequestFinalizeReasonID = reasonChangeID,
                    CustomerOrderRequestFinalizeReasonExplanation = explanation,
                    CheckMinimumDate = false
                };

                Finalize(parameters, true, null, null);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        //public void Finalize(int customerOrderRequestID, int reasonChangeID, string explanation,
        //    bool commitActions = true, ValidationResults validationResults = null)
        //{
        //    try
        //    {
        //        if (customerOrderRequestID <= 0)
        //            throw new ArgumentException("customerOrderRequestID");

        //        CustomerOrderRequestFinalizeParameters parameters = new CustomerOrderRequestFinalizeParameters()
        //        {
        //            CustomerOrderRequestID = customerOrderRequestID,
        //            DateTime = DateTime.Now,
        //            CustomerOrderRequestFinalizeReasonID = reasonChangeID,
        //            CustomerOrderRequestFinalizeReasonExplanation = explanation,
        //            CheckMinimumDate = false
        //        };

        //        Finalize(parameters, commitActions, validationResults);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
        //    }
        //}


        public void Finalize(CustomerOrderRequestEntity customerOrderRequest, DateTime finalizeDateTime, int reasonChangeID,
                string explanation, HL7MessagingProcessor hl7processor = null)
        {
            try
            {
                if (customerOrderRequest == null)
                    throw new ArgumentException("customerOrderRequest");

                CustomerOrderRequestFinalizeParameters parameters = new CustomerOrderRequestFinalizeParameters()
                {
                    CustomerOrderRequestID = customerOrderRequest.ID,
                    DateTime = finalizeDateTime,
                    CustomerOrderRequestFinalizeReasonID = reasonChangeID,
                    CustomerOrderRequestFinalizeReasonExplanation = explanation,
                    CheckMinimumDate = false
                };

                Finalize(parameters, true, null, hl7processor);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }



        public void Finalize(int customerOrderRequestID, DateTime finalizeDateTime, int reasonChangeID,
            string explanation)
        {
            try
            {
                if (customerOrderRequestID <= 0)
                    throw new ArgumentException("customerOrderRequestID");

                CustomerOrderRequestFinalizeParameters parameters = new CustomerOrderRequestFinalizeParameters()
                {
                    CustomerOrderRequestID = customerOrderRequestID,
                    DateTime = finalizeDateTime,
                    CustomerOrderRequestFinalizeReasonID = reasonChangeID,
                    CustomerOrderRequestFinalizeReasonExplanation = explanation,
                    CheckMinimumDate = false
                };

                Finalize(parameters, true, null, null);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        //public void Finalize(int customerOrderRequestID, DateTime finalizeDateTime, int reasonChangeID,
        //    string explanation, bool commitActions = true, ValidationResults validationResults = null)
        //{
        //    if (customerOrderRequestID <= 0)
        //        throw new ArgumentException("customerOrderRequestID");

        //    CustomerOrderRequestFinalizeParameters parameters = new CustomerOrderRequestFinalizeParameters()
        //    {
        //        CustomerOrderRequestID = customerOrderRequestID,
        //        DateTime = finalizeDateTime,
        //        CustomerOrderRequestFinalizeReasonID = reasonChangeID,
        //        CustomerOrderRequestFinalizeReasonExplanation = explanation,
        //        CheckMinimumDate = false
        //    };

        //    Finalize(parameters, commitActions, validationResults);
        //}

        public void Finalize(CustomerOrderRequestFinalizeParameters parameters,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null)
        {
            try
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters");

                if (parameters.CustomerOrderRequestID <= 0)
                    throw new ArgumentException("parameters.CustomerOrderRequestID");

                CustomerOrderRequestEntity customerOrderRequest = FindCustomerOrderRequest(parameters.CustomerOrderRequestID);
                if (customerOrderRequest == null)
                    throw new NullReferenceException(
                        string.Format(Properties.Resources.ERROR_CustomerOrderRequestNotFound, parameters.CustomerOrderRequestID));

                ValidationResults vr = validationResults ?? new ValidationResults();

                if (customerOrderRequest.OrderRequestSchPlanning != null)
                {
                    if (parameters.CheckMinimumDate)
                    {
                        customerOrderRequest.OrderRequestSchPlanning.EstimatedFinalizeAt = DateUtils.Min(customerOrderRequest.OrderRequestSchPlanning.EstimatedFinalizeAt, parameters.DateTime);
                    }
                    else
                    {
                        customerOrderRequest.OrderRequestSchPlanning.EstimatedFinalizeAt = parameters.DateTime;
                    }
                    customerOrderRequest.OrderRequestSchPlanning.EditStatus.Update();
                    ValidateDateTimeFinalization(customerOrderRequest, vr);
                }
                else
                {
                    vr.AddResult(new ValidationResult(
                        string.Format(Properties.Resources.ERROR_OrderNotFinalizedNotPlanning, customerOrderRequest.OrderNumber),
                        this, null, null, null));
                }

                bool relizeCommit = true;

                if (vr.IsValid)
                {
                    //Procesar
                    CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
                    CustomerOrderRequestEntity[] customerOrderRequestList =
                        corAnalyzer.Finalize(customerOrderRequest, parameters.DateTime);

                    //Cancelamos las tomas posteriores a la fecha de referencia de la cancelación
                    CancelActivityAfterDateTime(customerOrderRequest, parameters.DateTime,
                        parameters.ProcedureActFinalizeReasonID,
                        parameters.ProcedureActFinalizeReasonExplanation,
                        vr);

                    //Añadimos razones a CustomerOrderRequests
                    AddReasonToCustomerOrderRequests(customerOrderRequestList,
                        parameters.CustomerOrderRequestFinalizeReasonID,
                        parameters.CustomerOrderRequestFinalizeReasonExplanation);

                    //Añadimos razones a CustomerRoutines
                    AddReasonToCustomerRoutines(corAnalyzer.CustomerRoutines,
                        parameters.CustomerRoutineFinalizeReasonID,
                        parameters.CustomerRoutineFinalizeReasonExplanation);

                    //Añadimos razones a CustomerProcedures
                    AddReasonToCustomerProcedures(corAnalyzer.CustomerProcedures,
                        parameters.CustomerProcedureFinalizeReasonID,
                        parameters.CustomerProcedureFinalizeReasonExplanation);

                    //Añadimos razones a RoutineAct
                    AddReasonToRoutinesActs(corAnalyzer.RoutineActs,
                        parameters.RoutineActFinalizeReasonID,
                        parameters.RoutineActFinalizeReasonExplanation);

                    //Añadimos razones a ProcedureAct
                    AddReasonToProcedureActs(corAnalyzer.ProcedureActs,
                        parameters.ProcedureActFinalizeReasonID,
                        parameters.ProcedureActFinalizeReasonExplanation);

                    //Verificamos que hay CustomerOrderRequest que confirmar
                    //Esto no debería ocurrir nunca, pues si no hay ninguna debería ser 
                    //por algun error procesado en la sentencia anterior
                    if (customerOrderRequestList == null || customerOrderRequestList.Length <= 0)
                        throw new NullReferenceException("customerOrderRequestList");

                    //Una vez completado el proceso de confirmación, guardamos
                    Save(customerOrderRequestList, false, vr);

                    //Si hay RoutineActs, las guardamos
                    if (corAnalyzer.RoutineActs != null && corAnalyzer.RoutineActs.Any())
                        RoutineActBL.Save(corAnalyzer.RoutineActs.ToArray(), false, vr);

                    //Si hay ProcedureActs, los guardamos
                    if (corAnalyzer.ProcedureActs != null && corAnalyzer.ProcedureActs.Any())
                        ProcedureActBL.Save(corAnalyzer.ProcedureActs.ToArray(), false, vr);

                    //Si hay CustomerorderRealizations, lo guardamos
                    if (corAnalyzer.CustomerOrderRealizations != null && corAnalyzer.CustomerOrderRealizations.Any())
                        CustomerOrderRealizationBL.Save(corAnalyzer.CustomerOrderRealizations.ToArray(), false, vr);




                    ////////////////////////////////////////////////////////////////////////////
                    //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                    ////////////////////////////////////////////////////////////////////////////
                    //// SI NO VIENE EL PROCESSOR LO CREO
                    ////////////////////////////////////////////////////////////////////////////
                    bool returnIsProcessing = hl7processor != null;
                    HL7MessagingProcessor thishl7processor = hl7processor ?? new HL7MessagingProcessor(null, null);
                    if (!returnIsProcessing)
                        thishl7processor.SetMessageInProgess();
                    ////////////////////////////////////////////////////////////////////////////
                    // este necesita que se le pase HL7MessagingProcessor porque no es primario
                    // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de descontinuar la solicitud
                    //HAY QUE BUSCAR UN EVENTO PARA FINALIZAR ORDER
                    ////////////////////////////////////////////////////////////////////////////
                    relizeCommit = AnalysisSendORMMessage(corAnalyzer, HL7IDISEventEnum.DiscontinueOrder, returnIsProcessing, thishl7processor, vr);
                    if (!returnIsProcessing)
                        thishl7processor.ResetMessageInProgess();
                    ////////////////////////////////////////////////////////////////////////////
                    ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                    ////////////////////////////////////////////////////////////////////////////


                }

                //Realizamos el Commit de los cambios
                if (commitActions && relizeCommit)
                //if (commitActions)
                {
                    //Realizamos el Commit de los cambios
                    Commit(vr);
                }

                if (!relizeCommit)
                {
                    ProcessValidationResult(vr);
                }


                //Enviar notifications
                CustomerOrderRealizationBL.NotificationActBL.SendNotifications();
                CustomerOrderRealizationBL.NotificationActBL.ResetNotifications();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public void UndoFinalize(CustomerOrderRequestFinalizeParameters parameters,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null)
        {
            try
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters");

                if (parameters.CustomerOrderRequestID <= 0)
                    throw new ArgumentException("parameters.CustomerOrderRequestID");

                CustomerOrderRequestEntity customerOrderRequest = FindCustomerOrderRequest(parameters.CustomerOrderRequestID);
                if (customerOrderRequest == null)
                    throw new NullReferenceException(
                        string.Format(Properties.Resources.ERROR_CustomerOrderRequestNotFound, parameters.CustomerOrderRequestID));

                ValidationResults vr = validationResults ?? new ValidationResults();

                DateTime? finalizedDateTime = null;
                if (customerOrderRequest.OrderRequestSchPlanning != null
                    && customerOrderRequest.OrderRequestSchPlanning.EstimatedFinalizeAt.HasValue)
                {
                    finalizedDateTime = customerOrderRequest.OrderRequestSchPlanning.EstimatedFinalizeAt;
                    customerOrderRequest.OrderRequestSchPlanning.EstimatedFinalizeAt = null;
                    customerOrderRequest.OrderRequestSchPlanning.EditStatus.Update();
                }
                else
                {
                    vr.AddResult(new ValidationResult(
                        string.Format(Properties.Resources.ERROR_OrderNotFinalizedNotPlanning, customerOrderRequest.OrderNumber),
                        this, null, null, null));
                }

                bool relizeCommit = true;
                if (vr.IsValid)
                {
                    //Procesar
                    CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
                    CustomerOrderRequestEntity[] customerOrderRequestList =
                        corAnalyzer.UndoFinalize(customerOrderRequest);

                    //Eliminamos razones a CustomerOrderRequests
                    RemoveReasonToCustomerOrderRequests(customerOrderRequestList,
                        parameters.CustomerOrderRequestFinalizeReasonID,
                        parameters.CustomerOrderRequestFinalizeReasonExplanation);

                    //Eliminamos razones a CustomerRoutines
                    RemoveReasonToCustomerRoutines(corAnalyzer.CustomerRoutines,
                        parameters.CustomerRoutineFinalizeReasonID,
                        parameters.CustomerRoutineFinalizeReasonExplanation);

                    //Eliminamos razones a CustomerProcedures
                    RemoveReasonToCustomerProcedures(corAnalyzer.CustomerProcedures,
                        parameters.CustomerProcedureFinalizeReasonID,
                        parameters.CustomerProcedureFinalizeReasonExplanation);

                    //Eliminamos razones a RoutineAct
                    RemoveReasonToRoutinesActs(corAnalyzer.RoutineActs,
                        parameters.RoutineActFinalizeReasonID,
                        parameters.RoutineActFinalizeReasonExplanation);

                    //Eliminamos razones a ProcedureAct
                    RemoveReasonToProcedureActs(corAnalyzer.ProcedureActs,
                        parameters.ProcedureActFinalizeReasonID,
                        parameters.ProcedureActFinalizeReasonExplanation);

                    //Verificamos que hay CustomerOrderRequest que confirmar
                    //Esto no debería ocurrir nunca, pues si no hay ninguna debería ser 
                    //por algun error procesado en la sentencia anterior
                    if (customerOrderRequestList == null || customerOrderRequestList.Length <= 0)
                        throw new NullReferenceException("customerOrderRequestList");

                    //Una vez completado el proceso de confirmación, guardamos
                    Save(customerOrderRequestList, false, vr);

                    //Si hay RoutineActs, las guardamos
                    if (corAnalyzer.RoutineActs != null && corAnalyzer.RoutineActs.Any())
                        RoutineActBL.Save(corAnalyzer.RoutineActs.ToArray(), false, vr);

                    //Si hay ProcedureActs, los guardamos
                    if (corAnalyzer.ProcedureActs != null && corAnalyzer.ProcedureActs.Any())
                        ProcedureActBL.Save(corAnalyzer.ProcedureActs.ToArray(), false, vr);

                    //Si hay CustomerOrderRealizations, lo guardamos
                    if (corAnalyzer.CustomerOrderRealizations != null && corAnalyzer.CustomerOrderRealizations.Any())
                        CustomerOrderRealizationBL.Save(corAnalyzer.CustomerOrderRealizations.ToArray(), false, vr);


                    ////////////////////////////////////////////////////////////////////////////
                    //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                    ////////////////////////////////////////////////////////////////////////////
                    //// SI NO VIENE EL PROCESSOR LO CREO
                    ////////////////////////////////////////////////////////////////////////////
                    bool returnIsProcessing = hl7processor != null;
                    HL7MessagingProcessor thishl7processor = hl7processor ?? new HL7MessagingProcessor(null, null);
                    if (!returnIsProcessing)
                        thishl7processor.SetMessageInProgess();
                    ////////////////////////////////////////////////////////////////////////////
                    // este necesita que se le pase HL7MessagingProcessor porque no es primario
                    // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de descontinuar la solicitud
                    //HAY QUE BUSCAR UN EVENTO PARA FINALIZAR ORDER
                    ////////////////////////////////////////////////////////////////////////////
                    relizeCommit = AnalysisSendORMMessage(corAnalyzer, HL7IDISEventEnum.ReleaseOrder, returnIsProcessing, thishl7processor, vr);
                    if (!returnIsProcessing)
                        thishl7processor.ResetMessageInProgess();
                    ////////////////////////////////////////////////////////////////////////////
                    ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                    ////////////////////////////////////////////////////////////////////////////

                }

                //Realizamos el Commit de los cambios
                if (commitActions && relizeCommit)
                //if (commitActions)
                {
                    //Realizamos el Commit de los cambios
                    Commit(vr);
                }

                if (!relizeCommit)
                {
                    ProcessValidationResult(vr);
                }

                //Enviar notifications
                CustomerOrderRealizationBL.NotificationActBL.SendNotifications();
                CustomerOrderRealizationBL.NotificationActBL.ResetNotifications();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        public void Activate(CustomerOrderRequestEntity customerOrderRequest, bool commitActions = true,
            ValidationResults validationResults = null, HL7MessagingProcessor hl7processor = null)//Todo pasará al estado Confirmed
        {
            try
            {
                if (customerOrderRequest == null)
                    throw new ArgumentException("customerOrderRequest");
                ActivateSaveProcess(customerOrderRequest, commitActions, validationResults, hl7processor);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            }
        }

        private void ActivateSaveProcess(CustomerOrderRequestEntity customerOrderRequest,
            bool commitActions = true, ValidationResults validationResults = null,
            HL7MessagingProcessor hl7processor = null)
        {
            CustomerOrderRequestEntity parentCustomerOrderRequest = GetByID(customerOrderRequest.ParentCustomerOrderRequestID);
            ValidationResults vr = validationResults ?? new ValidationResults();

            //Procesar
            CustomerOrderRequestAnalyzer corAnalyzer = new CustomerOrderRequestAnalyzer(this, vr);
            CustomerOrderRequestEntity[] customerOrderRequestList =
                corAnalyzer.Activate(customerOrderRequest, parentCustomerOrderRequest);

            //Verificamos que hay CustomerOrderRequest que eliminar
            //Esto no debería ocurrir nunca, pues si no hay ninguna debería ser 
            //por algun error procesado en la sentencia anterior
            if (customerOrderRequestList == null || customerOrderRequestList.Length <= 0)
                throw new NullReferenceException("customerOrderRequestList");

            //Marcamos NO Placed (si aun no lo están) todas y cada una de las ordenes a ACTIVAR
            foreach (CustomerOrderRequestEntity cor in customerOrderRequestList)
            {
                if (cor.Placed)
                {
                    cor.Placed = false;
                    cor.EditStatus.Update();
                }
            }

            //Una vez completado el proceso de activacion, guardamos
            Save(customerOrderRequestList, false, vr);

            //Si hay RoutineActs, las guardamos
            if (corAnalyzer.RoutineActs != null && corAnalyzer.RoutineActs.Any())
                RoutineActBL.Save(corAnalyzer.RoutineActs.ToArray(), false, vr);

            //Si hay ProcedureActs, los guardamos
            if (corAnalyzer.ProcedureActs != null && corAnalyzer.ProcedureActs.Any())
                ProcedureActBL.Save(corAnalyzer.ProcedureActs.ToArray(), false, vr);

            //Si hay CustomerorderRealizations, lo guardamos
            if (corAnalyzer.CustomerOrderRealizations != null && corAnalyzer.CustomerOrderRealizations.Any())
                CustomerOrderRealizationBL.Save(corAnalyzer.CustomerOrderRealizations.ToArray(), false, vr);

            ////////////////////////////////////////////////////////////////////////////
            //// LLAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
            ////////////////////////////////////////////////////////////////////////////
            //// SI NO VIENE EL PROCESSOR LO CREO
            ////////////////////////////////////////////////////////////////////////////
            bool returnIsProcessing = hl7processor != null;
            HL7MessagingProcessor thishl7processor = hl7processor ?? new HL7MessagingProcessor(null, null);
            if (!returnIsProcessing)
                thishl7processor.SetMessageInProgess();
            ////////////////////////////////////////////////////////////////////////////
            // este necesita que se le pase HL7MessagingProcessor porque no es primario
            // y hay que capturar el retorno del método porque NO siempre se podrá registrar el commit de descontinuar la solicitud
            ////////////////////////////////////////////////////////////////////////////
            bool relizeCommit = AnalysisSendORMMessage(corAnalyzer, HL7IDISEventEnum.ActivateOrder, returnIsProcessing, thishl7processor, vr);
            if (!returnIsProcessing)
                thishl7processor.ResetMessageInProgess();
            ////////////////////////////////////////////////////////////////////////////
            ///FIN LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
            ////////////////////////////////////////////////////////////////////////////

            //Realizamos el Commit de los cambios
            if (commitActions && relizeCommit)
            //if (commitActions)
            {
                //Realizamos el Commit de los cambios
                Commit(vr);
            }

            if (!relizeCommit)
            {
                ProcessValidationResult(vr);
            }

        }

        public CustomerOrderRequestDTO[] GetCustomerOrderRequestList(OrderRequestByCustomerEpisodeSpecification filter, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (filter == null)
                    return null;

                int maxRows = ServiceRestrictionHelper.GetMaxRows(CommonEntities.Constants.EntityNames.CustomerOrderRequestDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                int maxCharacters = LoadParameterFromAppConfig<int>(MaxCharactersAllowedInQueryName, 100000);

                if (filter.IsFilteredByAny(OrderRequestByCustomerEpisodeOptionsEnum.RelatedEpisode))
                {
                    //buscar los episodios relacionados de customerEpisodeID
                    int customerEpisodeID = filter.CustomerEpisodeID;
                    int customerID = DataAccess.CustomerDA.GetCustomerIDByCustomerEpisodeID(customerEpisodeID);
                    int[] relatedEpisodeIDs = CustomerEpisodeBL.GetRelatedEpisodeIDsByCustomerIDsEpisodeIDs(new int[] { customerID }, new int[] { customerEpisodeID });

                    filter.ByRelatedEpisodeIDs(relatedEpisodeIDs);
                }

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(filter, maxRows);
                if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                        .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                        .Distinct()
                                        .OrderBy(i => i);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizationData(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsLocationScheduledDate(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizationBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CareProcessRealizationDA.ObtenerTipoAnestesia(corIDs.ToArray(), "INFINT001"), //131: Código de Tipo de Anestesia
                    ds, BackOffice.Entities.TableNames.IMQTipoAnestesiaTable);



                    CustomerOrderRequestDTOAdvancedAdapter coraa = new CustomerOrderRequestDTOAdvancedAdapter();
                    CustomerOrderRequestDTO[] cors = coraa.GetData(ds);
                    maxRecords = cors != null && cors.Length >= maxRows;
                    return cors;
                }
                return null;
            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestDTO[] GetCustomerOrderRequestList(CustomerProcessSpecification filter, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (filter == null || filter.ProcessChartIDs == null || filter.CareCenterIDs == null || filter.ProcessChartIDs.Length <= 0 ||
                    filter.CareCenterIDs.Length <= 0 || filter.StartDateTime == null || filter.EndDateTime == null)
                    return null;

                int maxRows = ServiceRestrictionHelper.GetMaxRows(CommonEntities.Constants.EntityNames.CustomerOrderRequestDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                int maxCharacters = LoadParameterFromAppConfig<int>(MaxCharactersAllowedInQueryName, 100000);

                filter = PreProcessCustomerProcessSpecification(filter);

                if (filter == null || filter.ProcessChartIDs == null || filter.CareCenterIDs == null || filter.ProcessChartIDs.Length <= 0 ||
                    filter.CareCenterIDs.Length <= 0 || filter.StartDateTime == null || filter.EndDateTime == null)
                    return null;

                List<CustomerOrderRequestDTO> allcors = new List<CustomerOrderRequestDTO>();


                DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(filter, maxRows);
                if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                        .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                        .Distinct()
                                        .OrderBy(i => i);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizationData(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsLocationScheduledDate(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizationBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCancelReason(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsFailedReason(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAnnulledReason(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    /// Tarea 388:


                    MergeTable(DataAccess.CareProcessRealizationDA.ObtenerTipoAnestesia(corIDs.ToArray(), "INFINT001"), //131: Código de Tipo de Anestesia
                    ds, BackOffice.Entities.TableNames.IMQTipoAnestesiaTable);

                    CustomerOrderRequestDTOAdvancedAdapter coraa = new CustomerOrderRequestDTOAdvancedAdapter();
                    CustomerOrderRequestDTO[] cors = coraa.GetData(ds);
                    maxRecords = cors != null && cors.Length >= maxRows;
                    if (cors != null && cors.Length > 0)
                        allcors.AddRange(cors);
                }

                if (filter.ApplyTo.In(CustomerOrderRequestApplyToEnum.RequestDate, CustomerOrderRequestApplyToEnum.ForeseenDate) &&
                    filter.Containing == CustomerOrderRequestContainingEnum.All &&
                    //filter.ProcessChartIDs.Length > 1 &&
                    (filter.PhysicianIDs == null || filter.PhysicianIDs.Length == 1) &&
                    (filter.AssistanceServiceIDs == null || filter.AssistanceServiceIDs.Length == 1) &&
                    (filter.RoutineIDs == null || filter.RoutineIDs.Length <= 0) &&
                    (filter.RoutineCodes == null || filter.RoutineCodes.Length <= 0) &&
                    (filter.ProcedureCodes == null || filter.ProcedureCodes.Length <= 0) &&
                    (filter.ProcedureIDs == null || filter.ProcedureIDs.Length <= 0) &&
                    (filter.RoutineTypeIDs == null || filter.RoutineTypeIDs.Length <= 0) &&
                    (filter.RoutineTypeCodes == null || filter.RoutineTypeCodes.Length <= 0) &&
                    (filter.WorkGroupIDs == null || filter.WorkGroupIDs.Length <= 0) &&
                    (filter.WorkGroupNames == null || filter.WorkGroupNames.Length <= 0) &&
                    (filter.WorkGroupNames == null || filter.WorkGroupNames.Length <= 0) &&
                    (filter.LocationIDs == null || filter.LocationIDs.Length <= 0) &&
                    (filter.LocationNames == null || filter.LocationNames.Length <= 0) &&
                    (filter.NeededSteps == 0)
                    )
                {

                    //todas estas son unplaced
                    UnplacedRequestFilterSpecification unpfilter = UnplacedRequestFilterSpecification.Create()
                        .ByCareCenters(filter.CareCenterIDs);

                    if (filter.ApplyTo == CustomerOrderRequestApplyToEnum.RequestDate)
                    {
                        unpfilter.ByFromDate(UnplacedRequestFindDateTypeEnum.RequestDateTime, filter.StartDateTime)
                                 .ByToDate(UnplacedRequestFindDateTypeEnum.RequestDateTime, filter.EndDateTime);
                    }
                    if (filter.ApplyTo == CustomerOrderRequestApplyToEnum.ForeseenDate)
                    {
                        unpfilter.ByFromDate(UnplacedRequestFindDateTypeEnum.RequestEffectiveAtDateTime, filter.StartDateTime)
                                 .ByToDate(UnplacedRequestFindDateTypeEnum.RequestEffectiveAtDateTime, filter.EndDateTime);
                    }


                    if (filter.OrderIDs != null && filter.OrderIDs.Length > 0)
                        unpfilter.ByOrder(filter.OrderIDs);
                    if (filter.OrderPriority != OrderPriorityEnum.None)
                        unpfilter.ByPriority(filter.OrderPriority);
                    if (filter.PhysicianIDs != null && filter.PhysicianIDs.Length == 1)
                        unpfilter.ByPhysician(filter.PhysicianIDs[0]);
                    if (filter.AssistanceServiceIDs != null && filter.AssistanceServiceIDs.Length == 1)
                        unpfilter.ByAssistanceService(filter.AssistanceServiceIDs[0]);

                    //aqui habría que poner los demás filtros o en la consulta de arriba permitir las no atendidas.
                    unpfilter.CHNumber = filter.CHNumber;
                    unpfilter.EpisodeNumber = filter.EpisodeNumber;
                    unpfilter.PlanningStatus = filter.PlanningStatus;
                    unpfilter.OrdStatus = filter.OrdStatus;
                    unpfilter.ActStatus = filter.ActStatus;

                    if (filter.ProcessChartIDs != null
                        && filter.ProcessChartIDs.Length == 1)
                        unpfilter.ProcessChartID = filter.ProcessChartIDs[0];

                    ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(unpfilter, maxRows, false);
                    if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                        && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                    {
                        IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                            .Distinct()
                                            .OrderBy(i => i);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCancelReason(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsFailedReason(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAnnulledReason(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        CustomerOrderRequestBaseDTOAdvancedAdapter coraa = new CustomerOrderRequestBaseDTOAdvancedAdapter();
                        CustomerOrderRequestBaseDTO[] cors = coraa.GetData(ds);
                        maxRecords = cors != null && cors.Length >= maxRows;
                        if (cors != null && cors.Length > 0)
                        {
                            CustomerOrderRequestDTO[] auxcors =
                                cors
                                .Where(c => !allcors.Any(tc => tc.CustomerOrderRequestID == c.CustomerOrderRequestID))
                                .Select(c => new CustomerOrderRequestDTO(c))
                                .ToArray();
                            if (auxcors != null && auxcors.Length > 0)
                                allcors.AddRange(auxcors);
                        }

                    }
                }
                return allcors.Count > 0 ? allcors.ToArray() : null;

            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestDTO[] GetCustomerOrderRequestListByIDs(int[] corIDs)
        {
            try
            {

                if (corIDs == null || corIDs.Length <= 0) return null;

                corIDs = corIDs.OrderBy(i => i).Distinct().ToArray();
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetByIDs(corIDs);
                if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizationData(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsLocationScheduledDate(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizationBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    /// Tarea 388:
                    MergeTable(DataAccess.CareProcessRealizationDA.ObtenerTipoAnestesia(corIDs.ToArray(), "INFINT001"), //131: Código de Tipo de Anestesia
                    ds, BackOffice.Entities.TableNames.IMQTipoAnestesiaTable);

                    CustomerOrderRequestDTOAdvancedAdapter coraa = new CustomerOrderRequestDTOAdvancedAdapter();
                    return coraa.GetData(ds);
                }
                return null;

            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }




        public CustomerOrderRequestDTO[] GetCustomerOrderRequestList(CitationMedicalOrderSpecification filter, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (filter == null)
                    return null;

                int maxRows = ServiceRestrictionHelper.GetMaxRows(CommonEntities.Constants.EntityNames.CustomerOrderRequestDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                maxRows = Int32.MaxValue;

                int maxCharacters = LoadParameterFromAppConfig<int>(MaxCharactersAllowedInQueryName, 100000);
                CommonEntities.AddInTokenBaseEntity[] phoneticAddinNames = null;

                if (ThereAreNCareCentersWithCodeGenerator())
                {
                    filter.ByCHNumber(filter.CHNumber, filter.CHCareCenterID);
                }
                else
                {
                    filter.ByCHNumber(filter.CHNumber);
                }

                if (filter.IsFilteredByAny(CitationMedicalOrderSearchOptionEnum.PhoneticLookupByFullName | CitationMedicalOrderSearchOptionEnum.PhoneticLookupByNameParts))
                {
                    List<CustomerOrderRequestDTO> myCustomerOrderRequests = new List<CustomerOrderRequestDTO>();
                    phoneticAddinNames = GetAvailablePhoneticAddins();
                    if ((phoneticAddinNames == null) || (phoneticAddinNames.Length == 0))
                        throw new Exception(Properties.Resources.MSG_NoPhoneticAddins);

                    CitationMedicalOrderSpecification mySpecification = filter.Clone() as CitationMedicalOrderSpecification;
                    foreach (CommonEntities.AddInTokenBaseEntity phoneticAddinName in phoneticAddinNames)
                    {
                        PhoneticTranslatorHostView host = AddInRepository.GetAddIn<PhoneticTranslatorHostView>(phoneticAddinName.AddinName);
                        if (host != null)
                        {
                            if (mySpecification.IsFilteredByAny(CitationMedicalOrderSearchOptionEnum.FirstName))
                                mySpecification.FirstName = host.Translate(mySpecification.FirstName);
                            if (mySpecification.IsFilteredByAny(CitationMedicalOrderSearchOptionEnum.LastName))
                                mySpecification.LastName = host.Translate(mySpecification.LastName);
                            if (mySpecification.IsFilteredByAny(CitationMedicalOrderSearchOptionEnum.LastName2))
                                mySpecification.LastName2 = host.Translate(mySpecification.LastName2);
                            if (mySpecification.IsFilteredByAny(CitationMedicalOrderSearchOptionEnum.PhoneticLookupByFullName))
                                mySpecification.PhoneticLookupFullName = host.Translate(mySpecification.PhoneticLookupFullName);
                        }

                        DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(mySpecification, maxRows, phoneticAddinName.AddinName);
                        if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                            && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                        {
                            IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                                .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                                .Distinct()
                                                .OrderBy(i => i);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedServiceName(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedActors(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizationData(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizedStatus(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsLocationScheduledDate(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizationBodySite(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            /// Tarea 388:


                            MergeTable(DataAccess.CareProcessRealizationDA.ObtenerTipoAnestesia(corIDs.ToArray(), "INFINT001"), //131: Código de Tipo de Anestesia
                            ds, BackOffice.Entities.TableNames.IMQTipoAnestesiaTable);

                            CustomerOrderRequestDTOAdvancedAdapter coraa = new CustomerOrderRequestDTOAdvancedAdapter();
                            CustomerOrderRequestDTO[] cors = coraa.GetData(ds);
                            if (cors != null)
                            {
                                myCustomerOrderRequests.AddRange(cors);
                                maxRecords = (cors.Length >= maxRows);
                                if (maxRecords)
                                {
                                    myCustomerOrderRequests = myCustomerOrderRequests
                                                    .Take(maxRows)
                                                    .ToList();
                                    break;
                                }
                            }
                        }
                    }
                    return myCustomerOrderRequests.Count > 0 ? myCustomerOrderRequests.ToArray() : null;
                }
                else
                {
                    DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(filter, maxRows);
                    if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                        && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                    {
                        IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                            .Distinct()
                                            .OrderBy(i => i);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedServiceName(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedActors(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizationData(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizedStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsLocationScheduledDate(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizationBodySite(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        /// Tarea 388:


                        MergeTable(DataAccess.CareProcessRealizationDA.ObtenerTipoAnestesia(corIDs.ToArray(), "INFINT001"), //131: Código de Tipo de Anestesia
                        ds, BackOffice.Entities.TableNames.IMQTipoAnestesiaTable);

                        CustomerOrderRequestDTOAdvancedAdapter coraa = new CustomerOrderRequestDTOAdvancedAdapter();
                        CustomerOrderRequestDTO[] cors = coraa.GetData(ds);
                        maxRecords = cors != null && cors.Length >= maxRows;
                        return cors;
                    }
                    return null;
                }
            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestBaseDTO[] GetCustomerOrderRequestListBase(UnplacedRequestFilterSpecification filter, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (filter == null)
                    return null;

                int maxRows = ServiceRestrictionHelper.GetMaxRows(CommonEntities.Constants.EntityNames.CustomerOrderRequestDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                int maxCharacters = LoadParameterFromAppConfig<int>(MaxCharactersAllowedInQueryName, 100000);
                CommonEntities.AddInTokenBaseEntity[] phoneticAddinNames = null;

                if (ThereAreNCareCentersWithCodeGenerator())
                {
                    filter.ByCHNumber(filter.CHNumber, filter.CHCareCenterID);
                }
                else
                {
                    filter.ByCHNumber(filter.CHNumber);
                }

                if (filter.IsFilteredByAny(UnplacedRequestFindOptionEnum.PhoneticLookupByFullName | UnplacedRequestFindOptionEnum.PhoneticLookupByNameParts))
                {
                    List<CustomerOrderRequestBaseDTO> myCustomerOrderRequests = new List<CustomerOrderRequestBaseDTO>();
                    phoneticAddinNames = GetAvailablePhoneticAddins();
                    if ((phoneticAddinNames == null) || (phoneticAddinNames.Length == 0))
                        throw new Exception(Properties.Resources.MSG_NoPhoneticAddins);

                    UnplacedRequestFilterSpecification mySpecification = filter.Clone() as UnplacedRequestFilterSpecification;
                    foreach (CommonEntities.AddInTokenBaseEntity phoneticAddinName in phoneticAddinNames)
                    {
                        PhoneticTranslatorHostView host = AddInRepository.GetAddIn<PhoneticTranslatorHostView>(phoneticAddinName.AddinName);
                        if (host != null)
                        {
                            if (mySpecification.IsFilteredByAny(UnplacedRequestFindOptionEnum.FirstName))
                                mySpecification.FirstName = host.Translate(mySpecification.FirstName);
                            if (mySpecification.IsFilteredByAny(UnplacedRequestFindOptionEnum.LastName))
                                mySpecification.LastName = host.Translate(mySpecification.LastName);
                            if (mySpecification.IsFilteredByAny(UnplacedRequestFindOptionEnum.LastName2))
                                mySpecification.LastName2 = host.Translate(mySpecification.LastName2);
                            if (mySpecification.IsFilteredByAny(UnplacedRequestFindOptionEnum.PhoneticLookupByFullName))
                                mySpecification.PhoneticLookupFullName = host.Translate(mySpecification.PhoneticLookupFullName);
                        }

                        DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(mySpecification, maxRows, true, phoneticAddinName.AddinName);
                        if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                            && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                        {
                            IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                                .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                                .Distinct()
                                                .OrderBy(i => i);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                            ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                            CustomerOrderRequestBaseDTOAdvancedAdapter coraa = new CustomerOrderRequestBaseDTOAdvancedAdapter();
                            CustomerOrderRequestBaseDTO[] cors = coraa.GetData(ds);

                            if (cors != null)
                            {
                                myCustomerOrderRequests.AddRange(cors);
                                maxRecords = (cors.Length >= maxRows);
                                if (maxRecords)
                                {
                                    myCustomerOrderRequests = myCustomerOrderRequests
                                                    .Take(maxRows)
                                                    .ToList();
                                    break;
                                }
                            }
                        }
                    }
                    return myCustomerOrderRequests.Count > 0 ? myCustomerOrderRequests.ToArray() : null;
                }
                else
                {
                    DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(filter, maxRows);
                    if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                        && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                    {
                        IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                            .Distinct()
                                            .OrderBy(i => i);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        CustomerOrderRequestBaseDTOAdvancedAdapter coraa = new CustomerOrderRequestBaseDTOAdvancedAdapter();
                        CustomerOrderRequestBaseDTO[] cors = coraa.GetData(ds);
                        maxRecords = cors != null && cors.Length >= maxRows;
                        return cors;
                    }
                }

                return null;
            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestBaseDTO[] GetCustomerOrderRequestListBaseForINDIGO(UnplacedRequestFilterSpecification filter, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (filter == null)
                    return null;

                int maxRows = ServiceRestrictionHelper.GetMaxRows(CommonEntities.Constants.EntityNames.CustomerOrderRequestDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                int maxCharacters = LoadParameterFromAppConfig<int>(MaxCharactersAllowedInQueryName, 100000);

                if (ThereAreNCareCentersWithCodeGenerator())
                {
                    filter.ByCHNumber(filter.CHNumber, filter.CHCareCenterID);
                }
                else
                {
                    filter.ByCHNumber(filter.CHNumber);
                }

                DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequestsForINDIGO(filter, maxRows, false);
                if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                        .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                        .Distinct()
                                        .OrderBy(i => i);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    //MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                    //ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    //MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                    //ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    CustomerOrderRequestBaseDTOAdvancedAdapter coraa = new CustomerOrderRequestBaseDTOAdvancedAdapter();
                    CustomerOrderRequestBaseDTO[] cors = coraa.GetData(ds);

                    //URL de acceso a la imagen de la realización
                    string mediaURL = LoadParameterFromAppConfig<string>(MediaServerCommand, string.Empty);

                    foreach (CustomerOrderRequestBaseDTO cor in cors)
                    {
                        if (cor.RelevantClinicalInfo != null && cor.RelevantClinicalInfo.Trim() != "")
                        {
                            Dictionary<string, object> fields =
                                new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

                            fields.Add("CHNumber", cor.CHNumber);
                            fields.Add("OrderIdentifier", string.Concat("V", cor.RelevantClinicalInfo));

                            cor.RelevantClinicalInfo = GetCodeString(mediaURL, fields);
                        }
                    }

                    maxRecords = cors != null && cors.Length >= maxRows;
                    return cors;
                }

                return null;
            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private string GetCodeString(string mask, IDictionary<string, object> fields)
        {
            StringBuilder sb = new StringBuilder();
            List<object> p = new List<object>();
            MatchCollection mc = Regex.Matches(mask, @"(?<param>\{[^\}]*\})");

            int i = 0;
            int pos = 0;
            string s;
            if (mc.Count == 0)
                return mask;

            foreach (Match m in mc)
            {
                sb.Append(mask.Substring(pos, m.Index - pos));
                pos = m.Index;
                s = m.Value;

                Match field = Regex.Match(s, @"\{([^,:\{\}]*)");
                string fieldName = field.Value.Substring(1);
                if ((fields != null) && (fields.ContainsKey(fieldName)))
                {
                    sb.Append("{" + (i++).ToString() + s.Substring(field.Index + field.Length));
                    p.Add(fields[fieldName]);
                    pos += m.Length;
                }
                else
                {
                    sb.Append("???");
                    pos += m.Length;
                }
            }

            if (pos < mask.Length)
            {
                sb.Append(mask.Substring(pos, mask.Length - pos));
            }

            return string.Format(sb.ToString(), p.ToArray());
        }

        public CustomerOrderRequestDTO[] GetCustomerOrderRequestList(HeldCustomerOrderRequestSpecification heldfilter, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (heldfilter == null || heldfilter.CareCenterID <= 0
                    || heldfilter.ProcessChartIDs == null || heldfilter.ProcessChartIDs.Length <= 0
                    || heldfilter.StartDateTime == null || heldfilter.EndDateTime == null)
                    return null;

                

                int maxRows = ServiceRestrictionHelper.GetMaxRows(CommonEntities.Constants.EntityNames.CustomerOrderRequestDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                int maxCharacters = LoadParameterFromAppConfig<int>(MaxCharactersAllowedInQueryName, 100000);

                CustomerProcessSpecification filter = new CustomerProcessSpecification()
                    .ByCareCenterIDs(new int[] { heldfilter.CareCenterID })
                    .ByStartDateTime(heldfilter.StartDateTime)
                    .ByEndDateTime(heldfilter.EndDateTime)
                    .ByProcessChartIDs(heldfilter.ProcessChartIDs)
                    .ByApplyTo(CustomerOrderRequestApplyToEnum.CitationDate)
                    .ByContaining(CustomerOrderRequestContainingEnum.All);
                if (heldfilter.OrderID > 0)
                    filter.ByOrderIDs(new int[] { heldfilter.OrderID });
                if (heldfilter.OrderPriority != OrderPriorityEnum.None)
                    filter.ByOrderPriority(heldfilter.OrderPriority);


                DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(filter, maxRows, heldfilter, null);
                if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                        .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                        .Distinct()
                                        .OrderBy(i => i);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizationData(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsLocationScheduledDate(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizationBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    /// Tarea 388:


                    MergeTable(DataAccess.CareProcessRealizationDA.ObtenerTipoAnestesia(corIDs.ToArray(), "INFINT001"), //131: Código de Tipo de Anestesia
                    ds, BackOffice.Entities.TableNames.IMQTipoAnestesiaTable);

                    CustomerOrderRequestDTOAdvancedAdapter coraa = new CustomerOrderRequestDTOAdvancedAdapter();
                    CustomerOrderRequestDTO[] cors = coraa.GetData(ds);
                    maxRecords = cors != null && cors.Length >= maxRows;
                    return cors;
                }
                return null;

            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestDTO[] GetCustomerOrderRequestList(TraceCustomerOrderRequestSpecification tracefilter, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (tracefilter == null || tracefilter.CustomerID <= 0
                    || tracefilter.CareCenterIDs == null || tracefilter.CareCenterIDs.Length <= 0
                    || tracefilter.ProcessChartIDs == null || tracefilter.ProcessChartIDs.Length <= 0
                    || tracefilter.StartDateTime == null || tracefilter.EndDateTime == null)
                    return null;

                int maxRows = ServiceRestrictionHelper.GetMaxRows(CommonEntities.Constants.EntityNames.CustomerOrderRequestDTOName);
                if (maxRows == 0) { maxRows = Int32.MaxValue; }

                int maxCharacters = LoadParameterFromAppConfig<int>(MaxCharactersAllowedInQueryName, 100000);

                int[] customerOrderRequestIDs = DataAccess.CustomerOrderRequestDA.GetAllRelatedRequests(tracefilter.CustomerOrderRequestIDs);
                if (customerOrderRequestIDs == null || customerOrderRequestIDs.Length <= 0) return null;

                tracefilter.ByCustomerOrderRequestIDs(customerOrderRequestIDs);
                int customerID = DataAccess.CustomerOrderRequestDA.GetCustomerIDByCorID(tracefilter.CustomerOrderRequestIDs[0]);

                CustomerProcessSpecification filter = new CustomerProcessSpecification()
                    .ByContaining(CustomerOrderRequestContainingEnum.All)
                    .ByCareCenterIDs(tracefilter.CareCenterIDs)
                    .ByStartDateTime(tracefilter.StartDateTime)
                    .ByEndDateTime(tracefilter.EndDateTime)
                    .ByProcessChartIDs(tracefilter.ProcessChartIDs)
                    .ByOrderIDs(new int[] { tracefilter.OrderID })
                    .ByApplyTo(CustomerOrderRequestApplyToEnum.RequestDate);

                List<CustomerOrderRequestDTO> tracecors = new List<CustomerOrderRequestDTO>();
                //todas estas son placed
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(filter, maxRows, null, tracefilter);
                if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                    && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                {
                    IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                        .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                        .Distinct()
                                        .OrderBy(i => i);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedServiceName(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizedActors(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizationData(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsRealizedStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsLocationScheduledDate(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRealizationBodySite(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                    ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                    /// Tarea 388:


                    MergeTable(DataAccess.CareProcessRealizationDA.ObtenerTipoAnestesia(corIDs.ToArray(), "INFINT001"), //131: Código de Tipo de Anestesia
                    ds, BackOffice.Entities.TableNames.IMQTipoAnestesiaTable);

                    CustomerOrderRequestDTOAdvancedAdapter coraa = new CustomerOrderRequestDTOAdvancedAdapter();
                    CustomerOrderRequestDTO[] cors = coraa.GetData(ds);
                    maxRecords = cors != null && cors.Length >= maxRows;
                    if (cors != null && cors.Length > 0)
                        tracecors.AddRange(cors);
                }

                //si existen ordenes que no están en las placed entonces pregunto por las unplaced
                if (tracecors.Count <= 0 || customerOrderRequestIDs.Any(id => !tracecors.Any(cor => cor.CustomerOrderRequestID == id)) && customerID > 0)
                {
                    //todas estas son unplaced
                    UnplacedRequestFilterSpecification unpfilter = UnplacedRequestFilterSpecification.Create()
                        .ByCareCenters(tracefilter.CareCenterIDs)
                        .ByFromDate(UnplacedRequestFindDateTypeEnum.RequestDateTime, tracefilter.StartDateTime)
                        .ByToDate(UnplacedRequestFindDateTypeEnum.RequestDateTime, tracefilter.EndDateTime.Value.Date.AddDays(-1))
                        .ByOrder(new int[] { tracefilter.OrderID })
                        .ByCustomer(customerID);

                    ds = DataAccess.CustomerOrderRequestDA.GetFilteringCustomerOrderRequests(unpfilter, maxRows, false);
                    if ((ds != null) && (ds.Tables.Contains(Administrative.Entities.TableNames.CustomerOrderRequestTable))
                        && (ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0))
                    {
                        IEnumerable<int> corIDs = ds.Tables[Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                                            .Select(r => r.Field<int>("CustomerOrderRequestID"))
                                            .Distinct()
                                            .OrderBy(i => i);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedServiceName(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestedActors(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsIsScheduled(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAbortedStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsCustomerSpecialCategories(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsAllRequestBodySite(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        MergeTable(DataAccess.CustomerOrderRequestDA.GetFilteringRestInfoByCORIDsConsentStatus(corIDs),
                        ds, Administrative.Entities.TableNames.AdditionalCORInfoTable, group_columns_array);

                        //AQUI FALTA PONER LOS DATOS DEL RealizationAppointmentService

                        CustomerOrderRequestBaseDTOAdvancedAdapter coraa = new CustomerOrderRequestBaseDTOAdvancedAdapter();
                        CustomerOrderRequestBaseDTO[] cors = coraa.GetData(ds);
                        maxRecords = cors != null && cors.Length >= maxRows;
                        if (cors != null && cors.Length > 0)
                        {
                            tracecors.AddRange(
                                cors
                                .Where(c => customerOrderRequestIDs.Contains(c.CustomerOrderRequestID) &&
                                        !tracecors.Any(tc => tc.CustomerOrderRequestID == c.CustomerOrderRequestID))
                                .Select(c => new CustomerOrderRequestDTO(c))
                                .ToArray()
                                );
                        }
                    }
                }
                return (tracecors.Count > 0) ? tracecors.ToArray() : null;
            }
            catch (CommonEntities.TooComplexQueryException ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service))
                    throw new FaultException<CommonEntities.TooComplexQueryError>(
                                        new CommonEntities.TooComplexQueryError(ex.Message), new FaultReason(ex.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public CustomerOrderRequestBaseDTO[] GetCustomerOrderRequestListBaseByCustomerCitation(int customerCitationID, out bool maxRecords)
        {
            maxRecords = false;
            try
            {
                if (customerCitationID <= 0)
                    return null;

                UnplacedRequestFilterSpecification filter = UnplacedRequestFilterSpecification.Create();
                filter.ByCustomerCitation(customerCitationID);
                return GetCustomerOrderRequestListBase(filter, out maxRecords);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /*esto es para utilizar en la vista de solicitudes
        cualquier sugerencia o mejora bienvenida sea
        el objetivo es detectar si se puede modificar la solicitud
        la condición es que mientras no tenga ni un acto iniciado se puede modificar.*/
        public bool ModifyOrderContentIsPossible(int customerOrderRequestID)
        {
            try
            {
                if (customerOrderRequestID <= 0)
                    return true;

                CustomerOrderRequestEntity customerOrderRequest = this.GetByID(customerOrderRequestID);
                if (customerOrderRequest == null)
                    return true;

                if (customerOrderRequest.Status != ActionStatusEnum.Pending
                    && customerOrderRequest.Status != ActionStatusEnum.Confirmed)
                    return false;

                CustomerOrderRealizationEntity[] corr = CustomerOrderRealizationBL.GetCustomerOrderRealizationsByCustomerOrderRequest(customerOrderRequest.ID, true);
                if (corr == null) return true;

                foreach (CustomerOrderRealizationEntity cor in corr)
                {
                    if (cor.ProcedureActs != null
                        && cor.ProcedureActs.Length > 0
                        && Array.Exists(cor.ProcedureActs, pa => pa.Status != ActionStatusEnum.Pending && pa.Status != ActionStatusEnum.Scheduled))
                        return false;
                    if (cor.RoutineActs != null
                        && cor.RoutineActs.Length > 0
                        && Array.Exists(cor.RoutineActs, ra => ra.ActStatus != ActionStatusEnum.Pending && ra.ActStatus != ActionStatusEnum.Scheduled))
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool CancelOrDeleteCustomerOrderRequestIsPossible(int customerAppointmentInformationID)
        {
            return DataAccess.CustomerOrderRequestDA.CancelOrDeleteCustomerOrderRequestIsPossible(customerAppointmentInformationID);
        }

        public bool IsLastPlanningChildOrderRealized(int customerOrderRequestID)
        {
            if (customerOrderRequestID <= 0) return false;
            return customerOrderRequestID == DataAccess.CustomerOrderRequestDA.IsLastPlanningChildOrderRealized(customerOrderRequestID);
        }

        public CustomerOrderRequestEntity GetCustomerOrderRequestByCustomerAppointmentInformation(int customerAppointmentInformationID)
        {
            try
            {
                int corID = DataAccess.CustomerOrderRequestDA.GetIDByCustomerAppointmentInformationID(customerAppointmentInformationID);

                if (corID > 0)
                    return GetCustomerOrderRequest(corID, false);
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
        /// esto tengo que verlo con alberto o con roberto para dejarlo incluido dentro del ORM
        /// </summary>
        public bool ReplacePrescriptionRequest(PrescriptionRequestEntity prescriptionRequest, int changeReasonID, string explanation,
            CustomerOrderRequestEntity customerOrderRequest, bool commitActions = true, ValidationResults validationResults = null)
        {
            try
            {
                if (customerOrderRequest == null)
                    throw new ArgumentException("customerOrderRequest");

                if (prescriptionRequest == null)
                    throw new ArgumentException("prescriptionRequest");

                ValidationResults vr = (validationResults != null)
                    ? validationResults
                    : new ValidationResults();

                AddReasonCustomerOrderRequest(customerOrderRequest, changeReasonID, explanation, vr);

                //Una vez completado el proceso de confirmación, guardamos
                Save(customerOrderRequest, false, vr);

                HandleBasicActions<PrescriptionRequestEntity>(prescriptionRequest, vr, Helpers.PrescriptionRequestHelper.Validate);

                ////////////////////////////////////////////////////////////////////////////
                ///LAMADA A LOS METODOS DE ADDIN DE HL7 PARA LAS ORDENES MEDICAS 
                ////////////////////////////////////////////////////////////////////////////




                if (commitActions)
                {
                    Commit(vr);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public string GetCustomerOrderRequestRequirementEntity(RequirementElementEnum requirementElement, int requirementEntityID)
        {
            try
            {
                switch (requirementElement)
                {
                    case RequirementElementEnum.Unknown:
                        return string.Empty;
                    case RequirementElementEnum.LocationAsRequirement:
                        return SetLocationData(requirementEntityID);
                    case RequirementElementEnum.EquipmentAsRequirement:
                        return SetEquipmentData(requirementEntityID);
                    case RequirementElementEnum.DeviceAsRequirement:
                        return SetDeviceData(requirementEntityID);
                    case RequirementElementEnum.ItemAsRequirement:
                    case RequirementElementEnum.AnesthesiaAsRequirement:
                    case RequirementElementEnum.ContrastAsRequirement:
                        return SetItemData(requirementEntityID);
                    case RequirementElementEnum.MaterialSpecimenAsRequirement:
                        return SetMaterialSpecimenData(requirementEntityID);
                    case RequirementElementEnum.RoutineAsRequirement:
                        return SetRoutineData(requirementEntityID);
                    case RequirementElementEnum.ProcedureAsRequirement:
                        return SetProcedureData(requirementEntityID);
                    case RequirementElementEnum.HumanResourceAsRequirement:
                        return SetHumanResourceData(requirementEntityID);
                    case RequirementElementEnum.PhysicianAsRequirement:
                        return SetPhysicianData(requirementEntityID);
                    default:
                        return string.Empty;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return string.Empty;
            }
        }
        #endregion

        #region Mensajes SIU^S12 HL7
        public bool SendPlanningMessage(int[] customerOrderRequestIDs)
        {
            try
            {
                bool result = true;
                if (customerOrderRequestIDs != null && customerOrderRequestIDs.Length > 0)
                {
                    HL7MessagingProcessor thishl7processor = new HL7MessagingProcessor(null, null);

                    foreach (int corID in customerOrderRequestIDs)
                    {
                        CustomerOrderRequestEntity cor = this.GetByID(corID);
                        if (cor != null)
                        {
                            try
                            {
                                Dictionary<string, object> entities = new Dictionary<string, object>();
                                entities.Add(CommonEntities.Constants.EntityNames.CustomerOrderRequestEntityName, cor);
                                CustomerEntity customer = CustomerBL.GetCustomer(cor.CustomerID);
                                entities.Add(CommonEntities.Constants.EntityNames.CustomerEntityName, customer);
                                CustomerEpisodeEntity customerEpisode = CustomerEpisodeBL.GetFullCustomerEpisode(cor.CustomerEpisodeID);
                                CustomerReservationEntity customerReservation = null;
                                if (customerEpisode != null)
                                {
                                    entities.Add(CommonEntities.Constants.EntityNames.CustomerEpisodeEntityName, customerEpisode);
                                    CustomerProcessEntity customerProcess = CustomerProcessBL.GetCustomerProcessByEpisodeID(customerEpisode.ID);
                                    entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, customerProcess);
                                    if (customerProcess != null)
                                    {
                                        ProcessChartEntity processChart = ProcessChartBL.GetByID(customerProcess.ProcessChartID);
                                        entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);
                                    }
                                    if (customerEpisode.AttendingPhysicianID > 0)
                                    {
                                        PhysicianEntity attendingPhysician = PhysicianBL.GetPhysician(customerEpisode.AttendingPhysicianID);
                                        entities.Add(CommonEntities.Constants.EntityNames.PhysicianEntityName, attendingPhysician);
                                    }
                                }
                                else
                                {
                                    customerReservation = CustomerReservationBL.GetCustomerReservationByCustomerOrderRequest(cor.ID);
                                    if (customerReservation != null)
                                    {
                                        entities.Add(CommonEntities.Constants.EntityNames.CustomerReservationEntityName, customerReservation);
                                        CustomerProcessEntity customerProcess = CustomerProcessBL.GetCustomerProcess(customerReservation.CustomerProcessID);
                                        entities.Add(CommonEntities.Constants.EntityNames.CustomerProcessEntityName, customerProcess);
                                        if (customerProcess != null)
                                        {
                                            ProcessChartEntity processChart = ProcessChartBL.GetByID(customerProcess.ProcessChartID);
                                            entities.Add(CommonEntities.Constants.EntityNames.ProcessChartEntityName, processChart);
                                        }
                                    }
                                }

                                if (cor.RequestedPersonID > 0)
                                {
                                    PersonEntity referringPhysician = PersonBL.GetByID(cor.RequestedPersonID);
                                    entities.Add(CommonEntities.Constants.EntityNames.PersonEntityName, referringPhysician);

                                }

                                if (customerEpisode != null || customerReservation != null)
                                {

                                    //// envia mensaje HL7  SIU^S12 
                                    thishl7processor.ResetEntities(entities);
                                    //HL7MessagingProcessor.ResetBLs(BLs);
                                    thishl7processor.SendSCHMessages(MessageTypeEnum.HL7_SIU12);
                                    /////////////////////////////////////////////////////

                                }
                            }
                            catch
                            {
                                result = false;
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }
        #endregion

        #region Mensajes QBP^Qnn HL7
        public string SendeFarmacoQBPMessage(CustomerProcessEntity[] customerProcesses, CustomerEntity[] customers,
                    CustomerEpisodeEntity[] customerEpisodes)
        {
            try
            {
                if (customerProcesses == null || customerProcesses.Length <= 0 ||
                    customers == null || customers.Length <= 0 ||
                    customerEpisodes == null || customerEpisodes.Length <= 0) return string.Empty;
                HL7MessagingProcessor thishl7processor = new HL7MessagingProcessor(null, null);
                Dictionary<string, object> entities = new Dictionary<string, object>();
                entities.Add(CommonEntities.Constants.EntityNames.ArrayCustomerProcessEntityName, customerProcesses);
                entities.Add(CommonEntities.Constants.EntityNames.ArrayCustomerEntityName, customers);
                entities.Add(CommonEntities.Constants.EntityNames.ArrayCustomerEpisodeEntityName, customerEpisodes);
                ProcessChartEntity[] processCharts = ProcessChartBL.GetAllProcessCharts();
                entities.Add(CommonEntities.Constants.EntityNames.ArrayProcessChartEntityName, processCharts);

                //// envia mensaje HL7  QBP^Qnn 
                thishl7processor.ResetEntities(entities);
                HL7Result result = thishl7processor.SendQBPMessages(MessageTypeEnum.HL7_QBPnn, true);
                /////////////////////////////////////////////////////
                return (result != null && result.Entries != null && result.Entries.Count > 0)
                    ? result.Entries[0].ReceiveMessage
                    : string.Empty;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return string.Empty;
            }
        }
        public string SendeFarmacoORR02Message(Dictionary<string, string> pacientesyprescripciones)
        {
            try
            {
                if (pacientesyprescripciones == null) return string.Empty;
                HL7MessagingProcessor thishl7processor = new HL7MessagingProcessor(null, null);
                Dictionary<string, object> entities = new Dictionary<string, object>();
                entities.Add(CommonEntities.Constants.EntityNames.ValidatePrescriptionName, pacientesyprescripciones);
                             

                //// envia mensaje HL7  QBP^Qnn 
                thishl7processor.ResetEntities(entities);
                HL7Result result = thishl7processor.SendORR02Messages(MessageTypeEnum.HL7_ORR02);
                /////////////////////////////////////////////////////
                return (result != null && result.Entries != null && result.Entries.Count > 0)
                    ? result.Entries[0].ReceiveMessage
                    : string.Empty;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return string.Empty;
            }
        }

        #endregion

        #region para las tomas de medicamentos
        public DispatchedFromEntity[] GetDispatcheds(int[] prescriptionIDs)
        {
            try
            {
                if (prescriptionIDs == null || prescriptionIDs.Length <= 0) return null;
                DataSet ds = DataAccess.CustomerOrderRequestDA.GetDispatchedsByPrescriptionIDs(prescriptionIDs);
                if ((ds.Tables != null) && ds.Tables.Contains(Administrative.Entities.TableNames.PrescriptionRequestTable)
                    && (ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].Rows.Count > 0))
                {
                    return ds.Tables[Administrative.Entities.TableNames.PrescriptionRequestTable].AsEnumerable()
                            .Where(row => (row["PrescriptionID"] as int? ?? 0) > 0)
                            .Select(row => new DispatchedFromEntity(
                                row["PrescriptionID"] as int? ?? 0,
                                EnumUtils.GetEnum<DispatchedFromEnum>(row["DispatchedFrom"])
                                )
                            )
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
        #endregion

        #region Pruebas agfa
        //// busca el último mensaje de una solicitud
        public string GetMessage(string placeOrderNumber, string messageType, string messageHeader)
        {
            try
            {
                return DataAccess.CustomerOrderRequestDA.GetMessageByPlaceOrderNumber(placeOrderNumber, messageType, messageHeader);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }

        }
        #endregion
    }

    #region Support classes
    public class CustomerOrderRequestDataAccess
    {
        public CustomerOrderRequestDA CustomerOrderRequestDA { get; set; }
        public OrderRequestSchPlanningDA OrderRequestSchPlanningDA { get; set; }
        public OrderRequestRoutineRelDA OrderRequestRoutineRelDA { get; set; }
        public OrderRequestRoutineTimeDA OrderRequestRoutineTimeDA { get; set; }
        public OrderRequestProcedureRelDA OrderRequestProcedureRelDA { get; set; }
        public OrderRequestTimeDA OrderRequestTimeDA { get; set; }
        public ProcessChartDA ProcessChartDA { get; set; }
        public OrderDA OrderDA { get; set; }
        public RoutineDA RoutineDA { get; set; }
        public RoutineActDA RoutineActDA { get; set; }
        public ProcedureDA ProcedureDA { get; set; }
        public ProcedureActDA ProcedureActDA { get; set; }
        public LocationDA LocationDA { get; set; }
        public PrescriptionRequestDA PrescriptionRequestDA { get; set; }
        public PrescriptionRequestTimeDA PrescriptionRequestTimeDA { get; set; }
        //        public PrescriptionRequestItemSequenceRelDA PrescriptionRequestItemSequenceRelDA { get; set; }
        //        public ItemTreatmentRouteDA ItemTreatmentRouteDA { get; set; }
        public EquipmentDA EquipmentDA { get; set; }
        public AdministrationMethodDA AdministrationMethodDA { get; set; }
        public AdministrationRouteDA AdministrationRouteDA { get; set; }
        public PharmaceuticalFormAdministrationRouteRelDA PharmaceuticalFormAdministrationRouteRelDA { get; set; }
        public PharmaceuticalFormDA PharmaceuticalFormDA { get; set; }
        public BodySiteDA BodySiteDA { get; set; }
        public BodySiteConceptDA BodySiteConceptDA { get; set; }
        public BodySiteClassificationDA BodySiteClassificationDA { get; set; }
        public BodySiteParticipationDA BodySiteParticipationDA { get; set; }
        public ItemTreatmentOrderSequenceDA ItemTreatmentOrderSequenceDA { get; set; }
        public ItemDA ItemDA { get; set; }
        public PhysUnitDA PhysUnitDA { get; set; }
        public TimePatternDA TimePatternDA { get; set; }
        public OrderRequestProcedureTimeDA OrderRequestProcedureTimeDA { get; set; }
        public OrderRequestProcedureRoutineRelDA OrderRequestProcedureRoutineRelDA { get; set; }
        public OrderRequestCustomerObservationRelDA OrderRequestCustomerObservationRelDA { get; set; }
        public CustomerOrderRequestReasonRelDA CustomerOrderRequestReasonRelDA { get; set; }
        public ReasonChangeDA ReasonChangeDA { get; set; }
        public RecordDeletedLogDA RecordDeletedLogDA { get; set; }
        public CustomerOrderRealizationDA CustomerOrderRealizationDA { get; set; }
        public CustomerAccountDA CustomerAccountDA { get; set; }
        public CustomerAdmissionDA CustomerAdmissionDA { get; set; }
        public CustomerAssistancePlanDA CustomerAssistancePlanDA { get; set; }
        public ProcedureActRoutineActRelDA ProcedureActRoutineActRelDA { get; set; }
        public CustomerProcedureDA CustomerProcedureDA { get; set; }
        public CustomerRoutineDA CustomerRoutineDA { get; set; }
        public CustomerProcedureRoutineRelDA CustomerProcedureRoutineRelDA { get; set; }
        public CustomerProcedureReasonRelDA CustomerProcedureReasonRelDA { get; set; }
        public OrderRequestCustomerProcedureRelDA OrderRequestCustomerProcedureRelDA { get; set; }
        public OrderRequestCustomerRoutineRelDA OrderRequestCustomerRoutineRelDA { get; set; }
        public CustomerProcedureTimeDA CustomerProcedureTimeDA { get; set; }
        public CustomerRoutineTimeDA CustomerRoutineTimeDA { get; set; }
        public OrderRequestADTInfoDA OrderRequestADTInfoDA { get; set; }
        public CustomerRoutineReasonRelDA CustomerRoutineReasonRelDA { get; set; }
        public CareProcessRealizationDA CareProcessRealizationDA { get; set; }

        public OrderRequestHumanResourceRelDA OrderRequestHumanResourceRelDA { get; set; }
        public ParticipateAsDA ParticipateAsDA { get; set; }
        public OrderRequestResourceRelDA OrderRequestResourceRelDA { get; set; }
        public OrderRequestEquipmentRelDA OrderRequestEquipmentRelDA { get; set; }
        public OrderRequestLocationRelDA OrderRequestLocationRelDA { get; set; }
        public OrderRequestBodySiteRelDA OrderRequestBodySiteRelDA { get; set; }
        public OrderRequestRequirementRelDA OrderRequestRequirementRelDA { get; set; }
        public RequirementDA RequirementDA { get; set; }
        public OrderRequestConsentRelDA OrderRequestConsentRelDA { get; set; }
        public ConsentPreprintDA ConsentPreprintDA { get; set; }
        public ConsentTypeDA ConsentTypeDA { get; set; }

        public CareCenterRelatedCodeGeneratorDA CareCenterRelatedCodeGeneratorDA { get; set; }

        public CustomerDA CustomerDA { get; set; }
    }

    public class CustomerOrderRequestHelpers
    {
        public CustomerOrderRequestHelper CustomerOrderRequestHelper { get; set; }
        public OrderRequestSchPlanningHelper OrderRequestSchPlanningHelper { get; set; }
        public OrderRequestRoutineRelHelper OrderRequestRoutineRelHelper { get; set; }
        public OrderRequestProcedureRelHelper OrderRequestProcedureRelHelper { get; set; }
        public OrderRequestTimeHelper OrderRequestTimeHelper { get; set; }
        public OrderHelper OrderHelper { get; set; }
        public RoutineHelper RoutineHelper { get; set; }
        public RoutineActHelper RoutineActHelper { get; set; }
        public ProcedureHelper ProcedureHelper { get; set; }
        public ProcedureActHelper ProcedureActHelper { get; set; }
        public PrescriptionRequestHelper PrescriptionRequestHelper { get; set; }
        //public PrescriptionRequestItemSequenceRelHelper PrescriptionRequestItemSequenceRelHelper { get; set; }
        public ItemTreatmentOrderSequenceHelper ItemTreatmentOrderSequenceHelper { get; set; }

        public PrescriptionRequestTimeHelper PrescriptionRequestTimeHelper { get; set; }
        public TimePatternHelper TimePatternHelper { get; set; }
        public OrderRequestProcedureRoutineRelHelper OrderRequestProcedureRoutineRelHelper { get; set; }
        public CustomerOrderRequestReasonRelHelper CustomerOrderRequestReasonRelHelper { get; set; }
        public ReasonChangeHelper ReasonChangeHelper { get; set; }
        public CustomerOrderRealizationHelper CustomerOrderRealizationHelper { get; set; }
        public CustomerProcedureHelper CustomerProcedureHelper { get; set; }
        public CustomerRoutineHelper CustomerRoutineHelper { get; set; }
        public CustomerProcedureRoutineRelHelper CustomerProcedureRoutineRelHelper { get; set; }
        public CustomerProcedureTimeHelper CustomerProcedureTimeHelper { get; set; }
        public CustomerRoutineTimeHelper CustomerRoutineTimeHelper { get; set; }
        public OrderRequestADTInfoHelper OrderRequestADTInfoHelper { get; set; }

        public OrderRequestHumanResourceRelHelper OrderRequestHumanResourceRelHelper { get; set; }
        public OrderRequestResourceRelHelper OrderRequestResourceRelHelper { get; set; }
        public OrderRequestEquipmentRelHelper OrderRequestEquipmentRelHelper { get; set; }
        public OrderRequestLocationRelHelper OrderRequestLocationRelHelper { get; set; }
        public OrderRequestBodySiteRelHelper OrderRequestBodySiteRelHelper { get; set; }
        public OrderRequestRequirementRelHelper OrderRequestRequirementRelHelper { get; set; }
        public OrderRequestConsentRelHelper OrderRequestConsentRelHelper { get; set; }
    }

    public class CustomerOrderRequestFinalizeParameters
    {
        public int CustomerOrderRequestID { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerOrderRequestFinalizeReasonID { get; set; }
        public string CustomerOrderRequestFinalizeReasonExplanation { get; set; }
        public int CustomerRoutineFinalizeReasonID { get; set; }
        public string CustomerRoutineFinalizeReasonExplanation { get; set; }
        public int CustomerProcedureFinalizeReasonID { get; set; }
        public string CustomerProcedureFinalizeReasonExplanation { get; set; }
        public int RoutineActFinalizeReasonID { get; set; }
        public string RoutineActFinalizeReasonExplanation { get; set; }
        public int ProcedureActFinalizeReasonID { get; set; }
        public string ProcedureActFinalizeReasonExplanation { get; set; }
        public bool CheckMinimumDate { get; set; }
    }

    public class CustomerOrderRequestCancelParameters
    {
        public int CustomerOrderRequestID { get; set; }
        public DateTime? DateTimeLimit { get; set; }
        public int CustomerOrderRequestCancelReasonID { get; set; }
        public string CustomerOrderRequestCancelReasonExplanation { get; set; }
        public int CustomerRoutineCancelReasonID { get; set; }
        public string CustomerRoutineCancelReasonExplanation { get; set; }
        public int CustomerProcedureCancelReasonID { get; set; }
        public string CustomerProcedureCancelReasonExplanation { get; set; }
        public int RoutineActCancelReasonID { get; set; }
        public string RoutineActCancelReasonExplanation { get; set; }
        public int ProcedureActCancelReasonID { get; set; }
        public string ProcedureActCancelReasonExplanation { get; set; }
    }

    public class CustomerOrderRequestRoutineActRelationship
        : CommonEntities.BasicRelationship<CustomerOrderRequestEntity, RoutineActEntity>
    {
        #region Constructors
        public CustomerOrderRequestRoutineActRelationship() : base() { }
        public CustomerOrderRequestRoutineActRelationship(
            CustomerOrderRequestEntity parent, RoutineActEntity child)
            : base(parent, child) { }
        #endregion
    }

    public class CustomerOrderRequestProcedureActRelationship
        : CommonEntities.BasicRelationship<CustomerOrderRequestEntity, ProcedureActEntity>
    {
        #region Constructors
        public CustomerOrderRequestProcedureActRelationship() : base() { }
        public CustomerOrderRequestProcedureActRelationship(
            CustomerOrderRequestEntity parent, ProcedureActEntity child)
            : base(parent, child) { }
        #endregion
    }

    public class ParentCustomerOrderRequestChildCustomerOrderRequestRelationship
        : CommonEntities.BasicRelationship<CustomerOrderRequestEntity, CustomerOrderRequestEntity>
    {
        #region Constructors
        public ParentCustomerOrderRequestChildCustomerOrderRequestRelationship() : base() { }
        public ParentCustomerOrderRequestChildCustomerOrderRequestRelationship(
            CustomerOrderRequestEntity parent, CustomerOrderRequestEntity child)
            : base(parent, child) { }
        #endregion
    }
    #endregion
}
