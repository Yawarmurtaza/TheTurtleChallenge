using Microsoft.Practices.Unity;

namespace TurtleChallenge.Business.Utilities
{
    public static class IoCContainer
    {
        private static IUnityContainer container;
     
        public static IUnityContainer Instance() => container ?? (container = new UnityContainer());
    }
}