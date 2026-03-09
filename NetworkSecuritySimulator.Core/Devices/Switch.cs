using System.Collections.Generic;

namespace NetworkSecuritySimulator.Core.Devices
{
    /// <summary>
    /// Represents a network switch device
    /// Switches connect multiple devices and can have port security vulnerabilities
    /// </summary>
    public class Switch : Device
    {
        /// <summary>
        /// Gets or sets the number of ports available on this switch
        /// </summary>
        public int NumberOfPorts { get; set; }

        /// <summary>
        /// Gets or sets the number of currently used ports
        /// </summary>
        public int UsedPorts { get; set; }

        /// <summary>
        /// Gets or sets whether port security is enabled
        /// </summary>
        public bool PortSecurityEnabled { get; set; }

        /// <summary>
        /// Gets or sets the list of connected device IDs
        /// </summary>
        public List<string> ConnectedDevices { get; set; }

        /// <summary>
        /// Constructor for Switch
        /// </summary>
        public Switch()
        {
            ConnectedDevices = new List<string>();
            NumberOfPorts = 48;
            UsedPorts = 0;
            PortSecurityEnabled = false;
            DeviceName = "Switch";
            Vulnerabilities.Add("MAC Flooding Vulnerable"); // Hidden vulnerability
        }

        /// <summary>
        /// Connects a device to this switch
        /// </summary>
        /// <param name="deviceId">The ID of the device to connect</param>
        public void ConnectDevice(string deviceId)
        {
            if (UsedPorts < NumberOfPorts && !ConnectedDevices.Contains(deviceId))
            {
                ConnectedDevices.Add(deviceId);
                UsedPorts++;
            }
        }

        /// <summary>
        /// Disconnects a device from this switch
        /// </summary>
        /// <param name="deviceId">The ID of the device to disconnect</param>
        public void DisconnectDevice(string deviceId)
        {
            if (ConnectedDevices.Contains(deviceId))
            {
                ConnectedDevices.Remove(deviceId);
                UsedPorts--;
            }
        }

        /// <summary>
        /// Enables port security on the switch
        /// This is a defense mechanism to prevent MAC flooding attacks
        /// </summary>
        public void EnablePortSecurity()
        {
            PortSecurityEnabled = true;
            ApplyDefense("Port Security");
        }

        /// <summary>
        /// Disables port security on the switch
        /// </summary>
        public void DisablePortSecurity()
        {
            PortSecurityEnabled = false;
            RemoveDefense("Port Security");
        }
    }
}
