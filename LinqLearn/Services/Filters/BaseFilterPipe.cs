using System;
using System.Linq.Expressions;
using LinqLearn.Models;

namespace LinqLearn.Services.Filters
{
    public abstract class BaseFilterPipe
    {
        protected BaseFilterPipe()
        {
        }

        public virtual Expression<Func<Game, bool>> Apply(Expression<Func<Game,bool>> previousExpression)
        {
            return previousExpression;
        }
    }
}
