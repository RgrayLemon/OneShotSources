﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace mouseFocus
{
    public class FocusAdorner : Adorner
    {
        // Be sure to call the base class constructor.
        public FocusAdorner(UIElement adornedElement)
          : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout system as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            //var drawRect = LayoutInformation.GetLayoutSlot((FrameworkElement)this.AdornedElement);

            // 修正
            var element = (FrameworkElement)this.AdornedElement;
            var drawRect = new Rect(0, 0, element.ActualWidth, element.ActualHeight);

            drawRect = new Rect(1, 1, drawRect.Width - 2, drawRect.Height - 2);

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Transparent);
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Black), 1);
            renderPen.DashStyle = new DashStyle(new double[] { 1, 2 }, 0);

            drawingContext.DrawRoundedRectangle(renderBrush, renderPen, drawRect, 3, 3);
        }
    }
}
