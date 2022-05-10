using Back.NET.Dtos;
using Back.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Back.NET.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }

        public async Task<PersonEntity?> Get(int id)
        {
            return await Persons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            PersonEntity entity = await Get(id);
            Persons.Remove(entity);
            SaveChanges();
            return true;
        }

        public async Task<PersonEntity> Add(CreatePerson person)
        {
            PersonEntity entity = new PersonEntity()
            {
                Id = null,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Email = person.Email,
                Country = person.Country,
                Idiom = person.Idiom,
            };

            EntityEntry<PersonEntity> response = await Persons.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar"));

        }

        
        public async Task<bool> Actualizar(PersonEntity personEntity)
        {
            Persons.Update(personEntity);
            await SaveChangesAsync();

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        }



    }

    public class PersonEntity
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Idiom { get; set; }


        public Person ToDto()
        {
            return new Person()
            {
                Country = Country,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Idiom = Idiom,
                Age = Age,
                Id = Id ?? throw new Exception("El id no puede ser NULL")
            };
        }

    }
}
