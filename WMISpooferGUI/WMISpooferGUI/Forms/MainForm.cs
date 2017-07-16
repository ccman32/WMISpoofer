using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Management;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using WMISpooferGUI.Model;

namespace WMISpooferGUI
{
    public partial class MainForm : Form
    {
        private bool isInstalled = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void updateInstallationStatus()
        {
            isInstalled = Installer.IsInstalled();
            currentInstallationStatusLabel.ForeColor = isInstalled ? Color.Lime : Color.Red;
            currentInstallationStatusLabel.Text = isInstalled ? "Installed" : "Not installed";
            installUninstallButton.Text = isInstalled ? "Uninstall" : "Install";
        }

        private void loadClasses()
        {
            List<ListViewItem> items = new List<ListViewItem>();
            WMIInterface.GetClasses(ref items);
            classesListView.Columns.Add("Name", -2, HorizontalAlignment.Left);
            classesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            classesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            classesListView.BeginUpdate();
            classesListView.Items.AddRange(items.ToArray());
            classesListView.Sort();
            classesListView.EndUpdate();

            if (classesListView.SelectedItems.Count > 0)
                classesListView.EnsureVisible(classesListView.SelectedItems[0].Index);
        }

        private void addNewItem(string className, string propertyName, string newValue)
        {
            foreach (ListViewItem existingItem in newValuesListView.Items)
            {
                if (existingItem.Text == className && existingItem.Name == propertyName)
                {
                    if (MessageBox.Show(
                        string.Format("The entry \"{0} - {1}\" already exists in the \"New Values\" list and has the value \"{2}\"! Do you want to overwrite it with the new value \"{3}\"?",
                            className,
                            propertyName,
                            existingItem.SubItems[2].Text,
                            newValue
                        ),
                        "Entry already exists",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        editItem(existingItem, newValue);

                        return;
                    }
                }
            }

            newValuesListView.BeginUpdate();

            ListViewItem item = new ListViewItem
            {
                Name = propertyName,
                Text = className
            };

            item.SubItems.Add(propertyName);
            item.SubItems.Add(newValue);
            newValuesListView.Items.Add(item);
            newValuesListView.Sort();
            newValuesListView.EndUpdate();
            newValuesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            newValuesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void editItem(ListViewItem itemToEdit, string newValue)
        {
            if (itemToEdit != null && newValue.Length > 0)
            {
                newValuesListView.BeginUpdate();
                itemToEdit.SubItems[2].Text = newValue;
                newValuesListView.Sort();
                newValuesListView.EndUpdate();
                newValuesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                newValuesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void deleteItem(ListViewItem itemToDelete)
        {
            if (itemToDelete != null)
            {
                newValuesListView.BeginUpdate();
                newValuesListView.Items.Remove(itemToDelete);
                newValuesListView.Sort();
                newValuesListView.EndUpdate();
                newValuesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                newValuesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void importItems(string path)
        {
            if (newValuesListView.Items.Count > 0
                    && MessageBox.Show("There are already entries in the \"New Values\" list! Do you want to delete them before importing the entries from the file?",
                    "Import",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                newValuesListView.Items.Clear();
            }

            foreach (string section in IniInterface.ReadSections(path))
            {
                string[] keyValuePairs = IniInterface.ReadKeyValuePairs(section, path);

                foreach (string keyValuePair in keyValuePairs)
                {
                    string[] keyAndValue = keyValuePair.Split('=');
                    addNewItem(section, keyAndValue[0], keyAndValue[1]);
                }
            }
        }

        private void exportItems(string path, bool overwrite)
        {
            if (overwrite && File.Exists(path))
            {
                File.Delete(path);
            }

            foreach (ListViewItem item in newValuesListView.Items)
            {
                IniInterface.WriteValue(item.Text, item.Name, item.SubItems[2].Text, path);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!(new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show("Please start this program as administrator!", "Missing administrator privileges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();

                return;
            }

            updateInstallationStatus();
            loadClasses();

            if (isInstalled)
            {
                importItems(Installer.GetInstallationIniFileName());
            }
        }

        private void installUninstallButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isInstalled)
                {
                    Installer.Uninstall();
                }
                else
                {
                    Installer.Install();
                }

                updateInstallationStatus();

                if (isInstalled
                    && MessageBox.Show("WMI Spoofer needs to be configured before it can be used. Do you want to go to the settings to configure it?",
                    "WMI Spoofer not configured",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mainTabControl.SelectTab(settingsTabPage);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("An error occurred while {0} WMI Spoofer: {1}", isInstalled ? "uninstalling" : "installing", ex.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mainTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == settingsTabPage && !isInstalled)
            {
                mainTabControl.SelectTab(installTabPage);
                MessageBox.Show("You need to install the program before you can open the program settings!", "WMI Spoofer not installed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void classesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (classesListView.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem currentItem = classesListView.SelectedItems[0];

            if (currentItem == null)
            {
                return;
            }

            WMIClass currentWmiClass = currentItem.Tag as WMIClass;
            propertiesListView.Clear();
            propertiesListView.Columns.Add("Property Name", -2, HorizontalAlignment.Left);
            propertiesListView.Columns.Add("Current Value (First instance)", -2, HorizontalAlignment.Left);
            propertiesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            propertiesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            propertiesListView.BeginUpdate();

            LoadingForm loadingWindow = new LoadingForm();
            loadingWindow.Show(this);

            ManagementObjectCollection instances = currentWmiClass.Class.GetInstances();

            foreach (PropertyData property in currentWmiClass.Class.Properties)
            {
                if (property.Type == CimType.String)
                {
                    string propName = property.Name;
                    string propDesc = String.Empty;
                    List<string> propValues = new List<string>();

                    try
                    {
                        foreach (ManagementObject c in instances)
                        {
                            object value = c.Properties[property.Name.ToString()].Value;

                            if (value != null)
                            {
                                propValues.Add(value.ToString());
                            }

                            break;
                        }
                    }
                    catch
                    {
                    }

                    foreach (QualifierData qualifier in property.Qualifiers)
                    {
                        if (qualifier.Name.Equals("Description", StringComparison.CurrentCultureIgnoreCase))
                            propDesc = qualifier.Value.ToString();
                    }

                    ListViewItem listItem = new ListViewItem
                    {
                        Name = propName,
                        Text = propName,
                        ToolTipText = propDesc,
                        Tag = property
                    };

                    listItem.SubItems.Add(string.Join(" | ", propValues));
                    propertiesListView.Items.Add(listItem);
                }
            }

            loadingWindow.Close();
            propertiesListView.Sorting = SortOrder.Ascending;
            propertiesListView.Sort();
            propertiesListView.EndUpdate();
        }

        private void propertiesListView_ItemActivate(object sender, EventArgs e)
        {
            if (propertiesListView.SelectedItems.Count == 0)
            {
                return;
            }

            string propertyName = propertiesListView.SelectedItems[0].Text;
            string newValue = Interaction.InputBox("Please enter the new value that you want for this property.", propertyName, propertiesListView.SelectedItems[0].SubItems[1].Text);

            if (newValue.Length > 0)
            {
                ListViewItem currentItem = classesListView.SelectedItems[0];

                if (currentItem == null)
                {
                    return;
                }

                WMIClass currentWmiClass = currentItem.Tag as WMIClass;
                addNewItem(currentWmiClass.DisplayName, propertyName, newValue);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (newValuesListView.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem currentItem = newValuesListView.SelectedItems[0];
            string propertyName = currentItem.SubItems[1].Text;
            string oldValue = currentItem.SubItems[2].Text;
            string newValue = Interaction.InputBox("Please enter the new value that you want for this property.", propertyName, oldValue);

            if (newValue.Length > 0)
            {
                editItem(currentItem, newValue);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (newValuesListView.SelectedItems.Count == 0)
            {
                return;
            }

            deleteItem(newValuesListView.SelectedItems[0]);
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog importOpenFileDialog = new OpenFileDialog();
            importOpenFileDialog.DefaultExt = "ini";
            importOpenFileDialog.Filter = "WMI Spoofer Configuration File (*.ini)|*.ini";

            if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                importItems(importOpenFileDialog.FileName);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportSaveFileDialog = new SaveFileDialog();
            exportSaveFileDialog.DefaultExt = "ini";
            exportSaveFileDialog.Filter = "WMI Spoofer Configuration File (*.ini)|*.ini";

            if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                exportItems(exportSaveFileDialog.FileName, true);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            exportItems(Installer.GetInstallationIniFileName(), true);

            MessageBox.Show("Successfully saved the settings!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            if (MessageBox.Show("It is recommended to restart your computer for the new settings to be applied to all running programs.\nDo you want to restart your computer now?",
                "WMI Spoofer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Installer.RestartComputer();
            }
        }
    }
}
