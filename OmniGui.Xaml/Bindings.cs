using System.Collections.Generic;

namespace OmniGui.Xaml
{
    public class Bindings
    {
        public static HashSet<BindDefinition> Definitions { get; } = new HashSet<BindDefinition>();

        public static void Register(BindDefinition bd)
        {
            Definitions.Add(bd);
        }
    }
}