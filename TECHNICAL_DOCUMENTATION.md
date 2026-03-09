# Technical Documentation

## Architecture Overview

### Layered Architecture
```
┌─────────────────────────────────────────┐
│  Presentation Layer (WPF UI)            │
│  - XAML Views                           │
│  - MVVM ViewModels                      │
│  - User Controls                        │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│  Application Layer                      │
│  - MainWindowViewModel                  │
│  - Command Handlers                     │
│  - Event Routing                        │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│  Business Logic Layer                   │
│  - SimulationEngine                     │
│  - AttackManager                        │
│  - DefenseManager                       │
│  - LogManager                           │
│  - StateManager                         │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│  Domain Layer                           │
│  - Device Models                        │
│  - Device State Machine                 │
│  - Topology Definitions                 │
│  - Attack & Defense Interfaces          │
└──────────────────┬──────────────────────┘
                   │
┌──────────────────▼──────────────────────┐
│  Data & Configuration Layer             │
│  - Topology Files                       │
│  - Log Storage                          │
│  - Configuration Files                  │
└─────────────────────────────────────────┘
```

## Design Patterns

### 1. State Machine Pattern
```csharp
Device State Transitions:
Normal ──[Vulnerability Added]──> Vulnerable
Vulnerable ──[3+ Vulnerabilities]──> Compromised
Any State ──[Go Offline]──> Offline
Offline ──[Come Online]──> Normal
```

### 2. Observer Pattern
- SimulationEngine publishes DeviceStateChanged events
- Managers subscribe to attack/defense events
- UI updates on event notifications
- LogManager logs all significant events

### 3. Manager Pattern
- AttackManager: Manages all attacks
- DefenseManager: Manages all defenses
- LogManager: Manages all logs
- StateManager: Manages device states

### 4. Factory Pattern
- TopologyLoader: Creates topology instances
- AttackFactory: Creates attack instances (future)
- DeviceFactory: Creates device instances (future)

### 5. MVVM Pattern
- Models: Device, LogEntry, etc.
- Views: XAML UI files
- ViewModels: Logic and data binding
- RelayCommand: Command implementation

## Class Relationships

### Core Module Dependency Graph
```
Device (Abstract)
  ├─> Switch
  ├─> Router
  ├─> Firewall
  └─> PC

DeviceState (Enum)
  └─> Device (uses)
  └─> StateManager (tracks)

SimulationEngine
  ├─> Device (manages)
  ├─> StateManager (uses)
  └─> Events (publishes)

AttackManager/DefenseManager
  ├─> IAttack/IDefense (interfaces)
  └─> SimulationEngine (interacts)

LogManager
  ├─> LogEntry (contains)
  ├─> SeverityLevel (uses)
  └─> SimulationEngine (subscribes)

Topology Interface
  ├─> INetworkTopology
  ├─> Device (contains)
  └─> TopologyLoader (creates)
```

## Key Interfaces

### IAttack Interface
```csharp
public interface IAttack
{
    string AttackName { get; }
    string Description { get; }
    int SeverityLevel { get; }
    bool Execute(string targetDeviceId);
}
```

### IDefense Interface
```csharp
public interface IDefense
{
    string DefenseName { get; }
    string Description { get; }
    int EffectivenessLevel { get; }
    bool Apply(string targetDeviceId);
    bool Remove(string targetDeviceId);
}
```

### INetworkTopology Interface
```csharp
public interface INetworkTopology
{
    string TopologyName { get; }
    string Description { get; }
    void BuildTopology();
    List<Device> GetAllDevices();
}
```

## State Machine Implementation

### Device State Transitions
```
Normal
  ├─ Add Vulnerability → Vulnerable
  └─ Go Offline → Offline

Vulnerable
  ├─ Add 3+ Vulnerabilities → Compromised
  ├─ Remove All Vulnerabilities → Normal
  └─ Go Offline → Offline

Compromised
  ├─ Remove Vulnerabilities (if < 3) → Vulnerable
  └─ Go Offline → Offline

Offline
  └─ Come Online → Normal
```

## Attack Implementation Details

### MAC Flooding Attack
- **Target**: Switch devices
- **Mechanism**: Fill CAM table with fake MAC entries
- **Initial Success Rate**: 95% (without port security)
- **With Port Security**: 5% success rate
- **Device Impact**: Switch enters failopen mode, forwarding all frames

### Unauthorized Access Attack
- **Target**: PC and Server devices  
- **Mechanism**: Exploit credentials or vulnerabilities
- **Base Success Rate**: 90%
- **With Antivirus**: 30% success rate
- **With Firewall**: 20% success rate
- **Device Impact**: Transition to Compromised state

