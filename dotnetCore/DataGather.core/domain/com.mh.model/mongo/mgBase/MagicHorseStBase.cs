
using com.mh.common.extension;
using com.mh.common.configuration;


namespace com.mh.model.mongo.mgBase
{

    public abstract class MagicHorseStBase : MagicHorseBase
    {
        public static string CollectionName<T>() where T : MagicHorseStBase
        {
            return $"{typeof(T).Name}_{ConfigManager.MongoMagicalHorseStatDataVersion}";
        }
    }

}