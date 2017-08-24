using System.Collections.Generic;
using System.Linq;

using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Allows the game to start with game settings and moves from the files. </summary>
    public class GameManager : IGameManager
    {
        private readonly IGameDataManager dataManager;
        private readonly IGameDataValidator dataValidator;
        private readonly IRotator rotator;
        private readonly IDictionary<Direction, IMoveProcessor> processors;

        public GameManager(IGameDataManager dataManager, IRotator rotator, IGameDataValidator dataValidator, IMoveProcessor[] moveProcessors)
        {
            this.dataManager = dataManager;
            this.rotator = rotator;
            this.dataValidator = dataValidator;
            this.processors = moveProcessors.ToDictionary(p => p.MoveDirection, p => p);
        }

        /// <summary> Starts the game. </summary>
        /// <param name="gameSettingsFilePath">File path to the game initial settings file.</param>
        /// <param name="moveSequenceFilePath">File path to the moves containing the sequences.</param>
        /// <returns>Collection of strings containing the outcome for each sequence in the moves file.</returns>
        public IEnumerable<string> Start(string gameSettingsFilePath, string moveSequenceFilePath)
        {
            GameSettings gameSettings = this.dataManager.GetGameSettings(gameSettingsFilePath);

            // Before starting to process the move sequences, make sure that the game settings has valid entries...
            // Please refer to the section "Assumption Made" in the ReadMe.docx file that defines some assumption that are made to validate the game settings.
            IList<string> results = this.dataValidator.ValidateGameSettings(gameSettings).ToList();
            if (results.Any())
            {
                return results;
            }

            results = new List<string>();

            // Since the game settings are valid. Get the moves from the file.
            IEnumerable<Sequence> moves = this.dataManager.GetMoves(moveSequenceFilePath);

            Turtle turtle = new Turtle();

            foreach (Sequence sequence in moves)
            {
                // assumption made: for every new sequence reset the turtle's direction and current position 
                // to the initial settings from the gamesettings.
                turtle.Location = new Point()
                {
                    Y = gameSettings.StartingPoint.Y,
                    X = gameSettings.StartingPoint.X
                };
                turtle.Direction = gameSettings.Direction;

                results.Add(ProcessSequence(sequence, turtle, gameSettings));
            }

            return results;
        }

        /// <summary> Processes the sequence of moves of the turtle on the board. Each sequence starts with the starting point and direction from the game-settings. </summary>
        /// <param name="sequence">Sequence of the moves.</param>
        /// <param name="turtle">Turtle to navigate.</param>
        /// <param name="gameSettings">Game settings.</param>
        /// <returns>Result of the sequence.</returns>
        private string ProcessSequence(Sequence sequence, Turtle turtle, GameSettings gameSettings)
        {
            string sequenceResult = sequence.Name;
            foreach (Move nextMove in sequence.Moves)
            {
                switch (nextMove)
                {
                    case Move.StepForward:
                        {
                            bool moveSuccess = this.processors[turtle.Direction].ProcessMove(gameSettings.GameBoard, turtle);
                            
                            // Becuase its not clear in the question about the condition when turtle hits the border.
                            // I have commented the code below which allows the current sequence of moves to stop
                            // and return border hit message and process the next sequence if any.
                            /*
                            if (!moveSuccess)
                            {
                                sequenceResult += ": " + turtle.Direction + " border hit!";
                                return sequenceResult;
                            }
                            */

                            if (gameSettings.Mines.Exists(p => p.Y == turtle.Location.Y && p.X == turtle.Location.X))
                            {
                                sequenceResult += ": Mine hit!";
                                return sequenceResult;
                            }

                            if (gameSettings.ExitPoint.Y == turtle.Location.Y &&
                                gameSettings.ExitPoint.X == turtle.Location.X)
                            {
                                sequenceResult += ": Success!";
                                return sequenceResult;
                            }

                            break;
                        }
                    case Move.Turn:
                        {
                            turtle.Direction = this.rotator.TurnRight90Degress(turtle.Direction);
                            break;
                        }
                }
            }

            return sequenceResult + ": Still in danger!";
        }
    }
}