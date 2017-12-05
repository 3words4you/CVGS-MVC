using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CVGS;
using CVGS.Controllers;
using CVGS.Models;

namespace CVGS.Tests.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        //all game ctl test
        GameController gameCtl = new GameController();
        Game game = new Game()
        {
            gameID = 10000,
            title = "game",
            description = "description",
            categoryID = 3,
            subCategoryID = 4,
            price = 10,
            publisher = "publisher",
            develpoer = "developer",
            releasedDate = DateTime.Now,
            createdDate = DateTime.Now,
            updatedDate = DateTime.Now,
        };

        [TestMethod]
        public void TestAddGameWithoutTitleShouldFailed()
        {
            // Arrange

            var game = this.game;
            game.title = null;

            // Act
            try
            {
                var result = gameCtl.Create(game);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(game.title);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void TestUpdateGameWithoutTitleShouldFailed()
        {
            // Arrange

            var game = this.game;
            game.title = null;

            // Act
            try
            {
                var result = gameCtl.Edit(game);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(game.title);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void SuccessAddGame()
        {
            var game = this.game;
            // Act
            try
            {
                var result = gameCtl.Create(game);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(game);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }


        //all event ctl test
        EventController eventCtl = new EventController();
        Event e = new Event()
        {
            eventID = 10000,
            title = "game",
            startDate = DateTime.Now.AddDays(-1),
            endDate = DateTime.Now,
            createdDate = DateTime.Now,
            updatedDate = DateTime.Now,
        };

        [TestMethod]
        public void TestAddEventWithoutTitleShouldFail()
        {
            // Arrange

            var e = this.e;
            e.title = null;

            // Act
            try
            {
                var result = eventCtl.Create(e);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(e.title);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void TestUpdateEventWithoutTitleShouldFailed()
        {
            // Arrange

            var e = this.e;
            e.title = null;

            // Act
            try
            {
                var result = eventCtl.Edit(e);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(e.title);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void TestEventStartDateMoreThanEndDateShouldFail()
        {
            // Arrange
            var e = this.e;
            e.startDate = DateTime.Now.AddDays(1);
            e.endDate = DateTime.Now;

            // Act
            try
            {
                var result = eventCtl.Edit(e);
                //if success should return redirectToRouteResult
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void SuccessAddEvent()
        {
            var e = this.e;
            // Act
            try
            {
                var result = eventCtl.Create(e);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(e);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        //all category ctl test
        CategoryController catCtl = new CategoryController();
        Category cat = new Category()
        {
            categoryID = 10000,
            categoryName = "category",
            createdDate = DateTime.Now,
            updatedDate = DateTime.Now,
        };

        [TestMethod]
        public void TestAddCategoryWithoutNameShouldFailed()
        {
            // Arrange

            var cat = this.cat;
            cat.categoryName = null;

            // Act
            try
            {
                var result = catCtl.Create(cat);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(cat.categoryName);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void TestEditCategoryWithoutNameShouldFailed()
        {
            // Arrange

            var cat = this.cat;
            cat.categoryName = null;

            // Act
            try
            {
                var result = catCtl.Edit(cat);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(cat.categoryName);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void SuccessAddCategory()
        {
            var cat = this.cat;
            // Act
            try
            {
                var result = catCtl.Create(cat);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(cat);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        //all paltform ctl test
        PlatformController ptCtl = new PlatformController();
        Platform pt = new Platform()
        {
            platformID = 10000,
            platformName = "platform",
            createdDate = DateTime.Now,
            updatedDate = DateTime.Now,
        };

        [TestMethod]
        public void TestAddPlatformWithoutNameShouldFailed()
        {
            // Arrange

            var pt = this.pt;
            pt.platformName = null;

            // Act
            try
            {
                var result = ptCtl.Create(pt);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(pt.platformName);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void TestEditPlatformWithoutNameShouldFailed()
        {
            // Arrange

            var pt = this.pt;
            pt.platformName = null;

            // Act
            try
            {
                var result = ptCtl.Edit(pt);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(pt.platformName);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void SuccessAddPlatform()
        {
            var pt = this.pt;
            // Act
            try
            {
                var result = ptCtl.Create(pt);
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                Assert.IsNull(pt);
            }
            catch (Exception ex)
            {
                Assert.Fail("got an exception: " + ex.GetBaseException().Message);
            }
        }
    }
}
