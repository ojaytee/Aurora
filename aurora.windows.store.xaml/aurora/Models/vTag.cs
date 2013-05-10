namespace aurora.Models
{
    public class vTag
    {
        #region Public Properties

        public int id { get; set; }

        public int postCount { get; set; }

        public post[] posts { get; set; }

        public int tagId { get; set; }

        public string title { get; set; }

        #endregion
    }
}