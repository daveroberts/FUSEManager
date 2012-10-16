using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;
using Dokan;

namespace FUSEManagerLib
{
    class DoubleMirror : DokanOperations
    {
        private string root_;
        private int count_;
        public DoubleMirror(string root)
        {
            root_ = root;
            count_ = 1;
        }

        private string GetPath(string filename)
        {
            string path = root_ + filename;
            return path;
        }

        public int CreateFile(String filename, FileAccess access, FileShare share,
            FileMode mode, FileOptions options, DokanFileInfo info)
        {
            string path = GetPath(filename);
            info.Context = count_++;
            // Specifies that the operating system should create a new file. This requires FileIOPermissionAccess.Write. If the file already exists, an IOException is thrown.
            if (mode == FileMode.CreateNew)
            {
                if (File.Exists(path))
                {
                    return -DokanNet.ERROR_FILE_EXISTS;
                }
                FileInfo f = new FileInfo(path);
                FileStream s = f.Open(FileMode.CreateNew);
                return 0;
            }
            // Specifies that the operating system should create a new file. If the file already exists, it will be overwritten.
            // This requires FileIOPermissionAccess.Write. System.IO.FileMode.Create is equivalent to requesting that if the file does not exist
            // use CreateNew; otherwise, use Truncate. If the file already exists but is a hidden file, an UnauthorizedAccessException is thrown.
            else if (mode == FileMode.Create)
            {
                FileInfo f = new FileInfo(path);
                FileStream s = f.Open(FileMode.Create);
                return 0;
            }
            // Specifies that the operating system should open an existing file. The ability to open the file is dependent on the value specified by FileAccess.
            // A System.IO.FileNotFoundException is thrown if the file does not exist.
            else if (mode == FileMode.Open)
            {
                if (File.Exists(path))
                {
                    try
                    {
                        FileInfo f = new FileInfo(path);
                        FileStream s = f.Open(FileMode.Open);
                    }
                    catch
                    {
                        return -DokanNet.ERROR_ACCESS_DENIED;
                    }
                    return 0;
                }
                else if (Directory.Exists(path))
                {
                    info.IsDirectory = true;
                    return 0;
                }
                else
                {
                    return -DokanNet.ERROR_FILE_NOT_FOUND;
                }
            }
            // Specifies that the operating system should open a file if it exists; otherwise, a new file should be created
            // If the file is opened with FileAccess.Read, FileIOPermissionAccess.Read is required
            // If the file access is FileAccess.Write then FileIOPermissionAccess.Write is required. If the file is opened with FileAccess.ReadWrite,
            // both FileIOPermissionAccess.Read and FileIOPermissionAccess.Write are required. If the file access is FileAccess.Append,
            // then FileIOPermissionAccess.Append is required.
            else if (mode == FileMode.OpenOrCreate)
            {
                if (File.Exists(path))
                {
                    return 0;
                }
                else if (Directory.Exists(path))
                {
                    info.IsDirectory = true;
                    return 0;
                }
                else
                {
                    FileInfo f = new FileInfo(path);
                    FileStream s = f.Open(FileMode.OpenOrCreate);
                    return 0;
                }
            }
            //Specifies that the operating system should open an existing file. Once opened, the file should be truncated so that its size is zero bytes.
            // This requires FileIOPermissionAccess.Write. Attempts to read from a file opened with Truncate cause an exception.
            else if (mode == FileMode.Truncate)
            {
                FileInfo f = new FileInfo(path);
                FileStream s = f.Open(FileMode.Truncate);
                return 0;
            }
            else if (mode == FileMode.Append)
            {
                FileInfo f = new FileInfo(path);
                FileStream s = f.Open(FileMode.Append);
                return 0;
            }
            else
            {
                return -DokanNet.DOKAN_ERROR;
            }
        }

        public int OpenDirectory(String filename, DokanFileInfo info)
        {
            info.Context = count_++;
            if (Directory.Exists(GetPath(filename)))
                return 0;
            else
                return -DokanNet.ERROR_PATH_NOT_FOUND;
        }

