using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schuss.GoogleCalWpf.Themes
{
    public class BrushResources
    {
        private BrushResources()
        {

        }

        public static ComponentResourceKey HeaderBackgroundBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_HeaderBackgroundKey");
            }
        }
        public static ComponentResourceKey MainBackgroundBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_MainBackgroundKey");
            }
        }
        public static ComponentResourceKey FooterBackgroundBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_FooterBackgroundKey");
            }
        }
        public static ComponentResourceKey NormalBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_NormalKey");
            }
        }
        public static ComponentResourceKey DisabledForegroundBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_DisabledForegroundKey");
            }
        }
        public static ComponentResourceKey DisabledBackgroundBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_DisabledBackgroundKey");
            }
        }
        public static ComponentResourceKey WindowBackgroundBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_WindowBackgroundKey");
            }
        }
        public static ComponentResourceKey SelectedBackgroundBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_SelectedBackgroundKey");
            }
        }
        public static ComponentResourceKey NormalBorderBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_NormalBorderKey");
            }
        }
        public static ComponentResourceKey DisabledBorderBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_DisabledBorderKey");
            }
        }
        public static ComponentResourceKey SolidBorderBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_SolidBorderKey");
            }
        }
        public static ComponentResourceKey LightBorderBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_LightBorderKey");
            }
        }
        public static ComponentResourceKey GlyphBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_GlyphKey");
            }
        }
        public static ComponentResourceKey DarkGlyphBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_DarkGlyphKey");
            }
        }
        public static ComponentResourceKey LightColorBrushKey
        {
            get
            {
                return new ComponentResourceKey(typeof(BrushResources), "Brush_LightColorKey");
            }
        }
    }
}
