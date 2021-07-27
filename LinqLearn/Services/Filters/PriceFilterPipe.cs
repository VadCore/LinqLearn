using System;
using System.Linq.Expressions;
using LinqLearn.Models;

namespace LinqLearn.Services.Filters
{
    public class PriceFilterPipe : BaseFilterPipe
    {
        private readonly decimal? _priceFrom;
        private readonly decimal? _priceTo;

        public PriceFilterPipe( decimal? priceTo, decimal? priceFrom)
        {
            _priceTo = priceTo;
            _priceFrom = priceFrom;
        }

        public override Expression<Func<Game, bool>> Apply(Expression<Func<Game, bool>> previousExpression)
        {
            Expression<Func<Game, bool>> fromExp = g => true;
            Expression<Func<Game, bool>> toExp = g => true;

            if (_priceFrom.HasValue)
            {
                fromExp = g => g.Price>=_priceFrom.Value;
            }

            if (_priceTo.HasValue)
            {
                toExp = g => g.Price <= _priceTo.Value;
            }

            return previousExpression.And(fromExp).And(toExp);
        }
    }
}
