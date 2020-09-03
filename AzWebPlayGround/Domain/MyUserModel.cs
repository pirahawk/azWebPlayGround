using System.ComponentModel.DataAnnotations;

namespace AzWebPlayGround.Domain
{
    public class MyUserModel
    {
        [Required]
        public string UserName { get; set; }
    }
}