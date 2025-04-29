using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Product;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork uof;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork uof, IMapper mapper)
        {
            this.uof = uof;
            this._mapper = mapper;
        }

        public async Task<ApiResponseDto<ProductDto>> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productCreateDto);
                product.CreatedDate = DateTime.UtcNow;

                await uof.Products.AddAsync(product);
                await uof.CompleteAsync();

                var productDto = _mapper.Map<ProductDto>(product);
                return ApiResponseDto<ProductDto>.SuccessResult(productDto, "Product created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<ProductDto>.FailureResult("Error creating product", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteProductAsync(int id)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(id);
                if (product == null)
                    return ApiResponseDto<bool>.FailureResult($"Product with ID {id} not found");

                product.IsActive = false;
                product.UpdatedDate = DateTime.UtcNow;
                uof.Products.Update(product);
                await uof.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "Product deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error deleting product", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            try
            {
                var products = await uof.Products.GetAllAsync();
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult("Error retrieving products", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ProductDto>>> GetDiscountedProductsAsync()
        {
            try
            {
                var products = await uof.Products.FindAsync(i => i.Discount > 0 && i.IsActive);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult("Error retrieving discounted products", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<ProductDetailDto>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(id);
                if (product == null)
                    return ApiResponseDto<ProductDetailDto>.FailureResult($"Product with ID {id} not found");

                var productDetailDto = _mapper.Map<ProductDetailDto>(product);
                return ApiResponseDto<ProductDetailDto>.SuccessResult(productDetailDto);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<ProductDetailDto>.FailureResult("Error retrieving product", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ProductDto>>> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                var category = await uof.Categories.GetByIdAsync(categoryId);
                if (category == null)
                    return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult($"Category with ID {categoryId} not found");

                var products = await uof.Products.FindAsync(i => i.CategoryId == categoryId && i.IsActive);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult("Error retrieving products by category", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ProductDto>>> GetProductsBySellerAsync(int sellerId)
        {
            try
            {
                var seller = await uof.Users.GetByIdAsync(sellerId);
                if (seller == null)
                    return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult($"Seller with ID {sellerId} not found");

                var products = await uof.Products.FindAsync(i => i.SellerId == sellerId && i.IsActive);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult("Error retrieving products by seller", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ProductDto>>> GetRelatedProductsAsync(int productId, int count = 5)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(productId);
                if (product == null)
                    return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult($"Product with ID {productId} not found");

                var relatedProducts = await uof.Products.FindAsync(i => i.CategoryId == product.CategoryId && i.Id != productId && i.IsActive);
                var relatedProductDtos = _mapper.Map<IEnumerable<ProductDto>>(relatedProducts.Take(count));
                return ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(relatedProductDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult("Error retrieving related products", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ProductDto>>> GetTopRatedProductsAsync(int count = 10)
        {
            try
            {
                var products = await uof.Products.FindAsync(i => i.IsActive && i.Rating != null);
                var topRatedProducts = products.OrderByDescending(i => i.Rating).Take(count);
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(topRatedProducts);
                return ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult("Error retrieving top rated products", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> HardDeleteProductAsync(int id)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(id);
                if (product == null)
                    return ApiResponseDto<bool>.FailureResult($"Product with ID {id} not found");

                uof.Products.Delete(product);
                await uof.CompleteAsync();
                return ApiResponseDto<bool>.SuccessResult(true, "Product permanently deleted");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error hard deleting product", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ProductDto>>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    var allProducts = await GetAllProductsAsync();
                    return allProducts;
                }

                searchTerm = searchTerm.ToLower();
                var products = await uof.Products.FindAsync(i =>
                    (i.Name.ToLower().Contains(searchTerm) ||
                    (i.Description != null && i.Description.ToLower().Contains(searchTerm))) &&
                    i.IsActive);

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return ApiResponseDto<IEnumerable<ProductDto>>.SuccessResult(productDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<ProductDto>>.FailureResult("Error searching products", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<ProductDto>> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
        {
            try
            {
                var existingProduct = await uof.Products.GetByIdAsync(id);
                if (existingProduct == null)
                    return ApiResponseDto<ProductDto>.FailureResult($"Product with ID {id} not found");

                if (productUpdateDto.Name != null)
                    existingProduct.Name = productUpdateDto.Name;

                if (productUpdateDto.Description != null)
                    existingProduct.Description = productUpdateDto.Description;

                if (productUpdateDto.Price.HasValue)
                    existingProduct.Price = productUpdateDto.Price.Value;

                if (productUpdateDto.Discount.HasValue)
                    existingProduct.Discount = productUpdateDto.Discount.Value;

                if (productUpdateDto.StockQuantity.HasValue)
                    existingProduct.StockQuantity = productUpdateDto.StockQuantity.Value;

                if (productUpdateDto.CategoryId.HasValue)
                    existingProduct.CategoryId = productUpdateDto.CategoryId.Value;

                if (productUpdateDto.ImageUrls != null)
                    existingProduct.ImageUrls = productUpdateDto.ImageUrls;

                if (productUpdateDto.IsActive.HasValue)
                    existingProduct.IsActive = productUpdateDto.IsActive.Value;

                existingProduct.UpdatedDate = DateTime.UtcNow;

                uof.Products.Update(existingProduct);
                await uof.CompleteAsync();

                var productDto = _mapper.Map<ProductDto>(existingProduct);
                return ApiResponseDto<ProductDto>.SuccessResult(productDto, "Product updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<ProductDto>.FailureResult("Error updating product", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> UpdateProductImagesAsync(int productId, List<string> imageUrls)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(productId);
                if (product == null)
                    return ApiResponseDto<bool>.FailureResult($"Product with ID {productId} not found");

                product.ImageUrls = imageUrls;
                product.UpdatedDate = DateTime.UtcNow;
                uof.Products.Update(product);
                await uof.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "Product images updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error updating product images", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> UpdateProductStockAsync(int productId, int newStock)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(productId);
                if (product == null)
                    return ApiResponseDto<bool>.FailureResult($"Product with ID {productId} not found");

                product.StockQuantity = newStock;
                product.UpdatedDate = DateTime.UtcNow;
                uof.Products.Update(product);
                await uof.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "Product stock updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error updating product stock", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> UpdateProductRatingAsync(int productId)
        {
            try
            {
                var product = await uof.Products.GetByIdAsync(productId);
                if (product == null)
                    return ApiResponseDto<bool>.FailureResult($"Product with ID {productId} not found");

                var reviews = await uof.Reviews.FindAsync(r => r.ProductID == productId);
                if (reviews.Any())
                {
                    product.Rating = reviews.Average(r => r.Rating);
                }
                else
                {
                    product.Rating = 0;
                }

                product.UpdatedDate = DateTime.UtcNow;
                uof.Products.Update(product);
                await uof.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "Product rating updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error updating product rating", new List<string> { ex.Message });
            }
        }
    }

}
