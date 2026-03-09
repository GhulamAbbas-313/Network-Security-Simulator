# Network Security Simulator - User Guide

## Getting Started

### Installation
1. Download the application from the releases page
2. Extract the zip file to your desired location
3. Run `NetworkSecuritySimulator.exe`
4. The application will start with the main dashboard

### First Run
1. Select a network topology from the dropdown menu
2. Click "Load Topology" to initialize the network
3. Click "Start Simulation" to begin
4. Observe the network status in the Device Monitor

## Using the Application

### Topology Selection
The application includes 12 pre-configured network topologies:
- **Star**: Simple topology with central switch (10 devices)
- **Bus**: Shared medium topology (9 devices)
- **Ring**: Circular network structure (10 devices)
- **Mesh**: Full mesh connectivity (18 devices)
- **Tree**: Hierarchical topology (16 devices)
- **Hybrid**: Star and Mesh combination (18 devices)
- **Enterprise LAN**: Large organization network (23 devices)
- **Campus Network**: University network (28 devices)
- **Data Center**: High-availability infrastructure (15 devices)
- **Branch Office**: Remote office network (12 devices)
- **DMZ Architecture**: Security zone separation (20 devices)
- **Multi-Layer Network**: Core-distribution-access (35 devices)

### Launching Attacks

1. Select a topology and load it
2. Go to the "Attack Control Panel" tab
3. Choose an attack type:
   - **MAC Flooding**: Attacks switches without port security
   - **Unauthorized Access**: Attempts to compromise PCs
   - **Misconfiguration**: Exploits router/firewall misconfiguration

4. Select a target device from the list
5. Click "Execute Attack"
6. Check the "System Logs" to see results

#### Attack Outcomes
- **Success**: Device state changes to Vulnerable or Compromised
- **Failure**: Defense prevented the attack
- **Partial**: Some impact with defense partially mitigating

### Applying Defenses

1. Go to the "Defense Configuration" tab
2. Choose a defense mechanism:
   - **Port Security**: Enable on switches (effectiveness: 95%)
   - **Firewall Rules**: Add rules to firewalls (effectiveness: 85%)
   - **Access Control Lists**: Configure on routers (effectiveness: 80%)

3. Select target device
4. Click "Apply Defense"
5. Monitor attack success rates decrease

### Monitoring Device States

The "Device State Monitor" tab shows all devices and their status:
- **Green (Normal)**: Device is secure
- **Yellow (Vulnerable)**: Device has vulnerabilities
- **Red (Compromised)**: Device has been successfully attacked
- **Gray (Offline)**: Device is disconnected

Color coding updates in real-time as attacks and defenses are applied.

### Analyzing Logs

1. Check the "System Logs" panel on the right
2. Logs are color-coded by severity:
   - **INFO** (Blue): Normal operations
   - **WARNING** (Yellow): Potential issues
   - **CRITICAL** (Red): Security incidents

3. Filter logs by severity using the dropdown
4. Click "Export Logs" to save as CSV file
5. Click "Clear Logs" to reset the log display

### Understanding Statistics

The Statistics panel shows:
- **Total Devices**: Number of devices in topology
- **Normal**: Secure devices (green)
- **Vulnerable**: At-risk devices (yellow)
- **Compromised**: Successfully attacked devices (red)

### Tips for Effective Simulation

1. **Test Defenses**: Apply defenses to similar devices and compare attack success rates
2. **Layer Defenses**: Use multiple defense mechanisms together for better protection
3. **Analyze Logs**: Review logs after each attack to understand what happened
4. **Compare Topologies**: Test same attacks on different topologies
5. **Document Results**: Keep notes on which defenses work best
6. **Experiment**: Try different attack/defense combinations

### Keyboard Shortcuts
- **F5**: Refresh device status
- **Ctrl+E**: Export logs
- **Ctrl+C**: Clear logs
- **Ctrl+R**: Reset simulation
- **Ctrl+S**: Save simulation state

### Troubleshooting

**Problem**: Application won't start
- Solution: Ensure you have .NET 6.0 or higher installed

**Problem**: Topology won't load
- Solution: Check that the topology files are in the data folder

**Problem**: UI elements are not responding
- Solution: Restart the application and try again

**Problem**: Logs are not appearing
- Solution: Ensure logging is enabled in settings

**Problem**: Performance is slow
- Solution: Close other applications, or reduce window size

## Advanced Features

### Custom Topologies (Future)
To create custom topologies, implement the `INetworkTopology` interface and add to the topology list

### Batch Attacks (Future)
Coming in version 2.0: Execute multiple attacks sequentially

### Network Trending (Future)
Coming in version 2.0: View security metrics over time

### Collaborative Mode (Future)
Coming in version 3.0: Simulate attacks with multiple players

## Support and Issues

For bug reports, feature requests, or questions:
1. Check the documentation folder
2. Review the implementation guide
3. Check existing issues on GitHub
4. Create a new issue with detailed information

## Credits

- Project developed as a Final Year Project
- Built with .NET 6.0 and WPF
- Uses various open-source libraries

## License

See LICENSE file for details
