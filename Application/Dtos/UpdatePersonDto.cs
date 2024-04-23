using Domain.Entities;

namespace Application.Dtos
{

    public class UpdatePersonDto
    {

        #region Properties

        public UpdateContactDetailsDto ContactDetails { get; set; }

        public ChangeTracker<string> FirstName { get; set; }

        public ChangeTracker<string> Surname { get; set; }

        public ChangeTracker<string> Title { get; set; }

        #endregion

        #region Methods

        public Person ApplyChanges(Person person)
        {
            if (this.ContactDetails != null)
                this.ContactDetails.ApplyChanges(person.ContactDetails);
            if (this.FirstName.HasBeenSet)
                person.FirstName = this.FirstName.Value;
            if (this.Surname.HasBeenSet)
                person.Surname = this.Surname.Value;
            if (this.Title.HasBeenSet)
                person.Title = this.Title.Value;

            return person;
        }

        #endregion

    }

}
