using System;

namespace Caveworks
{
    [Serializable]
    public class MyVector2
    {
        public float X;
        public float Y;

        public MyVector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }


        public override string ToString()
        {
            return "[ " + X + ", " + Y + " ]";
        }
    }
}
