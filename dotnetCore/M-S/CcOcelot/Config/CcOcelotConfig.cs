namespace CcOcelot {
    public class CcOcelotConfig {
        public string DbConnectionStrings { get; set; }

        public bool EnableTimer { set; get; } = false;

        public int TimerDelay { set; get; } = 30 * 60 * 1000;
    }
}