        public int CreateDirectory(String filename, DokanFileInfo info)
        {
            string path = GetPath(filename);
            DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(path);
            return 0;
        }

        public int Cleanup(String filename, DokanFileInfo info)
        {
            //Console.WriteLine("%%%%%% count = {0}", info.Context);
            return 0;
        }

        public int CloseFile(String filename, DokanFileInfo info)
        {
            return 0;
        }

        public int ReadFile(String filename, Byte[] buffer, ref uint readBytes,
            long offset, DokanFileInfo info)
        {
            try
            {
                FileStream fs = File.OpenRead(GetPath(filename));
                fs.Seek(offset, SeekOrigin.Begin);
                readBytes = (uint)fs.Read(buffer, 0, buffer.Length);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int WriteFile(String filename, Byte[] buffer,
            ref uint writtenBytes, long offset, DokanFileInfo info)
        {
            return -1;
        }

        public int FlushFileBuffers(String filename, DokanFileInfo info)
        {
            return -1;
        }

        public int GetFileInformation(String filename, FileInformation fileinfo, DokanFileInfo info)
        {
            string path = GetPath(filename);
            if (File.Exists(path))
            {
                FileInfo f = new FileInfo(path);

                fileinfo.Attributes = f.Attributes;
                fileinfo.CreationTime = f.CreationTime;
                fileinfo.LastAccessTime = f.LastAccessTime;
                fileinfo.LastWriteTime = f.LastWriteTime;
                fileinfo.Length = f.Length;
                return 0;
            }
            else if (Directory.Exists(path))
            {
                DirectoryInfo f = new DirectoryInfo(path);

                fileinfo.Attributes = f.Attributes;
                fileinfo.CreationTime = f.CreationTime;
                fileinfo.LastAccessTime = f.LastAccessTime;
                fileinfo.LastWriteTime = f.LastWriteTime;
                fileinfo.Length = 0;// f.Length;
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int FindFiles(String filename, ArrayList files, DokanFileInfo info)
        {
            string path = GetPath(filename);
            if (Directory.Exists(path))
            {
                DirectoryInfo d = new DirectoryInfo(path);
                FileSystemInfo[] entries = d.GetFileSystemInfos();
                foreach (FileSystemInfo f in entries)
                {
                    FileInformation fi = new FileInformation();
                    fi.Attributes = f.Attributes;
                    fi.CreationTime = f.CreationTime;
                    fi.LastAccessTime = f.LastAccessTime;
                    fi.LastWriteTime = f.LastWriteTime;
                    fi.Length = (f is DirectoryInfo) ? 0 : ((FileInfo)f).Length;
                    fi.FileName = f.Name;
                    files.Add(fi);
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int SetFileAttributes(String filename, FileAttributes attr, DokanFileInfo info)
        {
            return -1;
        }

        public int SetFileTime(String filename, DateTime ctime,
                DateTime atime, DateTime mtime, DokanFileInfo info)
        {
            return -1;
        }

        public int DeleteFile(String filename, DokanFileInfo info)
        {
            return -1;
        }

        public int DeleteDirectory(String filename, DokanFileInfo info)
        {
            return -1;
        }

        public int MoveFile(String filename, String newname, bool replace, DokanFileInfo info)
        {
            return -1;
        }

        public int SetEndOfFile(String filename, long length, DokanFileInfo info)
        {
            return -1;
        }

        public int SetAllocationSize(String filename, long length, DokanFileInfo info)
        {
            return -1;
        }

        public int LockFile(String filename, long offset, long length, DokanFileInfo info)
        {
            return 0;
        }

        public int UnlockFile(String filename, long offset, long length, DokanFileInfo info)
        {
            return 0;
        }

        public int GetDiskFreeSpace(ref ulong freeBytesAvailable, ref ulong totalBytes,
            ref ulong totalFreeBytes, DokanFileInfo info)
        {
            freeBytesAvailable = 512 * 1024 * 1024;
            totalBytes = 1024 * 1024 * 1024;
            totalFreeBytes = 512 * 1024 * 1024;
            return 0;
        }

        public int Unmount(DokanFileInfo info)
        {
            return 0;
        }
    }
}
