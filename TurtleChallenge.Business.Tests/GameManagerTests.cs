using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Expectations;

using TurtleChallenge.DomainModel;
using System;

namespace TurtleChallenge.Business.Tests
{
    [TestFixture]
    public class GameManagerTests
    {

        private IGameDataManager mockDataManager;
        private IGameDataValidator mockDataValidator;
        private IRotator mockRotator;
        private IMoveProcessor[] mockMoveProcessors;

        [SetUp]
        public void Setup()
        {
            this.mockDataManager = MockRepository.GenerateMock<IGameDataManager>();
            this.mockDataValidator = MockRepository.GenerateMock<IGameDataValidator>();
            this.mockRotator = MockRepository.GenerateMock<IRotator>();

            this.mockMoveProcessors = new IMoveProcessor[4];

            this.mockMoveProcessors[0] = MockRepository.GenerateMock<IMoveProcessor>();
            this.mockMoveProcessors[1] = MockRepository.GenerateMock<IMoveProcessor>();
            this.mockMoveProcessors[2] = MockRepository.GenerateMock<IMoveProcessor>();
            this.mockMoveProcessors[3] = MockRepository.GenerateMock<IMoveProcessor>();

            this.mockMoveProcessors[0].Stub(mp => mp.MoveDirection).Return(Direction.North);
            this.mockMoveProcessors[1].Stub(mp => mp.MoveDirection).Return(Direction.East);
            this.mockMoveProcessors[2].Stub(mp => mp.MoveDirection).Return(Direction.South);
            this.mockMoveProcessors[3].Stub(mp => mp.MoveDirection).Return(Direction.West);
        }
        
        [TearDown]
        public void TearDown()
        {
            this.mockDataValidator = null;
            this.mockDataManager = null;
            this.mockRotator = null;
            this.mockMoveProcessors = null;
        }

        [Test]
        public void Start_method_should_return_the_result_of_a_sequence_leading_to_exit()
        {
            //
            // Arrange.
            //
            const string GameSettingsFilePath = "game-settings.json";
            const string MovesFilePath = "moves.json";

            GameSettings gs = new GameSettings();
            gs.Direction = Direction.North;
            gs.GameBoard = new GameBoard() { Columns = 5, Rows = 5 };
            gs.ExitPoint = new Point() { Y = 1, X = 1 };
            gs.StartingPoint = new Point() { Y = 0, X = 0 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 2 },
                new Point(){X = 3, Y = 1 }
            };

            IEnumerable<Sequence> sequences = new List<Sequence>()
            {
                new Sequence()
                {
                    Name = "Sequence 1",
                    Moves = new List<Move>()
                    {
                        Move.StepForward, Move.Turn, Move.StepForward
                    }
                },
            };

            Turtle turtle = new Turtle() { Direction = gs.Direction, Location = new Point() { X = gs.StartingPoint.X, Y = gs.StartingPoint.Y } };

        
            this.SetupMoveProcessors(gs);
            this.mockDataManager.Stub(dm => dm.GetGameSettings(GameSettingsFilePath)).Return(gs);
            this.mockDataManager.Stub(dm => dm.GetMoves(MovesFilePath)).Return(sequences);
            this.mockDataValidator.Stub(dv => dv.ValidateGameSettings(gs)).Return(new List<string>());
            this.mockRotator.Stub(r => r.TurnRight90Degress(gs.Direction)).Return(Direction.East);
            
            IGameManager manager = new GameManager(this.mockDataManager, this.mockRotator, this.mockDataValidator, this.mockMoveProcessors);

            //
            // Act.
            //

            IEnumerable<string> results = manager.Start(GameSettingsFilePath, MovesFilePath);

