namespace ScanToCloud.WinUI
{
    public class Common
    {
        public enum ImageSizeMode
        {
            Fit,
            Stretch
        }

        public enum StorageType
        {
            Local,
            Dropbox,
            OneDrive,
            Box
        }

        public class DropNet
        {
            public const string API_KEY = "adgs0gzqv0sfkuv";
            public const string API_SECRET = "8qu2mrof5205puu";
        }
    }
}
