namespace user.Model {

    public class LoginResponse {
        
        public string AccessToken { get; set; }
        public long ExpireIn { get; set; }
        public string RefreshToken { get; set; }
    }
}