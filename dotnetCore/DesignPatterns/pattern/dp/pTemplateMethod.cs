namespace pattern.dp {

    public class pTemplateMethod {
        public static void main (string[] args) {

            //炒 - 手撕包菜
            ConcreteClass_BaoCai BaoCai = new ConcreteClass_BaoCai ();
            BaoCai.cookProcess ();

            //炒 - 蒜蓉菜心
            ConcreteClass_CaiXin CaiXin = new ConcreteClass_CaiXin ();
            CaiXin.cookProcess ();
        }

    }

    public abstract class AbstractClass {
        //模板方法，用来控制炒菜的流程 （炒菜的流程是一样的-复用）
        //申明为final，不希望子类覆盖这个方法，防止更改流程的执行顺序 
        public void cookProcess () {
            //第一步：倒油
            this.pourOil ();
            //第二步：热油
            this.HeatOil ();
            //第三步：倒蔬菜
            this.pourVegetable ();
            //第四步：倒调味料
            this.pourSauce ();
            //第五步：翻炒
            this.fry ();
        }

        //定义结构里哪些方法是所有过程都是一样的可复用的，哪些是需要子类进行实现的

        //第一步：倒油是一样的，所以直接实现
        void pourOil () {
            System.Console.WriteLine ("倒油");
        }

        //第二步：热油是一样的，所以直接实现
        void HeatOil () {
            System.Console.WriteLine ("热油");
        }

        //第三步：倒蔬菜是不一样的（一个下包菜，一个是下菜心）
        //所以声明为抽象方法，具体由子类实现 
        public abstract void pourVegetable ();

        //第四步：倒调味料是不一样的（一个下辣椒，一个是下蒜蓉）
        //所以声明为抽象方法，具体由子类实现 
        public abstract void pourSauce ();

        //第五步：翻炒是一样的，所以直接实现
        void fry () {
            System.Console.WriteLine ("炒啊炒啊炒到熟啊");
        }
    }

    //炒手撕包菜的类
    public class ConcreteClass_BaoCai : AbstractClass {

        public override void pourVegetable () {
            System.Console.WriteLine ("下锅的蔬菜是包菜");
        }

        public override void pourSauce () {
            System.Console.WriteLine ("下锅的酱料是辣椒");
        }
    }
    //炒蒜蓉菜心的类
    public class ConcreteClass_CaiXin : AbstractClass {
        public override void pourVegetable () {
            System.Console.WriteLine ("下锅的蔬菜是菜心");
        }

        public override void pourSauce () {
            System.Console.WriteLine ("下锅的酱料是蒜蓉");
        }
    }

}