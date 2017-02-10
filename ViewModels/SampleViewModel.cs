namespace ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;

    public class SampleViewModel : INotifyPropertyChanged
    {
        private string name;
        private string surname;

        public SampleViewModel()
        {
            WrittenText = new Subject<object>();
            WrittenText.Subscribe(t => { });
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
