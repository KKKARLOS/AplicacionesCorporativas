using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.DA
{
    public class CustomerPreAssessmentDA : DAServiceBase
    {
        #region Field length constants
        public const int ModifiedByLength = 256;
        #endregion

        public CustomerPreAssessmentDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerPreAssessmentDA(Gateway gateway) : base(gateway) { }

        public int Insert(int customerID, int personID, int customerProcessID, int processChartID, DateTime? initDate, DateTime? confirmedDate, int informationProvidedPersonType,
            int informationProvidedByID, string interviewMadeIn, int interviewMadeByID, int assistanceDegreeRequestedID, int assistanceDegreeRecommendedID,
            string preAssessmentDocument, string recommendations, string explanations, int ancestorID, int customerADTOrderID, int status, DateTime lastUpdated,
            string modifiedBy, Int64 dbTimeStamp
            )
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerPreAssessmentCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("PersonID", DbType.Int32, personID),
                    new StoredProcInParam("CustomerProcessID", DbType.Int32, customerProcessID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("InitDate", DbType.DateTime, initDate),
                    new StoredProcInParam("ConfirmedDate", DbType.DateTime, confirmedDate),
                    new StoredProcInParam("InformationProvidedPersonType", DbType.Int32, informationProvidedPersonType),
                    new StoredProcInParam("InformationProvidedByID", DbType.Int32, informationProvidedByID),
                    new StoredProcInParam("InterviewMadeIn", DbType.String, interviewMadeIn),
                    new StoredProcInParam("InterviewMadeByID", DbType.Int32, interviewMadeByID),
                    new StoredProcInParam("AssistanceDegreeRequestedID", DbType.Int32, assistanceDegreeRequestedID),
                    new StoredProcInParam("AssistanceDegreeRecommendedID", DbType.Int32, assistanceDegreeRecommendedID),
                    new StoredProcInParam("PreAssessmentDocument", DbType.String, preAssessmentDocument),
                    new StoredProcInParam("Recommendations", DbType.String, recommendations),
                    new StoredProcInParam("Explanations", DbType.String, explanations),
                    new StoredProcInParam("AncestorID", DbType.Int32, ancestorID),
                    new StoredProcInParam("CustomerADTOrderID", DbType.Int32, customerADTOrderID),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("LastUpdated", DbType.DateTime, (lastUpdated != DateTime.MinValue) ? (object)lastUpdated : (object)DBNull.Value),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy),
                    new StoredProcInParam("DBTimeStamp", DbType.Int64, dbTimeStamp)
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

        public int Update(int id, int customerID, int personID, int customerProcessID, int processChartID, DateTime? initDate, DateTime? confirmedDate, int informationProvidedPersonType,
            int informationProvidedByID, string interviewMadeIn, int interviewMadeByID, int assistanceDegreeRequestedID, int assistanceDegreeRecommendedID,
            string preAssessmentDocument, string recommendations, string explanations, int ancestorID, int customerADTOrderID, int status, DateTime lastUpdated,
            string modifiedBy, Int64 dbTimeStamp)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerPreAssessmentCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("PersonID", DbType.Int32, personID),
                    new StoredProcInParam("CustomerProcessID", DbType.Int32, customerProcessID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("InitDate", DbType.DateTime, initDate),
                    new StoredProcInParam("ConfirmedDate", DbType.DateTime, confirmedDate),
                    new StoredProcInParam("InformationProvidedPersonType", DbType.Int32, informationProvidedPersonType),
                    new StoredProcInParam("InformationProvidedByID", DbType.Int32, informationProvidedByID),
                    new StoredProcInParam("InterviewMadeIn", DbType.String, interviewMadeIn),
                    new StoredProcInParam("InterviewMadeByID", DbType.Int32, interviewMadeByID),
                    new StoredProcInParam("AssistanceDegreeRequestedID", DbType.Int32, assistanceDegreeRequestedID),
                    new StoredProcInParam("AssistanceDegreeRecommendedID", DbType.Int32, assistanceDegreeRecommendedID),
                    new StoredProcInParam("PreAssessmentDocument", DbType.String, preAssessmentDocument),
                    new StoredProcInParam("Recommendations", DbType.String, recommendations),
                    new StoredProcInParam("Explanations", DbType.String, explanations),
                    new StoredProcInParam("AncestorID", DbType.Int32, ancestorID),
                    new StoredProcInParam("CustomerADTOrderID", DbType.Int32, customerADTOrderID),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("LastUpdated", DbType.DateTime, (lastUpdated != DateTime.MinValue) ? (object)lastUpdated : (object)DBNull.Value),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy),
                    new StoredProcInParam("DBTimeStamp", DbType.Int64, dbTimeStamp)
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerPreAssessmentStampCommand,
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

        public int SetCustomerPreAssessmentStatus(int id, int status, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.SetCustomerPreAssessmentStatusCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
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

        public int DeleteCustomerPreAssessment(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.DeleteCustomerPreAssessmentByIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int CountCustomerPreAssessment()
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.CountCustomerPreAssessmentCommand))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["Count"].ToString(), 0);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetCustomerPreAssessment(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerPreAssessmentCommand,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        public DataSet GetCustomerPreAssessment(int id, ElementReasonTypeEnum elementReasonType)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("ObtenerCustomerPreAssessmentEntity",
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("PreAssessment", DbType.Int32, (int)elementReasonType)
                    );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentTypeRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.EpisodeReasonTypeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.EpisodeReasonTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.EpisodeReasonElementRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PreAssessmentTypeTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AssistanceDegreeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentCustomerTemplateRelTable;
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
        public DataSet GetCustomerPreAssessment2(int id, ElementReasonTypeEnum elementReasonType)
        {
            try
            {
                SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@ID", SqlDbType.Int, 4, id),
                        ParametroSql.add("@PreAssessment", SqlDbType.Int, 4, (int)elementReasonType)
					};
                DataSet ds = SqlHelper.ExecuteDataset("ObtenerCustomerPreAssessmentEntity", aParam);

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentTypeRelTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentReasonRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.EpisodeReasonTypeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.EpisodeReasonTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.EpisodeReasonElementRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PreAssessmentTypeTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AssistanceDegreeTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerPreAssessmentCustomerTemplateRelTable;
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

        //public DataSet GetCustomerPreAssessments()
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllCustomerPreAssessmentCommand,
        //            Administrative.Entities.TableNames.CustomerPreAssessmentTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public Int64 GetDBTimeStamp(int id)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerPreAssessmentDBTimeStampCommand,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[Administrative.Entities.TableNames.CustomerPreAssessmentTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[Administrative.Entities.TableNames.CustomerPreAssessmentTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetPreviousCustomerPreAssessment(int processChartID, int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPreviousCustomerPreAssessmentCommand,
                    Administrative.Entities.TableNames.CustomerPreAssessmentListDTOTable,
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPreviousCustomerPreAssessmentFromEpisode(int processChartID, int customerEpisodeID, int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPreviousCustomerPreAssessmentFromEpisodeCommand,
                    Administrative.Entities.TableNames.CustomerPreAssessmentListDTOTable,
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        

        public DataSet GetCustomerPreAssessmentByCustomerID(int customerID)
        {
            try
            {
                string query = string.Format(SQLProvider.GetCustomerPreAssessmentByCustomerIDCommand, "WHERE CPA.CustomerID=@CustomerID ");

                return this.Gateway.ExecuteQueryDataSet(query,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerPreAssessmentByCustomerID(int customerID, int processChartID)
        {
            try
            {
                string query = string.Format(SQLProvider.GetCustomerPreAssessmentByCustomerIDCommand, "WHERE CPA.CustomerID=@CustomerID AND CPA.ProcessChartID=@ProcessChartID ");

                return this.Gateway.ExecuteQueryDataSet(query,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerPreAssessmentByLocation(int locationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerPreAssessmentByProcessChartAndLocationCommand,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable,
                    new StoredProcInParam("LocationID", DbType.Int32, locationID),
                    new StoredProcInParam("ActiveStatus", DbType.Int16, CommonEntities.StatusEnum.Active),
                    new StoredProcInParam("ClosedStatus", DbType.Int16, CommonEntities.StatusEnum.Confirmed));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerPreAssessmentByLocationsID(int[] locationIDs)
        {
            try
            {
                if (locationIDs == null || locationIDs.Length <= 0) return null;
                string sqlQuery = string.Concat("JOIN CustomerEpisode CE WITH(NOLOCK) ON CE.CustomerID=CPA.CustomerID AND CE.ProcessChartID=CPA.ProcessChartID ", Environment.NewLine);
                sqlQuery += string.Concat("JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID=CA.[ID]", Environment.NewLine);
                sqlQuery += string.Concat("JOIN CustomerProcess CP WITH(NOLOCK) ON CP.CustomerEpisodeID=CE.[ID]", Environment.NewLine);
                sqlQuery += string.Concat("WHERE (CA.CurrentLocationID IN (", StringUtils.BuildIDString(locationIDs), "))");
                sqlQuery = string.Format(SQLProvider.GetCustomerPreAssessmentByCustomerIDCommand, sqlQuery);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Administrative.Entities.TableNames.CustomerPreAssessmentTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerPreAssessmentByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                string query = string.Format(SQLProvider.GetCustomerPreAssessmentByCustomerIDCommand,
                    "JOIN CustomerProcess CP WITH(NOLOCK) ON CPA.CustomerProcessID=CP.[ID] WHERE CP.CustomerEpisodeID=@CustomerEpisodeID");

                return this.Gateway.ExecuteQueryDataSet(query,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerPreAssessmentByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            if ((customerEpisodeIDs == null) || (customerEpisodeIDs.Length <= 0))
                return null;

            try
            {
                string query = string.Concat(SQLProvider.SelectDistinctCustomerPreAssessmentJoinStandardCommand, Environment.NewLine,
                    "	JOIN CustomerProcess CP WITH(NOLOCK) ON CPA.CustomerProcessID=CP.[ID] ", Environment.NewLine,
                    "WHERE (CP.CustomerEpisodeID IN (", StringUtils.BuildIDString(customerEpisodeIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(query,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerPreAssessmentByCustomerProcessIDs(int[] customerProcessIDs)
        {
            if ((customerProcessIDs == null) || (customerProcessIDs.Length <= 0))
                return null;

            try
            {
                string query = string.Concat(SQLProvider.SelectDistinctCustomerPreAssessmentJoinStandardCommand, Environment.NewLine,
                    "	JOIN CustomerProcess CP WITH(NOLOCK) ON CPA.CustomerProcessID=CP.[ID] ", Environment.NewLine,
                    "WHERE (CP.[ID] IN (", StringUtils.BuildIDString(customerProcessIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(query,
                    Administrative.Entities.TableNames.CustomerPreAssessmentTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerPreAssessmentsByCustomerProcess(int[] processChartIDs,
            BackOffice.Entities.BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
            int[] locations, int[] careCenterIDs, int assistanceServiceID,
            DateTime? startDateTime, DateTime? endDateTime)
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


                string finalQuery = SQLProvider.GetCustomerPreAssessmentsByCustomerProcessCommand;

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
                    if (step != BasicProcessStepsEnum.None)
                    {
                        /// primero analizo si tengo que poner mas joins
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerProcessStepsRel CPSR WITH(NOLOCK) ON CP.[ID] = CPSR.CustomerProcessID ");

                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            "CPSR.Step = ", ((long)step).ToString(), " AND CPSR.CurrentStepID > 0 ");
                        andPossible = true;
                        if (status != CommonEntities.StatusEnum.None)
                            wheres = string.Concat(wheres, " AND CPSR.StepStatus = ", ((int)status).ToString(), " ");
                    }
                    if (processChartIDs != null && processChartIDs.Length > 0)
                    {
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " PC.[ID] IN (", whereProcessChartIDs, ") ");
                        andPossible = true;
                    }
                    if (locations != null && locations.Length > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID = CA.ID ");
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CA.CurrentLocationID IN (", whereLocationIDs, ") ");
                        andPossible = true;
                    }
                    if (assistanceServiceID > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        if (locations == null || locations.Length <= 0)
                            includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID = CESR.CustomerEpisodeID ");
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CESR.AssistanceServiceID = ", assistanceServiceID.ToString(), " ");
                        andPossible = true;
                    }
                    if (careCenterIDs != null && careCenterIDs.Length > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        includes = string.Concat(includes, Environment.NewLine,
                                    "JOIN CareCenter CC WITH(NOLOCK) ON CP.CareCenterID = CC.[ID] ", Environment.NewLine,
                                    "JOIN Organization OCC WITH(NOLOCK) ON CC.OrganizationID = OCC.[ID] ");

                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CP.CareCenterID IN (", StringUtils.BuildIDString(careCenterIDs), ") ");
                        andPossible = true;
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
                            if (step == BasicProcessStepsEnum.Admission || step == BasicProcessStepsEnum.Reception)
                            {
                                if ((locations == null || locations.Length <= 0) && assistanceServiceID <= 0)
                                    includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CE.EndDateTime IS NULL OR CE.EndDateTime >= @StartDateTime) ");
                            }
                            else
                            {
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CPSR.StepDateTime IS NULL OR CPSR.StepDateTime >= @StartDateTime) ");
                            }
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
                            if (step == BasicProcessStepsEnum.Admission || step == BasicProcessStepsEnum.Reception)
                            {
                                if ((locations == null || locations.Length <= 0) && assistanceServiceID <= 0 && startDateTime == null)
                                    includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CE.StartDateTime IS NULL OR CE.StartDateTime <= @EndDateTime) ");
                            }
                            else
                            {
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CPSR.StepDateTime IS NULL OR CPSR.StepDateTime <= @EndDateTime) ");
                            }
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
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentTable,
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
        public DataSet GetCustomerPreAssessmentsByQueryCustomerProcessID(string queryFilterCustomerProcessMaxRows, StoredProcParam[] parameters)
        {
            try
            {
                string finalQuery = string.Format(SQLProvider.GetCustomerPreAssessmentsByWithQueryCustomerProcessFilterMaxRowCommand, queryFilterCustomerProcessMaxRows);

                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerPreAssessmentTable,
                    parameters
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public bool AssistanceDegreeRelInUse(int assistanceDegreeID, int preAssessmentTypeID, int processChartID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.AssistanceDegreeRelInUseCommand,
                    new StoredProcInParam("AssistanceDegreeID", DbType.Int32, assistanceDegreeID),
                    new StoredProcInParam("PreAssessmentTypeID", DbType.Int32, preAssessmentTypeID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID)
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

        public bool PreAssessmentTypeRelInUse(int preAssessmentTypeID, int processChartID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.PreAssessmentTypeRelInUsenCommand,
                    new StoredProcInParam("PreAssessmentTypeID", DbType.Int32, preAssessmentTypeID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID)
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

    }
}
