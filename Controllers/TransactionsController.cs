using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestApi.DTOs;
using TestApi.Enums;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TestContext _context;
        private readonly IMapper _mapper;

        public TransactionsController(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var transaction = await _context.Transactions.ToListAsync();
            return _mapper.Map<List<TransactionDto>>(transaction);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            var userId = _context.GetCurrentUserId();
            var accountsUser = await _context.Accounts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (!_context.Users.Any(x => x.Id == userId)) return NotFound();
            if (!_context.Categories.Any(x => x.UserId == userId && x.Id == transaction.CategoryId)) return NotFound("Не найдена категория!");
            if (!_context.Accounts.Any(x => x.Id == transaction.AccountId && x.UserId == userId)) return NotFound("Не найден счёт!");
            if (transaction.TransactionType == TransactionType.Income)
            {
                accountsUser.Balance += transaction.Amount;
            }
            if (transaction.TransactionType == TransactionType.Expense)
            {
                accountsUser.Balance -= transaction.Amount;
            }
            _context.Transactions.Add(transaction);
            _context.Accounts.Update(accountsUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            var userId = _context.GetCurrentUserId();
            var accountsUser = await _context.Accounts.FindAsync(userId);
            if (transaction.TransactionType == TransactionType.Income)
            {
                accountsUser.Balance -= transaction.Amount;
            }
            if (transaction.TransactionType == TransactionType.Expense)
            {
                accountsUser.Balance += transaction.Amount;
            }
            _context.Transactions.Remove(transaction);
            _context.Accounts.Update(accountsUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
