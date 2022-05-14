using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Driver: UserBase
    {
        public string ImageCv { get; set; }

        public int Accidents { get; set; } = 0;

        public int DeservedClients { get; set; } = 0;

        [Range(0, 10)]
        public float Rating { get; set; } = 0.0f;

        public bool IsAvailable { get; set; }
        public string CurrentLat { get; set; }
        public string CurrentLong { get; set; }
        public int? CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }
    }
}
