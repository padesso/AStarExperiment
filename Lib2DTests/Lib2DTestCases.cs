using Lib2D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lib2DTests
{
    [TestClass]
    public class Node2DTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var node = new Node2D(5, 10, true);

            // Assert
            Assert.AreEqual(5, node.X);
            Assert.AreEqual(10, node.Y);
            Assert.IsTrue(node.Walkable);
        }

        [TestMethod]
        public void Equals_SameCoordinates_ReturnsTrue()
        {
            // Arrange
            var node1 = new Node2D(1, 2, true);
            var node2 = new Node2D(1, 2, false);

            // Act & Assert
            Assert.AreEqual(node1, node2);
        }

        [TestMethod]
        public void Equals_DifferentCoordinates_ReturnsFalse()
        {
            // Arrange
            var node1 = new Node2D(1, 2, true);
            var node2 = new Node2D(2, 1, true);

            // Act & Assert
            Assert.AreNotEqual(node1, node2);
        }
    }

    [TestClass]
    public class Grid2DTests
    {
        private Grid2D _grid;

        [TestInitialize]
        public void Setup()
        {
            _grid = new Grid2D(10, 10);
        }

        [TestMethod]
        public void Constructor_CreatesCorrectGridSize()
        {
            // Assert
            Assert.AreEqual(10, _grid.Width);
            Assert.AreEqual(10, _grid.Height);
        }

        [TestMethod]
        public void GetNode_ValidPosition_ReturnsCorrectNode()
        {
            // Act
            var node = _grid.GetNode(5, 5);

            // Assert
            Assert.IsNotNull(node);
            Assert.AreEqual(5, node.X);
            Assert.AreEqual(5, node.Y);
        }

        [TestMethod]
        public void GetNode_InvalidPosition_ReturnsNull()
        {
            // Act
            var node = _grid.GetNode(-1, -1);

            // Assert
            Assert.IsNull(node);
        }

        [TestMethod]
        public void GetNeighbors_CenterNode_ReturnsFourNeighbors()
        {
            // Arrange
            var centerNode = _grid.GetNode(5, 5);

            // Act
            var neighbors = _grid.GetNeighbors(centerNode);

            // Assert
            Assert.AreEqual(4, neighbors.Count);
        }

        [TestMethod]
        public void GetNeighbors_CornerNode_ReturnsTwoNeighbors()
        {
            // Arrange
            var cornerNode = _grid.GetNode(0, 0);

            // Act
            var neighbors = _grid.GetNeighbors(cornerNode);

            // Assert
            Assert.AreEqual(2, neighbors.Count);
        }

        [TestMethod]
        public void SetWalkable_MakesNodeUnwalkable()
        {
            // Arrange
            var node = _grid.GetNode(5, 5);

            // Act
            node.Walkable = false;

            // Assert
            Assert.IsFalse(node.Walkable);
        }
    }
}
