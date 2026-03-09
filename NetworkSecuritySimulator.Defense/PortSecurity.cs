using NetworkSecuritySimulator.Core.Interfaces;
using System;

namespace NetworkSecuritySimulator.Defense
{
    /// <summary>
    /// Port Security Defense
    /// 
    /// Description:
    /// Port security is a network security feature that restricts which
    /// MAC addresses can connect to an interface on a network switch.
    /// It prevents MAC flooding attacks by limiting the number of MAC
    /// addresses that can be learned on a per-port basis.
    /// 
    /// Effectiveness: 95% against MAC flooding attacks
    /// Target Device: Switch
    /// Cost: Low (minimal performance impact)
    /// </summary>
    public class PortSecurity : IDefense
    {
        public string DefenseName => "Port Security";
        public string Description => "Restricts MAC addresses that can connect to switch ports";
        public int EffectivenessLevel => 95;

        private int maxMacAddressesPerPort;
        private bool violationActionEnabled;

        /// <summary>
        /// Constructor for Port Security
        /// </summary>
        public PortSecurity()
        {
            maxMacAddressesPerPort = 1;
            violationActionEnabled = true;
        }

        /// <summary>
        /// Applies port security to a target device (Switch)
        /// </summary>
        public bool Apply(string targetDeviceId)
        {
            // TODO: Implement port security application logic
            //
            // Steps:
            // 1. Get device from simulation engine
            // 2. Verify it's a Switch device
            // 3. Set switch.PortSecurityEnabled = true
            // 4. Configure maximum MAC addresses per port (default: 1)
            // 5. Enable violation action (shutdown or restrict)
            // 6. Log the defense application: "Port security enabled on switch"
            // 7. Return success/failure
            //
            // Additional logic:
            // - If device not found: return false
            // - If device is not a Switch: return false
            // - If already enabled: return true (idempotent)
            //
            // Effect on attacks:
            // - Prevents MAC flooding attacks from succeeding
            // - Reduces attack success rate from 95% to 5%

            return true; // Placeholder
        }

        /// <summary>
        /// Removes port security from a target device
        /// </summary>
        public bool Remove(string targetDeviceId)
        {
            // TODO: Implement port security removal logic
            //
            // Steps:
            // 1. Get device from simulation engine
            // 2. Verify it's a Switch device
            // 3. Set switch.PortSecurityEnabled = false
            // 4. Clear learned MAC addresses
            // 5. Log the change: "Port security disabled on switch"
            // 6. Return success/failure
            //
            // Side effects:
            // - Device becomes vulnerable to MAC flooding attacks again
            // - Increases attack success rate to 95%

            return true; // Placeholder
        }

        /// <summary>
        /// Sets the maximum number of MAC addresses per port
        /// </summary>
        public void SetMaxMacAddressesPerPort(int max)
        {
            maxMacAddressesPerPort = Math.Max(1, max);
        }

        /// <summary>
        /// Enables or disables violation action
        /// </summary>
        public void SetViolationAction(bool enabled)
        {
            violationActionEnabled = enabled;
        }
    }
}
