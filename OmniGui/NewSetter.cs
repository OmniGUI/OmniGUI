using System.Reflection;
using OmniXaml;

namespace OmniGui
{
    public class NewSetter
    {
        private readonly IStringSourceValueConverter converter;

        public NewSetter(IStringSourceValueConverter converter)
        {
            this.converter = converter;
        }

        public void Apply(object instance)
        {
            var typeOfInstance = instance.GetType();
            var property = typeOfInstance.GetRuntimeProperty(PropertyName);
            var result = converter.Convert(Value, property.PropertyType);

            var isSuccesfulConversion = result.Item1;
            if (!isSuccesfulConversion)
            {
                return;
            }

            var compatibleValue = result.Item2;               
            property.SetValue(instance, compatibleValue);
        }

        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}