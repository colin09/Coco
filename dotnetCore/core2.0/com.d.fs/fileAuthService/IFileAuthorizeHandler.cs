namespace com.d.fs.fileAuthService {

    public interface IFileAuthorizeHandler {

        Task<FileAuthorizeResult> AuthorizeAsync (HttpContext context, string path);
    }
}