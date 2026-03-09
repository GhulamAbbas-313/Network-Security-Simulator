using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Hybrid Topology
    /// 
    /// Network Structure:
    /// Combination of Star and Mesh topologies.
    /// Core network in mesh, access networks in star.
    /// 
    /// Components:
    /// - Core: 3 routers in mesh configuration
    /// - Access Layer: 3 switches in star to core
    /// - End Devices: 12 PC devices (4 per switch)
    /// - 1 Firewall between core and access
    /// 
    /// Hidden Vulnerabilities:
    /// - Firewall with default rules (not hardened)
    /// - Routers with overlapping subnets (misconfiguration)
    /// - Access switches with no security policies
    /// 
    /// Attack Vectors:
    /// - VLAN hopping attacks
    /// - Firewall rule exploitation
    /// - Routing attack on core routers
    /// </summary>
    public class HybridTopology
    {
        public string TopologyName => "Hybrid";
        public string Description => "Combination of Star and Mesh topologies";

        private List<Router> coreRouters;
        private List<Switch> accessSwitches;
        private List<PC> endDevices;
        private Firewall firewall;

        public HybridTopology()
        {
            coreRouters = new List<Router>();
            accessSwitches = new List<Switch>();
            endDevices = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create 3 core routers in mesh
            for (int i = 1; i <= 3; i++)
            {
                var router = new Router
                {
                    DeviceName = $"Core-Router-{i}",
                    IpAddress = $"10.0.0.{i}",
                    MacAddress = $"00:00:00:02:00:{i:X2}",
                    Subnet = "10.0.0.0/24"
                };
                coreRouters.Add(router);
            }

            // Create firewall
            firewall = new Firewall
            {
                DeviceName = "Core-Firewall",
                IpAddress = "10.0.1.1",
                MacAddress = "00:00:00:02:01:FF",
                Subnet = "10.0.1.0/24"
            };

            // Create 3 access switches
            for (int i = 1; i <= 3; i++)
            {
                var sw = new Switch
                {
                    DeviceName = $"Access-Switch-{i}",
                    IpAddress = $"192.168.{i}.1",
                    MacAddress = $"00:00:00:02:0{i}:FF",
                    Subnet = $"192.168.{i}.0/24",
                    NumberOfPorts = 24
                };
                accessSwitches.Add(sw);
            }

            // Create 12 end devices (4 per access switch)
            int pcCounter = 1;
            for (int i = 0; i < accessSwitches.Count; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    var pc = new PC
                    {
                        DeviceName = $"PC-Hybrid-{pcCounter:D2}",
                        IpAddress = $"192.168.{i + 1}.{10 + j}",
                        MacAddress = $"00:00:00:02:0{i + 1}:{10 + j:X2}",
                        Subnet = $"192.168.{i + 1}.0/24",
                        PrimaryUser = $"User{pcCounter}"
                    };
                    endDevices.Add(pc);
                    accessSwitches[i].ConnectDevice(pc.DeviceId);
                    pcCounter++;
                }
            }

            // Create misconfiguration vulnerability on core router
            coreRouters[0].CreateMisconfiguration();
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            devices.AddRange(coreRouters);
            devices.Add(firewall);
            devices.AddRange(accessSwitches);
            devices.AddRange(endDevices);
            return devices;
        }
    }
}
