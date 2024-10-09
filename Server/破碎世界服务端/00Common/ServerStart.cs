namespace 墨染服务端
{
    internal class ServerStart
    {
        static void Main(string[] args)
        {
            ServerRoot.Instance.Init();
            while (true)
            {
                ServerRoot.Instance.Update();
                Thread.Sleep(20);
            }
        }
    }
}
