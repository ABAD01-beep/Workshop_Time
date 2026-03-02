namespace Workshop;

public class Time
{
    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    public int Hour
    {
        get => _hour;
        set
        {
            if (!ValidHour(value))
                throw new ArgumentException($"The hour: {value}, is not valid.");
            _hour = value;
        }
    }

    public int Minute
    {
        get => _minute;
        set
        {
            if (!ValidMinute(value))
                throw new ArgumentException($"The minute: {value}, is not valid.");
            _minute = value;
        }
    }

    public int Second
    {
        get => _second;
        set
        {
            if (!ValidSecond(value))
                throw new ArgumentException($"The second: {value}, is not valid.");
            _second = value;
        }
    }

    public int Millisecond
    {
        get => _millisecond;
        set
        {
            if (!ValidMillisecond(value)) 
                throw new ArgumentException($"The millisecond: {value}, is not valid.");
            _millisecond = value;
        }
    }

    public Time() : this(0, 0, 0, 0) { }
    public Time(int hour) : this(hour, 0, 0, 0) { }
    public Time(int hour, int minute) : this(hour, minute, 0, 0) { }
    public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }

    public Time(int hour, int minute, int second, int milliseconds)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = milliseconds;
    }

    public override string ToString()
    {
        int displayHour = _hour % 12;
        if (displayHour == 0)
            displayHour = 12;

        string period = _hour < 12 ? "AM" : "PM";

        return $"{displayHour:00}:{_minute:00}:{_second:00}.{_millisecond:000} {period}";
    }

    public long ToMilliseconds()
    {
        return (long)_hour * 3600000 +
               (long)_minute * 60000 +
               (long)_second * 1000 +
               _millisecond;
    }

    public long ToSeconds()
    {
        return (long)_hour * 3600 +
               (long)_minute * 60 +
               _second;
    }

    public long ToMinutes()
    {
        return (long)_hour * 60 + _minute;
    }

    public bool IsOtherDay(Time other)
    {
        return this.ToMilliseconds() + other.ToMilliseconds() >= 86400000;
    }

    public Time Add(Time other)
    {
        long totalMilliseconds = this.ToMilliseconds() + other.ToMilliseconds();
        totalMilliseconds %= 86400000;

        int h = (int)(totalMilliseconds / 3600000);
        totalMilliseconds %= 3600000;

        int m = (int)(totalMilliseconds / 60000);
        totalMilliseconds %= 60000;

        int s = (int)(totalMilliseconds / 1000);
        int ms = (int)(totalMilliseconds % 1000);

        return new Time(h, m, s, ms);
    }
    private bool ValidHour(int hour)
    {
        return hour >= 0 && hour <= 23;
    }

    private bool ValidMinute(int minute)
    {
        return minute >= 0 && minute <= 59;
    }

    private bool ValidSecond(int second)
    {
        return second >= 0 && second <= 59;
    }

    private bool ValidMillisecond(int millisecond)
    {
        return millisecond >= 0 && millisecond <= 999;
    }
}