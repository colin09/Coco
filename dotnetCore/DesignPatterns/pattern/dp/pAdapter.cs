namespace pattern.dp {

    public class AdapterPattern {

        public static void main (string[] args) {

            Target mAdapter220V = new Adapter220V ();
            ImportedMachine mImportedMachine = new ImportedMachine ();

            //用户拿着进口机器插上适配器（调用Convert_110v()方法）
            //再将适配器插上原有插头（Convert_110v()方法内部调用Output_220v()方法输出220V）
            //适配器只是个外壳，对外提供110V，但本质还是220V进行供电
            mAdapter220V.Convert_110v ();
            mImportedMachine.Work ();

        }

    }

    public interface Target {
        //将220V转换输出110V（原有插头（Adaptee）没有的）
        void Convert_110v ();
    }

    class PowerPort220V {
        //原有插头只能输出220V
        public void Output_220v () { }
    }

    ///类的适配器模式
    class Adapter220V : PowerPort220V, Target {
        //期待的插头要求调用Convert_110v()，但原有插头没有
        //因此适配器补充上这个方法名
        //但实际上Convert_110v()只是调用原有插头的Output_220v()方法的内容
        //所以适配器只是将Output_220v()作了一层封装，封装成Target可以调用的Convert_110v()而已

        public void Convert_110v () {
            this.Output_220v ();
        }
    }
    class ImportedMachine {

        public void Work () {
            System.Console.WriteLine ("进口机器正常运行");
        }
    }
    /*============================================================================================================================ */

    public interface Target1 {
        //这是源类Adapteee没有的方法
        void Request ();
    }
    public class Adaptee1 {
        public void SpecificRequest () { }
    }
    //对象的适配器模式
    class Adapter1 : Target1 {
        // 直接关联被适配类  
        private Adaptee1 adaptee;

        // 可以通过构造函数传入具体需要适配的被适配类对象  
        public Adapter1 (Adaptee1 adaptee) {
            this.adaptee = adaptee;
        }
        public void Request () {
            // 这里是使用委托的方式完成特殊功能  
            this.adaptee.SpecificRequest ();
        }
    }
    public class AdapterPattern1 {
        public static void main (string[] args) {
            //需要先创建一个被适配类的对象作为参数  
            Target1 mAdapter = new Adapter1 (new Adaptee1 ());
            mAdapter.Request ();

        }
    }

}