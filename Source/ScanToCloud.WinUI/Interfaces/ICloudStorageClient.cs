namespace ScanToCloud.WinUI.Interfaces
{
    interface ICloudStorageClient
    {
        bool IsAuthenticated();
        bool Authenticate();
        bool Upload(string fileName);
    }
}
