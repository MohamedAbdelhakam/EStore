using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserServices.Cart;

[Route("api/[controller]")]
[ApiController]
[Authorize("Customer")]
public class CartController : ControllerBase
{
    private readonly ICartSercvice _cartSercvice;

    public CartController(ICartSercvice cartSercvice)
    {
        _cartSercvice = cartSercvice;
    }

    private IActionResult ValidateRequest(out int parsedCartId)
    {
        if (!ModelState.IsValid)
        {
            parsedCartId = 0;
            return BadRequest(ModelState);
        }

        var cartId = User.FindFirstValue("CartId");
        if (string.IsNullOrEmpty(cartId))
        {
            parsedCartId = 0;
            return Unauthorized("Invalid or missing cart information");
        }

        if (!int.TryParse(cartId, out parsedCartId))
            return BadRequest("Invalid cart ID format");

        return null;
    }

    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddToCart(int productId)
    {
        var validation = ValidateRequest(out int parsedCartId);
        if (validation != null)
            return validation;

        var result = await _cartSercvice.AddToCartAsync(parsedCartId, productId);
        return result.IsSucceed ? Ok(result) : BadRequest(result);
    }

    [HttpPut("Quantity")]
    public async Task<IActionResult> AddQuantityOfProductInCart(int productId, int quantity)
    {
        var validation = ValidateRequest(out int parsedCartId);
        if (validation != null)
            return validation;

        var result = await _cartSercvice.AddQuantityToProductInCartAsync(parsedCartId, productId, quantity);
        return result.IsSucceed ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("RemoveProduct")]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var validation = ValidateRequest(out int parsedCartId);
        if (validation != null)
            return validation;

        var result = await _cartSercvice.RemoveFromCartAsync(parsedCartId, productId);
        return result.IsSucceed ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("RemoveQuantityOfProduct")]
    public async Task<IActionResult> RemoveQuantityFromProduct(int productId, int quantity)
    {
        var validation = ValidateRequest(out int parsedCartId);
        if (validation != null)
            return validation;

        var result = await _cartSercvice.RemoveQuantityToProductInCartAsync(parsedCartId, productId, quantity);
        return result.IsSucceed ? Ok(result) : BadRequest(result);
    }

    [HttpGet("GetAllInCart")]
    public async Task<IActionResult> GetAllProductsInCart()
    {
        var validation = ValidateRequest(out int parsedCartId);
        if (validation != null)
            return validation;

        var result = await _cartSercvice.GetAllProductsInCartAsync(parsedCartId);
        return result.IsSucceed ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("ClearCart")]
    public async Task<IActionResult> ClearCart()
    {
        var validation = ValidateRequest(out int parsedCartId);
        if (validation != null)
            return validation;

        var result = await _cartSercvice.ClearCartAsync(parsedCartId);
        return result.IsSucceed ? Ok(result) : BadRequest(result);
    }
}