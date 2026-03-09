using NetworkSecuritySimulator.Core.Devices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetworkSecuritySimulator.Core.Engine
{
    /// <summary>
    /// Central simulation engine that manages the entire network security simulation
    /// Responsible for:
    /// - Loading network topologies
    /// - Managing device states
    /// - Executing attacks
    /// - Applying defenses
    /// - Generating logs
    /// </summary>
    public class SimulationEngine
    {
        private Dictionary<string, Device> devices;
        private StateManager stateManager;
        private List<string> simulationLogs;
        private bool isRunning;
        private DateTime startTime;

        /// <summary>
        /// Event triggered when a device state changes
        /// </summary>
        public event EventHandler<DeviceStateChangedEventArgs> DeviceStateChanged;

        /// <summary>
        /// Event triggered when a log entry is generated
        /// </summary>
        public event EventHandler<LogEntryEventArgs> LogGenerated;

        /// <summary>
        /// Constructor for SimulationEngine
        /// </summary>
        public SimulationEngine()
        {
            devices = new Dictionary<string, Device>();
            stateManager = new StateManager();
            simulationLogs = new List<string>();
            isRunning = false;
        }

        /// <summary>
        /// Starts the simulation
        /// </summary>
        public void StartSimulation()
        {
            isRunning = true;
            startTime = DateTime.Now;
            AddLog("INFO", "System", "Simulation started");
        }

        /// <summary>
        /// Stops the simulation
        /// </summary>
        public void StopSimulation()
        {
            isRunning = false;
            AddLog("INFO", "System", "Simulation stopped");
        }

        /// <summary>
        /// Gets whether the simulation is currently running
        /// </summary>
        public bool IsRunning => isRunning;

        /// <summary>
        /// Registers a device in the simulation
        /// </summary>
        /// <param name="device">The device to register</param>
        public void RegisterDevice(Device device)
        {
            if (device != null && !devices.ContainsKey(device.DeviceId))
            {
                devices[device.DeviceId] = device;
                stateManager.RegisterDevice(device.DeviceId, device.State);
                AddLog("INFO", "System", $"Device registered: {device.DeviceName}");
            }
        }

        /// <summary>
        /// Gets a device by its ID
        /// </summary>
        /// <param name="deviceId">The device ID</param>
        /// <returns>The device, or null if not found</returns>
        public Device GetDevice(string deviceId)
        {
            return devices.ContainsKey(deviceId) ? devices[deviceId] : null;
        }

        /// <summary>
        /// Gets all devices in the simulation
        /// </summary>
        /// <returns>List of all devices</returns>
        public List<Device> GetAllDevices()
        {
            return devices.Values.ToList();
        }

        /// <summary>
        /// Gets the count of devices by type
        /// </summary>
        /// <returns>Dictionary of device type counts</returns>
        public Dictionary<string, int> GetDeviceTypeCount()
        {
            var counts = new Dictionary<string, int>();
            foreach (var device in devices.Values)
            {
                string type = device.GetType().Name;
                if (!counts.ContainsKey(type))
                    counts[type] = 0;
                counts[type]++;
            }
            return counts;
        }

        /// <summary>
        /// Adds a log entry to the simulation logs
        /// </summary>
        /// <param name="severity">Severity level (INFO, WARNING, CRITICAL)</param>
        /// <param name="source">Source of the log (device name or system)</param>
        /// <param name="message">Log message</param>
        public void AddLog(string severity, string source, string message)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{severity}] [{source}] {message}";
            simulationLogs.Add(logEntry);
            
            // Trigger log generated event
            LogGenerated?.Invoke(this, new LogEntryEventArgs { LogEntry = logEntry });
        }

        /// <summary>
        /// Gets all simulation logs
        /// </summary>
        /// <returns>List of log entries</returns>
        public List<string> GetAllLogs()
        {
            return new List<string>(simulationLogs);
        }

        /// <summary>
        /// Clears all simulation logs
        /// </summary>
        public void ClearLogs()
        {
            simulationLogs.Clear();
        }

        /// <summary>
        /// Gets logs filtered by severity level
        /// </summary>
        /// <param name="severity">Severity level to filter by</param>
        /// <returns>List of matching logs</returns>
        public List<string> GetLogsBySeverity(string severity)
        {
            return simulationLogs.Where(log => log.Contains($"[{severity}]")).ToList();
        }

        /// <summary>
        /// Gets the current state of all devices
        /// </summary>
        /// <returns>Dictionary of device IDs and their states</returns>
        public Dictionary<string, Core.States.DeviceState> GetAllDeviceStates()
        {
            return stateManager.GetAllDeviceStates();
        }

        /// <summary>
        /// Gets simulation statistics
        /// </summary>
        /// <returns>Dictionary of statistics</returns>
        public Dictionary<string, object> GetStatistics()
        {
            var stats = new Dictionary<string, object>
            {
                { "TotalDevices", devices.Count },
                { "TotalLogs", simulationLogs.Count },
                { "IsRunning", isRunning },
                { "StartTime", startTime },
                { "UpTime", DateTime.Now - startTime }
            };

            // Add device state counts
            var states = GetAllDeviceStates();
            stats["NormalDevices"] = states.Count(s => s.Value == Core.States.DeviceState.Normal);
            stats["VulnerableDevices"] = states.Count(s => s.Value == Core.States.DeviceState.Vulnerable);
            stats["CompromisedDevices"] = states.Count(s => s.Value == Core.States.DeviceState.Compromised);
            stats["OfflineDevices"] = states.Count(s => s.Value == Core.States.DeviceState.Offline);

            return stats;
        }

        /// <summary>
        /// Resets the entire simulation
        /// </summary>
        public void Reset()
        {
            devices.Clear();
            stateManager = new StateManager();
            simulationLogs.Clear();
            isRunning = false;
            AddLog("INFO", "System", "Simulation reset");
        }
    }

    /// <summary>
    /// Event arguments for device state changes
    /// </summary>
    public class DeviceStateChangedEventArgs : EventArgs
    {
        public string DeviceId { get; set; }
        public Core.States.DeviceState NewState { get; set; }
        public Core.States.DeviceState OldState { get; set; }
    }

    /// <summary>
    /// Event arguments for log entries
    /// </summary>
    public class LogEntryEventArgs : EventArgs
    {
        public string LogEntry { get; set; }
    }
}
