using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;//khai báo using CompenentModel 
using System.Threading.Tasks;

namespace StudentManagerWeb.Models
{
    // Custom Validation Attribute cho Gmail
    public class GmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true; // Required sẽ xử lý null

            string email = value.ToString();

            // Kiểm tra email có kết thúc bằng @gmail.com không (không phân biệt hoa thường)
            return email.ToLower().EndsWith("@gmail.com");
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? "Email phải sử dụng @gmail.com";
        }
    }
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ tên phải có từ 2-100 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tuổi")]
        [Range(18, 65, ErrorMessage = "Tuổi phải từ 18 đến 65")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [Gmail(ErrorMessage = "Email phải sử dụng địa chỉ Gmail (@gmail.com)")]
        [StringLength(150, ErrorMessage = "Email không được quá 150 ký tự")]
        public string Mail { get; set; }

    }
}
