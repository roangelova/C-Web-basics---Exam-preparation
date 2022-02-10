using System.ComponentModel.DataAnnotations.Schema;

namespace SharedTrip.Data.Models
{
    public class UserTrip
    {
        public string UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User{ get; set; }


        public string TripId { get; set; }

        [ForeignKey(nameof(TripId))]
        public Trip Trip { get; set; }
    }
}