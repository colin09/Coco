using MongoDB.Bson;
using com.mh.common.extension;


namespace com.mh.model.mongo.mgBase
{
    public abstract class MagicHorseBase
    {
        /// <summary>
        /// ObjectId
        /// </summary>
        public ObjectId Id { get; set; }

        public string StringId
        {
            get
            {
                return Id.ToString();
            }
        }

        public ObjectId CreateObjectId()
        {
            return new ObjectId(StringHelper.GetGUIDString());
        }

        public static string GetNewStringId => GetObjectId().ToString();





        
        public static ObjectId GetObjectId(string id)
        {
            return new ObjectId(id);
        }

        public static ObjectId GetObjectId()
        {
            return new ObjectId(StringHelper.GetGUIDString());
        }
    }

}