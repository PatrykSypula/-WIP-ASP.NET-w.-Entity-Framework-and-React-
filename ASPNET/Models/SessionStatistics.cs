using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_EF.Models
{
    public class SessionStatistics
    {
        [Key]
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        [Column(TypeName = "varchar")]
        public int GoodAnswers { get; set; }
        public int AllAnswers { get; set; }
        [Column(TypeName = "varchar")]
        public string Percentage { get; set; }
        [Column(TypeName = "int")]
        public int DictionaryId { get; set; }
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        public User User { get; set; }
        public Dictionaries Dictionary { get; set; }
    }
}
