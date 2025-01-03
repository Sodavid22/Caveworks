using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class SettingsScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.MenuButtonFont);

        readonly static TextBox fullscreenText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "Fullscreen:", Fonts.MenuButtonFont);
        readonly static Button fullscreenButton = new Button(new Vector2(350, 60), Globals.UIButtonColor, 2, GameWindow.IsFullscreen.ToString(), Fonts.MenuButtonFont);

        readonly static TextBox volumeText = new TextBox(new Vector2(350, 60), Globals.UIButtonColor, 2, "Volume:", Fonts.MenuButtonFont);
        readonly static Slider volumeSlider = new Slider(new Vector2(350, 20), Globals.UIButtonColor, 2, Globals.GlobalVolume);

        readonly static UiElement[] uiElements = { backgroundBox, fullscreenText, fullscreenButton, volumeText, volumeSlider};


        public SettingsScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2), Anchor.Middle);

            fullscreenText.Place(new Vector2(GameWindow.WindowSize.X / 2 - 200, GameWindow.WindowSize.Y / 2 - 140), Anchor.Middle);
            fullscreenButton.Place(new Vector2(GameWindow.WindowSize.X / 2 + 200, GameWindow.WindowSize.Y / 2 - 140), Anchor.Middle);

            volumeText.Place(new Vector2(GameWindow.WindowSize.X / 2 - 200, GameWindow.WindowSize.Y / 2 - 70), Anchor.Middle);
            volumeSlider.Place(new Vector2(GameWindow.WindowSize.X / 2 + 200, GameWindow.WindowSize.Y / 2 - 70), Anchor.Middle);
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
                fullscreenButton.ChangeText(GameWindow.IsFullscreen.ToString());
                Globals.ActiveScene = new SettingsScene();
            }

            Globals.GlobalVolume = volumeSlider.GetValue();
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
