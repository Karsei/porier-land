using System.ComponentModel;
using System.Windows.Forms;

namespace PorierACTPlugin
{
    public class ColumnWrapper : GlobalizedSetting
    {
        [Browsable(true)]
        [Category("COLUMN_WRAPPER_CATEGORY_ALL")]
        [DisplayName("COLUMN_WRAPPER_DISPLAY_NAME_NAME")]
        [Description("COLUMN_WRAPPER_DESCRIPTION_NAME")]
        public string Name { get; set; }

        [Browsable(false)]
        public string DataPropertyName { get; set; }

        [Browsable(false)]
        public int DisplayIndex { get; set; }

        [Browsable(true)]
        [Category("COLUMN_WRAPPER_CATEGORY_ALL")]
        [DisplayName("COLUMN_WRAPPER_DISPLAY_NAME_VISIBLE")]
        [Description("COLUMN_WRAPPER_DESCRIPTION_VISIBLE")]
        [TypeConverter(typeof(VisibleConverter))]
        public bool Visible { get; set; }

        public static implicit operator DataGridViewColumn(ColumnWrapper columnWrapper)
        {
            DataGridViewColumn dataGridViewColumn = new DataGridViewColumn
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                DataPropertyName = columnWrapper.DataPropertyName,
                DisplayIndex = columnWrapper.DisplayIndex,
                HeaderText = columnWrapper.Name,
                Name = columnWrapper.Name,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Visible = columnWrapper.Visible
            };

            switch (columnWrapper.DataPropertyName)
            {
                case "Name":
                case "HiddenName":
                    dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    break;
                case "JobIcon":
                    dataGridViewColumn.CellTemplate = new DataGridViewImageCell
                    {
                        ImageLayout = DataGridViewImageCellLayout.Zoom,
                        Style = new DataGridViewCellStyle
                        {
                            Padding = new Padding(1)
                        }
                    };
                    break;
            }

            return dataGridViewColumn;
        }

        public static implicit operator ColumnWrapper(DataGridViewColumn dataGridViewColumn)
        {
            return new ColumnWrapper
            {
                Name = dataGridViewColumn.Name,
                DataPropertyName = dataGridViewColumn.DataPropertyName,
                DisplayIndex = dataGridViewColumn.DisplayIndex,
                Visible = dataGridViewColumn.Visible
            };
        }
    }
}