using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    [IntroduceInterface(typeof(INotifyPropertyChangedImplementor))]
    public class ViewModelAttribute :InstanceLevelAspect, IAspectProvider , INotifyPropertyChangedImplementor
    {
        private BaseRuleProvider _baseRuleProvider;

        public ViewModelAttribute(Type ruleProviderType)
        {
            _baseRuleProvider = Activator.CreateInstance(ruleProviderType) as BaseRuleProvider;
        }

        public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
        {
            var type = (Type)targetElement;

            foreach (var property in type.GetProperties())
            {
                if (_baseRuleProvider.HasRuleFor(property.Name))
                    yield return new AspectInstance(property,new NotifyPropertyChangedAttribute(_baseRuleProvider.GetDependentProperties(property.Name)));
                if(property.GetCustomAttribute(typeof(DefaultValueAttribute))!=null)
                    yield return new AspectInstance(property, new DefaultValueAspect(((DefaultValueAttribute) property.GetCustomAttribute(typeof(DefaultValueAttribute))).Value));

            }

        }

        [IntroduceMember]
        public event PropertyChangedEventHandler PropertyChanged;

        [IntroduceMember]
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(base.Instance,new PropertyChangedEventArgs(propertyName));
        }
    }
}