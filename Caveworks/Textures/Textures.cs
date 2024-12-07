using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public static class Textures
    {
        // empty texture
        public static Texture2D emptyTexture;

        // game background
        public static Texture2D menuBackground;

        public static void Load(ContentManager contentManager)
        {
            Textures.menuBackground = contentManager.Load<Texture2D>("factorio_background");
        }
    }
}
