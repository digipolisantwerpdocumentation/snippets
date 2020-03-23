namespace EventHandlerAdminExample
{
    public static class Config
    {
        public const string ApiKey = "<YOUR-API-KEY>";

        public const string BaseAddress = "https://api-gw-a.antwerpen.be/acpaas/eventhandleradmin/v2/";

        public const string NamespaceOwnerKey = "myNamespaceOwnerKey";

        public const string SubscriptionOwnerKey = "mySubscriptionOwnerKey";

        /// <summary>
        /// The admin owner key is needed for creating a namespace but is not available to regular consumers of the API.
        /// </summary>
        public const string AdminOwnerKey = "<ADMIN-OWNER-KEY>";

        public const string Namespace = "code-snippets-example-namespace";
        
        public const string Topic = "my-topic";
        
        public const string Subscription = "my-subscription";

        public const bool CreateNamespace = false;
    }
}
