using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domin.Enums
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
