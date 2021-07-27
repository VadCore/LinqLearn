using System;
using System.Linq.Expressions;
using LinqLearn.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace LinqLearn.Services
{
    public class GameFilterProvider : IGameFilterProvider
    {

        

        public Func<Game, bool> GetFilterFunction(GameSearchSettings filter)
        {
            //var minPrice = filter.MinPrice ?? 0;
            //var maxPrice = (filter.MaxPrice is null || filter.MaxPrice == 0)  ? decimal.MaxValue : filter.MaxPrice;
            //var genresCount = filter.Genres.Count();

            //Expression<Func<Game, bool>> predicate = game =>
            //(string.IsNullOrEmpty(filter.Name) || game.Name == filter.Name)
            //&&
            //game.Price <= maxPrice && game.Price >= minPrice
            //&&
            //(genresCount == 0 || game.Genres.Intersect(filter.Genres).Count() == genresCount);

            //return predicate.Compile();

            Expression<Func<Game, bool>> predicate = game => true;

            if (!string.IsNullOrEmpty(filter.Name))
                predicate = predicate.And(g => g.Name == filter.Name);

            if(filter.MinPrice != null && filter.MinPrice != 0)
                predicate = predicate.And(g => g.Price >= filter.MinPrice);

            if (filter.MaxPrice != null && filter.MaxPrice != 0)
                predicate = predicate.And(g => g.Price <= filter.MaxPrice);

            if (filter.Genres.Count > 0)
                predicate = predicate.And(g => g.Genres.Intersect(filter.Genres).Count() == filter.Genres.Count);



            return predicate.Compile();
        }
    }
}
