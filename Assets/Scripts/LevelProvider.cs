using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace DefaultNamespace {
    public class LevelProvider : ILevelProvider {
        
        public List<Level> LoadLevels(Player player) {
            var userLevels = JsonConvert.DeserializeObject<UserLevels>(levelsJson);

// Get levels for user ID "12345"
            var levelsForUser = userLevels.UserLevels.FirstOrDefault(u => u.UserId == player.PlayerId.ToString())?.LevelData?.Levels;

// Create a list of Level objects
            List<Level> levelList = levelsForUser.Select(levelData =>
                new Level(levelData.LevelNum, "Level " + levelData.LevelNum,levelData.Scoring, levelData.IsAvailable)
            ).ToList();
            
            return levelList;
        }
    }
}