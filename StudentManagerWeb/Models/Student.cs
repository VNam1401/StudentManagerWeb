using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;//khai báo using CompenentModel 
using System.Threading.Tasks;

namespace StudentManagerWeb.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Chưa nhập họ tên")]
        public string FullName { get; set; }
        [Range(18, 60, ErrorMessage = "Vui lòng nhập lại tuổi")]
        public int Age { get; set; }
        [EmailAddress(ErrorMessage = "Nhập đầy đủ email")]
        public string Mail { get; set; }

    }
}
