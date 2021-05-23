using AutoMapper;
using System;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Mapper.Resolvers;
using TeamManagement.Contracts.v1.Requests;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Mapper.MapperProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<ArticleCreateRequest, Article>().BeforeMap((request, article) => {
                if (request.Status == "")
                {
                    request.Status = "Draft";
                }

                article.DateOfPublishing = DateTime.Now;
            }).ForMember(article => article.Publisher, options => {
                options.MapFrom<ArticlePublisherResolver>();
            });

            CreateMap<HowToArticleCreateRequest, HowToArticle>().BeforeMap((request, article) => {
                article.DateOfPublishing = DateTime.Now;
            }).ForMember(article => article.Publisher, options => {
                options.MapFrom<ArticlePublisherResolver>();
            });

            CreateMap<CreatePollRequest, Poll>();
            CreateMap<EventCreateRequest, Event>().AfterMap((req, ev) =>
            {
                ev.DateTime = DateTime.Parse(req.DateTime);
            });

            CreateMap<CreateOptionRequest, Option>();

            CreateMap<ArticleUpdateRequest, Article>();
            CreateMap<HowToArticleUpdateRequest, HowToArticle>();
            CreateMap<TagCreateRequest, Tag>();
            CreateMap<Company, CompanyCreateRequest>().ReverseMap();
            CreateMap<AppUser, UserCreateRequest>().ReverseMap();
            CreateMap<Subscription, SubscriptionUpdateRequest>().ReverseMap();
            CreateMap<Company, CompanyGetRequest>().ReverseMap();
            CreateMap<Subscription, SubscriptionGetRequest>().ReverseMap();
            CreateMap<SubscriptionPlan, SubscriptionPlanGetRequest>().ReverseMap();
            CreateMap<SubscriptionPlan, SubscriptionUpdateRequest>().ReverseMap();
            CreateMap<Transaction, TransactionGetRequest>().ReverseMap();
            CreateMap<Subscription, SubscriptionCreateRequest>().ReverseMap();
            CreateMap<Transaction, TransactionUpdateRequest>().ReverseMap();
            CreateMap<AppUser, EmployeeRegistrationRequest>().ReverseMap();
            CreateMap<Project, ProjectCreateRequest>().ReverseMap();
            CreateMap<Team, TeamCreateRequest>().ReverseMap();
            CreateMap<TeamProject, TeamProjectCreateRequest>().ReverseMap();
        }
    }
}
