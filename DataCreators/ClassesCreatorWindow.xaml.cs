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
    /// Interaction logic for ClassCreatingWindow.xaml
    /// </summary>
    public partial class ClassesCreatorWindow : DataCreatorBaseWindow<SchoolClass>
    {
        public ClassesCreatorWindow()
        {
            InitializeComponent();
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            CreateItem();
            Close();
        }

        protected override SchoolClass CreateItem()
        {
            string name = _name.Text.Trim();
            int population;
            try
            {
                population = int.Parse(_population.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректные данные в поле 'Количество'");
                return null;
            }

            if (name + _population.Text == "")
            {
                MessageBox.Show("Все поля пусты");
                return null;
            }

            if (population < 0)
            {
                population = 0;
            }

            SchoolClass schoolClass = new SchoolClass(name, population);
            ItemCreated.Invoke(schoolClass);
            return schoolClass;
        }
    }
}
