using System.Collections.Generic;

namespace NetworkSecuritySimulator.Core.Devices
{
    /// <summary>
    /// Represents a network router device
    /// Routers manage traffic between subnets and can have misconfiguration vulnerabilities
    /// </summary>
    public class Router : Device
    {
        /// <summary>
        /// Gets or sets the routing table
        /// Maps destination subnet to next hop IP address
        /// </summary>
        public Dictionary<string, string> RoutingTable { get; set; }

        /// <summary>
        /// Gets or sets whether the router is properly configured
        /// Misconfigured routers are vulnerable to routing attacks
        /// </summary>
        public bool IsProperlyConfigured { get; set; }

        /// <summary>
        /// Gets or sets whether OSPF (Open Shortest Path First) is enabled
        /// </summary>
        public bool OspfEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of connected interfaces
        /// </summary>
        public int InterfaceCount { get; set; }

        /// <summary>
        /// Constructor for Router
        /// </summary>
        public Router()
        {
            RoutingTable = new Dictionary<string, string>();
            IsProperlyConfigured = true;
            OspfEnabled = false;
            InterfaceCount = 0;
            DeviceName = "Router";
            Vulnerabilities.Add("Misconfiguration Possible"); // Hidden vulnerability
        }

        /// <summary>
        /// Adds a route to the routing table
        /// </summary>
        /// <param name="destination">Destination subnet (e.g., "192.168.1.0/24")</param>
        /// <param name="nextHop">Next hop IP address</param>
        public void AddRoute(string destination, string nextHop)
        {
            if (!RoutingTable.ContainsKey(destination))
            {
                RoutingTable.Add(destination, nextHop);
            }
        }

        /// <summary>
        /// Removes a route from the routing table
        /// </summary>
        /// <param name="destination">Destination subnet to remove</param>
        public void RemoveRoute(string destination)
        {
            if (RoutingTable.ContainsKey(destination))
            {
                RoutingTable.Remove(destination);
            }
        }

        /// <summary>
        /// Creates a misconfiguration vulnerability in the router
        /// This can be exploited by attackers to redirect traffic
        /// </summary>
        public void CreateMisconfiguration()
        {
            IsProperlyConfigured = false;
            AddVulnerability("Default Gateway Misconfigured");
        }

        /// <summary>
        /// Fixes the misconfiguration
        /// This is a remediation step that can reduce vulnerability
        /// </summary>
        public void FixMisconfiguration()
        {
            IsProperlyConfigured = true;
            RemoveVulnerability("Default Gateway Misconfigured");
        }

        /// <summary>
        /// Enables OSPF protocol for dynamic routing
        /// </summary>
        public void EnableOspf()
        {
            OspfEnabled = true;
            ApplyDefense("OSPF Routing Protocol");
        }
    }
}
