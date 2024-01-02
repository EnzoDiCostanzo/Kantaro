using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enzo.Music;

public class Tono : Distanza
{
    public Tono()
    {
        Valore = 1F;
    }

    public static Tono Value { get; } = new();
}
