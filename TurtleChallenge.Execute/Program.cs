using System;
using System.Collections.Generic;
using log4net;
using Microsoft.Practices.Unity;
using TurtleChallenge.Business;
using TurtleChallenge.Business.Utilities;
using TurtleChallenge.Execute.Infrastructure;

namespace TurtleChallenge.Execute
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            try
            {
                /*
                 * Uncomment the following lines to run this project in debug more with following files.
                 */

                 //args = new string[2];
                 //args[0] = "game-settings.xml";
                 //args[1] = "moves.xml";

                log4net.Config.XmlConfigurator.Configure();

                Logger.InfoFormat("------------------------------------");
                Logger.InfoFormat("Turtle challenge game launched.");
                Logger.InfoFormat("------------------------------------");

                TurtleChallengeIoCConfigurator.ConfigureIoC();

                IGameManager manager = IoCContainer.Instance().Resolve<IGameManager>();

                string gameSettingsFile = args[0]; // assuming the user inputs the game settings file with its extension being  xml or json..
                string movesFile = args[1]; // assuming the user inputs the moves file with its extension being xml or json.

                Logger.InfoFormat($"Starting the game with {gameSettingsFile} and {movesFile} files.");
                IEnumerable<string> sequenceResults = manager.Start(gameSettingsFile, movesFile);
                
                foreach (string sequenceResult in sequenceResults)
                {
                    Console.WriteLine(sequenceResult);
                    Logger.InfoFormat(sequenceResult);
                }

                Logger.InfoFormat("------------------------------------");
                Logger.InfoFormat("Turtle challenge game exits.");
                Logger.InfoFormat("------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("A fatel error occured during the execution of the game. Please check the log file for the detailed error message.\n");
                Logger.ErrorFormat($"A fatel error occured during the execution of the game. Details {ex}");
            }
        }
    }
}