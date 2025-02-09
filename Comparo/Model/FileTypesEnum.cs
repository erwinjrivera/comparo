using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Model
{
    public enum FileTypeEnum
    {
        Excel,       // For .xls and .xlsx files
        CSV,         // For .csv files
        TabDelimited, // For .txt or .tsv files with tab separation
        Unsupported // Unknown file type
    }
}
