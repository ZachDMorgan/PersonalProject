namespace Domain.Entities
{

    public class Profession
    {

        #region Properties

        public Guid ID { get; set; }

        public string Name { get; set; }

        public HashSet<Practitioner> Practitioners { get; set; } = new HashSet<Practitioner>();

        #endregion

    }

}
