using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Caveworks
{
    public static class SaveManager
    {
        private const string SETTINGS_SAVEFILE_PATH = "CaveworksSettings.data";
        private const string WORLD_SAVEFILE_PATH = "CaveworksWorld.data";
        private static SettingsSaveFile SettingsFile {  get; set; }


        private static void Serialize(object data, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(path)) { File.Delete(path); }
            using (FileStream fileStream = File.Create(path))
            {         
                bf.Serialize(fileStream, data);
                fileStream.Close();
            }

        }


        private static object DeSerialize(string path)
        {
            object data = null;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(path))
            {
                using (FileStream fileStream = File.OpenRead(path))
                {
                    data = bf.Deserialize(fileStream);
                    fileStream.Close();
                }
            }
            return data;
        }


        public static void SaveGame()
        {
            SettingsFile = new SettingsSaveFile();
            SettingsFile.GetNewData();
            Serialize(SettingsFile, SETTINGS_SAVEFILE_PATH);
            if (Globals.World != null)
            {
                Serialize(Globals.World, WORLD_SAVEFILE_PATH);
            }
        }


        public static bool LoadSettings()
        {
            try
            {
               if (File.Exists(SETTINGS_SAVEFILE_PATH))
                {
                    SettingsFile = DeSerialize(SETTINGS_SAVEFILE_PATH) as SettingsSaveFile;
                    SettingsFile.LoadData();
                    return true;
                }
               return false;
            }
            catch
            {
                Debug.WriteLine("Loading settings failed!");
                return false;
            }
        }


        public static bool LoadWorldSave()
        {
            try
            {
                if (File.Exists(WORLD_SAVEFILE_PATH))
                {
                    Globals.World = DeSerialize(WORLD_SAVEFILE_PATH) as World;
                    return true;
                }
                return false;
            }
            catch
            {
                Debug.WriteLine("Loading world failed!");
                return false;
            }
        }

        public static bool ExistsWorldSave()
        {
            if (File.Exists(WORLD_SAVEFILE_PATH))
            {
                return true;
            }
            return false;
        }
    }
}
