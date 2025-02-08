using System;
using System.IO;

namespace Comparo.Controller.Utilities
{
    public static class FileUtility
    {
        public static string GetUniqueFileName(bool overwriteIfExists, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Filename cannot be null or empty", nameof(fileName));

            string directory = Path.GetDirectoryName(fileName);
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            // Ensure directory exists
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(fileName))
            {
                if (overwriteIfExists)
                {
                    return fileName;
                }
                else
                {
                    int counter = 1;
                    string newFileName;
                    do
                    {
                        newFileName = Path.Combine(directory, $"{nameWithoutExtension} ({counter}){extension}");
                        counter++;
                    } while (File.Exists(newFileName));

                    return newFileName;
                }
            }

            return fileName;
        }
    }

}
