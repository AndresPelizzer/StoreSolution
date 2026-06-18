namespace FootballBlazor.Client.Models;

public class Storage
{
    private string _token = string.Empty;

    public string Token
    {
        get => _token;
        set
        {
            _token = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged()
        => OnChange?.Invoke();
}