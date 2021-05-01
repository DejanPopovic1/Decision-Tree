using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }//Automatic property//We can also open get and set with { } and define a few steps. The statement is equivalent to get{return _name} set {_name = value};
                                         //Could also do:
        //public string Name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}


        [Display(Name = "Release Date"), DataType(DataType.Date)]

        //The DataType attributes only provide hints for the view engine to format the data (and supply attributes such as <a> for URL's and <a href="mailto:EmailAddress.com"> for email
        //The DataType attribute is used to specify a data type that is more specific than the database intrinsic type, they are not validation attributes
        //The DataType attribute can also enable the application to automatically provide type-specific features. For example, a mailto: link can be created for DataType.EmailAddress, and a date selector can be provided for DataType.Date in browsers that support HTML5.

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime ReleaseDate { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}