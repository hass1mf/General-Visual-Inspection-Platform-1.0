using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidiCore.Interface
{
    public interface IModule
    {
        void InitModule(string projectDirectory, string nodeName);

        void SaveModule(string projectDirectory, string nodeName);

        void CloseModule();

        void Run();

        bool StartSetForm();
    }
}
