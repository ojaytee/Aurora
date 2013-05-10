namespace aurora.Models
{
    using System;
    using System.Text;

    using Windows.UI.Text;
    using Windows.UI.Xaml;

    public class TagViewModel
    {
        #region Static Fields

        private static readonly Random random = new Random();

        #endregion

        #region Public Properties

        public double FontSize { get; private set; }

        public FontWeight FontWeight { get; private set; }

        public int Id { get; set; }

        public Thickness Margin { get; private set; }

        public int PostCount { get; set; }

        public post[] MostViewedPosts { get; set; }

        public post[] NewPosts { get; set; }

        public string Title { get; set; }

        public VerticalAlignment VerticalAlignment { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Normalize(double min, double max)
        {
            double n = (this.PostCount - min) / max;

            this.FontSize = 18.0 + (54.0 * n);
            if (n > 0.75)
            {
                this.FontWeight = FontWeights.Black;
            }
            else if (n > 0.50)
            {
                this.FontWeight = FontWeights.Normal;
            }
            else
            {
                this.FontWeight = FontWeights.Light;
            }

            double leftMargin = 10.0 + random.Next(50);
            this.Margin = new Thickness(leftMargin, 0, 10, 0);

            int alignment = random.Next(3);
            if (alignment == 0)
            {
                this.VerticalAlignment = VerticalAlignment.Top;
            }
            else if (alignment == 1)
            {
                this.VerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                this.VerticalAlignment = VerticalAlignment.Bottom;
            }
        }

        private string PostToString(post post)
        {
            var builder = new StringBuilder();

            builder.Append("- ");
            builder.AppendLine(post.title);

            builder.Append("  ");
            if (post.score.HasValue && post.score.Value > 0)
            {
                builder.Append(post.score.Value.ToString("#,###,###"));
                builder.Append(" | ");
            }
            else
            {
                builder.Append("no score | ");
            }

            if (post.viewCount.HasValue && post.viewCount.Value > 0)
            {
                builder.Append(post.viewCount.Value.ToString("#,###,###"));
                builder.Append(" views | ");
            }
            else
            {
                builder.Append("no views | ");
            }

            if (post.answerCount.HasValue && post.answerCount.Value > 0)
            {
                builder.Append(post.answerCount.Value.ToString("#,###,###"));
                builder.Append(" answers | ");
            }
            else
            {
                builder.Append("no answers | ");
            }

            if (post.commentCount.HasValue && post.commentCount.Value > 0)
            {
                builder.Append(post.commentCount.Value.ToString("#,###,###"));
                builder.Append(" comments | ");
            }
            else
            {
                builder.Append("no comments | ");
            }

            if (post.favoriteCount.HasValue && post.favoriteCount.Value > 0)
            {
                builder.Append(post.favoriteCount.Value.ToString("#,###,###"));
                builder.AppendLine(" favorites");
            }
            else
            {
                builder.AppendLine("no favortites");
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(this.PostCount.ToString("#,###,###") + " posts");
            builder.AppendLine();
            builder.AppendLine("MOST VIEWED POSTS");
            foreach (var post in this.MostViewedPosts)
            {
                builder.Append(this.PostToString(post));
            }

            builder.AppendLine();
            builder.AppendLine("NEW POSTS");
            foreach (var post in this.NewPosts)
            {
                builder.Append(this.PostToString(post));
            }

            return builder.ToString();
        }

        #endregion
    }
}