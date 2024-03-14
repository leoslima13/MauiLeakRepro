namespace MemoryLeakTest;

public class LogEntry : Entry
{
    ~LogEntry()
    {
        System.Diagnostics.Debug.WriteLine("LogEntry being destroyed");
    }
}

public class LogDatePicker : DatePicker
{
    ~LogDatePicker()
    {
        System.Diagnostics.Debug.WriteLine("LogDatePicker being destroyed");
    }
}

public class LogLabel : Label
{
    ~LogLabel()
    {
        System.Diagnostics.Debug.WriteLine("LogLabel being destroyed");
    }
}

public class LogButton : Button
{
    ~LogButton()
    {
        System.Diagnostics.Debug.WriteLine("LogButton being destroyed");
    }
}