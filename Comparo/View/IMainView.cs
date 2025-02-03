using Comparo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.View
{
    public interface IMainView
    {
        string ShowOpenFileDialog();

        void InitializeControls();

        void ShowFileImport(string fileName);

        IEnumerable<ImportActionModel> SourceImportActions { set; }

        IEnumerable<ImportActionModel> TargetImportActions { set; }
    }
}
