using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AidiCore.Attributes;
using System.Windows.Forms;
using System.Reflection;
using AidiCore.DataType;

namespace AidiCore.Manger
{
    public class AqModuleManger
    {
        public AqModuleManger()
        {
            LoadModules();
        }
        private void LoadModules()
        {
            TypeList = new Dictionary<string, Type>();
            this.ModuleNameList = new List<ModuleAttribute>();
            string path = Application.StartupPath + "\\Modules\\";
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                string[] array = files;
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    string extension = Path.GetExtension(text);
                    if (extension.ToLower()== ".dll")
                    {
                        try
                        {
                            Assembly assembly = Assembly.LoadFile(text);
                            IEnumerable<Type> exportedTypes = assembly.GetExportedTypes();
                            foreach (Type current in exportedTypes)
                            {
                                if (current.GetTypeInfo().IsClass)
                                {
                                    IEnumerable<ModuleAttribute> enumerable = current.GetTypeInfo().GetCustomAttributes().OfType<ModuleAttribute>();
                                    foreach (ModuleAttribute current2 in enumerable)
                                    {
                                        if (this.TypeList.ContainsKey(current2.ModuleName))
                                        {
                                            this.ModuleNameList.Add(current2);
                                            this.TypeList.Add(current2.ModuleName, current);
                                        }
                                        else
                                        {
                                            this.ModuleNameList.Add(current2);
                                            this.TypeList.Add(current2.ModuleName, current);
                                        }
                                    }
                                }

                            }
                        }
                        catch (Exception e)
                        {

                            throw;
                        }
                    }
                }
            }
        }

        private static AqModuleManger _instance = null;

        public static AqModuleManger Instance()
        {
            if (AqModuleManger._instance == null)
            {
                AqModuleManger._instance = new AqModuleManger();
            }
            return AqModuleManger._instance;
        }
        

        //又这个方法创建方案，可以确定不会创建的东西不会超纲
        public AqModuleData CreateModuleData(string ModuleName)
        {
            if (!this.TypeList.ContainsKey(ModuleName))
            {
                MessageBox.Show("模块不存在: " + ModuleName+"?");
                     
            }
            Type type = this.TypeList[ModuleName];
            return Activator.CreateInstance(type) as AqModuleData;
        }

        public List<ModuleAttribute> ModuleNameList
        {
            get;
            set;
        }


        public Dictionary<string, Type> TypeList
        {
            get;
            set;
        }


    }
}
