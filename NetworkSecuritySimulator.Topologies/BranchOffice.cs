using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Branch Office Network Topology
    /// 
    /// Network Structure:
    /// Remote branch office connected to headquarters via WAN/VPN.
    /// 
    /// Components:
    /// - 1 Gateway Router (WAN link to HQ)
    /// - 1 Local Switch
    /// - 1 Local Firewall
    /// - 1 Local Server (file cache/print server)
    /// - 8 Workstations
    /// - 1 IP Phone system (simulated as device)
    /// 
    /// Hidden Vulnerabilities:
    /// - Gateway router VPN misconfiguration
    /// - Firewall with incomplete rule set
    /// - Local server with weak credentials
    /// - Workstations with limited security updates
    /// 
    /// Attack Vectors:
    /// - VPN compromise
    /// - Firewall rule bypass
    /// - Server unauthorized access
    /// - Lateral movement through workstations
    /// </summary>
    public class BranchOffice
    {
        public string TopologyName => "Branch Office Network";
        public string Description => "Remote branch office connected to headquarters";

        private Router gatewayRouter;
        private Switch localSwitch;
        private Firewall localFirewall;
        private PC localServer;
        private List<PC> workstations;

        public BranchOffice()
        {
            workstations = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create gateway router
            gatewayRouter = new Router
            {
                DeviceName = "Branch-Gateway-Router",
                IpAddress = "172.16.0.1",
                MacAddress = "00:00:00:06:00:01",
                Subnet = "172.16.0.0/24"
            };
            gatewayRouter.CreateMisconfiguration(); // VPN misconfiguration

            // Create local switch
            localSwitch = new Switch
            {
                DeviceName = "Branch-Local-Switch",
                IpAddress = "192.168.100.1",
                MacAddress = "00:00:00:06:01:01",
                Subnet = "192.168.100.0/24",
                NumberOfPorts = 24
            };

            // Create local firewall
            localFirewall = new Firewall
            {
                DeviceName = "Branch-Local-Firewall",
                IpAddress = "192.168.100.254",
                MacAddress = "00:00:00:06:02:FE",
                Subnet = "192.168.100.0/24"
            };

            // Create local server
            localServer = new PC
            {
                DeviceName = "Branch-File-Server",
                IpAddress = "192.168.100.10",
                MacAddress = "00:00:00:06:03:0A",
                Subnet = "192.168.100.0/24",
                PrimaryUser = "BranchAdmin"
            };
            localSwitch.ConnectDevice(localServer.DeviceId);

            // Create workstations
            for (int i = 1; i <= 8; i++)
            {
                var ws = new PC
                {
                    DeviceName = $"Branch-WS-{i:D2}",
                    IpAddress = $"192.168.100.{20 + i}",
                    MacAddress = $"00:00:00:06:03:{20 + i:X2}",
                    Subnet = "192.168.100.0/24",
                    PrimaryUser = $"BranchEmployee{i}"
                };
                workstations.Add(ws);
                localSwitch.ConnectDevice(ws.DeviceId);
            }
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            if (gatewayRouter != null)
                devices.Add(gatewayRouter);
            if (localSwitch != null)
                devices.Add(localSwitch);
            if (localFirewall != null)
                devices.Add(localFirewall);
            if (localServer != null)
                devices.Add(localServer);
            devices.AddRange(workstations);
            return devices;
        }
    }
}
