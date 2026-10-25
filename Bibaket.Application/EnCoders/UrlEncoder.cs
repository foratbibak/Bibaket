using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.EnCoders
{
    public static class UrlEncoder
    {
        public static string Encoded(string param)
        {
            return  Uri.EscapeDataString(param);

        }
    }
}
