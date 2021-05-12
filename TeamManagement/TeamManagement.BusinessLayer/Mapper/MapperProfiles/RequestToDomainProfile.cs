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

            CreateMap<ArticleUpdateRequest, Article>();
            CreateMap<HowToArticleUpdateRequest, HowToArticle>();
            CreateMap<TagCreateRequest, Tag>();
            CreateMap<Company, CompanyCreateRequest>().ReverseMap();
        }
    }
}
