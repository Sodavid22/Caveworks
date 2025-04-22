using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;


namespace Caveworks
{
    public class EndScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.LargeFont);

        readonly static TextBox madeByTextBox = new TextBox(new Vector2(600, 60), Globals.UITextBoxColor, 2, "You Won!", Fonts.LargeFont);

        readonly static TextBox thanksTextBox = new TextBox(new Vector2(600, 60), Globals.UITextBoxColor, 2, "Thank you for playing!", Fonts.LargeFont);

        readonly static UiElement[] uiElements = { backgroundBox, madeByTextBox, thanksTextBox };

        static float[] color = new float[3] {0, 0.66f, 0.66f};
        static float[] colorDirection = new float[3] {1,1,-1};


        public EndScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2), Anchor.Middle);

            madeByTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 - 70), Anchor.Middle);

            thanksTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 - 0), Anchor.Middle);
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            for (int i = 0; i < 3; i++)
            {
                color[i] += colorDirection[i] * ((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000) / 4;
                if (color[i] >= 1)
                {
                    colorDirection[i] = -1;
                    color[i] = 1;
                }
                else if (color[i] <= 0)
                {
                    colorDirection[i] = 1;
                    color[i] = 0;
                }
            }

            Debug.WriteLine(color[0] + " " + color[1] + " " + color[2]);
            backgroundBox.ChangeColor(Color.FromNonPremultiplied(new Vector4(color[0], color[1], color[2], 1)));
        }


        public void Draw(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Draw();
            }
        }
    }
}
