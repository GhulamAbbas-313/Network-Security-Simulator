using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Ring Topology
    /// 
    /// Network Structure:
    /// Devices connected in a circular formation.
    /// Each device has two neighbors.
    /// 
    /// Components:
    /// - 8 PC devices in a ring
    /// - 1 Token Ring switch
    /// - 1 Router connection point
    /// 
    /// Hidden Vulnerabilities:
    /// - Unpatched Ring switch software
    /// - Router misconfiguration
    /// - Weak credentials on ring management
    /// 
    /// Attack Vectors:
    /// - Token manipulation attacks
    /// - Router misconfiguration exploitation
    /// - Unauthorized management access
    /// </summary>
    public class RingTopology
    {
        public string TopologyName => "Ring";
        public string Description => "Devices connected in circular ring formation";

        private List<PC> devices;
        private Router ringRouter;

        public RingTopology()
        {
            devices = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create 8 devices in ring
            for (int i = 1; i <= 8; i++)
            {
                var pc = new PC
                {
                    DeviceName = $"Ring-Node-{i:D2}",
                    IpAddress = $"192.168.2.{10 + i}",
                    MacAddress = $"00:00:00:00:02:{10 + i:X2}",
                    Subnet = "192.168.2.0/24",
                    PrimaryUser = $"User{i}"
                };
                devices.Add(pc);
            }

            // Create router
            ringRouter = new Router
            {
                DeviceName = "Ring-Router",
                IpAddress = "192.168.2.1",
                MacAddress = "00:00:00:00:02:01",
                Subnet = "192.168.2.0/24"
            };
            ringRouter.CreateMisconfiguration(); // Hidden vulnerability
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            devices.AddRange(this.devices);
            if (ringRouter != null)
                devices.Add(ringRouter);
            return devices;
        }
    }
}
