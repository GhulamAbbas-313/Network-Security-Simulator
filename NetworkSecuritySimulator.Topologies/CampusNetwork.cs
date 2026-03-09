using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Campus Network Topology
    /// 
    /// Network Structure:
    /// University/college campus with multiple buildings and networks.
    /// 
    /// Components:
    /// - Core network: 2 routers in redundancy
    /// - 3 Building switches (Admin, Academic, Dormitory)
    /// - 2 Firewalls (ingress/egress)
    /// - 20 Workstations distributed across buildings
    /// - 3 Servers (Web, Database, Authentication)
    /// - Wireless access points (simulation as devices)
    /// 
    /// Hidden Vulnerabilities:
    /// - Dormitory network with minimal security
    /// - Unpatched academic workstations
    /// - Weak access control on guest network
    /// - Database server with default credentials
    /// 
    /// Attack Vectors:
    /// - Lateral movement from dormitory to academic
    /// - Unauthorized access to database server
    /// - Building-to-building bridge compromise
    /// </summary>
    public class CampusNetwork
    {
        public string TopologyName => "Campus Network";
        public string Description => "University campus network across multiple buildings";

        private List<Router> coreRouters;
        private List<Switch> buildingSwitches;
        private List<Firewall> firewalls;
        private List<PC> servers;
        private List<PC> workstations;

        public CampusNetwork()
        {
            coreRouters = new List<Router>();
            buildingSwitches = new List<Switch>();
            firewalls = new List<Firewall>();
            servers = new List<PC>();
            workstations = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create core routers
            for (int i = 1; i <= 2; i++)
            {
                var router = new Router
                {
                    DeviceName = $"Campus-Core-{i}",
                    IpAddress = $"10.1.0.{i}",
                    MacAddress = $"00:00:00:04:00:{i:X2}",
                    Subnet = "10.1.0.0/24"
                };
                coreRouters.Add(router);
            }

            // Create firewalls
            for (int i = 1; i <= 2; i++)
            {
                var fw = new Firewall
                {
                    DeviceName = i == 1 ? "Ingress-Firewall" : "Egress-Firewall",
                    IpAddress = $"10.1.1.{i}",
                    MacAddress = $"00:00:00:04:01:{i:X2}",
                    Subnet = "10.1.1.0/24"
                };
                firewalls.Add(fw);
            }

            // Create building switches
            string[] buildings = { "Admin-Building", "Academic-Building", "Dormitory-Building" };
            for (int i = 0; i < buildings.Length; i++)
            {
                var sw = new Switch
                {
                    DeviceName = $"{buildings[i]}-Switch",
                    IpAddress = $"192.168.{i + 1}.1",
                    MacAddress = $"00:00:00:04:0{i + 1}:FF",
                    Subnet = $"192.168.{i + 1}.0/24",
                    NumberOfPorts = 48
                };
                buildingSwitches.Add(sw);
            }

            // Create servers
            var webServer = new PC
            {
                DeviceName = "Web-Server",
                IpAddress = "10.1.2.10",
                MacAddress = "00:00:00:04:02:0A",
                Subnet = "10.1.2.0/24",
                PrimaryUser = "WebAdmin"
            };
            servers.Add(webServer);

            var dbServer = new PC
            {
                DeviceName = "Database-Server",
                IpAddress = "10.1.2.11",
                MacAddress = "00:00:00:04:02:0B",
                Subnet = "10.1.2.0/24",
                PrimaryUser = "DBAdmin"
            };
            dbServer.DisablePatch(); // Unpatched vulnerability
            servers.Add(dbServer);

            var authServer = new PC
            {
                DeviceName = "Auth-Server",
                IpAddress = "10.1.2.12",
                MacAddress = "00:00:00:04:02:0C",
                Subnet = "10.1.2.0/24",
                PrimaryUser = "AuthAdmin"
            };
            servers.Add(authServer);

            // Create workstations (20 total, distributed)
            int wsCounter = 1;
            int[] wsPerBuilding = { 6, 8, 6 }; // Admin, Academic, Dormitory
            for (int i = 0; i < buildingSwitches.Count; i++)
            {
                for (int j = 1; j <= wsPerBuilding[i]; j++)
                {
                    var ws = new PC
                    {
                        DeviceName = $"{buildings[i]}-WS-{j:D2}",
                        IpAddress = $"192.168.{i + 1}.{10 + j}",
                        MacAddress = $"00:00:00:04:0{i + 1}:{10 + j:X2}",
                        Subnet = $"192.168.{i + 1}.0/24",
                        PrimaryUser = $"User{wsCounter}"
                    };

                    // Dormitory has less security
                    if (i == 2)
                    {
                        ws.DeactivateAntivirus();
                    }

                    workstations.Add(ws);
                    buildingSwitches[i].ConnectDevice(ws.DeviceId);
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
            devices.AddRange(firewalls);
            devices.AddRange(buildingSwitches);
            devices.AddRange(servers);
            devices.AddRange(workstations);
            return devices;
        }
    }
}
