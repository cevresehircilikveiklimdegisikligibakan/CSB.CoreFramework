// <copyright file="ServiceRegister.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>

using CSB.Core.Utilities.Caching;
using CSB.Core.Utilities.Security;
using CSB.Core.Utilities.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSB.Core.Utilities
{
    /// <summary>
    /// CSB.Core.Utilities işlemleri için servislerin kaydını yapan genişletme metotlarını barındırır.
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// CSB.Core.Utilities servislerini ekler.
        /// </summary>
        /// <param name="services">Servis kümesi.</param>
        /// <param name="settings">Serileştirme işlemleri için gerekli konfigürasyon.</param>
        /// <returns>Servis kümesini döndürür.</returns>
        public static IServiceCollection AddCoreUtilitiyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISecurityKeyService, SecurityKeyService>();
            services.AddScoped<ISigningCredentialService, SigningCredentialService>();

            services.Configure<CacheDownOptions>(o => configuration.GetSection("CacheDownOptions").Bind(o));

            return services;
        }
    }
}