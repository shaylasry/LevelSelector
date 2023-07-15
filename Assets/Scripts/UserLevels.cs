using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]

public class UsersLevelsData
{
    public List<UserLevels> UserLevels;
}
[Serializable]
public class UserLevels
{
    public int id;
    public int numberOfLevels;

    public List<Level> levels;
    // public List<UserLevelInfo> userLevels;
    //
    // public UserLevels(string jsonFilePath)
    // {
    //     if (File.Exists(jsonFilePath))
    //     {
    //         string json = File.ReadAllText(jsonFilePath);
    //         UserLevels data = JsonUtility.FromJson<UserLevels>(json);
    //         if (data != null)
    //         {
    //             userLevels = data.userLevels;
    //             foreach (UserLevelInfo u in userLevels)
    //             {
    //                 foreach (string info in u.userInfo.Keys)
    //                 {
    //                     Debug.Log(u.userInfo[info]);
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             Debug.LogError("Failed to deserialize JSON into UserLevels object.");
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("UserLevels JSON file not found at path: " + jsonFilePath);
    //     }
    // }
}
