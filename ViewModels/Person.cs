namespace Common
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Person : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Surname)}: {Surname}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}