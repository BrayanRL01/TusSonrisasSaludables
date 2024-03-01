using BackEnd.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly TusSonrisasSaludablesContext _context;

        public CategoriesController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        #region GetCategories
        // GET: api/<CategoriesController>
        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<VwCategory>>> SP_GetCategoriesView()
        {
            if (_context.VwCategories == null)
            {
                return NotFound();
            }

            try
            {
                var categories = await _context.VwCategories.FromSqlRaw("EXEC SP_GetCategoriesView").ToListAsync();
                await _context.Database.CloseConnectionAsync();

                return categories;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SubCategories")]
        public async Task<ActionResult<IEnumerable<VwSubCategory>>> SP_GetSubCategoriesView()
        {
            if (_context.VwSubCategories == null)
            {
                return NotFound();
            }

            try
            {
                var subs = await _context.VwSubCategories.FromSqlRaw("EXEC SP_GetSubCategoriesView").ToListAsync();
                await _context.Database.CloseConnectionAsync();

                return subs;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CategoriesController>/5
        [HttpGet("Category/{id}")]
        public async Task<ActionResult> SP_GetCategoryView(int id)
        {
            try
            {
                var categories = await _context.VwCategories.FromSqlInterpolated($"EXEC SP_GetCategoryView {id}").ToListAsync();
                var category = categories.FirstOrDefault();
                await _context.Database.CloseConnectionAsync();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la categoría: " + ex.Message);
            }
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult> SP_GetCategory(int id)
        {
            try
            {
                var categories = await _context.Categories.FromSqlInterpolated($"EXEC SP_GetCategory {id}").ToListAsync();
                var category = categories.FirstOrDefault();
                await _context.Database.CloseConnectionAsync();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la categoría: " + ex.Message);
            }
        }

        [HttpGet("SubCategory/{id}")]
        public async Task<ActionResult> SP_GetSubCategoryView(int id)
        {
            try
            {
                var categories = await _context.VwSubCategories.FromSqlInterpolated($"EXEC SP_GetSubCategoryView {id}").ToListAsync();
                var category = categories.FirstOrDefault();
                await _context.Database.CloseConnectionAsync();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se ha encontrado la subcategoría: " + ex.Message);
            }
        }
        #endregion

        #region PostCategories
        // POST api/<CategoriesController>
        [HttpPost("Category")]
        public async Task<ActionResult<Category>> PostCategory([FromBody] CategoryModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateCategory @CategoryName";

                var param = new SqlParameter[]
                {
                    new()
                    {
                        ParameterName = "@CategoryName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryName
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Categoría creada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo agregar la categoría: " + ex.Message);
            }
        }

        [HttpPost("SubCategory")]
        public async Task<ActionResult<Category>> PostSubCategory([FromBody] CategoryModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateSubCategory @MainCategory, @CategoryName";

                var param = new SqlParameter[]
                {
                    new()
                    {
                        ParameterName = "@MainCategory",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.MainCategoryId
                    },
                    new()
                    {
                        ParameterName = "@CategoryName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryName
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Subcategoría creada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo agregar la subcategoría: " + ex.Message);
            }
        }
        #endregion

        #region PutCategories
        // PUT api/<CategoriesController>/5
        [HttpPut("Category")]
        public async Task<ActionResult<Category>> PutCategory([FromBody] CategoryModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditCategory @CategoryID, @CategoryName";

                var param = new SqlParameter[]
                {
                     new()
                    {
                        ParameterName = "@CategoryID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryId
                    },
                    new()
                    {
                        ParameterName = "@CategoryName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryName
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Categoría actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar la categoría: " + ex.Message);
            }
        }

        [HttpPut("SubCategory")]
        public async Task<ActionResult<Category>> PutSubCategory([FromBody] CategoryModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditSubCategory @CategoryID, @MainCategory, @CategoryName";

                var param = new SqlParameter[]
                {
                      new SqlParameter()
                    {
                        ParameterName = "@CategoryID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryId
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@MainCategory",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.MainCategoryId
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@CategoryName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryName
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Subcategoría actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar la subcategoría: " + ex.Message);
            }
        }
        #endregion

        #region DeleteCategories
        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteCategory {id}");
                await _context.SaveChangesAsync();
                await _context.Database.CloseConnectionAsync();

                return Ok("Categoría/Subcategoría eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar el dato: " + ex.Message);
            }
        }
        #endregion
    }
}
