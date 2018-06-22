using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class YUDieHeader
    {
        private void InitializeComponent()
        {
            Dock = DockStyle.Top;
            
            prevButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Text = "◀",
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            prevButton.Click += PrevButton_Click;
            Controls.Add(prevButton);

            nextButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Text = "▶",
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            nextButton.Click += NextButton_Click;
            Controls.Add(nextButton);

            clearButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Text = "C",
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            clearButton.Click += ClearButton_Click;
            Controls.Add(clearButton);

            filterButton = new Button
            {
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Text = "F",
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            filterButton.Click += FilterButton_Click;
            Controls.Add(filterButton);

            nameLabel = new Label
            {
                AutoSize = true
            };
            Controls.Add(nameLabel);
        }
        
        private Label nameLabel;
        private Button prevButton;
        private Button nextButton;
        private Button clearButton;
        private Button filterButton;
    }
}