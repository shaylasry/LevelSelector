using System.Collections.Generic;

namespace DefaultNamespace {
    public class LevelPresenter : ILevelPresenter {
        public void DefaultLevelPresenter(IPlayerProvider playerProvider, ILevelProvider levelProvider) {
            Player curPlayer = playerProvider.LoadPlayerData(1);
            List<Level> levels = levelProvider.LoadLevels();
            foreach (Level level in levels)
            {
                // Code to be executed for each level
                if (IsLevelAvailable(curPlayer, level)) {
                    Score levelScore = LevelScoring(curPlayer, level);
                }
            }
        }

        public bool IsLevelAvailable(Player player, Level level) {
            throw new System.NotImplementedException();
        }

        public Score LevelScoring(Player player, Level level) {
            throw new System.NotImplementedException();
        }
    }
}