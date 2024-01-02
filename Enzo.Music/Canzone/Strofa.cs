﻿using System.Numerics;

namespace Enzo.Music;

public class Strofa : ICloneable, IEquatable<Strofa>, IEqualityOperators<Strofa, Strofa, bool>
{
    public string? Nome;
    public List<Parte?> Parti = new();

    protected void CopyFrom(Strofa strofa)
    {
        Nome = strofa.Nome;
        foreach (Parte? p in strofa.Parti)
        {
            Parti.Add((Parte?)p?.Clone());
        }
    }

    public virtual object Clone()
    {
        Strofa n = new();
        n.CopyFrom(this);
        return n;
    }

    public bool Equals(Strofa? other)
    {
        if (other == null) return false;
        bool uguali = string.Equals(Nome, other.Nome) && Parti.Count == other.Parti.Count;
        if (uguali)
        {
            for (int i = 0; i < Parti.Count; i++)
            {
                uguali = Parti[i]?.Equals(other.Parti[i]) ?? false;
                if (!uguali) break;
            }
        }
        return uguali;
    }
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is not Strofa) return false;

        return Equals((Strofa)obj);
    }
    public static new bool Equals(object? obj1, object? obj2) {
        if (!(obj1 != null && obj2 != null)) return false;
        if (obj1 is not Strofa) return false;
        return ((Strofa)obj1).Equals(obj2);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Strofa? left, Strofa? right)
    {
        return Strofa.Equals(left, right);
    }

    public static bool operator !=(Strofa? left, Strofa? right)
    {
        return !Strofa.Equals(left, right);
    }
}
