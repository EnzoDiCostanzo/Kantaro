using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Enzo.Music.KantaroWpf.Models;

public class FileElement
{
    /// <summary>
    /// Nome del file senza il percorso
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Indica se il file contiene errori che non consentono la corretta lettura della Canzone
    /// </summary>
    public bool HasErrors { get; set; } = false;
    /// <summary>
    /// Indica se il file deve essere visualizzato o meno all'utente.
    /// </summary>
    public bool IsVisible { get; set; } = true;
    /// <summary>
    /// Proprietà utilizzata per nascondere il file. Corrisponde alla negazione della proprietà IsVisible.
    /// </summary>
    public bool IsHidden => !IsVisible;
    /// <summary>
    /// Indica l'elemento speciale che consente di risalire alla cartella precedente(indicato con "..").
    /// </summary>
    public bool IsPreviousFolder { get; set; } = false;
    /// <summary>
    /// Indica se il file è un contenitore (cartella o file .kantoj).
    /// </summary>
    public bool IsContainer => IsPreviousFolder || IsFolder || IsListOfFiles;
    /// <summary>
    /// Indica se il file è di tipo lista (estensione .kantoj).
    /// </summary>
    public bool IsListOfFiles { get; set; } = false;
    /// <summary>
    /// Indica se si tratta di una cartella del file system.
    /// </summary>
    public bool IsFolder { get; set; } = false;
    /// <summary>
    /// Percorso assoluto del file.
    /// </summary>
    public string? FilePath { get; set; }
    /// <summary>
    /// Indica se il file esiste o meno.
    /// </summary>
    public bool Exists { get; set; } = true;
    /// <summary>
    /// Negazione della proprietà Exists. Indica se il file non esiste.
    /// </summary>
    public bool NotExists => !Exists;
    /// <summary>
    /// Titolo della canzone. Per i file di tipo Container (cartelle o file .kantoj) il valore è null (Nothing in Visual Basic).
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Negazione della proprietà IsContainer.
    /// Il valore true indica che il file è di tipo Canzone.
    /// </summary>
    public bool IsNotContainer => !IsContainer;

    public static FileElement Empty
    {
        get
        {
            FileElement element = new FileElement()
            {
                Exists = false
            };
            return element;
        }
    }
}
