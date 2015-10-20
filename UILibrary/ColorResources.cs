using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schuss.UI.Common.Themes
{
    public class ColorResources
    {
        /// <summary>
        /// Empty constructor since this is just a utility static class.
        /// </summary>
        private ColorResources()
        {

        }

        public static ComponentResourceKey RedBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ColorResources), "RedBrush");
            }
        }

        public static ComponentResourceKey WindowBackgroundKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ColorResources), "WindowBackground");
            }
        }

        public static ComponentResourceKey WindowTopBackgroundKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ColorResources), "WindowTopBackground");
            }
        }

        public static ComponentResourceKey WindowBottomBackgroundKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ColorResources), "WindowBottomBackground");
            }
        }

    }
}
