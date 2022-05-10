
using Back.NET.Dtos;
using Back.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Back.NET.Data
{
    public class CandidatoContext : DbContext
    {
        public CandidatoContext(DbContextOptions<CandidatoContext> options) : base(options)
        {
        }

        public DbSet<CandidatoEntity> Candidatos { get; set; }


        public async Task<CandidatoEntity?> Get(int id)
        {
            return await Candidatos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            CandidatoEntity entity = await Get(id);
            Candidatos.Remove(entity);
            SaveChanges();
            return true;
        }

        public async Task<CandidatoEntity> Add(CreateCandidato candidato)
        {
            CandidatoEntity entity = new CandidatoEntity()
            {
                Id = null,
                FirstName = candidato.FirstName,
                LastName = candidato.LastName,
                City = candidato.City,
                Country = candidato.Country,
                Phone = candidato.Phone,
                Email = candidato.Email,
                Technologies = candidato.Technologies,
            };

            EntityEntry<CandidatoEntity> response = await Candidatos.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar"));

        }


        public async Task<bool> Actualizar(CandidatoEntity candidatoEntity)
        {
            Candidatos.Update(candidatoEntity);
            await SaveChangesAsync();

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidato>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        }









    }








    public class CandidatoEntity
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Technologies { get; set; }


        public Candidato ToDto()
        {
            return new Candidato()
            {
                Country = Country,
                City = City,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Technologies = Technologies,
                Phone = Phone,
                Id = Id ?? throw new Exception("El id no puede ser NULL")
            };
        }

    }



}
