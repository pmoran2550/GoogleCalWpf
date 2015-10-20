using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schuss.GoogleCalWpf.Themes
{
    public class ComboBoxResources
    {
        private ComboBoxResources()
        {

        }

        public static ComponentResourceKey StandardComboBoxKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ComboBoxResources), "ComboBox_Standard");
            }
        }
    }
}
