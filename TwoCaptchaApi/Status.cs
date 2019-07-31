using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoCaptchaApi
{
    public enum Status
    {
        Success,
        Error,
        Timeout,
        CaptchaNotReady,
        BadKey
    }
}
