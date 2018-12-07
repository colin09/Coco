using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cap.db.entity {
        [Table("Test")]
        public class TestEntity {

            public int Id { get; set; }

            public string Name { get; set; }
        }
}