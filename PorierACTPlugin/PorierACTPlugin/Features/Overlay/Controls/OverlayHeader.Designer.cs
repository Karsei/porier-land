using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayHeader
    {
        private void InitializeComponent()
        {
            durationLabel = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            durationLabel.MouseDown += _MouseDown;
            durationLabel.MouseUp += _MouseUp;
            durationLabel.MouseMove += _MouseMove;
            Controls.Add(durationLabel);

            encounterNameLabel = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            encounterNameLabel.MouseDown += _MouseDown;
            encounterNameLabel.MouseUp += _MouseUp;
            encounterNameLabel.MouseMove += _MouseMove;
            Controls.Add(encounterNameLabel);

            rdpsLabel = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            rdpsLabel.MouseDown += _MouseDown;
            rdpsLabel.MouseUp += _MouseUp;
            rdpsLabel.MouseMove += _MouseMove;
            Controls.Add(rdpsLabel);

            rhpsLabel = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };
            rhpsLabel.MouseDown += _MouseDown;
            rhpsLabel.MouseUp += _MouseUp;
            rhpsLabel.MouseMove += _MouseMove;
            Controls.Add(rhpsLabel);

            collapsedTextLabel = new Label
            {
                AutoSize = true
            };
            collapsedTextLabel.MouseDown += _MouseDown;
            collapsedTextLabel.MouseUp += _MouseUp;
            collapsedTextLabel.MouseMove += _MouseMove;
            Controls.Add(collapsedTextLabel);

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

            hideNamesButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Text = "H",
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            hideNamesButton.Click += HideNamesButton_Click;
            Controls.Add(hideNamesButton);
        }

        private Label durationLabel;
        private Label encounterNameLabel;
        private Label rdpsLabel;
        private Label rhpsLabel;
        private Label collapsedTextLabel;
        private Button settingButton;
        private Button collapseButton;
        private Button hideNamesButton;
    }
}