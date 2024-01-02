using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enzo.Music;

public abstract class ModoBase
{
    protected ReadOnlyCollection<Distanza>? successioni;
    public ReadOnlyCollection<Distanza>? Successioni { get => successioni; }

    public int NumeroSuccessioni => Successioni?.Count ?? 0;

    protected void CreaSuccessioneDistanze(IList<Distanza> lista)
    {
        successioni = new ReadOnlyCollection<Distanza>(lista);
    }
}
