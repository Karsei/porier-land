using System.ComponentModel;

namespace PorierACTPlugin
{
    public partial class OverlaySetting : GlobalizedSetting
    {
        [Browsable(false)]
        public int TableSplitterDistance
        {
            get
            {
                return tableSplitterDistance;
            }

            set
            {
                tableSplitterDistance = value;
            }
        }
        private int tableSplitterDistance = 200;
    }
}