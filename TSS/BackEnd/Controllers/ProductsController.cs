using BackEnd.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ProductsController : ControllerBase
    {

        private readonly TusSonrisasSaludablesContext _context;

        public ProductsController(TusSonrisasSaludablesContext context)
        {
            _context = context;
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<VwProduct>>> SP_GetProductsView()
        {
            if (_context.VwProducts == null)
            {
                return NotFound("No se encontraron productos.");
            }

            try
            {
                var products = await _context.VwProducts.FromSqlRaw("EXEC SP_GetProductsView").ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se encontraron productos: " + ex.Message);
            }

        }

        // GET api/<ProductssController>/5
        [HttpGet("Product/{id}")]
        public async Task<ActionResult> SP_GetProductView(int id)
        {
            if (_context.VwProducts == null)
            {
                return NotFound();
            }

            try
            {
                var products = await _context.VwProducts.FromSqlInterpolated($"EXEC SP_GetProductView {id}").ToListAsync();
                var product = products.FirstOrDefault();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult> SP_GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            try
            {
                var products = await _context.Products.FromSqlInterpolated($"EXEC SP_GetProduct {id}").ToListAsync();
                var product = products.FirstOrDefault();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("Product")]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductModel entity)
        {
            try
            {
                string Query = "EXEC SP_CreateProduct @BrandID, @CategoryID, @ProductName, " +
                    "@Description, @Price, @Stock, @Image";

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@BrandID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BrandId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@CategoryID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@ProductName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProductName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Description",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProductDescription
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Price",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UnitPrice
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Stock",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Stock
                    },
                        new SqlParameter()
                    {
                        ParameterName = "@Image",
                        SqlDbType = System.Data.SqlDbType.Image,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProductImage
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok($"Producto {entity.ProductName} creado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo crear el producto: " + ex.Message);
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_DeleteProduct {id}");
                await _context.SaveChangesAsync();
                return Ok("Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo eliminar el producto: " + ex.Message);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("Product")]
        public async Task<ActionResult<Product>> PutProduct([FromBody] ProductModel entity)
        {
            try
            {
                string Query = "EXEC SP_EditProduct @ProductID ,@BrandID, " +
                    "@CategoryID, @ProductName, @Description, @Price, @Stock, @Image";

                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@ProductID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProductId
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@BrandID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.BrandId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@CategoryID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.CategoryId
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@ProductName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProductName
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Description",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProductDescription
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Price",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.UnitPrice
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Stock",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.Stock
                    },
                     new SqlParameter()
                    {
                        ParameterName = "@Image",
                        SqlDbType = System.Data.SqlDbType.Image,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = entity.ProductImage
                    }
                };

                await _context.Database.ExecuteSqlRawAsync(Query, param);
                await _context.SaveChangesAsync();

                return Ok($"Producto {entity.ProductName} actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo editar el producto: " + ex.Message);
            }
        }

    }
}
