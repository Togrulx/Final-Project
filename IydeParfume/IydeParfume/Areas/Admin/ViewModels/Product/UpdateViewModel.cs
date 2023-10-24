using IydeParfume.Areas.Admin.Validations;
using System.ComponentModel.DataAnnotations;
using FileExtensionsAttribute = IydeParfume.Areas.Admin.Validations.FileExtensionsAttribute;

namespace IydeParfume.Areas.Admin.ViewModels.Product
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }


        [Required]
        public List<int> CategoryIds { get; set; }
        public List<CatagoryListItemViewModel>? Catagories { get; set; }

        [Required]
        public List<int> SeasonIds { get; set; }
        public List<SeasonListItemViewModel>? Seasons { get; set; }

        [Required]
        public List<int> UsageTimeIds { get; set; }
        public List<UsageTimeListItemViewModel>? UsageTimes { get; set; }

        [Required]
        public List<int> GroupIds { get; set; }
        public List<GroupListItemViewModel>? Groups { get; set; }

        [Required]
        public List<int> BrandIds { get; set; }
        public List<BrandListItemViewModel>? Brands { get; set; }


        [Required]
        public List<int> SizeIds { get; set; }
        public List<SizeListItemViewModel>? Sizes { get; set; }
        public IFormFile? MainImage { get; set; }

        [FileExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public List<IFormFile>? AllImages { get; set; }
        public List<Images>? ImagesUrl { get; set; }
        public List<MainImages>? MainImgUrls { get; set; }
        public List<int>? ProductImgIds { get; set; }



        public class MainImages
        {
            public MainImages(int id, string imageUrl)
            {
                Id = id;
                ImageUrl = imageUrl; 
            }

            public int Id { get; set; }
            public string ImageUrl { get; set; }
        }

        public class Images 
        {
            public Images(int id, string imageUrl)
            {
                Id = id;
                ImageUrl = imageUrl;
            }

            public int Id { get; set; }
            public string ImageUrl { get; set; }
        }

    }
}
