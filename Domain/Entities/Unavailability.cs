namespace Domain.Entities
{

    public class Unavailability
    {

        #region Properties

        public Guid ID { get; set; }

        public DateTime EndOn { get; set; }

        public DateTime StartOn { get; set; }

        #endregion

    }

}
