using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<Chat> Chats { get; }
        IGenericRepository<Messages> Messages { get; }
        IGenericRepository<Notifications> Notifications { get; }

        Task<int> CompleteAsync();
    }
}
