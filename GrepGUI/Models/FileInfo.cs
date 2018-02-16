using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrepGUI.Models
{
    public class FileInfo : BindableBase
    {
        private string _FileName;

        public string FileName
        {
            get { return _FileName; }
            set { SetProperty(ref _FileName, value); }
        }
        private string _FilePath;

        
        public string FilePath
        {
            get { return _FilePath; }
            set { SetProperty(ref _FilePath, value); }
        }

        private uint _Line;
        public uint Line
        {
            get { return _Line; }
            set { SetProperty(ref _Line, value); }
        }
        private string _String;

        public string String
        {
            get { return _String; }
            set { SetProperty(ref _String, value); }
        }
    }
}
