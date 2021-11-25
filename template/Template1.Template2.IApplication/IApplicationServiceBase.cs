using SyZero.Application.Attributes;
using SyZero.Application.Service;

namespace Template1.Template2.IApplication
{
    [DynamicWebApi]
    public interface IApplicationServiceBase : IApplicationService, IDynamicWebApi
    {
    }
}



