using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SystemWrapper.Interface;

namespace SystemWrapper.Implemetation
{
    public class DirectoryInfoWrap : IDirectoryInfo
    {
        private DirectoryInfo _directoryInfo;

        public void Inizialize(string inputDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(inputDirectoryPath))
            {
                throw new ArgumentNullException(nameof(inputDirectoryPath));
            }
            _directoryInfo = new DirectoryInfo(inputDirectoryPath);
        }
       

        public IFileInfo[] GetFiles()
        {

            FileInfo[] fileInfos = _directoryInfo.GetFiles();
            var fileInfoWraps = new FileInfoWrap[fileInfos.Length];
            for (int i = 0; i < fileInfos.Length; i++)
                fileInfoWraps[i] = new FileInfoWrap(fileInfos[i]);
            return fileInfoWraps;
        }
    }
}
