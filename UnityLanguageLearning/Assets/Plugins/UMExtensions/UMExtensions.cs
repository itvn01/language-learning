using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace UMExtensions
{
    public static class UMExtensions
    {
        //==> LIST
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static DateTime[] GetDatesArray(this DateTime fromDate, DateTime toDate)
        {
            int days = (toDate - fromDate).Days;
            var dates = new DateTime[days];

            for (int i = 0; i < days; i++)
            {
                dates[i] = fromDate.AddDays(i);
            }

            return dates;
        }

        public static T GetRandomItem<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static void ShuffleItems<T>(this IList<T> list)
        {
            for (var i = list.Count - 1; i > 1; i--)
            {
                var j = UnityEngine.Random.Range(0, i + 1);
                var value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        public static T Shift<T>(this List<T> t)
        {
            T element = t[0];
            t.RemoveAt(0);
            return element;
        }

        public static void UnShift<T>(this List<T> t, T element)
        {
            t.Insert(0, element);
        }

        public static T Pop<T>(this List<T> t)
        {
            T element = t[t.Count - 1];
            t.RemoveAt(t.Count - 1);
            return element;
        }

        public static void Push<T>(this List<T> t, T element)
        {
            t.Add(element);
        }

        public static bool WithinIndex<T>(this List<T> t, int index)
        {
            return index >= 0 && index < t.Count;
        }

        public static bool HasItem<T>(this List<T> t)
        {
            return t.Count > 0;
        }

        //==> RIGIDBODY

        public static void ChangeDirection(this Rigidbody rb, Vector3 direction)
        {
            rb.velocity = direction.normalized * rb.velocity.magnitude;
        }

        public static void NormalizeVelocity(this Rigidbody rb, float magnitude = 1)
        {
            rb.velocity = rb.velocity.normalized * magnitude;
        }


        //==> VECTOR

        public static Vector3 CloneToVector3(this Vector3 vec3)
        {
            return new Vector3(vec3.x, vec3.y, vec3.z);
        }

        public static Vector3 CloneToVector3(this Vector2 vec2)
        {
            return new Vector3(vec2.x, vec2.y, 0);
        }

        public static Vector2 CloneToVector2(this Vector3 vec3)
        {
            return new Vector2(vec3.x, vec3.y);
        }

        // public static string ToJson<T>(this T instance)
        //     => JsonConvert.SerializeObject(instance, JsonSettings.SerializerDefaults);
    }

    public static class IntExtensions
    {
        public static bool IsGreaterThan(this int i, int value)
        {
            return i > value;
        }

        public static string FormatMoney(this long i)
        {
            return i.ToString("N0");
        }

        public static string FormatMoney(this int i)
        {
            return i.ToString("N0");
        }

        public static string FormatMoney(this double i)
        {
            return i.ToString("N0");
        }

        public static string FormatMoney(this float i)
        {
            return i.ToString("N0");
        }

        public static string FormatNumber(this int i)
        {
            return i.ToString("N0");
        }

        static string FormatMoneyKMB(this int i)
        {
            if (i >= 100000000)
            {
                return (i / 1000000D).ToString("0.#M");
            }
            if (i >= 1000000)
            {
                return (i / 1000000D).ToString("0.##M");
            }
            if (i >= 100000)
            {
                return (i / 1000D).ToString("0.#k");
            }
            if (i >= 10000)
            {
                return (i / 1000D).ToString("0.##k");
            }

            return i.ToString("#,0");
        }

        static string FormatMoneyKMB(this double i)
        {
            if (i >= 100000000)
            {
                return (i / 1000000D).ToString("0.#M");
            }
            if (i >= 1000000)
            {
                return (i / 1000000D).ToString("0.##M");
            }
            if (i >= 100000)
            {
                return (i / 1000D).ToString("0.#k");
            }
            if (i >= 10000)
            {
                return (i / 1000D).ToString("0.##k");
            }

            return i.ToString("#,0");
        }
    }

    public static class ImageExtensions
    {
        public static void SetAlpha(this Image image, float value)
        {
            var color = image.color;
            color.a = value;
            image.color = color;
        }
    }

    public static class TransformExtensions
    {
        public static void SetPositionX(this Transform transfrom, float value)
        {
            var position = transfrom.position;
            position.x = value;
            transfrom.position = position;
        }

        public static void SetPositionY(this Transform transfrom, float value)
        {
            var position = transfrom.position;
            position.y = value;
            transfrom.position = position;
        }

        public static void SetPositionZ(this Transform transfrom, float value)
        {
            var position = transfrom.position;
            position.z = value;
            transfrom.position = position;
        }

        public static void SetLocalPositionX(this Transform transfrom, float value)
        {
            var position = transfrom.localPosition;
            position.x = value;
            transfrom.localPosition = position;
        }

        public static void SetLocalPositionY(this Transform transfrom, float value)
        {
            var position = transfrom.localPosition;
            position.y = value;
            transfrom.localPosition = position;
        }

        public static void SetLocalPositionZ(this Transform transfrom, float value)
        {
            var position = transfrom.localPosition;
            position.z = value;
            transfrom.localPosition = position;
        }

        public static void SetLocalScaleX(this Transform transfrom, float value)
        {
            var scale = transfrom.localScale;
            scale.x = value;
            transfrom.localScale = scale;
        }

        public static void SetLocalScaleY(this Transform transfrom, float value)
        {
            var scale = transfrom.localScale;
            scale.y = value;
            transfrom.localScale = scale;
        }

        public static void SetLocalScaleZ(this Transform transfrom, float value)
        {
            var scale = transfrom.localScale;
            scale.z = value;
            transfrom.localScale = scale;
        }

        public static void ScaleByValue(this Transform transfrom, float value)
        {
            var scale = transfrom.localScale;
            transfrom.localScale = new Vector3(scale.x * value, scale.y * value, scale.z * value);
        }

        public static void SetScaleByValue(this Transform transfrom, float value)
        {
            transfrom.localScale = new Vector3(value, value, value);
        }

        public static void ResetTransformation(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void ResetLocalTransformation(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }

    public static class GameObjectExtensions
    {
        public static void ForceRebuildLayoutImmediate(this GameObject gameObject)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.GetComponent<RectTransform>());
        }

        public static void SetObjectAlpha(this GameObject gameObject, float alpha)
        {
            var canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
            canvasGroup.alpha = alpha;
        }

        public static void DestroyAllChild(this GameObject gameObject)
        {
            foreach (Transform child in gameObject.transform)
            {
                UnityEngine.Object.Destroy(child.gameObject, 0);
            }
        }

        public static void DestroyAllChild(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                UnityEngine.Object.Destroy(child.gameObject, 0);
            }
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }

        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }

        public static void SetParent(this GameObject gameObject, Transform parent)
        {
            gameObject.SetParent(parent, Vector3.zero, Vector3.one);
        }

        public static void SetParent(this GameObject gameObject, Transform parent, Vector3 position)
        {
            gameObject.SetParent(parent, position, Vector3.one);
        }

        public static void SetParent(this GameObject gameObject, Transform parent, Vector3 position, Vector3 scale)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = position;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = scale;
        }
    }

    public static class StringExtensions
    {
        public static int ToInt(this string str, int defaultVl = 0)
        {
            bool success = int.TryParse(str, out int x);
            if (success)
            {
                return x;
            }
            else
            {
                return defaultVl;
            }
        }

        public static float ToFloat(this string str, float defaultVl = 0.0f)
        {
            bool success = float.TryParse(str, out float x);
            if (success)
            {
                return x;
            }
            else
            {
                return defaultVl;
            }
        }
    }

    /// <summary>
    /// Dictionary extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// A Dictionary&lt;TKey,TValue&gt; extension method that attempts to
        /// remove a key from the dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">[out] The value.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        /// <remarks>https://github.com/zzzprojects/Eval-SQL.NET/blob/master/src/Z.Expressions.SqlServer.Eval/Extensions/Dictionary%602/TryRemove.cs</remarks>
        public static bool TryRemove<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            var isRemoved = dictionary.TryGetValue(key, out value);
            if (isRemoved)
            {
                dictionary.Remove(key);
            }

            return isRemoved;
        }

        public static TValue AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                // TRY / CATCH should be done here, but this application does not require it
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// A Dictionary&lt;TKey,TValue&gt; extension method that adds or
        /// updates value for the specified key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="key">The key.</param>
        /// <param name="addValueFactory">The add value factory.</param>
        /// <param name="updateValueFactory">The update value factory.</param>
        /// <returns>A TValue.</returns>
        /// <remarks>https://github.com/zzzprojects/Eval-SQL.NET/blob/master/src/Z.Expressions.SqlServer.Eval/Extensions/Dictionary%602/AddOrUpdate.cs</remarks>
        public static TValue AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            TValue value;
            TValue oldValue;
            if (dictionary.TryGetValue(key, out oldValue))
            {
                value = updateValueFactory(key, oldValue);
                dictionary[key] = value;
            }
            else
            {
                value = addValueFactory(key);
                dictionary.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// A Dictionary&lt;TKey,TValue&gt; extension method that attempts to
        /// add a value in the dictionary for the specified key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        /// <remarks>https://github.com/zzzprojects/Eval-SQL.NET/blob/master/src/Z.Expressions.SqlServer.Eval/Extensions/Dictionary%602/TryAdd.cs</remarks>
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }

            return true;
        }

        /// <summary>
        /// Gets the value, if available, or <paramref name="ifNotFound"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="self">The dictionary to search.</param>
        /// <param name="key">The item key.</param>
        /// <param name="ifNotFound">The fallback value.</param>
        /// <returns>
        /// Returns the item in <paramref name="self"/> that matches <paramref name="key"/>,
        /// falling back to the value of <paramref name="ifNotFound"/> if the item is unavailable.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue ifNotFound = default(TValue))
        {
            TValue val;
            return self.TryGetValue(key, out val) ? val : ifNotFound;
        }

        /// <summary>
        /// Thread-safe way to gets or add the specified dictionary
        /// key and value pair.
        /// </summary>
        public static TValue GetOrAddThreadSafe<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Func<TKey, TValue> factory)
        {
            TValue tValue;
            TValue tValue1;

            lock (self)
            {
                if (!self.TryGetValue(key, out tValue))
                {
                    tValue = factory(key);
                    self[key] = tValue;
                }

                tValue1 = tValue;
            }

            return tValue1;
        }

        public static bool ContainsKeyWithValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, params TValue[] values)
        {
            if (self == null || values == null || values.Length == 0)
            {
                return false;
            }

            TValue temp;
            try
            {
                if (!self.TryGetValue(key, out temp))
                {
                    return false;
                }
            }
            catch (ArgumentNullException)
            {
                return false;
            }

            return values.Any(v => v.Equals(temp));
        }

        /// <summary>
        /// Tries to obtain the given key, otherwise returns the default value.
        /// </summary>
        /// <typeparam name="T">The struct type.</typeparam>
        /// <param name="values">The dictionary for the lookup.</param>
        /// <param name="key">The key to look for.</param>
        /// <returns>A nullable struct type.</returns>
        /// <remarks>https://github.com/AngleSharp/AngleSharp/blob/master/src/AngleSharp/Common/ObjectExtensions.cs#L106</remarks>
        public static T? TryGet<T>(this IDictionary<string, object> values, string key)
            where T : struct
        {
            if (values.TryGetValue(key, out var value) && value is T result)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Tries to obtain the given key, otherwise returns null.
        /// </summary>
        /// <param name="values">The dictionary for the lookup.</param>
        /// <param name="key">The key to look for.</param>
        /// <returns>An object instance or null.</returns>
        /// <remarks>https://github.com/AngleSharp/AngleSharp/blob/master/src/AngleSharp/Common/ObjectExtensions.cs#L123</remarks>
        public static object TryGet(this IDictionary<string, object> values, string key)
        {
            values.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value of the given key, otherwise the provided default value.
        /// </summary>
        /// <typeparam name="T">The type of the keys.</typeparam>
        /// <typeparam name="TU">The type of the value.</typeparam>
        /// <param name="values">The dictionary for the lookup.</param>
        /// <param name="key">The key to look for.</param>
        /// <param name="defaultValue">The provided fallback value.</param>
        /// <returns>The value or the provided fallback.</returns>
        /// <remarks>https://github.com/AngleSharp/AngleSharp/blob/master/src/AngleSharp/Common/ObjectExtensions.cs#L139</remarks>
        public static TU GetOrDefault<T, TU>(this IDictionary<T, TU> values, T key, TU defaultValue)
        {
            return values.TryGetValue(key, out var value) ? value : defaultValue;
        }

        #region Coroutine Defination
        private static IEnumerator DoAction(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
        private static IEnumerator DoActionRealtime(float time, Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action?.Invoke();
        }
        private static IEnumerator DoActionWaitUntil(Func<bool> func, Action action)
        {
            yield return new WaitUntil(func);
            action?.Invoke();
        }
        private static IEnumerator DoActionWaitWhile(Func<bool> func, Action action)
        {
            yield return new WaitWhile(func);
            action?.Invoke();
        }
        private static IEnumerator DoActionWaitForEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
        private static IEnumerator DoActionWhile(Func<bool> func, Action action, Action after = null)
        {
            while (func.Invoke())
            {
                action?.Invoke();
                yield return new WaitForEndOfFrame();
            }
            after?.Invoke();
        }
        #endregion

        #region Extension Method
        /// <summary>
        /// Do Action Wait a Time
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="time">time</param>
        /// <param name="action">action to do</param>
        /// <returns></returns>
        public static Coroutine ActionWaitTime(this MonoBehaviour mono, float time, Action action)
        {
            return mono.StartCoroutine(DoAction(time, action));
        }
        /// <summary>
        /// Do Action Wait a Real time
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="time">time</param>
        /// <param name="action">action to do</param>
        /// <returns></returns>
        public static Coroutine ActionWaitRealTime(this MonoBehaviour mono, float time, Action action)
        {
            return mono.StartCoroutine(DoActionRealtime(time, action));
        }
        /// <summary>
        /// Do Action Wait until a Func is true
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="func">func check</param>
        /// <param name="action">action to do</param>
        /// <returns></returns>
        public static Coroutine ActionWaitUntil(this MonoBehaviour mono, Func<bool> func, Action action)
        {
            return mono.StartCoroutine(DoActionWaitUntil(func, action));
        }
        /// <summary>
        /// Do Action Wait while a Func is true
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="func">func check</param>
        /// <param name="action">action to do</param>
        /// <returns></returns>
        public static Coroutine ActionWaitWhile(this MonoBehaviour mono, Func<bool> func, Action action)
        {
            return mono.StartCoroutine(DoActionWaitWhile(func, action));
        }
        /// <summary>
        /// Do Action Wait For End of Frame
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="action">action to do</param>
        /// <returns></returns>
        public static Coroutine ActionWaitForEndOfFrame(this MonoBehaviour mono, Action action)
        {
            return mono.StartCoroutine(DoActionWaitForEndOfFrame(action));
        }
        /// <summary>
        /// Do Action While Func is true
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="func">func check</param>
        /// <param name="action">action to do</param>
        /// <param name="after">action onComplete</param>
        /// <returns></returns>
        public static Coroutine ActionWhile(this MonoBehaviour mono, Func<bool> func, Action action, Action after = null)
        {
            return mono.StartCoroutine(DoActionWhile(func, action, after));
        }
        #endregion
    }
}