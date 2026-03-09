using NetworkSecuritySimulator.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Attacks
{
    /// <summary>
    /// Manages all available attacks and their execution
    /// </summary>
    public class AttackManager
    {
        private List<IAttack> availableAttacks;

        /// <summary>
        /// Event triggered when an attack is executed
        /// </summary>
        public event EventHandler<AttackExecutedEventArgs> AttackExecuted;

        /// <summary>
        /// Constructor for AttackManager
        /// </summary>
        public AttackManager()
        {
            availableAttacks = new List<IAttack>();
            InitializeAttacks();
        }

        /// <summary>
        /// Initializes and registers all available attacks
        /// </summary>
        private void InitializeAttacks()
        {
            availableAttacks.Add(new MacFloodingAttack());
            availableAttacks.Add(new UnauthorizedAccessAttack());
            availableAttacks.Add(new MisconfigurationAttack());
            // TODO: Add more attack types as needed
        }

        /// <summary>
        /// Gets a specific attack by name
        /// </summary>
        /// <param name="attackName">Name of the attack</param>
        /// <returns>The attack interface, or null if not found</returns>
        public IAttack GetAttack(string attackName)
        {
            return availableAttacks.Find(a => a.AttackName.Equals(attackName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets all available attacks
        /// </summary>
        /// <returns>List of all attacks</returns>
        public List<IAttack> GetAllAttacks()
        {
            return new List<IAttack>(availableAttacks);
        }

        /// <summary>
        /// Executes a specific attack on a target device
        /// </summary>
        /// <param name="attackName">Name of the attack to execute</param>
        /// <param name="targetDeviceId">ID of the target device</param>
        /// <returns>True if attack was successful, false otherwise</returns>
        public bool ExecuteAttack(string attackName, string targetDeviceId)
        {
            var attack = GetAttack(attackName);
            if (attack == null)
                return false;

            bool success = attack.Execute(targetDeviceId);
            
            // Trigger event
            AttackExecuted?.Invoke(this, new AttackExecutedEventArgs
            {
                AttackName = attackName,
                TargetDeviceId = targetDeviceId,
                Success = success,
                Timestamp = DateTime.Now
            });

            return success;
        }

        /// <summary>
        /// Registers a custom attack
        /// </summary>
        /// <param name="attack">The attack to register</param>
        public void RegisterAttack(IAttack attack)
        {
            if (attack != null && !availableAttacks.Contains(attack))
            {
                availableAttacks.Add(attack);
            }
        }

        /// <summary>
        /// Gets attack statistics
        /// </summary>
        /// <returns>Dictionary of statistics</returns>
        public Dictionary<string, object> GetStatistics()
        {
            return new Dictionary<string, object>
            {
                { "TotalAttacks", availableAttacks.Count },
                { "HighSeverityAttacks", availableAttacks.FindAll(a => a.SeverityLevel >= 7).Count },
                { "MediumSeverityAttacks", availableAttacks.FindAll(a => a.SeverityLevel >= 4 && a.SeverityLevel < 7).Count },
                { "LowSeverityAttacks", availableAttacks.FindAll(a => a.SeverityLevel < 4).Count }
            };
        }
    }

    /// <summary>
    /// Event arguments for attack execution
    /// </summary>
    public class AttackExecutedEventArgs : EventArgs
    {
        public string AttackName { get; set; }
        public string TargetDeviceId { get; set; }
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
