using System.IO;

namespace TurtleChallenge.DataAccess
{
    /// <summary> Allows the readonly access to the files saved on the system. Since File class has static methods, this wraps 
    /// the calls so that its client classes can be unit tested and desired dependencies can be injected. </summary>
    public class FileReadOnlyAccess : IFileReadonlyAccess
    {
        /// <summary> Reads all text from given file path. </summary>
        /// <param name="filePath">Full path to the file with extension (if any)</param>
        /// <returns>A string containing the text from the file.</returns>
        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}