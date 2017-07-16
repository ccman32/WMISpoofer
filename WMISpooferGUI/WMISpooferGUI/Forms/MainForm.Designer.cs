namespace WMISpooferGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.installationStatusLabel = new System.Windows.Forms.Label();
            this.currentInstallationStatusLabel = new System.Windows.Forms.Label();
            this.installUninstallButton = new System.Windows.Forms.Button();
            this.classesGroupBox = new System.Windows.Forms.GroupBox();
            this.classesListView = new System.Windows.Forms.ListView();
            this.groupBoxClassProperties = new System.Windows.Forms.GroupBox();
            this.propertiesListView = new System.Windows.Forms.ListView();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.installTabPage = new System.Windows.Forms.TabPage();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.saveButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.newValuesGroupBox = new System.Windows.Forms.GroupBox();
            this.newValuesListView = new System.Windows.Forms.ListView();
            this.classColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.propertyColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newValueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.classesGroupBox.SuspendLayout();
            this.groupBoxClassProperties.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.installTabPage.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.newValuesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // installationStatusLabel
            // 
            this.installationStatusLabel.AutoSize = true;
            this.installationStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installationStatusLabel.Location = new System.Drawing.Point(201, 173);
            this.installationStatusLabel.Name = "installationStatusLabel";
            this.installationStatusLabel.Size = new System.Drawing.Size(156, 24);
            this.installationStatusLabel.TabIndex = 0;
            this.installationStatusLabel.Text = "Installation Status:";
            // 
            // currentInstallationStatusLabel
            // 
            this.currentInstallationStatusLabel.AutoSize = true;
            this.currentInstallationStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentInstallationStatusLabel.Location = new System.Drawing.Point(357, 173);
            this.currentInstallationStatusLabel.Name = "currentInstallationStatusLabel";
            this.currentInstallationStatusLabel.Size = new System.Drawing.Size(0, 24);
            this.currentInstallationStatusLabel.TabIndex = 1;
            // 
            // installUninstallButton
            // 
            this.installUninstallButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installUninstallButton.Location = new System.Drawing.Point(205, 200);
            this.installUninstallButton.Name = "installUninstallButton";
            this.installUninstallButton.Size = new System.Drawing.Size(270, 32);
            this.installUninstallButton.TabIndex = 2;
            this.installUninstallButton.UseVisualStyleBackColor = true;
            this.installUninstallButton.Click += new System.EventHandler(this.installUninstallButton_Click);
            // 
            // classesGroupBox
            // 
            this.classesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.classesGroupBox.Controls.Add(this.classesListView);
            this.classesGroupBox.Location = new System.Drawing.Point(7, 6);
            this.classesGroupBox.Name = "classesGroupBox";
            this.classesGroupBox.Size = new System.Drawing.Size(324, 225);
            this.classesGroupBox.TabIndex = 3;
            this.classesGroupBox.TabStop = false;
            this.classesGroupBox.Text = "Classes";
            // 
            // classesListView
            // 
            this.classesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classesListView.FullRowSelect = true;
            this.classesListView.HideSelection = false;
            this.classesListView.Location = new System.Drawing.Point(3, 16);
            this.classesListView.MultiSelect = false;
            this.classesListView.Name = "classesListView";
            this.classesListView.ShowItemToolTips = true;
            this.classesListView.Size = new System.Drawing.Size(318, 206);
            this.classesListView.TabIndex = 0;
            this.classesListView.UseCompatibleStateImageBehavior = false;
            this.classesListView.View = System.Windows.Forms.View.Details;
            this.classesListView.SelectedIndexChanged += new System.EventHandler(this.classesListView_SelectedIndexChanged);
            // 
            // groupBoxClassProperties
            // 
            this.groupBoxClassProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxClassProperties.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxClassProperties.Controls.Add(this.propertiesListView);
            this.groupBoxClassProperties.Location = new System.Drawing.Point(345, 6);
            this.groupBoxClassProperties.Name = "groupBoxClassProperties";
            this.groupBoxClassProperties.Size = new System.Drawing.Size(324, 225);
            this.groupBoxClassProperties.TabIndex = 4;
            this.groupBoxClassProperties.TabStop = false;
            this.groupBoxClassProperties.Text = "Class Properties";
            // 
            // propertiesListView
            // 
            this.propertiesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesListView.FullRowSelect = true;
            this.propertiesListView.HideSelection = false;
            this.propertiesListView.Location = new System.Drawing.Point(3, 16);
            this.propertiesListView.MultiSelect = false;
            this.propertiesListView.Name = "propertiesListView";
            this.propertiesListView.ShowItemToolTips = true;
            this.propertiesListView.Size = new System.Drawing.Size(318, 206);
            this.propertiesListView.TabIndex = 0;
            this.propertiesListView.UseCompatibleStateImageBehavior = false;
            this.propertiesListView.View = System.Windows.Forms.View.Details;
            this.propertiesListView.ItemActivate += new System.EventHandler(this.propertiesListView_ItemActivate);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.installTabPage);
            this.mainTabControl.Controls.Add(this.settingsTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(685, 431);
            this.mainTabControl.TabIndex = 5;
            this.mainTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.mainTabControl_Selecting);
            // 
            // installTabPage
            // 
            this.installTabPage.Controls.Add(this.installationStatusLabel);
            this.installTabPage.Controls.Add(this.installUninstallButton);
            this.installTabPage.Controls.Add(this.currentInstallationStatusLabel);
            this.installTabPage.Location = new System.Drawing.Point(4, 22);
            this.installTabPage.Name = "installTabPage";
            this.installTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.installTabPage.Size = new System.Drawing.Size(677, 405);
            this.installTabPage.TabIndex = 0;
            this.installTabPage.Text = "Installation";
            this.installTabPage.UseVisualStyleBackColor = true;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.saveButton);
            this.settingsTabPage.Controls.Add(this.importButton);
            this.settingsTabPage.Controls.Add(this.exportButton);
            this.settingsTabPage.Controls.Add(this.editButton);
            this.settingsTabPage.Controls.Add(this.deleteButton);
            this.settingsTabPage.Controls.Add(this.newValuesGroupBox);
            this.settingsTabPage.Controls.Add(this.groupBoxClassProperties);
            this.settingsTabPage.Controls.Add(this.classesGroupBox);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(677, 405);
            this.settingsTabPage.TabIndex = 1;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(590, 375);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(80, 23);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(590, 316);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(80, 23);
            this.importButton.TabIndex = 9;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(590, 345);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(80, 23);
            this.exportButton.TabIndex = 8;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(590, 258);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(80, 23);
            this.editButton.TabIndex = 7;
            this.editButton.Text = "Edit Value";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(590, 287);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(80, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete Value";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // newValuesGroupBox
            // 
            this.newValuesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newValuesGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.newValuesGroupBox.Controls.Add(this.newValuesListView);
            this.newValuesGroupBox.Location = new System.Drawing.Point(6, 237);
            this.newValuesGroupBox.Name = "newValuesGroupBox";
            this.newValuesGroupBox.Size = new System.Drawing.Size(578, 163);
            this.newValuesGroupBox.TabIndex = 5;
            this.newValuesGroupBox.TabStop = false;
            this.newValuesGroupBox.Text = "New Values";
            // 
            // newValuesListView
            // 
            this.newValuesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.classColumnHeader,
            this.propertyColumnHeader,
            this.newValueColumnHeader});
            this.newValuesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newValuesListView.FullRowSelect = true;
            this.newValuesListView.HideSelection = false;
            this.newValuesListView.Location = new System.Drawing.Point(3, 16);
            this.newValuesListView.MultiSelect = false;
            this.newValuesListView.Name = "newValuesListView";
            this.newValuesListView.ShowItemToolTips = true;
            this.newValuesListView.Size = new System.Drawing.Size(572, 144);
            this.newValuesListView.TabIndex = 0;
            this.newValuesListView.UseCompatibleStateImageBehavior = false;
            this.newValuesListView.View = System.Windows.Forms.View.Details;
            // 
            // classColumnHeader
            // 
            this.classColumnHeader.Text = "Class";
            this.classColumnHeader.Width = 37;
            // 
            // propertyColumnHeader
            // 
            this.propertyColumnHeader.Text = "Property";
            this.propertyColumnHeader.Width = 51;
            // 
            // newValueColumnHeader
            // 
            this.newValueColumnHeader.Text = "New Value";
            this.newValueColumnHeader.Width = 483;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 455);
            this.Controls.Add(this.mainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WMI Spoofer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.classesGroupBox.ResumeLayout(false);
            this.groupBoxClassProperties.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.installTabPage.ResumeLayout(false);
            this.installTabPage.PerformLayout();
            this.settingsTabPage.ResumeLayout(false);
            this.newValuesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label installationStatusLabel;
        private System.Windows.Forms.Label currentInstallationStatusLabel;
        private System.Windows.Forms.Button installUninstallButton;
        private System.Windows.Forms.GroupBox classesGroupBox;
        private System.Windows.Forms.ListView classesListView;
        private System.Windows.Forms.GroupBox groupBoxClassProperties;
        private System.Windows.Forms.ListView propertiesListView;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage installTabPage;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.GroupBox newValuesGroupBox;
        private System.Windows.Forms.ListView newValuesListView;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ColumnHeader classColumnHeader;
        private System.Windows.Forms.ColumnHeader propertyColumnHeader;
        private System.Windows.Forms.ColumnHeader newValueColumnHeader;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;
    }
}

