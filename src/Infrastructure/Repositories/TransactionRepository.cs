using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newbuy.Domain.Interfaces;
using newbuy.Domain.Models;
using newbuy.Infrastructure.Data;

namespace newbuy.Infrastructure.Repositories;
public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Transaction> AddTransaction(Guid userId, Guid productId)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ProductId = productId,
            PurchaseDate = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task<List<Transaction>> GetTransactionsByUserId(Guid userId)
    {
        return await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        return await _context.Transactions.ToListAsync();
    }
}
;