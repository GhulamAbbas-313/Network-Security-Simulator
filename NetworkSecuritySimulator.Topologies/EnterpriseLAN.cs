using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Enterprise LAN Topology
    /// 
    /// Network Structure:
    /// Large organization with multiple departments and security zones.
    /// 
    /// Components:
    /// - 2 Main routers (primary and backup)
    /// - 4 Department switches (Finance, HR, IT, Operations)
    /// - 1 Main Firewall
    /// - 2 Core servers (File Server, Mail Server)
    /// - 16 Workstations (4 per department)
    /// 
    /// Hidden Vulnerabilities:
    /// - Backup router not properly configured
    /// - Finance department has unpatched systems
    /// - Firewall with incomplete rules
    /// - VLAN misconfiguration allowing inter-VLAN routing
    /// 
    /// Attack Vectors:
    /// - VLAN hopping from Operations to Finance
    /// - Unauthorized access to servers
    /// - Lateral movement through unpatched systems
    /// </summary>
    public class EnterpriseLAN
    {
        public string TopologyName => "Enterprise LAN";
        public string Description => "Large enterprise network with departments and security zones";

        private List<Router> routers;
        private List<Switch> departmentSwitches;
        private Firewall mainFirewall;
        private List<PC> servers;
        private List<PC> workstations;

        public EnterpriseLAN()
        {
            routers = new List<Router>();
            departmentSwitches = new List<Switch>();
            servers = new List<PC>();
            workstations = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create main routers
            for (int i = 1; i <= 2; i++)
            {
                var router = new Router
                {
                    DeviceName = i == 1 ? "Primary-Router" : "Backup-Router",
                    IpAddress = $"10.0.0.{i}",
                    MacAddress = $"00:00:00:03:00:{i:X2}",
                    Subnet = "10.0.0.0/24"
                };
                routers.Add(router);
            }

            // Create main firewall
            mainFirewall = new Firewall
            {
                DeviceName = "Main-Firewall",
                IpAddress = "10.0.1.1",
                MacAddress = "00:00:00:03:01:FF",
                Subnet = "10.0.1.0/24"
            };

            // Create department switches
            string[] departments = { "Finance", "HR", "IT", "Operations" };
            for (int i = 0; i < departments.Length; i++)
            {
                var sw = new Switch
                {
                    DeviceName = $"{departments[i]}-Switch",
                    IpAddress = $"192.168.{i + 1}.1",
                    MacAddress = $"00:00:00:03:0{i + 1}:FF",
                    Subnet = $"192.168.{i + 1}.0/24",
                    NumberOfPorts = 48
                };
                departmentSwitches.Add(sw);
            }

            // Create servers
            var fileServer = new PC
            {
                DeviceName = "File-Server",
                IpAddress = "10.0.2.10",
                MacAddress = "00:00:00:03:02:0A",
                Subnet = "10.0.2.0/24",
                PrimaryUser = "ServerAdmin",
                SensitiveDataGB = 500
            };
            servers.Add(fileServer);

            var mailServer = new PC
            {
                DeviceName = "Mail-Server",
                IpAddress = "10.0.2.11",
                MacAddress = "00:00:00:03:02:0B",
                Subnet = "10.0.2.0/24",
                PrimaryUser = "MailAdmin"
            };
            servers.Add(mailServer);

            // Create workstations for each department
            int wsCounter = 1;
            for (int i = 0; i < departmentSwitches.Count; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    var ws = new PC
                    {
                        DeviceName = $"{departments[i]}-WS-{j:D2}",
                        IpAddress = $"192.168.{i + 1}.{10 + j}",
                        MacAddress = $"00:00:00:03:0{i + 1}:{10 + j:X2}",
                        Subnet = $"192.168.{i + 1}.0/24",
                        PrimaryUser = $"Employee{wsCounter}"
                    };

                    // Add specific vulnerabilities
                    if (i == 0) // Finance department
                    {
                        ws.DisablePatch(); // Unpatched system
                    }

                    workstations.Add(ws);
                    departmentSwitches[i].ConnectDevice(ws.DeviceId);
                    wsCounter++;
                }
            }

            // Backup router misconfiguration
            routers[1].CreateMisconfiguration();
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            devices.AddRange(routers);
            devices.Add(mainFirewall);
            devices.AddRange(departmentSwitches);
            devices.AddRange(servers);
            devices.AddRange(workstations);
            return devices;
        }
    }
}
