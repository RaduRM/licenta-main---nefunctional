namespace GestiuneFunerara.Services;
using Microsoft.JSInterop;

public class UserSessionService
{
    public int IdUtilizator { get; set; }
    public string? NumeComplet { get; set; }
    public string? Rol { get; set; }
    public string? ProfilImageUrl { get; set; }
    public string? Email { get; set; }
    public string? Telefon { get; set; }
    public string? ImagineProfil { get; set; } 



  
    public void Logout()
    {
        IdUtilizator = 0;
        NumeComplet = null;
        Rol = null;
        ImagineProfil = null;
        Email = null;
        Telefon = null;
    }

    // salvare sesiune în localstorage
    public async Task SaveSessionAsync(IJSRuntime js)
    {
        await js.InvokeVoidAsync("localStorage.setItem", "userId", IdUtilizator.ToString());
        await js.InvokeVoidAsync("localStorage.setItem", "nume", NumeComplet ?? "");
        await js.InvokeVoidAsync("localStorage.setItem", "rol", Rol ?? "");
        await js.InvokeVoidAsync("localStorage.setItem", "foto", ImagineProfil ?? "");
    }

    // incarcare sesiune din localstorage
    public async Task LoadSessionAsync(IJSRuntime js)
    {
        var id = await js.InvokeAsync<string>("localStorage.getItem", "userId");
        if (!string.IsNullOrEmpty(id))
        {
            IdUtilizator = int.Parse(id);
            NumeComplet = await js.InvokeAsync<string>("localStorage.getItem", "nume");
            Rol = await js.InvokeAsync<string>("localStorage.getItem", "rol");
            ImagineProfil = await js.InvokeAsync<string>("localStorage.getItem", "foto");
        }
    }

    // curatam localstorage cand dam logout
    public async Task ClearSessionAsync(IJSRuntime js)
    {
        await js.InvokeVoidAsync("localStorage.removeItem", "userId");
        await js.InvokeVoidAsync("localStorage.removeItem", "nume");
        await js.InvokeVoidAsync("localStorage.removeItem", "rol");
        await js.InvokeVoidAsync("localStorage.removeItem", "foto");
    }
}
