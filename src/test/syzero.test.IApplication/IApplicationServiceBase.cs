using SyZero.Application.Attributes;
using SyZero.Application.Service;

namespace syzero.test.IApplication
{
    [DynamicWebApi]
    public interface IApplicationServiceBase : IApplicationService, IDynamicWebApi
    {
    }
}



