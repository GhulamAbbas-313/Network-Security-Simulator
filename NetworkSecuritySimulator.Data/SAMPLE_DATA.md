# Network Security Simulator - Sample Configuration Data

## Topology Configuration Example

```json
{
  "topology": {
    "name": "Star Topology",
    "type": "Star",
    "description": "Simple star topology with central switch",
    "created": "2024-01-01T00:00:00Z",
    "devices": [
      {
        "id": "device-001",
        "name": "Central-Switch",
        "type": "Switch",
        "ipAddress": "192.168.1.1",
        "macAddress": "00:00:00:00:00:01",
        "vulnerabilities": ["MAC Flooding Vulnerable"],
        "defenses": []
      },
      {
        "id": "device-002",
        "name": "PC-01",
        "type": "PC",
        "ipAddress": "192.168.1.11",
        "macAddress": "00:00:00:00:00:0B",
        "vulnerabilities": ["Unpatched Software Available"],
        "defenses": ["Antivirus"]
      }
    ]
  }
}
```

## Attack Configuration Example

```json
{
  "attacks": [
    {
      "name": "MAC Flooding",
      "severity": 8,
      "description": "Floods switch with fake MAC addresses",
      "targetType": "Switch",
      "baseSuccessRate": 0.95,
      "defenseBypass": {
        "Port Security": 0.05
      }
    },
    {
      "name": "Unauthorized Access",
      "severity": 9,
      "description": "Attempts to gain unauthorized access",
      "targetType": ["PC", "Server"],
      "baseSuccessRate": 0.90,
      "defenseBypass": {
        "Antivirus": 0.30,
        "Firewall Rules": 0.20
      }
    }
  ]
}
```

## Defense Configuration Example

```json
{
  "defenses": [
    {
      "name": "Port Security",
      "type": "Network",
      "effectiveness": 95,
      "applicableTo": "Switch",
      "description": "Limits MAC addresses per port",
      "configuration": {
        "maxMacAddresses": 1,
        "violationAction": "shutdown"
      }
    },
    {
      "name": "Firewall Rules",
      "type": "Network",
      "effectiveness": 85,
      "applicableTo": ["Firewall", "Router"],
      "description": "Filters traffic based on rules",
      "configuration": {
        "implicitDeny": true,
        "rules": [
          "ANY:ANY:TCP:22:DENY",
          "ANY:ANY:TCP:23:DENY",
          "ANY:ANY:TCP:3389:DENY"
        ]
      }
    }
  ]
}
```

## Simulation Log Example

```
[2024-03-10 14:30:45] [INFO] [System] Simulation started
[2024-03-10 14:30:46] [INFO] [System] Device registered: Central-Switch
[2024-03-10 14:30:46] [INFO] [System] Device registered: PC-01
[2024-03-10 14:30:47] [INFO] [System] Device registered: PC-02
[2024-03-10 14:31:00] [INFO] [System] Simulation running with 3 devices
[2024-03-10 14:31:15] [WARNING] [Central-Switch] MAC Flooding Attack Started
[2024-03-10 14:31:20] [CRITICAL] [Central-Switch] Attack Successful - Device Compromised
[2024-03-10 14:31:25] [INFO] [Central-Switch] Port Security Applied
[2024-03-10 14:31:40] [WARNING] [Central-Switch] MAC Flooding Attack Detected - BLOCKED
[2024-03-10 14:31:41] [INFO] [Central-Switch] Attack Failed - Defense Active
```

## Device State Timeline Example

```
Device: PC-01
State: Normal
  ├─ 14:31:15 → Add Vulnerability "Unpatched SSH"
  │  State: Vulnerable
  │  Vulnerabilities: 1
  │
  ├─ 14:31:20 → Add Vulnerability "Weak Credentials"
  │  State: Vulnerable
  │  Vulnerabilities: 2
  │
  ├─ 14:31:25 → Execute Unauthorized Access Attack
  │  Success: YES
  │  State: Compromised
  │  Vulnerabilities: 2 (exploited)
  │
  └─ 14:31:30 → Severity Report
     Risk Level: CRITICAL
     Impact: Data Breach Likely
```

## Performance Metrics Example

```json
{
  "simulation_metrics": {
    "duration_seconds": 300,
    "total_devices": 12,
    "total_attacks": 5,
    "successful_attacks": 3,
    "attack_success_rate": 0.60,
    "total_defenses_applied": 4,
    "devices_compromised": 2,
    "devices_protected": 8,
    "vulnerability_count": 6,
    "logs_generated": 45
  },
  "device_breakdown": {
    "normal": 8,
    "vulnerable": 2,
    "compromised": 2,
    "offline": 0
  },
  "attack_breakdown": {
    "MAC Flooding": { "attempts": 2, "successful": 2 },
    "Unauthorized Access": { "attempts": 2, "successful": 1 },
    "Misconfiguration": { "attempts": 1, "successful": 0 }
  },
  "defense_effectiveness": {
    "Port Security": 0.95,
    "Firewall Rules": 0.80,
    "Access Control": 0.75
  }
}
```
