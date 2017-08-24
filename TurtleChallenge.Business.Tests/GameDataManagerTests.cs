using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Rhino.Mocks;
using TurtleChallenge.Business.Utilities;
using TurtleChallenge.DataAccess;
using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business.Tests
{
    [TestFixture]
    public class GameDataManagerTests
    {

        [TestCase(".json")]
        [TestCase(".xml")]
        public void GetGameSettings_Should_Return_Games_Settings_Data_From_File(string fileExtension)
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


            string gameSettingsFile = "game-settings" + fileExtension;
            

            IDataProvider mockDataProvider = MockRepository.GenerateMock<IDataProvider>();
            mockDataProvider.Stub(p => p.GetGameSettings(gameSettingsFile)).Return(gs);

            mockDataProvider.Stub(dp => dp.Extension).Return(fileExtension);

            IDataProvider[] dataProviders = new IDataProvider[1];
            dataProviders[0] = mockDataProvider;

            IoCContainer.Instance().RegisterInstance(fileExtension, mockDataProvider);

            IGameDataManager manager = new GameDataManager(dataProviders);

            //
            // Act.
            //

            GameSettings result = manager.GetGameSettings(gameSettingsFile);


            //
            // Assert.
            //


            Assert.That(result.GameBoard.Columns, Is.EqualTo(gs.GameBoard.Columns));
            Assert.That(result.GameBoard.Rows, Is.EqualTo(gs.GameBoard.Rows));
            Assert.That(result.Direction, Is.EqualTo(gs.Direction));
            mockDataProvider.AssertWasCalled(p => p.GetGameSettings(gameSettingsFile));
            Assert.That(result.Mines.Count(), Is.EqualTo(gs.Mines.Count()));

        }

        [TestCase(".json")]
        [TestCase(".xml")]
        public void GetMoves_should_return_Sequences_From_File(string fileExtension)
        {
            //
            // Arrange.
            //
            string MovesFilePath = "moves" + fileExtension;
            IEnumerable<Sequence> seqList = new List<Sequence>()
            {
                new Sequence()
                    {
                        Moves =  new List<Move>(){Move.StepForward,Move.Turn,Move.StepForward,Move.Turn},
                        Name = "Seq 1"
                    },
            };

            IDataProvider mockDataProvider = MockRepository.GenerateMock<IDataProvider>();
            mockDataProvider.Stub(p => p.GetAllMoves(MovesFilePath)).Return(seqList);
            mockDataProvider.Stub(p => p.Extension).Return(fileExtension);
            IDataProvider[] dataProviders = new IDataProvider[1];
            dataProviders[0] = mockDataProvider;
            IoCContainer.Instance().RegisterInstance(fileExtension, mockDataProvider);



            IGameDataManager manager = new GameDataManager(dataProviders);

            //
            // Act.
            //

            IEnumerable<Sequence> result = manager.GetMoves(MovesFilePath);


            //
            // Assert.
            //

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.ToList()[0].Moves.Count, Is.EqualTo(4));
            Assert.That(result.ToList()[0].Moves[3], Is.EqualTo(Move.Turn));

        }
    }
}
