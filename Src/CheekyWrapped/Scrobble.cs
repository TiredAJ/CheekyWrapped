using System.Globalization;

namespace CheekyWrapped;

public class Scrobble
{
    public Song Tune { get; private set; } = new Song("The Loch", "Nessie", "The Loch EP");
    public DateTime ScrobbledTime { get; private set; } = DateTime.MinValue;
    public long ScrobbleCount { get; private set; } = 0;

    public Scrobble(Song _Tune, DateTime _ScrobbledTime, long _ScrobbleCount = 1)
    {
        Tune = _Tune;
        ScrobbledTime = _ScrobbledTime;
        ScrobbleCount = _ScrobbleCount;
    }
    
    public void Increment()
    { ScrobbleCount++; }
    
    public static string GenerateKey(string _Row)
    {
        string[] Split = _Row.Split(',');

        if (Split.Length < 4)
        { throw new ArgumentException("Given key does not contain a datetime"); }
        
        return $"{Split[0]}/{Split[1]}/{Split[2]}";
    }

    public static Scrobble FromRow(string _Row)
    {
        string[] Split = _Row.Split(',');

        if (Split[0] is ("" or " "))
        { Split[0] = "(no artist)"; }

        if (Split[1] is ("" or " "))
        { Split[1] = "(No Album)"; }

        if (Split[2] is ("" or " "))
        { throw new ArgumentException("Inputted row has no title"); }

        if (Split[3] is ("" or " "))
        { throw new ArgumentException("Inputted row has no datetime"); }

        Song Tune = new Song(Split[2], Split[0], Split[1]);

        DateTime ScrobbleTime;

        try
        {
            ScrobbleTime = DateTime.ParseExact(Split[3],
                "dd MMM yyyy HH:mm",
                CultureInfo.InvariantCulture);
        }
        catch (Exception Exc)
        {
            Console.WriteLine($"Failed to parse date of record: {_Row}, exception: {Exc}");
            throw Exc;
        }
        
        return new Scrobble(Tune, ScrobbleTime);
    }
}

public class Song
{
    public string Title {get; private set;} = String.Empty;
    public string Artist {get; private set;} = String.Empty;
    public string Album {get; private set;} = String.Empty;

    public Song(string _Title, string _Artist, string _Album)
    {
        Title = _Title;
        Artist = _Artist;
        Album = _Album;
    }
    
    public static Song FromRow(string _Row)
    {
        string[] split = _Row.Split(',');
        
        return new Song(split[0], split[1], split[2]);
    }
    
    public string Key 
        => $"{Title}/{Album}/{Artist}";
}
