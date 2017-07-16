using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WMISpooferGUI.Model
{
    public static class IniInterface
    {
        public static int capacity = 16384;

        public static bool WriteValue(string section, string key, string value, string filePath)
        {
            bool result = NativeMethods.WritePrivateProfileString(section, key, value, filePath);
            return result;
        }

        public static bool DeleteSection(string section, string filepath)
        {
            bool result = NativeMethods.WritePrivateProfileString(section, null, null, filepath);
            return result;
        }

        public static bool DeleteKey(string section, string key, string filepath)
        {
            bool result = NativeMethods.WritePrivateProfileString(section, key, null, filepath);
            return result;
        }

        public static string ReadValue(string section, string key, string filePath, string defaultValue = "")
        {
            var value = new StringBuilder(capacity);
            NativeMethods.GetPrivateProfileString(section, key, defaultValue, value, value.Capacity, filePath);
            return value.ToString();
        }

        public static string[] ReadSections(string filePath)
        {
            while (true)
            {
                char[] chars = new char[capacity];
                int size = NativeMethods.GetPrivateProfileString(null, null, "", chars, capacity, filePath);

                if (size == 0)
                {
                    return null;
                }

                if (size < capacity - 2)
                {
                    string result = new String(chars, 0, size);
                    string[] sections = result.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                    return sections;
                }

                capacity *= 2;
            }
        }

        public static string[] ReadKeys(string section, string filePath)
        {
            while (true)
            {
                char[] chars = new char[capacity];
                int size = NativeMethods.GetPrivateProfileString(section, null, "", chars, capacity, filePath);

                if (size == 0)
                {
                    return null;
                }

                if (size < capacity - 2)
                {
                    string result = new String(chars, 0, size);
                    string[] keys = result.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                    return keys;
                }

                capacity *= 2;
            }
        }

        public static string[] ReadKeyValuePairs(string section, string filePath)
        {
            while (true)
            {
                IntPtr returnedString = Marshal.AllocCoTaskMem(capacity * sizeof(char));
                int size = NativeMethods.GetPrivateProfileSection(section, returnedString, capacity, filePath);

                if (size == 0)
                {
                    Marshal.FreeCoTaskMem(returnedString);
                    return null;
                }

                if (size < capacity - 2)
                {
                    string result = Marshal.PtrToStringAuto(returnedString, size - 1);
                    Marshal.FreeCoTaskMem(returnedString);
                    string[] keyValuePairs = result.Split('\0');
                    return keyValuePairs;
                }

                Marshal.FreeCoTaskMem(returnedString);
                capacity *= 2;
            }
        }
    }
}
