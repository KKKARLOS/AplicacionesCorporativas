using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.Administrative.Entities;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Common.Entities.Constants;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.DA
{
    public class CustomerEpisodeDA : DAServiceBase
    {
        #region Field length constants
        public const int EpisodeNumberLength = 50;
        public const int OriginLength = 100;
        public const int ModifiedByLength = 256;
        public const int AssignedCodeLength = 50;
        #endregion

        #region public methods
        public CustomerEpisodeDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerEpisodeDA(Gateway gateway) : base(gateway) { }

        public int Insert(int customerID, int processChartID, int customerProfileID, int customerClassificationID, int tariffID,
            int episodeTypeID, int admissionInsurerID, int physicianID, int customerPolicyID, int customerAdmissionID, string episodeNumber, string comments,
            int currentLocationAvailID, int currentEquipmentAvailID, DateTime startDateTime, DateTime? endDateTime,
            string origin, int predecessorID, int customerADTOrderID, CommonEntities.StatusEnum status, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerEpisodeCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CustomerProfileID", DbType.Int32, customerProfileID),
                    new StoredProcInParam("CustomerClassificationID", DbType.Int32, customerClassificationID),
                    new StoredProcInParam("TariffID", DbType.Int32, tariffID),
                    new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
                    new StoredProcInParam("AdmissionInsurerID", DbType.Int32, admissionInsurerID),
                    new StoredProcInParam("PhysicianID", DbType.Int32, physicianID),
                    new StoredProcInParam("CustomerPolicyID", DbType.Int32, customerPolicyID),
                    new StoredProcInParam("CustomerAdmissionID", DbType.Int32, customerAdmissionID),
                    new StoredProcInParam("Comments", DbType.String, comments),
                    new StoredProcInParam("CurrentLocationAvailabilityID", DbType.Int32, currentLocationAvailID),
                    new StoredProcInParam("CurrentEquipmentAvailabilityID", DbType.Int32, currentEquipmentAvailID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (startDateTime != DateTime.MinValue) ? startDateTime : (object)null),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (endDateTime != DateTime.MinValue) ? endDateTime : (object)null),
                    new StoredProcInParam("EpisodeNumber", DbType.String, SIIStrings.Left(episodeNumber, EpisodeNumberLength)),
                    new StoredProcInParam("Origin", DbType.String, SIIStrings.Left(origin, OriginLength)),
                    new StoredProcInParam("PredecessorID", DbType.Int32, predecessorID),
                    new StoredProcInParam("CustomerADTOrderID", DbType.Int32, customerADTOrderID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)),
                    new StoredProcInParam("Status", DbType.Int32, status)))
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

        public int Update(int id, int customerID, int processChartID, int customerProfileID, int customerClassificationID, int tariffID,
            int episodeTypeID, int admissionInsurerID, int physicianID, int customerPolicyID, int customerAdmissionID, string episodeNumber, string comments,
            int currentLocationAvailID, int currentEquipmentAvailID, DateTime startDateTime, DateTime? endDateTime,
            string origin, int predecessorID, int customerADTOrderID, CommonEntities.StatusEnum status, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CustomerProfileID", DbType.Int32, customerProfileID),
                    new StoredProcInParam("CustomerClassificationID", DbType.Int32, customerClassificationID),
                    new StoredProcInParam("TariffID", DbType.Int32, tariffID),
                    new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
                    new StoredProcInParam("CustomerPolicyID", DbType.Int32, customerPolicyID),
                    new StoredProcInParam("AdmissionInsurerID", DbType.Int32, admissionInsurerID),
                    new StoredProcInParam("PhysicianID", DbType.Int32, physicianID),
                    new StoredProcInParam("CustomerAdmissionID", DbType.Int32, customerAdmissionID),
                    new StoredProcInParam("Comments", DbType.String, comments),
                    new StoredProcInParam("CurrentLocationAvailID", DbType.Int32, currentLocationAvailID),
                    new StoredProcInParam("CurrentEquipmentAvailID", DbType.Int32, currentEquipmentAvailID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (startDateTime != DateTime.MinValue) ? startDateTime : (object)null),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (endDateTime != DateTime.MinValue) ? endDateTime : (object)null),
                    new StoredProcInParam("EpisodeNumber", DbType.String, SIIStrings.Left(episodeNumber, EpisodeNumberLength)),
                    new StoredProcInParam("Origin", DbType.String, SIIStrings.Left(origin, OriginLength)),
                    new StoredProcInParam("PredecessorID", DbType.Int32, predecessorID),
                    new StoredProcInParam("CustomerADTOrderID", DbType.Int32, customerADTOrderID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)),
                    new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Update(int id, int customerADTOrderID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerEpisodeCustomerADTOrderCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerADTOrderID", DbType.Int32, customerADTOrderID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int UpdatePhysician(int id, int physicianID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerEpisodePhysicianCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("PhysicianID", DbType.Int32, physicianID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public bool UpdateClosed(int id, DateTime? endDateTime, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateStatusCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Closed),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (endDateTime != null) ? endDateTime : (object)null),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }


        public bool ActualizaFechaFinEpisodio(int id, DateTime? endDateTime, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerEpisodeEndDateTimeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (endDateTime != null) ? endDateTime : (object)null),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool ActualizaCustomerLeave(int id, DateTime? leaveDateTime, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateLeaveDateTimeCommand,
                    new StoredProcInParam("EpisodeID", DbType.Int32, id),
                    new StoredProcInParam("LeaveDateTime", DbType.DateTime, (leaveDateTime != null) ? leaveDateTime : (object)null),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool ActualizaFechaInicioEpisodio(int id, DateTime? startDateTime, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerEpisodeStartDateTimeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (startDateTime != null) ? startDateTime : (object)null),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool UpdateCancelled(int id, DateTime? endDateTime, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateStatusCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (endDateTime != null) ? endDateTime : (object)null),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool UpdateStatus(int id, SII.HCD.Common.Entities.StatusEnum status, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerEpisodeStatusCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, (int)status),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool UpdateHeld(int id, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateStatusCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Held),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (object)null),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool UpdateActive(int id, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateStatusCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (object)null),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool UpdateADTOrderID(int id, int customerADTOrderID, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerADTOrderIDCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerADTOrderID", DbType.Int32, customerADTOrderID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public int UpdateCurrentLocationAvail(int id, int currentLocationAvailID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCurrentLocationAvailCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CurrentLocationAvailID", DbType.Int32, currentLocationAvailID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int UpdatePredecessor(int id, int predecessorID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdatePredecessorCustomerEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("PredecessorID", DbType.Int32, predecessorID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Delete(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.DeleteCustomerEpisodeCommand,
                     new StoredProcInParam("@ID", DbType.Int32, id));
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
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeDBTimeStampCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger64(reader["DBTimeStamp"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        //MT: NO SE UTILIZAN
        //public DataSet GetEpisodes(DateTime startDateTime, DateTime endDateTime)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodesCommand,
        //            SII.HCD.Administrative.Entities.TableNames.EpisodeTable,
        //            new StoredProcInParam("StartDateTime", DbType.DateTime, (startDateTime != DateTime.MinValue) ? startDateTime : (object)null),
        //            new StoredProcInParam("EndDateTime", DbType.DateTime, (endDateTime != DateTime.MinValue) ? endDateTime : (object)null));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        //public DataSet GetEpisodes(DateTime startDateTime, DateTime endDateTime, int maxRecords)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeTopNCommand,
        //            SII.HCD.Administrative.Entities.TableNames.EpisodeTable,
        //            new StoredProcInParam("StartDateTime", DbType.DateTime, (startDateTime != DateTime.MinValue) ? startDateTime : (object)null),
        //            new StoredProcInParam("EndDateTime", DbType.DateTime, (endDateTime != DateTime.MinValue) ? endDateTime : (object)null),
        //            new StoredProcInParam("MaxRecords", DbType.DateTime, maxRecords));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public DataSet GetEpisode(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.EpisodeTable, new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //MT: No lo llama nadie
        //public DataSet GetEpisodeByCustomer(int customerID)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByCustomerIDCommand,
        //            SII.HCD.Administrative.Entities.TableNames.EpisodeTable, new StoredProcInParam("CustomerID", DbType.Int32, customerID));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public int GetEpisodeIDByEpisodeNumber(string episodeNumber)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByEpisodeNumberCommand,
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetCurrentLocationID(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetCurrentLocationIDByEpisodeCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["LocationID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetEpisodeIDByEpisodeNumber(string episodeNumber, int episodeTypeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByEpisodeNumberEpisodeTypeIDCommand,
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber),
                    new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public DataSet GetLastCustomerEpisode(int admissionID, int processChartID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetLastCustomerEpisodeCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("AdmissionID", DbType.Int32, admissionID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeByAdmissionID(int admissionID, int processChartID, int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByAdmissionIDCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("AdmissionID", DbType.Int32, admissionID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisode(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByIDCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }
       

        public DataSet GetCustomerEpisode(int id, bool showOutdatedCoverAgress)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("ObtenerFullCustomerEpisodeEntity",
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("showOutdatedCoverAgrees", DbType.Boolean, showOutdatedCoverAgress),
                    new StoredProcInParam("MaxRecords", DbType.Int32, Int32.MaxValue)
                    );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.LocationAvailabilityTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.EpisodeReasonTypeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeReasonRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeLeaveReasonRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.EpisodeReasonTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.EpisodeLeaveReasonTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ConceptTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ConceptLeaveTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeAuthorizationTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AuthorizationTypeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeAuthorizationEntryTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeAuthorizationOpsTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.InsurerCoverAgreementDTOTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerCoverAgreeRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerInsurerAgreeRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerAssistAgreeRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerAgreeRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpInteropInfoTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeReferencedPhysicianRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPolicyTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PolicyTypeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerGuaranteeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerEpisodeGuarantorTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PersonBaseTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AddressTable;

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
        public DataSet GetCustomerEpisodeByIDs(int[] ids)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByIDsCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInTVPIntegerParam("TVPTable", ids));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetAllCustomerEpisodes(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllCustomerEpisodesByCustomerIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodesByMedicalEpisodeID(int medicalEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodesByMedicalEpisodeIDCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("MedicalEpisodeID", DbType.Int32, medicalEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodesByMedicalEpisodeIDs(int[] medicalEpisodeIDs)
        {
            try
            {
                if (medicalEpisodeIDs == null || medicalEpisodeIDs.Length <= 0) return null;
                medicalEpisodeIDs = medicalEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodesByMedicalEpisodeIDsCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInTVPIntegerParam("TVPTable", medicalEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }





        public DataSet GetRelatedCustomerEpisodesByCustomerID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRelatedCustomerEpisodesByCustomerIDCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int GetCustomerEpisodeIDByCustomerMedEpisodeActID(int customerMedEpisodeActID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByCustomerMedEpisodeActIDCommand,
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public DataSet GetCustomerEpisodeByCustomerMedEpisodeActID(int customerMedEpisodeActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByCustomerMedEpisodeActIDCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByAllEpisode(int careCenterID, int authorizationTypeID, string concatEpisodeTypes,
            DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAllEpisodeCommand;
                if (authorizationTypeID > 0)
                {
                    sqlQuery += Environment.NewLine + "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID ";
                }

                if ((careCenterID > 0) || ((int)status > 0) || (fromDate != null) || (toDate != null) || (!string.IsNullOrEmpty(concatEpisodeTypes)) || (authorizationTypeID > 0))
                {
                    sqlQuery += Environment.NewLine + "WHERE NOT(CE.[Status]=@CancelledStatus) ";

                    if (!string.IsNullOrEmpty(concatEpisodeTypes))
                        sqlQuery += string.Concat("AND (ET.[ID] IN ", concatEpisodeTypes, ")", Environment.NewLine);

                    if ((processChartIDs != null) && (processChartIDs.Length > 0))
                        sqlQuery += string.Concat(" AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                    if (careCenterID > 0)
                        sqlQuery += " AND (CC.[ID]=@CareCenterID) ";

                    if ((int)status > 0)
                        sqlQuery += " AND (CE.[Status]=@Status) ";

                    if (fromDate != null)
                        sqlQuery += " AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                    if (toDate != null)
                        sqlQuery += " AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";

                    if (authorizationTypeID > 0)
                        sqlQuery += " AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                }

                sqlQuery += Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByAllEpisodeCommand;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, (int)GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("PrincipalGuarantor", DbType.Int16, (int)GuarantorTypeEnum.PrincipalGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, (int)CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, (int)InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, (int)InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, (int)ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, (int)ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByAnyCoveragePrivate(int careCenterID, int authorizationTypeID, string concatEpisodeTypes,
            DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAnyCoveragePrivateCommand;
                sqlQuery += authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty;
                sqlQuery += "WHERE (CCAR.[ID] IS NULL) ";

                sqlQuery += " AND NOT(CE.[Status]=@CancelledStatus) " + Environment.NewLine;

                if (!string.IsNullOrEmpty(concatEpisodeTypes))
                    sqlQuery += "AND (ET.[ID] IN " + concatEpisodeTypes + ")" + Environment.NewLine;

                if ((processChartIDs != null) && (processChartIDs.Length > 0))
                    sqlQuery += string.Concat("AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                if (careCenterID > 0)
                    sqlQuery += "AND (CC.[ID]=@CareCenterID) ";

                if ((int)status > 0)
                    sqlQuery += "AND (CE.[Status]=@Status) ";

                if (fromDate != null)
                    sqlQuery += "AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                if (toDate != null)
                    sqlQuery += "AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";

                if (authorizationTypeID > 0)
                {
                    sqlQuery += "AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                }

                sqlQuery += Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoveragePrivateCommand;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("PrincipalGuarantor", DbType.Int16, GuarantorTypeEnum.PrincipalGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByAssistanceAgreementsCoveragePrivate(int careCenterID, int authorizationTypeID, string concatEpisodeTypes,
            string assistanceAgreementsCode, DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAssistanceAgreementsCoveragePrivateCommand;
                sqlQuery += authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty;
                sqlQuery += "WHERE (CCAR.[ID] IS NULL) " + Environment.NewLine;
                sqlQuery += "AND NOT(CE.[Status]=@CancelledStatus) " + Environment.NewLine;

                if (!string.IsNullOrEmpty(concatEpisodeTypes))
                    sqlQuery += "AND (ET.[ID] IN " + concatEpisodeTypes + ")" + Environment.NewLine;

                if ((processChartIDs != null) && (processChartIDs.Length > 0))
                    sqlQuery += string.Concat("AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                if (!string.IsNullOrEmpty(assistanceAgreementsCode))
                    sqlQuery += "AND (HAA.[ID] IN " + assistanceAgreementsCode + ")" + Environment.NewLine;

                if (careCenterID > 0)
                    sqlQuery += "AND (CC.[ID]=@CareCenterID) ";

                if ((int)status > 0)
                    sqlQuery += "AND (CE.[Status]=@Status) ";

                if (fromDate != null)
                    sqlQuery += "AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                if (toDate != null)
                    sqlQuery += "AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";

                if (authorizationTypeID > 0)
                {
                    sqlQuery += "AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                }

                sqlQuery += Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoveragePrivateCommand;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("PrincipalGuarantor", DbType.Int16, GuarantorTypeEnum.PrincipalGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByAgreementsCoveragePrivate(int careCenterID, int authorizationTypeID, string concatEpisodeTypes,
            string assistanceAgreementsCode, string agreementsCode, DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = String.Concat(
                    SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAssistanceAgreementsCoveragePrivateCommand, Environment.NewLine,
                    authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty,
                    "WHERE (CCAR.[ID] IS NULL) ", Environment.NewLine,
                    "AND NOT(EXISTS(SELECT TOP 1 [ID] FROM CustomerAgreeRel CAR WHERE (CAR.CustomerAssistanceAgreeRelID=CAAR.[ID]))) ", Environment.NewLine,
                    (!string.IsNullOrEmpty(assistanceAgreementsCode)) ? string.Concat("AND (HAA.[ID] IN ", assistanceAgreementsCode, ")") : string.Empty, Environment.NewLine);

                string sqlWhere = string.Empty;

                sqlWhere += " AND NOT(CE.[Status]=@CancelledStatus) " + Environment.NewLine;

                if ((careCenterID > 0) || ((int)status > 0) || (fromDate != null) || (toDate != null) || (!string.IsNullOrEmpty(concatEpisodeTypes)))
                {
                    if (!string.IsNullOrEmpty(concatEpisodeTypes))
                        sqlWhere += "AND (ET.[ID] IN " + concatEpisodeTypes + ")" + Environment.NewLine;

                    if ((processChartIDs != null) && (processChartIDs.Length > 0))
                        sqlWhere += string.Concat("AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                    if (careCenterID > 0)
                        sqlWhere += "AND (CC.[ID]=@CareCenterID) ";

                    if ((int)status > 0)
                        sqlWhere += "AND (CE.[Status]=@Status) ";

                    if (fromDate != null)
                        sqlWhere += "AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                    if (toDate != null)
                        sqlWhere += "AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";
                }
                if (authorizationTypeID > 0)
                {
                    sqlWhere += "AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                }

                sqlQuery += String.Concat(sqlWhere + Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoveragePrivateCommand);

                sqlQuery += String.Concat(Environment.NewLine, "UNION ", Environment.NewLine,
                    SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAgreementsCoveragePrivateCommand, Environment.NewLine,
                    authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty,
                    "WHERE (CCAR.[ID] IS NULL) ",
                    (!string.IsNullOrEmpty(agreementsCode)) ? string.Concat("AND (HA.[ID] IN ", agreementsCode, ")") : string.Empty, Environment.NewLine,
                    sqlWhere + Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoveragePrivateCommand);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("PrincipalGuarantor", DbType.Int16, GuarantorTypeEnum.PrincipalGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByAnyCoverageInsurer(int careCenterID, int authorizationTypeID, string concatEpisodeTypes,
            DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAnyCoverageInsurerCommand;
                sqlQuery += authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty;
                sqlQuery += Environment.NewLine + "WHERE NOT(CE.[Status]=@CancelledStatus) ";

                if ((careCenterID > 0) || ((int)status > 0) || (fromDate != null) || (toDate != null) || (!string.IsNullOrEmpty(concatEpisodeTypes)))
                {
                    if (!string.IsNullOrEmpty(concatEpisodeTypes))
                        sqlQuery += string.Concat(" AND (ET.[ID] IN ", concatEpisodeTypes, ")", Environment.NewLine);

                    if ((processChartIDs != null) && (processChartIDs.Length > 0))
                        sqlQuery += string.Concat(" AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                    if (careCenterID > 0)
                        sqlQuery += " AND (CC.[ID]=@CareCenterID) ";

                    if ((int)status > 0)
                        sqlQuery += " AND (CE.[Status]=@Status) ";

                    if (fromDate != null)
                        sqlQuery += " AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                    if (toDate != null)
                        sqlQuery += " AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";

                    if (authorizationTypeID > 0)
                    {
                        sqlQuery += "AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                    }
                }

                sqlQuery += Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoverageInsurerCommand;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByCoverageInsurerOfInsurerID(int careCenterID, int authorizationTypeID, string concatEpisodeTypes, int insurerID,
            DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAnyCoverageInsurerOfInsurerIDCommand;
                sqlQuery += authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty;
                sqlQuery += "WHERE (I.[ID]=@InsurerID) ";
                sqlQuery += " AND NOT(CE.[Status]=@CancelledStatus) " + Environment.NewLine;

                if (!string.IsNullOrEmpty(concatEpisodeTypes))
                    sqlQuery += "AND (ET.[ID] IN " + concatEpisodeTypes + ")" + Environment.NewLine;

                if ((processChartIDs != null) && (processChartIDs.Length > 0))
                    sqlQuery += string.Concat("AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                if (careCenterID > 0)
                    sqlQuery += "AND (CC.[ID]=@CareCenterID) ";

                if ((int)status > 0)
                    sqlQuery += "AND (CE.[Status]=@Status) ";

                if (fromDate != null)
                    sqlQuery += "AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                if (toDate != null)
                    sqlQuery += "AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";

                if (authorizationTypeID > 0)
                {
                    sqlQuery += "AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                }

                sqlQuery += Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoverageInsurerCommand;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("InsurerID", DbType.Int32, insurerID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByCoverAgreementsCoverageInsurer(int careCenterID, int authorizationTypeID, string concatEpisodeTypes, int insurerID,
            string concatCoverAgreementCodes, DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByCoverAgreementsCoverageInsurerCommand;
                sqlQuery += authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty;
                sqlQuery += "WHERE (I.[ID]=@InsurerID) ";
                sqlQuery += " AND NOT(CE.[Status]=@CancelledStatus) " + Environment.NewLine;

                if (!string.IsNullOrEmpty(concatEpisodeTypes))
                    sqlQuery += "AND (ET.[ID] IN " + concatEpisodeTypes + ")" + Environment.NewLine;

                if ((processChartIDs != null) && (processChartIDs.Length > 0))
                    sqlQuery += string.Concat("AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                if (!string.IsNullOrEmpty(concatCoverAgreementCodes))
                    sqlQuery += "AND (HICA.[ID] IN " + concatCoverAgreementCodes + ")" + Environment.NewLine;

                if (careCenterID > 0)
                    sqlQuery += "AND (CC.[ID]=@CareCenterID) ";

                if ((int)status > 0)
                    sqlQuery += "AND (CE.[Status]=@Status) ";

                if (fromDate != null)
                    sqlQuery += "AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                if (toDate != null)
                    sqlQuery += "AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";

                if (authorizationTypeID > 0)
                {
                    sqlQuery += "AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                }

                sqlQuery += Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoverageInsurerCommand;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("InsurerID", DbType.Int32, insurerID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargeExtendedInfoByAgreementsCoverageInsurer(int careCenterID, int authorizationTypeID, string concatEpisodeTypes, int insurerID,
            string concatCoverAgreementCodes, string agreementsCode, DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status, int[] processChartIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = String.Concat(SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByCoverAgreementsCoverageInsurerCommand, Environment.NewLine,
                    authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty,
                    "WHERE (I.[ID]=@InsurerID) ",
                    "AND NOT(EXISTS(SELECT TOP 1 [ID] FROM CustomerInsurerAgreeRel CIAR WHERE (CIAR.CustomerCoverAgreeRelID=CCAR.[ID]))) ", Environment.NewLine,
                    (!string.IsNullOrEmpty(concatCoverAgreementCodes)) ? string.Concat("AND (HICA.[ID] IN " + concatCoverAgreementCodes + ")") : string.Empty, Environment.NewLine);

                string sqlWhere = " AND NOT(CE.[Status]=@CancelledStatus) " + Environment.NewLine;

                if (!string.IsNullOrEmpty(concatEpisodeTypes))
                    sqlWhere += "AND (ET.[ID] IN " + concatEpisodeTypes + ")" + Environment.NewLine;

                if ((processChartIDs != null) && (processChartIDs.Length > 0))
                    sqlWhere += string.Concat("AND (PC.[ID] IN (", StringUtils.BuildIDString(processChartIDs), ")) ");

                if (careCenterID > 0)
                    sqlWhere += "AND (CC.[ID]=@CareCenterID) ";

                if ((int)status > 0)
                    sqlWhere += "AND (CE.[Status]=@Status) ";

                if (fromDate != null)
                    sqlWhere += "AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime >= @StartDateTime)) ";

                if (toDate != null)
                    sqlWhere += "AND ((CE.StartDateTime IS NULL) OR (CE.StartDateTime<=@EndDateTime)) ";

                if (authorizationTypeID > 0)
                {
                    sqlWhere += "AND (CEA.AuthorizationTypeID=@AuthorizationTypeID) ";
                }

                sqlQuery += String.Concat(sqlWhere + Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoverageInsurerCommand);

                sqlQuery += String.Concat(Environment.NewLine, "UNION ", Environment.NewLine,
                    SQLProvider.SelectTopMaxFromCustomerEpisodeWithChargeExtendedInfoByAgreementsCoverageInsurerCommand, Environment.NewLine,
                    authorizationTypeID > 0 ? "JOIN CustomerEpisodeAuthorization CEA ON CE.[ID]=CEA.CustomerEpisodeID " + Environment.NewLine : string.Empty,
                    "WHERE (I.[ID]=@InsurerID) ",
                    (!string.IsNullOrEmpty(agreementsCode)) ? string.Concat("AND (HIA.[ID] IN ", agreementsCode, ")") : string.Empty, Environment.NewLine,
                    sqlWhere + Environment.NewLine + SQLProvider.GroupByCustomerEpisodeWithChargeExtendedInfoByCoverageInsurerCommand);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("InsurerID", DbType.Int32, insurerID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status),
                    new StoredProcInParam("MaxRows", DbType.Int32, maxRecords),
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeDTOByCustomerEpisodesIDs(int[] customerEpisodeIDs)
        {
            if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;

            try
            {
                string sqlQuery = String.Concat(SQLProvider.SELECTDISTINCTCustomerEpisodeDTOJOINStandardCommand, Environment.NewLine,
                    "WHERE (CE.[ID] IN (", StringUtils.BuildIDString(customerEpisodeIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, (int)GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("PrincipalGuarantor", DbType.Int16, (int)GuarantorTypeEnum.PrincipalGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, (int)CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, (int)InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, (int)InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, (int)ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, (int)ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeDTOByID(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                    SQLProvider.GetCustomerEpisodeDTOByIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, (int)GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("PrincipalGuarantor", DbType.Int16, (int)GuarantorTypeEnum.PrincipalGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, (int)CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, (int)InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, (int)InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, (int)ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, (int)ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeDTOByInvoiceID(int invoiceID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                    SQLProvider.GetCustomerEpisodeDTOByInvoiceIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("InvoiceID", DbType.Int32, invoiceID),
                    new StoredProcInParam("InsurerAsGuarantor", DbType.Int16, (int)GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("PrincipalGuarantor", DbType.Int16, (int)GuarantorTypeEnum.PrincipalGuarantor),
                    new StoredProcInParam("CoverPendingStatus", DbType.Int16, (int)CoverChargeStatusEnum.Pending),
                    new StoredProcInParam("CoverNotCoveredStatus", DbType.Int16, (int)CoverChargeStatusEnum.NotCovered),
                    new StoredProcInParam("CancelledStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("SupercededStatus", DbType.Int16, (int)CommonEntities.StatusEnum.Superceded),
                    new StoredProcInParam("InvoiceChargeStatusPending", DbType.Int16, (int)InvoiceChargeStatusEnum.Pending),
                    new StoredProcInParam("AuthorizedNotInvoiceStatus", DbType.Int16, (int)InvoiceChargeStatusEnum.AuthorizedNotInvoiced),
                    new StoredProcInParam("InvoiceStatusPending", DbType.Int16, (int)InvoiceStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusPending", DbType.Int16, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("ActionStatusInitiated", DbType.Int16, (int)ActionStatusEnum.Initiated),
                    new StoredProcInParam("ActionStatusScheduled", DbType.Int16, (int)ActionStatusEnum.Scheduled),
                    new StoredProcInParam("StepGuarantee", DbType.Int16, (int)BasicProcessStepsEnum.Guarantee));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Boolean Exists(int id)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerEpisodeByIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)))
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

        public Boolean Exists(int id, string episodeNumber)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerEpisodeNumberByIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber)))
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

        public int GetEpisodeTariffID(int id)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetTariffIDByCustomerEpisodeIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["TariffID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public DataSet GetCustomerPreviousEpisodeByCustomerID(int customerID, DateTime leaveDateTime, int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerPreviousEpisodesByCustomerIDCommand, Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("LeaveDateTime", DbType.DateTime, leaveDateTime),
                    new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerPreviousEpisodeByCustomerID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerPreviousEpisodesONLYByCustomerIDCommand, Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }




        public int[] GetRelatedEpisodesByCustomerEpisodeID(int episodeID, bool includeThis)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRelatedByCustomerEpisodeIDCommand,
                    Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable,
                    new StoredProcInParam("EpisodeID", DbType.Int32, episodeID));
                if (ds != null && ds.Tables != null && ds.Tables.Contains(Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable)
                    && ds.Tables[Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable].Rows.Count > 0)
                {
                    List<int> episodes = (from row in ds.Tables[Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable].AsEnumerable()
                                          where (row["ID"] as int? ?? 0) > 0
                                          select (row["ID"] as int? ?? 0)).ToList();
                    if (includeThis && !Array.Exists(episodes.ToArray(), epid => epid == episodeID))
                        episodes.Add(episodeID);
                    return episodes.ToArray();
                }
                return includeThis ? new int[] { episodeID } : null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetRelatedEpisodesByListOfEpisodeID(int[] episodeIDs)
        {
            try
            {
                if (episodeIDs == null || episodeIDs.Length <= 0) return null;
                string whereEpisodeIDs = StringUtils.BuildIDString(episodeIDs);
                /// esta query sólo trae la información que está en customerprocess y no tiene where
                string finalQuery = string.Concat(SQLProvider.GetCustomerEpisodeBasicCommand, Environment.NewLine, "WHERE CE.[ID] IN (", whereEpisodeIDs, ") ");
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }


        public DataSet GetRelatedEpisodesByCustomerID(int customerID)
        {
            try
            {
                string finalQuery = string.Concat(SQLProvider.GetCustomerEpisodeBasicCommand, Environment.NewLine,
                    "WHERE CE.CustomerID = ", customerID.ToString());
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }


        public int UpdateTimeStamp(int id, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerEpisodeTimeStampCommand,
                                    new StoredProcInParam("ID", DbType.Int32, id),
                                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public DataSet GetCustomerEpisodesByCustomerID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodesByCustomerIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("CloseStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Closed));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodesForCustomerInvoiceByInvoiceID(int invoiceID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeForCustomerInvoiceByInvoiceIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("InvoiceID", DbType.Int32, invoiceID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Boolean ExistsCustomerEpisodeActive(int customerID, int processChartID, int careCenterID, int status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerEpisodeActiveCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Status", DbType.Int32, status)))
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


        public Boolean ExistNextCustomerEpisode(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistNextCustomerEpisodeCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("Superceded", DbType.Int32, (int)CommonEntities.StatusEnum.Superceded)
                    ))
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


        public Boolean ExistsCustomerEpisodeByStatuses(string episodeNumber, CommonEntities.StatusEnum[] statuses, int customerID, int processChartID)
        {
            try
            {
                int[] statusestoint = (statuses != null)
                    ? statuses.Select(s => (int)s).ToArray()
                    : new int[] { (int)CommonEntities.StatusEnum.Active };
                string ids = StringUtils.BuildIDString(statusestoint);
                string query = string.Concat("SELECT CE.[ID] FROM CustomerEpisode CE ", Environment.NewLine,
                     "  WHERE (CE.[Status] in (", ids, ")) AND (EpisodeNumber=", episodeNumber, ") ");
                if (customerID > 0)
                {
                    query = string.Concat(query, Environment.NewLine, "  AND (CE.[CustomerID]=", customerID.ToString(), ") ");
                }
                if (processChartID > 0)
                {
                    query = string.Concat(query, Environment.NewLine, "  AND (CE.[ProcessChartID]=", processChartID.ToString(), ") ");
                }
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(query))
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



        public DataSet GetCustomerEpisodesByEpisodeID(int episodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodesByEpisodeIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("EpisodeID", DbType.Int32, episodeID),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("CloseStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Closed));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeWithChargesDTOByStringListCustomerEpisodesID(string customerEpisodeIDs)
        {
            try
            {
                string sqlquery = string.Format(SQLProvider.GetCustomerEpisodeWithChargesDTOByCustomerEpisodesIDsCommand, customerEpisodeIDs);

                return this.Gateway.ExecuteQueryDataSet(sqlquery, Administrative.Entities.TableNames.CustomerEpisodeWithChargesDTOTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodesByInvoiceID(int invoiceID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByInvoiceIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("InvoiceID", DbType.Int32, invoiceID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeByDeliveryNoteStatus(int customerID, int customerDeliveryNoteStatus, int deliveryNoteStatus)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByDeliveryNoteCDNStatusAndDNStatusCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("DeliveryNoteStatus", DbType.Int32, deliveryNoteStatus),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeByDeliveryNoteStatus(int customerID, int customerDeliveryNoteStatus)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByDeliveryNoteCDNStatusCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeByDeliveryNoteStatusProcessChartID(int processChartID, int deliveryNoteStatus)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByOnlyDeliveryNoteStatusProcessChartIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("DeliveryNoteStatus", DbType.Int32, deliveryNoteStatus),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeByDeliveryNoteProcessChartIDNotExported(int processChartID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByOnlyDeliveryNoteProcessChartIDExportedCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("Exported", DbType.Boolean, false),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeBaseByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectCustomerEpisodeBaseFromCommand, Environment.NewLine,
                    "WHERE (E.[ID] IN (" + StringUtils.BuildIDString(customerEpisodeIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    Administrative.Entities.TableNames.CustomerEpisodeBaseTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeByCustomerEpisodeIDDeliveryNoteStatus(int customerEpisodeID, int customerDeliveryNoteStatus, int deliveryNoteStatus)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByCustomerEpisodeIDDeliveryNoteCDNStatusAndDNStatusCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("DeliveryNoteStatus", DbType.Int32, deliveryNoteStatus),
                    new StoredProcInParam("EpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeByCustomerEpisodeIDDeliveryNoteStatus(int customerEpisodeID, int customerDeliveryNoteStatus)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByCustomerEpisodeIDDeliveryNoteCDNStatusCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("EpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeByDeliveryNoteID(int deliveryNoteID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeByDeliveryNoteIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeBaseTable,
                    new StoredProcInParam("DeliveryNoteID", DbType.Int32, deliveryNoteID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeAttributes(int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeAttributesCommand,
                    SII.HCD.Common.Entities.TableNames.EACAttributeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("AddinAttributeType", DbType.Int32, (int)SII.HCD.Administrative.Entities.CoverAnalysis.AddinAttributeTypeEnum.CustomerEpisode),
                    new StoredProcInParam("EpisodeTypeTableName", DbType.String, EntityNames.EpisodeTypeEntityName),
                    new StoredProcInParam("EpisodeTypeKeyAttributeName", DbType.String, "Code"),
                    new StoredProcInParam("CustomerClassificationTableName", DbType.String, EntityNames.CustomerClassificationEntityName),
                    new StoredProcInParam("CustomerClassificationKeyAttributeName", DbType.String, "Code"),
                    new StoredProcInParam("ProfileTableName", DbType.String, EntityNames.ProfileEntityName),
                    new StoredProcInParam("ProfileKeyAttributeName", DbType.String, "Code"),
                    new StoredProcInParam("PolicyTypeTableName", DbType.String, EntityNames.PolicyTypeEntityName),
                    new StoredProcInParam("PolicyTypeKeyAttributeName", DbType.String, "Name")
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeToActsRequestDTO(int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeToActsRequestDTOCommand,
                    Entities.TableNames.ActsRequestDTOTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeNotificationDataByID(int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeNotificationDataByIDCommand,
                    Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetCustomerEpisodeID(string episodeNumber, string processChartName)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByEpisodeNumberAndProcessCommand,
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber),
                    new StoredProcInParam("ProcessChartName", DbType.String, processChartName)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }


        public int GetCustomerEpisodeIDByPCIDAndCCID(string episodeNumber, int processChartID, int careCenterID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByEpisodeNumberAndPCIDAndCCIDCommand,
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetCustomerEpisodeIDByNumberCCIDANDStatus(int customerID, string episodeNumber, int careCenterID, int status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByNumberCCIDANDStatusCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Status", DbType.Int32, status)
                    ))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetCustomerEpisodeIDByNumberANDStatus(int customerID, string episodeNumber, int status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByNumberANDStatusCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber),
                    new StoredProcInParam("Status", DbType.Int32, status)
                    ))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }


        public int GetCustomerEpisodeIDByCCIDANDStatus(int customerID, int careCenterID, int status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByCCIDANDStatusCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Status", DbType.Int32, status)
                    ))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }


        public int GetEpisodeIDByEpisodeNumber(int customerID, string episodeNumber, int careCenterID, string careCenterName)
        {
            try
            {
                string sqlQuery = SQLProvider.GetCustomerEpisodeIDByEpisodeNumberAndCareCenterActiveCommand;
                if (careCenterID > 0)
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "AND CC.[ID] = @CareCenterID");
                }
                else
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "AND CCORG.[Name] = @CareCenterName");
                }

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ClosedStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Closed),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("CareCenterName", DbType.String, careCenterName)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetEpisodeIDByEpisodeNumber(int customerID, string episodeNumber, int episodeCase, int careCenterID, string careCenterName)
        {
            try
            {
                string sqlQuery = SQLProvider.GetCustomerEpisodeIDByEpisodeNumberAndCareCenterCommand;
                if (careCenterID > 0)
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "AND CC.[ID] = @CareCenterID");
                }
                else
                {
                    sqlQuery = string.Concat(sqlQuery, Environment.NewLine, "AND CCORG.[Name] = @CareCenterName");
                }

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(sqlQuery,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("EpisodeNumber", DbType.String, episodeNumber),
                    new StoredProcInParam("EpisodeCase", DbType.Int32, episodeCase),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("CareCenterName", DbType.String, careCenterName)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public DataSet GetCustomerEpisodes(int maxRecord, int processChartID, CommonEntities.StatusEnum status, DateTime? startDateTime, DateTime? endDateTime)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodesTopNCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecord),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("Status", DbType.Int32, (int)status),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodes(int maxRecord, string recursiveLocations, CommonEntities.StatusEnum status, DateTime? startDateTime, DateTime? endDateTime)
        {
            try
            {
                string ExecuteQuery = String.Concat(recursiveLocations, SQLProvider.GetCustomerEpisodesByLocationTopNCommand);
                return this.Gateway.ExecuteQueryDataSet(ExecuteQuery, Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecord),
                    new StoredProcInParam("Status", DbType.Int32, (int)status),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //DO MIGUEL PARA SOLUCIONAR LA CAPTURA DEL ARBOL DE ACUERDOS DENTRO DEL EPISODIO 
        public Tuple<DateTime?, DateTime?> GetEpisodeDateTimes(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetEpisodeDateTimesCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    if (IsEmptyReader(reader)) return null;
                    Tuple<DateTime?, DateTime?> dates = new Tuple<DateTime?, DateTime?>(reader["StartDateTime"] as DateTime? ?? null, reader["EndDateTime"] as DateTime? ?? null);
                    return dates;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodesPrincipalGuarantor(int careCenterID, string concatEpisodeTypes, DateTime? fromDate, DateTime? toDate, CommonEntities.StatusEnum status)
        {
            try
            {
                string ExecuteQuery = String.Concat(concatEpisodeTypes, SQLProvider.GetCustomerEpisodesPrincipalGuarantorCommand);
                return this.Gateway.ExecuteQueryDataSet(ExecuteQuery, Common.Entities.TableNames.IDDescriptionTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, fromDate),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, toDate),
                    new StoredProcInParam("Status", DbType.Int16, (Int16)status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
            throw new NotImplementedException();
        }

        //DO ALBERTO
        //ESTOS MÉTODOS DEVUELVEN UNA LISTA DE [ID] DE LOS EPISODIOS QUE TENGAN UNA POLIZA DE LOS TIPOS SELECCIONADOS
        public DataSet GetEpisodesByPolicyTypes(string entities, string episodes)
        {
            try
            {
                string stringWhere = (!string.IsNullOrEmpty(entities) || !string.IsNullOrEmpty(episodes))
                    ? String.Concat("WHERE ", (!string.IsNullOrEmpty(episodes)) ? string.Concat("(CE.[ID] IN ", episodes, ") ") : string.Empty,
                        (!string.IsNullOrEmpty(entities)) ? string.Concat((!string.IsNullOrEmpty(episodes)) ? "AND " : string.Empty, "(CP.PolicyTypeID IN ", entities, ")") : string.Empty)
                    : String.Empty;

                string ExecuteQuery = String.Concat(SQLProvider.GetEpisodesByPolicyTypesCommand, Environment.NewLine, stringWhere);

                return this.Gateway.ExecuteQueryDataSet(ExecuteQuery, Administrative.Entities.TableNames.CustomerEpisodeTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeIDAndPredecessorIDsByCustomerID(int customerID)
        {
            try
            {
                string sqlQuery = string.Concat(SQLProvider.GetCustomerEpisodeIDAndPredecessorIDsCommand, Environment.NewLine, "WHERE (CE.CustomerID=", customerID.ToString(), ")");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.CustomerEpisodeTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerEpisodeIDAndPredecessorIDsByStringListCustomerEpisodeIDs(string customerIDs)
        {
            try
            {
                string sqlQuery = string.Concat(SQLProvider.GetCustomerEpisodeIDAndPredecessorIDsCommand, Environment.NewLine, "WHERE (CE.CustomerID IN (", customerIDs, "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.CustomerEpisodeTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Boolean ExistsByProcessChartID(int processChartID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerEpisodeByProcessChartIDCommand,
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID)))
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

        public int GetCustomerID(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerIDByCustomerEpisodeIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["CustomerID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public DataSet GetCustomerEpisodeInfoByDateRange(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeInfoByDateRangeCommand,
                    Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetEpisodeStatsByEpisodeAndRoutineTypeAndStatus(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetEpisodeStatsByEpisodeAndRoutineTypeAndStatusCommand,
                    Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetEpisodeStatsByRoutineTypeAndStatus(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetEpisodeStatsByRoutineTypeAndStatusCommand,
                    Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeBasic(int[] processChartIDs, BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
            int[] locations, int[] careCenterIDs, int assistanceServiceID, DateTime? startDateTime, DateTime? endDateTime)
        {
            try
            {
                string whereProcessChartIDs = string.Empty;
                string whereLocationIDs = string.Empty;
                if (processChartIDs != null && processChartIDs.Length > 0)
                {
                    whereProcessChartIDs = StringUtils.BuildIDString(processChartIDs);
                }
                if (locations != null && locations.Length > 0)
                {
                    whereLocationIDs = StringUtils.BuildIDString(locations);
                }


                /// esta query sólo trae la información que está en customerprocess y no tiene where
                string finalQuery = string.Format(SQLProvider.GetCustomerEpisodeBasicCommand);

                string includes = string.Empty;
                string wheres = string.Empty;
                if ((processChartIDs != null && processChartIDs.Length > 0) ||
                    (locations != null && locations.Length > 0) ||
                    step != BasicProcessStepsEnum.None ||
                    status != CommonEntities.StatusEnum.None ||
                    (careCenterIDs != null && careCenterIDs.Length > 0) || assistanceServiceID > 0 ||
                    startDateTime != null || endDateTime != null)
                {
                    bool andPossible = false;
                    wheres = string.Concat(wheres, Environment.NewLine, "WHERE ");
                    if (processChartIDs != null && processChartIDs.Length > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        /// YA ESTÁN LOS JOIN

                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " PC.[ID] IN (", whereProcessChartIDs, ") ");
                        andPossible = true;
                    }
                    if (locations != null && locations.Length > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        /// YA ESTÁN LOS JOIN

                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CA.CurrentLocationID IN (", whereLocationIDs, ") ");
                        andPossible = true;
                    }
                    if (assistanceServiceID > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        /// YA ESTÁN LOS JOIN

                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CESR.AssistanceServiceID = ", assistanceServiceID.ToString(), " ");
                        andPossible = true;
                    }
                    if (careCenterIDs != null && careCenterIDs.Length > 0)
                    {
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CP.CareCenterID IN (", StringUtils.BuildIDString(careCenterIDs), ") ");
                        andPossible = true;
                    }
                    if (step != BasicProcessStepsEnum.None)
                    {
                        /// primero analizo si tengo que poner mas joins
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerProcessStepsRel CPSR WITH(NOLOCK) ON CP.[ID] = CPSR.CustomerProcessID ");
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CPSR.Step = ", ((long)step).ToString(), " AND CPSR.CurrentStepID > 0 ");
                        andPossible = true;
                        if (status != CommonEntities.StatusEnum.None)
                            wheres = string.Concat(wheres, " AND CPSR.StepStatus = ", ((int)status).ToString(), " ");
                    }
                    if (startDateTime != null)
                    {
                        if (step == BasicProcessStepsEnum.None)
                        {
                            wheres = string.Concat(wheres, Environment.NewLine,
                                (andPossible) ? " AND " : string.Empty,
                                " (CP.CloseDateTime IS NULL OR CP.CloseDateTime >= @StartDateTime) ");
                            andPossible = true;
                        }
                        else
                        {
                            ////////////////////////////////////////////////////////////////////////////
                            ///
                            ///FALTA LA INCLUSIÓN DE LA COLUMNA EndDateTime en la tabla CustomerProcessStepsRel. Esto tengo que explicarselo a JL cuando hagamos las UofW de CustomerProcess
                            ///
                            ////////////////////////////////////////////////////////////////////////////
                            //wheres = string.Concat(wheres, Environment.NewLine,
                            //    (andPossible) ? " AND " : string.Empty,
                            //    " (CPSR.StepDateTime IS NULL OR CPSR.StepDateTime >= @StartDateTime) ");
                            //andPossible = true;
                            wheres = string.Concat(wheres, Environment.NewLine,
                                (andPossible) ? " AND " : string.Empty,
                                " (CP.CloseDateTime IS NULL OR CP.CloseDateTime >= @StartDateTime) ");
                            andPossible = true;
                        }
                    }
                    if (endDateTime != null)
                    {
                        if (step == BasicProcessStepsEnum.None)
                        {
                            wheres = string.Concat(wheres, Environment.NewLine,
                                (andPossible) ? " AND " : string.Empty,
                                " (CP.RegistrationDateTime <= @EndDateTime) ");
                            andPossible = true;
                        }
                        else
                        {
                            wheres = string.Concat(wheres, Environment.NewLine,
                                (andPossible) ? " AND " : string.Empty,
                                " (CPSR.StepDateTime IS NULL OR CPSR.StepDateTime <= @EndDateTime) ");
                            andPossible = true;
                        }
                    }
                    if (status != CommonEntities.StatusEnum.None && step == BasicProcessStepsEnum.None)
                    {
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CP.Status = ", ((int)status).ToString(), " ");
                        andPossible = true;
                    }
                }
                finalQuery = string.Concat(finalQuery, includes, wheres);
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }


        }

        //Optimizacion de censos
        //public DataSet GetCustomerEpisodeBasicByQueryCustomerProcessID(string queryFilterCustomerProcessMaxRows,
        //    DateTime? startDateTime, DateTime? endDateTime)
        //{
        //    try
        //    {
        //        string finalQuery = string.Format(SQLProvider.GetCustomerEpisodeBasicWithQueryCustomerProcessFilterMaxRowCommand, queryFilterCustomerProcessMaxRows);

        //        return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable,
        //            new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
        //            new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public DataSet GetCustomerEpisodeBasicByQueryCustomerProcessID(IEnumerable<int> ceIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeBasicWithQueryCustomerProcessFilterMaxRowCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable,
                    new StoredProcInTVPIntegerParam("TVPTable", ceIDs)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }



        public DataSet GetCustomerEpisodeBasic(int customerEpisodeID)
        {
            try
            {
                string finalQuery = string.Format(SQLProvider.GetCustomerEpisodeBasicCommand);
                finalQuery = string.Concat(finalQuery, Environment.NewLine, "WHERE CE.[ID] = ", customerEpisodeID.ToString());
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerEpisodeBasicByIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                string finalQuery = string.Concat(SQLProvider.GetCustomerEpisodeBasicCommand, Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ");
                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeBasicTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        #endregion

        public bool GetPostRelatedEpisodes(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetPostRelatedEpisodesCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("Closed", DbType.Int32, (int)CommonEntities.StatusEnum.Closed),
                    new StoredProcInParam("Active", DbType.Int32, (int)CommonEntities.StatusEnum.Active)))
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

        public bool GetEpisodeIsInvoiced(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetEpisodeIsInvoicedCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Confirmed)))
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

        public bool GetEpisodeIsCodified(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetEpisodeIsCodifiedCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Completed)
                    ))
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

        public DataSet GetCustomerEpisodeSimpleBase(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                /// esta query sólo trae la información que está en customerprocess y no tiene where
                string finalQuery = string.Concat(SQLProvider.GetCustomerEpisodeSimpleBaseByIDCommand, Environment.NewLine, "WHERE CE.[ID] IN (", whereEpisodeIDs, ") ");
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerRelatedEpisodeInfoDTOTable);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int GetCustomerEpisodeID(int careCenterID, string careCenterName, int customerID, string customerEpisodeNumber, bool episodeClose)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDByCareCenterCommand,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("CareCenterName", DbType.String, careCenterName),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerEpisodeNumber", DbType.String, customerEpisodeNumber),
                    new StoredProcInParam("Status", DbType.Int32, (episodeClose) ? (int)CommonEntities.StatusEnum.Closed : (int)CommonEntities.StatusEnum.Active)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : reader["ID"] as int? ?? 0;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public DataSet GetCustomerEpisodeWithOutStatus(int careCenterID, string careCenterName, int customerID, string customerEpisodeNumber)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeWithOutStatusByCareCenterCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("CareCenterName", DbType.String, careCenterName),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerEpisodeNumber", DbType.String, customerEpisodeNumber));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetLastCustomerEpisodeIDByCustomerID(int careCenterID, string careCenterName, int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetLastCustomerEpisodeIDByCustomerIDCommand,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("CareCenterName", DbType.String, careCenterName),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : reader["ID"] as int? ?? 0;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public int[] GetLinkedEpisodeIDs(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetLinkedCustomerEpisodeIDsCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ClosedStatus", DbType.Int32, CommonEntities.StatusEnum.Closed),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, CommonEntities.StatusEnum.Active)))
                {
                    List<int> result = new List<int>();
                    while (reader.Read())
                    {
                        int id = reader["ID"] as int? ?? 0;
                        if (id > 0)
                        {
                            result.Add(id);
                        }
                    }

                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetPredecessorEpisodeID(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetPredecessorEpisodeIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ClosedStatus", DbType.Int32, CommonEntities.StatusEnum.Closed),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, CommonEntities.StatusEnum.Active)))
                {
                    return (IsEmptyReader(reader)) ? 0 : reader["ID"] as int? ?? 0;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public bool HasCustomerEpisodes(int customerID, CommonEntities.StatusEnum status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDsByCustomerAndStatusCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("Status", DbType.Int32, status)
                    ))
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

        public int CustomerEpisodeID(int customerID, CommonEntities.StatusEnum status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeIDsByCustomerAndStatusCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("Status", DbType.Int32, status)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : reader["ID"] as int? ?? 0;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetCustomerEpisodeCustomerPolicy(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeCustomerPolicyCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public string GetCustomerEpisodeNumber(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeNumberByIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)
                    ))
                {
                    return (IsEmptyReader(reader))
                        ? string.Empty
                        : reader[0] as string;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return string.Empty;
            }
        }

        public EpisodeCaseEnum GetEpisodeCase(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerEpisodeCaseByIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)
                    ))
                {
                    return (IsEmptyReader(reader))
                        ? EpisodeCaseEnum.None
                        : (EpisodeCaseEnum)(reader[0] as short? ?? 0);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return EpisodeCaseEnum.None;
            }
        }

        public int[] GetCustomerEpisodeCareCenters(int[] customerEpisodeIDs)
        {
            try
            {
                string idString = StringUtils.BuildIDString(customerEpisodeIDs);
                string query = string.Format(SQLProvider.GetCustomerEpisodeCareCenterIDsCommand, idString);
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(query))
                {
                    List<int> careCenterIDs = new List<int>();
                    while (reader.Read())
                    {
                        careCenterIDs.Add(reader[0] as int? ?? 0);
                    }

                    return careCenterIDs.Where(id => id > 0)
                                        .Distinct()
                                        .ToArray();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetStartDateTimeAndEndDateTimeOfCustomerEpisodeByID(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetStartDateTimeAndEndDateTimeOfCustomerEpisodeByIDCommand,
                    Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetAllRelatedCustomerEpisodeIDs(int customerEpisodeID)
        {
            try
            {
                if (customerEpisodeID <= 0)
                    return null;

                DataSet ds = new DataSet();

                DatasetUtils.MergeTable(this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllCustomerEpisodePredecessorsByCustomerEpisodeIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)),
                    ds, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable);

                DatasetUtils.MergeTable(this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllCustomerEpisodeSucessorsByCustomerEpisodeIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)),
                    ds, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable);

                return ds;

                //OLD
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllRelatedCustomerEpisodeIDsCommand,
                //    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                //    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetAllRelatedCustomerEpisodeIDsByMedicalEpisodeIDs(int[] medicalEpisodeIDs)
        {
            try
            {
                if (medicalEpisodeIDs == null || medicalEpisodeIDs.Length <= 0) return null;
                medicalEpisodeIDs = medicalEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllRelatedCustomerEpisodeIDsByMedicalEpisodeIDsCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInTVPIntegerParam("TVPTable", medicalEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public void SaveCustomerEpisodeAuthorizations(int customerEpisodeID,
            int customerEpisodeAuthorizationID, int auth_InsurerID, int auth_AuthorizationTypeID, string auth_AuthorizationDocumentNumber,
            string auth_AuthorizedBy, Int16 auth_Status, DateTime auth_LastUpdated, string auth_ModifiedBy, bool auth_IsChipCard,

            int customerEpisodeAuthorizationEntryID, int authorizedActID, string authorizedElementName, int authorizedElementID, Int16 status, DateTime lastUpdated,
            string modifiedBy, bool isChipCard, int authorizationTypeID, string authorizationDocumentNumber,

            bool customerEpisodeAuthorizationNEW, bool customerEpisodeAuthorizationUPD, bool customerEpisodeAuthorizationEntryNEW)
        {
            try
            {
                this.Gateway.ExecuteStoredProcedure("saveCustomerEpisodeAuthorizations",
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CustomerEpisodeAuthorizationID", DbType.Int32, customerEpisodeAuthorizationID),
                    new StoredProcInParam("Auth_InsurerID", DbType.Int32, auth_InsurerID),
                    new StoredProcInParam("Auth_AuthorizationTypeID", DbType.Int32, auth_AuthorizationTypeID),
                    new StoredProcInParam("Auth_AuthorizationDocumentNumber", DbType.String, auth_AuthorizationDocumentNumber),
                    new StoredProcInParam("Auth_AuthorizedBy", DbType.String, auth_AuthorizedBy),
                    new StoredProcInParam("Auth_Status", DbType.Int16, auth_Status),
                    new StoredProcInParam("Auth_LastUpdated", DbType.DateTime, auth_LastUpdated),
                    new StoredProcInParam("Auth_ModifiedBy", DbType.String, auth_ModifiedBy),
                    new StoredProcInParam("Auth_IsChipCard", DbType.Boolean, auth_IsChipCard),
                    new StoredProcInParam("CustomerEpisodeAuthorizationEntryID", DbType.Int32, customerEpisodeAuthorizationEntryID),
                    new StoredProcInParam("AuthorizedActID", DbType.Int32, authorizedActID),
                    new StoredProcInParam("AuthorizedElementName", DbType.String, authorizedElementName),
                    new StoredProcInParam("AuthorizedElementID", DbType.Int32, authorizedElementID),
                    new StoredProcInParam("Status", DbType.Int16, status),
                    new StoredProcInParam("LastUpdated", DbType.DateTime, lastUpdated),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy),
                    new StoredProcInParam("IsChipCard", DbType.Boolean, isChipCard),
                    new StoredProcInParam("AuthorizationTypeID", DbType.Int32, authorizationTypeID),
                    new StoredProcInParam("AuthorizationDocumentNumber", DbType.String, authorizationDocumentNumber),

                    new StoredProcInParam("CustomerEpisodeAuthorizationNEW", DbType.Boolean, customerEpisodeAuthorizationNEW),
                    new StoredProcInParam("CustomerEpisodeAuthorizationUPD", DbType.Boolean, customerEpisodeAuthorizationUPD),
                    new StoredProcInParam("CustomerEpisodeAuthorizationEntryNEW", DbType.Boolean, customerEpisodeAuthorizationEntryNEW));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
            }
        }

        public int ObtenerPredecessorID(int customerEpisodeID)
        {

            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ObtenerPredecessorIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["PredecessorID"].ToString());

                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }

        }

        public int ObtenerEpisodioDestinoID(int predecessorID)
        {

            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ObtenerEpisodioDestinoIDCommand,
                    new StoredProcInParam("PredecessorID", DbType.Int32, predecessorID)
                    ))
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

        public DataSet GetCustomerEpisodeIDByPrescRequest(int[] prescriptionRequestIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerEpisodeIDByPrescriptionRequestCommand, SII.HCD.Administrative.Entities.TableNames.CustomerEpisodeTable,
                    new StoredProcInTVPIntegerParam("TVPTable", prescriptionRequestIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }





    }
}
