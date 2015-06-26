using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PeekPoker.Interface;

namespace PeekPoker.Plugin
{
    /// <summary>
    ///     Internal plug-in service class that get's all the information regarding the plug-in
    /// </summary>
    public class PluginService
    {
        private readonly List<AbstractPlugin> _optionPluginDatas = new List<AbstractPlugin>();
        private readonly List<AbstractPlugin> _pluginDatas = new List<AbstractPlugin>();

        /// <summary>
        ///     Plug-in service constructor
        /// </summary>
        /// <param name="folderPath"> The plug-in folder path </param>
        public PluginService(string folderPath)
        {
            try
            {
                foreach (string plugin in Directory.GetFiles(folderPath))
                {
                    var file = new FileInfo(plugin);
                    if (file.Extension.Equals(".dll"))
                    {
                        AddPlugin(plugin);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     Plug-in Data
        /// </summary>
        public List<AbstractPlugin> PluginDatas
        {
            get
            {
                _pluginDatas.Sort((x, y) => String.CompareOrdinal(y.ApplicationName, x.ApplicationName));
                return _pluginDatas;
            }
        }

        public List<AbstractPlugin> OptionPluginDatas
        {
            get
            {
                _optionPluginDatas.Sort((x, y) => String.CompareOrdinal(y.ApplicationName, x.ApplicationName));
                return _optionPluginDatas;
            }
        }

        private void AddPlugin(string pluginPath)
        {
            try
            {
                Assembly pluginAssembly = Assembly.LoadFrom(pluginPath); //Load assembly given its full name and path

                foreach (Type pluginType in pluginAssembly.GetTypes())
                {
                    if (!pluginType.IsPublic) continue; //break the for each loop to next iteration if any
                    if (pluginType.IsAbstract) continue; //break the for each loop to next iteration if any
                    //search for specified interface while ignoring case sensitivity
                    if (pluginType.BaseType == null ||
                        pluginType.BaseType.FullName != "PeekPoker.Interface.AbstractPlugin")
                        continue;
                    //New plug-in information setting
                    var pluginInterfaceInstance =
                        (AbstractPlugin) (Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString())));

                    if (pluginInterfaceInstance.PluginType == PluginType.Game)
                        _pluginDatas.Add(pluginInterfaceInstance);
                    else
                        _optionPluginDatas.Add(pluginInterfaceInstance);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}