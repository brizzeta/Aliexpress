using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KlikavaDbContext _context;
        private bool disposed = false;

        public UnitOfWork(KlikavaDbContext context)
        {
            _context = context;
        }

        private IGenericRepository<User> _users;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Category> _categories;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<OrderItem> _orderItems;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<Review> _reviews;
        private IGenericRepository<Chat> _chats;
        private IGenericRepository<Messages> _messages;
        private IGenericRepository<Notifications> _notifications;

        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);
        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);
        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);
        public IGenericRepository<OrderItem> OrderItems => _orderItems ??= new GenericRepository<OrderItem>(_context);
        public IGenericRepository<Payment> Payments => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<Review> Reviews => _reviews ??= new GenericRepository<Review>(_context);
        public IGenericRepository<Chat> Chats => _chats ??= new GenericRepository<Chat>(_context);
        public IGenericRepository<Messages> Messages => _messages ??= new GenericRepository<Messages>(_context);
        public IGenericRepository<Notifications> Notifications => _notifications ??= new GenericRepository<Notifications>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
