using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;


namespace Caveworks
{
    public class Sounds // use instances, not originals !!!
    {
        public static MySoundEffect ButtonClick {  get; private set; }
        public static MySoundEffect ButtonDecline { get; private set; }
        public static MySoundEffect ButtonClick2 { get; private set; }


        public static void Load(ContentManager contentManager)
        {
            ButtonClick = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-hover"), 1.0f);
            ButtonDecline = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-decline"), 1.0f);
            ButtonClick2 = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/ui-button-click"), 1.0f);
        }
    }
}
