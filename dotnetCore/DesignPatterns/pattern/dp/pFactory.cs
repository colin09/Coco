namespace pattern.dp {



    public class FactoryPattern {

        public static void main (string[] args) {
            //客户要产品A
            FactoryA mFactoryA = new FactoryA ();
            mFactoryA.Manufacture ().Show ();

            //客户要产品B
            FactoryB mFactoryB = new FactoryB ();
            mFactoryB.Manufacture ().Show ();
        }
    }

    abstract class Factory {
        public abstract Product Manufacture ();
    }

    abstract class Product {
        public abstract void Show ();
    }

    //具体产品A类
    class ProductA : Product {

        public override void Show () {
            System.Console.WriteLine("生产出了产品A");
        }
    }

    //具体产品B类
    class ProductB : Product {

        public override void Show () {
            System.Console.WriteLine("生产出了产品B");
        }
    }

    //工厂A类 - 生产A类产品
    class FactoryA : Factory {

        public override Product Manufacture () {
            return new ProductA ();
        }
    }

    //工厂B类 - 生产B类产品
    class FactoryB : Factory {

        public override Product Manufacture () {
            return new ProductB ();
        }
    }


}