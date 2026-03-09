using NetworkSecuritySimulator.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Defense
{
    /// <summary>
    /// Manages all available defense mechanisms and their application
    /// </summary>
    public class DefenseManager
    {
        private List<IDefense> availableDefenses;
        private Dictionary<string, List<string>> appliedDefenses; // DeviceId -> List of defense names

        /// <summary>
        /// Event triggered when a defense is applied
        /// </summary>
        public event EventHandler<DefenseAppliedEventArgs> DefenseApplied;

        /// <summary>
        /// Event triggered when a defense is removed
        /// </summary>
        public event EventHandler<DefenseRemovedEventArgs> DefenseRemoved;

        /// <summary>
        /// Constructor for DefenseManager
        /// </summary>
        public DefenseManager()
        {
            availableDefenses = new List<IDefense>();
            appliedDefenses = new Dictionary<string, List<string>>();
            InitializeDefenses();
        }

        /// <summary>
        /// Initializes and registers all available defenses
        /// </summary>
        private void InitializeDefenses()
        {
            availableDefenses.Add(new PortSecurity());
            availableDefenses.Add(new FirewallRules());
            // TODO: Add more defense types:
            // - Access Control Lists (ACL)
            // - VLANs
            // - Intrusion Detection/Prevention
            // - Encryption
            // - Multi-factor authentication
        }

        /// <summary>
        /// Gets a specific defense by name
        /// </summary>
        /// <param name="defenseName">Name of the defense</param>
        /// <returns>The defense interface, or null if not found</returns>
        public IDefense GetDefense(string defenseName)
        {
            return availableDefenses.Find(d => d.DefenseName.Equals(defenseName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets all available defenses
        /// </summary>
        /// <returns>List of all defenses</returns>
        public List<IDefense> GetAllDefenses()
        {
            return new List<IDefense>(availableDefenses);
        }

        /// <summary>
        /// Applies a specific defense to a target device
        /// </summary>
        /// <param name="defenseName">Name of the defense to apply</param>
        /// <param name="targetDeviceId">ID of the target device</param>
        /// <returns>True if defense was applied successfully, false otherwise</returns>
        public bool ApplyDefense(string defenseName, string targetDeviceId)
        {
            var defense = GetDefense(defenseName);
            if (defense == null)
                return false;

            bool success = defense.Apply(targetDeviceId);
            
            if (success)
            {
                if (!appliedDefenses.ContainsKey(targetDeviceId))
                    appliedDefenses[targetDeviceId] = new List<string>();

                if (!appliedDefenses[targetDeviceId].Contains(defenseName))
                    appliedDefenses[targetDeviceId].Add(defenseName);

                // Trigger event
                DefenseApplied?.Invoke(this, new DefenseAppliedEventArgs
                {
                    DefenseName = defenseName,
                    TargetDeviceId = targetDeviceId,
                    Timestamp = DateTime.Now
                });
            }

            return success;
        }

        /// <summary>
        /// Removes a specific defense from a target device
        /// </summary>
        /// <param name="defenseName">Name of the defense to remove</param>
        /// <param name="targetDeviceId">ID of the target device</param>
        /// <returns>True if defense was removed successfully, false otherwise</returns>
        public bool RemoveDefense(string defenseName, string targetDeviceId)
        {
            var defense = GetDefense(defenseName);
            if (defense == null)
                return false;

            bool success = defense.Remove(targetDeviceId);
            
            if (success && appliedDefenses.ContainsKey(targetDeviceId))
            {
                appliedDefenses[targetDeviceId].Remove(defenseName);

                // Trigger event
                DefenseRemoved?.Invoke(this, new DefenseRemovedEventArgs
                {
                    DefenseName = defenseName,
                    TargetDeviceId = targetDeviceId,
                    Timestamp = DateTime.Now
                });
            }

            return success;
        }

        /// <summary>
        /// Gets all applied defenses for a specific device
        /// </summary>
        /// <param name="targetDeviceId">The device ID</param>
        /// <returns>List of applied defense names</returns>
        public List<string> GetAppliedDefenses(string targetDeviceId)
        {
            return appliedDefenses.ContainsKey(targetDeviceId) 
                ? new List<string>(appliedDefenses[targetDeviceId]) 
                : new List<string>();
        }

        /// <summary>
        /// Checks if a specific defense is applied to a device
        /// </summary>
        /// <param name="defenseName">Defense name</param>
        /// <param name="targetDeviceId">Device ID</param>
        /// <returns>True if defense is applied, false otherwise</returns>
        public bool IsDefenseApplied(string defenseName, string targetDeviceId)
        {
            return appliedDefenses.ContainsKey(targetDeviceId) && 
                   appliedDefenses[targetDeviceId].Contains(defenseName);
        }

        /// <summary>
        /// Registers a custom defense
        /// </summary>
        /// <param name="defense">The defense to register</param>
        public void RegisterDefense(IDefense defense)
        {
            if (defense != null && !availableDefenses.Contains(defense))
            {
                availableDefenses.Add(defense);
            }
        }

        /// <summary>
        /// Gets defense statistics
        /// </summary>
        /// <returns>Dictionary of statistics</returns>
        public Dictionary<string, object> GetStatistics()
        {
            return new Dictionary<string, object>
            {
                { "TotalDefenses", availableDefenses.Count },
                { "DevicesWithDefenses", appliedDefenses.Count },
                { "TotalDefensesApplied", CalculateTotalDefensesApplied() },
                { "AverageDefensesPerDevice", appliedDefenses.Count > 0 ? (double)CalculateTotalDefensesApplied() / appliedDefenses.Count : 0 }
            };
        }

        /// <summary>
        /// Calculates total number of applied defenses
        /// </summary>
        private int CalculateTotalDefensesApplied()
        {
            int total = 0;
            foreach (var defenses in appliedDefenses.Values)
            {
                total += defenses.Count;
            }
            return total;
        }
    }

    /// <summary>
    /// Event arguments for defense application
    /// </summary>
    public class DefenseAppliedEventArgs : EventArgs
    {
        public string DefenseName { get; set; }
        public string TargetDeviceId { get; set; }
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Event arguments for defense removal
    /// </summary>
    public class DefenseRemovedEventArgs : EventArgs
    {
        public string DefenseName { get; set; }
        public string TargetDeviceId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
