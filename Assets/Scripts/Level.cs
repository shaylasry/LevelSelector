namespace DefaultNamespace {
    public class Level {
        private int _levelNumber;
        private string _levelName;
        private int _scoring;
        private bool _isAvailable;

        public int LevelNumber
        {
            get {
                return _levelNumber;
            }
        }
        public string LevelName
        {
            get {
                return _levelName;
            }
        }
        public int Scoring
        {
            get {
                return _scoring;
            }
        }
        public bool IsAvailable
        {
            get {
                return _isAvailable;
            }
        }
        public Level(int levelNumber, string levelName, int scoring, bool isAvailable) {
            this._levelNumber = levelNumber;
            this._levelName = levelName;
            this._scoring = scoring;
            this._isAvailable = isAvailable;

        }
    }
}