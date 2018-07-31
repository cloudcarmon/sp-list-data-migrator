using Microsoft.SharePoint.Client;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public interface IContextStrategy
    {
        void ProcessCommandLine();
        ClientContext GetContext();
    }

    public enum ContextStrategyType
    {
        WebLogin = 1,
        AppOnly = 2,
        NetworkCredential = 3
    }

    public static class ContextStrategyFactory
    {
        public static IContextStrategy GetContextStrategy(ContextStrategyType strategyType)
        {
            IContextStrategy contextStrategy = null;

            switch (strategyType)
            {
                case ContextStrategyType.WebLogin:
                    contextStrategy = new WebLoginContextStrategy();
                    break;
                case ContextStrategyType.AppOnly:
                    contextStrategy = new AppOnlyContextStrategy();
                    break;
                case ContextStrategyType.NetworkCredential:
                    contextStrategy = new NetworkCredentialContextStrategy();
                    break;
                default:
                    break;
            }

            return contextStrategy;
        }
    }
}
