namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using DynamicData;
    using DynamicData.Binding;

    public class SampleViewModel : INotifyPropertyChanged
    {
        private readonly IMessageService messageService;
        private string name;
        private string surname;

        public SampleViewModel(IMessageService messageService)
        {
            this.messageService = messageService;
            WrittenText = new Subject<object>();
            WrittenText.Subscribe(t => { });
            ShowMessageCommand = new DelegateCommand(() => messageService.ShowMessage("Saludos, colleiga!"));

            var people = new List<Person>()
            {
                new Person()
                {
                    Name = "José Manuel",
                    Surname = "Nieto",
                },
                new Person()
                {
                    Name = "Ana Isabel",
                    Surname = "Meana",
                },
                new Person()
                {
                    Name = "Bichito",
                    Surname = "LG",
                }
            };

            ISourceList<object> sourceList = new SourceList<object>();
            sourceList.AddRange(people);

            People = sourceList;
        }

        public ISubject<object> WrittenText { get; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowMessageCommand { get; set; }

        public ICommand DeleteItemCommand => new DelegateCommand(() => People.Add(new Person() { Name = "Nuevo", Surname = "Apps" }));

        public ISourceList<object> People { get; }

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
