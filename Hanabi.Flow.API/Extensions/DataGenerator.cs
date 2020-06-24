using Hanabi.Flow.Data;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hanabi.Flow.API.Extensions
{
    public static class DataGenerator
    {
        public static void UseDataGenerator(this IApplicationBuilder app, MyContext myContext)
        {
            myContext.GeneratorData();
        }
    }
}
