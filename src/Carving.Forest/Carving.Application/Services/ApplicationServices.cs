using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carving.Domain.Core.Repositories;
using Carving.Domain.Repository.EF.Repository;

namespace Carving.Application.Services
{
    public class ApplicationServices
    {
        public IRepositoryContext RepositoryContext { get; set; }
    }
}
