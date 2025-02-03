using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparo.Model
{
    public class ImportActionCollectionModel : List<ImportActionModel>
    {
        public DataFileModel DataFile { get; set; }
    }
}
