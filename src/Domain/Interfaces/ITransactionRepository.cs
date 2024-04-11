using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using newbuy.Domain.Models;

namespace newbuy.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> AddTransaction(Guid userId, Guid productId);
    Task<List<Transaction>> GetTransactionsByUserId(Guid userId);
    Task<List<Transaction>> GetAllTransactions();
}
