using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class ClassHourPair
    {
        private SchoolClass _class;
        private int _hours;

        public SchoolClass Class { get => _class; set => _class = value; }
        public int Hours { get => _hours; set => _hours = value; }

        public int StudentsHours => _class.Population * _hours;

        public ClassHourPair(SchoolClass schoolClass, int hours)
        {
            _class = schoolClass;
            _hours = hours;
        }
    }
}
