using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PorierACTPlugin
{
    public class AnchorTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string)) return null;
            if (value == null) return "";
            if (value.GetType() != typeof(AnchorType)) return null;

            switch ((AnchorType)value)
            {
                case AnchorType.TopLeft:
                    return Language.Resource.GetString("ANCHOR_TYPE_TOP_LEFT");
                case AnchorType.TopRight:
                    return Language.Resource.GetString("ANCHOR_TYPE_TOP_RIGHT");
                case AnchorType.BottomLeft:
                    return Language.Resource.GetString("ANCHOR_TYPE_BOTTOM_LEFT");
                case AnchorType.BottomRight:
                    return Language.Resource.GetString("ANCHOR_TYPE_BOTTOM_RIGHT");
            }

            return "";
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }

    public class AnchorTypeTypeEditor : UITypeEditor
    {
        IWindowsFormsEditorService editorService;

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
                AnchorTypeControl anchorTypeControl = new AnchorTypeControl(editorService)
                {
                    AnchorType = (AnchorType)value
                };
                editorService.DropDownControl(anchorTypeControl);
                value = anchorTypeControl.AnchorType;
            }

            return value;
        }
    }

    public class AnchorTypeControl : Panel
    {
        public AnchorType AnchorType;

        private IWindowsFormsEditorService editorService;
        private Button topLeftButton;
        private Button topRightButton;
        private Button bottomLeftButton;
        private Button bottomRightButton;

        public AnchorTypeControl(IWindowsFormsEditorService editorService) : base()
        {
            this.editorService = editorService;

            int buttonSize = (int)(Math.Min(Width, Height) / 4f);

            topLeftButton = new Button
            {
                Height = buttonSize,
                Left = 0,
                Top = 0,
                Width = buttonSize
            };
            topLeftButton.Click += TopLeftButton_Click;
            Controls.Add(topLeftButton);

            topRightButton = new Button
            {
                Height = buttonSize,
                Left = Width - buttonSize,
                Top = 0,
                Width = buttonSize
            };
            topRightButton.Click += TopRightButton_Click;
            Controls.Add(topRightButton);

            bottomLeftButton = new Button
            {
                Height = buttonSize,
                Left = 0,
                Top = Height - buttonSize,
                Width = buttonSize
            };
            bottomLeftButton.Click += BottomLeftButton_Click;
            Controls.Add(bottomLeftButton);

            bottomRightButton = new Button
            {
                Height = buttonSize,
                Left = Width - buttonSize,
                Top = Height - buttonSize,
                Width = buttonSize
            };
            bottomRightButton.Click += BottomRightButton_Click;
            Controls.Add(bottomRightButton);
        }

        private void TopLeftButton_Click(object sender, EventArgs e)
        {
            AnchorType = AnchorType.TopLeft;
            editorService.CloseDropDown();
        }

        private void TopRightButton_Click(object sender, EventArgs e)
        {
            AnchorType = AnchorType.TopRight;
            editorService.CloseDropDown();
        }

        private void BottomLeftButton_Click(object sender, EventArgs e)
        {
            AnchorType = AnchorType.BottomLeft;
            editorService.CloseDropDown();
        }

        private void BottomRightButton_Click(object sender, EventArgs e)
        {
            AnchorType = AnchorType.BottomRight;
            editorService.CloseDropDown();
        }
    }
}