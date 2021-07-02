using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class DataCreatorBaseWindow<T> : Window
    {
        public Action<T> ItemCreated;

        protected virtual T CreateItem()
        {
            ItemCreated.Invoke(default);
            return default;
        }
    }
}
