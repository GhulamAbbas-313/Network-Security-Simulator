using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Bus Topology
    /// 
    /// Network Structure:
    /// All devices connected to a single central cable (bus).
    /// Devices tap into the bus to connect.
    /// 
    /// Components:
    /// - 6 PC devices on main bus
    /// - 1 Printer (shared resource)
    /// - 1 File Server
    /// - Vulnerable to packet sniffing
    /// 
    /// Hidden Vulnerabilities:
    /// - No network segmentation
    /// - Shared media vulnerability
    /// - Server with weak access control
    /// 
    /// Attack Vectors:
    /// - Packet sniffing (promiscuous mode)
    /// - Unauthorized access to file server
    /// </summary>
    public class BusTopology
    {
        public string TopologyName => "Bus";
        public string Description => "All devices connected to central bus cable";

        private List<PC> workstations;
        private PC fileServer;
        private PC printer;

        public BusTopology()
        {
            workstations = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create 6 workstations
            for (int i = 1; i <= 6; i++)
            {
                var pc = new PC
                {
                    DeviceName = $"Workstation-{i:D2}",
                    IpAddress = $"192.168.1.{20 + i}",
                    MacAddress = $"00:00:00:00:01:{20 + i:X2}",
                    Subnet = "192.168.1.0/24",
                    PrimaryUser = $"User{i}"
                };
                workstations.Add(pc);
            }

            // Create file server
            fileServer = new PC
            {
                DeviceName = "FileServer-01",
                IpAddress = "192.168.1.50",
                MacAddress = "00:00:00:00:01:50",
                Subnet = "192.168.1.0/24",
                PrimaryUser = "ServerAdmin"
            };
            fileServer.DisablePatch(); // Hidden vulnerability

            // Create printer
            printer = new PC
            {
                DeviceName = "Printer-01",
                IpAddress = "192.168.1.51",
                MacAddress = "00:00:00:00:01:51",
                Subnet = "192.168.1.0/24"
            };
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            devices.AddRange(workstations);
            if (fileServer != null)
                devices.Add(fileServer);
            if (printer != null)
                devices.Add(printer);
            return devices;
        }
    }
}