            //
            // Assert.
            //

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.ToList()[0], Is.EqualTo("Sequence 1: Success!"));
        }

        [Test]
        public void Start_method_should_return_the_result_of_a_sequence_hitting_a_mine()
        {
            //
            // Arrange.
            //
            const string GameSettingsFilePath = "game-settings.json";
            const string MovesFilePath = "moves.json";

            GameSettings gs = new GameSettings();
            gs.Direction = Direction.North;
            gs.GameBoard = new GameBoard() { Columns = 5, Rows = 5 };
            gs.ExitPoint = new Point() { Y = 4, X = 4 };
            gs.StartingPoint = new Point() { Y = 0, X = 0 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 2 },
                new Point(){X = 3, Y = 1 },
                new Point(){X = 1, Y = 1 },
            };

            IEnumerable<Sequence> sequences = new List<Sequence>()
            {
                new Sequence()
                {
                    Name = "Sequence 1",
                    Moves = new List<Move>()
                    {
                        Move.StepForward, Move.Turn, Move.StepForward
                    }
                },
            };

            this.mockDataManager.Stub(dm => dm.GetGameSettings(GameSettingsFilePath)).Return(gs);
            this.mockDataManager.Stub(dm => dm.GetMoves(MovesFilePath)).Return(sequences);
            this.mockDataValidator.Stub(dv => dv.ValidateGameSettings(gs)).Return(new List<string>());
            this.mockRotator.Stub(r => r.TurnRight90Degress(gs.Direction)).Return(Direction.East);

            this.SetupMoveProcessors(gs);

            IGameManager manager = new GameManager(this.mockDataManager, this.mockRotator, this.mockDataValidator, this.mockMoveProcessors);

            //
            // Act.
            //

            IEnumerable<string> results = manager.Start(GameSettingsFilePath, MovesFilePath);

            //
            // Assert.
            //

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.ToList()[0], Is.EqualTo("Sequence 1: Mine hit!"));

        }

        [Test]
        public void Start_method_should_return_the_result_of_a_sequence_StillInDanger()
        {
            //
            // Arrange.
            //
            const string GameSettingsFilePath = "game-settings.json";
            const string MovesFilePath = "moves.json";

            GameSettings gs = new GameSettings();
            gs.Direction = Direction.North;
            gs.GameBoard = new GameBoard() { Columns = 5, Rows = 5 };
            gs.ExitPoint = new Point() { Y = 4, X = 4 };
            gs.StartingPoint = new Point() { Y = 0, X = 0 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 2 },
                new Point(){X = 3, Y = 1 },
                new Point(){X = 4, Y = 1 },
            };

            IEnumerable<Sequence> sequences = new List<Sequence>()
            {
                new Sequence()
                {
                    Name = "Sequence 1",
                    Moves = new List<Move>()
                    {
                        Move.StepForward, Move.Turn, Move.StepForward
                    }
                },
            };

            this.mockDataManager.Stub(dm => dm.GetGameSettings(GameSettingsFilePath)).Return(gs);
            this.mockDataManager.Stub(dm => dm.GetMoves(MovesFilePath)).Return(sequences);
            this.mockDataValidator.Stub(dv => dv.ValidateGameSettings(gs)).Return(new List<string>());
            this.mockRotator.Stub(r => r.TurnRight90Degress(gs.Direction)).Return(Direction.East);
            this.SetupMoveProcessors(gs);
            IGameManager manager = new GameManager(this.mockDataManager, this.mockRotator, this.mockDataValidator, this.mockMoveProcessors);

            //
            // Act.
            //

            IEnumerable<string> results = manager.Start(GameSettingsFilePath, MovesFilePath);

            //
            // Assert.
            //

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.ToList()[0], Is.EqualTo("Sequence 1: Still in danger!"));

        }

        [Test]
       
        public void Start_method_should_return_the_results_of_multiple_sequences()
        {
            //
            // Arrange.
            //
            const string GameSettingsFilePath = "game-settings.json";
            const string MovesFilePath = "moves.json";

            GameSettings gs = new GameSettings();
            gs.Direction = Direction.North;
            gs.GameBoard = new GameBoard() { Columns = 5, Rows = 5 };
            gs.ExitPoint = new Point() { Y = 4, X = 4 };
            gs.StartingPoint = new Point() { Y = 0, X = 0 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 1, Y = 2 },
                new Point(){X = 3, Y = 1 },
                new Point(){X = 4, Y = 1 },
            };

            IEnumerable<Sequence> sequences = new List<Sequence>()
            {
                new Sequence()
                {
                    Name = "Sequence 1",
                    Moves = new List<Move>() // successsful
                    {
                        Move.Turn, Move.StepForward, Move.StepForward, Move.Turn,Move.Turn,Move.Turn,
                        Move.StepForward,Move.StepForward,Move.StepForward,Move.StepForward,
                        Move.Turn,Move.StepForward, Move.StepForward
                    }
                },
                new Sequence()
                {
                    Name = "Sequence 2",
                    Moves = new List<Move>() // mine hit
                    {
                        Move.Turn, Move.StepForward, Move.StepForward, Move.StepForward, Move.Turn, Move.Turn, Move.Turn, Move.StepForward
                    }
                },
                new Sequence() 
                {
                    Name = "Sequence 3",
                    Moves = new List<Move>() // still in danger
                    {
                        Move.Turn, Move.StepForward, Move.StepForward, Move.Turn, Move.Turn, Move.Turn, Move.StepForward, Move.StepForward
                    }
                },
            };

            this.mockDataManager.Stub(dm => dm.GetGameSettings(GameSettingsFilePath)).Return(gs);
            this.mockDataManager.Stub(dm => dm.GetMoves(MovesFilePath)).Return(sequences);
            this.mockDataValidator.Stub(dv => dv.ValidateGameSettings(gs)).Return(new List<string>());
            this.mockRotator.Stub(r => r.TurnRight90Degress(gs.Direction)).Return(Direction.East);
            this.SetupMoveProcessors(gs);
            IGameManager manager = new GameManager(this.mockDataManager, this.mockRotator, this.mockDataValidator, this.mockMoveProcessors);

            //
            // Act.
            //

            IEnumerable<string> results = manager.Start(GameSettingsFilePath, MovesFilePath);

            //
            // Assert.
            //

            Assert.That(results.Count(), Is.EqualTo(3));
            Assert.That(results.ToList()[0], Is.EqualTo("Sequence 1: Success!"));
            Assert.That(results.ToList()[1], Is.EqualTo("Sequence 2: Mine hit!"));
            Assert.That(results.ToList()[2], Is.EqualTo("Sequence 3: Still in danger!"));

        }

        [Test]
        public void Start_method_should_return_list_of_errors_from_gameSettings_File()
        {
            //
            // Arrange.
            //
            const string GameSettingsFilePath = "game-settings.json";
            GameSettings gs = new GameSettings();
            IEnumerable<string> errorsFromGameSettings = new List<string>() { "Mine(s) are outsite the game board.", "Starting point is on Exit point."};
            this.mockDataValidator.Stub(dv => dv.ValidateGameSettings(gs)).Return(errorsFromGameSettings);
            
            this.mockDataManager.Stub(dm => dm.GetGameSettings(GameSettingsFilePath)).Return(gs);
            IGameManager manager = new GameManager(this.mockDataManager, this.mockRotator, this.mockDataValidator, this.mockMoveProcessors);

            //
            // Act.
            //

            IEnumerable<string> results = manager.Start(GameSettingsFilePath, "");

            //
            // Arrange.
            //


            Assert.That(results.Count(), Is.EqualTo(2));
            Assert.That(results.ToList()[0], Is.EqualTo("Mine(s) are outsite the game board."));
            Assert.That(results.ToList()[1], Is.EqualTo("Starting point is on Exit point."));

        }


        [Test]
        public void Smallest_board_with_a_mine_successfull_result()
        {
            //
            // Arrange.
            //

            const string GameSettingsFilePath = "game-settings.json";
            const string MovesFilePath = "moves.json";

            GameSettings gs = new GameSettings();
            gs.Direction = Direction.North;
            gs.GameBoard = new GameBoard() { Columns = 2, Rows = 2 };
            gs.ExitPoint = new Point() { Y = 0, X = 1 };
            gs.StartingPoint = new Point() { Y = 0, X = 0 };
            gs.Mines = new List<Point>()
            {
                new Point(){X = 0, Y = 1 }
            };

            IEnumerable<Sequence> sequences = new List<Sequence>()
            {
                new Sequence()
                {
                    Name = "Sequence 1",
                    Moves = new List<Move>() // successsful
                    {
                        Move.Turn, Move.StepForward 
                    }
                }
            };

            this.mockDataManager.Stub(dm => dm.GetGameSettings(GameSettingsFilePath)).Return(gs);
            this.mockDataManager.Stub(dm => dm.GetMoves(MovesFilePath)).Return(sequences);
            this.mockDataValidator.Stub(dv => dv.ValidateGameSettings(gs)).Return(new List<string>());
            this.mockRotator.Stub(r => r.TurnRight90Degress(gs.Direction)).Return(Direction.East);
            this.SetupMoveProcessors(gs);
            IGameManager manager = new GameManager(this.mockDataManager, this.mockRotator, this.mockDataValidator, this.mockMoveProcessors);

            //
            // Act.
            //

            IEnumerable<string> results = manager.Start(GameSettingsFilePath, MovesFilePath);

            //
            // Assert.
            //

            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.ToList()[0], Is.EqualTo("Sequence 1: Success!"));
        }



        [Test]
        public void JustStartAndEndPoint()
        {
            //
            // Arrange.
            //

            const string GameSettingsFilePath = "game-settings.json";
            const string MovesFilePath = "moves.json";

            GameSettings gs = new GameSettings();
            gs.Direction = Direction.North;
            gs.GameBoard = new GameBoard() { Columns = 1, Rows = 2 };

            gs.ExitPoint = new Point() { Y = 1, X = 0 };
            gs.StartingPoint = new Point() { Y = 0, X = 0 };

            gs.Mines = new List<Point>()
            {
               // new Point(){X = 0, Y = 1 }
            };

            IEnumerable<Sequence> sequences = new List<Sequence>()
            {
                new Sequence()
                {
                    Name = "Sequence 1",
                    Moves = new List<Move>() // successsful
                    {
                        Move.StepForward
                    }
                }
            };

            this.mockDataManager.Stub(dm => dm.GetGameSettings(GameSettingsFilePath)).Return(gs);
            this.mockDataManager.Stub(dm => dm.GetMoves(MovesFilePath)).Return(sequences);
            this.mockDataValidator.Stub(dv => dv.ValidateGameSettings(gs)).Return(new List<string>());
            this.mockRotator.Stub(r => r.TurnRight90Degress(gs.Direction)).Return(Direction.East);
            this.SetupMoveProcessors(gs);
            IGameManager manager = new GameManager(this.mockDataManager, this.mockRotator, this.mockDataValidator, this.mockMoveProcessors);

            //
            // Act.
            //

            IEnumerable<string> results = manager.Start(GameSettingsFilePath, MovesFilePath);

            //
            // Assert.
            //


            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.ToList()[0], Is.EqualTo("Sequence 1: Success!"));

        }


        private void SetupMoveProcessors(GameSettings gs)
        {
            this.mockMoveProcessors[0].Stub(mp => mp.ProcessMove(Arg<GameBoard>.Is.Anything, Arg<Turtle>.Is.Anything))
              .Return(true)
              .WhenCalled(_ =>
              {
                  Turtle t = (Turtle)_.Arguments[1];
                  if (t.Location.Y < gs.GameBoard.Rows)
                  {
                      t.Location.Y++;
                      _.ReturnValue = true;
                  }
                  else
                      _.ReturnValue = false;
              });
            this.mockMoveProcessors[1].Stub(mp => mp.ProcessMove(Arg<GameBoard>.Is.Equal(gs.GameBoard), Arg<Turtle>.Is.Anything)).Return(true)
                 .WhenCalled(_ =>
                 {
                     Turtle t = (Turtle)_.Arguments[1];
                     if (t.Location.X < gs.GameBoard.Columns)
                     {
                         t.Location.X++;
                         _.ReturnValue = true;
                     }
                     else
                         _.ReturnValue = false;
                 });
            this.mockMoveProcessors[2].Stub(mp => mp.ProcessMove(Arg<GameBoard>.Is.Equal(gs.GameBoard), Arg<Turtle>.Is.Anything)).Return(true)
                 .WhenCalled(_ =>
                 {
                     Turtle t = (Turtle)_.Arguments[1];
                     if (t.Location.Y > 0)
                     {
                         t.Location.Y--;
                         _.ReturnValue = true;
                     }
                     else
                         _.ReturnValue = false;
                 });
            this.mockMoveProcessors[3].Stub(mp => mp.ProcessMove(Arg<GameBoard>.Is.Equal(gs.GameBoard), Arg<Turtle>.Is.Anything)).Return(true)
                 .WhenCalled(_ =>
                 {
                     Turtle t = (Turtle)_.Arguments[1];
                     if (t.Location.Y < gs.GameBoard.Rows)
                     {
                         t.Location.Y++;
                         _.ReturnValue = true;
                     }
                     else
                         _.ReturnValue = false;
                 });
        }
    }
}