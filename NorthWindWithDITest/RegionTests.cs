using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Northwind.Controllers;
using Northwind.Models;
using Northwind.Service;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
namespace NorthWindWithDITest
{
    public class RegionTests
    {
        [Fact]
        public void IndexWithReturnListOfAllRegions()
        {
            var mockRepo = new Mock<IRegionService>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestRegions());

            var logger = Mock.Of<ILogger<RegionController>>();

            var controller = new RegionController(logger,mockRepo.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<RegionModel>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }
        [Fact]
        public void DetailsWithReturnOfOneRegions()
        {
            var mockRepo = new Mock<IRegionService>();
            var Id = 1;
            var inputModel = GetTestRegions().SingleOrDefault(x => x.RegionId == Id);
            mockRepo.Setup(repo => repo.GetDetail(Id)).Returns(inputModel);

            var logger = Mock.Of<ILogger<RegionController>>();

            var controller = new RegionController(logger, mockRepo.Object);

            var result = controller.Details(Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            var model = Assert.IsAssignableFrom<RegionModel>(viewResult.ViewData.Model);
            Assert.Equal(model, inputModel);
            
        }
        [Fact]
        public void Add_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var mockRepo = new Mock<IRegionService>();

            var logger = Mock.Of<ILogger<RegionController>>();

            var controller = new RegionController(logger, mockRepo.Object);
            controller.ModelState.AddModelError("Name", "Required");

            var newRegion = GetTestRegions().SingleOrDefault(x => x.RegionId == 1);

            var result = controller.Create(newRegion);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
        [Fact]
        public void Add_ReturnsReturnsRedirect_WhenModelStateIsvalid()
        {
            var mockRepo = new Mock<IRegionService>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<RegionModel>())).Verifiable();

            var logger = Mock.Of<ILogger<RegionController>>();

            var controller = new RegionController(logger, mockRepo.Object);

            var newRegion = GetTestRegions().SingleOrDefault(x => x.RegionId == 1);

            var result = controller.Create(newRegion);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index",redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
        private IEnumerable<RegionModel> GetTestRegions()
        {
            return new List<RegionModel>() {
                new RegionModel()
                {
                    RegionId=1,
                    RegionDescription="Test Region 1"
                },
                new RegionModel()
                {
                    RegionId=2,
                    RegionDescription="Test Region 2"
                }
            };
        }
    }
}
