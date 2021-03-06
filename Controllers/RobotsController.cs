﻿// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   RobotsController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using FlowerFest;
    using Microsoft.AspNetCore.Hosting;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.SyndicationFeed;
    using Microsoft.SyndicationFeed.Atom;
    using Microsoft.SyndicationFeed.Rss;

    public class RobotsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IOptionsSnapshot<BlogSettings> _settings;

        public RobotsController(
            IBlogService blogService,
            IHostingEnvironment environment,
            IOptionsSnapshot<BlogSettings> settings)
        {
            _blogService = blogService;
            _settings = settings;
        }

        [Route("/robots.txt")]
        [OutputCache(Profile = "default")]
        public string RobotsTxt()
        {
            var host = Request.Scheme + "://" + Request.Host;
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *");
            sb.AppendLine("Disallow:");
            sb.AppendLine($"sitemap: {host}/sitemap.xml");

            return sb.ToString();
        }

        [Route("/sitemap.xml")]
        public async Task SitemapXml()
        {
            var host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings {Indent = true}))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                var posts = await _blogService.GetPosts(int.MaxValue);

                foreach (var post in posts)
                {
                    var lastMod = new[] {post.PublishedDate, post.ModifiedDate};

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", $"{host}/Blog/{post.Slug}");
                    xml.WriteElementString("lastmod", lastMod.Max().ToString("yyyy-MM-ddThh:mmzzz"));
                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
            }
        }

        [Route("/rsd.xml")]
        public void RsdXml()
        {
            var host = Request.Scheme + "://" + Request.Host;

            Response.ContentType = "application/xml";
            Response.Headers["cache-control"] = "no-cache, no-store, must-revalidate";

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings {Indent = true}))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("rsd");
                xml.WriteAttributeString("version", "1.0");

                xml.WriteStartElement("service");

                xml.WriteElementString("enginename", "FlowerFestival");
                xml.WriteElementString("enginelink", "http://github.com/madskristensen/FlowerFestival/");
                xml.WriteElementString("homepagelink", host);

                xml.WriteStartElement("apis");
                xml.WriteStartElement("api");
                xml.WriteAttributeString("name", "MetaWeblog");
                xml.WriteAttributeString("preferred", "true");
                xml.WriteAttributeString("apilink", host + "/metaweblog");
                xml.WriteAttributeString("blogid", "1");

                xml.WriteEndElement(); // api
                xml.WriteEndElement(); // apis
                xml.WriteEndElement(); // service
                xml.WriteEndElement(); // rsd
            }
        }

        [Route("/feed/{type}")]
        public async Task Rss(string type)
        {
            Response.ContentType = "application/xml";
            var host = Request.Scheme + "://" + Request.Host;

            using (var xmlWriter = XmlWriter.Create(Response.Body, new XmlWriterSettings {Async = true, Indent = true}))
            {
                var posts = (await _blogService.GetPosts(10)).ToList();
                var writer = await GetWriter(type, xmlWriter, posts.Max(p => p.PublishedDate));

                foreach (var post in posts)
                {
                    var item = new AtomEntry
                    {
                        Title = post.Title,
                        Description = post.Content,
                        Id = $"{host}/Blog/{post.Slug}",
                        Published = post.PublishedDate,
                        LastUpdated = post.ModifiedDate,
                        ContentType = "html"
                    };

                    foreach (var category in post.Categories)
                    {
                        item.AddCategory(new SyndicationCategory(category));
                    }

                    item.AddContributor(new SyndicationPerson(_settings.Value.Owner, "test@example.com"));
                    item.AddLink(new SyndicationLink(new Uri(item.Id)));

                    await writer.Write(item);
                }
            }
        }

        private async Task<ISyndicationFeedWriter> GetWriter(string type, XmlWriter xmlWriter, DateTime updated)
        {
            var host = Request.Scheme + "://" + Request.Host + "/";

            if (type.Equals("rss", StringComparison.OrdinalIgnoreCase))
            {
                var rss = new RssFeedWriter(xmlWriter);
                await rss.WriteTitle(_settings.Value.Name);
                await rss.WriteDescription(_settings.Value.Description);
                await rss.WriteGenerator("FlowerFestival");
                await rss.WriteValue("link", host);
                return rss;
            }

            var atom = new AtomFeedWriter(xmlWriter);
            await atom.WriteTitle(_settings.Value.Name);
            await atom.WriteId(host);
            await atom.WriteSubtitle(_settings.Value.Description);
            await atom.WriteGenerator("FlowerFestival", "https://github.com/madskristensen/FlowerFestival", "1.0");
            await atom.WriteValue("updated", updated.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            return atom;
        }
    }
}