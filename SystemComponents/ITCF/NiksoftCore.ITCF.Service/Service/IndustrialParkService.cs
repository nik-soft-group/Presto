using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiksoftCore.ITCF.Service
{
    public interface IIndustrialParkService : IDataService<IndustrialPark>
    {
    }

    public class IndustrialParkService : DataService<IndustrialPark>, IIndustrialParkService
    {
        public IndustrialParkService(IITCFUnitOfWork uow) : base(uow)
        {
        }

        public override IList<IndustrialPark> GetPartOptional(List<Expression<Func<IndustrialPark, bool>>> predicate, int startIndex, int pageSize)
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