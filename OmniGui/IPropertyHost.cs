namespace OmniGui
{
    using System;
    using Zafiro.PropertySystem.Standard;

    namespace OmniGui

    {
        public interface IPropertyHost

        {

            void SetValue(ExtendedProperty property, object value);

            IObservable<object> GetChangedObservable(ExtendedProperty property);

            ExtendedProperty GetProperty(string propertyName);

        }

    }
}