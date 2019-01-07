using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataBaseProject.Data
{
    internal static class StreamManager
    {
        private static List<FileStream> activeFileStreams = new List<FileStream>();

        public static FileStream CreateFileStream(string filePath, FileMode mode)
        {
            try
            {
                var fileStream = activeFileStreams.Find(stream =>
                {
                    return stream.Name == filePath;
                });
                if (fileStream != null)
                {
                    CloseFileStream(fileStream);
                }
                var newStream = new FileStream(filePath, mode);
                activeFileStreams.Add(newStream);
                return newStream;
            }
            catch(FileNotFoundException exception)
            {
                throw exception;
            }
        }

        public static void CloseFileStream(FileStream fileStream)
        {
            fileStream.Close();
            activeFileStreams.Remove(fileStream);
        }

        public static void CloseFileStream(string filePath)
        {
            var fileStream = activeFileStreams.Find(stream =>
            {
                return stream.Name == filePath;
            });
            fileStream.Close();
            activeFileStreams.Remove(fileStream);
        }
    }
}
