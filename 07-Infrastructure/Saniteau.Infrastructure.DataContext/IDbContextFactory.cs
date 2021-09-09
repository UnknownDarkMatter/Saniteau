using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Infrastructure.DataContext
{
    public interface IDbContextFactory
    {
        SaniteauDbContext CreateDbContext();
    }
}
