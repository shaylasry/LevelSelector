using System.Collections.Generic;
using DefaultNamespace;

public interface ILevelProvider {
    //shuold get a source object from db
    List<Level> LoadLevels(Player player);
}