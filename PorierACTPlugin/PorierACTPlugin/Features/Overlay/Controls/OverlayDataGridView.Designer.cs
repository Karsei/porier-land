using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class OverlayDataGridView
    {
        private void InitializeComponent()
        {
            AllowDrop = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AllowUserToResizeColumns = true;
            AllowUserToResizeRows = true;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            MultiSelect = false;
            ReadOnly = true;

            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dock = DockStyle.Fill;
            ScrollBars = ScrollBars.Vertical;

            BackgroundColor = Color.FromArgb(1, 1, 1);
            BorderStyle = BorderStyle.None;
            AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;

            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DefaultCellStyle.BackColor = Color.Transparent;

            EnableHeadersVisualStyles = false;
            RowHeadersVisible = false;
            ShowCellErrors = true;
            ShowCellToolTips = false;
            ShowEditingIcon = false;
            ShowRowErrors = true;

            AutoGenerateColumns = false;
        }
    }
}