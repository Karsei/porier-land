using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class Overlay : OverlayForm
    {
        private List<string> healerJobNames = new List<string>
        {
            "CNJ",
            "WHM",
            "SCH",
            "AST"
        };
        
        private List<OverlayDataGridViewItem> dpsBackingSource;
        private List<OverlayDataGridViewItem> hpsBackingSource;
        
        public Overlay() : base()
        {
            InitializeComponent();

            using (FileStream fs = new FileStream(ActGlobals.oFormActMain.LogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                prevPos = fs.Length;
            }

            ActGlobals.oFormActMain.MainTreeView.AfterSelect += MainTreeView_AfterSelect;
            ActGlobals.oFormActMain.OnCombatStart += OFormActMain_OnCombatStart;
            ActGlobals.oFormActMain.OnLogLineRead += OFormActMain_OnLogLineRead;
        }
        
        protected override void Dispose(bool disposing)
        {
            DisableHotKeys();
            DisableTimers();
            
            ActGlobals.oFormActMain.MainTreeView.AfterSelect -= MainTreeView_AfterSelect;
            ActGlobals.oFormActMain.OnCombatStart -= OFormActMain_OnCombatStart;
            ActGlobals.oFormActMain.OnLogLineRead -= OFormActMain_OnLogLineRead;

            ActPlugin.Overlay = null;

            base.Dispose(disposing);
        }

        public override void RefreshFeature()
        {
            if (ActPlugin.Setting.OverlaySetting.DpsTable.Columns == null || ActPlugin.Setting.OverlaySetting.DpsTable.Columns.Count == 0)
            {
                ActPlugin.Setting.OverlaySetting.InitializeDpsTable();
            }

            if (ActPlugin.Setting.OverlaySetting.HpsTable.Columns == null || ActPlugin.Setting.OverlaySetting.HpsTable.Columns.Count == 0)
            {
                ActPlugin.Setting.OverlaySetting.InitializeHpsTable();
            }

            opacity = ActPlugin.Setting.OverlaySetting.Opacity;

            backgroundFormBackColor = ActPlugin.Setting.OverlaySetting.BackgroundFormBackColor;
            backgroundFormOpacity = ActPlugin.Setting.OverlaySetting.BackgroundFormOpacity;

            locationWrapper = ActPlugin.Setting.OverlaySetting.Location;
            sizeWrapper = ActPlugin.Setting.OverlaySetting.Size;

            roundCorner = ActPlugin.Setting.OverlaySetting.RoundCorner;

            autoSize = ActPlugin.Setting.OverlaySetting.AutoSize;

            if (ActPlugin.Setting.IsGameMode)
            {
                boundary = ActPlugin.Setting.OverlaySetting.GameModeBoundary;
                BackColor = ActPlugin.Setting.OverlaySetting.GameModeBackColor;
            }
            else
            {
                boundary = ActPlugin.Setting.OverlaySetting.Boundary;
                BackColor = ActPlugin.Setting.OverlaySetting.BackColor;
            }

            splitContainer.Panel1Collapsed = !ActPlugin.Setting.OverlaySetting.DpsTable.ShowTable;
            splitContainer.Panel2Collapsed = !ActPlugin.Setting.OverlaySetting.HpsTable.ShowTable;

            overlayHeader.RefreshFeature();
            DpsDataGridView.RefreshFeature();
            HpsDataGridView.RefreshFeature();

            base.RefreshFeature();

            if (!autoSize.EnableAutoSize)
            {
                splitContainer.SplitterDistance = Math.Max(ActPlugin.Setting.OverlaySetting.TableSplitterDistance, 0);
            }

            OnSizeChanged(new EventArgs());
        }

        public void ToggleHideNames()
        {
            DpsDataGridView.ToggleHideNames();
            HpsDataGridView.ToggleHideNames();
        }
    }
}