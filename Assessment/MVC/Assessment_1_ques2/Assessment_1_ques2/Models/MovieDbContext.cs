using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using System.Web;

namespace Assessment_1_ques2.Models
{
    public class MovieDbContext: DbContext
    {
        
        public DbSet<Movie> Movies { get; set; }
    }

}