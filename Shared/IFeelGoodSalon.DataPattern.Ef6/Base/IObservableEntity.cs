namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public enum EntityModelState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }

    public interface IObservableEntity
    {
        EntityModelState ModelState { get; set; }
    }
}
