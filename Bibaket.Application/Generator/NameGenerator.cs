using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Generator
{
    public static class NameGenerator
    {
        public static string GenerateUniqName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
