using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class CreditsScene : Scene
    {
        private static TextBox madeByTextBox = new TextBox(new Vector2(600, 60), new Vector4(0.5f, 0.5f, 0.5f, 1), 2, "Made by: David Sobek", Fonts.menuButtonFont);

        private static TextBox[] textBoxes = { madeByTextBox };

        public CreditsScene()
        {
            madeByTextBox.Load(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 - 140), Anchor.Middle);
        }

        public void Update(GameTime gameTime)
        {
            return;
        }

        public void Draw(GameTime gameTime)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Draw();
            }
        }
    }
}
