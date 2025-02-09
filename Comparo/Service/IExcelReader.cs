using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Service
{
    public interface IExcelReader
    {
        List<string> GetWorksheetNames(string filePath);

        Task<List<string>> GetWorksheetNamesAsync(string filePath);

        Task ConvertExcelToTextAsync(string excelFilePath, string sheetName, string outputTextFilePath);
    }
}
