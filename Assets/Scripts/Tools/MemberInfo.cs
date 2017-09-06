using System;
using System.Linq.Expressions;

namespace Assets.Scripts.Tools
{
    public static class MemberInfo
    {
        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}
