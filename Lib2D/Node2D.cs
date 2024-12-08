namespace Lib2D
{
    public class Node2D
    {
        public int X;
        public int Y;
        public bool Walkable = true;
        public bool IsPath = false;
        public bool IsStart = false;
        public bool IsGoal = false;
        public Node2D Parent;
        public float G;
        public float H;
        public float F;

        public Node2D(int x, int y, bool walkable)
        {
            X = x;
            Y = y;
            Walkable = walkable;
        }

        public override string ToString()
        {
            return $"Node2D: ({X}, {Y})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Node2D other = (Node2D)obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}