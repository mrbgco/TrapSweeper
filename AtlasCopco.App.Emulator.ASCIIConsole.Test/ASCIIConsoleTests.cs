using AtlasCopco.App.Emulator.Model;
using AtlasCopco.Integration.Maze;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AtlasCopco.App.Emulator.ASCIIConsole.Test
{
    [TestClass]
    public class ASCIIConsoleTests
    {
        [TestMethod]
        public void EmulateMaze_RoomHasTreasure_ReturnsTreasureFound()
        {
            var hunter = new Hunter
            {
                HealthPoint = 2, 
                StepsCount = 0,
                Name = "X" 
            };

            var mock = new Mock<IMazeIntegration>();
            mock.Setup(foo => foo.BuildMaze(It.IsAny<int>())).Verifiable();
            mock.Setup(foo => foo.GetEntranceRoom()).Returns(1);
            mock.Setup(foo => foo.HasTreasure(It.IsAny<int>())).Returns(true);

            var testEmulator = new Console.ASCIIConsole(mock.Object);
            testEmulator.NewGame(true);
            var result = testEmulator.StartNavigation(hunter);

            mock.Verify(foo => foo.BuildMaze(It.IsAny<int>()));
            Assert.AreEqual(MazeResult.TreasureFound, result);
        }

        [TestMethod]
        public void EmulateMaze_RoomHasTrap_ReturnsHunterDied()
        {
            var hunter = new Hunter
            {
                HealthPoint = 1, 
                StepsCount = 0,
                Name = "X" 
            };

            var mock = new Mock<IMazeIntegration>();
            mock.Setup(foo => foo.BuildMaze(It.IsAny<int>())).Verifiable();
            mock.Setup(foo => foo.GetEntranceRoom()).Returns(1);
            mock.Setup(foo => foo.HasTreasure(It.IsAny<int>())).Returns(false);
            mock.Setup(foo => foo.CausesInjury(It.IsAny<int>())).Returns(true);

            var testEmulator = new Console.ASCIIConsole(mock.Object);
            testEmulator.NewGame(true);
            var result = testEmulator.StartNavigation(hunter);

            mock.Verify(foo => foo.BuildMaze(It.IsAny<int>()));
            Assert.AreEqual(MazeResult.HunterDied, result);
        }
    }
}
