namespace pattern.dp {
    using System;

    public class SingletonPattern {

        public static void main (string[] args) {

            StoreHouse mStoreHouse1 = StoreHouse.getInstance ();
            StoreHouse mStoreHouse2 = StoreHouse.getInstance ();
            Carrier Carrier1 = new Carrier (mStoreHouse1);
            Carrier Carrier2 = new Carrier (mStoreHouse2);

            System.Console.WriteLine ("两个是不是同一个？");

            if (mStoreHouse1.Equals(mStoreHouse2)) {
                System.Console.WriteLine ("是同一个");
            } else {
                System.Console.WriteLine ("不是同一个");
            }
            //搬运工搬完货物之后出来汇报仓库商品数量
            Carrier1.MoveIn (30);
            System.Console.WriteLine ("仓库商品余量：" + Carrier1.mStoreHouse.getQuantity ());
            Carrier2.MoveOut (50);
            System.Console.WriteLine ("仓库商品余量：" + Carrier2.mStoreHouse.getQuantity ());
        }

    }

    //单例仓库类
    class StoreHouse {

        //仓库商品数量
        private int quantity = 100;
        //自己在内部实例化
        private static StoreHouse ourInstance = new StoreHouse ();
        //让外部通过调用getInstance()方法来返回唯一的实例。
        public static StoreHouse getInstance () {
            return ourInstance;
        }

        //封闭构造函数
        private StoreHouse () { }

        public void setQuantity (int quantity) {
            this.quantity = quantity;
        }

        public int getQuantity () {
            return quantity;
        }
    }

    //搬货工人类
    class Carrier {
        public StoreHouse mStoreHouse;
        public Carrier (StoreHouse storeHouse) {
            mStoreHouse = storeHouse;
        }
        //搬货进仓库
        public void MoveIn (int i) {
            mStoreHouse.setQuantity (mStoreHouse.getQuantity () + i);
        }
        //搬货出仓库
        public void MoveOut (int i) {
            mStoreHouse.setQuantity (mStoreHouse.getQuantity () - i);
        }
    }

}


/**
    单例模式的实现方式有多种，根据需求场景，可分为2大类、6种实现方式。
        a. 初始化单例类时 即 创建单例
            1. 饿汉式 ·  
                应用场景 · 除了初始化单例类时 即 创建单例外，继续延伸出来的是：单例对象 要求初始化速度快 & 占用内存小
                class Singleton {

                    // 1. 加载该类时，单例就会自动被创建
                    private static  Singleton ourInstance  = new  Singleton();
                    
                    // 2. 构造函数 设置为 私有权限
                    // 原因：禁止他人创建实例 
                    private Singleton() {
                    }
                    
                    // 3. 通过调用静态方法获得创建的单例
                    public static  Singleton newInstance() {
                        return ourInstance;
                    }
                }

            2. 枚举类型 · 这是 最简洁、易用 的单例实现方式  ·  单元素的枚举类型已经成为实现 Singleton的最佳方法
                public enum Singleton{

                    //定义1个枚举的元素，即为单例类的1个实例
                    INSTANCE;

                    // 隐藏了1个空的、私有的 构造方法
                    // private Singleton () {}

                }
                // 获取单例的方式：
                Singleton singleton = Singleton.INSTANCE;

        b. 按需、延迟创建单例
            1. 懒汉式（基础实现） 
                · 与 饿汉式 最大的区别是：单例创建的时机
                · 缺点 · 基础实现的懒汉式是线程不安全的
                class Singleton {
                    // 1. 类加载时，先不自动创建单例  即，将单例的引用先赋值为 Null
                    private static  Singleton ourInstance  = null；

                    // 2. 构造函数 设置为 私有权限
                    // 原因：禁止他人创建实例 
                    private Singleton() {
                    }
                    
                    // 3. 需要时才手动调用 newInstance（） 创建 单例   
                    public static  Singleton newInstance() {
                    // 先判断单例是否为空，以避免重复创建
                    if( ourInstance == null){
                        ourInstance = new Singleton();
                        }
                        return ourInstance;
                    }
                }
            2. 同步锁（懒汉式的改进） · 缺点 · 每次访问都要进行线程同步（即 调用synchronized锁)，造成过多的同步开销（加锁 = 耗时、耗能）
                // 写法1
                class Singleton {
                    // 1. 类加载时，先不自动创建单例
                    //  即，将单例的引用先赋值为 Null
                    private static  Singleton ourInstance  = null；
                    
                    // 2. 构造函数 设置为 私有权限
                    // 原因：禁止他人创建实例 
                    private Singleton() {
                    }
                    
                    // 3. 加入同步锁
                    public static synchronized Singleton getInstance(){
                        // 先判断单例是否为空，以避免重复创建
                        if ( ourInstance == null )
                            ourInstance = new Singleton();
                        return ourInstance;
                    }
                }


                // 写法2
                // 该写法的作用与上述写法作用相同，只是写法有所区别
                class Singleton{ 

                    private static Singleton instance = null;

                    private Singleton(){
                    }

                    public static Singleton getInstance(){
                        // 加入同步锁
                        synchronized(Singleton.class) {
                            if (instance == null)
                                instance = new Singleton();
                        }
                        return instance;
                    }
                }
            3. 双重校验锁（懒汉式的改进） · 缺点 · 实现复杂 = 多种判断，易出错
                class Singleton {
                    private static  Singleton ourInstance  = null；

                    private Singleton() {
                    }
                    
                    public static  Singleton newInstance() {
                        // 加入双重校验锁
                        // 校验锁1：第1个if
                        if( ourInstance == null){  // ①
                            synchronized (Singleton.class){ // ②
                                // 校验锁2：第2个 if
                                if( ourInstance == null){
                                    ourInstance = new Singleton();
                                }
                            }
                        }
                        return ourInstance;
                    }
                }

                // 说明
                // 校验锁1：第1个if
                // 作用：若单例已创建，则直接返回已创建的单例，无需再执行加锁操作
                // 即直接跳到执行 return ourInstance

                // 校验锁2：第2个 if 
                // 作用：防止多次创建单例问题
                // 原理
                // 1. 线程A调用newInstance()，当运行到②位置时，此时线程B也调用了newInstance()
                // 2. 因线程A并没有执行instance = new Singleton();，此时instance仍为空，因此线程B能突破第1层 if 判断，运行到①位置等待synchronized中的A线程执行完毕
                // 3. 当线程A释放同步锁时，单例已创建，即instance已非空
                // 4. 此时线程B 从①开始执行到位置②。此时第2层 if 判断 = 为空（单例已创建），因此也不会创建多余的实例

            4. 静态内部类 · 原理 · 根据 静态内部类 的特性，同时解决了按需加载、线程安全的问题，同时实现简洁
                class Singleton {
    
                    // 1. 创建静态内部类
                    private static class Singleton2 {
                    // 在静态内部类里创建单例
                    private static  Singleton ourInstance  = new Singleton()；
                    }

                    // 私有构造函数
                    private Singleton() {
                    }
                    
                    // 延迟加载、按需创建
                    public static  Singleton newInstance() {
                        return Singleton2.ourInstance;
                    }

                }

                // 调用过程说明：
                    // 1. 外部调用类的newInstance() 
                    // 2. 自动调用Singleton2.ourInstance
                    // 2.1 此时单例类Singleton2得到初始化
                    // 2.2 而该类在装载 & 被初始化时，会初始化它的静态域，从而创建单例；
                    // 2.3 由于是静态域，因此只会JVM只会加载1遍，Java虚拟机保证了线程安全性
                    // 3. 最终只创建1个单例







 */