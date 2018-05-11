using System.Reflection;

using Autofac.Extras.IocManager;

using Microsoft.Azure.WebJobs.Host.Config;

namespace FunctionApp1
{
    public class Startup : IExtensionConfigProvider
    {
        private static IRootResolver _resolver;

        public void Initialize(ExtensionConfigContext context)
        {
            _resolver = IocBuilder.New
                                  .UseAutofacContainerBuilder()
                                  .RegisterServices(r => r.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly()))
                                  .CreateResolver();

            context.AddBindingRule<InjectAttribute>()
                   .BindToInput<dynamic>(inject => _resolver.Resolve(inject.Type));
        }
    }
}
