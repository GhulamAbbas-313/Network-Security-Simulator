using System;

namespace NetworkSecuritySimulator.Logging
{
    /// <summary>
    /// Represents a single log entry in the system
    /// Each log entry contains timestamp, device, event type, and severity information
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Gets or sets the timestamp when the log was generated
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the device that generated the log
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the device that generated the log
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the type of event that occurred
        /// Examples: "PortSecurityViolation", "UnauthorizedAccess", "Misconfiguration"
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the severity level of the log entry
        /// </summary>
        public SeverityLevel Severity { get; set; }

        /// <summary>
        /// Gets or sets the detailed message describing the event
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the source of the log entry (system, attack engine, defense system, etc.)
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets optional additional data/metadata about the event
        /// </summary>
        public string AdditionalData { get; set; }

        /// <summary>
        /// Constructor for LogEntry
        /// </summary>
        public LogEntry()
        {
            Timestamp = DateTime.Now;
            Severity = SeverityLevel.INFO;
        }

        /// <summary>
        /// Constructor for LogEntry with parameters
        /// </summary>
        public LogEntry(string deviceName, string deviceId, string eventType, SeverityLevel severity, string message, string source = "System")
        {
            Timestamp = DateTime.Now;
            DeviceName = deviceName;
            DeviceId = deviceId;
            EventType = eventType;
            Severity = severity;
            Message = message;
            Source = source;
            AdditionalData = "";
        }

        /// <summary>
        /// Returns a formatted string representation of the log entry
        /// </summary>
        /// <returns>Formatted log string</returns>
        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Severity}] [{DeviceName}] {EventType}: {Message}";
        }

        /// <summary>
        /// Gets a CSV representation of the log entry
        /// </summary>
        /// <returns>CSV formatted string</returns>
        public string ToCsvString()
        {
            return $"\"{Timestamp:yyyy-MM-dd HH:mm:ss}\",\"{DeviceName}\",\"{DeviceId}\",\"{EventType}\",\"{Severity}\",\"{Message}\",\"{Source}\",\"{AdditionalData}\"";
        }
    }
}
