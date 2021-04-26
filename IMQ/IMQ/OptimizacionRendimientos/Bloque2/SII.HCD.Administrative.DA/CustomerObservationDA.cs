using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Misc;

namespace SII.HCD.Administrative.DA
{
    public class CustomerObservationDA : DAServiceBase
    {
        #region constructor
        public CustomerObservationDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerObservationDA(Gateway gateway) : base(gateway) { }
        #endregion

        #region public
        #region public olds
        public int Insert(int customerID, int observationID, int kindOf, int basicType, int observationValueID, int extObservationValueID, int customerObservationEvalTestID, int ancestorID, int specialCategoryType, int status, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerObservationCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID),
                    new StoredProcInParam("KindOf", DbType.Int32, kindOf),
                    new StoredProcInParam("BasicType", DbType.Int32, basicType),
                    new StoredProcInParam("ObservationValueID", DbType.Int32, observationValueID),
                    new StoredProcInParam("ExtObservationValueID", DbType.Int32, extObservationValueID),
                    new StoredProcInParam("CustomerObservationEvalTestID", DbType.Int32, customerObservationEvalTestID),
                    new StoredProcInParam("AncestorID", DbType.Int32, ancestorID),
                    new StoredProcInParam("SpecialCategoryType", DbType.Int32, specialCategoryType),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy)))
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

        public int Update(int id, int customerID, int observationID, int kindOf, int basicType, int observationValueID, int extObservationValueID, int customerObservationEvalTestID, int ancestorID, int specialCategoryType, int status, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerObservationCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID),
                    new StoredProcInParam("KindOf", DbType.Int32, kindOf),
                    new StoredProcInParam("BasicType", DbType.Int32, basicType),
                    new StoredProcInParam("ObservationValueID", DbType.Int32, observationValueID),
                    new StoredProcInParam("ExtObservationValueID", DbType.Int32, extObservationValueID),
                    new StoredProcInParam("CustomerObservationEvalTestID", DbType.Int32, customerObservationEvalTestID),
                    new StoredProcInParam("AncestorID", DbType.Int32, ancestorID),
                    new StoredProcInParam("SpecialCategoryType", DbType.Int32, specialCategoryType),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy)
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerObservationDBTimeStampCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy)
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
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerObservationStatusCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public bool UpdateSpecialCategoryType(int id, BackOffice.Entities.SpecialCategoryTypeEnum specialCategoryType, string modifiedBy)
        {
            try
            {
                int affectedRows = this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateSpecialCategoryTypeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("SpecialCategoryType", DbType.Int32, (int)specialCategoryType),
                    new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
                return (affectedRows > 0);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        public int DeleteCustomerObservation(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.DeleteCustomerObservationCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetCustomerObservation(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public Int64 GetDBTimeStamp(int id)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationDBTimeStampCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetCustomerObservations()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationsCommand, Administrative.Entities.TableNames.CustomerObservationTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public List<string> GetValueAlergiasIndigo(int[] customerIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) return null;


                DataSet ds= this.Gateway.ExecuteQueryDataSet(SQLProvider.GetValueAlergiasIndigo, TableNames.ObservationOptionTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs));

                List<string> result = new List<string>();
                foreach (DataRow row in ds.Tables[TableNames.ObservationOptionTable].Rows)
                {
                    string rowToString = row[0] + "_" + row[1];
                    if(!result.Contains(rowToString))
                        result.Add(rowToString);                   
                }
                return result;               

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public List<string> HasAntecedentes(int[] customerIDs)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteQueryDataSet(SQLProvider.HasAntecedentes, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs));

                List<string> result = new List<string>();
                foreach (DataRow row in ds.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows)
                {
                    result.Add(row[0].ToString());
                }
                return result;  

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }


        public DataSet GetCustomerObservationsByCustomerTemplate(int customerTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationsByCustomerTemplateIDCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("CustomerTemplateID", DbType.Int32, customerTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerObservationsByCustomerBlock(int customerBlockID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationsByCustomerBlockIDCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("CustomerBlockID", DbType.Int32, customerBlockID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetCustomerObservationSpecialCategory(int customerID, int observationTemplateID, int observationID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerSpecialCategoryByCustomerIDCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("Confirmed", DbType.Int32, (int)SII.HCD.BackOffice.Entities.ObservationStatusEnum.Confirmed),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID),
                    new StoredProcInParam("AsAlert", DbType.Int32, (int)SpecialCategoryTypeEnum.AsAlert),
                    new StoredProcInParam("AsDataOfInterest", DbType.Int32, (int)SpecialCategoryTypeEnum.AsDataOfInterest),
                    new StoredProcInParam("AsRecommendation", DbType.Int32, (int)SpecialCategoryTypeEnum.AsRecommendation));

                if (result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows[0]["CustomerSpecialCategory"].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public bool ExistCustomerObservationBySpecialCategoryType(int customerID, BackOffice.Entities.SpecialCategoryTypeEnum specialCategoryType)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistCustomerObservationBySpecialCategoryTypeCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("Confirmed", DbType.Int32, (int)SII.HCD.BackOffice.Entities.ObservationStatusEnum.Confirmed),
                    new StoredProcInParam("SpecialCategoryType", DbType.Int32, (int)specialCategoryType)))
                {
                    return (IsEmptyReader(reader)) ? false : (SIIConvert.ToInteger(reader["Count"].ToString()) != 0) ? true : false;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return false;
            }
        }

        /// <summary>
        /// Obtiene el ID de un CustomerObservation a partir de la rutina y la observación
        /// </summary>
        /// <param name="routineActID"></param>
        /// <param name="observationID"></param>
        /// <returns></returns>
        public int GetCustomerObservationIDByRoutine(int routineActID, int observationID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationIDByRoutineObservationCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("RoutineActID", DbType.Int32, routineActID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID));
                if (result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows[0]["CustomerObservationID"].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        /// <summary>
        /// Obtiene el ID de un CustomerObservation a partir del procedimiento y la observación
        /// </summary>
        /// <param name="procedureActID"></param>
        /// <param name="observationID"></param>
        /// <returns></returns>
        public int GetCustomerObservationIDByProcedure(int procedureActID, int observationID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationIDByProcedureObservationCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID));
                if (result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows[0]["CustomerObservationID"].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        /// <summary>
        /// Obtiene el ID de un CustomerObservation a partir de la realización de la orden y la observación
        /// </summary>
        /// <param name="procedureActID"></param>
        /// <param name="observationID"></param>
        /// <returns></returns>
        public int GetCustomerObservationIDByCustomerOrderRealization(int customerOrderRealizationD, int observationID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationIDByCustomerOrderRealizationObservationCommand, Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInParam("CustomerOrderRealizationD", DbType.Int32, customerOrderRealizationD),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID));
                if (result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[Administrative.Entities.TableNames.CustomerObservationTable].Rows[0]["CustomerObservationID"].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        #endregion

        #region RegisteredObservationValues
        public DataSet GetRegisteredObservationValuesByCustomerObservation(int customerObservationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerObservationIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("CustomerObservationID", DbType.Int32, customerObservationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        /// <summary>
        /// TODOS ESTOS MÉDOTOS REQUIEREN REVISIÓN
        /// </summary>
        /// 

        public string GetRegisteredObservationValueBySimulator()
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetRegisteredObservationValueBySimulatorCommand
                    ))
                {
                    return (IsEmptyReader(reader)) ? String.Empty : (reader["Texto"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess))
                    throw;
                else
                    return null;
            }
        }


        ///Este metodo OK, revisado MT
        public DataSet GetRegisteredObservationValuesByCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public DataSet GetRegisteredObservationValuesToInterviewAndReport(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesToInterviewAndReportCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerInterviewID(int customerInterviewID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerInterviewIDCommand,
                    BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerInterviewID", DbType.Int32, customerInterviewID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerTemplateID(int registeredObservationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerTemplateIDCommand,
                    BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ID", DbType.Int32, registeredObservationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndObservationTemplateID(int customerID, int observationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndObservationTemplateIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomer(int customerID, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                if ((fromDate != null) && (toDate == null)) //TODAS LAS CUSTOMEROBSERVATIONS DESDE FROMDATE
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDFromDateCommand,
                        SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("FromDate", DbType.DateTime, fromDate.Value));
                else if ((fromDate == null) && (toDate != null)) //TODAS LAS CUSTOMEROBSERVATIONS HASTA TODATE (TODATE SIEMPRE SERÁ TODATE.ADDDAYS(1))                       
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDToDateCommand,
                        SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("ToDate", DbType.DateTime, toDate.Value.AddDays(1)));
                else //TODAS LAS CUSTOMEROBSERVATIONS DESDE FROMDATE HASTA TODATE (TODATE SIEMPRE SERÁ TODATE.ADDDAYS(1))
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDBetweenDatesCommand,
                        SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                        new StoredProcInParam("FromDate", DbType.DateTime, fromDate.Value),
                        new StoredProcInParam("ToDate", DbType.DateTime, toDate.Value.AddDays(1)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndRoutineAct(int customerID, int routineActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndRoutineActIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("RoutineActID", DbType.Int32, routineActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public DataSet GetRegisteredObservationValuesByCustomerAndProcedureAct(int customerID, int procedureActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndProcedureActIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndCustomerOrderRequest(int customerID, int customerOrderRequestID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndCustomerOrderRequestIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndProcedureAct(int customerID)
        {
            try
            {
                return null;
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndAnyProcedureActCommand, 
                //    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndCustomerMedEpisodeAct(int customerID, int customerMedEpisodeActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndCustomerMedEpisodeActIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndCustomerMedEpisodeAct(int customerID)
        {
            try
            {
                return null;
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndAnyCustomerMedEpisodeActCommand, 
                //    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndRequestOrder(int customerID, int customerOrderRequestID)
        {
            try
            {
                return null;
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerAndCustomerOrderRequestIDCommand,
                //    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndRequestOrder(int customerID)
        {
            try
            {
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerAndAnyRequestOrderCommand, 
                //    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndOrderRealization(int customerID, int customerOrderRealizationID)
        {
            try
            {
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerAndCustomerOrderRealizationIDCommand, 
                //    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //    new StoredProcInParam("CustomerOrderRealizationID", DbType.Int32, customerOrderRealizationID));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndOrderRealization(int customerID)
        {
            try
            {
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerAndAnyCustomerOrderRealizationCommand,
                //    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndObsTemplate(int customerID, int observationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerAndObsTemplateCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndObsBlock(int customerID, int observationBlockID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerAndObsBlockIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationBlockID", DbType.Int32, observationBlockID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndObsID(int customerID, int observationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerAndObsIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndSpecialCategory(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndSpecialCategoryCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("SpecialCategoryTypeNone", DbType.Int32, (int)SII.HCD.BackOffice.Entities.SpecialCategoryTypeEnum.None));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion


        #region RegisteredObservationBlocks

        public DataSet GetRegisteredObservationBlocksByCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public DataSet GetRegisteredObservationBlocksByCOT(int[] customerTemplateIDs)
        {
            try
            {
                if (customerTemplateIDs == null || customerTemplateIDs.Length <= 0) return null;
                string customerTemplatesID = StringUtils.BuildIDString(customerTemplateIDs);

                string sqlQuery = string.Concat(SQLProvider.GetRegisteredObservationBlocksByCOTCommand, Environment.NewLine,
                    "WHERE CTBR.CustomerTemplateID IN (", customerTemplatesID, ")");
                return this.Gateway.ExecuteQueryDataSet(sqlQuery, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByID(int customerBlockID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerBlockID", DbType.Int32, customerBlockID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerAndCustomerOrderRequest(int customerID, int customerOrderRequestID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndCustomerOrderRequestIDCommand,
                    BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerAndRoutineAct(int customerID, int routineActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndRoutineActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("RoutineActID", DbType.Int32, routineActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerAndProcedureAct(int customerID, int procedureActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndProcedureActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerAndCustomerMedEpisodeAct(int customerID, int customerMedEpisodeActID)
        {
            try
            {
                /// MT: REVISADA
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndCustomerMedEpisodeActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerAndObsTemplateID(int customerID, int observationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerAndObsTemplateIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerReports(int customerReportsID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerReportsIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerReportsID", DbType.Int32, customerReportsID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerInterview(int customerInterviewID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerInterviewIDCommand,
                    BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("CustomerInterviewID", DbType.Int32, customerInterviewID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion


        #region RegisteredObservationTemplates

        public DataSet GetRegisteredObservationTemplateByID(int registeredObservationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplateByIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("ID", DbType.Int32, registeredObservationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesByCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public DataSet GetRegisteredObservationTemplatesByCustomerEpisode(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                string obstemplatesID = StringUtils.BuildIDString(observationTemplateIDs);
                string sqlQuery = string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerEpisodeRAObsCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerEpisodeRABlockCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerEpisodePAObsCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerEpisodePABlockCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerMedEpisodeActObsCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerMedEpisodeActBlockCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);

                //PreAssessment
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerPreAssessmentNoteObsCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerPreAssessmentNoteBlockCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetConfirmedRoutineActRegisteredObservationTemplatesByCustomerEpisode(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                string obstemplatesID = StringUtils.BuildIDString(observationTemplateIDs);
                string sqlQuery = string.Concat(SQLProvider.GetConfirmedRegisteredObservationTemplatesByCustomerEpisodeRAObsCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetConfirmedRegisteredObservationTemplatesByCustomerEpisodeRABlockCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, ActionStatusEnum.Confirmed),
                    new StoredProcInParam("CompletedStatus", DbType.Int32, ActionStatusEnum.Completed));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerMedEpisodeActRegisteredObservationTemplatesByCustomerEpisodeIDs(int[] customerEpisodeIDs, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;

                return this.Gateway.ExecuteStoredProcedureDataSet("GetCustomerMedEpisodeActRegisteredObservationTemplates",
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs),
                    new StoredProcInTVPIntegerParam("TVPTableObsTemplate", observationTemplateIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerMedEpisodeActHasReportsByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;

                return this.Gateway.ExecuteStoredProcedureDataSet("GetCustomerMedEpisodeActHasReports",
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInTVPIntegerParam("TVPEpisodes", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetConfirmedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomerEpisode(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                string obstemplatesID = StringUtils.BuildIDString(observationTemplateIDs);
                string sqlQuery = string.Concat(SQLProvider.GetConfirmedRegisteredObservationTemplatesByCustomerMedEpisodeActObsCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetConfirmedRegisteredObservationTemplatesByCustomerMedEpisodeActBlockCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, ObservationStatusEnum.Confirmed));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesIDByCustomerEpisodeID(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                string obstemplatesID = StringUtils.BuildIDString(observationTemplateIDs);

                string sqlQuery = string.Concat(SQLProvider.GetRegisteredObservationTemplatesByCustomerEpisodeIDCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, ObservationStatusEnum.Confirmed));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public bool ExistCustomerObservationWithExtValueByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                if (customerEpisodeID <= 0) return false;

                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistCustomerObservationWithExtValueByCustomerEpisodeIDCommand,
                   new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    return (IsEmptyReader(reader)) ? false : (SIIConvert.ToInteger(reader["Count"].ToString()) != 0) ? true : false;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return false;
            }
        }

        public DataSet ExistCustomerObservationWithExtValueByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;                

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.ExistCustomerObservationWithExtValueByCustomerEpisodeIDsCommand,
                   new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetAmendedCustomerMedEpisodeActRegisteredObservationTemplatesByCustomerEpisode(int customerEpisodeID, int[] observationTemplateIDs)
        {
            try
            {
                if (customerEpisodeID <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0) return null;
                string obstemplatesID = StringUtils.BuildIDString(observationTemplateIDs);
                string sqlQuery = string.Concat(SQLProvider.GetConfirmedRegisteredObservationTemplatesByCustomerMedEpisodeActObsCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);
                sqlQuery += string.Concat("UNION", Environment.NewLine);
                sqlQuery += string.Concat(SQLProvider.GetConfirmedRegisteredObservationTemplatesByCustomerMedEpisodeActBlockCommand,
                    " AND CT.ObservationTemplateID IN (", obstemplatesID, ")", Environment.NewLine);

                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, ObservationStatusEnum.Amended));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplateByCustomerAndObservationTemplate(int customerID, int observationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndObservationTemplateIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        
        public DataSet GetRegisteredLayoutByCustomerAndCustomerOrderRequest(int customerID, int customerOrderRequestID)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("ObtenerRegisteredLayoutEntity_ByCustomerAndOrderRequest",
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended)                    
                    );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.RegisteredObservationTemplateTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.RegisteredObservationBlockTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.BlockLayoutLabelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.RegisteredObservationValueTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ObservationTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ObservationOptionTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ObservationValueTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ExtObservationValueTable;
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
        public DataSet GetRegisteredObservationTemplatesByCustomerAndCustomerOrderRequest(int customerID, int customerOrderRequestID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndCustomerOrderRequestIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesByCustomerAndRoutineAct(int customerID, int routineActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndRoutineActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("RoutineActID", DbType.Int32, routineActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesByCustomerAndProcedureAct(int customerID, int procedureActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndProcedureActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesByCustomerAndCustomerMedEpisodeAct(int customerID, int customerMedEpisodeActID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndCustomerMedEpisodeActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerMedEpisodeActID", DbType.Int32, customerMedEpisodeActID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesByCustomerAndObsTemplateID(int customerID, int observationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerAndObsTemplateIDCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesByCustomerReports(int customerReportsID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerReportsIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerReportsID", DbType.Int32, customerReportsID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationTemplatesByCustomerInterview(int customerInterviewID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerInterviewIDCommand,
                    BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("CustomerInterviewID", DbType.Int32, customerInterviewID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion


        /// <summary>
        /// TODO: MT ESTOS MÉTODOS TENGO QUE QUITARLOS. ES LA BL LA QUE SE ENCARGARÁ DE LOS FILTROS
        /// </summary>
        public DataSet GetAllRegisteredObservations(int customerID, BackOffice.Entities.ElementTypeEnum elementType, int entityActID, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DataSet myDS = null;
                //switch (elementType)
                //{
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.Routine:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndRoutineActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("RoutineActID", DbType.Int32, entityActID));
                //        break;
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.Procedure:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndProcedureActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("ProcedureActID", DbType.Int32, entityActID));
                //        break;
                //    //case SII.HCD.BackOffice.Entities.ElementTypeEnum.Protocol:
                //    //    myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndProtocolActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //    //        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //    //        new StoredProcInParam("ProtocolActID", DbType.Int32, entityActID));
                //    //    break;
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.MedEpisode:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndMedEpisodeActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("MedEpisodeActID", DbType.Int32, entityActID));
                //        break;
                //}
                return myDS;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        /// <summary>
        /// TODO: MT ESTOS MÉTODOS TENGO QUE QUITARLOS. ES LA BL LA QUE SE ENCARGARÁ DE LOS FILTROS
        /// </summary>
        public DataSet GetAllRegisteredBlocks(int customerID, BackOffice.Entities.ElementTypeEnum elementType, int entityActID, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DataSet myDS = null;
                //switch (elementType)
                //{
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.Routine:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndRoutineActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("RoutineActID", DbType.Int32, entityActID));
                //        break;
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.Procedure:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndProcedureActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("ProcedureActID", DbType.Int32, entityActID));
                //        break;
                //    //case SII.HCD.BackOffice.Entities.ElementTypeEnum.Protocol:
                //    //    myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndProtocolActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                //    //        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //    //        new StoredProcInParam("ProtocolActID", DbType.Int32, entityActID));
                //    //    break;
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.MedEpisode:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndMedEpisodeActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("MedEpisodeActID", DbType.Int32, entityActID));
                //        break;
                //}
                return myDS;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        /// <summary>
        /// TODO: MT ESTOS MÉTODOS TENGO QUE QUITARLOS. ES LA BL LA QUE SE ENCARGARÁ DE LOS FILTROS
        /// </summary>
        public DataSet GetAllRegisteredTemplates(int customerID, BackOffice.Entities.ElementTypeEnum elementType, int entityActID, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DataSet myDS = null;
                //switch (elementType)
                //{
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.Routine:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndRoutineActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("RoutineActID", DbType.Int32, entityActID));
                //        break;
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.Procedure:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndProcedureActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("ProcedureActID", DbType.Int32, entityActID));
                //        break;
                //    //case SII.HCD.BackOffice.Entities.ElementTypeEnum.Protocol:
                //    //    myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndProtocolActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                //    //        new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //    //        new StoredProcInParam("ProtocolActID", DbType.Int32, entityActID));
                //    //    break;
                //    case SII.HCD.BackOffice.Entities.ElementTypeEnum.MedEpisode:
                //        myDS = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndMedEpisodeActIDCommand, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                //            new StoredProcInParam("MedEpisodeActID", DbType.Int32, entityActID));
                //        break;
                //}
                return myDS;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public bool ExistsCustomerObservationNotCancelledByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerObservationNotCancelledByCustomerEpisodeIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("PendingStatus", DbType.Int32, (int)ObservationStatusEnum.Pending),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, (int)ObservationStatusEnum.Confirmed)))
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

        public bool ExistsCustomerObservationNotCancelledByCustomerAppointmentInformationID(int appointmentID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerObservationNotCancelledByCustomerAppointmentInformationIDCommand,
                    new StoredProcInParam("CustomerAppointmentInformationID", DbType.Int32, appointmentID),
                    new StoredProcInParam("PendingStatus", DbType.Int32, (int)ObservationStatusEnum.Pending),
                    new StoredProcInParam("ConfirmedStatus", DbType.Int32, (int)ObservationStatusEnum.Confirmed)))
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

        public bool ExistByObservationID(int observationID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerObservationsByObservationIDCommand, BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID)
                    );

                if (result.Tables[BackOffice.Entities.TableNames.RegisteredObservationValueTable].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return false;
            }
        }

        public DataSet GetSPCByCustomerIDs(int[] customerIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) return null;
                customerIDs = customerIDs.OrderBy(m => m).Distinct().ToArray();


                //String whereINByCustomerIDs =  string.concat("WHERE CO.CustomerID IN (",String.Join(",", Array.ConvertAll(customerIDs, new Converter<int, string>(m => m.ToString()))),")");
                //string finalQuery = String.Format(SQLProvider.GetSPCByCustomerIDsCommand, whereINByCustomerIDs);
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetSPCByCustomerIDsCommand,
                    //finalQuery, 
                    Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs)
                );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCOByCustomerIDs(IEnumerable<int> customerIDs, ObservationStatusEnum status)
        {
            try
            {
                if (customerIDs == null || customerIDs.Count() <= 0)
                {
                    return null;
                }
                customerIDs = customerIDs
                                .OrderBy(m => m)
                                .Distinct()
                                .ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCOByCustomerIDsCommand,
                    Administrative.Entities.TableNames.CustomerBasicCustomerObservationTable,
                    new StoredProcInParam("Status", DbType.Int32, (int)status),
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs)
                );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCOValueByCustomerIDsAndObservationTemplate(IEnumerable<int> customerIDs, int observationTemplateID, int observationID, ObservationStatusEnum status)
        {
            try
            {
                if (customerIDs == null || customerIDs.Count() <= 0)
                {
                    return null;
                }
                customerIDs = customerIDs
                                .OrderBy(m => m)
                                .Distinct()
                                .ToArray();

                DataSet ds1 = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCOValueByCustomerIDsAndObservationTemplateByBlockCommand,
                    Administrative.Entities.TableNames.CustomerBasicCustomerObservationValueTable,
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID),
                    new StoredProcInParam("Status", DbType.Int32, (int)status),
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs)
                    );

                DataSet ds2 = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCOValueByCustomerIDsAndObservationTemplateCommand,
                    Administrative.Entities.TableNames.CustomerBasicCustomerObservationValueTable,
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID),
                    new StoredProcInParam("Status", DbType.Int32, (int)status),
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs)
                    );

                DatasetUtils.MergeTable(ds1, ds2, Administrative.Entities.TableNames.CustomerBasicCustomerObservationValueTable);
                return ds2;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        #endregion

        #region Public read methods
        //public DataSet GetIDTest(int[] ids)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(
        //            "SELECT  CT.[ID], CT.[ObservationTemplateID], CT.[DocumentName], CT.[RegistrationDateTime]," + Environment.NewLine + 
        //            "		CT.[Status], CT.[ModifiedBy], CT.[LastUpdated]," + Environment.NewLine + 
        //            "		CAST(CT.[DBTimeStamp]  AS BIGINT) DBTimestamp" + Environment.NewLine + 
        //            "FROM [CustomerTemplate] CT" + Environment.NewLine + 
        //            "JOIN CustomerTemplateObsRel CTOR ON CTOR.CustomerTemplateID = CT.[ID]" + Environment.NewLine + 
        //            "JOIN CustomerObservation CO ON CTOR.[CustomerObservationID] = CO.[ID]" + Environment.NewLine + 
        //            "WHERE CO.[CustomerID] IN (SELECT [ID] FROM @IDTable)",
        //            SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
        //            new StoredProcInParam("IDTable", DbType., ids));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}
        #endregion

        #region publicmethods from careactivitylistview
        public DataSet GetRegisteredObservationTemplatesByCustomersAndTemplates(int[] customerIDs, int[] observationTemplateIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0 || observationTemplateIDs == null || observationTemplateIDs.Length <= 0)
                    return null;

                string whereINByCustomers = string.Join(",", Array.ConvertAll(customerIDs, new Converter<int, string>(m => m.ToString())));
                string whereINByTemplates = string.Join(",", Array.ConvertAll(observationTemplateIDs, new Converter<int, string>(m => m.ToString())));
                string finalQuery = string.Format(SQLProvider.GetRegisteredObservationTemplatesByCustomersAndTemplatesCommand, whereINByCustomers, whereINByTemplates,
                    whereINByCustomers, whereINByTemplates);

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomersAndBlocks(int[] customerIDs, int[] observationBlockIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0 || observationBlockIDs == null || observationBlockIDs.Length <= 0)
                    return null;

                string whereINByCustomers = string.Join(",", Array.ConvertAll(customerIDs, new Converter<int, string>(m => m.ToString())));
                string whereINByBlocks = string.Join(",", Array.ConvertAll(observationBlockIDs, new Converter<int, string>(m => m.ToString())));
                string finalQuery = string.Format(SQLProvider.GetRegisteredObservationBlocksByCustomersAndBlocksCommand, whereINByCustomers, whereINByBlocks,
                    whereINByCustomers, whereINByBlocks);

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomersAndObservations(int[] customerIDs, int[] observationIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0 || observationIDs == null || observationIDs.Length <= 0)
                    return null;

                string whereINByCustomers = string.Join(",", Array.ConvertAll(customerIDs, new Converter<int, string>(m => m.ToString())));
                string whereINByObsIDs = string.Join(",", Array.ConvertAll(observationIDs, new Converter<int, string>(m => m.ToString())));

                string firstQuery = string.Format(SQLProvider.GetRegisteredObservationValuesByCustomersAndObservationsRoutineCommand,
                    whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs);

                firstQuery = string.Concat(firstQuery, Environment.NewLine, "UNION", Environment.NewLine,
                    string.Format(SQLProvider.GetRegisteredObservationValuesByCustomersAndObservationsProcedureCommand,
                    whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs));

                //firstQuery = string.Concat(firstQuery, Environment.NewLine, SQLProvider.GetObservationValuesByCustomersAndObservationsProtocolCommand);

                firstQuery = string.Concat(firstQuery, Environment.NewLine, "UNION", Environment.NewLine,
                    string.Format(SQLProvider.GetRegisteredObservationValuesByCustomersAndObservationsOrderCommand,
                    whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs));

                firstQuery = string.Concat(firstQuery, Environment.NewLine, "UNION", Environment.NewLine,
                    string.Format(SQLProvider.GetRegisteredObservationValuesByCustomersAndObservationsOrderCMEACommand,
                    whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs));

                string finalQuery = string.Concat(firstQuery, Environment.NewLine, "UNION", Environment.NewLine,
                    string.Format(SQLProvider.GetRegisteredObservationValuesByCustomersAndObservationsCMEACommand,
                    whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs, whereINByCustomers, whereINByObsIDs));

                return this.Gateway.ExecuteQueryDataSet(finalQuery, BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInParam("BlockLabelPosition", DbType.Int32, (int)LabelPositionEnum.Left),
                    new StoredProcInParam("AdmEpisodeType", DbType.Int32, (int)EpisodeTypeEnum.Admission),
                    new StoredProcInParam("MedEpisodeType", DbType.Int32, (int)EpisodeTypeEnum.Medical),
                    new StoredProcInParam("RoutineEntity", DbType.Int32, (int)ElementTypeEnum.Routine),
                    new StoredProcInParam("ProcedureEntity", DbType.Int32, (int)ElementTypeEnum.Procedure),
                    new StoredProcInParam("MedEpisodeEntity", DbType.Int32, (int)ElementTypeEnum.MedEpisode),
                    new StoredProcInParam("OrderRealizationEntity", DbType.Int32, (int)ElementTypeEnum.OrderRealization));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetObservationValuesByCustomersAndObservations(int[] customerIDs, int[] observationIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0 || observationIDs == null || observationIDs.Length <= 0)
                    return null;

                string whereINByCustomers = string.Join(",", Array.ConvertAll(customerIDs, new Converter<int, string>(m => m.ToString())));
                string whereINByObsIDs = string.Join(",", Array.ConvertAll(observationIDs, new Converter<int, string>(m => m.ToString())));
                string finalQuery = string.Format(SQLProvider.GetObservationValuesByCustomersAndObservationsCommand, whereINByCustomers, whereINByObsIDs);

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    BackOffice.Entities.TableNames.ObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetExtObservationValuesByCustomersAndObservations(int[] customerIDs, int[] observationIDs)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0 || observationIDs == null || observationIDs.Length <= 0) return null;
                string whereINByCustomers = string.Join(",", Array.ConvertAll(customerIDs, new Converter<int, string>(m => m.ToString())));
                string whereINByObsIDs = string.Join(",", Array.ConvertAll(observationIDs, new Converter<int, string>(m => m.ToString())));
                string finalQuery = string.Format(SQLProvider.GetExtObservationValuesByCustomersAndObservationsCommand, whereINByCustomers, whereINByObsIDs);
                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public DataSet GetExtObservationValuesByCustomerEpisodesAndObservations(int[] customerEpisodeIDs, string[] observationsToFind)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0 || observationsToFind == null || observationsToFind.Length <= 0)
                    return null;
                string whereINByCustomerEpisodes = string.Join(",", Array.ConvertAll(customerEpisodeIDs, new Converter<int, string>(m => m.ToString())));
                string whereINByObs = StringUtils.BuildCodeString(observationsToFind);
                string finalQuery = string.Format(SQLProvider.GetExtObservationValuesByCustomerEpisodesAndObservationsCommand, whereINByCustomerEpisodes, whereINByObs);
                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    SII.HCD.BackOffice.Entities.TableNames.ExtObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }



        public DataSet GetLastCustomerObservations(int[] customerEpisodeIDs, string[] observationsToFind)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0 || observationsToFind == null || observationsToFind.Length <= 0)
                    return null;
                string whereINByCustomers = StringUtils.BuildIDString(customerEpisodeIDs);
                string whereINByObs = StringUtils.BuildCodeString(observationsToFind);
                /*string finalQuery = string.Concat(SQLProvider.GetCustomerObservationByEpisodesAndObservationsCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON CO.CustomerID = CE.CustomerID ", Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereINByCustomers, ") ", Environment.NewLine,
                    "AND O.[AssignedCode] IN (", whereINByObs, ") ");*/

                string finalQuery = string.Concat(SQLProvider.GetCustomerObservationByEpisodesAndObservationsCommand, Environment.NewLine,
                    "WHERE RA.EpisodeID IN (", whereINByCustomers, ") AND O.[AssignedCode] IN (", whereINByObs, ") ");

                return this.Gateway.ExecuteQueryDataSet(finalQuery, Administrative.Entities.TableNames.CustomerObservationTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetLastCustomerObservationsByCustomerID(int customerID, string[] observationsToFind)
        {
            try
            {
                if (customerID <= 0 || observationsToFind == null || observationsToFind.Length <= 0)
                    return null;
                string whereINByObs = StringUtils.BuildCodeString(observationsToFind);
                string finalQuery = string.Concat(SQLProvider.GetCustomerObservationByEpisodesAndObservationsCommand, Environment.NewLine,
                    "WHERE CO.CustomerID = ", customerID.ToString(), Environment.NewLine,
                    "AND O.[AssignedCode] IN (", whereINByObs, ") ");
                return this.Gateway.ExecuteQueryDataSet(finalQuery, Administrative.Entities.TableNames.CustomerObservationTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }




        public DataSet GetObservationValuesByCustomerEpisodesAndObservations(int[] customerEpisodeIDs, string[] observationsToFind)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0 || observationsToFind == null || observationsToFind.Length <= 0)
                    return null;
                string whereINByCustomers = StringUtils.BuildIDString(customerEpisodeIDs);
                string whereINByObs = StringUtils.BuildCodeString(observationsToFind);
                /*string finalQuery = string.Concat(SQLProvider.GetObservationValuesByEpisodesAndObservationsCommand, Environment.NewLine,
                    "JOIN CustomerEpisode CE ON CO.CustomerID = CE.CustomerID ", Environment.NewLine,
                    "WHERE CE.[ID] IN (", whereINByCustomers, ") ", Environment.NewLine,
                    "AND O.[AssignedCode] IN (", whereINByObs, ") ");*/

                string finalQuery = string.Concat(SQLProvider.GetObservationValuesByEpisodesAndObservationsCommand, Environment.NewLine,
                    "LEFT OUTER JOIN RoutineActObsRel RAOR ON CO.[ID] = RAOR.[CustomerObservationID] ", Environment.NewLine,
                    "LEFT OUTER JOIN RoutineAct RA ON RAOR.[RoutineActID] = RA.[ID] AND CO.CustomerID = RA.CustomerID ", Environment.NewLine,
                    "WHERE RA.EpisodeID IN (", whereINByCustomers, ") AND O.[AssignedCode] IN (", whereINByObs, ") ");

                return this.Gateway.ExecuteQueryDataSet(finalQuery, BackOffice.Entities.TableNames.ObservationValueTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetObservationValuesByCustomersAndObservationsByCustomerID(int customerID, string[] observationsToFind)
        {
            try
            {
                if (customerID <= 0 || observationsToFind == null || observationsToFind.Length <= 0)
                    return null;
                string whereINByObs = StringUtils.BuildCodeString(observationsToFind);
                string finalQuery = string.Concat(SQLProvider.GetObservationValuesByEpisodesAndObservationsCommand, Environment.NewLine,
                    "WHERE CO.CustomerID = ", customerID.ToString(), Environment.NewLine,
                    "AND O.[AssignedCode] IN (", whereINByObs, ") ");
                return this.Gateway.ExecuteQueryDataSet(finalQuery, BackOffice.Entities.TableNames.ObservationValueTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        #endregion



        #region procedureActIDs TVP
        public DataSet GetRegisteredObservationTemplatesByCustomerAndProcedureActIDs(int[] procedureActIDs)
        {
            try
            {
                if (procedureActIDs == null || procedureActIDs.Length <= 0) return null;
                procedureActIDs = procedureActIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();


                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndProcedureActIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInTVPIntegerParam("TVPTable", procedureActIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerAndProcedureActIDs(int[] procedureActIDs)
        {
            try
            {
                if (procedureActIDs == null || procedureActIDs.Length <= 0) return null;
                procedureActIDs = procedureActIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndProcedureActIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInTVPIntegerParam("TVPTable", procedureActIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndProcedureActIDs(int[] procedureActIDs)
        {
            try
            {
                if (procedureActIDs == null || procedureActIDs.Length <= 0) return null;
                procedureActIDs = procedureActIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndProcedureActIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", procedureActIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region routineActIDs TVP
        public DataSet GetRegisteredObservationTemplatesByCustomerAndRoutineActIDs(int[] routineActIDs)
        {
            try
            {
                if (routineActIDs == null || routineActIDs.Length <= 0) return null;
                routineActIDs = routineActIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();


                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerIDAndRoutineActIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInTVPIntegerParam("TVPTable", routineActIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerAndRoutineActIDs(int[] routineActIDs)
        {
            try
            {
                if (routineActIDs == null || routineActIDs.Length <= 0) return null;
                routineActIDs = routineActIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerIDAndRoutineActIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInTVPIntegerParam("TVPTable", routineActIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerAndRoutineActIDs(int[] routineActIDs)
        {
            try
            {
                if (routineActIDs == null || routineActIDs.Length <= 0) return null;
                routineActIDs = routineActIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerIDAndRoutineActIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", routineActIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region medicalEpisodeIDs TVP
        public DataSet GetRegisteredObservationTemplatesByMedicalEpisodeIDs(int[] medicalEpisodeIDs)
        {
            try
            {
                if (medicalEpisodeIDs == null || medicalEpisodeIDs.Length <= 0) return null;
                medicalEpisodeIDs = medicalEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();


                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByMedicalEpisodeIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInTVPIntegerParam("TVPTable", medicalEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByMedicalEpisodeIDs(int[] medicalEpisodeIDs)
        {
            try
            {
                if (medicalEpisodeIDs == null || medicalEpisodeIDs.Length <= 0) return null;
                medicalEpisodeIDs = medicalEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByMedicalEpisodeIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInTVPIntegerParam("TVPTable", medicalEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByMedicalEpisodeIDs(int[] medicalEpisodeIDs)
        {
            try
            {
                if (medicalEpisodeIDs == null || medicalEpisodeIDs.Length <= 0) return null;
                medicalEpisodeIDs = medicalEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByMedicalEpisodeIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", medicalEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region customerEpisodeIDs TVP
        public DataSet GetRegisteredObservationTemplatesByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();


                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerEpisodeIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerEpisodeIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerEpisodeIDs(int[] customerEpisodeIDs)
        {
            try
            {
                if (customerEpisodeIDs == null || customerEpisodeIDs.Length <= 0) return null;
                customerEpisodeIDs = customerEpisodeIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                DataSet ds = new DataSet();
                DatasetUtils.MergeTable(this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerEpisodeIDs1Command,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs)),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
                DatasetUtils.MergeTable(this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerEpisodeIDs2Command,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs)),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);

                DatasetUtils.MergeTable(this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerEpisodeIDs3Command,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", customerEpisodeIDs)),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
                return ds;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region customerProcessIDs TVP
        public DataSet GetRegisteredObservationTemplatesByCustomerProcessIDs(int[] customerProcessIDs)
        {
            try
            {
                if (customerProcessIDs == null || customerProcessIDs.Length <= 0) return null;
                customerProcessIDs = customerProcessIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();


                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationTemplatesByCustomerProcessIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationTemplateTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerProcessIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationBlocksByCustomerProcessIDs(int[] customerProcessIDs)
        {
            try
            {
                if (customerProcessIDs == null || customerProcessIDs.Length <= 0) return null;
                customerProcessIDs = customerProcessIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationBlocksByCustomerProcessIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationBlockTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerProcessIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetRegisteredObservationValuesByCustomerProcessIDs(int[] customerProcessIDs)
        {
            try
            {
                if (customerProcessIDs == null || customerProcessIDs.Length <= 0) return null;
                customerProcessIDs = customerProcessIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();
                DataSet ds = new DataSet();
                DatasetUtils.MergeTable(this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerProcessIDs1Command,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", customerProcessIDs)),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
                DatasetUtils.MergeTable(this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRegisteredObservationValuesByCustomerProcessIDs2Command,
                    SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable,
                    new StoredProcInParam("ObservationStatusAmended", DbType.Int32, (int)ObservationStatusEnum.Amended),
                    new StoredProcInParam("ObservationStatusCancelled", DbType.Int32, (int)ObservationStatusEnum.Cancelled),
                    new StoredProcInTVPIntegerParam("TVPTable", customerProcessIDs)),
                    ds, SII.HCD.BackOffice.Entities.TableNames.RegisteredObservationValueTable);
                return ds;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion





        public DataSet GetCompositeByObservationIDsAndCustomerProcessIDs(int[] customerProcessIDs, int[] obsIDs)
        {
            try
            {
                if (customerProcessIDs == null || customerProcessIDs.Length <= 0) return null;
                customerProcessIDs = customerProcessIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();
                if (obsIDs == null || obsIDs.Length <= 0) return null;
                obsIDs = obsIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCompositeByObservationIDsAndCustomerProcessIDsCommand,
                    SII.HCD.BackOffice.Entities.TableNames.ObservationValueTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerProcessIDs),
                    new StoredProcInTVPIntegerParam("TVPObsTable", obsIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int[] GetCareprocessIDsWithdoctorNotes(int[] customerAssistanceProcessIDs)
        {
            try
            {
                if (customerAssistanceProcessIDs == null || customerAssistanceProcessIDs.Length <= 0) return null;

                customerAssistanceProcessIDs = customerAssistanceProcessIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();
                //OBTENGO LOS customerAssistanceProcess
                DataSet ds = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCareprocessIDsWithdoctorNotesCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerAssistanceProcessIDs));

                if (ds != null && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable)
                        && ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    return ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable].AsEnumerable()
                        .Where(row => (row["ID"] as int? ?? 0) > 0)
                        .Select(row => (row["ID"] as int? ?? 0))
                        .Distinct()
                        .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int[] GetCareprocessIDsWithAlergias(int[] customerAssistanceProcessIDs)
        {
            try
            {
                if (customerAssistanceProcessIDs == null || customerAssistanceProcessIDs.Length <= 0) return null;

                customerAssistanceProcessIDs = customerAssistanceProcessIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();
                //OBTENGO LOS customerAssistanceProcess
                DataSet ds = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCareprocessIDsWithAlergiasCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerAssistanceProcessIDs));

                if (ds != null && ds.Tables.Contains(SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable)
                        && ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable].Rows.Count > 0)
                {
                    return ds.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerObservationTable].AsEnumerable()
                        .Where(row => (row["ID"] as int? ?? 0) > 0)
                        .Select(row => (row["ID"] as int? ?? 0))
                        .Distinct()
                        .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }




    }
}
