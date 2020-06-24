using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hanabi.Flow.Common.Helpers
{
    public class AppSettings
    {
        static IConfiguration Configuration { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {

                if (sections.Any())
                {
                    // 获取配置中的对应内容
                    return Configuration[string.Join(":", sections)];
                }
                
            }
            catch (Exception) { }

            return "";
        }
    }
}
