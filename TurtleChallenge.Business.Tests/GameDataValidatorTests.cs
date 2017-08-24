using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business.Tests
{
    [TestFixture]
    public class GameDataValidatorTests
    {

        private IGameDataValidator validator;

        [SetUp]
        public void Setup()
        {
            this.validator = new GameDataValidator();
        }


        [TearDown]
        public void TearDown()
        {
            this.validator = null;
        }



        [Test]
        public void ValidateGameSettings_Should_return_no_errors_for_a_valid_gameSettings()
        {
            //
            // Arrange.
            //

            GameSettings gs = new GameSettings();

            gs.GameBoard = new GameBoard() { Columns = 50, Rows = 50 };
            gs.ExitPoint = new Point() { Y = 45, X = 25 };
            gs.StartingPoint = new Point() { Y = 12, X = 33 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 10 },
                new Point(){X = 2, Y = 12 },
                new Point(){X = 2, Y = 13 },
                new Point(){X = 3, Y = 14 }
            };

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //

            Assert.That(possibleErrors.Count(), Is.Zero);
        }


        [Test]
        public void ValidateGameSettings_Should_return_error_when_starting_point_is_outside_the_game_board()
        {
            //
            // Arrange.
            //

            GameSettings gs = new GameSettings();

            gs.GameBoard = new GameBoard() { Columns = 50, Rows = 50 };
            gs.ExitPoint = new Point() { Y = 45, X = 25 };
            gs.StartingPoint = new Point() { Y = 120, X = 33 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 10 },
                new Point(){X = 2, Y = 12 },
                new Point(){X = 2, Y = 13 },
                new Point(){X = 3, Y = 14 }
            };

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //

            Assert.That(possibleErrors.Count(), Is.EqualTo(1));
            Assert.That(possibleErrors.ToList()[0], Is.EqualTo("Starting point is not valid for the game board."));
        }


        [Test]
        public void ValidateGameSettings_Should_return_error_when_exit_point_is_out_of_the_game_board()
        {
            //
            // Arrange.
            //

            GameSettings gs = new GameSettings();

            gs.GameBoard = new GameBoard() { Columns = 50, Rows = 50 };
            gs.ExitPoint = new Point() { Y = 95, X = 55 };
            gs.StartingPoint = new Point() { Y = 12, X = 33 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 10 },
                new Point(){X = 2, Y = 12 },
                new Point(){X = 2, Y = 13 },
                new Point(){X = 3, Y = 14 }
            };

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //

            Assert.That(possibleErrors.Count(), Is.EqualTo(1));
            Assert.That(possibleErrors.ToList()[0], Is.EqualTo("Exit point is not valid for the game board."));
        }

        [Test]
        public void ValidateGameSettings_Should_return_error_whe_any_of_the_mines_is_on_start_point()
        {
            //
            // Arrange.
            //

            GameSettings gs = new GameSettings();

            gs.GameBoard = new GameBoard() { Columns = 50, Rows = 50 };
            gs.ExitPoint = new Point() { Y = 5, X = 5 };
            gs.StartingPoint = new Point() { Y = 12, X = 33 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 10 },
                new Point(){X = 2, Y = 12 },
                new Point(){X = 2, Y = 13 },
                new Point(){X = 33, Y = 140 }
            };

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //

            Assert.That(possibleErrors.Count(), Is.EqualTo(1));
            Assert.That(possibleErrors.ToList()[0], Is.EqualTo("Mine(s) are outside the game board."));

        }

        [Test]
        public void ValidateGameSettings_Should_return_error_whe_any_of_the_mines_is_on_exit_location()
        {
            //
            // Arrange.
            //

            GameSettings gs = new GameSettings();

            gs.GameBoard = new GameBoard() { Columns = 50, Rows = 50 };
            gs.ExitPoint = new Point() { Y = 35, X = 45 };
            gs.StartingPoint = new Point() { Y = 12, X = 33 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 10 },
                new Point(){X = 2, Y = 12 },
                new Point(){X = 2, Y = 13 },
                new Point(){X = 45, Y = 35 }
            };

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //

            Assert.That(possibleErrors.Count(), Is.EqualTo(1));
            Assert.That(possibleErrors.ToList()[0], Is.EqualTo("A mine is on the exit location."));
        }



        [Test]
        public void ValidateGameSettings_Should_return_error_whe_any_of_the_mines_is_on_start_location()
        {
            //
            // Arrange.
            //

            GameSettings gs = new GameSettings();

            gs.GameBoard = new GameBoard() { Columns = 50, Rows = 50 };
            gs.ExitPoint = new Point() { Y = 5, X = 4 };
            gs.StartingPoint = new Point() { Y = 10, X = 1 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 10 },
                new Point(){X = 2, Y = 12 },
                new Point(){X = 2, Y = 13 },
                new Point(){X = 45, Y = 35 }
            };

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //

            Assert.That(possibleErrors.Count(), Is.EqualTo(1));
            Assert.That(possibleErrors.ToList()[0], Is.EqualTo("A mine is on the start location."));
        }


        [Test]
        public void Board_Of_1_Tile_Should_Be_invalid()
        {
            //
            // Arrange.
            //

            GameSettings gs = new GameSettings();

            gs.GameBoard = new GameBoard() { Columns = 1, Rows = 1 }; // one col one row = 1 tile as game board. 1 tile has x = 0 and y = 0 co-ordinates.
            gs.ExitPoint = new Point() { Y = 0, X = 0 };
            gs.StartingPoint = new Point() { Y = 0, X = 0 };
            gs.Mines = new List<Point>();

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //

            Assert.That(possibleErrors.Count(), Is.EqualTo(1));
            Assert.That(possibleErrors.ToList()[0], Is.EqualTo("Start and Exit locations can not be the same."));
        }


        [Test]
        public void List_all_invalid_items()
        {
            
            GameSettings gs = new GameSettings();
            gs.GameBoard = new GameBoard() { Columns = 4, Rows = 0 };
            gs.ExitPoint = new Point() { Y = 0, X = 4 };
            gs.StartingPoint = new Point() { Y = 10, X = 1 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 10 },
                new Point(){X = 2, Y = 12 },
                new Point(){X = 2, Y = 13 },
                new Point(){X = 45, Y = 35 }
            };

            //
            // Act.
            //

            IEnumerable<string> possibleErrors = this.validator.ValidateGameSettings(gs);

            //
            // Assert.
            //


            Assert.That(possibleErrors.Count(), Is.GreaterThan(0));
            Assert.That(possibleErrors.ToList()[0], Is.EqualTo("Game board has invalid rows / columns."));
            Assert.That(possibleErrors.ToList()[1], Is.EqualTo("Starting point is not valid for the game board."));
            Assert.That(possibleErrors.ToList()[2], Is.EqualTo("Exit point is not valid for the game board."));
            Assert.That(possibleErrors.ToList()[3], Is.EqualTo("Mine(s) are outside the game board."));
            Assert.That(possibleErrors.ToList()[4], Is.EqualTo("A mine is on the start location."));
        }
    }
}