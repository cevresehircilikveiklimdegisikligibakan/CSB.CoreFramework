// <copyright file="ServiceRegister.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>
using CSB.Core.Utilities.Caching.Redis.Services;
using CSB.Core.Utilities.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSB.Core.Utilities.Caching.Redis
{
    /// <summary>
    /// Önbellek mekanizması için genişletme metotlarını barındırır.
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// Önbellekleme servisini ekler.
        /// </summary>
        /// <param name="services">Servis kümesi.</param>
        /// <param name="configuration">Yapılandırma ayarları.</param>
        /// <returns>Servis kümesini döndürür.</returns>
        public static IServiceCollection AddRedisCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedisService(configuration);
            services.AddScoped<ICacheService, CacheService>();
            return services;
        }
    }
}