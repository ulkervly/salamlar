using App.Business.Services.Implementation;
using App.Business.Services.Interfaces;
using App.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.ServiceRegistration
{
     public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
        }
    }
}
