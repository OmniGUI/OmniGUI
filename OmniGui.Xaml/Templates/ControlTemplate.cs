// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.


namespace OmniGui.Xaml.Templates
{
    public class ControlTemplate : Template
    {
        public Layout ApplyTo(Layout layout)
        {
            return Content.LoadFor(layout);
        }

        public string Target { get; set; }
    }
}