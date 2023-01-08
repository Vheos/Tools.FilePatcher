namespace Vheos.Tools.FilePatcher.Code.Message;

public readonly struct Message
{
    private static readonly string DefaultContent = string.Empty;
    private static readonly MessageType DefaultType = MessageType.Information;

    public readonly string Title;
    public readonly string Content;
    public readonly MessageType Type;
    public readonly DateTime Time;

    public bool IsInformation => Type == MessageType.Information;
    public bool IsSuggestion => Type == MessageType.Suggestion;
    public bool IsWarning => Type == MessageType.Warning;
    public bool IsError => Type == MessageType.Error;

    public Message(string title, string content, MessageType type)
    {
        Title = title;
        Content = content;
        Type = type;
        Time = DateTime.Now;
    }
    public Message(string title, string content)
        : this(title, content, DefaultType) { }
    public Message(string title, MessageType type)
        : this(title, DefaultContent, type) { }
    public Message(string title)
        : this(title, DefaultContent, DefaultType) { }
}