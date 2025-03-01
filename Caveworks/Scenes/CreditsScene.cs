﻿using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class CreditsScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.MenuButtonFont);

        readonly static TextBox madeByTextBox = new TextBox(new Vector2(600, 60), Globals.UITextBoxColor, 2, "Made by: David Sobek", Fonts.MenuButtonFont);

        // TESTCODE
        readonly static Button imageButton = new Button(new Vector2(64, 64), Globals.UIButtonColor, 2, Textures.Player);

        readonly static UiElement[] uiElements = { backgroundBox, madeByTextBox, imageButton };


        public CreditsScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2), Anchor.Middle);

            madeByTextBox.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2 - 140), Anchor.Middle);

            imageButton.Place(new Vector2(10, 10), Anchor.TopLeft);
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (imageButton.IsPressed(MouseKey.Left))
            {
                Sounds.ButtonClick.play(1);
            }

            if (imageButton.IsPressed(MouseKey.Right))
            {
                Sounds.ButtonClick2.play(1);
            }
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
