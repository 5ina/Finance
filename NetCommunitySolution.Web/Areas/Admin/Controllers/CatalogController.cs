using Abp.AutoMapper;
using Abp.UI;
using NetCommunitySolution.Domain.Products;
using NetCommunitySolution.Products;
using NetCommunitySolution.Web.Areas.Admin.Models.Products;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.Layui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Admin.Controllers
{
    public class CatalogController : AdminBaseController
    {

        #region ctor && Fields
        private readonly ICategoryService _categoryService;

        public CatalogController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        #endregion

        #region Utilities
        [NonAction]
        protected void PrepareCategoryModel(CategoryModel model)
        {
            if (model == null)
                throw new UserFriendlyException("model");

            var parents = _categoryService.GetAllCategories(parentId: 0);
            foreach (var item in parents.Items)
            {
                model.AvailableParentCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Selected = false,
                    Text = item.Name,
                });
            }
            model.AvailableParentCategories.Insert(0, new SelectListItem
            {
                Value = "0",
                Selected = true,
                Text = "顶级类别",
            });
        }

        #endregion

        #region Method

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult List(DataSourceRequest command, string keywords)
        {
            var categories = _categoryService.GetAllCategories(keywords: keywords,
                showHidden: true,
                pageIndex: command.page - 1,
                pageSize: command.limit);
            var jsonData = new DataSourceResult
            {
                data = categories.Items.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Parent = x.ParentId == 0 ? "--" : _categoryService.GetCategoryById(x.ParentId).Name,
                    Published = x.Published ? "发布" : "未发布",
                    DisplayOrder = x.DisplayOrder,                    
                }).ToList(),
                count = categories.TotalCount,
            };
            return AbpJson(jsonData);
        }

        public ActionResult Create()
        {
            var model = new CategoryModel();
            PrepareCategoryModel(model);
            model.DisplayOrder = 999;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CategoryModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<Category>();
                model.Id = _categoryService.InsertCategory(entity);
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("Index");
            }

            PrepareCategoryModel(model);
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            var model = category.MapTo<CategoryModel>();
            PrepareCategoryModel(model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CategoryModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryService.GetCategoryById(model.Id);
                category = model.MapTo<CategoryModel, Category>(category);
                _categoryService.UpdateCategory(category);
                return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("Index");
            }
            PrepareCategoryModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return AbpJson("success");
            }
            catch (Exception e)
            {
                return AbpJson(e.Message);
            }

        }

        #endregion

    }
}