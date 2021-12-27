using System.ComponentModel.DataAnnotations;

namespace JobWebApi.AppModels.Models
{
    public class CvUpload 
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
        [Key]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
