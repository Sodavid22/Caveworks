using System;

namespace Caveworks
{
    [Serializable]
    public class MyVector2Int
    {
        public int X;
        public int Y;

        public MyVector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }


        public override string ToString()
        {
            return "[ " + X + ", " + Y + " ]";
        }

        public MyVector2 ToMyVector2()
        {
            return new MyVector2(this.X, this.Y);
        }
    }
}