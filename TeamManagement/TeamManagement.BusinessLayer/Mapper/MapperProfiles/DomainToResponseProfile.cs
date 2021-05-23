using AutoMapper;
using System.Collections.Generic;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Mapper.Resolvers;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Mapper.MapperProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Article, ArticleCreateResponse>().BeforeMap((article, response) =>
            {
                response.PublisherUserName = article.Publisher.UserName;
            });

            CreateMap<Article, ArticleGetByIdResponse>().BeforeMap((article, response) =>
            {
                response.PublisherFullName = article.Publisher.FirstName + " " + article.Publisher.LastName;
            }).ForMember(response => response.IsMadeByUser, options =>
                options.MapFrom<ArticleGetByIdResponseIsMadeByUserResolver>()
            ); ;

            CreateMap<HowToArticle, HowToArticleCreateResponse>().BeforeMap((article, response) =>
            {
                response.PublisherFullName = article.Publisher.FirstName + " " + article.Publisher.LastName;
            });

            CreateMap<HowToArticle, HowToArticleGetResponse>().BeforeMap((article, response) =>
            {
                response.PublisherFullName = article.Publisher.FirstName + " " + article.Publisher.LastName;
            }).ForMember(response => response.IsMadeByUser, options =>
                options.MapFrom<HowToArticleGetResponseIsMadeByUserResolver>()
            );

            CreateMap<Article, ArticleMenuResponse>();

            CreateMap<AppUser, GetUserResponse>().AfterMap((appuser, response) => {
                response.FullName = appuser.FirstName + " " + appuser.LastName;
            }).ForMember(response => response.isAdmin, options => {
                options.MapFrom<GetUsersResponseIsAdminResolver>();
            });

            CreateMap<AppUser, TeamMembersResponse>().AfterMap((appuser, response) => response.FullName = appuser.FirstName + " " + appuser.LastName);
            CreateMap<Event, EventsForUserResponse>().AfterMap((ev, response) =>
            {
                response.CreatedByName = ev.CreatedBy.FirstName + " " + ev.CreatedBy.LastName;
                response.Attendies = new List<AttendiesResponse>();
                foreach (var att in ev.AppUserEvents)
                {
                    response.Attendies.Add(new AttendiesResponse { Email = att.AppUser.Email, Id = att.AppUser.Id, Status = att.Status });
                }
            });

            CreateMap<Option, GetPollsOptionsResponse>();
            CreateMap<Poll, GetPollsResponse>().AfterMap((poll, response) => response.CreatedByName = poll.CreatedBy.FirstName + " " + poll.CreatedBy.LastName);
            CreateMap<Company, CompanyCreateResponse>().ReverseMap();
            CreateMap<Company, CompanyGetByIdResponse>().ReverseMap();
            CreateMap<SubscriptionPlan, SubscriptionPlanGetResponse>().ReverseMap();
            CreateMap<Company, CompanyGetResponse>().ReverseMap();
            CreateMap<Subscription, SubscriptionGetResponse>().ReverseMap();
            CreateMap<SubscriptionPlan, SubscriptionPlanGetResponse>().ReverseMap();
            CreateMap<Transaction, TransactionGetResponse>().ReverseMap();
            CreateMap<Subscription, SubscriptionCreateResponse>().ReverseMap();
            CreateMap<Transaction, TransactionUpdateResponse>().ReverseMap();
            CreateMap<Project, ProjectCreateResponse>().ReverseMap();
            CreateMap<Team, TeamCreateResponse>().ReverseMap();
            CreateMap<Project, ProjectGetResponse>().ReverseMap();
            CreateMap<Team, TeamGetResponse>().ReverseMap();
            CreateMap<TeamProject, TeamProjectCreateResponse>().ReverseMap();
        }
    }
}
