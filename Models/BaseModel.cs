using System.ComponentModel.DataAnnotations.Schema;

namespace TodoServices.Models
{
    public class BaseModel : IBaseModel
    {
        /// <summary>
        ///     Entity Framework Identification
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "bigint(20)")]
        public long ID { get; set; }

        /// <summary>
        ///     Created data entry Date
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "datetime(6)")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     Last Updated data entry Date
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "datetime(6)")]
        public DateTime UpdateDate { get; set; }
    }
}
