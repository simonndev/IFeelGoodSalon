namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
