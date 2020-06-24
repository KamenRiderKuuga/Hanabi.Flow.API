using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hanabi.Flow.Model.Models
{
    public class UserInfo : RootEntity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string LoginPassword { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int RoleId { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int CreateId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string UserName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = int.MaxValue, IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public System.DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public System.DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime LastErrTime { get; set; } = DateTime.Now;

        // 性别
        [SugarColumn(IsNullable = true)]
        public int Sex { get; set; } = 0;

        // 年龄
        [SugarColumn(IsNullable = true)]
        public int Age { get; set; }

        // 生日
        [SugarColumn(IsNullable = true)]
        public DateTime Birth { get; set; } = DateTime.Now;

        /// <summary>
        /// 头像路径
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string PicPath { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
    }
}
