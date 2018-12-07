using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace cap.db.entity {

    [Table ("Users")]
    public class UserEntity : BaseEntity {

        [MaxLength (32)]
        public string Name { set; get; }

        [MaxLength (32)]
        public string NickName { set; get; }

        [MaxLength (32)]
        public string Password { set; get; }

        [MaxLength (16)]
        public string Mobile { set; get; }

        [MaxLength (32)]
        public string EMail { set; get; }

        [MaxLength (32)]
        public string Logo { set; get; }

        public int Gender { set; get; }

        [MaxLength (256)]
        public string Description { set; get; }

        [MaxLength (32)]
        public string Country { set; get; }

        [MaxLength (32)]
        public string Province { set; get; }

        [MaxLength (32)]
        public string City { set; get; }

        public static void BuildMapping (ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserEntity> ().ToTable ("Users");
        }

    }
}