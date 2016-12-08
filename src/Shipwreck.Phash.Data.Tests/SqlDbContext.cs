using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Shipwreck.Phash.Data
{
    internal sealed class SqlDbContext : DbContext
    {
        static SqlDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<SqlDbContext>());
        }
    }
}