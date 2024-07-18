using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuanLiQuanAn.Models;
using QuanLiQuanAn.Utils;
using System.Threading.Tasks;

namespace QuanLiQuanAn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CTHDController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public CTHDController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // POST: api/CTHD
        [HttpPost]
        public async Task<IActionResult> PostCTHD([FromBody] CCTHD cthd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    string sql = "INSERT INTO CTHD (SoHD, MaMA, SoLuong) VALUES (@sohd, @mamonan, @sluong)";
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@sohd", cthd.SoHD);
                        cmd.Parameters.AddWithValue("@mamonan", cthd.MaMA);
                        cmd.Parameters.AddWithValue("@sluong", cthd.SoLuong);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        if (rows > 0)
                        {
                            return CreatedAtAction(nameof(GetCTHD), new { id = cthd.SoHD }, cthd);
                        }
                        else
                        {
                            return BadRequest(new { message = "Insert failed" });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/CTHD/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCTHD(int id)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    string sql = "SELECT * FROM CTHD WHERE SoHD = @id";
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var cthd = new CCTHD
                                {
                                    SoHD = reader.GetInt32(0),
                                    MaMA = reader.GetString(1),
                                    SoLuong = reader.GetInt32(2)
                                };
                                return Ok(cthd);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT: api/CTHD/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTHD(int id, [FromBody] CCTHD cthd)
        {
            if (id != cthd.SoHD)
            {
                return BadRequest();
            }

            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    string sql = "UPDATE CTHD SET MaMA = @mamonan, SoLuong = @sluong WHERE SoHD = @sohd";
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@sohd", cthd.SoHD);
                        cmd.Parameters.AddWithValue("@mamonan", cthd.MaMA);
                        cmd.Parameters.AddWithValue("@sluong", cthd.SoLuong);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        if (rows > 0)
                        {
                            return NoContent();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE: api/CTHD/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCTHD(int id)
        {
            try
            {
                using (var connection = _databaseHelper.GetConnection())
                {
                    string sql = "DELETE FROM CTHD WHERE SoHD = @id";
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        if (rows > 0)
                        {
                            return NoContent();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
