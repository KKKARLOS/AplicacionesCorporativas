using System;
using SII.HCD.Common.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;
using System.Data;
using System.Collections.Generic;

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

        public CustomerCodificationDTO[] GetData(List<List<object>> datos)
        {
            if (datos == null) return null;

            List<CustomerCodificationDTO> lista = new List<CustomerCodificationDTO>();

            foreach (List<object> l in datos)
            {

                CustomerCodificationDTO customer = new CustomerCodificationDTO(

                    l[0] == null ? 0 : int.Parse(l[0].ToString()),
                    l[1] == null ? 0 : int.Parse(l[1].ToString()),
                    l[2] == null ? 0 : int.Parse(l[2].ToString()),
                    l[3] == null ? 0 : int.Parse(l[3].ToString()),
                    l[4] == null ? 0 : int.Parse(l[4].ToString()),
                    l[5] == null ? 0 : int.Parse(l[5].ToString()),
                    l[6] == null ? 0 : int.Parse(l[6].ToString()),
                    l[7] == null ? 0 : int.Parse(l[7].ToString()),

                    l[8] == null ? string.Empty : l[8].ToString(),
                    l[9] == null ? string.Empty : l[9].ToString(),
                    l[10] == null ? string.Empty : l[10].ToString(),
                    l[11] == null ? string.Empty : l[11].ToString(),

                    BackOffice.Entities.SexEnumNames.GetName(GetEnum<BackOffice.Entities.SexEnum>(l[12])),

                    l[13] == null ? string.Empty : l[13].ToString(),
                    l[14] == null ? string.Empty : l[14].ToString(),
                    l[15] == null ? string.Empty : l[15].ToString(),

                    DateUtils.GetAgeDes(
                        l[16] == DBNull.Value ? (DateTime?)null : DateTime.Parse(l[16].ToString()),
                        l[17] == DBNull.Value ? (DateTime?)null : DateTime.Parse(l[17].ToString())
                    ),

                    l[18] == DBNull.Value ? (DateTime?)null : DateTime.Parse(l[18].ToString()),
                    l[19] == null ? string.Empty : l[19].ToString(),
                    l[20] == DBNull.Value ? DateTime.Now : DateTime.Parse(l[20].ToString()),

                    l[21] == null ? string.Empty : l[21].ToString(),
                    l[22] == null ? string.Empty : l[22].ToString(),
                    l[23] == null ? string.Empty : l[23].ToString(),

                    l[24] == DBNull.Value ? (DateTime?)null : DateTime.Parse(l[24].ToString()),

                    l[25] == null ? string.Empty : l[25].ToString(),
                    l[26] == null ? string.Empty : l[26].ToString(),
                    l[27] == null ? string.Empty : l[27].ToString(),

                    GetEnum<StatusEnum>(l[28]),
                    l[29] == null ? string.Empty : l[29].ToString(),

                    l[30] == null ? false : bool.Parse(l[30].ToString()),
                    l[31] == null ? false : bool.Parse(l[31].ToString()),
                    l[32] == null ? false : bool.Parse(l[32].ToString()),
                    l[33] == null ? false : bool.Parse(l[33].ToString()),
                    l[34] == null ? false : bool.Parse(l[34].ToString()),
                    l[35] == null ? false : bool.Parse(l[35].ToString()),

                    l[36] == null ? string.Empty : l[36].ToString(),
                    l[37] == null ? false : bool.Parse(l[37].ToString()),
                    l[38] == null ? string.Empty : l[38].ToString(),
                    l[39] == null ? false : bool.Parse(l[39].ToString()),

                    l[40] == null ? 0 : double.Parse(l[40].ToString()),
                    l[41] == null ? 0 : decimal.Parse(l[41].ToString()),
                    l[42] == null ? string.Empty : l[42].ToString(),

                    l[43] == DBNull.Value ? (DateTime?)null : DateTime.Parse(l[43].ToString()),
                    l[44] == DBNull.Value ? (DateTime?)null : DateTime.Parse(l[44].ToString()),
                    l[45] == DBNull.Value ? (DateTime?)null : DateTime.Parse(l[45].ToString())
                );

                lista.Add(customer);
            }

            return lista.ToArray();
        }

        #endregion
    }
}
