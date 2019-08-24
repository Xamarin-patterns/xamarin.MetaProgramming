using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Xamarin.MetaProgramming
{
    internal static class Helper
    {
        internal static string GetMemberType(this LambdaExpression expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var propertyInfo = (PropertyInfo)memberExpression.Member;
            return propertyInfo.PropertyType.Name;
        }
        internal static string GetMemberName(this Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("");
            }

            if (expression is LambdaExpression )
            {
                // Reference type property or field
                var memberExpression =(MemberExpression) ((LambdaExpression)expression).Body;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("");
        }

        internal static string GetMemberName(this UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
    }
}
