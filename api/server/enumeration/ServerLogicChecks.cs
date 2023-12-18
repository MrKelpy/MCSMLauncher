namespace mcsm.api.server.enumeration
{
    /// <summary>
    /// This enum is responsible for defining a few different logical expressions that can be checked for a server.
    /// </summary>
    public enum ServerLogicChecks
    {
        /// <summary>
        /// Whether the server's "online-mode" property is set to false.
        /// </summary>
        IsCracked,
            
        /// <summary>
        /// Whether the server editor buffers contain the "spawn-protection" property and if it isn't set to 0.
        /// </summary>
        IsSpawnProtectionEnabled
    }
}