using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuanLiQuanAn.Models;
using QuanLiQuanAn.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLiQuanAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatBanController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public DatBanController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/DatBan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CDatBan>>> GetDatBans()
        {
            var sql = "SELECT * FROM datban";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            using var reader = await cmd.ExecuteReaderAsync();

            var datBans = new List<CDatBan>();
            while (await reader.ReadAsync())
            {
                var db = new CDatBan
                {
                    TaiKhoan = new CTaiKhoan { TenTK = reader.GetString(0) },
                    ViTriBan = reader.GetInt32(1),
                    Ngay = reader.GetDateTime(2),
                    Gio = reader.GetTimeSpan(3),
                    SoLuong = reader.GetInt32(4),
                    GhiChu = reader.GetString(5)
                };
                datBans.Add(db);
            }

            return datBans;
        }

        // GET: api/DatBan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CDatBan>> GetDatBan(string id)
        {
            var sql = "SELECT * FROM datban WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", id);
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var db = new CDatBan
                {
                    TaiKhoan = new CTaiKhoan { TenTK = reader.GetString(0) },
                    ViTriBan = reader.GetInt32(1),
                    Ngay = reader.GetDateTime(2),
                    Gio = reader.GetTimeSpan(3),
                    SoLuong = reader.GetInt32(4),
                    GhiChu = reader.GetString(5)
                };
                return db;
            }

            return NotFound();
        }

        // PUT: api/DatBan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatBan(string id, CDatBan datBan)
        {
            if (id != datBan.TaiKhoan.TenTK)
            {
                return BadRequest();
            }

            var sql = "UPDATE datban SET vitriban = @vitriban, ngay = @ngay, gio = @gio, songuoi = @songuoi, ghichu = @ghichu WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@vitriban", datBan.ViTriBan);
            cmd.Parameters.AddWithValue("@ngay", datBan.Ngay);
            cmd.Parameters.AddWithValue("@gio", datBan.Gio);
            cmd.Parameters.AddWithValue("@songuoi", datBan.SoLuong);
            cmd.Parameters.AddWithValue("@ghichu", datBan.GhiChu);
            cmd.Parameters.AddWithValue("@tentk", datBan.TaiKhoan.TenTK);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                if (!DatBanExists(id))
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

        // POST: api/DatBan
        [HttpPost]
        public async Task<ActionResult<CDatBan>> PostDatBan(CDatBan datBan)
        {
            var sql = "INSERT INTO datban (tentk, vitriban, ngay, gio, songuoi, ghichu) VALUES (@tentk, @vitriban, @ngay, @gio, @songuoi, @ghichu)";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", datBan.TaiKhoan.TenTK);
            cmd.Parameters.AddWithValue("@vitriban", datBan.ViTriBan);
            cmd.Parameters.AddWithValue("@ngay", datBan.Ngay);
            cmd.Parameters.AddWithValue("@gio", datBan.Gio);
            cmd.Parameters.AddWithValue("@songuoi", datBan.SoLuong);
            cmd.Parameters.AddWithValue("@ghichu", datBan.GhiChu);

            await cmd.ExecuteNonQueryAsync();
            return CreatedAtAction("GetDatBan", new { id = datBan.TaiKhoan.TenTK }, datBan);
        }

        // DELETE: api/DatBan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatBan(string id)
        {
            var sql = "DELETE FROM datban WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", id);

            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool DatBanExists(string id)
        {
            var sql = "SELECT COUNT(1) FROM datban WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", id);
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
