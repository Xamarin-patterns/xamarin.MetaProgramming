using System;
using PostSharp.Aspects;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    public class DefaultValueAspect : LocationInterceptionAspect
    {
        private object _value;

        public DefaultValueAspect(object value)
        {
            _value = value;
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (args.GetCurrentValue() == null)
                args.SetNewValue(_value);
            args.ProceedGetValue();
        }
    }
}