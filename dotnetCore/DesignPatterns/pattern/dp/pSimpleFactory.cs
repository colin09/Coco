namespace pattern.dp.simpleF {
    using System;

    public class SimpleFactoryPattern {

        public static void main (string[] args) {

            //Factory mFactory = new Factory ();

            //客户要产品A
            try {
                //调用工厂类的静态方法 & 传入不同参数从而创建产品实例
                Factory.Manufacture ("A").Show ();
            } catch (NullReferenceException ex) {
                System.Console.WriteLine ("没有这一类产品" + ex.Message);
            }

            //客户要产品B
            try {
                Factory.Manufacture ("B").Show ();
            } catch (NullReferenceException ex) {
                System.Console.WriteLine ("没有这一类产品" + ex.Message);
            }

            //客户要产品C
            try {
                Factory.Manufacture ("C").Show ();
            } catch (NullReferenceException ex) {
                System.Console.WriteLine ("没有这一类产品" + ex.Message);
            }

            //客户要产品D
            try {
                Factory.Manufacture ("D").Show ();
            } catch (NullReferenceException ex) {
                System.Console.WriteLine ("没有这一类产品" + ex.Message);
            }
        }
    }

    abstract class Product {
        public abstract void Show ();
    }

    //具体产品类A
    class ProductA : Product {

        public override void Show () {
            System.Console.WriteLine ("生产出了产品A ");
        }
    }

    //具体产品类B
    class ProductB : Product {

        public override void Show () {
            System.Console.WriteLine ("生产出了产品C ");
        }
    }

    //具体产品类C
    class ProductC : Product {

        public override void Show () {
            System.Console.WriteLine ("生产出了产品C ");
        }
    }

    class Factory {

        public static Product Manufacture (string ProductName) {
            //工厂类里用switch语句控制生产哪种商品；
            //使用者只需要调用工厂类的静态方法就可以实现产品类的实例化。
            switch (ProductName) {
                case "A":
                    return new ProductA ();
                case "B":
                    return new ProductB ();
                case "C":
                    return new ProductC ();
                default:
                    return null;
            }
        }
    }

}