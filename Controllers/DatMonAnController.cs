using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuanLiQuanAn.Models;
using QuanLiQuanAn.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLiQuanAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatMonAnController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public DatMonAnController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/DatMonAn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CDatMonAn>>> GetMonAns()
        {
            var sql = "SELECT * FROM monan";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            using var reader = await cmd.ExecuteReaderAsync();

            var monAns = new List<CDatMonAn>();
            while (await reader.ReadAsync())
            {
                var ma = new CDatMonAn
                {
                    MaMA = reader.GetString(0),
                    TenMA = reader.GetString(1),
                    Gia = (double)reader.GetDecimal(2),
                    Url_anh = reader.GetString(3)
                };
                monAns.Add(ma);
            }

            return monAns;
        }

        // GET: api/DatMonAn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CDatMonAn>> GetMonAn(string id)
        {
            var sql = "SELECT * FROM monan WHERE mamonan = @mamonan";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@mamonan", id);
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var ma = new CDatMonAn
                {
                    MaMA = reader.GetString(0),
                    TenMA = reader.GetString(1),
                    Gia = (double)reader.GetDecimal(2),
                    Url_anh = reader.GetString(3)
                };
                return ma;
            }

            return NotFound();
        }

        // PUT: api/DatMonAn/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonAn(string id, CDatMonAn monAn)
        {
            if (id != monAn.MaMA)
            {
                return BadRequest();
            }

            var sql = "UPDATE monan SET tenmonan = @tenmonan, giatien = @giatien WHERE mamonan = @mamonan";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@mamonan", monAn.MaMA);
            cmd.Parameters.AddWithValue("@tenmonan", monAn.TenMA);
            cmd.Parameters.AddWithValue("@giatien", monAn.Gia);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                if (!MonAnExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DatMonAn
        [HttpPost]
        public async Task<ActionResult<CDatMonAn>> PostMonAn(CDatMonAn monAn)
        {
            var sql = "INSERT INTO monan (mamonan, tenmonan, giatien, url_anh) VALUES (@mamonan, @tenmonan, @giatien, @url_anh)";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@mamonan", monAn.MaMA);
            cmd.Parameters.AddWithValue("@tenmonan", monAn.TenMA);
            cmd.Parameters.AddWithValue("@giatien", monAn.Gia);
            cmd.Parameters.AddWithValue("@url_anh", monAn.Url_anh);

            await cmd.ExecuteNonQueryAsync();
            return CreatedAtAction("GetMonAn", new { id = monAn.MaMA }, monAn);
        }

        // DELETE: api/DatMonAn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonAn(string id)
        {
            var sql = "DELETE FROM monan WHERE mamonan = @mamonan";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@mamonan", id);

            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/DatMonAn/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<CDatMonAn>>> SearchMonAn(string criteria)
        {
            var sql = "SELECT * FROM monan WHERE mamonan LIKE @dk OR tenmonan LIKE @dk OR giatien LIKE @dk OR url_anh LIKE @dk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@dk", "%" + criteria + "%");

            using var reader = await cmd.ExecuteReaderAsync();
            var monAns = new List<CDatMonAn>();
            while (await reader.ReadAsync())
            {
                var ma = new CDatMonAn
                {
                    MaMA = reader.GetString(0),
                    TenMA = reader.GetString(1),
                    Gia = (double)reader.GetDecimal(2),
                    Url_anh = reader.GetString(3)
                };
                monAns.Add(ma);
            }

            return monAns;
        }

        private bool MonAnExists(string id)
        {
            var sql = "SELECT COUNT(1) FROM monan WHERE mamonan = @mamonan";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@mamonan", id);
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
