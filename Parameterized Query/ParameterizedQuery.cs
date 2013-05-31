using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using System.Linq.Expressions;
namespace Parameterized_Query
{
    public class ParameterizedQuery<T>
    {
        private IQueryable<T> _queryable;
        private IEnumerable<SearchParameter> _searchParams;

        public ParameterizedQuery(IQueryable<T> queryable,IEnumerable<SearchParameter> searchParams)
        {
            _queryable = queryable;
            _searchParams = searchParams;
         
        }

        public IQueryable<T> Results()
        {
           
            var paramExpr = Expression.Parameter(typeof(T),"z");
 

            foreach(var p in _searchParams)
            {
                var propExpression = Expression.Property(paramExpr,p.FieldName);
                var searchExpr = Expression.Constant(p.SearchValue);
                var searchExpr2 = Expression.Constant(p.SearchValue2);

                Expression where;
                switch(p.SearchType)
                {
                    case ComparisonType.Equals:
                        where = Expression.Equal(propExpression,searchExpr);
                        break;
                    case ComparisonType.LessThan:
                        where = Expression.LessThan(propExpression, searchExpr);
                        break;
                    case ComparisonType.MoreThan:
                        where = Expression.GreaterThan(propExpression, searchExpr);
                        break;
                    case ComparisonType.Contains:
                        where = Expression.Call(propExpression, typeof(string).GetMethod("Contains"), searchExpr);
                        break;
                    case ComparisonType.Between:
                        where = Expression.And(Expression.GreaterThanOrEqual(propExpression, searchExpr), Expression.LessThanOrEqual(propExpression, searchExpr2));
                        break;
                    case ComparisonType.StartsWith:
                        where = Expression.Call(propExpression, typeof(string).GetMethod("StartsWith"), searchExpr);
                        break;
                    default:
                        where = Expression.Equal(propExpression,searchExpr);
                        break;

                };

                _queryable = _queryable.Where(Expression.Lambda<Func<T, bool>>(where, paramExpr));
            }


            return _queryable;

        }
    }



}
