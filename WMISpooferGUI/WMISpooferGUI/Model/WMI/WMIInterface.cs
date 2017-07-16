using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace WMISpooferGUI.Model
{
    class WMIInterface
    {
        public static void GetClasses(ref List<ListViewItem> items)
        {
            ManagementPath cimv2path = new ManagementPath(string.Format("\\\\{0}\\ROOT\\CIMV2", SystemInformation.ComputerName.ToUpperInvariant()));

            ConnectionOptions connection = new ConnectionOptions
            {
                EnablePrivileges = true,
                Impersonation = ImpersonationLevel.Impersonate,
                Authentication = AuthenticationLevel.Default,
                Username = null,
                SecurePassword = null
            };

            ManagementScope scope = new ManagementScope(cimv2path, connection);
            scope.Connect();

            ObjectGetOptions options = new ObjectGetOptions();
            ManagementObject cimv2namespace = new ManagementObject(scope, cimv2path, options);

            ManagementScope cimv2scope = new ManagementScope(cimv2namespace.Path, connection);
            ObjectQuery query = new ObjectQuery("SELECT * FROM meta_class WHERE __Class LIKE \"%%%\" AND NOT __Class LIKE \"[_][_]%\" AND NOT __Class LIKE \"Win32_Perf%\" AND NOT __Class LIKE \"MSFT[_]%\"");

            EnumerationOptions eOption = new EnumerationOptions
            {
                EnumerateDeep = true,
                UseAmendedQualifiers = true
            };

            ManagementObjectSearcher queryAllClasses = new ManagementObjectSearcher(scope, query, eOption);
            List<WMIClass> classes = new List<WMIClass>();

            foreach (ManagementClass currentClass in (from ManagementClass currentClass in queryAllClasses.Get()
                                                orderby currentClass.ClassPath.ClassName
                                                select currentClass))
            {
                bool stringPropertyFound = false;

                foreach (PropertyData property in currentClass.Properties)
                {
                    if (property.Type == CimType.String)
                    {
                        stringPropertyFound = true;
                    }
                }

                if (
                    stringPropertyFound
                    )
                {
                    WMIClass newClass = new WMIClass(currentClass);

                    ListViewItem item = new ListViewItem
                    {
                        Name = newClass.DisplayName,
                        Text = newClass.DisplayName,
                        ToolTipText = newClass.Description,
                        Tag = newClass
                    };

                    item.SubItems.Add(newClass.Description);
                    items.Add(item);
                }
            }
        }
    }
}
