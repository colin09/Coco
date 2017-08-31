using com.wx.common.helper;
using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace com.wx.listener.HttpModules
{
    public class RequestLogHttpModule : IHttpModule
    {
         ILog _log;

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            UnityBootStrapper.Init();
            _log = LocatorFacade.Current.Resolve<ILog>();
            //在 IIS 7.0 集成模式下

            #region  --  HttpApplication19个标准事件  --

            context.BeginRequest += OnBeginRequest;
            //context.AuthenticateRequest += OnAuthenticateRequest;
            //context.PostAuthenticateRequest += OnPostAuthenticateRequest;
            //context.AuthorizeRequest += OnAuthorizeRequest;
            //context.PostAuthorizeRequest += OnPostAuthorizeRequest;

            //context.ResolveRequestCache += OnResolveRequestCache;
            //context.PostResolveRequestCache += OnPostResolveRequestCache;
            //context.PostMapRequestHandler += OnPostMapRequestHandler;
            //context.AcquireRequestState += OnAcquireRequestState;
            //context.PostAcquireRequestState += OnPostAcquireRequestState;

            //context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
            //context.PostRequestHandlerExecute += OnPostRequestHandlerExecute;
            //context.ReleaseRequestState += OnReleaseRequestState;
            //context.PostReleaseRequestState += OnPostReleaseRequestState;
            //context.UpdateRequestCache += OnUpdateRequestCache;
            //context.PostUpdateRequestCache += OnPostUpdateRequestCache;

            //context.LogRequest += OnLogRequest;
            //context.PostLogRequest += OnPostLogRequest;
            //context.EndRequest += OnEndRequest;

            #endregion

            context.Error += OnError;
            //context.MapRequestHandler += OnMapRequestHandler;
            //context.PreSendRequestContent += OnPreSendRequestContent;
            //context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            _log.Info("<hr />");

            StringBuilder str = new StringBuilder();
            str.Append("1 : OnBeginRequest <br />");

            HttpContext context = HttpContext.Current;
            var url = context.Request.Url;
            var browser = context.Request.Browser;
            var queryStr = context.Request.QueryString;
            var form = context.Request.Form;

            str.Append("====》 url:" + url + "<br />");
            str.Append("====》 browser:" + browser + "<br />");
            str.Append("====》 query:" + queryStr + "<br />");
            str.Append("====》 form:" + form + "<br />");
            _log.Info(str.ToString());
        }
        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            _log.Info("2 : OnAuthenticateRequest");
        }
        private void OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            _log.Info("3 : OnPostAuthenticateRequest");
        }
        private void OnAuthorizeRequest(object sender, EventArgs e)
        {
            _log.Info("4 : OnAuthorizeRequest");
        }
        private void OnPostAuthorizeRequest(object sender, EventArgs e)
        {
            _log.Info("5 : OnPostAuthorizeRequest");
        }

        private void OnResolveRequestCache(object sender, EventArgs e)
        {
            _log.Info("6 : OnResolveRequestCache");
        }
        private void OnPostResolveRequestCache(object sender, EventArgs e)
        {
            _log.Info("7 : OnPostResolveRequestCache");
        }
        private void OnPostMapRequestHandler(object sender, EventArgs e)
        {
            _log.Info("8 : OnPostMapRequestHandler");
        }
        private void OnAcquireRequestState(object sender, EventArgs e)
        {
            _log.Info("9 : OnAcquireRequestState");
        }
        private void OnPostAcquireRequestState(object sender, EventArgs e)
        {
            _log.Info("10 : OnPostAcquireRequestState");
        }

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            _log.Info("11 : OnPreRequestHandlerExecute");
        }
        private void OnPostRequestHandlerExecute(object sender, EventArgs e)
        {
            _log.Info("12 : OnPostRequestHandlerExecute");
        }
        private void OnReleaseRequestState(object sender, EventArgs e)
        {
            _log.Info("13 : OnReleaseRequestState");
        }
        private void OnPostReleaseRequestState(object sender, EventArgs e)
        {
            _log.Info("14 : OnPostReleaseRequestState");
        }
        private void OnUpdateRequestCache(object sender, EventArgs e)
        {
            _log.Info("15 : OnUpdateRequestCache");
        }
        private void OnPostUpdateRequestCache(object sender, EventArgs e)
        {
            _log.Info("16 : OnPostUpdateRequestCache");
        }
        private void OnLogRequest(object sender, EventArgs e)
        {
            _log.Info("17 : OnLogRequest");
        }
        private void OnPostLogRequest(object sender, EventArgs e)
        {
            _log.Info("18 : OnPostLogRequest");
        }
        private void OnEndRequest(object sender, EventArgs e)
        {
            _log.Info("19 : OnEndRequest");
        }


        private void OnError(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context == null) return;

            var exception = context.Server.GetLastError();
            if (exception == null) return;

            var httpException = exception as HttpException;

            int statusCode = 404;
            if (httpException != null)
            {
                statusCode = httpException.GetHttpCode();
            }

            //包括记录异常的内部包含异常
            while (exception != null)
            {
                //_log.Error("Global:");
                _log.Error(exception);
                exception = exception.InnerException;
            }

            context.Server.ClearError();
            context.Response.TrySkipIisCustomErrors = true;

            //输出错误信息
            var response = String.Empty;

            var result = new
            {
                Message = "服务器正在维护请稍后重试！",
                StatusCode = statusCode//StatusCode.InternalServerError
            };
            response =result.ToJson();

            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Write(response);

            /*
            var format = context.Request[Define.Format];
            if (String.IsNullOrEmpty(format))
            {
                format = String.Empty; // 如果为空，将会使用默认值
            }
             
            switch (format.ToLower())
            {
                case Define.Json:
                    response = Utils.DataContractToJson(result);
                    context.Response.ContentType = "application/json; charset=utf-8";
                    break;
                case Define.Xml:
                    //response = result.ToXml();
                    context.Response.ContentType = "text/xml; charset=utf-8";
                    break;
                default:
                    response = Utils.DataContractToJson(result);
                    context.Response.ContentType = "text/html; charset=utf-8";
                    break;
            }
            context.Response.Write(response);
            */
        }
        private void OnMapRequestHandler(object sender, EventArgs e)
        {
            _log.Info("8.1 : OnMapRequestHandler");
        }
        private void OnPreSendRequestContent(object sender, EventArgs e)
        {
            _log.Info("19.1 : OnPreSendRequestContent");
        }
        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            _log.Info("19.2 : OnPreSendRequestHeaders");
        }

    }
}


