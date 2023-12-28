using System.ComponentModel.DataAnnotations;

namespace UserTrain.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        public string Email { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required(ErrorMessage ="لطفا پسورد را وارد کنید")]
        public string Password { get; set; }=string.Empty;
    }
}
