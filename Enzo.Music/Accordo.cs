using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Enzo.Music;

public class Accordo : ICloneable, IAdditionOperators<Accordo, Distanza, Accordo>, ISubtractionOperators<Accordo, Distanza, Accordo>
    , IEqualityOperators<Accordo, Accordo, bool>
{


    private struct FormaPreferita(Scala scala, string testo)
    {
        public Scala Scala = scala;
        public string Testo = testo;
    }

    public class EstensioneT
    {
        public enum EstensioneVariazioneSemitonoEnum
        {
            None = 0,
            Diminuito = 1,
            Aumentato = 2
        }

        public int Valore;
        public EstensioneVariazioneSemitonoEnum VariazioneSemitono;

        public static EstensioneT Empty => new();

        public bool IsEmpty => Valore == 0;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not EstensioneT) return false;
            EstensioneT o = obj as EstensioneT ?? Empty;
            return Valore.Equals(o.Valore) && VariazioneSemitono.Equals(o.VariazioneSemitono);
        }
        public override string ToString()
        {
            System.Text.StringBuilder sb = new();
            if (Valore > 0) sb.Append(Valore);
            if (VariazioneSemitono == EstensioneVariazioneSemitonoEnum.Aumentato) sb.Append('+');
            if (VariazioneSemitono == EstensioneVariazioneSemitonoEnum.Diminuito) sb.Append("dim");
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        //public static explicit operator EstensioneT(int value)
        //{
        //    return new EstensioneT { Valore = value };
        //}

        public static implicit operator EstensioneT(int value)
        {
            TryParse(value, out EstensioneT ext);
            return ext;
        }

        public static bool TryParse(string value, out EstensioneT? result)
        {
            ArgumentNullException.ThrowIfNull(value);
            EstensioneT r = new();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var regEst = new Regex("^[0-9]+");
                if (!regEst.IsMatch(value))
                {
                    result = null;
                    return false;
                }

                r.Valore = int.Parse(regEst.Match(value).Value);
                r.VariazioneSemitono = value[regEst.Match(value).Value.Length..] switch
                {
                    "+" => EstensioneVariazioneSemitonoEnum.Aumentato,
                    "dim" => EstensioneVariazioneSemitonoEnum.Diminuito,
                    _ => EstensioneVariazioneSemitonoEnum.None,
                };
            }
            result = r;
            return true;
        }

        public static bool TryParse(int value, out EstensioneT result)
        {
            EstensioneT r = new()
            {
                Valore = value,
                VariazioneSemitono = EstensioneVariazioneSemitonoEnum.None
            };
            result = r;
            return true;
        }

        internal EstensioneT() { }

        public EstensioneT(int valore, EstensioneVariazioneSemitonoEnum varSemitono)
        {
            Valore = valore;
            VariazioneSemitono = varSemitono;
        }
    }

    private readonly Scala scala;

    public Accordo(Scala scala) : this(scala, EstensioneT.Empty)
    {
    }

    private Accordo(Scala scala, string testoPreferito) : this(scala)
    {
    }

    public Accordo(Scala scala, EstensioneT estensione)
    {
        this.scala = scala;
        this.Estensione = estensione;
    }

    public Accordo(Scala scala, int num) : this(scala, num, EstensioneT.EstensioneVariazioneSemitonoEnum.None)
    {
    }

    public Accordo(Scala scala, int num, EstensioneT.EstensioneVariazioneSemitonoEnum varEstensione)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(num);
        this.scala = scala;
        Estensione = new() { Valore = num, VariazioneSemitono = varEstensione };
    }

    public Accordo(Scala scala, int num, EstensioneT.EstensioneVariazioneSemitonoEnum varEstensione, Nota basso) : this(scala, num, varEstensione)
    {
        Basso = basso;
    }
    public Accordo(Scala scala, EstensioneT estensione, Nota basso) : this(scala, estensione)
    {
        Basso = basso;
    }

    public Scala Scala => scala;
    public Nota? Basso { get; set; }
    public EstensioneT Estensione { get; set; }

    #region Definizione degli Operatori
    public static Accordo operator +(Accordo left, Distanza right)
    {
        if (right.Semitoni == 0) return left;
        Accordo n = new(new Scala(left.Scala.NotaFondamentale + right, left.Scala.Modo), left.Estensione);
        if (left.Basso is not null) n.Basso = left.Basso + right;

        return n;
    }

    public static Accordo operator -(Accordo left, Distanza right)
    {
        if (right.Semitoni == 0) return left;
        Accordo n = new(new Scala(left.Scala.NotaFondamentale - right, left.Scala.Modo), left.Estensione);
        if (left.Basso is not null) n.Basso = left.Basso - right;

        return n;
    }

    public static bool operator ==(Accordo? left, Accordo? right)
    {
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Accordo? left, Accordo? right)
    {
        if (left is null || right is null) return false;
        return !left.Equals(right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Accordo) return false;
        Accordo a = (Accordo)obj;
        bool bScala = a.Scala.Equals(Scala);
        bool bBasso = Basso is null && a.Basso is null ||
                      Basso is not null && a.Basso is not null && Basso.Equals(a.Basso);
        bool bAlter = Estensione is null && a.Estensione is null ||
                      Estensione is not null && a.Estensione is not null && Estensione.Equals(a.Estensione);
        return bScala && bAlter && bBasso && bAlter;
    }

    public static bool Equals(Accordo? left, Accordo? right)
    {
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    #endregion

    public static bool TryParse(string value, out Accordo? result)
    {
        ArgumentNullException.ThrowIfNull(value);
        var parti = value.Split("/");
        if (!Scala.TryParse(parti[0], out Scala? sc)) throw new InvalidCastException($"Testo non convertibile in Accordo: {parti[0]}");
        if (sc is null) throw new InvalidCastException();
        Accordo acc = new(sc, value);
        if (parti[0][sc.ToString().Length..].Length > 0)
        {
            string temp = parti[0][sc.ToString().Length..];
            EstensioneT ext = new();
            string num = new([.. temp.ToCharArray().TakeWhile(char.IsDigit)]);
            if (int.TryParse(num, out int valNumb)) ext.Valore = valNumb;
            if (num != temp)
            {
                ext.VariazioneSemitono = temp[num.Length..] switch
                {
                    "+" => EstensioneT.EstensioneVariazioneSemitonoEnum.Aumentato,
                    "dim" => EstensioneT.EstensioneVariazioneSemitonoEnum.Diminuito,
                    _ => EstensioneT.EstensioneVariazioneSemitonoEnum.None,
                };
            }
            acc.Estensione = ext;
        }
        if (parti.Length > 1)
        {
            if (parti.Length != 2 || !Nota.TryParse(parti[1], out Nota? basso)) throw new InvalidCastException($"Testo non convertibile in Accordo: {parti[1]}");
            acc.Basso = basso;
        }
        result = acc;
        return true;
    }

    public static explicit operator Accordo?(string value)
    {
        _ = TryParse(value, out Accordo? a);
        return a;
    }

    public override string ToString()
    {
        return ToString(false);
    }
    public string ToString(bool bemollePrefer)
    {
        System.Text.StringBuilder sb = new();
        sb.Append(Scala.ToString(bemollePrefer));
        if (Scala.Modo is ModoMinoreArmonica) sb.Append('-');
        if (Estensione is not null) sb.Append(Estensione.ToString());
        if (Basso is not null)
        {
            sb.Append('/');
            sb.Append(Basso.ToString());
        }
        return sb.ToString();
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public object Clone()
    {
        Accordo n = Basso is null ? new(scala, Estensione) : new(scala, Estensione, Basso);
        return n;
    }
}
