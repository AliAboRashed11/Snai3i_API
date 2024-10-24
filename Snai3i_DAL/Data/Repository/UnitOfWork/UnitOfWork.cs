using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Snai3i_DAL.Data.Context;
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
    public class UnitOfWork:IUnitOfWork
    {


        
        private readonly SnaiiDBContext _context = null;
        private IDbContextTransaction? _objTran = null;

        public ToolRepository Toolss { get; private set; }
        public SizeRepository Sizee { get; private set; }
        public SalesRepositoryy Saless { get; private set; }

        //public ICraftRepo Craftt { get; private set; }

        public UnitOfWork(SnaiiDBContext context)
        {
            _context = context;
            Saless = new SalesRepositoryy(_context);
            Toolss = new ToolRepository(_context);
            Sizee = new SizeRepository(_context);
            Cards = new CartRebo(_context);

            //Craftt = new CraftRepo(_context);
        }
        public CartRebo Cards { get; private set; }

        //public ICraftRepo Craftt { get; private set; }

    
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


        public async Task CompleteAsync()
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
