using System.Collections.Generic;
using NetworkSecuritySimulator.Core.Devices;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Interface for network topologies
    /// All topology classes should implement this interface
    /// </summary>
    public interface INetworkTopology
    {
        /// <summary>
        /// Gets the name of this topology
        /// </summary>
        string TopologyName { get; }

        /// <summary>
        /// Gets the description of this topology
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Builds the topology structure
        /// </summary>
        void BuildTopology();

        /// <summary>
        /// Gets all devices in this topology
        /// </summary>
        /// <returns>List of all devices</returns>
        List<Device> GetAllDevices();
    }
}
