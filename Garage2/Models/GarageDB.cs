using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Garage2.Models
{
    public class GarageDB : DbContext
    {
        public GarageDB () : base("name=Garageconnection")
        {

        }

        public DbSet<Garage> Vehicles { get; set; }
    }
}