namespace NetworkSecuritySimulator.Core.Interfaces
{
    /// <summary>
    /// Interface defining attack behavior
    /// Implement this interface for each attack type
    /// </summary>
    public interface IAttack
    {
        /// <summary>
        /// Gets the name of the attack
        /// </summary>
        string AttackName { get; }

        /// <summary>
        /// Gets the description of the attack
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Executes the attack on a target device
        /// </summary>
        /// <param name="targetDeviceId">The ID of the device to attack</param>
        /// <returns>True if attack was successful, false otherwise</returns>
        bool Execute(string targetDeviceId);

        /// <summary>
        /// Gets the severity level of the attack (1-10)
        /// </summary>
        int SeverityLevel { get; }
    }
}
