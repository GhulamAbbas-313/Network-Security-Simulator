namespace NetworkSecuritySimulator.Core.Interfaces
{
    /// <summary>
    /// Interface defining defense mechanism behavior
    /// Implement this interface for each defense type
    /// </summary>
    public interface IDefense
    {
        /// <summary>
        /// Gets the name of the defense mechanism
        /// </summary>
        string DefenseName { get; }

        /// <summary>
        /// Gets the description of the defense
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Applies the defense to a target device
        /// </summary>
        /// <param name="targetDeviceId">The ID of the device to protect</param>
        /// <returns>True if defense was applied successfully, false otherwise</returns>
        bool Apply(string targetDeviceId);

        /// <summary>
        /// Removes the defense from a device
        /// </summary>
        /// <param name="targetDeviceId">The ID of the device</param>
        /// <returns>True if defense was removed successfully, false otherwise</returns>
        bool Remove(string targetDeviceId);

        /// <summary>
        /// Gets the effectiveness level of this defense (0-100%)
        /// </summary>
        int EffectivenessLevel { get; }
    }
}
