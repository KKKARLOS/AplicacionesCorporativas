using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.Administrative.Entities;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.Administrative.DA
{
    public class CustomerContactPersonDA : DAServiceBase
    {
        #region Field length constants
        public const int ModifiedByLength = 256;
        #endregion

        public CustomerContactPersonDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerContactPersonDA(Gateway gateway) : base(gateway) { }

        public int Insert(int personID, int customerID, int contactTypeID, bool urgentContact, bool alternativeContact, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerContactPersonCommand,
                    new StoredProcInParam("PersonID", DbType.Int32, personID),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ContactTypeID", DbType.Int32, contactTypeID),
                    new StoredProcInParam("UrgentContact", DbType.Boolean, urgentContact),
                    new StoredProcInParam("AlternativeContact", DbType.Boolean, alternativeContact),
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

        public int Update(int id, int customerID, int contactTypeID, bool urgentContact, bool alternativeContact, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerContactPersonCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID),
                    new StoredProcInParam("ContactTypeID", DbType.Int32, contactTypeID),
                    new StoredProcInParam("UrgentContact", DbType.Boolean, urgentContact),
                    new StoredProcInParam("AlternativeContact", DbType.Boolean, alternativeContact),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.DeleteCustomerContactPersonCommand,
                    new StoredProcInParam("ID", DbType.Int32, id));
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
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactPersonDBTimeStampCommand, TableNames.CustomerContactPersonTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[TableNames.CustomerContactPersonTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[TableNames.CustomerContactPersonTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetCustomerContactPersons(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactPersonsCommand, TableNames.CustomerContactPersonListDTOTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //public DataSet GetCustomerContactPerson(int id)
        //{
        //    try
        //    {
        //        return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactPersonCommand, TableNames.CustomerContactPersonTable,
        //            new StoredProcInParam("ID", DbType.Int32, id));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        else return null;
        //    }
        //}
        public DataSet GetCustomerContactPerson(int id)
        {
            try
            {
                SqlParameter[] aParam = new SqlParameter[]{
                        ParametroSql.add("@ID", SqlDbType.Int, 4, id)
                    };
                DataSet ds = SqlHelper.ExecuteDataset("Obtener_CustomerContactPerson_Entity", aParam);

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerContactPersonTable;
                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.ContactTypeTable;
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
        public int GetCustomerContactPersonsFromPerson(int personID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactPersonsFromPersonCommand, TableNames.CustomerContactPersonTable,
                    new StoredProcInParam("PersonID", DbType.Int32, personID));
                if (result.Tables[TableNames.CustomerContactPersonTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.CustomerContactPersonTable].Rows[0]["Count"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetIDDescriptionCCPByCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionCCPByCustomerCommand, CommonEntities.TableNames.IDDescriptionTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
    }
}