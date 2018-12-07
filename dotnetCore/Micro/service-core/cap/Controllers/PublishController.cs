using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cap.db;
using cap.db.entity;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace cap.Controllers {
    [Route ("pub")]
    [ApiController]
    public class PublishController : ControllerBase {

        private readonly IConfiguration _config;
        private readonly ICapPublisher _publisher;

        public PublishController (IConfiguration config, ICapPublisher publisher) {
            this._config = config;
            this._publisher = publisher;
        }

        [HttpGet ]
        public async Task<IActionResult> WithoutTransaction () {
            await _publisher.PublishAsync ("cap1.rabbitmq.mysql.WithoutTransaction", DateTime.Now);

            return Ok ();
        }
        /*
        [HttpGet ("/ado")]
        public IActionResult WithTransaction () {
            using (var connection = new MySql.Data.MySqlConnection (_config.GetConnectionString ("HuiConnectionString"))) {
                using (var transaction = connection.BeginTransaction (_publisher, autoCommit : false)) {
                    //your business code
                    connection.Execute ("insert into test(name) values('test')", transaction: (IDbTransaction) transaction.DbTransaction);

                    for (int i = 0; i < 5; i++) {
                        _publisher.Publish ("cap.rabbitmq.mysql.WithTransaction", DateTime.Now);
                    }

                    transaction.Commit ();
                }
            }
            return Ok ();
        }*/

        [HttpGet ("ef")]
        public IActionResult EntityFrameworkWithTransaction ([FromServices] HuiContext db) {
            using (var trans = db.Database.BeginTransaction (_publisher, autoCommit : false)) {
                db.Test.Add (new TestEntity () { Name = "ef.transaction" });

                for (int i = 0; i < 5; i++) {
                    _publisher.Publish ("cap.rabbitmq.mysql.EntityFrameworkWithTransaction", DateTime.Now);
                }

                db.SaveChanges ();
                trans.Commit ();
            }
            return Ok ();
        }

        [NonAction]
        [CapSubscribe ("cap.rabbitmq.mysql.*")]
        public void Subscriber (DateTime time) {    
            Console.WriteLine ($"{DateTime.Now}, Subscriber invoked, Sent time:{time}");
        }

        [NonAction]
        [CapSubscribe ("cap1.rabbitmq.mysql.*")]
        public void Subscriber2 (DateTime time) {
            Console.WriteLine ($"{DateTime.Now}, Subscriber invoked, Sent time:{time}");
        }

    }
}