
using HeatApp.Behaviors;
using System.Linq;
using Xamarin.Forms;

namespace HeatApp.Views.HeatViews.Controls
{
    public partial class EntryView : ContentView
    {
        #region Constructors
        public EntryView()
        {
            InitializeComponent();
        }
        #endregion

        #region BindablePropertties
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
               propertyName: nameof(Text),
               returnType: typeof(string),
               declaringType: typeof(EntryView),
               defaultValue: "",
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: TextPropertyChanged);
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
               propertyName: nameof(Text),
               returnType: typeof(string),
               declaringType: typeof(EntryView),
               defaultValue: "",
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: FontFamilyPropertyChanged);
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
               propertyName: nameof(Text),
               returnType: typeof(string),
               declaringType: typeof(EntryView),
               defaultValue: "",
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: IconPropertyChanged);
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
               propertyName: nameof(IsPassword),
               returnType: typeof(bool),
               declaringType: typeof(EntryView),
               defaultValue: false,
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: IsPasswordPropertyChanged);
        public static readonly BindableProperty IDProperty = BindableProperty.Create(
              propertyName: nameof(ID),
              returnType: typeof(string),
              declaringType: typeof(EntryView),
              defaultValue: "",
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
              propertyName: nameof(PlaceHolder),
              returnType: typeof(string),
              declaringType: typeof(EntryView),
              defaultValue: "",
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: PlaceHolderPropertyChanged);
        public static readonly BindableProperty MaskProperty = BindableProperty.Create(
              propertyName: nameof(Mask),
              returnType: typeof(string),
              declaringType: typeof(EntryView),
              defaultValue: "",
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: MaskPropertyChanged);
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
              propertyName: nameof(Keyboard),
              returnType: typeof(Keyboard),
              declaringType: typeof(EntryView),
              defaultValue: Keyboard.Default,
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: KeyboardPropertyChanged);
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
              propertyName: nameof(IsValid),
              returnType: typeof(bool),
              declaringType: typeof(EntryView),
              defaultValue: true,
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: IsValidPropertyChanged);
        public static readonly BindableProperty AddValidationProperty = BindableProperty.Create(
              propertyName: nameof(AddValidation),
              returnType: typeof(bool),
              declaringType: typeof(EntryView),
              defaultValue: false,
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: AddValidationPropertyChanged);
        #endregion

        #region Propertties
        public string Text
        {
            get
            {
                return (string)base.GetValue(TextProperty);
            }
            set
            {
                if (Text != value)
                    base.SetValue(TextProperty, value);
            }
        }
        public string FontFamily
        {
            get
            {
                return (string)base.GetValue(FontFamilyProperty);
            }
            set
            {
                if (FontFamily != value)
                    base.SetValue(FontFamilyProperty, value);
            }
        }
        public string Icon
        {
            get
            {
                return (string)base.GetValue(IconProperty);
            }
            set
            {
                if (Icon != value)
                    base.SetValue(IconProperty, value);
            }
        }
        public bool IsPassword
        {
            get
            {
                return (bool)base.GetValue(IsPasswordProperty);
            }
            set
            {
                if (IsPassword != value)
                    base.SetValue(IsPasswordProperty, value);
            }
        }
        public string ID
        {
            get
            {
                return (string)base.GetValue(IDProperty);
            }
            set
            {
                if (ID != value)
                    base.SetValue(IDProperty, value);
            }
        }
        public string PlaceHolder
        {
            get
            {
                return (string)base.GetValue(PlaceHolderProperty);
            }
            set
            {
                if (PlaceHolder != value)
                    base.SetValue(PlaceHolderProperty, value);
            }
        }
        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)base.GetValue(KeyboardProperty);
            }
            set
            {
                if (Keyboard != value)
                    base.SetValue(KeyboardProperty, value);
            }
        }
        public string Mask
        {
            get
            {
                return (string)base.GetValue(MaskProperty);
            }
            set
            {
                if (Mask != value)
                    base.SetValue(MaskProperty, value);
            }
        }
        public bool IsValid
        {
            get
            {
                return (bool)base.GetValue(IsValidProperty);
            }
            set
            {
                if (IsValid != value)
                    base.SetValue(IsValidProperty, value);
            }
        }
        public bool AddValidation
        {
            get
            {
                return (bool)base.GetValue(AddValidationProperty);
            }
            set
            {
                if (AddValidation != value)
                    base.SetValue(AddValidationProperty, value);
            }
        }
        #endregion

        #region PropertyChanged
        private static void TextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != null)
                control.entry.Text = (newvalue != null) ? newvalue.ToString() : string.Empty;
        }
        private static void FontFamilyPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != null)
                control.label.FontFamily = (newvalue != null) ? newvalue.ToString() : string.Empty;
        }
        private static void IconPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != null)
                control.label.Text = (newvalue != null) ? newvalue.ToString() : string.Empty;
        }
        private static void PlaceHolderPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != oldvalue)
                control.entry.Placeholder = (newvalue != null) ? newvalue.ToString() : string.Empty;
        }
        private static void IsPasswordPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != oldvalue)
                control.entry.IsPassword = (newvalue != null) ? (bool)newvalue : false;
        }
        private static void KeyboardPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != oldvalue)
                control.entry.Keyboard = (newvalue != null) ? (Keyboard)newvalue : Keyboard.Default;
        }
        private static void MaskPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != oldvalue)
            {
                if (control.entry.Behaviors.Any(p => p.GetType() == typeof(EntryMaskedBehavior)))
                    control.entry.Behaviors.Remove(control.entry.Behaviors.Where(p => p.GetType() == typeof(EntryMaskedBehavior)).FirstOrDefault());
                if (!string.IsNullOrEmpty(newvalue.ToString()) && !string.IsNullOrWhiteSpace(newvalue.ToString()))
                {
                    var behavior = new EntryMaskedBehavior();
                    behavior.Mask = newvalue.ToString();
                    control.entry.Behaviors.Add(behavior);

                }
            }
        }
        private static void AddValidationPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != oldvalue)
            {
                if (((newvalue != null) ? (bool)newvalue : false))
                {
                    if (control.entry.Behaviors.Any(p => p.GetType() == typeof(EntryLineValidationBehaviour)))
                        return;
                    var behavior = new EntryLineValidationBehaviour();
                    behavior.IsValid = (newvalue != null) ? (bool)newvalue : false;
                    control.entry.Behaviors.Add(behavior);
                }
                else
                {
                    if (control.entry.Behaviors.Any(p => p.GetType() == typeof(EntryLineValidationBehaviour)))
                        control.entry.Behaviors.Remove(control.entry.Behaviors.Where(p => p.GetType() == typeof(EntryLineValidationBehaviour)).FirstOrDefault());
                }
            }
        }
        private static void IsValidPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (EntryView)bindable;
            if (newvalue != oldvalue)
            {
                if (control.entry.Behaviors.Any(p => p.GetType() == typeof(EntryLineValidationBehaviour)))
                {
                    ((EntryLineValidationBehaviour)control.entry.Behaviors
                        .Where(p => p.GetType() == typeof(EntryLineValidationBehaviour)).FirstOrDefault()).IsValid = (newvalue != null) ? (bool)newvalue : false;
                }
            }
        }
        #endregion

        #region Events
        private void EntryValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = entry.Text ?? string.Empty;
        }
        #endregion
    }
}