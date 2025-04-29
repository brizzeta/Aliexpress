using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Application.DTOs.User;
using Application.DTOs.Product;
using Application.DTOs.Category;
using Application.DTOs.Order;
using Application.DTOs.Review;
using Application.DTOs.Chat;
using Application.DTOs.Notification;
using System.Linq;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Password_hash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(_ => 0.0));
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Product mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.Name));

            CreateMap<Product, ProductDetailDto>()
                .IncludeBase<Product, ProductDto>()
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.SellerEmail, opt => opt.MapFrom(src => src.Seller.Email))
                .ForMember(dest => dest.SellerPhone, opt => opt.MapFrom(src => src.Seller.Phone))
                .ForMember(dest => dest.SellerRating, opt => opt.MapFrom(src => src.Seller.Rating));

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Review, ProductReviewDto>()
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.Name));

            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null));

            CreateMap<Category, CategoryListItemDto>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null))
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count));

            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Order mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Order, OrderDetailDto>()
                .IncludeBase<Order, OrderDto>()
                .ForMember(dest => dest.BuyerEmail, opt => opt.MapFrom(src => src.Buyer.Email))
                .ForMember(dest => dest.BuyerPhone, opt => opt.MapFrom(src => src.Buyer.Phone))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Product, ProductSimpleDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrls.FirstOrDefault()))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.Name));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => OrderStatus.Pending));

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // OrderItem mappings
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<OrderItemCreateDto, OrderItem>();

            // Payment mappings
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentCreateDto, Payment>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => PaymentStatus.Pending));

            // Review mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.Name));

            CreateMap<ReviewCreateDto, Review>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<ReviewUpdateDto, Review>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Chat and Messages mappings
            CreateMap<Chat, ChatDto>()
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.Name))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.Name))
                .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src =>
                    src.Messages.OrderByDescending(m => m.CreatedDate).FirstOrDefault()));

            CreateMap<Chat, ChatListItemDto>()
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.Name))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.Name))
                .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src =>
                    src.Messages.OrderByDescending(m => m.CreatedDate).FirstOrDefault()))
                .ForMember(dest => dest.UnreadMessagesCount, opt => opt.Ignore()); 

            CreateMap<ChatCreateDto, Chat>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Messages, opt => opt.Ignore());

            CreateMap<Messages, MessageDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name));

            CreateMap<MessageCreateDto, Messages>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId));


            // Notification mappings
            CreateMap<Notifications, NotificationDto>()
                .ForMember(dest => dest.RecepientName, opt => opt.MapFrom(src => src.Recepient.Name))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<NotificationCreateDto, Notifications>();

            CreateMap<NotificationUpdateDto, Notifications>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
