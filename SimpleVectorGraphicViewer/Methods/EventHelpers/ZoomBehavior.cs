using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleVectorGraphicViewer.Methods.EventHelpers
{
    public class ZoomBehavior : DependencyObject
    {
        #region IsEnabled attached property

        // Required
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
          "IsEnabled", typeof(bool), typeof(ZoomBehavior), new PropertyMetadata(default(bool), ZoomBehavior.OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject attachingElement, bool value) => attachingElement.SetValue(ZoomBehavior.IsEnabledProperty, value);

        public static bool GetIsEnabled(DependencyObject attachingElement) => (bool)attachingElement.GetValue(ZoomBehavior.IsEnabledProperty);

        #endregion

        #region ZoomFactor attached property

        // Optional
        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.RegisterAttached(
          "ZoomFactor", typeof(double), typeof(ZoomBehavior), new PropertyMetadata(0.1));

        public static void SetZoomFactor(DependencyObject attachingElement, double value) => attachingElement.SetValue(ZoomBehavior.ZoomFactorProperty, value);

        public static double GetZoomFactor(DependencyObject attachingElement) => (double)attachingElement.GetValue(ZoomBehavior.ZoomFactorProperty);

        #endregion

        #region ScrollViewer attached property

        // Optional
        public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.RegisterAttached(
          "ScrollViewer", typeof(ScrollViewer), typeof(ZoomBehavior), new PropertyMetadata(default(ScrollViewer)));

        public static void SetScrollViewer(DependencyObject attachingElement, ScrollViewer value) => attachingElement.SetValue(ZoomBehavior.ScrollViewerProperty, value);

        public static ScrollViewer GetScrollViewer(DependencyObject attachingElement) => (ScrollViewer)attachingElement.GetValue(ZoomBehavior.ScrollViewerProperty);

        #endregion

        private static void OnIsEnabledChanged(DependencyObject attachingElement, DependencyPropertyChangedEventArgs e)
        {
            if (!(attachingElement is FrameworkElement frameworkElement))
            {
                throw new ArgumentException("Attaching element must be of type FrameworkElement");
            }

            bool isEnabled = (bool)e.NewValue;
            if (isEnabled)
            {
                frameworkElement.PreviewMouseWheel += ZoomBehavior.Zoom_OnMouseWheel;
                frameworkElement.SizeChanged += ZoomBehavior.OnSizeChanged;
                frameworkElement.LayoutUpdated += ZoomBehavior.OnLayoutUpdated;

                if (ZoomBehavior.TryGetScaleTransform(frameworkElement, out _))
                {
                    return;
                }

                if (frameworkElement.LayoutTransform is TransformGroup transformGroup)
                {
                    transformGroup.Children.Add(new ScaleTransform());
                }
                else
                {
                    frameworkElement.LayoutTransform = new ScaleTransform();
                }
            }
            else
            {
                frameworkElement.PreviewMouseWheel -= ZoomBehavior.Zoom_OnMouseWheel;
                frameworkElement.SizeChanged -= ZoomBehavior.OnSizeChanged;
                frameworkElement.LayoutUpdated -= ZoomBehavior.OnLayoutUpdated;
            }
        }

        private static void Zoom_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var zoomTargetElement = sender as FrameworkElement;

            Point mouseCanvasPosition = e.GetPosition(zoomTargetElement);
            double scaleFactor = e.Delta > 0
              ? ZoomBehavior.GetZoomFactor(zoomTargetElement)
              : -1 * ZoomBehavior.GetZoomFactor(zoomTargetElement);

            ZoomBehavior.ApplyZoomToAttachedElement(mouseCanvasPosition, scaleFactor, zoomTargetElement);

            ZoomBehavior.AdjustScrollViewer(mouseCanvasPosition, scaleFactor, zoomTargetElement);
        }

        private static void ApplyZoomToAttachedElement(Point mouseCanvasPosition, double scaleFactor, FrameworkElement zoomTargetElement)
        {
            if (!ZoomBehavior.TryGetScaleTransform(zoomTargetElement, out ScaleTransform scaleTransform))
            {
                throw new InvalidOperationException("No ScaleTransform found");
            }

            scaleTransform.CenterX = mouseCanvasPosition.X;
            scaleTransform.CenterY = mouseCanvasPosition.Y;

            scaleTransform.ScaleX = Math.Max(0.5, scaleTransform.ScaleX + scaleFactor);
            scaleTransform.ScaleY = Math.Max(0.5, scaleTransform.ScaleY + scaleFactor);
        }

        private static void AdjustScrollViewer(Point mouseCanvasPosition, double scaleFactor, FrameworkElement zoomTargetElement)
        {
            ScrollViewer scrollViewer = ZoomBehavior.GetScrollViewer(zoomTargetElement);
            if (scrollViewer == null)
            {
                return;
            }

            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + mouseCanvasPosition.X * scaleFactor);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + mouseCanvasPosition.Y * scaleFactor);
        }

        private static bool TryGetScaleTransform(FrameworkElement frameworkElement, out ScaleTransform scaleTransform)
        {
            scaleTransform = null;

            if (frameworkElement.LayoutTransform is TransformGroup transformGroup)
            {
                scaleTransform = transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
            }
            else if (frameworkElement.LayoutTransform is ScaleTransform transform)
            {
                scaleTransform = transform;
            }

            return scaleTransform != null;
        }

        private static void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                EnsureAllChildrenVisible(frameworkElement);
            }
        }

        private static void OnLayoutUpdated(object sender, EventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                EnsureAllChildrenVisible(frameworkElement);
            }
        }

        private static void EnsureAllChildrenVisible(FrameworkElement frameworkElement)
        {
            if (!TryGetScaleTransform(frameworkElement, out ScaleTransform scaleTransform))
            {
                return;
            }

            var children = ((Canvas)frameworkElement).Children.OfType<FrameworkElement>();
            foreach (var child in children)
            {
                var childBounds = child.TransformToAncestor(frameworkElement).TransformBounds(new Rect(0, 0, child.ActualWidth, child.ActualHeight));

                if (childBounds.Left < 0 || childBounds.Top < 0 ||
                    childBounds.Right > frameworkElement.ActualWidth ||
                    childBounds.Bottom > frameworkElement.ActualHeight)
                {
                    // Perform Zoom Out
                    scaleTransform.ScaleX = Math.Max(0.5, scaleTransform.ScaleX - 0.1);
                    scaleTransform.ScaleY = Math.Max(0.5, scaleTransform.ScaleY - 0.1);
                    return; // After adjusting zoom, exit to allow layout to update
                }
            }
        }
    }
}
