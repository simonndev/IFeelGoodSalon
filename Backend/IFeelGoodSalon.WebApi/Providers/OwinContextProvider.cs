using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IFeelGoodSalon.WebApi.Providers
{
    public interface IOwinContextProvider
    {
        IOwinContext CurrentContext { get; }
    }

    public class OwinContextProvider
    {
    }
}