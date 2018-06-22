using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class SettingForm
    {
        private void InitializeComponent()
        {
            ShowIcon = false;
            ShowInTaskbar = false;
            Size = new Size(500, 500);
            TopMost = true;

            propertyGrid = new PropertyGrid
            {
                Dock = DockStyle.Fill,
                ToolbarVisible = false
            };
            Controls.Add(propertyGrid);
        }

        private PropertyGrid propertyGrid;
    }
}