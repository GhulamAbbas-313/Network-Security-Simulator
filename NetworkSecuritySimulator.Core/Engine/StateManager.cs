using NetworkSecuritySimulator.Core.Devices;
using NetworkSecuritySimulator.Core.States;
using System;
using System.Collections.Generic;

namespace NetworkSecuritySimulator.Core.Engine
{
    /// <summary>
    /// Manages device state transitions
    /// Implements the state machine: Normal → Vulnerable → Compromised
    /// </summary>
    public class StateManager
    {
        private Dictionary<string, DeviceState> deviceStates;
        private Dictionary<string, int> vulnerabilityCount;

        /// <summary>
        /// Constructor for StateManager
        /// </summary>
        public StateManager()
        {
            deviceStates = new Dictionary<string, DeviceState>();
            vulnerabilityCount = new Dictionary<string, int>();
        }

        /// <summary>
        /// Registers a device with the state manager
        /// </summary>
        /// <param name="deviceId">The unique identifier of the device</param>
        /// <param name="initialState">The initial state of the device</param>
        public void RegisterDevice(string deviceId, DeviceState initialState = DeviceState.Normal)
        {
            if (!deviceStates.ContainsKey(deviceId))
            {
                deviceStates[deviceId] = initialState;
                vulnerabilityCount[deviceId] = 0;
            }
        }

        /// <summary>
        /// Gets the current state of a device
        /// </summary>
        /// <param name="deviceId">The device ID</param>
        /// <returns>The current device state</returns>
        public DeviceState GetDeviceState(string deviceId)
        {
            return deviceStates.ContainsKey(deviceId) ? deviceStates[deviceId] : DeviceState.Offline;
        }

        /// <summary>
        /// Transitions a device state based on vulnerabilities
        /// </summary>
        /// <param name="deviceId">The device ID</param>
        /// <param name="vulnerabilityAdded">True if vulnerability was added, false if removed</param>
        public void UpdateDeviceState(string deviceId, bool vulnerabilityAdded)
        {
            if (!deviceStates.ContainsKey(deviceId))
                return;

            if (vulnerabilityAdded)
            {
                vulnerabilityCount[deviceId]++;
            }
            else
            {
                vulnerabilityCount[deviceId] = Math.Max(0, vulnerabilityCount[deviceId] - 1);
            }

            // State machine logic
            if (vulnerabilityCount[deviceId] == 0)
            {
                deviceStates[deviceId] = DeviceState.Normal;
            }
            else if (vulnerabilityCount[deviceId] >= 1 && vulnerabilityCount[deviceId] < 3)
            {
                deviceStates[deviceId] = DeviceState.Vulnerable;
            }
            else if (vulnerabilityCount[deviceId] >= 3)
            {
                deviceStates[deviceId] = DeviceState.Compromised;
            }
        }

        /// <summary>
        /// Compromises a device (forces it to Compromised state)
        /// </summary>
        /// <param name="deviceId">The device ID</param>
        public void CompromiseDevice(string deviceId)
        {
            if (deviceStates.ContainsKey(deviceId))
            {
                deviceStates[deviceId] = DeviceState.Compromised;
                vulnerabilityCount[deviceId] = 10; // Set high vulnerability count
            }
        }

        /// <summary>
        /// Takes a device offline
        /// </summary>
        /// <param name="deviceId">The device ID</param>
        public void TakeDeviceOffline(string deviceId)
        {
            if (deviceStates.ContainsKey(deviceId))
            {
                deviceStates[deviceId] = DeviceState.Offline;
            }
        }

        /// <summary>
        /// Brings a device back online
        /// </summary>
        /// <param name="deviceId">The device ID</param>
        public void BringDeviceOnline(string deviceId)
        {
            if (deviceStates.ContainsKey(deviceId))
            {
                deviceStates[deviceId] = DeviceState.Normal;
                vulnerabilityCount[deviceId] = 0;
            }
        }

        /// <summary>
        /// Gets all devices and their current states
        /// </summary>
        /// <returns>Dictionary of device IDs and their states</returns>
        public Dictionary<string, DeviceState> GetAllDeviceStates()
        {
            return new Dictionary<string, DeviceState>(deviceStates);
        }

        /// <summary>
        /// Gets the number of vulnerabilities on a device
        /// </summary>
        /// <param name="deviceId">The device ID</param>
        /// <returns>Number of vulnerabilities</returns>
        public int GetVulnerabilityCount(string deviceId)
        {
            return vulnerabilityCount.ContainsKey(deviceId) ? vulnerabilityCount[deviceId] : 0;
        }
    }
}
