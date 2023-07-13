using System.Collections.Generic;

namespace DefaultNamespace {
    public interface ILevelProvider {
        //shuold get a source object from db
        List<Level> LoadLevels();
    }
}