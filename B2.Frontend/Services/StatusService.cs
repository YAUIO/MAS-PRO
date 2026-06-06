namespace B2.Frontend.Services;

public class StatusService
{
    public string Status { get; private set; }
    public string CssClass { get; private set; }

    public event Action? OnChange;

    public async Task SetStatusAsync(string status,  StatusType? type = null, int delay = 2000)
    {
        CssClass = type switch
        {
            StatusType.Success => "alert-success",
            StatusType.Error => "alert-danger",
            StatusType.Warning => "alert-warning",
            _ => "alert-info"
        };

        Status = status;

        OnChange?.Invoke();
        
        await Task.Delay(delay);

        Status = string.Empty;
        
        OnChange?.Invoke();
    }
    
    public enum StatusType
    {
        Success,
        Error,
        Warning,
    }
}