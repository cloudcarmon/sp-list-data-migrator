using Microsoft.SharePoint.Client;
using System;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public abstract class BaseContextStrategy : IContextStrategy
    {
        public abstract ClientContext GetContext();
        public abstract void ProcessCommandLine();
    }
}
