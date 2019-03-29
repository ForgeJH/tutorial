using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NasBPDM
{
    static class IniHelper
    {
        public enum Section
        {
            Day,
            Path
        }
        public enum Key
        {
            day,
            TFPath,
            TFSPath,
            CsprojPath,
            DevPath,
            BatPath
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        public static string ReadIni(Section section, Key key = Key.day)
        {
            StringBuilder temp = new StringBuilder(255);
            int ret = GetPrivateProfileString(
                section.ToString()
                , key == Key.day ? DateTime.Today.ToString("yyyyMMdd") : key.ToString()
                , section == Section.Day ? "1" : ""
                , temp
                , 255
                , string.Format(@"{0}\ini.ini", Application.StartupPath));
            return temp.ToString();
        }

        public static void WriteIni(Section section, Key key, string value)
        {
            WritePrivateProfileString(section.ToString(), key.ToString(), value, string.Format(@"{0}\ini.ini", Application.StartupPath));
        }

        public static void WriteIni(Section section, string key, string value)
        {
            WritePrivateProfileString(section.ToString(), key, value, string.Format(@"{0}\ini.ini", Application.StartupPath));
        }
    }
}
