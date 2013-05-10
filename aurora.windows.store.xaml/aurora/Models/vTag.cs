namespace aurora.Models
{
    public class vTag
    {
        #region Public Properties

        public int id { get; set; }

        public int postCount { get; set; }

        public post[] mostViewedPosts { get; set; }

        public post[] newPosts { get; set; }

        public int tagId { get; set; }

        public string title { get; set; }

        #endregion
    }
}