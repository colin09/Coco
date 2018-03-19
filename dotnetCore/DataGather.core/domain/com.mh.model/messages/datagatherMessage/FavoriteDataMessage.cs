using System;

namespace com.mh.model.messages.datagatherMessage
{

    public class FavoriteDataMessage : DataGatherBaseMessage
    {
        public override int ActionType
        {
            get { return (int)MessageAction.NewFavorite; }
            set { }
        }


        public int CustomerId { get; set; }

        public int BuyerUserId { get; set; }

        public int FavoriteFromType { get; set; }

        /// <summary>
        /// 关注 / 取消 0
        /// </summary>
        public int FavoriteOption { set; get; }


    }
}
