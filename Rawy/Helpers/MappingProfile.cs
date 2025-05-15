using AutoMapper;
using core.Models;
using Rawy.Dtos;
using Rawy.Dtos.catygoryDtos;
using Rawy.Dtos.favoriteDtos;
using Rawy.Dtos.PlayListDtos;

namespace Rawy.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //----------------------------------------------------------/User
            //CreateMap<BaseUser, showuserdto>()
            //    .ForMember(b => b.Id, db => db.MapFrom(a => a.Id))
            //    .ForMember(b => b.DisplayName, db => db.MapFrom(a => a.DisplayName))
            //    .ForMember(b => b.ProfilePicture, db => db.MapFrom(a => a.ProfilePicture))
            //    .ForMember(b => b.DateJoined, db => db.MapFrom(a => a.DateJoined))
            //    .ForMember(b => b.Reviews, db => db.MapFrom(a => a.Reviews))
            //    .ForMember(b => b.Records, db => db.MapFrom(a => a.Records))
            //    .ForMember(b => b.Cv_Url, db => db.MapFrom(a => a.Cv_Url))
            //    .ForMember(b => b.Favorites, db => db.MapFrom(a => a.Favorites))
            //    .ForMember(b => b.Playlists, db => db.MapFrom(a => a.Playlists));
            //________________________________________________________/Book

            CreateMap<Book, bookdtos>()
                
                 .ForMember(b => b.Aurthorname, db => db.MapFrom(a => a.Aurthor))
          
                 .ForMember(dest => dest.RecordDtos, opt => opt.MapFrom(src => src.record.Where(r => r.Okay_Record)))
                 .ForMember(b => b.book, db => db.MapFrom(a => a.bookurl))
                 .ForMember(b => b.catygoriesname, db => db.MapFrom(a => a.catygories))
                 .ForMember(b => b.reviewsdtos, db => db.MapFrom(a => a.reviews))
                 .ForMember(b => b.CoverImage, db => db.MapFrom<bookReturnPicReSolve>()).ReverseMap()
                     .ForMember(dest => dest.AurthorId, opt => opt.MapFrom(src => src.AurthorId))
                 .ForMember(dest => dest.record, opt => opt.Ignore()) 
                 .ForMember(dest => dest.Aurthor, opt => opt.Ignore())
                 .ForMember(dest => dest.reviews, opt => opt.Ignore())
                 .ForMember(dest => dest.catygories, opt => opt.Ignore());

            //________________________________________________________/Catygory

            CreateMap<Catygory, CatygoryDtos>()
    
                .ForMember(b => b.Type, O => O.MapFrom(a => a.Type));

            CreateMap<Catygory, CatygorybyidDtos >()

    .ForMember(b => b.Name, O => O.MapFrom(a => a.Type))
    .ForMember(b => b.bookdtos, O => O.MapFrom(a => a.books));

            CreateMap<AddCatygoryDto, Catygory>()
    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdateCatygoryDto, Catygory>()
     
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Name)).ReverseMap();

            //________________________________________________________/Record

            CreateMap<RecordDtos, Record>()
     
         .ForMember(dest => dest.AudioFile, opt => opt.MapFrom(src => src.AudioFile))
         .ForMember(dest => dest.DatePosted, opt => opt.MapFrom(src => src.DatePosted))
         .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.bookId)) 
         .ForMember(dest => dest.Okay_Record, opt => opt.MapFrom(src => src.IsRecording)) 
         .ForMember(dest => dest.episodeId, opt => opt.MapFrom(src => src.episodeId)) 
         .ForMember(dest => dest.BaseUserId, opt => opt.Ignore())
         .ForMember(dest => dest.User, opt => opt.Ignore()).ReverseMap();

            //________________________________________________________/Review

            CreateMap<Review, ReviewDto>()
 
                          .ForMember(b => b.Comment, db => db.MapFrom(a => a.Comment))
                          .ForMember(b => b.Rating, db => db.MapFrom(a => a.Rating))
                          .ForMember(b => b.BookId, db => db.MapFrom(a => a.BookId))
                          .ForMember(b => b.UserId, db => db.MapFrom(a => a.UserId))
                          .ForMember(b => b.episodeId, db => db.MapFrom(a => a.episodeId))
                          .ForMember(b => b.ProdcastId, db => db.MapFrom(a => a.ProdcastId))
                          .ForMember(b => b.DatePosted, db => db.MapFrom(a => a.DatePosted)).ReverseMap();

            CreateMap<Review, AddReviewDto>()
              .ForMember(b => b.Comment, db => db.MapFrom(a => a.Comment))
              .ForMember(b => b.Rating, db => db.MapFrom(a => a.Rating))
              .ForMember(b => b.BookId, db => db.MapFrom(a => a.BookId)).ReverseMap();

            //________________________________________________________/Aurthor
            CreateMap<Aurthor, AuthorDtos>()
                
                     .ForMember(b => b.Name, db => db.MapFrom(a => a.Name))
                    
                     .ForMember(b => b.Descriotion, db => db.MapFrom(a => a.Descriotion))
                     .ForMember(b => b.ProfilePicture, db => db.MapFrom(a => a.ProfilePicture)).ReverseMap();
            CreateMap<Aurthor, authoridDtos>()

              .ForMember(b => b.Name, db => db.MapFrom(a => a.Name))

              .ForMember(b => b.Descriotion, db => db.MapFrom(a => a.Descriotion))
              .ForMember(b => b.ProfilePicture, db => db.MapFrom(a => a.ProfilePicture))
              .ForMember(b => b.Books, db => db.MapFrom(a => a.Books))
              .ReverseMap();
            ;
            //________________________________________________________/Favorite

            CreateMap<Favorite, FavoriteDtos>()
                    .ForMember(b => b.Id, db => db.MapFrom(a => a.Id))
              .ForMember(b => b.Name, O => O.MapFrom(a => a.Name))
              .ForMember(b => b.Books, O => O.MapFrom(a => a.Books)).ReverseMap();
            ;
            CreateMap<AddFavoriteDto, Favorite>()
           .ForMember(dest => dest.Books, opt => opt.Ignore()).ReverseMap();

            CreateMap<UpdateFavoriteDto, Favorite>()
                    .ForMember(b => b.Id, db => db.MapFrom(a => a.Id))
           .ForMember(dest => dest.Books, opt => opt.Ignore()).ReverseMap();
            //________________________________________________________/Prodcast


            CreateMap<Prodcast,ProdcastDto>()
   
              .ForMember(b => b.Prodcastname, O => O.MapFrom(a => a.Prodcastname))
              .ForMember(b => b.Prodcastimage, O => O.MapFrom(a => a.Prodcastimage))
              .ForMember(b => b.episodeDtos, O => O.MapFrom(a => a.episode))
              .ForMember(b => b.reviews, O => O.MapFrom(a => a.reviews))
              .ForMember(b => b.ReleaseDate, O => O.MapFrom(a => a.ReleaseDate)).
              ReverseMap()
            ;
            // Create
            CreateMap<CreateProdcastDto, Prodcast>()
                .ForMember(dest => dest.episode, opt => opt.Ignore())
                .ForMember(dest => dest.reviews, opt => opt.Ignore());

            // Update
            CreateMap<UpdateProdcastDto, Prodcast>()
                .ForMember(dest => dest.episode, opt => opt.Ignore())
                .ForMember(dest => dest.reviews, opt => opt.Ignore());

            //________________________________________________________/PlayList
            CreateMap<Playlist, PlayListDtos>()
                    .ForMember(b => b.Id, db => db.MapFrom(a => a.Id))
          .ForMember(b => b.Name, O => O.MapFrom(a => a.Name))
          .ForMember(b => b.records, O => O.MapFrom(a => a.records))
          .ForMember(b => b.DateCreated, O => O.MapFrom(a => a.DateCreated)).ReverseMap();


            CreateMap<AddPlayListDtoss, Playlist>()
        .ForMember(dest => dest.records, opt => opt.MapFrom(src =>
        src.RecordIds.Select(id => new Record { Id = id }).ToList())).ReverseMap();

            CreateMap<UserInterestbook, UserInterestDtos>()
                    .ForMember(b => b.Id, db => db.MapFrom(a => a.Id))
    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
    .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
    .ReverseMap();

        }
    }
}
