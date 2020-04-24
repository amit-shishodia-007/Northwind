using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Models;
using Northwind.Service;

namespace Northwind.Controllers
{
    public class RegionController : Controller
    {
        private readonly ILogger<RegionController> _logger;
        private readonly IRegionService _regionService;

        public RegionController(ILogger<RegionController> logger, IRegionService regionService)
        {
            _logger = logger;
            _regionService = regionService;
        }
        // GET: Region
        public ActionResult Index()
        {
            try
            {
                var allRegions = _regionService.GetAll();
                return View(allRegions);
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // GET: Region/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var details = _regionService.GetDetail(id);
                return View(details);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Region/Create
        public ActionResult Create()
        {
            try
            {
                var model = new RegionModel();
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Region/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    _regionService.Add(model);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Region/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var details = _regionService.GetDetail(id);
                return View(details);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Region/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, RegionModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                model.RegionId = Id;
                _regionService.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Region/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var details = _regionService.GetDetail(id);
                return View(details);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Region/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RegionModel model, int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                // TODO: Add delete logic here
                _regionService.Delete(Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}