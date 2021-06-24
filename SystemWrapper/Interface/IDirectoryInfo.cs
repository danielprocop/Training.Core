using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SystemWrapper.Interface
{
    public interface IDirectoryInfo
    {
        IFileInfo[] GetFiles();
        void Inizialize(string inputDirectoryPath);
    }
}
