using Microsoft.JSInterop;
using Moq;
using TheBeRealProject.Services;

namespace TheBeRealProjectTest.Services;
public class ThemeServiceTest
{
    [Fact]
    public async Task SetThemeAsync_should_call_set_theme_js()
    {
        var jsRuntimeMock = new Mock<IJSRuntime>();
        jsRuntimeMock.Setup(m => m.InvokeAsync<It.IsAnyType>("setTheme", It.IsAny<object?[]?>())).Verifiable();

        var sut = new ThemeService(jsRuntimeMock.Object);

        sut.ThemeChanged += (sender, args) => 
        {
            jsRuntimeMock.Verify();
        };  
        await sut.SetThemeAsync(Guid.NewGuid().ToString()).ConfigureAwait(false);
    }

    [Fact]
    public async Task InitAsync_should_call_get_theme_js()
    {
        var expected = Guid.NewGuid().ToString();
        var jsRuntimeMock = new Mock<IJSRuntime>();
        jsRuntimeMock.Setup(m => m.InvokeAsync<string>("getTheme", It.IsAny<object?[]?>())).ReturnsAsync(expected);

        var sut = new ThemeService(jsRuntimeMock.Object);

        await sut.InitAsync().ConfigureAwait(false);

        Assert.Equal(expected, sut.Theme);
    }
}
