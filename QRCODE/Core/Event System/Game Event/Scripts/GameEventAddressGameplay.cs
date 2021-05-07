using QRCode.Events;

namespace QRCode
{
    /// <summary>
    /// Register here all adress for use Game Event class.
    /// </summary>
    
    [Address(5, typeof(GameEventAddressGameplay))]
    public enum GameEventAddressGameplay : byte
    {
        Test_01,
        Test_02
    }

    [Address(1, typeof(GameEventAddressCore))]
    public enum GameEventAddressCore : byte
    {
        Test_01,
        Test_02
    }
}
