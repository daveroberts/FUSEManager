using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using Dokan;

namespace FUSEManagerLib
{
    public class LoggingFS : DokanOperations
    {
        private DokanOperations _fileSystem;
        private bool _logging;

        public LoggingFS(DokanOperations oFS, bool logging)
        {
            this._fileSystem = oFS;
            this._logging = logging;
        }

        public int Cleanup(string filename, DokanFileInfo info)
        {
            int result = _fileSystem.Cleanup(filename, info);
            if (this._logging)
            {
                Console.WriteLine("Cleanup: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int CloseFile(string filename, DokanFileInfo info)
        {
            int result = _fileSystem.CloseFile(filename, info);
            if (this._logging)
            {
                Console.WriteLine("CloseFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int CreateDirectory(string filename, DokanFileInfo info)
        {
            int result = _fileSystem.CreateDirectory(filename, info);
            if (this._logging)
            {
                Console.WriteLine("CreateDirectory: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int CreateFile(string filename, FileAccess access, FileShare share, FileMode mode, FileOptions options, DokanFileInfo info)
        {
            int result = _fileSystem.CreateFile(filename, access, share, mode, options, info);
            if (this._logging)
            {
                Console.WriteLine("CreateFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int DeleteDirectory(string filename, DokanFileInfo info)
        {
            int result = _fileSystem.DeleteDirectory(filename, info);
            if (this._logging)
            {
                Console.WriteLine("DeleteDirectory: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int DeleteFile(string filename, DokanFileInfo info)
        {
            int result = _fileSystem.DeleteFile(filename, info);
            if (this._logging)
            {
                Console.WriteLine("DeleteFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int FindFiles(string filename, ArrayList files, DokanFileInfo info)
        {
            int result = _fileSystem.FindFiles(filename, files, info);
            if (this._logging)
            {
                Console.WriteLine("FindFiles: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int FlushFileBuffers(string filename, DokanFileInfo info)
        {
            int result = _fileSystem.FlushFileBuffers(filename, info);
            if (this._logging)
            {
                Console.WriteLine("FlushFileBuffers: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int GetDiskFreeSpace(ref ulong freeBytesAvailable, ref ulong totalBytes, ref ulong totalFreeBytes, DokanFileInfo info)
        {
            int result = _fileSystem.GetDiskFreeSpace(ref freeBytesAvailable, ref totalBytes, ref totalFreeBytes, info);
            if (this._logging)
            {
                Console.WriteLine("GetDiskFreeSpace");
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int GetFileInformation(string filename, FileInformation fileinfo, DokanFileInfo info)
        {
            int result = _fileSystem.GetFileInformation(filename, fileinfo, info);
            if (this._logging)
            {
                Console.WriteLine("GetFileInformation: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int LockFile(string filename, long offset, long length, DokanFileInfo info)
        {
            int result = _fileSystem.LockFile(filename, offset, length, info);
            if (this._logging)
            {
                Console.WriteLine("LockFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int MoveFile(string filename, string newname, bool replace, DokanFileInfo info)
        {
            int result = _fileSystem.MoveFile(filename, newname, replace, info);
            if (this._logging)
            {
                Console.WriteLine("MoveFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int OpenDirectory(string filename, DokanFileInfo info)
        {
            int result = _fileSystem.OpenDirectory(filename, info);
            if (this._logging)
            {
                Console.WriteLine("OpenDirectory: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int ReadFile(string filename, byte[] buffer, ref uint readBytes, long offset, DokanFileInfo info)
        {
            int result = _fileSystem.ReadFile(filename, buffer, ref readBytes, offset, info);
            if (this._logging)
            {
                Console.WriteLine("ReadFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int SetAllocationSize(string filename, long length, DokanFileInfo info)
        {
            int result = _fileSystem.SetAllocationSize(filename, length, info);
            if (this._logging)
            {
                Console.WriteLine("SetAllocationSize: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int SetEndOfFile(string filename, long length, DokanFileInfo info)
        {
            int result = _fileSystem.SetEndOfFile(filename, length, info);
            if (this._logging)
            {
                Console.WriteLine("SetEndOfFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int SetFileAttributes(string filename, FileAttributes attr, DokanFileInfo info)
        {
            int result = _fileSystem.SetFileAttributes(filename, attr, info);
            if (this._logging)
            {
                Console.WriteLine("SetFileAttributes: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int SetFileTime(string filename, DateTime ctime, DateTime atime, DateTime mtime, DokanFileInfo info)
        {
            int result = _fileSystem.SetFileTime(filename, ctime, atime, mtime, info);
            if (this._logging)
            {
                Console.WriteLine("SetFileTime: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int UnlockFile(string filename, long offset, long length, DokanFileInfo info)
        {
            int result = _fileSystem.UnlockFile(filename, offset, length, info);
            if (this._logging)
            {
                Console.WriteLine("UnlockFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int Unmount(DokanFileInfo info)
        {
            int result = _fileSystem.Unmount(info);
            if (this._logging)
            {
                Console.WriteLine("Unmount");
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
        public int WriteFile(string filename, byte[] buffer, ref uint writtenBytes, long offset, DokanFileInfo info)
        {
            int result = _fileSystem.WriteFile(filename, buffer, ref writtenBytes, offset, info);
            if (this._logging)
            {
                Console.WriteLine("WriteFile: " + filename);
                Console.WriteLine("Result: " + result);
            }
            return result;
        }
    }
}
