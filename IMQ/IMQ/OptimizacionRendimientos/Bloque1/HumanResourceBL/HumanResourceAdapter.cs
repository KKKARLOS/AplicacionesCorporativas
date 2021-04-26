using System;
using System.Data;
using SII.Framework.Common;

namespace SII.HCD.BackOffice.Entities
{
    public class HumanResourceAdapter: EntityAdapterBase<HumanResourceEntity>
    {
        public HumanResourceAdapter() : base(TableNames.HumanResourceTable) { }

        public HumanResourceAdapter(string tableName) : base(tableName) { }

        public override void AddEntityToDataSet(HumanResourceEntity Entity, DataSet dataset)
        {
            throw new NotImplementedException();
        }

        protected override DataSet FilterRow(DataRow row, DataSet dataset)
        {
            DataSet result = dataset.Copy();
            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ResourceDeviceRelTable))
            {
                result.Tables.Remove(SII.HCD.BackOffice.Entities.TableNames.ResourceDeviceRelTable);
                result.Tables.Add(GetFilteredTable(dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.ResourceDeviceRelTable],
                    "(HumanResourceID='" + row["ID"].ToString() + "')"));
            }
            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HHRRProfileRelTable))
            {
                result.Tables.Remove(SII.HCD.BackOffice.Entities.TableNames.HHRRProfileRelTable);
                result.Tables.Add(GetFilteredTable(dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.HHRRProfileRelTable],
                    "(HumanResourceID='" + row["ID"].ToString() + "')"));
            }
            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAvailPatternTable))
            {
                result.Tables.Remove(SII.HCD.BackOffice.Entities.TableNames.PersonAvailPatternTable);
                result.Tables.Add(GetFilteredTable(dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonAvailPatternTable],
                    "(PersonID='" + row["PersonID"].ToString() + "')"));
            }

            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonCareCenterAccessTable))
            {
                result.Tables.Remove(SII.HCD.BackOffice.Entities.TableNames.PersonCareCenterAccessTable);
                result.Tables.Add(GetFilteredTable(dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonCareCenterAccessTable],
                    "(PersonID='" + row["PersonID"].ToString() + "')"));
            }

            return result;
        }

        public override DataSet GetDataSetSchema()
        {
            DataSet result = new DataSet();
            DataTable table = result.Tables.Add(TableName);
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("PersonID", typeof(int));
            table.Columns.Add("FileNumber", typeof(String));
            table.Columns.Add("HasAvailability", typeof(bool));
            table.Columns.Add("AdmitNotification", typeof(bool));
            table.Columns.Add("IncludingEmail", typeof(bool));
            table.Columns.Add("DBTimeStamp", typeof(Int64));

            ResourceDeviceAdapter resourceDeviceAdapter = new ResourceDeviceAdapter();
            this.AddTableSchema(resourceDeviceAdapter.GetDataSetSchema(), result);

            HHRRProfileRelAdapter hhrrProfileRelAdapter = new HHRRProfileRelAdapter();
            this.AddTableSchema(hhrrProfileRelAdapter.GetDataSetSchema(), result);

            PersonAvailPatternAdapter hhrrAvailPatternAdapter = new PersonAvailPatternAdapter();
            this.AddTableSchema(hhrrAvailPatternAdapter.GetDataSetSchema(), result);

            PersonCareCenterAccessAdapter personCareCenterAccessAdapter = new PersonCareCenterAccessAdapter();
            this.AddTableSchema(personCareCenterAccessAdapter.GetDataSetSchema(), result);

            return result;
        }

        protected override HumanResourceEntity GetRecord(DataRow row, DataSet dataset)
        {
            ResourceDeviceAdapter resourceDeviceAdapter = new ResourceDeviceAdapter();
            ResourceDeviceEntity[] allocatedDevices = null;
            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.ResourceDeviceRelTable))
            {
                if (dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.ResourceDeviceRelTable].Rows.Count > 0)
                {
                    allocatedDevices = resourceDeviceAdapter.GetData(dataset);
                }
            }

            HHRRProfileRelAdapter hhrrProfileRelAdapter = new HHRRProfileRelAdapter();
            HHRRProfileRelEntity[] profiles = null;
            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.HHRRProfileRelTable))
            {
                if (dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.HHRRProfileRelTable].Rows.Count > 0)
                {
                    profiles = hhrrProfileRelAdapter.GetData(dataset);
                }
            }

            PersonAvailPatternAdapter hhrrAvailPatternAdapter = new PersonAvailPatternAdapter();
            PersonAvailPatternEntity[] availPatterns = null;
            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonAvailPatternTable))
            {
                if (dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonAvailPatternTable].Rows.Count > 0)
                {
                    availPatterns = hhrrAvailPatternAdapter.GetData(dataset);
                }
            }

            PersonCareCenterAccessAdapter personCareCenterAccessAdapter = new PersonCareCenterAccessAdapter();
            PersonCareCenterAccessEntity[] careCentersAccess = null;
            if (dataset.Tables.Contains(SII.HCD.BackOffice.Entities.TableNames.PersonCareCenterAccessTable))
            {
                if (dataset.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonCareCenterAccessTable].Rows.Count > 0)
                {
                    careCentersAccess = personCareCenterAccessAdapter.GetData(dataset);
                }
            }

            return new HumanResourceEntity(SIIConvert.ToInteger(row["ID"].ToString(), 0),
                null,
                SIIConvert.ToBoolean(row["AdmitNotification"].ToString(), false),
                SIIConvert.ToBoolean(row["IncludingEmail"].ToString(), false),
                row["FileNumber"].ToString(),
                SIIConvert.ToBoolean(row["HasAvailability"].ToString(), false),
                profiles,
                availPatterns,
                allocatedDevices,
                careCentersAccess,
                SIIConvert.ToDateTime(row["LastUpdated"].ToString(), DateTime.MinValue),
                row["ModifiedBy"].ToString(),
                false,
                SIIConvert.ToInteger64(row["DBTimeStamp"].ToString(), 0));
        }
    }
}
