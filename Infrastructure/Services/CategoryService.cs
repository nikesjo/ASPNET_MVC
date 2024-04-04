using Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class CategoryService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
    {
        return null!;
    }
}
