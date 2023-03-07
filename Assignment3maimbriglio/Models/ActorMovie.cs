﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3maimbriglio.Models
{
    public class ActorMovie
    {
        public int Id { get; set; }
        [ForeignKey("Actor")]
        public int? ActorID { get; set; }
        public Actor? Actor { get; set; }
        [ForeignKey("Movie")]
        public int? MovieID { get; set; }
        public Movie? Movie { get; set; }
    }
}
