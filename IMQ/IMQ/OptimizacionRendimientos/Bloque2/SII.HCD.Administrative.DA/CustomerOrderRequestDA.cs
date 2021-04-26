using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.Administrative.Entities;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Common.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;
namespace SII.HCD.Administrative.DA
{
    public class CustomerOrderRequestDA : DAServiceBase
    {
        #region Field length constants
        public const int ModifiedByLength = 256;
        #endregion

        #region Constructors
        public CustomerOrderRequestDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerOrderRequestDA(Gateway gateway) : base(gateway) { }
        #endregion

        #region Public methods
        public int Insert(int orderID, string orderNumber, string placeOrderNumber, int customerID, int customerEpisodeID, int customerMedEpisodeActID, int requestedPersonID,
            int requestedInsurerID, int policyTypeID, int requestedLocationID, int requestedCareCenterID, int assistanceServiceID, int medicalSpecialtyID,
            string relevantClinicalInfo, string presumptiveDiagnosis,
            DateTime requestDateTime, DateTime? requestEffectiveAtDateTime, int episodeAssistanceServiceID, int attendingPhysicianID,
            int requestAppointmentID, string requestExplanation, int orderControlCode, int responseFlag, int priority, int criticalTimeID,
            bool orderPrinted, int parentCustomerOrderRequestID, DateTime registrationDateTime, bool placed, int status, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerOrderRequestCommand,
                    new StoredProcInParam("OrderID", DbType.Int32, orderID),
                    new StoredProcInParam("OrderNumber", DbType.String, orderNumber),
                    new StoredProcInParam("PlaceOrderNumber", DbType.String, placeOrderNumber),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID),
                    new StoredProcInParam("RequestedPersonID", DbType.Int32, requestedPersonID),
                    new StoredProcInParam("RequestedInsurerID", DbType.Int32, requestedInsurerID),
                    new StoredProcInParam("PolicyTypeID", DbType.Int32, policyTypeID),
                    new StoredProcInParam("RequestedLocationID", DbType.Int32, requestedLocationID),
                    new StoredProcInParam("RequestedCareCenterID", DbType.Int32, requestedCareCenterID),
                    new StoredProcInParam("AssistanceServiceID", DbType.Int32, assistanceServiceID),
                    new StoredProcInParam("MedicalSpecialtyID", DbType.Int32, medicalSpecialtyID),
                    new StoredProcInParam("RelevantClinicalInfo", DbType.String, relevantClinicalInfo),
                    new StoredProcInParam("PresumptiveDiagnosis", DbType.String, presumptiveDiagnosis),
                    new StoredProcInParam("RequestDateTime", DbType.DateTime, requestDateTime),
                    new StoredProcInParam("RequestEffectiveAtDateTime", DbType.DateTime, (requestEffectiveAtDateTime != null) ? (object)requestEffectiveAtDateTime : (object)DBNull.Value),
                    new StoredProcInParam("EpisodeAssistanceServiceID", DbType.Int32, episodeAssistanceServiceID),
                    new StoredProcInParam("AttendingPhysicianID", DbType.Int32, attendingPhysicianID),
                    new StoredProcInParam("RequestAppointmentID", DbType.Int32, requestAppointmentID),
                    new StoredProcInParam("RequestExplanation", DbType.String, requestExplanation),
                    new StoredProcInParam("OrderControlCode", DbType.Int32, orderControlCode),
                    new StoredProcInParam("ResponseFlag", DbType.Int32, responseFlag),
                    new StoredProcInParam("Priority", DbType.Int32, priority),
                    new StoredProcInParam("CriticalTimeID", DbType.Int32, criticalTimeID),
                    new StoredProcInParam("OrderPrinted", DbType.Boolean, orderPrinted),
                    new StoredProcInParam("ParentCustomerOrderRequestID", DbType.Int32, parentCustomerOrderRequestID),
                    new StoredProcInParam("RegistrationDateTime", DbType.DateTime, registrationDateTime),
                    new StoredProcInParam("Placed", DbType.Boolean, placed),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Update(int id, int orderID, string orderNumber, string placeOrderNumber, int customerID, int customerEpisodeID, int customerMedEpisodeActID,
            int requestedPersonID, int requestedInsurerID, int policyTypeID, int requestedLocationID, int requestedCareCenterID, int assistanceServiceID,
            int medicalSpecialtyID, string relevantClinicalInfo, string presumptiveDiagnosis,
            DateTime requestDateTime, DateTime? requestEffectiveAtDateTime, int episodeAssistanceServiceID, int attendingPhysicianID,
            int requestAppointmentID, string requestExplanation, int orderControlCode, int responseFlag, int priority, int criticalTimeID,
            bool orderPrinted, int parentCustomerOrderRequestID, DateTime registrationDateTime, bool placed, int status, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("OrderID", DbType.Int32, orderID),
                    new StoredProcInParam("OrderNumber", DbType.String, orderNumber),
                    new StoredProcInParam("PlaceOrderNumber", DbType.String, placeOrderNumber),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID),
                    new StoredProcInParam("RequestedPersonID", DbType.Int32, requestedPersonID),
                    new StoredProcInParam("RequestedInsurerID", DbType.Int32, requestedInsurerID),
                    new StoredProcInParam("PolicyTypeID", DbType.Int32, policyTypeID),
                    new StoredProcInParam("RequestedLocationID", DbType.Int32, requestedLocationID),
                    new StoredProcInParam("RequestedCareCenterID", DbType.Int32, requestedCareCenterID),
                    new StoredProcInParam("AssistanceServiceID", DbType.Int32, assistanceServiceID),
                    new StoredProcInParam("MedicalSpecialtyID", DbType.Int32, medicalSpecialtyID),
                    new StoredProcInParam("RelevantClinicalInfo", DbType.String, relevantClinicalInfo),
                    new StoredProcInParam("PresumptiveDiagnosis", DbType.String, presumptiveDiagnosis),
                    new StoredProcInParam("RequestDateTime", DbType.DateTime, requestDateTime),
                    new StoredProcInParam("RequestEffectiveAtDateTime", DbType.DateTime, (requestEffectiveAtDateTime != null) ? (object)requestEffectiveAtDateTime : (object)DBNull.Value),
                    new StoredProcInParam("EpisodeAssistanceServiceID", DbType.Int32, episodeAssistanceServiceID),
                    new StoredProcInParam("AttendingPhysicianID", DbType.Int32, attendingPhysicianID),
                    new StoredProcInParam("RequestAppointmentID", DbType.Int32, requestAppointmentID),
                    new StoredProcInParam("RequestExplanation", DbType.String, requestExplanation),
                    new StoredProcInParam("OrderControlCode", DbType.Int32, orderControlCode),
                    new StoredProcInParam("ResponseFlag", DbType.Int32, responseFlag),
                    new StoredProcInParam("Priority", DbType.Int32, priority),
                    new StoredProcInParam("CriticalTimeID", DbType.Int32, criticalTimeID),
                    new StoredProcInParam("OrderPrinted", DbType.Boolean, orderPrinted),
                    new StoredProcInParam("ParentCustomerOrderRequestID", DbType.Int32, parentCustomerOrderRequestID),
                    new StoredProcInParam("RegistrationDateTime", DbType.DateTime, registrationDateTime),
                    new StoredProcInParam("Placed", DbType.Boolean, placed),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Update(int id, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestDBTimeStampCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int UpdateParentOrder(int id, int parentOrderID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateParentOrderCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("ParentOrderID", DbType.Int32, parentOrderID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int UpdateResponseFlag(int id, int responseFlag, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestResponseFlagCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("ResponseFlag", DbType.Int32, responseFlag),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public bool UpdateStatus(int id, int status, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestStatusCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public int UpdatePlaced(int id, bool placed, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestPlacedCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Placed", DbType.Boolean, placed),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public bool UpdateCustomerEpisodeID(int id, int customerEpisodeID, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestCustomerEpisodeIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public int UpdateCustomerEpisodeIDByIDs(int[] ids, int customerEpisodeID, string modifiedBy)
        {
            try
            {
                string mySQL = String.Concat(SQLProvider.UpdateCustomerOrderRequestSetCustomerEpisodeIDCommand, Environment.NewLine,
                    "WHERE [ID] IN (", StringUtils.BuildIDString(ids), ")");

                return this.Gateway.ExecuteQueryNonQuery(mySQL,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public bool UpdateOrderPrinted(int id, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestPrintedCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool CustomerOrderRequestIsScheduled(int corID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.CustomerOrderRequestIsScheduledCommand,
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, corID)))
                {
                    return (!IsEmptyReader(reader));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }

        }

        public int Delete(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(
                    SQLProvider.DeleteCustomerOrderRequestCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("PendingStatus", DbType.Int32, ActionStatusEnum.Pending)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public Int64 GetDBTimeStamp(int id)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestDBTimeStampCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[Entities.TableNames.CustomerOrderRequestTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetByID(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByIDCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        public DataSet GetByID(int id, PatternTypeEnum PatternType, long DBTimeStamp)
        {
            try
            {
                
                 DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("ObtenerCustomerOrderRequestEntity",
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("@Superceded", DbType.Int32, (int)ActionStatusEnum.Superceded),
                    new StoredProcInParam("@PatternType", DbType.Int32, PatternType),
                    new StoredProcInParam("@DBTimeStamp", DbType.Int64, DBTimeStamp)
                    );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerOrderRequestTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ReasonChangeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.PrescriptionRequestTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.PrescriptionRequestTimeTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PharmaceuticalFormTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AdministrationRouteTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AdministrationMethodTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.LocationBaseTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.BodySiteConceptTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.EquipmentBaseTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PhysicalUnitTable;

                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ItemBaseTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.TimePatternTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestSchPlanningTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestTimeTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ProcedureBaseTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureRoutineRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureTimeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureReasonRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRoutineTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.RoutineBaseTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRoutineTimeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRoutineReasonRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestProcedureRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestProcedureTimeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestRoutineRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestRoutineTimeTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ParticipateAsTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestResourceRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestEquipmentRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestLocationRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestADTInfoTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestBodySiteRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.BodySiteTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.BodySiteParticipationTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestRequirementRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.RequirementTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestConsentRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ConsentPreprintTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ConsentTypeTable;

                    return ds;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        public DataSet GetByIDs(int[] ids)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestsByIDsCommand,
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInTVPIntegerParam("TVPTable", ids));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByParentCustomerOrderRequestID(int parentCustomerOrderRequestID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByParentCustomerOrderRequestIDCommand,
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("ParentCustomerOrderRequestID", DbType.Int32, parentCustomerOrderRequestID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        public DataSet GetByParentCustomerOrderRequestID(int parentCustomerOrderRequestID, PatternTypeEnum PatternType, long DBTimeStamp)
        {
            try
            {

                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("ObtenerCustomerOrderRequestEntity_ByParentCustomerOrderRequestID",
                   new StoredProcInParam("@ParentCustomerOrderRequestID", DbType.Int32, parentCustomerOrderRequestID),
                   new StoredProcInParam("@Superceded", DbType.Int32, (int)ActionStatusEnum.Superceded),
                   new StoredProcInParam("@PatternType", DbType.Int32, PatternType),
                   new StoredProcInParam("@DBTimeStamp", DbType.Int64, DBTimeStamp)
                   );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerOrderRequestTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerOrderRequestReasonRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.PrescriptionRequestTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.PrescriptionRequestTimeTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PharmaceuticalFormTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AdministrationRouteTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AdministrationMethodTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PhysicalUnitTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.LocationBaseTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.BodySiteConceptTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.EquipmentBaseTable;


                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ItemTreatmentOrderSequenceTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ItemBaseTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.TimePatternTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestSchPlanningTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestTimeTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ProcedureBaseTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureRoutineRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureTimeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerProcedureReasonRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRoutineTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.RoutineBaseTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRoutineTimeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRoutineReasonRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ReasonChangeTable;


                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestProcedureRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestProcedureTimeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestProcedureRoutineRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestRoutineRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestRoutineTimeTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestHumanResourceRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ParticipateAsTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestResourceRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestEquipmentRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestLocationRelTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestADTInfoTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestBodySiteRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.BodySiteTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.BodySiteParticipationTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestRequirementRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.RequirementTable;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.OrderRequestConsentRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ConsentPreprintTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ConsentTypeTable;

                    return ds;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        public DataSet GetByCustomerMedEpisodeActID(int customerMedEpisodeActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByCustomerMedEpisodeActIDCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByOrderNumber(string orderNumber)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByOrderNumberCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("OrderNumber", DbType.String, orderNumber));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByPlaceOrderNumber(string placeOrderNumber)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByPlaceOrderNumberCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("PlaceOrderNumber", DbType.String, placeOrderNumber));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByOrderNumberAndPlaceOrderNumber(string orderNumber, string placeOrderNumber)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByOrderNumberAndPlaceOrderNumberCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("OrderNumber", DbType.String, orderNumber),
                    new StoredProcInParam("PlaceOrderNumber", DbType.String, placeOrderNumber));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByCustomerID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByCustomerIDCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOnlySchByCustomerID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOnlySchCustomerOrderRequestByCustomerIDCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("Pending", DbType.Int32, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("Scheduled", DbType.Int32, (int)ActionStatusEnum.Scheduled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByCustomerIDAndOrderID(int customerID, int orderID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                    SQLProvider.GetCustomerOrderRequestByCustomerIDAndOrderIDCommand,
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("OrderID", DbType.Int32, orderID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetLastCustomerOrderRequestByCustomerIDAndOrderID(int customerID, int orderID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                    SQLProvider.GetLastCustomerOrderRequestByCustomerIDAndOrderIDCommand,
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("OrderID", DbType.Int32, orderID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByCustomerID(int customerID, int[] medicalEpisodeIDs)
        {
            try
            {
                String whereFilterByIDs = String.Empty;
                if ((medicalEpisodeIDs != null) && (medicalEpisodeIDs.Length > 0))
                {
                    whereFilterByIDs = String.Join(",", Array.ConvertAll(medicalEpisodeIDs, new Converter<int, string>(m => m.ToString())));
                }

                return this.Gateway.ExecuteQueryDataSet(String.Format(SQLProvider.GetCustomerOrderRequestByCustomerIDFilteredByMedicalEpisodeIDsCommand, whereFilterByIDs),
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetActiveDrugPrescriptionByCustomerID(int customerID, int orderClassType)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestsWithActivePrescriptionsByCustomerIDCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("OrderClassType", DbType.Int32, orderClassType),
                    new StoredProcInParam("Status", DbType.Int16, ActionStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestByCustomerEpisodeIDCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrderRequestActRequestDTOByCustomerMedEpisodeActIDMedicalEpisodeID(int customerMedEpisodeActID, int medicalEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrderRequestActRequestDTOByCustomerMedEpisodeActIDMedicalEpisodeIDCommand,
                    Administrative.Entities.TableNames.OrderRequestActsRequestDTOTable,
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID),
                    new StoredProcInParam("MedicalEpisodeID", DbType.Int32, medicalEpisodeID),
                    new StoredProcInParam("OrderClassTypePrescription", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetIDByCustomerOrderRealizationID(int customerOrderRealizationID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDCustomerOrderRequestByCustomerOrderRealizationIDCommand,
                    new StoredProcInParam("customerOrderRealizationID", DbType.Int32, customerOrderRealizationID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByRoutineActID(int routineActID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDCustomerOrderRequestByRoutineActIDCommand,
                    new StoredProcInParam("RoutineActID", DbType.Int32, routineActID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByProcedureActID(int procedureActID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDCustomerOrderRequestByProcedureActIDCommand,
                    new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByPrescriptionRequestID(int prescriptionRequestID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDCustomerOrderRequestByPrescriptionRequestIDCommand,
                    new StoredProcInParam("PrescriptionRequestID", DbType.Int32, prescriptionRequestID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByPrescriptionProcedureActID(int procedureActID, int itemID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDCustomerOrderRequestByPrescriptionProcedureActIDCommand,
                    new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID),
                    new StoredProcInParam("ItemID", DbType.Int32, itemID)))
                {
                    return (IsEmptyReader(reader))
                        ? 0
                        : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByCustomerAppointmentInformationID(int customerAppointmentInformationID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerOrderRequestIDByCustomerAppointmentInformationIDCommand,
                    new StoredProcInParam("CustomerAppointmentInformationID", DbType.Int32, customerAppointmentInformationID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetPrescriptionOrderByEpisodeAndItem(int episodeID, int itemID, DateTime supplyDateTime)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetPrescriptionOrderByEpisodeAndItemCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, episodeID),
                    new StoredProcInParam("ItemID", DbType.Int32, itemID),
                    new StoredProcInParam("SupplyDateTime", DbType.DateTime, supplyDateTime)))
                {
                    return (IsEmptyReader(reader))
                        ? 0
                        : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetCustomerIDByCorID(int customerOrderRequestID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerIDByCorIDCommand,
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["CustomerID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByCustomerRoutineID(int customerRoutineID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDCustomerOrderRequestByCustomerRoutineIDCommand,
                    new StoredProcInParam("CustomerRoutineID", DbType.Int32, customerRoutineID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByCustomerProcedureID(int customerProcedureID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDCustomerOrderRequestByCustomerProcedureIDCommand,
                    new StoredProcInParam("CustomerProcedureID", DbType.Int32, customerProcedureID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public ResponseFlagEnum GetCustomerOrderRequestResponseFlagByRoutineAct(int routineActID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerOrderRequestResponseFlagByRoutineActIDCommand,
                    new StoredProcInParam("RoutineActID", DbType.Int32, routineActID)))
                {
                    return (IsEmptyReader(reader)) ? ResponseFlagEnum.None : EnumUtils.GetEnum<ResponseFlagEnum>(reader["ResponseFlag"]);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return ResponseFlagEnum.None;
            }
        }

        public ResponseFlagEnum GetCustomerOrderRequestResponseFlagByProcedureAct(int procedureActID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerOrderRequestResponseFlagByProcedureActIDCommand,
                    new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID)))
                {
                    return (IsEmptyReader(reader)) ? ResponseFlagEnum.None : EnumUtils.GetEnum<ResponseFlagEnum>(reader["ResponseFlag"]);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return ResponseFlagEnum.None;
            }
        }

        public int[] GetIDForCitationByOrderIDsAndCustomerID(int[] orderIDs, int customerID)
        {
            try
            {
                if ((orderIDs == null) || (orderIDs.Length <= 0))
                    return null;

                string sqlQuery = string.Concat(SQLProvider.GetIDForCitationByOrderIDsAndCustomerIDCommand, Environment.NewLine,
                    "   AND (COR.OrderID IN (", StringUtils.BuildIDString(orderIDs), "))");

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("StatusConfirmed", DbType.Int32, (int)ActionStatusEnum.Confirmed),
                        new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                        new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)EpisodeCaseEnum.EmergencyOutPatient),
                        new StoredProcInParam("InPatient", DbType.Int32, (int)EpisodeCaseEnum.InPatient),
                        new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                        new StoredProcInParam("ParentOrder", DbType.Int32, (int)OrderControlCodeEnum.ParentOrder)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int[] GetIDForCitationByOrderIDsAndCustomerIDNotChildOrder(int[] orderIDs, int customerID)
        {
            try
            {
                if ((orderIDs == null) || (orderIDs.Length <= 0))
                    return null;

                string sqlQuery = string.Concat(SQLProvider.GetIDForCitationByOrderIDsAndCustomerIDNotChildOrderCommand, Environment.NewLine,
                    "   AND (COR.OrderID IN (", StringUtils.BuildIDString(orderIDs), "))");

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("StatusConfirmed", DbType.Int32, (int)ActionStatusEnum.Confirmed),
                        new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                        new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)EpisodeCaseEnum.EmergencyOutPatient),
                        new StoredProcInParam("InPatient", DbType.Int32, (int)EpisodeCaseEnum.InPatient),
                        new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                        new StoredProcInParam("StatusCancelled", DbType.Int32, (int)CommonEntities.StatusEnum.Cancelled),
                        new StoredProcInParam("ChildOrder", DbType.Int32, (int)OrderControlCodeEnum.ChildOrder)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int[] GetIDForCitationByCustomerIDAndParentCustomerOrderRequestID(int customerID, int parentCustomerOrderRequestID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetIDForCitationByParentCustomerOrderRequestIDOrderIDsAndCustomerIDCommand,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("ParentCustomerOrderRequestID", DbType.Int32, parentCustomerOrderRequestID),
                        new StoredProcInParam("StatusConfirmed", DbType.Int32, (int)ActionStatusEnum.Confirmed),
                        new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                        new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)EpisodeCaseEnum.EmergencyOutPatient),
                        new StoredProcInParam("InPatient", DbType.Int32, (int)EpisodeCaseEnum.InPatient),
                        new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }
                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int[] GetIDForCitationByParentCustomerOrderRequestIDOrderIDsAndCustomerID(int[] orderIDs, int customerID, int parentCustomerOrderRequestID)
        {
            try
            {
                if ((orderIDs == null) || (orderIDs.Length <= 0))
                    return null;

                string sqlQuery = string.Concat(SQLProvider.GetIDForCitationByParentCustomerOrderRequestIDOrderIDsAndCustomerIDCommand, Environment.NewLine,
                    "   AND (COR.OrderID IN (", StringUtils.BuildIDString(orderIDs), "))");

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("ParentCustomerOrderRequestID", DbType.Int32, parentCustomerOrderRequestID),
                        new StoredProcInParam("StatusConfirmed", DbType.Int32, (int)ActionStatusEnum.Confirmed),
                        new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                        new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)EpisodeCaseEnum.EmergencyOutPatient),
                        new StoredProcInParam("InPatient", DbType.Int32, (int)EpisodeCaseEnum.InPatient),
                        new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int[] GetIDForReceptionByOrderIDsAndCustomerID(int[] orderIDs, int customerID)
        {
            try
            {
                if ((orderIDs == null) || (orderIDs.Length <= 0))
                    return null;

                string sqlQuery = string.Concat(SQLProvider.GetIDForReceptionByOrderIDsAndCustomerIDCommand, Environment.NewLine,
                    "   AND (COR.OrderID IN (", StringUtils.BuildIDString(orderIDs), "))");

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("StatusConfirmed", DbType.Int32, (int)ActionStatusEnum.Confirmed),
                        new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                        new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)EpisodeCaseEnum.EmergencyOutPatient),
                        new StoredProcInParam("InPatient", DbType.Int32, (int)EpisodeCaseEnum.InPatient),
                        new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int[] GetAllRelatedRequests(int[] corIDs)
        {
            try
            {
                if ((corIDs == null) || (corIDs.Length <= 0))
                    return null;

                string thisquery = string.Concat("SELECT DISTINCT RCOR.[ID] FROM CustomerOrderRequest RCOR", Environment.NewLine,
                                        "WHERE RCOR.ParentCustomerOrderRequestID IN ", Environment.NewLine,
                                        "(SELECT DISTINCT COR.ParentCustomerOrderRequestID ", Environment.NewLine,
                                        " FROM CustomerOrderRequest COR WHERE COR.ParentCustomerOrderRequestID > 0", Environment.NewLine,
                                        " AND (COR.ID IN (", StringUtils.BuildIDString(corIDs), ")))", Environment.NewLine,
                                        "UNION ", Environment.NewLine,
                                        "SELECT DISTINCT COR.[ID] FROM CustomerOrderRequest COR", Environment.NewLine,
                                        "WHERE (COR.ID IN (", StringUtils.BuildIDString(corIDs), "))");

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(thisquery))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public bool ExistsByCustomerEpisodeID(int customerEpisodeID, int[] statusList)
        {
            try
            {
                string sqlQuery = String.Empty;
                if ((statusList != null) && (statusList.Length > 0))
                    sqlQuery = string.Concat(SQLProvider.ExistsCustomerOrderRequestByCustomerEpisodeIDCommand, Environment.NewLine,
                    "AND ([Status] IN (", StringUtils.BuildIDString(statusList), "))");
                else
                    sqlQuery = SQLProvider.ExistsCustomerOrderRequestByCustomerEpisodeIDCommand;

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    return (IsEmptyReader(reader)) ? false : true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool ExistsByCustomerEpisodeIDIgnoredIDs(int customerEpisodeID, int[] ignoredIDs, int[] statusList)
        {
            try
            {
                string sqlQuery = String.Empty;
                if ((statusList != null) && (statusList.Length > 0))
                    sqlQuery = string.Concat(SQLProvider.ExistsCustomerOrderRequestByCustomerEpisodeIDCommand, Environment.NewLine,
                    "AND ([Status] IN (", StringUtils.BuildIDString(statusList), "))");
                else
                    sqlQuery = SQLProvider.ExistsCustomerOrderRequestByCustomerEpisodeIDCommand;

                if ((ignoredIDs != null) && (ignoredIDs.Length > 0))
                    sqlQuery += string.Concat("AND NOT([ID] IN (", StringUtils.BuildIDString(ignoredIDs), ")) ");

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    return (IsEmptyReader(reader)) ? false : true;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public int UpdateStatusByCustomerEpisodeID(int customerEpisodeID, string modifiedBy,
            ActionStatusEnum status,
            //ESTO VIENE DE EFARMACO Y DE TRASLADOS TEMPORALES
            IEnumerable<int> nonAbortableProcedures = null
            )
        {
            try
            {

                IEnumerable<int> procedureIDs = nonAbortableProcedures ?? Enumerable.Empty<int>();

                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerOrderRequestStatusByCustomerEpisodeIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)),

                    new StoredProcInTVPIntegerParam("TVPTable", procedureIDs),


                    new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public bool ExistsByLocationID(int locationID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerOrderRequestByLocationIDCommand,
                    new StoredProcInParam("RequestedLocationID", DbType.Int32, locationID)))
                {
                    return (!IsEmptyReader(reader));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public DataSet GetCustomerOrderRequestIDsByCustomerReservationIDWithoutCustomerEpisodeID(int customerReservationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestIDsByCustomerReservationIDWithoutCustomerEpisodeIDCommand,
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerReservationID", DbType.Int32, customerReservationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestIDsByCustomerReservationIDCustomerEpisodeID(int customerReservationID, int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestIDsByCustomerReservationIDCustomerEpisodeIDCommand,
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerReservationID", DbType.Int32, customerReservationID),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public bool AbortAutomaticCustomerOrderRequestFinalizeOnLeaving(int customerEpisodeID, int reasonChangeID, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.AbortAutomaticCustomerOrderRequestFinalizeOnLeavingCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ReasonChangeID", DbType.Int32, reasonChangeID),
                    new StoredProcInParam("PendingStatus", DbType.Int32, ActionStatusEnum.Pending),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, ActionStatusEnum.Confirmed),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, ActionStatusEnum.Completed),
                    new StoredProcInParam("CancelledStatus", DbType.Int32, ActionStatusEnum.Cancelled),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public DataSet GetCustomerOrderRequestByCustomerReservationID(int customerReservationID)
        {
            try
            {

                string finalQuery = SQLProvider.GetCustomerOrderRequestByCustomerReservationCommand;
                string childQuery = SQLProvider.GetChildCustomerOrderRequestByCustomerReservationCommand;

                finalQuery = string.Concat(finalQuery, Environment.NewLine, "UNION", Environment.NewLine, childQuery);
                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    Administrative.Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerReservationID", DbType.Int32, customerReservationID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public bool ExistsPolicyTypeByCustomerOrderRequest(int policyTypeID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.ExistsPolicyTypeByCustomerOrderRequestCommand, Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("PolicyTypeID", DbType.Int32, policyTypeID));
                return (result.Tables[Entities.TableNames.CustomerOrderRequestTable].Rows.Count > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public IEnumerable<int> GetCompletedCustomerOrderRequest(int customerAssistancePlanID, int reasonChangeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetCompletedCustomerOrderRequestByReasonChangeCommand,
                    new StoredProcInParam("CustomerAssistancePlanID", DbType.Int32, customerAssistancePlanID),
                    new StoredProcInParam("ReasonChangeID", DbType.Int32, reasonChangeID)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public IEnumerable<int> GetCompletedCustomerOrderRequestNotAppointmentByParentCustomerOrderRequestID(
            int parentCustomerOrderRequestID, int corReasonChangeID, int caiReasonChangeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetCompletedCustomerOrderRequestNotAppointmentByReasonChangeParentCustomerOrderRequestIDCommand,
                    new StoredProcInParam("ParentCustomerOrderRequestID", DbType.Int32, parentCustomerOrderRequestID),
                    new StoredProcInParam("CORReasonChangeID", DbType.Int32, corReasonChangeID),
                    new StoredProcInParam("CAIReasonChangeID", DbType.Int32, caiReasonChangeID),
                    new StoredProcInParam("StatusCancelled", DbType.Int32, (int)ActionStatusEnum.Cancelled),
                    new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)EpisodeCaseEnum.EmergencyOutPatient),
                    new StoredProcInParam("InPatient", DbType.Int32, (int)EpisodeCaseEnum.InPatient),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public IEnumerable<int> GetCompletedCustomerOrderRequestNotAppointmentByCustomerOrderRequestID(int customerOrderRequestID,
            int corReasonChangeID, int caiReasonChangeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetCompletedCustomerOrderRequestNotAppointmentByReasonChangeCustomerOrderRequestIDCommand,
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID),
                    new StoredProcInParam("CORReasonChangeID", DbType.Int32, corReasonChangeID),
                    new StoredProcInParam("CAIReasonChangeID", DbType.Int32, caiReasonChangeID),
                    new StoredProcInParam("StatusCancelled", DbType.Int32, (int)ActionStatusEnum.Cancelled),
                    new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)EpisodeCaseEnum.EmergencyOutPatient),
                    new StoredProcInParam("InPatient", DbType.Int32, (int)EpisodeCaseEnum.InPatient),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        result.Add(reader["ID"] as int? ?? 0);
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public ActionStatusEnum GetCustomerOrderRequestStatus(int id)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetCustomerOrderRequestStatusCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)))
                {
                    return !IsEmptyReader(reader)
                        ? (ActionStatusEnum)(reader["Status"] as Int16? ?? 0)
                        : ActionStatusEnum.None;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return ActionStatusEnum.None;
            }
        }

        public DataSet GetVirtualCustomerReports(DateTime? fromDate, DateTime toDate, int customerID, int[] processChartIDs, int elementTypeID)
        {
            try
            {
                String whereFilterByCustomerID = string.Empty;
                if (customerID > 0)
                    whereFilterByCustomerID = "AND COR.CustomerID=@CustomerID";

                String whereFilterByProcessChartIDs = String.Empty;
                if ((processChartIDs != null)
                    && (processChartIDs.Length > 0))
                {
                    whereFilterByProcessChartIDs = String.Join(",", Array.ConvertAll(processChartIDs, new Converter<int, string>(m => m.ToString())));
                }

                int[] steps = new int[] { (int)BasicProcessStepsEnum.Reception, (int)BasicProcessStepsEnum.Admission, (int)BasicProcessStepsEnum.Transfer };
                String whereFilterBySteps = String.Join(",", Array.ConvertAll(steps, new Converter<int, string>(m => m.ToString())));

                string finalQuery = String.Format(String.Concat(SQLProvider.GetVirtualCustomerReportsFromCustomerOrderRequestCommand,
                                                !String.IsNullOrEmpty(whereFilterByCustomerID) ? whereFilterByCustomerID : String.Empty),
                                    whereFilterByProcessChartIDs,
                                    whereFilterBySteps);

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    Administrative.Entities.TableNames.CustomerReportsTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, toDate),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ElementTypeID", DbType.Int32, elementTypeID),
                    new StoredProcInParam("CustomerLeaveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, (int)ActionStatusEnum.Completed),
                    new StoredProcInParam("PendingStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Pending),
                    new StoredProcInParam("AppElement", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetVirtualCustomerStudyReportRels(DateTime? fromDate, DateTime toDate, int customerID, int[] processChartIDs, int elementTypeID)
        {
            try
            {
                String whereFilterByCustomerID = string.Empty;
                if (customerID > 0)
                    whereFilterByCustomerID = "AND COR.CustomerID=@CustomerID";

                String whereFilterByProcessChartIDs = String.Empty;
                if ((processChartIDs != null)
                    && (processChartIDs.Length > 0))
                {
                    whereFilterByProcessChartIDs = String.Join(",", Array.ConvertAll(processChartIDs, new Converter<int, string>(m => m.ToString())));
                }

                string finalQuery = String.Format(String.Concat(SQLProvider.GetVirtualCustomerStudyReportRelsFromCustomerOrderRequestCommand,
                                                !String.IsNullOrEmpty(whereFilterByCustomerID) ? whereFilterByCustomerID : String.Empty),
                             whereFilterByProcessChartIDs);

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    Administrative.Entities.TableNames.CustomerStudyReportRelTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, toDate),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ElementTypeID", DbType.Int32, elementTypeID),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, (int)ActionStatusEnum.Completed),
                    new StoredProcInParam("AppElement", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetVirtualInterpretationReports(DateTime? fromDate, DateTime toDate, int customerID, int[] processChartIDs, int elementTypeID)
        {
            try
            {
                String whereFilterByCustomerID = string.Empty;
                if (customerID > 0)
                    whereFilterByCustomerID = "AND COR.CustomerID=@CustomerID";

                String whereFilterByProcessChartIDs = String.Empty;
                if ((processChartIDs != null)
                    && (processChartIDs.Length > 0))
                {
                    whereFilterByProcessChartIDs = String.Join(",", Array.ConvertAll(processChartIDs, new Converter<int, string>(m => m.ToString())));
                }

                string finalQuery = String.Format(String.Concat(SQLProvider.GetVirtualInterpretationReportsFromCustomerOrderRequestCommand,
                                                !String.IsNullOrEmpty(whereFilterByCustomerID) ? whereFilterByCustomerID : String.Empty),
                             whereFilterByProcessChartIDs);

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    Administrative.Entities.TableNames.InterpretrationReportTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, toDate),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ElementTypeID", DbType.Int32, elementTypeID),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, (int)ActionStatusEnum.Completed),
                    new StoredProcInParam("AppElement", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public bool ExitsByAssistanceServiceID(int assistanceServiceID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.ExistsCustomerOrderRequestByAssistanceServiceIDCommand,
                    new StoredProcInParam("AssistanceServiceID", DbType.Int32, assistanceServiceID)))
                {
                    return (!IsEmptyReader(reader));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool CancelOrDeleteCustomerOrderRequestIsPossible(int customerAppointmentInformationID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.CancelOrDeleteCustomerOrderRequestsIsPossibleByCustomerAppointmentInformationCommand,
                    new StoredProcInParam("ID", DbType.Int32, customerAppointmentInformationID),
                    new StoredProcInParam("MedicalOrderAppointmentElement", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("CancelledStatus", DbType.Int32, (int)ActionStatusEnum.Cancelled),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, (int)ActionStatusEnum.Completed),
                    new StoredProcInParam("SupercededStatus", DbType.Int32, (int)ActionStatusEnum.Superceded)))
                {
                    return (!IsEmptyReader(reader));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool ExistsCancelOrSupercededCustomerOrderRequests(int customerAppointmentInformationID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCancelOrSupercededCustomerOrderRequestsByCustomerAppointmentInformationCommand,
                    new StoredProcInParam("ID", DbType.Int32, customerAppointmentInformationID),
                    new StoredProcInParam("MedicalOrderAppointmentElement", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("CancelledStatus", DbType.Int32, (int)ActionStatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int32, (int)ActionStatusEnum.Superceded)))
                {
                    return (!IsEmptyReader(reader));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool ExistsCompletedCustomerOrderRequests(int customerAppointmentInformationID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCompletedCustomerOrderRequestsByCustomerAppointmentInformationCommand,
                    new StoredProcInParam("ID", DbType.Int32, customerAppointmentInformationID),
                    new StoredProcInParam("MedicalOrderAppointmentElement", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, (int)ActionStatusEnum.Completed)))
                {
                    return (!IsEmptyReader(reader));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        /// <summary>
        /// esta es la nsima forma de traer a una vista una entidad completa para presentar en el censo de solicitudes.
        /// </summary>
        public DataSet GetFilteringCustomerOrderRequests(Entities.CustomerProcessSpecification filter, int maxRows,
            HeldCustomerOrderRequestSpecification heldfilter = null, TraceCustomerOrderRequestSpecification tracefilter = null)
        {
            if (filter == null || filter.ProcessChartIDs == null || filter.CareCenterIDs == null || filter.ProcessChartIDs.Length <= 0 ||
                filter.CareCenterIDs.Length <= 0 || filter.StartDateTime == null || filter.EndDateTime == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                SII.HCD.BackOffice.DA.CustomerOrderRequestQueryBuilder _builder = new BackOffice.DA.CustomerOrderRequestQueryBuilder();

                string sqlQuery = string.Empty;
                if (filter.Containing == CustomerOrderRequestContainingEnum.All)
                {
                    //esto se ha hecho para evitar duplicidades cuando después se llama a los solicitudes con recepción

                    Entities.CustomerProcessSpecification filterclonado = filter.Clone() as Entities.CustomerProcessSpecification;
                    filterclonado.Containing = CustomerOrderRequestContainingEnum.OnlyEpisode;
                    sqlQuery = _builder.GetOrderListSingleQuery(filterclonado, true, heldfilter, tracefilter);

                    filterclonado = filter.Clone() as Entities.CustomerProcessSpecification;
                    filterclonado.Containing = CustomerOrderRequestContainingEnum.Citation;
                    if (filterclonado.ApplyTo.In(CustomerOrderRequestApplyToEnum.RequestPlanningDate))
                    {
                        filterclonado.ApplyTo = CustomerOrderRequestApplyToEnum.CitationDate;
                    }
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "UNION", Environment.NewLine, _builder.GetOrderListSingleQuery(filterclonado, true, heldfilter, tracefilter));


                    filterclonado = filter.Clone() as Entities.CustomerProcessSpecification;
                    if (heldfilter == null)
                    {
                        if (filterclonado.ApplyTo.In(CustomerOrderRequestApplyToEnum.RequestPlanningDate))
                        {
                            filterclonado.ApplyTo = CustomerOrderRequestApplyToEnum.ReservationDate;
                        }
                        filterclonado.Containing = CustomerOrderRequestContainingEnum.Reservation;
                        sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "UNION", Environment.NewLine, _builder.GetOrderListSingleQuery(filterclonado, true, heldfilter, tracefilter));
                    }

                    filterclonado = filter.Clone() as Entities.CustomerProcessSpecification;
                    filterclonado.Containing = CustomerOrderRequestContainingEnum.Reception;
                    if (filterclonado.ApplyTo.In(CustomerOrderRequestApplyToEnum.RequestPlanningDate))
                    {
                        filterclonado.ApplyTo = CustomerOrderRequestApplyToEnum.CitationDate;
                    }
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "UNION", Environment.NewLine, _builder.GetOrderListSingleQuery(filterclonado, true, heldfilter, tracefilter));
                    sqlQuery = string.Concat("(SELECT DISTINCT TOP(@MaxRecords) TT2.* FROM (", Environment.NewLine,
                                        sqlQuery,
                                        ") TT2 ORDER BY TT2.CustomerOrderRequestID) TT ");
                    //filter.Containing = CustomerOrderRequestContainingEnum.All;
                }
                else
                {
                    sqlQuery = string.Concat("(", Environment.NewLine,
                                        _builder.GetOrderListSingleQuery(filter, true, heldfilter, tracefilter),
                                        ") TT ");
                }

                sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                    "JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON TT.CustomerOrderRequestID=ORSP.CustomerOrderRequestID", Environment.NewLine,
                    "JOIN ProcessChart PCHT WITH(NOLOCK) ON TT.ProcessChartID = PCHT.[ID]", Environment.NewLine,
                    "JOIN EpisodeType ET WITH(NOLOCK) ON PCHT.EpisodeConfigID = ET.[ID]", Environment.NewLine,
                    "JOIN CareCenter CCPC WITH(NOLOCK) ON TT.CareCenterID = CCPC.[ID]", Environment.NewLine,
                    "JOIN Organization CCPCORG WITH(NOLOCK) ON CCPC.OrganizationID=CCPCORG.[ID]", Environment.NewLine,
                    "JOIN Customer C WITH(NOLOCK) ON TT.CustomerID=C.[ID]", Environment.NewLine,
                    "JOIN Person CUSTP WITH(NOLOCK) ON C.PersonID=CUSTP.[ID]", Environment.NewLine,
                    "JOIN SensitiveData CSD WITH(NOLOCK) ON CUSTP.[ID]=CSD.PersonID", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest CORP WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=CORP.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON TT.CustomerEpisodeID=CE.[ID]  AND NOT(CE.[Status] = ", ((int)(StatusEnum.Superceded)).ToString(), ")", Environment.NewLine,
                    "LEFT JOIN Insurer AdmI WITH(NOLOCK) ON CE.AdmissionInsurerID=AdmI.[ID] ", Environment.NewLine,
                    "LEFT JOIN Organization AdmO WITH(NOLOCK) ON AdmI.OrganizationID=AdmO.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerPolicy CPol WITH(NOLOCK) ON CE.CustomerPolicyID=CPol.[ID] ", Environment.NewLine,
                    "LEFT JOIN PolicyType PolT WITH(NOLOCK) ON CPol.PolicyTypeID=PolT.[ID] ", Environment.NewLine,
                    "LEFT JOIN Physician CEPH WITH(NOLOCK) ON CE.PhysicianID=CEPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN Person CEPPH WITH(NOLOCK) ON CEPH.PersonID=CEPPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "LEFT JOIN Location ADMLOC  WITH(NOLOCK) ON CA.CurrentLocationID = ADMLOC.ID ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID=CESR.CustomerEpisodeID AND CESR.Step = ", ((int)BasicProcessStepsEnum.Admission).ToString(), Environment.NewLine,
                    "LEFT JOIN AssistanceService CESRASS WITH(NOLOCK) ON CESR.AssistanceServiceID = CESRASS.ID", Environment.NewLine,
                    "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID] AND CRCH.CareCenterID = TT.CareCenterID ", Environment.NewLine,
                    "LEFT JOIN Person RPHP WITH(NOLOCK) ON TT.RequestedPersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Physician RPhy WITH(NOLOCK) ON RPhy.PersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Location LOC WITH(NOLOCK) ON TT.RequestedLocationID=LOC.[ID]", Environment.NewLine,
                    "LEFT JOIN Insurer IR WITH(NOLOCK) ON TT.RequestedInsurerID=IR.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID=ORG.[ID]", Environment.NewLine,
                    "LEFT JOIN PolicyType PT WITH(NOLOCK) ON TT.PolicyTypeID=PT.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CC WITH(NOLOCK) ON TT.RequestedCareCenterID=CC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID=CCORG.[ID]", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=ADTCOR.ID", Environment.NewLine,
                    "LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.ID=ADTORP.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.ID=ADTInfo.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON TT.CustomerOrderRequestID=PR.CustomerOrderRequestID AND NOT(PR.[Status]=", ((int)ActionStatusEnum.Superceded).ToString(), ")", Environment.NewLine,
                    "LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.ID=ORCPR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerProcedure CPR WITH(NOLOCK) ON ORCPR.CustomerProcedureID = CPR.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerRoutine CRR WITH(NOLOCK) ON ORCRR.CustomerRoutineID = CRR.[ID]", Environment.NewLine,
                    "LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=TT.MedicalSpecialtyID", Environment.NewLine,
                    "LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]", Environment.NewLine,
                    "LEFT JOIN TimePattern DUR WITH(NOLOCK) ON ORSP.EstimatedDurationID=DUR.[ID]", Environment.NewLine,
                    "LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON TT.AssistanceServiceID=ASS.ID", Environment.NewLine,
                    "LEFT JOIN AppointmentService APPS WITH(NOLOCK) ON APPS.ID = TT.AppointmentServiceID AND TT.CustomerOrderRequestID=APPS.ElementID ", Environment.NewLine,
                    "           AND APPS.AppointmentElement=", ((int)AppointmentElementEnum.MedicalOrder).ToString(), Environment.NewLine,
                    "LEFT JOIN CustomerAppointmentInformation CAI WITH(NOLOCK) ON APPS.CustomerAppointmentInformationID=CAI.[ID] ", Environment.NewLine,
                    "LEFT JOIN Location LSCH WITH(NOLOCK) ON CAI.ResourceElement=", ((int)AppointmentResourceElementEnum.Location).ToString(), " AND CAI.ResourceID=LSCH.[ID]", Environment.NewLine,
                    "LEFT JOIN Equipment ESCH WITH(NOLOCK) ON CAI.ResourceElement=", ((int)AppointmentResourceElementEnum.Equipment).ToString(), " AND CAI.ResourceID=ESCH.[ID]", Environment.NewLine,
                    "LEFT JOIN CustomerCitation CT WITH(NOLOCK) ON  CT.ID = TT.CustomerCitationID AND CAI.CustomerCitationID=CT.[ID]", Environment.NewLine,
                    "                    			AND CT.CustomerProcessID = TT.CustomerProcessID", Environment.NewLine,
                    "LEFT JOIN CustomerReception CRN WITH(NOLOCK) ON CRN.ID = TT.CustomerReceptionID AND CRN.CustomerEpisodeID = TT.CustomerEpisodeID ", Environment.NewLine,
                    "           AND CRN.CustomerProcessID = TT.CustomerProcessID AND NOT(CRN.[Status] = ", ((int)(StatusEnum.Superceded)).ToString(), ")", Environment.NewLine,
                   "LEFT JOIN (SELECT RAI2.*, RIAR.CustomerAppointmentInformationID, RIAR.AppointmentServiceID ", Environment.NewLine,
                   "           FROM ReceptionAdditionalInfo RAI2 WITH(NOLOCK) ", Environment.NewLine,
                   "               JOIN ReceptionInfoAppointmentRel RIAR WITH(NOLOCK) ON RAI2.[ID]=RIAR.ReceptionAdditionalInfoID AND ", Environment.NewLine,
                   "                     ((RIAR.StepType = 128 AND  RIAR.CustomerAppointmentInformationID > 0) OR (RIAR.StepType = 0 AND  RIAR.CustomerAppointmentInformationID = 0))", Environment.NewLine,
                   "           WHERE (RAI2.CustomerReceptionID > 0) AND (RAI2.[Status] IN (1,4,8))", Environment.NewLine,
                   "           ) AS RAI ON RAI.AppointmentServiceID=APPS.ID AND RAI.CustomerAppointmentInformationID = APPS.CustomerAppointmentInformationID ", Environment.NewLine,
                   "           AND RAI.CustomerReceptionID = CRN.[ID]");
                
                //if (!String.IsNullOrWhiteSpace(filter.CHNumber)
                //    || !String.IsNullOrWhiteSpace(filter.EpisodeNumber))
                //{
                //    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "WHERE");

                //    string sqlWhere = string.Empty;

                //    if (!String.IsNullOrWhiteSpace(filter.CHNumber))
                //    {
                //        sqlWhere = " @CHNumber = CASE WHEN NOT(CRCH.ID IS NULL) THEN CRCH.CHNumber ELSE C.CHNumber END";

                //        _params.Add(new StoredProcInParam("CHNumber", DbType.String, filter.CHNumber));
                //    }

                //    if (!String.IsNullOrWhiteSpace(filter.EpisodeNumber))
                //    {
                //        if (!String.IsNullOrWhiteSpace(sqlWhere))
                //            sqlWhere = string.Concat(sqlWhere, " AND");

                //        sqlWhere = string.Concat(sqlWhere, " CE.EpisodeNumber = @EpisodeNumber");

                //        _params.Add(new StoredProcInParam("EpisodeNumber", DbType.String, filter.EpisodeNumber));
                //    }

                //    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, sqlWhere);
                //}

                _params.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, filter.StartDateTime.Value));
                _params.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, filter.EndDateTime.Value));

                sqlQuery = string.Concat(SQLProvider.SelectDistinctTopNFilteringCustomerOrderRequestBasicJoinStandardCommand, Environment.NewLine,
                            sqlQuery, Environment.NewLine);
                _params.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRows));

                _params.Add(new StoredProcInParam("Aborted", DbType.Int32, ActionStatusEnum.Aborted));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    Administrative.Entities.TableNames.CustomerOrderRequestTable,
                    _params.ToArray()
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringCustomerOrderRequests(OrderRequestByCustomerEpisodeSpecification filter, int maxRows)
        {
            if (filter == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRows));

                CustomerOrderRequestByCustomerEpisodeQueryBuilder _builder = new CustomerOrderRequestByCustomerEpisodeQueryBuilder();
                string sqlSubQuery = string.Concat("(", Environment.NewLine, _builder.GetOrderListSingleQuery(filter), ") TT ");

                string joinQuery = string.Concat(sqlSubQuery, Environment.NewLine,
                    "JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON TT.CustomerOrderRequestID=ORSP.CustomerOrderRequestID", Environment.NewLine,
                    "JOIN ProcessChart PCHT WITH(NOLOCK) ON TT.ProcessChartID = PCHT.[ID]", Environment.NewLine,
                    "JOIN EpisodeType ET WITH(NOLOCK) ON PCHT.EpisodeConfigID = ET.[ID]", Environment.NewLine,
                    "JOIN CareCenter CCPC WITH(NOLOCK) ON TT.CareCenterID = CCPC.[ID]", Environment.NewLine,
                    "JOIN Organization CCPCORG WITH(NOLOCK) ON CCPC.OrganizationID=CCPCORG.[ID]", Environment.NewLine,
                    "JOIN Customer C WITH(NOLOCK) ON TT.CustomerID=C.[ID]", Environment.NewLine,
                    "JOIN Person CUSTP WITH(NOLOCK) ON C.PersonID=CUSTP.[ID]", Environment.NewLine,
                    "JOIN SensitiveData CSD WITH(NOLOCK) ON CUSTP.[ID]=CSD.PersonID", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest CORP WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=CORP.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON TT.CustomerEpisodeID=CE.[ID] ", Environment.NewLine,
                    "LEFT JOIN Insurer AdmI WITH(NOLOCK) ON CE.AdmissionInsurerID=AdmI.[ID] ", Environment.NewLine,
                    "LEFT JOIN Organization AdmO WITH(NOLOCK) ON AdmI.OrganizationID=AdmO.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerPolicy CPol WITH(NOLOCK) ON CE.CustomerPolicyID=CPol.[ID] ", Environment.NewLine,
                    "LEFT JOIN PolicyType PolT WITH(NOLOCK) ON CPol.PolicyTypeID=PolT.[ID] ", Environment.NewLine,
                    "LEFT JOIN Physician CEPH WITH(NOLOCK) ON CE.PhysicianID=CEPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN Person CEPPH WITH(NOLOCK) ON CEPH.PersonID=CEPPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "LEFT JOIN Location ADMLOC  WITH(NOLOCK) ON CA.CurrentLocationID = ADMLOC.ID ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID=CESR.CustomerEpisodeID AND CESR.Step = ", ((int)BasicProcessStepsEnum.Admission).ToString(), Environment.NewLine,
                    "LEFT JOIN AssistanceService CESRASS WITH(NOLOCK) ON CESR.AssistanceServiceID = CESRASS.ID", Environment.NewLine,
                    "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID] AND CRCH.CareCenterID = TT.CareCenterID ", Environment.NewLine,
                    "LEFT JOIN Person RPHP WITH(NOLOCK) ON TT.RequestedPersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Physician RPhy WITH(NOLOCK) ON RPhy.PersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Location LOC WITH(NOLOCK) ON TT.RequestedLocationID=LOC.[ID]", Environment.NewLine,
                    "LEFT JOIN Insurer IR WITH(NOLOCK) ON TT.RequestedInsurerID=IR.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID=ORG.[ID]", Environment.NewLine,
                    "LEFT JOIN PolicyType PT WITH(NOLOCK) ON TT.PolicyTypeID=PT.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CC WITH(NOLOCK) ON TT.RequestedCareCenterID=CC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID=CCORG.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.ID=ORCPR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerProcedure CPR WITH(NOLOCK) ON ORCPR.CustomerProcedureID = CPR.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerRoutine CRR WITH(NOLOCK) ON ORCRR.CustomerRoutineID = CRR.[ID]", Environment.NewLine,
                    "LEFT JOIN Location LSCH WITH(NOLOCK) ON TT.ResourceElement=", ((int)AppointmentResourceElementEnum.Location).ToString(), " AND TT.ResourceID=LSCH.[ID]", Environment.NewLine,
                    "LEFT JOIN Equipment ESCH WITH(NOLOCK) ON TT.ResourceElement=", ((int)AppointmentResourceElementEnum.Equipment).ToString(), " AND TT.ResourceID=ESCH.[ID]", Environment.NewLine,
                    "LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=TT.MedicalSpecialtyID", Environment.NewLine,
                    "LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]", Environment.NewLine,
                    "LEFT JOIN TimePattern DUR WITH(NOLOCK) ON ORSP.EstimatedDurationID=DUR.[ID]", Environment.NewLine,
                    "LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON TT.AssistanceServiceID=ASS.ID", Environment.NewLine,
                    "LEFT JOIN CustomerAppointmentInformation CAI WITH(NOLOCK) ON TT.AppointmentInformationID=CAI.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerAppointmentInfoReasonRel CAIRR WITH(NOLOCK) ON CAI.[ID]=CAIRR.CustomerAppointmentInformationID ", Environment.NewLine,
                    "LEFT JOIN ReasonChange RCCAI WITH(NOLOCK) ON CAIRR.ReasonChangeID=RCCAI.ID ", Environment.NewLine,
                    "LEFT JOIN AppointmentService APPS WITH(NOLOCK) ON APPS.ID=TT.AppointmentServiceID AND TT.CustomerOrderRequestID=APPS.ElementID", Environment.NewLine,
                    "           AND APPS.AppointmentElement=", ((int)AppointmentElementEnum.MedicalOrder).ToString(), Environment.NewLine,
                    "LEFT JOIN CustomerCitation CT WITH(NOLOCK) ON CAI.CustomerCitationID=CT.[ID] ", Environment.NewLine,
                    "           AND CT.CustomerProcessID=TT.CustomerProcessID  AND NOT(CT.ID IS NULL)", Environment.NewLine,
                    "LEFT JOIN CustomerReception CRN WITH(NOLOCK) ON CRN.CustomerEpisodeID=TT.CustomerEpisodeID ", Environment.NewLine,
                    "           AND CRN.CustomerProcessID=TT.CustomerProcessID AND NOT(CRN.ID IS NULL)", Environment.NewLine,
                    "LEFT JOIN (SELECT RAI2.*, RIAR.CustomerAppointmentInformationID, RIAR.AppointmentServiceID ", Environment.NewLine,
                    "           FROM ReceptionAdditionalInfo RAI2 WITH(NOLOCK) ", Environment.NewLine,
                    "               JOIN ReceptionInfoAppointmentRel RIAR WITH(NOLOCK) ON RAI2.[ID]=RIAR.ReceptionAdditionalInfoID AND ", Environment.NewLine,
                    "                     ((RIAR.StepType = 128 AND  RIAR.CustomerAppointmentInformationID > 0) OR (RIAR.StepType = 0 AND  RIAR.CustomerAppointmentInformationID = 0))", Environment.NewLine,
                    "           WHERE (RAI2.CustomerReceptionID > 0) AND (RAI2.[Status] IN (1,4,8))", Environment.NewLine,
                    "           ) AS RAI ON RAI.AppointmentServiceID=APPS.ID AND RAI.CustomerAppointmentInformationID = APPS.CustomerAppointmentInformationID ", Environment.NewLine,
                    "           AND RAI.CustomerReceptionID = CRN.[ID] ");


                string fullQuery = string.Concat(SQLProvider.SelectDistinctTopNFilteringCustomerOrderRequestDTOByCitationMedicalOrderCommand, Environment.NewLine,
                            joinQuery, Environment.NewLine);

                _builder.SetOrderListParams(filter, _params);

                return this.Gateway.ExecuteQueryDataSet(fullQuery,
                    Administrative.Entities.TableNames.CustomerOrderRequestTable,
                    _params.ToArray()
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringCustomerOrderRequests(CitationMedicalOrderSpecification filter, int maxRows, string addinName = null)
        {
            if (filter == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRows));

                CitationMedicalOrderQueryBuilder _builder = new CitationMedicalOrderQueryBuilder();
                string sqlSubQuery = string.Concat("(", Environment.NewLine,
                    _builder.GetCitationMedicalOrderListSingleQuery(filter, _params), ") TT ");

                string joinQuery = string.Concat(sqlSubQuery, Environment.NewLine,
                    "JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON TT.CustomerOrderRequestID=ORSP.CustomerOrderRequestID", Environment.NewLine,
                    "JOIN ProcessChart PCHT WITH(NOLOCK) ON TT.ProcessChartID = PCHT.[ID]", Environment.NewLine,
                    "JOIN EpisodeType ET WITH(NOLOCK) ON PCHT.EpisodeConfigID = ET.[ID]", Environment.NewLine,
                    "JOIN CareCenter CCPC WITH(NOLOCK) ON TT.CareCenterID = CCPC.[ID]", Environment.NewLine,
                    "JOIN Organization CCPCORG WITH(NOLOCK) ON CCPC.OrganizationID=CCPCORG.[ID]", Environment.NewLine,
                    "JOIN Customer C WITH(NOLOCK) ON TT.CustomerID=C.[ID]", Environment.NewLine,
                    "JOIN Person CUSTP WITH(NOLOCK) ON C.PersonID=CUSTP.[ID]", Environment.NewLine,
                    "JOIN SensitiveData CSD WITH(NOLOCK) ON CUSTP.[ID]=CSD.PersonID", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest CORP WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=CORP.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON TT.CustomerEpisodeID=CE.[ID] ", Environment.NewLine,
                    "LEFT JOIN Insurer AdmI WITH(NOLOCK) ON CE.AdmissionInsurerID=AdmI.[ID] ", Environment.NewLine,
                    "LEFT JOIN Organization AdmO WITH(NOLOCK) ON AdmI.OrganizationID=AdmO.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerPolicy CPol WITH(NOLOCK) ON CE.CustomerPolicyID=CPol.[ID] ", Environment.NewLine,
                    "LEFT JOIN PolicyType PolT WITH(NOLOCK) ON CPol.PolicyTypeID=PolT.[ID] ", Environment.NewLine,
                    "LEFT JOIN Physician CEPH WITH(NOLOCK) ON CE.PhysicianID=CEPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN Person CEPPH WITH(NOLOCK) ON CEPH.PersonID=CEPPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "LEFT JOIN Location ADMLOC  WITH(NOLOCK) ON CA.CurrentLocationID = ADMLOC.ID ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID=CESR.CustomerEpisodeID AND CESR.Step = ", ((int)BasicProcessStepsEnum.Admission).ToString(), Environment.NewLine,
                    "LEFT JOIN AssistanceService CESRASS WITH(NOLOCK) ON CESR.AssistanceServiceID = CESRASS.ID", Environment.NewLine,
                    "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID] AND CRCH.CareCenterID = TT.CareCenterID ", Environment.NewLine,
                    "LEFT JOIN Person RPHP WITH(NOLOCK) ON TT.RequestedPersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Physician RPhy WITH(NOLOCK) ON RPhy.PersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Location LOC WITH(NOLOCK) ON TT.RequestedLocationID=LOC.[ID]", Environment.NewLine,
                    "LEFT JOIN Insurer IR WITH(NOLOCK) ON TT.RequestedInsurerID=IR.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID=ORG.[ID]", Environment.NewLine,
                    "LEFT JOIN PolicyType PT WITH(NOLOCK) ON TT.PolicyTypeID=PT.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CC WITH(NOLOCK) ON TT.RequestedCareCenterID=CC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID=CCORG.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.ID=ORCPR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerProcedure CPR WITH(NOLOCK) ON ORCPR.CustomerProcedureID = CPR.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerRoutine CRR WITH(NOLOCK) ON ORCRR.CustomerRoutineID = CRR.[ID]", Environment.NewLine,
                    "LEFT JOIN Location LSCH WITH(NOLOCK) ON TT.ResourceElement=", ((int)AppointmentResourceElementEnum.Location).ToString(), " AND TT.ResourceID=LSCH.[ID]", Environment.NewLine,
                    "LEFT JOIN Equipment ESCH WITH(NOLOCK) ON TT.ResourceElement=", ((int)AppointmentResourceElementEnum.Equipment).ToString(), " AND TT.ResourceID=ESCH.[ID]", Environment.NewLine,
                    "LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=TT.MedicalSpecialtyID", Environment.NewLine,
                    "LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]", Environment.NewLine,
                    "LEFT JOIN TimePattern DUR WITH(NOLOCK) ON ORSP.EstimatedDurationID=DUR.[ID]", Environment.NewLine,
                    "LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON TT.AssistanceServiceID=ASS.ID", Environment.NewLine,
                    "LEFT JOIN CustomerAppointmentInformation CAI WITH(NOLOCK) ON TT.AppointmentInformationID=CAI.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerAppointmentInfoReasonRel CAIRR WITH(NOLOCK) ON CAI.[ID]=CAIRR.CustomerAppointmentInformationID ", Environment.NewLine,
                    "LEFT JOIN ReasonChange RCCAI WITH(NOLOCK) ON CAIRR.ReasonChangeID=RCCAI.ID ", Environment.NewLine,
                    "LEFT JOIN AppointmentService APPS WITH(NOLOCK) ON APPS.ID=TT.AppointmentServiceID AND TT.CustomerOrderRequestID=APPS.ElementID", Environment.NewLine,
                    "           AND APPS.AppointmentElement=", ((int)AppointmentElementEnum.MedicalOrder).ToString(), Environment.NewLine,
                    "LEFT JOIN CustomerCitation CT WITH(NOLOCK) ON CAI.CustomerCitationID=CT.[ID] ", Environment.NewLine,
                    "           AND CT.CustomerProcessID=TT.CustomerProcessID  AND NOT(CT.ID IS NULL)", Environment.NewLine,
                    "LEFT JOIN CustomerReception CRN WITH(NOLOCK) ON CRN.CustomerEpisodeID=TT.CustomerEpisodeID ", Environment.NewLine,
                    "           AND CRN.CustomerProcessID=TT.CustomerProcessID AND NOT(CRN.ID IS NULL)", Environment.NewLine,
                    "LEFT JOIN (SELECT RAI2.*, RIAR.CustomerAppointmentInformationID, RIAR.AppointmentServiceID ", Environment.NewLine,
                    "           FROM ReceptionAdditionalInfo RAI2 WITH(NOLOCK) ", Environment.NewLine,
                    "               JOIN ReceptionInfoAppointmentRel RIAR WITH(NOLOCK) ON RAI2.[ID]=RIAR.ReceptionAdditionalInfoID AND ", Environment.NewLine,
                    "                     ((RIAR.StepType = 128 AND  RIAR.CustomerAppointmentInformationID > 0) OR (RIAR.StepType = 0 AND  RIAR.CustomerAppointmentInformationID = 0))", Environment.NewLine,
                    "           WHERE (RAI2.CustomerReceptionID > 0) AND (RAI2.[Status] IN (1,4,8))", Environment.NewLine,
                    "           ) AS RAI ON RAI.AppointmentServiceID=APPS.ID AND RAI.CustomerAppointmentInformationID = APPS.CustomerAppointmentInformationID ", Environment.NewLine,
                    "           AND RAI.CustomerReceptionID = CRN.[ID] ");


                string fullQuery = string.Concat(SQLProvider.SelectDistinctTopNFilteringCustomerOrderRequestDTOByCitationMedicalOrderCommand, Environment.NewLine,
                            joinQuery, Environment.NewLine);

                _builder.SetOrderListParams(filter, _params, addinName);

                return this.Gateway.ExecuteQueryDataSet(fullQuery,
                    Administrative.Entities.TableNames.CustomerOrderRequestTable,
                    999,
                    _params.ToArray()
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringCustomerOrderRequests(UnplacedRequestFilterSpecification filter, int maxRows, bool onlyplaceds = true, string addinName = null)
        {
            if (filter == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                UnplacedCustomerOrderRequestQueryBuilder _builder = new UnplacedCustomerOrderRequestQueryBuilder();

                string sqlQuery = string.Concat("(", Environment.NewLine,
                                    _builder.GetOrderListSingleQuery(filter, true, onlyplaceds),
                                    ") TT ");

                sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                    "JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON TT.CustomerOrderRequestID=ORSP.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest CORP WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=CORP.[ID] ", Environment.NewLine,
                    "LEFT JOIN ProcessChart PCHT WITH(NOLOCK) ON TT.ProcessChartID = PCHT.[ID]", Environment.NewLine,
                    "LEFT JOIN EpisodeType ET WITH(NOLOCK) ON PCHT.EpisodeConfigID = ET.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CCPC WITH(NOLOCK) ON TT.CareCenterID = CCPC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization CCPCORG WITH(NOLOCK) ON CCPC.OrganizationID=CCPCORG.[ID]", Environment.NewLine,
                    "LEFT JOIN Customer C WITH(NOLOCK) ON TT.CustomerID=C.[ID]", Environment.NewLine,
                    "LEFT JOIN Person CUSTP WITH(NOLOCK) ON C.PersonID=CUSTP.[ID]", Environment.NewLine,
                    "LEFT JOIN SensitiveData CSD WITH(NOLOCK) ON CUSTP.[ID]=CSD.PersonID", Environment.NewLine,
                    "LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON TT.CustomerEpisodeID=CE.[ID] ", Environment.NewLine,
                    "LEFT JOIN Insurer AdmI WITH(NOLOCK) ON CE.AdmissionInsurerID=AdmI.[ID] ", Environment.NewLine,
                    "LEFT JOIN Organization AdmO WITH(NOLOCK) ON AdmI.OrganizationID=AdmO.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerPolicy CPol WITH(NOLOCK) ON CE.CustomerPolicyID=CPol.[ID] ", Environment.NewLine,
                    "LEFT JOIN PolicyType PolT WITH(NOLOCK) ON CPol.PolicyTypeID=PolT.[ID] ", Environment.NewLine,
                    "LEFT JOIN Physician CEPH WITH(NOLOCK) ON CE.PhysicianID=CEPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN Person CEPPH WITH(NOLOCK) ON CEPH.PersonID=CEPPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "LEFT JOIN Location ADMLOC  WITH(NOLOCK) ON CA.CurrentLocationID = ADMLOC.ID ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID=CESR.CustomerEpisodeID AND CESR.Step = ", ((int)BasicProcessStepsEnum.Admission).ToString(), Environment.NewLine,
                    "LEFT JOIN AssistanceService CESRASS WITH(NOLOCK) ON CESR.AssistanceServiceID = CESRASS.ID");
                /*if (filter.IsFilteredByAny(UnplacedRequestFindOptionEnum.CustomerCitationID))
                {*/
                sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                    "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID]");
                /*}
                else
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                        "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID] AND CRCH.CareCenterID=@CareCenterIDToShowCHNumber");
                }*/
                sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                    "LEFT JOIN Person RPHP WITH(NOLOCK) ON TT.RequestedPersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Physician RPhy WITH(NOLOCK) ON RPhy.PersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Location LOC WITH(NOLOCK) ON TT.RequestedLocationID=LOC.[ID]", Environment.NewLine,
                    "LEFT JOIN Insurer IR WITH(NOLOCK) ON TT.RequestedInsurerID=IR.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID=ORG.[ID]", Environment.NewLine,
                    "LEFT JOIN PolicyType PT WITH(NOLOCK) ON TT.PolicyTypeID=PT.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CC WITH(NOLOCK) ON TT.RequestedCareCenterID=CC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID=CCORG.[ID]", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=ADTCOR.ID", Environment.NewLine,
                    "LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.ID=ADTORP.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.ID=ADTInfo.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON TT.CustomerOrderRequestID=PR.CustomerOrderRequestID AND NOT(PR.[Status]=", ((int)ActionStatusEnum.Superceded).ToString(), ")", Environment.NewLine,
                    "LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID]", Environment.NewLine,
                    "LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=TT.MedicalSpecialtyID", Environment.NewLine,
                    "LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]", Environment.NewLine,
                    "LEFT JOIN TimePattern DUR WITH(NOLOCK) ON ORSP.EstimatedDurationID=DUR.[ID]", Environment.NewLine,
                    "LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON TT.AssistanceServiceID=ASS.ID", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.ID=ORCPR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerProcedure CPR WITH(NOLOCK) ON ORCPR.CustomerProcedureID = CPR.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerRoutine CRR WITH(NOLOCK) ON ORCRR.CustomerRoutineID = CRR.[ID]");

                /*if (!String.IsNullOrWhiteSpace(filter.CHNumber)
                    || !String.IsNullOrWhiteSpace(filter.EpisodeNumber))
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "WHERE");

                    string sqlWhere = string.Empty;

                    if (!String.IsNullOrWhiteSpace(filter.CHNumber))
                    {
                        sqlWhere = " @CHNumberAux = CASE WHEN NOT(CRCH.ID IS NULL) THEN CRCH.CHNumber ELSE C.CHNumber END";

                        _params.Add(new StoredProcInParam("CHNumberAux", DbType.String, filter.CHNumber));
                    }

                    if (!String.IsNullOrWhiteSpace(filter.EpisodeNumber))
                    {
                        if (!String.IsNullOrWhiteSpace(sqlWhere))
                            sqlWhere = string.Concat(sqlWhere, " AND");

                        sqlWhere = string.Concat(sqlWhere, " CE.EpisodeNumber = @EpisodeNumber");

                        _params.Add(new StoredProcInParam("EpisodeNumber", DbType.String, filter.EpisodeNumber));
                    }

                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, sqlWhere);
                }*/

                sqlQuery = string.Concat(SQLProvider.SelectDistinctTopNFilteringCustomerOrderRequestBasicJoinStandardUnplacedCommand, Environment.NewLine,
                            sqlQuery, Environment.NewLine);

                _builder.SetOrderListParams(filter, _params, addinName);
                _params.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRows));
                _params.Add(new StoredProcInParam("CareCenterIDToShowCHNumber", DbType.Int32, filter.CareCenterIDToShowCHNumber));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    Administrative.Entities.TableNames.CustomerOrderRequestTable,
                    _params.ToArray()
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringCustomerOrderRequestsForINDIGO(UnplacedRequestFilterSpecification filter, int maxRows, bool onlyplaceds = true, string addinName = null)
        {
            if (filter == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                UnplacedCustomerOrderRequestQueryBuilder _builder = new UnplacedCustomerOrderRequestQueryBuilder();

                string sqlQuery = string.Concat("(", Environment.NewLine,
                                    _builder.GetOrderListSingleQuery(filter, true, onlyplaceds, true),
                                    ") TT ");

                sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                    "JOIN OrderRequestSchPlanning ORSP WITH(NOLOCK) ON TT.CustomerOrderRequestID=ORSP.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest CORP WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=CORP.[ID] ", Environment.NewLine,
                    "LEFT JOIN OrderRequestRoutineRel ORRRel WITH(NOLOCK) ON ORRRel.CustomerOrderRequestID=TT.CustomerOrderRequestID ", Environment.NewLine,
                    "LEFT JOIN ProcessChart PCHT WITH(NOLOCK) ON TT.ProcessChartID = PCHT.[ID]", Environment.NewLine,
                    "LEFT JOIN EpisodeType ET WITH(NOLOCK) ON PCHT.EpisodeConfigID = ET.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CCPC WITH(NOLOCK) ON TT.CareCenterID = CCPC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization CCPCORG WITH(NOLOCK) ON CCPC.OrganizationID=CCPCORG.[ID]", Environment.NewLine,
                    "LEFT JOIN Customer C WITH(NOLOCK) ON TT.CustomerID=C.[ID]", Environment.NewLine,
                    "LEFT JOIN Person CUSTP WITH(NOLOCK) ON C.PersonID=CUSTP.[ID]", Environment.NewLine,
                    "LEFT JOIN SensitiveData CSD WITH(NOLOCK) ON CUSTP.[ID]=CSD.PersonID", Environment.NewLine,
                    "LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON TT.CustomerEpisodeID=CE.[ID] ", Environment.NewLine,
                    "LEFT JOIN Insurer AdmI WITH(NOLOCK) ON CE.AdmissionInsurerID=AdmI.[ID] ", Environment.NewLine,
                    "LEFT JOIN Organization AdmO WITH(NOLOCK) ON AdmI.OrganizationID=AdmO.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerPolicy CPol WITH(NOLOCK) ON CE.CustomerPolicyID=CPol.[ID] ", Environment.NewLine,
                    "LEFT JOIN PolicyType PolT WITH(NOLOCK) ON CPol.PolicyTypeID=PolT.[ID] ", Environment.NewLine,
                    "LEFT JOIN Physician CEPH WITH(NOLOCK) ON CE.PhysicianID=CEPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN Person CEPPH WITH(NOLOCK) ON CEPH.PersonID=CEPPH.[ID] ", Environment.NewLine,
                    "LEFT JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "LEFT JOIN Location ADMLOC  WITH(NOLOCK) ON CA.CurrentLocationID = ADMLOC.ID ", Environment.NewLine,
                    "LEFT JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID=CESR.CustomerEpisodeID AND CESR.Step = ", ((int)BasicProcessStepsEnum.Admission).ToString(), Environment.NewLine,
                    "LEFT JOIN AssistanceService CESRASS WITH(NOLOCK) ON CESR.AssistanceServiceID = CESRASS.ID");
                if (filter.IsFilteredByAny(UnplacedRequestFindOptionEnum.CustomerCitationID))
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                        "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID]");
                }
                else
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                        "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID] AND CRCH.CareCenterID=@CareCenterIDToShowCHNumber");
                }
                sqlQuery = string.Concat(sqlQuery, Environment.NewLine,
                    "LEFT JOIN Person RPHP WITH(NOLOCK) ON TT.RequestedPersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Physician RPhy WITH(NOLOCK) ON RPhy.PersonID=RPHP.[ID]", Environment.NewLine,
                    "LEFT JOIN Location LOC WITH(NOLOCK) ON TT.RequestedLocationID=LOC.[ID]", Environment.NewLine,
                    "LEFT JOIN Insurer IR WITH(NOLOCK) ON TT.RequestedInsurerID=IR.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization ORG WITH(NOLOCK) ON IR.OrganizationID=ORG.[ID]", Environment.NewLine,
                    "LEFT JOIN PolicyType PT WITH(NOLOCK) ON TT.PolicyTypeID=PT.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CC WITH(NOLOCK) ON TT.RequestedCareCenterID=CC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization CCORG WITH(NOLOCK) ON CC.OrganizationID=CCORG.[ID]", Environment.NewLine,
                    "LEFT JOIN CustomerOrderRequest ADTCOR WITH(NOLOCK) ON TT.ParentCustomerOrderRequestID=ADTCOR.ID", Environment.NewLine,
                    "LEFT JOIN OrderRequestSchPlanning ADTORP WITH(NOLOCK) ON ADTCOR.ID=ADTORP.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN OrderRequestADTInfo ADTInfo WITH(NOLOCK) ON ADTCOR.ID=ADTInfo.CustomerOrderRequestID", Environment.NewLine,
                    "LEFT JOIN PrescriptionRequest PR WITH(NOLOCK) ON TT.CustomerOrderRequestID=PR.CustomerOrderRequestID AND NOT(PR.[Status]=", ((int)ActionStatusEnum.Superceded).ToString(), ")", Environment.NewLine,
                    "LEFT JOIN Item I WITH(NOLOCK) ON PR.ItemID=I.[ID]", Environment.NewLine,
                    "LEFT JOIN MedicalSpecialty MS WITH(NOLOCK) ON MS.ID=TT.MedicalSpecialtyID", Environment.NewLine,
                    "LEFT JOIN TimePattern TP WITH(NOLOCK) ON ORSP.FrequencyOfApplicationID=TP.[ID]", Environment.NewLine,
                    "LEFT JOIN TimePattern DUR WITH(NOLOCK) ON ORSP.EstimatedDurationID=DUR.[ID]", Environment.NewLine,
                    "LEFT JOIN AssistanceService ASS WITH(NOLOCK) ON TT.AssistanceServiceID=ASS.ID", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerProcedureRel ORCPR WITH(NOLOCK) ON ORSP.ID=ORCPR.OrderRequestSchPlanningID", Environment.NewLine,
                    "LEFT JOIN CustomerProcedure CPR WITH(NOLOCK) ON ORCPR.CustomerProcedureID = CPR.[ID]", Environment.NewLine,
                    "LEFT JOIN OrderRequestCustomerRoutineRel ORCRR WITH(NOLOCK) ON ORSP.[ID]=ORCRR.OrderRequestSchPlanningID", Environment.NewLine,
                    "     JOIN CustomerRoutine CRR WITH(NOLOCK) ON ORCRR.CustomerRoutineID = CRR.[ID] AND CRR.RoutineID IS NOT NULL ", Environment.NewLine,
                    "LEFT JOIN CustomerReception CRN WITH(NOLOCK) ON CRN.CustomerEpisodeID=TT.CustomerEpisodeID ", Environment.NewLine,
                            "AND CRN.CustomerProcessID=TT.CustomerProcessID AND NOT(CRN.ID IS NULL)");

                sqlQuery = string.Concat(SQLProvider.SelectDistinctTopNFilteringCustomerOrderRequestBasicJoinStandardUnplacedForINDIGOCommand, Environment.NewLine,
                            sqlQuery, Environment.NewLine);

                _builder.SetOrderListParams(filter, _params, addinName);
                _params.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRows));
                _params.Add(new StoredProcInParam("CareCenterIDToShowCHNumber", DbType.Int32, filter.CareCenterIDToShowCHNumber));
                _params.Add(new StoredProcInParam("AppointmentElement", DbType.Int32, 3));
                _params.Add(new StoredProcInParam("ActionStatusCancelled", DbType.Int32, 4));
                _params.Add(new StoredProcInParam("StatusHeld", DbType.Int32, 8));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    Administrative.Entities.TableNames.CustomerOrderRequestTable,
                    _params.ToArray()
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsAllRealizedActors(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAllRealizedActorsCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsAllRealizedServiceName(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAllRealizedServiceNameCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsRealizedStatus(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsRealizedStatusCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsLocationScheduledDate(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsLocationScheduledDateCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }        

        public DataSet GetFilteringRestInfoByCORIDsRealizationData(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsRealizationDataCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsAllRealizationBodySite(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAllRealizationBodySiteCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }        

        public DataSet GetFilteringRestInfoByCORIDsAllRequestedServiceName(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAllRequestedServiceNameCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }        

        public DataSet GetFilteringRestInfoByCORIDsAllRequestedActors(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAllRequestedActorsCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsIsScheduled(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInParam("MedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder));
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsIsScheduledCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }        

        public DataSet GetFilteringRestInfoByCORIDsAbortedStatus(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInParam("Aborted", DbType.Int32, (int)ActionStatusEnum.Aborted));
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAbortedStatusCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsCustomerSpecialCategories(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsCustomerSpecialCategoriesCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsAllRequestBodySite(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAllRequestBodySiteCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }        

        public DataSet GetFilteringRestInfoByCORIDsConsentStatus(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsConsentStatusCommand,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestIDsByCustomerReceptionID(int customerReceptionID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerOrderRequestIDsByCustomerReceptionIDCommand,
                    Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInParam("CustomerReceptionID", DbType.Int32, customerReceptionID),
                    new StoredProcInParam("AppointmentElement", DbType.Int32, AppointmentElementEnum.MedicalOrder)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region Pruebas agfa
        public string GetMessageByPlaceOrderNumber(string placeOrderNumber, string messageType, string messageHeader)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetMessageByPlaceOrderNumber,
                    new StoredProcInParam("PlaceOrderNumber", DbType.String, placeOrderNumber),
                    new StoredProcInParam("MessageType", DbType.String, messageType),
                    new StoredProcInParam("MessageHeader", DbType.String, messageHeader)
                    ))
                {
                    return (IsEmptyReader(reader)) ? string.Empty : reader["ExtendedMessage"] as string;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return string.Empty;
            }

        }
        #endregion

        public DataSet GetAllRelatedCustomerOrderRequestIDsByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllRelatedCustomerOrderRequestIDsByCustomerEpisodeIDsCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerOrderRequestTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int IsLastPlanningChildOrderRealized(int customerOrderRequestID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.IsLastPlanningChildOrderRealizedCommand,
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, (int)ActionStatusEnum.Completed),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, (int)ActionStatusEnum.Confirmed),
                    new StoredProcInParam("SupervisedStatus", DbType.Int32, (int)ActionStatusEnum.Supervised)
                ))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public bool CustomerOrderRequestItIsPrescription(int corID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.CustomerOrderRequestItIsPrescriptionCommand,
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, corID)))
                {
                    return (!IsEmptyReader(reader));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }

        }

        public string GetAccesionNumberByCORId(int customerOrderRequestID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetAccesionNumberByCORIDCommand,
                    new StoredProcInParam("customerOrderRequestID", DbType.Int32, customerOrderRequestID)))
                {
                    return (IsEmptyReader(reader)) ? "" : reader["AccessionNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return "";
            }
        }

        #region para las tomas de medicamentos
        public DataSet GetDispatchedsByPrescriptionIDs(int[] prescriptionIDs)
        {
            try
            {
                if (prescriptionIDs == null || prescriptionIDs.Length <= 0) return null;
                prescriptionIDs = prescriptionIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                string sqlQuery = string.Concat(
                    "SELECT DISTINCT PR.ID PrescriptionID,", Environment.NewLine,
                    "(CASE WHEN EXISTS(SELECT IBE.ID FROM ItemBox IB ", Environment.NewLine,
                    "       JOIN ItemBoxEntry IBE ON IB.ID = IBE.ItemBoxID", Environment.NewLine,
                    "       WHERE IB.LocationID = PR.LocationID AND IBE.ItemID = PR.ItemID) THEN 1  ", Environment.NewLine, /* Repository */
                    "      ELSE 2 END) DispatchedFrom", Environment.NewLine, /* UnidosisCar */
                    "FROM PrescriptionRequest PR ", Environment.NewLine,
                    "JOIN @TVPTable TVP ON PR.ID = TVP.ID"
                    );

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    SII.HCD.Administrative.Entities.TableNames.PrescriptionRequestTable,
                    new StoredProcInTVPIntegerParam("TVPTable", prescriptionIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        public DataSet GetFilteringRestInfoByCORIDsCancelReason(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));
                _params.Add(new StoredProcInParam("CancelledStatus", DbType.Int32, ActionStatusEnum.Cancelled));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsCancelReason,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsFailedReason(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));
                _params.Add(new StoredProcInParam("CancelledStatus", DbType.Int32, ActionStatusEnum.Cancelled));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsFailedReason,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetFilteringRestInfoByCORIDsAnnulledReason(IEnumerable<int> corIDs)
        {
            if (corIDs == null)
                return null;

            try
            {
                List<StoredProcParam> _params = new List<StoredProcParam>();
                _params.Add(new StoredProcInTVPIntegerParam("TVPTable", corIDs));
                _params.Add(new StoredProcInParam("AbortedStatus", DbType.Int32, ActionStatusEnum.Aborted));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetFilteringRestInfoByCORIDsAnnulledReason,
                    Administrative.Entities.TableNames.AdditionalCORInfoTable,
                    _params.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }     
    }
}
