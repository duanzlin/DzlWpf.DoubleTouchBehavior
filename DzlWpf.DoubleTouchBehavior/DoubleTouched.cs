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

        public static readonly DependencyProperty TouchSharedProperty = DependencyProperty.RegisterAttached("TouchShared", typeof(bool), typeof(DoubleTouched), new PropertyMetadata(OnSetTouchSharedCallback));
        public static void SetTouchShared(UIElement element, bool shared)
        {
            element.SetValue(TouchSharedProperty, shared);
        }
        public static bool GetTouchShared(UIElement element)
        {
            return (bool)element.GetValue(TouchSharedProperty);
        }

        public static readonly DependencyProperty TouchInervalProperty = DependencyProperty.RegisterAttached("TouchInerval", typeof(int), typeof(DoubleTouched), new PropertyMetadata(2000, OnSetTouchInervalCallback));
        public static void SetTouchInerval(UIElement element, int shared)
        {
            element.SetValue(TouchInervalProperty, shared);
        }
        public static int GetTouchInerval(UIElement element)
        {
            return (int)element.GetValue(TouchInervalProperty);
        }

        private static void OnSetTouchInervalCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = dependencyObject as UIElement;
            if (element != null)
            {
                DoubleTouchCommandBehavior behavior = GetOrCreateBehavior(element);
                behavior.TouchInerval = (int)e.NewValue;
            }
        }

        private static void OnSetTouchSharedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = dependencyObject as UIElement;
            if (element != null)
            {
                DoubleTouchCommandBehavior behavior = GetOrCreateBehavior(element);
                behavior.TouchShared = (bool)e.NewValue;
            }
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
