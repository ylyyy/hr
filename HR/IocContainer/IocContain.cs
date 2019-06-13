using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace IocContainer
{
    public class IocContain
    {
        public static T CreateAll<T>(string loadconfig, string resolve)
        {
            UnityContainer ioc = CreateIoc(loadconfig);
            T all = ioc.Resolve<T>(resolve);
            return all;
        }
        private static UnityContainer CreateIoc(string name)
        {
            UnityContainer ioc = new UnityContainer();
            //把Unity文件转成对象
            ExeConfigurationFileMap exf = new ExeConfigurationFileMap();
            //获取绝对路径
            exf.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Unity.config";
            //把Unity文件对象转成了配置对像
            Configuration cf = ConfigurationManager.OpenMappedExeConfiguration(exf, ConfigurationUserLevel.None);
            //读取配置对象中的Unity节点
            UnityConfigurationSection cfs = (UnityConfigurationSection)cf.GetSection("unity");
            //把节点装入容器
            ioc.LoadConfiguration(cfs, name);
            return ioc;
        }
    }
}
