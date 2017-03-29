using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SunAndLuna.Models
{
    public class Diary
    {
        [Key]
        public int DiaryID { get; set; }

        [Required(ErrorMessage = "Diary Title is required")]
        [Display(Name = "Diary Title")]
        public string DiaryTitle { get; set; }

        [Required(ErrorMessage = "Diary Content is required")]
        [Display(Name = "Diary Content")]
        public string DiaryContent { get; set; }

        [Required(ErrorMessage = "Release Date is required")]
        [Display(Name = "Release Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; }

        public int UserID { get; set; }
        public bool IsDelete { get; set; }
    }

    public class DiaryDBContext : DbContext
    {
        public DbSet<Diary> DiaryNotes { get; set; }
    }
}