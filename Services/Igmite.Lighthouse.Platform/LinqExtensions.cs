using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Igmite.Lighthouse.Platform
{
    public static class LinqExtensions
    {
        public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (valueSelector == null)
            {
                throw new ArgumentNullException("valueSelector");
            }
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            ParameterExpression expression = valueSelector.Parameters.Single<ParameterExpression>();
            if (!values.Any<TValue>())
            {
                return e => false;
            }
            return Expression.Lambda<Func<TElement, bool>>((from value in values select Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue)))).Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal)), new ParameterExpression[] { expression });
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T local in items)
            {
                action(local);
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            Random random = new Random();
            int count = list.Count;
            int minValue = count;
            T local = default(T);
            while (minValue >= 1)
            {
                minValue--;
                int num3 = random.Next(minValue, count);
                local = list[num3];
                list[num3] = list[minValue];
                list[minValue] = local;
            }
        }
    }
}