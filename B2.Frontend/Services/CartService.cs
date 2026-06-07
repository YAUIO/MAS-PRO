using System.Text.Json;
using B2.App.Dtos;
using B2.Frontend.Models;
using Microsoft.JSInterop;

namespace B2.Frontend.Services;

public class CartService(IJSRuntime js) : ICartService
{
    private const string StorageKey = "Cart";

    public async Task AddToCartAsync(ListingDto dto, Guid sellerId)
    {
        var cart = await GetCartAsync();
        
        var item = cart.FirstOrDefault(p => p.Product.ProductId == dto.ProductId && sellerId == p.SellerId);
        
        if (item == null)
        {
            item = new()
            {
                SellerId = sellerId,
                Product = dto,
                Amount = 1
            };
        }
        else
        {
            cart.Remove(item);
            item.Amount++;
        }
        
        cart.Add(item);

        await WriteToStorage(cart);
    }
    
    public async Task DeleteFromCart(ListingDto dto, Guid sellerId)
    {
        var cart = await GetCartAsync();
        
        var item = cart.FirstOrDefault(p => p.Product.ProductId == dto.ProductId && sellerId == p.SellerId);
        
        if (item == null)
        {
            return;
        }
        
        cart.Remove(item);
        item.Amount--;
        
        if (item.Amount > 0)
            cart.Add(item);

        await WriteToStorage(cart);
    }

    public async Task ClearCart()
    {
        await js.InvokeVoidAsync("localStorage.removeItem", StorageKey);
    }

    public async Task<List<CartItem>> GetCartAsync()
    {
        var json = await js.InvokeAsync<string>("localStorage.getItem", StorageKey);

        if (string.IsNullOrEmpty(json))
            return [];

        return JsonSerializer.Deserialize<List<CartItem>>(json)!;
    }

    private async Task WriteToStorage(List<CartItem> cart)
    {
        await js.InvokeVoidAsync("localStorage.setItem", StorageKey, JsonSerializer.Serialize(cart));
    }
}