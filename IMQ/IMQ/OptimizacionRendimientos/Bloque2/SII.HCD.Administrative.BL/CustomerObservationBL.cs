using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using SII.Framework.Entities.Services;
using SII.Framework.ExceptionHandling;
using SII.Framework.Interfaces;
using SII.Framework.Logging.LOPD;
using SII.HCD.Administrative.DA;
using SII.HCD.Administrative.Entities;
using SII.HCD.Administrative.Services;
using SII.HCD.Assistance.DA;
using SII.HCD.Assistance.Entities;
using SII.HCD.BackOffice.BL;
using SII.HCD.BackOffice.DA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.BackOffice.Services;
using SII.HCD.Common.BL;
using SII.HCD.Common.DA;
using SII.HCD.Misc;
using SII.HCD.Misc.IoC;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.BL
{
    public class CustomerObservationBL : BusinessLayerBase<RegisteredLayoutEntity>, ICustomerObservationService
    {
        #region Fields
        private CustomerObservationDataAccess _dataAccess = null;
        private CustomerObservationHelpers _helpers = null;

        //BL,s
        private ElementBL _elementBL;
        //private ObservationBL _observationBL;
        //private ObservationBlockBL _observationBlockBL;
        //private ObservationTemplateBL _observationTemplateBL;
        private RoutineActBL _routineActBL;
        private ProcedureActBL _procedureActBL;
        private NotificationActBL _notificationActBL;
        private IObservationCacheService _observationCache;


        private SpecialCategoryDA _specialCategoryDA = null;
        #endregion

        #region Properties
        private CustomerObservationDataAccess DataAccess
        {
            get
            {
                if (_dataAccess == null)
                    InitializeDataAccess();

                return _dataAccess;
            }
        }

        private CustomerObservationHelpers Helpers
        {
            get
            {
                if (_helpers == null)
                    InitializeHelpers();

                return _helpers;
            }
        }

        public ElementBL ElementBL
        {
            get
            {
                if (_elementBL == null)
                    _elementBL = new ElementBL();

                return _elementBL;
            }
        }

        //public ObservationBL ObservationBL
        //{
        //    get
        //    {
        //        if (_observationBL == null)
        //            _observationBL = new ObservationBL();

        //        return _observationBL;
        //    }
        //}

        //public ObservationBlockBL ObservationBlockBL
        //{
        //    get
        //    {
        //        if (_observationBlockBL == null)
        //            _observationBlockBL = new ObservationBlockBL();

        //        return _observationBlockBL;
        //    }
        //}

        //public ObservationTemplateBL ObservationTemplateBL
        //{
        //    get
        //    {
        //        if (_observationTemplateBL == null)
        //            _observationTemplateBL = new ObservationTemplateBL();

        //        return _observationTemplateBL;
        //    }
        //}

        public RoutineActBL RoutineActBL
        {
            get
            {
                if (_routineActBL == null)
                    _routineActBL = CreateDependentBusinessLayer<RoutineActBL, RoutineActEntity>();

                return _routineActBL;
            }
        }

        public ProcedureActBL ProcedureActBL
        {
            get
            {
                if (_procedureActBL == null)
                    _procedureActBL = CreateDependentBusinessLayer<ProcedureActBL, ProcedureActEntity>();

                return _procedureActBL;
            }
        }

        public NotificationActBL NotificationActBL
        {
            get
            {
                if (_notificationActBL == null)
                    _notificationActBL = CreateDependentBusinessLayer<NotificationActBL, NotificationActEntity>();

                return _notificationActBL;
            }
        }

        private SpecialCategoryDA SpecialCategoryDA
        {
            get
            {
                if (_specialCategoryDA == null)
                    _specialCategoryDA = new SpecialCategoryDA();

                return _specialCategoryDA;
            }
        }
        #endregion

        #region Constructors
        public CustomerObservationBL()
            : base()
        {
            InitializeDataAccess();
            InitializeCache();
        }

        public CustomerObservationBL(CustomerObservationDataAccess dataAccess)
            : base()
        {
            _dataAccess = dataAccess;
            InitializeCache();
        }

        public CustomerObservationBL(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            InitializeDataAccess();
            InitializeCache();
        }

        public CustomerObservationBL(UnitOfWork unitOfWork, CustomerObservationDataAccess dataAccess)
            : base(unitOfWork)
        {
            _dataAccess = dataAccess;
            InitializeCache();
        }
        #endregion

        #region Private methods
        private void InitializeDataAccess()
        {
            _dataAccess = new CustomerObservationDataAccess()
            {
                CustomerDA = new CustomerDA(),
                CustomerTemplateDA = new CustomerTemplateDA(),
                CustomerTemplateBlockRelDA = new CustomerTemplateBlockRelDA(),
                CustomerTemplateObsRelDA = new CustomerTemplateObsRelDA(),
                CustomerBlockDA = new CustomerBlockDA(),
                CustomerBlockObsRelDA = new CustomerBlockObsRelDA(),
                CustomerObservationDA = new CustomerObservationDA(),
                RoutineActObsRelDA = new RoutineActObsRelDA(),
                ProcedureActObsRelDA = new ProcedureActObsRelDA(),
                CustomerMedEpisodeActObsRelDA = new CustomerMedEpisodeActObsRelDA(),
                //ProtocolActObsRelDA = new ProtocolActObsRelDA(),
                ObservationDA = new ObservationDA(),
                ObservationConceptRelDA = new ObservationConceptRelDA(),
                ObservationBlockDA = new ObservationBlockDA(),
                ObservationTemplateDA = new ObservationTemplateDA(),
                ObservationOptionDA = new ObservationOptionDA(),
                ObservationValueDA = new ObservationValueDA(),
                ExtObservationValueDA = new ExtObservationValueDA(),
                BlockLayoutLabelDA = new BlockLayoutLabelDA(),
                RecordDeletedLogDA = new RecordDeletedLogDA(),
                CareProcessRealizationDA = new CareProcessRealizationDA(),

                StepPreprintDA = new StepPreprintDA(),
                CustomerProcessDA = new CustomerProcessDA(),
                CustomerEpisodeDA = new CustomerEpisodeDA(),
                MedicalEpisodeDA = new MedicalEpisodeDA(),
                CustomerOrderRequestDA = new CustomerOrderRequestDA(),
                RoutineActDA = new RoutineActDA(),
                ProcedureActDA = new ProcedureActDA(),
            };
        }

        private void InitializeCache()
        {
            _observationCache = IoCFactory.CurrentContainer.Resolve<IObservationCacheService>();
        }

        private void InitializeHelpers()
        {
            //El primer acceso refresca la cache

            //El resto no la actualiza por motivis de rendimiento
            _helpers = new CustomerObservationHelpers
            {

            };
        }

        private bool ExistsCustomer(int customerID)
        {
            if (customerID <= 0)
                return false;

            return DataAccess.CustomerDA.Exists(customerID);
        }

        private ObservationEntity FindObservation(int observationID)
        {
            if (observationID <= 0)
                return null;

            //return ObservationBL.GetObservation(observationID);
            return _observationCache.ObservationCache.Get(observationID, false);
        }

        private ObservationStatusEnum FindStatusCustomerObservation(int customerObservationID)
        {
            if (customerObservationID <= 0)
                return ObservationStatusEnum.None;

            DataSet ds = DataAccess.CustomerObservationDA.GetCustomerObservation(customerObservationID);

            if ((ds == null)
                || (ds.Tables == null)
                || !ds.Tables.Contains(Administrative.Entities.TableNames.CustomerObservationTable)
                || (ds.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count <= 0))
                return ObservationStatusEnum.None;

            return EnumUtils.GetEnum<ObservationStatusEnum>(ds.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows[0]["Status"]);
        }

        private int[] GetTemplatesByStepAndProcessChart(BasicProcessStepsEnum step, int customerEpisodeID)
        {
            if (customerEpisodeID <= 0) return null;

            if (step != BasicProcessStepsEnum.None)
            {
                DataSet ds = DataAccess.StepPreprintDA.GetObservationTemplateIDsByStepAndEpisode(step, customerEpisodeID);
                return (ds != null && ds.Tables != null && ds.Tables.Contains(BackOffice.Entities.TableNames.ObservationTemplateTable))
                    ? (from row in ds.Tables[BackOffice.Entities.TableNames.ObservationTemplateTable].AsEnumerable()
                       select row["ObservationTemplateID"] as int? ?? 0).ToArray()
                    : null;
            }
            else
            {
                DataSet ds = DataAccess.StepPreprintDA.GetObservationTemplateIDsByEpisode(customerEpisodeID);
                return (ds != null && ds.Tables != null && ds.Tables.Contains(BackOffice.Entities.TableNames.ObservationTemplateTable))
                    ? (from row in ds.Tables[BackOffice.Entities.TableNames.ObservationTemplateTable].AsEnumerable()
                       select row["ObservationTemplateID"] as int? ?? 0).ToArray()
                    : null;
            }
        }

        private void SetObservations(RegisteredObservationValueEntity[] obsValues)
        {
            if (obsValues == null)
                return;

            int[] obsIDs = (from obs in obsValues select obs.Observation.ID).Distinct().ToArray();

            if (obsIDs != null)
            {
                //ObservationEntity[] observations = ObservationBL.GetAllFullObservations(obsIDs);
                ObservationEntity[] observations = _observationCache.ObservationCache.FindAll(o => obsIDs.Contains(o.ID), false);

                if ((observations != null) && (observations.Length > 0))
                {
                    foreach (RegisteredObservationValueEntity rov in obsValues)
                    {
                        ObservationEntity observation = (from obs in observations where obs.ID == rov.Observation.ID select obs).FirstOrDefault();
                        if (observation != null)
                            rov.Observation = observation;
                    }
                }
            }
        }

        private void SetObservations(CustomerObservationEntity[] obsValues)
        {
            if (obsValues == null)
                return;

            int[] obsIDs = (from obs in obsValues select obs.ObservationID).Distinct().ToArray();

            if (obsIDs != null)
            {
                ObservationEntity[] observations = _observationCache.ObservationCache.FindAll(o => obsIDs.Contains(o.ID), false);

                if ((observations != null) && (observations.Length > 0))
                {
                    foreach (CustomerObservationEntity co in obsValues)
                    {
                        ObservationEntity observation = (from obs in observations where obs.ID == co.ObservationID select obs).FirstOrDefault();
                        if (observation != null)
                            co.Observation = observation;
                    }
                }
            }
        }

        private SpecialCategoryTypeEnum GetSpecialCategory(ObservationEntity oe)
        {
            if (oe == null) return SpecialCategoryTypeEnum.None;
            int spc = SpecialCategoryDA.GetSpecialCategoryByObservationID(oe.ID);
            switch (spc)
            {
                case 1: return SpecialCategoryTypeEnum.AsAlert;
                case 2: return SpecialCategoryTypeEnum.AsRecommendation;
                case 4: return SpecialCategoryTypeEnum.AsDataOfInterest;
                default: return SpecialCategoryTypeEnum.None;
            }
        }

        private RegisteredObservationValueEntity[] OrderedObservations(RegisteredObservationValueEntity[] obs)
        {
            if ((obs == null) || (obs.Length <= 0))
                return null;

            return (from rov in obs select rov).OrderBy(rov => rov.RegistrationDateTime).ToArray();
        }

        private RegisteredObservationTemplateEntity[] GetRegisteredObservationTemplatesByCustomerTemplateIDs(int customerID, DataSet dataset)
        {
            if (dataset != null && dataset.Tables != null
                && dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable))
            {

                int[] customerTemplateIDs = (from row in dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].AsEnumerable()
                                             select row["CustomerTemplateID"] as int? ?? 0).ToArray();
                if (customerTemplateIDs == null || customerTemplateIDs.Length <= 0) return null;
                #region RegisteredObservationBlocks
                DataSet ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCOT(customerTemplateIDs);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Copy();
                    dataset.Tables.Add(dt);
                }

                #region BlockLayouts
                ds2 = DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels();
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable].Copy();
                    dataset.Tables.Add(dt);
                }
                #endregion


                /////////////// 
                //
                // DE AQUI EN ADELANTE ESTOS DA DEBERÍAN SIMPLIFARSE Y BUSCAR POR LOS REALES  
                //
                ////////////////
                #region RegisteredObservationValues
                ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomer(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Copy();
                    dataset.Tables.Add(dt);
                }
                #region Observations
                ds2 = DataAccess.ObservationDA.GetAllObservations();
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationTable].Copy();
                    dataset.Tables.Add(dt);
                }
                #endregion

                #region ObservationValues
                ds2 = DataAccess.ObservationValueDA.GetAllObservationValues(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                    dataset.Tables.Add(dt);
                }
                #endregion

                #region ExtObservationValues
                ds2 = DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Copy();
                    dataset.Tables.Add(dt);
                }
                #endregion
                #endregion
                #endregion

                RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                RegisteredObservationTemplateEntity[] observationTemplates = this.RebuildTemplates(registeredObservationTemplateAdapter.GetData(dataset));
                this.SetObservations(observationTemplates);
                return this.OrderedTemplates(observationTemplates);
            }
            else return null;
        }

        #region Add Dataset of ObservationTable
        private void AddDataSetObservationsByCustomerObservation(int customerObservationID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ObservationDA.GetObservationsByCustomerObservationID(customerObservationID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationTable].Copy();
                ds.Tables.Add(dt);
            }
        }

        private void AddDataSetObservations(int customerID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ObservationDA.GetObservationsByCustomerID(customerID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationTable].Copy();
                ds.Tables.Add(dt);
            }
        }

        private void AddDataSetObservations(int customerID, int observationID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ObservationDA.GetObservation(observationID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationTable].Copy();
                ds.Tables.Add(dt);
            }
        }
        #endregion

        #region Add Dataset of ObservationValueTable
        private void AddDataSetObservationValuesByCustomerObservation(int customerObservationID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ObservationValueDA.GetAllObservationValuesByCustomerObservation(customerObservationID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                ds.Tables.Add(dt);
            }
        }

        private void AddDataSetObservationValues(int customerID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ObservationValueDA.GetAllObservationValues(customerID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                ds.Tables.Add(dt);
            }
        }

        private void AddDataSetObservationValues(int customerID, int observationID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ObservationValueDA.GetAllObservationValuesByCustomerAndObsID(customerID, observationID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                ds.Tables.Add(dt);
            }
        }
        #endregion

        #region Add Dataset of ExtObservationValueTable
        private void AddDataSetExtObservationValuesByCustomerObservation(int customerObservationID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ExtObservationValueDA.GetAllExtObservationValuesByCustomerObservation(customerObservationID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Copy();
                ds.Tables.Add(dt);
            }
        }

        private void AddDataSetExtObservationValues(int customerID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Copy();
                ds.Tables.Add(dt);
            }
        }

        private void AddDataSetExtObservationValues(int customerID, int observationID, DataSet ds)
        {
            DataSet ds2 = DataAccess.ExtObservationValueDA.GetAllExtObservationValuesByCustomerAndObsID(customerID, observationID);
            if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable)))
            {
                DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Copy();
                ds.Tables.Add(dt);
            }
        }
        #endregion
        #endregion

        #region ORM handlers
        #region ORMHandler<ObservationValueEntity> implementation
        private void ObservationValueNew(ObservationValueEntity entity)
        {
            if (entity == null)
                return;

            entity.ID = DataAccess.ObservationValueDA.Insert(entity.DTValue, entity.BoolValue, entity.IntValue, entity.DecValue,
                entity.DbValue, entity.StValue, GetIdentityUserName());
        }

        private void ObservationValueUpdate(ObservationValueEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.ObservationValueDA.Update(entity.ID, entity.DTValue, entity.BoolValue, entity.IntValue, entity.DecValue,
                entity.DbValue, entity.StValue, GetIdentityUserName());
        }

        private void ObservationValueDelete(ObservationValueEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.ObservationValueDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<ExtObservationValueEntity> implementation
        private void ExtObservationValueNew(ExtObservationValueEntity entity)
        {
            if (entity == null)
                return;

            entity.ID = DataAccess.ExtObservationValueDA.Insert(entity.Value, GetIdentityUserName());
        }

        private void ExtObservationValueUpdate(ExtObservationValueEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.ExtObservationValueDA.Update(entity.ID, entity.Value, GetIdentityUserName());
        }

        private void ExtObservationValueDelete(ExtObservationValueEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.ExtObservationValueDA.Delete(entity.ID);
        }
        #endregion

        #region ORMHandler<RegisteredObservationValueEntity> implementation
        private void RegisteredObservationValueNew(RegisteredObservationValueEntity entity)
        {
            if (entity == null)
                return;

            entity.ID = DataAccess.CustomerObservationDA.Insert(entity.CustomerID, entity.Observation.ID, (int)entity.Observation.KindOf, (int)entity.Observation.BasicType,
                            (entity.ObservationValue != null) ? entity.ObservationValue.ID : 0, (entity.ExtObservationValue != null) ? entity.ExtObservationValue.ID : 0,
                            entity.CustomerObservationEvalTestID, entity.AncestorID, (int)entity.SpecialCategoryType, (int)entity.Status, GetIdentityUserName());

            if (entity.Observation != null)
                DataAccess.ObservationDA.SetObservationInUseByCustomer(entity.Observation.ID);

            LOPDLogger.Write(SII.HCD.Common.Entities.Constants.EntityNames.CustomerObservationEntityName, entity.ID, ActionType.Create);
        }

        private void RegisteredObservationValueUpdate(RegisteredObservationValueEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.CustomerObservationDA.Update(entity.ID, entity.CustomerID, entity.Observation.ID, (int)entity.Observation.KindOf, (int)entity.Observation.BasicType,
                            (entity.ObservationValue != null) ? entity.ObservationValue.ID : 0, (entity.ExtObservationValue != null) ? entity.ExtObservationValue.ID : 0,
                            entity.CustomerObservationEvalTestID, entity.AncestorID, (int)entity.SpecialCategoryType, (int)entity.Status, GetIdentityUserName());

            if (entity.Observation != null)
                DataAccess.ObservationDA.SetObservationInUseByCustomer(entity.Observation.ID);

            LOPDLogger.Write(SII.HCD.Common.Entities.Constants.EntityNames.CustomerObservationEntityName, entity.ID, ActionType.Modify);
        }

        private void RegisteredObservationValueDelete(RegisteredObservationValueEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.CustomerObservationDA.DeleteCustomerObservation(entity.ID);

            if (entity.Observation != null)
            {
                if (!DataAccess.ObservationDA.GetObservationInUseByCustomer(entity.Observation.ID))
                    DataAccess.ObservationDA.ResetObservationInUseByCustomer(entity.Observation.ID);
            }
        }
        #endregion

        #region ORMHandler<RegisteredObservationBlockEntity> implementation
        private void RegisteredObservationBlockNew(RegisteredObservationBlockEntity entity)
        {
            if (entity == null)
                return;

            entity.ID = DataAccess.CustomerBlockDA.Insert(entity.ObservationBlockID, (int)entity.Status, GetIdentityUserName());

            if (entity.ObservationBlockID > 0)
                DataAccess.ObservationBlockDA.SetObservationBlockInUseByCustomer(entity.ObservationBlockID);
        }

        private void RegisteredObservationBlockUpdate(RegisteredObservationBlockEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.CustomerBlockDA.Update(entity.ID, entity.ObservationBlockID, (int)entity.Status, GetIdentityUserName());

            if (entity.ObservationBlockID > 0)
                DataAccess.ObservationBlockDA.SetObservationBlockInUseByCustomer(entity.ObservationBlockID);
        }

        private void RegisteredObservationBlockDelete(RegisteredObservationBlockEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.CustomerBlockDA.DeleteCustomerBlock(entity.ID);

            if (entity.ObservationBlockID > 0)
            {
                if (!DataAccess.ObservationBlockDA.GetObservationBlockInUseByCustomer(entity.ObservationBlockID))
                    DataAccess.ObservationBlockDA.ResetObservationBlockInUseByCustomer(entity.ObservationBlockID);
            }

        }
        #endregion

        #region ORMHandler<RegisteredObservationTemplateEntity> implementation
        private void RegisteredObservationTemplateNew(RegisteredObservationTemplateEntity entity)
        {
            if (entity == null)
                return;

            entity.ID = DataAccess.CustomerTemplateDA.Insert(entity.ObservationTemplateID, entity.ExportDocumentName, (int)entity.Status, GetIdentityUserName());

            if (entity.ObservationTemplateID > 0)
                DataAccess.ObservationTemplateDA.SetObservationTemplateInUseByCustomer(entity.ObservationTemplateID);
        }

        private void RegisteredObservationTemplateUpdate(RegisteredObservationTemplateEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.CustomerTemplateDA.Update(entity.ID, entity.ObservationTemplateID, entity.ExportDocumentName, (int)entity.Status, GetIdentityUserName());

            if (entity.ObservationTemplateID > 0)
                DataAccess.ObservationTemplateDA.SetObservationTemplateInUseByCustomer(entity.ObservationTemplateID);
        }

        private void RegisteredObservationTemplateDelete(RegisteredObservationTemplateEntity entity)
        {
            if (entity == null)
                return;

            DataAccess.CustomerTemplateDA.DeleteCustomerTemplate(entity.ID);

            if (entity.ObservationTemplateID > 0)
            {
                if (!DataAccess.ObservationTemplateDA.GetObservationTemplateInUseByCustomer(entity.ObservationTemplateID))
                    DataAccess.ObservationTemplateDA.ResetObservationTemplateInUseByCustomer(entity.ObservationTemplateID);
            }
        }
        #endregion

        #region ORMHandler<RegisteredObservationValueRegisteredObservationValueRelationship> implementation
        private void RegisteredObservationValueRegisteredObservationValueRelationshipNew(RegisteredObservationValueRegisteredObservationValueRelationship entity)
        {
            entity.Child.CustomerObservationEvalTestID = entity.Parent.ID;
        }

        private void RegisteredObservationValueRegisteredObservationValueRelationshipUpdate(RegisteredObservationValueRegisteredObservationValueRelationship entity)
        {
            entity.Child.CustomerObservationEvalTestID = entity.Parent.ID;
        }

        private void RegisteredObservationValueRegisteredObservationValueRelationshipDelete(RegisteredObservationValueRegisteredObservationValueRelationship entity)
        {

        }
        #endregion

        #region ORMHandler<RegisteredObservationBlockRegisteredObservationValueRelationship> implementation
        private void RegisteredObservationBlockRegisteredObservationValueRelationshipNew(RegisteredObservationBlockRegisteredObservationValueRelationship entity)
        {
            if (entity == null)
                return;

            if ((entity.Parent.ID <= 0) || (entity.Child.ID <= 0))
                throw new InvalidOperationException(Properties.Resources.ERROR_TransientState);

            DataAccess.CustomerBlockObsRelDA.Insert(entity.Parent.ID, entity.Child.ID);
        }

        private void RegisteredObservationBlockRegisteredObservationValueRelationshipDelete(RegisteredObservationBlockRegisteredObservationValueRelationship entity)
        {
            if (entity == null)
                return;

            if ((entity.Parent.ID <= 0) || (entity.Child.ID <= 0))
                throw new InvalidOperationException(Properties.Resources.ERROR_TransientState);

            DataAccess.CustomerBlockObsRelDA.DeleteByCustomerBlockAndCustomerObservation(entity.Parent.ID, entity.Child.ID);
        }
        #endregion

        #region ORMHandler<RegisteredObservationTemplateRegisteredObservationBlockRelationship> implementation
        private void RegisteredObservationTemplateRegisteredObservationBlockRelationshipNew(RegisteredObservationTemplateRegisteredObservationBlockRelationship entity)
        {
            if (entity == null)
                return;

            if ((entity.Parent.ID <= 0) || (entity.Child.ID <= 0))
                throw new InvalidOperationException(Properties.Resources.ERROR_TransientState);

            DataAccess.CustomerTemplateBlockRelDA.Insert(entity.Child.ID, entity.Parent.ID);
        }

        private void RegisteredObservationTemplateRegisteredObservationBlockRelationshipDelete(RegisteredObservationTemplateRegisteredObservationBlockRelationship entity)
        {
            if (entity == null)
                return;

            if ((entity.Parent.ID <= 0) || (entity.Child.ID <= 0))
                throw new InvalidOperationException(Properties.Resources.ERROR_TransientState);

            DataAccess.CustomerTemplateBlockRelDA.DeleteByCustomerBlockAndCustomerTemplate(entity.Child.ID, entity.Parent.ID);
        }
        #endregion

        #region ORMHandler<RegisteredObservationTemplateRegisteredObservationValueRelationship> implementation
        private void RegisteredObservationTemplateRegisteredObservationValueRelationshipNew(RegisteredObservationTemplateRegisteredObservationValueRelationship entity)
        {
            if (entity == null)
                return;

            if ((entity.Parent.ID <= 0) || (entity.Child.ID <= 0))
                throw new InvalidOperationException(Properties.Resources.ERROR_TransientState);

            DataAccess.CustomerTemplateObsRelDA.Insert(entity.Child.ID, entity.Parent.ID);
        }

        private void RegisteredObservationTemplateRegisteredObservationValueRelationshipDelete(RegisteredObservationTemplateRegisteredObservationValueRelationship entity)
        {
            if (entity == null)
                return;

            if ((entity.Parent.ID <= 0) || (entity.Child.ID <= 0))
                throw new InvalidOperationException(Properties.Resources.ERROR_TransientState);

            DataAccess.CustomerTemplateObsRelDA.DeleteByCustomerTemplateAndCustomerObservation(entity.Parent.ID, entity.Child.ID);
        }
        #endregion

        #region ORMHandler<ExtObservationValueEntity> implementation
        private void ObservationConceptRelNew(ObservationConceptRelEntity entity)
        {
            if ((entity == null) || (entity.Concept == null) || (entity.ObservationEncodingID <= 0))
                return;

            entity.ID = DataAccess.ObservationConceptRelDA.Insert(entity.ObservationEncodingID, entity.Concept.ID, GetIdentityUserName());

            //Modificar el DBTimeStamp de la observation y del ObservationBlock o/y ObservationTemplate que contengan esta observacion.
            DataAccess.ObservationConceptRelDA.MarkRelatedUpdatedObservationFromObservationEncodingObservationConceptRel(entity.ID, GetIdentityUserName());
            DataAccess.ObservationConceptRelDA.MarkRelatedUpdatedObservationBlocksFromObservationBlockRelObservationEncodingObservationConceptRel(entity.ID, GetIdentityUserName());
            DataAccess.ObservationConceptRelDA.MarkRelatedUpdatedObservationTemplatesFromObservationTemplateRelObservationEncodingObservationConceptRel(entity.ID, GetIdentityUserName());
            DataAccess.ObservationConceptRelDA.MarkRelatedUpdatedObservationTemplatesFromObservationTemplateRelObservationBlockRelObservationEncodingObservationConceptRel(entity.ID, GetIdentityUserName());
        }
        #endregion
        #endregion

        #region Analysis methods
        private RegisteredObservationValueEntity GetObservationRelationshipWithObservationAmended(RegisteredObservationValueEntity observation,
            RegisteredLayoutEntity layout)
        {
            if ((observation == null) || (layout == null))
                return null;

            RegisteredObservationTemplateEntity template = null;
            RegisteredObservationBlockEntity block = null;
            RegisteredObservationValueEntity result = null;
            if (observation.CustomerTemplateID > 0)
            {
                if ((layout.Templates != null) && (layout.Templates.Length > 0))
                {
                    template = (from rot in layout.Templates
                                where (rot.ID == observation.CustomerTemplateID)
                                select rot).FirstOrDefault();

                    if (template != null)
                    {
                        if (observation.CustomerBlockID > 0)
                        {
                            if ((template.Blocks != null) && (template.Blocks.Length > 0))
                            {
                                block = (from rob in template.Blocks
                                         where (rob.ID == observation.CustomerBlockID)
                                         select rob).FirstOrDefault();

                                if (block != null)
                                {
                                    if ((block.Observations != null) && (block.Observations.Length > 0))
                                    {
                                        result = (from rov in block.Observations
                                                  where (rov.CustomerTemplateID == observation.CustomerTemplateID) && (rov.CustomerBlockID == observation.CustomerBlockID)
                                                    && (rov.ObservationID == observation.ObservationID) && (rov.ID != observation.ID)
                                                  select rov).FirstOrDefault();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (observation.CustomerBlockID > 0)
                {
                    if ((layout.Blocks != null) && (layout.Blocks.Length > 0))
                    {
                        block = (from rob in layout.Blocks
                                 where (rob.ID == observation.CustomerBlockID)
                                 select rob).FirstOrDefault();

                        if (block != null)
                        {
                            if ((block.Observations != null) && (block.Observations.Length > 0))
                            {
                                result = (from rov in block.Observations
                                          where (rov.CustomerTemplateID == 0) && (rov.CustomerBlockID == observation.CustomerBlockID)
                                            && (rov.ObservationID == observation.ObservationID) && (rov.ID != observation.ID)
                                          select rov).FirstOrDefault();
                            }
                        }
                    }
                }
                else
                {
                    if ((layout.Observations != null) && (layout.Observations.Length > 0))
                    {
                        result = (from rov in block.Observations
                                  where (rov.CustomerTemplateID == 0) && (rov.CustomerBlockID == 0)
                                    && (rov.ObservationID == observation.ObservationID) && (rov.ID != observation.ID)
                                  select rov).FirstOrDefault();
                    }
                }
            }

            return result;
        }

        private void ValidateCustomer(RegisteredLayoutEntity entity, ValidationResults validationResults)
        {
            if ((entity == null) || (validationResults == null))
                return;

            if (!this.ExistsCustomer(entity.CustomerID))
                validationResults.AddResult(new ValidationResult(
                    string.Format(Properties.Resources.ERROR_CustomerNotFound, entity.CustomerID),
                    this, null, null, null));

            RegisteredObservationValueEntity[] observationValues = entity.GetAllRegisteredObservationValue();

            if ((observationValues != null) && (observationValues.Length > 0))
            {
                foreach (RegisteredObservationValueEntity value in observationValues)
                    value.CustomerID = entity.CustomerID;
            }
        }

        private void ValidateObservation(RegisteredObservationValueEntity entity, ValidationResults validationResults)
        {
            if ((entity == null) || (validationResults == null))
                return;

            if (entity.Observation == null)
                validationResults.AddResult(new ValidationResult(
                    string.Format(Properties.Resources.MSG_CustomerObservationObservationIsRequired, entity.LabelName),
                    this, null, null, null));

            if (FindObservation(entity.Observation.ID) == null)
                validationResults.AddResult(new ValidationResult(
                    string.Format(Properties.Resources.ERROR_ObservationNotFound, entity.ID),
                    this, null, null, null));
        }

        private void ValidateRegisteredObservationValueKingOf(RegisteredObservationValueEntity entity, ValidationResults validationResults)
        {
            if ((entity == null) || (entity.Observation == null) || (validationResults == null))
                return;

            switch (entity.Observation.KindOf)
            {
                case ObservationTypeEnum.None:
                    validationResults.AddResult(new ValidationResult(
                        string.Format(Properties.Resources.ERROR_ObservationKindOfNotFound, entity.LabelName),
                        this, null, null, null));
                    break;
                case ObservationTypeEnum.GenericObservation:
                    if (entity.Observation.BasicType == BasicObservationTypeEnum.None)
                        validationResults.AddResult(new ValidationResult(
                            string.Format(Properties.Resources.ERROR_ObservationBasicTypeNotFound,
                                entity.LabelName, ObservationTypeEnumNames.GetName(entity.Observation.KindOf)),
                            this, null, null, null));

                    if (entity.Observation.BasicType == BasicObservationTypeEnum.RichText)
                    {
                        if (entity.ExtObservationValue == null)
                            validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_ObservationValueBasicTypeNotFound,
                                    entity.LabelName, ObservationTypeEnumNames.GetName(entity.Observation.KindOf),
                                    BasicObservationTypeEnumNames.GetName(entity.Observation.BasicType)),
                                this, null, null, null));
                    }
                    else
                    {
                        if (entity.ObservationValue == null)
                            validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_ObservationValueBasicTypeNotFound,
                                    entity.LabelName, ObservationTypeEnumNames.GetName(entity.Observation.KindOf),
                                    BasicObservationTypeEnumNames.GetName(entity.Observation.BasicType)),
                                this, null, null, null));
                    }
                    break;
                case ObservationTypeEnum.CodeObservation:
                case ObservationTypeEnum.CodifiedCodeObservation:
                    if (entity.ObservationValue == null)
                        validationResults.AddResult(new ValidationResult(
                            string.Format(Properties.Resources.ERROR_ObservationValueNotFound,
                                entity.LabelName, ObservationTypeEnumNames.GetName(entity.Observation.KindOf)),
                            this, null, null, null));
                    break;
                case ObservationTypeEnum.MultiSelectObservation:
                case ObservationTypeEnum.ResultInfo:
                case ObservationTypeEnum.EvalTestObservation:
                case ObservationTypeEnum.ImageObservation:
                case ObservationTypeEnum.ImageAndROIsObservation:
                case ObservationTypeEnum.CodifiedMultiSelectObservation:
                    if ((entity.ObservationValue == null) || (entity.ExtObservationValue == null))
                        validationResults.AddResult(new ValidationResult(
                            string.Format(Properties.Resources.ERROR_ObservationValueNotFound,
                                entity.LabelName, ObservationTypeEnumNames.GetName(entity.Observation.KindOf)),
                            this, null, null, null));
                    break;
                default: break;
            }
        }

        private void AnalyzeRegisteredObservationValueAmended(RegisteredObservationValueEntity entity, RegisteredLayoutEntity layout,
            ValidationResults validationResults)
        {
            if ((entity == null) || (validationResults == null))
                return;

            //Si viene en estado amended buscar su pareja y relacionarlas. (Antigua(Amended).AncestorID -- Nueva(Pending o Confirmed).ID)
            RegisteredObservationValueEntity obsRelAmended = GetObservationRelationshipWithObservationAmended(entity, layout);

            if (obsRelAmended != null)
            {
                //Si la relacionada esta a marcada como nueva y no tiene valor resetar la amended.
                if ((obsRelAmended.ID <= 0) && !obsRelAmended.HasInfo())
                    entity.EditStatus.Reset();
                {
                    if (obsRelAmended.EditStatus.Value == StatusEntityValue.New)
                    {
                        obsRelAmended.AncestorID = entity.ID;
                    }
                }
            }
            else validationResults.AddResult(new ValidationResult(
                    string.Format(Properties.Resources.ERROR_ObservationAmendedNotFound, entity.LabelName),
                    this, null, null, null));

        }

        private void AnalyzeRegisteredObservationValue(RegisteredObservationValueEntity entity, RegisteredObservationBlockEntity entityBlock,
            RegisteredObservationTemplateEntity entityTemplate, RegisteredLayoutEntity layout, ValidationResults validationResults)
        {
            if ((entity == null) || (entity.EditStatus.Value == StatusEntityValue.NewAndDeleted) || (validationResults == null))
                return;

            if (entity.Required
                && !entity.HasInfo())
            {
                string blockName = (entityBlock != null) ? entityBlock.Name : string.Empty;
                string templateName = (entityTemplate != null)
                    ? (!string.IsNullOrEmpty(entityTemplate.TemplateTitle)) ? entityTemplate.TemplateTitle : entityTemplate.Name
                    : string.Empty;

                if (entityBlock != null && entityTemplate != null)
                {
                    validationResults.AddResult(
                        new ValidationResult(string.Format(Properties.Resources.MSG_ObservationRequiredOnBlockAndTemplate,
                                                            entity.LabelName, blockName, templateName),
                                            this, null, null, null));
                }
                else if (entityTemplate != null)
                {
                    validationResults.AddResult(
                        new ValidationResult(string.Format(Properties.Resources.MSG_ObservationRequiredOnTemplate,
                                                            entity.LabelName, templateName),
                                            this, null, null, null));
                }
            }

            if (!validationResults.IsValid) return;

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.Deleted:
                    if (entity.ID > 0)
                    {
                        //buscar el estado de la observacion en la base de datos si no es pending dar un error
                        ObservationStatusEnum dbStatus = FindStatusCustomerObservation(entity.ID);
                        if (dbStatus != ObservationStatusEnum.Pending)
                            validationResults.AddResult(
                                new ValidationResult(string.Format(Properties.Resources.ERROR_NotCanRemoveCustomerObservationSaveDB,
                                                                    entity.LabelName,
                                                                    ObservationStatusEnumNames.GetName(dbStatus)),
                                                    this,
                                                    null,
                                                    null,
                                                    null));
                    }

                    if (entity.ObservationValue != null)
                        entity.ObservationValue.EditStatus.Delete();

                    if (entity.ExtObservationValue != null)
                        entity.ExtObservationValue.EditStatus.Delete();
                    break;
                case StatusEntityValue.New:
                    ValidateObservation(entity, validationResults);

                    ValidateRegisteredObservationValueKingOf(entity, validationResults);

                    if (validationResults.IsValid)
                    {
                        if ((entity.ID <= 0) && !entity.HasInfo())
                        {
                            entity.EditStatus.Reset();

                            if (entity.ObservationValue != null)
                                entity.ObservationValue.EditStatus.Reset();

                            if (entity.ExtObservationValue != null)
                                entity.ExtObservationValue.EditStatus.Reset();
                        }
                    }
                    break;
                case StatusEntityValue.Updated:
                    ValidateObservation(entity, validationResults);

                    ValidateRegisteredObservationValueKingOf(entity, validationResults);

                    if (entity.ID > 0)
                    {
                        //buscar el estado de la observacion en la base de datos.
                        ObservationStatusEnum dbStatus = FindStatusCustomerObservation(entity.ID);
                        //validar si el estado en la BD es confirmed. Si lo es error de validacion al no ser que venga en estado Amended
                        if (dbStatus == ObservationStatusEnum.Confirmed)
                        {
                            if (entity.Status == ObservationStatusEnum.Amended)
                            {
                                AnalyzeRegisteredObservationValueAmended(entity, layout, validationResults);
                            }
                            else if (entity.Status == ObservationStatusEnum.Cancelled)
                            {

                            }
                            else if (entity.Observation.IsCalculated)
                            {

                            }
                            else
                            {
                                /*validationResults.AddResult(
                                    new ValidationResult(string.Format(Properties.Resources.ERROR_NotCanModifyCustomerObservation,
                                                                        entity.LabelName,
                                                                        ObservationStatusEnumNames.GetName(dbStatus)),
                                                        this,
                                                        null,
                                                        null,
                                                        null));*/
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            if ((entity.Observation != null) && (entity.Observation.ObservationEncoding != null)
                && (entity.Observation.ObservationEncoding.ObservationConcepts != null) && (entity.Observation.ObservationEncoding.ObservationConcepts.Length > 0))
                HandleBasicListActions<ObservationConceptRelEntity>(entity.Observation.ObservationEncoding.ObservationConcepts, validationResults);

            if (entity.ObservationValue != null)
                HandleBasicActions<ObservationValueEntity>(entity.ObservationValue, validationResults);

            if (entity.ExtObservationValue != null)
                HandleBasicActions<ExtObservationValueEntity>(entity.ExtObservationValue, validationResults);

            HandleBasicActions<RegisteredObservationValueEntity>(entity, validationResults);

            if (validationResults.IsValid)
                NotificationActBL.HandleNotifications(entity, false, validationResults);
        }

        private void AnalyzeRegisteredObservationBlock(RegisteredObservationBlockEntity entity, RegisteredObservationTemplateEntity entityTemplate,
            RegisteredLayoutEntity layout, ValidationResults validationResults)
        {
            if ((entity == null) || (validationResults == null))
                return;

            List<RegisteredObservationBlockRegisteredObservationValueRelationship> listBlockObservationRel = new List<RegisteredObservationBlockRegisteredObservationValueRelationship>();

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.Deleted:
                    //validar que todas sus observaciones estan marcadas como delete.
                    if ((entity.ID > 0) && (entity.Observations != null) && (entity.Observations.Length > 0))
                    {
                        if (Array.Exists(entity.Observations, rov => (rov.ID > 0) && (rov.EditStatus.Value != StatusEntityValue.Deleted) && (rov.EditStatus.Value != StatusEntityValue.NewAndDeleted)))
                            validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_NotCanRemoveBlockNotAllObservationsDelete, entity.Name),
                                this, null, null, null));

                        AnalyzeRegisteredObservationValueList(entity.Observations, entity, entityTemplate, layout, validationResults);

                        foreach (RegisteredObservationValueEntity observation in entity.Observations)
                        {
                            if (observation.CustomerObservationEvalTestID != 0
                                || observation.ID <= 0)
                                continue;

                            RegisteredObservationBlockRegisteredObservationValueRelationship robrovr
                                = new RegisteredObservationBlockRegisteredObservationValueRelationship(entity, observation);
                            robrovr.EditStatus.Delete();
                            listBlockObservationRel.Add(robrovr);

                        }
                    }
                    break;
                case StatusEntityValue.New:
                    if ((entity.Observations != null) && (entity.Observations.Length > 0))
                    {
                        AnalyzeRegisteredObservationValueList(entity.Observations, entity, entityTemplate, layout, validationResults);

                        foreach (RegisteredObservationValueEntity observation in entity.Observations)
                        {
                            if (observation.CustomerObservationEvalTestID != 0) continue;

                            if (observation.EditStatus.Value == StatusEntityValue.New)
                            {
                                RegisteredObservationBlockRegisteredObservationValueRelationship robrovr
                                    = new RegisteredObservationBlockRegisteredObservationValueRelationship(entity, observation);
                                robrovr.EditStatus.New();
                                listBlockObservationRel.Add(robrovr);
                            }
                        }
                    }
                    break;
                case StatusEntityValue.Updated:
                    if ((entity.Observations != null) && (entity.Observations.Length > 0))
                    {
                        AnalyzeRegisteredObservationValueList(entity.Observations, entity, entityTemplate, layout, validationResults);

                        foreach (RegisteredObservationValueEntity observation in entity.Observations)
                        {
                            if (observation.CustomerObservationEvalTestID != 0) continue;

                            RegisteredObservationBlockRegisteredObservationValueRelationship robrovr
                                = new RegisteredObservationBlockRegisteredObservationValueRelationship(entity, observation);

                            switch (observation.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    robrovr.EditStatus.Delete();
                                    listBlockObservationRel.Add(robrovr);
                                    break;
                                case StatusEntityValue.New:
                                    robrovr.EditStatus.New();
                                    listBlockObservationRel.Add(robrovr);
                                    break;
                                case StatusEntityValue.Updated:
                                    //robrovr.EditStatus.Update();
                                    //listBlockObservationRel.Add(robrovr);
                                    break;
                                default: break;
                            }
                        }
                    }
                    break;
                default: break;
            }

            if ((listBlockObservationRel.Count <= 0) && (entity.EditStatus.Value != StatusEntityValue.Updated))
                entity.EditStatus.Reset();

            HandleBasicActions<RegisteredObservationBlockEntity>(entity, validationResults);

            HandleBasicListActions<RegisteredObservationBlockRegisteredObservationValueRelationship>(listBlockObservationRel, validationResults);
        }

        private void AnalyzeRegisteredObservationTemplate(RegisteredObservationTemplateEntity entity, RegisteredLayoutEntity layout,
            ValidationResults validationResults)
        {
            if ((entity == null) || (validationResults == null))
                return;

            LoadRegisteredObservationValuesRequired(entity);

            List<RegisteredObservationTemplateRegisteredObservationBlockRelationship> listTemplateBlockRel
                = new List<RegisteredObservationTemplateRegisteredObservationBlockRelationship>();

            List<RegisteredObservationTemplateRegisteredObservationValueRelationship> listTemplateObservationRel
                = new List<RegisteredObservationTemplateRegisteredObservationValueRelationship>();

            switch (entity.EditStatus.Value)
            {
                case StatusEntityValue.Deleted:
                    //validar que todas sus observaciones y bloques estan marcados como delete.
                    bool canDelete = (entity.ID > 0);
                    if ((entity.ID > 0) && (entity.Observations != null) && (entity.Observations.Length > 0))
                    {
                        if (Array.Exists(entity.Observations, rov => (rov.ID > 0) && (rov.EditStatus.Value != StatusEntityValue.Deleted) && (rov.EditStatus.Value != StatusEntityValue.NewAndDeleted)))
                        {
                            validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_NotCanRemoveTemplateNotAllObservationsDelete, entity.Name),
                                this, null, null, null));
                            canDelete = false;
                        }
                    }

                    if ((entity.ID > 0) && (entity.Blocks != null) && (entity.Blocks.Length > 0))
                    {
                        if (Array.Exists(entity.Blocks, rob => (rob.ID > 0) && (rob.EditStatus.Value != StatusEntityValue.Deleted) && (rob.EditStatus.Value != StatusEntityValue.NewAndDeleted)))
                        {
                            validationResults.AddResult(new ValidationResult(
                                string.Format(Properties.Resources.ERROR_NotCanRemoveTemplateNotAllBlocksDelete, entity.Name),
                                this, null, null, null));
                            canDelete = false;
                        }
                    }

                    if (canDelete)
                    {
                        if ((entity.Observations != null) && (entity.Observations.Length > 0))
                        {
                            AnalyzeRegisteredObservationValueList(entity.Observations, null, entity, layout, validationResults);

                            foreach (RegisteredObservationValueEntity observation in entity.Observations)
                            {
                                if (observation.CustomerObservationEvalTestID != 0
                                    || observation.ID <= 0)
                                    continue;

                                RegisteredObservationTemplateRegisteredObservationValueRelationship rotrovr
                                    = new RegisteredObservationTemplateRegisteredObservationValueRelationship(entity, observation);
                                rotrovr.EditStatus.Delete();
                                listTemplateObservationRel.Add(rotrovr);
                            }
                        }

                        if ((entity.Blocks != null) && (entity.Blocks.Length > 0))
                        {
                            foreach (RegisteredObservationBlockEntity block in entity.Blocks)
                            {
                                AnalyzeRegisteredObservationBlock(block, entity, layout, validationResults);

                                if (block.ID <= 0)
                                    continue;

                                RegisteredObservationTemplateRegisteredObservationBlockRelationship rotrobr
                                    = new RegisteredObservationTemplateRegisteredObservationBlockRelationship(entity, block);
                                rotrobr.EditStatus.Delete();
                                listTemplateBlockRel.Add(rotrobr);
                            }
                        }
                    }
                    break;
                case StatusEntityValue.New:
                    if ((entity.Observations != null) && (entity.Observations.Length > 0))
                    {
                        AnalyzeRegisteredObservationValueList(entity.Observations, null, entity, layout, validationResults);

                        foreach (RegisteredObservationValueEntity observation in entity.Observations)
                        {
                            if (observation.CustomerObservationEvalTestID != 0) continue;

                            if (observation.EditStatus.Value == StatusEntityValue.New)
                            {
                                RegisteredObservationTemplateRegisteredObservationValueRelationship rotrovr
                                    = new RegisteredObservationTemplateRegisteredObservationValueRelationship(entity, observation);
                                rotrovr.EditStatus.New();
                                listTemplateObservationRel.Add(rotrovr);
                            }
                        }
                    }

                    if ((entity.Blocks != null) && (entity.Blocks.Length > 0))
                    {
                        foreach (RegisteredObservationBlockEntity block in entity.Blocks)
                        {
                            AnalyzeRegisteredObservationBlock(block, entity, layout, validationResults);

                            if (block.EditStatus.Value == StatusEntityValue.New)
                            {
                                RegisteredObservationTemplateRegisteredObservationBlockRelationship rotrobr
                                    = new RegisteredObservationTemplateRegisteredObservationBlockRelationship(entity, block);
                                rotrobr.EditStatus.New();
                                listTemplateBlockRel.Add(rotrobr);
                            }
                        }
                    }
                    break;
                case StatusEntityValue.Updated:
                    if ((entity.Observations != null) && (entity.Observations.Length > 0))
                    {
                        AnalyzeRegisteredObservationValueList(entity.Observations, null, entity, layout, validationResults);

                        foreach (RegisteredObservationValueEntity observation in entity.Observations)
                        {
                            if (observation.CustomerObservationEvalTestID != 0) continue;

                            RegisteredObservationTemplateRegisteredObservationValueRelationship rotrovr
                                = new RegisteredObservationTemplateRegisteredObservationValueRelationship(entity, observation);

                            switch (observation.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    rotrovr.EditStatus.Delete();
                                    listTemplateObservationRel.Add(rotrovr);
                                    break;
                                case StatusEntityValue.New:
                                    rotrovr.EditStatus.New();
                                    listTemplateObservationRel.Add(rotrovr);
                                    break;
                                case StatusEntityValue.Updated:
                                    //rotrovr.EditStatus.Update();
                                    //listTemplateObservationRel.Add(rotrovr);
                                    break;
                                default: break;
                            }
                        }
                    }

                    if ((entity.Blocks != null) && (entity.Blocks.Length > 0))
                    {
                        foreach (RegisteredObservationBlockEntity block in entity.Blocks)
                        {
                            AnalyzeRegisteredObservationBlock(block, entity, layout, validationResults);

                            RegisteredObservationTemplateRegisteredObservationBlockRelationship rotrobr
                                = new RegisteredObservationTemplateRegisteredObservationBlockRelationship(entity, block);

                            switch (block.EditStatus.Value)
                            {
                                case StatusEntityValue.Deleted:
                                    rotrobr.EditStatus.Delete();
                                    listTemplateBlockRel.Add(rotrobr);
                                    break;
                                case StatusEntityValue.New:
                                    rotrobr.EditStatus.New();
                                    listTemplateBlockRel.Add(rotrobr);
                                    break;
                                case StatusEntityValue.Updated:
                                    //rotrobr.EditStatus.Update();
                                    //listTemplateBlockRel.Add(rotrobr);
                                    break;
                                default: break;
                            }
                        }
                    }
                    break;
                default: break;
            }

            if ((listTemplateBlockRel.Count <= 0) && (listTemplateObservationRel.Count <= 0) && (entity.EditStatus.Value != StatusEntityValue.Updated))
                entity.EditStatus.Reset();

            HandleBasicActions<RegisteredObservationTemplateEntity>(entity, validationResults);

            if (listTemplateBlockRel.Count > 0)
                HandleBasicListActions<RegisteredObservationTemplateRegisteredObservationBlockRelationship>(listTemplateBlockRel, validationResults);

            if (listTemplateObservationRel.Count > 0)
                HandleBasicListActions<RegisteredObservationTemplateRegisteredObservationValueRelationship>(listTemplateObservationRel, validationResults);
        }

        private void LoadRegisteredObservationValuesRequired(RegisteredObservationTemplateEntity entity)
        {
            if (entity == null) return;

            //ObservationTemplateEntity observationTemplate = ObservationTemplateBL.GetObservationTemplate(entity.ObservationTemplateID);
            ObservationTemplateEntity observationTemplate = _observationCache.ObservationTemplateCache.Get(entity.ObservationTemplateID, false);
            if (observationTemplate == null || observationTemplate.Blocks == null || observationTemplate.Blocks.Length <= 0) return;

            foreach (ObservationTemplateRelEntity otr in observationTemplate.Blocks)
            {
                if (otr.ObservationBlockID > 0
                    && otr.Required
                    && entity.Blocks != null
                    && entity.Blocks.Length > 0)
                {
                    RegisteredObservationBlockEntity rob = entity.Blocks.Where(b => b.ObservationBlockID == otr.ObservationBlockID).Select(b => b).FirstOrDefault();
                    if (rob != null && rob.Observations != null && rob.Observations.Length > 0)
                    {
                        foreach (RegisteredObservationValueEntity rov in rob.Observations)
                        {
                            rov.Required = true;
                        }
                    }
                }
                if (otr.ObservationID > 0
                    && otr.Required
                    && entity.Observations != null
                    && entity.Observations.Length > 0)
                {
                    RegisteredObservationValueEntity rov = entity.Observations.Where(o => o.ObservationID == otr.ObservationID).Select(o => o).FirstOrDefault();
                    if (rov != null)
                        rov.Required = true;
                }
            }
        }

        private void AnalyzeRegisteredLayout(RegisteredLayoutEntity entity, ValidationResults validationResults)
        {
            if ((entity == null) || (validationResults == null)
                || ((entity.Templates == null) && (entity.Blocks == null) && (entity.Observations == null)))
                return;

            //Análisis de entidades RegisteredLayoutEntity -> Entities
            this.ValidateCustomer(entity, validationResults);

            //Análisis de RegisteredLayoutEntity
            if (validationResults.IsValid)
            {
                //Verificar identificadores de relación entre entidades

                //Análisis de entidades Entities -> RegisteredLayoutEntity
                if ((entity.Templates != null) && (entity.Templates.Length > 0))
                {
                    foreach (RegisteredObservationTemplateEntity template in entity.Templates)
                    {
                        AnalyzeRegisteredObservationTemplate(template, entity, validationResults);
                    }
                }

                if ((entity.Blocks != null) && (entity.Blocks.Length > 0))
                {
                    foreach (RegisteredObservationBlockEntity block in entity.Blocks)
                    {
                        AnalyzeRegisteredObservationBlock(block, null, entity, validationResults);
                    }
                }

                AnalyzeRegisteredObservationValueList(entity.Observations, null, null, entity, validationResults);
            }
        }

        private void AnalyzeRegisteredObservationValueList(RegisteredObservationValueEntity[] observations, RegisteredObservationBlockEntity rob,
            RegisteredObservationTemplateEntity rot, RegisteredLayoutEntity registeredLayout, ValidationResults validationResults)
        {
            if ((observations != null) && (observations.Length > 0))
            {
                RegisteredObservationValueEntity[] rovEvalTests = observations
                    .Where(obs => obs.Observation.KindOf == ObservationTypeEnum.EvalTestObservation)
                    .Select(obs => obs)
                    .ToArray();

                if (rovEvalTests != null && rovEvalTests.Length > 0)
                {
                    foreach (RegisteredObservationValueEntity observationEvalTest in rovEvalTests)
                    {
                        AnalyzeRegisteredObservationValue(observationEvalTest, rob, rot, registeredLayout, validationResults);

                        RegisteredObservationValueEntity[] relatedEvalTestROVs = observations
                            .Where(obs => obs.Observation.KindOf != ObservationTypeEnum.EvalTestObservation
                                && obs.CustomerObservationEvalTestID == observationEvalTest.ID)
                            .Select(obs => obs)
                            .ToArray();

                        if (relatedEvalTestROVs != null && relatedEvalTestROVs.Length > 0)
                        {
                            IEnumerable<RegisteredObservationValueRegisteredObservationValueRelationship> observationEvalTestObservationRelList
                                = GetRelationshipList<RegisteredObservationValueEntity, RegisteredObservationValueEntity, RegisteredObservationValueRegisteredObservationValueRelationship>(observationEvalTest, relatedEvalTestROVs);

                            if (observationEvalTestObservationRelList.Any())
                                HandleBasicListActions<RegisteredObservationValueRegisteredObservationValueRelationship>(observationEvalTestObservationRelList, validationResults);
                        }
                    }
                }

                RegisteredObservationValueEntity[] notROVEvalTests = observations
                    .Where(obs => obs.Observation.KindOf != ObservationTypeEnum.EvalTestObservation)
                    .Select(obs => obs)
                    .ToArray();
                if (notROVEvalTests != null && notROVEvalTests.Length > 0)
                {
                    foreach (RegisteredObservationValueEntity observation in notROVEvalTests)
                    {
                        AnalyzeRegisteredObservationValue(observation, rob, rot, registeredLayout, validationResults);
                    }
                }
            }
        }
        #endregion

        #region BusinessLayerBase protected overriden methods
        protected override void RegisterHandlers()
        {
            UnitOfWork.RegisterHandler(typeof(RegisteredLayoutEntity),
                new ORMHandler<RegisteredLayoutEntity>(null, null, null));

            UnitOfWork.RegisterHandler(typeof(ObservationValueEntity),
                new ORMHandler<ObservationValueEntity>(ObservationValueNew, ObservationValueUpdate, ObservationValueDelete));

            UnitOfWork.RegisterHandler(typeof(ExtObservationValueEntity),
                new ORMHandler<ExtObservationValueEntity>(ExtObservationValueNew, ExtObservationValueUpdate, ExtObservationValueDelete));

            UnitOfWork.RegisterHandler(typeof(RegisteredObservationValueEntity),
                new ORMHandler<RegisteredObservationValueEntity>(RegisteredObservationValueNew, RegisteredObservationValueUpdate,
                    RegisteredObservationValueDelete));

            UnitOfWork.RegisterHandler(typeof(RegisteredObservationBlockEntity),
                new ORMHandler<RegisteredObservationBlockEntity>(RegisteredObservationBlockNew, RegisteredObservationBlockUpdate,
                    RegisteredObservationBlockDelete));

            UnitOfWork.RegisterHandler(typeof(RegisteredObservationTemplateEntity),
                new ORMHandler<RegisteredObservationTemplateEntity>(RegisteredObservationTemplateNew, RegisteredObservationTemplateUpdate,
                    RegisteredObservationTemplateDelete));

            UnitOfWork.RegisterHandler(typeof(RegisteredObservationValueRegisteredObservationValueRelationship),
                new ORMHandler<RegisteredObservationValueRegisteredObservationValueRelationship>(
                    RegisteredObservationValueRegisteredObservationValueRelationshipNew,
                    RegisteredObservationValueRegisteredObservationValueRelationshipUpdate,
                    RegisteredObservationValueRegisteredObservationValueRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(RegisteredObservationBlockRegisteredObservationValueRelationship),
                new ORMHandler<RegisteredObservationBlockRegisteredObservationValueRelationship>(
                    RegisteredObservationBlockRegisteredObservationValueRelationshipNew, null,
                    RegisteredObservationBlockRegisteredObservationValueRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(RegisteredObservationTemplateRegisteredObservationBlockRelationship),
                new ORMHandler<RegisteredObservationTemplateRegisteredObservationBlockRelationship>(
                    RegisteredObservationTemplateRegisteredObservationBlockRelationshipNew, null,
                    RegisteredObservationTemplateRegisteredObservationBlockRelationshipDelete));

            UnitOfWork.RegisterHandler(typeof(RegisteredObservationTemplateRegisteredObservationValueRelationship),
                new ORMHandler<RegisteredObservationTemplateRegisteredObservationValueRelationship>(
                    RegisteredObservationTemplateRegisteredObservationValueRelationshipNew, null,
                    RegisteredObservationTemplateRegisteredObservationValueRelationshipDelete));

            if (!UnitOfWork.IsHandlerRegistered(typeof(ObservationConceptRelEntity)))
                UnitOfWork.RegisterHandler(typeof(ObservationConceptRelEntity),
                    new ORMHandler<ObservationConceptRelEntity>(ObservationConceptRelNew, null, null));
        }

        protected override void AnalyzeActions(RegisteredLayoutEntity entity, ValidationResults validationResults)
        {
            base.AnalyzeActions(entity, validationResults);

            InitializeHelpers();
            AnalyzeRegisteredLayout(entity, validationResults);
        }

        public override RegisteredLayoutEntity Save(RegisteredLayoutEntity entity)
        {
            RegisteredLayoutEntity cor = base.Save(entity);
            cor.EditStatus.Reset();
            NotificationActBL.SendNotifications();
            NotificationActBL.ResetNotifications();

            return cor;
        }
        #endregion

        #region BusinessLayerBase public overriden methods
        public override long GetDBTimestamp(int id)
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }
        #endregion

        #region ICustomerObservationService Members
        public int ExistCustomerObservationBySpecialCategoryType(int customerID)
        {
            try
            {
                ObservationTemplateBL observationTemplateBL = new ObservationTemplateBL();
                int observationTemplateID = observationTemplateBL.GetExceptionalInfoTemplate();
                int observationID = observationTemplateBL.GetExceptionalInfoLOPD();

                return DataAccess.CustomerObservationDA.GetCustomerObservationSpecialCategory(customerID, observationTemplateID, observationID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return 0;
            }
        }

        public bool UpdateSpecialCategoryType(int customerObservationID, SpecialCategoryTypeEnum specialCategoryType)
        {
            try
            {
                if (customerObservationID <= 0) throw new ArgumentNullException("customerObservationID");

                return DataAccess.CustomerObservationDA.UpdateSpecialCategoryType(customerObservationID, specialCategoryType, GetIdentityUserName());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public RegisteredObservationValueEntity[] GetRegisteredObservationValuesByCustomer(int customerID)
        {
            try
            {
                DataSet ds = DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomer(customerID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)))
                {
                    _observationCache.UpdateCaches();

                    //AddDataSetObservations(customerID, ds);
                    MergeTable(DataAccess.ObservationDA.GetObservationsByCustomerID(customerID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);

                    //AddDataSetObservationValues(customerID, ds);
                    MergeTable(DataAccess.ObservationValueDA.GetAllObservationValues(customerID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);

                    //AddDataSetExtObservationValues(customerID, ds);
                    MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);

                    //RegisteredObservationValueAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                    RegisteredObservationValueEntity[] observationValues = registeredObservationValueAdapter.GetData(ds);
                    SetObservations(observationValues);
                    return OrderedObservations(observationValues);
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        #region Observation Value Entity
        public ObservationValueEntity GetLastCustomerObservationValue(int observationID, int customerID)
        {
            try
            {
                ObservationValueAdvancedAdapter observationValueAdapter = new ObservationValueAdvancedAdapter();
                _observationCache.UpdateCaches();

                DataSet ds = DataAccess.ObservationValueDA.GetLastCustomerObservationValue(observationID, customerID);

                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Rows != null)
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Rows.Count > 0))
                {
                    return observationValueAdapter.GetInfo(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Rows[0], ds);
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

        #region Ext Observation Value Entity
        public ExtObservationValueEntity GetLastCustomerExtObservationValue(int observationID, int customerID)
        {
            try
            {
                ExtObservationValueAdapter extObservationValueAdapter = new ExtObservationValueAdapter();
                _observationCache.UpdateCaches();

                DataSet ds = DataAccess.ExtObservationValueDA.GetLastCustomerExtObservationValue(observationID, customerID);

                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Rows != null)
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Rows.Count > 0))
                {
                    return extObservationValueAdapter.GetInfo(ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Rows[0], ds);
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

        #region Read Methos for All and Current
        /// <summary>
        /// estos son para obsdiagrams y ya llevan las observaciones implicitas 
        /// </summary>
        public RegisteredLayoutEntity GetAllRegisteredLayoutByRegisteredTemplate(int customerID, RegisteredObservationTemplateEntity rot)
        {
            try
            {
                _observationCache.UpdateCaches();

                if ((rot == null) || ((rot.Blocks == null) && (rot.Observations == null)) || (rot.ObservationTemplateID <= 0) || (customerID <= 0)) return null;
                RegisteredObservationTemplateEntity[] templates = GetRegisteredObservationTemplateByObservationTemplateID(customerID, rot.ObservationTemplateID);
                this.SetObservations(new RegisteredObservationTemplateEntity[] { rot });
                this.SetObservations(templates);
                if ((templates != null) && (templates.Length > 0) && ((from lrot in templates where lrot.ID == rot.ID select lrot).FirstOrDefault() != null))
                {
                    return new RegisteredLayoutEntity(customerID, null, null, templates);
                }
                else
                {
                    List<RegisteredObservationTemplateEntity> myTemplates = new List<RegisteredObservationTemplateEntity>();
                    if ((templates != null) && (templates.Length > 0)) myTemplates.AddRange(templates);
                    myTemplates.Add(rot);
                    return new RegisteredLayoutEntity(customerID, null, null, myTemplates.ToArray());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetAllRegisteredLayoutByRegisteredBlock(int customerID, RegisteredObservationBlockEntity rob)
        {
            try
            {
                _observationCache.UpdateCaches();

                if ((rob == null) || (rob.Observations == null) || (rob.ObservationBlockID <= 0) || (customerID <= 0)) return null;
                RegisteredObservationBlockEntity[] blocks = GetRegisteredObservationBlockByObservationBlockID(customerID, rob.ObservationBlockID);
                this.SetObservations(new RegisteredObservationBlockEntity[] { rob });
                this.SetObservations(blocks);
                if ((blocks != null) && (blocks.Length > 0) && ((from lrob in blocks where lrob.ID == rob.ID select lrob).FirstOrDefault() != null))
                {

                    return new RegisteredLayoutEntity(customerID, null, OrderedBlocks(blocks), null);
                }
                else
                {
                    List<RegisteredObservationBlockEntity> myBlocks = new List<RegisteredObservationBlockEntity>();
                    if ((blocks != null) && (blocks.Length > 0)) myBlocks.AddRange(blocks);
                    myBlocks.Add(rob);
                    return new RegisteredLayoutEntity(customerID, null, OrderedBlocks(myBlocks.ToArray()), null);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetAllRegisteredLayoutByRegisteredObservation(int customerID, RegisteredObservationValueEntity rov)
        {
            try
            {
                _observationCache.UpdateCaches();

                if ((rov == null) || (rov.Observation == null) || (rov.Observation.ID <= 0) || (customerID <= 0))
                    return null;

                RegisteredObservationValueEntity[] obsValues = GetRegisteredObservationValuesByObservationID(customerID, rov.Observation.ID);
                this.SetObservations(new RegisteredObservationValueEntity[] { rov });
                this.SetObservations(obsValues);
                if ((obsValues != null) && (obsValues.Length > 0) && ((from lrov in obsValues where lrov.ID == rov.ID select lrov).FirstOrDefault() != null))
                {
                    return new RegisteredLayoutEntity(customerID, OrderedObservations(obsValues), null, null);
                }
                else
                {
                    List<RegisteredObservationValueEntity> myObsValues = new List<RegisteredObservationValueEntity>();
                    if ((obsValues != null) && (obsValues.Length > 0)) myObsValues.AddRange(obsValues);
                    myObsValues.Add(rov);
                    return new RegisteredLayoutEntity(customerID, OrderedObservations(myObsValues.ToArray()), null, null);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        #endregion
        #endregion

        #region RegisteredObservationValueEntity private methods
        private RegisteredObservationValueEntity[] RebuildObservations(RegisteredObservationValueEntity[] rovs, ObservationTemplateEntity observationTemplate, RegisteredObservationHeaderDTO header)
        {
            if ((observationTemplate == null) || (observationTemplate.Blocks == null))
                return null;

            List<RegisteredObservationValueEntity> rovlist = new List<RegisteredObservationValueEntity>();

            if ((rovs != null) && (rovs.Length > 0))
                rovlist.AddRange(rovs);

            //foreach (ObservationTemplateRelEntity obsRel in observationTemplate.Blocks)
            //{
            //    if ((obsRel.ElementType == TemplateElementTypeEnum.Observation)
            //        && ((rovs == null) || ((from o in rovs where o.Observation.ID == obsRel.Observation.ID select o).FirstOrDefault() == null)))
            //    {
                    //if (obsRel.Status == CommonEntities.StatusEnum.Active)
                    //{
                    //    header.ObservationID = obsRel.Observation.ID;
                    //    header.VirtualObsID = virtualObsID;
                    //    RegisteredObservationValueEntity rov = GetVirtualRegisteredObservationValue(header, out virtualObsID);
                    //    if (rov != null)
                    //    {
                    //        rov.Order = obsRel.Order;
                    //        rovlist.Add(rov);
                    //    }
                    //}
            //    }
            //}
            return rovlist.ToArray();
        }

        private RegisteredObservationValueEntity GetFirstRegisteredObservationValue(RegisteredObservationBlockEntity lrob)
        {
            if (lrob != null)
            {
                if ((lrob.Observations != null) && (lrob.Observations.Length > 0))
                {
                    return lrob.Observations[0];
                }
            }
            return null;
        }

        private RegisteredObservationValueEntity GetFirstRegisteredObservationValue(RegisteredObservationTemplateEntity lrot)
        {
            if (lrot != null)
            {
                if ((lrot.Blocks != null) && (lrot.Blocks.Length > 0))
                {
                    foreach (RegisteredObservationBlockEntity rob in lrot.Blocks)
                    {
                        if ((rob.Observations != null) && (rob.Observations.Length > 0))
                        {
                            return rob.Observations[0];
                        }
                    }
                }
                if ((lrot.Observations != null) && (lrot.Observations.Length > 0))
                {
                    return lrot.Observations[0];
                }
            }
            return null;
        }

        private RegisteredObservationValueEntity GetFirstRegisteredObservationValue(RegisteredLayoutEntity rle)
        {
            if ((rle == null) || (((rle.Observations == null) || (rle.Observations.Length <= 0))
                && ((rle.Blocks == null) || (rle.Blocks.Length <= 0)) && ((rle.Templates == null) || (rle.Templates.Length <= 0)))) return null;
            if ((rle.Templates != null) && (rle.Templates.Length > 0))
            {
                return GetFirstRegisteredObservationValue(rle.Templates[0]);
            }
            if ((rle.Blocks != null) && (rle.Blocks.Length > 0))
            {
                return GetFirstRegisteredObservationValue(rle.Blocks[0]);
            }
            if ((rle.Observations != null) && (rle.Observations.Length > 0))
            {
                return rle.Observations[0];
            }
            return null;
        }

        private RegisteredObservationValueEntity GetRegisteredObservationValueByCustomerObservation(int customerObservationID)
        {
            try
            {
                DataSet ds = DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerObservation(customerObservationID);
                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)))
                {
                    AddDataSetObservationsByCustomerObservation(customerObservationID, ds);
                    AddDataSetObservationValuesByCustomerObservation(customerObservationID, ds);
                    AddDataSetExtObservationValuesByCustomerObservation(customerObservationID, ds);
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                    RegisteredObservationValueEntity[] observationValues = registeredObservationValueAdapter.GetData(ds);
                    SetObservations(observationValues);
                    return observationValues[0];
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private RegisteredObservationValueEntity[] GetRegisteredObservationValuesByObservationID(int customerID, int observationID)
        {
            try
            {
                DataSet ds = DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndObsID(customerID, observationID);
                if ((ds != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))
                {
                    //AddDataSetObservations(customerID, observationID, ds);
                    MergeTable(DataAccess.ObservationDA.GetObservation(observationID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);

                    //AddDataSetObservationValues(customerID, observationID, ds);
                    MergeTable(DataAccess.ObservationValueDA.GetAllObservationValuesByCustomerAndObsID(customerID, observationID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);

                    //AddDataSetExtObservationValues(customerID, observationID, ds);
                    MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValuesByCustomerAndObsID(customerID, observationID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);

                    //RegisteredObservationValueAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                    RegisteredObservationValueEntity[] observationValues = registeredObservationValueAdapter.GetData(ds);
                    SetObservations(observationValues);
                    return OrderedObservations(observationValues);
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        //solo de uso interno entre BLs
        public RegisteredObservationValueEntity[] GetRegisteredObservationValuesByObservationID(int customerID, string[] observations)
        {
            try
            {
                if (customerID <= 0 || observations == null || observations.Length <= 0) return null;
                List<RegisteredObservationValueEntity> rovlist = new List<RegisteredObservationValueEntity>();
                //ESTO DEBERÍA CONVERTIRSE EN UNA SOLA BL
                //REVISAR SIN FALTA
                int observationID = 0;
                foreach (string obsCode in observations)
                {
                    observationID = DataAccess.ObservationDA.GetObservationIDByCode(obsCode);
                    if (observationID > 0)
                    {
                        RegisteredObservationValueEntity[] rovs = GetRegisteredObservationValuesByObservationID(customerID, observationID);
                        if (rovs != null && rovs.Length > 0)
                            rovlist.AddRange(rovs);
                    }
                }
                return rovlist.Count > 0 ? rovlist.ToArray() : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredObservationValueEntity GetNewROV(int customerID, string observationCode)
        {
            IObservationCacheService _masterObservationCache = IoCFactory.CurrentContainer
                .Resolve<IObservationCacheService>();
            ObservationEntity oe = _masterObservationCache.ObservationCache.Find(obs => obs.AssignedCode == observationCode);
            return GetNewROV(customerID, oe);
        }

        public RegisteredObservationValueEntity GetNewROV(int customerID, int observationID)
        {
            IObservationCacheService _masterObservationCache = IoCFactory.CurrentContainer
                .Resolve<IObservationCacheService>();
            ObservationEntity oe = _masterObservationCache.ObservationCache.Get(observationID, false);
            return GetNewROV(customerID, oe);
        }

        public RegisteredObservationValueEntity GetNewROV(int customerID, ObservationEntity oe)
        {
            ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
            ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
            SpecialCategoryTypeEnum spc = this.GetSpecialCategory(oe);

            return new RegisteredObservationValueEntity(customerID, 0, string.Empty, EpisodeTypeEnum.None,
                CommonEntities.StatusEnum.None, null, ElementTypeEnum.None, 0, string.Empty, null, 0, string.Empty, 0, 0, 0, oe.Name, true,
                LabelPositionEnum.Left, 1, false, oe.ID, oe, ove, eove, 0, DateTime.Now,
                spc, ObservationStatusEnum.None, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
        }

        #endregion

        #region RegisteredObservationBlockEntity private methods
        private RegisteredObservationBlockEntity RebuildBlock(RegisteredObservationBlockEntity rob)
        {
            if ((rob == null) || (rob.ObservationBlockID <= 0) || (rob.Observations == null) || (rob.Observations.Length <= 0))
                return null;

            //ObservationBlockEntity ob = ObservationBlockBL.GetFullObservationBlock(rob.ObservationBlockID);
            ObservationBlockEntity ob = _observationCache.ObservationBlockCache.Get(rob.ObservationBlockID, false);

            if ((ob == null) || (ob.Observations == null) || (ob.Observations.Length <= 0))
                return null;

            int blockObs = ob.Observations.Length;
            if (blockObs == rob.Observations.Length)
                return rob;

            //int auxVirtualObsID = -1;
            RegisteredObservationValueEntity rov = rob.Observations[0];
            RegisteredObservationHeaderDTO header = new RegisteredObservationHeaderDTO(rov.CustomerID, rov.EpisodeID, rov.EpisodeNumber, rov.EpisodeType, rov.EpisodeStatus, rov.EpisodeDateTime,
                rov.EntityType, rov.EntityActID, rov.EntityActName, rov.EntityID, rov.EntityName, 1, rov.SpecialCategoryType, 0, 0, 0, 0, rob.ObservationBlockID, 0);
            List<RegisteredObservationValueEntity> rovList = new List<RegisteredObservationValueEntity>();
            rovList.AddRange(rob.Observations);
            foreach (ObservationBlockRelEntity obr in ob.Observations)
            {
                rov = (from rovb in rob.Observations where rovb.Observation.ID == obr.Observation.ID select rovb).FirstOrDefault();
                if ((rov == null)
                    && (obr.Status == CommonEntities.StatusEnum.Active))
                {
                    //header.VirtualBlockID = rob.ID;
                    //header.ObservationID = obr.Observation.ID;
                    //header.VirtualObsID = auxVirtualObsID;
                    //rov = GetVirtualRegisteredObservationValue(header, out auxVirtualObsID);
                    //if (rov != null)
                    //{
                    //    rov.Order = obr.Order;
                    //    rov.VisibleLabel = obr.VisibleLabel;
                    //    rovList.Add(rov);
                    //}
                }
            }
            rob.Observations = OrderedObservations(rovList.ToArray());
            return rob;
        }

        private RegisteredObservationBlockEntity[] RebuildBlocks(RegisteredObservationBlockEntity[] robs)
        {
            if ((robs == null) || (robs.Length <= 0)) return null;
            List<RegisteredObservationBlockEntity> robList = new List<RegisteredObservationBlockEntity>();
            foreach (RegisteredObservationBlockEntity block in robs)
            {
                RegisteredObservationBlockEntity rob = RebuildBlock(block);
                if (rob != null)
                {
                    rob.Order = block.Order;
                    robList.Add(rob);
                }
            }
            return robList.ToArray();
        }

        private RegisteredObservationBlockEntity[] RebuildBlocks(RegisteredObservationBlockEntity[] robs, ObservationTemplateEntity observationTemplate, RegisteredObservationHeaderDTO header)
        {
            if ((observationTemplate == null) || (observationTemplate.Blocks == null)) return null;
            int blocksQ = (from b in observationTemplate.Blocks where b.ElementType == TemplateElementTypeEnum.ObservationBlock select b).Count();
            //if ((robs != null) && (blocksQ == robs.Length)) return robs;
            List<RegisteredObservationBlockEntity> robList = new List<RegisteredObservationBlockEntity>();
            //if ((robs != null) && (robs.Length > 0)) robList.AddRange(robs);
            int virtualBlockID = header.VirtualBlockID;
            int virtualObsID = header.VirtualObsID;
            //int auxVirtualTemplateID = 0;


            RegisteredObservationBlockEntity rob = null;
            foreach (ObservationTemplateRelEntity block in observationTemplate.Blocks)
            {
                if (block.ElementType == TemplateElementTypeEnum.ObservationBlock)
                {
                    rob = ((robs != null) && (robs.Length > 0)) ? (from b in robs where b.ObservationBlockID == block.ObservationBlock.ID select b).FirstOrDefault() : null;
                    if (rob == null)
                    {
                        //if (block.Status == CommonEntities.StatusEnum.Active)
                        //{
                        //    header.ObservationBlockID = block.ObservationBlock.ID;
                        //    header.VirtualBlockID = virtualBlockID;
                        //    header.VirtualObsID = virtualObsID;
                        //    header.OrderBlockID = block.Order;
                        //    rob = GetVirtualRegisteredObservationBlock(header, out virtualBlockID, out virtualObsID);
                        //    rob.Order = block.Order;
                        //    rob.VisibleLabel = block.VisibleLabel;
                        //    robList.Add(rob);
                        //}
                    }
                    else
                    {
                        rob = RebuildBlock(rob);
                        if (rob != null)
                        {
                            rob.VisibleLabel = block.VisibleLabel;
                            robList.Add(rob);
                        }
                    }
                }
            }
            return robList.ToArray();
        }

        private void SetObservations(RegisteredObservationBlockEntity[] blocks)
        {
            if ((blocks == null) || (blocks.Length <= 0))
                return;

            foreach (RegisteredObservationBlockEntity rob in blocks)
            {
                //ObservationBlockEntity observationBlock = ObservationBlockBL.GetObservationBlock(rob.ObservationBlockID);
                ObservationBlockEntity observationBlock = _observationCache.ObservationBlockCache.Get(rob.ObservationBlockID, false);
                //if ((rob.Observations == null) || (observationBlock.Observations.Length > rob.Observations.Length)) rob.Observations = RebuildObservations(rob.Observations, observationBlock, rob.ID);
                if ((observationBlock != null) && (observationBlock.Observations != null))
                {
                    if ((rob.Observations != null) && (rob.Observations.Length > 0))
                    {
                        SetObservations(rob.Observations);
                    }
                }
            }
        }

        private RegisteredObservationBlockEntity[] OrderedBlocks(RegisteredObservationBlockEntity[] blocks)
        {
            if (blocks == null) return null;
            foreach (RegisteredObservationBlockEntity rob in blocks)
            {
                if (rob.Observations != null)
                    rob.Observations = (from o in rob.Observations select o).OrderBy(o => o.Order).ToArray();
            }
            return (from rob in blocks select rob).OrderBy(rob => rob.RegistrationDateTime).ToArray();
        }

        public RegisteredObservationBlockEntity[] GetRegisteredObservationBlockByObservationBlockID(int customerID, int observationBlockID)
        {
            try
            {
                if ((customerID <= 0) || (observationBlockID <= 0)) return null;
                RegisteredObservationBlockEntity[] myBlocks = this.GetRegisteredObservationBlocksByCustomer(customerID);
                if ((myBlocks != null) && (myBlocks.Length > 0))
                {
                    foreach (RegisteredObservationBlockEntity b in myBlocks)
                    {
                        if (b.Observations != null)
                        {
                            b.Observations = (from o in b.Observations select o).OrderBy(o => o.Order).ToArray();
                        }
                    }
                    return (from rob in myBlocks where rob.ObservationBlockID == observationBlockID select rob).OrderByDescending(rob => rob.RegistrationDateTime).ToArray();
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private RegisteredObservationBlockEntity[] GetRegisteredObservationBlocksByCustomer(int customerID)
        {
            try
            {
                DataSet ds = DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomer(customerID);

                #region BlockLayouts
                DataSet ds2 = DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels();
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion

                #region RegisteredObservationValues
                ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomer(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Copy();
                    ds.Tables.Add(dt);
                }
                #region Observations
                ds2 = DataAccess.ObservationDA.GetAllObservations();
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion

                #region ObservationValues
                ds2 = DataAccess.ObservationValueDA.GetAllObservationValues(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion

                #region ExtObservationValues
                ds2 = DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion
                #endregion

                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)))
                {
                    RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                    RegisteredObservationBlockEntity[] observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                    return observationBlocks;
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

        #region RegisteredObservationTemplateEntity pivate methods
        private RegisteredObservationTemplateEntity[] OrderedTemplates(RegisteredObservationTemplateEntity[] templates)
        {
            if ((templates == null) || (templates.Length <= 0))
                return null;

            foreach (RegisteredObservationTemplateEntity lrot in templates)
            {
                LoadRegisteredObservationValuesRequired(lrot);

                if ((lrot.Blocks != null) && (lrot.Blocks.Length > 0))
                {
                    foreach (RegisteredObservationBlockEntity b in lrot.Blocks)
                    {
                        if (b.Observations != null)
                            b.Observations = (from o in b.Observations select o).OrderBy(o => o.Order).ToArray();
                    }

                    lrot.Blocks = (from b in lrot.Blocks select b).OrderBy(b => b.Order).ToArray();
                }

                if ((lrot.Observations != null) && (lrot.Observations.Length > 0))
                {
                    lrot.Observations = (from o in lrot.Observations select o).OrderBy(o => o.Order).ToArray();
                }
            }

            return (from rot in templates select rot).OrderBy(rot => rot.RegistrationDateTime).ToArray();
        }

        private ObservationTemplateEntity ObservationTemplate(ObservationTemplateEntity[] otes, int templateID)
        {
            if ((otes == null) || (otes.Length <= 0))
                return null;

            return (from ote in otes where ote.ID == templateID select ote).FirstOrDefault();
        }

        public RegisteredObservationTemplateEntity[] RebuildTemplates(RegisteredObservationTemplateEntity[] templates)
        {
            if ((templates == null) || (templates.Length <= 0))
                return null;

            int[] templateIDs = (from rot in templates select rot.ObservationTemplateID).Distinct().ToArray();

            //ObservationTemplateEntity[] otes = ObservationTemplateBL.GetObservationTemplates(templateIDs);
            ObservationTemplateEntity[] otes = _observationCache.ObservationTemplateCache.FindAll(ot => templateIDs.Contains(ot.ID), false);

            int auxVirtualBlockID = 0;
            foreach (RegisteredObservationTemplateEntity lrot in templates)
            {
                RegisteredObservationValueEntity rov = this.GetFirstRegisteredObservationValue(lrot);
                if (rov != null)
                {
                    RegisteredObservationHeaderDTO header = new RegisteredObservationHeaderDTO(rov.CustomerID, rov.EpisodeID, rov.EpisodeNumber, rov.EpisodeType, rov.EpisodeStatus, rov.EpisodeDateTime,
                        rov.EntityType, rov.EntityActID, rov.EntityActName, rov.EntityID, rov.EntityName, 0, SpecialCategoryTypeEnum.None/*rov.SpecialCategoryType*/, lrot.ID, -1, -1, lrot.ObservationTemplateID, 0, 0);
                    ObservationTemplateEntity ote = this.ObservationTemplate(otes, lrot.ObservationTemplateID);
                    if ((ote.Blocks != null) && (ote.Blocks.Length > 0))
                    {
                        lrot.Blocks = RebuildBlocks(lrot.Blocks, ote, header);
                        auxVirtualBlockID = header.VirtualBlockID;
                        header.VirtualBlockID = 0;
                        lrot.Observations = RebuildObservations(lrot.Observations, ote, header);
                        header.VirtualBlockID = auxVirtualBlockID;
                    }
                }
            }
            return OrderedTemplates(templates);
        }

        private void SetObservations(RegisteredObservationTemplateEntity[] templates)
        {
            if ((templates == null) || (templates.Length <= 0))
                return;

            foreach (RegisteredObservationTemplateEntity rot in templates)
            {
                //ObservationTemplateEntity observationtemplate = ObservationTemplateBL.GetObservationTemplate(rot.ObservationTemplateID);
                ObservationTemplateEntity observationtemplate = _observationCache.ObservationTemplateCache.Get(rot.ObservationTemplateID, false);

                if ((observationtemplate != null) && (observationtemplate.Blocks != null))
                {
                    //int blocksq = (from b in observationtemplate.Blocks where b.ElementType == TemplateElementTypeEnum.ObservationBlock select b).Count();
                    //int obsq = (from b in observationtemplate.Blocks where b.ElementType == TemplateElementTypeEnum.Observation select b).Count();
                    //if ((blocksq > 0) && ((rot.Blocks == null) || (blocksq > rot.Blocks.Length))) rot.Blocks = RebuildBlocks(rot.Blocks,rot,
                    if ((rot.Blocks != null) && (rot.Blocks.Length > 0))
                    {
                        SetObservations(rot.Blocks);
                    }
                    //if ((obsq > 0) && ((rot.Observations == null) || (obsq > rot.Observations.length))) rot.Observations = RebuildObservations(rot.observations, observationtemplate, rot.id);
                    if ((rot.Observations != null) && (rot.Observations.Length > 0))
                    {
                        SetObservations(rot.Observations);
                    }
                }
            }
        }

        public RegisteredObservationTemplateEntity[] GetRegisteredObservationTemplatesByCustomer(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                int customerID = DataAccess.CustomerEpisodeDA.GetCustomerID(customerEpisodeID);
                if (customerID <= 0) return null;
                DataSet ds = DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerEpisode(customerEpisodeID, observationTemplateIDs);
                return GetRegisteredObservationTemplatesByCustomerTemplateIDs(customerID, ds);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private RegisteredObservationTemplateEntity[] GetConfirmedRoutineActRegisteredObservationTemplatesByCustomer(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                int customerID = DataAccess.CustomerEpisodeDA.GetCustomerID(customerEpisodeID);
                if (customerID <= 0) return null;
                DataSet ds = DataAccess.CustomerObservationDA.GetConfirmedRoutineActRegisteredObservationTemplatesByCustomerEpisode(customerEpisodeID, observationTemplateIDs);
                return GetRegisteredObservationTemplatesByCustomerTemplateIDs(customerID, ds);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// Confirmend and Amended (2, 8)
        /// </summary>
        /// <param name="customerEpisodeID"></param>
        /// <returns></returns>
        private RegisteredObservationTemplateEntity[] GetCustomerMedEpisodeActRegisteredObservationTemplatesByEpisode(int customerEpisodeID, int[] observationTemplateIDs)
        {            
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                int customerID = DataAccess.CustomerEpisodeDA.GetCustomerID(customerEpisodeID);
                if (customerID <= 0) return null;

                int[] customerEpisodeIDs = new int[] { customerEpisodeID };

                DataSet ds = DataAccess.CustomerObservationDA.GetCustomerMedEpisodeActRegisteredObservationTemplatesByCustomerEpisodeIDs(customerEpisodeIDs, observationTemplateIDs);
                return GetRegisteredObservationTemplatesByCustomerTemplateIDs(customerID, ds);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private RegisteredObservationTemplateEntity[] GetConfirmedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomer(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                int customerID = DataAccess.CustomerEpisodeDA.GetCustomerID(customerEpisodeID);
                if (customerID <= 0) return null;
                DataSet ds = DataAccess.CustomerObservationDA.GetConfirmedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomerEpisode(customerEpisodeID, observationTemplateIDs);
                return GetRegisteredObservationTemplatesByCustomerTemplateIDs(customerID, ds);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        private RegisteredObservationTemplateEntity[] GetAmendedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomer(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                int customerID = DataAccess.CustomerEpisodeDA.GetCustomerID(customerEpisodeID);
                if (customerID <= 0) return null;
                DataSet ds = DataAccess.CustomerObservationDA.GetAmendedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomerEpisode(customerEpisodeID, observationTemplateIDs);
                return GetRegisteredObservationTemplatesByCustomerTemplateIDs(customerID, ds);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// DEVUELVE UNA LISTA DE RegisteredObservationTemplateEntity ORDENADA POR FECHA QUE SE BUSCA POR PACIENTE
        /// </summary>
        public RegisteredObservationTemplateEntity[] GetRegisteredObservationTemplatesByCustomer(int customerID)
        {
            try
            {
                DataSet ds = DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomer(customerID);

                #region RegisteredObservationBlocks
                DataSet ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomer(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Copy();
                    ds.Tables.Add(dt);
                }

                #region BlockLayouts
                ds2 = DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels();
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion

                #region RegisteredObservationValues
                ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomer(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Copy();
                    ds.Tables.Add(dt);
                }
                #region Observations
                ds2 = DataAccess.ObservationDA.GetAllObservations();
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion

                #region ObservationValues
                ds2 = DataAccess.ObservationValueDA.GetAllObservationValues(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion

                #region ExtObservationValues
                ds2 = DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID);
                if ((ds2 != null) && (ds2.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable)))
                {
                    DataTable dt = ds2.Tables[SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable].Copy();
                    ds.Tables.Add(dt);
                }
                #endregion
                #endregion
                #endregion

                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)))
                {
                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationTemplateEntity[] observationTemplates = this.RebuildTemplates(registeredObservationTemplateAdapter.GetData(ds));
                    this.SetObservations(observationTemplates);
                    return this.OrderedTemplates(observationTemplates);
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

        #region RegisteredObservationTemplateEntity public methods
        /// <summary>
        /// DEVUELVE UNA LISTA DE RegisteredObservationTemplateEntity ORDENADA POR FECHA QUE SE BUSCA POR PACIENTE Y ObservationTemplateID
        /// </summary>
        public RegisteredObservationTemplateEntity[] GetRegisteredObservationTemplateByObservationTemplateID(int customerID, int observationTemplateID)
        {
            try
            {
                if ((customerID <= 0) || (observationTemplateID <= 0)) return null;
                RegisteredObservationTemplateEntity[] myTemplates = this.GetRegisteredObservationTemplatesByCustomer(customerID);
                return ((myTemplates != null) && (myTemplates.Length > 0))
                    ? (from rot in myTemplates where rot.ObservationTemplateID == observationTemplateID select rot).ToArray()
                    : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredObservationTemplateEntity[] GetRegisteredObservationTemplateByProcessStep(int customerEpisodeID, BasicProcessStepsEnum step)
        {
            //// primero busca las plantillas para el paso de proceso ( ObservationTemplateID[] ) que están en los steppreprints
            //// después con esas ObservationTemplateID[] busca cuáles pueden estar relacionadas con el episodio 
            //// ya sea porque están vinculadas a alguna rutina/procedimiento del episodio o porque están relacionadas con algún episodiomedico asociado al episodio

            try
            {
                if (customerEpisodeID <= 0 || step == BasicProcessStepsEnum.None) return null;
                int[] ObservationTemplateIDs = this.GetTemplatesByStepAndProcessChart(step, customerEpisodeID);
                if (ObservationTemplateIDs == null || ObservationTemplateIDs.Length <= 0) return null;
                return this.GetRegisteredObservationTemplatesByCustomer(customerEpisodeID, ObservationTemplateIDs);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredObservationTemplateEntity[] GetConfirmedRoutineActRegisteredObservationTemplateByProcessStep(int customerEpisodeID, BasicProcessStepsEnum step)
        {
            //// primero busca las plantillas para el paso de proceso ( ObservationTemplateID[] ) que están en los steppreprints
            //// después con esas ObservationTemplateID[] busca cuáles pueden estar relacionadas con el episodio 
            //// ya sea porque están vinculadas a alguna rutina/procedimiento del episodio o porque están relacionadas con algún episodiomedico asociado al episodio

            try
            {
                if (customerEpisodeID <= 0) return null;
                int[] ObservationTemplateIDs = this.GetTemplatesByStepAndProcessChart(step, customerEpisodeID);
                if (ObservationTemplateIDs == null || ObservationTemplateIDs.Length <= 0) return null;
                return this.GetConfirmedRoutineActRegisteredObservationTemplatesByCustomer(customerEpisodeID, ObservationTemplateIDs);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredObservationTemplateEntity[] GetCustomerMedEpisodeActRegisteredObservationTemplateByCustomerID(int customerID, DataSet dataset)
        {
            try
            {
                if (customerID <= 0 || dataset == null) return null;

                DataRow[] rows = dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].AsEnumerable()
                            .Where(row => row.Field<int>("CustomerID") == customerID).ToArray();

                if (rows.Length <= 0) return null;

                DataTable dt = rows.CopyToDataTable();
                dt.TableName = SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable;

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                return this.GetRegisteredObservationTemplatesByCustomerTemplateIDs(customerID, ds);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// Confirmed and amended
        /// </summary>
        /// <param name="customerEpisodeID"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public RegisteredObservationTemplateEntity[] GetCustomerMedEpisodeActRegisteredObservationTemplateByProcessStep(int customerEpisodeID, BasicProcessStepsEnum step)
        {
            //// primero busca las plantillas para el paso de proceso ( ObservationTemplateID[] ) que están en los steppreprints
            //// después con esas ObservationTemplateID[] busca cuáles pueden estar relacionadas con el episodio 
            //// ya sea porque están vinculadas a alguna rutina/procedimiento del episodio o porque están relacionadas con algún episodiomedico asociado al episodio

            try
            {
                if (customerEpisodeID <= 0) return null;
                int[] ObservationTemplateIDs = this.GetTemplatesByStepAndProcessChart(step, customerEpisodeID);
                if (ObservationTemplateIDs == null || ObservationTemplateIDs.Length <= 0) return null;
                return this.GetCustomerMedEpisodeActRegisteredObservationTemplatesByEpisode(customerEpisodeID, ObservationTemplateIDs);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredObservationTemplateEntity[] GetConfirmedCustomerMedEpisodeActRegisteredObservationTemplateByProcessStep(int customerEpisodeID, BasicProcessStepsEnum step)
        {
            //// primero busca las plantillas para el paso de proceso ( ObservationTemplateID[] ) que están en los steppreprints
            //// después con esas ObservationTemplateID[] busca cuáles pueden estar relacionadas con el episodio 
            //// ya sea porque están vinculadas a alguna rutina/procedimiento del episodio o porque están relacionadas con algún episodiomedico asociado al episodio

            try
            {
                if (customerEpisodeID <= 0) return null;
                int[] ObservationTemplateIDs = this.GetTemplatesByStepAndProcessChart(step, customerEpisodeID);
                if (ObservationTemplateIDs == null || ObservationTemplateIDs.Length <= 0) return null;
                return this.GetConfirmedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomer(customerEpisodeID, ObservationTemplateIDs);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public bool GetHasConfirmedCustomerMedEpisodeActRegisteredObservationTemplate(int customerEpisodeID)
        {
            /*
             * 
             * Debe recibir por parámetro los ids de template???
             * debe recibir el customerId para no calcularlo
             * 
             * Retornar únicamente true/false si hay o no informe alta/otro
             * 
             * 
             */

            try
            {
                if (customerEpisodeID <= 0) return false;
                //int[] ObservationTemplateIDs = this.GetTemplatesByStepAndProcessChart(step, customerEpisodeID);
                int[] ObservationTemplateIDs = new int[] { 16, 25 };

                DataSet ds = DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesIDByCustomerEpisodeID(customerEpisodeID, ObservationTemplateIDs);

                if (ds == null || ds.Tables == null || ds.Tables[0].Rows.Count <= 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public bool ExistCustomerObservationWithExtValueByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;

                return DataAccess.CustomerObservationDA.ExistCustomerObservationWithExtValueByCustomerEpisodeID(customerEpisodeID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return false;
            }
        }

        public List<int> ExistCustomerObservationWithExtValueByCustomerEpisodeIDs(int[] customerEpisodeID)
        {
            try
            {
                DataSet ds = DataAccess.CustomerObservationDA.ExistCustomerObservationWithExtValueByCustomerEpisodeIDs(customerEpisodeID.Distinct().ToArray());
                List<int> list = new List<int>();

                if (ds != null && ds.Tables != null && ds.Tables[0] != null)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (Convert.ToInt32(row["Count"]) > 0)
                            list.Add(Convert.ToInt32(row["customerEpisodeID"]));
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredObservationTemplateEntity[] GetAmendedCustomerMedEpisodeActRegisteredObservationTemplateByProcessStep(int customerEpisodeID, BasicProcessStepsEnum step)
        {
            //// primero busca las plantillas para el paso de proceso ( ObservationTemplateID[] ) que están en los steppreprints
            //// después con esas ObservationTemplateID[] busca cuáles pueden estar relacionadas con el episodio 
            //// ya sea porque están vinculadas a alguna rutina/procedimiento del episodio o porque están relacionadas con algún episodiomedico asociado al episodio

            try
            {
                if (customerEpisodeID <= 0) return null;

                if (step != BasicProcessStepsEnum.None)
                {
                    int[] ObservationTemplateIDs = this.GetTemplatesByStepAndProcessChart(step, customerEpisodeID);
                    if (ObservationTemplateIDs == null || ObservationTemplateIDs.Length <= 0) return null;
                    return this.GetAmendedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomer(customerEpisodeID, ObservationTemplateIDs);
                }
                else
                {
                    int[] ObservationTemplateIDs = this.GetTemplatesByStepAndProcessChart(step, customerEpisodeID);
                    if (ObservationTemplateIDs == null || ObservationTemplateIDs.Length <= 0) return null;
                    return this.GetAmendedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomer(customerEpisodeID, ObservationTemplateIDs);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredObservationTemplateEntity GetRegisteredObservationTemplateByID(int registeredObservationTemplateID)
        {
            try
            {
                if (registeredObservationTemplateID <= 0) return null;

                DataSet ds = new DataSet();

                #region RegisteredObservationTemplates
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplateByID(registeredObservationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);

                #region RegisteredObservationBlocks
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCOT(new int[] { registeredObservationTemplateID }),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                #region BlockLayouts
                MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                #endregion

                #region RegisteredObservationValues
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerTemplateID(registeredObservationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                #region Observations
                MergeTable(DataAccess.ObservationDA.GetAllObservations(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);

                #region Options
                MergeTable(DataAccess.ObservationOptionDA.GetAll(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationOptionTable);
                #endregion
                #endregion

                #region ObservationValues
                MergeTable(DataAccess.ObservationValueDA.GetObservationValuesByCustomerTemplateID(registeredObservationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                #endregion

                #region ExtObservationValues
                MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValuesByCustomerTemplateID(registeredObservationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
                #endregion
                #endregion
                #endregion
                #endregion

                if ((ds.Tables != null)
                    && (((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable))
                            && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                        || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable))
                            && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                        || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable))
                            && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))))
                {
                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();

                    _observationCache.UpdateCaches();

                    RegisteredObservationTemplateEntity[] observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    if (observationTemplates != null
                        && observationTemplates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in observationTemplates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }

                    return observationTemplates[0];
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

        #region Registered Layout public methods
        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndObservationTemplate(int customerID, int observationTemplateID)
        {
            try
            {
                DataSet ds = new DataSet();

                #region RegisteredObservationTemplate
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplateByCustomerAndObservationTemplate(customerID, observationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
                #endregion

                #region RegisteredObservationValues
                /// Devuelve las customerobservations además de su enlace al customerblockid o al customertemplateid
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndObservationTemplateID(customerID, observationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                #region Observations
                /// Devuelve las observations que indirectamente estrán asociadas a un cliente
                MergeTable(DataAccess.ObservationDA.GetObservationsByCustomerIDAndObservationTemplateID(customerID, observationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);
                #endregion

                #region ObservationValues
                ///// Devuelve las ObservationValues de un cliente
                MergeTable(DataAccess.ObservationValueDA.GetObservationValuesByCustomerAndObservationTemplate(customerID, observationTemplateID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                #endregion
                #endregion

                if ((ds.Tables != null)
                    && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable))
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();
                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();

                    _observationCache.UpdateCaches();

                    RegisteredObservationTemplateEntity[] observationTemplates = null;
                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    registeredLayout.Templates = observationTemplates;

                    if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }

                    return registeredLayout;
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

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomer(int customerID)
        {
            try
            {
                DataSet ds = new DataSet();

                #region RegisteredObservationTemplates
                /// Devuelve los customertemplates de un cliente sin importar su procedencia
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomer(customerID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
                #endregion

                #region RegisteredObservationBlocks
                /// Devuelve los blocks asociados a un cliente sin importar su procedencia
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomer(customerID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                #region BlockLayouts
                /// Devuelve la tabla de BlockLayoutLabel completa
                MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                #endregion

                #region RegisteredObservationValues
                /// Devuelve las customerobservations además de su enlace al customerblockid o al customertemplateid
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomer(customerID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                #region Observations
                /// Devuelve las observations que indirectamente estrán asociadas a un cliente
                MergeTable(DataAccess.ObservationDA.GetObservationsByCustomerID(customerID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);
                #endregion

                #region ObservationValues
                ///// Devuelve las ObservationValues de un cliente
                MergeTable(DataAccess.ObservationValueDA.GetAllObservationValues(customerID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                #endregion

                #region ExtObservationValues
                ///// Devuelve las ExtObservationValues de un cliente
                MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
                #endregion
                #endregion
                #endregion

                if ((ds.Tables != null)
                    && (((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();

                    _observationCache.UpdateCaches();

                    RegisteredObservationTemplateEntity[] observationTemplates = null;
                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    if ((observationTemplates != null) && (observationTemplates.Length > 0))
                    {
                        observationTemplates = this.RebuildTemplates(observationTemplates);
                        this.SetObservations(observationTemplates);
                        registeredLayout.Templates = observationTemplates;
                    }
                    RegisteredObservationBlockEntity[] observationBlocks = null;
                    observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                    if (observationBlocks != null)
                    {
                        RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
                                                                                    where block.CustomerTemplateID == 0
                                                                                    select block).ToArray();
                        if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Length > 0))
                        {
                            onlyObservationBlocks = this.RebuildBlocks(onlyObservationBlocks);
                            this.SetObservations(onlyObservationBlocks);
                            registeredLayout.Blocks = onlyObservationBlocks;
                        }
                    }

                    RegisteredObservationValueEntity[] observations = null;
                    observations = registeredObservationValueAdapter.GetData(ds);
                    if (observations != null)
                    {
                        RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                               where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                               select observation).ToArray();
                        if ((onlyObservations != null) && (onlyObservations.Length > 0))
                        {
                            this.SetObservations(onlyObservations);
                            registeredLayout.Observations = onlyObservations;
                        }
                    }

                    if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }

                    return registeredLayout;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndRoutineAct(int customerID, int routineActID)
        {
            try
            {
                DataSet ds = new DataSet();

                #region RegisteredObservationTemplates
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerAndRoutineAct(customerID, routineActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);

                #region RegisteredObservationBlocks
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerAndRoutineAct(customerID, routineActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                #region BlockLayouts
                MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                #endregion

                #region RegisteredObservationValues
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndRoutineAct(customerID, routineActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                #region Observations
                MergeTable(DataAccess.ObservationDA.GetAllObservations(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);

                #region Options
                MergeTable(DataAccess.ObservationOptionDA.GetAll(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationOptionTable);
                #endregion
                #endregion

                #region ObservationValues
                MergeTable(DataAccess.ObservationValueDA.GetAllObservationValuesAndRoutineAct(customerID, routineActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                #endregion

                #region ExtObservationValues
                MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValuesRoutineAct(customerID, routineActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
                #endregion
                #endregion
                #endregion

                #endregion

                if ((ds.Tables != null)
                    && (((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                    RegisteredObservationTemplateEntity[] observationTemplates = null;

                    _observationCache.UpdateCaches();

                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    if ((observationTemplates != null) && (observationTemplates.Length > 0))
                    {
                        observationTemplates = this.RebuildTemplates(observationTemplates);
                        this.SetObservations(observationTemplates);
                        registeredLayout.Templates = observationTemplates;
                    }
                    RegisteredObservationBlockEntity[] observationBlocks = null;
                    observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                    if (observationBlocks != null)
                    {
                        RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
                                                                                    where block.CustomerTemplateID == 0
                                                                                    select block).ToArray();
                        if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Length > 0))
                        {
                            onlyObservationBlocks = this.RebuildBlocks(onlyObservationBlocks);
                            this.SetObservations(onlyObservationBlocks);
                            registeredLayout.Blocks = onlyObservationBlocks;
                        }
                    }
                    ////aqui tengo que hacer el rebuild de los bloques

                    RegisteredObservationValueEntity[] observations = null;
                    observations = registeredObservationValueAdapter.GetData(ds);
                    if (observations != null)
                    {
                        RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                               where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                               select observation).ToArray();
                        if ((onlyObservations != null) && (onlyObservations.Length > 0))
                        {
                            this.SetObservations(onlyObservations);
                            registeredLayout.Observations = onlyObservations;
                        }
                    }

                    if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }

                    return registeredLayout;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndProcedureAct(int customerID, int procedureActID)
        {
            try
            {
                DataSet ds = new DataSet();

                #region RegisteredObservationTemplates
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerAndProcedureAct(customerID, procedureActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);

                #region RegisteredObservationBlocks
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerAndProcedureAct(customerID, procedureActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                #region BlockLayouts
                MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                #endregion

                #region RegisteredObservationValues
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndProcedureAct(customerID, procedureActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                #region Observations
                MergeTable(DataAccess.ObservationDA.GetAllObservations(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);

                #region Options
                MergeTable(DataAccess.ObservationOptionDA.GetAll(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationOptionTable);
                #endregion
                #endregion

                #region ObservationValues
                MergeTable(DataAccess.ObservationValueDA.GetAllObservationValuesAndProcedureAct(customerID, procedureActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                #endregion

                #region ExtObservationValues
                MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValuesProcedureAct(customerID, procedureActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
                #endregion
                #endregion
                #endregion
                #endregion

                if ((ds.Tables != null)
                    && (((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                    RegisteredObservationTemplateEntity[] observationTemplates = null;

                    _observationCache.UpdateCaches();

                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    if ((observationTemplates != null) && (observationTemplates.Length > 0))
                    {
                        observationTemplates = this.RebuildTemplates(observationTemplates);
                        this.SetObservations(observationTemplates);
                        registeredLayout.Templates = observationTemplates;
                    }
                    RegisteredObservationBlockEntity[] observationBlocks = null;
                    observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                    if (observationBlocks != null)
                    {
                        RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
                                                                                    where block.CustomerTemplateID == 0
                                                                                    select block).ToArray();
                        if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Length > 0))
                        {
                            onlyObservationBlocks = this.RebuildBlocks(onlyObservationBlocks);
                            this.SetObservations(onlyObservationBlocks);
                            registeredLayout.Blocks = onlyObservationBlocks;
                        }
                    }
                    ////aqui tengo que hacer el rebuild de los bloques


                    RegisteredObservationValueEntity[] observations = null;
                    observations = registeredObservationValueAdapter.GetData(ds);
                    if (observations != null)
                    {
                        RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                               where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                               select observation).ToArray();
                        if ((onlyObservations != null) && (onlyObservations.Length > 0))
                        {
                            this.SetObservations(onlyObservations);
                            registeredLayout.Observations = onlyObservations;
                        }
                    }
                    //RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    //RegisteredObservationTemplateAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdapter();
                    //RegisteredObservationBlockAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdapter();
                    //RegisteredObservationValueAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdapter();
                    //RegisteredObservationTemplateEntity[] observationTemplates = null;
                    //observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    //RegisteredObservationBlockEntity[] observationBlocks = null;
                    //observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                    //RegisteredObservationValueEntity[] observations = null;
                    //observations = registeredObservationValueAdapter.GetData(ds);

                    //RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
                    //                                                            where block.CustomerTemplateID == 0
                    //                                                            select block).ToArray();

                    //RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                    //                                                       where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                    //                                                       select observation).ToArray();

                    //if ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                    //    registeredLayout.Templates = observationTemplates;
                    ////if ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                    //if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Count() > 0))
                    //    registeredLayout.Blocks = onlyObservationBlocks;
                    ////if ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))
                    //if ((onlyObservations != null) && (onlyObservations.Count() > 0))
                    //    registeredLayout.Observations = onlyObservations;

                    if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }

                    return registeredLayout;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndOrderRequest(int customerID, int customerOrderRequestID)
        {

            //try
            //{
            //    RegisteredLayoutEntity oldRegisteredLayoutEntity = GetRegisteredLayoutByCustomerAndOrderRequest(customerID, customerOrderRequestID, true);
            //    RegisteredLayoutEntity newRegisteredLayoutEntity = GetRegisteredLayoutByCustomerAndOrderRequest_new(customerID, customerOrderRequestID, true);

            //    bool res = newRegisteredLayoutEntity.CompareEquals(oldRegisteredLayoutEntity);
            //    return newRegisteredLayoutEntity;
            //}
            //catch (Exception ex)
            //{
            //    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
            //    return null;
            //}

            return GetRegisteredLayoutByCustomerAndOrderRequest(customerID, customerOrderRequestID, true);
        }
        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndOrderRequest(int customerID, int customerOrderRequestID, bool refreshCache)
        {
            try
            {
                if (refreshCache) _observationCache.UpdateCaches();

                DataSet ds2 = new DataSet();
                DataSet ds = new DataSet();

                ds2 = DataAccess.CustomerObservationDA.GetRegisteredLayoutByCustomerAndCustomerOrderRequest(customerID, customerOrderRequestID);
                
                foreach (DataTable oTabla in ds2.Tables) if (oTabla.Rows.Count > 0) ds.Tables.Add(oTabla.Copy());

                if ((ds.Tables != null)
                    && (((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                        || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                        || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                    RegisteredObservationTemplateEntity[] observationTemplates = null;

                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    if ((observationTemplates != null) && (observationTemplates.Length > 0))
                    {
                        observationTemplates = this.RebuildTemplates(observationTemplates);
                        this.SetObservations(observationTemplates);
                        registeredLayout.Templates = observationTemplates;
                    }
                    RegisteredObservationBlockEntity[] observationBlocks = null;
                    observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                    if (observationBlocks != null)
                    {
                        RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
                                                                                    where block.CustomerTemplateID == 0
                                                                                    select block).ToArray();
                        if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Length > 0))
                        {
                            onlyObservationBlocks = this.RebuildBlocks(onlyObservationBlocks);
                            this.SetObservations(onlyObservationBlocks);
                            registeredLayout.Blocks = onlyObservationBlocks;
                        }
                    }
                    RegisteredObservationValueEntity[] observations = null;
                    observations = registeredObservationValueAdapter.GetData(ds);
                    if (observations != null)
                    {
                        RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                               where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                               select observation).ToArray();
                        if ((onlyObservations != null) && (onlyObservations.Length > 0))
                        {
                            this.SetObservations(onlyObservations);
                            registeredLayout.Observations = onlyObservations;
                        }
                    }

                    if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }
                    if (registeredLayout != null)
                        registeredLayout.CustomerID = customerID;
                    return registeredLayout;
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
        //public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndOrderRequest_old(int customerID, int customerOrderRequestID, bool refreshCache)
        //{
        //    try
        //    {
        //        if (refreshCache)
        //            _observationCache.UpdateCaches();

        //        DataSet ds = new DataSet();

        //        #region RegisteredObservationTemplates
        //        MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerAndCustomerOrderRequest(customerID, customerOrderRequestID),
        //            ds, BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);

        //        #region RegisteredObservationBlocks
        //        MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerAndCustomerOrderRequest(customerID, customerOrderRequestID),
        //            ds, BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

        //        #region BlockLayouts
        //        MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
        //            ds, BackOffice.Entities.TableNames.BlockLayoutLabelTable);
        //        #endregion

        //        #region RegisteredObservationValues
        //        MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndCustomerOrderRequest(customerID, customerOrderRequestID),
        //            ds, BackOffice.Entities.TableNames.RegisteredObservationValueTable);

        //        #region Observations
        //        MergeTable(DataAccess.ObservationDA.GetAllObservations(),
        //            ds, BackOffice.Entities.TableNames.ObservationTable);

        //        #region Options
        //        MergeTable(DataAccess.ObservationOptionDA.GetAll(),
        //            ds, BackOffice.Entities.TableNames.ObservationOptionTable);
        //        #endregion
        //        #endregion

        //        #region ObservationValues
        //        MergeTable(DataAccess.ObservationValueDA.GetAllObservationValuesAndCustomerOrderRequest(customerID, customerOrderRequestID),
        //            ds, BackOffice.Entities.TableNames.ObservationValueTable);
        //        #endregion

        //        #region ExtObservationValues
        //        MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValuesCustomerOrderRequest(customerID, customerOrderRequestID),
        //            ds, BackOffice.Entities.TableNames.ExtObservationValueTable);
        //        #endregion
        //        #endregion
        //        #endregion

        //        #endregion

        //        if ((ds.Tables != null)
        //            && (((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
        //                || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
        //                || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))))
        //        {
        //            RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

        //            RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
        //            RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
        //            RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
        //            RegisteredObservationTemplateEntity[] observationTemplates = null;                    

        //            observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
        //            if ((observationTemplates != null) && (observationTemplates.Length > 0))
        //            {
        //                observationTemplates = this.RebuildTemplates(observationTemplates);
        //                this.SetObservations(observationTemplates);
        //                registeredLayout.Templates = observationTemplates;
        //            }
        //            RegisteredObservationBlockEntity[] observationBlocks = null;
        //            observationBlocks = registeredObservationBlockAdapter.GetData(ds);
        //            if (observationBlocks != null)
        //            {
        //                RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
        //                                                                            where block.CustomerTemplateID == 0
        //                                                                            select block).ToArray();
        //                if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Length > 0))
        //                {
        //                    onlyObservationBlocks = this.RebuildBlocks(onlyObservationBlocks);
        //                    this.SetObservations(onlyObservationBlocks);
        //                    registeredLayout.Blocks = onlyObservationBlocks;
        //                }
        //            }
        //            RegisteredObservationValueEntity[] observations = null;
        //            observations = registeredObservationValueAdapter.GetData(ds);
        //            if (observations != null)
        //            {
        //                RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
        //                                                                       where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
        //                                                                       select observation).ToArray();
        //                if ((onlyObservations != null) && (onlyObservations.Length > 0))
        //                {
        //                    this.SetObservations(onlyObservations);
        //                    registeredLayout.Observations = onlyObservations;
        //                }
        //            }

        //            if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
        //            {
        //                foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
        //                {
        //                    LoadRegisteredObservationValuesRequired(rot);
        //                }
        //            }
        //            if (registeredLayout != null)
        //                registeredLayout.CustomerID = customerID;
        //            return registeredLayout;
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

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndOrderRealization(int customerID, int orderRealizationID)
        {
            return null;
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndCustomerMedEpisodeAct(int customerID, int customerMedEpisodeActID)
        {
            try
            {
                DataSet ds = new DataSet();

                #region RegisteredObservationTemplates
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerAndCustomerMedEpisodeAct(customerID, customerMedEpisodeActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);

                #region RegisteredObservationBlocks
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerAndCustomerMedEpisodeAct(customerID, customerMedEpisodeActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                #region BlockLayouts

                MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                #endregion

                #region RegisteredObservationValues
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndCustomerMedEpisodeAct(customerID, customerMedEpisodeActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                #region Observations
                MergeTable(DataAccess.ObservationDA.GetAllObservations(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);

                #region Options
                MergeTable(DataAccess.ObservationOptionDA.GetAll(),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationOptionTable);
                #endregion
                #endregion

                #region ObservationValues
                MergeTable(DataAccess.ObservationValueDA.GetAllObservationValuesAndCustomerMedEpisodeAct(customerID, customerMedEpisodeActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                #endregion

                #region ExtObservationValues
                MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValuesCustomerMedEpisodeAct(customerID, customerMedEpisodeActID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
                #endregion
                #endregion
                #endregion
                #endregion

                if ((ds.Tables != null)
                    && (((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                    || ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();

                    _observationCache.UpdateCaches();

                    RegisteredObservationTemplateEntity[] observationTemplates = null;
                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    RegisteredObservationBlockEntity[] observationBlocks = null;
                    observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                    RegisteredObservationValueEntity[] observations = null;
                    observations = registeredObservationValueAdapter.GetData(ds);

                    RegisteredObservationBlockEntity[] onlyObservationBlocks = observationBlocks != null ? (from block in observationBlocks
                                                                                                            where block.CustomerTemplateID == 0
                                                                                                            select block).ToArray() : null;

                    RegisteredObservationValueEntity[] onlyObservations = observationBlocks != null ? (from observation in observations
                                                                                                       where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                                                       select observation).ToArray() : null;

                    if ((ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)) && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                        registeredLayout.Templates = observationTemplates;
                    if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Count() > 0))
                        registeredLayout.Blocks = onlyObservationBlocks;
                    if ((onlyObservations != null) && (onlyObservations.Count() > 0))
                        registeredLayout.Observations = onlyObservations;

                    if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }

                    return registeredLayout;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndCustomerMedicalEpisode(int customerID, int medicalEpisodeID)
        {
            try
            {
                List<int> customerMedEpisodeActs = null;
                CustomerMedEpisodeActBL customerMedEpisodeAct = new CustomerMedEpisodeActBL();
                CustomerMedEpisodeActBaseEntity[] cmea = customerMedEpisodeAct.GetCustomerMedEpisodeActBaseByMedicalEpisode(medicalEpisodeID);
                if ((cmea != null) && (cmea.Length > 0))
                    customerMedEpisodeActs = (from cma in cmea select cma.ID).ToList();
                if ((customerMedEpisodeActs != null) && (customerMedEpisodeActs.Count > 0))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();
                    List<RegisteredObservationValueEntity> observations = new List<RegisteredObservationValueEntity>();
                    List<RegisteredObservationBlockEntity> blocks = new List<RegisteredObservationBlockEntity>();
                    List<RegisteredObservationTemplateEntity> templates = new List<RegisteredObservationTemplateEntity>();

                    _observationCache.UpdateCaches();

                    foreach (int customerMedEpisodeActID in customerMedEpisodeActs)
                    {
                        RegisteredLayoutEntity registeredLayoutByMedAct = new RegisteredLayoutEntity();
                        registeredLayoutByMedAct = GetRegisteredLayoutByCustomerAndCustomerMedEpisodeAct(customerID, customerMedEpisodeActID);
                        if (registeredLayoutByMedAct != null)
                        {
                            if ((registeredLayoutByMedAct.Observations != null) && (registeredLayoutByMedAct.Observations.Length > 0))
                                observations.AddRange(registeredLayoutByMedAct.Observations);
                            if ((registeredLayoutByMedAct.Blocks != null) && (registeredLayoutByMedAct.Blocks.Length > 0))
                                blocks.AddRange(registeredLayoutByMedAct.Blocks);
                            if ((registeredLayoutByMedAct.Templates != null) && (registeredLayoutByMedAct.Templates.Length > 0))
                                templates.AddRange(registeredLayoutByMedAct.Templates);
                        }
                    }
                    registeredLayout.Observations = observations.ToArray();
                    registeredLayout.Blocks = blocks.ToArray();
                    registeredLayout.Templates = templates.ToArray();

                    if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                    {
                        foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                        {
                            LoadRegisteredObservationValuesRequired(rot);
                        }
                    }

                    return registeredLayout;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerAndAct(int customerID, ElementTypeEnum elementType, int entityActID, DateTime? fromDate, DateTime? toDate)
        {
            if ((customerID <= 0) || ((elementType == ElementTypeEnum.None) && (entityActID > 0)) || (entityActID <= 0)) return null;
            try
            {
                RegisteredLayoutEntity rle = null;

                _observationCache.UpdateCaches();

                switch (elementType)
                {
                    case ElementTypeEnum.Routine:
                        rle = GetRegisteredLayoutByCustomerAndRoutineAct(customerID, entityActID);
                        RoutineBL rbl = new RoutineBL();
                        ObservationRelEntity[] routineObs = rbl.GetRoutineObsByAct(entityActID);
                        rle = RebuildRegisteredLayout(GetHeaderDTOByAct(rle, elementType, entityActID), rle, routineObs);
                        break;
                    case ElementTypeEnum.Procedure:
                        rle = GetRegisteredLayoutByCustomerAndProcedureAct(customerID, entityActID);
                        ProcedureBL pbl = new ProcedureBL();
                        ObservationRelEntity[] procedureObs = pbl.GetProcedureObsByAct(entityActID);
                        rle = RebuildRegisteredLayout(GetHeaderDTOByAct(rle, elementType, entityActID), rle, procedureObs);
                        break;
                    case ElementTypeEnum.Protocol:
                        //rle = GetRegisteredLayoutByCustomerAndProtocolAct(customerID, entityActID);
                        break;
                    case ElementTypeEnum.MedEpisode:
                        rle = GetRegisteredLayoutByCustomerAndCustomerMedEpisodeAct(customerID, entityActID);
                        CustomerMedEpisodeActBL cmeabl = new CustomerMedEpisodeActBL();
                        ObservationRelEntity[] customerMedEpisodeActObs = cmeabl.GetCustomerMedEpisodeActObsByAct(entityActID);
                        rle = RebuildRegisteredLayout(GetHeaderDTOByAct(rle, elementType, entityActID), rle, customerMedEpisodeActObs);
                        break;
                    case ElementTypeEnum.RequestOrder:
                        rle = GetRegisteredLayoutByCustomerAndOrderRequest(customerID, entityActID);
                        break;
                    case ElementTypeEnum.OrderRealization:
                        rle = GetRegisteredLayoutByCustomerAndOrderRealization(customerID, entityActID);
                        break;
                }
                ////aqui falta filtrar por las fechas
                return rle;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerReports(int customerID, int customerReportsID)
        {
            //TODO: Pendiente de Optimizar que los DA filtren por "customerReportsID"
            try
            {
                DataSet ds = new DataSet();

                #region RegisteredObservationTemplates
                /// Devuelve los customertemplates de un cliente sin importar su procedencia
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerReports(customerReportsID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
                if ((ds != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)))
                {
                    #region RegisteredObservationBlocks
                    /// Devuelve los blocks asociados a un cliente sin importar su procedencia
                    MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerReports(customerReportsID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                    #region BlockLayouts
                    /// Devuelve la tabla de BlockLayoutLabel completa
                    MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
                        ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                    #endregion

                    #region RegisteredObservationValues
                    /// Devuelve las customerobservations además de su enlace al customerblockid o al customertemplateid
                    MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesToInterviewAndReport(customerID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
                    #region Observations
                    /// Devuelve las observations que indirectamente estrán asociadas a un cliente
                    MergeTable(DataAccess.ObservationDA.GetObservationsByCustomerID(customerID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);
                    #endregion

                    #region ObservationValues
                    ///// Devuelve las ObservationValues de un cliente
                    MergeTable(DataAccess.ObservationValueDA.GetAllObservationValues(customerID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                    #endregion

                    #region ExtObservationValues
                    ///// Devuelve las ExtObservationValues de un cliente
                    MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
                    #endregion
                    #endregion
                    #endregion
                }
                #endregion

                if ((ds.Tables != null) && (ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)
                    && (ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0)))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();

                    _observationCache.UpdateCaches();

                    RegisteredObservationTemplateEntity[] observationTemplates = null;
                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    if ((observationTemplates != null) && (observationTemplates.Length > 0))
                    {
                        observationTemplates = this.RebuildTemplates(observationTemplates);
                        this.SetObservations(observationTemplates);
                        registeredLayout.Templates = observationTemplates;

                        if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                        {
                            foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                            {
                                LoadRegisteredObservationValuesRequired(rot);
                            }
                        }
                    }

                    RegisteredObservationValueEntity[] observations = null;
                    observations = registeredObservationValueAdapter.GetData(ds);
                    RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                           where observation.CustomerObservationEvalTestID != 0//observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                           select observation).ToArray();
                    if ((onlyObservations != null) && (onlyObservations.Length > 0))
                    {
                        this.SetObservations(onlyObservations);
                        registeredLayout.Observations = onlyObservations;
                    }

                    return registeredLayout;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerInterview(int customerID, int customerInterviewID)
        {
            try
            {
                DataSet ds = new DataSet();

                #region RegisteredObservationTemplates
                // Devuelve los customertemplates de un cliente sin importar su procedencia
                MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerInterview(customerInterviewID),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
                if ((ds != null) && ds.Tables.Contains(BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                {
                    #region RegisteredObservationBlocks
                    // Devuelve los blocks asociados a un cliente sin importar su procedencia
                    MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerInterview(customerInterviewID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                    #region BlockLayouts
                    // Devuelve la tabla de BlockLayoutLabel completa
                    MergeTable(DataAccess.BlockLayoutLabelDA.GetAllBlockLayoutLabels(),
                        ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                    #endregion

                    #region RegisteredObservationValues
                    /// Devuelve las customerobservations además de su enlace al customerblockid o al customertemplateid
                    MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerInterviewID(customerInterviewID),
                        ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                    if ((ds != null) && ds.Tables.Contains(BackOffice.Entities.TableNames.RegisteredObservationValueTable)
                        && (ds.Tables[BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))
                    {
                        #region Observations
                        // Devuelve las observations que indirectamente estrán asociadas a un cliente
                        MergeTable(DataAccess.ObservationDA.GetObservationsByCustomerID(customerID),
                            ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);
                        #endregion

                        #region ObservationValues
                        // Devuelve las ObservationValues de un cliente
                        MergeTable(DataAccess.ObservationValueDA.GetAllObservationValues(customerID),
                            ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
                        #endregion

                        #region ExtObservationValues
                        // Devuelve las ExtObservationValues de un cliente
                        MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValues(customerID),
                            ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
                        #endregion
                    }
                    #endregion
                }
                    #endregion
                #endregion

                if ((ds.Tables != null)
                    && (ds.Tables.Contains(BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)
                    && (ds.Tables[BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0)))
                {
                    RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity();

                    RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                    RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();

                    _observationCache.UpdateCaches();

                    RegisteredObservationTemplateEntity[] observationTemplates = null;
                    observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                    if ((observationTemplates != null) && (observationTemplates.Length > 0))
                    {
                        observationTemplates = this.RebuildTemplates(observationTemplates);
                        this.SetObservations(observationTemplates);
                        registeredLayout.Templates = observationTemplates;

                        if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                        {
                            foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                            {
                                LoadRegisteredObservationValuesRequired(rot);
                            }
                        }
                    }

                    RegisteredObservationValueEntity[] observations = null;
                    observations = registeredObservationValueAdapter.GetData(ds);
                    RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                           where observation.CustomerObservationEvalTestID != 0//observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                           select observation).ToArray();
                    if ((onlyObservations != null) && (onlyObservations.Length > 0))
                    {
                        this.SetObservations(onlyObservations);
                        registeredLayout.Observations = onlyObservations;
                    }

                    return registeredLayout;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        public RegisteredLayoutEntity NewRoutineRegisteredLayout(RealizationTypeEnum typeRealization, int entityActID, RoutineEntity entity,
            int customerID, int episodeID, string episodeNumber, CommonEntities.StatusEnum episodeStatus, DateTime? episodeDateTime)
        {
            return NewProcedureOrRoutineRegisteredLayout(typeRealization, entityActID, entity, customerID, episodeID, episodeNumber, episodeStatus, episodeDateTime);
        }

        public RegisteredLayoutEntity NewProcedureRegisteredLayout(RealizationTypeEnum typeRealization, int entityActID, ProcedureEntity entity,
            int customerID, int episodeID, string episodeNumber, CommonEntities.StatusEnum episodeStatus, DateTime? episodeDateTime)
        {
            return NewProcedureOrRoutineRegisteredLayout(typeRealization, entityActID, entity, customerID, episodeID, episodeNumber, episodeStatus, episodeDateTime);
        }
        #endregion












        #region registeredlayouts by TVP Methods

        #region privates with TVP
        private void SetBlockLabelsByBlockIDs(DataSet ds)
        {
            if (ds == null || ds.Tables == null || !ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable) ||
                ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count <= 0) return;

            int[] ids = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable].AsEnumerable()
                            .Where(row => (row["ObservationBlockID"] as int? ?? 0) > 0)
                            .Select(row => (row["ObservationBlockID"] as int? ?? 0))
                            .OrderBy(id => id)
                            .Distinct()
                            .ToArray();

            #region BlockLayouts
            MergeTable(DataAccess.BlockLayoutLabelDA.GetBlockLayoutLabelsByIDs(ids),
                ds, SII.HCD.BackOffice.Entities.TableNames.BlockLayoutLabelTable);
            #endregion
        }

        private void SetObservationsByIDs(DataSet ds)
        {
            if (ds == null || ds.Tables == null || !ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable) ||
                ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count <= 0) return;

            int[] ids = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].AsEnumerable()
                        .Where(row => (row["ObservationID"] as int? ?? 0) > 0)
                        .Select(row => (row["ObservationID"] as int? ?? 0))
                        .OrderBy(id => id)
                        .Distinct()
                        .ToArray();

            #region Observations
            MergeTable(DataAccess.ObservationDA.GetObservationByIDs(ids),
                ds, SII.HCD.BackOffice.Entities.TableNames.ObservationTable);
            #endregion

            #region Options
            MergeTable(DataAccess.ObservationOptionDA.GetObservationOptionByIDs(ids),
                ds, SII.HCD.BackOffice.Entities.TableNames.ObservationOptionTable);
            #endregion

            ids = ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].AsEnumerable()
            .Where(row => (row["CustomerObservationID"] as int? ?? 0) > 0)
            .Select(row => (row["CustomerObservationID"] as int? ?? 0))
            .OrderBy(id => id)
            .Distinct()
            .ToArray();


            #region ObservationValues
            MergeTable(DataAccess.ObservationValueDA.GetAllObservationValuesByIDs(ids),
                ds, SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable);
            #endregion

            #region ExtObservationValues
            MergeTable(DataAccess.ExtObservationValueDA.GetAllExtObservationValuesByIDs(ids),
                ds, SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable);
            #endregion

        }

        private void SetRegisteredObservationsByCustomerOrderRequestIDs(DataSet ds, int[] customerOrderRequestIDs)
        {
            if (customerOrderRequestIDs == null || customerOrderRequestIDs.Length <= 0 || ds == null) return;
            DataSet ds1 = DataAccess.RoutineActDA.GetByIDsCustomerOrderRequestIDs(customerOrderRequestIDs);
            int[] ractIDs = ds1.Tables[Assistance.Entities.TableNames.RoutineActTable].AsEnumerable()
                .Where(row => (row["ID"] as int? ?? 0) > 0)
                .Select(row => (row["ID"] as int? ?? 0))
                .OrderBy(id => id)
                .Distinct()
                .ToArray();

            if (ractIDs != null && ractIDs.Length > 0)
            {
                SetRegisteredObservationsByRoutineActsIDs(ds, ractIDs);
            }
            ds1 = DataAccess.ProcedureActDA.GetByIDsCustomerOrderRequestIDs(customerOrderRequestIDs);
            int[] pactIDs = ds1.Tables[Assistance.Entities.TableNames.ProcedureActTable].AsEnumerable()
                .Where(row => (row["ID"] as int? ?? 0) > 0)
                .Select(row => (row["ID"] as int? ?? 0))
                .OrderBy(id => id)
                .Distinct()
                .ToArray();

            if (pactIDs != null && pactIDs.Length > 0)
            {
                SetRegisteredObservationsByProcedureActsIDs(ds, pactIDs);
            }
        }

        private void SetRegisteredObservationsByRoutineActsIDs(DataSet ds, int[] routineActIDs)
        {
            if (routineActIDs == null || routineActIDs.Length <= 0 || ds == null) return;
            #region RegisteredObservationTemplates
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerAndRoutineActIDs(routineActIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
            #endregion

            #region RegisteredObservationBlocks
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerAndRoutineActIDs(routineActIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);
            SetBlockLabelsByBlockIDs(ds);
            #endregion

            #region RegisteredObservationValues
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndRoutineActIDs(routineActIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
            SetObservationsByIDs(ds);
            #endregion
        }

        private void SetRegisteredObservationsByProcedureActsIDs(DataSet ds, int[] procedureActIDs)
        {
            if (procedureActIDs == null || procedureActIDs.Length <= 0 || ds == null) return;
            #region RegisteredObservationTemplates
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerAndProcedureActIDs(procedureActIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
            #endregion

            #region RegisteredObservationBlocks
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerAndProcedureActIDs(procedureActIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);
            SetBlockLabelsByBlockIDs(ds);
            #endregion

            #region RegisteredObservationValues
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerAndProcedureActIDs(procedureActIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
            SetObservationsByIDs(ds);
            #endregion
        }

        private void SetRegisteredObservationsByMedicalEpìsodeIDs(DataSet ds, int[] medicalEpisodeIDs, bool includeOrderObs)
        {
            if (medicalEpisodeIDs == null || medicalEpisodeIDs.Length <= 0 || ds == null) return;
            #region RegisteredObservationTemplates
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByMedicalEpisodeIDs(medicalEpisodeIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
            #endregion

            #region RegisteredObservationBlocks
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByMedicalEpisodeIDs(medicalEpisodeIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);
            SetBlockLabelsByBlockIDs(ds);
            #endregion

            #region RegisteredObservationValues
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByMedicalEpisodeIDs(medicalEpisodeIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
            SetObservationsByIDs(ds);
            #endregion

        }

        private void SetRegisteredObservationsByCustomerEpìsodeIDs(DataSet ds, int[] customerEpisodeIDs, bool includeOrderObs)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0 || ds == null) return;
            #region RegisteredObservationTemplates
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerEpisodeIDs(customerEpisodeIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
            #endregion

            #region RegisteredObservationBlocks
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerEpisodeIDs(customerEpisodeIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);
            SetBlockLabelsByBlockIDs(ds);
            #endregion

            #region RegisteredObservationValues
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerEpisodeIDs(customerEpisodeIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
            SetObservationsByIDs(ds);
            #endregion
        }

        private void SetRegisteredObservationsByCustomerProcessIDs(DataSet ds, int[] customerProcessIDs)
        {
            if (customerProcessIDs == null || customerProcessIDs.Length <= 0 || ds == null) return;
            #region RegisteredObservationTemplates
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomerProcessIDs(customerProcessIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
            #endregion

            #region RegisteredObservationBlocks
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomerProcessIDs(customerProcessIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);
            SetBlockLabelsByBlockIDs(ds);
            #endregion

            #region RegisteredObservationValues
            MergeTable(DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomerProcessIDs(customerProcessIDs),
                ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
            SetObservationsByIDs(ds);
            #endregion
        }

        private RegisteredLayoutEntity ConvertToRLE(DataSet ds)
        {
            if (ds != null && ds.Tables != null &&
                ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable) &&
                ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0)
            {
                RegisteredLayoutEntity registeredLayout = null;

                RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                RegisteredObservationTemplateEntity[] observationTemplates = null;

                _observationCache.UpdateCaches();

                observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                if ((observationTemplates != null) && (observationTemplates.Length > 0))
                {
                    observationTemplates = this.RebuildTemplates(observationTemplates);
                    this.SetObservations(observationTemplates);
                    if (registeredLayout == null) registeredLayout = new RegisteredLayoutEntity(observationTemplates[0].GetCustomerID(), null, null, null);
                    registeredLayout.Templates = observationTemplates;
                }
                RegisteredObservationBlockEntity[] observationBlocks = null;
                observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                if (observationBlocks != null)
                {
                    RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
                                                                                where block.CustomerTemplateID == 0
                                                                                select block).ToArray();
                    if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Length > 0))
                    {
                        onlyObservationBlocks = this.RebuildBlocks(onlyObservationBlocks);
                        this.SetObservations(onlyObservationBlocks);
                        if (registeredLayout == null) registeredLayout = new RegisteredLayoutEntity(onlyObservationBlocks[0].GetCustomerID(), null, null, null);
                        registeredLayout.Blocks = onlyObservationBlocks;
                    }
                }
                ////aqui tengo que hacer el rebuild de los bloques

                RegisteredObservationValueEntity[] observations = null;
                observations = registeredObservationValueAdapter.GetData(ds);
                if (observations != null)
                {
                    RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                           where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                           select observation).ToArray();
                    if ((onlyObservations != null) && (onlyObservations.Length > 0))
                    {
                        this.SetObservations(onlyObservations);
                        if (registeredLayout == null) registeredLayout = new RegisteredLayoutEntity(onlyObservations[0].CustomerID, null, null, null);
                        registeredLayout.Observations = onlyObservations;
                    }
                }
                return registeredLayout;
            }
            else return null;
        }

        private RegisteredLayoutEntity[] ConvertToMultipleRLE(DataSet ds)
        {
            if (ds.Tables != null &&
                ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable) &&
                ds.Tables[SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0)
            {
                List<RegisteredLayoutEntity> registeredLayoutList = new List<RegisteredLayoutEntity>();

                RegisteredObservationTemplateAdvancedAdapter registeredObservationTemplateAdapter = new RegisteredObservationTemplateAdvancedAdapter();
                RegisteredObservationBlockAdvancedAdapter registeredObservationBlockAdapter = new RegisteredObservationBlockAdvancedAdapter();
                RegisteredObservationValueAdvancedAdapter registeredObservationValueAdapter = new RegisteredObservationValueAdvancedAdapter();
                RegisteredObservationTemplateEntity[] observationTemplates = null;

                _observationCache.UpdateCaches();

                observationTemplates = registeredObservationTemplateAdapter.GetData(ds);
                if ((observationTemplates != null) && (observationTemplates.Length > 0))
                {
                    observationTemplates = this.RebuildTemplates(observationTemplates);
                    this.SetObservations(observationTemplates);

                    foreach (RegisteredObservationTemplateEntity rot in observationTemplates)
                    {
                        if (registeredLayoutList.Count <= 0 ||
                            !registeredLayoutList.Any(rl => rl.GetRelatedAct().Item1 == rot.GetRelatedAct().Item1 && rl.GetRelatedAct().Item2 == rot.GetRelatedAct().Item2))
                        {
                            RegisteredLayoutEntity rle = new RegisteredLayoutEntity(rot.GetCustomerID(), null, null, new RegisteredObservationTemplateEntity[] { rot });
                            registeredLayoutList.Add(rle);
                        }
                        else
                        {
                            RegisteredLayoutEntity rle = registeredLayoutList.Where(rl => rl.GetRelatedAct().Item1 == rot.GetRelatedAct().Item1 &&
                                        rl.GetRelatedAct().Item2 == rot.GetRelatedAct().Item2).FirstOrDefault();
                            if (rle != null)
                            {
                                List<RegisteredObservationTemplateEntity> rots = new List<RegisteredObservationTemplateEntity>();
                                if (rle.Templates != null && rle.Templates.Length > 0)
                                    rots.AddRange(rle.Templates);
                                rots.Add(rot);
                                rle.Templates = rots.ToArray();
                            }
                        }
                    }
                }
                RegisteredObservationBlockEntity[] observationBlocks = null;
                observationBlocks = registeredObservationBlockAdapter.GetData(ds);
                if (observationBlocks != null)
                {
                    RegisteredObservationBlockEntity[] onlyObservationBlocks = (from block in observationBlocks
                                                                                where block.CustomerTemplateID == 0
                                                                                select block).ToArray();
                    if ((onlyObservationBlocks != null) && (onlyObservationBlocks.Length > 0))
                    {
                        onlyObservationBlocks = this.RebuildBlocks(onlyObservationBlocks);
                        this.SetObservations(onlyObservationBlocks);
                        foreach (RegisteredObservationBlockEntity rob in onlyObservationBlocks)
                        {
                            if (registeredLayoutList.Count <= 0 ||
                                !registeredLayoutList.Any(rl => rl.GetRelatedAct().Item1 == rob.GetRelatedAct().Item1 && rl.GetRelatedAct().Item2 == rob.GetRelatedAct().Item2))
                            {
                                RegisteredLayoutEntity rle = new RegisteredLayoutEntity(rob.GetCustomerID(), null, new RegisteredObservationBlockEntity[] { rob }, null);
                                registeredLayoutList.Add(rle);
                            }
                            else
                            {
                                RegisteredLayoutEntity rle = registeredLayoutList.Where(rl => rl.GetRelatedAct().Item1 == rob.GetRelatedAct().Item1 &&
                                            rl.GetRelatedAct().Item2 == rob.GetRelatedAct().Item2).FirstOrDefault();
                                if (rle != null)
                                {
                                    List<RegisteredObservationBlockEntity> robs = new List<RegisteredObservationBlockEntity>();
                                    if (rle.Blocks != null && rle.Blocks.Length > 0)
                                        robs.AddRange(rle.Blocks);
                                    robs.Add(rob);
                                    rle.Blocks = robs.ToArray();
                                }
                            }
                        }
                    }
                }
                ////aqui tengo que hacer el rebuild de los bloques

                RegisteredObservationValueEntity[] observations = null;
                observations = registeredObservationValueAdapter.GetData(ds);
                if (observations != null)
                {
                    RegisteredObservationValueEntity[] onlyObservations = (from observation in observations
                                                                           where observation.CustomerBlockID == 0 && observation.CustomerTemplateID == 0
                                                                           select observation).ToArray();
                    if ((onlyObservations != null) && (onlyObservations.Length > 0))
                    {
                        this.SetObservations(onlyObservations);
                        foreach (RegisteredObservationValueEntity rov in onlyObservations)
                        {
                            if (registeredLayoutList.Count <= 0 ||
                                !registeredLayoutList.Any(rl => rl.GetRelatedAct().Item1 == rov.EntityType && rl.GetRelatedAct().Item2 == rov.EntityActID))
                            {
                                RegisteredLayoutEntity rle = new RegisteredLayoutEntity(rov.CustomerID, new RegisteredObservationValueEntity[] { rov }, null, null);
                                registeredLayoutList.Add(rle);
                            }
                            else
                            {
                                RegisteredLayoutEntity rle = registeredLayoutList.Where(rl => rl.GetRelatedAct().Item1 == rov.EntityType && rl.GetRelatedAct().Item2 == rov.EntityActID).FirstOrDefault();
                                if (rle != null)
                                {
                                    List<RegisteredObservationValueEntity> rovs = new List<RegisteredObservationValueEntity>();
                                    if (rle.Observations != null && rle.Observations.Length > 0)
                                        rovs.AddRange(rle.Observations);
                                    rovs.Add(rov);
                                    rle.Observations = rovs.ToArray();
                                }
                            }
                        }
                    }
                }
                return registeredLayoutList.Count > 0
                    ? registeredLayoutList.ToArray()
                    : null;
            }
            else return null;

        }
        #endregion

        #region publics with TVP

        /// <summary>
        /// este servicio carga todas las observaciones de las rutinas 
        /// incluidas las observaciones de los infomres de órdenes médicas 
        /// </summary>
        public RegisteredLayoutEntity[] GetRegisteredLayoutByCustomerAndRoutineActIDs(int[] routineActIDs)
        {
            try
            {
                if (routineActIDs == null || routineActIDs.Length <= 0) return null;

                DataSet ds = new DataSet();

                SetRegisteredObservationsByRoutineActsIDs(ds, routineActIDs);

                #region ConvertToMultipleRLE
                return ConvertToMultipleRLE(ds);
                #endregion
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// este servicio carga todas las observaciones de los procedimientos 
        /// incluidas las observaciones de los infomres de órdenes médicas 
        /// </summary>
        public RegisteredLayoutEntity[] GetRegisteredLayoutByCustomerAndProcedureActIDs(int[] procedureActIDs)
        {
            try
            {
                if (procedureActIDs == null || procedureActIDs.Length <= 0) return null;

                DataSet ds = new DataSet();

                SetRegisteredObservationsByProcedureActsIDs(ds, procedureActIDs);

                #region ConvertToMultipleRLE
                return ConvertToMultipleRLE(ds);
                #endregion

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// este servicio carga todas las observaciones de episodio médico
        /// mas todos los episodios relacionados sean médicos, administrativos o relativas a los procesos si allepisodes = true
        /// carga las observaciones de las órdenes médicas si includeOrderObs = true
        /// </summary>
        public RegisteredLayoutEntity GetRegisteredLayoutByMedicalEpisodeID(int medicalEpisodeID, bool allepisodes, bool includeOrderObs)
        {
            try
            {
                if (medicalEpisodeID <= 0) return null;
                DataSet ds = DataAccess.MedicalEpisodeDA.GetAllRelatedMedicalEpisodeIDs(medicalEpisodeID);
                if (ds != null && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.MedicalEpisodeTable) && ds.Tables[SII.HCD.Administrative.Entities.TableNames.MedicalEpisodeTable].Rows.Count > 0)
                {
                    int[] relatedMedicalEpisodeIDs = ds.Tables[SII.HCD.Administrative.Entities.TableNames.MedicalEpisodeTable].AsEnumerable()
                        .Where(row => (row["MedicalEpisodeID"] as int? ?? 0) > 0)
                        .Select(row => (row["MedicalEpisodeID"] as int? ?? 0))
                        .OrderBy(id => id)
                        .Distinct()
                        .ToArray();
                    if (relatedMedicalEpisodeIDs == null || relatedMedicalEpisodeIDs.Length <= 0) return null;
                    return GetRegisteredLayoutByMedicalEpisodeIDs(relatedMedicalEpisodeIDs, allepisodes, includeOrderObs);
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
        /// este servicio carga todas las observaciones de episodio administrativos
        /// mas todos los episodios relacionados administrativos o relativas a los procesos si allepisodes = true
        /// carga las observaciones de las órdenes médicas si includeOrderObs = true
        /// </summary>
        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerEpisodeID(int customerEpisodeID, bool allepisodes, bool includeOrderObs)
        {
            try
            {
                if (customerEpisodeID <= 0) return null;
                DataSet ds = DataAccess.CustomerEpisodeDA.GetAllRelatedCustomerEpisodeIDs(customerEpisodeID);
                if (ds != null && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable) && ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0)
                {
                    int[] relatedCustomerEpisodeIDs = ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].AsEnumerable()
                        .Where(row => (row["CustomerEpisodeID"] as int? ?? 0) > 0)
                        .Select(row => (row["CustomerEpisodeID"] as int? ?? 0))
                        .OrderBy(id => id)
                        .Distinct()
                        .ToArray();
                    if (relatedCustomerEpisodeIDs == null || relatedCustomerEpisodeIDs.Length <= 0) return null;
                    return GetRegisteredLayoutByCustomerEpisodeIDs(relatedCustomerEpisodeIDs, allepisodes, includeOrderObs);
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
        /// este servicio carga todas las observaciones de los episodios médicos
        /// mas todos los episodios relacionados administrativos o relativas a los procesos si allepisodes = true
        /// carga las observaciones de las órdenes médicas si includeOrderObs = true
        /// </summary>
        public RegisteredLayoutEntity GetRegisteredLayoutByMedicalEpisodeIDs(int[] medicalEpisodeIDs, bool allepisodes, bool includeOrderObs)
        {
            try
            {
                if (medicalEpisodeIDs == null || medicalEpisodeIDs.Length <= 0) return null;
                DataSet ds = new DataSet();
                SetRegisteredObservationsByMedicalEpìsodeIDs(ds, medicalEpisodeIDs, includeOrderObs);
                #region allepisodes
                if (allepisodes)
                {
                    DataSet ds1 = DataAccess.CustomerEpisodeDA.GetAllRelatedCustomerEpisodeIDsByMedicalEpisodeIDs(medicalEpisodeIDs);
                    if (ds1 != null && ds1.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable) &&
                        ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].Rows.Count > 0)
                    {
                        int[] relatedCustomerEpisodeIDs = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable].AsEnumerable()
                            .Where(row => (row["CustomerEpisodeID"] as int? ?? 0) > 0)
                            .Select(row => (row["CustomerEpisodeID"] as int? ?? 0))
                            .OrderBy(id => id)
                            .Distinct()
                            .ToArray();
                        if (relatedCustomerEpisodeIDs != null && relatedCustomerEpisodeIDs.Length > 0)
                            SetRegisteredObservationsByCustomerEpìsodeIDs(ds, relatedCustomerEpisodeIDs, includeOrderObs);
                    }
                    ds1 = DataAccess.CustomerProcessDA.GetAllRelatedCustomerProcessIDsByMedicalEpisodeIDs(medicalEpisodeIDs);
                    if (ds1 != null && ds1.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcessTable)
                        && ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcessTable].Rows.Count > 0)
                    {
                        int[] relatedCustomerProcessIDs = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcessTable].AsEnumerable()
                            .Where(row => (row["CustomerProcessID"] as int? ?? 0) > 0)
                            .Select(row => (row["CustomerProcessID"] as int? ?? 0))
                            .OrderBy(id => id)
                            .Distinct()
                            .ToArray();
                        if (relatedCustomerProcessIDs != null && relatedCustomerProcessIDs.Length > 0)
                            SetRegisteredObservationsByCustomerProcessIDs(ds, relatedCustomerProcessIDs);
                    }
                }
                #endregion

                #region ConvertToRLE
                return ConvertToRLE(ds);
                #endregion
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// este servicio carga todas las observaciones de los episodios administrativos 
        /// relativas a los procesos si allepisodes = true
        /// carga las observaciones de las órdenes médicas si includeOrderObs = true
        /// </summary>
        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerEpisodeIDs(int[] customerEpisodeIDs, bool allepisodes, bool includeOrderObs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                DataSet ds = new DataSet();
                SetRegisteredObservationsByCustomerEpìsodeIDs(ds, customerEpisodeIDs, includeOrderObs);
                #region allepisodes
                if (allepisodes)
                {
                    DataSet ds1 = DataAccess.CustomerProcessDA.GetAllRelatedCustomerProcessIDsByCustomerEpisodeIDs(customerEpisodeIDs);
                    if (ds1 != null && ds1.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerProcessTable)
                        && ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcessTable].Rows.Count > 0)
                    {
                        int[] relatedCustomerProcessIDs = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerProcessTable].AsEnumerable()
                            .Where(row => (row["CustomerProcessID"] as int? ?? 0) > 0)
                            .Select(row => (row["CustomerProcessID"] as int? ?? 0))
                            .OrderBy(id => id)
                            .Distinct()
                            .ToArray();
                        if (relatedCustomerProcessIDs != null && relatedCustomerProcessIDs.Length > 0)
                            SetRegisteredObservationsByCustomerProcessIDs(ds, relatedCustomerProcessIDs);
                    }
                }
                if (includeOrderObs)
                {
                    DataSet ds1 = DataAccess.CustomerOrderRequestDA.GetAllRelatedCustomerOrderRequestIDsByCustomerEpisodeIDs(customerEpisodeIDs);
                    if (ds1 != null && ds1.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable)
                        && ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0)
                    {
                        int[] relatedCustomerOrderRequestIDs = ds1.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable].AsEnumerable()
                            .Where(row => (row["CustomerOrderRequestID"] as int? ?? 0) > 0)
                            .Select(row => (row["CustomerOrderRequestID"] as int? ?? 0))
                            .OrderBy(id => id)
                            .Distinct()
                            .ToArray();
                        if (relatedCustomerOrderRequestIDs != null && relatedCustomerOrderRequestIDs.Length > 0)
                            SetRegisteredObservationsByCustomerOrderRequestIDs(ds, relatedCustomerOrderRequestIDs);

                    }
                }
                #endregion
                #region ConvertToRLE
                return ConvertToRLE(ds);
                #endregion
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

        /// <summary>
        /// este servicio carga todas las observaciones de los procesos 
        /// estas son: 
        /// Las plantillas de las valoraciones
        /// Las palntillas de las entrevistas
        /// </summary>
        public RegisteredLayoutEntity GetRegisteredLayoutByCustomerProcessIDs(int[] customerProcessIDs)
        {
            try
            {
                if (customerProcessIDs == null || customerProcessIDs.Length <= 0) return null;
                DataSet ds = new DataSet();
                SetRegisteredObservationsByCustomerProcessIDs(ds, customerProcessIDs);
                #region ConvertToRLE
                return ConvertToRLE(ds);
                #endregion
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        #endregion
        #endregion







        #region Virtual Registered entities private methods
        private RegisteredObservationHeaderDTO GetHeaderDTOByAct(ElementTypeEnum elementType, int entityActID)
        {
            if (((elementType == ElementTypeEnum.None) && (entityActID > 0)) || (entityActID <= 0)) return null;
            RegisteredObservationHeaderDTO header = null;
            switch (elementType)
            {
                case ElementTypeEnum.Routine:
                    RoutineBL routineBL = new RoutineBL();
                    return routineBL.GetHeadetDTOByAct(entityActID);
                case ElementTypeEnum.Procedure:
                    ProcedureBL procedureBL = new ProcedureBL();
                    return procedureBL.GetHeadetDTOByAct(entityActID);
                case ElementTypeEnum.Protocol:
                    break;
                case ElementTypeEnum.MedEpisode:
                    CustomerMedEpisodeActBL customerMedEpisodeActBL = new CustomerMedEpisodeActBL();
                    return customerMedEpisodeActBL.GetHeadetDTOByAct(entityActID);
                case ElementTypeEnum.RequestOrder:
                    break;
                case ElementTypeEnum.OrderRealization:
                    break;
            }
            return header;
        }

        private RegisteredObservationHeaderDTO GetHeaderDTOByAct(RegisteredLayoutEntity rle, ElementTypeEnum elementType, int entityActID)
        {
            RegisteredObservationHeaderDTO header = null;
            if ((rle != null)
                && (((rle.Observations != null) && (rle.Observations.Length > 0))
                    || ((rle.Blocks != null) && (rle.Blocks.Length > 0))
                    || ((rle.Templates != null) && (rle.Templates.Length > 0))))
            {
                RegisteredObservationValueEntity rov = GetFirstRegisteredObservationValue(rle);
                if (rov == null)
                {
                    header = GetHeaderDTOByAct(elementType, entityActID);
                }
                else
                {
                    header = new RegisteredObservationHeaderDTO(rov.CustomerID, rov.EpisodeID, rov.EpisodeNumber, rov.EpisodeType,
                                rov.EpisodeStatus, rov.EpisodeDateTime, rov.EntityType, entityActID, rov.EntityActName,
                                rov.EntityID, rov.EntityName, 0, SpecialCategoryTypeEnum.None, -1, -1, -1, 0, 0, 0);
                }
            }
            else
            {
                header = GetHeaderDTOByAct(elementType, entityActID);
            }
            return header;
        }

        private RegisteredLayoutEntity RebuildRegisteredLayout(RegisteredObservationHeaderDTO headerDTO, RegisteredLayoutEntity rle, ObservationRelEntity[] obsRels)
        {
            if ((obsRels == null) || (obsRels.Length <= 0))
                return rle;

            if (rle == null) rle = new RegisteredLayoutEntity();
            List<RegisteredObservationValueEntity> lrovs = new List<RegisteredObservationValueEntity>();
            if ((rle != null) && (rle.Observations != null) && (rle.Observations.Length > 0)) lrovs.AddRange(rle.Observations);
            List<RegisteredObservationBlockEntity> lrobs = new List<RegisteredObservationBlockEntity>();
            if ((rle != null) && (rle.Blocks != null) && (rle.Blocks.Length > 0)) lrobs.AddRange(rle.Blocks);
            List<RegisteredObservationTemplateEntity> lrots = new List<RegisteredObservationTemplateEntity>();
            if ((rle != null) && (rle.Templates != null) && (rle.Templates.Length > 0)) lrots.AddRange(rle.Templates);

            rle.Templates = (lrots.Count > 0) ? lrots.ToArray() : null;
            rle.Blocks = (lrobs.Count > 0) ? lrobs.ToArray() : null;
            rle.Observations = (lrovs.Count > 0) ? lrovs.ToArray() : null;
            return rle;
        }
        #endregion

        #region Virtual Registered Layouts By Realization
        public RegisteredLayoutEntity NewProcedureOrRoutineRegisteredLayout(RealizationTypeEnum typeRealization, int entityActID, object entity,
            int customerID, int episodeID, string episodeNumber, CommonEntities.StatusEnum episodeStatus, DateTime? episodeDateTime)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (episodeID < 0)
                throw new ArgumentNullException("episodeID");

            if (customerID < 0)
                throw new ArgumentNullException("customerID");

            int entityID = 0;
            string entityActName = string.Empty;
            string entityName = string.Empty;
            List<ObservationRelEntity> orels = new List<ObservationRelEntity>();

            switch (typeRealization)
            {
                case RealizationTypeEnum.Routine:
                    RoutineEntity re = (RoutineEntity)entity;
                    entityID = re.ID;
                    entityName = re.Name;
                    if ((re.Observations != null) && (re.Observations.Length > 0))
                    {
                        foreach (ObservationRelEntity ore in re.Observations)
                        {
                            if (ore != null) orels.Add(ore);
                        }
                    }
                    break;
                case RealizationTypeEnum.Procedure:
                    ProcedureEntity pe = (ProcedureEntity)entity;
                    entityID = pe.ID;
                    entityName = pe.Name;
                    if ((pe.Observations != null) && (pe.Observations.Length > 0))
                    {
                        foreach (ObservationRelEntity ore in pe.Observations)
                        {
                            if (ore != null) orels.Add(ore);
                        }
                    }
                    break;
                default: throw new ArgumentNullException("typeRealization");
            }

            if ((orels != null) && (orels.Count > 0))
            {
                IObservationCacheService _masterObservationCache = IoCFactory.CurrentContainer
                    .Resolve<IObservationCacheService>();
                _masterObservationCache.UpdateCaches();

                int virtualObsID = -1;
                int virtualBlockID = -1;
                int virtualTemplateID = -1;
                List<RegisteredObservationTemplateEntity> lrot = new List<RegisteredObservationTemplateEntity>();
                List<RegisteredObservationBlockEntity> lrob = new List<RegisteredObservationBlockEntity>();
                List<RegisteredObservationValueEntity> lrov = new List<RegisteredObservationValueEntity>();

                foreach (ObservationRelEntity ore in orels)
                {
                    if ((ore.ElementType == CHElementTypeEnum.ObservationTemplate) && (ore.Status == CommonEntities.StatusEnum.Active))
                    {
                        List<RegisteredObservationValueEntity> ltrov = new List<RegisteredObservationValueEntity>();
                        List<RegisteredObservationBlockEntity> ltrob = new List<RegisteredObservationBlockEntity>();

                        ObservationTemplateEntity ote = _masterObservationCache.ObservationTemplateCache.Get(ore.ElementID, false);
                        foreach (ObservationTemplateRelEntity obt in ote.Blocks)
                        {
                            if ((obt.ElementType == TemplateElementTypeEnum.ObservationBlock) && (obt.Status == CommonEntities.StatusEnum.Active))
                            {
                                List<RegisteredObservationValueEntity> lsecondaryrov = new List<RegisteredObservationValueEntity>();
                                foreach (ObservationBlockRelEntity obs in obt.ObservationBlock.Observations)
                                {
                                    if (obs.Status == CommonEntities.StatusEnum.Active)
                                    {
                                        ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                                        ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                                        RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance,
                                            episodeStatus, episodeDateTime, (typeRealization == RealizationTypeEnum.Routine) ? ElementTypeEnum.Routine : ElementTypeEnum.Procedure,
                                            entityActID, entityActName, null, entityID, entityName, virtualTemplateID, virtualBlockID, virtualObsID, obs.LabelName,
                                            obs.VisibleLabel, obt.ObservationBlock.BlockLabelPosition, obs.Order, obt.Required, obs.Observation.ID, obs.Observation, ove, eove, 0, DateTime.Now,
                                            SpecialCategoryTypeEnum.None, ObservationStatusEnum.None, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                                        lsecondaryrov.Add(rov);
                                        virtualObsID--;
                                    }
                                }
                                RegisteredObservationBlockEntity rob = new RegisteredObservationBlockEntity(0, virtualBlockID, obt.ObservationBlock.ID,
                                    obt.ObservationBlock.Name, obt.ObservationBlock.LabelTitle, obt.ObservationBlock.BlockLayout, obt.Order, obt.ElementTitle,
                                    obt.ElementTitlePosition, obt.VisibleLabel, obt.ObservationBlock.BlockLayoutItems, obt.ObservationBlock.BlockLabelPosition,
                                    obt.ObservationBlock.BlockLayoutLabels, lsecondaryrov.ToArray(), DateTime.Now, ObservationStatusEnum.Pending);
                                ltrob.Add(rob);
                                virtualBlockID--;
                            }

                            if ((obt.ElementType == TemplateElementTypeEnum.Observation) && (obt.Status == CommonEntities.StatusEnum.Active))
                            {
                                ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                                ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                                RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance,
                                    episodeStatus, episodeDateTime, (typeRealization == RealizationTypeEnum.Routine) ? ElementTypeEnum.Routine : ElementTypeEnum.Procedure,
                                    entityActID, entityActName, null, entityID, entityName, virtualTemplateID, 0, virtualObsID, obt.ElementTitle, obt.VisibleLabel,
                                    obt.ElementTitlePosition, obt.Order, obt.Required, obt.Observation.ID, obt.Observation, ove, eove, 0, DateTime.Now, SpecialCategoryTypeEnum.None,
                                    ObservationStatusEnum.None, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                                ltrov.Add(rov);
                                virtualObsID--;
                            }
                        }

                        RegisteredObservationTemplateEntity rte = new RegisteredObservationTemplateEntity(virtualTemplateID, ote.ID, ote.Name, ote.TemplateTitle,
                            ote.ExportDocumentName, ote.TemplateEditionPresentation, ote.TemplateLayout, ote.TemplateLayoutItems, ote.TemplateViewResults,
                            ote.TemplateViewResultItems, ote.TemplateCopy, ltrob.ToArray(), ltrov.ToArray(), DateTime.Now, ObservationStatusEnum.Pending);
                        lrot.Add(rte);
                        virtualTemplateID--;
                    }

                    if ((ore.ElementType == CHElementTypeEnum.ObservationBlock) && (ore.Status == CommonEntities.StatusEnum.Active))
                    {
                        List<RegisteredObservationValueEntity> lbrov = new List<RegisteredObservationValueEntity>();
                        ObservationBlockEntity obe = _masterObservationCache.ObservationBlockCache.Get(ore.ElementID, false);
                        foreach (ObservationBlockRelEntity obs in obe.Observations)
                        {
                            if (obs.Status == CommonEntities.StatusEnum.Active)
                            {
                                ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                                ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                                RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance,
                                    episodeStatus, episodeDateTime, (typeRealization == RealizationTypeEnum.Routine) ? ElementTypeEnum.Routine : ElementTypeEnum.Procedure,
                                    entityActID, entityActName, null, entityID, entityName, 0, virtualBlockID, virtualObsID, obs.LabelName, obs.VisibleLabel, LabelPositionEnum.Left,
                                    obs.Order, false, obs.Observation.ID, obs.Observation, ove, eove, 0, DateTime.Now, SpecialCategoryTypeEnum.None, ObservationStatusEnum.None,
                                    DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                                lbrov.Add(rov);
                                virtualObsID--;
                            }
                        }
                        RegisteredObservationBlockEntity rob = new RegisteredObservationBlockEntity(0, 0, obe.ID, obe.Name, obe.LabelTitle, obe.BlockLayout, 1,
                            obe.LabelTitle, LabelPositionEnum.Top, true, obe.BlockLayoutItems, obe.BlockLabelPosition, obe.BlockLayoutLabels, lbrov.ToArray(),
                            DateTime.Now, ObservationStatusEnum.Pending);
                        lrob.Add(rob);
                        virtualBlockID--;
                    }

                    if ((ore.ElementType == CHElementTypeEnum.Observation) && (ore.Status == CommonEntities.StatusEnum.Active))
                    {
                        ObservationEntity oe = _masterObservationCache.ObservationCache.Get(ore.ElementID, false);
                        ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                        ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                        RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance, episodeStatus,
                            episodeDateTime, (typeRealization == RealizationTypeEnum.Routine) ? ElementTypeEnum.Routine : ElementTypeEnum.Procedure, entityActID,
                            entityActName, null, entityID, entityName, 0, 0, virtualObsID, oe.Name, true, LabelPositionEnum.Left, 1, false, oe.ID, oe, ove, eove, 0, DateTime.Now,
                            SpecialCategoryTypeEnum.None, ObservationStatusEnum.None, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                        lrov.Add(rov);
                        virtualObsID--;
                    }
                }
                RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity(customerID,
                    (lrov.Count > 0) ? lrov.ToArray() : null,
                    (lrob.Count > 0) ? lrob.ToArray() : null,
                    (lrot.Count > 0) ? lrot.ToArray() : null);
                if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                {
                    foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                    {
                        LoadRegisteredObservationValuesRequired(rot);
                    }
                }
                return registeredLayout;
            }
            else return null;
        }

        public RegisteredLayoutEntity NewCustomerOrderRequestRegisteredLayout(CustomerOrderRequestEntity customerOrderRequest)
        {
            if (customerOrderRequest == null || customerOrderRequest.Order == null)
                throw new ArgumentNullException("customerOrderRequest");

            int customerID = customerOrderRequest.CustomerID;
            int episodeID = (customerOrderRequest.CustomerEpisode != null) ? customerOrderRequest.CustomerEpisode.ID : 0;
            string episodeNumber = (customerOrderRequest.CustomerEpisode != null) ? customerOrderRequest.CustomerEpisode.EpisodeNumber : string.Empty;
            DateTime? episodeDateTime = (customerOrderRequest.CustomerEpisode != null) ? (customerOrderRequest.CustomerEpisode.StartDateTime != DateTime.MinValue) ? customerOrderRequest.CustomerEpisode.StartDateTime : (DateTime?)null : null;
            CommonEntities.StatusEnum episodeStatus = (customerOrderRequest.CustomerEpisode != null) ? customerOrderRequest.CustomerEpisode.Status : CommonEntities.StatusEnum.None;
            int entityActID = customerOrderRequest.ID;
            string entityActName = customerOrderRequest.Order.Name;
            int entityID = customerOrderRequest.Order.ID;
            string entityName = customerOrderRequest.Order.Name;
            List<ObservationRelEntity> orels = new List<ObservationRelEntity>();
            if (customerOrderRequest.Order.Observations == null || customerOrderRequest.Order.Observations.Length <= 0) return null;
            orels.AddRange(customerOrderRequest.Order.Observations);


            if ((orels != null) && (orels.Count > 0))
            {
                IObservationCacheService _masterObservationCache =
                    IoCFactory.CurrentContainer.Resolve<IObservationCacheService>();
                _masterObservationCache.UpdateCaches();

                int virtualObsID = -1;
                int virtualBlockID = -1;
                int virtualTemplateID = -1;
                List<RegisteredObservationTemplateEntity> lrot = new List<RegisteredObservationTemplateEntity>();
                List<RegisteredObservationBlockEntity> lrob = new List<RegisteredObservationBlockEntity>();
                List<RegisteredObservationValueEntity> lrov = new List<RegisteredObservationValueEntity>();

                foreach (ObservationRelEntity ore in orels)
                {
                    if ((ore.ElementType == CHElementTypeEnum.ObservationTemplate) && (ore.Status == CommonEntities.StatusEnum.Active))
                    {
                        List<RegisteredObservationValueEntity> ltrov = new List<RegisteredObservationValueEntity>();
                        List<RegisteredObservationBlockEntity> ltrob = new List<RegisteredObservationBlockEntity>();

                        ObservationTemplateEntity ote = _masterObservationCache.ObservationTemplateCache.Get(ore.ElementID, false);
                        foreach (ObservationTemplateRelEntity obt in ote.Blocks)
                        {
                            if ((obt.ElementType == TemplateElementTypeEnum.ObservationBlock) && (obt.Status == CommonEntities.StatusEnum.Active))
                            {
                                List<RegisteredObservationValueEntity> lsecondaryrov = new List<RegisteredObservationValueEntity>();
                                foreach (ObservationBlockRelEntity obs in obt.ObservationBlock.Observations)
                                {
                                    if (obs.Status == CommonEntities.StatusEnum.Active)
                                    {
                                        ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                                        ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                                        RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance,
                                            episodeStatus, episodeDateTime, ElementTypeEnum.RequestOrder,
                                            entityActID, entityActName, null, entityID, entityName, virtualTemplateID, virtualBlockID, virtualObsID, obs.LabelName,
                                            obs.VisibleLabel, obt.ObservationBlock.BlockLabelPosition, obs.Order, obt.Required, obs.Observation.ID, obs.Observation, ove, eove, 0, DateTime.Now,
                                            SpecialCategoryTypeEnum.None, ObservationStatusEnum.None, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                                        lsecondaryrov.Add(rov);
                                        virtualObsID--;
                                    }
                                }
                                RegisteredObservationBlockEntity rob = new RegisteredObservationBlockEntity(0, virtualBlockID, obt.ObservationBlock.ID,
                                    obt.ObservationBlock.Name, obt.ObservationBlock.LabelTitle, obt.ObservationBlock.BlockLayout, obt.Order, obt.ElementTitle,
                                    obt.ElementTitlePosition, obt.VisibleLabel, obt.ObservationBlock.BlockLayoutItems, obt.ObservationBlock.BlockLabelPosition,
                                    obt.ObservationBlock.BlockLayoutLabels, lsecondaryrov.ToArray(), DateTime.Now, ObservationStatusEnum.Pending);
                                ltrob.Add(rob);
                                virtualBlockID--;
                            }

                            if ((obt.ElementType == TemplateElementTypeEnum.Observation) && (obt.Status == CommonEntities.StatusEnum.Active))
                            {
                                ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                                ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                                RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance,
                                    episodeStatus, episodeDateTime, ElementTypeEnum.RequestOrder,
                                    entityActID, entityActName, null, entityID, entityName, virtualTemplateID, 0, virtualObsID, obt.ElementTitle, obt.VisibleLabel,
                                    obt.ElementTitlePosition, obt.Order, obt.Required, obt.Observation.ID, obt.Observation, ove, eove, 0, DateTime.Now, SpecialCategoryTypeEnum.None,
                                    ObservationStatusEnum.None, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                                ltrov.Add(rov);
                                virtualObsID--;
                            }
                        }

                        RegisteredObservationTemplateEntity rte = new RegisteredObservationTemplateEntity(virtualTemplateID, ote.ID, ote.Name, ote.TemplateTitle,
                            ote.ExportDocumentName, ote.TemplateEditionPresentation, ote.TemplateLayout, ote.TemplateLayoutItems, ote.TemplateViewResults,
                            ote.TemplateViewResultItems, ote.TemplateCopy, ltrob.ToArray(), ltrov.ToArray(), DateTime.Now, ObservationStatusEnum.Pending);
                        lrot.Add(rte);
                        virtualTemplateID--;
                    }

                    if ((ore.ElementType == CHElementTypeEnum.ObservationBlock) && (ore.Status == CommonEntities.StatusEnum.Active))
                    {
                        List<RegisteredObservationValueEntity> lbrov = new List<RegisteredObservationValueEntity>();
                        ObservationBlockEntity obe = _masterObservationCache.ObservationBlockCache.Get(ore.ElementID, false);
                        foreach (ObservationBlockRelEntity obs in obe.Observations)
                        {
                            if (obs.Status == CommonEntities.StatusEnum.Active)
                            {
                                ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                                ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                                RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance,
                                    episodeStatus, episodeDateTime, ElementTypeEnum.RequestOrder,
                                    entityActID, entityActName, null, entityID, entityName, 0, virtualBlockID, virtualObsID, obs.LabelName, obs.VisibleLabel, LabelPositionEnum.Left,
                                    obs.Order, false, obs.Observation.ID, obs.Observation, ove, eove, 0, DateTime.Now, SpecialCategoryTypeEnum.None, ObservationStatusEnum.None,
                                    DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                                lbrov.Add(rov);
                                virtualObsID--;
                            }
                        }
                        RegisteredObservationBlockEntity rob = new RegisteredObservationBlockEntity(0, 0, obe.ID, obe.Name, obe.LabelTitle, obe.BlockLayout, 1,
                            obe.LabelTitle, LabelPositionEnum.Top, true, obe.BlockLayoutItems, obe.BlockLabelPosition, obe.BlockLayoutLabels, lbrov.ToArray(),
                            DateTime.Now, ObservationStatusEnum.Pending);
                        lrob.Add(rob);
                        virtualBlockID--;
                    }

                    if ((ore.ElementType == CHElementTypeEnum.Observation) && (ore.Status == CommonEntities.StatusEnum.Active))
                    {
                        ObservationEntity oe = _masterObservationCache.ObservationCache.Get(ore.ElementID, false);
                        ObservationValueEntity ove = new ObservationValueEntity(0, null, null, null, null, null, null, DateTime.Now, string.Empty, 0);
                        ExtObservationValueEntity eove = new ExtObservationValueEntity(0, null, DateTime.Now, String.Empty, 0);
                        RegisteredObservationValueEntity rov = new RegisteredObservationValueEntity(customerID, episodeID, episodeNumber, EpisodeTypeEnum.Assistance, episodeStatus,
                            episodeDateTime, ElementTypeEnum.RequestOrder, entityActID,
                            entityActName, null, entityID, entityName, 0, 0, virtualObsID, oe.Name, true, LabelPositionEnum.Left, 1, false, oe.ID, oe, ove, eove, 0, DateTime.Now,
                            SpecialCategoryTypeEnum.None, ObservationStatusEnum.None, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                        lrov.Add(rov);
                        virtualObsID--;
                    }
                }
                RegisteredLayoutEntity registeredLayout = new RegisteredLayoutEntity(customerID,
                    (lrov.Count > 0) ? lrov.ToArray() : null,
                    (lrob.Count > 0) ? lrob.ToArray() : null,
                    (lrot.Count > 0) ? lrot.ToArray() : null);
                if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                {
                    foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                    {
                        LoadRegisteredObservationValuesRequired(rot);
                    }
                }
                registeredLayout.CustomerID = customerOrderRequest.CustomerID;
                return registeredLayout;
            }
            else
                return null;
        }
        #endregion

        #region Public read methods from CareActivityListView
        #region private aux methods
        //////////////////////////////////////////////////////
        // estos métodos se están utilizando en varias BL, sería recomendable ponerlos en un processor o similar. PREGUNTAR A ROBERTO
        //////////////////////////////////////////////////////
        private int[] GetListRecursiveLocations(int locationID)
        {
            if (locationID <= 0)
                return null;

            DataSet dataset = DataAccess.CareProcessRealizationDA.GetRecursiveLocations(locationID, null);
            if (!dataset.Tables.Contains(Common.Entities.TableNames.IDDescriptionTable)
                || dataset.Tables[Common.Entities.TableNames.IDDescriptionTable].Rows == null
                || dataset.Tables[Common.Entities.TableNames.IDDescriptionTable].Rows.Count <= 0)
                return null;

            return (from row in dataset.Tables[Common.Entities.TableNames.IDDescriptionTable].AsEnumerable()
                    select row["ID"] as int? ?? 0).ToArray();
        }

        private int[] GetCustomerByCareProcessByLocations(DataSet ds)
        {
            if (ds == null || !ds.Tables.Contains(BackOffice.Entities.TableNames.RegisteredLayoutTable)
                || ds.Tables[BackOffice.Entities.TableNames.RegisteredLayoutTable].Rows == null
                || ds.Tables[BackOffice.Entities.TableNames.RegisteredLayoutTable].Rows.Count <= 0)
                return null;

            return (from row in ds.Tables[BackOffice.Entities.TableNames.RegisteredLayoutTable].AsEnumerable()
                    select row["CustomerID"] as int? ?? 0).ToArray();
        }

        private int[] GetNurseNotesTemplates()
        {
            DataSet dataset = DataAccess.ObservationTemplateDA.GetObservationTemplatesByNurseNotes();
            if (!dataset.Tables.Contains(BackOffice.Entities.TableNames.ObservationTemplateTable)
                || dataset.Tables[BackOffice.Entities.TableNames.ObservationTemplateTable].Rows == null
                || dataset.Tables[BackOffice.Entities.TableNames.ObservationTemplateTable].Rows.Count <= 0)
                return null;

            return (from row in dataset.Tables[BackOffice.Entities.TableNames.ObservationTemplateTable].AsEnumerable()
                    select row["ID"] as int? ?? 0).ToArray();
        }

        private int[] GetNurseNotesBlocks()
        {
            DataSet dataset = DataAccess.ObservationBlockDA.GetObservationBlocksByNurseNotes();
            if (!dataset.Tables.Contains(BackOffice.Entities.TableNames.ObservationBlockTable)
                || dataset.Tables[BackOffice.Entities.TableNames.ObservationBlockTable].Rows == null
                || dataset.Tables[BackOffice.Entities.TableNames.ObservationBlockTable].Rows.Count <= 0)
                return null;

            return (from row in dataset.Tables[BackOffice.Entities.TableNames.ObservationBlockTable].AsEnumerable()
                    select row["ID"] as int? ?? 0).ToArray();
        }

        private int[] GetNurseNotesObservatios()
        {
            DataSet dataset = DataAccess.ObservationDA.GetObservationsByNurseNotes();
            if (!dataset.Tables.Contains(BackOffice.Entities.TableNames.ObservationTable)
                || dataset.Tables[BackOffice.Entities.TableNames.ObservationTable].Rows == null
                || dataset.Tables[BackOffice.Entities.TableNames.ObservationTable].Rows.Count <= 0)
                return null;

            return (from row in dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationTable].AsEnumerable()
                    select row["ID"] as int? ?? 0).ToArray();
        }
        #endregion

        public RegisteredLayoutEntity[] GetCareProcessRegisteredLayoutsByLocations(int locationID)
        {
            try
            {
                if (locationID <= 0)
                    return null;

                int[] locationIDs = this.GetListRecursiveLocations(locationID);
                int[] observationTemplateIDs = this.GetNurseNotesTemplates();
                int[] observationblockIDs = this.GetNurseNotesBlocks();
                int[] observationIDs = this.GetNurseNotesObservatios();

                if ((observationTemplateIDs == null || observationTemplateIDs.Length <= 0)
                    && (observationblockIDs == null || observationblockIDs.Length <= 0)
                    && (observationIDs == null || observationIDs.Length <= 0))
                    return null;

                DataSet ds = DataAccess.CareProcessRealizationDA.GetCustomerIDsWithCareProcessListByLocations(locationIDs);
                if (ds != null && ds.Tables.Contains(Assistance.Entities.TableNames.CustomerCareProcessTable)
                    && (ds.Tables[Assistance.Entities.TableNames.CustomerCareProcessTable].Rows.Count > 0))
                {
                    _observationCache.UpdateCaches();

                    DataTable dt = ds.Tables[Assistance.Entities.TableNames.CustomerCareProcessTable];
                    dt.TableName = BackOffice.Entities.TableNames.RegisteredLayoutTable;

                    int[] customerIDs = GetCustomerByCareProcessByLocations(ds);
                    if (customerIDs == null || customerIDs.Length <= 0)
                        return null;

                    DataSet ds2;
                    #region RegisteredObservationTemplates
                    // Devuelve los customertemplates de un grupo de clientes a partir de unos templates
                    ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationTemplatesByCustomersAndTemplates(customerIDs, observationTemplateIDs);
                    if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.RegisteredObservationTemplateTable)
                        && (ds2.Tables[BackOffice.Entities.TableNames.RegisteredObservationTemplateTable].Rows.Count > 0))
                    {
                        MergeTable(ds2, ds, BackOffice.Entities.TableNames.RegisteredObservationTemplateTable);
                    }
                    #endregion

                    #region RegisteredObservationBlocks
                    // Devuelve los customerblocks de un grupo de clientes a partir de unos blocks
                    ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationBlocksByCustomersAndBlocks(customerIDs, observationblockIDs);
                    if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.RegisteredObservationBlockTable)
                        && (ds2.Tables[BackOffice.Entities.TableNames.RegisteredObservationBlockTable].Rows.Count > 0))
                    {
                        MergeTable(ds2, ds, BackOffice.Entities.TableNames.RegisteredObservationBlockTable);

                        #region BlockLayouts
                        ds2 = DataAccess.BlockLayoutLabelDA.GetBlockLayoutLabelsByIDs(observationblockIDs);
                        if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.BlockLayoutLabelTable)
                            && (ds2.Tables[BackOffice.Entities.TableNames.BlockLayoutLabelTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.BlockLayoutLabelTable);
                        }
                        #endregion
                    }
                    #endregion

                    #region RegisteredObservationValues
                    // Devuelve las customerobservations además de su enlace al customerblockid o al customertemplateid
                    ds2 = DataAccess.CustomerObservationDA.GetRegisteredObservationValuesByCustomersAndObservations(customerIDs, observationIDs);
                    if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.RegisteredObservationValueTable)
                        && (ds2.Tables[BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0))
                    {
                        MergeTable(ds2, ds, BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                        #region ObservationValues
                        // Devuelve las ObservationValues de un cliente
                        ds2 = DataAccess.CustomerObservationDA.GetObservationValuesByCustomersAndObservations(customerIDs, observationIDs);
                        if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.ObservationValueTable)
                            && (ds2.Tables[BackOffice.Entities.TableNames.ObservationValueTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.ObservationValueTable);
                        }
                        #endregion

                        #region ExtObservationValues
                        // Devuelve las ExtObservationValues de un cliente
                        ds2 = DataAccess.CustomerObservationDA.GetExtObservationValuesByCustomersAndObservations(customerIDs, observationIDs);
                        if ((ds2 != null) && ds2.Tables.Contains(BackOffice.Entities.TableNames.ExtObservationValueTable)
                            && (ds2.Tables[BackOffice.Entities.TableNames.ExtObservationValueTable].Rows.Count > 0))
                        {
                            MergeTable(ds2, ds, BackOffice.Entities.TableNames.ExtObservationValueTable);
                        }
                        #endregion
                    }
                    #endregion

                    RegisteredLayoutAdvancedAdapter rlaa = new RegisteredLayoutAdvancedAdapter();
                    RegisteredLayoutEntity[] registeredLayouts = rlaa.GetData(ds);
                    if (registeredLayouts != null && registeredLayouts.Length > 0)
                    {
                        foreach (RegisteredLayoutEntity registeredLayout in registeredLayouts)
                        {
                            if (registeredLayout.Templates != null && registeredLayout.Templates.Length > 0)
                            {
                                foreach (RegisteredObservationTemplateEntity rot in registeredLayout.Templates)
                                {
                                    LoadRegisteredObservationValuesRequired(rot);
                                }
                            }
                        }
                    }
                    return registeredLayouts;
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

        #region aux observations to prescriptions
        public CustomerObservationEntity[] GetLastCustomerObservations(int[] customerEpisodeIDs, string eacelementName, string eacattributeName)
        {
            try
            {
                CommonEntities.ElementEntity eacelementEntity = ElementBL.GetElementByName(eacelementName);
                CommonEntities.AttributeEntity observationsAttribute = (eacelementEntity != null)
                    ? eacelementEntity.GetAttribute(eacattributeName)
                    : null;
                if (observationsAttribute == null || observationsAttribute.AttributeOptions == null ||
                    observationsAttribute.AttributeOptions.Length <= 0) return null;

                string[] observationsToFind = (from attop in observationsAttribute.AttributeOptions
                                               where !string.IsNullOrEmpty(attop.Value)
                                               select attop.Value).ToArray();

                DataSet ds = _dataAccess.CustomerObservationDA.GetLastCustomerObservations(customerEpisodeIDs, observationsToFind);
                if (ds != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerObservationTable)
                    && ds.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    DataSet ds1 = _dataAccess.CustomerObservationDA.GetObservationValuesByCustomerEpisodesAndObservations(customerEpisodeIDs, observationsToFind);
                    if (ds1 != null && ds1.Tables.Contains(BackOffice.Entities.TableNames.ObservationValueTable)
                        && ds1.Tables[BackOffice.Entities.TableNames.ObservationValueTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                        ds.Tables.Add(dt);
                        CustomerObservationAdvancedAdapter coaa = new CustomerObservationAdvancedAdapter();
                        CustomerObservationEntity[] thiscovs = coaa.GetData(ds);
                        if (thiscovs != null && thiscovs.Length > 0)
                        {
                            this.SetObservations(thiscovs);
                        }
                        return thiscovs;
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

        public CustomerObservationEntity[] GetLastCustomerObservations(int customerID, string eacelementName, string eacattributeName)
        {
            try
            {
                CommonEntities.ElementEntity eacelementEntity = ElementBL.GetElementByName(eacelementName);
                CommonEntities.AttributeEntity observationsAttribute = (eacelementEntity != null)
                    ? eacelementEntity.GetAttribute(eacattributeName)
                    : null;
                if (observationsAttribute == null || observationsAttribute.AttributeOptions == null ||
                    observationsAttribute.AttributeOptions.Length <= 0) return null;

                string[] observationsToFind = (from attop in observationsAttribute.AttributeOptions
                                               where !string.IsNullOrEmpty(attop.Value)
                                               select attop.Value).ToArray();

                DataSet ds = _dataAccess.CustomerObservationDA.GetLastCustomerObservationsByCustomerID(customerID, observationsToFind);
                if (ds != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerObservationTable)
                    && ds.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    DataSet ds1 = _dataAccess.CustomerObservationDA.GetObservationValuesByCustomersAndObservationsByCustomerID(customerID, observationsToFind);
                    if (ds1 != null && ds1.Tables.Contains(BackOffice.Entities.TableNames.ObservationValueTable)
                        && ds1.Tables[BackOffice.Entities.TableNames.ObservationValueTable].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[BackOffice.Entities.TableNames.ObservationValueTable].Copy();
                        ds.Tables.Add(dt);
                        CustomerObservationAdvancedAdapter coaa = new CustomerObservationAdvancedAdapter();
                        CustomerObservationEntity[] thiscovs = coaa.GetData(ds);
                        if (thiscovs != null && thiscovs.Length > 0)
                        {
                            this.SetObservations(thiscovs);
                        }
                        return thiscovs;
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


        #region auxiliares para indigo y efarmaco
        public CompositeObservationValueEntity[] GetCompositeByObservationIDsAndCustomerProcessIDs(int[] customerProcessIDs, int[] obsIDs)
        {
            // por ahora sólo se utilizará para Diagnostico principal CIE y Descripción.
            if (customerProcessIDs == null || customerProcessIDs.Length <= 0 || obsIDs == null || obsIDs.Length <= 0) return null;
            try
            {
                DataSet ds = DataAccess.CustomerObservationDA.GetCompositeByObservationIDsAndCustomerProcessIDs(customerProcessIDs, obsIDs);
                if (ds != null && ds.Tables != null &&
                    ds.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable) &&
                    ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].Rows.Count > 0)
                {
                    return ds.Tables[SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable].AsEnumerable()
                        .Where(row => (row["CustomerProcessID"] as int? ?? 0) > 0)
                        .Select(row => new CompositeObservationValueEntity(
                            (row["CustomerID"] as int? ?? 0),
                            (row["CustomerProcessID"] as int? ?? 0),
                            (row["ObservationID"] as int? ?? 0),
                            (row["ObservationName"] as string ?? string.Empty),
                            (row["ObservationValue"] as string ?? string.Empty)))
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

        #region Simulator
        public string GetRegisteredObservationValueBySimulator()
        {
            string Mensaje="";

            Mensaje = DataAccess.CustomerObservationDA.GetRegisteredObservationValueBySimulator();
            return Mensaje;

        }
        #endregion

        #region observations lopd
        public RegisteredObservationTemplateEntity GetLOPDTemplatesByCustomer(int customerID)
        {
            try
            {
                CommonEntities.ElementEntity _dataonlinemetadata = ElementBL.GetElementByName(CommonEntities.Constants.EntityNames.DataOnLineShowName);
                if (_dataonlinemetadata == null || _dataonlinemetadata.Attributes == null || _dataonlinemetadata.Attributes.Length <= 0) return null;
                CommonEntities.AttributeEntity templateattr = _dataonlinemetadata.GetAttribute("DataOfInterestTemplate");
                if (templateattr == null || string.IsNullOrEmpty(templateattr.DefaultValue)) return null;
                int observationTemplateID = DataAccess.ObservationTemplateDA.FindObservationTemplate(templateattr.DefaultValue);
                if (observationTemplateID <= 0) return null;

                RegisteredObservationTemplateEntity[] rots = this.GetRegisteredObservationTemplateByObservationTemplateID(customerID, observationTemplateID);
                if (rots == null || rots.Length <= 0)
                {
                    CustomerCareRecordsProcessor CustomerCareRecords = new CustomerCareRecordsProcessor();
                    RegisteredObservationHeaderDTO headerDTO = CustomerCareRecords.GetHeaderDTOByAct(SII.HCD.BackOffice.Entities.ElementTypeEnum.None, null, null);
                    headerDTO.ObservationTemplateID = observationTemplateID;
                    return CustomerCareRecords.GetVirtualRegisteredObservationTemplate(headerDTO);
                }
                else
                    return rots.OrderByDescending(rot => rot.RegistrationDateTime).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }
        #endregion

        public List<string> GetValueAlergiasIndigo(int[] customerIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) throw new ArgumentNullException("customerIDs");

                return DataAccess.CustomerObservationDA.GetValueAlergiasIndigo(customerIDs.Distinct().ToArray());
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

                return DataAccess.CustomerObservationDA.HasAntecedentes(customerIDs.Distinct().ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.Service)) throw;
                return null;
            }
        }

    }

    #region ReportObservationResult class
    enum ReportObservationResultType { HorizontalCells, VerticalCells, Rows }
    class ReportObservationResult
    {
        //public ReportObservationResultType Mode { get; set; }
        public ReportBlockRowDTO[] Rows { get; set; }
        public ReportObservationDTO Label { get; set; }
        public ReportObservationDTO Value { get; set; }
        public ReportObservationDTO NormalValue { get; set; }
    }
    #endregion

    #region Support classes
    public class CustomerObservationDataAccess
    {
        public CustomerDA CustomerDA { get; set; }
        public CustomerTemplateDA CustomerTemplateDA { get; set; }
        public CustomerTemplateBlockRelDA CustomerTemplateBlockRelDA { get; set; }
        public CustomerTemplateObsRelDA CustomerTemplateObsRelDA { get; set; }
        public CustomerBlockDA CustomerBlockDA { get; set; }
        public CustomerBlockObsRelDA CustomerBlockObsRelDA { get; set; }
        public CustomerObservationDA CustomerObservationDA { get; set; }
        public ObservationDA ObservationDA { get; set; }
        public ObservationConceptRelDA ObservationConceptRelDA { get; set; }
        public ObservationBlockDA ObservationBlockDA { get; set; }
        public ObservationTemplateDA ObservationTemplateDA { get; set; }
        public RoutineActObsRelDA RoutineActObsRelDA { get; set; }
        public ProcedureActObsRelDA ProcedureActObsRelDA { get; set; }
        public CustomerMedEpisodeActObsRelDA CustomerMedEpisodeActObsRelDA { get; set; }
        //public ProtocolActObsRelDA ProtocolActObsRelDA { get; set; }
        public ObservationOptionDA ObservationOptionDA { get; set; }
        public ObservationValueDA ObservationValueDA { get; set; }
        public ExtObservationValueDA ExtObservationValueDA { get; set; }
        public BlockLayoutLabelDA BlockLayoutLabelDA { get; set; }
        public RecordDeletedLogDA RecordDeletedLogDA { get; set; }
        public CareProcessRealizationDA CareProcessRealizationDA { get; set; }

        public StepPreprintDA StepPreprintDA { get; set; }
        public CustomerProcessDA CustomerProcessDA { get; set; }
        public CustomerEpisodeDA CustomerEpisodeDA { get; set; }
        public MedicalEpisodeDA MedicalEpisodeDA { get; set; }
        public CustomerOrderRequestDA CustomerOrderRequestDA { get; set; }
        public RoutineActDA RoutineActDA { get; set; }
        public ProcedureActDA ProcedureActDA { get; set; }
    }

    public class CustomerObservationHelpers
    {
    }
    #endregion
}
