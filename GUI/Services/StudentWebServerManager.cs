using System;

namespace Student_Management_System_CSharp_SGU2025.GUI.Services
{
    /// <summary>
    /// Manager để quản lý singleton instance của StudentWebServer
    /// </summary>
    public class StudentWebServerManager
    {
        private static StudentWebServerManager _instance;
        private static readonly object _lock = new object();
        private StudentWebServer webServer;

        public static StudentWebServerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new StudentWebServerManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private StudentWebServerManager()
        {
            webServer = new StudentWebServer();
        }

        /// <summary>
        /// Khởi động web server
        /// </summary>
        public void StartServer()
        {
            if (!webServer.IsRunning)
            {
                webServer.Start();
            }
        }

        /// <summary>
        /// Dừng web server
        /// </summary>
        public void StopServer()
        {
            if (webServer.IsRunning)
            {
                webServer.Stop();
            }
        }

        /// <summary>
        /// Lấy base URL của web server từ UrlResolver
        /// </summary>
        public string GetBaseUrl()
        {
            if (!webServer.IsRunning)
            {
                StartServer();
            }
            return webServer.GetBaseUrl();
        }

        /// <summary>
        /// Kiểm tra server có đang chạy không
        /// </summary>
        public bool IsServerRunning()
        {
            return webServer.IsRunning;
        }
    }
}

