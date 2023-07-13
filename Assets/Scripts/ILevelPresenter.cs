namespace DefaultNamespace {
    public interface ILevelPresenter {
        void DefaultLevelPresenter(IPlayerProvider playerProvider, ILevelProvider levelProvider);

        bool IsLevelAvailable(Player player, Level level);
        Score LevelScoring(Player player, Level level);

    }
}