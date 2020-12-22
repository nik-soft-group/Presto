using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiksoftCore.ITCF.Service
{
    public interface IUserLegalFormService : IDataService<UserLegalForm>
    {
    }

    public class UserLegalFormService : DataService<UserLegalForm>, IUserLegalFormService
    {
        public UserLegalFormService(IITCFUnitOfWork uow) : base(uow)
        {
        }

        public override IList<UserLegalForm> GetPartOptional(List<Expression<Func<UserLegalForm, bool>>> predicate, int startIndex, int pageSize)
        {
            var query = TEntity.Where(predicate[0]);
            for (int i = 1; i < predicate.Count; i++)
            {
                query = query.Where(predicate[i]);
            }
            return query.OrderByDescending(i => i.Id).ThenBy(t => t.Id).Skip(startIndex).Take(pageSize).ToList();
        }
    }
}