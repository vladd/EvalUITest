using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace EvalUITest
{
    class CaretEndBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            OnVmTextChanged();
            AssociatedObject.TextChanged += OnControlTextChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.TextChanged -= OnControlTextChanged;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text", typeof(string), typeof(CaretEndBehavior),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                              (o, args) => ((CaretEndBehavior)o).OnVmTextChanged()));

        bool isInControlInitiatedChange = false;
        void OnVmTextChanged()
        {
            var text = Text;
            if (isInControlInitiatedChange)
                return;
            if (AssociatedObject != null) // blendability
            {
                AssociatedObject.Text = text;
                AssociatedObject.Select(text?.Length ?? 0, 0);
            }
        }

        void OnControlTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                isInControlInitiatedChange = true;
                SetCurrentValue(TextProperty, AssociatedObject.Text);
            }
            finally
            {
                isInControlInitiatedChange = false;
            }
        }
    }
}
