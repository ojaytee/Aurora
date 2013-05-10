namespace aurora
{
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class WrapPanel : Panel
    {
        #region Static Fields

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation), typeof(WrapPanel), null);

        #endregion

        #region Constructors and Destructors

        public WrapPanel()
        {
            this.Orientation = Orientation.Horizontal;
        }

        #endregion

        #region Public Properties

        public Orientation Orientation
        {
            get
            {
                return (Orientation)this.GetValue(OrientationProperty);
            }

            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }

        #endregion

        #region Methods

        protected override Size ArrangeOverride(Size finalSize)
        {
            var point = new Point(0, 0);

            int i = 0;

            if (this.Orientation == Orientation.Horizontal)
            {
                double largestHeight = 0.0;

                foreach (var child in this.Children)
                {
                    child.Arrange(
                        new Rect(
                            point, new Point(point.X + child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));

                    if (child.DesiredSize.Height > largestHeight)
                    {
                        largestHeight = child.DesiredSize.Height;
                    }

                    point.X = point.X + child.DesiredSize.Width;

                    if ((i + 1) < this.Children.Count)
                    {
                        if ((point.X + this.Children[i + 1].DesiredSize.Width) > finalSize.Width)
                        {
                            point.X = 0;
                            point.Y = point.Y + largestHeight;
                            largestHeight = 0.0;
                        }
                    }

                    i++;
                }
            }
            else
            {
                double largestWidth = 0.0;

                foreach (var child in this.Children)
                {
                    child.Arrange(
                        new Rect(
                            point, new Point(point.X + child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));

                    if (child.DesiredSize.Width > largestWidth)
                    {
                        largestWidth = child.DesiredSize.Width;
                    }

                    point.Y = point.Y + child.DesiredSize.Height;

                    if ((i + 1) < this.Children.Count)
                    {
                        if ((point.Y + this.Children[i + 1].DesiredSize.Height) > finalSize.Height)
                        {
                            point.Y = 0;
                            point.X = point.X + largestWidth;
                            largestWidth = 0.0;
                        }
                    }

                    i++;
                }
            }

            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var child in this.Children)
            {
                child.Measure(new Size(availableSize.Width, availableSize.Height));
            }

            return base.MeasureOverride(availableSize);
        }

        #endregion
    }
}