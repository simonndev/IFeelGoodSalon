namespace IFeelGoodSalon.Models.Base
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
