namespace Ordering.Domain.Common
{
    public interface ITrackable
    {
        string CreatedBy { get; set; }
        long CreatedAt { get; set; }
        string UpdatedBy { get; set; }
        long? UpdatedAt { get; set; }
    }
}
