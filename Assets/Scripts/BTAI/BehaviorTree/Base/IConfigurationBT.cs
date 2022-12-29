namespace UnchordMetroidvania
{
    public interface IConfigurationBT
    {
        long curFps { get; set; }

        void ClearFps();
        void AddFps();
        void SetFps(long fps);
    }
}