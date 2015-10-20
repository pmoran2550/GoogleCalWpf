using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schuss.GoogleCalWpf.Themes
{
    public class TextResources
    {
        private TextResources()
        {

        }

        public static ComponentResourceKey StandardHeaderTextKey
        {
            get
            {
                return new ComponentResourceKey(typeof(TextResources), "Text_StandardHeader");
            }
        }
        public static ComponentResourceKey SmallHeaderTextKey
        {
            get
            {
                return new ComponentResourceKey(typeof(TextResources), "Text_SmallHeader");
            }
        }
        public static ComponentResourceKey StandardTextKey
        {
            get
            {
                return new ComponentResourceKey(typeof(TextResources), "Text_Standard");
            }
        }
        public static ComponentResourceKey LargeButtonTextKey
        {
            get
            {
                return new ComponentResourceKey(typeof(TextResources), "Text_LargeButton");
            }
        }
        public static ComponentResourceKey StandardButtonTextKey
        {
            get
            {
                return new ComponentResourceKey(typeof(TextResources), "Text_StandardButton");
            }
        }
    }
}
