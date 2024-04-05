namespace newbuy.App.Services;

public class DateTimeCorrection
{
    public DateTime GetCorrectedDateTime(DateTime dateTime)
    {
        return dateTime.AddHours(-3);
    }
}
