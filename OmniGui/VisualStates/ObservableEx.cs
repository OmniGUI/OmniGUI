using System;
using System.Reactive;
using System.Reactive.Linq;

namespace OmniGui.VisualStates
{
    public static class ObservableEx
    {
        public static IObservable<Unit> ToUnit<T>(this IObservable<T> source)
        {
            return source.Select(_ => Unit.Default);
        }
    }
}