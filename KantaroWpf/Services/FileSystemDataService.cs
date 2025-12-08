using Enzo.Music;
using Enzo.Music.KantaroWpf.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace Enzo.Music.KantaroWpf.Services;

class FileSystemDataService : IDataService
{

    private const string INDEX_FILE_NAME = "index.kantoj";

    public async Task CreateIndexAsync(string folderPath)
    {
        string folderFullPath = Path.IsPathRooted(folderPath) ? folderPath : Path.GetFullPath(folderPath);
        string fileName = Path.Combine(folderFullPath, INDEX_FILE_NAME);
        var canzoni = new Dictionary<string, Canzone?>();
        if (!Path.Exists(folderFullPath)) return;
        var kantoFiles = Directory.GetFiles(folderFullPath, "*.kanto");
        foreach (string f in kantoFiles)
            canzoni.Add(f, await GetCanzoneFromFilePathAsync(f));
        if (canzoni.Count == 0 && File.Exists(fileName))
            await Task.Run(() =>
            {
                try
                {
                    if(File.GetAttributes(fileName).HasFlag(FileAttributes.ReadOnly))
                        File.SetAttributes(fileName, FileAttributes.Normal);
                    File.Delete(fileName);
                }
                catch
                {
                }
            });
        else
        {
            Canzoni canti = new();
            foreach (var dicElem in canzoni)
                canti.Add(dicElem.Key, dicElem.Value?.Titolo ?? String.Empty);
            if (canti.Items.Any())
            {
                var x = canti.ToXDocument();
                await Task.Run(() =>
                {
                    using var wr = XmlWriter.Create(fileName);
                    x.WriteTo(wr);
                });
            }
        }
    }

    public async Task<Canzone?> GetCanzoneFromFilePathAsync(string filePath)
    {
        try
        {
            if (!Path.IsPathRooted(filePath)) filePath = Path.GetFullPath(filePath);
            if (string.IsNullOrEmpty(filePath) || !Path.Exists(filePath))
                return null;
            return await Canzone.FromStreamAsync(new StreamReader(filePath));
        }
        catch
        {
            return null;
        }
        ;
    }

    public async Task<string> GetCurrentFolderAsync()
    {
        var curDir = Directory.GetCurrentDirectory();
        if (!Path.IsPathRooted(curDir)) curDir = Path.GetFullPath(curDir);
        return await Task.FromResult(curDir);
    }

    public async Task<List<FileElement>> GetFileElementsFromFolderAsync(string folderPath)
    {
        if (!string.IsNullOrWhiteSpace(folderPath) && !Path.IsPathRooted(folderPath)) folderPath = Path.GetFullPath(folderPath);
        var ret = new List<FileElement>();
        if (folderPath is null) return ret;
        if (Path.GetPathRoot(folderPath) != folderPath)
            ret.Add(new FileElement { FileName = "..", IsFolder = true, IsPreviousFolder = true, FilePath = folderPath, Title = ".." });
        if (Path.GetExtension(folderPath) == ".kantoj")
        {
            ret.AddRange(await GetFileElementsFromKantojAsync(folderPath));
            return ret;
        }
        else
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    foreach (string f in Directory.GetDirectories(folderPath))
                    {
                        var fn = Path.GetFileName(f);
                        var e = new FileElement { FileName = fn, Title = fn, FilePath = f, IsFolder = true, Exists = true };
                        ret.Add(e);
                    }
                    bool trovatoIndexFile = false;
                    foreach (string f in Directory.GetFiles(folderPath, "*.kantoj"))
                    {
                        if (Path.GetFileName(f) == INDEX_FILE_NAME)
                        {
                            trovatoIndexFile = true;
                            continue;
                        }
                        string fn = Path.GetFileNameWithoutExtension(f);
                        var e = new FileElement { FileName = fn, Title = fn, FilePath = f, IsListOfFiles = true, Exists = true };
                        ret.Add(e);
                    }
                    string indexFilePath = Path.Combine(folderPath, INDEX_FILE_NAME);
                    var kantoFiles = Directory.GetFiles(folderPath, "*.kanto");
                    if (trovatoIndexFile && kantoFiles.Length != 0)
                    {
                        var kantoFileElements = await GetFileElementsFromKantojAsync(indexFilePath);
                        var lastChange = kantoFiles.Max(File.GetLastWriteTimeUtc);
                        if (kantoFileElements.Count() != kantoFiles.Length || lastChange > File.GetLastWriteTimeUtc(indexFilePath))
                        {
                            await CreateIndexAsync(folderPath);
                            kantoFileElements = await GetFileElementsFromKantojAsync(indexFilePath);
                        }
                        ret.AddRange(kantoFileElements.OrderBy((e) => e.Title));
                    }
                    else
                    {
                        ret.AddRange(GetFileElementsFromKantoFiles(kantoFiles).OrderBy((e) => e.Title));
                        await CreateIndexAsync(folderPath);
                    }
                }
            }
            catch //(Exception ex)
            {

            }
            return ret;
        }
    }

    private static async Task<IEnumerable<FileElement>> GetFileElementsFromKantojAsync(string kantojFilePath)
    {
        if (!Path.IsPathRooted(kantojFilePath)) kantojFilePath = Path.GetFullPath(kantojFilePath);
        IEnumerable<FileElement>? ret = null;
        await Task.Run(() =>
        {
            var xDoc = XDocument.Load(kantojFilePath);
            var dirPath = Path.GetDirectoryName(kantojFilePath) ?? string.Empty;
            var kantoFiles = from k in xDoc.Descendants("kanto")
                             select Path.Combine(dirPath, k.Value);
            ret = GetFileElementsFromKantoFiles(kantoFiles);
        });
        ret ??= [];
        return [.. ret];
    }

    private static IEnumerable<FileElement> GetFileElementsFromKantoFiles(IEnumerable<string> kantoFiles)
    {
        var ret = new List<FileElement>();
        var canti = from fn in kantoFiles
                    select new FileElement
                    {
                        FilePath = fn,
                        FileName = Path.GetFileNameWithoutExtension(fn),
                        Exists = System.IO.File.Exists(fn)
                    };
        Parallel.ForEach(canti, (c) =>
        {
            if (c.Exists && c.FilePath is not null)
                try
                {
                    var xSong = XDocument.Load(c.FilePath);
                    var sb = new StringBuilder();
                    sb.Append(xSong.Element("canzone")?.Attribute("title")?.Value);
                    if (xSong.Element("canzone")?.Attribute("autore") is not null)
                        sb.Append($" ({xSong.Element("canzone")?.Attribute("autore")?.Value})");
                    c.Title = sb.ToString();
                }
                catch //(Exception ex)
                {
                    c.Title = c.FileName;
                    c.HasErrors = true;
                }
            else
                c.Title = c.FileName;
            ret.Add(c);
        });
        try
        {
            Parallel.ForEach(ret, c => Task.Run(() =>
            {
                try
                {
                    if (c?.FilePath is not null && Canzone.FromXDocument(XDocument.Load(c.FilePath)) is null)
                        c.HasErrors = true;
                }
                catch //(Exception ex)
                {
                    c.HasErrors = true;
                }
            }));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error");
        }
        return ret;
    }
}
