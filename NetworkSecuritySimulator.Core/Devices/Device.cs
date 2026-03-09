using NetworkSecuritySimulator.Core.States;
using System;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Core.Devices
{
    /// <summary>
    /// Base class for all network devices
    /// This is an abstract class that defines common properties and methods
    /// </summary>
    public abstract class Device
    {
        /// <summary>
        /// Gets or sets the unique identifier for this device
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the device (e.g., "PC-01", "Router-Main")
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or sets the current state of the device
        /// </summary>
        public DeviceState State { get; set; }

        /// <summary>
        /// Gets or sets the subnet/network this device belongs to
        /// </summary>
        public string Subnet { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the device
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the MAC address of the device
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// List of vulnerabilities present in this device
        /// </summary>
        public List<string> Vulnerabilities { get; set; }

        /// <summary>
        /// List of active defenses applied to this device
        /// </summary>
        public List<string> ActiveDefenses { get; set; }

        /// <summary>
        /// Indicates if this device is connected to the network
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// Timestamp of when this device was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Constructor for Device base class
        /// </summary>
        protected Device()
        {
            DeviceId = Guid.NewGuid().ToString();
            State = DeviceState.Normal;
            Vulnerabilities = new List<string>();
            ActiveDefenses = new List<string>();
            IsConnected = true;
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Virtual method to add a vulnerability to this device
        /// </summary>
        /// <param name="vulnerability">The vulnerability to add</param>
        public virtual void AddVulnerability(string vulnerability)
        {
            if (!Vulnerabilities.Contains(vulnerability))
            {
                Vulnerabilities.Add(vulnerability);
                // When a vulnerability is added, mark device as Vulnerable
                if (State == DeviceState.Normal)
                {
                    State = DeviceState.Vulnerable;
                }
            }
        }

        /// <summary>
        /// Virtual method to remove a vulnerability from this device
        /// </summary>
        /// <param name="vulnerability">The vulnerability to remove</param>
        public virtual void RemoveVulnerability(string vulnerability)
        {
            Vulnerabilities.Remove(vulnerability);
            // If no vulnerabilities remain, return to Normal state
            if (Vulnerabilities.Count == 0 && State == DeviceState.Vulnerable)
            {
                State = DeviceState.Normal;
            }
        }

        /// <summary>
        /// Virtual method to apply a defense mechanism
        /// </summary>
        /// <param name="defense">The defense mechanism to apply</param>
        public virtual void ApplyDefense(string defense)
        {
            if (!ActiveDefenses.Contains(defense))
            {
                ActiveDefenses.Add(defense);
            }
        }

        /// <summary>
        /// Virtual method to remove a defense mechanism
        /// </summary>
        /// <param name="defense">The defense mechanism to remove</param>
        public virtual void RemoveDefense(string defense)
        {
            ActiveDefenses.Remove(defense);
        }

        /// <summary>
        /// Virtual method to get device information as a string
        /// </summary>
        /// <returns>String representation of the device</returns>
        public override string ToString()
        {
            return $"Device: {DeviceName} ({DeviceId}) - State: {State} - IP: {IpAddress}";
        }
    }
}
