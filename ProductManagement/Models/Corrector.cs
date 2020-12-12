using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public static class Corrector
    {
        public static T isNull<T>(this T v1, T defaultValue)
        {
            return v1 == null ? defaultValue : v1;
        }
    }
}