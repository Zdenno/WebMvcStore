using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("Kategorija")]
    public class Kategorija
    {
        public Kategorija()
        {
            Proizvods = new HashSet<Proizvod>();
        }

        public int KategorijaId { get; set; }

        [Required(ErrorMessage = "Morate uneti naziv instrumenta")]
        [Display(Name = "Naziv modela")]
        [StringLength(40, ErrorMessage = "Instrument ima najvise 40 karaktera")]
        public string NazivKategorije { get; set; }

        [Required(ErrorMessage = "Morate uneti opis instrumenta")]
        [Display(Name = "Opis modela")]
        [StringLength(100, ErrorMessage = "Opis ima najvise 100 karaktera")]
        public string OpisKategorije { get; set; }

        [Required(ErrorMessage = "Morate uneti datum")]
        [Display(Name = "Datum")]
        [DisplayFormat(DataFormatString = "{0:/MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime DatumKreiranja { get; set; }

        public virtual ICollection<Proizvod> Proizvods { get; set; }
    }
}