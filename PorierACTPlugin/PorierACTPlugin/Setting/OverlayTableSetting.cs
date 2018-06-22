using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    [TypeConverter(typeof(OverlayTableSettingConverter))]
    public class OverlayTableSetting : GlobalizedSetting
    {
        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_TABLE")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_TABLE")]
        [TypeConverter(typeof(BoolConverter))]
        public bool ShowTable
        {
            get
            {
                return showTable;
            }

            set
            {
                showTable = value;
            }
        }
        private bool showTable = true;

        [Browsable(false)]
        public bool IsNameHidden
        {
            get
            {
                return isNameHidden;
            }

            set
            {
                isNameHidden = value;
            }
        }
        private bool isNameHidden = false;

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_COLUMNS")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_COLUMNS")]
        [TypeConverter(typeof(ColumnWrapperListConverter))]
        [Editor(typeof(ColumnWrapperListTypeEditor), typeof(UITypeEditor))]
        public List<ColumnWrapper> Columns
        {
            get
            {
                return columns;
            }

            set
            {
                columns = value;
            }
        }
        private List<ColumnWrapper> columns = new List<ColumnWrapper>();

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_ANTI_ALIASING")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_ANTI_ALIASING")]
        [TypeConverter(typeof(BoolConverter))]
        public bool AntiAliasing
        {
            get
            {
                return antiAliasing;
            }

            set
            {
                antiAliasing = value;
            }
        }
        private bool antiAliasing = true;

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_COLUMN_HEADER_BACK_COLOR")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_COLUMN_HEADER_BACK_COLOR")]
        public ColorWrapper ColumnHeaderBackColor
        {
            get
            {
                return columnHeaderBackColor;
            }

            set
            {
                columnHeaderBackColor = value;
            }
        }
        private ColorWrapper columnHeaderBackColor = Color.FromArgb(0, 0, 0);

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_COLUMN_HEADER_TEXT")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_COLUMN_HEADER_TEXT")]
        public TextWrapper ColumnHeaderText
        {
            get
            {
                return columnHeaderText;
            }

            set
            {
                columnHeaderText = value;
            }
        }
        private TextWrapper columnHeaderText = new TextWrapper
        {
            FontWrapper = new Font("맑은 고딕", 8f, FontStyle.Regular),
            ColorWrapper = Color.FromArgb(128, 128, 128)
        };

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_TEXT")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_TEXT")]
        public TextWrapper Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }
        private TextWrapper text = new TextWrapper
        {
            FontWrapper = new Font("맑은 고딕", 8f, FontStyle.Regular),
            ColorWrapper = Color.FromArgb(225, 225, 225)
        };

        [Browsable(false)]
        public string CurrentSortingDataPropertyName { get; set; }

        [Browsable(false)]
        public SortOrder CurrentSortOrder { get; set; }

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_COMBINE_PETS")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_COMBINE_PETS")]
        [TypeConverter(typeof(BoolConverter))]
        public bool CombinePets { get; set; }

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_INCLUDE_LIMIT_IN_SORTING")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_INCLUDE_LIMIT_IN_SORTING")]
        [TypeConverter(typeof(BoolConverter))]
        public bool IncludeLimitInSorting { get; set; }

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_LIMIT")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_LIMIT")]
        [TypeConverter(typeof(BoolConverter))]
        public bool ShowLimit { get; set; }

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_ONLY_HEALER")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_ONLY_HEALER")]
        [TypeConverter(typeof(BoolConverter))]
        public bool ShowOnlyHealer { get; set; }

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_GRAPH")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_GRAPH")]
        [TypeConverter(typeof(BoolConverter))]
        public bool ShowGraph { get; set; }

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_CURRENT_GRAPH_DATA_PROPERTY_NAME")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_CURRENT_GRAPH_DATA_PROPERTY_NAME")]
        [TypeConverter(typeof(CurrentGraphDataPropertyNameConverter))]
        [Editor(typeof(CurrentGraphDataPropertyNameTypeEditor), typeof(UITypeEditor))]
        public string CurrentGraphDataPropertyName { get; set; }

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_JOB_COLORS")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_JOB_COLORS")]
        [TypeConverter(typeof(JobColorsConverter))]
        [Editor(typeof(JobColorsTypeEditor), typeof(UITypeEditor))]
        public List<JobColorWrapper> JobColors
        {
            get
            {
                return jobColors;
            }

            set
            {
                jobColors = value;
            }
        }
        private List<JobColorWrapper> jobColors = new List<JobColorWrapper>();

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_GRID_COLOR")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_GRID_COLOR")]
        public ColorWrapper GridColor
        {
            get
            {
                return gridColor;
            }

            set
            {
                gridColor = value;
            }
        }
        private ColorWrapper gridColor = Color.FromArgb(25, 25, 25);

        [Browsable(true)]
        [Category("OVERLAY_TABLE_SETTING_CATEGORY_ALL")]
        [DisplayName("OVERLAY_TABLE_SETTING_DISPLAY_NAME_CELL_PADDING")]
        [Description("OVERLAY_TABLE_SETTING_DESCRIPTION_CELL_PADDING")]
        public int CellPadding
        {
            get
            {
                return cellPadding;
            }

            set
            {
                cellPadding = value;
            }
        }
        private int cellPadding = 2;

        public override string ToString()
        {
            return "(" + (ShowTable ? Language.Resource.GetString("TRUE") : Language.Resource.GetString("FALSE")) + ")";
        }

        public void Initialize()
        {
            jobColors = new List<JobColorWrapper>
            {
                new JobColorWrapper
                {
                    Name = "GLA",
                    ColorWrapper = Color.FromArgb(123, 154, 162)
                },
                new JobColorWrapper
                {
                    Name = "MRD",
                    ColorWrapper = Color.FromArgb(169, 26, 22)
                },

                new JobColorWrapper
                {
                    Name = "LNC",
                    ColorWrapper = Color.FromArgb(55, 82, 216)
                },
                new JobColorWrapper
                {
                    Name = "PGL",
                    ColorWrapper = Color.FromArgb(179, 137, 21)
                },
                new JobColorWrapper
                {
                    Name = "ARC",
                    ColorWrapper = Color.FromArgb(173, 197, 81)
                },
                new JobColorWrapper
                {
                    Name = "ROG",
                    ColorWrapper = Color.FromArgb(238, 46, 72)
                },
                new JobColorWrapper
                {
                    Name = "THM",
                    ColorWrapper = Color.FromArgb(103, 69, 152)
                },
                new JobColorWrapper
                {
                    Name = "ACN",
                    ColorWrapper = Color.FromArgb(50, 103, 11)
                },

                new JobColorWrapper
                {
                    Name = "CNJ",
                    ColorWrapper = Color.FromArgb(189, 189, 189)
                },

                new JobColorWrapper
                {
                    Name = "PLD",
                    ColorWrapper = Color.FromArgb(123, 154, 162)
                },
                new JobColorWrapper
                {
                    Name = "WAR",
                    ColorWrapper = Color.FromArgb(169, 26, 22)
                },
                new JobColorWrapper
                {
                    Name = "DRK",
                    ColorWrapper = Color.FromArgb(104, 37, 49)
                },

                new JobColorWrapper
                {
                    Name = "DRG",
                    ColorWrapper = Color.FromArgb(55, 82, 216)
                },
                new JobColorWrapper
                {
                    Name = "MNK",
                    ColorWrapper = Color.FromArgb(179, 137, 21)
                },
                new JobColorWrapper
                {
                    Name = "SAM",
                    ColorWrapper = Color.FromArgb(228, 90, 15)
                },
                new JobColorWrapper
                {
                    Name = "BRD",
                    ColorWrapper = Color.FromArgb(173, 197, 81)
                },
                new JobColorWrapper
                {
                    Name = "NIN",
                    ColorWrapper = Color.FromArgb(238, 46, 72)
                },
                new JobColorWrapper
                {
                    Name = "MCH",
                    ColorWrapper = Color.FromArgb(20, 138, 169)
                },
                new JobColorWrapper
                {
                    Name = "BLM",
                    ColorWrapper = Color.FromArgb(103, 69, 152)
                },
                new JobColorWrapper
                {
                    Name = "SMN",
                    ColorWrapper = Color.FromArgb(50, 103, 11)
                },
                new JobColorWrapper
                {
                    Name = "RDM",
                    ColorWrapper = Color.FromArgb(172, 41, 151)
                },

                new JobColorWrapper
                {
                    Name = "WHM",
                    ColorWrapper = Color.FromArgb(189, 189, 189)
                },
                new JobColorWrapper
                {
                    Name = "SCH",
                    ColorWrapper = Color.FromArgb(50, 48, 123)
                },
                new JobColorWrapper
                {
                    Name = "AST",
                    ColorWrapper = Color.FromArgb(177, 86, 28)
                },

                new JobColorWrapper
                {
                    Name = "LIMIT",
                    ColorWrapper = Color.FromArgb(255, 187, 0)
                },
                new JobColorWrapper
                {
                    Name = "PET",
                    ColorWrapper = Color.FromArgb(117, 117, 177)
                }
            };
        }
    }
}