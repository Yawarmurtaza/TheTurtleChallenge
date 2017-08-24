using System.Collections.Generic;
using NUnit.Framework;
using TurtleChallenge.DomainModel;
using Rhino.Mocks;
using System.Linq;

namespace TurtleChallenge.DataAccess.Tests
{
    [TestFixture]
    public class XmlDataProviderTests
    {

        private IFileReadonlyAccess mockFileAccess;

        private IDataProvider provider;


        [SetUp]
        public void Setup()
        {
            this.mockFileAccess = MockRepository.GenerateMock<IFileReadonlyAccess>();
            this.provider = new XmlDataProvider(this.mockFileAccess);
        }

        [TearDown]
        public void TearDown()
        {
            this.mockFileAccess = null;
            this.provider = null;
        }

        [Test]
        public void GetGameSettings_Should_Successfully_Return_The_GameSettings_From_The_XML_File()
        {

            //
            // Arrange.
            //

            const string GameSettingsXmlContents = "<gameSettings>	<boardSize>		<columns>4</columns>		<rows>4</rows>	</boardSize>		<startingPosition>		<x>0</x>			<y>0</y>	</startingPosition>	<direction>South</direction>	<exitPoint>		<x>3</x>		<y>3</y>	</exitPoint>		<mine>		<x>1</x>		<y>1</y>	</mine>	<mine>		<x>2</x>		<y>1</y>	</mine>	<mine>		<x>1</x>		<y>2</y>	</mine>		<mine>		<x>2</x>		<y>2</y>	</mine>	</gameSettings>";
            const string FilePath = "game-settings.xml";

            this.mockFileAccess.Stub(fs => fs.ReadAllText(FilePath)).Return(GameSettingsXmlContents);

            //
            // Act.
            //

            GameSettings game = provider.GetGameSettings(FilePath);


            //
            // Assert.
            //


            Assert.That(game.Direction, Is.EqualTo(Direction.South));
            Assert.That(game.ExitPoint.Y, Is.EqualTo(3));
            Assert.That(game.ExitPoint.X, Is.EqualTo(3));
            Assert.That(game.Mines.Count, Is.EqualTo(4));
            Assert.That(game.GameBoard.Columns, Is.EqualTo(4));
            Assert.That(game.GameBoard.Rows, Is.EqualTo(4));


        }

        [Test]
        public void GetAllMoves_Should_Return_SequenceMoves_From_File()
        {
            //
            // Arrange.
            //
            const string MovesXmlContents = "<sequences>	<sequence  name=\"sequence 1 - success\"> 		<action>rotate</action>		<action>rotate</action>		<action>rotate</action>		<action>move</action>	<action>move</action>				<action>move</action>			<action>rotate</action>		<action>rotate</action>		<action>rotate</action>			<action>move</action>		<action>move</action>				<action>move</action>				</sequence>	<sequence name=\"sequence 2 - 1,2 mine hit\"> 	<action>rotate</action> 		<action>rotate</action> 		<action>move</action>		<action>move</action>		<action>move</action>		<action>rotate</action>			<action>move</action>			<action>rotate</action>		<action>move</action>				</sequence>	<sequence name=\"sequence 3 - living in the danger\">	<action>rotate</action> 		<action>rotate</action> 		<action>move</action> 		<action>move</action>		<action>move</action>		<action>rotate</action> 	<action>move</action>		<action>move</action>			</sequence></sequences>";
            const string MovesXmlFilePath = "moves.xml";

            this.mockFileAccess.Stub(fa => fa.ReadAllText(MovesXmlFilePath)).Return(MovesXmlContents);
            
            //
            // Act.
            //
            IEnumerable<Sequence> allSequences = this.provider.GetAllMoves(MovesXmlFilePath);

            //
            // Assert.
            //


            Assert.That(allSequences.Count, Is.EqualTo(3));

            List<Sequence> seqList = allSequences.ToList();

            Assert.That(seqList[0].Moves.Count, Is.EqualTo(12));
            Assert.That(seqList[0].Name, Is.EqualTo("sequence 1 - success"));
        }

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetGameSettings_should_throw_InvalidOperationException_when_a_negative_coordinates_are_found()
        {
            //
            // Arrange.
            //

            const string GameSettingsXmlContents = "<gameSettings>	<boardSize>		<columns>4</columns>		<rows>4</rows>	</boardSize>		<startingPosition>		<x>-2</x>			<y>-2</y>	</startingPosition>	<direction>South</direction>	<exitPoint>		<x>3</x>		<y>3</y>	</exitPoint>		<mine>		<x>1</x>		<y>1</y>	</mine>	<mine>		<x>2</x>		<y>1</y>	</mine>	<mine>		<x>1</x>		<y>2</y>	</mine>		<mine>		<x>2</x>		<y>2</y>	</mine>	</gameSettings>";
            const string FilePath = "game-settings.xml";

            this.mockFileAccess.Stub(fs => fs.ReadAllText(FilePath)).Return(GameSettingsXmlContents);

            //
            // Act.
            //

            GameSettings game = provider.GetGameSettings(FilePath);
        }

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetAllMoves_Should_throw_InvalidOperationException_when_invalid_move_is_found()
        {
            //
            // Arrange.
            //
            const string MovesXmlContents = "<sequences>	<sequence  name=\"sequence 1 - success\"> 		<action>spin</action>		<action>rotate</action>		<action>rotate</action>		<action>move</action>	<action>move</action>				<action>move</action>			<action>rotate</action>		<action>rotate</action>		<action>rotate</action>			<action>move</action>		<action>move</action>				<action>move</action>				</sequence>	<sequence name=\"sequence 2 - 1,2 mine hit\"> 	<action>rotate</action> 		<action>rotate</action> 		<action>move</action>		<action>move</action>		<action>move</action>		<action>rotate</action>			<action>move</action>			<action>rotate</action>		<action>move</action>				</sequence>	<sequence name=\"sequence 3 - living in the danger\">	<action>rotate</action> 		<action>rotate</action> 		<action>move</action> 		<action>move</action>		<action>move</action>		<action>rotate</action> 	<action>move</action>		<action>move</action>			</sequence></sequences>";
            const string MovesXmlFilePath = "moves.xml";

            this.mockFileAccess.Stub(fa => fa.ReadAllText(MovesXmlFilePath)).Return(MovesXmlContents);

            //
            // Act.
            //
            IEnumerable<Sequence> allSequences = this.provider.GetAllMoves(MovesXmlFilePath);
           
        }
    }
}