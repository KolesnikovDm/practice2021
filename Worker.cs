using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;


namespace WpfApp
{
    public class Worker : DataType, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private string _middleName;
        private string _lastName;

        private ObservableCollection<Subject> _subjects;

        private int _salary;

        public string Name => _name;
        public string MiddleName => _middleName;
        public string LastName => _lastName;

        public ObservableCollection<Subject> Subjects => _subjects;

        public int Salary
        {
            get => _salary;
            set
            {
                if(_salary != value)
                {
                    _salary = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Worker(string name, string middleName, string lastName)
        {
            _name = name;
            _middleName = middleName;
            _lastName = lastName;
            _subjects = new ObservableCollection<Subject>();
        }

        public void CalculateSalary(int studentsHoursCost)
        {
            Salary = 0;
            foreach (Subject subject in _subjects)
            {
                foreach (ClassHourPair classHour in subject.Hours)
                {
                    Salary += classHour.StudentsHours * studentsHoursCost;
                }
            }
        }

        public override void CopyValues(DataType another)
        {
            Worker worker = another as Worker;
            _name = worker.Name;
            _middleName = worker.MiddleName;
            _lastName = worker.LastName;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Worker worker = obj as Worker;
            return Name == worker.Name
                && MiddleName == worker.MiddleName 
                && LastName == worker.LastName;
        }

        public override int GetHashCode()
        {
            return (Name + MiddleName + LastName).GetHashCode();
        }

        public override string ToString()
        {
            return string.Join(' ', LastName, Name, MiddleName);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
