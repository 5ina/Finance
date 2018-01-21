using Abp.AutoMapper;
using Abp.Domain.Uow;
using NetCommunitySolution.Articles;
using NetCommunitySolution.Common;
using NetCommunitySolution.Domain.Articles;
using NetCommunitySolution.Domain.Configuration;
using NetCommunitySolution.Seo;
using NetCommunitySolution.Web.Areas.Admin.Models.Articles;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.Layui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public partial class TopicController : AdminBaseController
    {
        #region Fields

        private readonly ITopicService _topicService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ISettingService _settingService;

        #endregion Fields

        #region Constructors

        public TopicController(ITopicService topicService,
            IUrlRecordService urlRecordService,
            ISettingService settingService)
        {
            this._topicService = topicService;
            this._urlRecordService = urlRecordService;
            this._settingService = settingService;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected SeoSetting GetSeoSetting()
        {
            return _settingService.GetSeoSetting();
        }
        
        #endregion

        #region List

        public ActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {

            var topicModels = _topicService.GetAllTopics(true)
                .Select(x => x.MapTo<TopicModel>())
                .ToList();

            foreach (var topic in topicModels)
            {
                topic.Body = "";
            }
            var gridModel = new DataSourceResult
            {
                data = topicModels,
                count = topicModels.Count
            };

            return AbpJson(gridModel);
        }

        #endregion

        #region Create / Edit / Delete

        public ActionResult Create()
        {

            var model = new TopicModel();
            //default values
            model.DisplayOrder = 1;
            model.Published = true;

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [UnitOfWork]
        public ActionResult Create(TopicModel model, bool continueEditing)
        {

            if (ModelState.IsValid)
            {
                var seoSettings = GetSeoSetting();
                var topic = model.MapTo<Topic>();
                _topicService.InsertTopic(topic);
                //search engine name
                model.SeName = topic.ValidateSeName(model.SeName, topic.Title ?? topic.SystemName, true, seoSettings);
                _urlRecordService.SaveSlug(topic, model.SeName);
                
                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = topic.Id });
                }
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var topic = _topicService.GetTopicById(id);
            if (topic == null)
                return RedirectToAction("List");

            var model = topic.MapTo<TopicModel>();
            model.Url = Url.RouteUrl("Topic", new { SeName = topic.GetSeName() }, "http");

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [UnitOfWork]
        public ActionResult Edit(TopicModel model, bool continueEditing)
        {
            var topic = _topicService.GetTopicById(model.Id);
            if (topic == null)
                return RedirectToAction("List");
            
            if (ModelState.IsValid)
            {
                var seoSettings = GetSeoSetting();
                topic = model.MapTo<TopicModel, Topic>(topic);
                _topicService.UpdateTopic(topic);
                //search engine name
                model.SeName = topic.ValidateSeName(model.SeName, topic.Title ?? topic.SystemName, true, seoSettings);
                _urlRecordService.SaveSlug(topic, model.SeName);
                
                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = topic.Id });
                }
                return RedirectToAction("Index");
            }                        

            model.Url = Url.RouteUrl("Topic", new { SeName = topic.GetSeName() }, "http");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return RedirectToAction("List");
            _topicService.DeleteTopic(id);

            return AbpJson("success");
        }

        #endregion
    }
}