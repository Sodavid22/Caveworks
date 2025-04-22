using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;


namespace Caveworks
{
    public class Sounds // use instances, not originals !!!
    {
        public static MySoundEffect ButtonClick {  get; private set; }
        public static MySoundEffect ButtonDecline { get; private set; }
        public static MySoundEffect ButtonClick2 { get; private set; }
        public static MySoundEffect Ding { get; private set; }


        public static MySoundEffect Pickaxe { get; private set; }
        private static MySoundEffect Thud { get; set; }
        public static MySoundEffect Woosh { get; private set; }

        // single instance
        public static MySoundEffectInstance Burning;
        public static MySoundEffectInstance Machine;
        public static MySoundEffectInstance Machine2;
        public static MySoundEffectInstance Drill;


        public static void Load(ContentManager contentManager)
        {
            ButtonClick = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-hover"), 1.0f);
            ButtonDecline = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-decline"), 1.0f);
            ButtonClick2 = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/ui-button-click"), 1.0f);
            Ding = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/ding"), 1.0f);

            Pickaxe = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/Items/pickaxe"), 0.2f);
            Thud = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/Items/thud"), 0.2f);
            Woosh = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/Items/woosh"), 0.6f);

            // single instance
            Burning = new MySoundEffectInstance(contentManager.Load<SoundEffect>("SoundEffects/Machines/burning"), 0.2f);
            Machine = new MySoundEffectInstance(contentManager.Load<SoundEffect>("SoundEffects/Machines/machine"), 1.0f);
            Machine2 = new MySoundEffectInstance(contentManager.Load<SoundEffect>("SoundEffects/Machines/machine2"), 1.0f);
            Drill = new MySoundEffectInstance(contentManager.Load<SoundEffect>("SoundEffects/Machines/drill"), 0.4f);
        }


        public static float PlaceSoundCooldown = 0;

        public static void PlayPlaceSound()
        {
            if (PlaceSoundCooldown < 0)
            {
                Thud.Play(1);
                PlaceSoundCooldown = 0.02f;
            }
        }
    }
}
