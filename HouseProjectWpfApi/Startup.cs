using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer.Repositories;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.EF;
using BuisnesLogicLayer.DTO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccesLayer.Enteties;
using AutoMapper;
using BuisnesLogicLayer.MappersConfigurations;
using BuisnesLogicLayer.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using HouseProjectWpfApi.Commands;

namespace HouseProjectWpfApi
{
    public class Startup
    {
        //private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public static void ConfigureServices(IServiceCollection services)
        {
            #region Repositories
            services.AddTransient<IAdRepository, AdRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            services.AddTransient<IForCompareRepository, ForCompareRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            #endregion

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #region Services
            services.AddTransient<IAdServices, AdServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ICommentServices, CommentServices>();
            services.AddTransient<IFavoritesServices, FavoritesServices>();
            services.AddTransient<IForCompareServices, ForcompareServices>();
            services.AddTransient<IImageServices, ImageServices>();
            #endregion

            #region Validators
            services.AddTransient<IValidator<AdCreateDTO>, AdValidator>();
            services.AddTransient<IValidator<AdEditDTO>, AdEditValidator>();
            services.AddTransient<IValidator<UserRegisterDTO>, UserValidator>();
            services.AddTransient<IValidator<CommentCreateDTO>, CommentValidator>();
            #endregion

            services.AddDbContext<AppDBContext>(opts => opts.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = UAHP1; Trusted_Connection = True; MultipleActiveResultSets=true"));

            services.AddControllers();
                     
            // For Mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // For Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();
        }
    }
}
