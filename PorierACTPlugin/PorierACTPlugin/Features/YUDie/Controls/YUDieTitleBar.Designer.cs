using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class YUDieTitleBar
    {
        private void InitializeComponent()
        {
            settingButton = new Button
            {
                BackColor = Color.Transparent,
                BackgroundImage = Image.Resource.SETTING,
                BackgroundImageLayout = ImageLayout.Zoom,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            settingButton.Click += SettingButton_Click;
            Controls.Add(settingButton);

            collapseButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            collapseButton.Click += CollapseButton_Click;
            Controls.Add(collapseButton);

            loadButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Text = "L",
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            loadButton.Click += LoadButton_Click;
            Controls.Add(loadButton);
        }
        
        private Button settingButton;
        private Button collapseButton;
        private Button loadButton;
    }
}