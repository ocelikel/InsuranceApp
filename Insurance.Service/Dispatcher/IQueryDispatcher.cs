using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Dispatcher
{
    public interface IQueryDispatcher
    {
        TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery;

        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
