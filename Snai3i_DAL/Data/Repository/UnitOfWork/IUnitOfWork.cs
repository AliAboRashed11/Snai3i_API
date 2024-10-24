using Snai3i_DAL.Data.Repository.CardRepository;
using Snai3i_DAL.Data.Repository.SalesRepository;
using Snai3i_DAL.Data.Repository.SizesRepository;
using Snai3i_DAL.Data.Repository.ToolsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {

        SalesRepositoryy Saless { get; }

        ToolRepository Toolss { get; }
        //   ICraftRepo Craftt { get; }
        SizeRepository Sizee { get; }


        CartRebo Cards { get; }
        //This Method will Start the database Transaction
        void CreateTransaction();
        //This Method will Commit the database Transaction
        void Commit();
        //This Method will Rollback the database Transaction
        void Rollback();
        //This Method will call the SaveChanges method
        Task CompleteAsync();





      //  Task<int> CompleteAsync();


    }
}
