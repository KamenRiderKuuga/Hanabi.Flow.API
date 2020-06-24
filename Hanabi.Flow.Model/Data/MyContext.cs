using Hanabi.Flow.Common.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hanabi.Flow.Data
{
    public class MyContext
    {
        private SqlSugarClient _db;
        private static string _connectionString;
        private static DbType _dbType;

        /// <summary>
        /// 数据连接对象 
        /// Blog.Core 
        /// </summary>
        public SqlSugarClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }

        /// <summary>
        /// 连接字符串 
        /// Blog.Core
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        /// <summary>
        /// 数据库类型 
        /// Blog.Core 
        /// </summary>
        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        public MyContext()
        {
            string connectionString = AppSettings.app("DBSetting", "DBString");
            string dbType = AppSettings.app("DBSetting", "DBType");
            _connectionString = connectionString;
            _dbType = dbType.ObjToEnum<DbType>();
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("数据库连接字符串为空");
            }

            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//mark
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = true
                }
            });
        }

        public void GeneratorData()
        {
            Console.WriteLine("正在初始化数据库...");
            _db.DbMaintenance.CreateDatabase();
            Console.WriteLine("数据库初始化完毕");

            Console.WriteLine("正在遍历Models");
            var modelTypes = Assembly.GetExecutingAssembly().GetTypes()
                                                       .Where(type => type.IsClass && type.Namespace == "Hanabi.Flow.Model.Models")
                                                       .Select(type => type)
                                                       .ToList();

            modelTypes.ForEach(model =>
            {
                if (!_db.DbMaintenance.IsAnyTable(model.Name))
                {
                    Console.WriteLine($"正在建{model.Name}表");
                    _db.CodeFirst.InitTables(model);
                }
            });

            Console.WriteLine("建表完成");
        }

        /// <summary>
        /// 功能描述:根据实体类生成数据库表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lstEntitys">指定的实体</param>
        public void CreateTableByEntity<T>(bool blnBackupTable, params T[] lstEntitys) where T : class, new()
        {
            Type[] lstTypes = null;
            if (lstEntitys != null)
            {
                lstTypes = new Type[lstEntitys.Length];
                for (int i = 0; i < lstEntitys.Length; i++)
                {
                    T t = lstEntitys[i];
                    lstTypes[i] = typeof(T);
                }
            }
            CreateTableByEntity(blnBackupTable, lstTypes);
        }

        /// <summary>
        /// 功能描述:根据实体类生成数据库表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lstEntitys">指定的实体</param>
        public void CreateTableByEntity(bool blnBackupTable, params Type[] lstEntitys)
        {
            if (blnBackupTable)
            {
                _db.CodeFirst.BackupTable().InitTables(lstEntitys); //change entity backupTable            
            }
            else
            {
                _db.CodeFirst.InitTables(lstEntitys);
            }
        }
    }
}