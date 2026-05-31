using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.Enums.User
{
    public enum AdminCreateUserResult
    {
        Success,
        Error,
        EmailDuplicated,
        MobileDuplicated,
        UserNameDuplicated,
        NationalCodeDuplicated,
        InvalidImage,
        UnknownError,
        DatabaseError
    }
}
