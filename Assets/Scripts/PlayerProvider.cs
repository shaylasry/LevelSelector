namespace DefaultNamespace {
    public class PlayerProvider : IPlayerProvider{
        public Player LoadPlayerData(int playerId) {
            return new Player(playerId);
        }
    }
}