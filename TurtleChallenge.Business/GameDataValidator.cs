using System.Collections.Generic;
using System.Linq;

using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Allows the games rules to be validated. </summary>
    public class GameDataValidator : IGameDataValidator
    {
        /// <summary> Validates that the initial game settings is valid. </summary>
        /// <param name="gameSettings">Game settings to validate.</param>
        /// <returns>Null if no errors found, list containing errors otherwise.</returns>
        public IEnumerable<string> ValidateGameSettings(GameSettings gameSettings)
        {
            List<string> errorList = new List<string>();

            if (CheckGameBoardHasValidNumberOfColumnsAndRows(gameSettings.GameBoard))
            {
                errorList.Add("Game board has invalid rows / columns.");
            }

            // Check starting point is within the game board

            if (!CheckLocationExitsOnBoard(gameSettings.GameBoard, gameSettings.StartingPoint))
            {
                errorList.Add("Starting point is not valid for the game board.");
            }

            // check exit point is within the game board
            if (!CheckLocationExitsOnBoard(gameSettings.GameBoard, gameSettings.ExitPoint))
            {
                errorList.Add("Exit point is not valid for the game board.");
            }
            
            // check all mines are within game board
            if(!CheckMinesAreWithinGameBoard(gameSettings.GameBoard, gameSettings.Mines))
            {
                errorList.Add("Mine(s) are outside the game board.");
            }
            
            // starting point is not on any of the mines.
            if(CheckTheLocationIsOnAMine(gameSettings.Mines, gameSettings.StartingPoint))
            {
                errorList.Add("A mine is on the start location.");
            }

            // exit point is not on any of the mines
            if (CheckTheLocationIsOnAMine(gameSettings.Mines, gameSettings.ExitPoint))
            {
                errorList.Add("A mine is on the exit location.");
            }

            // start and exit cant be on the same point.
            if(this.CheckStartAndExitPointsAreSame(gameSettings.StartingPoint, gameSettings.ExitPoint))
            {
                errorList.Add("Start and Exit locations can not be the same.");
            }

            return errorList;
        }

        private bool CheckLocationExitsOnBoard(GameBoard board, Point location)
        {
            return location.X < board.Columns && location.Y < board.Rows;
        }

        private bool CheckGameBoardHasValidNumberOfColumnsAndRows(GameBoard board)
        {
            return !(board.Columns > 0 && board.Rows > 0);
        }

        private bool CheckStartAndExitPointsAreSame(Point startingPoint, Point exitPoint)
        {
            return startingPoint.Equals(exitPoint);
        }

        private bool CheckTheLocationIsOnAMine(List<Point> mines, Point location)
        {
            return mines.Contains(location, new PointComparer());
        }

        private bool CheckMinesAreWithinGameBoard(GameBoard board, List<Point> mines)
        {
            return !mines.Any(nextMine => nextMine.Y >= board.Rows || nextMine.X >= board.Columns);
        }
    }
}