using System;
using System.Collections.ObjectModel;
using NetworkSecuritySimulator.Core.Engine;
using NetworkSecuritySimulator.Logging;
using NetworkSecuritySimulator.Attacks;
using NetworkSecuritySimulator.Defense;

namespace NetworkSecuritySimulator.UI.ViewModels
{
    /// <summary>
    /// Main ViewModel for the application
    /// Coordinates between UI and simulation engine
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private SimulationEngine simulationEngine;
        private LogManager logManager;
        private AttackManager attackManager;
        private DefenseManager defenseManager;

        private bool isSimulationRunning;
        private int totalDevices;
        private int normalDevices;
        private int vulnerableDevices;
        private int compromisedDevices;
        private string simulationStatus;

        // Collections for UI binding
        public ObservableCollection<string> Topologies { get; set; }
        public ObservableCollection<string> Logs { get; set; }
        public ObservableCollection<DeviceViewModel> Devices { get; set; }
        public ObservableCollection<AttackViewModel> AvailableAttacks { get; set; }
        public ObservableCollection<DefenseViewModel> AvailableDefenses { get; set; }

        public MainWindowViewModel()
        {
            InitializeCollections();
            InitializeEngines();
        }

        /// <summary>
        /// Initializes observable collections
        /// </summary>
        private void InitializeCollections()
        {
            Topologies = new ObservableCollection<string>
            {
                "Star Topology",
                "Bus Topology",
                "Ring Topology",
                "Mesh Topology",
                "Tree Topology",
                "Hybrid Topology",
                "Enterprise LAN",
                "Campus Network",
                "Data Center Network",
                "Branch Office Network",
                "DMZ Architecture",
                "Multi-Layer Network"
            };

            Logs = new ObservableCollection<string>();
            Devices = new ObservableCollection<DeviceViewModel>();
            AvailableAttacks = new ObservableCollection<AttackViewModel>();
            AvailableDefenses = new ObservableCollection<DefenseViewModel>();
        }

        /// <summary>
        /// Initializes simulation engines
        /// </summary>
        private void InitializeEngines()
        {
            // Create instances of managers
            simulationEngine = new SimulationEngine();
            logManager = new LogManager();
            attackManager = new AttackManager();
            defenseManager = new DefenseManager();

            // Populate attacks from manager
            foreach (var attack in attackManager.GetAllAttacks())
            {
                AvailableAttacks.Add(new AttackViewModel 
                { 
                    Name = attack.AttackName, 
                    Description = attack.Description,
                    Severity = attack.SeverityLevel
                });
            }

            // Populate defenses from manager
            foreach (var defense in defenseManager.GetAllDefenses())
            {
                AvailableDefenses.Add(new DefenseViewModel 
                { 
                    Name = defense.DefenseName, 
                    Description = defense.Description,
                    Effectiveness = defense.EffectivenessLevel
                });
            }

            // Subscribe to events
            simulationEngine.DeviceStateChanged += OnDeviceStateChanged;
            simulationEngine.LogGenerated += OnLogGenerated;
            attackManager.AttackExecuted += OnAttackExecuted;
            defenseManager.DefenseApplied += OnDefenseApplied;
        }

        /// <summary>
        /// Starts the simulation
        /// </summary>
        public void StartSimulation()
        {
            // TODO: Start simulation
            // simulationEngine.StartSimulation();
            // IsSimulationRunning = true;
            // SimulationStatus = "Running";
        }

        /// <summary>
        /// Stops the simulation
        /// </summary>
        public void StopSimulation()
        {
            // TODO: Stop simulation
            // simulationEngine.StopSimulation();
            // IsSimulationRunning = false;
            // SimulationStatus = "Stopped";
        }

        /// <summary>
        /// Resets the simulation
        /// </summary>
        public void ResetSimulation()
        {
            // TODO: Reset simulation
            // simulationEngine.Reset();
            // IsSimulationRunning = false;
            // SimulationStatus = "Ready";
            // ClearUI();
        }

        /// <summary>
        /// Loads a network topology
        /// </summary>
        public void LoadTopology(string topologyName)
        {
            // TODO: Implement topology loading
            // Case topologyName:
            //   "Star Topology": var topology = new StarTopology();
            //   etc.
            // topology.BuildTopology();
            // var devices = topology.GetAllDevices();
            // foreach (var device in devices)
            // {
            //     simulationEngine.RegisterDevice(device);
            //     Devices.Add(new DeviceViewModel(device));
            // }
        }

        // Properties with change notification
        public bool IsSimulationRunning
        {
            get => isSimulationRunning;
            set => SetProperty(ref isSimulationRunning, value);
        }

        public int TotalDevices
        {
            get => totalDevices;
            set => SetProperty(ref totalDevices, value);
        }

        public int NormalDevices
        {
            get => normalDevices;
            set => SetProperty(ref normalDevices, value);
        }

        public int VulnerableDevices
        {
            get => vulnerableDevices;
            set => SetProperty(ref vulnerableDevices, value);
        }

        public int CompromisedDevices
        {
            get => compromisedDevices;
            set => SetProperty(ref compromisedDevices, value);
        }

        public string SimulationStatus
        {
            get => simulationStatus;
            set => SetProperty(ref simulationStatus, value);
        }

        // Event handlers
        private void OnDeviceStateChanged(object sender, DeviceStateChangedEventArgs e)
        {
            // TODO: Update device view models
        }

        private void OnLogGenerated(object sender, LogEntryEventArgs e)
        {
            // TODO: Add log to UI
            // Logs.Insert(0, e.LogEntry);
        }

        private void OnAttackExecuted(object sender, AttackExecutedEventArgs e)
        {
            // TODO: Update UI after attack
        }

        private void OnDefenseApplied(object sender, DefenseAppliedEventArgs e)
        {
            // TODO: Update UI after defense application
        }

        /// <summary>
        /// Clears the UI when simulation is reset
        /// </summary>
        private void ClearUI()
        {
            Devices.Clear();
            Logs.Clear();
            TotalDevices = 0;
            NormalDevices = 0;
            VulnerableDevices = 0;
            CompromisedDevices = 0;
        }
    }
}
