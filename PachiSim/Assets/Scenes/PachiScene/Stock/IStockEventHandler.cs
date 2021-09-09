namespace PachiSim.Scenes.PachiScene
{
    public interface IStockHolderReseiver
    {
        void OnBeginDigest();
        void OnEndDigest();
        void OnUpdateStock( Stock digestStock, Stock[] reserveStocks );
    }
}
