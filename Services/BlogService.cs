// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using DTO;
    using Extensions;
    using Helpers;
    using Interfaces;
    using Models;
    using Repository.Interfaces;

    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _repository;

        private readonly string[] _reservedCharacters =
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

        public BlogService(IBlogRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<BlogPost>> GetPosts(int count, int skip = 0)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Count must be above 0.");
            }

            return Task.FromResult(
                _mapper.Map<IEnumerable<BlogPost>>(
                    _repository
                        .All(
                            //post =>
                            //    post.IsPublished,
                            null,
                            post =>
                                post.PublishedDate,
                            skip, count)));
        }

        public Task<IEnumerable<BlogPost>> GetPostsByCategory(string category)
        {
            Gaurd.ThrowIfNull(category);

            return Task.FromResult(
                _mapper.Map<IEnumerable<BlogPost>>(
                    _repository.All(post =>
                        post.Categories
                            .Any(c =>
                                c.Equals(category, StringComparison.OrdinalIgnoreCase)))));
        }

        public Task<BlogPost> GetPostBySlug(string slug)
        {
            Gaurd.ThrowIfNull(slug);

            return Task.FromResult(
                _mapper.Map<BlogPost>(
                    _repository
                        .All(
                            post =>
                                post.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault()));
        }

        public Task<BlogPost> GetPostById(Guid id)
        {
            Gaurd.ThrowIfNull(id);

            return Task.FromResult(
                _mapper.Map<BlogPost>(
                    _repository
                        .Get(post => post.Id.Equals(id))));
        }

        public Task<IEnumerable<string>> GetCategories()
        {
            return Task.FromResult(
                _repository
                    .All(
                        //post =>
                        //    post.IsPublished,
                        null)
                    .SelectMany(
                        post =>
                            post.Categories));
        }

        public Task<IEnumerable<BlogPost>> Search(string term)
        {
            Gaurd.ThrowIfNull(term);

            var comparison = StringComparison.OrdinalIgnoreCase;

            return Task.FromResult(
                _mapper.Map<IEnumerable<BlogPost>>(
                    _repository
                        .All(
                            post =>
                                (post.Title.Contains(term, comparison) ||
                                 post.Content.Contains(term, comparison) ||
                                 post.Description.Contains(term, comparison))
                                && post.IsPublished,
                            post =>
                                post.PublishedDate)));
        }

        public Task<BlogPost> AddComment(Guid id, Comment comment)
        {
            Gaurd.ThrowIfNull(id);
            Gaurd.ThrowIfNull(comment);

            var post = _repository.Get(p => p.Id.Equals(id));
            if (post == null)
            {
                return null;
            }

            post.Comments.Add(_mapper.Map<CommentModel>(comment));

            if (_repository.Update(post))
            {
                return Task.FromResult(_mapper.Map<BlogPost>(post));
            }

            return null;
        }

        public Task<BlogPost> DeleteComment(Guid postId, Guid commentId)
        {
            Gaurd.ThrowIfNull(postId);
            Gaurd.ThrowIfNull(commentId);

            var post = _repository.Get(p => p.Id.Equals(postId));

            var comment = post?.Comments.FirstOrDefault(c =>
                c.Id.Equals(commentId));

            if (comment != null)
            {
                post.Comments.Remove(comment);

                if (_repository.Update(post))
                {
                    return Task.FromResult(_mapper.Map<BlogPost>(post));
                }
                
            }

            return null;
        }

        public Task<bool> CreatePost(BlogPost post)
        {
            Gaurd.ThrowIfNull(post);

            var model = _mapper.Map<BlogPostModel>(post);

            model.PublishedDate = DateTime.UtcNow;

            if (string.IsNullOrEmpty(model.Slug))
            {
                model.Slug = GenerateSlug(model.Title);
            }

            if (_repository.Create(model))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> DeletePost(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Post id can not be empty");
            }

            var model = _repository
                .Get(post => post.Id.Equals(id));

            if (model != null)
            {
                return Task.FromResult(
                    _repository.Delete(model));
            }

            return Task.FromResult(false);
        }

        public Task<bool> UpdatePost(BlogPost post)
        {
            Gaurd.ThrowIfNull(post);

            var model = _mapper.Map<BlogPostModel>(post);
            model.ModifiedDate = DateTime.UtcNow;

            if (_repository.Update(model))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        /// <summary>
        ///     Removes URL reserved characters from the passed string
        /// </summary>
        /// <param name="text">The text to remove reserved characters from</param>
        /// <returns>The cleaned text</returns>
        private string RemoveReservedCharacters(string text)
        {
            return _reservedCharacters.Aggregate(text, (current, character) =>
                current.Replace(character, ""));
        }

        /// <summary>
        ///     Removes accented diatric text from a string
        /// </summary>
        /// <param name="text">The text to remove diatrics from</param>
        /// <returns>The cleaned text</returns>
        private string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (var c in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        ///     Generates a slug for the given title
        /// </summary>
        /// <param name="title">The title to generate a slug for</param>
        /// <returns>The slug</returns>
        private string GenerateSlug(string title)
        {
            title = title.ToLowerInvariant().Replace(" ", "-");
            title = RemoveDiacritics(title);
            title = RemoveReservedCharacters(title);

            return title.ToLowerInvariant();
        }
    }
}