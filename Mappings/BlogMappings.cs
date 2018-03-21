// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using System.Text.RegularExpressions;
    using AutoMapper;
    using DTO;
    using Models;

    public class BlogMappings : ContentMapping
    {
        public override void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<BlogPost, BlogPostModel>();
            config.CreateMap<BlogPostModel, BlogPost>();
        }

        private string CompileContent(string content)
        {
            // Set up lazy loading of images/iframes
            content = content.Replace(" src=\"",
                " src=\"data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==\" data-src=\"");

            // Youtube content embedded using this syntax: [youtube:xyzAbc123]
            var video =
                "<div class=\"video\">" +
                "<iframe width=\"560\" height=\"315\" " +
                "title=\"YouTube embed\" src=\"about:blank\" " +
                "data-src=\"https://www.youtube-nocookie.com/embed/{0}?modestbranding=1&amp;hd=1&amp;rel=0&amp;theme=light\" " +
                "allowfullscreen>" +
                "</iframe>" +
                "</div>";
            content = Regex.Replace(content, @"\[youtube:(.*?)\]", m => string.Format(video, m.Groups[1].Value));

            return content;
        }
    }
}