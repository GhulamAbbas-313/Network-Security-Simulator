using NetworkSecuritySimulator.Core.Interfaces;

namespace NetworkSecuritySimulator.UI.ViewModels
{
    /// <summary>
    /// ViewModel for an attack
    /// Wraps IAttack interface for UI binding
    /// </summary>
    public class AttackViewModel : ViewModelBase
    {
        private IAttack attack;
        private string name;
        private string description;
        private int severity;
        private bool isExecuting;

        public AttackViewModel(IAttack attack)
        {
            this.attack = attack;
            Name = attack.AttackName;
            Description = attack.Description;
            Severity = attack.SeverityLevel;
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int Severity
        {
            get => severity;
            set => SetProperty(ref severity, value);
        }

        public bool IsExecuting
        {
            get => isExecuting;
            set => SetProperty(ref isExecuting, value);
        }

        public IAttack Attack => attack;

        /// <summary>
        /// Returns a visual representation of severity
        /// </summary>
        public string SeverityDisplay
        {
            get
            {
                if (Severity >= 8)
                    return $"High ({Severity}/10)";
                if (Severity >= 5)
                    return $"Medium ({Severity}/10)";
                return $"Low ({Severity}/10)";
            }
        }
    }
}
