Public Class FileElement
    ''' <summary>
    ''' Nome del file senza il percorso
    ''' </summary>
    ''' <returns></returns>
    Public Property FileName As String

    ''' <summary>
    ''' Indica se il file contiene errori che non consentono la corretta lettura della Canzone
    ''' </summary>
    ''' <returns></returns>
    Public Property HasErrors As Boolean = False

    ''' <summary>
    ''' Indica se il file deve essere visualizzato o meno all'utente.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsVisible As Boolean = True

    ''' <summary>
    ''' Proprietà utilizzata per nascondere il file. Corrisponde alla negazione della proprietà IsVisible.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsHidden As Boolean
        Get
            Return Not IsVisible
        End Get
    End Property

    ''' <summary>
    ''' Indica l'elemento speciale che consente di risalire alla cartella precedente (indicato con "..").
    ''' </summary>
    ''' <returns></returns>
    Public Property IsPreviousFolder As Boolean = False

    ''' <summary>
    ''' Indica se il file è un contenitore (cartella o file .kantoj).
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsContainer As Boolean
        Get
            Return IsPreviousFolder OrElse IsFolder OrElse IsListOfFiles
        End Get
    End Property

    ''' <summary>
    ''' Indica se il file è di tipo lista (estensione .kantoj).
    ''' </summary>
    ''' <returns></returns>
    Public Property IsListOfFiles As Boolean = False

    ''' <summary>
    ''' Indica se si tratta di una cartella del file system.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsFolder As Boolean = False

    ''' <summary>
    ''' Percorso assoluto del file.
    ''' </summary>
    ''' <returns></returns>
    Public Property FilePath As String

    ''' <summary>
    ''' Indica se il file esiste o meno.
    ''' </summary>
    ''' <returns></returns>
    Public Property Exists As Boolean = True

    ''' <summary>
    ''' Negazione della proprietà Exists. Indica se il file non esiste.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NotExists As Boolean
        Get
            Return Not Exists
        End Get
    End Property

    ''' <summary>
    ''' Titolo della canzone. Per i file di tipo Container (cartelle o file .kantoj) il valore è null (Nothing in Visual Basic).
    ''' </summary>
    ''' <returns></returns>
    Public Property Title As String

    ''' <summary>
    ''' Negazione della proprietà IsContainer.
    ''' True indica che il file è di tipo Canzone.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsNotContainer As Boolean
        Get
            Return Not IsContainer
        End Get
    End Property
End Class
