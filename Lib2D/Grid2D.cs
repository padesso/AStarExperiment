using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib2D
{
    public class Grid2D
    {
        public int Width;
        public int Height;
        public Node2D[] _nodes;
        public PriorityQueue<Node2D, float> _openSet;
        public HashSet<Node2D> _closedSet;

        public Grid2D(int width, int height)
        {
            Width = width;
            Height = height;

            _nodes = new Node2D[width * height];
            _openSet = new PriorityQueue<Node2D, float>();
            _closedSet = new HashSet<Node2D>();

            Reset();
        }

        public Node2D GetNode(int x, int y)
        {
            //return null if out of bounds
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return null;
            }

            return _nodes[x + y * Width];
        }

        public void SetNode(int x, int y, Node2D node)
        {
            _nodes[x + y * Width] = node;
        }

        public List<Node2D> GetNeighbors(Node2D node)
        {
            List<Node2D> neighbors = new List<Node2D>();

            int x = node.X;
            int y = node.Y;

            if (x > 0)
            {
                neighbors.Add(GetNode(x - 1, y));
            }

            if (x < Width - 1)
            {
                neighbors.Add(GetNode(x + 1, y));
            }

            if (y > 0)
            {
                neighbors.Add(GetNode(x, y - 1));
            }

            if (y < Height - 1)
            {
                neighbors.Add(GetNode(x, y + 1));
            }

            return neighbors;
        }

        //Solve from start to goal and return the path as a list of nodes
        public List<Node2D> SolveAStar(Node2D startNode, Node2D goalNode)
        {
            _openSet.Clear();
            _closedSet.Clear();

            _openSet.Enqueue(startNode, 0);

            while (_openSet.Count > 0)
            {
                Node2D currentNode = _openSet.Dequeue();

                if (currentNode.Equals(goalNode))
                {
                    return ReconstructPath(startNode, goalNode);
                }

                _closedSet.Add(currentNode);

                List<Node2D> neighbors = GetNeighbors(currentNode);

                foreach (Node2D neighbor in neighbors)
                {
                    if (!_closedSet.Contains(neighbor) && neighbor.Walkable)
                    {
                        float tentativeGScore = currentNode.G + 1;

                        if (OpenSetContains(neighbor) && tentativeGScore >= neighbor.G)
                        {
                            continue;
                        }

                        neighbor.Parent = currentNode;
                        neighbor.G = tentativeGScore;
                        neighbor.H = Heuristic(neighbor, goalNode);
                        neighbor.F = neighbor.G + neighbor.H;

                        if (!OpenSetContains(neighbor))
                        {
                            _openSet.Enqueue(neighbor, neighbor.F);
                        }
                    }
                }
            }

            return null;

        }

        private float Heuristic(Node2D neighbor, Node2D goalNode)
        {
            //Euclidean distance
            return (float)Math.Sqrt(Math.Pow(neighbor.X - goalNode.X, 2) + Math.Pow(neighbor.Y - goalNode.Y, 2));
        }

        private bool OpenSetContains(Node2D node)
        {
            if (_openSet.UnorderedItems.Any(pair => pair.Element.Equals(node)))
            {
                return true;
            }

            return false;
        }

        private List<Node2D> ReconstructPath(Node2D startNode, Node2D goalNode)
        {
            List<Node2D> path = new List<Node2D>();

            Node2D currentNode = goalNode;

            while (!currentNode.Equals(startNode))
            {
                path.Add(currentNode);
                currentNode.IsPath = true;
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            return path;
        }

        public void Print()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Node2D node = GetNode(x, y);

                    if (node.Walkable && !node.IsPath)
                    {
                        Console.Write(". ");
                    }
                    else if (node.Walkable && node.IsPath)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("# ");
                    }
                }

                Console.WriteLine();
            }
        }

        public void Reset()
        {
            //Populate the grid with walkable nodes
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _nodes[x + y * Width] = new Node2D(x, y, true);
                }
            }
        }
    }
}
