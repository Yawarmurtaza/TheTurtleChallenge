using log4net;
using Microsoft.Practices.Unity;

using TurtleChallenge.Business;
using TurtleChallenge.Business.Utilities;
using TurtleChallenge.DataAccess;

namespace TurtleChallenge.Execute.Infrastructure
{
    /// <summary>
    /// The IoC configurator for Turtle challenge.
    /// </summary>
    public class TurtleChallengeIoCConfigurator
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void ConfigureIoC()
        {
            IUnityContainer container = IoCContainer.Instance();
            container.RegisterType<IDataProvider, XmlDataProvider>(".xml"); // for the xml files
            container.RegisterType<IDataProvider, JsonDataProvider>(".json"); // for json files
            container.RegisterType<IGameDataManager, GameDataManager>();
            container.RegisterType<IGameManager, GameManager>();
            container.RegisterType<IGameDataValidator, GameDataValidator>();
            container.RegisterType<IRotator, Rotator>();
            container.RegisterType<IMoveProcessor, NorthMoveProcessor>("NorthMoveProcessor");
            container.RegisterType<IMoveProcessor, EastMoveProcessor>("EastMoveProcessor");
            container.RegisterType<IMoveProcessor, SouthMoveProcessor>("SouthMoveProcessor");
            container.RegisterType<IMoveProcessor, WestMoveProcessor>("WestMoveProcessor");
            container.RegisterType<IFileReadonlyAccess, FileReadOnlyAccess>();

            Logger.InfoFormat("Dependencies registered successfully.");
        }
    }
}