using System;
using System.Reactive.Linq;

namespace WpfApp
{
    public class SampleViewModel
    {
        public SampleViewModel()
        {
            
        }

        public IObservable<string> Text { get; } =
            Observable.Interval(TimeSpan.FromSeconds(1)).Select(p => "Si bebes no conduzcas, " + p.ToString());

    }
}
