namespace CW_4.Helpers;

public class AgeHelper
{
    public static int GetFullYears(int ageInDays)
    {
        return ageInDays / 365;
    }
    public static string FormatAge(int ageInDays)
    {
        int years = ageInDays / 365;
        ageInDays %= 365;
        int months = ageInDays / 30;
        int days = ageInDays % 30;
        List<string> parts = new List<string>();
        if (years > 0)
        {
            parts.Add($"{years} {GetYearWord(years)}");
        }
        if (months > 0)
        {
            parts.Add($"{months} {GetMonthWord(months)}");
        }
        if (days > 0 || parts.Count == 0)
        {
            parts.Add($"{days} {GetDayWord(days)}");
        }
        return string.Join(" ", parts);
    }

    private static string GetYearWord(int value)
    {
        if (value % 10 == 1 && value % 100 != 11)
        {
            return "год";
        }
        if (value % 10 >= 2 && value % 10 <= 4 && (value % 100 < 10 || value % 100 >= 20))
        {
            return "года";
        }
        return "лет";
    }

    private static string GetMonthWord(int value)
    {
        if (value % 10 == 1 && value % 100 != 11)
        {
            return "месяц";
        }
        if (value % 10 >= 2 && value % 10 <= 4 && (value % 100 < 10 || value % 100 >= 20))
        {
            return "месяца";
        }
        return "месяцев";
    }

    private static string GetDayWord(int value)
    {
        if (value % 10 == 1 && value % 100 != 11)
        {
            return "день";
        }
        if (value % 10 >= 2 && value % 10 <= 4 && (value % 100 < 10 || value % 100 >= 20))
        {
            return "дня";
        }
        return "дней";
    }
}