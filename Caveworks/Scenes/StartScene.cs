using Microsoft.Xna.Framework;


namespace Caveworks
{
    internal class StartScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.LargeFont);

        readonly static TextBox worldSizeText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "World size:", Fonts.LargeFont);
        readonly static Slider worldSizeSlider = new Slider(new Vector2(350, 20), Globals.UIButtonColor, 2, 8, 16, 16, 0);

        readonly static TextBox researchMultText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "Research cost:", Fonts.LargeFont);
        readonly static Slider researchMultSlider = new Slider(new Vector2(350, 20), Globals.UIButtonColor, 2, 0.1f, 1, 2, 1);

        readonly static Button startButton = new Button(new Vector2(200, 60), Globals.UIButtonColor, 2, "Start", Fonts.LargeFont);

        readonly static UiElement[] uiElements = { backgroundBox, worldSizeText, worldSizeSlider, researchMultText, researchMultSlider, startButton };


        public StartScene()
        { 
            backgroundBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2), Anchor.Middle);

            worldSizeText.Place(new Vector2(GameWindow.Size.X / 2 - 200, GameWindow.Size.Y / 2 - 140), Anchor.Middle);
            worldSizeSlider.Place(new Vector2(GameWindow.Size.X / 2 + 200, GameWindow.Size.Y / 2 - 140), Anchor.Middle);

            researchMultText.Place(new Vector2(GameWindow.Size.X / 2 - 200, GameWindow.Size.Y / 2 - 0), Anchor.Middle);
            researchMultSlider.Place(new Vector2(GameWindow.Size.X / 2 + 200, GameWindow.Size.Y / 2 - 0), Anchor.Middle);

            startButton.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 + 140), Anchor.Middle);
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (startButton.IsPressed(MouseKey.Left))
            {
                Globals.World = new World((int)worldSizeSlider.GetValue(), researchMultSlider.GetValue());
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
