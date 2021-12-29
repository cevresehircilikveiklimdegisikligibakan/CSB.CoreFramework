// <copyright file="ServiceRegister.cs" company="Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü">
// Copyright (c) Çevre Şehircilik ve İklim Değişikliği Bakanlığı - Coğrafi Bilgi sistemleri Genel Müdürlüğü - Yazılım Teknolojileri Daire Başkanlığı. Tüm hakları saklıdır.
// </copyright>
using CSB.Core.Utilities.Redis.Entities;
using CSB.Core.Utilities.Redis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSB.Core.Utilities.Redis
{
    /// <summary>
    /// Redis önbellek mekanizması için genişletme metotlarını barındırır.
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// <see cref="RedisOptions"/> sınıfının özelliklerine göre redis önbellek servisini ekler.
        /// </summary>
        /// <param name="services">Servis kümesi.</param>
        /// <param name="configuration">Yapılandırma ayarları.</param>
        /// <returns>Servis kümesini döndürür.</returns>
        public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            RedisOptions redisOptions = new RedisOptions();
            IConfigurationSection configurationSection = configuration.GetSection(nameof(RedisOptions));
            configurationSection.Bind(redisOptions);

            services.Configure<RedisOptions>(configureOptions =>
            {
                configureOptions.ConnectionString = redisOptions.ConnectionString;
                configureOptions.DatabaseId = redisOptions.DatabaseId;
                configureOptions.Timeout = redisOptions.Timeout;
            });

            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IRedisGeospatialService, RedisService>();
            return services;
        }
    }
}