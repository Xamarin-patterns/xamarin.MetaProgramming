using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Xamarin.MetaProgramming
{
    [Serializable]
    public class Rule<T>
    {
        internal DependentProperty Property { get; set; }
        internal List<DependentProperty> DependentProperties { get; set; }=new List<DependentProperty>();
        public Rule<T> DependOn(Expression<Func<T, object>> expression)
        {
            DependentProperties.Add(new DependentProperty(expression.GetMemberName(), expression.GetMemberType()));
            return this;
        }

    }
}