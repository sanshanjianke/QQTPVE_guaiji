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
    public class LittleFruitVillage : BaseScript
    {
        public LittleFruitVillage(CancellationToken ct) : base(ct)
        {
        }

        private Action<string> _logAction;
        public void SetLogAction(Action<string> logAction)
        {
            _logAction = logAction;
        }



        public int WaitTime;


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



        public override string Name
        {
            get { return "小果村刷材料"; }
        }

        public override string Description
        {
            get { return "用于 小果村 地图的挂机脚本。地图作者「天璇」。注意关闭道具赛，注意输入法切换到英文模式。ps:需要6500血，因为要放9个泡。6500以下请刷抓蜗牛。注意，有时候会因为电脑卡造成判定不准。"; }
        }

        public override void Run()
        {
<<<<<<< HEAD
            while (true)
=======
            while(true)
>>>>>>> a3d88f470c8af5dbbfc534216c9c91f05fb60e63
            {
                RunOneRound();
            }
        }

        private int i = 1; // 局数

        private void RunOneRound()
        {

            LogToUI($"开始新一轮游戏，现在是第{i}局");
            i = i + 1;
            // 点击开始按钮
            // input.ActivateWindow(input.GetWindowHandleByPartialTitle("QQTPVE")); // 让窗体最前 已弃用

            // Wait(100);

            // input.MoveMouseToClientPositionByPartialTitle("QQTPVE", 919, 497); // 移至开始按钮 已弃用

            Wait(100);

            // input.ClickMouse(3, 50);

            input.Press("f7");

            LogToUI("等待" + (WaitTime / 1000.0).ToString("F2") + "秒后进入游戏...");

<<<<<<< HEAD
            Wait(WaitTime);



            input.MoveMultiple("down", 12);
            input.MoveMultiple("right", 20);

            input.MoveMultiple("down", 2);
            input.MoveMultiple("up", 1);
            Wait(2500); // 等怪来了，就打
            input.Press("z");

            string[] dirs = { "left", "right" };
<<<<<<< HEAD
=======

            for (int row = 0; row < 6; row++)
            {
                for (int k = 0; k < 2; k++)
                {
                    input.Press("space");
                    for (int i = 0; i < 7; i++)
                    {
                        input.MoveMultiple(dirs[row % 2], 1);
                        input.Press("space");
                    }
                    if (k == 1) // 躲避
                    {
                        if (row % 2 == 1) // -> 卡进两个房子中
                        {
                            input.MoveMultiple("down", row);
                            input.MoveMultiple("right", 2);
                            input.MoveMultiple("down", 2);
                            Wait(1000); //等泡泡爆炸
                            input.MoveMultiple("up", 1);
                            input.MoveMultiple("left", 2);
                            input.MoveMultiple("up", row + 1);
                        }
                        else // <- 卡进两朵花中
                        {
                            input.MoveMultiple("left", 2);
                            input.MoveMultiple("down", row + 1);
                            Wait(1500); //等泡泡爆炸
                            input.MoveMultiple("up", row + 2);
                            input.MoveMultiple("right", 2);
                        }
                    }
                    else
                    {
                        input.MoveMultiple(dirs[row % 2], 1);
                        input.MoveMultiple("down", 1);
                        Wait(1500); //等泡泡爆炸
                        input.MoveMultiple("up", 1);
                    }
                }
            }
            
            
            input.MoveMultiple("left",15);
            input.MoveMultiple("up",2);
            input.MoveMultiple("right",15);
            input.MoveMultiple("left",17);
            
            input.MoveMultiple("up",1);    // 定位到墙角
            input.MoveMultiple("left",3);
            input.MoveMultiple("up",1);
            input.MoveMultiple("left",3);
            input.MoveMultiple("up",1);
            input.MoveMultiple("left",3);
            
            
            input.MoveMultiple("up",15);
            input.Press("space");
            input.MoveMultiple("down",1);
            input.Press("space");            
            input.MoveMultiple("down",1);
            input.Press("space");
            input.MoveMultiple("down",1);
            input.Press("space");            
            input.MoveMultiple("down",1);
            input.Press("space");
            input.MoveMultiple("down",1);
            input.Press("space");            
            input.MoveMultiple("down",1);
            input.Press("space");            
            input.MoveMultiple("down",1);
            input.Press("space");            
            input.MoveMultiple("down",1);
            input.Press("space"); 
>>>>>>> a3d88f470c8af5dbbfc534216c9c91f05fb60e63

            for (int row = 0; row < 6; row++)
            {
                for (int k = 0; k < 2; k++)
                {
                    if (k == 1)
                    {
                        input.MoveMultiple("up", 1);
                    }
                    input.Press("space");
                    for (int i = 0; i < 7; i++)
                    {
                        input.MoveMultiple(dirs[row % 2], 1);
                        input.Press("space");
                    }
                    if (k == 1) // 躲避
                    {
                        if (row % 2 == 1) // -> 卡进两个房子中
                        {
                            input.MoveMultiple("down", row);
                            input.MoveMultiple("right", 2);
                            input.MoveMultiple("down", 2);
                            Wait(1000); //等泡泡爆炸
                            input.MoveMultiple("up", 1);
                            input.MoveMultiple("left", 2);
                            input.MoveMultiple("up", row + 1);
                        }
                        else // <- 卡进两朵花中
                        {
                            input.MoveMultiple("left", 2);
                            input.MoveMultiple("down", row + 1);
                            Wait(1500); //等泡泡爆炸
                            input.MoveMultiple("up", row + 2);
                            input.MoveMultiple("right", 2);
                        }

                    }
                    else
                    {
                        input.MoveMultiple(dirs[row % 2], 1);
                        input.MoveMultiple("down", 1);
                        Wait(1500); //等泡泡爆炸
                    }
                }
            }


            input.MoveMultiple("left", 15);
            input.MoveMultiple("up", 2);
            input.MoveMultiple("right", 15);
            input.MoveMultiple("left", 17);

            input.MoveMultiple("up", 1);    // 定位到墙角
            input.MoveMultiple("left", 3);
            input.MoveMultiple("up", 1);
            input.MoveMultiple("left", 3);
            input.MoveMultiple("up", 1);
            input.MoveMultiple("left", 3);


            input.MoveMultiple("up", 15);
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
            input.MoveMultiple("down", 1);
            input.Press("space");
            input.MoveMultiple("down", 1);
            input.Press("space");

            input.MoveMultiple("down", 1);
            input.MoveMultiple("right", 1);

            Wait(1500);

            input.MoveMultiple("left", 1);
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
            input.MoveMultiple("down", 1);
            input.Press("space");

            input.MoveMultiple("right", 1);
            input.MoveMultiple("up", 1);
            input.MoveMultiple("right", 3);
            Wait(1500);
            input.MoveMultiple("left", 5);
            input.MoveMultiple("up", 18);
            input.MoveMultiple("right", 1);
            input.MoveMultiple("down", 18);
            input.MoveMultiple("right", 1);
            input.MoveMultiple("up", 18);

            Wait(1500);
            input.MoveMultiple("down", 1);
            input.Press("space");
            Wait(600);
            input.Press("space");
            Wait(5000);
            input.Press("space");
            Wait(600);
            input.Press("space");
            Wait(5000);
            input.Press("space");
            Wait(600);
            input.Press("space");
            Wait(20000);

        }

    }
}

