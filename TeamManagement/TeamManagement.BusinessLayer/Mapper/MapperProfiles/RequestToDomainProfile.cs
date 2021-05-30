using AutoMapper;
using System;
using System.Collections.Generic;
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

            CreateMap<CreateReportRequest, Report>().AfterMap((req, rep) =>
            {
                rep.ReportRecords = new List<ReportRecord>();

                foreach (var coderev in req.CodeReview)
                {
                    rep.ReportRecords.Add(new ReportRecord { RecordName = "Code Review", Value = coderev });
                }

                foreach (var res in req.Resolved)
                {
                    rep.ReportRecords.Add(new ReportRecord { RecordName = "Resolved", Value = res });
                }

                foreach (var act in req.Active)
                {
                    rep.ReportRecords.Add(new ReportRecord { RecordName = "Active", Value = act });
                }
            });

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
            CreateMap<AppUser, EmployeeUpdateRequest>().ReverseMap();
            CreateMap<Project, ProjectUpdateRequest>().ReverseMap();
        }
    }
}
