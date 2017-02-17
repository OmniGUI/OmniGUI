namespace OmniGui.Xaml.Layouts
{
    using System;
    using DynamicData;
    using DynamicData.Binding;
    using OmniGui.Layouts;
    using Templates;
    using Xaml;
    using Zafiro.PropertySystem.Standard;

    public class List : Layout
    {
        public static readonly ExtendedProperty SourceProperty = PropertyEngine.RegisterProperty("Source", typeof(List),
            typeof(IObservableCollection<object>), new PropertyMetadata());

        public static readonly ExtendedProperty ItemTemplateProperty = PropertyEngine.RegisterProperty("ItemTemplate", typeof(List),
            typeof(DataTemplate), new PropertyMetadata());


        private IDisposable subscription;
        private readonly StackPanel panel;

        public List(TemplateInflator inflator)
        {
            panel = new StackPanel();
            this.AddChild(panel);

            subscription = GetChangedObservable(SourceProperty).Subscribe(obj =>
            {
                var source = (ISourceList<object>)obj;

                Platform.Current.EventDriver.Invalidate();

                source.Connect()
                    .OnItemAdded(AddItem)                    
                    .ForEachChange(_ => Platform.Current.EventDriver.Invalidate())
                    .Subscribe();
            });
        }

        private void AddItem(object item)
        {
            var wrappedItem = WrapItem(item);
            panel.AddChild(wrappedItem);
        }

        private Layout WrapItem(object item)
        {
            if (ItemTemplate != null)
            {
                var inflated = (Layout) ItemTemplate.ApplyTo(item);
                inflated.DataContext = item;
            }

            return new TextBlock
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