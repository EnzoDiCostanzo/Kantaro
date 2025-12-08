using System.Diagnostics;
using System.Numerics;

namespace Enzo.Music;

[DebuggerDisplay("{GetText}")]
public class Distanza : IAdditionOperators<Distanza, Distanza, Distanza>, IAdditionOperators<Distanza, int, Distanza>, IAdditionOperators<Distanza, double, Distanza>
    , IMultiplyOperators<Distanza, int, Distanza>
    , IEqualityOperators<Distanza, Distanza, bool>
{
    /// <summary>
    /// Valore che indica la distanza in Toni
    /// </summary>
    public float Toni => (float)Semitoni/2;

    /// <summary>
    /// Valore che indica la distanza in semitoni
    /// </summary>
    public int Semitoni { get; set; }

    /// <summary>
    /// Rappresenta il valore della distanza espresso in Toni
    /// </summary>
    public float Valore
    {
        get => (float)Semitoni / 2;
        set
        {
            if (2 * value % 1 != 0) // Consente la conversione solo per i valori interi o con il ".5"
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Impossibile convertire il valore in Toni e Semitoni");
            }
            Semitoni =(int)(value * 2);
        }
    }

    private string GetText => Toni.ToString();

    public static Distanza operator +(Distanza left, Distanza right)
    {
        return new Distanza() { Valore = left.Valore + right.Valore };
    }

    public static Distanza operator +(Distanza left, int right)
    {
        return new Distanza() { Valore = left.Valore + right };
    }

    public static Distanza operator +(Distanza left, double right)
    {
        return new Distanza() { Valore = left.Valore + (float)right };
    }

    public static Distanza operator *(Distanza left, int right)
    {
        return new Distanza() { Valore = left.Valore * right };
    }
    public static Distanza operator *(int left, Distanza right)
    {
        return right * left;
    }

    public static bool operator ==(Distanza? left, Distanza? right)
    {
        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(Distanza? left, Distanza? right)
    {
        return !(left==right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Distanza) return false;
        return (obj as Distanza)?.Valore.Equals(Valore) ?? false;
    }
    public static bool Equals(object obj, Distanza? other)
    {
        if (obj is null || obj is not Distanza) return false;
        return (obj as Distanza)?.Equals(other) ?? false;
    }

    public static explicit operator Distanza(double initialData)
    {
        return (Distanza)Convert.ToSingle(initialData);
    }

    public static explicit operator Distanza(int initialData)
    {
        return (Distanza)Convert.ToSingle(initialData);
    }

    public static explicit operator Distanza(float initialData)
    {
        var r = new Distanza();
        try
        {
            r.Valore = initialData;
        }
        catch //(ArgumentOutOfRangeException ex)
        {
            throw new InvalidCastException("Impossibile convertire il valore in un oggetto di tipo 'Distanza'");
        }
        return r;
    }

    public static implicit operator float(Distanza initialData)
    {
        return initialData.Valore;
    }

    public static explicit operator Distanza(string initialData)
    {
        return (Distanza)float.Parse(initialData);
    }

    public static implicit operator string(Distanza initialData)
    {
        return initialData.Valore.ToString();
    }

    public override string ToString()
    {
        return Valore.ToString();
    }

    public override int GetHashCode()
    {
        return Semitoni.GetHashCode();
    }
}
