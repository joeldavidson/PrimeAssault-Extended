using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Game.Helpers
{
    /// <summary>
    /// Json Helper for parsing the Service returned datasets
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0034:Simplify 'default' expression", Justification = "<Pending>")]
    #pragma warning disable CA1031 // Do not catch general exception types
    public static class JsonHelper
    {
        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetJsonString(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return null;
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return null;
                }

                return tempJsonObject;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool GetJsonBool(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return false;
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return false;
                }

                if (tempJsonObject == "True")
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int GetJsonInteger(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return -1;
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return -1;
                }

                var myReturn = int.Parse(tempJsonObject);
                return myReturn;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static double GetJsonDouble(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return -1;
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return -1;
                }

                var myReturn = double.Parse(tempJsonObject);
                return myReturn;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Get Json Long
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static long GetJsonLong(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return -1;
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return -1;
                }

                var myReturn = long.Parse(tempJsonObject);
                return myReturn;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Get Json Ulong
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static ulong GetJsonuLong(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return 0;
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return 0;
                }

                var myReturn = ulong.Parse(tempJsonObject);
                return myReturn;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static List<T> GetJsonList<T>(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return default(List<T>);
            }

            if (json == null)
            {
                return default(List<T>);
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field];
                if (tempJsonObject == null)
                {
                    return default(List<T>);
                }

                return tempJsonObject.ToObject<List<T>>();

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return default(List<T>);
            }
        }

        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T GetObject<T>(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return default(T);
            }

            if (json == null)
            {
                return default(T);
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field];
                if (tempJsonObject == null)
                {
                    return default(T);
                }

                return tempJsonObject.ToObject<T>();

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return default(T);
            }
        }

        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static List<string> GetJsonStringList(JObject json, string field)
        {
            var myStringList = new List<string>();

            if (string.IsNullOrEmpty(field))
            {
                return myStringList;
            }

            if (json == null)
            {
                return myStringList;
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field];
                if (tempJsonObject == null)
                {
                    return myStringList;
                }

                var myJsonString = tempJsonObject.ToString();

                myStringList = JsonConvert.DeserializeObject<List<string>>(myJsonString);
                return myStringList;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return myStringList;
            }
        }

        /// <summary>
        /// Takes a json object, and retrieves a string from it matching the field
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static JObject GetJsonObject(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return null;
            }

            // Get Field
            try
            {

                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return null;
                }

                // Empty List
                if (tempJsonObject.Equals("[]"))
                {
                    return null;
                }

                //Parse the string to make sure it is valid json
                json = JObject.Parse(tempJsonObject);

                //Could not get unit Test to generate invalid json, so commenting out this block of clode.
                //if (json == null)
                //{
                //    return null;
                //}

                return json;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get the Object List of Object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static List<JObject> GetJObjectList(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return new List<JObject>();
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return new List<JObject>();
                }

                // Empty List
                if (tempJsonObject.Equals("[]"))
                {
                    return new List<JObject>();
                }

                //Parse the string to make sure it is valid json
                var tempJarray = JArray.Parse(tempJsonObject);

                List<JObject> tempJObjectList = new List<JObject>();
                foreach (var item in tempJarray.Children())
                {
                    tempJObjectList.Add((JObject)item);
                }

                return tempJObjectList;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return new List<JObject>();
            }
        }

        /// <summary>
        /// Get the Object as a List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static List<T> GetObjectList<T>(JObject json, string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return new List<T>();
            }

            // Get Field
            try
            {
                var tempJsonObject = json[field].ToString();
                if (string.IsNullOrEmpty(tempJsonObject))
                {
                    return new List<T>();
                }

                // Empty List
                if (tempJsonObject.Equals("[]"))
                {
                    return new List<T>();
                }

                //Parse the string to make sure it is valid json
                var tempJarray = JArray.Parse(tempJsonObject);

                List<T> tempJObjectList = new List<T>();
                foreach (var item in tempJarray.Children())
                {
                    tempJObjectList.Add((T)Activator.CreateInstance(typeof(T), new object[] { (JObject)item }));
                }

                return tempJObjectList;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                return new List<T>();
            }
        }
    }
}