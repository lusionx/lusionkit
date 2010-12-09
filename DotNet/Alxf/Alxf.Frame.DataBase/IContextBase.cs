using System;
namespace Alxf.Frame.DataBase
{
    interface IContextBase<TContext> where TContext : System.Data.Linq.DataContext
    {
        TContext Current { get; }
        TContext New { get; }
        void Reset();
    }
}
