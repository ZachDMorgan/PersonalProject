namespace Domain.Entities
{

    public class Service
    {

        #region Properties

        public Guid ID { get; set; }

        public double Cost { get; set; }

        public int Duration { get; set; }

        public string Name { get; set; }

        public HashSet<PractitionerService> Practitioners { get; set; }

        #endregion

    }

}
