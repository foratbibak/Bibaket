using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Extensions
{
    public static class PriceConvertor
    {
        public static string ToToman(this int price)
        {
            return price.ToString("#,0 تومان");
        }
        public static string ToToman(this double price)
        {
            return price.ToString("#,0 تومان");
        }
    }
}
