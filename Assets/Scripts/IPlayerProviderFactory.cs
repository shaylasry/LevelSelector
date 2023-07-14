public class IPlayerProviderFactory
{
    public static IPlayerProvider createPlayerProvider()
    {
        return new PlayerProvider();
    }    
}