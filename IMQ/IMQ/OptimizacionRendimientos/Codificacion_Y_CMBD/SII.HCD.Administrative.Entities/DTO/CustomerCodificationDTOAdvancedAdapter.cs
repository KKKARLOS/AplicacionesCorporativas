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
        public CustomerCodificationDTO[] ConversionFromDataReader(SqlDataReader dr)
        {
            CustomerCodificationDTO oCustomerCodificationDTO = null;
            List<CustomerCodificationDTO> lst = new List<CustomerCodificationDTO>();

            while (dr.Read())
            {
                oCustomerCodificationDTO = new CustomerCodificationDTO(
                dr["CustomerProcessID"] as int? ?? 0,
                dr["ProcessChartID"] as int? ?? 0,
                dr["CareCenterID"] as int? ?? 0,
                dr["CustomerCodificationID"] as int? ?? 0,
                dr["CustomerEpisodeID"] as int? ?? 0,
                dr["CustomerLeaveID"] as int? ?? 0,
                dr["CustomerID"] as int? ?? 0,
                dr["PersonID"] as int? ?? 0,
                dr["CareCenterName"] as string ?? string.Empty,
                dr["ProcessChartName"] as string ?? string.Empty,
                dr["EpisodeTypeName"] as string ?? string.Empty,
                dr["CHNumber"] as string ?? string.Empty,
                SII.HCD.BackOffice.Entities.SexEnumNames.GetName(GetEnum<SII.HCD.BackOffice.Entities.SexEnum>(dr["Sex"])),
                dr["FirstName"] as string ?? string.Empty,
                dr["LastName"] as string ?? string.Empty,
                dr["LastName2"] as string ?? string.Empty,
                DateUtils.GetAgeDes(dr["BirthDate"] as DateTime? ?? null, null),
                dr["DeathDateTime"] as DateTime? ?? null,
                dr["CustomerEpisodeNumber"] as string ?? string.Empty,
                dr["CustomerEpisodeDate"] as DateTime? ?? DateTime.Now,
                dr["EpisodeReason"] as string ?? string.Empty,
                dr["AssistanceService"] as string ?? string.Empty,
                dr["Origin"] as string ?? string.Empty,
                dr["CustomerEpisodeEndDate"] as DateTime? ?? null,
                dr["EpisodeLeaveReason"] as string ?? string.Empty,
                dr["DischargeAssistanceService"] as string ?? string.Empty,
                dr["DischargeFacilityName"] as string ?? string.Empty,
                GetEnum<CommonEntities.StatusEnum>(dr["CustomerCodificationStatus"]),
                dr["PrimaryDiagnosis"] as string ?? string.Empty,
                dr["HasOtherDiagnosis"] as bool? ?? false,
                dr["IncludePreviousEpisodes"] as bool? ?? false,
                dr["HasMorphologies"] as bool? ?? false,
                dr["HasSurgicalProcedures"] as bool? ?? false,
                dr["HasObstetricProcedures"] as bool? ?? false,
                dr["HasOtherProcedures"] as bool? ?? false,
                dr["PrimaryMDC"] as string ?? string.Empty,
                dr["HasOtherMDCs"] as bool? ?? false,
                dr["PrimaryDRG"] as string ?? string.Empty,
                dr["HasOtherDRGs"] as bool? ?? false,
                dr["PrimaryDRGWeight"] as double? ?? 0,
                dr["RelatedCost"] as decimal? ?? 0,
                dr["Explanation"] as string ?? string.Empty,
                //dr["CodifiedConfirm"] as bool? ?? false,
                //dr["Leave"] as bool? ?? false,
                //dr["Exported"] as bool? ?? false,
                dr["RegistrationDateTime"] as DateTime? ?? null,
                dr["ConfirmedDateTime"] as DateTime? ?? null,
                dr["ExportedDateTime"] as DateTime? ?? null                   
                );
                lst.Add(oCustomerCodificationDTO);
            }
            dr.Close();
            return lst.ToArray();
        }
    }
}
