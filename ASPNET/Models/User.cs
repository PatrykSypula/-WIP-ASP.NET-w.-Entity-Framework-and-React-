using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_EF.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar")]
        public string Login { get; set; }
        [Column(TypeName = "varchar")]
        public string Password { get; set; }
        [Column(TypeName = "varchar")]
        public string Email { get; set; }
        [Column(TypeName = "int")]
        public int IsActive { get; set; }

        public List<Dictionaries> Dictionaries { get; set; } = new List<Dictionaries>();
        public List<SessionStatistics> SessionStatistics { get; set; } = new List<SessionStatistics>();
        public List<SubscribedDictionary> SubscribedDictionaries { get; set; } = new List<SubscribedDictionary>();
    }
}
