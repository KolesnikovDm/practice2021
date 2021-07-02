using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp
{
    public class Subject : DataType
    {
        public string Name;

        private ObservableCollection<ClassHourPair> _hours;

        public ObservableCollection<ClassHourPair> Hours => _hours;

        public Subject(string name)
        {
            Name = name;
            _hours = new ObservableCollection<ClassHourPair>();
        }

        public override void CopyValues(DataType another)
        {
            Subject subject = another as Subject;
            Name = subject.Name;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Subject subject = obj as Subject;
            return Name == subject.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
