// Carving.Application IAddQrCodeServices.cs
// writer sundy
// Last Update Time 2015-04-29-19:46
// Create Time 2015-04-29-19:46

using System;

namespace Carving.Application
{
    public interface IAddQrCodeServices
    {
        int Create(string code, Guid tableID);
    }
}