﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Drawing;
using System.Threading.Tasks;

//**********************************************
//文件名：SaveImageDecorator
//命名空间：SaveImage
//CLR版本：4.0.30319.42000
//内容：实现CSaveImage功能，与CSaveImage是为聚合关系
//功能：CSaveImage的装饰者
//文件关系：
//作者：胡志乾
//小组：
//生成日期：2018/4/23 14:50:27
//版本号：V1.0.0.0
//修改日志：
//版权说明：
//联系电话：18352567214
//**********************************************

namespace SaveImage
{
    public class SaveImageDecorator : ISaveImage
    {
        //被装饰者
        private ISaveImage mySaveImage;


        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SaveImageDecorator(ISaveImage _SaveImage)
        {
            mySaveImage = _SaveImage;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置图片保存路径
        /// </summary>
        public string SavePath
        {
            get { return mySaveImage.SavePath; }
            set { mySaveImage.SavePath = value; }
        }

        /// <summary>
        /// 获取保存图片的根目录
        /// </summary>
        public string SaveImageRootDictroy
        {
            get { return mySaveImage.SaveImageRootDictroy; }
        }

        /// <summary>
        /// 获取或设置保存图像的类型
        /// </summary>
        public SaveImageType SaveType
        {
            get { return mySaveImage.SaveType; }
            set { mySaveImage.SaveType = value; }
        }

        /// <summary>
        /// 获取或设置是否保存图像
        /// </summary>
        public bool IsSaveImage
        {
            get { return mySaveImage.IsSaveImage; }
            set { mySaveImage.IsSaveImage = value; }
        }

        /// <summary>
        /// 获取之前被保存图像的名称
        /// </summary>
        public string ImageName
        {
            get { return mySaveImage.ImageName; }
        }

        /// <summary>
        /// 获取或设置是否在图像名中自动添加时间后缀
        /// </summary>
        public bool IsAddTimeToImageName
        {
            get { return mySaveImage.IsAddTimeToImageName; }
            set { mySaveImage.IsAddTimeToImageName = value; }
        }

        /// <summary>
        /// 获取或设置图像队列数量
        /// </summary>
        public int ImageQueueMaxCount
        {
            get { return mySaveImage.ImageQueueMaxCount; }
            set { mySaveImage.ImageQueueMaxCount = value; }
        }

        public string SectionName
        {
            get { return mySaveImage.SectionName; }
            set { mySaveImage.SectionName = value; }
        }

        public string ConfigFilePath
        {
            get { return mySaveImage.ConfigFilePath; }
        }
        #endregion


        #region 公共方法

        /// <summary>
        /// 保存bitmap类型的图片
        /// </summary>
        /// <param name="image"></param>
        public virtual string Save(Bitmap image, string imageName)
        {
            try
            {
                return mySaveImage.Save(image, imageName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public virtual void SaveImageByFullName(Bitmap image, string imageFullName)
        {
            try
            {
                mySaveImage.SaveImageByFullName(image, imageFullName);
            }
            catch (Exception ex)
            {
                LogModules.LogControlser.WriteLog(ex.ToString());
                throw ex;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        #endregion

        #region 私有方法

        protected virtual void Dispose(bool disposing)
        {
            //释放非托管内存

            if (disposing)
            {
                //释放托管内存
                mySaveImage.Dispose();
            }

        }


        #endregion

        #region 委托



        #endregion

        #region 事件
        /// <summary>
        /// 保存图像完成事件
        /// </summary>
        public event SaveImasgeCompleteEventHandle SaveCompleteEvent;

        /// <summary>
        /// 保存图像路径改变事件
        /// </summary>
        public event SavePathChangedEventHandle SavePathChangedEvent;

        /// <summary>
        /// 保存图像根目录改变事件
        /// </summary>
        public event SaveImageRootDirectoryChangedEventHandle RootDirectoryChangedEvent;

        #endregion
    }
}
