using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.Addin.Entities;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.DA
{
    public class CustomerCodificationDA : DAServiceBase
    {
        #region Field lenght consts
        public const int ModifiedByLength = 256;
        public const int CaseMixGroupConceptEncoderNameLength = 100;
        #endregion

        #region Constructors
        public CustomerCodificationDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerCodificationDA(Gateway gateway) : base(gateway) { }
        #endregion

        #region Private methods
        #endregion

        #region Public methods
        #region Write methods
        public int Insert(int customerID, int processChartID, int customerEpisodeID,
            int caseMixGroupID, int caseMixGroupConceptID, decimal estimatedCost, double relatedWeight,
            decimal relatedCost, string explanation, DateTime registrationDateTime,
            DateTime? confirmedDate, int codifiedByID, int confirmedByID,
            bool exported, DateTime? exportedDateTime, string exportedBy,
            CommonEntities.StatusEnum status, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerCodificationCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CaseMixGroupID", DbType.Int32, caseMixGroupID),
                    new StoredProcInParam("CaseMixGroupConceptID", DbType.Int32, caseMixGroupConceptID),
                    new StoredProcInParam("EstimatedCost", DbType.Decimal, estimatedCost),
                    new StoredProcInParam("RelatedWeight", DbType.Double, relatedWeight),
                    new StoredProcInParam("RelatedCost", DbType.Decimal, relatedCost),
                    new StoredProcInParam("Explanation", DbType.String, explanation),
                    new StoredProcInParam("RegistrationDateTime", DbType.DateTime, (registrationDateTime != null) ? (object)registrationDateTime : (object)null),
                    new StoredProcInParam("ConfirmedDate", DbType.DateTime, (confirmedDate != null) ? (object)confirmedDate : (object)null),
                    new StoredProcInParam("CodifiedByID", DbType.Int32, codifiedByID),
                    new StoredProcInParam("ConfirmedByID", DbType.Int32, confirmedByID),

                    new StoredProcInParam("Exported", DbType.Boolean, exported),
                    new StoredProcInParam("ExportedDateTime", DbType.DateTime, (exportedDateTime != null) ? (object)exportedDateTime : (object)null),
                    new StoredProcInParam("ExportedBy", DbType.String, SIIStrings.Left(exportedBy, ModifiedByLength)),

                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
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

        public int Update(int id, int customerID, int processChartID, int customerEpisodeID,
            int caseMixGroupID, int caseMixGroupConceptID, decimal estimatedCost, double relatedWeight,
            decimal relatedCost, string explanation, DateTime registrationDateTime,
            DateTime? confirmedDate, int codifiedByID, int confirmedByID,
            bool exported, DateTime? exportedDateTime, string exportedBy,
            CommonEntities.StatusEnum status, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerCodificationCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CaseMixGroupID", DbType.Int32, caseMixGroupID),
                    new StoredProcInParam("CaseMixGroupConceptID", DbType.Int32, caseMixGroupConceptID),
                    new StoredProcInParam("EstimatedCost", DbType.Decimal, estimatedCost),
                    new StoredProcInParam("RelatedWeight", DbType.Double, relatedWeight),
                    new StoredProcInParam("RelatedCost", DbType.Decimal, relatedCost),
                    new StoredProcInParam("Explanation", DbType.String, explanation),
                    new StoredProcInParam("RegistrationDateTime", DbType.DateTime, (registrationDateTime != null) ? (object)registrationDateTime : (object)null),
                    new StoredProcInParam("ConfirmedDate", DbType.DateTime, (confirmedDate != null) ? (object)confirmedDate : (object)null),
                    new StoredProcInParam("CodifiedByID", DbType.Int32, codifiedByID),
                    new StoredProcInParam("ConfirmedByID", DbType.Int32, confirmedByID),

                    new StoredProcInParam("Exported", DbType.Boolean, exported),
                    new StoredProcInParam("ExportedDateTime", DbType.DateTime, (exportedDateTime != null) ? (object)exportedDateTime : (object)null),
                    new StoredProcInParam("ExportedBy", DbType.String, SIIStrings.Left(exportedBy, ModifiedByLength)),

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

        public int Delete(int id, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.DeleteCustomerCodificationCommand,
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

        public int SetStatus(int id, CommonEntities.StatusEnum status, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.SetStatusCustomerCodificationCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int16, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public void SetAllExported(int[] ccIDs, DateTime exportedDateTime, string modifiedBy)
        {
            try
            {
                string whereIDs = string.Empty;
                if (ccIDs != null && ccIDs.Length > 0)
                {
                    whereIDs = StringUtils.BuildIDString(ccIDs);
                }
                this.Gateway.ExecuteQueryNonQuery(string.Concat(SQLProvider.SetAllExportedCommand, Environment.NewLine,
                    "WHERE [ID] IN (", whereIDs, ")"),
                new StoredProcInParam("ExportedDateTime", DbType.DateTime, exportedDateTime),
                new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
            }
        }

        #endregion

        #region Read methods
        public DataSet Get(int id)
        {
            try
            {
                string sqlQuery = string.Concat(SQLProvider.GetCustomerCodificationByIDCommand,Environment.NewLine, "WHERE [ID] = @ID");
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetByIDs(int[] ids)
        {
            try
            {
                if (ids == null || ids.Length <= 0) return null;
                string whereCCODIDs = StringUtils.BuildIDString(ids);
                string sqlQuery = string.Concat(SQLProvider.GetCustomerCodificationByIDCommand, Environment.NewLine, 
                    " WHERE [ID] IN (", whereCCODIDs, ") ");
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }


        public Int64 GetDBTimeStamp(int id)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCodificationTimeStampCommand, SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable) && result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        //public DataSet GetCustomerCodification(int maxRecords)
        //{
        //    try
        //    {
        //        if (maxRecords < 0)
        //            throw new ArgumentException("maxRecords");

        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCodificationWithMaxRecordsCommand, TableNames.CustomerCodificationTable,
        //            new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetByTimeStamp(long timestamp)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCodificationByTimeStampCommand, SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable,
                    new StoredProcInParam("DBTimeStamp", DbType.Int64, timestamp));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerCodificationByCustomerID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCodificationByCustomerCommand, SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerCodificationByCustomerEpisode(int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCodificationByCustomerEpisodeCommand, SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public bool ExistsByProcessChartID(int processChartID, CommonEntities.StatusEnum status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerCodificationByProcessChartIDCommand,
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("Status", DbType.Int32, (int)status)
                    ))
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


        public DataSet GetMDSCustomerCodification(int maxRows, int[] processChartIDs,
                BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
                int[] locations, int[] careCenterIDs, int assistanceServiceID,
                DateTime? startDateTime, DateTime? endDateTime, string transferReason,
                MixedCodificationStatusEnum codificationStatus)
        {
            try
            {   
                return this.Gateway.ExecuteStoredProcedureDataSet("GetMDSCustomerCodification", 
                    SII.HCD.Administrative.Entities.TableNames.CustomerCodificationTable,
                    600,

                    new StoredProcInParam("Step", DbType.Int32, (step != null) ? (int)step : (int)BasicProcessStepsEnum.None),
                    new StoredProcInParam("Status", DbType.Int32, (status != null) ? (int)status : (int)CommonEntities.StatusEnum.None),
                    new StoredProcInParam("AssistanceService", DbType.Int32, assistanceServiceID),
                    new StoredProcInParam("CodificationStatus", DbType.Int32, (codificationStatus != null) ? (int)codificationStatus : (int)MixedCodificationStatusEnum.None),
                    new StoredProcInParam("TransferReason", DbType.String, transferReason),

                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime),

                    new StoredProcInTVPIntegerParam("TVPProcessChartIDs", processChartIDs),
                    new StoredProcInTVPIntegerParam("TVPLocations", locations),
                    new StoredProcInTVPIntegerParam("TVPCareCenterIDs", careCenterIDs)
                    );              
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public SqlDataReader GetMDSCustomerCodificationDR(int maxRows, int[] processChartIDs,
                BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
                int[] locations, int[] careCenterIDs, int assistanceServiceID,
                DateTime? startDateTime, DateTime? endDateTime, string transferReason,
                MixedCodificationStatusEnum codificationStatus)
        {

            try
            {  
                SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@Step", SqlDbType.Int, 4, (step != null) ? (int)step : (int)BasicProcessStepsEnum.None),
						ParametroSql.add("@Status", SqlDbType.Int, 4,  (status != null) ? (int)status : (int)CommonEntities.StatusEnum.None),
						ParametroSql.add("@AssistanceService", SqlDbType.Int, 4, assistanceServiceID),
						ParametroSql.add("@CodificationStatus", SqlDbType.Int, 4,  (codificationStatus != null) ? (int)codificationStatus : (int)MixedCodificationStatusEnum.None),
                        ParametroSql.add("@TransferReason", SqlDbType.Text, transferReason),
						ParametroSql.add("@StartDateTime", SqlDbType.DateTime, 8, startDateTime),
						ParametroSql.add("@EndDateTime", SqlDbType.DateTime, 8, endDateTime),

						ParametroSql.add("@TVPProcessChartIDs", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(processChartIDs)),
						ParametroSql.add("@TVPLocations", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(locations)),
						ParametroSql.add("@TVPCareCenterIDs", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListTPVInteger(careCenterIDs))
					};
                SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GetMDSCustomerCodification", aParam);
                return dr;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        #endregion
        #endregion

        #region Public Methods ONLY Related MDS and DRG extraction and export info
        #region Read methods
        //public DataSet GetMDSReportLines(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {

        //        string findCommand = string.Concat(SQLProvider.GetMDSReportLinesCommand,Environment.NewLine, 
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND (CE.StartDateTime <= @ToDate) AND (@Step = 2048))", Environment.NewLine ,
        //            "OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))");


        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSReportLineTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }

        //}

        public DataSet GetMDSReportLines(int[] customerEpisodeIDs)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;

            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetMDSReportLinesCommand,Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSReportLineTable);
                    
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }

        }

        public DataSet GetMDSReportLinesCodification(int[] customerEpisodeIDs)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;

            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetMDSReportLinesCommand, Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSReportLineTable);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }

        }

        //public DataSet GetMDSHeadLineInfo(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //         string findCommand = string.Concat(SQLProvider.GetMDSHeadLineInfoCommand,Environment.NewLine, 
        //                    "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND ",Environment.NewLine,
        //                    "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSHeadLineInfoTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("DocumentType", DbType.Int32, (int)MDSDocumentTypeEnum.OutPatientReport),
        //            new StoredProcInParam("DocumentDate", DbType.DateTime, DateTime.Now));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        //public DataSet GetMDSHeadLineInfo(int[] customerEpisodeIDs)
        //{
        //    if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
        //    try
        //    {
        //        string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
        //        string findCommand = string.Concat(SQLProvider.GetMDSHeadLineInfoCommand, Environment.NewLine,
        //                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSHeadLineInfoTable,
        //            new StoredProcInParam("DocumentType", DbType.Int32, (int)MDSDocumentTypeEnum.OutPatientReport),
        //            new StoredProcInParam("DocumentDate", DbType.DateTime, DateTime.Now),
        //            new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.EmergencyOutPatient),
        //            new StoredProcInParam("InPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.InPatient)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetMDSHeadLineInfo(int[] customerEpisodeIDs)
        {
            try
            {
                int[] Destino;

                int iLongitudMaxima = 100;
                int iRegistrosTratados = 0;
                DataSet dsEpisodeHeadLineInfo = new DataSet();
                DataSet dsAux = new DataSet();
                string whereEpisodeIDs = string.Empty;
                string findCommand = string.Empty;

                if (customerEpisodeIDs.Length > iLongitudMaxima)
                {
                    for (int iVecesProcesado = 1; iVecesProcesado <= (customerEpisodeIDs.Length / iLongitudMaxima); iVecesProcesado++)
                    {
                        Destino = new int[iLongitudMaxima];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, iLongitudMaxima);

                        iRegistrosTratados = iVecesProcesado * iLongitudMaxima;

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetMDSHeadLineInfoCommand, Environment.NewLine,
                                "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSHeadLineInfoTable,
                            new StoredProcInParam("DocumentType", DbType.Int32, (int)MDSDocumentTypeEnum.OutPatientReport),
                            new StoredProcInParam("DocumentDate", DbType.DateTime, DateTime.Now),
                            new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.EmergencyOutPatient),
                            new StoredProcInParam("InPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.InPatient)
                            );

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeHeadLineInfo.Tables.Count <= 0)
                            {
                                dsEpisodeHeadLineInfo = dsAux.Clone();
                            }

                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeHeadLineInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }

                    if (customerEpisodeIDs.Length - iRegistrosTratados > 0)
                    {
                        Destino = new int[customerEpisodeIDs.Length - iRegistrosTratados];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, customerEpisodeIDs.Length - iRegistrosTratados);

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetMDSHeadLineInfoCommand, Environment.NewLine,
                                "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSHeadLineInfoTable,
                            new StoredProcInParam("DocumentType", DbType.Int32, (int)MDSDocumentTypeEnum.OutPatientReport),
                            new StoredProcInParam("DocumentDate", DbType.DateTime, DateTime.Now),
                            new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.EmergencyOutPatient),
                            new StoredProcInParam("InPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.InPatient)
                            );

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeHeadLineInfo.Tables.Count <= 0)
                            {
                                dsEpisodeHeadLineInfo = dsAux.Clone();
                            }
                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeHeadLineInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Destino = new int[customerEpisodeIDs.Length];
                    Array.Copy(customerEpisodeIDs, Destino, customerEpisodeIDs.Length);

                    whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                    findCommand = string.Concat(SQLProvider.GetMDSHeadLineInfoCommand, Environment.NewLine,
                                "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                    dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSHeadLineInfoTable,
                            new StoredProcInParam("DocumentType", DbType.Int32, (int)MDSDocumentTypeEnum.OutPatientReport),
                            new StoredProcInParam("DocumentDate", DbType.DateTime, DateTime.Now),
                            new StoredProcInParam("EmergencyOutPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.EmergencyOutPatient),
                            new StoredProcInParam("InPatient", DbType.Int32, (int)SII.HCD.BackOffice.Entities.EpisodeCaseEnum.InPatient)
                            );

                    if (dsAux.Tables.Count > 0)
                    {
                        if (dsEpisodeHeadLineInfo.Tables.Count <= 0)
                        {
                            dsEpisodeHeadLineInfo = dsAux.Clone();
                        }

                        if (dsAux.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                            {
                                dsEpisodeHeadLineInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                            }
                        }
                    }
                }

                return dsEpisodeHeadLineInfo;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetMDSFacilityInfo(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate, string facilityServiceType)
        //{
        //    try
        //    {
        //        string findCommand = string.Concat(SQLProvider.GetMDSFacilityInfoCommand,Environment.NewLine, 
        //                   "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND ", Environment.NewLine ,
        //                   "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSFacilityInfoTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("FacilityServiceType", DbType.String, facilityServiceType));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetMDSFacilityInfo(int[] customerEpisodeIDs, string facilityServiceType)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);

                string findCommand = string.Concat(SQLProvider.GetMDSFacilityInfoCommand, Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSFacilityInfoTable,
                    new StoredProcInParam("FacilityServiceType", DbType.String, facilityServiceType)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetMDSAddressInfo(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {

        //        string findCommand = string.Concat(SQLProvider.GetMDSAddress1InfoCommand,Environment.NewLine, 
        //           "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND ", Environment.NewLine ,
        //           "(((CE.StartDateTime >= @FromDate) AND (CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ", Environment.NewLine ,
        //           "((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "UNION",Environment.NewLine, 
        //            SQLProvider.GetMDSAddress2InfoCommand,Environment.NewLine, 
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND " , Environment.NewLine ,
        //            "(((CE.StartDateTime >= @FromDate) AND (CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR " , Environment.NewLine ,
        //            "((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAddressInfoTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetMDSAddressInfo(int[] customerEpisodeIDs)
        {
            try
            {
                int[] Destino;

                int iLongitudMaxima = 1000;
                int iRegistrosTratados = 0;
                DataSet dsEpisodeAddressInfo = new DataSet();
                DataSet dsAux = new DataSet();
                string whereEpisodeIDs = string.Empty;
                string findCommand = string.Empty;

                if (customerEpisodeIDs.Length > iLongitudMaxima)
                {
                    for (int iVecesProcesado = 1; iVecesProcesado <= (customerEpisodeIDs.Length / iLongitudMaxima); iVecesProcesado++)
                    {
                        Destino = new int[iLongitudMaxima];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, iLongitudMaxima);

                        iRegistrosTratados = iVecesProcesado * iLongitudMaxima;

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetMDSAddress1InfoCommand, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ")", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetMDSAddress2InfoCommand, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAddressInfoTable);

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeAddressInfo.Tables.Count <= 0)
                            {
                                dsEpisodeAddressInfo = dsAux.Clone();
                            }

                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeAddressInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }

                    if (customerEpisodeIDs.Length - iRegistrosTratados > 0)
                    {
                        Destino = new int[customerEpisodeIDs.Length - iRegistrosTratados];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, customerEpisodeIDs.Length - iRegistrosTratados);

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetMDSAddress1InfoCommand, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ")", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetMDSAddress2InfoCommand, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAddressInfoTable);

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeAddressInfo.Tables.Count <= 0)
                            {
                                dsEpisodeAddressInfo = dsAux.Clone();
                            }
                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeAddressInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Destino = new int[customerEpisodeIDs.Length];
                    Array.Copy(customerEpisodeIDs, Destino, customerEpisodeIDs.Length);

                    whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                    findCommand = string.Concat(SQLProvider.GetMDSAddress1InfoCommand, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ")", Environment.NewLine,
                        "UNION", Environment.NewLine,
                        SQLProvider.GetMDSAddress2InfoCommand, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                    dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAddressInfoTable);

                    if (dsAux.Tables.Count > 0)
                    {
                        if (dsEpisodeAddressInfo.Tables.Count <= 0)
                        {
                            dsEpisodeAddressInfo = dsAux.Clone();
                        }

                        if (dsAux.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                            {
                                dsEpisodeAddressInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                            }
                        }
                    }
                }

                //whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);


                //return this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                //    new StoredProcInParam("SpecializedCareAttributeID", DbType.Int32, specializedCareAttributeID));
                //return this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                //    /*new StoredProcInParam("CustomerEpisodeID", DbType.String, string.Concat( "(", whereEpisodeIDs, ")")),*/
                //    new StoredProcInParam("LocUCIID", DbType.Int32, 45));
                return dsEpisodeAddressInfo;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetMDSAddressInfo(int[] customerEpisodeIDs)
        //{
        //    if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
        //    try
        //    {
        //        string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
        //        string findCommand = string.Concat(SQLProvider.GetMDSAddress1InfoCommand, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ")", Environment.NewLine,
        //            "UNION", Environment.NewLine,
        //            SQLProvider.GetMDSAddress2InfoCommand, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAddressInfoTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        //public DataSet GetMDSCustomerInfo(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate, string defaultIdentifier)
        //{
        //    try
        //    {
        //        string findCommand = string.Concat(SQLProvider.GetMDSCustomerInfoCommand,Environment.NewLine, 
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND " , Environment.NewLine ,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND " , Environment.NewLine ,
        //            "(CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" + Environment.NewLine +
        //            "AND IT.[Name]=@DefaultIdentifier");

        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSCustomerInfoTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("DefaultIdentifier", DbType.String, defaultIdentifier));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetMDSCustomerInfo(int[] customerEpisodeIDs, string defaultIdentifier)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetMDSCustomerInfoCommand,Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ") ");
                return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSCustomerInfoTable,
                    new StoredProcInParam("DefaultIdentifier", DbType.String, defaultIdentifier));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetMDSTransferFacilityInfo(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        string findCommand = string.Concat(SQLProvider.GetMDSTransferFacilityInfoCommand,Environment.NewLine, 
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND " , Environment.NewLine ,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))");

        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSTransferFacilityInfoTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetMDSFacilityNumberInfo(int[] customerEpisodeIDs, string defaultIdentifier)
        {
            //
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetMDSFacilityNumberInfoCommand, Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                //if (!string.IsNullOrEmpty(defaultIdentifier))
                //    findCommand = string.Concat(findCommand, Environment.NewLine, " AND NOT(IT.[Name]='", defaultIdentifier, "') ");
                return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSFacilityNumberInfoTable,
                    new StoredProcInParam("DefaultIdentifier", DbType.String, defaultIdentifier));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetMDSCHFacilityNumberInfo(int[] customerEpisodeIDs)
        {
            //
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetMDSCHFacilityNumberInfoCommand, Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSCHFacilityNumberInfoTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }



        public DataSet GetMDSTransferFacilityInfo(int[] customerEpisodeIDs)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetMDSTransferFacilityInfoCommand, Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSTransferFacilityInfoTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetEpisodeLeaveReason(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        string findCommand = string.Concat(SQLProvider.GetEpisodeLeaveReasonCommand, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND (CE.StartDateTime <= @ToDate) AND ", Environment.NewLine,
        //            "(@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetEpisodeLeaveReason(int[] customerEpisodeIDs)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetEpisodeLeaveReasonCommand, Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                return this.Gateway.ExecuteQueryDataSet(findCommand, SII.HCD.Administrative.Entities.TableNames.EpisodeLeaveReasonTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetEpisodeReason(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        string findCommand = string.Concat(SQLProvider.GetEpisodeReasonCommand, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND " , Environment.NewLine ,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetEpisodeReason(int[] customerEpisodeIDs)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                string findCommand = string.Concat(SQLProvider.GetEpisodeReasonCommand, Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereEpisodeIDs, ")");
                return this.Gateway.ExecuteQueryDataSet(findCommand, SII.HCD.Administrative.Entities.TableNames.EpisodeReasonTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetEpisodeLocation(int[] customerEpisodeIDs, int specializedCareAttributeID)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                int[] Destino;

                int iLongitudMaxima = 1000;
                int iRegistrosTratados = 0;
                DataSet dsEpisodeLocation = new DataSet();
                DataSet dsAux = new DataSet();
                string whereEpisodeIDs = string.Empty;
                string findCommand = string.Empty;

                if (customerEpisodeIDs.Length > iLongitudMaxima)
                {
                    for (int iVecesProcesado = 1; iVecesProcesado <= (customerEpisodeIDs.Length / iLongitudMaxima); iVecesProcesado++)
                    {
                        Destino = new int[iLongitudMaxima];                        

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, iLongitudMaxima);

                        iRegistrosTratados = iVecesProcesado * iLongitudMaxima;

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetEpisodeLocation1Command,
                            "(", whereEpisodeIDs, ")");
                        findCommand += string.Concat(SQLProvider.GetEpisodeLocation2Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND LOCC.ID = @LocUCIID");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,                
                            new StoredProcInParam("LocUCIID", DbType.Int32, 45));

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeLocation.Tables.Count <= 0)
                            {
                                dsEpisodeLocation = dsAux.Clone();
                            }

                            if (dsAux.Tables[0].Rows.Count > 0)
                            { 
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeLocation.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }

                    if (customerEpisodeIDs.Length - iRegistrosTratados > 0)
                    {
                        Destino = new int[customerEpisodeIDs.Length - iRegistrosTratados];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, customerEpisodeIDs.Length - iRegistrosTratados);

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetEpisodeLocation1Command,
                            "(", whereEpisodeIDs, ")");
                        findCommand += string.Concat(SQLProvider.GetEpisodeLocation2Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND LOCC.ID = @LocUCIID");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                            new StoredProcInParam("LocUCIID", DbType.Int32, 45));

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeLocation.Tables.Count <= 0)
                            {
                                dsEpisodeLocation = dsAux.Clone();
                            }
                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeLocation.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Destino = new int[customerEpisodeIDs.Length];
                    Array.Copy(customerEpisodeIDs, Destino, customerEpisodeIDs.Length);

                    whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                    findCommand = string.Concat(SQLProvider.GetEpisodeLocation1Command,
                        "(", whereEpisodeIDs, ")");
                    findCommand += string.Concat(SQLProvider.GetEpisodeLocation2Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND LOCC.ID = @LocUCIID");
                    dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                        new StoredProcInParam("LocUCIID", DbType.Int32, 45));

                    if (dsAux.Tables.Count > 0)
                    {
                        if (dsEpisodeLocation.Tables.Count <= 0)
                        {
                            dsEpisodeLocation = dsAux.Clone();
                        }

                        if (dsAux.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                            {
                                dsEpisodeLocation.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                            }
                        }
                    }
                }

                //whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
                
                
                //return this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                //    new StoredProcInParam("SpecializedCareAttributeID", DbType.Int32, specializedCareAttributeID));
                //return this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                //    /*new StoredProcInParam("CustomerEpisodeID", DbType.String, string.Concat( "(", whereEpisodeIDs, ")")),*/
                //    new StoredProcInParam("LocUCIID", DbType.Int32, 45));
                return dsEpisodeLocation;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetEpisodeEncoders(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        string findCommand = string.Concat(SQLProvider.GetEpisodeEncoders1Command, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND ", Environment.NewLine,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "AND CPSR.Step = 8388608 /* Codification */ AND CD.PrimaryDiagnosis = 0", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders2Command, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND ", Environment.NewLine,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders3Command, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND ", Environment.NewLine,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 1 /* Surgical */", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders4Command, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND " , Environment.NewLine ,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 2 /* Obstetric */", Environment.NewLine ,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders5Command, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND " , Environment.NewLine ,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass IN (3,4) /* Others */", Environment.NewLine ,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders6Command, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND " , Environment.NewLine ,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "AND CPSR.Step = 8388608 /* Codification */ AND CD.PrimaryDiagnosis = 0", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders7Command, Environment.NewLine,
        //            "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND ", Environment.NewLine,
        //            "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //            "AND CPSR.Step = 8388608 /* Codification */");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.EpisodeEncodersTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        //public DataSet GetEpisodeEncoders(int[] customerEpisodeIDs)
        //{
        //    if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
        //    try
        //    {
        //        string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
        //        string findCommand = string.Concat(SQLProvider.GetEpisodeEncoders1Command, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders2Command, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders3Command, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 1 /* Surgical */", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders4Command, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 2 /* Obstetric */", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders5Command, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass IN (3,4) /* Others */", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders6Command, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
        //            "UNION" , Environment.NewLine,
        //            SQLProvider.GetEpisodeEncoders7Command, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
        //            "ORDER BY RealizationDateTime");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.EpisodeEncodersTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetEpisodeEncoders(int[] customerEpisodeIDs)
        {
            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                int[] Destino;

                int iLongitudMaxima = 1000;
                int iRegistrosTratados = 0;
                DataSet dsEpisodeEncoders = new DataSet();
                DataSet dsAux = new DataSet();
                string whereEpisodeIDs = string.Empty;
                string findCommand = string.Empty;

                if (customerEpisodeIDs.Length > iLongitudMaxima)
                {
                    for (int iVecesProcesado = 1; iVecesProcesado <= (customerEpisodeIDs.Length / iLongitudMaxima); iVecesProcesado++)
                    {
                        Destino = new int[iLongitudMaxima];                        

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, iLongitudMaxima);

                        iRegistrosTratados = iVecesProcesado * iLongitudMaxima;

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetEpisodeLocation1Command,
                            "(", whereEpisodeIDs, ")");
                        findCommand = string.Concat(SQLProvider.GetEpisodeEncoders1Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders2Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders3Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 1 /* Surgical */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders4Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 2 /* Obstetric */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders5Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass IN (3,4) /* Others */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders6Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders7Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
                            "ORDER BY RealizationDateTime");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.EpisodeEncodersTable);

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeEncoders.Tables.Count <= 0)
                            {
                                dsEpisodeEncoders = dsAux.Clone();
                            }

                            if (dsAux.Tables[0].Rows.Count > 0)
                            { 
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeEncoders.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }

                    if (customerEpisodeIDs.Length - iRegistrosTratados > 0)
                    {
                        Destino = new int[customerEpisodeIDs.Length - iRegistrosTratados];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, customerEpisodeIDs.Length - iRegistrosTratados);

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetEpisodeEncoders1Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders2Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders3Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 1 /* Surgical */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders4Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 2 /* Obstetric */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders5Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass IN (3,4) /* Others */", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders6Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
                            "UNION", Environment.NewLine,
                            SQLProvider.GetEpisodeEncoders7Command, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
                            "ORDER BY RealizationDateTime");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.EpisodeEncodersTable);

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeEncoders.Tables.Count <= 0)
                            {
                                dsEpisodeEncoders = dsAux.Clone();
                            }
                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeEncoders.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Destino = new int[customerEpisodeIDs.Length];
                    Array.Copy(customerEpisodeIDs, Destino, customerEpisodeIDs.Length);

                    whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                    findCommand = string.Concat(SQLProvider.GetEpisodeEncoders1Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
                        "UNION", Environment.NewLine,
                        SQLProvider.GetEpisodeEncoders2Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
                        "UNION", Environment.NewLine,
                        SQLProvider.GetEpisodeEncoders3Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 1 /* Surgical */", Environment.NewLine,
                        "UNION", Environment.NewLine,
                        SQLProvider.GetEpisodeEncoders4Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass = 2 /* Obstetric */", Environment.NewLine,
                        "UNION", Environment.NewLine,
                        SQLProvider.GetEpisodeEncoders5Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ AND EP.ProcedureClass IN (3,4) /* Others */", Environment.NewLine,
                        "UNION", Environment.NewLine,
                        SQLProvider.GetEpisodeEncoders6Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */ ", Environment.NewLine,
                        "UNION", Environment.NewLine,
                        SQLProvider.GetEpisodeEncoders7Command, Environment.NewLine,
                        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = 8388608 /* Codification */", Environment.NewLine,
                        "ORDER BY RealizationDateTime");
                    dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.EpisodeEncodersTable);

                    if (dsAux.Tables.Count > 0)
                    {
                        if (dsEpisodeEncoders.Tables.Count <= 0)
                        {
                            dsEpisodeEncoders = dsAux.Clone();
                        }

                        if (dsAux.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                            {
                                dsEpisodeEncoders.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                            }
                        }
                    }
                }

                return dsEpisodeEncoders;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        //public DataSet GetMDSAssistanceInfo(int episodeTypeID, BasicProcessStepsEnum step, DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {

        //        string findCommand = string.Concat(SQLProvider.GetMDSAssistanceInfoCommand, Environment.NewLine,
        //        "WHERE CE.EpisodeTypeID=@EpisodeTypeID AND (((CE.StartDateTime >= @FromDate) AND " , Environment.NewLine ,
        //        "(CE.StartDateTime <= @ToDate) AND (@Step = 2048)) OR ((CE.EndDateTime >= @FromDate) AND (CE.EndDateTime <= @ToDate) AND (@Step = 16384)))" , Environment.NewLine ,
        //        "AND CPSR.Step = 32768 /* Realization */");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAssistanceInfoTable,
        //            new StoredProcInParam("EpisodeTypeID", DbType.Int32, episodeTypeID),
        //            new StoredProcInParam("Step", DbType.Int32, (int)step),
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate >= SqlDateTime.MinValue) ? fromDate : SqlDateTime.MinValue),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate >= SqlDateTime.MinValue) ? toDate : SqlDateTime.MinValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        //public DataSet GetMDSAssistanceInfo(int[] customerEpisodeIDs)
        //{
        //    if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
        //    try
        //    {
        //        string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
        //        string findCommand = string.Concat(SQLProvider.GetMDSAssistanceInfoCommand, Environment.NewLine,
        //            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = @Step");
        //        return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAssistanceInfoTable,
        //            new StoredProcInParam("Step", DbType.Int64, (long)BasicProcessStepsEnum.Realization));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return null;
        //    }
        //}

        public DataSet GetMDSAssistanceInfo(int[] customerEpisodeIDs)
        {
            //if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            //try
            //{
            //    string whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);
            //    string findCommand = string.Concat(SQLProvider.GetMDSAssistanceInfoCommand, Environment.NewLine,
            //        "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = @Step");
            //    return this.Gateway.ExecuteQueryDataSet(findCommand, TableNames.MDSAssistanceInfoTable,
            //        new StoredProcInParam("Step", DbType.Int64, (long)BasicProcessStepsEnum.Realization));
            //}
            //catch (Exception ex)
            //{
            //    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
            //    return null;
            //}

            if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
            try
            {
                int[] Destino;

                int iLongitudMaxima = 100;
                int iRegistrosTratados = 0;
                DataSet dsEpisodeAssistanceInfo = new DataSet();
                DataSet dsAux = new DataSet();
                string whereEpisodeIDs = string.Empty;
                string findCommand = string.Empty;

                if (customerEpisodeIDs.Length > iLongitudMaxima)
                {
                    for (int iVecesProcesado = 1; iVecesProcesado <= (customerEpisodeIDs.Length / iLongitudMaxima); iVecesProcesado++)
                    {
                        Destino = new int[iLongitudMaxima];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, iLongitudMaxima);

                        iRegistrosTratados = iVecesProcesado * iLongitudMaxima;

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetMDSAssistanceInfoCommand, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = @Step");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.MDSAssistanceInfoTable,
                            new StoredProcInParam("Step", DbType.Int64, (long)BasicProcessStepsEnum.Realization));

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeAssistanceInfo.Tables.Count <= 0)
                            {
                                dsEpisodeAssistanceInfo = dsAux.Clone();
                            }

                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeAssistanceInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }

                    if (customerEpisodeIDs.Length - iRegistrosTratados > 0)
                    {
                        Destino = new int[customerEpisodeIDs.Length - iRegistrosTratados];

                        Array.Copy(customerEpisodeIDs, iRegistrosTratados, Destino, 0, customerEpisodeIDs.Length - iRegistrosTratados);

                        whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                        findCommand = string.Concat(SQLProvider.GetMDSAssistanceInfoCommand, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = @Step");
                        dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.MDSAssistanceInfoTable,
                            new StoredProcInParam("Step", DbType.Int64, (long)BasicProcessStepsEnum.Realization));

                        if (dsAux.Tables.Count > 0)
                        {
                            if (dsEpisodeAssistanceInfo.Tables.Count <= 0)
                            {
                                dsEpisodeAssistanceInfo = dsAux.Clone();
                            }
                            if (dsAux.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                                {
                                    dsEpisodeAssistanceInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Destino = new int[customerEpisodeIDs.Length];
                    Array.Copy(customerEpisodeIDs, Destino, customerEpisodeIDs.Length);

                    whereEpisodeIDs = StringUtils.BuildIDString(Destino);
                    findCommand = string.Concat(SQLProvider.GetMDSAssistanceInfoCommand, Environment.NewLine,
                            "WHERE CE.[ID] IN (", whereEpisodeIDs, ") AND CPSR.Step = @Step");
                    dsAux = this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.MDSAssistanceInfoTable,
                        new StoredProcInParam("Step", DbType.Int64, (long)BasicProcessStepsEnum.Realization));

                    if (dsAux.Tables.Count > 0)
                    {
                        if (dsEpisodeAssistanceInfo.Tables.Count <= 0)
                        {
                            dsEpisodeAssistanceInfo = dsAux.Clone();
                        }

                        if (dsAux.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsAux.Tables[0].Rows.Count; i++)
                            {
                                dsEpisodeAssistanceInfo.Tables[0].ImportRow(dsAux.Tables[0].Rows[i]);
                            }
                        }
                    }
                }

                //whereEpisodeIDs = StringUtils.BuildIDString(customerEpisodeIDs);


                //return this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                //    new StoredProcInParam("SpecializedCareAttributeID", DbType.Int32, specializedCareAttributeID));
                //return this.Gateway.ExecuteQueryDataSet(findCommand, BackOffice.Entities.TableNames.LocationTable,
                //    /*new StoredProcInParam("CustomerEpisodeID", DbType.String, string.Concat( "(", whereEpisodeIDs, ")")),*/
                //    new StoredProcInParam("LocUCIID", DbType.Int32, 45));
                return dsEpisodeAssistanceInfo;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Boolean CanBePrincipalDiagnosis(string strAssignedCode)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.CanBePrincipalDiagnosis,                    
                    new StoredProcInParam("AssignedCode", DbType.String, strAssignedCode)))
                {
                    return (IsEmptyReader(reader)) ? true : false;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public Boolean CanBePrincipalEpisodeProcedure(string strAssignedCode)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.CanBePrincipalEpisodeProcedure,                    
                    new StoredProcInParam("AssignedCode", DbType.String, strAssignedCode)))
                {
                    return (IsEmptyReader(reader)) ? true : false;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }


        #endregion
        #endregion



    }
}

