using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for WorkerCreatingWindow.xaml
    /// </summary>
    public partial class WorkerCreatorWindow : DataCreatorBaseWindow<Worker>
    {
        public WorkerCreatorWindow()
        {
            InitializeComponent();
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            CreateItem();
            Close();
        }

        protected override Worker CreateItem()
        {
            string name = _name.Text.Trim();
            string middleName = _middleName.Text.Trim();
            string lastName = _lastName.Text.Trim();

            if (name + middleName + lastName == "")
            {
                MessageBox.Show("Все поля пусты");
                return null;
            }

            Worker worker = new Worker(name, middleName, lastName);
            ItemCreated.Invoke(worker);

            return worker;
        }
    }
}
