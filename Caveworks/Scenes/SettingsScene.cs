using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class SettingsScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.LargeFont);

        readonly static TextBox fullscreenText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "Fullscreen:", Fonts.LargeFont);
        readonly static Button fullscreenButton = new Button(new Vector2(350, 60), Globals.UIButtonColor, 2, GameWindow.IsFullscreen.ToString(), Fonts.LargeFont);

        readonly static TextBox volumeText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "Volume:", Fonts.LargeFont);
        readonly static Slider volumeSlider = new Slider(new Vector2(350, 20), Globals.UIButtonColor, 2, 0, Globals.GlobalVolume * 100, 100, 0);

        readonly static TextBox lightText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "Light Distance:", Fonts.LargeFont);
        readonly static Slider lightSlider = new Slider(new Vector2(350, 20), Globals.UIButtonColor, 2, 16, Globals.LightDistance, 24, 0);

        readonly static UiElement[] uiElements = { backgroundBox, fullscreenText, fullscreenButton, volumeText, volumeSlider, lightText, lightSlider};


        public SettingsScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2), Anchor.Middle);

            fullscreenText.Place(new Vector2(GameWindow.Size.X / 2 - 200, GameWindow.Size.Y / 2 - 140), Anchor.Middle);
            fullscreenButton.Place(new Vector2(GameWindow.Size.X / 2 + 200, GameWindow.Size.Y / 2 - 140), Anchor.Middle);

            volumeText.Place(new Vector2(GameWindow.Size.X / 2 - 200, GameWindow.Size.Y / 2 - 70), Anchor.Middle);
            volumeSlider.Place(new Vector2(GameWindow.Size.X / 2 + 200, GameWindow.Size.Y / 2 - 70), Anchor.Middle);

            lightText.Place(new Vector2(GameWindow.Size.X / 2 - 200, GameWindow.Size.Y / 2 - 0), Anchor.Middle);
            lightSlider.Place(new Vector2(GameWindow.Size.X / 2 + 200, GameWindow.Size.Y / 2 - 0), Anchor.Middle);


            if (GameWindow.IsFullscreen)
            {
                fullscreenButton.SetText("Enabled");
            }
            else
            {
                fullscreenButton.SetText("Disabled");
            }
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (fullscreenButton.IsPressed(MouseKey.Left))
            {
                GameWindow.ToggleFullscreen();
                if (GameWindow.IsFullscreen)
                {
                    fullscreenButton.SetText("Enabled");
                }
                else
                {
                    fullscreenButton.SetText("Disabled");
                }
                Globals.ActiveScene = new SettingsScene();
            }

            Globals.GlobalVolume = volumeSlider.GetValue() / 100;
            Globals.LightDistance = (int)lightSlider.GetValue();
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
