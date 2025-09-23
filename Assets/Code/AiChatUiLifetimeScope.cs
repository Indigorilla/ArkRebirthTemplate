using VContainer;
using VContainer.Unity;

public class AiChatUiLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<AiChatUiPresenter>();
    }
}
