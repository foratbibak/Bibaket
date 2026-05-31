using Bibaket.Domain.Enums.User;
using Bibaket.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<AdminCreateUserResult> CreatUserInAdmin(AdminCreatUserViewModel model);
    }
}
