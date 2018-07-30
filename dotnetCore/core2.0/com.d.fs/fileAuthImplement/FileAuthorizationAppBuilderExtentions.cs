namespace com.d.fs.fileAuthImplement {

    public static class FileAuthorizationAppBuilderExtentions {

        public static IApplicationBuilder UseFileAuthorization (this IApplicationBuilder app) {
            if (app == null) {
                throw new ArgumentNullException (nameof (app));
            }

            return app.UseMiddleware<FileAuthenticationMiddleware> ();
        }
    }
}