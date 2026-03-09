using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Data Center Network Topology
    /// 
    /// Network Structure:
    /// High-availability, redundant data center infrastructure.
    /// 
    /// Components:
    /// - Core Layer: 2 core routers (mesh)
    /// - Distribution Layer: 2 distribution switches
    /// - Core Firewall (DMZ/Internal)
    /// - 2 Web Servers (load balanced)
    /// - 2 Database Servers (replicated)
    /// - 2 Application Servers
    /// - 1 Backup Server
    /// - Management network with admin workstations
    /// 
    /// Hidden Vulnerabilities:
    /// - Web servers with unpatched web framework
    /// - Database replication traffic unencrypted
    /// - Backup server with weak access control
    /// - Core router with misconfiguration
    /// 
    /// Attack Vectors:
    /// - Web server exploitation
    /// - Database replication interception
    /// - Backup data exfiltration
    /// - Core routing attack
    /// </summary>
    public class DataCenter
    {
        public string TopologyName => "Data Center Network";
        public string Description => "High-availability redundant data center infrastructure";

        private List<Router> coreRouters;
        private List<Switch> distributionSwitches;
        private Firewall coreFirewall;
        private List<PC> servers;
        private List<PC> adminWorkstations;

        public DataCenter()
        {
            coreRouters = new List<Router>();
            distributionSwitches = new List<Switch>();
            servers = new List<PC>();
            adminWorkstations = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create core routers (mesh)
            for (int i = 1; i <= 2; i++)
            {
                var router = new Router
                {
                    DeviceName = $"DC-Core-Router-{i}",
                    IpAddress = $"10.100.0.{i}",
                    MacAddress = $"00:00:00:05:00:{i:X2}",
                    Subnet = "10.100.0.0/24"
                };
                coreRouters.Add(router);
            }

            // Create core firewall
            coreFirewall = new Firewall
            {
                DeviceName = "DC-Core-Firewall",
                IpAddress = "10.100.1.1",
                MacAddress = "00:00:00:05:01:FF",
                Subnet = "10.100.1.0/24"
            };

            // Create distribution switches
            for (int i = 1; i <= 2; i++)
            {
                var sw = new Switch
                {
                    DeviceName = $"DC-Distribution-Switch-{i}",
                    IpAddress = $"10.100.{i}.1",
                    MacAddress = $"00:00:00:05:0{i}:FF",
                    Subnet = $"10.100.{i}.0/24",
                    NumberOfPorts = 48
                };
                distributionSwitches.Add(sw);
            }

            // Create servers
            // Web servers
            for (int i = 1; i <= 2; i++)
            {
                var ws = new PC
                {
                    DeviceName = $"Web-Server-{i}",
                    IpAddress = $"10.100.10.{i}",
                    MacAddress = $"00:00:00:05:0A:{i:X2}",
                    Subnet = "10.100.10.0/24",
                    PrimaryUser = "WebServiceUser"
                };
                ws.DisablePatch(); // Web framework vulnerability
                servers.Add(ws);
            }

            // Database servers
            for (int i = 1; i <= 2; i++)
            {
                var ds = new PC
                {
                    DeviceName = $"Database-Server-{i}",
                    IpAddress = $"10.100.20.{i}",
                    MacAddress = $"00:00:00:05:14:{i:X2}",
                    Subnet = "10.100.20.0/24",
                    PrimaryUser = "DBServiceUser",
                    SensitiveDataGB = 1000
                };
                servers.Add(ds);
            }

            // Application servers
            for (int i = 1; i <= 2; i++)
            {
                var appSrv = new PC
                {
                    DeviceName = $"App-Server-{i}",
                    IpAddress = $"10.100.30.{i}",
                    MacAddress = $"00:00:00:05:1E:{i:X2}",
                    Subnet = "10.100.30.0/24",
                    PrimaryUser = "AppServiceUser"
                };
                servers.Add(appSrv);
            }

            // Backup server
            var backupSrv = new PC
            {
                DeviceName = "Backup-Server",
                IpAddress = "10.100.40.1",
                MacAddress = "00:00:00:05:28:01",
                Subnet = "10.100.40.0/24",
                PrimaryUser = "BackupAdmin",
                SensitiveDataGB = 2000
            };
            servers.Add(backupSrv);

            // Admin workstations
            for (int i = 1; i <= 3; i++)
            {
                var admin = new PC
                {
                    DeviceName = $"Admin-Workstation-{i}",
                    IpAddress = $"10.100.50.{i}",
                    MacAddress = $"00:00:00:05:32:{i:X2}",
                    Subnet = "10.100.50.0/24",
                    PrimaryUser = $"Administrator{i}"
                };
                adminWorkstations.Add(admin);
            }

            // Add vulnerability to core router
            coreRouters[0].CreateMisconfiguration();
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            devices.AddRange(coreRouters);
            devices.Add(coreFirewall);
            devices.AddRange(distributionSwitches);
            devices.AddRange(servers);
            devices.AddRange(adminWorkstations);
            return devices;
        }
    }
}
