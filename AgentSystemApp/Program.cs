using System;
using System.Windows.Forms;

using Clifton.AgentSystem.Agents;
using Clifton.AgentSystem.Lib;
using Clifton.AgentSystem.Processing;

namespace AgentSystemApp
{
    static class Program
    {
        static MainForm mainForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();

            Processor processor = InitializeAgentSystem();
            RegisterAgents(processor);
            StartAgentSystem(processor);
            SayHello(processor);

            Application.Run(mainForm);
        }

        static Processor InitializeAgentSystem()
        {
            Processor processor = new Processor();

            return processor;
        }

        static void RegisterAgents(Processor processor)
        {
            processor.RegisterAgent(new OutputAgent(Clifton.AgentSystem.Lib.Constants.AnyContext, "LogMessage", null, data => Log(data.Message)));
            processor.RegisterAgent(new AppConfigAgent("AppConfig", "Key", "AppConfigValue"));
        }

        static void StartAgentSystem(Processor processor)
        {
            processor.StartAsynchrononousProcessing();
        }

        static void SayHello(Processor processor)
        {
            var hello = AgentType.New("App", "LogMessage").KeyValue("Message", "Agent System Running").Get();
            processor.QueueData(hello);
        }

        static void Log(string msg)
        {
            mainForm.Log(msg);
        }
    }
}
