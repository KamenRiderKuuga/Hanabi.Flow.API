using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hanabi.Flow.Model.Models
{
    public class UserRole : RootEntity
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar",Length = 20,IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string Description { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? CreateId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
    }
}
