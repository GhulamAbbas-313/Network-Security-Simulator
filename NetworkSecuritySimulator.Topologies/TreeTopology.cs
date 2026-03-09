using NetworkSecuritySimulator.Core.Devices;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Topologies
{
    /// <summary>
    /// Tree Topology
    /// 
    /// Network Structure:
    /// Hierarchical organization with root switch and branch switches.
    /// Resembles a tree structure (also called hierarchical star).
    /// 
    /// Components:
    /// - 1 Root Switch
    /// - 3 Branch Switches (level 2)
    /// - 12 PC devices (connected to branch switches)
    /// - Well-organized but single points of failure
    /// 
    /// Hidden Vulnerabilities:
    /// - Root switch without port security
    /// - Branch switches with weak configurations
    /// - Unpatched devices throughout
    /// 
    /// Attack Vectors:
    /// - MAC flooding on root switch (affects all branches)
    /// - Spanning tree attacks
    /// - Unauthorized access to branch switches
    /// </summary>
    public class TreeTopology
    {
        public string TopologyName => "Tree";
        public string Description => "Hierarchical topology with root and branch switches";

        private Switch rootSwitch;
        private List<Switch> branchSwitches;
        private List<PC> devices;

        public TreeTopology()
        {
            branchSwitches = new List<Switch>();
            devices = new List<PC>();
        }

        /// <summary>
        /// Builds the topology
        /// </summary>
        public void BuildTopology()
        {
            // Create root switch
            rootSwitch = new Switch
            {
                DeviceName = "Root-Switch",
                IpAddress = "192.168.0.1",
                MacAddress = "00:00:00:00:00:FF",
                Subnet = "192.168.0.0/24",
                NumberOfPorts = 48
            };

            // Create 3 branch switches
            for (int i = 1; i <= 3; i++)
            {
                var branch = new Switch
                {
                    DeviceName = $"Branch-Switch-{i}",
                    IpAddress = $"192.168.{i}.1",
                    MacAddress = $"00:00:00:00:0{i}:FF",
                    Subnet = $"192.168.{i}.0/24",
                    NumberOfPorts = 24
                };
                branchSwitches.Add(branch);
                rootSwitch.ConnectDevice(branch.DeviceId);
            }

            // Create 12 PC devices (4 per branch)
            int pcCounter = 1;
            for (int i = 0; i < branchSwitches.Count; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    var pc = new PC
                    {
                        DeviceName = $"PC-Tree-{pcCounter:D2}",
                        IpAddress = $"192.168.{i + 1}.{10 + j}",
                        MacAddress = $"00:00:00:00:0{i + 1}:{10 + j:X2}",
                        Subnet = $"192.168.{i + 1}.0/24",
                        PrimaryUser = $"User{pcCounter}"
                    };
                    devices.Add(pc);
                    branchSwitches[i].ConnectDevice(pc.DeviceId);
                    pcCounter++;
                }
            }
        }

        /// <summary>
        /// Gets all devices in the topology
        /// </summary>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            if (rootSwitch != null)
                devices.Add(rootSwitch);
            devices.AddRange(branchSwitches);
            devices.AddRange(this.devices);
            return devices;
        }
    }
}
