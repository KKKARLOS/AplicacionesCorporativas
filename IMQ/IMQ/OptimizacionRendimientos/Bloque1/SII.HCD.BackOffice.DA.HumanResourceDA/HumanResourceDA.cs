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
namespace SII.HCD.BackOffice.DA
{
    public class HumanResourceDA : DAServiceBase
    {
        #region Field length constants
        public const int FileNumberLength = 50;
        public const int ModifiedByLength = 256;
        #endregion

        #region Constructors
        public HumanResourceDA() : base(GetDatabaseName("HCDDB")) { }
        public HumanResourceDA(Gateway gateway) : base(gateway) { }
        #endregion

        #region Public methods
        public int Insert(int personID, string fileNumber, bool hasAvailability, bool admitNotification, bool includingEmail, string modifiedBy)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertHHRRCommand,
                    new StoredProcInParam("PersonID", DbType.Int32, personID),
                    new StoredProcInParam("FileNumber", DbType.String, SIIStrings.Left(fileNumber, FileNumberLength)),
                    new StoredProcInParam("HasAvailability", DbType.Boolean, hasAvailability),
                    new StoredProcInParam("AdmitNotification", DbType.Boolean, admitNotification),
                    new StoredProcInParam("IncludingEmail", DbType.Boolean, includingEmail),
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

        public int Update(int id, string fileNumber, bool hasAvailability, bool admitNotification, bool includingEmail, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateHHRRCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("FileNumber", DbType.String, SIIStrings.Left(fileNumber, FileNumberLength)),
                    new StoredProcInParam("HasAvailability", DbType.Boolean, hasAvailability),
                    new StoredProcInParam("AdmitNotification", DbType.Boolean, admitNotification),
                    new StoredProcInParam("IncludingEmail", DbType.Boolean, includingEmail),
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
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateHHRRStampCommand,
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

        public int Update(int id, bool hasAvailability, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdateHHRRHasAvailabilityCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("HasAvailability", DbType.Boolean, hasAvailability),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int GetIDByPersonID(int personID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHumanResourceIDByPersonID,
                    new StoredProcInParam("PersonID", DbType.Int32, personID)))
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

        public int GetIDByUserName(string userName)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHumanResourceIDByUserName,
                    new StoredProcInParam("UserName", DbType.String, userName)))
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

        public int GetIDByFileNumber(string fileNumber)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHumanResourceIDByFileNumberCommand,
                    new StoredProcInParam("FileNumber", DbType.String, fileNumber)))
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

        public int GetPersonID(int id)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHumanResourcePersonIDByID,
                    new StoredProcInParam("HumanResourceID", DbType.Int32, id)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["PersonID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int GetIDByDeviceRel(int resourceDeviceRelID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHumanResourceIDByResourceDeviceRelID,
                    new StoredProcInParam("ResourceDeviceRel", DbType.Int32, resourceDeviceRelID)))
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

        public int GetDefaultProfileID(int humanResourceID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHumanResourceDefaultProfileID,
                    new StoredProcInParam("HumanResourceID", DbType.Int32, humanResourceID)))
                {
                    return (IsEmptyReader(reader)) ? -1 : SIIConvert.ToInteger(reader["ProfileID"].ToString(), -1);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }
