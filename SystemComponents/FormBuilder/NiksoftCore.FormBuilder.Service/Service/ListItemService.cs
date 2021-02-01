using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiksoftCore.FormBuilder.Service
{
    public interface IListItemService : IDataService<ListItem>
    {
    }

    public class ListItemService : DataService<ListItem>, IListItemService
    {
        public ListItemService(IFbUnitOfWork uow) : base(uow)
        {
        }

        public override IList<ListItem> GetPartOptional(List<Expression<Func<ListItem, bool>>> predicate, int startIndex, int pageSize)
        {
            var query = TEntity.Where(predicate[0]);
            for (int i = 1; i < predicate.Count; i++)
            {
                query = query.Where(predicate[i]);
            }
            return query.OrderByDescending(i => i.OrderId).ThenBy(t => t.Id).Skip(startIndex).Take(pageSize).ToList();
        }
    }
}