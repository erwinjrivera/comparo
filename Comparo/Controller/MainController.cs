using Comparo.Model;
using Comparo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Controller
{
    public class MainController
    {
        private IMainView _view;

        private ImportActionCollectionModel _importActions;
        private List<ImportActionModel> _sourceImportActions = new List<ImportActionModel>();
        private List<ImportActionModel> _targetImportActions = new List<ImportActionModel>();

        public ImportActionCollectionModel ImportActions
        {
            get 
            { 
                return _importActions; 
            }
            set 
            {
                _importActions = value;

                RefreshImportActions();
            }
        }

        public void RefreshImportActions()
        {
            if (_importActions == null)
                return;

            var sources = _importActions.FindAll(x => x.Action == ActionTypeEnum.Source);
            var targets = _importActions.FindAll(x => x.Action == ActionTypeEnum.Target);

            if (sources != null && sources.Any()) 
            {
                _sourceImportActions.AddRange(sources);
            }

            if (targets != null && targets.Any())
            {
                _targetImportActions.AddRange(targets);
            }

            _view.SourceImportActions = _sourceImportActions;
            _view.TargetImportActions = _targetImportActions;
        }

        public MainController(IMainView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.InitializeControls();
        }

        public void Import()
        {
            var fileName = _view.ShowOpenFileDialog();

            if (fileName == null || fileName.Length == 0)
            {
                // log or something

                return;
            }

            _view.ShowFileImport(fileName);
        }

        public void Test()
        {
            var x = ImportActions;
        }
        
    }
}
