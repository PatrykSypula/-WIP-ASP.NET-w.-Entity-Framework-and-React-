using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_EF.Models
{
    public class Dictionaries
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar")]
        public string DictionaryName { get; set; }
        [Column(TypeName = "varchar")]
        public string DictionaryLevel { get; set; }
        [Column(TypeName = "int")]
        public int IsDefaultDictionary { get; set; }
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public User User { get; set; }
        public List<SubscribedDictionary> SubscribedDictionaries { get; set; } = new List<SubscribedDictionary>();
        public List<Words> Words { get; set; } = new List<Words>();
        public List<SessionStatistics> SessionStatistics { get; set; } = new List<SessionStatistics>();
    }
}
