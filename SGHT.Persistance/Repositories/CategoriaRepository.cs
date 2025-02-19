using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Interfaces.Configuration;

namespace SGHT.Persistance.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(SGHTContext context) : base(context)
        { 
        private readonly SGHTContext _context;
        private readonly ILogger<CategoriaRepository> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaRepository(SGHTContext context,
                                ILogger<CategoriaRepository> logger,
                                IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetHabitacionByCategory(int categoryId)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await (from categoria in _context.Catetgorias
                                    join hbtcn in _context.Habitaciones on categoria.HabitacionId equals hbtcn.IdHabitacion
                                    where categoria.HabitacionId == IdHabitacion
                                    select new HabitacionMdel()
                                    {
                                        CourseId = course.Id,
                                        CreationDate = course.CreationDate,
                                        Credits = course.Credits,
                                        DepartmentId = depto.Id,
                                        DepartmentName = depto.Name,
                                        Title = course.Title
                                    }).ToListAsync();

                result.Data = querys;

            }
            catch (Exception ex)
            {
                result.Message = this._configuration["ErrorCourseRepository:GetCourseByDepartment"];
                result.Success = false;
                this._logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public override Task<OperationResult> SaveEntityAsync(Categoria entity)
        {
            return base.SaveEntityAsync(entity);
        }
        public override Task<OperationResult> UpdateEntityAsync(Categoria entity)
        {
            return base.UpdateEntityAsync(entity);
        }
    }
}
    

