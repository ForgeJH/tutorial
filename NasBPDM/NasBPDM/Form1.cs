using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NasBPDM
{
    public partial class Form1 : Form
    {
        private BackgroundWorker bkWorker = new BackgroundWorker();
        private Button clickBtn;
        private string resultText=string.Empty;


        public Form1()
        {
            InitializeComponent();

            InitEvent();
            InitControl();
        }

        private void InitEvent()
        {
            btnUpdate.Click +=btn_Click;
            btnIn.Click += btn_Click;
            btnOut.Click += btn_Click;

            btnLoad.Click += btnLoad_Click;
            btnSave.Click += btnSave_Click;

            bkWorker.DoWork += bkWorker_DoWork;
            bkWorker.RunWorkerCompleted += bkWorker_RunWorkerCompleted;
            bkWorker.WorkerSupportsCancellation = false;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            IniHelper.WriteIni(IniHelper.Section.Day, DateTime.Today.ToString("yyyyMMdd"), NasBuild.Instance()._dayCount.ToString());
            IniHelper.WriteIni(IniHelper.Section.Path, IniHelper.Key.BatPath, txtBatPath.Text);
            IniHelper.WriteIni(IniHelper.Section.Path, IniHelper.Key.DevPath, txtDevPath.Text);
            IniHelper.WriteIni(IniHelper.Section.Path, IniHelper.Key.TFPath, txtTFPath.Text);
            IniHelper.WriteIni(IniHelper.Section.Path, IniHelper.Key.TFSPath, txtTFSPath.Text);
        }

        void btnLoad_Click(object sender, EventArgs e)
        {
            txtTFSPath.Text = IniHelper.ReadIni(IniHelper.Section.Path, IniHelper.Key.TFSPath);
            txtTFPath.Text = IniHelper.ReadIni(IniHelper.Section.Path, IniHelper.Key.TFPath);
            txtDevPath.Text = IniHelper.ReadIni(IniHelper.Section.Path, IniHelper.Key.DevPath);
            txtBatPath.Text = IniHelper.ReadIni(IniHelper.Section.Path, IniHelper.Key.BatPath);
        }

        private void InitControl()
        {
            if (string.IsNullOrEmpty(txtTFSPath.Text.Trim()))
                txtTFSPath.Text = @"D:\tfs";
            if (string.IsNullOrEmpty(txtTFPath.Text.Trim()))
                txtTFPath.Text = @"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE";
            if (string.IsNullOrEmpty(txtDevPath.Text.Trim()))
                txtDevPath.Text = @"D:\tfs\NeoHowMuch2-내부\NeoHowMuch2\bin\dev";
            if (string.IsNullOrEmpty(txtBatPath.Text.Trim()))
                txtBatPath.Text = @"D:\tfs\NeoHowMuch2-내부\NeoHowMuch2\bin";
        }

        private void SetNasBuild()
        {   
            NasBuild.Instance()._tfsPath = txtTFSPath.Text;
            NasBuild.Instance()._tfPath = txtTFPath.Text;
            NasBuild.Instance()._devPath = txtDevPath.Text;
            NasBuild.Instance()._batPath = txtBatPath.Text;

            NasBuild.Instance().SetNasPath();
            
            NasBuild.Instance()._updateVersion = FileHelper.GetUpdateVsersion(
                NasBuild.Instance()._csprojPath);
            NasBuild.Instance()._fileVersion = FileHelper.GetApplicationVersion(
                NasBuild.Instance()._releasePath);
        }

        protected override void OnShown(EventArgs e)
        {
            SetNasBuild();
        }

        private void SaveIni()
        {
            IniHelper.WriteIni(IniHelper.Section.Path, IniHelper.Key.TFSPath, txtTFSPath.Text);
            IniHelper.WriteIni(IniHelper.Section.Path, IniHelper.Key.TFPath, txtTFPath.Text);
        }

        private void DisplayResult(string result)
        {
            txtResult.Text = string.Format("{0}\r\n{1}\r\n-----------------", txtResult.Text, result);
        }

        private void btnEventStart(bool isStart)
        {
            if (isStart)
                btnLoad.Enabled = btnSave.Enabled = btnIn.Enabled = btnOut.Enabled = btnUpdate.Enabled = false;
            else
                btnLoad.Enabled = btnSave.Enabled = btnIn.Enabled = btnOut.Enabled = btnUpdate.Enabled = true;
        }

        void btn_Click(object sender, EventArgs e)
        {
            SetNasBuild();
            txtResult.Text = "";
            btnEventStart(true);
            clickBtn = (Button)sender;

            bkWorker.RunWorkerAsync();
        }

        void bkWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnEventStart(false);
            if (e.Result == null)
                return;

            DisplayResult(e.Result.ToString());
            labNXCOMPAT.Text = (e.Result.ToString().IndexOf("NXCOMPAT-SUCCESS") > 0) ? "NXCOMPAT : 성공" : "NXCOMPAT : ";
            labLARGEADDRESSAWARE.Text = (e.Result.ToString().IndexOf("LARGEADDRESSAWARE-SUCCESS") > 0) ? "LARGEADDRESSAWARE : 성공" : "LARGEADDRESSAWARE : ";
        }

        void bkWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            btnEventStart(true);

            #region btnUpdate
            if (clickBtn.Equals(btnUpdate))
            {
                var result = FileHelper.RunCommandLine(
                    FileHelper.CommandType.일반
                    , string.Format("\"{0}\\TF.exe\" get \"{1}\\NeoHowMuch2-내부\" /recursive"
                    , NasBuild.Instance()._tfPath
                    , NasBuild.Instance()._tfsPath));
                e.Result = result;
            }
            #endregion
            #region btnOut
            else if (clickBtn.Equals(btnOut))
            {
                string releaseVsersion = string.Format("{0}{1}_{2}"
                    , NasBuild.Instance()._fileVersion[2]
                    , NasBuild.Instance()._fileVersion[3]
                    , NasBuild.Instance()._updateVersion);
                string targetFolder = string.Format("{0}_{1}"
                    , NasBuild.Instance()._releasePath
                    ,releaseVsersion);

                var diSource = new DirectoryInfo(NasBuild.Instance()._releasePath);
                var diTarget = new DirectoryInfo(targetFolder);
                var diDev = new DirectoryInfo(NasBuild.Instance()._devPath);

                FileHelper.DeepCopy(diSource, diTarget);
                resultText = "---------Release 파일 복사완료";
                resultText = string.Format(FileHelper.DeleteAppFilder(diTarget), resultText);
                resultText = string.Format(FileHelper.DeleteHostFile(diTarget), resultText);
                resultText = string.Format(FileHelper.EditBatFile(NasBuild.Instance()._batPath, releaseVsersion), resultText);

                FileHelper.DeepCopy(diDev, diTarget);
                resultText = string.Format("{0}\r\n---------Dev 파일 복사완료", resultText);

                if (SystemInformation.ComputerName != "PC156")
                {
                    resultText = string.Format("{0}\r\n{1}", resultText
                        ,FileHelper.RunCommandLine(FileHelper.CommandType.NXCOMPAT, targetFolder));
                    resultText = string.Format("{0}\r\n{1}", resultText
                        ,FileHelper.RunCommandLine(FileHelper.CommandType.LARGEADDRESSAWARE, targetFolder));
                }
                Process.Start(string.Format(@"{0}\test_{1}.bat", NasBuild.Instance()._batPath, releaseVsersion));

                string resultMsg = string.Format("2E NAS 외부 업데이트완료{0}({1})"
                , DateTime.Now.ToString("yyyyMMdd")
                , string.Format("v{0}.{1}", NasBuild.Instance()._fileVersion[2], NasBuild.Instance()._fileVersion[3]));

                e.Result = string.Format("{0}\r\n\r\n{1}", resultText, resultMsg);
            }
            #endregion
            #region btnIn
            else if (clickBtn.Equals(btnIn))
            {
                //변경이력 체크인
                FileHelper.RunCommandLine(
                    FileHelper.CommandType.일반
                    , string.Format("\"{0}\\TF.exe\" checkin \"{1}\" /recursive /comment:\"{2}_{3}\""
                        , NasBuild.Instance()._tfPath
                        , string.Format(@"{0}\NeoHowMuch2-내부", NasBuild.Instance()._tfsPath)
                        , DateTime.Today.ToString("yyyyMMdd")
                        , DateTime.Now.ToString("HHMM")));

                //update 버전 수정
                FileHelper.EditCsproj(string.Format("{0}\\NeoHowMuch2-내부\\NeoHowMuch2", NasBuild.Instance()._tfsPath));

                //게시
                MessageBox.Show("파일을 게시 후 확인을 눌러주세요.");

                //버전정보 업데이트
                NasBuild.Instance()._updateVersion = FileHelper.GetUpdateVsersion(
                    NasBuild.Instance()._csprojPath);
                NasBuild.Instance()._fileVersion = FileHelper.GetApplicationVersion(
                    NasBuild.Instance()._releasePath);

                //수정된 update 버전 체크인
                string checkComment = string.Format("{0}{1}_{2}"
                    , NasBuild.Instance()._fileVersion[2]
                    , NasBuild.Instance()._fileVersion[3]
                    , NasBuild.Instance()._updateVersion);
                FileHelper.RunCommandLine(FileHelper.CommandType.일반
                    , string.Format("\"{0}\\TF.exe\" checkin \"{1}\" /recursive /comment:\"{2}\""
                        , NasBuild.Instance()._tfsPath
                        , string.Format(@"{0}\NeoHowMuch2-내부", NasBuild.Instance()._tfsPath)
                        , checkComment));

                //Label 생성
                FileHelper.RunCommandLine(FileHelper.CommandType.일반
                    , string.Format("\"{0}\\TF.exe\" label \"{1}\" \"{2}\" /comment:\"{1}\""
                    , NasBuild.Instance()._tfPath
                    , checkComment
                    , string.Format(@"{0}\NeoHowMuch2-내부", NasBuild.Instance()._tfsPath)));

                string resultMsg = string.Format("2E NAS 내부 업데이트완료{0}_{1}({2})"
                , DateTime.Now.ToString("yyyyMMdd")
                , NasBuild.Instance()._dayCount.ToString("00")
                , string.Format("v{0}.{1}", NasBuild.Instance()._fileVersion[2], NasBuild.Instance()._fileVersion[3]));

                e.Result = resultMsg;
            }
            #endregion
        }
    }
}
