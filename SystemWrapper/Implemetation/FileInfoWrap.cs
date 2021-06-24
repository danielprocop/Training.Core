using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SystemWrapper.Interface;

namespace SystemWrapper.Implemetation
{
    public class FileInfoWrap : IFileInfo
    {
        public string FullName { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public FileInfoWrap(string fullName, string name, string extension)
        {
            FullName = fullName;
            Name = name;
            Extension = extension;
        }
       
    }
}
