using Comparo.Model;
using Comparo.Service;
using Comparo.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Controller
{
    public class FileImportController
    {
        private IFileImportView _view;
        private ImportActionCollectionModel _model;
        private IDataExtractorService _service;
        //private List<ImportActionModel> _importActions;

        public FileImportController(IFileImportView view, ImportActionCollectionModel model, IDataExtractorService service)
        {
            _view = view;
            _model = model;    
            _service = service;
        }

        public void Initialize()
        {
            _view.InitializeControls();
            _view.FileName = _model.DataFile.FileName;
            

            GetWorksheets();
        }

        public bool ValidateImportActions()
        {
            if (_model == null || _model.All(x => x.Action == ActionTypeEnum.Skip))
            {
                _view.StatusText = "No import action selected";
            }
            else
            {
                _view.StatusText = string.Empty;

                // if excel, convert to text

                ImportFile();

                return true;
            }

            return false;
        }

        private async void ImportFile()
        {
            var toBeImported = _model.FindAll(i => i.Action != ActionTypeEnum.Skip);

            if (_model.DataFile.FileType == FileTypeEnum.Excel)
            {
                _view.ShowLoading(true, "Importing file, please wait...");

                foreach (var item in toBeImported)
                {
                    var directory = Path.GetDirectoryName(item.FileName);
                    var fileName = Path.GetFileNameWithoutExtension(item.FileName)
                        + "-" + item.Table 
                        + "-" + item.Action.ToString()                        
                        + ".txt";

                    var finalFileName = Controller.Utilities.FileUtility.GetUniqueFileName(false, Path.Combine(directory, fileName));

                    item.OutputTextFile = finalFileName;
                    

                    await _service.ConvertExcelToTextAsync(item.FileName, item.Table, item.OutputTextFile);
                }

                _view.ShowLoading(false, string.Empty);
            }
        }

        private async void GetWorksheets()
        {
            _view.ShowLoading(true, "Reading file, please wait...");

            if (_model.DataFile.FileType == FileTypeEnum.Excel)
            {
                _model.DataFile.Tables = await _service.GetWorksheetNamesAsync(_model.DataFile.FileName);
            }

            DisplayImportActions();

            _view.ShowLoading(false, string.Empty);
        }

        private void DisplayImportActions()
        {
            if (_model.DataFile.Tables != null && _model.DataFile.Tables.Count > 0)
            {
                _model.Clear();

                foreach (var table in _model.DataFile.Tables)
                {
                    _model.Add(
                        new ImportActionModel
                        {
                            FileName = _model.DataFile.FileName,
                            Table = table,
                            Action = ActionTypeEnum.Skip
                        });
                }

                _view.ImportActions = _model;
            }            
        }


    }
}
