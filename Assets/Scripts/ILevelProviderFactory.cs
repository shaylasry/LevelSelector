public class ILevelProviderFactory
{
    public static ILevelProvider createLevelProvider()
    {
        return new LevelProvider();
    }    
}
