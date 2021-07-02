using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    public class DataChooserWindow<T> : DataChooserWindowBase
        where T : DataType
    {
        public Action<List<T>> Accepted;

        public DataChooserWindow(DataList<T> dataList)
        {
            InitializeComponent();
            foreach (T item in dataList.Items)
            {
                _listBox.Items.Add(item);
            }
        }

        protected override void OnAcceptButtonClick(object sender, RoutedEventArgs e)
        {
            List<T> checkedItems = new List<T>();
            foreach (T item in _listBox.SelectedItems)
            {
                checkedItems.Add(item);
            }
            Accepted.Invoke(checkedItems);
            Close();
        }
    }
}
