using System.Xml.Linq;

namespace Enzo.Music;
public class Canzoni
{
    public class CanzoniItem
    {
        public string FilePath;
        public string Title;

        internal CanzoniItem(string filePath, string title)
        {
            FilePath = filePath;
            Title = title;
        }
    }
    private readonly List<CanzoniItem> canzoni;
    public IEnumerable<CanzoniItem> Items => canzoni;

    public Canzoni()
    {
        canzoni = new List<CanzoniItem>();
    }

    public Canzoni(string fileKantoj) : this()
    {
        var xDoc = XDocument.Load(fileKantoj);
        canzoni.AddRange(FromXDocument(xDoc, Path.GetDirectoryName(fileKantoj)).Items);
    }

    public void Add(string pathCanzone, string title)
    {
        canzoni.Add(new CanzoniItem(pathCanzone, title));
    }

    public void Add(CanzoniItem item)
    {
        canzoni.Add(item);
    }

    /// <summary>
    /// Restituisce la classe Canzoni relativa all'xml del file kantoj
    /// </summary>
    /// <param name="document">Oggetto XDocument della lista di canzoni</param>
    /// <param name="basePath">Percorso del file system dove è contenuto il file d'origine</param>
    /// <returns></returns>
    public static Canzoni FromXDocument(XDocument document, string? basePath)
    {
        Canzoni kj = new();
        var canti = from k in document.Descendants("kanto")
                    let filePath = Path.Combine(basePath ?? string.Empty, k.Value)
                    select new CanzoniItem(filePath, k.Attribute("title")?.Value ?? string.Empty);
        kj.canzoni.AddRange(canti);
        return kj;
    }

    public XDocument ToXDocument()
    {
        XDocument doc = new(new XDeclaration("1.0", "utf-8", "yes"), new XElement("kantoj"));
        foreach (var c in Items)
        {
            XElement k = new("kanto") { Value = c.FilePath };
            if (!string.IsNullOrEmpty(c.Title))
                k.SetAttributeValue("title", c.Title);
            if (doc.Root?.LastNode is null)
                doc.Root?.AddFirst(k);
            else
                doc.Root.LastNode.AddAfterSelf(k);
        }
        return doc;
    }
}