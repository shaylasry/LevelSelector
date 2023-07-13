using System.Collections.Generic;

namespace DefaultNamespace {
    public class LevelProvider : ILevelProvider {
        
        public List<Level> LoadLevels() {
            List<Level> levelList = new List<Level>();
            for (int i = 0; i < 10; i++) {
                string levelName = "Level " + i;
                levelList.Add(new Level(i + 1, levelName));
            }

            return levelList;
        }
    }
}