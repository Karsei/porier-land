using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    partial class Overlay
    {
        private void InitializeComponent()
        {
            splitContainer = new AnimatedSplitContainer
            {
                Dock = DockStyle.Fill,
                IsSplitterFixed = ActPlugin.Setting.OverlaySetting.AutoSize.EnableAutoSize,
                Orientation = Orientation.Horizontal,
                Panel1MinSize = 0,
                Panel2MinSize = 0,
                SplitterWidth = 1
            };
            containerPanel.Controls.Add(splitContainer);

            overlayHeader = new OverlayHeader();
            containerPanel.Controls.Add(overlayHeader);

            dpsBackingSource = new List<OverlayDataGridViewItem>();
            DpsDataGridView = new OverlayDataGridView(ActPlugin.Setting.OverlaySetting.DpsTable, dpsBackingSource)
            {
                DataSource = new BindingList<OverlayDataGridViewItem>(dpsBackingSource)
            };
            splitContainer.Panel1.Controls.Add(DpsDataGridView);

            hpsBackingSource = new List<OverlayDataGridViewItem>();
            HpsDataGridView = new OverlayDataGridView(ActPlugin.Setting.OverlaySetting.HpsTable, hpsBackingSource)
            {
                DataSource = new BindingList<OverlayDataGridViewItem>(hpsBackingSource)
            };
            splitContainer.Panel2.Controls.Add(HpsDataGridView);
        }

        private OverlayHeader overlayHeader;
        private SplitContainer splitContainer;
        
        public OverlayDataGridView DpsDataGridView;
        public OverlayDataGridView HpsDataGridView;
    }
}