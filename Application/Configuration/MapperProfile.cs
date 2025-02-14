using Application.DTO;
using Application.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Authenticate
            CreateMap<Account, AuthenticateDTO>().ReverseMap();
            CreateMap<Account, SignUpDTO>().ReverseMap();
            //Account
            CreateMap<Account, QueryAccountDTO>().ReverseMap();
            CreateMap<Account, CommandAccountDTO>().ReverseMap();
            //ComicCategory
            CreateMap<ComicCategory, QueryComicCategoryDTO>().ReverseMap();
            CreateMap<ComicCategory, CommandComicCategoryDTO>().ReverseMap();
            //Comic
            CreateMap<Comic, QueryComicDTO>().ReverseMap();
            CreateMap<Comic, CommandComicDTO>().ReverseMap();
            //ComicChapter
            CreateMap<ComicChapter, QueryComicChapterDTO>().ReverseMap();
            CreateMap<ComicChapter, CommandComicChapterDTO>().ReverseMap();
            //ComicChapterImage
            CreateMap<ComicChapterImage, QueryComicChapterImageDTO>().ReverseMap();
            CreateMap<ComicChapterImage, CommandComicChapterImageDTO>().ReverseMap();
            //ComicChapterComment
            CreateMap<ComicChapterComment, QueryComicChapterCommentDTO>().ReverseMap();
            CreateMap<ComicChapterComment, CommandComicChapterCommentDTO>().ReverseMap();
            //Bookmark
            CreateMap<Bookmark, QueryBookmarkDTO>().ReverseMap();
            CreateMap<Bookmark, CommandBookmarkDTO>().ReverseMap();
            //ForumTopicCategory
            CreateMap<ForumTopicCategory,QueryForumTopicCategoryDTO>().ReverseMap();
            CreateMap<ForumTopicCategory,CommandForumTopicCategoryDTO>().ReverseMap();
            //ForumTopic
            CreateMap<ForumTopic,QueryForumTopicDTO>().ReverseMap();
            CreateMap<ForumTopic,CommandForumTopicDTO>().ReverseMap();   
            //ForumTopicComment
            CreateMap<ForumTopicComment, QueryForumTopicCommentDTO>().ReverseMap();
            CreateMap<ForumTopicComment, CommandForumTopicCommentDTO>().ReverseMap();
        }
    }
}
