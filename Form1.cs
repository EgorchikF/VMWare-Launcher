using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VM_Wrae_launcher
{
    public partial class LaucnherForm : Form
    {
        public LaucnherForm()
        {
            InitializeComponent();
        }

        async void LaucnherForm_Load(object sender, EventArgs e)
        {
            // Список служб для управления ими
            string[] services = { "VMAuthdService", "VMwareAutostartService", "VMnetDHCP", "VMware NAT Service", "VMUSBArbService" };

            // Перевод служб в режим "вручную"
            foreach (var service in services)
            {
                cmd($"sc config \"{service}\" start= demand");
            }
            // Запуск служб VMware
            foreach (var service in services)
            {
                cmd($"sc start \"{service}\"");
            }

            // Запуск необходимых процессов
            cmd("start \"\" \"C:\\Windows\\SysWOW64\\vmnetdhcp.exe\"");
            cmd("start \"\" \"C:\\Windows\\SysWOW64\\vmnat.exe\"");
            cmd("start \"\" \"%PROGRAMFILES(X86)%\\VMware\\VMware Workstation\\vmware.exe\"");

            // Ожидание закрытия VMware Workstation
            while (Process.GetProcessesByName("vmware").Length > 0)
            {
                await Task.Delay(2000); // Проверка каждые 2 секунды
            }

            // Завершение всех процессов VMware
            cmd("taskkill /f /im vmware-autostart.exe & " + "taskkill /f /im vmnetdhcp.exe & " +"taskkill /f /im vmware-usbarbitrator64.exe & " 
                + "taskkill /f /im vmware-authd.exe & " +"taskkill /f /im vmnat.exe");

            // Остановка служб
            foreach (var service in services)
            {
                cmd($"sc stop \"{service}\"");
            }

            // Перевод служб в режим "отключено"
            foreach (var service in services)
            {
                cmd($"sc config \"{service}\" start= disabled");
            }

            // Завершение текущего процесса (лаунчера)
            Application.Exit();
        }

        void cmd(string line)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {line}",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true, // Скрыть окно
                UseShellExecute = false // Не использовать оболочку
            };

            using (Process process = Process.Start(processInfo))
            {
                process.WaitForExit();  // Ожидание завершения команды
            }
        }
    }
}