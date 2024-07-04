using ASPNET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET.Models
{
    public class Dictionaries
    {
        [Key]
		public int Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string DictionaryName { get; set; }
        [Column(TypeName = "int")]
        public int DictionaryLevelId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string DictionaryDescription { get; set; }
        [Column(TypeName = "int")]
        public int IsDefaultDictionary { get; set; }
        [Column(TypeName = "int")]
        public int IsPublic {  get; set; }
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public User User { get; set; }
        public DictionaryLevelValues DictionaryLevelValues { get; set; }
        public List<SubscribedDictionary> SubscribedDictionaries { get; set; } = new List<SubscribedDictionary>();
        public List<Words> Words { get; set; } = new List<Words>();
        public List<Statistics> Statistics { get; set; } = new List<Statistics>();
    }
}
