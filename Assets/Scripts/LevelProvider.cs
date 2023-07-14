using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

 public class LevelProvider : ILevelProvider {
     public List<Level> LoadLevels(Player player)
     {
         string userLevelsJsonPath = Path.Combine(Application.dataPath, "Databases", "UserLevels.json");
         string json = File.ReadAllText(userLevelsJsonPath);
         Debug.Log("before fetch all users  "  + userLevelsJsonPath);
         RootObject allUsers = JsonUtility.FromJson<RootObject>(json);
         List<Level> levelList = new List<Level>();
         
            
            return levelList;
        }
        
   
}


