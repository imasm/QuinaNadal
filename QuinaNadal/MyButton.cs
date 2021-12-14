using System.Windows;
using System.Windows.Controls;

namespace QuinaNadal
{
    public class MyButton : Button
    {
        public static readonly DependencyProperty MarcatProperty =

            DependencyProperty.Register(
            "Marcat",
            typeof(bool),
            typeof(MyButton),
            new FrameworkPropertyMetadata(false));

        public bool Marcat
        {
            get => (bool)GetValue(MarcatProperty);
            set => SetValue(MarcatProperty, value);
        }

        protected override void OnClick()
        {
            base.OnClick();
            Marcat = !Marcat;
        }
    }
}