namespace Back.NET.Data
{
    public interface IUpdateCandidato
    {
        Task<Models.Candidato?> Execute(Models.Candidato candidato);
    }

    public class UpdateCandidato : IUpdateCandidato
    {

        private readonly CandidatoContext _candidatoContext;

        public UpdateCandidato(CandidatoContext candidatoContext)
        {
            _candidatoContext = candidatoContext;
        }


        public async Task<Models.Candidato?> Execute(Models.Candidato candidato)
        {
            var entity = await _candidatoContext.Get(candidato.Id);

            if (entity == null)
                return null;

            entity.FirstName = candidato.FirstName;
            entity.LastName = candidato.LastName;
            entity.City = candidato.City;
            entity.Country = candidato.Country;
            entity.Phone = candidato.Phone;
            entity.Email = candidato.Email;
            entity.Technologies = candidato.Technologies;

            await _candidatoContext.Actualizar(entity);
            return entity.ToDto();

        }

    }
}
