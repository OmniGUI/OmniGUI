namespace OmniGui.Xaml.Layouts
{
    using System;
    using System.Collections.Generic;
    using DynamicData;
    using DynamicData.Binding;
    using global::OmniGui.Layouts;
    using Templates;
    using Xaml;
    using Zafiro.PropertySystem;
    using Zafiro.PropertySystem.Standard;

    public class List : Layout
    {
        private readonly TemplateInflator controlTemplateInflator;

        public static ExtendedProperty SourceProperty;
        public static ExtendedProperty ItemTemplateProperty;
        
        private IDisposable subscription;
        private readonly StackPanel panel;
        private readonly Func<ICollection<ControlTemplate>> getControlTemplates;

        public List(TemplateInflator controlTemplateInflator, Func<ICollection<ControlTemplate>> getControlTemplates, IPropertyEngine propertyEngine) : base()
        {
            this.controlTemplateInflator = controlTemplateInflator;
            this.getControlTemplates = getControlTemplates;
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
                controlTemplateInflator.Inflate(withDataTemplateApplied, getControlTemplates());
                
                return withDataTemplateApplied;
            }

            return new TextBlock()
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