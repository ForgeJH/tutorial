using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NasBPDM
{
    static class FileHelper
    {
        public enum CommandType
        {
            코드사인,
            NXCOMPAT,
            LARGEADDRESSAWARE,
            일반
        }

        public static void DeepCopy(DirectoryInfo sourcePath, DirectoryInfo targetPath)
        {
            Directory.CreateDirectory(targetPath.FullName);
            foreach (var fi in sourcePath.GetFiles())
            {
                fi.CopyTo(Path.Combine(targetPath.FullName, fi.Name), true);
            }
            foreach (var diSourceSubDir in sourcePath.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    targetPath.CreateSubdirectory(diSourceSubDir.Name);
                DeepCopy(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static string DeleteHostFile(DirectoryInfo sourcePath)
        {
            try
            {
                foreach (var fi in sourcePath.GetFiles())
                {
                    if (fi.Name.IndexOf("vshost") > 0)
                        fi.Delete();
                }
                return "{0}---------host 파일 삭제완료\r\n";
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return "{0}---------host 파일 삭제실패\r\n";
            }
        }

        public static string DeleteAppFilder(DirectoryInfo sourcePath)
        {
            foreach (var dr in sourcePath.GetDirectories())
            {
                if (dr.Name.StartsWith("app."))
                {
                    try
                    {
                        dr.Delete();
                    }
                    catch (Exception ex)
                    {
                        ExceptionMessage(ex);
                        return "{0}---------app.publish 폴더 삭제실패\r\n";
                    }
                    return "{0}---------app.publish 폴더 삭제완료\r\n";
                }
            }
            return "";
        }

        public static string EditBatFile(string batFolder, string version)
        {
            try
            {
                string line = string.Empty;
                using (StreamReader reader = new StreamReader(string.Format(@"{0}\test.bat", batFolder), Encoding.Default))
                {
                    using (StreamWriter writer = new StreamWriter(
                        string.Format(@"{0}\test_{1}.bat", batFolder, version)
                        , false
                        , Encoding.Default))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.IndexOf("Release_") > 0)
                            {
                                line = line.Replace("Release_", string.Format("Release_{0}", version));
                            }
                            writer.WriteLine(line);
                        }
                    }
                }
                return "{0}---------Bat 파일 수정완료\r\n";
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
                return "{0}---------Bat 파일 수정실패\r\n";
            }
        }

        public static string GetUpdateVsersion(string csprojFolder, string csprojFileName = "NeoHowMuch2.csproj")
        {
            try
            {
                string path = string.Format(@"{0}\{1}", csprojFolder, csprojFileName);
                string[] allLine = File.ReadAllLines(path);
                for (int i = 0; i < allLine.Count(); i++)
                {
                    var line = allLine[i];
                    if (line.IndexOf("MinimumRequiredVersion") > 0 || line.IndexOf("ApplicationVersion") > 0)
                    {
                        var version = line.Split('.');
                        var newVersion = (Convert.ToInt32(version[2])).ToString();
                        allLine[i] = string.Format("{0}.{1}.{2}.{3}", version[0], version[1], newVersion, version[3]);

                        return newVersion;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
            return "";
        }

        public static string EditCsproj(string csprojFolder, string csprojFileName = "NeoHowMuch2.csproj")
        {
            try
            {
                string path = string.Format(@"{0}\{1}", csprojFolder, csprojFileName);

                string[] allLine = File.ReadAllLines(path);
                for (int i = 0; i < allLine.Count(); i++)
                {
                    var line = allLine[i];
                    if (line.IndexOf("MinimumRequiredVersion") > 0 || line.IndexOf("ApplicationVersion") > 0)
                    {
                        var version = line.Split('.');
                        var newVersion = (Convert.ToInt32(version[2]) + 1).ToString();
                        allLine[i] = string.Format("{0}.{1}.{2}.{3}", version[0], version[1], newVersion, version[3]);

                        NasBuild.Instance()._updateVersion = newVersion;
                    }
                }

                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var line in allLine)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "csproj파일버전 수정";
        }

        public static string RunCommandLine(CommandType Type, string exePathorCommandLine)
        {
            string commandline = string.Empty;
            string vsvars32Command = "call \"C:\\Program Files (x86)\\Microsoft Visual Studio 11.0\\Common7\\Tools\\vsvars32.bat\"";

            switch (Type)
            {
                case CommandType.코드사인:
                    commandline = string.Format(@"D:\00.Code\Code\signtool sign /fd SHA256 /a /f D:\01.tfs\Files\iquest.co.kr.pfx /t http://timestamp.verisign.com/scripts/timstamp.dll /p dwc2534 {0}\NeoHowMuch2.exe", exePathorCommandLine);
                    break;
                case CommandType.LARGEADDRESSAWARE:
                    commandline = string.Format(@"editbin.exe /LARGEADDRESSAWARE {0}\NeoHowMuch2.exe", exePathorCommandLine);
                    break;
                case CommandType.NXCOMPAT:
                    commandline = string.Format(@"editbin.exe /NXCOMPAT:NO {0}\NeoHowMuch2.exe", exePathorCommandLine);
                    break;
                case CommandType.일반:
                    commandline = exePathorCommandLine;
                    break;
            }

            try
            {
                ProcessStartInfo cmd = new ProcessStartInfo();
                Process process = new Process();

                cmd.FileName = @"cmd";
                cmd.WindowStyle = ProcessWindowStyle.Hidden;
                cmd.CreateNoWindow = true;
                cmd.UseShellExecute = false;

                cmd.RedirectStandardOutput = true;
                cmd.RedirectStandardInput = true;
                cmd.RedirectStandardError = true;

                process.EnableRaisingEvents = false;
                process.StartInfo = cmd;
                process.Start();

                if (Type == CommandType.NXCOMPAT || Type == CommandType.LARGEADDRESSAWARE)
                    process.StandardInput.Write(vsvars32Command + Environment.NewLine);
                process.StandardInput.Write(commandline + Environment.NewLine);

                process.StandardInput.Close();

                process.WaitForExit();
                string result = process.StandardOutput.ReadToEnd();
                process.Close();

                if (Type == CommandType.NXCOMPAT
                    && result.ToUpper().IndexOf("MICROSOFT (R) COFF/PE EDITOR VERSION") > 0
                    && result.ToUpper().IndexOf("ERROR") < 0)
                    return "NXCOMPAT-SUCCESS";
                else if (Type == CommandType.LARGEADDRESSAWARE
                    && result.ToUpper().IndexOf("MICROSOFT (R) COFF/PE EDITOR VERSION") > 0
                    && result.ToUpper().IndexOf("ERROR") < 0)
                    return "LARGEADDRESSAWARE-SUCCESS";

                return result;
            }
            catch (Exception ex)
            {
                return string.Format("에러 - {0}\r\n{1}", ex.ToString(), ex.InnerException.ToString());
            }
        }

        private static void ExceptionMessage(Exception ex)
        {
            if (ex.InnerException != null)
                MessageBox.Show(ex.InnerException.Message);
            else
                MessageBox.Show(ex.Message);
        }

        public static List<string> GetApplicationVersion(string releaseFolder)
        {
            string releasePath = string.Format(@"{0}\{1}", releaseFolder, "NeoHowMuch2.exe");
            string fileVersion= FileVersionInfo.GetVersionInfo(releasePath).FileVersion.ToString();

            if (fileVersion.IndexOf(".") > 0)
                return (fileVersion.Split('.')).ToList();
            else
                return null;
        }
    }
}
