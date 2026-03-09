using System.Windows.Controls;

namespace NetworkSecuritySimulator.UI.Views
{
    /// <summary>
    /// TopologyCanvas user control
    /// Displays the network topology visualization
    /// </summary>
    public partial class TopologyCanvas : UserControl
    {
        public TopologyCanvas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draws a network device on the canvas
        /// </summary>
        public void DrawDevice(double x, double y, string deviceName, string deviceType, string state)
        {
            // TODO: Implement device drawing
            // - Draw rectangle or shape for device
            // - Add label with device name
            // - Color code based on state (green=normal, yellow=vulnerable, red=compromised)
            // - Add tooltip with device details
        }

        /// <summary>
        /// Draws a connection line between two devices
        /// </summary>
        public void DrawConnection(double x1, double y1, double x2, double y2, string connectionType)
        {
            // TODO: Implement connection drawing
            // - Draw line between devices
            // - Style based on connection type (solid, dashed, colored)
            // - Add labels for connection info
        }

        /// <summary>
        /// Clears the canvas
        /// </summary>
        public void Clear()
        {
            CanvasArea.Children.Clear();
        }
    }
}
