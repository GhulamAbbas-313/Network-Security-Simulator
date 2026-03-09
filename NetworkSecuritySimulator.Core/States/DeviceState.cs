namespace NetworkSecuritySimulator.Core.States
{
    /// <summary>
    /// Enum representing the state of a network device
    /// Follows a state machine model: Normal → Vulnerable → Compromised
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// Device is operating normally with no detected vulnerabilities
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Device has vulnerabilities but has not been compromised yet
        /// Attacks are more likely to succeed in this state
        /// </summary>
        Vulnerable = 1,

        /// <summary>
        /// Device has been compromised by an attack
        /// The attacker has control or access to sensitive data
        /// </summary>
        Compromised = 2,

        /// <summary>
        /// Device is offline or unreachable
        /// </summary>
        Offline = 3
    }
}
