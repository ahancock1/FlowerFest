﻿// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   IBlogService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Models;

    public interface IOldBlogService
    {
        Task<IEnumerable<Post>> GetPosts(int count, int skip = 0);

        Task<IEnumerable<Post>> GetPostsByCategory(string category);

        Task<Post> GetPostBySlug(string slug);

        Task<Post> GetPostById(string id);

        Task<IEnumerable<string>> GetCategories();

        Task SavePost(Post post);

        Task DeletePost(Post post);

        Task<IEnumerable<Post>> Search(string term);

        Task<string> SaveFile(byte[] bytes, string fileName, string suffix = null);
    }

    public abstract class InMemoryBlogServiceBase : IOldBlogService
    {
        public InMemoryBlogServiceBase(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }

        protected List<Post> Cache { get; set; }
        protected IHttpContextAccessor ContextAccessor { get; }

        public virtual Task<IEnumerable<Post>> GetPosts(int count, int skip = 0)
        {
            var isAdmin = IsAdmin();

            var posts = Cache
                .Where(p => p.PublishedDate <= DateTime.UtcNow && (p.IsPublished || isAdmin))
                .Skip(skip)
                .Take(count);

            return Task.FromResult(posts);
        }

        public virtual Task<IEnumerable<Post>> GetPostsByCategory(string category)
        {
            var isAdmin = IsAdmin();

            var posts = from p in Cache
                where p.PublishedDate <= DateTime.UtcNow && (p.IsPublished || isAdmin)
                where p.Categories.Contains(category, StringComparer.OrdinalIgnoreCase)
                select p;

            return Task.FromResult(posts);
        }

        public virtual Task<Post> GetPostBySlug(string slug)
        {
            var post = Cache.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
            var isAdmin = IsAdmin();

            if (post != null && post.PublishedDate <= DateTime.UtcNow && (post.IsPublished || isAdmin))
            {
                return Task.FromResult(post);
            }

            return Task.FromResult<Post>(null);
        }

        public virtual Task<Post> GetPostById(string id)
        {
            var post = Cache.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            var isAdmin = IsAdmin();

            if (post != null && post.PublishedDate <= DateTime.UtcNow && (post.IsPublished || isAdmin))
            {
                return Task.FromResult(post);
            }

            return Task.FromResult<Post>(null);
        }

        public virtual Task<IEnumerable<string>> GetCategories()
        {
            var isAdmin = IsAdmin();

            var categories = Cache
                .Where(p => p.IsPublished || isAdmin)
                .SelectMany(post => post.Categories)
                .Select(cat => cat.ToLowerInvariant())
                .Distinct();

            return Task.FromResult(categories);
        }

        public abstract Task SavePost(Post post);

        public abstract Task DeletePost(Post post);

        public abstract Task<string> SaveFile(byte[] bytes, string fileName, string suffix = null);

        protected void SortCache()
        {
            Cache.Sort((p1, p2) => p2.PublishedDate.CompareTo(p1.PublishedDate));
        }

        protected bool IsAdmin()
        {
            return ContextAccessor.HttpContext?.User?.Identity.IsAuthenticated == true;
        }

        public abstract Task<IEnumerable<Post>> Search(string term);
    }
}