using System;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    public class DependentProperty
    {
        public DependentProperty(string memberName, string memberType)
        {
            PropertyType = memberType;
            PropertyName = memberName;
        }

        public string PropertyType { get; set; }

        public string PropertyName { get; set; }
    }
}