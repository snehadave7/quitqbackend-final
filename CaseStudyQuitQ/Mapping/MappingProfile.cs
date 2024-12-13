using CaseStudyQuitQ.Models;
using AutoMapper;

namespace CaseStudyQuitQ.Mapping {
    public class MappingProfile:Profile { // Profile: An AutoMapper class that holds configuration for object mappings. 
        public MappingProfile() {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTOForUser>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<DeliveryAddress, DeliveryAddressDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
            
        }
    }
}
