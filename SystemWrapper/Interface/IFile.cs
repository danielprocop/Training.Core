using System;
using System.Collections.Generic;
using System.Text;

namespace SystemWrapper.Interface
{
    public interface IFile
    {
        void Move(string sourceFileName, string destFileName);
        byte[] ReadAllBytes(string path);

    }
}
