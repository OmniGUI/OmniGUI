namespace OmniGui.Xaml
{
    using System;
    using System.Reactive.Linq;

    public class Model
    {
        public IObservable<string> Text { get; } = Observable.Interval(TimeSpan.FromSeconds(1)).Select(p => "Si bebes no conduzcas, " + p.ToString());
    }
}