namespace Back.NET.Data
{
    public interface IUpdatePerson
    {
        Task<Models.Person?> Execute(Models.Person person);
    }

    public class UpdatePerson : IUpdatePerson
    {

        private readonly PersonContext _personContext;

        public UpdatePerson(PersonContext personContext)
        {
            _personContext = personContext;
        }


        public async Task<Models.Person?> Execute(Models.Person person)
        {
            var entity = await _personContext.Get(person.Id);

            if (entity == null)
                return null;

            entity.FirstName = person.FirstName;
            entity.LastName = person.LastName;
            entity.Age = person.Age;
            entity.Email = person.Email;
            entity.Country = person.Country;
            entity.Idiom = person.Idiom;

            await _personContext.Actualizar(entity);
            return entity.ToDto();

        }

    }
}