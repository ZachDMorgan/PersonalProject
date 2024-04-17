namespace Domain.Entities
{

    public class Availability
    {

        #region Properties

        public Guid ID { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeOnly EndTime { get; set; }

        public TimeOnly StartTime { get; set; }

        #endregion

    }

}
