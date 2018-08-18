using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebApplication2.Models
{
    [Table("Proizvod")]
    public class Proizvod
    {
        public int ProizvodId { get; set; }
        [Required(ErrorMessage = "Morate uneti id instrumenta")]

        public int KategorijaId { get; set; }

        [Required(ErrorMessage = "Morate uneti naziv proizvoda")]
        [Display(Name = "Naziv proizvoda")]
        [StringLength(40, ErrorMessage = "Proizvod ima najvise 40 karaktera")]
        public string NazivProizvodjaca { get; set; }
        [Required(ErrorMessage = "Morate uneti cenu")]
        [Display(Name = "Cena")]

        public decimal Cena { get; set; }
        [Required(ErrorMessage = "Morate uneti kolicinu")]
        [Display(Name = "Kolicina")]
        public int KolicinaNaLageru { get; set; }




        [MaxLength]
        [Column(TypeName = "image")]
        public byte[] Slika { get; set; }
        [Required]
        public string OglasPostavio { get; set; }
        public virtual Kategorija Kategorije { get; set; }
    }
}