using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class Amenity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;

        public List<Apartment> Apartments { get; private set; } = [];

        public Amenity(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception ("Name cannot be null when creating new amenity");

            Id = new Guid();
            Name = name;
        }
    }
}
