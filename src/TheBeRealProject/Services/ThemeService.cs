using Microsoft.JSInterop;

namespace TheBeRealProject.Services;

public class ThemeService
{
  private readonly IJSRuntime _runtime;

  public string Theme { get; private set; } = "light";

  public ThemeService(IJSRuntime runtime)
  {
    _runtime = runtime;
  }

  public ValueTask SetThemeAsync(string theme)
  {
    Theme = theme;
    return _runtime.InvokeVoidAsync("setTheme", theme);
  } 
}