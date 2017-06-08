using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Garage
    {
        
        public int Id { get; set; }
        [Required]
        [MinLength(6)][MaxLength(6)]
        public string RegNr { get; set; }
        [Required]
        [Display(Name ="Färg")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Märke")]
        public string Brand { get; set; }
        [Display(Name = "Antal hjul")]        
        public int NumberOfWheels { get; set; }
        [Required]
        [Display(Name = "Typ av fordon")]
        public Type Type { get; set; }
        public DateTime Parkerad { get; set; }
        [Display(Name = "Total tid parkerad")]
        public int TotalParkedTime { get; set; }
        [Display(Name = "Pris")]
        public float Price { get; set; }
    }

    public enum Type
    {
        Bil,
        Motorcykel,
        Flygplan,
        Buss
    }

    public enum Search
    {        
        FordonsTyp,
        RegNr,
        Färg,
        Märke,
        Antalhjul
    }

    public class Receipt
    {
        public int Id { get; set; }
        public string RegNr { get; set; }
        [Display(Name = "Färg")]
        public string Color { get; set; }
        [Display(Name = "Märke")]
        public string Brand { get; set; }
        [Display(Name = "Antal hjul")]
        public int NumberOfWheels { get; set; }
        [Display(Name = "Typ av fordon")]
        public Type Type { get; set; }
        public DateTime Parkerad { get; set; }
        [Display(Name = "Total tid parkerad")]
        public int TotalParkedTime { get; set; }
        [Display(Name = "Pris")]
        public float Price { get; set; }
    }
}