using Microsoft.EntityFrameworkCore;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Tests
{
    public class DbContextFactoryMock : IDbContextFactory
    {
        private readonly string _dbName;
        public DbContextFactoryMock(string dbName)
        {
            _dbName = dbName;
        }
        public SaniteauDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SaniteauDbContext>().UseInMemoryDatabase(databaseName: _dbName).Options;
            return new SaniteauDbContext(options);
        }
    }
}
