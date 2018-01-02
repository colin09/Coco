using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.mongo.data
{


    [BsonIgnoreExtraElements]
    public class MgContent : MgBaseModel
    {
        public int NavId{set;get;}
        /// <summary>
        /// 1 index,2 main,3 detail
        /// </summary>
        public int NavType{set;get;}
        public float SourceId{set;get;}
        public List<> Items{set;get;}
    }

    public class MgContentItem{
    	public float Id{set;get;}
    	public string Name{set;get;}
    	public string Desc{set;get;}
    	public string ImgUrl{set;get;}
    	public string Remark{set;get;}
    	public string Sort{set;get;}
    }
}




/*
json:
{
	'NavId':0, 
	'NavPageType': //1 index,2 main,3 detail
	'SourceId':1,
	'Items':[
		{
			'id':1,
			'name':'fff',
			'desc':'图片之上',
			'imgUrl':'',
			'remark':'图片之下',
			'sort':999
		}
	]
}
*/