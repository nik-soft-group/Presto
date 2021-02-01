using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiksoftCore.FormBuilder.Service
{
    public interface IListControlService : IDataService<ListControl>
    {
    }

    public class ListControlService : DataService<ListControl>, IListControlService
    {
        public ListControlService(IFbUnitOfWork uow) : base(uow)
        {
        }

        public override IList<ListControl> GetPartOptional(List<Expression<Func<ListControl, bool>>> predicate, int startIndex, int pageSize)
        {
            var query = TEntity.Where(predicate[0]);
            for (int i = 1; i < predicate.Count; i++)
            {
                query = query.Where(predicate[i]);
            }
            return query.OrderBy(i => i.Id).ThenBy(t => t.Id).Skip(startIndex).Take(pageSize).ToList();
        }
    }
}