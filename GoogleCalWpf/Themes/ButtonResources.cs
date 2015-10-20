using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schuss.GoogleCalWpf.Themes
{
    public class ButtonResources
    {
        private ButtonResources()
        {

        }

        public static ComponentResourceKey GlassButtonKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ButtonResources), "Button_Glass");
            }
        }
        public static ComponentResourceKey RaisedButtonKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ButtonResources), "Button_Raised");
            }
        }
        public static ComponentResourceKey StandardToggleButtonKey
        {
            get
            {
                return new ComponentResourceKey(typeof(ButtonResources), "Button_StandardToggle");
            }
        }
    }
}
