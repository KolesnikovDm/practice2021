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
    /// Interaction logic for ClassesDataEditWindow.xaml
    /// </summary>
    public partial class DataEditorWindow : Window
    {
        public DataEditorWindow()
        {
            InitializeComponent();
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnRemoveButtonClick(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnModifyButtonClick(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
