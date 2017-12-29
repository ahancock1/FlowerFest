// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   PostHelper.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Helpers
{
    using FlowerFest.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class PostHelper
    {
        private static IEnumerable<string> _reservedCharacters = new List<string>
            {
                "!",
                "#",
                "$",
                "&",
                "'",
                "(",
                ")",
                "*",
                ",",
                "/",
                ":",
                ";",
                "=",
                "?",
                "@",
                "[",
                "]",
                "\"",
                "%",
                ".",
                "<",
                ">",
                "\\",
                "^",
                "_",
                "'",
                "{",
                "}",
                "|",
                "~",
                "`",
                "+"
            };

        /// <summary>
        /// Removes URL reserved characters from the passed string
        /// </summary>
        /// <param name="text">The text to remove reserved characters from</param>
        /// <returns>The cleaned text</returns>
        public static string RemoveReservedCharacters(string text)
        {
            foreach (var chr in _reservedCharacters)
            {
                text = text.Replace(chr, "");
            }

            return text;
        }

        /// <summary>
        /// Removes accented diatric text from a string
        /// </summary>
        /// <param name="text">The text to remove diatrics from</param>
        /// <returns>The cleaned text</returns>
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Generates a slug for the given title
        /// </summary>
        /// <param name="title">The title to generate a slug for</param>
        /// <returns>The slug</returns>
        public static string GenerateSlug(string title)
        {
            title = title.ToLowerInvariant().Replace(" ", "-");
            title = RemoveDiacritics(title);
            title = RemoveReservedCharacters(title);

            return title.ToLowerInvariant();
        }

        /// <summary>
        /// Checks to see if the post is older than a given number of days and returns true if comments
        /// are still permitted.
        /// </summary>
        /// <param name="post">The post to check</param>
        /// <param name="days">The number of days permitted before comments close</param>
        /// <returns></returns>
        public static bool AreCommentsOpen(Post post, int days)
        {
            return post.PublishedDate.AddDays(days) >= DateTime.UtcNow;
        }

        /// <summary>
        /// Compiles the content including any videos
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static string CompileContent(Post post)
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

        public static string GetGravatar(Comment comment)
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
