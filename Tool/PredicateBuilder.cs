using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Tool
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
/*
 * var context = GenerateContext();  

02 Expression<Func<Customer, bool>> filter = PredicateBuilder.False<Customer>();  

03 string[] cities = { "London", "Paris" };  

04    

05 foreach (string item in cities)  

06 {  

07     string tmp = item;  

08     filter = filter.Or(c => c.City == tmp);  

09 }  

10    

11 context.Log = Console.Out;  

12 context.Customers.Where(filter).Count().Dump(); 

 */
