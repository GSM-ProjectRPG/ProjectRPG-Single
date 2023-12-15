using MySql.Data.EntityFramework;
using System;
using System.Data.Common;
using System.Data.Entity;


namespace DB
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class UserData : DbContext
    {
        public DbSet<LoginData> LoginData { get; set; }

        public UserData() : base()
        {
        }

        public UserData(DbConnection connection, bool contextOwnsConnectiong)
            : base(connection, contextOwnsConnectiong)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LoginData>().MapToStoredProcedures();
        }
    }

    public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
