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
            this.GlobalVolume = Globals.GlobalVolume;
            this.Fullscreen = GameWindow.IsFullscreen;
        }

        public void LoadData()
        {
            Globals.GlobalVolume = this.GlobalVolume;
            if (GameWindow.IsFullscreen != Fullscreen) { GameWindow.ToggleFullscreen(); }
        }
    }
}