### Misconfiguration Attack
- **Target**: Router and Firewall devices
- **Mechanism**: Exploit routing/firewall misconfigurations
- **Success Rate (Misconfigured)**: 85%
- **Success Rate (Properly Configured)**: 20%
- **Device Impact**: Compromise leads to lateral movement

## Defense Implementation Details

### Port Security
- **Protection**: Prevents MAC flooding attacks
- **Implementation**: Limit MAC addresses per port
- **Effectiveness**: 95%
- **Configuration**: Max 1-2 MAC addresses per port

### Firewall Rules
- **Protection**: Blocks unauthorized traffic
- **Implementation**: ACL-based packet filtering
- **Effectiveness**: 85%
- **Rules**: DENY sensitive ports (22, 23, 3389)

### Access Control Lists
- **Protection**: Fine-grained traffic control
- **Implementation**: Layer 3-4 filtering
- **Effectiveness**: 80%
- **Configuration**: URL subnet-based access

## Event Flow

### Attack Execution Flow
```
User Selects Attack
        ↓
AttackManager.ExecuteAttack()
        ↓
IAttack.Execute(targetId)
        ↓
Get Target Device
        ↓
Check Vulnerabilities
        ↓
Calculate Success Probability
        ↓
Random Success Check
        ↓
Update Device State
        ↓
Generate Log Entry
        ↓
Fire AttackExecuted Event
        ↓
UI Updates
```

### Defense Application Flow
```
User Applies Defense
        ↓
DefenseManager.ApplyDefense()
        ↓
IDefense.Apply(targetId)
        ↓
Get Target Device
        ↓
Verify Compatibility
        ↓
Add to Device Defenses
        ↓
Generate Log Entry
        ↓
Fire DefenseApplied Event
        ↓
UI Updates
```

## Database Schema (Future)

### Logs Table
```sql
CREATE TABLE Logs (
    LogId INT PRIMARY KEY,
    Timestamp DATETIME,
    DeviceId VARCHAR(50),
    EventType VARCHAR(100),
    Severity VARCHAR(20),
    Message TEXT,
    Source VARCHAR(50)
);
```

### Devices Table
```sql
CREATE TABLE Devices (
    DeviceId VARCHAR(50) PRIMARY KEY,
    DeviceName VARCHAR(100),
    DeviceType VARCHAR(50),
    IpAddress VARCHAR(15),
    State VARCHAR(20),
    CreatedAt DATETIME
);
```

## Performance Considerations

### Optimization Strategies
1. **Object Pooling**: Reuse device/event objects
2. **Asynchronous Logging**: Don't block UI thread
3. **Lazy Loading**: Load topology data on demand
4. **Virtualization**: Render only visible log items
5. **Caching**: Store frequently accessed data

### Scalability Limits
- Current: 35 devices (Multi-Layer Network)
- Planned: Support 100+ devices
- Challenge: Visualization rendering performance

## Testing Strategy

### Unit Tests
- Device state transitions
- Attack/defense calculations
- Log filtering
- Topology loading

### Integration Tests
- Full attack-defense cycle
- Event propagation
- UI updates
- Log persistence

### Performance Tests
- Attack execution time
- Log write throughput
- UI refresh rate
- Memory usage

## API Reference

### SimulationEngine Key Methods
- `RegisterDevice(Device)`: Add device to simulation
- `StartSimulation()`: Begin simulation
- `StopSimulation()`: Pause simulation
- `GetAllDevices()`: Get all registered devices
- `GetStatistics()`: Get simulation stats

### AttackManager Key Methods
- `ExecuteAttack(attackName, targetId)`: Execute attack
- `GetAllAttacks()`: Get available attacks
- `RegisterAttack(IAttack)`: Register custom attack

### DefenseManager Key Methods
- `ApplyDefense(defenseName, targetId)`: Apply defense
- `RemoveDefense(defenseName, targetId)`: Remove defense
- `IsDefenseApplied(defenseName, targetId)`: Check if applied

### LogManager Key Methods
- `CreateLog(...)`: Create log entry
- `GetLogsBySeverity(SeverityLevel)`: Filter logs
- `ClearLogs()`: Clear all logs
- `GetStatistics()`: Log stats

## Deployment

### System Requirements
- Windows 10 or later
- .NET Runtime 6.0 or higher
- Minimum 4GB RAM
- 500MB free disk space

### Installation Steps
1. Download installer
2. Run setup executable
3. Follow installation wizard
4. Launch application

### Configuration Files
- appsettings.json: Application settings
- topology configs: Network topology definitions
- logs folder: Log file storage

## Future Enhancements

1. **Real Network Integration**: Connect to live networks
2. **Advanced Visualization**: 3D network topology rendering
3. **Custom Attacks/Defenses**: Plugin architecture
4. **Cloud Deployment**: SaaS version
5. **Mobile Companion**: iOS/Android app
6. **AI Integration**: Machine learning for attack prediction
