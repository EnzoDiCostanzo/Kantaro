using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Enzo.Music;

public class Scala : IEqualityOperators<Scala, Scala, bool>
{
    public Nota NotaFondamentale { get; }

    public ModoBase _Modo;
    public ModoBase Modo => _Modo;

    public Scala(Nota notaFondamentale, ModoBase modo)
    {
        ArgumentNullException.ThrowIfNull(notaFondamentale, nameof(notaFondamentale));
        ArgumentNullException.ThrowIfNull(modo, nameof(modo));
        NotaFondamentale = notaFondamentale;
        _Modo = modo;
    }
    public Nota this[int index]
    {
        get
        {
            var incrementi = index % Modo.NumeroSuccessioni;
            Nota r = NotaFondamentale;
            for (int i = 0; i <= incrementi - 1; i++)
                if (!(Modo.Successioni?[i] is null)) r += Modo.Successioni[i];
            return r;
        }
    }

    #region Verifica uguaglianze
    public static bool operator ==(Scala? left, Scala? right)
    {
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Scala? left, Scala? right)
    {
        if (left is null || right is null) return false;
        return !left.Equals(right);
    }
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is not Scala) return false;
        Scala? s = obj as Scala;
        bool bNota = s?.NotaFondamentale?.Equals(NotaFondamentale) ?? false;
        bool bModo = s?.Modo?.Equals(s.Modo) ?? false;
        return bNota && bModo;
    }
    public static bool Equals(Scala? left, Scala? right)
    {
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    #endregion

    public override string ToString()
    {
        return ToString(false);
    }
    public string ToString(bool bemollePrefer)
    {
        return NotaFondamentale.ToString(bemollePrefer);
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public static bool TryParse(string value, out Scala? result)
    {
        ModoBase? modo;
        if (value.Length > 3 && value.EndsWith("dim", StringComparison.CurrentCultureIgnoreCase))
            value = value[..^3];

        if (!Nota.TryParse(value.TrimEnd("m-+0123456789".ToCharArray()), out Nota? nb))
        {
            result = null;
            return false;
        }

        System.Text.RegularExpressions.Regex regExNum = new("[0-9]+\\+*");
        var accordoBase = regExNum.Replace(value, string.Empty);
        if (!Nota.TryParse(accordoBase, out Nota? na))
        {
            result = null;
            //return false;
        }

        if (Nota.Equals(nb, na))
            modo = ModoMaggiore.GetInstance();
        else
            modo = ModoMinoreArmonica.GetInstance();

        result = nb is null ? null : new Scala(nb, modo);
        return true;
    }
}
