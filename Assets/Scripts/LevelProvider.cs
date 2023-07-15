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
         UsersLevelsData allUsers = JsonUtility.FromJson<UsersLevelsData>(json);
         
         //shuold init or not?
         List<Level> levelList = new List<Level>();
         foreach (UserLevels userLevels in allUsers.UserLevels)
         {
             if (userLevels.id == player.playerId)
             {
                 levelList = new List<Level>(userLevels.levels);
             }
         }
            
         return levelList;
        }
        
   
}


