using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.BackOffice.Entities;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.BackOffice.DA
{
    public class OrganizationDA : DAServiceBase
    {
        #region Field length constants
        public const int NameLength = 100;
        public const int SocialReasonLength = 200;
        public const int TelecomAddressLength = 250;
        public const int ModifiedByLength = 256;
        #endregion

        public OrganizationDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public OrganizationDA(Gateway gateway) : base(gateway) { }

        public int Insert(string name, string socialReason, string comments, string telecomAddress,
            int addressID, int addressID2, string modifiedBy, int status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertOrganizationCommand,
                    new StoredProcInParam("Name", DbType.String, SIIStrings.Left(name, NameLength)),
                    new StoredProcInParam("SocialReason", DbType.String, SIIStrings.Left(socialReason, SocialReasonLength)),
                    new StoredProcInParam("Comments", DbType.String, comments),
                    new StoredProcInParam("TelecomAddress", DbType.String, SIIStrings.Left(telecomAddress, TelecomAddressLength)),
                    new StoredProcInParam("AddressID", DbType.Int32, addressID),
                    new StoredProcInParam("Address2ID", DbType.Int32, addressID2),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)),
                    new StoredProcInParam("Status", DbType.Int32, status)
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

        public int Update(int id, string name, string socialReason, string comments, string telecomAddress,
            int addressID, int addressID2, string modifiedBy, int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateOrganizationCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Name", DbType.String, SIIStrings.Left(name, NameLength)),
                    new StoredProcInParam("SocialReason", DbType.String, SIIStrings.Left(socialReason, SocialReasonLength)),
                    new StoredProcInParam("Comments", DbType.String, comments),
                    new StoredProcInParam("TelecomAddress", DbType.String, SIIStrings.Left(telecomAddress, TelecomAddressLength)),
                    new StoredProcInParam("AddressID", DbType.Int32, addressID),
                    new StoredProcInParam("Address2ID", DbType.Int32, addressID2),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)),
                    new StoredProcInParam("Status", DbType.Int32, status)
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateOrganizationStampCommand,
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

        public Int64 GetDBTimeStamp(int id)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationDBTimeStampCommand, TableNames.OrganizationTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[TableNames.OrganizationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[TableNames.OrganizationTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetOrganizations(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsCommand, TableNames.OrganizationListDTOTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizations(DateTime fromDate, DateTime toDate, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsTopNCommand, TableNames.OrganizationListDTOTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizations(string name, string socialReason, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsTopNAnyCatCommand, TableNames.OrganizationAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, name),
                    new StoredProcInParam("SocialReason", DbType.String, socialReason),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationsExcludeCCO(string name, string socialReason, int identifierTypeID, string idNumber, int categoryID, int maxRecords, int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsTopNExcludeCCOCommand, TableNames.OrganizationAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, name),
                    new StoredProcInParam("SocialReason", DbType.String, socialReason),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationsExcludeCareCenters(string name, string socialReason, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsTopNExcludeCareCentersCommand, TableNames.OrganizationAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, name),
                    new StoredProcInParam("SocialReason", DbType.String, socialReason),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationsExcludeInsurers(string name, string socialReason, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsTopNExcludeInsurersCommand, TableNames.OrganizationAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, name),
                    new StoredProcInParam("SocialReason", DbType.String, socialReason),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationsExcludeSuppliers(string name, string socialReason, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsTopNExcludeSuppliersCommand, TableNames.OrganizationAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, name),
                    new StoredProcInParam("SocialReason", DbType.String, socialReason),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationsExcludeBuyers(string name, string socialReason, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsTopNExcludeBuyersCommand, TableNames.OrganizationAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, name),
                    new StoredProcInParam("SocialReason", DbType.String, socialReason),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        public DataSet GetOrganization(int organizationID)
        {
            try
            {
                SqlParameter[] aParam = new SqlParameter[]{
                        ParametroSql.add("@organizationID", SqlDbType.Int, 4, organizationID)
                    };
                DataSet ds = SqlHelper.ExecuteDataset("ObtenerOrganizationEntity", aParam);

                if (ds.Tables.Count != 0)
                {
                    int i = 0;
                    ds.Tables[i++].TableName = TableNames.OrganizationTable;
                    ds.Tables[i++].TableName = TableNames.OrganizationTelephoneTable;
                    ds.Tables[i++].TableName = TableNames.TelephoneTable;
                    ds.Tables[i++].TableName = TableNames.AddressTable;
                    ds.Tables[i++].TableName = TableNames.Address2Table;
                    ds.Tables[i++].TableName = TableNames.OrganizationCategoryTable;
                    ds.Tables[i++].TableName = TableNames.CategoryTable;
                    ds.Tables[i++].TableName = TableNames.IdentifierTable;
                    ds.Tables[i++].TableName = TableNames.IdentifierTypeTable;
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
/*
        public DataSet GetOrganization(int organizationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationCommand, TableNames.OrganizationTable,
                    new StoredProcInParam("ID", DbType.Int32, organizationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
*/
        public DataSet GetOrganizationByIDs(int[] organizationIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationsByIDsCommand, TableNames.OrganizationTable,
                    new StoredProcInTVPIntegerParam("TVPTable", organizationIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationByPhysicianTimestamp(long timestamp)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                    SQLProvider.GetOrganizationByPhysicianCommand,
                    TableNames.OrganizationTable,
                    new StoredProcInParam("DBTimestamp", DbType.Int64, timestamp));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetAllOrganization()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllOrganizationCommand, TableNames.OrganizationTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetAllCareCenterOrganization()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllCareCenterOrganizationCommand, TableNames.OrganizationBaseTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationBaseUsedInLocationAsCareCenter()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationBaseUsedInLocationAsCareCenterCommand, TableNames.OrganizationBaseTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationWithCustomer()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationWithCustomerCommand, TableNames.OrganizationBaseTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetOrganizationWithPhysician()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetOrganizationWithPhysicianCommand, TableNames.OrganizationBaseTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetOrganization(OrganizationFindRequest organization)
        {
            try
            {
                if (organization == null) throw new ArgumentNullException("findorganization");

                if (!organization.MandatoryName) return -1;

                DataSet result = null;
                string sqlCommand = "SELECT [ID] FROM Organization WHERE ";

                if (organization.MandatoryName)
                {
                    sqlCommand = String.Concat(sqlCommand, "[Name]=@Name");
                }

                result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.OrganizationTable,
                    new StoredProcInParam("Name", DbType.String, organization.Name));

                if (result.Tables[TableNames.OrganizationTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.OrganizationTable].Rows[0]["ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetOrganization(CareCenterFindRequest careCenter)
        {
            try
            {
                if (careCenter == null) throw new ArgumentNullException("findcareCenter");

                if (!careCenter.MandatoryName) return -1;

                DataSet result = null;
                string sqlCommand = "SELECT [ID] FROM Organization WHERE ";

                if (careCenter.MandatoryName)
                {
                    sqlCommand = String.Concat(sqlCommand, "[Name]=@Name");
                }

                result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.CareCenterTable,
                    new StoredProcInParam("Name", DbType.String, careCenter.Name));

                if (result.Tables[TableNames.CareCenterTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.CareCenterTable].Rows[0]["ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetOrganization(InsurerFindRequest insurer)
        {
            try
            {
                if (insurer == null) throw new ArgumentNullException("findinsurer");

                if (!insurer.MandatoryName) return -1;

                DataSet result = null;
                string sqlCommand = "SELECT [ID] FROM Organization WHERE ";

                if (insurer.MandatoryName)
                {
                    sqlCommand = String.Concat(sqlCommand, "[Name]=@Name");
                }

                result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.InsurerTable,
                    new StoredProcInParam("Name", DbType.String, insurer.Name));

                if (result.Tables[TableNames.InsurerTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.InsurerTable].Rows[0]["ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetOrganization(SupplierFindRequest supplier)
        {
            try
            {
                if (supplier == null) throw new ArgumentNullException("findsupplier");

                if (!supplier.MandatoryName) return -1;

                DataSet result = null;
                string sqlCommand = "SELECT [ID] FROM Organization WHERE ";

                if (supplier.MandatoryName)
                {
                    sqlCommand = String.Concat(sqlCommand, "[Name]=@Name");
                }

                result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.SupplierTable,
                    new StoredProcInParam("Name", DbType.String, supplier.Name));

                if (result.Tables[TableNames.SupplierTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.SupplierTable].Rows[0]["ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetOrganization(BuyerFindRequest buyer)
        {
            try
            {
                if (buyer == null) throw new ArgumentNullException("findbuyer");

                if (!buyer.MandatoryName) return -1;

                DataSet result = null;
                string sqlCommand = "SELECT [ID] FROM Organization WHERE ";

                if (buyer.MandatoryName)
                {
                    sqlCommand = String.Concat(sqlCommand, "[Name]=@Name");
                }

                result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.BuyerTable,
                    new StoredProcInParam("Name", DbType.String, buyer.Name));

                if (result.Tables[TableNames.BuyerTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.BuyerTable].Rows[0]["ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public DataSet GetRelatedOrganizationByInvoicePrintRule(int invoicePrintRuleID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRelatedOrganizationByInvoicePrintRuleCommand, TableNames.OrganizationListDTOTable,
                    new StoredProcInParam("InvoicePrintRuleID", DbType.Int32, invoicePrintRuleID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetInsurerOrganizationsByInvoicePrintRule(int invoicePrintRuleID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetInsurerOrganizationsByInvoicePrintRuleCommand, TableNames.OrganizationPrintRuleListDTOTable,
                    new StoredProcInParam("InvoicePrintRuleID", DbType.Int32, invoicePrintRuleID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetCareCenterOrganizationsByInvoicePrintRule(int invoicePrintRuleID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCareCenterOrganizationsByInvoicePrintRuleCommand, TableNames.OrganizationPrintRuleListDTOTable,
                    new StoredProcInParam("InvoicePrintRuleID", DbType.Int32, invoicePrintRuleID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionByCategory(string name, int categoryID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionOrganizationByCategoryCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("Name", DbType.String, (!String.IsNullOrEmpty(name)) ? name : string.Empty),
                            new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int Exists(string name, int categoryID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.ExistsOrganizationByNameAndCategoryCommand, TableNames.OrganizationTable,
                    new StoredProcInParam("Name", DbType.String, name), new StoredProcInParam("CategoryID", DbType.String, categoryID));
                return (result.Tables[TableNames.OrganizationTable].Rows.Count > 0)
                    ? SIIConvert.ToInteger(result.Tables[TableNames.OrganizationTable].Rows[0]["ID"].ToString()) : 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetIDDescriptionWithMasterID(CategoryOrganizationKeyEnum categoryOrganizationKey)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionWithMasterIDCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionWithMasterIDTable,
                            new StoredProcInParam("CategoryKey", DbType.Int32, (int)categoryOrganizationKey),
                            new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionWithMasterID(int requestedCareCenterID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionWithMasterIDByOrganizationIDCommand,
                            "RefCareCenter", new StoredProcInParam("OrganizationID", DbType.Int32, requestedCareCenterID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetParentOrganizationIDFromLocation(int locationID, CommonEntities.StatusEnum status)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetParentOrganizationIDFromLocationCommand, TableNames.OrganizationTable,
                    new StoredProcInParam("LocationID", DbType.Int32, locationID),
                    new StoredProcInParam("Status", DbType.Int32, (int)status));
                return (result.Tables[TableNames.OrganizationTable].Rows.Count > 0)
                    ? result.Tables[TableNames.OrganizationTable].Rows[0]["OrganizationID"] as int? ?? 0 : 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public void MarkUpdatedRelatedProcessChart(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromAdmissionConfigAssistanceCoverGroupRel(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromAdmissionConfigAssistanceCoverGroupRelCareCenterOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("StepConfig", DbType.Int64, (long)BasicProcessStepsEnum.Admission),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReceptionConfigAssistanceCoverGroupRel(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReceptionConfigAssistanceCoverGroupRelCareCenterOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("StepConfig", DbType.Int64, (long)BasicProcessStepsEnum.Reception),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromAdmissionConfigAssistanceCoverGroupRelInsurerCoverAgreement(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromAdmissionConfigAssistanceCoverGroupRelAssistanceCoverGroupInsurerCoverAgreementInsurerOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("StepConfig", DbType.Int64, (long)BasicProcessStepsEnum.Admission),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReceptionConfigAssistanceCoverGroupRelInsurerCoverAgreement(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReceptionConfigAssistanceCoverGroupRelAssistanceCoverGroupInsurerCoverAgreementInsurerOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("StepConfig", DbType.Int64, (long)BasicProcessStepsEnum.Reception),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromTransferConfigTransferProcessRel(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromTransferConfigTransferProcessRelOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromInvoiceConfigInvoiceConfigInvoiceAgreeRelInvoiceConfigAgreementInsurer(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromInvoiceConfigInvoiceConfigInvoiceAgreeRelInvoiceConfigAgreementInsurerCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("GuarantorType", DbType.Int16, (short)GuarantorTypeEnum.InsurerAsGuarantor),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromInvoiceConfigInvoiceConfigInvoiceAgreeRelInvoiceConfigAgreementCareCenter(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromInvoiceConfigInvoiceConfigInvoiceAgreeRelInvoiceAgreementCareCenterCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("GuarantorType", DbType.Int16, (short)GuarantorTypeEnum.InsurerAsGuarantor),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRemittanceRelInvoiceAgreeRemittanceConfigInsurer(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRelInsurerInvoiceAgreementInsurerCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("GuarantorType", DbType.Int16, (short)GuarantorTypeEnum.InsurerAsGuarantor),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRemittanceRelInvoiceAgreeRemittanceConfigCareCenter(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRelInvoiceAgreementCareCenterCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("GuarantorType", DbType.Int16, (short)GuarantorTypeEnum.InsurerAsGuarantor),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        //public void MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRemittanceRelInvoiceAgreeRemittanceConfigBankOrganization(int id, string modifiedBy)
        //{
        //    try
        //    {
        //        this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigInvoiceAgreeRemittanceRelInvoiceAgreeRemittanceConfigBankOrganizationCommand,
        //                new StoredProcInParam("OrganizationID", DbType.Int32, id),
        //                new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return;
        //    }
        //}

        public void MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigExportElementRelExportElementConfigRelInsurer(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigExportElementRelExportElementConfigRelInsurerCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("GuarantorType", DbType.Int16, (short)GuarantorTypeEnum.InsurerAsGuarantor),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigExportElementRelExportElementConfigRelCareCenter(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromRemittanceConfigRemittanceConfigExportElementRelExportElementConfigRelCareCenterCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("GuarantorType", DbType.Int16, (short)GuarantorTypeEnum.InsurerAsGuarantor),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkRelatedUpdatedItemsFromSupplierItemRelationshipSupplier(int organizationID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkRelatedUpdatedItemsFromSupplierItemRelationshipSupplierCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, organizationID),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedRoutineFromRoutineDietaryServiceRelCareCenter(int organizationID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkRelatedUpdatedRoutinesFromRoutineDietaryServiceRelCareCenterCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, organizationID),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedPhysicianFromOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedPhysicianFromPersonAvailPatternCareCenter(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedPhysicianFromPersonAvailPatternCareCenterOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedPhysicianFromPersonCareCenterAccessCareCenter(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedPhysicianFromPersonCareCenterAccessCareCenterOrganizationCommand,
                        new StoredProcInParam("OrganizationID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }
    }
}
