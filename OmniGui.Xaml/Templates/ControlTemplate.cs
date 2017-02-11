// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.


namespace OmniGui.Xaml.Templates
{
    using OmniXaml.Attributes;

    public class ControlTemplate
    {
        [Content]
        [FragmentLoader(FragmentLoader = typeof(ConstructionFragmentLoader))]
        public TemplateContent Content { get; set; }

        public Layout Build()
        {
            return Content.Load();
        }
    }
}