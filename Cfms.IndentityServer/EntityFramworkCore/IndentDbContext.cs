using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cfms.IndentityServer.EntityFramworkCore
{
    public class IdentDbContext:DbContext
    {
        public IdentDbContext(DbContextOptions options) :base(options){ }
    }
}
