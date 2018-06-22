using Advanced_Combat_Tracker;
using System;
using System.Drawing;
using System.IO;

namespace PorierACTPlugin
{
    public partial class YUDie : OverlayForm
    {
        public YUDie() : base()
        {
            InitializeComponent();

            using (FileStream fs = new FileStream(ActGlobals.oFormActMain.LogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                prevPos = fs.Length;
            }

            ActGlobals.oFormActMain.OnLogLineRead += OFormActMain_OnLogLineRead;
        }
        
        protected override void Dispose(bool disposing)
        {
            DisableHotKeys();
            DisableTimers();

            ActGlobals.oFormActMain.OnLogLineRead -= OFormActMain_OnLogLineRead;

            ActPlugin.YUDie = null;

            base.Dispose(disposing);
        }

        public override void RefreshFeature()
        {
            opacity = ActPlugin.Setting.YUDieSetting.Opacity;

            backgroundFormBackColor = ActPlugin.Setting.YUDieSetting.BackgroundFormBackColor;
            backgroundFormOpacity = ActPlugin.Setting.YUDieSetting.BackgroundFormOpacity;

            locationWrapper = ActPlugin.Setting.YUDieSetting.Location;
            sizeWrapper = ActPlugin.Setting.YUDieSetting.Size;

            roundCorner = ActPlugin.Setting.YUDieSetting.RoundCorner;

            autoSize = ActPlugin.Setting.YUDieSetting.AutoSize;

            if (ActPlugin.Setting.IsGameMode)
            {
                boundary = ActPlugin.Setting.YUDieSetting.GameModeBoundary;
                BackColor = ActPlugin.Setting.YUDieSetting.GameModeBackColor;
            }
            else
            {
                boundary = ActPlugin.Setting.YUDieSetting.Boundary;
                BackColor = ActPlugin.Setting.YUDieSetting.BackColor;
            }

            yUDieTitleBar.RefreshFeature();
            yUDieHeader.RefreshFeature();
            yUDiePanel.RefreshFeature();

            yUDiePanel.Visible = !ActPlugin.Setting.YUDieSetting.HeaderIsCollapsed;
            
            base.RefreshFeature();
        }

        public void AutoResize()
        {
            autoResizeOverlay();
        }

        protected override void autoResizeOverlay()
        {
            Size titleBarDesiredSize = yUDieTitleBar.DesiredSize;
            Size headerDesiredSize = yUDieHeader.DesiredSize;
            Size panelDesiredSize = yUDiePanel.DesiredSize;

            Width = Math.Max(Math.Max(titleBarDesiredSize.Width, headerDesiredSize.Width), panelDesiredSize.Width);
            Height = ActPlugin.Setting.YUDieSetting.HeaderIsCollapsed ? yUDieHeader.Height + yUDieTitleBar.Height : yUDieHeader.Height + panelDesiredSize.Height + yUDieTitleBar.Height;

            base.autoResizeOverlay();
        }
    }
}