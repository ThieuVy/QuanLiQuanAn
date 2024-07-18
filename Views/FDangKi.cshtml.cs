using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLiQuanAn.Controllers;
using QuanLiQuanAn.Models;

public class FDangKi : PageModel
{
    private readonly TaiKhoanController _ctrlTaiKhoan;

    // Initialize Register property in the constructor
    public FDangKi(TaiKhoanController ctrlTaiKhoan)
    {
        _ctrlTaiKhoan = ctrlTaiKhoan;
        Register = new CTaiKhoan(); // Initialize Register property here
    }

    [BindProperty]
    public CTaiKhoan Register { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CTaiKhoan obj = new CTaiKhoan
        {
            TenTK = Register.TenTK,
            HoTen = Register.HoTen,
            NgaySinh = Register.NgaySinh,
            DiaChi = Register.DiaChi,
            SoDT = Register.SoDT,
            MatKhau = Register.MatKhau,
            Email = Register.Email,
            DiemTL = 0,
            HangTV = "Đồng"
        };

        bool result = _ctrlTaiKhoan.insert(obj);
        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi lưu thông tin đăng ký!");
            return Page();
        }

        TempData["Message"] = "Đăng ký thành công!";
        return RedirectToPage("/Login");
    }
}
