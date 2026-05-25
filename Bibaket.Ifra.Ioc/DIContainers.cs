using Bibaket.Application.Services.Implementation;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domin.Contracts;
using Bibaket.Ifra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Bibaket.Ifra.Ioc
{
    public static class DIContainers
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddScoped<IAccountServices, AccountServices>();
            #endregion
        }
    }
}
