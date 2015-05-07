using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carving.Domain.Core.Repositories;

namespace Carving.Application.Services
{
    public class ApplicationServices
    {
        public IRepositoryContext RepositoryContext { get; set; }
    }
}
