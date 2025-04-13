namespace HRLeaveManagement.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
