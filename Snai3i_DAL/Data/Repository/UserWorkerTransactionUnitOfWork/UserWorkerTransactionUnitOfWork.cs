using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Repository.OrdersRepository;
using Snai3i_DAL.Data.Repository.ReviewsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.UserWorkerTransactionUnitOfWork
{
    public class UserWorkerTransactionUnitOfWork : IUserWorkerTransactionUnitOfWork
    {
        private readonly SnaiiDBContext _context = null ;
        private IDbContextTransaction? _objTran = null;

        // the error is here we should have used this 
        //private readonly IOrdersRepository _ordersRepository;
        //private readonly IReviewsRepository _reviewsRepository;

        public OrdersRepositoryy Orderss { get;  set; }
        public ReviewsRepositoryy Reviewss { get;  set; }



        public UserWorkerTransactionUnitOfWork(SnaiiDBContext context )
        {
            _context = context;

            Reviewss = new  ReviewsRepositoryy(_context);
            Orderss = new OrdersRepositoryy(_context);

        }





        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _objTran?.Commit();
        }

        public void Rollback()
        {
            _objTran?.Rollback();

            _objTran?.Dispose();
        }

        public async Task Save()
        {
            try
            {
                //Calling DbContext Class SaveChanges method 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception, possibly logging the details
                // The InnerException often contains more specific details
                throw new Exception(ex.Message, ex);
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
