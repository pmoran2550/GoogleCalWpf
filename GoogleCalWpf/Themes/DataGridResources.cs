using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schuss.GoogleCalWpf.Themes
{
    public class DataGridResources
    {
        private DataGridResources()
        {

        }

        public static ComponentResourceKey StandardDataGridKey
        {
            get
            {
                return new ComponentResourceKey(typeof(DataGridResources), "DataGrid_Standard");
            }
        }
        public static ComponentResourceKey StandardDataGridCellKey
        {
            get
            {
                return new ComponentResourceKey(typeof(DataGridResources), "DataGridCell_Standard");
            }
        }
        public static ComponentResourceKey StandardDataGridRowKey
        {
            get
            {
                return new ComponentResourceKey(typeof(DataGridResources), "DataGridRow_Standard");
            }
        }
    }
    public class DataGridColumnHeaderResources
    {
        private DataGridColumnHeaderResources()
        {
        }

        public static ComponentResourceKey StandardDataGridColumnHeaderKey
        {
            get
            {
                return new ComponentResourceKey(typeof(DataGridColumnHeaderResources), "DataGridColumnHeader_Standard");
            }
        }
    }
    
}
