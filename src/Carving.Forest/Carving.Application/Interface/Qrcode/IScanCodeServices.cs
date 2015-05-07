// Carving.Application IScanCode.cs
// writer sundy
// Last Update Time 2015-04-14-19:48
// Create Time 2015-04-14-19:48

using Carving.Domain.Model;

namespace Carving.Application
{
    public interface IScanCodeServices
    {
        QrCode ScanCode();
    }
}