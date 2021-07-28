public interface ISimpleLogger
{
    void Debug(string text);
    void Error(string text);
    void Fatal(string text);
    void Info(string text);
    void Trace(string text);
    void Warning(string text);
}