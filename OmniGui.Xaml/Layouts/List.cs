namespace OmniGui.Xaml.Layouts
{
    using System;
    using DynamicData;
    using DynamicData.Binding;
    using OmniGui.Layouts;
    using Xaml;
    using Zafiro.PropertySystem.Standard;

    public class List : Layout
    {
        public static readonly ExtendedProperty SourceProperty = PropertyEngine.RegisterProperty("Source", typeof(List),
            typeof(IObservableCollection<object>), new PropertyMetadata());

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
            panel.AddChild(new TextBlock()
            {
                Text = item.ToString(),
                DataContext = item,
            });
        }

        public IObservableCollection<object> Source
        {
            get { return (IObservableCollection<object>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}