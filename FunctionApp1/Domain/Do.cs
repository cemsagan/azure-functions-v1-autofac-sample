using Autofac.Extras.IocManager;

namespace FunctionApp1
{
    public class Do : IDo, ITransientDependency
    {
      public string GetNameMessage(string name)
      {
          return $"Your name is {name}";
      }
    }
}
