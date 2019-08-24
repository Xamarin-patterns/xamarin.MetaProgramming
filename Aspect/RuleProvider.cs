using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    public class ConfigurationProvider<T> : BaseRuleProvider
    {
        private List<Rule<T>> _rules;

        public ConfigurationProvider()
        {
            _rules = new List<Rule<T>>();
        }

        public Rule<T> NotifyPropertyChange(Expression<Func<T, object>> expression)
        {
            var rule = new Rule<T> {Property = new DependentProperty(expression.GetMemberName(), expression.GetMemberType()) };
            _rules.Add(rule);
            return rule;
        }

        internal override DependentProperty[] GetDependentProperties(string argName)
        {
            return _rules.Where(r => r.DependentProperties.Any(x=>x.PropertyName==argName)).Select(x=>x.Property).ToArray();
        }

        internal override bool HasRuleFor(string argName)
        {
            return _rules.Any(r => r.Property.PropertyName == argName);
        }
    }
}