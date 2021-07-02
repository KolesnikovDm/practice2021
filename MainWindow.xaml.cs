using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Session Session => _session;

        private Session _session = new Session();

        private string _workersSave = @"Data\Workers.json";
        private string _subjectsSave = @"Data\Subjects.json";
        private string _classesSave = @"Data\Classes.json";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AppData.Workers.Load(_workersSave);
            AppData.Subjects.Load(_subjectsSave);
            AppData.SchoolClasses.Load(_classesSave);

            _session.Load(@"Session.json");

            _subjectsListBox.IsEnabled = false;
            _subjectsChooseButton.IsEnabled = false;
            _subjectRemoveButton.IsEnabled = false;
            _classesListBox.IsEnabled = false;
            _classesChooseButton.IsEnabled = false;
            _classRemoveButton.IsEnabled = false;
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _session.Save(@"Sess.json");
        }

        private void OnMenuWorkersClick(object sender, RoutedEventArgs e)
        {
            DataEditor<Worker, WorkerCreatorWindow> workersEditor 
                    = new DataEditor<Worker, WorkerCreatorWindow>(AppData.Workers, _workersSave);

            workersEditor.ShowDialog();
        }

        private void OnMenuSubjectsClick(object sender, RoutedEventArgs e)
        {
            DataEditor<Subject, SubjectCreatorWindow> subjectsEditor
                    = new DataEditor<Subject, SubjectCreatorWindow>(AppData.Subjects, _subjectsSave);

            subjectsEditor.ShowDialog();
        }

        private void OnMenuClassesClick(object sender, RoutedEventArgs e)
        {
            DataEditor<SchoolClass, ClassesCreatorWindow> classesEditor
                    = new DataEditor<SchoolClass, ClassesCreatorWindow>(AppData.SchoolClasses, _classesSave);

            classesEditor.ShowDialog();
        }

        private void OnWorkersChooseButtonClick(object sender, RoutedEventArgs e)
        {
            DataChooserWindow<Worker> dataChooser = new DataChooserWindow<Worker>(AppData.Workers);
            dataChooser.Accepted += FillWorkers;
            dataChooser.ShowDialog();
        }

        private void OnSubjectsChooseButtonClick(object sender, RoutedEventArgs e)
        {
            DataChooserWindow<Subject> dataChooser = new DataChooserWindow<Subject>(AppData.Subjects);
            dataChooser.Accepted += FillSubjects;
            dataChooser.ShowDialog();
        }

        private void OnClassesChooseButtonClick(object sender, RoutedEventArgs e)
        {
            DataChooserWindow<SchoolClass> dataChooser = new DataChooserWindow<SchoolClass>(AppData.SchoolClasses);
            dataChooser.Accepted += FillClasses;
            dataChooser.ShowDialog();
        }

        private void FillWorkers(List<Worker> workers)
        {
            List<Worker> sessionWorkers = _session.Workers.ToList();
            foreach (Worker worker in workers)
            {
                Worker copiedWorker = new Worker(worker.Name, worker.MiddleName, worker.LastName);

                if (sessionWorkers.Find(w => w.Equals(copiedWorker)) == null)
                    _session.Workers.Add(copiedWorker);
            }
        }

        private void FillSubjects(List<Subject> subjects)
        {
            Worker selectedWorker = _workersListBox.SelectedItem as Worker;
            List<Subject> workerSubjects = selectedWorker.Subjects.ToList();
            foreach (Subject subject in subjects)
            {
                Subject copiedSubject = new Subject(subject.Name);

                if (workerSubjects.Find(s => s.Equals(copiedSubject)) == null)
                    selectedWorker.Subjects.Add(copiedSubject);
            }
        }

        private void FillClasses(List<SchoolClass> classes)
        {
            Subject selectedSubject = _subjectsListBox.SelectedItem as Subject;
            List<ClassHourPair> subjectClasses = selectedSubject.Hours.ToList();

            foreach (SchoolClass class_ in classes)
            {
                SchoolClass copiedClass = new SchoolClass(class_.Name, class_.Population);

                if (subjectClasses.Find(s => s.Class.Equals(copiedClass)) == null)
                    selectedSubject.Hours.Add(new ClassHourPair(copiedClass, 0));

            }
        }

        private void OnWorkersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Worker selectedWorker = _workersListBox.SelectedItem as Worker;

            _subjectsListBox.UnselectAll();

            if (selectedWorker == null)
            {
                _subjectsListBox.IsEnabled = false;
                _subjectsChooseButton.IsEnabled = false;
                _subjectRemoveButton.IsEnabled = false;
                return;
            }

            (_workersListBox.SelectedItem as Worker).CalculateSalary(_session.StudentsHoursCost);

            _subjectsListBox.IsEnabled = true;
            _subjectsChooseButton.IsEnabled = true;
            _subjectRemoveButton.IsEnabled = true;
        }

        private void OnSubjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subject selectedSubject = _subjectsListBox.SelectedItem as Subject;

            _classesListBox.UnselectAll();

            if (selectedSubject == null)
            {
                _classesListBox.IsEnabled = false;
                _classesChooseButton.IsEnabled = false;
                _classRemoveButton.IsEnabled = false;
                return;
            }

            _classesListBox.IsEnabled = true;
            _classesChooseButton.IsEnabled = true;
            _classRemoveButton.IsEnabled = true;
        }

        private void OnWorkerRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            Worker selectedWorker = _workersListBox.SelectedItem as Worker;
            _session.Workers.Remove(selectedWorker);
        }

        private void OnSubjectRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            Worker selectedWorker = _workersListBox.SelectedItem as Worker;
            Subject selectedSubject = _subjectsListBox.SelectedItem as Subject;
            selectedWorker.Subjects.Remove(selectedSubject);
        }

        private void OnClassRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            Worker selectedWorker = _workersListBox.SelectedItem as Worker;
            Subject selectedSubject = _subjectsListBox.SelectedItem as Subject;
            ClassHourPair selectedClass = _classesListBox.SelectedItem as ClassHourPair;
            selectedSubject.Hours.Remove(selectedClass);
        }

        private void OnHoursPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OnHoursTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = "0";
            }
            (_workersListBox.SelectedItem as Worker).CalculateSalary(_session.StudentsHoursCost);
            _studentsHoursListBox.Items.Refresh();
        }
    }
}
