using Enzo.Music.KantaroWpf.Services;
using Enzo.Music.KantaroWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Enzo.Music.KantaroWpf.Services
{
    class DesignDataService : IDataService
    {
        public async Task CreateIndexAsync(string folderPath)
        {
            await Task.Run(() => null);
        }

        public Task<Canzone?> GetCanzoneFromFilePathAsync(string filePath)
        {
            return Task.FromResult(new Canzone { Titolo = "Canzone 1", Autore = "Autore 1" }??null);
        }

        public Task<string> GetCurrentFolderAsync()
        {
            return Task.FromResult(System.IO.Directory.GetCurrentDirectory());
        }

        public Task<List<FileElement>> GetFileElementsFromFolderAsync(string folderPath)
        {
            List<FileElement> ret = new() {
                new FileElement {FileName = "..", FilePath = "c:\\", IsFolder = true, IsPreviousFolder = true, Title = ".."},
                new FileElement {FileName = "Cartella d'esempio", FilePath = "c:\\temp\\folder1", IsFolder = true, Title = "folder1"},
                new FileElement {FileName = "kantoj1", FilePath = "c:\\temp\\file1.kantoj", IsListOfFiles = true, Title = "file1.kantoj"},
                new FileElement {FileName = "kanto1", FilePath = "c:\\temp\\file1.kanto", Title = "Alleluia (Toronto 2002)"},
                new FileElement {FileName = "kanto2", FilePath = "c:\\temp\\file2.kanto", Title = "Altra canzone della lista con nome molto lungo"},
                new FileElement {FileName = "kanto3", FilePath = "c:\\temp\\file3.kanto", Title = "Canzone con errori", HasErrors = true},
                new FileElement {FileName = "kanto4", FilePath = "c:\\temp\\file4.kanto", Title = "File mancante", Exists = false}
                };
            return Task.FromResult(ret);
        }
    }
}
