using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class YUDiePanel
    {
        private void InitializeComponent()
        {
            AutoScroll = true;
            BackColor = Color.FromArgb(1, 1, 1);
            Dock = DockStyle.Fill;

            filterButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Height = 25,
                Text = "F",
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false,
                Width = 25
            };
            filterButton.Click += FilterButton_Click;
            Controls.Add(filterButton);
        }

        private Button filterButton;
    }
}