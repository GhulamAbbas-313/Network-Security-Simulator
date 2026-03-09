# Quick Start Guide - Network Security Simulator

## 5-Minute Setup

### Prerequisites
- Windows 10/11
- .NET 6.0 SDK or Runtime
- Visual Studio 2022 (optional, for development)

### Installation

1. **Clone or Extract Project**
   ```bash
   cd "d:\Final Year Project\FINAL YEAR\NetworkSecuritySimulator"
   ```

2. **Open with Visual Studio**
   - Open `NetworkSecuritySimulator.sln`
   - Build solution: `Ctrl+Shift+B`

3. **Or Build from Command Line**
   ```bash
   dotnet build
   dotnet run --project NetworkSecuritySimulator.UI
   ```

### First Simulation (3 steps)

1. **Load a Topology**
   - Launch the application
   - Select "Star Topology" from the dropdown
   - Click "Load Topology"
   - You should see ~10 devices appear in the device grid

2. **Start Simulation**
   - Click "Start Simulation" button
   - Status bar should show "Running"
   - Observe initial device states (all green = Normal)

3. **Execute an Attack**
   - Go to "Attack Control Panel" tab
   - Select "MAC Flooding" attack
   - Pick "Central-Switch" as target
   - Click "Execute Attack"
   - Check logs to see if attack succeeded

### Key UI Sections

| Section | Purpose | What to do |
|---------|---------|-----------|
| Left Sidebar | Topology selection & control | Select topology, start/stop simulation |
| Center Tabs | Main functionality | Switch between attack, defense, and monitoring |
| Right Panel | Status and logs | Watch logs and statistics in real-time |
| Device Grid | Monitor device status | See device states and vulnerabilities |

### First Attack Scenario

**Objective**: Compromise a PC using Unauthorized Access attack

Steps:
1. Load "Enterprise LAN" topology
2. Start simulation
3. Go to Attack Control Panel
4. Select "Unauthorized Access" attack
5. Target any "WS-*" (workstation) device
6. Execute attack
7. Check logs for success/failure message
8. Device should change to "Compromised" state (red)

### First Defense Scenario

**Objective**: Protect against MAC Flooding

Steps:
1. Load "Star Topology"
2. Start simulation
3. Go to Defense Configuration
4. Select "Port Security" defense
5. Target "Central-Switch"
6. Apply defense
7. Now try MAC Flooding attack on same switch
8. Attack should fail (see logs)

### Understanding Device Colors

```
🟢 Green   = Normal (secure)
🟡 Yellow  = Vulnerable (at-risk)
🔴 Red     = Compromised (attacked)
⚫ Gray    = Offline (disconnected)
```

### Next Steps

1. **Explore All Topologies**: Try each of the 12 topologies
2. **Test All Attacks**: Execute each attack type
3. **Experiment with Defenses**: Apply defenses and see impact
4. **Analyze Logs**: Export and review log files
5. **Compare Results**: See which defenses stop which attacks best

### Troubleshooting

| Problem | Solution |
|---------|----------|
| Application won't start | Install .NET 6.0 from dotnet.microsoft.com |
| Topology won't load | Check NetworkSecuritySimulator.Topologies.dll exists |
| No devices appear | Click "Load Topology" after selecting |
| Logs are empty | Start simulation and execute an attack |
| Buttons don't work | Ensure topology is loaded first |

### Useful Keyboard Shortcuts
- `F5` - Refresh device status
- `Ctrl+R` - Reset simulation
- `Ctrl+E` - Export logs
- `Ctrl+L` - Clear logs

### Performance Tips

1. Start with "Star Topology" for fastest loading
2. Close other applications for better performance
3. Use "Reset" button if UI becomes sluggish
4. Reduce window size for faster rendering

### Common Questions

**Q: What's the difference between attacks?**
A: MAC Flooding targets switches, Unauthorized Access targets PCs, Misconfiguration targets routers.

**Q: Can I run multiple attacks at once?**
A: Currently one at a time. Simultaneous attacks coming in v2.0.

**Q: How do I see attack success rates?**
A: Check the statistics panel and logs. Defense effectiveness reduces success rates.

**Q: Where are logs saved?**
A: In memory during simulation. Export via "Export Logs" button.

**Q: Can I create custom topologies?**
A: Currently not in v1.0. Add to feature request for v2.0.

### Video Tutorial (Planned)

Coming soon: YouTube video walkthrough of key features

### Get Help

1. Check USER_GUIDE.md for detailed instructions
2. Review TECHNICAL_DOCUMENTATION.md for architecture
3. Check IMPLEMENTATION_GUIDE.md for development help
4. Read inline code comments for implementation details

### Ready to Code?

Start with the IMPLEMENTATION_GUIDE.md to complete the TODO implementations.

Happy simulating! 🚀
