using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqLearn.Models;
using LinqLearn.Services.Filters;

namespace LinqLearn.Services
{
    public class GameFilterProvider : IGameFilterProvider
    {
        public Func<Game, bool> GetFilterFunction(GameSearchSettings searchSettingsCollection)
        {
            var filterPipes = RegisterPipes(searchSettingsCollection);

            var filterExpression = ApplyPipeline(filterPipes);

            return filterExpression.Compile();
        }

        private Expression<Func<Game, bool>> ApplyPipeline(IEnumerable<BaseFilterPipe> filterPipes)
        {
            Expression<Func<Game, bool>> filterExpression = g => true;

            filterExpression = filterPipes.Aggregate(filterExpression, (current, pipe) => pipe.Apply(current));

            return filterExpression;
        }

        private IEnumerable<BaseFilterPipe> RegisterPipes(GameSearchSettings searchSettings)
        {
            var pipes = new List<BaseFilterPipe>();

            if (searchSettings.Genres != null && searchSettings.Genres.Any())
            {
                pipes.Add(new GenresFilterPipe(searchSettings.Genres));
            }

            if (searchSettings.MaxPrice.HasValue || searchSettings.MinPrice.HasValue)
            {
                pipes.Add(new PriceFilterPipe(searchSettings.MaxPrice, searchSettings.MinPrice));
            }

            if (!string.IsNullOrWhiteSpace(searchSettings.Name) && searchSettings.Name.Length > 3)
            {
                pipes.Add(new NameFilterPipe(searchSettings.Name));
            }

            return pipes;
        }
    }
}
