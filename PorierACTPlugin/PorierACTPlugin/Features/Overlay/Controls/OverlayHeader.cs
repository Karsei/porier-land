using System;
using System.Drawing;
using System.Text;

namespace PorierACTPlugin
{
    public partial class OverlayHeader : HeaderPanel
    {
        public Size DesiredSize;
        private TimeSpan currentDuration;
        private string currentEncounterName;
        private double currentRdps;
        private double currentRhps;

        public OverlayHeader() : base()
        {
            DesiredSize = new Size(0, 0);

            InitializeComponent();
        }

        public override void RefreshFeature()
        {
            headerBackColor = ActPlugin.Setting.OverlaySetting.HeaderBackColor;
            height = ActPlugin.Setting.OverlaySetting.HeaderIsCollapsed ? ActPlugin.Setting.OverlaySetting.HeaderCollapsedHeight : ActPlugin.Setting.OverlaySetting.HeaderHeight;
            parentForm = ActPlugin.Overlay;

            base.RefreshFeature();

            collapseButton.ForeColor = Color.FromArgb(255 - headerBackColor.R, 255 - headerBackColor.G, 255 - headerBackColor.B);
            collapseButton.Text = ActPlugin.Setting.OverlaySetting.HeaderIsCollapsed ? "▼" : "▲";
            hideNamesButton.ForeColor = Color.FromArgb(255 - headerBackColor.R, 255 - headerBackColor.G, 255 - headerBackColor.B);

            if (ActPlugin.Setting.OverlaySetting.HeaderIsCollapsed)
            {
                durationLabel.Visible = false;
                encounterNameLabel.Visible = false;
                rdpsLabel.Visible = false;
                rhpsLabel.Visible = false;

                collapsedTextLabel.Visible = true;
                ActPlugin.Setting.OverlaySetting.HeaderCollapsedText.FontWrapper.Size = (int)(Height / 2.0f);
                collapsedTextLabel.Font = ActPlugin.Setting.OverlaySetting.HeaderCollapsedText.FontWrapper;
                collapsedTextLabel.ForeColor = ActPlugin.Setting.OverlaySetting.HeaderCollapsedText.ColorWrapper;
            }
            else
            {
                collapsedTextLabel.Visible = false;

                durationLabel.Visible = true;
                encounterNameLabel.Visible = true;
                rdpsLabel.Visible = true;
                rhpsLabel.Visible = true;

                durationLabel.Font = ActPlugin.Setting.OverlaySetting.HeaderDurationText.FontWrapper;
                durationLabel.ForeColor = ActPlugin.Setting.OverlaySetting.HeaderDurationText.ColorWrapper;

                encounterNameLabel.Font = ActPlugin.Setting.OverlaySetting.HeaderEncounterNameText.FontWrapper;
                encounterNameLabel.ForeColor = ActPlugin.Setting.OverlaySetting.HeaderEncounterNameText.ColorWrapper;

                rdpsLabel.Font = ActPlugin.Setting.OverlaySetting.HeaderRdpsText.FontWrapper;
                rdpsLabel.ForeColor = ActPlugin.Setting.OverlaySetting.HeaderRdpsText.ColorWrapper;

                rhpsLabel.Font = ActPlugin.Setting.OverlaySetting.HeaderRhpsText.FontWrapper;
                rhpsLabel.ForeColor = ActPlugin.Setting.OverlaySetting.HeaderRhpsText.ColorWrapper;
            }
            
            SetValue(currentDuration, currentEncounterName, currentRdps, currentRhps);
        }

        public void SetValue(TimeSpan duration, string encounterName, double rdps, double rhps)
        {
            currentDuration = duration;
            currentEncounterName = encounterName;
            currentRdps = rdps;
            currentRhps = rhps;

            if (ActPlugin.Setting.OverlaySetting.HeaderIsCollapsed)
            {
                StringBuilder stringBuilder = new StringBuilder();

                if (ActPlugin.Setting.OverlaySetting.HeaderShowEncounterName)
                {
                    stringBuilder.Append(encounterName + " ");
                }

                if (ActPlugin.Setting.OverlaySetting.HeaderShowDuration)
                {
                    stringBuilder.Append("(" + duration.ToString("mm\\:ss") + ") ");
                }

                if (ActPlugin.Setting.OverlaySetting.HeaderShowRdps)
                {
                    stringBuilder.Append("RDPS " + rdps.ToString("0,0"));
                }

                if (ActPlugin.Setting.OverlaySetting.HeaderShowRhps)
                {
                    if (ActPlugin.Setting.OverlaySetting.HeaderShowRdps)
                    {
                        stringBuilder.Append(" / ");
                    }

                    stringBuilder.Append("RHPS " + rhps.ToString("0,0"));
                }

                collapsedTextLabel.Text = stringBuilder.ToString();
                collapsedTextLabel.Top = (int)((Height - collapsedTextLabel.Height) / 2.0f);

                DesiredSize.Width = collapsedTextLabel.Width + settingButton.Width + collapseButton.Width + hideNamesButton.Width + 10;
            }
            else
            {
                durationLabel.Text = ActPlugin.Setting.OverlaySetting.HeaderShowDuration ? duration.ToString("mm\\:ss") : "";
                durationLabel.Top = (int)((Height - durationLabel.Height) / 2.0f);

                encounterNameLabel.Text = ActPlugin.Setting.OverlaySetting.HeaderShowEncounterName ? encounterName : "";
                encounterNameLabel.Left = durationLabel.Right + 5;
                encounterNameLabel.Top = (int)((Height - encounterNameLabel.Height) / 2.0f);

                rdpsLabel.Text = ActPlugin.Setting.OverlaySetting.HeaderShowRdps ? "RDPS " + rdps.ToString("0,0") : "";
                rhpsLabel.Text = ActPlugin.Setting.OverlaySetting.HeaderShowRhps ? "RHPS " + rhps.ToString("0,0") : "";

                rdpsLabel.Left = encounterNameLabel.Right + 5;
                rhpsLabel.Left = rdpsLabel.Left;

                rdpsLabel.Top = (int)((Height - rdpsLabel.Height - rhpsLabel.Height) / 2.0f);
                rhpsLabel.Top = rdpsLabel.Bottom;

                DesiredSize.Width = durationLabel.Width + encounterNameLabel.Width + Math.Max(rdpsLabel.Width, rhpsLabel.Width)
                    + settingButton.Width + collapseButton.Width + hideNamesButton.Width + 20;
            }
        }
    }
}