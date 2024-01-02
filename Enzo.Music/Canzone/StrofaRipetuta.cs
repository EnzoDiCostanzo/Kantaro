namespace Enzo.Music;

public class StrofaRipetuta : Strofa
{
    /// <summary>
    /// Nome della strofa da ripetere
    /// </summary>
    public string Riferimento;

    public StrofaRipetuta(string riferimento)
    {
        Riferimento = riferimento;
    }

    public override object Clone()
    {
        StrofaRipetuta c = new(Riferimento);
        c.CopyFrom(this);
        //c.Riferimento = Riferimento;
        return c;
    }
}
