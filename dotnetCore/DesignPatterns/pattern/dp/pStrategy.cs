namespace pattern.dp {
    public class StrategyPattern {
        public static void main (string[] args) {

            Context_SalesMan mSalesMan;

            //春节来了，使用春节促销活动
            System.Console.WriteLine ("对于春节：");
            mSalesMan = new Context_SalesMan("A");
            mSalesMan.SalesManShow ();

            //中秋节来了，使用中秋节促销活动
            System.Console.WriteLine ("对于中秋节：");
            mSalesMan = new Context_SalesMan("B");
            mSalesMan.SalesManShow ();

            //圣诞节来了，使用圣诞节促销活动
            System.Console.WriteLine ("对于圣诞节：");
            mSalesMan = new Context_SalesMan("C");
            mSalesMan.SalesManShow ();
        }
    }

    public abstract class Strategy {

        public abstract void Show ();
    }

    //为春节准备的促销活动A
    class StrategyA : Strategy {

        public override void Show () {
            System.Console.WriteLine ("为春节准备的促销活动A");
        }
    }

    //为中秋节准备的促销活动B
    class StrategyB : Strategy {

        public override void Show () {
            System.Console.WriteLine ("为中秋节准备的促销活动B");
        }
    }

    //为圣诞节准备的促销活动C
    class StrategyC : Strategy {

        public override void Show () {
            System.Console.WriteLine ("为圣诞节准备的促销活动C");
        }
    }
    class Context_SalesMan {
        //持有抽象策略角色的引用
        private Strategy strategy;

        //生成销售员实例时告诉销售员什么节日（构造方法）
        //使得让销售员根据传入的参数（节日）选择促销活动（这里使用一个简单的工厂模式）
        public Context_SalesMan(string festival) {
            switch (festival) {
                //春节就使用春节促销活动
                case "A":
                    strategy = new StrategyA ();
                    break;
                    //中秋节就使用中秋节促销活动
                case "B":
                    strategy = new StrategyB ();
                    break;
                    //圣诞节就使用圣诞节促销活动
                case "C":
                    strategy = new StrategyC ();
                    break;
            }

        }

        //向客户展示促销活动
        public void SalesManShow () {
            strategy.Show ();
        }

    }

}