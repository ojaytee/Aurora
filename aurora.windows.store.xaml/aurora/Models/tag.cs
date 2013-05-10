namespace aurora.Models
{
    using System.Collections.Generic;

    public class tag
    {
        #region Constructors and Destructors

        public tag()
        {
            this.posts = new HashSet<post>();
        }

        #endregion

        #region Public Properties

        public long id { get; set; }

        public ICollection<post> posts { get; set; }

        public string title { get; set; }

        #endregion
    }
}