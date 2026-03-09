using System.Collections.Generic;

namespace NetworkSecuritySimulator.Core.Devices
{
    /// <summary>
    /// Represents a firewall device
    /// Firewalls control incoming and outgoing network traffic based on rules
    /// </summary>
    public class Firewall : Device
    {
        /// <summary>
        /// Gets or sets whether the firewall is active/enabled
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the list of firewall rules
        /// Format: "SourceIP:DestinationIP:Protocol:Port:Action"
        /// </summary>
        public List<string> FirewallRules { get; set; }

        /// <summary>
        /// Gets or sets whether the firewall uses stateful inspection
        /// Stateful inspection is more effective than stateless filtering
        /// </summary>
        public bool StatefulInspectionEnabled { get; set; }

        /// <summary>
        /// Gets or sets the log of blocked traffic attempts
        /// </summary>
        public List<string> BlockedTrafficLog { get; set; }

        /// <summary>
        /// Gets or sets the number of packets processed
        /// </summary>
        public long PacketsProcessed { get; set; }

        /// <summary>
        /// Constructor for Firewall
        /// </summary>
        public Firewall()
        {
            IsActive = true;
            FirewallRules = new List<string>();
            StatefulInspectionEnabled = false;
            BlockedTrafficLog = new List<string>();
            PacketsProcessed = 0;
            DeviceName = "Firewall";
        }

        /// <summary>
        /// Adds a firewall rule
        /// </summary>
        /// <param name="rule">The rule to add (e.g., "ANY:10.0.0.0/8:TCP:22:DENY")</param>
        public void AddRule(string rule)
        {
            if (!FirewallRules.Contains(rule))
            {
                FirewallRules.Add(rule);
            }
        }

        /// <summary>
        /// Removes a firewall rule
        /// </summary>
        /// <param name="rule">The rule to remove</param>
        public void RemoveRule(string rule)
        {
            FirewallRules.Remove(rule);
        }

        /// <summary>
        /// Enables stateful inspection for more advanced filtering
        /// </summary>
        public void EnableStatefulInspection()
        {
            StatefulInspectionEnabled = true;
            ApplyDefense("Stateful Inspection");
        }

        /// <summary>
        /// Processes network traffic against firewall rules
        /// </summary>
        /// <param name="sourceIp">Source IP address</param>
        /// <param name="destinationIp">Destination IP address</param>
        /// <param name="protocol">Protocol (TCP, UDP, ICMP, etc.)</param>
        /// <param name="port">Port number</param>
        /// <returns>True if traffic is allowed, false if blocked</returns>
        public bool ProcessTraffic(string sourceIp, string destinationIp, string protocol, int port)
        {
            PacketsProcessed++;
            
            // TODO: Implement traffic filtering logic
            // Check against firewall rules
            // Return true if allowed, false if blocked
            // Log blocked attempts

            return true; // Placeholder
        }

        /// <summary>
        /// Activates the firewall
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        /// <summary>
        /// Deactivates the firewall
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }

        /// <summary>
        /// Gets the firewall status summary
        /// </summary>
        /// <returns>Status string</returns>
        public string GetStatus()
        {
            return $"Firewall {(IsActive ? "ACTIVE" : "INACTIVE")} - Rules: {FirewallRules.Count} - Packets Processed: {PacketsProcessed}";
        }
    }
}
