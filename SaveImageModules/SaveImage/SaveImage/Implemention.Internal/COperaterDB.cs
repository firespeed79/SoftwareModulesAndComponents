﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using DataBaseComponent;
using DataBaseComponent.SqlServer;
using System.Windows.Forms;

//**********************************************
//文件名：COperaterDB
//命名空间：SaveImage.Implemention.Internal
//CLR版本：4.0.30319.42000
//内容：
//功能：
//文件关系：
//作者：胡志乾
//小组：
//生成日期：2018/5/12 14:58:16
//版本号：V1.0.0.0
//修改日志：
//版权说明：
//联系电话：18352567214
//**********************************************

namespace SaveImage.Implemention.Internal
{
    internal class COperaterDB
    {
        private static string mdfFilePath = System.Environment.CurrentDirectory + @"\SaveImageDB.mdf";
        private string linkString;


        private SqlServerHelper sqlServerHelper;
        #region 构造函数

        public COperaterDB()
        {
            linkString = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True;Connect Timeout=30;User Instance=true", mdfFilePath);
            sqlServerHelper = new SqlServerHelper(linkString);
            LinkDB = sqlServerHelper.TestCanLink();  //测试是否能连接上数据库
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbLinkStr">数据库文件的完整路径</param>
        public COperaterDB(string dbLinkStr)
        {
            linkString = dbLinkStr;
            sqlServerHelper = new SqlServerHelper(linkString);
            LinkDB = sqlServerHelper.TestCanLink();  //测试是否能连接上数据库
        }
        #endregion


        #region 属性
        /// <summary>
        /// 获取数据库是否连接
        /// </summary>
        public bool LinkDB { get; private set; } = false;


        #endregion

        #region 公共方法

        /// <summary>
        /// 删除数据库中对应图片文件路径行
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool DeleteInfo(string filePath)
        {
            try
            {
                if (sqlServerHelper.DeleteRowData("SaveInfo", "FilePath", filePath))
                {
                    //LogModules.LogControlser.WriteLog("删除数据记录：" + filePath);
                    return true;
                }
                else
                {
                    //LogModules.LogControlser.WriteLog("未能删除数据记录：" + filePath);
                    return false;
                }
            }
            catch (Exception ex)
            {
                //LogModules.LogControlser.WriteLog("未能删除数据记录出错：" + ex.ToString());
                return false;
            }

        }

        /// <summary>
        /// 向数据库中写入保存图片的信息
        /// </summary>
        /// <param name="saveTime">保存时间</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        public bool WriteSaveInfo(string saveTime, string savePath)
        {
            try
            {
                return sqlServerHelper.WriteDataToDB("SaveInfo", new[] { "SaveTime", "FilePath" }, new[] { saveTime, savePath });
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取最早的文件路径
        /// </summary>
        /// <returns></returns>
        public string[] GetEarlistSavePath(int queryCount)
        {
            try
            {
                return sqlServerHelper.QueryByTimeTheEarliestFieldsValue<string>("SaveInfo", "SaveTime", "FilePath", queryCount);
            }
            catch (Exception ex)
            {
                LogModules.LogControlser.WriteLog(ex.ToString());
                throw ex;
            }

        
        }

        /// <summary>
        /// 获取数据库中大于某一时间的所有文件
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<string> GetBeforeTimeAllFile(string time)
        {
            return sqlServerHelper.QueryByTimeBeforeTimeOneFieldAllValue("SaveInfo", "SaveTime", time, "FilePath");
        }

        public void ClearDB()
        {
            sqlServerHelper.DeleteAllTableData("SaveInfo");
        }
        #endregion

        #region 私有方法



        #endregion

        #region 委托



        #endregion

        #region 事件



        #endregion
    }
}
