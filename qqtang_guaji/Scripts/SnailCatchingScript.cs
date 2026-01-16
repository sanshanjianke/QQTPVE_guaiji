/*
 * 由SharpDevelop创建。
 * 用户： sansh
 * 日期: 2025/12/22
 * 时间: 17:25
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */

using System;
using System.Threading;

namespace Scripts
{
    public class SnailCatchingScript : BaseScript
    {
        public SnailCatchingScript(CancellationToken ct) : base(ct) { }
      
        public int MapCount;
    	
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
    
			if (_logAction != null) {
				_logAction.Invoke(formattedMessage);
			}
		}
    	
    	
        public override string Name 
        { 
            get { return "捉蜗牛刷经验刷材料挂机"; } 
        }
        
        public override string Description 
        { 
            get { return "用于 捉蜗牛 地图的挂机脚本,注意关闭道具赛。地图作者名「残冰」。p.s. 4500以下血会暴毙，没法刷材料，只能刷经验。4500以上可以刷材料。"; } 
        }
        
        public override void Run()
        {
            LogToUI("5秒后脚本开始运行...");

            Wait(5000);

            int MapCountIn = MapCount;

            InputSimulator.DirectInputSimulator simulator = InputSimulator.DirectInputSimulator.Instance;

            // input.MoveMouseToClientPositionByPartialTitle("QQTPVE", 150, 200); 示例
            // 应对120分钟后禁止开始游戏，此处自动创建房间，选择地图等
            while (true)
            {
                while (MapCountIn != 0)
                {
                    MapCountIn--;
                    RunOneRound();
                }


                IntPtr hWnd = input.GetWindowHandleByPartialTitle("QQTPVE");
                input.MinimizeWindowIfNotMinimized(hWnd);

                LogToUI("本轮地图已完成，准备创建新房间...");

                // 创建新房间，选择地图等操作

                Wait(10000);
                input.SendLeftClick(958, 571); // 离开房间
                Wait(10000);
                input.SendLeftClick(824, 567); // 创建房间
                Wait(10000);
                input.SendLeftClick(775, 140); // 点击地图
                Wait(3000);
                input.SendLeftClick(327, 310); // 选择作者
                Wait(3000);
                input.SendLeftClick(602, 387); // 选择地图
                Wait(3000);
                MapCountIn = MapCount;
            }
        }
        private int i = 1; // jushu

        

        private void RunOneRound()
        {
            LogToUI($"开始新一轮游戏,现在是第{i}局");
            i = i + 1;

            // 点击开始按钮


            /// input.ActivateWindow(input.GetWindowHandleByPartialTitle("QQTPVE")); // 让窗体最前   已弃用



            // input.MoveMouseToClientPositionByPartialTitle("QQTPVE", 919, 497); // 移至开始按钮  已弃用

            Wait(100);
            input.Press("f7");

            LogToUI("等待进入游戏...");
            Wait(3000);
            
            // 第一关移动
            input.MoveMultiple("up", 8);
            input.MoveMultiple("right", 12);
            input.MoveMultiple("up", 6);

            LogToUI("等待5秒后第二关拾取...");
            Wait(5000);
            
            input.MoveMultiple("right", 2);
            
            input.MoveMultiple("down", 8);
            input.Hold("down", 180);
            
            input.Hold("right", 160);
            input.Hold("right", 180);
            
            for(int i = 0; i < 50; i++)
            {
                input.Press("space");
                Wait(15);
            }
            
            input.MoveMultiple("up", 1);
            
            input.MoveMultiple("left", 1);
            
            Wait(5000);
            
            input.MoveMultiple("up", 1);
            
            input.MoveMultiple("right", 5);
            
            input.MoveMultiple("down", 1);
            
            input.MoveMultiple("left", 4);
            
            input.MoveMultiple("down", 1);
            
            input.MoveMultiple("right", 6);
            
            input.MoveMultiple("down", 1);
            
            input.MoveMultiple("left", 5);
            
            input.MoveMultiple("down", 1);
            
            input.MoveMultiple("right", 4);
            
            input.MoveMultiple("down", 1);
            
            input.MoveMultiple("left", 4);
            
            LogToUI("等待游戏结束...");
            Wait(7000);
        }
    }
}

