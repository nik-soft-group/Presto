﻿using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NiksoftCore.ITCF.Service
{
    public interface IProductGroupService : IDataService<ProductGroup>
    {
    }

    public class ProductGroupService : DataService<ProductGroup>, IProductGroupService
    {
        public ProductGroupService(IITCFUnitOfWork uow) : base(uow)
        {
        }

        public override IList<ProductGroup> GetPartOptional(List<Expression<Func<ProductGroup, bool>>> predicate, int startIndex, int pageSize)
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