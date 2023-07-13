using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace DefaultNamespace
{
    public class LevelProvider : ILevelProvider {
        public List<Level> LoadLevels(Player player) {
            string filePath = Path.Combine(Application.streamingAssetsPath, "Databases/userLevels.json");

            // Read the JSON data from the file
            string json = File.ReadAllText(filePath);
            
            Debug.Log("before \n" + json);

            // Remove comments from JSON (if any)
            json = RemoveJsonComments(json);
            Debug.Log("after \n" + json);

            // Parse the JSON data
            Dictionary<string, object> data = (Dictionary<string, object>)Json.Deserialize(json);

            // Get the levels for "12345"
            Dictionary<string, object> userLevelsData = (Dictionary<string, object>)((List<object>)data["userLevels"])[0];

            // Create a list to store the Level objects
            List<Level> levelList = new List<Level>();

            // Get the levels array
            List<object> levelsData = (List<object>)userLevelsData["levels"];

            // Iterate over each level
            for (int i = 0; i < levelsData.Count; i++)
            {
                // Extract the properties of the level
                int levelNumber = i + 1;
                Dictionary<string, object> levelData = (Dictionary<string, object>)levelsData[i];
                string levelName = (string)levelData["levelName"];
                int scoring = Convert.ToInt32(levelData["scoring"]);
                bool isAvailable = Convert.ToBoolean(levelData["isAvailable"]);

                // Create a new Level object and add it to the list
                Level level = new Level(levelNumber, levelName, scoring, isAvailable);
                levelList.Add(level);
            }


            return levelList;
        }

        // Remove comments from JSON string
        private static string RemoveJsonComments(string json)
        {
            // Remove single-line comments
            json = Regex.Replace(json, @"\/\/.*$", "", RegexOptions.Multiline);

            // Remove multi-line comments
            json = Regex.Replace(json, @"/\*(.*?)\*/", "", RegexOptions.Singleline);

            return json;
        }
    }
    
    // Simple JSON parser and serializer
    public static class Json
    {
        public static object Deserialize(string json)
        {
            return JsonParser.FromJson(json);
        }

        public static string Serialize(object obj)
        {
            return JsonSerializer.ToJson(obj);
        }
    }

    internal static class JsonParser
    {
        public static object FromJson(string json)
        {
            // JSON deserialization logic goes here...
            return null;
        }
    }

    internal static class JsonSerializer
    {
        public static string ToJson(object obj)
        {
            // JSON serialization logic goes here...
            return null;
        }
    }
}


