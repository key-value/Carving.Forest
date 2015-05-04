// Carving.Domain.Repository.EF QrCodeRepository.cs
// writer sundy
// Last Update Time 2015-04-29-19:52
// Create Time 2015-04-29-19:52

using Carving.Domain.Core.Repositories;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF.Repository;
using Carving.Infrastructrue.Aop;

namespace Carving.Domain.Repository.EF.Data
{
    [Injection(typeof(IQrCodeRepository))]
    public class QrCodeRepository : BaseRepository<QrCode>, IQrCodeRepository
    {
        public QrCodeRepository(IRepositoryContext context)
            : base(context)
        {
        }
    }
}