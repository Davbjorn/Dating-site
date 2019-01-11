using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Testgrej.Models
{
    public class ProfileViewModel
    {        
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Description")]
        public string AboutMe { get; set; }

        public string Post { get; set; }
    }
}