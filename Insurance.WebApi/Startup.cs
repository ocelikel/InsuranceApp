using AutoMapper;
using Insurance.Model;
using Insurance.Model.Models;
using Insurance.Repository.Repository.Concrete;
using Insurance.Repository.Repository.Interface;
using Insurance.Service.Cqrs.Command;
using Insurance.Service.Cqrs.CommandHandler;
using Insurance.Service.Cqrs.Query;
using Insurance.Service.Cqrs.QueryHandler;
using Insurance.Service.Dispatcher;
using Insurance.Service.Extensions;
using Insurance.Service.Mapping;
using Insurance.Service.Services;
using Insurance.Shared.Engine;
using Insurance.Shared.SignalR;
using Insurance.Shared.ViewModels;
using InsuranceQueue;
using InsuranceQueue.Consumers;
using MassTransit;
using MassTransit.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();
            services.AddScoped<ICreateUserInsuranceService, CreateUserInsuranceService>();
            services.AddScoped<IGetUserOffersService, GetUserOffersService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICreateUserOfferService, CreateUserOfferService>();
            //services.AddScoped(typeof(IDomainCommandHandler<CreateUserCommand>), typeof(CreateUserCommandHandler));
            services.AddScoped(typeof(IDomainCommandAsyncHandler<CreateUserCommand>), typeof(CreateUserCommandHandler));
            services.AddScoped(typeof(IDomainCommandAsyncHandler<CreateOfferCommand>), typeof(CreateOfferCommandHandler));
            services.AddScoped(typeof(IQueryHandler<GetUserOffersByIdentityNumberQuery, List<OfferViewModel>>), typeof(GetUserOffersByIdentityNumberQueryHandler));
            services.AddScoped(typeof(IQueryHandler<GetUserOffersByProcessIdQuery, List<OfferViewModel>>), typeof(GetUserOffersByProcessIdQueryHandler));
            services.AddScoped(typeof(IQueryHandler<GetUserInfoQuery, UserInformationRequest>), typeof(GetUserInfoQueryHandler));
            
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IContext>(f => { return new InsuranceContext("Server = (localdb)\\MSSQLLocalDB; Database = InsuranceDb; Trusted_Connection = True; "); });
            
            services.AddCors();
            services.AddSignalR();

            services.AddMassTransit(x =>
            {
                x.AddSignalRHub<OfferHub>(cfg => {/*Configure hub lifetime manager*/});
                x.AddConsumer<GetInsuranceOfferConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    string rabbitmqConnectionString = Configuration["RabbitMqConnectionString"];
                    cfg.Host(rabbitmqConnectionString);
                    cfg.ConfigureEndpoints(context);
                });

                const string redisConfigurationString = "127.0.0.1";
                x.AddSagaStateMachine<GetOfferProcessStateMachine, GetOfferProcessState>(typeof(GetOfferProcessStateMachineDefinition))
                   .RedisRepository(r =>
                   {
                       r.DatabaseConfiguration(redisConfigurationString);

                       r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                   });

            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserInsuranceMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddEntityFrameworkSqlServer();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Insurance.WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder.WithOrigins("https://localhost:44366")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            });
         
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Insurance.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OfferHub>("/offerHub");
            });
        }
    }
}
