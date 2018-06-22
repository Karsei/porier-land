using System.Windows.Forms;

namespace PorierACTPlugin
{
    public partial class SettingForm : Form
    {
        public SettingForm(object selectedObject) : base()
        {
            InitializeComponent();

            propertyGrid.SelectedObject = selectedObject;
        }

        public void SetPropertyValueChanged(PropertyValueChangedEventHandler propertyValueChangedEventHandler)
        {
            propertyGrid.PropertyValueChanged += propertyValueChangedEventHandler;
        }
    }
}