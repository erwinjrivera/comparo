using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Model
{
    public class DataFileModel
    {
        private string _fileName;

        public FileTypeEnum FileType { get; set; }

        public List<string> Tables { get; set; }

        //public string SelectedTable { get; set; }

        public string FileName
        {
            get { return _fileName; }
            set 
            {
                _fileName = value;

                SetFileType();
            }
        }
        
        private void SetFileType()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                FileType = FileTypeEnum.Unsupported;

            string extension = Path.GetExtension(FileName).ToLower();

            switch (extension)
            {
                case ".xls":
                case ".xlsx":
                    FileType = FileTypeEnum.Excel;
                    break;
                case ".csv":
                    FileType = FileTypeEnum.CSV;
                    //SelectedTable = Path.GetFileName(FileName);
                    Tables = new List<string> { Path.GetFileNameWithoutExtension(FileName) };
                    break;
                case ".txt":
                case ".tsv":
                    FileType = FileTypeEnum.TabDelimited;
                    //SelectedTable = Path.GetFileName(FileName);
                    Tables = new List<string> { Path.GetFileNameWithoutExtension(FileName) };
                    break;
                default:
                    FileType = FileTypeEnum.Unsupported; // unknown
                    break;
            }
        }

    }
}
