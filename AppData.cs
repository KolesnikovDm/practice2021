using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public static class AppData
    {
        public static DataList<Worker> Workers = new DataList<Worker>();
        public static DataList<Subject> Subjects = new DataList<Subject>();
        public static DataList<SchoolClass> SchoolClasses = new DataList<SchoolClass>();
    }
}
