using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiksoftCore.ITCF.Service
{
    public interface IProductService : IDataService<Product>
    {
    }

    public class ProductService : DataService<Product>, IProductService
    {
        public ProductService(IITCFUnitOfWork uow) : base(uow)
        {
        }

        public override IList<Product> GetPartOptional(List<Expression<Func<Product, bool>>> predicate, int startIndex, int pageSize)
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