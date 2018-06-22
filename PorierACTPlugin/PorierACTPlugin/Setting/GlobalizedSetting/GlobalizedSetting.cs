using System;
using System.ComponentModel;

namespace PorierACTPlugin
{
    public class GlobalizedPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor basePropertyDescriptor;

        public GlobalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
        {
            this.basePropertyDescriptor = basePropertyDescriptor;
        }

        public override bool CanResetValue(object component)
        {
            return basePropertyDescriptor.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get
            {
                return basePropertyDescriptor.ComponentType;
            }
        }

        public override string DisplayName
        {
            get
            {
                try
                {
                    return Language.Resource.GetString(base.DisplayName);
                }
                catch
                {
                    return base.DisplayName;
                }
            }
        }

        public override string Description
        {
            get
            {
                try
                {
                    return Language.Resource.GetString(base.Description);
                }
                catch
                {
                    return base.Description;
                }
            }
        }

        public override string Category
        {
            get
            {
                try
                {
                    return Language.Resource.GetString(base.Category);
                }
                catch
                {
                    return base.Category;
                }
            }
        }

        public override object GetValue(object component)
        {
            return basePropertyDescriptor.GetValue(component);
        }

        public override bool IsReadOnly
        {
            get
            {
                return basePropertyDescriptor.IsReadOnly;
            }
        }

        public override string Name
        {
            get
            {
                return basePropertyDescriptor.Name;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return basePropertyDescriptor.PropertyType;
            }
        }

        public override void ResetValue(object component)
        {
            basePropertyDescriptor.ResetValue(component);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
            //return basePropertyDescriptor.ShouldSerializeValue(component);
        }

        public override void SetValue(object component, object value)
        {
            basePropertyDescriptor.SetValue(component, value);
        }
    }

    public class GlobalizedSetting : ICustomTypeDescriptor
    {
        private PropertyDescriptorCollection globalizedProperties;

        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (globalizedProperties == null)
            {
                PropertyDescriptorCollection baseProperties = TypeDescriptor.GetProperties(this, attributes, true);

                globalizedProperties = new PropertyDescriptorCollection(null);

                foreach (PropertyDescriptor propertyDescriptor in baseProperties)
                {
                    globalizedProperties.Add(new GlobalizedPropertyDescriptor(propertyDescriptor));
                }
            }

            return globalizedProperties;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            if (globalizedProperties == null)
            {
                PropertyDescriptorCollection baseProperties = TypeDescriptor.GetProperties(this, true);

                globalizedProperties = new PropertyDescriptorCollection(null);

                foreach (PropertyDescriptor propertyDescriptor in baseProperties)
                {
                    globalizedProperties.Add(new GlobalizedPropertyDescriptor(propertyDescriptor));
                }
            }

            return globalizedProperties;
        }

        public object GetPropertyOwner(PropertyDescriptor propertyDescriptor)
        {
            return this;
        }
    }
}