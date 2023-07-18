namespace DefaultNamespace {
    public interface ILevelPresenter {
        void DefaultLevelPresenter(IPlayerProvider playerProvider, ILevelProvider levelProvider);

        bool IsLevelAvailable(Level level);
        Score LevelScoring(Level level);

    }
}