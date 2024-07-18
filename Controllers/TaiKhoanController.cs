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
    public class TaiKhoanController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public TaiKhoanController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/TaiKhoan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CTaiKhoan>>> GetTaiKhoans()
        {
            var sql = "SELECT * FROM taikhoan";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            using var reader = await cmd.ExecuteReaderAsync();

            var taiKhoans = new List<CTaiKhoan>();
            while (await reader.ReadAsync())
            {
                var tk = new CTaiKhoan
                {
                    TenTK = reader.GetString(0),
                    HoTen = reader.GetString(1),
                    NgaySinh = reader.GetDateTime(2),
                    DiaChi = reader.GetString(3),
                    SoDT = reader.GetString(4),
                    Email = reader.GetString(5),
                    MatKhau = reader.GetString(6),
                    DiemTL = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
                    HangTV = reader.GetString(8)
                };
                taiKhoans.Add(tk);
            }

            return taiKhoans;
        }

        // GET: api/TaiKhoan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTaiKhoan>> GetTaiKhoan(string id)
        {
            var sql = "SELECT * FROM taikhoan WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", id);
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var tk = new CTaiKhoan
                {
                    TenTK = reader.GetString(0),
                    HoTen = reader.GetString(1),
                    NgaySinh = reader.GetDateTime(2),
                    DiaChi = reader.GetString(3),
                    SoDT = reader.GetString(4),
                    Email = reader.GetString(5),
                    MatKhau = reader.GetString(6),
                    DiemTL = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
                    HangTV = reader.GetString(8)
                };
                return tk;
            }

            return NotFound();
        }

        // PUT: api/TaiKhoan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiKhoan(string id, CTaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TenTK)
            {
                return BadRequest();
            }

            var sql = "UPDATE taikhoan SET hoten = @hoten, ngaysinh = @ngaysinh, diachi = @diachi, sdt = @sdt, " +
                      "email = @email, matkhau = @matkhau, dtl = @dtl, hangtv = @hangtv WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", taiKhoan.TenTK);
            cmd.Parameters.AddWithValue("@hoten", taiKhoan.HoTen);
            cmd.Parameters.AddWithValue("@ngaysinh", taiKhoan.NgaySinh);
            cmd.Parameters.AddWithValue("@diachi", taiKhoan.DiaChi);
            cmd.Parameters.AddWithValue("@sdt", taiKhoan.SoDT);
            cmd.Parameters.AddWithValue("@email", taiKhoan.Email);
            cmd.Parameters.AddWithValue("@matkhau", taiKhoan.MatKhau);
            cmd.Parameters.AddWithValue("@dtl", taiKhoan.DiemTL);
            cmd.Parameters.AddWithValue("@hangtv", taiKhoan.HangTV);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                if (!TaiKhoanExists(id))
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

        // POST: api/TaiKhoan
        [HttpPost]
        public async Task<ActionResult<CTaiKhoan>> PostTaiKhoan(CTaiKhoan taiKhoan)
        {
            var sql = "INSERT INTO taikhoan (tentk, hoten, ngaysinh, diachi, sdt, email, matkhau, dtl, hangtv) VALUES " +
                      "(@tentk, @hoten, @ngaysinh, @diachi, @sdt, @email, @matkhau, @dtl, @hangtv)";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", taiKhoan.TenTK);
            cmd.Parameters.AddWithValue("@hoten", taiKhoan.HoTen);
            cmd.Parameters.AddWithValue("@ngaysinh", taiKhoan.NgaySinh);
            cmd.Parameters.AddWithValue("@diachi", taiKhoan.DiaChi);
            cmd.Parameters.AddWithValue("@sdt", taiKhoan.SoDT);
            cmd.Parameters.AddWithValue("@email", taiKhoan.Email);
            cmd.Parameters.AddWithValue("@matkhau", taiKhoan.MatKhau);
            cmd.Parameters.AddWithValue("@dtl", taiKhoan.DiemTL);
            cmd.Parameters.AddWithValue("@hangtv", taiKhoan.HangTV);

            await cmd.ExecuteNonQueryAsync();
            return CreatedAtAction("GetTaiKhoan", new { id = taiKhoan.TenTK }, taiKhoan);
        }

        // DELETE: api/TaiKhoan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoan(string id)
        {
            var sql = "DELETE FROM taikhoan WHERE tentk = @tentk";
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

        // GET: api/TaiKhoan/Login
        [HttpGet("Login")]
        public async Task<ActionResult<CTaiKhoan>?> GetTaiKhoanByLogin(string tentk, string matkhau)
        {
            var sql = "SELECT * FROM taikhoan WHERE tentk = @tentk AND matkhau = @matkhau";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", tentk);
            cmd.Parameters.AddWithValue("@matkhau", matkhau);
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var tk = new CTaiKhoan
                {
                    TenTK = reader.GetString(0),
                    HoTen = reader.GetString(1),
                    NgaySinh = reader.GetDateTime(2),
                    DiaChi = reader.GetString(3),
                    SoDT = reader.GetString(4),
                    Email = reader.GetString(5),
                    MatKhau = reader.GetString(6),
                    DiemTL = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
                    HangTV = reader.GetString(8)
                };
                return tk;
            }

            return null;
        }

        // PUT: api/TaiKhoan/UpdatePassword
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(string tentk, string mkMoi)
        {
            var sql = "UPDATE taikhoan SET matkhau = @mkMoi WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@mkMoi", mkMoi);
            cmd.Parameters.AddWithValue("@tentk", tentk);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException)
            {
                if (!TaiKhoanExists(tentk))
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

        private bool TaiKhoanExists(string id)
        {
            var sql = "SELECT COUNT(1) FROM taikhoan WHERE tentk = @tentk";
            using var cnn = _databaseHelper.GetConnection();
            using var cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@tentk", id);

            return (int)cmd.ExecuteScalar() > 0;
        }

        internal bool insert(CTaiKhoan obj)
        {
            throw new NotImplementedException();
        }
    }
}
