using System;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Loads and manages network topology instances
    /// Factory pattern implementation
    /// </summary>
    public class TopologyLoader
    {
        /// <summary>
        /// List of available topologies with metadata
        /// </summary>
        public static List<TopologyInfo> GetAvailableTopologies()
        {
            return new List<TopologyInfo>
            {
                new TopologyInfo { Name = "Star Topology", Description = "All devices connected to central switch", DeviceCount = 10 },
                new TopologyInfo { Name = "Bus Topology", Description = "All devices connected to central bus cable", DeviceCount = 9 },
                new TopologyInfo { Name = "Ring Topology", Description = "Devices connected in circular ring formation", DeviceCount = 10 },
                new TopologyInfo { Name = "Mesh Topology", Description = "Every device connected to every other device", DeviceCount = 18 },
                new TopologyInfo { Name = "Tree Topology", Description = "Hierarchical topology with root and branch switches", DeviceCount = 16 },
                new TopologyInfo { Name = "Hybrid Topology", Description = "Combination of Star and Mesh topologies", DeviceCount = 18 },
                new TopologyInfo { Name = "Enterprise LAN", Description = "Large enterprise network with departments and security zones", DeviceCount = 23 },
                new TopologyInfo { Name = "Campus Network", Description = "University campus network across multiple buildings", DeviceCount = 28 },
                new TopologyInfo { Name = "Data Center Network", Description = "High-availability redundant data center infrastructure", DeviceCount = 15 },
                new TopologyInfo { Name = "Branch Office Network", Description = "Remote branch office connected to headquarters", DeviceCount = 12 },
                new TopologyInfo { Name = "DMZ Architecture", Description = "Demilitarized zone separating internal and external networks", DeviceCount = 20 },
                new TopologyInfo { Name = "Multi-Layer Network", Description = "Enterprise network with core, distribution, and access layers", DeviceCount = 35 }
            };
        }

        /// <summary>
        /// Loads a topology by name
        /// </summary>
        /// <param name="topologyName">Name of the topology to load</param>
        /// <returns>Instantiated topology, or null if not found</returns>
        public static INetworkTopology LoadTopology(string topologyName)
        {
            // TODO: Implement topology loading
            // return topologyName switch
            // {
            //     "Star Topology" => new StarTopology(),
            //     "Bus Topology" => new BusTopology(),
            //     "Ring Topology" => new RingTopology(),
            //     "Mesh Topology" => new MeshTopology(),
            //     "Tree Topology" => new TreeTopology(),
            //     "Hybrid Topology" => new HybridTopology(),
            //     "Enterprise LAN" => new EnterpriseLAN(),
            //     "Campus Network" => new CampusNetwork(),
            //     "Data Center Network" => new DataCenter(),
            //     "Branch Office Network" => new BranchOffice(),
            //     "DMZ Architecture" => new DMZArchitecture(),
            //     "Multi-Layer Network" => new MultiLayerNetwork(),
            //     _ => null
            // };

            return null; // Placeholder
        }
    }

    /// <summary>
    /// Metadata about a topology
    /// </summary>
    public class TopologyInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DeviceCount { get; set; }
    }
}
