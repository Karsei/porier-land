using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class MainTab
    {
        private void InitializeComponent()
        {
            Dock = DockStyle.Fill;

            resetSettingsButton = new Button
            {
                AutoSize = true,
                BackColor = Color.Red,
                Dock = DockStyle.Top
            };
            resetSettingsButton.Click += ResetSettingsButton_Click;
            Controls.Add(resetSettingsButton);

            editSettingsButton = new Button
            {
                AutoSize = true,
                Dock = DockStyle.Top
            };
            editSettingsButton.Click += EditSettingsButton_Click;
            Controls.Add(editSettingsButton);
        }

        private Button resetSettingsButton;
        private Button editSettingsButton;
    }
}