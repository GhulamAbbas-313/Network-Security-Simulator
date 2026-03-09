using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Star Topology
    /// 
    /// Network Structure:
    /// All devices connect to a central switch/hub.
    /// Single point of failure.
    /// 
    /// Components:
    /// - 1 Central Switch (hub)
    /// - 8 PC devices
    /// - 1 Server
    /// 
    /// Hidden Vulnerabilities:
    /// - Switch has no port security (MAC flooding vulnerability)
    /// - Server has unpatched software
    /// 
    /// Attack Vectors:
    /// - MAC Flooding on central switch
    /// - Unauthorized access to server
    /// </summary>
    public class StarTopology
    {
        public string TopologyName => "Star";
        public string Description => "All devices connected to central switch";
        
        private Switch centralSwitch;
        private List<PC> pcs;
        private PC server;

        /// <summary>
        /// Creates a Star topology
        /// </summary>
        public StarTopology()
        {
            pcs = new List<PC>();
        }

        /// <summary>
        /// Builds the topology with all devices
        /// </summary>
        public void BuildTopology()
        {
            // Create central switch
            centralSwitch = new Switch
            {
                DeviceName = "Central-Switch",
                IpAddress = "192.168.1.1",
                MacAddress = "00:00:00:00:00:01",
                Subnet = "192.168.1.0/24"
            };

            // Create 8 PC devices
            for (int i = 1; i <= 8; i++)
            {
                var pc = new PC
                {
                    DeviceName = $"PC-{i:D2}",
                    IpAddress = $"192.168.1.{10 + i}",
                    MacAddress = $"00:00:00:00:00:{10 + i:X2}",
                    Subnet = "192.168.1.0/24",
                    PrimaryUser = $"User{i}"
                };
                pcs.Add(pc);
                centralSwitch.ConnectDevice(pc.DeviceId);
            }

            // Create server
            server = new PC
            {
                DeviceName = "Server-01",
                IpAddress = "192.168.1.100",
                MacAddress = "00:00:00:00:00:64",
                Subnet = "192.168.1.0/24",
                PrimaryUser = "Administrator"
            };
            centralSwitch.ConnectDevice(server.DeviceId);

            // TODO: Verify vulnerabilities are present
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            if (centralSwitch != null)
                devices.Add(centralSwitch);
            devices.AddRange(pcs);
            if (server != null)
                devices.Add(server);
            return devices;
        }
    }
}
