using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Dispatcher
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        TResult Execute(TQuery query);

        Task<TResult> ExecuteAsync(TQuery query);
    }
}
