# Network Security Simulator Configuration

This file contains the project structure and configuration information.

## Project Name
Network Security Simulator (NSS)

## Version
1.0.0

## Description
A .NET WPF desktop application for simulating network topologies, launching cyber attacks, applying defense mechanisms, and visualizing device behavior in a GUI dashboard.

## Technology Stack
- Language: C#
- UI Framework: WPF (Windows Presentation Foundation)
- .NET Framework: .NET 6.0 or higher
- Architecture: MVVM (Model-View-ViewModel)
- Design Patterns: Observer, State Machine, Manager

## Project Structure
- **NetworkSecuritySimulator.Core**: Core simulation logic (devices, states, engine)
- **NetworkSecuritySimulator.Topologies**: 12 predefined network topologies
- **NetworkSecuritySimulator.Attacks**: Attack simulation implementations
- **NetworkSecuritySimulator.Defense**: Defense mechanism implementations
- **NetworkSecuritySimulator.Logging**: Logging system
- **NetworkSecuritySimulator.UI**: WPF user interface
- **NetworkSecuritySimulator.Data**: Data storage and configuration files

## Key Features
1. 12 Network Topologies - Pre-configured network structures
2. Attack Simulation Engine - MAC Flooding, Unauthorized Access, Misconfiguration
3. Defense Mechanisms - Port Security, Firewall Rules, Access Control
4. Device State Machine - Normal → Vulnerable → Compromised
5. Real-time Log Collection - Email, Device events, Attack/Defense actions
6. Interactive Dashboard - Topology visualization, attack control, defense panel
7. Device Monitoring - Real-time status updates and statistics

## Getting Started
1. Open the solution in Visual Studio 2019 or later
2. Build the solution (Ctrl+Shift+B)
3. Run the application (F5)
4. Select a network topology from the dropdown
5. Load the topology
6. Start the simulation
7. Execute attacks and apply defenses

## Future Enhancements
- Real network packet simulation
- Advanced visualization with drag-and-drop
- Custom topology designer
- Export simulation reports
- Multi-player simulation support
- Integration with real network tools
