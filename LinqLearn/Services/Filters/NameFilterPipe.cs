using System;
using System.Linq.Expressions;
using LinqLearn.Models;

namespace LinqLearn.Services.Filters
{
    public class NameFilterPipe : BaseFilterPipe
    {
        private readonly string _name;

        public NameFilterPipe( string name)
        {
            _name = name;
        }

        public override Expression<Func<Game, bool>> Apply(Expression<Func<Game, bool>> previousExpression)
        {
            Expression<Func<Game, bool>> newExp = g => g.Name.Contains(_name, StringComparison.OrdinalIgnoreCase) 
                                                       || _name.Contains(g.Name, StringComparison.OrdinalIgnoreCase) ;

            return previousExpression.And(newExp);
        }
    }
}
