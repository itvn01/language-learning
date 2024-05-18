using UnityEngine;
using System.Collections;
using System;

namespace UMExtensions
{
    public class JsonArrayHelper
    {
        public static T FromJson<T>(string json)
        {
            return UnityEngine.JsonUtility.FromJson<T>(json);
        }

        public static string ToJson(object obj)
        {
            return UnityEngine.JsonUtility.ToJson(obj);
        }

        public static T[] ArrayFromJson<T>(string json)
        {
            Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ArrayToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return UnityEngine.JsonUtility.ToJson(wrapper);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}