using System;
using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class Overlay
    {
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            
            if (autoSize != null && !autoSize.EnableAutoSize)
            {
                DpsDataGridView.AutoResizeColumns();
                HpsDataGridView.AutoResizeColumns();
            }
        }

        protected override void autoResizeOverlay()
        {
            int dpsTableHeight = DpsDataGridView.Rows.GetRowsHeight(DataGridViewElementStates.None) + DpsDataGridView.ColumnHeadersHeight;
            int hpsTableHeight = HpsDataGridView.Rows.GetRowsHeight(DataGridViewElementStates.None) + HpsDataGridView.ColumnHeadersHeight;

            Height =
                dpsTableHeight +
                hpsTableHeight +
                overlayHeader.Height +
                (boundary * 2) +
                splitContainer.SplitterWidth +
                ActPlugin.Setting.OverlaySetting.AutoSizeTableDistance;
            splitContainer.SplitterDistance = dpsTableHeight + ActPlugin.Setting.OverlaySetting.AutoSizeTableDistance;
            
            DpsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            HpsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Size headerDesiredSize = overlayHeader.DesiredSize;

            Width = Math.Max(Math.Max(
                DpsDataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + (boundary * 2),
                HpsDataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + (boundary * 2)),
                headerDesiredSize.Width);
            
            DpsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DpsDataGridView.AutoResizeColumns();
            
            HpsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            HpsDataGridView.AutoResizeColumns();

            base.autoResizeOverlay();
        }
    }
}