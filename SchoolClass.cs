using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class SchoolClass : DataType
    {
        public string Name;
        public int Population;

        public SchoolClass(string name, int population)
        {
            Name = name;
            Population = population;
        }

        public override void CopyValues(DataType another)
        {
            SchoolClass schoolClass = (SchoolClass)another;
            Name = schoolClass.Name;
            Population = schoolClass.Population;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            SchoolClass schoolClass = obj as SchoolClass;
            return Name == schoolClass.Name;
        }

        public override int GetHashCode()
        {
            return (Name + Population).GetHashCode();
        }

        public override string ToString()
        {
            return Name + " (" + Population.ToString() + "чел.)";
        }
    }
}
