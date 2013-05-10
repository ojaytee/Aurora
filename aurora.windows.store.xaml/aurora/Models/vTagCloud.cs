﻿namespace aurora
{
    using Windows.UI.Text;

    public class vTagCloud
    {
        #region Public Properties

        public int id { get; set; }

        public int posts { get; set; }

        public string tagName { get; set; }

        #endregion
    }

    public class TagViewModel
    {
        #region Public Properties

        public double FontSize { get; set; }

        public FontWeight FontWeight { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public double NormalizedPosts { get; set; }

        public int Posts { get; set; }

        #endregion
    }
}