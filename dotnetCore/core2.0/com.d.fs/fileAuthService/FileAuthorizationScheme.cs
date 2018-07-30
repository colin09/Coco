namespace com.d.fs.fileAuthService {

    public class FileAuthorizationScheme {

        public FileAuthorizationScheme (string name, Type handlerType) {

            if (string.IsNullOrEmpty (name)) {
                throw new ArgumentException ("name must be a valid string.", nameof (name));
            }

            Name = name;
            HandlerType = handlerType ??
                throw new ArgumentNullException (nameof (handlerType));
        }


        
        public string Name { get; }

        public Type HandlerType { get; }
    }
}