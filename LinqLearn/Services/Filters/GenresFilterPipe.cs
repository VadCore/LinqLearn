using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqLearn.Models;

namespace LinqLearn.Services.Filters
{
    public class GenresFilterPipe : BaseFilterPipe
    {
        private readonly ICollection<string> _genres;

        public GenresFilterPipe(ICollection<string> genres)
        {
            _genres = genres;
        }

        public override Expression<Func<Game, bool>> Apply(Expression<Func<Game, bool>> previousExpression)
        {
            Expression<Func<Game, bool>> newExp = g => g.Genres.Intersect(_genres).Any();

            return previousExpression.And(newExp);
        }
    }
}
