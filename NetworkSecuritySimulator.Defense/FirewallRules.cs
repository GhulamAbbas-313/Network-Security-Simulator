using NetworkSecuritySimulator.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Defense
{
    /// <summary>
    /// Firewall Rules Defense
    /// 
    /// Description:
    /// Firewall rules define which traffic is allowed or denied based on
    /// source/destination IP, port, and protocol. Well-configured firewall
    /// rules prevent unauthorized access and lateral movement.
    /// 
    /// Effectiveness: 80-95% depending on rule configuration
    /// Target Device: Firewall, Router
    /// Cost: Medium (processing overhead)
    /// </summary>
    public class FirewallRules : IDefense
    {
        public string DefenseName => "Firewall Rules";
        public string Description => "Defines allowed/blocked traffic based on rules";
        public int EffectivenessLevel => 85;

        private List<string> rules;
        private bool implicitDenyEnabled;

        /// <summary>
        /// Represents a single firewall rule
        /// </summary>
        public class Rule
        {
            public string SourceIP { get; set; }
            public string DestinationIP { get; set; }
            public string Protocol { get; set; }
            public int Port { get; set; }
            public string Action { get; set; } // ALLOW or DENY

            public override string ToString()
            {
                return $"{SourceIP} -> {DestinationIP}:{Port} ({Protocol}) : {Action}";
            }
        }

        /// <summary>
        /// Constructor for FirewallRules
        /// </summary>
        public FirewallRules()
        {
            rules = new List<string>();
            implicitDenyEnabled = true;
        }

        /// <summary>
        /// Applies firewall rules to a device
        /// </summary>
        public bool Apply(string targetDeviceId)
        {
            // TODO: Implement firewall rules application logic
            //
            // Steps:
            // 1. Get device from simulation engine
            // 2. Verify it's a Firewall or Router device
            // 3. Add comprehensive security rules:
            //    - Deny all incoming on sensitive ports (22, 23, 3389)
            //    - Allow outgoing HTTPS (443) and HTTP (80)
            //    - Allow DNS (53)
            //    - Deny all by default (implicit deny)
            // 4. Enable rule logging
            // 5. Log the defense application
            // 6. Return success/failure
            //
            // Rules to add:
            // - "ANY:ANY:TCP:22:DENY" (SSH)
            // - "ANY:ANY:TCP:23:DENY" (Telnet)
            // - "ANY:ANY:TCP:3389:DENY" (RDP)
            // - "ANY:ANY:TCP:80:ALLOW" (HTTP)
            // - "ANY:ANY:TCP:443:ALLOW" (HTTPS)
            // - "ANY:ANY:UDP:53:ALLOW" (DNS)

            return true; // Placeholder
        }

        /// <summary>
        /// Removes firewall rules from a device
        /// </summary>
        public bool Remove(string targetDeviceId)
        {
            // TODO: Implement firewall rules removal logic
            //
            // Steps:
            // 1. Get device from simulation engine
            // 2. Verify it's a Firewall or Router device
            // 3. Clear all rules
            // 4. Disable logging
            // 5. Log the change
            // 6. Return success/failure
            //
            // Side effects:
            // - Device becomes vulnerable to unauthorized access
            // - Misconfiguration attacks have higher success rate

            return true; // Placeholder
        }

        /// <summary>
        /// Adds a specific rule to the firewall
        /// </summary>
        public void AddRule(Rule rule)
        {
            rules.Add(rule.ToString());
        }

        /// <summary>
        /// Parses and adds a rule from string format
        /// </summary>
        public void AddRule(string sourceIP, string destIP, string protocol, int port, string action)
        {
            string rule = $"{sourceIP}:{destIP}:{protocol}:{port}:{action}";
            rules.Add(rule);
        }

        /// <summary>
        /// Gets all configured rules
        /// </summary>
        public List<string> GetRules()
        {
            return new List<string>(rules);
        }

        /// <summary>
        /// Sets implicit deny (default deny all traffic)
        /// </summary>
        public void SetImplicitDeny(bool enabled)
        {
            implicitDenyEnabled = enabled;
        }

        /// <summary>
        /// Gets rule count
        /// </summary>
        public int RuleCount => rules.Count;
    }
}
