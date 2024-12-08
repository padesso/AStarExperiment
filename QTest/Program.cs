using Lib2D;

Grid2D grid2D = new Grid2D(10, 10);

grid2D.SetNode(1, 0, new Node2D(0, 0, false));
grid2D.SetNode(1, 1, new Node2D(0, 0, false));
grid2D.SetNode(1, 2, new Node2D(0, 0, false));
grid2D.SetNode(1, 3, new Node2D(0, 0, false)); 
grid2D.SetNode(1, 4, new Node2D(0, 0, false));
grid2D.SetNode(1, 5, new Node2D(0, 0, false));
grid2D.SetNode(1, 6, new Node2D(0, 0, false));
grid2D.SetNode(1, 7, new Node2D(0, 0, false));
grid2D.SetNode(1, 8, new Node2D(0, 0, false));

grid2D.SetNode(3, 1, new Node2D(0, 0, false));
grid2D.SetNode(3, 2, new Node2D(0, 0, false));
grid2D.SetNode(3, 3, new Node2D(0, 0, false));
grid2D.SetNode(3, 4, new Node2D(0, 0, false));
grid2D.SetNode(3, 5, new Node2D(0, 0, false));
grid2D.SetNode(3, 6, new Node2D(0, 0, false));
grid2D.SetNode(3, 7, new Node2D(0, 0, false));
grid2D.SetNode(3, 8, new Node2D(0, 0, false));
grid2D.SetNode(3, 9, new Node2D(0, 0, false));

Node2D start = grid2D.GetNode(0, 0);
start.IsStart = true;
Node2D goal = grid2D.GetNode(9, 9);
goal.IsGoal = true;

List<Node2D> path = grid2D.SolveAStar(start, goal);

grid2D.Print();

Console.ReadLine();