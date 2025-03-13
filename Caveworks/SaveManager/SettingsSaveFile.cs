using System;


namespace Caveworks
{
    [Serializable]
    public class SettingsSaveFile
    {
        float GlobalVolume {  get; set; }
        bool Fullscreen { get; set; }
        int LigtDistance { get; set; }


        public void GetNewData()
        {
            this.GlobalVolume = Globals.GlobalVolume;
            this.Fullscreen = GameWindow.IsFullscreen;
            this.LigtDistance = Globals.LightDistance;
        }

        public void LoadData()
        {
            Globals.GlobalVolume = this.GlobalVolume;
            if (GameWindow.IsFullscreen != Fullscreen) { GameWindow.ToggleFullscreen(); }
            Globals.LightDistance = this.LigtDistance;
        }
    }
}
