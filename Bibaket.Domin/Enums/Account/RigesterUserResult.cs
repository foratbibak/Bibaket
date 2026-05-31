using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Enums.Account
{
    public enum RegisterUserResult
    {
        Success,
        InValidInputs,
        EmailDuplicated,
        UserNameDuplicated,
        SendActivationEmail,
        Failed
    }
}
