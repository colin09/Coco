namespace com.d.fs.fileAuthService {

    public interface IFileAuthorizationService {

        string AuthorizationScheme { get; }

        string FileRootPath { get; }
        
        Task<FileAuthorizeResult> AuthorizeAsync (HttpContext context, string path);

    }

}