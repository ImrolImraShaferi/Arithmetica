using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testing.Services
{
    public interface IFileSystem
    {
        Task<string> GetExternalStorage();
    }
}
