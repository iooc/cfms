using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Authentication.EntityFrameworkCore
{
    public class CfmsIdentityDbContext<TTenant, TUser, TRole, TPrimaryKey> : IdentityDbContext<TUser,TRole, TPrimaryKey> 
        where TUser : IdentityUser<TPrimaryKey>
        where TRole : IdentityRole<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}
