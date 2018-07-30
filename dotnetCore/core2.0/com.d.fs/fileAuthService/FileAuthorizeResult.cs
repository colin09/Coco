namespace com.d.fs.fileAuthService {

    public class FileAuthorizeResult {

        public bool Succeeded { get; }

        public string RelativePath { get; }

        public string FileDownloadName { get; set; }

        public Exception Failure { get; }
    }
}