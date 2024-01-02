using System.Diagnostics;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Enzo.Music;

[DebuggerDisplay("{GetText}")]
public class Nota : IAdditionOperators<Nota, Distanza, Nota>, IAdditionOperators<Nota, float, Nota>
    , ISubtractionOperators<Nota, Distanza, Nota>, ISubtractionOperators<Nota, float, Nota>
    , IEqualityOperators<Nota, Nota, bool>
{
    public NotaEnum Valore;

    public Nota(NotaEnum value)
    {
        if (value < NotaEnum.DO || value > NotaEnum.SI) throw new IndexOutOfRangeException();
        Valore = value;
    }

    public bool IsNaturale => new NotaEnum[] { NotaEnum.DO, NotaEnum.RE, NotaEnum.MI, NotaEnum.FA, NotaEnum.SOL, NotaEnum.LA, NotaEnum.SI }.Contains(Valore);

    #region Istanze statiche di tutte le note
    public static Nota DO => new(NotaEnum.DO);
    public static Nota DOdiesis => new(NotaEnum.DOdiesis);
    public static Nota REb => new(NotaEnum.DOdiesis);
    public static Nota RE => new(NotaEnum.RE);
    public static Nota REdiesis => new(NotaEnum.REdiesis);
    public static Nota MIb => new(NotaEnum.REdiesis);
    public static Nota MI => new(NotaEnum.MI);
    public static Nota FA => new(NotaEnum.FA);
    public static Nota FAdiesis => new(NotaEnum.FAdiesis);
    public static Nota SOLb => new(NotaEnum.FAdiesis);
    public static Nota SOL => new(NotaEnum.SOL);
    public static Nota SOLdiesis => new(NotaEnum.SOLdiesis);
    public static Nota LAb => new(NotaEnum.SOLdiesis);
    public static Nota LA => new(NotaEnum.LA);
    public static Nota LAdiesis => new(NotaEnum.LAdiesis);
    public static Nota SIb => new(NotaEnum.LAdiesis);
    public static Nota SI => new(NotaEnum.SI);
    #endregion

    #region Definizione degli Operatori

    public static Nota operator +(Nota left, Distanza right)
    {
        if (right.Valore < 0) return left - new Distanza { Valore = -1 * right.Valore };
        NotaEnum n = (NotaEnum)(((int)left.Valore + right.Semitoni) % 12); // le note vanno da 0 ad 11
        return new Nota(n);
    }

    public static Nota operator +(Nota left, float right)
    {
        return left + (Distanza)right;
    }

    public static Nota operator -(Nota left, Distanza right)
    {
        NotaEnum n = (NotaEnum)(((int)left.Valore + 12 - right.Semitoni) % 12); // le note vanno da 0 ad 11
        return new Nota(n);
    }

    public static Nota operator -(Nota left, float right)
    {
        return left - (Distanza)right;
    }

    public static bool operator ==(Nota? left, Nota? right)
    {
        if (left is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Nota? left, Nota? right)
    {
        if (left is null) return false;
        return !left.Equals(right);
    }
    #endregion

    private string GetText => Valore.ToString();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Nota) return false;
        return Valore.Equals(((Nota?)obj)?.Valore);
    }

    public static bool Equals(Nota? obj1, object? obj2)
    {
        if (obj1 == null || obj2 == null) return false;
        return obj1.Equals(obj2);
    }

    public override string ToString()
    {
        return ToString(false);
    }

    public string ToString(bool bemollePrefer)
    {
        var sb = new System.Text.StringBuilder();
        if (IsNaturale)
        {
            var txtNota = Valore.ToString();
            sb.Append(txtNota[..1].ToUpper());
            sb.Append(txtNota[1..].ToLower());
        }
        else
        {
            if (bemollePrefer)
            {
                Nota succ = this + Semitono.Value;
                sb.Append(succ.ToString()).Append('b');
            }
            else
            {
                Nota prec = this - Semitono.Value;
                sb.Append(prec.ToString()).Append('#');
            }
        }
        return sb.ToString();
    }

    public override int GetHashCode()
    {
        return Valore.GetHashCode();
    }

    public static bool TryParse(string value, out Nota? result)
    {
        NotaEnum n;
        string testo;
        if (value.EndsWith("b")) {
            if (!Enum.TryParse(value[..^1].ToUpper(), out n))
            {
                result = null;
                return false;
            }

            testo = string.Format("{0}", new Nota(n) - .5F).ToUpper();
        } else {
            testo = value.ToUpper();
        }
        if (!Enum.TryParse(testo.Replace("#", "diesis"), out n))
        {
            result = null;
            return false;
        }

        result = new Nota(n);
        return true;
    }
}
