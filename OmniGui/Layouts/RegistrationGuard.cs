namespace OmniGui.Layouts
{
    using System;
    using System.Collections.Generic;

    public static class RegistrationGuard
    {
        static readonly ISet<Type> Registrators = new HashSet<Type>();

        public static void RegisterFor<T>(Action action)
        {
            if (!Registrators.Contains(typeof(T)))
            {
                Registrators.Add(typeof(T));
                action();
            }
        }
    }
}