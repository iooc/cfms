using Cfms.Basic.Interfaces.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cfms.Authentication.EntityFrameworkCore
{
    /// <summary>
    /// 带用户管理的数据访问上下文
    /// </summary>
    /// <typeparam name="TTenant">租户实体</typeparam>
    /// <typeparam name="TUser">用户实体</typeparam>
    /// <typeparam name="TRole">角色实体</typeparam>
    /// <typeparam name="TPrimaryKey">授权数据的主键类型</typeparam>
    public abstract class CrossIdentityDbContext<TTenant, TUser, TRole, TPrimaryKey> 
        : IdentityDbContext<TUser,TRole, TPrimaryKey> , ICrossDbContext
        where TUser : IdentityUser<TPrimaryKey>
        where TRole : IdentityRole<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// 使用给定的数据访问上下文可选参数初始化标示数据库的基类
        /// </summary>
        /// <param name="options">可选参数</param>
        public CrossIdentityDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
