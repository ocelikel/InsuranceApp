using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Dispatcher
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider provider) => serviceProvider = provider;

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery
           => ((IQueryHandler<TQuery, TResult>)serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>))).Execute(query);
      
        public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
            => await ((IQueryHandler<TQuery, TResult>)serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>))).ExecuteAsync(query);

    }
}
