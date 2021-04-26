namespace CodeGenerator2005
{
    partial class TestDBManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.dgvProperties = new System.Windows.Forms.DataGridView();
            this.dgvChild = new System.Windows.Forms.DataGridView();
            this.lblColumns = new System.Windows.Forms.Label();
            this.lblProperties = new System.Windows.Forms.Label();
            this.lblTableChilds = new System.Windows.Forms.Label();
            this.lblTablePK = new System.Windows.Forms.Label();
            this.dgvPrimaryKeys = new System.Windows.Forms.DataGridView();
            this.lblRelatedTables = new System.Windows.Forms.Label();
            this.dgvRelatedTables = new System.Windows.Forms.DataGridView();
            this.lblTableParent = new System.Windows.Forms.Label();
            this.dgvTableParent = new System.Windows.Forms.DataGridView();
            this.lstRelations = new System.Windows.Forms.ComboBox();
            this.lblRelationType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrimaryKeys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatedTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableParent)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTables
            // 
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.Location = new System.Drawing.Point(2, 44);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.Size = new System.Drawing.Size(502, 150);
            this.dgvTables.TabIndex = 0;
            this.dgvTables.DoubleClick += new System.EventHandler(this.dgvTables_DoubleClick);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(491, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load Tables";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgvColumns
            // 
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Location = new System.Drawing.Point(2, 216);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.Size = new System.Drawing.Size(502, 244);
            this.dgvColumns.TabIndex = 2;
            // 
            // dgvProperties
            // 
            this.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProperties.Location = new System.Drawing.Point(522, 337);
            this.dgvProperties.Name = "dgvProperties";
            this.dgvProperties.Size = new System.Drawing.Size(502, 123);
            this.dgvProperties.TabIndex = 3;
            // 
            // dgvChild
            // 
            this.dgvChild.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChild.Location = new System.Drawing.Point(4, 484);
            this.dgvChild.Name = "dgvChild";
            this.dgvChild.Size = new System.Drawing.Size(502, 150);
            this.dgvChild.TabIndex = 4;
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(5, 199);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(76, 13);
            this.lblColumns.TabIndex = 6;
            this.lblColumns.Text = "Table Columns";
            // 
            // lblProperties
            // 
            this.lblProperties.AutoSize = true;
            this.lblProperties.Location = new System.Drawing.Point(525, 320);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(85, 13);
            this.lblProperties.TabIndex = 7;
            this.lblProperties.Text = "Table Properties";
            // 
            // lblTableChilds
            // 
            this.lblTableChilds.AutoSize = true;
            this.lblTableChilds.Location = new System.Drawing.Point(4, 465);
            this.lblTableChilds.Name = "lblTableChilds";
            this.lblTableChilds.Size = new System.Drawing.Size(59, 13);
            this.lblTableChilds.TabIndex = 8;
            this.lblTableChilds.Text = "Table Child";
            // 
            // lblTablePK
            // 
            this.lblTablePK.AutoSize = true;
            this.lblTablePK.Location = new System.Drawing.Point(524, 196);
            this.lblTablePK.Name = "lblTablePK";
            this.lblTablePK.Size = new System.Drawing.Size(48, 13);
            this.lblTablePK.TabIndex = 9;
            this.lblTablePK.Text = "Table PK";
            // 
            // dgvPrimaryKeys
            // 
            this.dgvPrimaryKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrimaryKeys.Location = new System.Drawing.Point(522, 216);
            this.dgvPrimaryKeys.Name = "dgvPrimaryKeys";
            this.dgvPrimaryKeys.Size = new System.Drawing.Size(502, 101);
            this.dgvPrimaryKeys.TabIndex = 10;
            // 
            // lblRelatedTables
            // 
            this.lblRelatedTables.AutoSize = true;
            this.lblRelatedTables.Location = new System.Drawing.Point(509, 22);
            this.lblRelatedTables.Name = "lblRelatedTables";
            this.lblRelatedTables.Size = new System.Drawing.Size(78, 13);
            this.lblRelatedTables.TabIndex = 12;
            this.lblRelatedTables.Text = "Related Tables";
            // 
            // dgvRelatedTables
            // 
            this.dgvRelatedTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelatedTables.Location = new System.Drawing.Point(520, 43);
            this.dgvRelatedTables.Name = "dgvRelatedTables";
            this.dgvRelatedTables.Size = new System.Drawing.Size(502, 150);
            this.dgvRelatedTables.TabIndex = 11;
            // 
            // lblTableParent
            // 
            this.lblTableParent.AutoSize = true;
            this.lblTableParent.Location = new System.Drawing.Point(520, 466);
            this.lblTableParent.Name = "lblTableParent";
            this.lblTableParent.Size = new System.Drawing.Size(73, 13);
            this.lblTableParent.TabIndex = 14;
            this.lblTableParent.Text = "Table Parents";
            // 
            // dgvTableParent
            // 
            this.dgvTableParent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableParent.Location = new System.Drawing.Point(520, 485);
            this.dgvTableParent.Name = "dgvTableParent";
            this.dgvTableParent.Size = new System.Drawing.Size(502, 150);
            this.dgvTableParent.TabIndex = 13;
            // 
            // lstRelations
            // 
            this.lstRelations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstRelations.FormattingEnabled = true;
            this.lstRelations.Items.AddRange(new object[] {
            "Child",
            "Parent",
            "Both"});
            this.lstRelations.Location = new System.Drawing.Point(895, 12);
            this.lstRelations.Name = "lstRelations";
            this.lstRelations.Size = new System.Drawing.Size(121, 21);
            this.lstRelations.TabIndex = 15;
            // 
            // lblRelationType
            // 
            this.lblRelationType.AutoSize = true;
            this.lblRelationType.Location = new System.Drawing.Point(811, 15);
            this.lblRelationType.Name = "lblRelationType";
            this.lblRelationType.Size = new System.Drawing.Size(78, 13);
            this.lblRelationType.TabIndex = 16;
            this.lblRelationType.Text = "Relations Type";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 643);
            this.Controls.Add(this.lblRelationType);
            this.Controls.Add(this.lstRelations);
            this.Controls.Add(this.lblTableParent);
            this.Controls.Add(this.dgvTableParent);
            this.Controls.Add(this.lblRelatedTables);
            this.Controls.Add(this.dgvRelatedTables);
            this.Controls.Add(this.dgvPrimaryKeys);
            this.Controls.Add(this.lblTablePK);
            this.Controls.Add(this.lblTableChilds);
            this.Controls.Add(this.lblProperties);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.dgvChild);
            this.Controls.Add(this.dgvProperties);
            this.Controls.Add(this.dgvColumns);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dgvTables);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrimaryKeys)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatedTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableParent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.DataGridView dgvProperties;
        private System.Windows.Forms.DataGridView dgvChild;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.Label lblProperties;
        private System.Windows.Forms.Label lblTableChilds;
        private System.Windows.Forms.Label lblTablePK;
        private System.Windows.Forms.DataGridView dgvPrimaryKeys;
        private System.Windows.Forms.Label lblRelatedTables;
        private System.Windows.Forms.DataGridView dgvRelatedTables;
        private System.Windows.Forms.Label lblTableParent;
        private System.Windows.Forms.DataGridView dgvTableParent;
        private System.Windows.Forms.ComboBox lstRelations;
        private System.Windows.Forms.Label lblRelationType;
    }
}

