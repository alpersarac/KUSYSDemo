using KUSYSDemo.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ILogger<object> _logger;
        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration, ILogger<object> logger)
        {
            _db = db;
            _configuration = configuration;
            _logger = logger;
            Student = new StudentRepository(db, configuration, logger);
            Course = new CourseRepository(db, configuration, logger);
        }
        public IStudentRepository Student { get; private set; }
        public ICourseRepository Course { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
