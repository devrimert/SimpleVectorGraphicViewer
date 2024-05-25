using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleVectorGraphicViewer.Methods.EventHelpers
{

    public static class MouseWheelHelper
    {
        public static readonly DependencyProperty ZoomInCommandProperty =
            DependencyProperty.RegisterAttached(
                "ZoomInCommand",
                typeof(ICommand),
                typeof(MouseWheelHelper),
                new PropertyMetadata(OnCommandChanged));

        public static readonly DependencyProperty ZoomOutCommandProperty =
            DependencyProperty.RegisterAttached(
                "ZoomOutCommand",
                typeof(ICommand),
                typeof(MouseWheelHelper),
                new PropertyMetadata(OnCommandChanged));

        public static void SetZoomInCommand(UIElement element, ICommand value)
        {
            element.SetValue(ZoomInCommandProperty, value);
        }

        public static ICommand GetZoomInCommand(UIElement element)
        {
            return (ICommand)element.GetValue(ZoomInCommandProperty);
        }

        public static void SetZoomOutCommand(UIElement element, ICommand value)
        {
            element.SetValue(ZoomOutCommandProperty, value);
        }

        public static ICommand GetZoomOutCommand(UIElement element)
        {
            return (ICommand)element.GetValue(ZoomOutCommandProperty);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if (e.OldValue == null && e.NewValue != null)
                {
                    element.PreviewMouseWheel += OnMouseWheel;
                }
                else if (e.OldValue != null && e.NewValue == null)
                {
                    element.PreviewMouseWheel -= OnMouseWheel;
                }
            }
        }

        private static void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is UIElement element)
            {
                var zoomInCommand = GetZoomInCommand(element);
                var zoomOutCommand = GetZoomOutCommand(element);

                if (e.Delta > 0 && zoomInCommand != null && zoomInCommand.CanExecute(null))
                {
                    zoomInCommand.Execute(null);
                }
                else if (e.Delta < 0 && zoomOutCommand != null && zoomOutCommand.CanExecute(null))
                {
                    zoomOutCommand.Execute(null);
                }
            }
        }
    }
}
