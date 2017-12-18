//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CVGS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int gameID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [Required]
        [Display(Name = "Develpoer")]
        public string develpoer { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public string publisher { get; set; }
        [Required]
        [Display(Name = "Released Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime releasedDate { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(0, 999.99)]
        public Nullable<decimal> price { get; set; }
        
        public Nullable<int> categoryID { get; set; }
        
        public Nullable<int> subCategoryID { get; set; }
        [Display(Name = "Created At")]
        public Nullable<System.DateTime> createdDate { get; set; }
        [Display(Name = "Updated At")]
        public Nullable<System.DateTime> updatedDate { get; set; }
        public virtual Category Category { get; set; }
        public virtual Category Category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
