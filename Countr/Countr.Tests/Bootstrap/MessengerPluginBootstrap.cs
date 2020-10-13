using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;
using MvvmCross.Plugins.Messenger;

namespace Countr.Tests.Bootstrap
{
    public class MessengerPluginBootstrap
        : MvxPluginBootstrapAction<MvvmCross.Plugins.Messenger.PluginLoader>
    {
        protected override void Load (IMvxPluginManager manager)
        {
            base.Load (manager);
            Mvx.RegisterSingleton<IMvxMessenger>(new MvxMessengerHub());
        }
    }
}