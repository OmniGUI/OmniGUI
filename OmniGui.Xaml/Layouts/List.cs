using System.Reactive.Linq;

namespace OmniGui.Xaml.Layouts
{
    using System;
    using DynamicData;
    using DynamicData.Binding;
    using OmniGui.Layouts;
    using Templates;
    using Zafiro.PropertySystem.Standard;

    public class List : Layout
    {
        private readonly Func<ResourceStore> containerFactory;
        private readonly Action<Layout> inflate;
        public static readonly ExtendedProperty SourceProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("Source", typeof(List), typeof(IObservableCollection<object>), new PropertyMetadata());
        public static readonly ExtendedProperty ItemTemplateProperty = OmniGuiPlatform.PropertyEngine.RegisterProperty("ItemTemplate", typeof(List), typeof(DataTemplate), new PropertyMetadata());

        private IDisposable subscription;
        private readonly StackPanel panel;
        public List(Func<ResourceStore> containerFactory, Platform platform) : base(platform)
        {
            this.containerFactory = containerFactory;
            panel = new StackPanel(platform);
            this.AddChild(panel);

            subscription = GetChangedObservable(SourceProperty)
                .Where(o => o != null)
                .Subscribe(obj =>
            {
                var source = (ISourceList<object>)obj;

                Platform.RenderSurface.ForceRender();

                source.Connect()
                    .OnItemAdded(AddItem)                    
                    .ForEachChange(_ => Platform.RenderSurface.ForceRender())
                    .Subscribe();
            });
        }

        private void AddItem(object item)
        {
            var wrappedItem = GenerateTemplateFor(item);
            panel.AddChild(wrappedItem);

            // Set the data context after adding the child, because the act of adding the child to a panel automatically sets the data context to its parent's            
            wrappedItem.DataContext = item;
        }

        private Layout GenerateTemplateFor(object item)
        {
            if (ItemTemplate != null)
            {
                var withDataTemplateApplied = (Layout) ItemTemplate.ApplyTo(item);
                containerFactory().Inflate(withDataTemplateApplied);
                
                return withDataTemplateApplied;
            }

            return new TextBlock(Platform)
            {
                Text = item.ToString(),
            };
        }

        public IObservableCollection<object> Source
        {
            get { return (IObservableCollection<object>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
    }
}