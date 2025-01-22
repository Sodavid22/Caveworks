using Microsoft.Xna.Framework;


namespace Caveworks
{
    internal class StartScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.MenuButtonFont);

        readonly static TextBox worldSizeText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "World size:", Fonts.MenuButtonFont);
        readonly static Slider worldSizeSlider = new Slider(new Vector2(350, 20), Globals.UIButtonColor, 2, 4, 16, 32);

        readonly static Button startButton = new Button(new Vector2(200, 60), Globals.UIButtonColor, 2, "Start", Fonts.MenuButtonFont);

        readonly static UiElement[] uiElements = { backgroundBox, worldSizeText, worldSizeSlider, startButton };


        public StartScene()
        { 
            backgroundBox.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2), Anchor.Middle);

            worldSizeText.Place(new Vector2(GameWindow.WindowSize.X / 2 - 200, GameWindow.WindowSize.Y / 2 - 140), Anchor.Middle);
            worldSizeSlider.Place(new Vector2(GameWindow.WindowSize.X / 2 + 200, GameWindow.WindowSize.Y / 2 - 140), Anchor.Middle);

            startButton.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2 + 140), Anchor.Middle);
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (startButton.IsPressed(MouseKey.Left))
            {
                Globals.World = new World((int)worldSizeSlider.GetValue());
                Globals.ActiveScene = new WorldScene();
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
