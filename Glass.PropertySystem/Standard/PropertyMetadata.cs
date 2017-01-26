namespace Glass.PropertySystem.Standard
{
    using System;

    public class PropertyMetadata
    {
        public object DefaultValue { get; set; }
        public IObserver<object> Observer { get; set; }
        public bool Inherit { get; set; }
    }
}