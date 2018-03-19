using System;



namespace com.mh.model.mysql.entity
{

    public class UserEntity : BaseEntity
    {
        public string Name { set; get; }
        public string NickName { set; get; }
        public string Password { set; get; }

        public string Mobile { set; get; }
        public string EMail { set; get; }
        public string Logo { set; get; }
        public int Gender { set; get; }
        public string Description { set; get; }

        public string Country { set; get; }
        public string Province { set; get; }
        public string City { set; get; }
    }

}