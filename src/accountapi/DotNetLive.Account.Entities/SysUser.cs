﻿using DotNetLive.Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetLive.Account.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("auth.sysuser")]
    public class SysUser : BaseEntity
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("username")]
        public string UserName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("phonenumber")]
        public string PhoneNumber { get; set; }
        [Column("passwordhash")]
        public string PasswordHash { get; set; }
        [Column("securitystamp")]
        public string SecurityStamp { get; set; }
        [Column("istwofactorenabled")]
        public bool IsTwoFactorEnabled { get; set; }
        [Column("accessfailedcount")]
        public int AccessFailedCount { get; set; }
        [Column("islockoutenabled")]
        public bool IsLockoutEnabled { get; set; }
        [Column("lockoutenddate")]
        public DateTime? LockoutEndDate { get; set; }
        [Column("createdon")]
        public DateTime? CreatedOn { get; set; }
        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
}
