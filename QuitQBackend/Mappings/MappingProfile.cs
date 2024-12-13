using AutoMapper;
using QuitQBackend.Models;

namespace QuitQBackend.Mappings {
    public class MappingProfile:Profile {

        public MappingProfile() {
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<SubCategory,SubCategoryDTO>().ReverseMap();
            CreateMap<Review,ReviewDTO>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTOForUser>().ReverseMap();
            CreateMap<Payment,PaymentDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<DeliveryAddress, DeliveryAddressDTO>().ReverseMap();
            CreateMap<Cart,CartDTO>().ReverseMap();
            CreateMap<CartItem,CartItemDTO>().ReverseMap();
        }
    }
}
