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
    /// Interaction logic for SubjectCreatingWindow.xaml
    /// </summary>
    public partial class SubjectCreatorWindow : DataCreatorBaseWindow<Subject>
    {
        public SubjectCreatorWindow()
        {
            InitializeComponent();
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            CreateItem();
            Close();
        }

        protected override Subject CreateItem()
        {
            string name = _name.Text.Trim();

            if (name == "")
            {
                MessageBox.Show("Поле пусто");
                return null;
            }

            Subject subject = new Subject(name);
            ItemCreated.Invoke(subject);
            return subject;
        }
    }
}
