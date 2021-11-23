using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Application.Attributes;
using SyZero.Application.Service;

namespace Template1.Template2.Application
{
    [DynamicWebApi]
    public  interface IApplicationServiceBase : IApplicationService, IDynamicWebApi
    {
    }
}