/*
        public DataSet GetByID(int id)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByID,
                    TableNames.HumanResourceTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
*/
        public DataSet GetByID(int id)
        {
            try
            {
                SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@HumanResourceID", SqlDbType.Int, 4, id)
					};
                DataSet ds = SqlHelper.ExecuteDataset("ObtenerHumanResourceEntity", aParam);

                if (ds.Tables.Count != 0)
                {
                    int i = 0;

                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.HumanResourceTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.HHRRProfileRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ProfileTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ParticipateAsProfileRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ParticipateAsTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.ResourceDeviceRelTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.DeviceTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.DeviceTypeTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PersonAvailPatternTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AvailPatternTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AvailabilityTable;
                    ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PersonCareCenterAccessTable;

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
        public DataSet GetByPersonID(int personID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByPersonID,
                    TableNames.HumanResourceTable,
                    new StoredProcInParam("PersonID", DbType.Int32, personID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHumanResources()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResources, TableNames.HumanResourceDTOTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetActiveHumanResources()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetActiveHumanResources, TableNames.HumanResourceDTOTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByAdmitNotification()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceWithAdmitNotification, TableNames.HumanResourceDTOTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByAdmitNotificationAndProfile(int profileID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceWithAdmitNotificationByProfile, TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByDevice()
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceWithDevice,
                    TableNames.HumanResourceDTOTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByPersonDevice(int personDeviceRelID)
        {
            try
            {
                //Devuelve HR.[ID] AS HumanResourceID, Person.FirstName, Person.LastName, Person.LastName2
                //del recurso humano asociado a la relación con dispositivo pasado como parámetro
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByPersonDeviceCommand,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("PersonDeviceRelID", DbType.Int32, personDeviceRelID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByProfile(int profileID)
        {
            try
            {
                //Devuelve HR.[ID] AS HumanResourceID, Person.FirstName, Person.LastName, Person.LastName2
                //de los recursos humanos asociados al profile pasado como parámetro
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByProfileCommand,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetWithFileNumberByProfile(int profileID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceWithFileNumberByProfileCommand,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByCategory(CategoryPersonKeyEnum categoryKey)
        {
            try
            {
                //Devuelve HR.[ID] AS HumanResourceID, Person.FirstName, Person.LastName, Person.LastName2
                //de los recursos humanos asociados al profile pasado como parámetro
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByCategoryCommand,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("CategoryKey", DbType.Int32, categoryKey));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByProfileAndCareCenter(int profileID, int careCenterID)
        {
            try
            {
                //Devuelve HR.[ID] AS HumanResourceID, Person.FirstName, Person.LastName, Person.LastName2
                //de los recursos humanos asociados al profile pasado como parámetro
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByProfileAndCareCenterCommand,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByProfilesAndCareCenter(int[] profileIDs, int careCenterID)
        {
            try
            {
                string sqlQuery = SQLProvider.GetHumanResourceByProfilesAndCareCenterCommand;
                if ((profileIDs != null) && (profileIDs.Length > 0))
                    sqlQuery += string.Concat(" AND (PF.[ID] IN (", StringUtils.BuildIDString(profileIDs), "))");
                return this.Gateway.ExecuteQueryDataSet(sqlQuery,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHumanResourcesByCareCenter(int careCenterID, int[] profileIDs)
        {
            if (careCenterID <= 0) return null;

            try
            {
                string whereProfileIDs = string.Empty;
                if (profileIDs != null && profileIDs.Length > 0)
                {
                    whereProfileIDs = StringUtils.BuildIDString(profileIDs);
                }

                string selectSQLQuery = SQLProvider.CommonSELECTActiveHumanResourcesCommand;
                string fromSQLQuery = SQLProvider.CommonFROMActiveHumanResourcesCommand;
                string whereSQLQuery = SQLProvider.CommonWHEREActiveHumanResourcesCommand;

                if (careCenterID > 0)
                {
                    fromSQLQuery += string.Concat(Environment.NewLine, "JOIN PersonCareCenterAccess PCC ON PER.[ID]=PCC.PersonID");

                    whereSQLQuery += string.Concat(Environment.NewLine, "AND (PCC.CareCenterID=@CareCenterID) AND (PCC.StartAccessDate<@Date)",
                                                    Environment.NewLine, "AND ((PCC.EndAccessDate is null) OR (PCC.EndAccessDate>=@Date))");
                }
                if ((profileIDs != null)
                    && (profileIDs.Length > 0))
                {
                    fromSQLQuery += string.Concat(Environment.NewLine, "LEFT JOIN HHRRProfileRel HPR ON HPR.HumanResourceID = HR.[ID]",
                                                    Environment.NewLine, "LEFT JOIN [Profile] PF ON HPR.ProfileID = PF.[ID]");

                    whereSQLQuery += string.Concat(Environment.NewLine, "AND (PF.[ID] IN (", StringUtils.BuildIDString(profileIDs), "))");
                }

                string finalSQLQuery = string.Concat(selectSQLQuery,
                    Environment.NewLine, fromSQLQuery,
                    Environment.NewLine, whereSQLQuery);


                return this.Gateway.ExecuteQueryDataSet(finalSQLQuery, TableNames.HumanResourceTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Date", DbType.DateTime, DateTime.Now));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByCareCenter(int careCenterID)
        {
            try
            {
                //Devuelve HR.[ID] AS HumanResourceID, Person.FirstName, Person.LastName, Person.LastName2
                //de los recursos humanos asociados al profile pasado como parámetro
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByCareCenterCommand,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByCareCenterAndAssistanceService(int assistanceServiceID, int careCenterID)
        {
            try
            {
                //Devuelve HR.[ID] AS HumanResourceID, Person.FirstName, Person.LastName, Person.LastName2
                //de los recursos humanos asociados al profile pasado como parámetro
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByCareCenterAndAssistanceServiceCommand,
                    TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("AssistanceServiceID", DbType.Int32, assistanceServiceID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHHRRs(string identifierTypeName, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHHRRsTopNCommand, TableNames.HHRRListDTOTable,
                    new StoredProcInParam("Name", DbType.String, identifierTypeName),
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHHRRListDTOByID(int id, string identifierTypeName)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHHRRListDTOByIDCommand,
                    TableNames.HHRRListDTOTable,
                    new StoredProcInParam("Name", DbType.String, identifierTypeName),
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
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHHRRDBTimeStampCommand, TableNames.HumanResourceTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[TableNames.HumanResourceTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[TableNames.HumanResourceTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int FindFileNumber(string fileNumber)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.FindFileNumberCommand, TableNames.HumanResourceTable,
                    new StoredProcInParam("FileNumber", DbType.String, fileNumber));
                if (result.Tables[TableNames.HumanResourceTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.HumanResourceTable].Rows[0]["ID"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetHumanResourceByCustomerReservation(int customerReservationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByCustomerReservationCommand,
                    TableNames.HumanResourceBasicTable,
                    new StoredProcInParam("CustomerReservationID", DbType.Int32, customerReservationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHumanResourcesByCitationConfig(int citationConfigID, int resourceTypeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByCitationConfigIDCommand, TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("CitationConfigID", DbType.Int32, citationConfigID),
                    new StoredProcInParam("ResourceElement", DbType.Int32, AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("ResourceTypeID", DbType.Int32, resourceTypeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHumanResourcesByWLConfig(int waitingListConfigID, int resourceTypeID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHumanResourceByWLConfigIDCommand, TableNames.HumanResourceDTOTable,
                    new StoredProcInParam("WaitingListConfigID", DbType.Int32, waitingListConfigID),
                    new StoredProcInParam("ResourceElement", DbType.Int32, AppointmentResourceElementEnum.Location),
                    new StoredProcInParam("ResourceTypeID", DbType.Int32, resourceTypeID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public bool GetHasAvailabilityByHumanResourceID(int id)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHasAvailabilityByHumanResourceIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)))
                {
                    return (IsEmptyReader(reader)) ? false : reader["HasAvailability"] as bool? ?? false;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return false;
            }
        }

        public DataSet GetHHRRsWithProfileByEmployeeIsActiveCareCenterID(int identifierTypeID, int careCenterID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDCommand, TableNames.HHRRListDTOTable,
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDRoutineID(int identifierTypeID, int careCenterID, int routineID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDRoutineIDCommand, TableNames.HHRRListDTOTable,
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("RoutineID", DbType.Int32, routineID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDProcedureID(int identifierTypeID, int careCenterID, int procedureID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetHHRRsWithProfileByEmployeeIsActiveCareCenterIDProcedureIDCommand, TableNames.HHRRListDTOTable,
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("ProcedureID", DbType.Int32, procedureID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionsByProfiles(int[] profileIDs)
        {
            try
            {
                string sqlQuery = SQLProvider.GetHumanResourcesByProfiles;
                if ((profileIDs != null) && (profileIDs.Length > 0))
                    sqlQuery += string.Concat(" WHERE (HRPR.ProfileID IN (", StringUtils.BuildIDString(profileIDs), "))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, Common.Entities.TableNames.IDDescriptionTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetHumanResourceByCustomerProcess(int[] processChartIDs,
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


                string finalQuery = SQLProvider.GetHumanResourceByCustomerProcessCommand;

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
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.BackOffice.Entities.TableNames.HumanResourceBasicTable,
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

        public string GetHumanResourceNameByID(int humanResourceID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetHumanResourceNameByIDCommand,
                    new StoredProcInParam("HumanResourceID", DbType.Int32, humanResourceID)))
                {
                    return (IsEmptyReader(reader))
                        ? string.Empty
                        : CommonEntities.DescriptionBuilder.PersonBuildName(
                                reader["FirstName"].ToString(),
                                reader["Lastname"].ToString(),
                                reader["LastName2"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return string.Empty;
            }
        }

        public bool DeleteHHRRProfileIsPossible(int hhrrID, int profileID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.DeleteHHRRProfileIsPossible,
                    new StoredProcInParam("HHRRID", DbType.Int32, hhrrID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID)))
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
    }
}