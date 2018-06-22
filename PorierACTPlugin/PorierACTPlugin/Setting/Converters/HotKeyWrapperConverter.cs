using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class HotKeyWrapperConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";
            if (value.GetType() != typeof(HotKeyWrapper)) return "";

            return ((HotKeyWrapper)value).KeyString;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class HotKeyWrapperTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            }

            if (editorService != null)
            {
                HotKeyWrapperControl hotKeyWrapperControl = new HotKeyWrapperControl(editorService);
                editorService.DropDownControl(hotKeyWrapperControl);
                value = hotKeyWrapperControl.Result;
            }

            return value;
        }
    }

    public class HotKeyWrapperControl : TextBox
    {
        public HotKeyWrapper Result;

        private IWindowsFormsEditorService editorService;

        public HotKeyWrapperControl(IWindowsFormsEditorService editorService) : base()
        {
            this.editorService = editorService;
            Result = new HotKeyWrapper();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            e.SuppressKeyPress = true;

            switch (e.KeyCode)
            {
                case Keys.Alt:
                case Keys.Control:
                case Keys.ControlKey:
                case Keys.Shift:
                case Keys.ShiftKey:
                case Keys.Menu:
                case Keys.LWin:
                case Keys.RWin:
                case Keys.HanjaMode:
                    return;
            }

            if (e.Alt)
            {
                Text += "Alt + ";
            }

            if (e.Control)
            {
                Text += "Ctrl + ";
            }

            if (e.Shift)
            {
                Text += "Shift + ";
            }

            Text += e.KeyCode.ToString();

            Result = new HotKeyWrapper
            {
                KeyCode = e.KeyCode,
                KeyString = Text,
                Modifiers = 0
            };

            if (e.Alt)
            {
                Result.Modifiers |= (uint)Modifier.Alt;
            }

            if (e.Control)
            {
                Result.Modifiers |= (uint)Modifier.Control;
            }

            if (e.Shift)
            {
                Result.Modifiers |= (uint)Modifier.Shift;
            }

            editorService.CloseDropDown();
        }

        [Flags]
        public enum Modifier : uint
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }
    }
}