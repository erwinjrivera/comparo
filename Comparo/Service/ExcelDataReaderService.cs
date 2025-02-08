using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Service
{
    public class TextAndExcelDataReaderService : IDataExtractorService
    {
        /// <summary>
        /// Retrieves all worksheet names from an Excel file.
        /// </summary>
        public List<string> GetWorksheetNames(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Excel file not found.", filePath);

            List<string> sheetNames = new List<string>();

            // Open the Excel file efficiently
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataset = reader.AsDataSet();
                    foreach (DataTable table in dataset.Tables)
                    {
                        sheetNames.Add(table.TableName); // Store sheet names
                    }
                }
            }

            return sheetNames;
        }

        /// <summary>
        /// Retrieves all worksheet names from an Excel file asynchronously.
        /// </summary>
        public async Task<List<string>> GetWorksheetNamesAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Excel file not found.", filePath);

            return await Task.Run(() =>
            {
                List<string> sheetNames = new List<string>();

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet();
                        foreach (DataTable table in dataSet.Tables)
                        {
                            sheetNames.Add(table.TableName);
                        }
                    }
                }
                return sheetNames;
            });
        }

        /// <summary>
        /// Asynchronously converts a specific worksheet from an Excel file to a tab-delimited text file.
        /// </summary>
        public async Task ConvertExcelToTextAsync(string excelFilePath, string sheetName, string outputTextFilePath)
        {
            await Task.Run(() =>
            {
                if (!File.Exists(excelFilePath))
                    throw new FileNotFoundException("Excel file not found.", excelFilePath);

                using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet();
                        var table = dataSet.Tables[sheetName];

                        if (table == null)
                            throw new ArgumentException($"Sheet '{sheetName}' not found in the Excel file.");

                        using (var writer = new StreamWriter(outputTextFilePath, false, Encoding.UTF8))
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                string line = string.Join("\t", row.ItemArray);
                                writer.WriteLine(line);
                            }
                        }
                    }
                }
            });
        }
    }
}
