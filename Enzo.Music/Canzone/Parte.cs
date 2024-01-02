using System.Numerics;

namespace Enzo.Music;

public class Parte : ICloneable, IEquatable<Parte>, IEqualityOperators<Parte, Parte, bool>
{
    public Accordo? Accordo;
    public string? Testo;

    public object Clone()
    {
        Parte p = new();
        if (Accordo != null) p.Accordo = Accordo.Clone() as Accordo;
        if (Testo != null) p.Testo = Testo;
        return p;
    }

    public bool Equals(Parte? other)
    {
        if (other == null) return false;
        bool uguali = String.Equals(Testo, other.Testo) && Accordo.Equals(Accordo, other.Accordo);
        return uguali;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not Parte) return false;
        return Equals((Parte)obj);
    }

    public static bool Equals(Parte? a, Parte? b)
    {
        if (a == null || b == null) return false;
        return a.Equals(b);
    }

    public static new bool Equals(object? a, object? b)
    {
        if (a == null || b == null) return false;
        if (!(a is Parte && b is Parte)) return false;
        return ((Parte)a).Equals(b);
    }

    public override int GetHashCode()
    {
        return (Testo ?? string.Empty).GetHashCode();
    }

    public static bool operator ==(Parte? left, Parte? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Parte? left, Parte? right)
    {
        return !Equals(left, right);
    }
}
