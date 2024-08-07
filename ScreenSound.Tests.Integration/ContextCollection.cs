using ScreenSound.Tests.Integration;

namespace ScreenSound.Tests.IntegracaoDB
{
    [CollectionDefinition("ContextCollection")]
    public class ContextCollection : ICollectionFixture<ScreenSoundWebApplicationFactory>
    {
    }
}