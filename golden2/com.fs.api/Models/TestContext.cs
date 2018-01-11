

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace com.fs.api.Models
{

    public class TestContext : DbContext
    {

        public TestContext()
        {

        }

        public TestContext(DbContextOptions<TestContext> options)
        : base(options)
        {

        }


        private void MemoryDb()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;

            /**
            using (var context = new TestDbContext(options)) {
                context.TestEntities.Add(testEntity);
                context.SaveChanges();
            } 
             */
        }

        private void MemorySQLiteDb()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseSqlite(connection);
            options = builder.Options;

            /**
            using (var context = new TestDbContext(options)) {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                //.--.--.--.
            }
             */
        }


    }

}