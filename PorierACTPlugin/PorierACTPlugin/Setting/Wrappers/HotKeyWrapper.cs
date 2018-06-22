using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(HotKeyWrapperConverter))]
    [Editor(typeof(HotKeyWrapperTypeEditor), typeof(UITypeEditor))]
    public class HotKeyWrapper
    {
        public uint Modifiers { get; set; }
        public Keys KeyCode { get; set; }
        public string KeyString { get; set; }
    }
}