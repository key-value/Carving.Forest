﻿// Carving.Domain.Repository.EF TableRepository.cs
// writer sundy
// Last Update Time 2015-04-14-20:27
// Create Time 2015-04-14-20:27

using System.ComponentModel.Composition;
using Carving.Domain.Core.Repositories;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF.Repository;
using Carving.Infrastructrue.Aop;

namespace Carving.Domain.Repository.EF.Data
{
    [Injection(typeof(ITableRepository))]
    public class TableRepository : BaseRepository<Table>, ITableRepository
    {
        public TableRepository(IRepositoryContext context)
            : base(context)
        {
        }
    }
}