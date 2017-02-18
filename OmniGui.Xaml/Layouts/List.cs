namespace OmniGui.Xaml.Layouts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DynamicData;
    using DynamicData.Binding;
    using OmniGui.Layouts;
    using Templates;
    using Xaml;
    using Zafiro.PropertySystem.Standard;

    public class List : Layout
    {
        private readonly TemplateInflator controlTemplateInflator;

        public static readonly ExtendedProperty SourceProperty = PropertyEngine.RegisterProperty("Source", typeof(List),
            typeof(IObservableCollection<object>), new PropertyMetadata());

        public static readonly ExtendedProperty ItemTemplateProperty = PropertyEngine.RegisterProperty("ItemTemplate", typeof(List),
            typeof(DataTemplate), new PropertyMetadata());


        private IDisposable subscription;
        private readonly StackPanel panel;
        private readonly Func<ICollection<ControlTemplate>> controlTemplates;

        public List(TemplateInflator controlTemplateInflator, Func<ICollection<ControlTemplate>> controlTemplates)
        {
            this.controlTemplateInflator = controlTemplateInflator;
            this.controlTemplates = controlTemplates;
            panel = new StackPanel();
            this.AddChild(panel);

            subscription = GetChangedObservable(SourceProperty).Subscribe(obj =>
            {
                var source = (ISourceList<object>)obj;

                Platform.Current.EventSource.Invalidate();

                source.Connect()
                    .OnItemAdded(AddItem)                    
                    .ForEachChange(_ => Platform.Current.EventSource.Invalidate())
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
                controlTemplateInflator.Inflate(withDataTemplateApplied, controlTemplates());
                
                return withDataTemplateApplied;
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