using System.ComponentModel;

namespace PorierACTPlugin
{
    public class JobColorWrapper : GlobalizedSetting
    {
        [Browsable(false)]
        public string Name { get; set; }

        [Browsable(true)]
        [Category("JOB_COLOR_WRAPPER_CATEGORY_ALL")]
        [DisplayName("JOB_COLOR_WRAPPER_DISPLAY_NAME_COLOR_WRAPPER")]
        [Description("JOB_COLOR_WRAPPER_DESCRIPTION_COLOR_WRAPPER")]
        public ColorWrapper ColorWrapper { get; set; }
    }
}