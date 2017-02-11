namespace Common
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

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
