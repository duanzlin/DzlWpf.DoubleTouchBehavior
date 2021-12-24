using System.Windows;
using System.Windows.Input;

namespace DzlWpf.DoubleTouchBehavior
{
    public class DoubleTouched
    {
        private static readonly DependencyProperty DoubleTouchCommandBehaviorProperty = DependencyProperty.RegisterAttached("DoubleTouchCommandBehavior", typeof(DoubleTouchCommandBehavior), typeof(DoubleTouched), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(DoubleTouched), new PropertyMetadata(OnSetCommandCallback));
        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
        public static ICommand GetCommand(UIElement element)
        {
            return element.GetValue(CommandProperty) as ICommand;
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(DoubleTouched), new PropertyMetadata(OnSetCommandParameterCallback));
        public static void SetCommandParameter(UIElement element, object parameter)
        {
            element.SetValue(CommandParameterProperty, parameter);
        }
        public static object GetCommandParameter(UIElement element)
        {
            return element.GetValue(CommandParameterProperty);
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = dependencyObject as UIElement;
            if (element != null)
            {
                DoubleTouchCommandBehavior behavior = GetOrCreateBehavior(element);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = dependencyObject as UIElement;
            if (element != null)
            {
                DoubleTouchCommandBehavior behavior = GetOrCreateBehavior(element);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static DoubleTouchCommandBehavior GetOrCreateBehavior(UIElement element)
        {
            DoubleTouchCommandBehavior behavior = element.GetValue(DoubleTouchCommandBehaviorProperty) as DoubleTouchCommandBehavior;
            if (behavior == null)
            {
                behavior = new DoubleTouchCommandBehavior(element);
                element.SetValue(DoubleTouchCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}
