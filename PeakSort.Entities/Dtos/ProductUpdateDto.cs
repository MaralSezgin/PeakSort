using PeakSort.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Entities.Dtos
{
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır ")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır ")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MinLength(20, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır ")]
        public string Description { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır ")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır ")]
        public string Thumbnail { get; set; }


        [DisplayName("Seo içerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(500, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır ")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır ")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır ")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır ")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir")]

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public bool  IsActive { get; set; }
        public bool Deleted { get; set; }
    }
}
