using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class SettingsSaveFile
    {
        float GlobalVolume {  get; set; }
        bool Fullscreen { get; set; }


        public void GetNewData()
        {
            GlobalVolume = Globals.GetGlobalVolume();
            Fullscreen = GameWindow.IsFullscreen();
        }

        public void LoadData()
        {
            Globals.SetGlobalVolume(GlobalVolume);
            if (GameWindow.IsFullscreen() != Fullscreen) { GameWindow.ToggleFullscreen(); }
        }
    }
}
