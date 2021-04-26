using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeGenerator2005
{
    public partial class TestDBManager : Form
    {
        public TestDBManager()
        {
            InitializeComponent();
        }
        DBManager _DBManager;

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                _DBManager = new DBManager();
                string strConn = "Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=pubs;Data Source=nesreen";
                if (_DBManager.OpenDataLinkConnection(ref strConn))
                {
                    dgvTables.DataSource = _DBManager.GetAllTables(); ;
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvTables_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvTables.CurrentRow.Index > -1 )//&& dgvTables.CurrentCell.ColumnIndex == 1)
                {
                    DataGridViewRow dgvr = dgvTables.CurrentRow;
                    object cellValueTableName = dgvr.Cells["Table_Name"].Value;
                    object cellValueDBName = dgvr.Cells["Table_Catalog"].Value;
                    if (cellValueTableName != null && cellValueDBName!=null)
                    {
                        string tablename = Convert.ToString(cellValueTableName);
                        string dbname = Convert.ToString(cellValueDBName);
                        dgvColumns.DataSource = _DBManager.GetAllColumns(tablename);
                        dgvProperties.DataSource = _DBManager.GetTableProperties(tablename);
                        dgvChild.DataSource = _DBManager.GetChildTables(tablename);
                        dgvTableParent.DataSource = _DBManager.GetParentTables(tablename);
                        dgvPrimaryKeys.DataSource = _DBManager.GetPrimaryKeys(tablename);
                        int relationType = lstRelations.SelectedIndex + 1;
                        dgvRelatedTables.DataSource = _DBManager.GetRelatedTables(tablename, (RelationType)relationType);
                    }
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstRelations.SelectedIndex = 2;

        }

      

      
    }
}