
namespace ProgressAPI.Models
{
    public class ProgressUpdateRequest
    {
        public string UserId { get; set; }

        public string MediaId { get; set; }

        public long ProgressSeconds { get; set; }
    }
}
