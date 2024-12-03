namespace CheekyWrapped;

public class DataReader
{
    public static List<Scrobble> LoadScrobbles(string _Filename, DateTime _EarlyCap, DateTime _LateCap)
    {
        using StreamReader Reader = new(_Filename);

        Dictionary<string, Scrobble> Scrobbles = new();
        long FailCount = 0;

        while (!Reader.EndOfStream)
        {
            string? Line = Reader.ReadLine();
            
            if (Line is null)
            { continue; }

            string Key = string.Empty;

            try
            {
                Key = Scrobble.GenerateKey(Line);

                if (Scrobbles.ContainsKey(Key))
                { Scrobbles[Key].Increment(); }
                else
                { Scrobbles.Add(Key, Scrobble.FromRow(Line)); }

            }
            catch (Exception exc)
            {
                Console.WriteLine($"Failed record: {Line}, Exception: {exc}");
                FailCount++;
            }
        }

        var Filtered = Scrobbles.AsParallel()
            .Select(X => X.Value)
            .Where(X => X.ScrobbledTime > _EarlyCap)
            .Where(X => X.ScrobbledTime < _LateCap)
            .ToList();        

        //Console.WriteLine($"Unique scrobbles: {Scrobbles.Count}");
        Console.WriteLine($"Filtered scrobbles: {Filtered.Count}");
        Console.WriteLine($"Failed scrobbles: {FailCount}");

        return Scrobbles.Values.ToList();
    }
}
