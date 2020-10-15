using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeatApp.Behaviors
{
    public class ValidatableObject<T> : IValidatable<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();

        private List<string> errors = new List<string>();
        public List<string> Errors
        {
            get => errors;
            set => SetProperty(ref errors, value);
        }

        private bool cleanOnChange = true;
        public bool CleanOnChange
        {
            get => cleanOnChange;
            set => SetProperty(ref cleanOnChange, value);
        }

        T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                if (CleanOnChange)
                    IsValid = true;
            }
        }
        private bool isValid = true;
        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        public virtual bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
        public override string ToString()
        {
            return $"{Value}";
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}