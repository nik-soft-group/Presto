using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiksoftCore.FormBuilder.Service
{
    public interface IFormCategroyService : IDataService<FormCategroy>
    {
    }

    public class FormCategroyService : DataService<FormCategroy>, IFormCategroyService
    {
        public FormCategroyService(IFbUnitOfWork uow) : base(uow)
        {
        }

        public override IList<FormCategroy> GetPartOptional(List<Expression<Func<FormCategroy, bool>>> predicate, int startIndex, int pageSize)
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