namespace aurora
{
    using System;

    public class Posts
    {
        #region Public Properties

        public int? acceptedAnswerId { get; set; }

        public int? answerCount { get; set; }

        public DateTime? closedDate { get; set; }

        public int? commentCount { get; set; }

        public DateTime? communityOwnedDate { get; set; }

        public DateTime? creationDate { get; set; }

        public int? favoriteCount { get; set; }

        public int id { get; set; }

        public DateTime? lastActivityDate { get; set; }

        public DateTime? lastEditDate { get; set; }

        public int? lastEditorUserId { get; set; }

        public int? ownerUserId { get; set; }

        public int? rowId { get; set; }

        public int? score { get; set; }

        public string title { get; set; }

        public int? viewCount { get; set; }

        #endregion
    }
}