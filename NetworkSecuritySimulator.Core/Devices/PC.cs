using System.Collections.Generic;

namespace NetworkSecuritySimulator.Core.Devices
{
    /// <summary>
    /// Represents a personal computer (PC) device on the network
    /// PCs are end-user devices that can be targeted by attacks
    /// </summary>
    public class PC : Device
    {
        /// <summary>
        /// Gets or sets the username of the primary user
        /// </summary>
        public string PrimaryUser { get; set; }

        /// <summary>
        /// Gets or sets whether the operating system is up to date with patches
        /// </summary>
        public bool IsPatched { get; set; }

        /// <summary>
        /// Gets or sets whether an antivirus solution is installed and active
        /// </summary>
        public bool AntivirusActive { get; set; }

        /// <summary>
        /// Gets or sets whether a personal firewall is enabled
        /// </summary>
        public bool PersonalFirewallEnabled { get; set; }

        /// <summary>
        /// Gets or sets the list of installed software/applications
        /// </summary>
        public List<string> InstalledSoftware { get; set; }

        /// <summary>
        /// Gets or sets the list of open network connections
        /// </summary>
        public List<string> ActiveConnections { get; set; }

        /// <summary>
        /// Gets or sets the amount of sensitive data stored on this PC (in GB)
        /// </summary>
        public double SensitiveDataGB { get; set; }

        /// <summary>
        /// Constructor for PC
        /// </summary>
        public PC()
        {
            IsPatched = true;
            AntivirusActive = true;
            PersonalFirewallEnabled = false;
            InstalledSoftware = new List<string>();
            ActiveConnections = new List<string>();
            SensitiveDataGB = 0;
            DeviceName = "PC";
            Vulnerabilities.Add("Unpatched Software Available"); // Hidden vulnerability
        }

        /// <summary>
        /// Disables a security patch to create a vulnerability
        /// This can be used to simulate unpatched systems
        /// </summary>
        public void DisablePatch()
        {
            IsPatched = false;
            AddVulnerability("Missing Security Patch");
        }

        /// <summary>
        /// Enables security patches on the PC
        /// </summary>
        public void EnablePatches()
        {
            IsPatched = true;
            RemoveVulnerability("Missing Security Patch");
        }

        /// <summary>
        /// Activates antivirus protection
        /// </summary>
        public void ActivateAntivirus()
        {
            AntivirusActive = true;
            ApplyDefense("Antivirus");
        }

        /// <summary>
        /// Deactivates antivirus protection
        /// </summary>
        public void DeactivateAntivirus()
        {
            AntivirusActive = false;
            RemoveDefense("Antivirus");
        }

        /// <summary>
        /// Enables personal firewall
        /// </summary>
        public void EnablePersonalFirewall()
        {
            PersonalFirewallEnabled = true;
            ApplyDefense("Personal Firewall");
        }

        /// <summary>
        /// Sets the amount of sensitive data on this PC
        /// </summary>
        /// <param name="dataGB">Size in GB</param>
        public void SetSensitiveData(double dataGB)
        {
            SensitiveDataGB = dataGB;
        }

        /// <summary>
        /// Gets the overall security posture of the PC
        /// </summary>
        /// <returns>Security score (0-100)</returns>
        public int GetSecurityScore()
        {
            int score = 100;
            if (!IsPatched) score -= 30;
            if (!AntivirusActive) score -= 20;
            if (!PersonalFirewallEnabled) score -= 15;
            if (Vulnerabilities.Count > 0) score -= (Vulnerabilities.Count * 5);
            return System.Math.Max(0, score);
        }
    }
}
