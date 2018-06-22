using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class OverlayDataGridView : DataGridView
    {
        private Dictionary<string, Color> jobColorDictionary = new Dictionary<string, Color>();
        private Dictionary<string, int> jobOrderDictionary = new Dictionary<string, int>
        {
            { "GLA", 0 },
            { "PLD", 1 },
            { "MRD", 2 },
            { "WAR", 3 },
            { "DRK", 4 },

            { "LNC", 5 },
            { "DRG", 6 },
            { "PGL", 7 },
            { "MNK", 8 },
            { "SAM", 9 },
            { "ARC", 10 },
            { "BRD", 11 },
            { "ROG", 12 },
            { "NIN", 13 },
            { "MCH", 14 },
            { "THM", 15 },
            { "BLM", 16 },
            { "ACN", 17 },
            { "SMN", 18 },
            { "RDM", 19 },

            { "CNJ", 20 },
            { "WHM", 21 },
            { "SCH", 22 },
            { "AST", 23 },

            { "LIMIT", 24 },
            { "PET", 25 }
        };
        private OverlayTableSetting setting;
        private List<OverlayDataGridViewItem> backingSource;

        public OverlayDataGridView(OverlayTableSetting setting, List<OverlayDataGridViewItem> backingSource) : base()
        {
            InitializeComponent();

            this.setting = setting;
            this.backingSource = backingSource;
        }

        protected override void Dispose(bool disposing)
        {
            setting.Columns.Clear();

            foreach (DataGridViewColumn column in Columns)
            {
                setting.Columns.Add(column);
            }

            setting.Columns.Sort((x, y) => x.DisplayIndex > y.DisplayIndex ? 1 : -1);

            base.Dispose(disposing);
        }

        public void RefreshFeature()
        {
            ColumnHeadersDefaultCellStyle.BackColor = setting.ColumnHeaderBackColor;
            ColumnHeadersDefaultCellStyle.Font = setting.ColumnHeaderText.FontWrapper;
            ColumnHeadersDefaultCellStyle.ForeColor = setting.ColumnHeaderText.ColorWrapper;

            DefaultCellStyle.Font = setting.Text.FontWrapper;
            DefaultCellStyle.ForeColor = setting.Text.ColorWrapper;
            DefaultCellStyle.Padding = new Padding(setting.CellPadding, 1, setting.CellPadding, 1);

            ColumnHeadersBorderStyle =
                ActPlugin.Setting.OverlaySetting.AutoSize.EnableAutoSize
                ? DataGridViewHeaderBorderStyle.None
                : DataGridViewHeaderBorderStyle.Single;

            GridColor = setting.GridColor;

            jobColorDictionary.Clear();

            if (setting.JobColors == null || setting.JobColors.Count == 0)
            {
                setting.Initialize();
            }

            foreach (JobColorWrapper jobColorWrapper in setting.JobColors)
            {
                jobColorDictionary.Add(jobColorWrapper.Name, jobColorWrapper.ColorWrapper);
            }

            Columns.Clear();

            foreach (ColumnWrapper column in setting.Columns)
            {
                Columns.Add(column);
            }

            SetHideNames(setting.IsNameHidden);

            Sort();
        }

        public void SetHideNames(bool hideName)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                if (hideName)
                {
                    if (column.DataPropertyName == "Name")
                    {
                        column.DataPropertyName = "HiddenName";
                        break;
                    }
                }
                else
                {
                    if (column.DataPropertyName == "HiddenName")
                    {
                        column.DataPropertyName = "Name";
                        break;
                    }
                }
            }
        }

        public void ToggleHideNames()
        {
            setting.IsNameHidden = !setting.IsNameHidden;
            SetHideNames(setting.IsNameHidden);
        }
    }
}