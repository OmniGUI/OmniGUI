using System;
using System.Collections.Generic;
using System.Text;

namespace OmniGui.Xaml.Templates
{
    public class DataTemplate : Template
    {
        public object ApplyTo(object item)
        {
            return this.Content.Load();
        }
    }
}
