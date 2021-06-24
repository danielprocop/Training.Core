using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SystemWrapper.Interface
{
    public class FileWrap: IFile
    {
        public virtual void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        public virtual byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

    }
}
