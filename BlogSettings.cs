// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   BlogSettings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest
{
    public class BlogSettings
    {
        public string Name { get; set; } = "FlowerFest";
        public string Description { get; set; } = "Christchurch's new event to showcase the benefit of flowers, nature and gardening to everyone's mental health and wellbeing.";
        public string Owner { get; set; } = "Glenda Stansbury";
        public int PostsPerPage { get; set; } = 24;
        public int CommentsCloseAfterDays { get; set; } = 31;
    }
}