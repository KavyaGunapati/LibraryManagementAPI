
using AutoMapper;
using LibraryManagementAPI.DataAccess.Entities;
using LibraryManagementAPI.Models.DTOs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BookCreate, Book>();
        CreateMap<BookUpdate, Book>()
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(dest => dest.CategoryId, opt => opt.Condition(src => src.CategoryId.HasValue));

        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<UserRegister, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

        CreateMap<BorrowRecord, BorrowReturn>()
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryCreate, Category>();
        CreateMap<CategoryUpdate, Category>().ForMember(dest=>dest.Name,opt=>opt.Condition(src=>src.Name != null));
    }
}
