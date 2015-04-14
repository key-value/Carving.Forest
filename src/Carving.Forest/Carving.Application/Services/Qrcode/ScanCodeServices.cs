
using Carving.Domain.Core.Specifications;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF;

namespace Carving.Application
{
    public class ScanCodeServices : IScanCodeServices
    {
        private ITableRepository _tableRepository;


        public int ScanCode()
        {
            if (_tableRepository == null)
            {
                return 0;
            }
            _tableRepository.Find(Specification<Table>.Eval(x => x.Name == "xxx"));
            return 1;
        }
    }
}
