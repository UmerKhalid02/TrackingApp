using System.Linq.Expressions;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Parameters;

namespace TrackingApp.Application.Extensions
{
    public class OrderPagedList<T> : List<T>
    {
        public OrderPagedList(IEnumerable<T> items, int count, OrderPageParamter pageParams)
        {
            CurrentPage = pageParams.PageNumber;
            PageSize = pageParams.PageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageParams.PageSize);
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static OrderPagedList<T> CreateAsync(IQueryable<T> source, OrderPageParamter pageParams)
        {
            if (source == null) throw new ArgumentNullException("source");
            int count = source.Count();

            // Create a parameter to pass into the Lambda expression
            var parameter = Expression.Parameter(typeof(T), "Entity");

            //  create the selector part, but support child properties (it works without . too)
            String[] childProperties = pageParams.OrderBy.Split('.');
            MemberExpression property = Expression.Property(parameter, childProperties[0]);
            for (int i = 1; i < childProperties.Length; i++)
            {
                property = Expression.Property(property, childProperties[i]);
            }

            LambdaExpression selector = Expression.Lambda(property, parameter);

            string methodName = (pageParams.OrderType.ToLower() == PaginationOrder.Descending.ToLower()) ? "OrderByDescending" : "OrderBy";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                                            new Type[] { source.ElementType, property.Type },
                                            source.Expression, Expression.Quote(selector));

            var res = source.Provider.CreateQuery<T>(resultExp);

            var items = res.Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize);

            return new OrderPagedList<T>(items, count, pageParams);
        }

        public static PaginationResponseModel GetPaginationResponse(IQueryable<T> source, RequestPageParamter pageParams)
        {
            if (source == null) throw new ArgumentNullException("source");
            int count = source.Count();

            if (!string.IsNullOrEmpty(pageParams.OrderBy))
            {
                // Create a parameter to pass into the Lambda expression
                var parameter = Expression.Parameter(typeof(T), "Entity");

                //  create the selector part, but support child properties (it works without . too)
                String[] childProperties = pageParams.OrderBy.Split('.');
                MemberExpression property = Expression.Property(parameter, childProperties[0]);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = Expression.Property(property, childProperties[i]);
                }

                LambdaExpression selector = Expression.Lambda(property, parameter);

                string methodName = (pageParams.OrderType == PaginationOrder.Descending) ? "OrderByDescending" : "OrderBy";

                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(selector));

                source = source.Provider.CreateQuery<T>(resultExp);
            }

            var items = source.Skip((pageParams.PageNumber - 1) * pageParams.PageSize).Take(pageParams.PageSize);

            return new PaginationResponseModel
            {
                Items = items.ToList(),
                Pagination = new Pagination
                {
                    CurrentPage = pageParams.PageNumber,
                    TotalPages = (int)Math.Ceiling(count / (double)pageParams.PageSize),
                    PageSize = pageParams.PageSize,
                    TotalCount = count
                }
            };

        }
    }
}
