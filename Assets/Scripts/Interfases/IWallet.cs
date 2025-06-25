public interface IWallet
{
    int Gold { get; }
    void AddGold(int value);
    bool SpendGold(int value);
    bool HasEnoughGold(int value);
    void SetGold(int value);
}
