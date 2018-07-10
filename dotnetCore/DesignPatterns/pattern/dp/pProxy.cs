namespace pattern.dp {

    public class ProxyPattern {

        public static void main (string[] args) {
            Subject proxy = new Proxy ();
            proxy.buyMac ();
        }

    }

    public interface Subject {
        void buyMac ();
    }

    public class RealSubject : Subject {
        public void buyMac () {
            System.Console.WriteLine ("买一台Mac");
        }
    }

    public class Proxy : Subject {

        public void buyMac () {
            //引用并创建真实对象实例，即”我“
            RealSubject realSubject = new RealSubject ();
            //调用真实对象的方法，进行代理购买Mac
            realSubject.buyMac ();
            //代理对象额外做的操作
            this.WrapMac ();
        }

        public void WrapMac () {
            System.Console.WriteLine ("用盒子包装好Mac");
        }
    }

}