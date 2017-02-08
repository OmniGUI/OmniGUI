namespace ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;

    public class SampleViewModel : INotifyPropertyChanged
    {
        private string text;

        public SampleViewModel()
        {
            WrittenText = new Subject<object>();
            WrittenText.Subscribe(t => { });
        }

        public ISubject<object> WrittenText { get; }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
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
