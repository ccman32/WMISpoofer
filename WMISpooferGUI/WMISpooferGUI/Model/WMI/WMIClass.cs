using System;
using System.Linq;
using System.Management;

namespace WMISpooferGUI.Model
{
    class WMIClass
    {
        private string description;
        private string displayName;

        public WMIClass(ManagementClass actualClass)
        {
            Class = actualClass;
        }

        public ManagementClass Class { get; set; }

        public string DisplayName
        {
            get
            {
                if (displayName == null)
                    displayName = Class.ClassPath.ClassName;

                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        public string Description
        {
            get
            {
                try
                {
                    foreach (QualifierData qualifier in from QualifierData qualifier in Class.Qualifiers where qualifier.Name.Equals("Description", StringComparison.CurrentCultureIgnoreCase) select qualifier)
                    {
                        description = Class.GetQualifierValue("Description").ToString();
                    }
                }
                catch (ManagementException ex)
                {
                    if ((ex.ErrorCode).ToString() == "NotFound")
                        description = string.Empty;
                    else
                        description = "Failed to get the Class Description";
                }

                return description;
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
