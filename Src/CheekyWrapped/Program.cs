namespace CheekyWrapped;

class Program
{
    static void Main()
    {
        DateTime EarlyCap = new DateTime(2023, 1, 1);
        DateTime LateCap = new DateTime(2023, 9, 30);
        
        _ = DataReader.LoadScrobbles("/home/tiredaj/Downloads/you_bloody_rock.csv", EarlyCap, LateCap);
        _ = DataReader.LoadScrobbles("/home/tiredaj/Downloads/tiredaj.csv", EarlyCap, LateCap);
    }
}