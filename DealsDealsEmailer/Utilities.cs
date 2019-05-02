using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DealsDealsEmailer
{
    public class Utilities
    {
        public static void BindListToGridView<T>(List<T> list, DataGridView GridView)
        {
            var bindingList = new BindingList<T>(list);
            var source = new BindingSource(bindingList, null);
            GridView.DataSource = source;
        }
    }
}
