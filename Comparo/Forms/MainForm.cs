using Comparo.Controller;
using Comparo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comparo.Forms
{
    public partial class MainForm : Form, View.IMainView
    {
        private MainController _mainController;

        private static string _filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|Text Documents|*.txt|CSV (Comma delimited)|*.csv|All Files|*.*";
        private static string _defaultExt = ".xlsx";

        public MainForm()
        {
            InitializeComponent();
        }

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            _mainController = new MainController(this);
            _mainController.Initialize();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mainController.Import();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Debug.WriteLine(((ImportActionModel)cbSource.SelectedItem).FileName);
            var root = new TreeNode("adsf");
            root.Nodes.Add(new TreeNode("Sheet1"));

            treeViewSource.Nodes.Add(root);
        }

        #endregion Events


        #region Methods

        public void InitializeControls()
        {
            openFileDialog1.Filter = _filter;
            openFileDialog1.DefaultExt = _defaultExt;

            cbSource.ComboBox.DisplayMember = "Table";
            cbSource.ComboBox.ValueMember = "FileName";
            cbTarget.ComboBox.DisplayMember = "Table";
            cbTarget.ComboBox.ValueMember = "FileName";
        }

        public string ShowOpenFileDialog()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }

            return null;
        }

        public void ShowFileImport(string fileName)
        {
            var fileImportForm = new FileImportForm(fileName);
            if (fileImportForm.ShowDialog() == DialogResult.OK)
            {
                _mainController.ImportActions = fileImportForm.Model;
            }
            else
            {
                _mainController.ImportActions = null;
            }
        }

        #endregion Methods


        #region Properties

        public IEnumerable<ImportActionModel> SourceImportActions
        {
            set
            {
                var selectedItem = cbSource.SelectedItem;

                if (value != null && value.Count() > 0)
                {
                    cbSource.Items.Clear();
                    cbSource.Items.AddRange(value.ToArray());
                }

                if (selectedItem != null)
                {
                    cbSource.SelectedItem = selectedItem;
                }

                if (selectedItem == null && cbSource.Items != null && cbSource.Items.Count > 0)
                {
                    cbSource.SelectedIndex = 0;
                }
            }
        }

        public IEnumerable<ImportActionModel> TargetImportActions
        {
            set
            {
                var selectedItem = cbTarget.SelectedItem;

                if (value != null && value.Count() > 0)
                {
                    cbTarget.Items.Clear();
                    cbTarget.Items.AddRange(value.ToArray());
                }

                if (selectedItem != null)
                {
                    cbTarget.SelectedItem = selectedItem;
                }

                if (selectedItem == null && cbTarget.Items != null && cbTarget.Items.Count > 0)
                {
                    cbTarget.SelectedIndex = 0;
                }
            }
        }

        #endregion Properties
        
    }
}
