using System;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    public abstract class BaseRuleProvider
    {
        internal abstract DependentProperty[] GetDependentProperties(string argName);


        internal abstract bool HasRuleFor(string argName);

    }
}