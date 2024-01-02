using Enzo.Music.KantaroWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Enzo.Music.KantaroWpf.Services;

public interface IDataService
{
    Task<string> GetCurrentFolderAsync();
    Task<Canzone?> GetCanzoneFromFilePathAsync(string filePath);
    Task<List<FileElement>> GetFileElementsFromFolderAsync(string folderPath);
    Task CreateIndexAsync(string folderPath);
}
