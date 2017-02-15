namespace OmniGui.Layouts.Grid
{
    using System.Collections.Generic;

    public static class DictExtensions
    {
        public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue i)
        {
            var success = dict.TryGetValue(key, out TValue v);

            return success ? v : i;
        }
    }
}