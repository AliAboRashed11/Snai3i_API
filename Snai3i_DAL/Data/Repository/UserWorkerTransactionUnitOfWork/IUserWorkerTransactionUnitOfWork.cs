using Snai3i_DAL.Data.Repository.OrdersRepository;
using Snai3i_DAL.Data.Repository.ReviewsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.UserWorkerTransactionUnitOfWork
{
    public interface IUserWorkerTransactionUnitOfWork : IDisposable
    {
        OrdersRepositoryy Orderss { get; set; }

        ReviewsRepositoryy Reviewss { get; set; }

        //This Method will Start the database Transaction
        void CreateTransaction();
        //This Method will Commit the database Transaction
        void Commit();
        //This Method will Rollback the database Transaction
        void Rollback();
        //This Method will call the SaveChanges method
        public Task Save();
    }
}
