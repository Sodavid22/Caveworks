using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Caveworks
{
    public class Button
    {
        private Texture2D texture;

        Vector2 position;
        Vector2 size;
        string text;

        public Button(Vector2 position, Vector2 size, string text)
        {
            this.position = position;
            this.size = size;
            this.text = text;
        }

        public void Update()
        {
            return;
        }

        public void Draw()
        {
            return;
        }
    }
}
