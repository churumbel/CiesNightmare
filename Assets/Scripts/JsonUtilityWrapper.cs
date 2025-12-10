using System.Collections.Generic;
using UnityEngine;

public static class JsonUtilityWrapper
{
    public static List<T> FromJsonList<T>(string json)
    {
        string newJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> array;
    }
}

