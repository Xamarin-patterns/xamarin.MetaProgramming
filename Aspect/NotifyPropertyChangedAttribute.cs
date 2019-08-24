using System;
using PostSharp.Aspects;
using Xamarin.Forms;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    public class NotifyPropertyChangedAttribute:LocationInterceptionAspect
    {
        private DependentProperty[] _DependentProperties;

        public NotifyPropertyChangedAttribute(DependentProperty[] dependentProperties)
        {
            _DependentProperties = dependentProperties;
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            args.SetNewValue(args.Value);
            var instance = (INotifyPropertyChangedImplementor) args.Instance;
            instance.RaisePropertyChanged(args.LocationName);
            foreach (var dependentProperty in _DependentProperties)
            {
                if (dependentProperty.PropertyType == "ICommand")
                {
                    (args.Instance.GetType().GetProperty(dependentProperty.PropertyName)
                        .GetValue(args.Instance) as Command).ChangeCanExecute();
                }
                else
                    instance.RaisePropertyChanged(dependentProperty.PropertyName);
            }
            args.ProceedSetValue();
        }
    }
}