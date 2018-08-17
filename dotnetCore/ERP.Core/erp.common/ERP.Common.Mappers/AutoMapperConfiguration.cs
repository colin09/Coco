using System;
using System.Linq;
using System.Reflection;

namespace ERP.Common.Mappers
{
    public class AutoMapperConfiguration
    {
        private static bool initialized = false;
        [STAThread]
        public static void Initialize(Assembly[] assemblies)
        {
            if (initialized) return;

            AutoMapper.Mapper.Initialize(config =>
            {
                //注册 mapperConfig
                var autoMapperConfigs = assemblies.SelectMany(a => a.GetTypes().Where(t => t.IsPublic && !t.IsInterface && !t.IsAbstract)
                    .Where(t => typeof(IAutoMapperConfig).IsAssignableFrom(t)).ToArray()).ToArray();
                IAutoMapperConfig tempConfig;
                foreach (var autoMapperConfig in autoMapperConfigs)
                {
                    tempConfig = (IAutoMapperConfig)Activator.CreateInstance(autoMapperConfig);
                    tempConfig.Map(config);
                }
            });

            initialized = true;
        }
    }
}