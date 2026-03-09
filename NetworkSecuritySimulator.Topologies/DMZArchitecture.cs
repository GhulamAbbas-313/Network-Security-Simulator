using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// DMZ Architecture Topology
    /// 
    /// Network Structure:
    /// Demilitarized Zone (DMZ) separating internal and external networks.
    /// 
    /// Components:
    /// Internal Zone:
    /// - 1 Internal Firewall
    /// - 1 Internal Switch
    /// - 8 Internal Workstations
    /// - 1 Internal Server (database)
    /// 
    /// DMZ Zone:
    /// - 1 DMZ Switch
    /// - 1 Web Server (public-facing)
    /// - 1 Mail Server (SMTP/POP3)
    /// - 1 Application Server
    /// 
    /// External Zone:
    /// - 1 Edge Firewall
    /// - 1 Internet Router
    /// 
    /// Hidden Vulnerabilities:
    /// - Web server unpatched framework
    /// - DMZ firewall rules incomplete
    /// - Internal firewall rules too permissive
    /// - Database server weak credentials
    /// 
    /// Attack Vectors:
    /// - Web server exploitation
    /// - DMZ compromise for internal access
    /// - Firewall rule bypass
    /// - Mail server attacks
    /// </summary>
    public class DMZArchitecture
    {
        public string TopologyName => "DMZ Architecture";
        public string Description => "Demilitarized zone separating internal and external networks";

        // External zone
        private Router internetRouter;
        private Firewall edgeFirewall;

        // DMZ zone
        private Switch dmzSwitch;
        private List<PC> dmzServers;

        // Internal zone
        private Firewall internalFirewall;
        private Switch internalSwitch;
        private List<PC> internalServers;
        private List<PC> internalWorkstations;

        public DMZArchitecture()
        {
            dmzServers = new List<PC>();
            internalServers = new List<PC>();
            internalWorkstations = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // External Zone
            internetRouter = new Router
            {
                DeviceName = "Internet-Router",
                IpAddress = "203.0.113.1",
                MacAddress = "00:00:00:07:00:01",
                Subnet = "203.0.113.0/24"
            };

            edgeFirewall = new Firewall
            {
                DeviceName = "Edge-Firewall",
                IpAddress = "10.0.0.1",
                MacAddress = "00:00:00:07:01:01",
                Subnet = "10.0.0.0/24"
            };

            // DMZ Zone
            dmzSwitch = new Switch
            {
                DeviceName = "DMZ-Switch",
                IpAddress = "10.1.0.1",
                MacAddress = "00:00:00:07:02:01",
                Subnet = "10.1.0.0/24",
                NumberOfPorts = 24
            };

            // DMZ Servers
            var webServer = new PC
            {
                DeviceName = "DMZ-Web-Server",
                IpAddress = "10.1.0.10",
                MacAddress = "00:00:00:07:02:0A",
                Subnet = "10.1.0.0/24",
                PrimaryUser = "WebServiceUser"
            };
            webServer.DisablePatch(); // Web framework vulnerability
            dmzServers.Add(webServer);
            dmzSwitch.ConnectDevice(webServer.DeviceId);

            var mailServer = new PC
            {
                DeviceName = "DMZ-Mail-Server",
                IpAddress = "10.1.0.20",
                MacAddress = "00:00:00:07:02:14",
                Subnet = "10.1.0.0/24",
                PrimaryUser = "MailServiceUser"
            };
            dmzServers.Add(mailServer);
            dmzSwitch.ConnectDevice(mailServer.DeviceId);

            var appServer = new PC
            {
                DeviceName = "DMZ-App-Server",
                IpAddress = "10.1.0.30",
                MacAddress = "00:00:00:07:02:1E",
                Subnet = "10.1.0.0/24",
                PrimaryUser = "AppServiceUser"
            };
            dmzServers.Add(appServer);
            dmzSwitch.ConnectDevice(appServer.DeviceId);

            // Internal Zone
            internalFirewall = new Firewall
            {
                DeviceName = "Internal-Firewall",
                IpAddress = "10.2.0.1",
                MacAddress = "00:00:00:07:03:01",
                Subnet = "10.2.0.0/24"
            };

            internalSwitch = new Switch
            {
                DeviceName = "Internal-Switch",
                IpAddress = "192.168.1.1",
                MacAddress = "00:00:00:07:04:01",
                Subnet = "192.168.1.0/24",
                NumberOfPorts = 48
            };

            // Internal servers
            var dbServer = new PC
            {
                DeviceName = "Internal-Database-Server",
                IpAddress = "192.168.1.50",
                MacAddress = "00:00:00:07:04:32",
                Subnet = "192.168.1.0/24",
                PrimaryUser = "DBAdmin",
                SensitiveDataGB = 500
            };
            internalServers.Add(dbServer);
            internalSwitch.ConnectDevice(dbServer.DeviceId);

            // Internal workstations
            for (int i = 1; i <= 8; i++)
            {
                var ws = new PC
                {
                    DeviceName = $"Internal-WS-{i:D2}",
                    IpAddress = $"192.168.1.{100 + i}",
                    MacAddress = $"00:00:00:07:04:{100 + i:X2}",
                    Subnet = "192.168.1.0/24",
                    PrimaryUser = $"Employee{i}"
                };
                internalWorkstations.Add(ws);
                internalSwitch.ConnectDevice(ws.DeviceId);
            }
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            if (internetRouter != null)
                devices.Add(internetRouter);
            if (edgeFirewall != null)
                devices.Add(edgeFirewall);
            if (dmzSwitch != null)
                devices.Add(dmzSwitch);
            devices.AddRange(dmzServers);
            if (internalFirewall != null)
                devices.Add(internalFirewall);
            if (internalSwitch != null)
                devices.Add(internalSwitch);
            devices.AddRange(internalServers);
            devices.AddRange(internalWorkstations);
            return devices;
        }
    }
}
