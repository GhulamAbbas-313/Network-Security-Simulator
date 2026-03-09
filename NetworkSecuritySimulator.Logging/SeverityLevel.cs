using System;

namespace NetworkSecuritySimulator.Logging
{
    /// <summary>
    /// Enumeration of log severity levels
    /// </summary>
    public enum SeverityLevel
    {
        /// <summary>
        /// Information level - normal operational messages
        /// </summary>
        INFO = 0,

        /// <summary>
        /// Warning level - potentially problematic situations
        /// </summary>
        WARNING = 1,

        /// <summary>
        /// Critical level - serious issues that require immediate attention
        /// </summary>
        CRITICAL = 2
    }
}
