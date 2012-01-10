using System.Linq.Expressions;
using System.Reflection;

namespace MvcGrabBag.Web.Helpers
{
    public static class ReflectionHelper
    {
        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            MemberExpression memberExpression;

            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = expression.Body as MemberExpression;
            }

            return (PropertyInfo)memberExpression.Member;
        }

    }
}