using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Mesh Topology
    /// 
    /// Network Structure:
    /// Every device is connected to every other device.
    /// Redundant connections and high reliability.
    /// 
    /// Components:
    /// - 6 Router devices in full mesh
    /// - 12 PC devices (2 per router)
    /// - High connectivity, complex security
    /// 
    /// Hidden Vulnerabilities:
    /// - Complex routing configuration (misconfiguration vulnerability)
    /// - Multiple routers without coordinated security
    /// - BGP/OSPF misconfiguration on routers
    /// 
    /// Attack Vectors:
    /// - Routing protocol attacks
    /// - Man-in-the-middle (due to redundant paths)
    /// - Unauthorized access via secondary paths
    /// </summary>
    public class MeshTopology
    {
        public string TopologyName => "Mesh";
        public string Description => "Every device connected to every other device";

        private List<Router> routers;
        private List<PC> computers;

        public MeshTopology()
        {
            routers = new List<Router>();
            computers = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create 6 routers in mesh
            for (int i = 1; i <= 6; i++)
            {
                var router = new Router
                {
                    DeviceName = $"Router-{i:D2}",
                    IpAddress = $"10.0.{i}.1",
                    MacAddress = $"00:00:00:01:00:{i:X2}",
                    Subnet = $"10.0.{i}.0/24"
                };
                routers.Add(router);
            }

            // Create 12 PC devices (2 per router)
            int pcCounter = 1;
            for (int i = 0; i < routers.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var pc = new PC
                    {
                        DeviceName = $"PC-Mesh-{pcCounter:D2}",
                        IpAddress = $"10.0.{i + 1}.{100 + j}",
                        MacAddress = $"00:00:00:01:01:{pcCounter:X2}",
                        Subnet = $"10.0.{i + 1}.0/24",
                        PrimaryUser = $"User{pcCounter}"
                    };
                    computers.Add(pc);
                    pcCounter++;
                }
            }

            // Mark routers as misconfigured for mesh attacks
            routers[0].CreateMisconfiguration();
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            devices.AddRange(routers);
            devices.AddRange(computers);
            return devices;
        }
    }
}
