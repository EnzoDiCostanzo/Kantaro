namespace Enzo.Music;

public class ModoMinoreNaturale : ModoBase
{
    public ModoMinoreNaturale()
    {
        CreaSuccessioneDistanze(new List<Distanza>()
            {
                Tono.Value,
                Semitono.Value,
                Tono.Value,
                Tono.Value,
                Semitono.Value,
                Tono.Value,
                Tono.Value
            });
    }
    public static ModoMinoreNaturale GetInstance() => new();
}

public class ModoMinoreMelodica : ModoBase
{
    public ModoMinoreMelodica()
    {
        CreaSuccessioneDistanze(new List<Distanza>()
            {
                Tono.Value,
                Semitono.Value,
                Tono.Value,
                Tono.Value,
                Tono.Value,
                Tono.Value,
                Semitono.Value
            });
    }
    public static ModoMinoreMelodica GetInstance() => new();
}

public class ModoMinoreArmonica : ModoBase
{
    public ModoMinoreArmonica()
    {
        CreaSuccessioneDistanze(new List<Distanza>()
            {
                Tono.Value,
                Semitono.Value,
                Tono.Value,
                Tono.Value,
                Semitono.Value,
                Tono.Value + Semitono.Value,
                Semitono.Value
            });
    }
    public static ModoMinoreArmonica GetInstance() => new();
}
