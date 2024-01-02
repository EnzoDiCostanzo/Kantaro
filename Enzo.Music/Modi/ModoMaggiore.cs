namespace Enzo.Music;

public class ModoMaggiore : ModoBase
{
    public ModoMaggiore()
    {
        CreaSuccessioneDistanze(new List<Distanza>()
            {
                Tono.Value,
                Tono.Value,
                Semitono.Value,
                Tono.Value,
                Tono.Value,
                Tono.Value,
                Semitono.Value
            });
    }

    public static ModoMaggiore GetInstance() => new();
}
