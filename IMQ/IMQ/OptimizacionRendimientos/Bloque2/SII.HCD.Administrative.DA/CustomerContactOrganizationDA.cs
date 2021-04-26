using System;
using System.Collections.Generic;
using System.Text;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using System.Data;
using System.Data.SqlClient;
using SII.Framework.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.ExceptionHandling;
using SII.HCD.Administrative.Entities;

namespace SII.HCD.Administrative.DA
{
    public class CustomerContactOrganizationDA : DAServiceBase
    {
        #region Field length constants
        public const int ModifiedByLength = 256;
        #endregion

        public CustomerContactOrganizationDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public CustomerContactOrganizationDA(Gateway gateway) : base(gateway) { }

        public int Insert(int organizationID, int customerID, int contactTypeID, bool urgentContact, bool alternativeContact, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertCustomerContactOrganizationCommand,
                    new StoredProcInParam("OrganizationID", DbType.Int32, organizationID),
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateCustomerContactOrganizationCommand,
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.DeleteCustomerContactOrganizationCommand,
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
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactOrganizationDBTimeStampCommand, TableNames.CustomerContactOrganizationTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[TableNames.CustomerContactOrganizationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[TableNames.CustomerContactOrganizationTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetCustomerContactOrganizations(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactOrganizationsCommand, TableNames.CustomerContactOrganizationListDTOTable,
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
 
 /* 
        public DataSet GetCustomerContactOrganization(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactOrganizationCommand, TableNames.CustomerContactOrganizationTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
              
        public DataSet GetCustomerContactOrganization(int id)
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

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerContactOrganizationTable;
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
*/
        public DataSet GetCustomerContactOrganization(int id)
        {
            try
            {
                DataSet ds = this.Gateway.ExecuteStoredProcedureDataSet("Obtener_CustomerContactOrganization_Entity",
                    new StoredProcInParam("ID", DbType.Int32, id)
                    );

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = Administrative.Entities.TableNames.CustomerContactOrganizationTable;

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
        public int GetCustomerContactOrganizationsFromOrganization(int organizationID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactOrganizationsFromOrganizationCommand, TableNames.CustomerContactOrganizationTable,
                    new StoredProcInParam("OrganizationID", DbType.Int32, organizationID));
                if (result.Tables[TableNames.CustomerContactOrganizationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.CustomerContactOrganizationTable].Rows[0]["Count"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }
    }
}