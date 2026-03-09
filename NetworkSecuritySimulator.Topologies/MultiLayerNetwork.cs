using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Multi-Layer Network Topology
    /// 
    /// Network Structure:
    /// Enterprise network with three distinct layers: Core, Distribution, Access.
    /// 
    /// Components:
    /// Core Layer (10.0.0.0/24):
    /// - 2 Core Routers
    /// - 1 Core Firewall
    /// - 2 Core Switches
    /// 
    /// Distribution Layer (10.1.0.0/24):
    /// - 2 Distribution Routers
    /// - 2 Distribution Switches
    /// - 1 Distribution Firewall
    /// 
    /// Access Layer (Multiple subnets):
    /// - 4 Access Switches
    /// - 3 VLANs (Admin, Users, Guests)
    /// - 24 Workstations (8 per VLAN)
    /// - 4 Servers
    /// 
    /// Hidden Vulnerabilities:
    /// - VLAN misconfiguration allowing inter-VLAN routing
    /// - Core router with BGP misconfiguration
    /// - Distribution layer with incomplete ACLs
    /// - Guest VLAN with minimal security
    /// 
    /// Attack Vectors:
    /// - VLAN hopping from guest to admin
    /// - Lateral movement between layers
    /// - BGP route hijacking
    /// - Firewall rule bypass at distribution layer
    /// </summary>
    public class MultiLayerNetwork
    {
        public string TopologyName => "Multi-Layer Network";
        public string Description => "Enterprise network with core, distribution, and access layers";

        // Core Layer
        private List<Router> coreRouters;
        private Firewall coreFirewall;
        private List<Switch> coreSwitches;

        // Distribution Layer
        private List<Router> distributionRouters;
        private List<Switch> distributionSwitches;
        private Firewall distributionFirewall;

        // Access Layer
        private List<Switch> accessSwitches;
        private List<PC> servers;
        private List<PC> workstations;

        public MultiLayerNetwork()
        {
            coreRouters = new List<Router>();
            coreSwitches = new List<Switch>();
            distributionRouters = new List<Router>();
            distributionSwitches = new List<Switch>();
            accessSwitches = new List<Switch>();
            servers = new List<PC>();
            workstations = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Core Layer Setup
            for (int i = 1; i <= 2; i++)
            {
                var router = new Router
                {
                    DeviceName = $"Core-Router-{i}",
                    IpAddress = $"10.0.0.{i}",
                    MacAddress = $"00:00:00:08:00:{i:X2}",
                    Subnet = "10.0.0.0/24"
                };
                if (i == 1)
                    router.CreateMisconfiguration(); // BGP misconfiguration
                coreRouters.Add(router);
            }

            coreFirewall = new Firewall
            {
                DeviceName = "Core-Firewall",
                IpAddress = "10.0.1.1",
                MacAddress = "00:00:00:08:01:01",
                Subnet = "10.0.1.0/24"
            };

            for (int i = 1; i <= 2; i++)
            {
                var sw = new Switch
                {
                    DeviceName = $"Core-Switch-{i}",
                    IpAddress = $"10.0.2.{i}",
                    MacAddress = $"00:00:00:08:02:{i:X2}",
                    Subnet = "10.0.2.0/24",
                    NumberOfPorts = 48
                };
                coreSwitches.Add(sw);
            }

            // Distribution Layer Setup
            for (int i = 1; i <= 2; i++)
            {
                var router = new Router
                {
                    DeviceName = $"Distribution-Router-{i}",
                    IpAddress = $"10.1.0.{i}",
                    MacAddress = $"00:00:00:08:03:{i:X2}",
                    Subnet = "10.1.0.0/24"
                };
                distributionRouters.Add(router);
            }

            for (int i = 1; i <= 2; i++)
            {
                var sw = new Switch
                {
                    DeviceName = $"Distribution-Switch-{i}",
                    IpAddress = $"10.1.1.{i}",
                    MacAddress = $"00:00:00:08:04:{i:X2}",
                    Subnet = "10.1.1.0/24",
                    NumberOfPorts = 48
                };
                distributionSwitches.Add(sw);
            }

            distributionFirewall = new Firewall
            {
                DeviceName = "Distribution-Firewall",
                IpAddress = "10.1.2.1",
                MacAddress = "00:00:00:08:05:01",
                Subnet = "10.1.2.0/24"
            };

            // Access Layer Setup
            string[] vlans = { "Admin-VLAN", "Users-VLAN", "Guests-VLAN", "Servers-VLAN" };
            
            for (int i = 0; i < vlans.Length; i++)
            {
                var sw = new Switch
                {
                    DeviceName = $"Access-Switch-{vlans[i]}",
                    IpAddress = $"192.168.{10 + i}.1",
                    MacAddress = $"00:00:00:08:06:{10 + i:X2}",
                    Subnet = $"192.168.{10 + i}.0/24",
                    NumberOfPorts = 24
                };
                accessSwitches.Add(sw);
            }

            // Create servers on Servers VLAN
            for (int i = 1; i <= 4; i++)
            {
                var server = new PC
                {
                    DeviceName = $"ML-Server-{i}",
                    IpAddress = $"192.168.13.{10 + i}",
                    MacAddress = $"00:00:00:08:07:{10 + i:X2}",
                    Subnet = "192.168.13.0/24",
                    PrimaryUser = "ServerAdmin",
                    SensitiveDataGB = 100 * i
                };
                servers.Add(server);
                accessSwitches[3].ConnectDevice(server.DeviceId);
            }

            // Create workstations (8 per VLAN: Admin, Users, Guests)
            int wsCounter = 1;
            int[] wperVLAN = { 8, 8, 8 };
            for (int v = 0; v < 3; v++)
            {
                for (int j = 1; j <= wperVLAN[v]; j++)
                {
                    var ws = new PC
                    {
                        DeviceName = $"WS-{vlans[v]}-{j:D2}",
                        IpAddress = $"192.168.{10 + v}.{100 + j}",
                        MacAddress = $"00:00:00:08:07:{wsCounter:X2}",
                        Subnet = $"192.168.{10 + v}.0/24",
                        PrimaryUser = $"User{wsCounter}"
                    };

                    // Guest VLAN has minimal security
                    if (v == 2)
                    {
                        ws.DeactivateAntivirus();
                        ws.DisablePatch();
                    }

                    workstations.Add(ws);
                    accessSwitches[v].ConnectDevice(ws.DeviceId);
                    wsCounter++;
                }
            }
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            devices.AddRange(coreRouters);
            devices.Add(coreFirewall);
            devices.AddRange(coreSwitches);
            devices.AddRange(distributionRouters);
            devices.Add(distributionFirewall);
            devices.AddRange(distributionSwitches);
            devices.AddRange(accessSwitches);
            devices.AddRange(servers);
            devices.AddRange(workstations);
            return devices;
        }
    }
}
