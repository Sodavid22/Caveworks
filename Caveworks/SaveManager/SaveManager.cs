using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Caveworks
{
    public static class SaveManager
    {
        private const string SETTINGS_SAVEFILE_PATH = "CaveworksSettings.data";
        private static SettingsSaveFile SettingsFile {  get; set; }


        private static void Serialize(object data, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(path)) { File.Delete(path); }
            FileStream fileStream = File.Create(path);
            bf.Serialize(fileStream, data);
            fileStream.Close();
        }


        private static object DeSerialize(string path)
        {
            object data = null;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(path))
            {
                FileStream fileStream = File.OpenRead(path);
                data = bf.Deserialize(fileStream);
                fileStream.Close();
            }
            return data;
        }


        public static void SaveGame()
        {
            SettingsFile = new SettingsSaveFile();
            SettingsFile.GetNewData();
            Serialize(SettingsFile, SETTINGS_SAVEFILE_PATH);
        }


        public static void LoadGame()
        {
            try
            {
               if (File.Exists(SETTINGS_SAVEFILE_PATH))
                {
                    SettingsFile = DeSerialize(SETTINGS_SAVEFILE_PATH) as SettingsSaveFile;
                    SettingsFile.LoadData();
                }
            }
            catch
            {
                Debug.WriteLine("Loading data failed!");
                return;
            }
        }
    }
}