#region  --  HttpApplication  --


/*
        * 
        * 
        * 
    公共属性	Application	获取应用程序的当前状态。
    公共属性	Context	获取关于当前请求的 HTTP 特定信息。
    受保护的属性	Events	获取处理所有应用程序事件的事件处理程序委托列表。
    公共属性	Modules	获取当前应用程序的模块集合。
    公共属性	Request	获取当前请求所对应的内部请求对象。
    公共属性	Response	获取当前请求所对应的内部响应对象。
    公共属性	Server	获取当前请求所对应的内部服务器对象。
    公共属性	Session	获取提供对会话数据的访问的内部会话对象。
    公共属性	Site	获取或设置 IComponent 实现的网站接口。
    公共属性	User	获取当前请求的内部用户对象。
        * 
        * 
        * 
        * 
        * 
    公共事件    AcquireRequestState	当 ASP.NET 获取与当前请求关联的当前状态（如会话状态）时发生。
    公共事件	AuthenticateRequest	当安全模块已建立用户标识时发生。
    公共事件	AuthorizeRequest	当安全模块已验证用户授权时发生。
    公共事件	BeginRequest	在 ASP.NET 响应请求时作为 HTTP 执行管线链中的第一个事件发生。
    公共事件*	Disposed	在释放应用程序时发生。
    公共事件	EndRequest	在 ASP.NET 响应请求时作为 HTTP 执行管线链中的最后一个事件发生。
    公共事件*	Error	当引发未经处理的异常时发生。
    公共事件	LogRequest	恰好在 ASP.NET 为当前请求执行任何记录之前发生。
    公共事件*	MapRequestHandler	基础结构。在选择了用来响应请求的处理程序时发生。
    公共事件	PostAcquireRequestState	在已获得与当前请求关联的请求状态（例如会话状态）时发生。
    公共事件	PostAuthenticateRequest	当安全模块已建立用户标识时发生。
    公共事件	PostAuthorizeRequest	在当前请求的用户已获授权时发生。
    公共事件	PostLogRequest	在 ASP.NET 处理完 LogRequest 事件的所有事件处理程序后发生。
    公共事件	PostMapRequestHandler	在 ASP.NET 已将当前请求映射到相应的事件处理程序时发生。
    公共事件	PostReleaseRequestState	在 ASP.NET 已完成所有请求事件处理程序的执行并且请求状态数据已存储时发生。
    公共事件	PostRequestHandlerExecute	在 ASP.NET 事件处理程序（例如，某页或某个 XML Web service）执行完毕时发生。
    公共事件	PostResolveRequestCache	在 ASP.NET 跳过当前事件处理程序的执行并允许缓存模块满足来自缓存的请求时发生。
    公共事件	PostUpdateRequestCache	在 ASP.NET 完成缓存模块的更新并存储了用于从缓存中为后续请求提供服务的响应后，发生此事件。
    公共事件	PreRequestHandlerExecute	恰好在 ASP.NET 开始执行事件处理程序（例如，某页或某个 XML Web services）前发生。
    公共事件*	PreSendRequestContent	恰好在 ASP.NET 向客户端发送内容之前发生。
    公共事件*	PreSendRequestHeaders	恰好在 ASP.NET 向客户端发送 HTTP 标头之前发生。
    公共事件	ReleaseRequestState	在 ASP.NET 执行完所有请求事件处理程序后发生。 该事件将使状态模块保存当前状态数据。
    公共事件	ResolveRequestCache	在 ASP.NET 完成授权事件以使缓存模块从缓存中为请求提供服务后发生，从而绕过事件处理程序（例如某个页或 XML Web services）的执行。
    公共事件	UpdateRequestCache	当 ASP.NET 执行完事件处理程序以使缓存模块存储将用于从缓存为后续请求提供服务的响应时发生。
        * 
        * 
        * 
        * 
        * 
    按照以下顺序引发应用程序事件：
    || 
    BeginRequest  ------------->在 ASP.NET 响应请求时作为 HTTP 执行管线链中的第一个事件发生；
 * 
    AuthenticateRequest ------->当安全模块已建立用户标识时发生；
 * 
    PostAuthenticateRequest --->当安全模块已建立用户标识时发生；
 * 
    AuthorizeRequest ---------->当安全模块已验证用户授权时发生；
 * 
    PostAuthorizeRequest ------>在当前请求的用户已获授权时发生；
 * 
    ResolveRequestCache ------->在 ASP.NET 执行完所有请求事件处理程序后发生；该事件将使状态模块保存当前状态数据；
 * 
    PostResolveRequestCache --->在 ASP.NET 跳过当前事件处理程序的执行并允许缓存模块满足来自缓存的请求时发生；
 * 
    在 PostResolveRequestCache 事件之后和 PostMapRequestHandler 事件之前，会创建一个事件处理程序（一个对应于请求 URL 的页）。 如果服务器在集成模式下运行 IIS 7.0 并且 .NET Framework 至少为 3.0 版本，则会引发 MapRequestHandler 事件。 如果服务器在经典模式下运行 IIS 7.0 或者运行的是较早版本的 IIS，则无法处理此事件。
 * 
    PostMapRequestHandler ----->在 ASP.NET 已将当前请求映射到相应的事件处理程序时发生；
 * 
    AcquireRequestState ------->当 ASP.NET 获取与当前请求关联的当前状态（如会话状态）时发生；
 * 
    PostAcquireRequestState --->在已获得与当前请求关联的请求状态（例如会话状态）时发生；
 * 
    PreRequestHandlerExecute -->恰好在 ASP.NET 开始执行事件处理程序（例如，某页或某个 XML Web services）前发生；
 * 
    执行事件处理程序。
 * 
    PostRequestHandlerExecute ->在 ASP.NET 事件处理程序（例如，某页或某个 XML Web service）执行完毕时发生；
 * 
    ReleaseRequestState ------->在 ASP.NET 执行完所有请求事件处理程序后发生。 该事件将使状态模块保存当前状态数据；
 * 
    PostReleaseRequestState --->在 ASP.NET 已完成所有请求事件处理程序的执行并且请求状态数据已存储时发生；
 * 
    在引发 PostReleaseRequestState 事件之后，现有的所有响应筛选器都将对输出进行筛选。
 * 
    UpdateRequestCache -------->当 ASP.NET 执行完事件处理程序以使缓存模块存储将用于从缓存为后续请求提供服务的响应时发生；
 * 
    PostUpdateRequestCache ---->在 ASP.NET 完成缓存模块的更新并存储了用于从缓存中为后续请求提供服务的响应后，发生此事件；
 * 
    LogRequest . -------------->恰好在 ASP.NET 为当前请求执行任何记录之前发生；
 * 
    仅在 IIS 7.0 处于集成模式并且 .NET Framework 至少为 3.0 版本的情况下才支持此事件。
    PostLogRequest ------------>在 ASP.NET 处理完 LogRequest 事件的所有事件处理程序后发生；
 * 
    仅在 IIS 7.0 处于集成模式并且 .NET Framework 至少为 3.0 版本的情况下才支持此事件。
    EndRequest ---------------->在 ASP.NET 响应请求时作为 HTTP 执行管线链中的最后一个事件发生；
        * 
        * 
        * 
    */

#endregion




#region  --  ASP.NET系统中默认的HttpModule：
/*
    DefaultAuthenticationModule 确保上下文中存在 Authentication 对象。无法继承此类。 
    FileAuthorizationModule 验证远程用户是否具有访问所请求文件的 NT 权限。无法继承此类。 
    FormsAuthenticationModule 启用 ASP.NET 应用程序以使用 Forms 身份验证。无法继承此类。 
    PassportAuthenticationModule 提供环绕 PassportAuthentication 服务的包装。无法继承此类。 
    SessionStateModule   为应用程序提供会话状态服务。 
    UrlAuthorizationModule   提供基于 URL 的授权服务以允许或拒绝对指定资源的访问。无法继承此类。 
    WindowsAuthenticationModule 启用 ASP.NET 应用程序以使用 Windows/IIS 身份验证。无法继承此类
*/
#endregion