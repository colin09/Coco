namespace pattern.dp.abs {

    public class pAbstractFactory {

        //生产工作流程
        public class AbstractFactoryPattern {

            public static void main (string[] args) {

                FactoryA mFactoryA = new FactoryA ();
                FactoryB mFactoryB = new FactoryB ();
                //A厂当地客户需要容器产品A
                mFactoryA.ManufactureContainer ().Show ();
                //A厂当地客户需要模具产品A
                mFactoryA.ManufactureMould ().Show ();

                //B厂当地客户需要容器产品B
                mFactoryB.ManufactureContainer ().Show ();
                //B厂当地客户需要模具产品B
                mFactoryB.ManufactureMould ().Show ();

            }
        }
    }

    abstract class Factory {
        public abstract Product ManufactureContainer ();
        public abstract Product ManufactureMould ();
    }

    //A厂 - 生产模具+容器产品
    class FactoryA : Factory {

        public override Product ManufactureContainer () {
            return new ContainerProductA ();
        }

        public override Product ManufactureMould () {
            return new MouldProductA ();
        }
    }

    //B厂 - 生产模具+容器产品
    class FactoryB : Factory {

        public override Product ManufactureContainer () {
            return new ContainerProductB ();
        }

        public override Product ManufactureMould () {
            return new MouldProductB ();
        }
    }

    abstract class Product {
        public abstract void Show ();
    }

    //容器产品抽象类
    abstract class ContainerProduct : Product {

        public override abstract void Show ();
    }

    //模具产品抽象类
    abstract class MouldProduct : Product {

        public override abstract void Show ();
    }

    //容器产品A类
    class ContainerProductA : ContainerProduct {

        public override void Show () {
            System.Console.WriteLine ("生产出了容器产品A");
        }
    }

    //容器产品B类
    class ContainerProductB : ContainerProduct {

        public override void Show () {
            System.Console.WriteLine ("生产出了容器产品B");
        }
    }

    //模具产品A类
    class MouldProductA : MouldProduct {

        public override void Show () {
            System.Console.WriteLine ("生产出了模具产品A");
        }
    }

    //模具产品B类
    class MouldProductB : MouldProduct {

        public override void Show () {
            System.Console.WriteLine ("生产出了模具产品B");
        }
    }

}