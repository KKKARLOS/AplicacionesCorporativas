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
using SII.HCD.Administrative.Entities;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.DA
{
    public class CustomerDA : DAServiceBase
    {
        #region Field length constants
        public const int IdentificationNumberLength = 50;
        public const int ShortIDNumberLength = 15;
        public const int CHNumberLength = 50;
        public const int DeathReasonLength = 200;
        public const int ModifiedByLength = 256;
        #endregion

        #region public methods
        public CustomerDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerDA(Gateway gateway) : base(gateway) { }

        public int Insert(int personID, bool poorlyIdentified, string identificationNumber, string shortIDNumber, string chNumber, int currentAdmissionID, int currentEpisodeID, int profileID,
            int customerClassificationID, int organizationID, int customerNameConfidentiality,
            int customerIdentifierConfidentiality, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerCommand,
                    new StoredProcInParam("PersonID", DbType.Int32, personID),
                    new StoredProcInParam("PoorlyIdentified", DbType.Boolean, poorlyIdentified),
                    new StoredProcInParam("IdentificationNumber", DbType.String, SIIStrings.Left(identificationNumber, IdentificationNumberLength)),
                    new StoredProcInParam("ShortIDNumber", DbType.String, SIIStrings.Left(shortIDNumber, ShortIDNumberLength)),
                    new StoredProcInParam("CHNumber", DbType.String, SIIStrings.Left(chNumber, CHNumberLength)),
                    new StoredProcInParam("CurrentAdmissionID", DbType.Int32, currentAdmissionID),
                    new StoredProcInParam("CurrentEpisodeID", DbType.Int32, currentEpisodeID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID),
                    new StoredProcInParam("CustomerClassificationID", DbType.Int32, customerClassificationID),
                    new StoredProcInParam("OrganizationID", DbType.Int32, organizationID),
                    new StoredProcInParam("CustomerNameConfidentiality", DbType.Int32, customerNameConfidentiality),
                    new StoredProcInParam("CustomerIdentifierConfidentiality", DbType.Int32, customerIdentifierConfidentiality),
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

        public int Update(int id, bool poorlyIdentified, string identificationNumber, string shortIDNumber, string chNumber, int profileID,
            int customerClassificationID, int organizationID, int customerNameConfidentiality,
            int customerIdentifierConfidentiality, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("PoorlyIdentified", DbType.Boolean, poorlyIdentified),
                    new StoredProcInParam("IdentificationNumber", DbType.String, SIIStrings.Left(identificationNumber, IdentificationNumberLength)),
                    new StoredProcInParam("ShortIDNumber", DbType.String, SIIStrings.Left(shortIDNumber, ShortIDNumberLength)),
                    new StoredProcInParam("CHNumber", DbType.String, SIIStrings.Left(chNumber, CHNumberLength)),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID),
                    new StoredProcInParam("CustomerClassificationID", DbType.Int32, customerClassificationID),
                    new StoredProcInParam("OrganizationID", DbType.Int32, organizationID),
                    new StoredProcInParam("CustomerNameConfidentiality", DbType.Int32, customerNameConfidentiality),
                    new StoredProcInParam("CustomerIdentifierConfidentiality", DbType.Int32, customerIdentifierConfidentiality),
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerStampCommand,
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

        public int UpdateCurrentAdmisionEpisode(int id, int admissionID, int episodeID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerCurrentAdmissionEpisodeCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CurrentAdmissionID", DbType.Int32, admissionID),
                    new StoredProcInParam("CurrentEpisodeID", DbType.Int32, episodeID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
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
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerDBTimeStampCommand, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        //public DataSet GetCustomerListDTO(int customerID, string identifierTypeName, int observationTemplateID)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerListDTOByIDCommand, SII.HCD.Administrative.Entities.TableNames.CustomerListDTOTable,
        //            new StoredProcInParam("CustomerID", DbType.Int32, customerID),
        //            new StoredProcInParam("Name", DbType.String, identifierTypeName),
        //            new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public DataSet GetCustomers(DateTime fromDate, DateTime toDate, string identifierTypeName, int observationTemplateID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersCommand, SII.HCD.Administrative.Entities.TableNames.CustomerListDTOTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("Name", DbType.String, identifierTypeName),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //public DataSet GetCustomers(DateTime fromDate, DateTime toDate, string identifierTypeName, int maxRecords, int observationTemplateID)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersTopNCommand, SII.HCD.Administrative.Entities.TableNames.CustomerListDTOTable,
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value),
        //            new StoredProcInParam("Name", DbType.String, identifierTypeName),
        //            new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
        //            new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        //public DataSet GetCustomersWithoutProcessActive(DateTime fromDate, DateTime toDate, string identifierTypeName, int maxRecords, int observationTemplateID)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersTopNWithoutProcessActiveCommand,
        //            SII.HCD.Administrative.Entities.TableNames.CustomerListDTOTable,
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value),
        //            new StoredProcInParam("Name", DbType.String, identifierTypeName),
        //            new StoredProcInParam("StatusActive", DbType.Int32, (int)CommonEntities.StatusEnum.Active),
        //            new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
        //            new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        //public DataSet GetCustomersByProcessChart(DateTime fromDate, DateTime toDate, string identifierTypeName, int processChartID, int status, int maxRecords, int observationTemplateID)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersTopNByProcessChartCommand, SII.HCD.Administrative.Entities.TableNames.CustomerListDTOTable,
        //            new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
        //            new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value),
        //            new StoredProcInParam("Name", DbType.String, identifierTypeName),
        //            new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
        //            new StoredProcInParam("Status", DbType.Int32, status),
        //            new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
        //            new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        //public DataSet GetCustomersByActiveProcess(string identifierTypeName, int maxRecords, int observationTemplateID)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersTopNByActiveProcessCommand, SII.HCD.Administrative.Entities.TableNames.CustomerListDTOTable,
        //            new StoredProcInParam("Name", DbType.String, identifierTypeName),
        //            new StoredProcInParam("Status", DbType.Int32, (int)SII.HCD.Common.Entities.StatusEnum.Active),
        //            new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
        //            new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}

        public DataSet GetActiveCustomerIDByDevice(int deviceID, DateTime dateTime)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                    SQLProvider.GetActiveCustomerIDByDeviceCommand, SII.HCD.Administrative.Entities.TableNames.CustomerListDTOTable,
                    new StoredProcInParam("DeviceID", DbType.Int32, deviceID),
                    new StoredProcInParam("DateTime", DbType.DateTime, dateTime));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCommand, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInParam("ID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        /*
        public DataSet GetCustomer(int customerID, bool getNOKs, bool getCCPs, bool getCCOs)
        {
            try
            {
                SqlParameter[] aParam = new SqlParameter[]{
                        ParametroSql.add("@ID", SqlDbType.Int, 4, customerID),
                        ParametroSql.add("@getNOKs", SqlDbType.Bit, 1, getNOKs),
                        ParametroSql.add("@getCCPs", SqlDbType.Bit, 1, getCCPs),
                        ParametroSql.add("@getCCOs", SqlDbType.Bit, 1, getCCOs)
                    };

                DataSet ds = SqlHelper.ExecuteDataset("ObtenerCustomerEntity", aParam);

                int i = 0;

                ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ProfileTable;
                ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerAdmissionTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.LocationTable;
                ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerClassificationTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.OrganizationTable;
                ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRelatedCHNumberTable;

                if (getNOKs) 
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.NOKListDTOTable;
                if (getCCPs) 
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerContactPersonListDTOTable;
                if (getCCOs) 
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerContactOrganizationListDTOTable;

                return ds;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
   */
        public DataSet GetCustomer(int customerID, bool getNOKs, bool getCCPs, bool getCCOs)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("ObtenerCustomerEntity",
                        new StoredProcInParam("ID", DbType.Int32, customerID),
                        new StoredProcInParam("getNOKs", DbType.Boolean, getNOKs),
                        new StoredProcInParam("getCCPs", DbType.Boolean, getCCPs),
                        new StoredProcInParam("getCCOs", DbType.Boolean, getCCOs)
                    );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ProfileTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerAdmissionTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.LocationTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerClassificationTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.OrganizationTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRelatedCHNumberTable;

                    if (getNOKs)
                        ds.Tables[i++].TableName = Administrative.Entities.TableNames.NOKListDTOTable;
                    if (getCCPs)
                        ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerContactPersonListDTOTable;
                    if (getCCOs)
                        ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerContactOrganizationListDTOTable;

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
        public DataSet GetCustomersByIDs(int[] ids)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersByIDsCommand, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInTVPIntegerParam("TVPTable", ids));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public bool Exists(int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerByIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, customerID)))
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

        public int GetPersonID(int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerPersonIDCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["PersonID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public DataSet GetPersonIDCustAccountIDCustEpisodeID(int customerID, int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonIDCustAccountIDCustEpisodeIDCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetCustomerIDByPersonID(int personID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerIDByPersonIDCommand,
                    new StoredProcInParam("personID", DbType.Int32, personID)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }
        
        public DataSet GetCustomerByPersonID_old(int personID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerByPersonIDCommand, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInParam("PersonID", DbType.Int32, personID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
 /*      
        public DataSet GetCustomerByPersonID(int personID)
        {
            try
            {
                SqlParameter[] aParam = new SqlParameter[]{
                        ParametroSql.add("@PersonID", SqlDbType.Int, 4, personID)
                    };
                DataSet ds = SqlHelper.ExecuteDataset("Obtener_Customer_Entity", aParam);

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ProfileTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerAdmissionTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.LocationTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerClassificationTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRelatedCHNumberTable;

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
         */
        public DataSet GetCustomerByPersonID(int personID)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("Obtener_Customer_Entity",
                    new StoredProcInParam("PersonID", DbType.Int32, personID)
                    );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ProfileTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerAdmissionTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.LocationTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerClassificationTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerRelatedCHNumberTable;

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
        public DataSet GetCustomerBaseEpisodeWithChargesDTOByEpisodeID(int episodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerBaseEpisodeWithChargesDTOByEpisodeIDCommand,
                    Administrative.Entities.TableNames.CustomerBaseEpisodeWithChargesDTOTable,
                    new StoredProcInParam("EpisodeID", DbType.Int32, episodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerBaseEpisodeWithChargesDTOByStringListCustomerEpisodesID(string customerEpisodeIDs)
        {
            try
            {
                string sqlquery = string.Concat(SQLProvider.SelectCustomerBaseEpisodeWithChargesDTOFROMCustomerEpisodeCommand, Environment.NewLine,
                    "WHERE (CE.[ID] IN (" + customerEpisodeIDs, "))");

                return this.Gateway.ExecuteQueryDataSet(sqlquery, Administrative.Entities.TableNames.CustomerBaseEpisodeWithChargesDTOTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int GetCustomerByOrganizationID(int organizationID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerByOrganizationIDCommand,
                    new StoredProcInParam("OrganizationID", DbType.Int32, organizationID)
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

        public int FindIdentificationNumber(string identificationNumber)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.FindIdentificationNumberCommand, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInParam("IdentificationNumber", DbType.String, identificationNumber));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["ID"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int FindCHNumber(string chNumber)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.FindCHNumberCommand, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInParam("CHNumber", DbType.String, chNumber));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows[0]["ID"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDFromIdentificationNumber(string identificationNumber, string identifierType)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerIDFromIdentificationNumberCommand,
                    new StoredProcInParam("IDNumber", DbType.String, identificationNumber),
                    new StoredProcInParam("IdentifierType", DbType.String, identifierType)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["CustomerID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public DataSet GetCustomerTelephones(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerTelephonesCommand,
                    BackOffice.Entities.TableNames.TelephoneTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerNOKTelephones(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerNOKTelephonesCommand,
                    BackOffice.Entities.TableNames.TelephoneTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerDevices(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerDevicesCommand,
                    BackOffice.Entities.TableNames.DeviceTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("DateTime", DbType.DateTime, DateTime.Now));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int SetCustomerOrganization(int customerID, int organizationID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.SetCustomerOrganizationCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("OrganizationID", DbType.Int32, organizationID),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public void UpdateCustomerProfileAndClassification(int customerID, int profileID, int classificationID, int admissionID, int episodeID)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerProfileAndClassificationCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID),
                    new StoredProcInParam("ClassificationID", DbType.Int32, classificationID),
                    new StoredProcInParam("AdmissionID", DbType.Int32, admissionID),
                    new StoredProcInParam("EpisodeID", DbType.Int32, episodeID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
            }
        }

        public string GetCustomerName(int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerNameByIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, customerID)))
                {
                    return (IsEmptyReader(reader)) ? String.Empty : reader["CustomerName"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return String.Empty;
            }
        }

        public DataSet GetCustomerNameData(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerNameDataByIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInParam("ID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public string GetCustomerCH(int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerCHByIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, customerID)))
                {
                    return (IsEmptyReader(reader)) ? String.Empty : reader["CHNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return String.Empty;
            }
        }

        public int GetCurrentEpisodeID(int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCurrentEpisodeIDByCustomerIDCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["CurrentEpisodeID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetCurrentLocationID(int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCurrentLocationIDByCustomerIDCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["CurrentLocationID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public decimal GetCustomerDebtQty(int customerID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerDebtQtyByCustomerIDCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("InsurerGuar", DbType.Int16, (int)GuarantorTypeEnum.InsurerAsGuarantor),
                    new StoredProcInParam("InvoiceType", DbType.Int16, (int)InvoiceTypeEnum.Normal),
                    new StoredProcInParam("InvoiceClosed", DbType.Int16, (int)SII.HCD.BackOffice.Entities.InvoiceStatusEnum.Closed),
                    new StoredProcInParam("InvoicePaid", DbType.Int16, (int)PaymentStatusEnum.Paid)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToDecimal(reader["DebtQty"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetCustomersByDeliveryNoteStatus(int customerID, int customerDeliveryNoteStatus, int deliveryNoteStatus, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersDeliveryNoteByCDNStatusAndDNStatusCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("DeliveryNoteStatus", DbType.Int32, deliveryNoteStatus),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersByDeliveryNoteStatus(int customerID, int customerDeliveryNoteStatus, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersDeliveryNoteByCDNStatusCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersByDeliveryNoteStatusProcessChartID(int processChartID, int deliveryNoteStatus, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerBaseDTOByDeliveryNoteStatusProcessChartIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("DeliveryNoteStatus", DbType.Int32, deliveryNoteStatus),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersByProcessChartIDNotExported(int processChartID, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerBaseDTOByProcessChartIDExportedCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("Exported", DbType.Boolean, false),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersOfDeliveryNoteByCustomerEpisodeIDs(int[] customerEpisodeIDs, int coverElementID)
        {
            try
            {
                string sqlquery = string.Concat(SQLProvider.SelectCustomerFromDeliveryNoteCommand,
                    "AND (CDN.CustomerEpisodeID IN (", StringUtils.BuildIDString(customerEpisodeIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlquery,
                    Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersByDeliveryNoteStatusByCustomerEpisodeID(int customerEpisodeID, int customerDeliveryNoteStatus, int deliveryNoteStatus, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersDeliveryNoteByCustomerEpisodeIDCDNStatusAndDNStatusCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("DeliveryNoteStatus", DbType.Int32, deliveryNoteStatus),
                    new StoredProcInParam("EpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersByDeliveryNoteStatusByCustomerEpisodeID(int customerEpisodeID, int customerDeliveryNoteStatus, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersDeliveryNoteByCustomerEpisodeIDCDNStatusCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CustomerDeliveryNoteStatus", DbType.Int32, customerDeliveryNoteStatus),
                    new StoredProcInParam("EpisodeID", DbType.Int32, customerEpisodeID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersByDeliveryNoteID(int deliveryNoteID, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerByDeliveryNoteIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("DeliveryNoteID", DbType.Int32, deliveryNoteID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomersByInvoiceID(int invoiceID, int coverElementID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomersByInvoiceIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("InvoiceID", DbType.Int32, invoiceID),
                    new StoredProcInParam("CoverElementID", DbType.Int32, coverElementID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerBaseDTOByID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerBaseDTOByIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerBaseDTOByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerBaseDTOByCustomerEpisodeIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetCustomerIDByCustomerEpisodeID(int customerEpisodeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerBaseDTOByCustomerEpisodeIDCommand,
                    new StoredProcInParam("CustomerEpisodeID", DbType.Int32, customerEpisodeID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public DataSet GetCustomerBaseDTOWithIdentifierByID(int customerID, string identifierType)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerBaseDTOWithIdentifierByIDCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("IdentifierType", DbType.String, identifierType));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerBaseDTOByDemographics(int identifierTypeID, string identifierNumber, string firstName, string lastName, string lastName2)
        {
            try
            {
                StringBuilder commandText = new StringBuilder(SQLProvider.GetActiveCustomerBaseDTOIDCommand + Environment.NewLine);
                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("ActiveStatus", DbType.Int32, CommonEntities.StatusEnum.Active));

                if (!string.IsNullOrWhiteSpace(identifierNumber))
                {
                    commandText.AppendLine("AND PIR.IdentifierTypeID = @IdentifierTypeID AND PIR.IDNumber LIKE @IdentifierNumber");

                    parameters.Add(new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID));
                    parameters.Add(new StoredProcInParam("IdentifierNumber", DbType.String, identifierNumber + "%"));
                }

                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    commandText.AppendLine("AND P.FirstName LIKE @FirstName");

                    parameters.Add(new StoredProcInParam("FirstName", DbType.String, firstName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    commandText.AppendLine("AND P.LastName LIKE @LastName");

                    parameters.Add(new StoredProcInParam("LastName", DbType.String, lastName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(lastName2))
                {
                    commandText.AppendLine("AND P.LastName2 LIKE @LastName2");

                    parameters.Add(new StoredProcInParam("LastName2", DbType.String, lastName2 + "%"));
                }

                return this.Gateway.ExecuteQueryDataSet(commandText.ToString(),
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerBaseDTOWithCurrentMedEpisodeActByDemographics(
            int identifierTypeID, string identifierNumber, string firstName, string lastName, string lastName2)
        {
            try
            {
                StringBuilder commandText = new StringBuilder(SQLProvider.GetActiveCustomerBaseDTOWithCurrentMedEpisodeCommand + Environment.NewLine);
                List<StoredProcParam> parameters = new List<StoredProcParam>();
                parameters.Add(new StoredProcInParam("ActiveStatus", DbType.Int32, CommonEntities.StatusEnum.Active));

                if (!string.IsNullOrWhiteSpace(identifierNumber))
                {
                    commandText.AppendLine("AND PIR.IdentifierTypeID = @IdentifierTypeID AND PIR.IDNumber LIKE @IdentifierNumber");

                    parameters.Add(new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID));
                    parameters.Add(new StoredProcInParam("IdentifierNumber", DbType.String, identifierNumber + "%"));
                }

                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    commandText.AppendLine("AND P.FirstName LIKE @FirstName");

                    parameters.Add(new StoredProcInParam("FirstName", DbType.String, firstName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    commandText.AppendLine("AND P.LastName LIKE @LastName");

                    parameters.Add(new StoredProcInParam("LastName", DbType.String, lastName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(lastName2))
                {
                    commandText.AppendLine("AND P.LastName2 LIKE @LastName2");

                    parameters.Add(new StoredProcInParam("LastName2", DbType.String, lastName2 + "%"));
                }

                return this.Gateway.ExecuteQueryDataSet(commandText.ToString(),
                    SII.HCD.Administrative.Entities.TableNames.CustomerBaseDTOTable,
                    parameters.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionAllCustomers()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionAllCustomerCommand,
                    Common.Entities.TableNames.IDDescriptionTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionByCustomerCommand,
                    Common.Entities.TableNames.IDDescriptionTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public string GetRelationship(int customerID, int personID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetRelationshipByCustomerIDCommand,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("PersonID", DbType.Int32, personID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? string.Empty : reader["Relationship"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return string.Empty;
            }
        }

        /// <summary>
        /// Amplicaciones para dar solución al laboratorio
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public int GetCustomerByCHNumber(string chNumber)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerByCHNumberCommand,
                    new StoredProcInParam("CHNumber", DbType.String, chNumber)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public int GetCustomerByCHNumber(string chNumber, int careCenterID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerByCHNumberAndCareCenterCommand,
                    new StoredProcInParam("CHNumber", DbType.String, chNumber),
                    new StoredProcInParam("CareCenterID", DbType.String, careCenterID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }


        public int GetCustomerIdentifierNumber(string identificationNumber)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerByIdentificationNumberCommand,
                    new StoredProcInParam("IdentificationNumber", DbType.String, identificationNumber)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }


        public int GetCustomerByPersonData(string firstName, string lastName, string idNumber, int identifierTypeID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerByPersonDataCommand,
                    new StoredProcInParam("FirstName", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public DataSet GetIDDescriptionCustomers(string firstName, string lastName, string lastName2, string identifier, int defaultIdentifierID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionCustomersCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("FirstName", DbType.String, (!String.IsNullOrEmpty(firstName)) ? firstName : string.Empty),
                            new StoredProcInParam("LastName", DbType.String, (!String.IsNullOrEmpty(lastName)) ? lastName : string.Empty),
                            new StoredProcInParam("LastName2", DbType.String, (!String.IsNullOrEmpty(lastName2)) ? lastName2 : string.Empty),
                            new StoredProcInParam("Identifier", DbType.String, (!String.IsNullOrEmpty(identifier)) ? identifier : string.Empty),
                            new StoredProcInParam("DefaultIdentifierID", DbType.Int32, defaultIdentifierID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetCustomerIDByDeviceCode(string deviceCode)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetCustomerIDByDeviceCodeCommand,
                    new StoredProcInParam("DeviceCode", DbType.String, deviceCode)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["CustomerID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public int GetNumberOfCustomers(DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(
                    SQLProvider.GetNumberOfCustomersCommand,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["Total"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public bool ExistsByProfileID(int profileID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.ExistsCustomerByProfileIDCommand,
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID)))
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

        public DataSet GetDuplicatedCustomers(int duplicateGroupID, string defaultIdentifierName)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetDuplicatedCustomersCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerMergedDTOTable,
                    new StoredProcInParam("DuplicateGroupID", DbType.Int32, duplicateGroupID),
                    new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetDuplicatedCustomers(int[] personIDs, string defaultIdentifierName)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetDuplicatedCustomersByPersonIDsCommand,
                    SII.HCD.Administrative.Entities.TableNames.CustomerMergedDTOTable,
                    new StoredProcInTVPIntegerParam("TVPTable", personIDs),
                    new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerDataByInvoiceIDs(int[] invoiceIDs, bool byCareCenter)
        {
            try
            {
                if (byCareCenter)
                {
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCareCenterDataByInvoiceIDsCommand,
                        SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                        new StoredProcInTVPIntegerParam("TVPTable", invoiceIDs));
                }
                else
                {
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerDataByInvoiceIDsCommand,
                        SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                        new StoredProcInTVPIntegerParam("TVPTable", invoiceIDs));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region new public methods

        #region private methods
        private string GetWheresCustomerFind(CustomerFilterSpecification filters)
        {
            if (filters == null || filters.FindOptions == 0) return string.Empty;
            string wheres = "WHERE ";
            bool activateAnd = false;
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.FindMode) && filters.FindMode < 0)
            {
                if (filters.FindMode == -2)
                    wheres = string.Concat(wheres, " NOT(");
                wheres = string.Concat(wheres, " EXISTS(SELECT CP1.[ID] FROM CustomerProcess CP1  WITH(NOLOCK) WHERE CP1.CustomerID = C.[ID]) ");
                if (filters.FindMode == -2)
                    wheres = string.Concat(wheres, " )");
                wheres = string.Concat(wheres, Environment.NewLine);
                activateAnd = true;
            }
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.ProcessChartID))
            {
                wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, " CP.ProcessChartID = ", filters.ProcessChartID.ToString(), Environment.NewLine);
                activateAnd = true;
            }
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.CareCenterID) && filters.IsFilteredByAny(CustomerFindOptionEnum.ProcessChartID))
            {
                wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, " CP.CareCenterID = ", filters.CareCenterID.ToString(), Environment.NewLine);
                activateAnd = true;
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.FromDate))
            {
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.ProcessChartID))
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "( @FromDate IS NULL OR CP.CloseDateTime IS NULL OR CP.CloseDateTime >= @FromDate) ", Environment.NewLine);
                else
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, " ( @FromDate IS NULL OR P.RegistrationDate >= @FromDate) ", Environment.NewLine);
                activateAnd = true;
            }
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.ToDate))
            {
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.ProcessChartID))
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "( @ToDate IS NULL OR CP.CloseDateTime IS NULL OR CP.CloseDateTime < @ToDate) ", Environment.NewLine);
                else
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, " ( @ToDate IS NULL OR P.RegistrationDate < @ToDate) ", Environment.NewLine);
                activateAnd = true;
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByNameParts))
            {
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.FirstName))
                {
                    wheres = String.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(PPI.FirstName LIKE @FirstName) ", Environment.NewLine);
                    activateAnd = true;
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName))
                {
                    wheres = String.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(PPI.LastName LIKE @LastName) ", Environment.NewLine);
                    activateAnd = true;
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName2))
                {
                    wheres = String.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(PPI.LastName2 LIKE @LastName2) ", Environment.NewLine);
                    activateAnd = true;
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.FirstName | CustomerFindOptionEnum.LastName | CustomerFindOptionEnum.LastName2))
                {
                    wheres = String.Concat(wheres, " AND (PPI.AddinName=@AddinName) ");
                    activateAnd = true;
                }
            }
            else if (filters.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByFullName))
            {
                wheres = String.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(PPI.FullName LIKE @FullName) AND (PPI.AddinName=@AddinName) ", Environment.NewLine);
                activateAnd = true;
            }
            else
            {
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.FirstName))
                {
                    wheres = String.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(P.FirstName LIKE @FirstName) ", Environment.NewLine);
                    activateAnd = true;
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName))
                {
                    wheres = String.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(P.LastName LIKE @LastName) ", Environment.NewLine);
                    activateAnd = true;
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName2))
                {
                    wheres = String.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(P.LastName2 LIKE @LastName2) ", Environment.NewLine);
                    activateAnd = true;
                }
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.IdentifierType))
            {
                wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(PIR.IdentifierTypeID=@IdentifierTypeID) ", Environment.NewLine);
                activateAnd = true;
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.IdentifierNumber))
            {
                if (filters.MatchingMode == MatchingModeEnum.Like)
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(PIR.IDNumber LIKE @IdentifierNumber) ", Environment.NewLine);
                else
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(PIR.IDNumber=@IdentifierNumber) ", Environment.NewLine);
                activateAnd = true;
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumber) && filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter) &&
                filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberCareCenterID))
            {
                wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(CRCN.CHNumber LIKE @CHNumber) AND (CRCN.CareCenterID=@CHNumberCareCenterID) ", Environment.NewLine);
                activateAnd = true;
            }
            else if ((filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberCareCenterID)) && (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter)))
            {
                wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(CRCN.CareCenterID=@CHNumberCareCenterID) ", Environment.NewLine);
                activateAnd = true;
            }
            else if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumber))
            {
                wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(C.CHNumber LIKE @CHNumber) ", Environment.NewLine);
                activateAnd = true;
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.Deceased))
            {
                if (filters.Deceased)
                {
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(NOT SS.DeathDateTime IS NULL) ", Environment.NewLine);
                    activateAnd = true;
                }
                else
                {
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(SS.DeathDateTime IS NULL) ", Environment.NewLine);
                    activateAnd = true;
                }
            }
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PoolyIdentified))
            {
                wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(C.PoorlyIdentified=@PoorlyIdentified) ", Environment.NewLine);
                activateAnd = true;
            }
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.HasDuplicate))
            {
                if (filters.HasDuplicate)
                {
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(P.DuplicateGroupID > 0) ", Environment.NewLine);
                }
                else
                {
                    wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(P.DuplicateGroupID = 0) ", Environment.NewLine);
                }
                activateAnd = true;
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.Status))
            {
                if (filters.Status != null)
                {
                    if (filters.Status.Length == 1)
                    {
                        int myStatus = (int)filters.Status[0];
                        wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(P.[Status]=", myStatus.ToString(), ") ", Environment.NewLine);
                    }
                    else
                    {
                        int[] myStatusList = filters.Status.Select(st => (int)st).ToArray();
                        wheres = string.Concat(wheres, (activateAnd) ? "AND " : string.Empty, "(P.[Status] IN (", StringUtils.BuildIDString(myStatusList), ")) ", Environment.NewLine);
                    }
                    activateAnd = true;
                }
            }

            return wheres;
        }

        private StoredProcParam[] GetParamsCustomerFind(CustomerFilterSpecification filters,
            int maxRows, string defaultIdentifierName, string phoneticAddinName)
        {
            List<StoredProcParam> result = new List<StoredProcParam>();
            result.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRows));
            result.Add(new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName));
            //result.Add(new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
            //result.Add(new StoredProcInParam("ObservationID", DbType.Int32, observationID));
            result.Add(new StoredProcInParam("FromDate", DbType.DateTime, (filters.FromDate != null) ? (object)filters.FromDate.Value : (object)DBNull.Value));
            result.Add(new StoredProcInParam("ToDate", DbType.DateTime, (filters.ToDate != null) ? (object)filters.ToDate.Value : (object)DBNull.Value));


            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByNameParts))
            {
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.FirstName))
                {
                    result.Add(new StoredProcInParam("FirstName", DbType.String, string.Concat(filters.FirstName, "%")));
                    if (!result.Exists(p => p.Name == "AddinName"))
                        result.Add(new StoredProcInParam("AddinName", DbType.String, phoneticAddinName));
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName))
                {
                    result.Add(new StoredProcInParam("LastName", DbType.String, string.Concat(filters.LastName, "%")));
                    if (!result.Exists(p => p.Name == "AddinName"))
                        result.Add(new StoredProcInParam("AddinName", DbType.String, phoneticAddinName));
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName2))
                {
                    result.Add(new StoredProcInParam("LastName2", DbType.String, string.Concat(filters.LastName2, "%")));
                    if (!result.Exists(p => p.Name == "AddinName"))
                        result.Add(new StoredProcInParam("AddinName", DbType.String, phoneticAddinName));
                }
            }
            else if (filters.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByFullName))
            {
                result.Add(new StoredProcInParam("FullName", DbType.String, string.Concat(filters.PhoneticLookupFullName, "%")));
                if (!result.Exists(p => p.Name == "AddinName"))
                    result.Add(new StoredProcInParam("AddinName", DbType.String, phoneticAddinName));
            }
            else
            {
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.FirstName))
                    result.Add(new StoredProcInParam("FirstName", DbType.String, string.Concat(filters.FirstName, "%")));

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName))
                    result.Add(new StoredProcInParam("LastName", DbType.String, string.Concat(filters.LastName, "%")));

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.LastName2))
                    result.Add(new StoredProcInParam("LastName2", DbType.String, string.Concat(filters.LastName2, "%")));
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.IdentifierType))
                result.Add(new StoredProcInParam("IdentifierTypeID", DbType.Int32, filters.IdentifierTypeID));

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.IdentifierNumber))
                if (filters.MatchingMode == MatchingModeEnum.Exact)
                    result.Add(new StoredProcInParam("IdentifierNumber", DbType.String, filters.Identifier));
                else
                    result.Add(new StoredProcInParam("IdentifierNumber", DbType.String, filters.Identifier + "%"));

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumber))
            {
                result.Add(new StoredProcInParam("CHNumber", DbType.String, filters.CHNumber + "%"));
            }
            if ((filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberCareCenterID)) && (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter)))
            {
                result.Add(new StoredProcInParam("CHNumberCareCenterID", DbType.Int32, filters.CHNumberCareCenterID));
            }
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PoolyIdentified))
            {
                result.Add(new StoredProcInParam("PoorlyIdentified", DbType.Boolean, filters.PoorlyIdentified));
            }
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PersonIDs))
            {
                result.Add(new StoredProcInTVPIntegerParam("TVPTable", filters.PersonIDs));
            }

            return result.ToArray();
        }

        private string GetJoinsCustomerFind(CustomerFilterSpecification filters)
        {
            if (filters == null || filters.FindOptions == 0) return string.Empty;
            string joins = string.Empty;
            if (filters.IsFilteredByAny(CustomerFindOptionEnum.ProcessChartID | CustomerFindOptionEnum.CareCenterID))
                joins = string.Concat(joins, Environment.NewLine, "JOIN CustomerProcess CP WITH(NOLOCK) ON C.[ID] = CP.CustomerID ");

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.IdentifierType | CustomerFindOptionEnum.IdentifierNumber))
                joins = String.Concat(joins, Environment.NewLine, "JOIN PersonIdentifierRel PIR WITH(NOLOCK) ON PIR.PersonID=P.[ID] ");

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PhoneticLookupByNameParts | CustomerFindOptionEnum.PhoneticLookupByFullName))
                joins = String.Concat(joins, Environment.NewLine, "JOIN PersonPhoneticInfo PPI WITH(NOLOCK) ON PPI.PersonID=P.[ID] ");

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumber | CustomerFindOptionEnum.CHNumberCareCenterID) && filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter))
            {
                joins = String.Concat(joins, Environment.NewLine, "JOIN CustomerRelatedCHNumber CRCN ON C.[ID]=CRCN.CustomerID ");
            }

            if (filters.IsFilteredByAny(CustomerFindOptionEnum.PersonIDs))
            {
                joins = String.Concat(joins, Environment.NewLine, "JOIN @TVPTable TVP ON TVP.[ID]=P.[ID] ");
            }

            return joins;
        }
        #endregion

        public DataSet GetCustomerBasic(int[] processChartIDs, BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
            int[] locations, int[] careCenterIDs, int assistanceServiceID, DateTime? startDateTime, DateTime? endDateTime,
            string defaultIdentifierName, int observationTemplateID, int observationID)
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
                string finalQuery = string.Format(SQLProvider.GetCustomerBasicCommand);

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
                        includes = string.Concat(includes, Environment.NewLine, "JOIN ProcessChart PC WITH(NOLOCK) ON CP.ProcessChartID = PC.[ID] ");

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
                            wheres = string.Concat(wheres, Environment.NewLine,
                                (andPossible) ? " AND " : string.Empty,
                                " (CP.CloseDateTime IS NULL OR CP.CloseDateTime >= @StartDateTime) ");
                            andPossible = true;
                            ////////////////////////////////////////////////////////////////////////////
                            ///
                            ///FALTA LA INCLUSIÓN DE LA COLUMNA EndDateTime en la tabla CustomerProcessStepsRel. Esto tengo que explicarselo a JL cuando hagamos las UofW de CustomerProcess
                            ///
                            ////////////////////////////////////////////////////////////////////////////
                            //wheres = string.Concat(wheres, Environment.NewLine,
                            //    (andPossible) ? " AND " : string.Empty,
                            //    " (CPSR.StepDateTime IS NULL OR CPSR.StepDateTime >= @StartDateTime) ");
                            //andPossible = true;
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
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime),
                    new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName),
                    new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID),
                    new StoredProcInParam("ObservationID", DbType.Int32, observationID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }

        }

        //Optimizacion de censos
        //public DataSet GetCustomerBasicByQueryCustomerProcessID(string queryFilterCustomerProcessMaxRows, DateTime? startDateTime, DateTime? endDateTime,
        //    string defaultIdentifierName, int observationTemplateID, int observationID)
        //{
        //    try
        //    {
        //        string finalQuery = string.Format(SQLProvider.GetCustomerBasicWithQueryCustomerProcessFilterMaxRowCommand, queryFilterCustomerProcessMaxRows);

        //        return this.Gateway.ExecuteQueryDataSet(finalQuery, Administrative.Entities.TableNames.CustomerBasicTable,
        //            new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
        //            new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime),
        //            new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName),
        //            new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID),
        //            new StoredProcInParam("ObservationID", DbType.Int32, observationID)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }

        //}

        //public DataSet GetCustomerBasicByQueryCustomerProcessID(IEnumerable<int> ceIDs,
        //                                                        string defaultIdentifierName,
        //                                                        int observationTemplateID,
        //                                                        int observationID)
        public DataSet GetCustomerBasicByQueryCustomerProcessID(IEnumerable<int> ceIDs, string defaultIdentifierName)
        {
            try
            {
                List<StoredProcParam> myParams = new List<StoredProcParam>();
                myParams.Add(new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName));
                //myParams.Add(new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
                //myParams.Add(new StoredProcInParam("ObservationID", DbType.Int32, observationID));
                myParams.Add(new StoredProcInTVPIntegerParam("TVPTable", ceIDs));

                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerBasicWithQueryCustomerProcessFilterMaxRowCommand,
                    Administrative.Entities.TableNames.CustomerBasicTable,
                    myParams.ToArray()
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerBasic(int customerID, int customerProcessID, string defaultIdentifierName)
        {
            try
            {
                string finalQuery = string.Format(SQLProvider.GetCustomerBasicCommand);
                finalQuery = string.Concat(finalQuery, Environment.NewLine, "WHERE C.[ID] = ", customerID.ToString());
                finalQuery = string.Concat(finalQuery, Environment.NewLine, " AND CP.[ID] = ", customerProcessID.ToString());
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable,
                    new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName)
                    //new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID),
                    //new StoredProcInParam("ObservationID", DbType.Int32, observationID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }

        }

        public DataSet GetCustomerBasic(int[] customerIDs, int[] customerProcessIDs, string defaultIdentifierName)
        {
            try
            {
                if (customerIDs == null || customerIDs.Length <= 0) return null;
                customerIDs = customerIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();

                string finalQuery = string.Concat(SQLProvider.GetCustomerBasicCommand,
                 Environment.NewLine, "JOIN @TVPTable TVP ON C.[ID]=TVP.[ID] ");

                if (customerProcessIDs != null && customerProcessIDs.Length > 0)
                {
                    customerProcessIDs = customerProcessIDs.Where(id => id > 0).OrderBy(id => id).Distinct().ToArray();
                    finalQuery = string.Concat(finalQuery, Environment.NewLine, "JOIN @TVPTable2 TVP2 ON CP.[ID]=TVP2.[ID] ");
                }
                else
                {
                    customerProcessIDs = new int[] { 0 };
                }
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable,
                    new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName),
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs),
                    new StoredProcInTVPIntegerParam("TVPTable2", customerProcessIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }

        }




        public DataSet GetCustomerBasic(int customerID, string defaultIdentifierName)
        {
            try
            {
                string finalQuery = SQLProvider.GetCustomerBasicCommand;
                finalQuery = string.Concat(finalQuery, Environment.NewLine, "WHERE C.[ID] = ", customerID.ToString());
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable,
                    new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName)
                    //new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID),
                    //new StoredProcInParam("ObservationID", DbType.Int32, observationID)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }

        }

        public DataSet GetCustomerBasicList(CustomerFilterSpecification filters, int maxRows, string defaultIdentifierName, string phoneticAddinName = "")
        {
            try
            {
                string strCHNumber = "C.CHNumber";
                if (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter))
                {
                    strCHNumber = "'' CHNumber";
                }
                if ((filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumber | CustomerFindOptionEnum.CHNumberCareCenterID)) && (filters.IsFilteredByAny(CustomerFindOptionEnum.CHNumberInCareCenter)))
                {
                    strCHNumber = "CRCN.CHNumber";
                }

                if (filters.IsFilteredByAny(CustomerFindOptionEnum.CareCenterID))
                    strCHNumber = string.Concat("(CASE WHEN EXISTS(SELECT CR.[ID] FROM CustomerRelatedCHNumber CR WITH(NOLOCK) WHERE CR.CustomerID=C.[ID] AND CR.CareCenterID=CP.CareCenterID) ", Environment.NewLine,
                       " THEN (SELECT TOP 1 CR.CHNumber FROM CustomerRelatedCHNumber CR WITH(NOLOCK) WHERE CR.CustomerID=C.[ID] AND CR.CareCenterID=CP.CareCenterID) ELSE C.CHNumber END) CHNumber");

                string finalQuery = string.Format(SQLProvider.GetOnlyCustomerBasicCommand, strCHNumber);

                string joints = GetJoinsCustomerFind(filters);
                string wheres = GetWheresCustomerFind(filters);
                StoredProcParam[] parameters = GetParamsCustomerFind(filters, maxRows, defaultIdentifierName, phoneticAddinName);
                finalQuery = string.Concat(finalQuery, joints, Environment.NewLine, wheres);
                return this.Gateway.ExecuteQueryDataSet(
                    finalQuery, SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable, parameters);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerBasic(int customerID, int careCenterID, bool chNumberInCareCenter, string defaultIdentifierName)
        {
            try
            {
                string strCHNumber = "C.CHNumber";

                if (chNumberInCareCenter)
                {
                    strCHNumber = string.Concat("(CASE WHEN EXISTS(SELECT CR.[ID] FROM CustomerRelatedCHNumber CR WITH(NOLOCK) WHERE CR.CustomerID=C.[ID] AND CR.CareCenterID=@CareCenterID) ", Environment.NewLine,
                       " THEN (SELECT TOP 1 CR.CHNumber FROM CustomerRelatedCHNumber CR WITH(NOLOCK) WHERE CR.CustomerID=C.[ID] AND CR.CareCenterID=@CareCenterID) ELSE C.CHNumber END) CHNumber");
                }

                string finalQuery = string.Concat(string.Format(SQLProvider.GetOnlyCustomerBasicCommand, strCHNumber), Environment.NewLine,
                    "WHERE (C.[ID]=@CustomerID)");

                List<StoredProcParam> result = new List<StoredProcParam>();
                result.Add(new StoredProcInParam("MaxRecords", DbType.Int32, 1));
                result.Add(new StoredProcInParam("CustomerID", DbType.Int32, customerID));
                result.Add(new StoredProcInParam("DefaultIdentifierName", DbType.String, defaultIdentifierName));
                //result.Add(new StoredProcInParam("ObservationTemplateID", DbType.Int32, observationTemplateID));
                //result.Add(new StoredProcInParam("ObservationID", DbType.Int32, observationID));
                if (chNumberInCareCenter)
                {
                    result.Add(new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID));
                }

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                    SII.HCD.Administrative.Entities.TableNames.CustomerBasicTable,
                    result.ToArray()
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCHNumbersOfPersons(int[] mergedPersonIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCHNumbersOfPersons, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInTVPIntegerParam("TVPTable", mergedPersonIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCustomerCHNumbersOfPersonsByCareCenter(int[] mergedPersonIDs, int careCenterID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerCHNumbersOfPersonsByCareCenter, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInTVPIntegerParam("TVPTable", mergedPersonIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        #endregion

        //public bool MergeCustomerInfo(int newCustomerID, int oldCustomerID, int newPersonID, int oldPersonID)
        //{
        //    try
        //    {
        //        using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.MergeCustomersCommand,
        //            new StoredProcInParam("NewCustomerID", DbType.Int32, newCustomerID),
        //            new StoredProcInParam("OldCustomerID", DbType.Int32, oldCustomerID),
        //            new StoredProcInParam("NewPersonID", DbType.Int32, newPersonID),
        //            new StoredProcInParam("OldPersonID", DbType.Int32, oldPersonID)
        //            ))
        //        {
        //            return (IsEmptyReader(reader)) ? false : true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return false;
        //    }

        //}

        public int GetCustomerIDByCHNumberAndCCID(string chnumber, int careCenterID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerIDByCHNumberAndCCIDCommand,
                    new StoredProcInParam("CHNumber", DbType.String, chnumber),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID)
                    ))
                {
                    return (IsEmptyReader(reader)) ? -1 : reader["ID"] as int? ?? -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }
        public int GetCustomerIDByCHNumber(string chnumber)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetCustomerIDByCHNumberCommand,
                    new StoredProcInParam("CHNumber", DbType.String, chnumber)
                    ))
                {
                    return (IsEmptyReader(reader)) ? -1 : reader["ID"] as int? ?? -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }


        public Tuple<int, int>[] GetFromDuplicatesCustomerByTableName(int[] oldcustomerIDs, string tableName)
        {
            try
            {
                if (oldcustomerIDs == null || oldcustomerIDs.Length <= 0 || string.IsNullOrEmpty(tableName)) return null;
                string sql = string.Concat(
                    "SELECT DISTINCT TVP.[ID], R.[ID] ResultID ", Environment.NewLine,
                    "FROM ", tableName, " R WITH(NOLOCK) ", Environment.NewLine,
                    "JOIN @TVPTable TVP ON R.CustomerID=TVP.[ID]");

                DataSet result = this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                                        new StoredProcInTVPIntegerParam("TVPTable", oldcustomerIDs));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].AsEnumerable()
                            .Select(r => new Tuple<int, int>(r["ID"] as int? ?? 0, r["ResultID"] as int? ?? 0))
                            .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Tuple<int, int>[] GetFromDuplicatesCustomerOfCustomerOrderRequest(int[] oldcustomerIDs)
        {
            try
            {
                //aqui buscamos todos los CustomerOrderRequest que no están en ningún episodio
                if (oldcustomerIDs == null || oldcustomerIDs.Length <= 0) return null;
                string sql = string.Concat(
                    "SELECT DISTINCT TVP.[ID], R.[ID] ResultID ", Environment.NewLine,
                    "FROM CustomerOrderRequest R WITH(NOLOCK) ", Environment.NewLine,
                    "JOIN @TVPTable TVP ON R.CustomerID=TVP.[ID]", Environment.NewLine,
                    "WHERE R.CustomerEpisodeID <=0 AND R.CustomerMedEpisodeActID <=0");

                DataSet result = this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                                        new StoredProcInTVPIntegerParam("TVPTable", oldcustomerIDs));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].AsEnumerable()
                            .Select(r => new Tuple<int, int>(r["ID"] as int? ?? 0, r["ResultID"] as int? ?? 0))
                            .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Tuple<int, int>[] GetFromDuplicatesCustomerOfCustomerObservations(int[] oldcustomerIDs)
        {
            try
            {
                //aqui buscamos todos los CustomerObservations que no están en ningún episodio, ni en nigún otro sitio
                if (oldcustomerIDs == null || oldcustomerIDs.Length <= 0) return null;
                string sql = string.Concat(
                    "SELECT DISTINCT TVP.[ID], R.[ID] ResultID ", Environment.NewLine,
                    "FROM CustomerObservation R WITH(NOLOCK) ", Environment.NewLine,
                    "JOIN @TVPTable TVP ON R.CustomerID=TVP.[ID]", Environment.NewLine,
                    "WHERE ", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM CustomerMedEpisodeActObsRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM CustomerObservationNotificationRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM CustomerOrderRealizationObsNotificationRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM NotificationAct WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM OrderRealizationObsRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM OrderRequestCustomerObservationRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM ProcedureActObsNotificationRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM ProcedureActObsRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM ProtocolActObsNotificationRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM ProtocolActObsRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM RoutineActObsNotificationRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID])) AND", Environment.NewLine,
                    "   NOT(EXISTS(SELECT [ID] FROM RoutineActObsRel WITH(NOLOCK) WHERE CustomerObservationID = R.[ID]))"
                    );

                DataSet result = this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                                        new StoredProcInTVPIntegerParam("TVPTable", oldcustomerIDs));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].AsEnumerable()
                            .Select(r => new Tuple<int, int>(r["ID"] as int? ?? 0, r["ResultID"] as int? ?? 0))
                            .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Tuple<int, int>[] GetFromDuplicatesCustomerOfCustomerCards(int[] oldcustomerIDs)
        {
            try
            {
                //aqui buscamos todos los CustomerCard que no están en ninguna Póliza

                if (oldcustomerIDs == null || oldcustomerIDs.Length <= 0) return null;
                string sql = string.Concat(
                    "SELECT DISTINCT TVP.[ID], R.[ID] ResultID ", Environment.NewLine,
                    "FROM CustomerCard R WITH(NOLOCK)", Environment.NewLine,
                    "JOIN @TVPTable TVP ON R.CustomerID=TVP.[ID]", Environment.NewLine,
                    "WHERE R.CustomerPolicyID <=0"
                    );

                DataSet result = this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                                        new StoredProcInTVPIntegerParam("TVPTable", oldcustomerIDs));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].AsEnumerable()
                            .Select(r => new Tuple<int, int>(r["ID"] as int? ?? 0, r["ResultID"] as int? ?? 0))
                            .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Tuple<int, int>[] GetFromDuplicatesCustomerOfCustomerAccountCharges(int[] oldcustomerIDs)
        {
            try
            {
                //aqui buscamos todos los CustomerAccountCharges que no están en ningún episodio

                if (oldcustomerIDs == null || oldcustomerIDs.Length <= 0) return null;
                string sql = string.Concat(
                    "SELECT DISTINCT TVP.[ID], R.[ID] ResultID ", Environment.NewLine,
                    "FROM CustomerAccountCharge R WITH(NOLOCK)", Environment.NewLine,
                    "JOIN @TVPTable TVP ON R.CustomerID=TVP.[ID]", Environment.NewLine,
                    "WHERE R.SourceEpisodeID <=0"
                    );

                DataSet result = this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                                        new StoredProcInTVPIntegerParam("TVPTable", oldcustomerIDs));
                if (result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].Rows.Count > 0)
                {
                    return result.Tables[SII.HCD.Administrative.Entities.TableNames.CustomerTable].AsEnumerable()
                            .Select(r => new Tuple<int, int>(r["ID"] as int? ?? 0, r["ResultID"] as int? ?? 0))
                            .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetCustomerNameByIDs(int[] customerIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerNameByIDs, SII.HCD.Administrative.Entities.TableNames.CustomerTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

    }
}
