using System;
using System.Collections.Generic;
using System.Linq;

namespace NetworkSecuritySimulator.Logging
{
    /// <summary>
    /// Manages all log entries in the simulation
    /// Responsible for creating, storing, filtering, and exporting logs
    /// </summary>
    public class LogManager
    {
        private List<LogEntry> logEntries;
        private int maxLogEntries;

        /// <summary>
        /// Event triggered when a new log entry is created
        /// </summary>
        public event EventHandler<LogEntryAddedEventArgs> LogEntryAdded;

        /// <summary>
        /// Constructor for LogManager
        /// </summary>
        /// <param name="maxEntries">Maximum number of log entries to keep (default: 10000)</param>
        public LogManager(int maxEntries = 10000)
        {
            logEntries = new List<LogEntry>();
            maxLogEntries = maxEntries;
        }

        /// <summary>
        /// Adds a log entry to the log collection
        /// </summary>
        /// <param name="entry">The log entry to add</param>
        public void AddLog(LogEntry entry)
        {
            if (entry == null)
                return;

            logEntries.Add(entry);

            // Remove oldest entries if we exceed max capacity
            if (logEntries.Count > maxLogEntries)
            {
                logEntries.RemoveAt(0);
            }

            // Trigger event
            LogEntryAdded?.Invoke(this, new LogEntryAddedEventArgs { LogEntry = entry });
        }

        /// <summary>
        /// Creates and adds a log entry in one operation
        /// </summary>
        public void CreateLog(string deviceName, string deviceId, string eventType, SeverityLevel severity, string message, string source = "System")
        {
            var entry = new LogEntry(deviceName, deviceId, eventType, severity, message, source);
            AddLog(entry);
        }

        /// <summary>
        /// Gets all log entries
        /// </summary>
        /// <returns>List of all log entries</returns>
        public List<LogEntry> GetAllLogs()
        {
            return new List<LogEntry>(logEntries);
        }

        /// <summary>
        /// Gets logs filtered by severity level
        /// </summary>
        /// <param name="severity">Severity level to filter by</param>
        /// <returns>List of logs with matching severity</returns>
        public List<LogEntry> GetLogsBySeverity(SeverityLevel severity)
        {
            return logEntries.Where(log => log.Severity == severity).ToList();
        }

        /// <summary>
        /// Gets logs from a specific device
        /// </summary>
        /// <param name="deviceId">The device ID to filter by</param>
        /// <returns>List of logs from the specified device</returns>
        public List<LogEntry> GetLogsByDevice(string deviceId)
        {
            return logEntries.Where(log => log.DeviceId == deviceId).ToList();
        }

        /// <summary>
        /// Gets logs for a specific event type
        /// </summary>
        /// <param name="eventType">The event type to filter by</param>
        /// <returns>List of logs matching the event type</returns>
        public List<LogEntry> GetLogsByEventType(string eventType)
        {
            return logEntries.Where(log => log.EventType == eventType).ToList();
        }

        /// <summary>
        /// Gets logs within a specific time range
        /// </summary>
        /// <param name="startTime">Start of time range</param>
        /// <param name="endTime">End of time range</param>
        /// <returns>List of logs within the time range</returns>
        public List<LogEntry> GetLogsByTimeRange(DateTime startTime, DateTime endTime)
        {
            return logEntries.Where(log => log.Timestamp >= startTime && log.Timestamp <= endTime).ToList();
        }

        /// <summary>
        /// Gets the most recent N log entries
        /// </summary>
        /// <param name="count">Number of recent logs to retrieve</param>
        /// <returns>List of recent log entries</returns>
        public List<LogEntry> GetRecentLogs(int count)
        {
            return logEntries.TakeLast(count).ToList();
        }

        /// <summary>
        /// Searches logs by message content (case-insensitive)
        /// </summary>
        /// <param name="searchTerm">Term to search for in messages</param>
        /// <returns>List of matching logs</returns>
        public List<LogEntry> SearchLogs(string searchTerm)
        {
            return logEntries.Where(log => log.Message.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Gets statistics about the logs
        /// </summary>
        /// <returns>Dictionary containing log statistics</returns>
        public Dictionary<string, object> GetStatistics()
        {
            var stats = new Dictionary<string, object>
            {
                { "TotalLogs", logEntries.Count },
                { "CriticalLogs", logEntries.Count(l => l.Severity == SeverityLevel.CRITICAL) },
                { "WarningLogs", logEntries.Count(l => l.Severity == SeverityLevel.WARNING) },
                { "InfoLogs", logEntries.Count(l => l.Severity == SeverityLevel.INFO) },
                { "UniqueDevices", logEntries.Select(l => l.DeviceId).Distinct().Count() },
                { "UniqueEventTypes", logEntries.Select(l => l.EventType).Distinct().Count() }
            };

            if (logEntries.Count > 0)
            {
                stats["OldestLog"] = logEntries.First().Timestamp;
                stats["NewestLog"] = logEntries.Last().Timestamp;
            }

            return stats;
        }

        /// <summary>
        /// Clears all log entries
        /// </summary>
        public void ClearLogs()
        {
            logEntries.Clear();
        }

        /// <summary>
        /// Gets the total number of log entries
        /// </summary>
        public int LogCount => logEntries.Count;
    }

    /// <summary>
    /// Event arguments for when a log entry is added
    /// </summary>
    public class LogEntryAddedEventArgs : EventArgs
    {
        public LogEntry LogEntry { get; set; }
    }
}
