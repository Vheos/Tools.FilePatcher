namespace Vheos.Tools.FilePatcher.Code.Message;

public class MessageSet : HashSet<Message>
{
    public event Action<Message> OnNewMessage = delegate { };

    public IEnumerable<Message> Informations => this.Where(message => message.IsInformation);
    public IEnumerable<Message> Suggestions => this.Where(message => message.IsSuggestion);
    public IEnumerable<Message> Warnings => this.Where(message => message.IsWarning);
    public IEnumerable<Message> Errors => this.Where(message => message.IsError);

    public new bool Add(Message message)
    {
        if (!base.Add(message))
            return false;

        OnNewMessage(message);
        return true;
    }
    public void Inform(string title, string content) => Add(new(title, content, MessageType.Information));
    public void Inform(string title) => Add(new(title, MessageType.Information));
    public void Suggest(string title, string content) => Add(new(title, content, MessageType.Suggestion));
    public void Suggest(string title) => Add(new(title, MessageType.Suggestion));
    public void Warn(string title, string content) => Add(new(title, content, MessageType.Warning));
    public void Warn(string title) => Add(new(title, MessageType.Warning));
    public void Err(string title, string content) => Add(new(title, content, MessageType.Error));
    public void Err(string title) => Add(new(title, MessageType.Error));
}