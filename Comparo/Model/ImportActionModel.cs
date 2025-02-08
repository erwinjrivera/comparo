using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Model
{
    public class ImportActionModel
    {
        public string FileName { get; set; }

        public string Table { get; set; }

        public string OutputTextFile { get; set; }

        public ActionTypesEnum Action { get; set; }
    }
}
