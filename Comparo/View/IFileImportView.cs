using Comparo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.View
{
    public interface IFileImportView
    {
        string FileName { set; }

        string StatusText { set; }

        void ShowLoading(bool isLoading, string message = "Loading...");

        void InitializeControls();

        List<ImportActionModel> ImportActions { set; }
    }
}
