namespace Domain.Entities
{

    public class Person
    {

        #region Properties

        public Guid ID { get; set; }

        public ContactDetails ContactDetails { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        #endregion

    }

}
