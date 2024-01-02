namespace Enzo.Music;

public class Semitono : Distanza
{
    public Semitono()
    {
        Valore = 0.5F;
    }

    public static Semitono Value { get; } = new();
}
