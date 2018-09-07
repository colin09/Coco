namespace pattern.dp {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BuilderPattern {

        public static void main (string[] args) {

            //逛了很久终于发现一家合适的电脑店
            //找到该店的老板和装机人员
            Director director = new Director ();
            Builder builder = new ConcreteBuilder ();

            //沟通需求后，老板叫装机人员去装电脑
            director.Construct (builder);

            //装完后，组装人员搬来组装好的电脑
            Computer computer = builder.GetComputer ();
            //组装人员展示电脑给小成看
            computer.Show ();
        }

    }

    public abstract class Builder {

        //第一步：装CPU
        //声明为抽象方法，具体由子类实现 
        public abstract void BuildCPU ();

        //第二步：装主板
        //声明为抽象方法，具体由子类实现 
        public abstract void BuildMainboard ();

        //第三步：装硬盘
        //声明为抽象方法，具体由子类实现 
        public abstract void BuildHD ();

        //返回产品的方法：获得组装好的电脑
        public abstract Computer GetComputer ();
    }

    public class Director {
        //指挥装机人员组装电脑
        public void Construct (Builder builder) {

            builder.BuildCPU ();
            builder.BuildMainboard ();
            builder.BuildHD ();
        }
    }

    //装机人员1
    public class ConcreteBuilder : Builder {
        //创建产品实例
        Computer computer = new Computer ();

        //组装产品

        public override void BuildCPU () {
            computer.Add ("组装CPU ");
        }

        public override void BuildMainboard () {
            computer.Add ("组装主板 ");
        }

        public override void BuildHD () {
            computer.Add ("组装主板 ");
        }

        //返回组装成功的电脑
        public override Computer GetComputer () {
            return computer;
        }
    }

    public class Computer {

        //电脑组件的集合
        private List<string> parts = new List<string> ();

        //用于将组件组装到电脑里
        public void Add (string part) {
            parts.Add (part);
        }

        public void Show () {
            for (int i = 0; i < parts.Count; i++) {
                System.Console.WriteLine("组件" + parts[i] + "装好了");
            }
            System.Console.WriteLine("电脑组装完成， 请验收");

        }

    }





}


/*
    模式讲解：

        指挥者（Director）直接和客户（Client）进行需求沟通；
        沟通后指挥者将客户创建产品的需求划分为各个部件的建造请求（Builder）；
        将各个部件的建造请求委派到具体的建造者（ConcreteBuilder）；
        各个具体建造者负责进行产品部件的构建；
        最终构建成具体产品（Product）。

 */