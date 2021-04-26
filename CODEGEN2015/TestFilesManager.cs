using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CodeGenerator2005
{
    public partial class TestFilesManager : Form
    {
        #region Private Variables
        private FolderBrowserDialog _FolderBrowserDialog;
        private DBManager _DBManager;
        private FilesManager _FileManager;
        private FilesSettingInfo _FilesSettingInfo;
        string _TemplateFolder = "";
        string TemplateSP = "";
        string TemplateDO = "";
        string TemplateDA = "";
        string TemplateBus = "";
        private Boolean initLoad;
        #endregion

        #region Constructor
        public TestFilesManager()
        {
            InitializeComponent();
            _TemplateFolder = Application.StartupPath + "\\FilesTemplate\\";
            _FolderBrowserDialog = new FolderBrowserDialog();
            _DBManager = new DBManager();
            _FilesSettingInfo = new FilesSettingInfo();
            btnGenerateSP.Enabled = false;
            btnGenerateDO.Enabled = false;
            rbtnSQL.Checked = true;
            rbtnCS.Checked = true;
            initLoad = true; //Detectamos si es el arranque de la app para no generar el XML
            ReadXMLfromFile(); //últimos valores utilizados de carpeta temporal y base de datos
            initLoad = false;
        }
        #endregion

        #region Methods
        private void FillSettings()
        {

            _FilesSettingInfo.FolderPath = txtFolder.Text;
            if (rbtnCS.Checked) _FilesSettingInfo.CodeLanguage = CodeLanguage.CS;
            if (rbtnVB.Checked) _FilesSettingInfo.CodeLanguage = CodeLanguage.VB;
            if (rbtnSQL.Checked) _FilesSettingInfo.DBLanguage = DBLanguage.SQL;
            _FilesSettingInfo.UseDataObject = chkUseDataObject.Checked;
            _FilesSettingInfo.OriginalTemplates = chkOriginalTemplates.Checked;
            _FilesSettingInfo.TableName = txtTableName.Text;
            _FilesSettingInfo.AppName = txtAppName.Text;

            _FilesSettingInfo.SPTemplate = TemplateSP;
            _FilesSettingInfo.SPOwner = txtOwnerSP.Text;

            _FilesSettingInfo.SPBeforeDeleteAll = txtBeforeDeleteAllSP.Text;
            _FilesSettingInfo.SPBeforeDeleteByPK = txtBeforeDeleteByPKSP.Text;
            _FilesSettingInfo.SPBeforeInsert = txtBeforeInsertSP.Text;
            _FilesSettingInfo.SPBeforeUpdateByPK = txtBeforeUpdateByPKSP.Text;
            _FilesSettingInfo.SPBeforeSelectAll = txtBeforeSelectAllSP.Text;
            _FilesSettingInfo.SPBeforeSelectByPK = txtBeforeSelectByPKSP.Text;


            _FilesSettingInfo.SPAfterDeleteAll = txtAfterDeleteAllSP.Text;
            _FilesSettingInfo.SPAfterDeleteByPK = txtAfterDeleteByPKSP.Text;
            _FilesSettingInfo.SPAfterInsert = txtAfterInsertSP.Text;
            _FilesSettingInfo.SPAfterUpdateByPK = txtAfterUpdateByPKSP.Text;
            _FilesSettingInfo.SPAfterSelectAll = txtAfterSelectAllSP.Text;
            _FilesSettingInfo.SPAfterSelectByPK = txtAfterSelectByPKSP.Text;



            _FilesSettingInfo.DOAfterName = "";
            _FilesSettingInfo.DONameSpace = txtNameSpaceDO.Text;
            _FilesSettingInfo.DOParentClass = "";
            _FilesSettingInfo.DOParentInterface = "";
            _FilesSettingInfo.DOTemplate = TemplateDO;
            _FilesSettingInfo.DOWCFService = chkWCFServices.Checked;


            _FilesSettingInfo.DAAfterName = "";
            _FilesSettingInfo.DABeforeName = "";
            _FilesSettingInfo.DANameSpace = txtNameSpaceDA.Text;
            _FilesSettingInfo.DAParentInterface = "";

            _FilesSettingInfo.DATemplate = TemplateDA;
            _FilesSettingInfo.WSATemplate = "";
            _FilesSettingInfo.DADBFactoryClass = "";
            _FilesSettingInfo.DAProviderType = "";
            _FilesSettingInfo.DACommandType = "StoredProcedure";
            _FilesSettingInfo.BusAfterName = "";
            _FilesSettingInfo.BusBeforeName = "";
            _FilesSettingInfo.BusNameSpace = txtNameSpaceBus.Text;
            _FilesSettingInfo.BusParentClass = "";
            _FilesSettingInfo.BusParentInterface = "";

            _FilesSettingInfo.BusTemplate = TemplateBus;


            _FilesSettingInfo.WebDetailsAfterName = txtAfterWebDetails.Text;
            _FilesSettingInfo.WebDetailsBeforeName = txtBeforeWebDetails.Text;
            _FilesSettingInfo.WebDetailsTemplate = txtCodeTemplateWebDetails.Text;
            _FilesSettingInfo.WebDetailsTemplateHtml = txtHtmlTemplateWebDetails.Text;

            _FilesSettingInfo.WebHomePageName = txtHomepage.Text;
            _FilesSettingInfo.WebLoginSessionName = txtLoginSessionName.Text;

            _FilesSettingInfo.WebMasterName = txtMasterPageClass.Text;
            _FilesSettingInfo.WebMasterTitle = txtMasterPageTitle.Text;
            _FilesSettingInfo.WebMasterTemplate = txtCodeTemplateWebMaster.Text;
            _FilesSettingInfo.WebMasterTemplateHtml = txtHtmlTemplateWebMaster.Text;

            _FilesSettingInfo.WebSearchAfterName = txtAfterWebSearch.Text;
            _FilesSettingInfo.WebSearchBeforeName = txtBeforeWebSearch.Text;
            _FilesSettingInfo.WebSearchTemplate = txtCodeTemplateWebSearch.Text;
            _FilesSettingInfo.WebSearchTemplateHtml = txtHtmlTemplateWebSearch.Text;

       }
        #endregion

        #region Events

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (_FolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string strPath = _FolderBrowserDialog.SelectedPath;
                    txtFolder.Text = strPath;

                }
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }

        }

        private void btnTemplates_Click(object sender, EventArgs e)
        {
            try
            {
                if (_FolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string strPath = _FolderBrowserDialog.SelectedPath;
                    txtTemplates.Text = strPath + "\\";
                    _TemplateFolder = strPath + "\\";
                    reloadTemplates();

                }
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }
        
        private void TestFilesManager_Load(object sender, EventArgs e)
        {
            try
            {
                //Cambio
                //lstDBProviderDA.SelectedIndex = 0;
                //Cambio
                //lstDBCommandTypeDA.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }

        private void btnGenerateSP_Click(object sender, EventArgs e)
        {
            try
            {
                FillSettings();
                _FileManager.CreateSPFile(_FilesSettingInfo);
                MessageBox.Show("Done");

            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string strPath = txtFolder.Text;
                string strConnection = txtConnection.Text;

                if (strPath.Length > 0 && strConnection.Length > 0)
                {
                    _DBManager.OpenDataLinkConnection(ref strConnection);
                    txtConnection.Text = strConnection;
                }
                else
                {
                    Messages.ShowMessage("Please Select Path  .... ");
                }

            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }

        }

        private void btnGenerateDO_Click(object sender, EventArgs e)
        {
            try
            {
                FillSettings();
                _FileManager.CreateDOFile(_FilesSettingInfo);
                MessageBox.Show("Done");

            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Seguro que quieres obtener la información de todas las tablas?", "Aviso", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    _DBManager.ConnectionString = txtConnection.Text;
                    _FilesSettingInfo.TablesInfo = _DBManager.GetAllTablesInfo();
                    FillSettings();
                    _FileManager = new FilesManager(_FilesSettingInfo);

                    #region View Tables
                    if (_FilesSettingInfo.TablesInfo != null && _FilesSettingInfo.TablesInfo.Count > 0)
                    {
                        lstTables.DataSource = _FilesSettingInfo.TablesInfo;
                        lstTables.DisplayMember = "Name";
                        lstTables.ValueMember = "Name";
                        btnGenerateDO.Enabled = true;
                        btnGenerateSP.Enabled = true;
                        btnGenerateDA.Enabled = true;
                        btnGenerateWeb.Enabled = true;
                        btnGenerateBus.Enabled = true;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }

        private void lstTables_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                #region View Columns
                if (lstTables.SelectedItem != null)
                {
                    lstColumns.DataSource = ((TableInfo)lstTables.SelectedItem).arrColumns;
                    lstColumns.DisplayMember = "Name";
                    lstColumns.ValueMember = "Name";
                }
                #endregion
            }

            catch (Exception ex)
            {
                Messages.ShowErrorMessage(ex);
            }
        }

        private void rbtnVB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TemplateDO = FilesManager.GetFileData(_TemplateFolder + "DO_Template_" + CodeLanguage.VB.ToString());
                TemplateDA = FilesManager.GetFileData(_TemplateFolder + "DA_Template_" + CodeLanguage.VB.ToString());
                TemplateBus = FilesManager.GetFileData(_TemplateFolder + "Bus_Template_" + CodeLanguage.VB.ToString());

                FillSettings();
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }


        }

        private void rbtnCS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                    if (_FilesSettingInfo.OriginalTemplates)
                {
    
                    TemplateDO = FilesManager.GetFileData(_TemplateFolder + "DO_Template_" + CodeLanguage.CS.ToString());
                    TemplateDA = FilesManager.GetFileData(_TemplateFolder + "DA_Template_" + CodeLanguage.CS.ToString());
                    TemplateBus = FilesManager.GetFileData(_TemplateFolder + "Bus_Template_" + CodeLanguage.CS.ToString());
                }
                else
                {
                    
                    TemplateDO = FilesManager.GetFileData(_TemplateFolder + "IB_DO_Template_" + CodeLanguage.CS.ToString());
                    TemplateDA = FilesManager.GetFileData(_TemplateFolder + "IB_DA_Template_" + CodeLanguage.CS.ToString());
                    TemplateBus = FilesManager.GetFileData(_TemplateFolder + "IB_Bus_Template_" + CodeLanguage.CS.ToString());
                }

            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }

        private void rbtnSQL_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                
                TemplateSP = FilesManager.GetFileData(_TemplateFolder + "IB_SP_Template_SQL");// CUIDADO, tiene que existir en el bin
                FillSettings();
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }



        private void btnGenerateDA_Click(object sender, EventArgs e)
        {
            try
            {
                FillSettings();
                _FileManager.CreateDAFile(_FilesSettingInfo);
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }

        private void btnGenerateBus_Click(object sender, EventArgs e)
        {
            try
            {
                FillSettings();
                _FileManager.CreateBusFile(_FilesSettingInfo);
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }

        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            if (!chkModels.Checked && !chkDAL.Checked && !chkBLL.Checked)
            {
                MessageBox.Show("You must select one of the following as a minimum: Model, DAL or BLL", "CodeGenerator 2015", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
            if (chkModels.Checked)
            {
                try
                {
                    FillSettings();
                    _FileManager.CreateDOFile(_FilesSettingInfo);
                    

                }
                catch (Exception ex)
                {

                    Messages.ShowErrorMessage(ex);
                }
            }
            if (chkDAL.Checked){
                try
                {
                    FillSettings();
                    _FileManager.CreateDAFile(_FilesSettingInfo);
                    
                }
                catch (Exception ex)
                {

                    Messages.ShowErrorMessage(ex);
                }
            }
            if (chkBLL.Checked)
            {
                try
                {
                    FillSettings();
                    _FileManager.CreateBusFile(_FilesSettingInfo);
                    
                }
                catch (Exception ex)
                {

                    Messages.ShowErrorMessage(ex);
                }
            }
            MessageBox.Show("Done"); 
        }

        private void btnGenerateWeb_Click(object sender, EventArgs e)
        {
            try
            {
                FillSettings();
                _FileManager.CreateWebGUIFile(_FilesSettingInfo);
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }
        }

        
        #endregion

        #region cambios PROYECTO IBERMATICA

        private void btnOneTable_Click(object sender, EventArgs e)
        {
            try
            {

                _DBManager.ConnectionString = txtConnection.Text;
                FillSettings();
                _FilesSettingInfo.TablesInfo = _DBManager.AddOneTableInfo(_FilesSettingInfo.TableName);
                FillSettings();
                _FileManager = new FilesManager(_FilesSettingInfo);

                #region View Tables
                if (_FilesSettingInfo.TablesInfo != null && _FilesSettingInfo.TablesInfo.Count > 0)
                {
                    lstTables.DataSource = _FilesSettingInfo.TablesInfo;
                    lstTables.DisplayMember = "Name";
                    lstTables.ValueMember = "Name";
                    btnGenerateDO.Enabled = true;
                    btnGenerateSP.Enabled = true;
                    btnGenerateDA.Enabled = true;
                    btnGenerateWeb.Enabled = true;
                    btnGenerateBus.Enabled = true;
                    btnGenerateCode.Enabled = true;
                }
                #endregion
            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }

        }
        

        private void chkOriginalTemplates_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(chkOriginalTemplates.Checked){
                    TemplateSP = FilesManager.GetFileData(_TemplateFolder + "SP_Template_" + DBLanguage.SQL.ToString());
                    TemplateDO = FilesManager.GetFileData(_TemplateFolder + "DO_Template_" + CodeLanguage.CS.ToString());
                    TemplateDA = FilesManager.GetFileData(_TemplateFolder + "DA_Template_" + CodeLanguage.CS.ToString());
                    TemplateBus = FilesManager.GetFileData(_TemplateFolder + "Bus_Template_" + CodeLanguage.CS.ToString());
                }
                else
                {
                    TemplateSP = FilesManager.GetFileData(_TemplateFolder + "IB_SP_Template_SQL");
                    TemplateDO = FilesManager.GetFileData(_TemplateFolder + "IB_DO_Template_" + CodeLanguage.CS.ToString());
                    TemplateDA = FilesManager.GetFileData(_TemplateFolder + "IB_DA_Template_" + CodeLanguage.CS.ToString());
                    TemplateBus = FilesManager.GetFileData(_TemplateFolder + "IB_Bus_Template_" + CodeLanguage.CS.ToString());
                }
                
                FillSettings();

            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }

        }


        private void reloadTemplates()
        {
            try
            {
                if (chkOriginalTemplates.Checked)
                {

                    TemplateSP = FilesManager.GetFileData(_TemplateFolder + "SP_Template_" + DBLanguage.SQL.ToString());
                    TemplateDO = FilesManager.GetFileData(_TemplateFolder + "DO_Template_" + CodeLanguage.CS.ToString());
                    TemplateDA = FilesManager.GetFileData(_TemplateFolder + "DA_Template_" + CodeLanguage.CS.ToString());
                    TemplateBus = FilesManager.GetFileData(_TemplateFolder + "Bus_Template_" + CodeLanguage.CS.ToString());
                }
                else
                {
                    TemplateSP = FilesManager.GetFileData(_TemplateFolder + "IB_SP_Template_SQL");
                    TemplateDO = FilesManager.GetFileData(_TemplateFolder + "IB_DO_Template_" + CodeLanguage.CS.ToString());
                    TemplateDA = FilesManager.GetFileData(_TemplateFolder + "IB_DA_Template_" + CodeLanguage.CS.ToString());
                    TemplateBus = FilesManager.GetFileData(_TemplateFolder + "IB_Bus_Template_" + CodeLanguage.CS.ToString());
                }

                FillSettings();

            }
            catch (Exception ex)
            {

                Messages.ShowErrorMessage(ex);
            }

        }

        #region Cargar/Guardar la última configuración (carpeta destino de documentos y base de datos) en un XML
        private void ReadXMLfromFile()
        {

            XmlTextReader reader = new XmlTextReader("Gcconfig.xml");
            while (reader.Read()) 
            {
                switch (reader.NodeType) 
                {
                    case XmlNodeType.Element: // The node is an element.
                        switch (reader.Name) { 
                            case "TMPFOLDER":
                                if (reader.Read())
                                    txtFolder.Text = reader.Value;
                                break;
                            case "DB":
                                if (reader.Read())
                                    txtConnection.Text = reader.Value;
                                break;
                        }
                        break;
                }
            }
            reader.Close();
        }

        private void WriteXMLtoFile()
        {
            XmlWriter xmlWriter = XmlWriter.Create("Gcconfig.xml");

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("GCCONFIG");

            xmlWriter.WriteStartElement("TMPFOLDER");
            xmlWriter.WriteString(txtFolder.Text);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("DB");
            xmlWriter.WriteString(txtConnection.Text);

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            if(!initLoad)
                WriteXMLtoFile();
        }

        private void txtConnection_TextChanged(object sender, EventArgs e)
        {
            if (!initLoad)
                WriteXMLtoFile();
        }
        #endregion 


        
        
        private void DeleteOne_Click(object sender, EventArgs e)
        {
            try
            {
                _DBManager.ConnectionString = txtConnection.Text;
                FillSettings();
                
                if (lstTables.SelectedItem != null)
                {
                    string[] arrTableNames = new string[10];
                    int nTableNames = 0;

                    for(int i=0; i<lstTables.Items.Count; i++){
                        if( (TableInfo)lstTables.Items[i] != lstTables.SelectedItem)
                            arrTableNames[nTableNames++] = ((TableInfo)lstTables.Items[i]).Name;
                    }

                    _FilesSettingInfo.TablesInfo = _DBManager.UpdateMappedTables(arrTableNames);

                    #region View Tables
                   
                        lstTables.DataSource = _FilesSettingInfo.TablesInfo;
                        lstTables.DisplayMember = "Name";
                        lstTables.ValueMember = "Name";
                        btnGenerateDO.Enabled = true;
                        btnGenerateSP.Enabled = true;
                        btnGenerateDA.Enabled = true;
                        btnGenerateWeb.Enabled = true;
                        btnGenerateBus.Enabled = true;

                        lstColumns.DataSource = null;
                        lstColumns.DisplayMember = "";
                        lstColumns.ValueMember = "";
                        
                    #endregion


                }
               
            }
            catch (Exception ex)
            {
                Messages.ShowErrorMessage(ex);
            }
        }
        private void txtAppName_TextChanged(object sender, EventArgs e)
        {
            txtNameSpaceDO.Text = "IB." + txtAppName.Text + ".Models";
            txtNameSpaceDA.Text = "IB." + txtAppName.Text + ".DAL";
            txtNameSpaceBus.Text = "IB." + txtAppName.Text + ".BLL";

        }
        #endregion



    }

}
