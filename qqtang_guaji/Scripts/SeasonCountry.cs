using System;
using System.Threading;


namespace Scripts
{
    public class SeasonCountry : BaseScript
    {
        public SeasonCountry(CancellationToken ct) : base(ct)
        {
        }
        public override string Name
        {
            get { return "SeasonCountry"; }
        }

        public override string Description
        {
            get { return "赛季国家脚本，自动执行一轮完整流程。"; }
        }

        public int WaitTime;

        private Action<string> _logAction;
        public void SetLogAction(Action<string> logAction)
        {
            _logAction = logAction;
        }
        // 然后修改Log方法：
        private void LogToUI(string message)
        {
            string formattedMessage = string.Format("[{0:HH:mm:ss}] {1}", DateTime.Now, message);
            Console.WriteLine(formattedMessage);

            if (_logAction != null)
            {
                _logAction.Invoke(formattedMessage);
            }
        }


        public override void Run()
        {
            

            LogToUI("开始游戏");


            while (true)
            {
                
                RunOneRound();
            }
        }

        private int i=1; // jushu

        private void RunOneRound()
        {
            LogToUI($"开始新一轮游戏,现在是第{i}局");
            i = i + 1;

            Wait(100);

            input.Press("f7");

            LogToUI("等待" + WaitTime / 1000 + "秒后进入游戏...");

            Wait(WaitTime);


            input.MoveMultiple("right", 8);
            input.MoveMultiple("down", 3);
            input.Press("7");

            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 3);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 8);
            Wait(4000);

            input.MoveMultiple("up", 20);
            input.MoveMultiple("down", 21);
            input.MoveMultiple("left",5);

            input.MoveMultiple("up", 3);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.MoveMultiple("up", 1);
            Wait(2000);

            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.Press("space");
            input.MoveMultiple("right", 1);
            input.MoveMultiple("up", 4);
            Wait(2000);

            input.MoveMultiple("left", 18);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("right", 18);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("left", 18);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("right", 18);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("left", 18);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("right", 18);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("left", 18);

            input.MoveMultiple("right", 1);
            input.Press("space");
            Wait(100);
            input.Press("space");
            Wait(100);
            input.Press("space");
            input.Press("6");

            Wait(15000);
            /*
            input.MoveMultiple("up", 7);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.MoveMultiple("down", 1);
            input.Press("6");

            input.MoveMultiple("up", 3);
            input.MoveMultiple("right", 3);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("left", 2);
            input.MoveMultiple("down", 1);
            input.Press("6");

            input.MoveMultiple("up", 3);
            input.MoveMultiple("right", 11);

            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 5);
            Wait(2000);

            input.MoveMultiple("right", 12);
            input.MoveMultiple("left", 1);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("left", 7);
            input.MoveMultiple("down", 1);
            input.MoveMultiple("right", 8);
            input.MoveMultiple("up", 4);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("left", 1);
            input.Press("space");
            input.MoveMultiple("down",1);
            input.MoveMultiple("left", 1);
            Wait(2000);
            */



            LogToUI("等待游戏结束...");
            Wait(7000);
        }


    }
}

