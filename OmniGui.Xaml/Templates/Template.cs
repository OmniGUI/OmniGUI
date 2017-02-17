namespace OmniGui.Xaml.Templates
{
    using OmniXaml.Attributes;

    public class Template
    {
        [Content]
        [FragmentLoader(FragmentLoader = typeof(ConstructionFragmentLoader))]
        public TemplateContent Content { get; set; }
    }
}