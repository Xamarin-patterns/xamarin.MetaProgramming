using System;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    public class DependentProperty
    {
        public DependentProperty(string memberName, Type memberType)
        {
            PropertyType = memberType;
            PropertyName = memberName;
        }

        public Type PropertyType { get; set; }

        public string PropertyName { get; set; }
    }
}