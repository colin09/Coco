using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.ioc.IOCAdapter
{
    /// <summary>
    /// 通用的Service Locator接口，该接口用户从容器中获取服务实例或者向容器中注册实例
    /// </summary>
    public interface ILocator
    {
        #region Properties

        /// <summary>
        /// 获取当前所使用的容器名称
        /// </summary>
        string Name { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 向容器中注册 单例服务类型
        /// </summary>
        /// <typeparam name="TService">要注册的服务类型</typeparam>
        /// <typeparam name="TType">要注册的组件</typeparam>
        /// <exception>在获取实例过程中抛出异常
        ///   <cref>ActivationException</cref>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void RegisterSingleton<TService, TType>() where TType : TService;

        /// <summary>
        /// 向容器中注册 单例服务类型
        /// </summary>
        /// <typeparam name="TService">要注册的服务类型</typeparam>
        /// <typeparam name="TType">要注册的组件</typeparam>
        /// <exception>在获取实例过程中抛出异常
        ///   <cref>ActivationException</cref>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void RegisterSingleton<TService, TType>(string key) where TType : TService;

        /// <summary>
        /// 向容器中注册服务类型
        /// </summary>
        /// <typeparam name="TService">要注册的服务类型</typeparam>
        /// <typeparam name="TType">要注册的组件</typeparam>
        /// <exception>在获取实例过程中抛出异常
        ///   <cref>ActivationException</cref>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void Register<TService, TType>() where TType : TService;

        /// <summary>
        /// 通过<paramref name="key"/>作为键值向容器中注册服务类型
        /// </summary>
        /// <typeparam name="TService">要注册的服务类型</typeparam>
        /// <typeparam name="TType">要注册的组件</typeparam>
        /// <param name="key">所指定的键值</param>
        /// <exception>在获取实例过程中抛出异常
        ///   <cref>ActivationException</cref>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void Register<TService, TType>(string key) where TType : TService;

        /// <summary>
        /// 通过<typeparamref name="TService"/>指定的服务类型获取服务实例
        /// </summary>
        /// <typeparam name="TService">要获取的对象服务类型</typeparam>
        /// <exception>在获取实例过程中抛出异常
        ///   <cref>ActivationException</cref>
        /// </exception>
        /// <returns>请求的服务实例</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        TService Resolve<TService>();

        /// <summary>
        /// 通过<typeparamref>
        ///     <name>key</name>
        ///   </typeparamref> 给定的名称获取服务实例
        /// </summary>
        /// <typeparam name="TService">要获取的对象服务类型</typeparam>
        /// <param name="key">对象在容器中注册的名称</param>
        /// <exception>在获取实例过程中抛出异常
        ///   <cref>ActivationException</cref>
        /// </exception>
        /// <returns>请求的服务实例</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        TService Resolve<TService>(string key);

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns></returns>
        IEnumerable<object> ResolveAll(Type type);

        #endregion

        #region Events

        /// <summary>
        /// 向容器中注册类型之前触发该事件
        /// </summary>
        event EventHandler<EventArgs> Registering;

        /// <summary>
        /// 向容器中注册类型之后触发该事件
        /// </summary>
        event EventHandler<EventArgs> Registered;

        /// <summary>
        /// 向容器中获取类型之前触发该事件
        /// </summary>
        event EventHandler<EventArgs> Resolving;

        /// <summary>
        /// 向容器中获取类型之后触发该事件
        /// </summary>
        event EventHandler<EventArgs> Resolved;

        #endregion
    }
}
