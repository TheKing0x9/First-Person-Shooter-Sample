using UnityEngine;
using System.IO;

namespace FPS.Data
{
    public class JsonSaver
    {
        #if UNITY_EDITOR
        private static readonly string s_Filename = "fps_editor.sav";
        #else
        private static readonly string s_Filename = "fps.sav";
        #endif

        public static string GetSaveFilename()
        {
            return string.Format("{0}/{1}", Application.persistentDataPath, s_Filename);
        }

        protected virtual StreamWriter GetWriteStream()
        {
            return new StreamWriter(new FileStream(GetSaveFilename(), FileMode.Create));
        }

        protected virtual StreamReader GetReadStream()
        {
            return new StreamReader(new FileStream(GetSaveFilename(), FileMode.Open));
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);

            using (StreamWriter writer = GetWriteStream())
            {
                writer.Write(json);
            }
        }

        public bool Load(SaveData data)
        {
            string loadFilename = GetSaveFilename();

            if (File.Exists(loadFilename))
            {
                using (StreamReader reader = GetReadStream())
                {
                    JsonUtility.FromJsonOverwrite(reader.ReadToEnd(), data);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the save file
        /// </summary>
        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }
    }
}