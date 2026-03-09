using NetworkSecuritySimulator.Core.Devices;
using NetworkSecuritySimulator.Core.States;

namespace NetworkSecuritySimulator.UI.ViewModels
{
    /// <summary>
    /// ViewModel for a network device
    /// Wraps Device model for UI binding
    /// </summary>
    public class DeviceViewModel : ViewModelBase
    {
        private Device device;
        private string displayName;
        private string ipAddress;
        private DeviceState currentState;
        private int vulnerabilityCount;
        private int defenseCount;
        private string statusColor;

        public DeviceViewModel(Device device)
        {
            this.device = device;
            DisplayName = device.DeviceName;
            IpAddress = device.IpAddress;
            CurrentState = device.State;
            VulnerabilityCount = device.Vulnerabilities.Count;
            DefenseCount = device.ActiveDefenses.Count;
            UpdateStatusColor();
        }

        /// <summary>
        /// Updates the status color based on device state
        /// </summary>
        public void UpdateStatusColor()
        {
            StatusColor = CurrentState switch
            {
                DeviceState.Normal => "Green",
                DeviceState.Vulnerable => "Orange",
                DeviceState.Compromised => "Red",
                DeviceState.Offline => "Gray",
                _ => "Black"
            };
        }

        // Properties
        public string DisplayName
        {
            get => displayName;
            set => SetProperty(ref displayName, value);
        }

        public string IpAddress
        {
            get => ipAddress;
            set => SetProperty(ref ipAddress, value);
        }

        public DeviceState CurrentState
        {
            get => currentState;
            set
            {
                if (SetProperty(ref currentState, value))
                {
                    UpdateStatusColor();
                }
            }
        }

        public int VulnerabilityCount
        {
            get => vulnerabilityCount;
            set => SetProperty(ref vulnerabilityCount, value);
        }

        public int DefenseCount
        {
            get => defenseCount;
            set => SetProperty(ref defenseCount, value);
        }

        public string StatusColor
        {
            get => statusColor;
            set => SetProperty(ref statusColor, value);
        }

        public Device Device => device;
    }
}
