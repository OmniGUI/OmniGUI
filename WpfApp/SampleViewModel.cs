using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WpfApp
{
    public class SampleViewModel
    {
        public SampleViewModel()
        {
            WrittenText = new Subject<object>();
            WrittenText.Subscribe(t => { });
        }

        public IObservable<string> Text { get; } =
            Observable.Interval(TimeSpan.FromSeconds(1)).Select(p => "Si bebes no conduzcas, " + p.ToString());

        public ISubject<object> WrittenText { get; }

    }
}
