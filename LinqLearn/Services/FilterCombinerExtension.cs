using LinqLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LinqLearn.Services
{
    public static class FilterCombinerExtension
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(
                left.Body,
                Expression.Invoke(right, left.Parameters)
            ),
            left.Parameters);
        }
    }
}
