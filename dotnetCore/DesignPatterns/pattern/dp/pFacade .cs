namespace pattern.dp {

        //外观模式
        public class FacadePattern {

            public static void main (string[] args) {
                {
                    //实例化电器类
                    SubSystemA_Light light = new SubSystemA_Light ();
                    SubSystemB_Television television = new SubSystemB_Television ();
                    SubSystemC_Aircondition aircondition = new SubSystemC_Aircondition ();

                    //传参
                    Facade facade = new Facade (light, television, aircondition);

                    //客户端直接与外观对象进行交互
                    facade.on();
                    System.Console.WriteLine("可以看电视了");
                    facade.off();
                    System.Console.WriteLine("可以睡觉了");
                }
            }

            public class Facade {

                SubSystemA_Light light;
                SubSystemB_Television television;
                SubSystemC_Aircondition aircondition;

                //传参
                public Facade (SubSystemA_Light light, SubSystemB_Television television, SubSystemC_Aircondition aircondition) {
                    this.light = light;
                    this.television = television;
                    this.aircondition = aircondition;

                }
                //起床后一键开电器
                public void on() {
                    System.Console.WriteLine("起床了");
                    light.on();
                    television.on();
                    aircondition.on();

                }
                public void off() {
                    //睡觉时一键关电器
                    System.Console.WriteLine("睡觉了");
                    light.off();
                    television.off();
                    aircondition.off();
                }
            }

            //灯类
            public class SubSystemA_Light {
                public void on () {
                    System.Console.WriteLine("打开了灯....");
                }

                public void off () {
                    System.Console.WriteLine("关闭了灯....");
                }
            }

            //电视类
            public class SubSystemB_Television {
                public void on () {
                    System.Console.WriteLine("打开了电视....");
                }

                public void off () {
                    System.Console.WriteLine("关闭了电视....");
                }
            }

            //空调类
            public class SubSystemC_Aircondition {
                public void on () {
                    System.Console.WriteLine("打开了电视....");
                }

                public void off () {
                    System.Console.WriteLine("关闭了电视....");
                }
            }
        }
}



/**

    外观模式与适配器模式不同的是：适配器模式是将一个对象包装起来以改变其接口，而外观是将一群对象 ”包装“起来以简化其接口。它们的意图是不一样的，适配器是将接口转换为不同接口，而外观模式是提供一个统一的接口来简化接口。



 */