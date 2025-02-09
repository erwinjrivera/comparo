using Comparo.Controller;
using Comparo.Model;
using Comparo.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comparo.Forms
{
    public partial class FileImportForm : Form, View.IFileImportView
    {
        private string _fileName;

        private ImportActionCollectionModel _model;

        private Controller.FileImportController _controller;

        public ImportActionCollectionModel Model 
        { 
            get 
            { 
                return _model; 
            } 
        }

        public FileImportForm(string fileName)
        {
            InitializeComponent();

            _fileName = fileName;
            _model = new ImportActionCollectionModel 
            { 
                DataFile = new DataFileModel { FileName = _fileName } 
            };

            (new Forms.Utilities.DropShadow()).ApplyShadows(this);
            this.FormBorderStyle = FormBorderStyle.None; // no borders
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void FileImportForm_Load(object sender, EventArgs e)
        {            
            _controller = new FileImportController(this, _model, new TextAndExcelDataReaderService());
            _controller.Initialize();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_controller.ValidateImportActions())
            {
                this.DialogResult = DialogResult.OK;  // Close Form2 only if validation is successful
                this.Close();
            }
        }

        #region View Members

        public void InitializeControls()
        {
            toolStripStatusLabel1.Text = string.Empty;
            dataGridView1.AutoGenerateColumns = false;

            // Create and configure columns
            DataGridViewTextBoxColumn tableColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Table",
                HeaderText = "Table",
                ReadOnly = true
            };

            DataGridViewComboBoxColumn actionColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Action",
                HeaderText = "Import As",
                DataSource = Enum.GetValues(typeof(ActionTypeEnum)),
                Width = 60
            };

            dataGridView1.Columns.Add(tableColumn);
            dataGridView1.Columns.Add(actionColumn);
            dataGridView1.Columns[1].Width = 80;
        }

        public List<ImportActionModel> ImportActions
        {
            set
            {
                dataGridView1.DataSource = value;
            }
        }

        public string FileName
        {
            set
            {
                lblFileName.Text = value;
            }
        }

        public string StatusText
        {
            set
            {
                toolStripStatusLabel1.Text = value;
            }
        }

        public void ShowLoading(bool isLoading, string message)
        {
            btnCancel.Enabled = !isLoading;
            btnOk.Enabled = !isLoading;

            toolStripStatusLabel1.Text = message;
            toolStripProgressBar1.Visible = isLoading;
        }



        #endregion

        
    }
}
