using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public abstract class DataType
    {
        public abstract void CopyValues(DataType another);
    }
}
