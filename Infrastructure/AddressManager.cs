using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure;

public class AddressManager(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        try
        {
            _context.Addresses.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return false;
    }

    public async Task<AddressEntity> GetAddressAsync(string UserId)
    {
        try
        {
            var addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == UserId);
            return addressEntity!;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        try
        {
            var existing = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return false;
    }
}
