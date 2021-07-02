using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class DataEditor<T, W> : DataEditorWindow
        where T: DataType
        where W: DataCreatorBaseWindow<T>, new()
    {
        private List<T> _createdItems = new List<T>();
        private List<T> _removedItems = new List<T>();
        private Dictionary<T, T> _modifiedItems = new Dictionary<T, T>();

        private DataList<T> _dataList;
        private string _savePath;

        public DataEditor(DataList<T> dataList, string savePath)
        {
            InitializeComponent();
            _dataList = dataList;
            _savePath = savePath;
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            FillListBox();
        }

        protected override void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            W creator = new W();
            creator.ItemCreated += OnItemCreated;
            creator.ShowDialog();
        }

        protected override void OnRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            T selectedItem = (T)_listBox.SelectedItem;

            if (selectedItem == null)
                return;

            _listBox.Items.Remove(selectedItem);
            _removedItems.Add(selectedItem);
        }

        protected override void OnModifyButtonClick(object sender, RoutedEventArgs e)
        {
            T selectedItem = (T)_listBox.SelectedItem;

            if (selectedItem == null)
                return;

            W creator = new W();
            creator.ItemCreated += OnItemModified;
            creator.ShowDialog();
        }

        protected override void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void OnItemCreated(T item)
        {
            _listBox.Items.Add(item);
            _createdItems.Add(item);
        }

        private void OnItemModified(T item)
        {
            T selectedItem = (T)_listBox.SelectedItem;

            _modifiedItems.Add(selectedItem, item);

            _listBox.Items[_listBox.SelectedIndex] = item;
            _listBox.Items.Refresh();
        }

        private void FillListBox()
        {
            foreach (T item in _dataList.Items)
            {
                _listBox.Items.Add(item);
            }
        }

        private void Save()
        {
            foreach (T item in _createdItems)
            {
                _dataList.Items.Add(item);
            }
            foreach (T item in _removedItems)
            {
                _dataList.Items.Remove(item);
            }
            foreach (T item in _modifiedItems.Keys)
            {
                _dataList.Items.Find(i => i == item).CopyValues(_modifiedItems[item]);
            }

            _dataList.Save(_savePath);

            _createdItems.Clear();
            _removedItems.Clear();
            _modifiedItems.Clear();

            MessageBox.Show("Сохранено");
        }
    }
}