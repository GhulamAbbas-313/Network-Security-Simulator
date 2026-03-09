using NetworkSecuritySimulator.Core.Interfaces;

namespace NetworkSecuritySimulator.UI.ViewModels
{
    /// <summary>
    /// ViewModel for a defense mechanism
    /// Wraps IDefense interface for UI binding
    /// </summary>
    public class DefenseViewModel : ViewModelBase
    {
        private IDefense defense;
        private string name;
        private string description;
        private int effectiveness;
        private bool isApplied;

        public DefenseViewModel(IDefense defense)
        {
            this.defense = defense;
            Name = defense.DefenseName;
            Description = defense.Description;
            Effectiveness = defense.EffectivenessLevel;
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

        public int Effectiveness
        {
            get => effectiveness;
            set => SetProperty(ref effectiveness, value);
        }

        public bool IsApplied
        {
            get => isApplied;
            set => SetProperty(ref isApplied, value);
        }

        public IDefense Defense => defense;

        /// <summary>
        /// Returns effectiveness as a percentage display
        /// </summary>
        public string EffectivenessDisplay => $"{Effectiveness}% Effective";
    }
}
