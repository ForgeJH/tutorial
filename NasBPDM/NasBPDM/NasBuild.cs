using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NasBPDM
{
    class NasBuild
    {
        private static NasBuild _instance;
        public string _updateVersion { get; set; }
        public List<string> _fileVersion { get; set; }

        public string _tfsPath { get; set; }
        public string _tfPath { get; set; }
        public string _releasePath { get; set; }
        public string _csprojPath { get; set; }
        public string _devPath { get; set; }
        public string _batPath { get; set; }
                
        public string _sourceFolder { get; set; }
        public int _dayCount { get; set; }
        

        protected NasBuild()
        {  
        }

        public static NasBuild Instance()
        {
            if (_instance == null)
                _instance = new NasBuild();

            return _instance;
        }

        public void SetNasPath(string tfsPath ="")
        {
            if (string.IsNullOrEmpty(tfsPath))
                tfsPath = _tfsPath;

            _releasePath = string.Format("{0}\\NeoHowMuch2-내부\\NeoHowMuch2\\bin\\Release", tfsPath);
            _csprojPath = string.Format("{0}\\NeoHowMuch2-내부\\NeoHowMuch2", tfsPath);
        }
    }
}
