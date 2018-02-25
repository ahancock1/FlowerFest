// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   PostHelper.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using Models;
    using ViewModels.Blog;

    public static class PostHelper
    {


        /// <summary>
        ///     Checks to see if the post is older than a given number of days and returns true if comments
        ///     are still permitted.
        /// </summary>
        /// <param name="post">The post to check</param>
        /// <param name="days">The number of days permitted before comments close</param>
        /// <returns></returns>
        public static bool AreCommentsOpen(Post post, int days)
        {
            return post.PublishedDate.AddDays(days) >= DateTime.UtcNow;
        }

        /// <summary>
        ///     Compiles the content including any videos
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static string CompileContent(PostDetailViewModel post)
        {
            var result = post.Content;

            // Set up lazy loading of images/iframes
            result = result.Replace(" src=\"",
                " src=\"data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==\" data-src=\"");

            // Youtube content embedded using this syntax: [youtube:xyzAbc123]
            var video =
                "<div class=\"video\"><iframe width=\"560\" height=\"315\" title=\"YouTube embed\" src=\"about:blank\" data-src=\"https://www.youtube-nocookie.com/embed/{0}?modestbranding=1&amp;hd=1&amp;rel=0&amp;theme=light\" allowfullscreen></iframe></div>";
            result = Regex.Replace(result, @"\[youtube:(.*?)\]", m => string.Format(video, m.Groups[1].Value));

            return result;
        }

        public static string CompileContent(Comment comment)
        {
            return comment.Content;
        }

        public static string GetGravatar(CommentViewModel comment)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(comment.Email.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (var i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return $"https://www.gravatar.com/avatar/{sb.ToString().ToLowerInvariant()}?s=60&d=blank";
            }
        }
    }
}