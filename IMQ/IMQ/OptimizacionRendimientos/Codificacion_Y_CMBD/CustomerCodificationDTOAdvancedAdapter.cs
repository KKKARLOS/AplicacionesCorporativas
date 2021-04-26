using System;
using System.Collections.Generic;
using SII.HCD.Common.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;
using System.Data.SqlClient;

namespace SII.HCD.Administrative.Entities
{
    public class CustomerCodificationDTOAdvancedAdapter
        : EntityAdvancedAdapterBase<CustomerCodificationDTO>
    {
        #region Fields
        //Here add dependent entities adapters 
        #endregion

        #region Constructors
        public CustomerCodificationDTOAdvancedAdapter() : base(TableNames.CustomerCodificationTable) { }

        public CustomerCodificationDTOAdvancedAdapter(string tableName) : base(tableName) { }
        #endregion

        #region EntityAdvancedAdapterBase members
        public override CustomerCodificationDTO GetInfo(
            System.Data.DataRow row, System.Data.DataSet dataset)
        {
            if (row == null)
                return null;

            return new CustomerCodificationDTO(
                row["CustomerProcessID"] as int? ?? 0,
                row["ProcessChartID"] as int? ?? 0,
                row["CareCenterID"] as int? ?? 0,
                row["CustomerCodificationID"] as int? ?? 0,
                row["CustomerEpisodeID"] as int? ?? 0,
                row["CustomerLeaveID"] as int? ?? 0,
                row["CustomerID"] as int? ?? 0,
                row["PersonID"] as int? ?? 0,
                row["CareCenterName"] as string ?? string.Empty,
                row["ProcessChartName"] as string ?? string.Empty,
                row["EpisodeTypeName"] as string ?? string.Empty,
                row["CHNumber"] as string ?? string.Empty,
                SII.HCD.BackOffice.Entities.SexEnumNames.GetName(GetEnum<SII.HCD.BackOffice.Entities.SexEnum>(row["Sex"])),
                row["FirstName"] as string ?? string.Empty,
                row["LastName"] as string ?? string.Empty,
                row["LastName2"] as string ?? string.Empty,
                DateUtils.GetAgeDes(row["BirthDate"] as DateTime? ?? null, null),
                row["DeathDateTime"] as DateTime? ?? null,
                row["CustomerEpisodeNumber"] as string ?? string.Empty,
                row["CustomerEpisodeDate"] as DateTime? ?? DateTime.Now,
                row["EpisodeReason"] as string ?? string.Empty,
                row["AssistanceService"] as string ?? string.Empty,
                row["Origin"] as string ?? string.Empty,
                row["CustomerEpisodeEndDate"] as DateTime? ?? null,
                row["EpisodeLeaveReason"] as string ?? string.Empty,
                row["DischargeAssistanceService"] as string ?? string.Empty,
                row["DischargeFacilityName"] as string ?? string.Empty,
                GetEnum<CommonEntities.StatusEnum>(row["CustomerCodificationStatus"]),
                row["PrimaryDiagnosis"] as string ?? string.Empty,
                row["HasOtherDiagnosis"] as bool? ?? false,
                row["IncludePreviousEpisodes"] as bool? ?? false,
                row["HasMorphologies"] as bool? ?? false,
                row["HasSurgicalProcedures"] as bool? ?? false,
                row["HasObstetricProcedures"] as bool? ?? false,
                row["HasOtherProcedures"] as bool? ?? false,
                row["PrimaryMDC"] as string ?? string.Empty,
                row["HasOtherMDCs"] as bool? ?? false,
                row["PrimaryDRG"] as string ?? string.Empty,
                row["HasOtherDRGs"] as bool? ?? false,
                row["PrimaryDRGWeight"] as double? ?? 0,
                row["RelatedCost"] as decimal? ?? 0,
                row["Explanation"] as string ?? string.Empty,
                //row["CodifiedConfirm"] as bool? ?? false,
                //row["Leave"] as bool? ?? false,
                //row["Exported"] as bool? ?? false,
                row["RegistrationDateTime"] as DateTime? ?? null,
                row["ConfirmedDateTime"] as DateTime? ?? null,
                row["ExportedDateTime"] as DateTime? ?? null
            );
        }
        #endregion
    }
    public class CustomerCodificationDTOdAdapterDataReader
    {
        public static SII.HCD.Administrative.Entities.CustomerCodificationDTO[] Conversion(SqlDataReader dr)
        {

            SII.HCD.Administrative.Entities.CustomerCodificationDTO oCustomerCodificationDTO = null;
            List<SII.HCD.Administrative.Entities.CustomerCodificationDTO> lst = new List<SII.HCD.Administrative.Entities.CustomerCodificationDTO>();

            while (dr.Read())
            {
                oCustomerCodificationDTO = new SII.HCD.Administrative.Entities.CustomerCodificationDTO();

                oCustomerCodificationDTO.CustomerProcessID = (dr["CustomerProcessID"] != DBNull.Value) ? int.Parse(dr["CustomerProcessID"].ToString()) : 0;
                oCustomerCodificationDTO.ProcessChartID = (dr["ProcessChartID"] != DBNull.Value) ? int.Parse(dr["ProcessChartID"].ToString()) : 0;
                oCustomerCodificationDTO.CareCenterID = (dr["CareCenterID"] != DBNull.Value) ? int.Parse(dr["CareCenterID"].ToString()) : 0;
                oCustomerCodificationDTO.CustomerCodificationID = (dr["CustomerCodificationID"] != DBNull.Value) ? int.Parse(dr["CustomerCodificationID"].ToString()) : 0;
                oCustomerCodificationDTO.CustomerEpisodeID = (dr["CustomerEpisodeID"] != DBNull.Value) ? int.Parse(dr["CustomerEpisodeID"].ToString()) : 0;
                oCustomerCodificationDTO.CustomerLeaveID = (dr["CustomerLeaveID"] != DBNull.Value) ? int.Parse(dr["CustomerLeaveID"].ToString()) : 0;
                oCustomerCodificationDTO.CustomerID = (dr["CustomerID"] != DBNull.Value) ? int.Parse(dr["CustomerID"].ToString()) : 0;
                oCustomerCodificationDTO.PersonID = (dr["PersonID"] != DBNull.Value) ? int.Parse(dr["PersonID"].ToString()) : 0;
                oCustomerCodificationDTO.CareCenterName = (dr["CareCenterName"] != DBNull.Value) ? dr["CareCenterName"].ToString() : string.Empty;
                oCustomerCodificationDTO.ProcessChartName = (dr["ProcessChartName"] != DBNull.Value) ? dr["ProcessChartName"].ToString() : string.Empty;
                oCustomerCodificationDTO.EpisodeTypeName = (dr["EpisodeTypeName"] != DBNull.Value) ? dr["EpisodeTypeName"].ToString() : string.Empty;
                oCustomerCodificationDTO.CHNumber = (dr["CHNumber"] != DBNull.Value) ? dr["CHNumber"].ToString() : string.Empty;

                //SII.HCD.BackOffice.Entities.SexEnumNames.GetName(GetEnum<SII.HCD.BackOffice.Entities.SexEnum>(row["Sex"]));
                //oCustomerCodificationDTO.Sex = SII.HCD.BackOffice.Entities.SexEnumNames.GetName(GetEnum<SII.HCD.BackOffice.Entities.SexEnum>(short.Parse(dr["Sex"].ToString)));

                oCustomerCodificationDTO.FirstName = (dr["FirstName"] != DBNull.Value) ? dr["FirstName"].ToString() : string.Empty;
                oCustomerCodificationDTO.LastName = (dr["LastName"] != DBNull.Value) ? dr["LastName"].ToString() : string.Empty;
                oCustomerCodificationDTO.LastName2 = (dr["LastName2"] != DBNull.Value) ? dr["LastName2"].ToString() : string.Empty;
                oCustomerCodificationDTO.Age = DateUtils.GetAgeDes((dr["BirthDate"] != DBNull.Value) ? (DateTime?)dr["BirthDate"] : null, null);

                oCustomerCodificationDTO.DeathDateTime = (dr["DeathDateTime"] != DBNull.Value) ? (DateTime?)dr["DeathDateTime"] : null;
                oCustomerCodificationDTO.CustomerEpisodeNumber = (dr["CustomerEpisodeNumber"] != DBNull.Value) ? dr["CustomerEpisodeNumber"].ToString() : string.Empty;
                oCustomerCodificationDTO.CustomerEpisodeDate = (dr["CustomerEpisodeDate"] != DBNull.Value) ? (DateTime)dr["CustomerEpisodeDate"] : DateTime.Now;

                oCustomerCodificationDTO.EpisodeReason = (dr["EpisodeReason"] != DBNull.Value) ? dr["EpisodeReason"].ToString() : string.Empty;
                oCustomerCodificationDTO.AssistanceService = (dr["AssistanceService"] != DBNull.Value) ? dr["AssistanceService"].ToString() : string.Empty;
                oCustomerCodificationDTO.Origin = (dr["Origin"] != DBNull.Value) ? dr["Origin"].ToString() : string.Empty;

                oCustomerCodificationDTO.CustomerEpisodeEndDate = (dr["CustomerEpisodeEndDate"] != DBNull.Value) ? (DateTime?)dr["CustomerEpisodeEndDate"] : DateTime.Now;

                oCustomerCodificationDTO.EpisodeLeaveReason = (dr["EpisodeLeaveReason"] != DBNull.Value) ? dr["EpisodeLeaveReason"].ToString() : string.Empty;
                oCustomerCodificationDTO.DischargeAssistanceService = (dr["DischargeAssistanceService"] != DBNull.Value) ? dr["DischargeAssistanceService"].ToString() : string.Empty;
                oCustomerCodificationDTO.DischargeFacilityName = (dr["DischargeFacilityName"] != DBNull.Value) ? dr["DischargeFacilityName"].ToString() : string.Empty;

                //GetEnum<CommonEntities.StatusEnum>(row["CustomerCodificationStatus"]),
                //oCustomerCodificationDTO.CustomerCodificationStatus = GetEnum<CommonEntities.StatusEnum>(short.Parse(dr["CustomerCodificationStatus"].ToString()));

                oCustomerCodificationDTO.PrimaryDiagnosis = (dr["PrimaryDiagnosis"] != DBNull.Value) ? dr["PrimaryDiagnosis"].ToString() : string.Empty;
                oCustomerCodificationDTO.HasOtherDiagnosis = (dr["HasOtherDiagnosis"] != DBNull.Value) ? (bool)dr["HasOtherDiagnosis"] : false;
                oCustomerCodificationDTO.IncludePreviousEpisodes = (dr["IncludePreviousEpisodes"] != DBNull.Value) ? (bool)dr["IncludePreviousEpisodes"] : false;
                oCustomerCodificationDTO.HasMorphologies = (dr["HasMorphologies"] != DBNull.Value) ? (bool)dr["HasMorphologies"] : false;
                oCustomerCodificationDTO.HasSurgicalProcedures = (dr["HasSurgicalProcedures"] != DBNull.Value) ? (bool)dr["HasSurgicalProcedures"] : false;
                oCustomerCodificationDTO.HasObstetricProcedures = (dr["HasObstetricProcedures"] != DBNull.Value) ? (bool)dr["HasObstetricProcedures"] : false;
                oCustomerCodificationDTO.HasOtherProcedures = (dr["HasOtherProcedures"] != DBNull.Value) ? (bool)dr["HasOtherProcedures"] : false;
                oCustomerCodificationDTO.PrimaryMDC = (dr["PrimaryMDC"] != DBNull.Value) ? dr["PrimaryMDC"].ToString() : string.Empty;
                oCustomerCodificationDTO.HasOtherMDCs = (dr["HasOtherMDCs"] != DBNull.Value) ? (bool)dr["HasOtherMDCs"] : false;
                oCustomerCodificationDTO.PrimaryDRG = (dr["PrimaryDRG"] != DBNull.Value) ? dr["PrimaryDRG"].ToString() : string.Empty;
                oCustomerCodificationDTO.HasOtherDRGs = (dr["HasOtherDRGs"] != DBNull.Value) ? (bool)dr["HasOtherDRGs"] : false;
                oCustomerCodificationDTO.PrimaryDRGWeight = (dr["PrimaryDRGWeight"] != DBNull.Value) ? double.Parse(dr["PrimaryDRGWeight"].ToString()) : 0;
                oCustomerCodificationDTO.RelatedCost = (dr["RelatedCost"] != DBNull.Value) ? decimal.Parse(dr["RelatedCost"].ToString()) : 0;
                oCustomerCodificationDTO.Explanation = (dr["Explanation"] != DBNull.Value) ? dr["Explanation"].ToString() : string.Empty;
                oCustomerCodificationDTO.RegistrationDateTime = (dr["RegistrationDateTime"] != DBNull.Value) ? (DateTime?)dr["RegistrationDateTime"] : null;
                oCustomerCodificationDTO.ConfirmedDateTime = (dr["ConfirmedDateTime"] != DBNull.Value) ? (DateTime?)dr["ConfirmedDateTime"] : null;
                oCustomerCodificationDTO.ExportedDateTime = (dr["ExportedDateTime"] != DBNull.Value) ? (DateTime?)dr["ExportedDateTime"] : null;


                lst.Add(oCustomerCodificationDTO);
            }
            dr.Close();
            return lst.ToArray();
        }
    }
}
