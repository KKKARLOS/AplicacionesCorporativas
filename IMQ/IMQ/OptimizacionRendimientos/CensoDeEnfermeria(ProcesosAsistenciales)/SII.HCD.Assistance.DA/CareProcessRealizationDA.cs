using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Assistance.DA
{
    public class CareProcessRealizationDA : DAServiceBase
    {
        #region Field length constants
        #endregion

        #region Private methods
        private string BuildActionStatusString(int statusSelected)
        {
            if (statusSelected <= 0)
                return "0";

            StringBuilder sb = new StringBuilder();
            foreach (ActionStatusEnum action in Enum.GetValues(typeof(ActionStatusEnum)))
            {
                if ((statusSelected & (int)action) == (int)action)
                {
                    sb.Append(",");
                    sb.Append(((int)action).ToString());
                }
            }

            return sb.ToString().Substring(1);
        }

        private int[] BuildActionStatusArray(int statusSelected)
        {
            List<int> statusList = new List<int>();
            if (statusSelected <= 0)
            {
                statusList.Add(0);
                return statusList.ToArray();
            }

            foreach (ActionStatusEnum action in Enum.GetValues(typeof(ActionStatusEnum)))
            {
                if ((statusSelected & (int)action) == (int)action)
                {
                    statusList.Add((int)action);
                }
            }
            if (statusList.Count <= 0)
            {
                statusList.Add(0);
            }
            return statusList.ToArray();
        }
        #endregion

        #region Constructors
        public CareProcessRealizationDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CareProcessRealizationDA(Gateway gateway) : base(gateway) { }
        #endregion

        public bool HasActivity(int customerAssistancePlanID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.HasActivityCommand, SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable,
                    new StoredProcInParam("CustomerAssistancePlanID", DbType.Int32, customerAssistancePlanID));
                if (result.Tables[SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable].Rows.Count > 0)
                {
                    return (SIIConvert.ToInteger(result.Tables[SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable].Rows[0]["Qty"].ToString()) > 0);
                }
                else return false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool HasOrders(int customerEpisodeID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.HasOrdersCommand, SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
                if (result.Tables[SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable].Rows.Count > 0)
                {
                    return (SIIConvert.ToInteger(result.Tables[SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable].Rows[0]["Qty"].ToString()) > 0);
                }
                else return false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool HasPrescriptions(int customerEpisodeID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.HasPrescriptionsCommand, SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
                if (result.Tables[SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable].Rows.Count > 0)
                {
                    return (SIIConvert.ToInteger(result.Tables[SII.HCD.Assistance.Entities.TableNames.CareProcessRealizationTable].Rows[0]["Qty"].ToString()) > 0);
                }
                else return false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public int GetCustomerSpecialCategories(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerSpecialCategoriesCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["SpCategories"].ToString(), 0);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        #region CustomerCareProcessEntity members

        public DataSet GetCustomerCareProcessListStoreProcedure(int[] locationIDs, DateTime? fromDate, DateTime? toDate,
            int selectedStatus, int[] defaultIdentifierTypeIDs, int observationExceptionalInfoTemplateID, int observationExceptionalInfoLOPDID,
            int[] procedurePrescriptionIDs)
        {
            try
            {
                if (selectedStatus != 127)
                {
                    int[] actionStatus = this.BuildActionStatusArray(selectedStatus);
                          
                    return this.Gateway.ExecuteStoredProcedureDataSet("GetCustomerCareProcessList",
                        //Assistance.Entities.TableNames.CustomerCareProcessTable,
                        new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                        new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                        new StoredProcInTVPIntegerParam("TVPLocations", locationIDs),
                        new StoredProcInParam("ObservationExceptionalInfoTemplateID", DbType.Int32, observationExceptionalInfoTemplateID),
                        new StoredProcInParam("ObservationExceptionalInfoLOPDID", DbType.Int32, observationExceptionalInfoLOPDID),

                        new StoredProcInTVPIntegerParam("ProcedurePrescriptionIDs", procedurePrescriptionIDs),
                        new StoredProcInTVPIntegerParam("DefaultIdentiferTypes", defaultIdentifierTypeIDs),
                        new StoredProcInTVPIntegerParam("SelectedStatus", actionStatus));
                    /*
                    SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@StartDateTime", SqlDbType.DateTime, 8, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
						ParametroSql.add("@EndDateTime", SqlDbType.DateTime, 8, (toDate != null) ? (object)toDate : (object)DBNull.Value),
						ParametroSql.add("@TVPLocations", SqlDbType.Structured, locationIDs),
						ParametroSql.add("@ObservationExceptionalInfoTemplateID", SqlDbType.Int, 4, observationExceptionalInfoTemplateID),
						ParametroSql.add("@ObservationExceptionalInfoLOPDID", SqlDbType.Int, 4, observationExceptionalInfoLOPDID),
						ParametroSql.add("@ProcedurePrescriptionIDs", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(procedurePrescriptionIDs)),
						ParametroSql.add("@DefaultIdentiferTypes", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(defaultIdentifierTypeIDs)),
						ParametroSql.add("@SelectedStatus", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(actionStatus))
					};

                    return SqlHelper.ExecuteDataset("GetCustomerCareProcessList", aParam);*/ 	
                }
                else
                {
                   
                    return this.Gateway.ExecuteStoredProcedureDataSet("GetCustomerCareProcessListWithoutStatus",
                        //Assistance.Entities.TableNames.CustomerCareProcessTable,
                        new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                        new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                        new StoredProcInTVPIntegerParam("TVPLocations", locationIDs),
                        new StoredProcInParam("ObservationExceptionalInfoTemplateID", DbType.Int32, observationExceptionalInfoTemplateID),
                        new StoredProcInParam("ObservationExceptionalInfoLOPDID", DbType.Int32, observationExceptionalInfoLOPDID),

                        new StoredProcInTVPIntegerParam("ProcedurePrescriptionIDs", procedurePrescriptionIDs),
                        new StoredProcInTVPIntegerParam("DefaultIdentiferTypes", defaultIdentifierTypeIDs));
                     /*
                    SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@StartDateTime", SqlDbType.DateTime, 8, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
						ParametroSql.add("@EndDateTime", SqlDbType.DateTime, 8, (toDate != null) ? (object)toDate : (object)DBNull.Value),
						ParametroSql.add("@TVPLocations", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(locationIDs)),
						ParametroSql.add("@ObservationExceptionalInfoTemplateID", SqlDbType.Int, 4, observationExceptionalInfoTemplateID),
						ParametroSql.add("@ObservationExceptionalInfoLOPDID", SqlDbType.Int, 4, observationExceptionalInfoLOPDID),
                        ParametroSql.add("@ProcedurePrescriptionIDs", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(procedurePrescriptionIDs)),
                        ParametroSql.add("@DefaultIdentiferTypes", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(defaultIdentifierTypeIDs))
					};
                    return SqlHelper.ExecuteDataset("GetCustomerCareProcessListWithoutStatus", aParam);  */
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerCareProcessEntity
        public DataSet GetCustomerCareProcessList(int[] locationIDs, DateTime? fromDate, DateTime? toDate,
            int[] defaultIdentifierTypeIDs, int maxRecords)
        {
            try
            {
                //string sqlQuery = string.Concat(string.Format(SQLProvider.GetCustomerCareProcessTopNByLocationIDAndDateNurseStationCommand,
                //    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                //        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                //                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                //          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR WITH(NOLOCK) JOIN IdentifierType IT WITH(NOLOCK) ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                //                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                //        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));

                //sqlQuery += string.Concat(" AND (CE.Status != @Status) ");

                //if (fromDate != null)
                //    sqlQuery += string.Concat("AND ((CE.Status = @ActiveStatus) OR ((CE.EndDateTime>=@StartDateTime) AND (CE.Status=@ClosedStatus))) ");

                //if (toDate != null)
                //    sqlQuery += string.Concat("AND (CE.StartDateTime<=@EndDateTime) ");

                //if ((locationIDs != null) && (locationIDs.Length > 0))
                //    sqlQuery += string.Concat("AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");

                //if ((locationIDs != null) && (locationIDs.Length > 0))
                //{
                //    sqlQuery += string.Concat("AND (CA.CurrentLocationID IN (SELECT [ID] FROM @TVPTable))");
                //}

                return this.Gateway.ExecuteStoredProcedureDataSet(SQLProvider.GetCustomerCareProcessTopNByLocationIDAndDateNurseStationCommand, 
                    Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ClosedStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Closed),
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInTVPIntegerParam("TVPTableLOC", locationIDs),
                    new StoredProcInTVPIntegerParam("TVPTableIdentifiers", defaultIdentifierTypeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCareProcessList(int[] locationIDs, DateTime? fromDate, DateTime? toDate,
            int[] defaultIdentifierTypeIDs, StationModeEnum stationMode, int maxRecords)
        {
            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.GetCustomerCareProcessTopNByLocationIDAndDateCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR WITH(NOLOCK) JOIN IdentifierType IT WITH(NOLOCK) ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));

                sqlQuery += string.Concat(" AND (CE.Status != @Status) ");

                if (fromDate != null)
                    sqlQuery += string.Concat("AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlQuery += string.Concat("AND (CE.StartDateTime<=@EndDateTime) ");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                {
                    switch (stationMode)
                    {
                        case StationModeEnum.DiagnosticWorkStation:
                        case StationModeEnum.SurgeryWorkStation:
                            sqlQuery += string.Concat("AND (EXISTS(SELECT RA.[ID] FROM RoutineAct RA WITH(NOLOCK) WHERE RA.EpisodeID=CE.[ID] AND RA.LocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            sqlQuery += string.Concat("OR EXISTS(SELECT CR.[ID] FROM CustomerRoutine CR WITH(NOLOCK) WHERE CR.EpisodeID=CE.[ID] AND CR.LocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            sqlQuery += string.Concat("OR EXISTS(SELECT PA.[ID] FROM ProcedureAct PA WITH(NOLOCK) WHERE PA.EpisodeID=CE.[ID] AND PA.LocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            sqlQuery += string.Concat("OR EXISTS(SELECT CP.[ID] FROM CustomerProcedure CP WITH(NOLOCK) WHERE CP.EpisodeID=CE.[ID] AND CP.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")) )");
                            break;
                    }
                }

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCareProcessListForDiets(int careCenterID, int[] locationIDs, int locationClassID,
            DateTime? fromDate, DateTime? toDate, int[] defaultIdentifierTypeIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.GetCustomerCareProcessTopNByLocationIDAndDateCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR JOIN IdentifierType IT ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));

                sqlQuery += string.Concat(" AND (CE.Status != @Status) ");
                sqlQuery += string.Concat(" AND (CP.CareCenterID = @CareCenterID) ");

                if (fromDate != null)
                    sqlQuery += string.Concat("AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlQuery += string.Concat("AND (CE.StartDateTime<=@EndDateTime) ");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlQuery += string.Concat("AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                if (locationClassID > 0)
                    sqlQuery += string.Concat(" AND LC.ID=", locationClassID.ToString());

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCareProcessListForDietsForReports(int careCenterID, int[] locationIDs, int locationClassID,
            DateTime? fromDate, DateTime? toDate, int[] defaultIdentifierTypeIDs, int maxRecords)
        {
            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.GetCustomerCareProcessTopNByLocationIDAndDateForReportsCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR JOIN IdentifierType IT ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));

                sqlQuery += string.Concat(" AND (CE.Status != @Status) ");
                sqlQuery += string.Concat(" AND (CP.CareCenterID = @CareCenterID) ");

                if (fromDate != null)
                    sqlQuery += string.Concat("AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlQuery += string.Concat("AND (CE.StartDateTime<=@EndDateTime) ");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlQuery += string.Concat("AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                if (locationClassID > 0)
                    sqlQuery += string.Concat(" AND LC.ID=", locationClassID.ToString());

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Cancelled),
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCareProcessByID(int customerCarePlanID, int[] defaultIdentifierTypeIDs)
        {
            try
            {
                string sqlQuery = string.Concat("SELECT DISTINCT TOP 1 ", string.Format(SQLProvider.GetCustomerCareProcessByIDCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR JOIN IdentifierType IT ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));
                sqlQuery += "WHERE (CAP.ID = @CustomerCarePlanID) ";

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("CustomerCarePlanID", DbType.Int32, customerCarePlanID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCareProcessByCustomerID(int customerID, int[] defaultIdentifierTypeIDs)
        {
            try
            {
                string sqlQuery = string.Concat("SELECT DISTINCT ", string.Format(SQLProvider.GetCustomerCareProcessByIDCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR WITH(NOLOCK) JOIN IdentifierType IT WITH(NOLOCK) ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));
                sqlQuery += "WHERE (CAP.CustomerID = @CustomerID) ";

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerIDsWithCareProcessListByLocations(int[] locations)
        {
            try
            {
                String whereINByLocations = String.Empty;
                if ((locations != null)
                    && (locations.Length > 0))
                {
                    whereINByLocations = String.Join(",", Array.ConvertAll(locations, new Converter<int, string>(m => m.ToString())));
                }
                string finalQuery = String.Format(SQLProvider.GetCustomerIDsWithCareProcessListByLocationsCommand, whereINByLocations);
                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    SII.HCD.Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("ThisDateTime", DbType.DateTime, DateTime.Now));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCareProcessByCustomerEpisodeIDs(int[] episodeIDs, int[] defaultIdentifierTypeIDs)
        {
            if (episodeIDs == null || episodeIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctCustomerCareProcessJoinWhereStandardCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR WITH(NOLOCK) JOIN IdentifierType IT WITH(NOLOCK) ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));

                sqlQuery += string.Concat(" AND (CE.[ID] IN (", StringUtils.BuildIDString(episodeIDs), ")) ");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCareProcessByCustomerProcessIDs(int[] customerProcessIDs, int[] defaultIdentifierTypeIDs)
        {
            if (customerProcessIDs == null || customerProcessIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctCustomerCareProcessJoinWhereCustomerProcessCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR WITH(NOLOCK) JOIN IdentifierType IT WITH(NOLOCK) ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));

                sqlQuery += string.Concat(" AND (CP.[ID] IN (", StringUtils.BuildIDString(customerProcessIDs), ")) ");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    Assistance.Entities.TableNames.CustomerCareProcessTable,
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerSpecialCategories
        public DataSet GetCustomerSpecialCategoriesByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string where = "WHERE (CE.ID=@CustomerEpisodeID)";
                string sqlQuery = string.Concat(SQLProvider.SelectDictinctCustomerSpecialCategoriesJoinStandardCustomerEpisodeCommand,
                    Environment.NewLine, where, Environment.NewLine,
                    "GROUP BY CE.CustomerID, CO.SpecialCategoryType");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerSpecialCategoriesTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerSpecialCategoriesByCustomerEpisodeIDs(int[] episodeIDs)
        {
            if (episodeIDs == null || episodeIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDictinctCustomerSpecialCategoriesJoinStandardCustomerEpisodeCommand,
                    Environment.NewLine, "WHERE (CE.[ID] IN (", StringUtils.BuildIDString(episodeIDs), ")) ", Environment.NewLine,
                    "GROUP BY CE.CustomerID, CO.SpecialCategoryType");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerSpecialCategoriesTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerSpecialCategoriesByCustomerID(int customerID)
        {
            if (customerID <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDictinctCustomerSpecialCategoriesJoinStandardCustomerCommand,
                    Environment.NewLine, "WHERE (C.[ID]=@CustomerID) ", Environment.NewLine,
                    "GROUP BY C.[ID], CO.SpecialCategoryType");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerSpecialCategoriesTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public DataSet GetCustomerSpecialCategoriesByCustomerIDs(int[] customerIDs)
        {
            if (customerIDs == null || customerIDs.Length <= 0)
                return null;

            try
            {

                customerIDs = customerIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                string sqlQuery = string.Concat(SQLProvider.SelectDictinctCustomerSpecialCategoriesJoinStandardCustomerCommand,
                    Environment.NewLine, "JOIN @TVPTable TVP ON CO.CustomerID=TVP.[ID] ", Environment.NewLine,
                    "GROUP BY C.[ID], CO.SpecialCategoryType");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerSpecialCategoriesTable,
                     new StoredProcInTVPIntegerParam("TVPTable", customerIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }



        public DataSet GetCustomerSpecialCategoriesByCustomerProcessIDs(int[] customerProcessIDs)
        {
            if (customerProcessIDs == null || customerProcessIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDictinctCustomerSpecialCategoriesJoinStandardCustomerProcessCommand,
                    Environment.NewLine, "WHERE (CP.[ID] IN (", StringUtils.BuildIDString(customerProcessIDs), ")) ", Environment.NewLine,
                    "GROUP BY CP.CustomerID, CO.SpecialCategoryType");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerSpecialCategoriesTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //ServiceActBasicEntity
        public DataSet GetServicesActsByAssistancePlanOfProcedureActs(int assistancePlanID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerServiceActsByAssistancePlanOfProcedureActsCommand,
                    Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("AssistancePlanID", DbType.Int32, assistancePlanID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerServiceActs(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate,
            int selectedStatus)
        {
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerServiceActsRoutinesByLocationIDAndDateCommand, Environment.NewLine,
                    "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))");

                if (fromDate != null)
                    sqlRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");

                string sqlProcedureQuery = string.Concat(string.Format(SQLProvider.GetCustomerServiceActsProceduresByLocationIDAndDateCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                    Environment.NewLine, "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                if (fromDate != null)
                    sqlProcedureQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlProcedureQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlProcedureQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerServiceActsByDiets(int careCenterID, int[] locationIDs, int routineTypeID,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (routineTypeID <= 0 || careCenterID <= 0) return null;
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerServiceActsRoutinesByLocationIDAndDateCommand, Environment.NewLine,
                    "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))");

                sqlRoutineQuery += string.Concat(" AND (CP.CareCenterID=@CareCenterID) ");
                sqlRoutineQuery += string.Concat(" AND (RT.ID=@RoutineTypeID) ");

                if (fromDate != null)
                    sqlRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime))");

                if (toDate != null)
                    sqlRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime)");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlRoutineQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("RoutineTypeID", DbType.Int32, routineTypeID),

                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerServiceActs(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate,
            int selectedStatus, StationModeEnum stationMode)
        {
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerServiceActsRoutinesByLocationIDAndDateCommand, Environment.NewLine,
                    "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))");

                if (fromDate != null)
                    sqlRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                {
                    switch (stationMode)
                    {
                        case StationModeEnum.NurseWorkStation:
                            sqlRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            break;
                        case StationModeEnum.DiagnosticWorkStation:
                        case StationModeEnum.SurgeryWorkStation:
                            sqlRoutineQuery += string.Concat("AND RA.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                            break;
                    }
                }

                string sqlProcedureQuery = string.Concat(string.Format(SQLProvider.GetCustomerServiceActsProceduresByLocationIDAndDateCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                    Environment.NewLine, "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                if (fromDate != null)
                    sqlProcedureQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlProcedureQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                {
                    switch (stationMode)
                    {
                        case StationModeEnum.NurseWorkStation:
                            sqlRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            break;
                        case StationModeEnum.DiagnosticWorkStation:
                        case StationModeEnum.SurgeryWorkStation:
                            sqlProcedureQuery += string.Concat("AND PA.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                            break;
                    }
                }

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerServiceActsByCustomerEpisodeID(int customerEpisodeID, int[] procPrescriptionIDs, int selectedStatus)
        {
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerServiceActsRoutinesCommand, Environment.NewLine,
                    "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))");

                sqlRoutineQuery += "  AND (CE.ID = @CustomerEpisodeID)";

                string sqlProcedureQuery = string.Concat(string.Format(SQLProvider.GetCustomerServiceActsProceduresCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                    Environment.NewLine, "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                sqlProcedureQuery += "  AND (CE.ID = @CustomerEpisodeID)";

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerServiceActsByCustomerID(int customerID, int[] procPrescriptionIDs, int selectedStatus)
        {
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerServiceActsRoutinesCommand, Environment.NewLine,
                    "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))");

                sqlRoutineQuery += "  AND (CE.CustomerID = @CustomerID)";

                string sqlProcedureQuery = string.Concat(string.Format(SQLProvider.GetCustomerServiceActsProceduresCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                    Environment.NewLine, "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                sqlProcedureQuery += "  AND (CE.CustomerID = @CustomerID)";

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public DataSet GetCustomerServiceActsByCustomerEpisodeIDs(int[] customerEpisodeIDs, int[] procPrescriptionIDs, int selectedStatus)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();


                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerServiceActsRoutinesCommand,
                    Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ",
                    Environment.NewLine, "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))");

                //sqlRoutineQuery += "  AND (CE.CustomerID = @CustomerID)";

                string sqlProcedureQuery = string.Concat(string.Format(SQLProvider.GetCustomerServiceActsProceduresCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                    Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ",
                    Environment.NewLine, "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                //sqlProcedureQuery += "  AND (CE.CustomerID = @CustomerID)";

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }



        //ServiceActBasicActiveNotificationEntity
        public DataSet GetCustomerServiceActActiveNotificationsByDiets(int careCenterID, int[] locationIDs, int routineTypeID,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (routineTypeID <= 0 || careCenterID <= 0) return null;
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerServiceActActiveNotificationsRoutinesByLocationIDAndDateCommand, Environment.NewLine,
                    "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))");

                sqlRoutineQuery += string.Concat(" AND (CP.CareCenterID=@CareCenterID) ");
                sqlRoutineQuery += string.Concat(" AND (RT.ID=@RoutineTypeID) ");

                if (fromDate != null)
                    sqlRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime))");

                if (toDate != null)
                    sqlRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime)");

                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlRoutineQuery, Assistance.Entities.TableNames.ServiceActBasicActiveNotificationTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("RoutineTypeID", DbType.Int32, routineTypeID),
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //ScheduleServiceBasicEntity
        public DataSet GetCustomerScheduleServices(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate,
            int selectedStatus)
        {
            try
            {
                string sqlCustomerRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByLocationIDAndDateCommand, Environment.NewLine,
                    "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlCustomerRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                string sqlCustomerProcedureQuery = string.Concat(
                    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresByLocationIDAndDateCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                    "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlCustomerProcedureQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                string sqlCustomerRoutineAtRoutineActQuery = string.Concat(SQLProvider.SelectDistinctCustomerScheduleServicesRoutinesAtRoutineActsCommand, Environment.NewLine,
                    "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerRoutineAtRoutineActQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerRoutineAtRoutineActQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlCustomerRoutineAtRoutineActQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                string sqlCustomerProcedureAtProcedureActQuery = string.Concat(
                    string.Format(SQLProvider.SelectDistinctCustomerScheduleServicesProceduresAtProcedureActsCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                    "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerProcedureAtProcedureActQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerProcedureAtProcedureActQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlCustomerProcedureAtProcedureActQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                //string sqlRoutineWhitoutCEQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithoutEpisodeByLocationIDAndDateCommand, Environment.NewLine);
                //if (fromDate != null)
                //    sqlRoutineWhitoutCEQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                //if (toDate != null)
                //    sqlRoutineWhitoutCEQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                //if ((locationIDs != null) && (locationIDs.Length > 0))
                //    sqlRoutineWhitoutCEQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                //string sqlProcedureWhitoutCEQuery = string.Concat(
                //    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresWithoutEpisodeByLocationIDAndDateCommand,
                //    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                //        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                //            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                //        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine);
                //if (fromDate != null)
                //    sqlProcedureWhitoutCEQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                //if (toDate != null)
                //    sqlProcedureWhitoutCEQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                //if ((locationIDs != null) && (locationIDs.Length > 0))
                //    sqlProcedureWhitoutCEQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                string sqlQuery = string.Concat(sqlCustomerRoutineQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerRoutineAtRoutineActQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureAtProcedureActQuery
                    /*Environment.NewLine, "UNION", Environment.NewLine, sqlRoutineWhitoutCEQuery, 
                    Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureWhitoutCEQuery*/);

                sqlQuery = string.Concat(sqlQuery, " order by CE.CustomerID");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("PendingStatus", DbType.Int32, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("InitiatedStatus", DbType.Int32, (int)ActionStatusEnum.Initiated),
                    new StoredProcInParam("ScheduledStatus", DbType.Int32, (int)ActionStatusEnum.Scheduled),
                    new StoredProcInParam("CancelledStatus", DbType.Int32, (int)ActionStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerScheduleServices(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate,
            int selectedStatus, StationModeEnum stationMode)
        {
            try
            {
                string sqlCustomerRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByLocationIDAndDateCommand,
                    Environment.NewLine, "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                {
                    switch (stationMode)
                    {
                        case StationModeEnum.NurseWorkStation:
                            sqlCustomerRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            break;
                        case StationModeEnum.DiagnosticWorkStation:
                        case StationModeEnum.SurgeryWorkStation:
                            sqlCustomerRoutineQuery += string.Concat("AND CR.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                            break;
                    }
                }

                string sqlCustomerProcedureQuery = string.Concat(
                    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresByLocationIDAndDateCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                    "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                {
                    switch (stationMode)
                    {
                        case StationModeEnum.NurseWorkStation:
                            sqlCustomerProcedureQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            break;
                        case StationModeEnum.DiagnosticWorkStation:
                        case StationModeEnum.SurgeryWorkStation:
                            sqlCustomerProcedureQuery += string.Concat("AND CP.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                            break;
                    }
                }

                string sqlCustomerRoutineAtRoutineActQuery = string.Concat(SQLProvider.SelectDistinctCustomerScheduleServicesRoutinesAtRoutineActsCommand, Environment.NewLine,
                    "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerRoutineAtRoutineActQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerRoutineAtRoutineActQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                {
                    switch (stationMode)
                    {
                        case StationModeEnum.NurseWorkStation:
                            sqlCustomerRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            break;
                        case StationModeEnum.DiagnosticWorkStation:
                        case StationModeEnum.SurgeryWorkStation:
                            sqlCustomerRoutineQuery += string.Concat("AND RA.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                            break;
                    }
                }

                string sqlCustomerProcedureAtProcedureActQuery = string.Concat(
                    string.Format(SQLProvider.SelectDistinctCustomerScheduleServicesProceduresAtProcedureActsCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                    "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerProcedureAtProcedureActQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerProcedureAtProcedureActQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                {
                    switch (stationMode)
                    {
                        case StationModeEnum.NurseWorkStation:
                            sqlCustomerProcedureQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                            break;
                        case StationModeEnum.DiagnosticWorkStation:
                        case StationModeEnum.SurgeryWorkStation:
                            sqlCustomerProcedureQuery += string.Concat("AND PA.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                            break;
                    }
                }

                //string sqlRoutineWhitoutCEQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithoutEpisodeByLocationIDAndDateCommand, Environment.NewLine);
                //if (fromDate != null)
                //    sqlRoutineWhitoutCEQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                //if (toDate != null)
                //    sqlRoutineWhitoutCEQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                //if ((locationIDs != null) && (locationIDs.Length > 0))
                //{
                //    switch (stationMode)
                //    {
                //        case StationModeEnum.NurseWorkStation:
                //            sqlRoutineWhitoutCEQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                //            break;
                //        case StationModeEnum.DiagnosticWorkStation:
                //        case StationModeEnum.SurgeryWorkStation:
                //            sqlRoutineWhitoutCEQuery += string.Concat("AND CR.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                //            break;
                //    }
                //}

                //string sqlProcedureWhitoutCEQuery = string.Concat(
                //    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresWithoutEpisodeByLocationIDAndDateCommand,
                //    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                //        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                //            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                //        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine);
                //if (fromDate != null)
                //    sqlProcedureWhitoutCEQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                //if (toDate != null)
                //    sqlProcedureWhitoutCEQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                //if ((locationIDs != null) && (locationIDs.Length > 0))
                //{
                //    switch (stationMode)
                //    {
                //        case StationModeEnum.NurseWorkStation:
                //            sqlProcedureWhitoutCEQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                //            break;
                //        case StationModeEnum.DiagnosticWorkStation:
                //        case StationModeEnum.SurgeryWorkStation:
                //            sqlProcedureWhitoutCEQuery += string.Concat("AND CP.LocationID IN (", StringUtils.BuildIDString(locationIDs), ")");
                //            break;
                //    }
                //}

                string sqlQuery = string.Concat(sqlCustomerRoutineQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerRoutineAtRoutineActQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureAtProcedureActQuery
                    /*Environment.NewLine, "UNION", Environment.NewLine, sqlRoutineWhitoutCEQuery, 
                    Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureWhitoutCEQuery*/);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("PendingStatus", DbType.Int32, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("InitiatedStatus", DbType.Int32, (int)ActionStatusEnum.Initiated),
                    new StoredProcInParam("ScheduledStatus", DbType.Int32, (int)ActionStatusEnum.Scheduled),
                    new StoredProcInParam("CancelledStatus", DbType.Int32, (int)ActionStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerScheduleServicesByCustomerEpisodeID(int customerEpisodeID, int[] procPrescriptionIDs, int selectedStatus)
        {
            try
            {
                string sqlCustomerRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByCustomerEpisodeIDCommand, Environment.NewLine,
                    "WHERE (CE.ID=@CustomerEpisodeID) AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                string sqlCustomerProcedureQuery = string.Concat(
                    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresByCustomerEpisodeIDCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                    "WHERE (CE.ID=@CustomerEpisodeID) AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                string sqlCustomerRoutineAtRoutineActQuery = string.Concat(SQLProvider.SelectDistinctCustomerScheduleServicesRoutinesAtRoutineActsCommand, Environment.NewLine,
                    "WHERE (CE.ID=@CustomerEpisodeID) AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) ");

                string sqlCustomerProcedureAtProcedureActQuery = string.Concat(
                    string.Format(SQLProvider.SelectDistinctCustomerScheduleServicesProceduresAtProcedureActsCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                    "WHERE (CE.ID=@CustomerEpisodeID) AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                //string sqlRoutineWhitoutCEQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithoutEpisodeCommand, Environment.NewLine);
                //sqlRoutineWhitoutCEQuery += " AND CE.[ID] = @CustomerEpisodeID ";

                //string sqlProcedureWhitoutCEQuery = string.Concat(
                //    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresWithoutEpisodeCommand,
                //    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                //        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                //            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                //        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine);
                //sqlProcedureWhitoutCEQuery += " AND CE.[ID] = @CustomerEpisodeID ";

                string sqlQuery = string.Concat(sqlCustomerRoutineQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerRoutineAtRoutineActQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureAtProcedureActQuery
                    /*, Environment.NewLine, "UNION", Environment.NewLine, sqlRoutineWhitoutCEQuery, 
                      Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureWhitoutCEQuery*/);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerScheduleServicesByCustomerID(int customerID, int[] procPrescriptionIDs, int selectedStatus)
        {
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByCustomerEpisodeIDCommand, Environment.NewLine,
                    "WHERE (CE.CustomerID=@CustomerID)", " AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                string sqlProcedureQuery = string.Concat(
                    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresByCustomerEpisodeIDCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                    "WHERE (CE.CustomerID=@CustomerID)", "AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                string sqlRoutineWhitoutCEQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithoutEpisodeCommand,
                    Environment.NewLine, "WHERE (CR.EpisodeID <= 0) ", Environment.NewLine);
                sqlRoutineWhitoutCEQuery += " AND CE.CustomerID = @CustomerID ";

                string sqlProcedureWhitoutCEQuery = string.Concat(
                    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresWithoutEpisodeCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                        Environment.NewLine, "WHERE (CP.EpisodeID <= 0) ", Environment.NewLine);

                sqlRoutineWhitoutCEQuery += " AND CE.CustomerID = @CustomerID ";

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlRoutineWhitoutCEQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureWhitoutCEQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerScheduleServicesByCustomerEpisodeIDs(int[] customerEpisodeIDs, int[] procPrescriptionIDs, int selectedStatus)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();


                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByCustomerEpisodeIDCommand,
                    Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ", Environment.NewLine,
                    //"WHERE (CE.CustomerID=@CustomerID)", " AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                    "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                string sqlProcedureQuery = string.Concat(
                    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresByCustomerEpisodeIDCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                    Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ", Environment.NewLine,
                    //"WHERE (CE.CustomerID=@CustomerID)", "AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                    "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                string sqlRoutineWhitoutCEQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithoutEpisodeCommand,
                   Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ",
                   Environment.NewLine, "WHERE (CR.EpisodeID <= 0) ", Environment.NewLine);
                //sqlRoutineWhitoutCEQuery += " AND CE.CustomerID = @CustomerID ";

                string sqlProcedureWhitoutCEQuery = string.Concat(
                    string.Format(SQLProvider.GetCustomerScheduleServicesProceduresWithoutEpisodeCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                        Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ",
                        Environment.NewLine, "WHERE (CP.EpisodeID <= 0) ", Environment.NewLine);

                //sqlRoutineWhitoutCEQuery += " AND CE.CustomerID = @CustomerID ";

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlRoutineWhitoutCEQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureWhitoutCEQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }



        public DataSet GetCustomerScheduleServicesByDiets(int careCenterID, int[] locationIDs, int routineTypeID,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            try
            {
                string sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByLocationIDAndDateCommand,
                    Environment.NewLine,
                    "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);

                sqlRoutineQuery += string.Concat(" AND (CP.CareCenterID=@CareCenterID) ");
                sqlRoutineQuery += string.Concat(" AND (R.RoutineTypeID=@RoutineTypeID) ");

                if (fromDate != null)
                    sqlRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlRoutineQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                string sqlRoutineWhitoutCEQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithoutEpisodeByLocationIDAndDateCommand, Environment.NewLine);

                sqlRoutineWhitoutCEQuery += string.Concat(" AND (CP.CareCenterID=@CareCenterID) ");
                sqlRoutineWhitoutCEQuery += string.Concat(" AND (R.RoutineTypeID=@RoutineTypeID) ");

                if (fromDate != null)
                    sqlRoutineWhitoutCEQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlRoutineWhitoutCEQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");
                if ((locationIDs != null) && (locationIDs.Length > 0))
                    sqlRoutineWhitoutCEQuery += string.Concat(" AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                string sqlQuery = string.Concat(sqlRoutineQuery, Environment.NewLine, "UNION", Environment.NewLine, sqlRoutineWhitoutCEQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("RoutineTypeID", DbType.Int32, routineTypeID),

                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("PendingStatus", DbType.Int32, (int)ActionStatusEnum.Pending),
                    new StoredProcInParam("InitiatedStatus", DbType.Int32, (int)ActionStatusEnum.Initiated),
                    new StoredProcInParam("ScheduledStatus", DbType.Int32, (int)ActionStatusEnum.Scheduled),
                    new StoredProcInParam("CancelledStatus", DbType.Int32, (int)ActionStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerOrderRequestBasicEntity
        public DataSet GetNotParentCustomerOrderRequests(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                //string sqlWhere = string.Concat("WHERE NOT(COR.OrderControlCode=@OrderControlCode) AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);
                //if (fromDate != null)
                //    sqlWhere += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime))");
                //if (toDate != null)
                //    sqlWhere += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                //string sqlQuery = string.Concat(
                //    SQLProvider.SelectDistinctCustomerOrderRequestBasicJoinCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                //    Environment.NewLine, "UNION", Environment.NewLine,
                //    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINCustomerRoutineCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                //    Environment.NewLine, "UNION", Environment.NewLine,
                //    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINCustomerProcedureCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                //    Environment.NewLine, "UNION", Environment.NewLine,
                //    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINRoutineActCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                //    Environment.NewLine, "UNION", Environment.NewLine,
                //    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINProcedureActCustomerEpisodeCommand, Environment.NewLine, sqlWhere);


                return this.Gateway.ExecuteStoredProcedureDataSet("GetNotParentCustomerOrderRequestsNurseStation", Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("OrderControlCode", DbType.Int32, (int)OrderControlCodeEnum.ParentOrder),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded),
                    new StoredProcInTVPIntegerParam("TVPTableLOC", locationIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetNotParentCustomerOrderRequestsByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                string sqlWhere = string.Concat("WHERE (CE.[ID]=@CustomerEpisodeID) AND NOT(COR.OrderControlCode=@OrderControlCode) ");

                string sqlQuery = string.Concat(
                    SQLProvider.SelectDistinctCustomerOrderRequestBasicJoinCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINCustomerRoutineCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINCustomerProcedureCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINRoutineActCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestBasicJOINProcedureActCustomerEpisodeCommand, Environment.NewLine, sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderControlCode", DbType.Int32, (int)OrderControlCodeEnum.ParentOrder),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestsByCustomerID(int customerID)
        {
            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctCustomerOrderRequestBasicJoinStandardCommand, string.Empty), Environment.NewLine);

                sqlQuery += "WHERE (COR.CustomerID = @CustomerID)";
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestsByCustomerIDs(int[] customerIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) return null;
                customerIDs = customerIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctCustomerOrderRequestBasicJoinStandardCommand, string.Empty), Environment.NewLine);
                sqlQuery += "JOIN @TVPTable TVP ON COR.CustomerID=TVP.[ID] ";

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }




        public DataSet GetCustomerOrderRequestsByIDs(int[] customerOrderRequestIDs)
        {
            if ((customerOrderRequestIDs == null) || (customerOrderRequestIDs.Length <= 0))
                return null;

            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctCustomerOrderRequestBasicJoinStandardCommand, string.Empty), Environment.NewLine);
                sqlQuery += string.Concat("JOIN @TVPTable TVP ON COR.ID=TVP.[ID]");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerOrderRequestIDs),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetNotParentCustomerOrderRequestsByIDs(int[] customerOrderRequestIDs)
        {
            if ((customerOrderRequestIDs == null) || (customerOrderRequestIDs.Length <= 0))
                return null;

            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctCustomerOrderRequestBasicJoinStandardCommand, string.Empty), Environment.NewLine);

                sqlQuery += string.Concat("WHERE (COR.ID IN (", StringUtils.BuildIDString(customerOrderRequestIDs), ")) AND NOT(COR.OrderControlCode=@OrderControlCode) ");  //que NO sean PADRES

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInParam("OrderControlCode", DbType.Int32, (int)OrderControlCodeEnum.ParentOrder),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //UserTimeEntity-->ScheduleServiceBasicEntity
        public DataSet GetScheduleServicesTimesByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlCustomerRoutineQuery = string.Concat(SQLProvider.SelectDistinctCustomerRoutineTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON CR.EpisodeID=CE.[ID] ", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE CE.[ID]=@CustomerEpisodeID");

                string sqlCustomerProcedureQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID] ", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE CE.[ID]=@CustomerEpisodeID");

                string sqlCustomerRoutineAtRoutineActQuery = string.Concat(SQLProvider.SelectDistinctCustomerRoutineTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN RoutineAct RA WITH(NOLOCK) ON CR.[ID]=RA.CustomerRoutineID AND RA.EpisodeID<>CR.EpisodeID", Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON RA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE CE.[ID]=@CustomerEpisodeID ");

                string sqlCustomerProcedureAtProcedureActQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN ProcedureAct PA WITH(NOLOCK) ON CP.[ID]=PA.CustomerProcedureID AND PA.EpisodeID<>CP.EpisodeID", Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON PA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE CE.[ID] = @CustomerEpisodeID");

                string sqlQuery = string.Concat(sqlCustomerRoutineQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerRoutineAtRoutineActQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureAtProcedureActQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesTimesByCustomerID(int customerID)
        {
            try
            {
                if (customerID <= 0) return null;
                string sqlQuery = string.Concat(SQLProvider.GetCustomerProcedureTimesByCustomerIDCommand,
                    Environment.NewLine, "WHERE CE.CustomerID = @CustomerID",
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.GetCustomerRoutineTimesByCustomerIDCommand,
                    Environment.NewLine, "WHERE CE.CustomerID = @CustomerID");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesTimesByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                string sqlQuery = string.Concat(SQLProvider.GetCustomerProcedureTimesByCustomerIDCommand,
                    Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] ",
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.GetCustomerRoutineTimesByCustomerIDCommand,
                    Environment.NewLine, "JOIN @TVPTable TVP ON CE.[ID]=TVP.[ID] "
                    );

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.BackOffice.Entities.TableNames.UserTimeTable,
                   new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }



        public DataSet GetScheduleServicesTimes(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlCustomerRoutineQuery = string.Concat(SQLProvider.SelectDistinctCustomerRoutineTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON CR.EpisodeID=CE.[ID] ", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine,
                    " AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlCustomerProcedureQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID] ", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine,
                    " AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlCustomerRoutineAtRoutineActQuery = string.Concat(SQLProvider.SelectDistinctCustomerRoutineTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN RoutineAct RA WITH(NOLOCK) ON CR.[ID]=RA.CustomerRoutineID AND RA.EpisodeID<>CR.EpisodeID", Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON RA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine,
                    " AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerRoutineQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlCustomerProcedureAtProcedureActQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN ProcedureAct PA WITH(NOLOCK) ON CP.[ID]=PA.CustomerProcedureID AND PA.EpisodeID<>CP.EpisodeID", Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON PA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine,
                    " AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlCustomerProcedureQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlQuery = string.Concat(sqlCustomerRoutineQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerRoutineAtRoutineActQuery,
                    Environment.NewLine, "UNION", Environment.NewLine, sqlCustomerProcedureAtProcedureActQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //UserTimeEntity-->CustomerOrderRequestBasicEntity
        public DataSet GetCustomerOrderRequestsTimesAtNotParentCustomerOrderReqestByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (COR.CustomerEpisodeID=@CustomerEpisodeID) AND NOT(COR.OrderControlCode=@OrderControlCode)");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN CustomerRoutine CR WITH(NOLOCK) ON CR.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CR.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN RoutineAct RA WITH(NOLOCK) ON RA.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON RA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN ProcedureAct PA WITH(NOLOCK) ON PA.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON PA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere
                    );

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.OrderRequestTimeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderControlCode", DbType.Int32, (int)OrderControlCodeEnum.ParentOrder));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestsTimesByCustomerID(int customerID)
        {
            try
            {
                if (customerID <= 0) return null;
                string sqlQuery = string.Format(SQLProvider.GetCustomerOrderRequestsTimesCommand,
                    "WHERE CE.CustomerID = @CustomerID");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.OrderRequestTimeTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestsTimesAtNotParentCustomerOrderReqests(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE NOT(COR.OrderControlCode=@OrderControlCode) AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);
                if (fromDate != null)
                    sqlWhere += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime))");
                if (toDate != null)
                    sqlWhere += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN CustomerRoutine CR WITH(NOLOCK) ON CR.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CR.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN RoutineAct RA WITH(NOLOCK) ON RA.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON RA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN ProcedureAct PA WITH(NOLOCK) ON PA.[ID]=COR.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON PA.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere
                    );

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.OrderRequestTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("OrderControlCode", DbType.Int32, (int)OrderControlCodeEnum.ParentOrder));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestsTimesByCustomerOrderRequestIDs(int[] customerOrderRequestIDs)
        {
            if ((customerOrderRequestIDs == null) || (customerOrderRequestIDs.Length <= 0))
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand,
                    Environment.NewLine,
                    "JOIN @TVPTable TVP ON COR.[ID]=TVP.[ID]");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                                Administrative.Entities.TableNames.OrderRequestTimeTable,
                                new StoredProcInTVPIntegerParam("TVPTable", customerOrderRequestIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetNotParentCustomerOrderRequestsTimesByCustomerOrderRequestIDs(int[] customerOrderRequestIDs)
        {
            if ((customerOrderRequestIDs == null) || (customerOrderRequestIDs.Length <= 0))
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand,
                    "WHERE (COR.[ID] IN (", StringUtils.BuildIDString(customerOrderRequestIDs), ")) AND NOT(COR.OrderControlCode = 2) ");  //que no sea padre

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.OrderRequestTimeTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerOrderRequestAttendingPhysicians
        public DataSet GetCustomerOrderRequestAttendingPhysicians(int[] customerOrderRequestIDs)
        {
            if ((customerOrderRequestIDs == null) || (customerOrderRequestIDs.Length <= 0))
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsAttendingPhysiciansCommand,
                    Environment.NewLine,
                    "JOIN @TVPTable TVP ON COR.[ID]=TVP.[ID]");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                                SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                                new StoredProcInTVPIntegerParam("TVPTable", customerOrderRequestIDs)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetNotParentCustomerOrderRequestAttendingPhysicians(int[] customerOrderRequestIDs)
        {
            if ((customerOrderRequestIDs == null) || (customerOrderRequestIDs.Length <= 0))
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsAttendingPhysiciansCommand,
                    "WHERE (COR.[ID] IN (", StringUtils.BuildIDString(customerOrderRequestIDs), ")) AND NOT(COR.OrderControlCode=@OrderControlCode) ");  //que NO sean PADRES

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Common.Entities.TableNames.IDDescriptionTable,
                    new StoredProcInParam("OrderControlCode", DbType.Int32, (int)OrderControlCodeEnum.ParentOrder));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerOrderRequestIDHasAppointment
        public DataSet GetCustomerOrderRequestIDHasAppointmentByCustomerEpisodeID(int customerEpisodeID, int selectedStatus)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string whereCitation = string.Concat("WHERE (CE.ID=@CustomerEpisodeID)", Environment.NewLine,
                    " AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed))", Environment.NewLine);

                string whereWaitingList = string.Concat("WHERE (CE.ID=@CustomerEpisodeID)", Environment.NewLine,
                    " AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

                string sqlQuery = string.Concat(
                    //Routine
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfRoutineCitationCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON RA.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereCitation, " AND (PARA.[ID] IS NULL)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfRoutineWaitingListCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON RA.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereWaitingList, " AND (PARA.[ID] IS NULL)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //Procedure
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfProcedureCitationCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON PA.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereCitation, Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfProcedureWaitingListCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON PA.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereWaitingList, Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //CustomerRoutine
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerRoutineCitationCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON CR.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereCitation, Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerRoutineWaitingListCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON CR.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereWaitingList, Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //CustomerProcedure
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerProcedureCitationCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereCitation, Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerProcedureWaitingListCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    whereWaitingList);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestHasAppointmentTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //ActPlanification
        public DataSet GetActPlanificationByCustomerEpisodeID(int customerEpisodeID, int selectedStatus)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(
                    //Routine
                    SQLProvider.SelectDictinctActPlanificationRoutineActJoinStandardCommand, Environment.NewLine,
                    "WHERE (RA.EpisodeID=@CustomerEpisodeID) AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //Procedure
                    SQLProvider.SelectDictinctActPlanificationProcedureActJoinStandardCommand, Environment.NewLine,
                    "WHERE (PA.EpisodeID=@CustomerEpisodeID) AND (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine
                    );

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ActPlanificationTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
                    new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //Recursive Locations
        public DataSet GetRecursiveLocations(int locationID, string username)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("GetListRecursiveLocations",
                        new StoredProcInParam("LocationID", DbType.Int32, locationID),
                        new StoredProcInParam("Username", DbType.String, username));

                if (ds.Tables.Count > 0 && ds.Tables[0] != null)
                    ds.Tables[0].TableName = SII.HCD.Common.Entities.TableNames.IDDescriptionTable;

                return ds;
               
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRecursiveLocationsCommand,
                //    SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                //    new StoredProcInParam("LocationID", DbType.Int32, locationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region ActivityInLocations
        //ServiceActBasicEntity
        public DataSet GetServiceActsRoutinesWithActivityInLocations(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(QueryServiceActsRoutineOfCitation(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsRoutineOfWaitingList(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsRoutineOfScheludedInRealization(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsRoutineOfReservationScheludedInRealization(locationIDs, fromDate, toDate, selectedStatus));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
                    new StoredProcInParam("StepReception", DbType.Int32, (int)BasicProcessStepsEnum.Reception),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetServiceActsProceduresWithActivityInLocations(int[] locationIDs, int[] procPrescriptionIDs,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(QueryServiceActsProcedureOfCitation(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsProcedureOfWaitingList(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsProcedureOfScheludedInRealization(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsProcedureOfReservationScheludedInRealization(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
                    new StoredProcInParam("StepReception", DbType.Int32, (int)BasicProcessStepsEnum.Reception),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetServiceActsProcedureRoutinesWithActivityInLocations(int[] locationIDs, int[] procPrescriptionIDs,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(QueryServiceActsProcedureRoutinesOfCitation(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsProcedureRoutinesOfWaitingList(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsProcedureRoutinesOfScheludedInRealization(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryServiceActsProcedureRoutinesOfReservationScheludedInRealization(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
                    new StoredProcInParam("StepReception", DbType.Int32, (int)BasicProcessStepsEnum.Reception),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        private string QueryServiceActsRoutineOfCitation(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfCitationCommand, Environment.NewLine,
                "WHERE (PARA.[ID] IS NULL) ",/*AND (RAS.[ID] IS NULL) */ "AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (RA.EndDateTime>=@StartDateTime) ");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (RA.StartDateTime<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryServiceActsRoutineOfWaitingList(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfWaitingListCommand, Environment.NewLine,
                "WHERE (PARA.[ID] IS NULL) ", /*AND (RAS.[ID] IS NULL) */ "AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (RA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (RA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsRoutineOfScheludedInRealization(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfScheludedInRealizationCommand, Environment.NewLine,
                "WHERE (PARA.[ID] IS NULL) AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), ")) /*AND (AppS.[ID] is NUll)*/", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (RA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (RA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsRoutineOfReservationScheludedInRealization(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfReservationScheludedInRealizationCommand, Environment.NewLine,
                "WHERE (PARA.[ID] IS NULL) AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (RA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (RA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfCitation(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfCitationCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : string.Format("CAST({0} AS bigint)", ((int)AdditionalInfoTypeEnum.WithInformation).ToString())), Environment.NewLine,
                "WHERE  (RAS.[ID] IS NULL) AND (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureRoutinesOfCitation(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProcedureRoutinesOfCitationCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "WHERE ", /*(RAS.[ID] IS NULL) AND*/ "(PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfWaitingList(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfWaitingListCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : string.Format("CAST({0} AS bigint)", ((int)AdditionalInfoTypeEnum.WithInformation).ToString())), Environment.NewLine,
                "WHERE (RAS.[ID] IS NULL) AND (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureRoutinesOfWaitingList(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProcedureRoutinesOfWaitingListCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "WHERE ", /*(RAS.[ID] IS NULL) AND*/ "(PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfScheludedInRealization(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfScheludedInRealizationCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : string.Format("CAST({0} AS bigint)", ((int)AdditionalInfoTypeEnum.WithInformation).ToString())), Environment.NewLine,
                "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) /*AND (AppS.[ID] is NUll)*/ AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureRoutinesOfScheludedInRealization(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProcedureRoutinesOfScheludedInRealizationCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) /*AND (AppS.[ID] is NUll)*/ AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfReservationScheludedInRealization(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfReservationScheludedInRealizationCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : string.Format("CAST({0} AS bigint)", ((int)AdditionalInfoTypeEnum.WithInformation).ToString())), Environment.NewLine,
                "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureRoutinesOfReservationScheludedInRealization(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProcedureRoutinesOfReservationScheludedInRealizationCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND (PA.EndDateTime>=@StartDateTime)");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (PA.StartDateTime<@EndDateTime)");

            return sqlQuery;
        }

        public DataSet GetServiceActsWithHospitalizationRequestCustomerEpisodeActive(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((procedureIDs == null || procedureIDs.Length <= 0)
                && (routineIDs == null || routineIDs.Length <= 0))
                return null;

            try
            {
                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                    sqlRoutineQuery = QueryServiceActsRoutinesOfNotScheluded(fromDate, toDate);

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                    sqlProcedureQuery = QueryServiceActsProcedureOfNotScheluded(procPrescriptionIDs, fromDate, toDate);

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                int[] statusList = this.BuildActionStatusArray(selectedStatus);
                int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient, (int)EpisodeCaseEnum.RoutineOutPatient };
                episodeCases = episodeCases
                                    .OrderBy(ec => ec)
                                    .ToArray();

                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine));
                parameters.Add(new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure));
                parameters.Add(new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1a", routineIDs));
                }
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1b", procedureIDs));
                }
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable2", statusList));
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable3", episodeCases));
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable, parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetServiceActsWithReservationRequest(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((procedureIDs == null || procedureIDs.Length <= 0)
                && (routineIDs == null || routineIDs.Length <= 0))
                return null;

            try
            {
                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                    sqlRoutineQuery = QueryServiceActsRoutinesOfReservationNotScheluded(fromDate, toDate);

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                    sqlProcedureQuery = QueryServiceActsProcedureOfReservationNotScheluded(procPrescriptionIDs, fromDate, toDate);

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                int[] statusList = this.BuildActionStatusArray(selectedStatus);
                int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient, (int)EpisodeCaseEnum.RoutineOutPatient };
                episodeCases = episodeCases
                                    .OrderBy(ec => ec)
                                    .ToArray();

                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine));
                parameters.Add(new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure));
                parameters.Add(new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
                parameters.Add(new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation));
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1a", routineIDs));
                }
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1b", procedureIDs));
                }
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable2", statusList));
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable3", episodeCases));
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable, parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //public DataSet GetServiceActsWithOutPatientRequestOfReceptionDirect(int[] routineIDs, int[] procedureIDs,
        //            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        //{
        //    if ((procedureIDs == null || procedureIDs.Length <= 0)
        //        && (routineIDs == null || routineIDs.Length <= 0))
        //        return null;

        //    try
        //    {
        //        int[] episodeCases = new int[] { /*(int)EpisodeCaseEnum.DayTreatment,*/ (int)EpisodeCaseEnum.RoutineOutPatient };

        //        string sqlRoutineQuery = string.Empty;
        //        if ((routineIDs != null) && (routineIDs.Length > 0))
        //            sqlRoutineQuery = QueryServiceActsRoutinesOfNotScheludedReceptionDirect(routineIDs, episodeCases, fromDate, toDate, selectedStatus);

        //        string sqlProcedureQuery = string.Empty;
        //        if ((procedureIDs != null) && (procedureIDs.Length > 0))
        //            sqlProcedureQuery = QueryServiceActsProcedureOfNotScheludedReceptionDirect(procedureIDs, procPrescriptionIDs, episodeCases, fromDate, toDate, selectedStatus);

        //        if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
        //            return null;

        //        string sqlQuery = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
        //        {
        //            sqlQuery = sqlRoutineQuery;
        //            if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
        //                sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
        //        }
        //        else
        //            sqlQuery = sqlProcedureQuery;

        //        if (string.IsNullOrWhiteSpace(sqlQuery))
        //            return null;

        //        return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
        //            new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
        //            new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
        //            new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
        //            new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure),
        //            new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
        //            new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
        //            new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
        //            new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public DataSet GetServiceActsWithOutPatientRequestOfReceptionDirect(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((procedureIDs == null || procedureIDs.Length <= 0)
                && (routineIDs == null || routineIDs.Length <= 0))
                return null;

            try
            {
                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                    sqlRoutineQuery = QueryServiceActsRoutinesOfNotScheludedReceptionDirect(fromDate, toDate);

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                    sqlProcedureQuery = QueryServiceActsProcedureOfNotScheludedReceptionDirect(procPrescriptionIDs, fromDate, toDate);

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                int[] statusList = this.BuildActionStatusArray(selectedStatus);
                int[] episodeCases = new int[] { (int)EpisodeCaseEnum.RoutineOutPatient };
                episodeCases = episodeCases
                                    .OrderBy(ec => ec)
                                    .ToArray();

                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine));
                parameters.Add(new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure));
                parameters.Add(new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
                parameters.Add(new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder));
                parameters.Add(new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled));
                parameters.Add(new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed));
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1a", routineIDs));
                }
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1b", procedureIDs));
                }
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable2", statusList));
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable3", episodeCases));
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable, parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetServiceActsWithOutPatientRequestOfWithoutReceptionDirect(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((procedureIDs == null || procedureIDs.Length <= 0)
                && (routineIDs == null || routineIDs.Length <= 0))
                return null;

            try
            {
                int[] episodeCases = new int[] { /*(int)EpisodeCaseEnum.DayTreatment,*/ (int)EpisodeCaseEnum.RoutineOutPatient };

                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                    sqlRoutineQuery = QueryServiceActsRoutinesOfNotScheludedWithoutReceptionDirect(routineIDs, episodeCases, fromDate, toDate, selectedStatus);

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                    sqlProcedureQuery = QueryServiceActsProcedureOfNotScheludedWithoutReceptionDirect(procedureIDs, procPrescriptionIDs, episodeCases, fromDate, toDate, selectedStatus);

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ServiceActBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
                    new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        private string QueryServiceActsRoutinesOfNotScheluded(DateTime? fromDate, DateTime? toDate)
        {
            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfNotScheludedCommand, Environment.NewLine,
                "AND (CE.[Status]=@StatusActive)", Environment.NewLine,
                "AND (COR.RequestDateTime<=@EndDateTime)"
                );

            //if (toDate != null)
            //    sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
            //                "       OR ((COR.[ID] IS NULL) AND (RA.StartDateTime<@EndDateTime))) ");

            return sqlQuery;
        }

        private string QueryServiceActsRoutinesOfReservationNotScheluded(DateTime? fromDate, DateTime? toDate)
        {
            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfReservationNotScheludedCommand, Environment.NewLine,
                "AND ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive)) ", Environment.NewLine,
                "     OR ((CE.[ID] is null) AND (CP.[Status]=@StatusActive))) ", Environment.NewLine,
                "AND (COR.RequestDateTime<=@EndDateTime)"
                );

            //if (toDate != null)
            //    sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
            //                "       OR ((COR.[ID] IS NULL) AND (RA.StartDateTime<@EndDateTime))) ");

            return sqlQuery;
        }

        private string QueryServiceActsRoutinesOfNotScheludedReceptionDirect(DateTime? fromDate, DateTime? toDate)
        {
            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfNotScheludedReceptionDirectCommand, Environment.NewLine,
                "   AND (CE.Status=@StatusActive)", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (toDate != null)
                sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (RA.StartDateTime<@EndDateTime))) ");

            return sqlQuery;
        }

        private string QueryServiceActsRoutinesOfNotScheludedWithoutReceptionDirect(int[] routineIDs, int[] episodeCases, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (routineIDs == null || routineIDs.Length <= 0 || episodeCases == null || episodeCases.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctServiceActsRoutinesOfNotScheludedWithoutReceptionDirectCommand, Environment.NewLine,
                "   AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (CE.Status=@StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                "   AND (RA.RoutineID IN (", StringUtils.BuildIDString(routineIDs), "))", Environment.NewLine);

            if (toDate != null)
                sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (RA.StartDateTime<@EndDateTime))) ");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfNotScheluded(int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate)
        {
            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfNotScheludedCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "   AND (CE.[Status]=@StatusActive)", Environment.NewLine,
                "   AND COR.RequestDateTime<=@EndDateTime",
                Environment.NewLine);

            //if (toDate != null)
            //    sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
            //                "       OR ((COR.[ID] IS NULL) AND (PA.StartDateTime<@EndDateTime))) ");


            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfReservationNotScheluded(int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate)
        {
            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfReservationNotScheludedCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                        ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "AND ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive)) ", Environment.NewLine,
                "    OR ((CE.[ID] is null) AND (CP.[Status]=@StatusActive))) ", Environment.NewLine,
                "AND (COR.RequestDateTime<=@EndDateTime)",
                Environment.NewLine);

            //if (toDate != null)
            //    sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
            //                "       OR ((COR.[ID] IS NULL) AND (PA.StartDateTime<@EndDateTime))) ");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfNotScheludedReceptionDirect(int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate)
        {
            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfNotScheludedReceptionDirectCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                    ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "   AND (CE.[Status]=@StatusActive)", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (toDate != null)
                sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (PA.StartDateTime<@EndDateTime))) ");

            return sqlQuery;
        }

        private string QueryServiceActsProcedureOfNotScheludedWithoutReceptionDirect(int[] procedureIDs, int[] procPrescriptionIDs, int[] episodeCases, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (procedureIDs == null || procedureIDs.Length <= 0 || episodeCases == null || episodeCases.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctServiceActsProceduresOfNotScheludedWithoutReceptionDirectCommand,
                    ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                    ? string.Format("CAST((CASE WHEN (PA.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                            StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                        : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                "   AND (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (CE.Status=@StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                "   AND (PA.ProcedureID IN (", StringUtils.BuildIDString(procedureIDs), "))", Environment.NewLine);

            if (toDate != null)
                sqlQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (PA.StartDateTime<@EndDateTime))) ");

            return sqlQuery;
        }

        //ScheduleServiceBasicEntity
        public DataSet GetScheduleServicesRoutinesWithActivityInLocations(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(QueryScheduleServicesRoutineOfCitation(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesRoutineOfWaitingList(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesRoutineOfReservationScheludedInRealization(locationIDs, fromDate, toDate, selectedStatus));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesProceduresWithActivityInLocations(int[] locationIDs, int[] procPrescriptionIDs,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(QueryScheduleServicesProcedureOfCitation(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesProcedureOfWaitingList(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesProcedureOfReservationScheludedInRealization(locationIDs, procPrescriptionIDs, fromDate, toDate, selectedStatus));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        private string QueryScheduleServicesRoutineOfCitation(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctScheduleServicesRoutinesOfCitationCommand, Environment.NewLine,
                "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime))");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesRoutineOfReservationScheludedInRealization(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctScheduleServicesRoutinesOfReservationScheludedInRealizationCommand, Environment.NewLine,
                "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime))");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesRoutineOfWaitingList(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctScheduleServicesRoutinesOfWaitingListCommand, Environment.NewLine,
                "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime))");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CR.StartAt<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryScheduleServicesProcedureOfCitation(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctScheduleServicesProceduresOfCitationCommand,
                ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                    ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                        StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                    : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                Environment.NewLine,
                "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime))");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CP.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesProcedureOfWaitingList(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctScheduleServicesProceduresOfWaitingListCommand,
                ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                    ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                        StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                    : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                Environment.NewLine,
                "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime))");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CP.StartAt<@EndDateTime)");

            return sqlQuery;
        }

        private string QueryScheduleServicesProcedureOfReservationScheludedInRealization(int[] locationIDs, int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctScheduleServicesProceduresOfReservationScheludedInRealizationCommand,
                ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                    ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                        StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                    : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()),
                Environment.NewLine,
                "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime))");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CP.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        //public DataSet GetScheduleServicesWithHospitalizationRequestCustomerEpisodeActive(int[] routineIDs, int[] procedureIDs,
        //    int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        //{
        //    if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
        //        return null;

        //    try
        //    {
        //        int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient };

        //        string sqlRoutineQuery = string.Empty;
        //        if ((routineIDs != null) && (routineIDs.Length > 0))
        //        {
        //            sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByLocationIDAndDateCommand, Environment.NewLine,
        //                "   JOIN EpisodeType ET WITH(NOLOCK) ON CE.EpisodeTypeID=ET.[ID] ", Environment.NewLine,
        //                //"   LEFT JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
        //                "   JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
        //                "WHERE (CE.[ID] > 0) AND (CR.AssistancePlanID > 0) AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
        //                "   AND (CE.Status = @StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
        //                "   AND (CR.RoutineID IN (", StringUtils.BuildIDString(routineIDs), ")) ",
        //                "   AND COR.RequestDateTime<=@EndDateTime"

        //                );

        //            //if (toDate != null)
        //            //    sqlRoutineQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
        //            //        "       OR ((COR.[ID] IS NULL) AND (CR.StartAt<@EndDateTime))) ");
        //        }

        //        string sqlProcedureQuery = string.Empty;
        //        if ((procedureIDs != null) && (procedureIDs.Length > 0))
        //        {
        //            sqlProcedureQuery = string.Concat(
        //                string.Format(SQLProvider.GetCustomerScheduleServicesProceduresByLocationIDAndDateCommand,
        //                ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
        //                    ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
        //                        StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
        //                    : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
        //                "   JOIN EpisodeType ET ON CE.EpisodeTypeID=ET.[ID] ", Environment.NewLine,
        //                //"   LEFT JOIN CustomerOrderRequest COR ON CP.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
        //                "   JOIN CustomerOrderRequest COR ON CP.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
        //                "WHERE (CE.[ID] > 0) AND (CP.AssistancePlanID > 0) AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
        //                "   AND (CE.Status = @StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
        //                "   AND (CP.ProcedureID IN (", StringUtils.BuildIDString(procedureIDs), "))",
        //                "   AND COR.RequestDateTime<=@EndDateTime"

        //                );

        //            //if (toDate != null)
        //            //    sqlProcedureQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
        //            //        "       OR ((COR.[ID] IS NULL) AND (CP.StartAt<@EndDateTime))) ");
        //        }

        //        if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
        //            return null;

        //        string sqlQuery = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
        //        {
        //            sqlQuery = sqlRoutineQuery;
        //            if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
        //                sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
        //        }
        //        else
        //            sqlQuery = sqlProcedureQuery;

        //        if (string.IsNullOrWhiteSpace(sqlQuery))
        //            return null;

        //        return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
        //            new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
        //            new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
        //            new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public DataSet GetScheduleServicesWithHospitalizationRequestCustomerEpisodeActive(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesByLocationIDAndDateCommand, Environment.NewLine,
                        "JOIN EpisodeType ET WITH(NOLOCK) ON CE.EpisodeTypeID=ET.[ID] ", Environment.NewLine,
                        "JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "JOIN @TVPTable1a TVP1a ON CR.RoutineID=TVP1a.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CR.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE (CE.[ID] > 0) AND (CR.AssistancePlanID > 0)", Environment.NewLine,
                        "AND (CE.[Status]=@StatusActive)", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );
                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(
                        string.Format(SQLProvider.GetCustomerScheduleServicesProceduresByLocationIDAndDateCommand,
                        ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                            ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                                StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                            : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                        "JOIN EpisodeType ET ON CE.EpisodeTypeID=ET.[ID] ", Environment.NewLine,
                        "JOIN CustomerOrderRequest COR ON CP.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "JOIN @TVPTable1b TVP1b ON CP.ProcedureID=TVP1b.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CP.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE (CE.[ID] > 0) AND (CP.AssistancePlanID > 0)", Environment.NewLine,
                        "AND (CE.[Status]=@StatusActive)", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;


                int[] statusList = this.BuildActionStatusArray(selectedStatus);
                int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient, (int)EpisodeCaseEnum.RoutineOutPatient };
                episodeCases = episodeCases
                                    .OrderBy(ec => ec)
                                    .ToArray();

                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1a", routineIDs));
                }
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1b", procedureIDs));
                }
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable2", statusList));
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable3", episodeCases));
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable, parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //public DataSet GetScheduleServicesWithReservationRequest(int[] routineIDs, int[] procedureIDs,
        //    int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        //{
        //    if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
        //        return null;

        //    try
        //    {
        //        int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient };

        //        string sqlRoutineQuery = string.Empty;
        //        if ((routineIDs != null) && (routineIDs.Length > 0))
        //        {
        //            sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithReservationCommand, Environment.NewLine,
        //                //"   LEFT JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
        //                "   JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
        //                "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
        //                "   AND ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive)) ", Environment.NewLine,
        //                "       OR ((CE.[ID] is null) AND (CP.[Status]=@StatusActive))) ", Environment.NewLine,
        //                "   AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
        //                "   AND (CR.RoutineID IN (", StringUtils.BuildIDString(routineIDs), ")) ",
        //                "   AND COR.RequestDateTime<=@EndDateTime"
        //                );

        //            //if (toDate != null)
        //            //    sqlRoutineQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
        //            //        "       OR ((COR.[ID] IS NULL) AND (CR.StartAt<@EndDateTime))) ");
        //        }

        //        string sqlProcedureQuery = string.Empty;
        //        if ((procedureIDs != null) && (procedureIDs.Length > 0))
        //        {
        //            sqlProcedureQuery = string.Concat(
        //                string.Format(SQLProvider.GetCustomerScheduleServicesProceduresWithReservationCommand,
        //                ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
        //                    ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
        //                        StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
        //                    : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,

        //               "   JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CP.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,

        //                "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
        //                "   AND ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive)) ", Environment.NewLine,
        //                "       OR ((CE.[ID] is null) AND (CPro.[Status]=@StatusActive))) ", Environment.NewLine,
        //                "   AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
        //                "   AND (CP.ProcedureID IN (", StringUtils.BuildIDString(procedureIDs), "))",
        //                "   AND COR.RequestDateTime<=@EndDateTime"
        //                );

        //            //if (toDate != null)
        //            //    sqlProcedureQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
        //            //        "       OR ((COR.[ID] IS NULL) AND (CP.StartAt<@EndDateTime))) ");
        //        }

        //        if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
        //            return null;

        //        string sqlQuery = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
        //        {
        //            sqlQuery = sqlRoutineQuery;
        //            if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
        //                sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
        //        }
        //        else
        //            sqlQuery = sqlProcedureQuery;

        //        if (string.IsNullOrWhiteSpace(sqlQuery))
        //            return null;

        //        return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
        //            new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
        //            new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
        //            new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
        //            new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
        //            new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
        //            new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public DataSet GetScheduleServicesWithReservationRequest(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.GetCustomerScheduleServicesRoutinesWithReservationCommand, Environment.NewLine,
                        "JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "JOIN @TVPTable1a TVP1a ON CR.RoutineID=TVP1a.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CR.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive)) ", Environment.NewLine,
                        "OR ((CE.[ID] is null) AND (CP.[Status]=@StatusActive))) ", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );
                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(
                        string.Format(SQLProvider.GetCustomerScheduleServicesProceduresWithReservationCommand,
                        ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                            ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                                StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                            : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,

                        "JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CP.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "JOIN @TVPTable1b TVP1b ON CP.ProcedureID=TVP1b.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CP.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive)) ", Environment.NewLine,
                        "OR ((CE.[ID] is null) AND (CPro.[Status]=@StatusActive))) ", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                int[] statusList = this.BuildActionStatusArray(selectedStatus);
                int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient, (int)EpisodeCaseEnum.RoutineOutPatient };
                episodeCases = episodeCases
                                    .OrderBy(ec => ec)
                                    .ToArray();

                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
                parameters.Add(new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation));
                parameters.Add(new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine));
                parameters.Add(new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure));
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1a", routineIDs));
                }
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1b", procedureIDs));
                }
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable2", statusList));
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable3", episodeCases));
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable, parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesWithOutPatientRequestOfReceptionDirect(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                int[] episodeCases = new int[] { /*(int)EpisodeCaseEnum.DayTreatment,*/ (int)EpisodeCaseEnum.RoutineOutPatient };

                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.SelectDistinctScheduleServicesRoutinesOfReceptionDirectCommand, Environment.NewLine,
                        "   LEFT JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status = @StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CR.RoutineID IN (", StringUtils.BuildIDString(routineIDs), ")) ", Environment.NewLine,
                        "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

                    if (toDate != null)
                        sqlRoutineQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (CR.StartAt<@EndDateTime))) ");

                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(
                        string.Format(SQLProvider.SelectDistinctScheduleServicesProceduresOfReceptionDirectCommand,
                        ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                            ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                                StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                            : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                        "   LEFT JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CP.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status = @StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CP.ProcedureID IN (", StringUtils.BuildIDString(procedureIDs), "))", Environment.NewLine,
                        "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

                    if (toDate != null)
                        sqlProcedureQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (CP.StartAt<@EndDateTime))) ");
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        /// <summary>
        /// Busca los ScheduleServices de los episodios de tratamieto y ambulantes que estan activos y no se han recepcionado paro pertenecen a un episodio activo.
        /// </summary>
        /// <param name="routineIDs"></param>
        /// <param name="procedureIDs"></param>
        /// <param name="procPrescriptionIDs"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="selectedStatus"></param>
        /// <returns></returns>
        public DataSet GetScheduleServicesWithOutPatientRequestOfWithoutReceptionDirect(int[] routineIDs, int[] procedureIDs,
            int[] procPrescriptionIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                int[] episodeCases = new int[] { /*(int)EpisodeCaseEnum.DayTreatment,*/ (int)EpisodeCaseEnum.RoutineOutPatient };

                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.SelectDistinctScheduleServicesRoutinesOfWithoutReceptionDirectCommand, Environment.NewLine,
                        "   LEFT JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CR.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status = @StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CR.RoutineID IN (", StringUtils.BuildIDString(routineIDs), ")) ", Environment.NewLine,
                        "   AND (AppS.[ID] is null) ", Environment.NewLine);

                    if (toDate != null)
                        sqlRoutineQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (CR.StartAt<@EndDateTime))) ");

                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(
                        string.Format(SQLProvider.SelectDistinctScheduleServicesProceduresOfWithoutReceptionDirectCommand,
                        ((procPrescriptionIDs != null) && (procPrescriptionIDs.Length > 0))
                            ? string.Format("CAST((CASE WHEN (CP.ProcedureID IN ({0})) THEN {1} ELSE {2} END) AS bigint)",
                                StringUtils.BuildIDString(procPrescriptionIDs), ((int)AdditionalInfoTypeEnum.Prescription).ToString(), ((int)AdditionalInfoTypeEnum.WithInformation).ToString())
                            : ((int)AdditionalInfoTypeEnum.WithInformation).ToString()), Environment.NewLine,
                        "   LEFT JOIN CustomerOrderRequest COR WITH(NOLOCK) ON CP.CustomerOrderRequestID = COR.[ID] ", Environment.NewLine,
                        "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status = @StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CP.ProcedureID IN (", StringUtils.BuildIDString(procedureIDs), "))", Environment.NewLine,
                        "   AND (AppS.[ID] is null) ", Environment.NewLine);

                    if (toDate != null)
                        sqlProcedureQuery += string.Concat("  AND ((NOT(COR.[ID] IS NULL) AND ((NOT(COR.RequestEffectiveAtDateTime IS NULL) AND (COR.RequestEffectiveAtDateTime < @EndDateTime)) OR (COR.RequestDateTime < @EndDateTime))) ", Environment.NewLine,
                            "       OR ((COR.[ID] IS NULL) AND (CP.StartAt<@EndDateTime))) ");
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesRoutinesIDs(int[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctScheduleServicesRoutinesStandardCommand, Environment.NewLine,
                    "WHERE (CR.[ID] IN (", StringUtils.BuildIDString(ids), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesProceduresIDs(int[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctScheduleServicesProceduresStandardCommand, Environment.NewLine,
                    "WHERE (CP.[ID] IN (", StringUtils.BuildIDString(ids), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //UserTimeEntity-->ScheduleServiceBasicEntity
        public DataSet GetUserTimesScheduleServiceRoutineWithActivityInLocations(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(QueryScheduleServicesTimesRoutineOfCitation(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesTimesRoutineOfWaitingList(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesTimesRoutineOfReservationScheludedInRealization(locationIDs, fromDate, toDate, selectedStatus));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetUserTimesScheduleServiceProcedureWithActivityInLocations(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(QueryScheduleServicesTimesProcedureOfCitation(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesTimesProcedureOfWaitingList(locationIDs, fromDate, toDate, selectedStatus),
                    Environment.NewLine, "UNION ", Environment.NewLine,
                    QueryScheduleServicesTimesProcedureOfReservationScheludedInRealization(locationIDs, fromDate, toDate, selectedStatus));

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        private string QueryScheduleServicesTimesRoutineOfCitation(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceRoutineOfCitationCommand, Environment.NewLine,
                "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) ", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);


            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime)) ");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesTimesRoutineOfReservationScheludedInRealization(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceRoutineWithReservationCommand, Environment.NewLine,
                "   JOIN RealizationAppointmentService RAS ON (RA.[ID]=RAS.ElementID) AND (RAS.AppointmentElement=@ElementRoutine) ", Environment.NewLine,
                "   JOIN Location L ON (RAS.ResourceID=L.[ID]) AND (RAS.ResourceElement = @ResourceElementLocation) ", Environment.NewLine,
                "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime)) ");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesTimesRoutineOfWaitingList(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceRoutineOfWaitingListCommand, Environment.NewLine,
                "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) ", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime)) ");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesTimesProcedureOfCitation(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceProcedureOfCitationCommand, Environment.NewLine,
                "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) ", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime)) ");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CP.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesTimesProcedureOfWaitingList(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceProcedureOfWaitingListCommand, Environment.NewLine,
                "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) ", Environment.NewLine,
                "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime)) ");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CP.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        private string QueryScheduleServicesTimesProcedureOfReservationScheludedInRealization(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return String.Empty;

            string sqlQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceProcedureWithReservationCommand, Environment.NewLine,
                "   JOIN RealizationAppointmentService RAS WITH(NOLOCK) ON (PA.[ID]=RAS.ElementID) AND (RAS.AppointmentElement=@ElementProcedure) ", Environment.NewLine,
                "   JOIN Location L WITH(NOLOCK) ON (RAS.ResourceID=L.[ID]) AND (RAS.ResourceElement = @ResourceElementLocation) ", Environment.NewLine,
                "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), ")) AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine);

            if (fromDate != null)
                sqlQuery += string.Concat(" AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime)) ");
            if (toDate != null)
                sqlQuery += string.Concat(" AND (CP.StartAt<@EndDateTime) ");

            return sqlQuery;
        }

        public DataSet GetScheduleServicesTimesWithHospitalizationRequestCustomerEpisodeActive(int[] routineIDs, int[] procedureIDs,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.SelectDistinctCustomerRoutineTimesJoinStandardCommand, Environment.NewLine,
                        "JOIN CustomerEpisode CE WITH(NOLOCK) ON CR.EpisodeID=CE.[ID]", Environment.NewLine,
                        "JOIN EpisodeType ET WITH(NOLOCK) ON CE.EpisodeTypeID=ET.[ID]", Environment.NewLine,
                        "JOIN CustomerOrderRequest COR ON CR.CustomerOrderRequestID=COR.[ID]", Environment.NewLine,
                        "JOIN @TVPTable1a TVP1a ON CR.RoutineID=TVP1a.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CR.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE (CE.[Status]=@StatusActive)", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );

                    if (toDate != null)
                        sqlRoutineQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");
                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                        "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID] ", Environment.NewLine,
                        "JOIN EpisodeType ET WITH(NOLOCK) ON CE.EpisodeTypeID=ET.[ID] ", Environment.NewLine,
                        "JOIN CustomerOrderRequest COR ON CP.CustomerOrderRequestID=COR.[ID] ", Environment.NewLine,
                        "JOIN @TVPTable1b TVP1b ON CP.ProcedureID=TVP1b.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CP.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE (CE.[Status]=@StatusActive)", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );

                    if (toDate != null)
                        sqlProcedureQuery += string.Concat("  AND (CP.StartAt<@EndDateTime) ");
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                int[] statusList = this.BuildActionStatusArray(selectedStatus);
                int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient, (int)EpisodeCaseEnum.RoutineOutPatient };
                episodeCases = episodeCases
                                    .OrderBy(ec => ec)
                                    .ToArray();

                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1a", routineIDs));
                }
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1b", procedureIDs));
                }
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable2", statusList));
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable3", episodeCases));
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable, parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesTimesWithReservationRequest(int[] routineIDs, int[] procedureIDs,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.SelectDistinctCustomerRoutineTimesJoinStandardCommand, Environment.NewLine,
                        "JOIN RoutineAct RA WITH(NOLOCK) ON CR.[ID]=RA.CustomerRoutineID ", Environment.NewLine,
                        "JOIN ReservedRoutineAct RRA WITH(NOLOCK) ON RA.[ID]=RRA.ElementID AND RRA.Element=@ElementRoutine " + Environment.NewLine +
                        "JOIN CustomerReservation CRes WITH(NOLOCK) ON RRA.CustomerReservationID=CRes.[ID] ", Environment.NewLine,
                        "JOIN CustomerProcessStepsRel CPSR WITH(NOLOCK) ON CRes.[ID]=CPSR.CurrentStepID AND CPSR.[Step]=@StepReservation ", Environment.NewLine,
                        "JOIN CustomerProcess CP WITH(NOLOCK) ON CPSR.CustomerProcessID=CP.[ID] ", Environment.NewLine,
                        "JOIN CustomerOrderRequest COR ON CR.CustomerOrderRequestID=COR.[ID] ", Environment.NewLine,
                        "LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID=CE.[ID] ", Environment.NewLine,
                        "JOIN ProcessChart PC WITH(NOLOCK) ON CP.ProcessChartID=PC.[ID] ", Environment.NewLine,
                        "JOIN EpisodeType ET WITH(NOLOCK) ON PC.EpisodeConfigID=ET.[ID] ", Environment.NewLine,
                        "JOIN @TVPTable1a TVP1a ON CR.RoutineID=TVP1a.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CR.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive))", Environment.NewLine,
                        "      OR ((CE.[ID] is null) AND (CP.[Status]=@StatusActive)))", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );

                    if (toDate != null)
                        sqlRoutineQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");
                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                        "JOIN ProcedureAct PA WITH(NOLOCK) ON CP.[ID]=PA.CustomerProcedureID ", Environment.NewLine,
                        "JOIN ReservedProcedureAct RPA WITH(NOLOCK) ON PA.[ID]=RPA.ElementID AND RPA.Element=@ElementProcedure " + Environment.NewLine +
                        "JOIN CustomerReservation CRes WITH(NOLOCK) ON RPA.CustomerReservationID=CRes.[ID] ", Environment.NewLine,
                        "JOIN CustomerProcessStepsRel CPSR WITH(NOLOCK) ON CRes.[ID]=CPSR.CurrentStepID AND CPSR.[Step]=@StepReservation ", Environment.NewLine,
                        "JOIN CustomerProcess CPro WITH(NOLOCK) ON CPSR.CustomerProcessID=CP.[ID] ", Environment.NewLine,
                        "JOIN CustomerOrderRequest COR ON CP.CustomerOrderRequestID=COR.[ID] ", Environment.NewLine,
                        "LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON CPro.CustomerEpisodeID=CE.[ID] ", Environment.NewLine,
                        "JOIN ProcessChart PC WITH(NOLOCK) ON CPro.ProcessChartID=PC.[ID] ", Environment.NewLine,
                        "JOIN EpisodeType ET WITH(NOLOCK) ON PC.EpisodeConfigID=ET.[ID] ", Environment.NewLine,
                        "JOIN @TVPTable1b TVP1b ON CP.ProcedureID=TVP1b.[ID]", Environment.NewLine,
                        "JOIN @TVPTable2 TVP2 ON CP.[Status]=TVP2.[ID]", Environment.NewLine,
                        "JOIN @TVPTable3 TVP3 ON ET.EpisodeCase=TVP3.[ID]", Environment.NewLine,
                        "WHERE ((NOT(CE.[ID] is null) AND (CE.[Status]=@StatusActive)) ", Environment.NewLine,
                        "       OR ((CE.[ID] is null) AND (CPro.[Status]=@StatusActive))) ", Environment.NewLine,
                        "AND (COR.RequestDateTime<=@EndDateTime)"
                        );

                    if (toDate != null)
                        sqlProcedureQuery += string.Concat(" AND (CP.StartAt<@EndDateTime) ");
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                int[] statusList = this.BuildActionStatusArray(selectedStatus);
                int[] episodeCases = new int[] { (int)EpisodeCaseEnum.DayTreatment, (int)EpisodeCaseEnum.EmergencyOutPatient, (int)EpisodeCaseEnum.InPatient, (int)EpisodeCaseEnum.RoutineOutPatient };
                episodeCases = episodeCases
                                    .OrderBy(ec => ec)
                                    .ToArray();

                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value));
                parameters.Add(new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
                parameters.Add(new StoredProcInParam("StepReservation", DbType.Int32, (int)BasicProcessStepsEnum.Reservation));
                parameters.Add(new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine));
                parameters.Add(new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure));
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1a", routineIDs));
                }
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    parameters.Add(new StoredProcInTVPIntegerParam("TVPTable1b", procedureIDs));
                }
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable2", statusList));
                parameters.Add(new StoredProcInTVPIntegerParam("TVPTable3", episodeCases));
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable, parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesTimesWithOutPatientRequestOfReceptionDirect(int[] routineIDs, int[] procedureIDs,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                int[] episodeCases = new int[] { /*(int)EpisodeCaseEnum.DayTreatment,*/ (int)EpisodeCaseEnum.RoutineOutPatient };

                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceRoutineOfReceptionDirectCommand, Environment.NewLine,
                        "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status=@StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CR.RoutineID IN (", StringUtils.BuildIDString(routineIDs), "))", Environment.NewLine,
                        "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

                    if (toDate != null)
                        sqlRoutineQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");
                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceProcedureOfReceptionDirectCommand, Environment.NewLine,
                        "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status=@StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CP.ProcedureID IN (", StringUtils.BuildIDString(procedureIDs), "))", Environment.NewLine,
                        "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

                    if (toDate != null)
                        sqlProcedureQuery += string.Concat("  AND (CP.StartAt<@EndDateTime) ");
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesTimesWithOutPatientRequestOfWithoutReceptionDirect(int[] routineIDs, int[] procedureIDs,
            DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if ((routineIDs == null || routineIDs.Length <= 0) && (procedureIDs == null || procedureIDs.Length <= 0))
                return null;

            try
            {
                int[] episodeCases = new int[] { /*(int)EpisodeCaseEnum.DayTreatment,*/ (int)EpisodeCaseEnum.RoutineOutPatient };

                string sqlRoutineQuery = string.Empty;
                if ((routineIDs != null) && (routineIDs.Length > 0))
                {
                    sqlRoutineQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceRoutineOfWithoutReceptionDirectCommand, Environment.NewLine,
                        "WHERE (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status=@StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CR.RoutineID IN (", StringUtils.BuildIDString(routineIDs), "))", Environment.NewLine,
                        "   AND (AppS.[ID] IS NULL) ", Environment.NewLine);

                    if (toDate != null)
                        sqlRoutineQuery += string.Concat(" AND (CR.StartAt<@EndDateTime) ");
                }

                string sqlProcedureQuery = string.Empty;
                if ((procedureIDs != null) && (procedureIDs.Length > 0))
                {
                    sqlProcedureQuery = string.Concat(SQLProvider.SelectDistinctUserTimesScheduleServiceProcedureOfWithoutReceptionDirectCommand, Environment.NewLine,
                        "WHERE (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                        "   AND (CE.Status=@StatusActive) AND (ET.EpisodeCase IN (", StringUtils.BuildIDString(episodeCases), ")) ", Environment.NewLine,
                        "   AND (CP.ProcedureID IN (", StringUtils.BuildIDString(procedureIDs), "))", Environment.NewLine,
                        "   AND (AppS.[ID] IS NULL) ", Environment.NewLine);

                    if (toDate != null)
                        sqlProcedureQuery += string.Concat("  AND (CP.StartAt<@EndDateTime) ");
                }

                if (string.IsNullOrWhiteSpace(sqlRoutineQuery) && string.IsNullOrWhiteSpace(sqlProcedureQuery))
                    return null;

                string sqlQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(sqlRoutineQuery))
                {
                    sqlQuery = sqlRoutineQuery;
                    if (!string.IsNullOrWhiteSpace(sqlProcedureQuery))
                        sqlQuery += string.Concat(Environment.NewLine, "UNION", Environment.NewLine, sqlProcedureQuery);
                }
                else
                    sqlQuery = sqlProcedureQuery;

                if (string.IsNullOrWhiteSpace(sqlQuery))
                    return null;

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesTimesByRoutineIDs(int[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerRoutineTimesJoinStandardCommand, Environment.NewLine,
                    "WHERE (CR.[ID] IN (", StringUtils.BuildIDString(ids), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesTimesByProcedureIDs(int[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                    "WHERE (CP.[ID] IN (", StringUtils.BuildIDString(ids), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerProcessActivityInLocationEntity
        public DataSet GetCustomerProcessActivityInLocationByCustomerProcessIDs(int[] customerProcessIDs, int[] defaultIdentifierTypeIDs)
        {
            if (customerProcessIDs == null || customerProcessIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(string.Format(SQLProvider.SelectDistinctCustomerProcessActivityInLocationJoinStandardCommand,
                    ((defaultIdentifierTypeIDs != null) && (defaultIdentifierTypeIDs.Length > 0))
                        ? string.Concat(string.Format("ISNULL((SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS IDNumber, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine,
                          string.Format("ISNULL((SELECT TOP 1 IT.[Name] FROM PersonIdentifierRel PIR JOIN IdentifierType IT ON PIR.IdentifierTypeID=IT.[ID] WHERE (PIR.IdentifierTypeID IN ({0})) AND (PIR.PersonID=P.[ID])), '') AS DefaultIdentifier, ",
                                    StringUtils.BuildIDString(defaultIdentifierTypeIDs)), Environment.NewLine)
                        : string.Concat("'' AS IDNumber, '' AS DefaultIdentifier, ", Environment.NewLine)));

                sqlQuery += string.Concat("WHERE (CP.[ID] IN (", StringUtils.BuildIDString(customerProcessIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerProcessActivityInLocationTable,
                    new StoredProcInParam("ActiveStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerOrderRequestIDHasAppointment
        public DataSet GetCustomerOrderRequestIDHasAppointmentWithActivityInLocations(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string whereCitation = string.Concat("WHERE (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                    "   AND NOT(AppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed))", Environment.NewLine);

                string whereWaitingList = string.Concat("WHERE (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), "))", Environment.NewLine,
                    "   AND NOT(WLAppS.[Status] IN (@AvailStatusCancelled, @AvailStatusMissed)) ", Environment.NewLine);

                string sqlQuery = string.Concat(
                    //Routine
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfRoutineCitationCommand, Environment.NewLine,
                    whereCitation, " AND (PARA.[ID] IS NULL) AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND (RA.EndDateTime>=@StartDateTime) AND (RA.StartDateTime<@EndDateTime)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfRoutineWaitingListCommand, Environment.NewLine,
                    whereWaitingList, " AND (PARA.[ID] IS NULL) AND (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND (RA.EndDateTime>=@StartDateTime) AND (RA.StartDateTime<@EndDateTime)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //Procedure
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfProcedureCitationCommand, Environment.NewLine,
                    whereCitation, " AND (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND (PA.EndDateTime>=@StartDateTime) AND (PA.StartDateTime<@EndDateTime)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfProcedureWaitingListCommand, Environment.NewLine,
                    whereWaitingList, " AND (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND (PA.EndDateTime>=@StartDateTime) AND (PA.StartDateTime<@EndDateTime)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //CustomerRoutine
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerRoutineCitationCommand, Environment.NewLine,
                    whereCitation, " AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime)) AND (CR.StartAt<@EndDateTime)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerRoutineWaitingListCommand, Environment.NewLine,
                    whereWaitingList, " AND (CR.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND ((CR.EndingTo IS NULL) OR (CR.EndingTo>=@StartDateTime)) AND (CR.StartAt<@EndDateTime)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //CustomerProcedure
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerProcedureCitationCommand, Environment.NewLine,
                    whereCitation, " AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime)) AND (CP.StartAt<@EndDateTime)", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestIDHasAppointmentOfCustomerProcedureWaitingListCommand, Environment.NewLine,
                    whereWaitingList, " AND (CP.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    " AND ((CP.EndingTo IS NULL) OR (CP.EndingTo>=@StartDateTime)) AND (CP.StartAt<@EndDateTime)");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestHasAppointmentTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ElementMedicalOrder", DbType.Int32, (int)AppointmentElementEnum.MedicalOrder),
                    new StoredProcInParam("StepCitation", DbType.Int32, (int)BasicProcessStepsEnum.Citation),
                    new StoredProcInParam("StepWaitingList", DbType.Int32, (int)BasicProcessStepsEnum.WaitingList),
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("AvailStatusCancelled", DbType.Int32, (int)AvailStatusEnum.Cancelled),
                    new StoredProcInParam("AvailStatusMissed", DbType.Int32, (int)AvailStatusEnum.Missed)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //ActPlanification
        public DataSet GetActPlanificationtWithActivityInLocations(int[] locationIDs, DateTime? fromDate, DateTime? toDate, int selectedStatus)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(
                    //Routine
                    SQLProvider.SelectDictinctActPlanificationRoutineActJoinStandardCommand, Environment.NewLine,
                    "WHERE (RA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine,
                    "UNION", Environment.NewLine,
                    //Procedure
                    SQLProvider.SelectDictinctActPlanificationProcedureActJoinStandardCommand, Environment.NewLine,
                    "WHERE (PA.[Status] IN (", this.BuildActionStatusString(selectedStatus), "))", Environment.NewLine,
                    "   AND (L.[ID] IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine
                    );

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ActPlanificationTable,
                    new StoredProcInParam("ResourceElementLocation", DbType.Int32, (int)AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("ElementRoutine", DbType.Int32, (int)AppointmentElementEnum.Routine),
                    new StoredProcInParam("ElementProcedure", DbType.Int32, (int)AppointmentElementEnum.Procedure)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        public DataSet GetCustomerMedEpisodeClosed(int[] ccpIDs)
        {
            if ((ccpIDs == null) || (ccpIDs.Length <= 0)) return null;
            try
            {
                string sqlQuery = string.Concat(SQLProvider.ValidateMedEpisodeCloseByCCPIDs2Command,
                    " AND CAP.[ID] IN (", StringUtils.BuildIDString(ccpIDs), ")");
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerCareProcessTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        #region Para listados de prescripciones
        //CustomerOrderRequest (Prescription)
        public DataSet GetCustomerOrderRequestsPrescriptionsByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) AND (OT.OrderCase=@OrderClassificationType)", Environment.NewLine);
                if (fromDate != null)
                    sqlWhere += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlWhere += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestBasicPrescriptionJOINCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestBasicPrescriptionJOINCustomerProcedureCustomerEpisodeCommand, Environment.NewLine, sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestsPrescriptionsByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CE.[ID]=@CustomerEpisodeID) AND (OT.OrderCase=@OrderClassificationType)", Environment.NewLine);

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestBasicPrescriptionJOINCustomerEpisodeCommand, Environment.NewLine, sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestBasicPrescriptionJOINCustomerProcedureCustomerEpisodeCommand, Environment.NewLine, sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.CustomerOrderRequestBasicTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerOrderRequestsTime -- CustomerOrderRequest (Prescription)
        public DataSet GetCustomerOrderRequestTimesPrescriptionsByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) AND (OT.OrderCase=@OrderClassificationType)", Environment.NewLine);
                if (fromDate != null)
                    sqlWhere += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlWhere += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.OrderRequestTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestTimesPrescriptionsByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CE.[ID]=@CustomerEpisodeID) AND (OT.OrderCase=@OrderClassificationType)", Environment.NewLine);

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestsTimesJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //CustomerOrderRequestReasonRel -- CustomerOrderRequest (Prescription)
        public DataSet GetCustomerOrderRequestReasonRelsPrescriptionsByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) AND (OT.OrderCase=@OrderClassificationType)", Environment.NewLine);
                if (fromDate != null)
                    sqlWhere += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlWhere += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestReasonRelsJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestReasonRelsJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerOrderRequestReasonRelsPrescriptionsByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CE.[ID]=@CustomerEpisodeID) AND (OT.OrderCase=@OrderClassificationType)", Environment.NewLine);

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerOrderRequestReasonRelsJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctCustomerOrderRequestReasonRelsJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN PrescriptionRequest PR WITH(NOLOCK) ON COR.[ID]=PR.CustomerOrderRequestID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.OrderRequestTimeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //ScheduleServices (Prescription) solo CustomerProcedures
        public DataSet GetCustomerScheduleServicesPrescrptionsByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(
                    string.Format(SQLProvider.SelectDistinctCustomerScheduleServicesProcedurePrescriptionJoinStandardCommand,
                        string.Concat("CAST(", ((int)AdditionalInfoTypeEnum.Prescription).ToString(), " AS bigint)")), Environment.NewLine,
                        "WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine);

                if (fromDate != null)
                    sqlQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerScheduleServicesPrescriptionsByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(
                    string.Format(SQLProvider.SelectDistinctCustomerScheduleServicesProcedurePrescriptionJoinStandardCommand,
                        string.Concat("CAST(", ((int)AdditionalInfoTypeEnum.Prescription).ToString(), " AS bigint)")), Environment.NewLine,
                        "WHERE (CE.ID=@CustomerEpisodeID) ", Environment.NewLine);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ScheduleServiceBasicTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //UserTimes -- ScheduleServices (Prescription) solo CustomerProcedures
        public DataSet GetScheduleServicesPrescrptionsTimesByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID] ", Environment.NewLine,
                    "   JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID] ", Environment.NewLine,
                    "   JOIN PrescriptionRequest PR WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ");

                if (fromDate != null)
                    sqlQuery += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");

                if (toDate != null)
                    sqlQuery += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetScheduleServicesPrescrptionsTimesByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectDistinctCustomerProcedureTimesJoinStandardCommand, Environment.NewLine,
                    "   JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID] ", Environment.NewLine,
                    "   JOIN PrescriptionRequest PR WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID AND NOT(PR.[Status]=@ActionStatusSuperceded)", Environment.NewLine,
                    "WHERE (CE.ID=@CustomerEpisodeID)");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, BackOffice.Entities.TableNames.UserTimeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //PrescriptionRequest 
        public DataSet GetPrescriptionRequestsByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                
                return this.Gateway.ExecuteStoredProcedureDataSet("GetPrescriptionRequestsByLocationIDs",
                        
                        new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                        new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                        new StoredProcInTVPIntegerParam("LocationIDs", locationIDs));
                /* 
                SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@StartDateTime", SqlDbType.DateTime, 8, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
						ParametroSql.add("@EndDateTime", SqlDbType.DateTime, 8, (toDate != null) ? (object)toDate : (object)DBNull.Value),
						ParametroSql.add("@LocationIDs", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(locationIDs))
					};
                return SqlHelper.ExecuteDataset("GetPrescriptionRequestsByLocationIDs", aParam);*/
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPrescriptionRequestsByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE NOT(PR.[Status]=@ActionStatusSuperceded) AND (CE.[ID]=@CustomerEpisodeID) AND (OT.OrderCase=@OrderClassificationType)");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctPrescriptionRequestJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctPrescriptionRequestJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.PrescriptionRequestTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //PrescriptionRequestTimes
        public DataSet GetPrescriptionRequestTimesByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE NOT(PR.[Status]=@ActionStatusSuperceded) AND (OT.OrderCase=@OrderClassificationType) ", Environment.NewLine,
                    "   AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine);
                if (fromDate != null)
                    sqlWhere += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlWhere += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctPrescriptionRequestTimeJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctPrescriptionRequestTimeJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.PrescriptionRequestTimeTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPrescriptionRequestTimesByCustomerEpisodeIDs(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE NOT(PR.[Status]=@ActionStatusSuperceded) AND (CE.[ID]=@CustomerEpisodeID) AND (OT.OrderCase=@OrderClassificationType)");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctPrescriptionRequestTimeJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON COR.CustomerEpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere,
                    Environment.NewLine, "UNION", Environment.NewLine,
                    SQLProvider.SelectDistinctPrescriptionRequestTimeJoinStandardCommand, Environment.NewLine,
                    "    JOIN [Order] O WITH(NOLOCK) ON COR.OrderID=O.[ID]", Environment.NewLine,
                    "    JOIN [OrderType] OT WITH(NOLOCK) ON O.OrderTypeID = OT.[ID]", Environment.NewLine,
                    "    JOIN CustomerProcedure CP WITH(NOLOCK) ON CP.[ID]=PR.CustomerProcedureID", Environment.NewLine,
                    "    JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.EpisodeID=CE.[ID]", Environment.NewLine,
                    "    JOIN CustomerAdmission CA WITH(NOLOCK) ON CA.[ID]=CE.CustomerAdmissionID", Environment.NewLine,
                    sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.PrescriptionRequestTimeTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //ProcedureAct (Prescription)
        public DataSet GetProcedureActsPrescriptionsByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                
                return this.Gateway.ExecuteStoredProcedureDataSet("GetProcedureActPrescriptionsByLocationIDs",

                        new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                        new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                        new StoredProcInTVPIntegerParam("LocationIDs", locationIDs));  
                /* 
                SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@StartDateTime", SqlDbType.DateTime, 8, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
						ParametroSql.add("@EndDateTime", SqlDbType.DateTime, 8, (toDate != null) ? (object)toDate : (object)DBNull.Value),
						ParametroSql.add("@LocationIDs", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(locationIDs))
					};
                return SqlHelper.ExecuteDataset("GetProcedureActPrescriptionsByLocationIDs", aParam);*/
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetProcedureActsPrescriptionsByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CE.[ID]=@CustomerEpisodeID) AND (OT.OrderCase=@OrderClassificationType)");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctProcedureActPrescriptionCommand, Environment.NewLine, sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ProcedureActTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //ProcedureActResourceRel (Prescription)
        public DataSet GetProcedureActResourceRelsPrescriptionsByLocationIDs(int[] locationIDs, DateTime? fromDate, DateTime? toDate)
        {
            if (locationIDs == null || locationIDs.Length <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (OT.OrderCase=@OrderClassificationType) ", Environment.NewLine,
                    "   AND (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), ")) ", Environment.NewLine);
                if (fromDate != null)
                    sqlWhere += string.Concat(" AND ((CE.EndDateTime IS NULL) OR (CE.EndDateTime>=@StartDateTime)) ");
                if (toDate != null)
                    sqlWhere += string.Concat(" AND (CE.StartDateTime<=@EndDateTime) ");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctProcedureActResourceRelPrescriptionCommand, Environment.NewLine, sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ProcedureActResourceRelTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, (fromDate != null) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, (toDate != null) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetProcedureActResourceRelsPrescriptionsByCustomerEpisodeID(int customerEpisodeID)
        {
            if (customerEpisodeID <= 0)
                return null;

            try
            {
                string sqlWhere = string.Concat("WHERE (CE.[ID]=@CustomerEpisodeID) AND (OT.OrderCase=@OrderClassificationType)");

                string sqlQuery = string.Concat(SQLProvider.SelectDistinctProcedureActResourceRelPrescriptionCommand, Environment.NewLine, sqlWhere);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Assistance.Entities.TableNames.ProcedureActResourceRelTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("OrderClassificationType", DbType.Int32, (int)OrderClassTypeEnum.DrugPrescription),
                    new StoredProcInParam("ActionStatusSuperceded", DbType.Int32, (int)ActionStatusEnum.Superceded));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion


        /// esto viene provocado por la tarea 382
        /// que necesitan que el servicio y cirujano que van en tablas IMQ aparezcan en el censo de solicitudes
        public DataSet ObtenerServicioYCirujano(int[] corIDs)
        {
            if ((corIDs == null) || (corIDs.Length <= 0))
                return null;

            try
            {
                string sql = string.Concat(
                    "SELECT DISTINCT hhrr.ID, COR.ID CustomerOrderRequestID, hhrr.ProfileID, ", Environment.NewLine,
                    "ASS.Name, P.FirstName CirFirstName,  P.LastName CirLastName,  P.LastName2 CirLastName2 ", Environment.NewLine,
                    "FROM CustomerOrderRequest COR ", Environment.NewLine,
                    "JOIN  @TVPTable TVP ON COR.ID = TVP.ID ", Environment.NewLine,
                    "JOIN IB_IMQ_OrderRequestServiceWGRel IBR ON COR.ID = IBR.CustomerOrderRequestID ", Environment.NewLine,
                    "JOIN AssistanceService ASS ON IBR.ServiceID = ASS.ID ", Environment.NewLine,
                    "JOIN OrderRequestHumanResourceRel hhrr on cor.ID = hhrr.CustomerOrderRequestID ", Environment.NewLine,
                    "JOIN HumanResource hr on hhrr.HumanResourceID = hr.ID ", Environment.NewLine,
                    "JOIN Person p on hr.PersonID = p.id ",
                    "LEFT JOIN Profile pr on hhrr.ProfileID = pr.ID");

                return this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.Assistance.Entities.TableNames.IMQServicioYCirujanoTable,
                    new StoredProcInTVPIntegerParam("TVPTable", corIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }

        }

        /// esto viene provocado por la tarea 388
        /// que necesitan que el anestesista que vaAnestesista en tablas IMQ aparezcan en el censo de solicitudes
        public DataSet ObtenerAnestesista(int[] corIDs, string profileCode)
        {
            if ((corIDs == null) || (corIDs.Length <= 0))
                return null;

            try
            {
                string sql = string.Concat(
                    "SELECT DISTINCT COR.ID, P.FirstName AnestFirstName,  P.LastName AnestLastName,  P.LastName2 AnestLastName2 ", Environment.NewLine,
                    "FROM CustomerOrderRequest COR ", Environment.NewLine,
                    "JOIN  @TVPTable TVP ON COR.ID = TVP.ID ", Environment.NewLine,
                    "JOIN ProcedureAct PA ON PA.CustomerOrderRequestID = COR.ID ", Environment.NewLine,
                    "JOIN ProcedureActHumanResourceRel hhrr on PA.ID = hhrr.ProcedureActID ", Environment.NewLine,
                    "JOIN HumanResource hr on hhrr.HumanResourceID = hr.ID ", Environment.NewLine,
                    "JOIN Person p on hr.PersonID = p.id ", Environment.NewLine,
                    "LEFT JOIN Profile pr on hhrr.ProfileID = pr.ID ", Environment.NewLine,
                    "WHERE pr.[Code]=@ProfileCode");

                return this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.BackOffice.Entities.TableNames.IMQAnestesistaTable,
                    new StoredProcInParam("ProfileCode", DbType.String, profileCode),
                    new StoredProcInTVPIntegerParam("TVPTable", corIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }

        }

        /// esto viene provocado por la tarea 388
        /// que necesitan que el tipo de anestesia  aparezcan en el censo de solicitudes
        public DataSet ObtenerTipoAnestesia(int[] corIDs, string ObservationCode)//la observacion 131 es tipo anestesia, en principio el profileCode deberia ser 131
        {
            if ((corIDs == null) || (corIDs.Length <= 0))
                return null;

            try
            {
                string sql = string.Concat(
                    "SELECT DISTINCT p.CustomerOrderRequestID ID, p.LastUpdated,  obsopt.[Description] Description ", Environment.NewLine,
                    "FROM CustomerObservation cor ", Environment.NewLine,
                    "JOIN Observation obs on cor.ObservationID=obs.ID ", Environment.NewLine,
                    "JOIN ObservationOption obsopt on obsopt.ObservationID=obs.ID ", Environment.NewLine,
                    "JOIN ObservationValue ov on cor.ObservationValueID=ov.ID and obsopt.ID=ov.IntValue ", Environment.NewLine,
                    "JOIN ProcedureActObsRel paor on paor.CustomerObservationID=cor.ID ", Environment.NewLine,
                    "JOIN ProcedureAct p on paor.ProcedureActID=p.ID ", Environment.NewLine,
                    "JOIN  @TVPTable TVP ON p.CustomerOrderRequestID = TVP.ID ", Environment.NewLine,
                    "where obs.AssignedCode = @ObservationCode ", Environment.NewLine,
                    "order by p.LastUpdated desc");

                return this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.BackOffice.Entities.TableNames.IMQTipoAnestesiaTable,
                    new StoredProcInParam("ObservationCode", DbType.String, ObservationCode),
                    new StoredProcInTVPIntegerParam("TVPTable", corIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }

        }


        public DataSet GetEpisodeService(int[] customerEpisodeIDs)
        {
            if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;
            try
            {
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).Distinct().OrderBy(id => id).ToArray();
                if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                    return null;
                string sql = string.Concat(
                    "SELECT CESR.CustomerEpisodeID, ASERV.Name AssistanceServiceName, ASERV.Encoder AssistanceServiceCode, ASERV.AssignedCode AssistanceServiceAssignedCode ", Environment.NewLine,
                    "       FROM CustomerEpisodeServiceRel CESR WITH(NOLOCK) ", Environment.NewLine,
                    "        JOIN AssistanceService ASERV WITH(NOLOCK) ON CESR.AssistanceServiceID = ASERV.[ID] ", Environment.NewLine,
                    "        JOIN @TVPTable TVP ON TVP.ID = CESR.CustomerEpisodeID ", Environment.NewLine,
                    "        ORDER BY CESR.StartAt DESC");

                return this.Gateway.ExecuteQueryDataSet(sql, Administrative.Entities.TableNames.CustomerEpisodeServiceRelTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


    }
}
