using System.IO;
using UnityEngine;

namespace RoadRage.Tools.GameHelpers
{
    public class JsonSerialization<T>
    {
        private string _path;

        public JsonSerialization(string fileName)
        {
            _path = Path.Combine(Application.dataPath, fileName);
        }

        public (bool, T) DeSerialization()
        {
            T data = default;

            if (File.Exists(_path))
            {
                data = JsonUtility.FromJson<T>(File.ReadAllText(_path));
                return (true, data);
            }

            return (false, data);
        }

        public void Serialization(T data)
        {
            File.WriteAllText(_path, JsonUtility.ToJson(data));
        }
    }
}