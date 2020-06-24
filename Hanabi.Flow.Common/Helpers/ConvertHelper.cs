using System;
using System.Collections.Generic;
using System.Text;

namespace Hanabi.Flow.Common.Helpers
{   
    public static class ConvertHelper
    {
        public static T ObjToEnum<T>(this object value) where T : Enum
        {
            Enum.TryParse(typeof(T), Convert.ToString(value), out object returnValue);

            if (returnValue != null)
            {
                return (T)returnValue;
            }

            return default;
        }
    }
}
