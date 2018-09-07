namespace com.d.fs.fileAuthService {

    public interface IFileAuthorizationHandlerProvider {

        Type GetHandlerType (string scheme);

        bool Exist (string scheme);

    }
}

