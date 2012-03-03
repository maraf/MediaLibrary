using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaLibrary.Web.Models.Domain;
using MediaLibrary.Web.Models.Repository;
using MediaLibrary.Web.Models.ViewModel;
using Microsoft.Practices.Unity;
using MediaLibrary.Web.Mvc;

namespace MediaLibrary.Web.Controllers
{
    public class DatabaseController : MediaLibrary.Web.Mvc.Controller
    {
        [Dependency]
        IRepository<Database> Databases { get; set; }

        [Dependency]
        IRepository<DatabaseRevision> Revisions { get; set; }

        public ActionResult Index()
        {
            return View(Databases.GetList().Where(d => d.Owner.ID == UserAccount.ID).OrderBy(d => d.Name).ThenByDescending(d => d.Revision).ToList());
        }

        public ActionResult Create()
        {
            return View(new CreateDatabaseModel());
        }

        [HttpPost]
        public ActionResult Create(CreateDatabaseModel model)
        {
            if (Databases.GetList().Count(d => d.Owner.ID == UserAccount.ID && d.Name == model.Name) != 0)
                ModelState.AddModelError("Name", String.Format("You already have database with name '{0}'.", model.Name));

            if (ModelState.IsValid)
            {
                Database database = new Database
                {
                    Name = model.Name,
                    Revision = 1,
                    OwnerID = UserAccount.ID
                };
                Databases.Add(database);

                DatabaseRevision revision = new DatabaseRevision
                {
                    DatabaseID = database.ID,
                    Content = model.Content,
                    Timestamp = DateTime.Now,
                    Revision = 1
                };
                Revisions.Add(revision);

                ShowMessage("Database created");
                return RedirectToAction("index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            Database database = Databases.Get(id);
            if (database == null || database.OwnerID != UserAccount.ID)
            {
                ShowMessage("No such database", HtmlMessageType.Error);
                return RedirectToAction("index");
            }

            return View(new EditDatabaseModel(database, Revisions.GetList().Where(r => r.DatabaseID == database.ID).OrderByDescending(r => r.Revision)));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Database database = Databases.Get(id);
            if (database == null || database.OwnerID != UserAccount.ID)
            {
                ShowMessage("No such database", HtmlMessageType.Error);
                return RedirectToAction("index");
            }

            Databases.Delete(database);
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(EditDatabaseModel model)
        {
            Database database = Databases.Get(model.ID);
            if (database == null || database.OwnerID != UserAccount.ID)
            {
                ShowMessage("No such database", HtmlMessageType.Error);
                return RedirectToAction("index");
            }

            if (ModelState.IsValid)
            {
                database.Name = model.Name;
                Databases.Update(database);
                ShowMessage("Database updated.");
            }

            return View(new EditDatabaseModel(database, Revisions.GetList().Where(r => r.DatabaseID == database.ID).OrderByDescending(r => r.Revision)));
        }

        public ActionResult CreateRevision(int id)
        {
            return View("EditRevision", new EditRevisionModel
            {
                DatabaseID = id
            });
        }

        public ActionResult EditRevision(int id)
        {
            DatabaseRevision revision = Revisions.Get(id);
            if (revision == null || revision.Database.OwnerID != UserAccount.ID)
            {
                ShowMessage("No such revision", HtmlMessageType.Error);
                return RedirectToAction("index");
            }

            return View(new EditRevisionModel
            {
                RevisionID = revision.ID,
                DatabaseID = revision.DatabaseID,
                Content = revision.Content
            });
        }

        [HttpPost]
        public ActionResult EditRevision(EditRevisionModel model)
        {
            Database database = Databases.GetList().FirstOrDefault(d => d.ID == model.DatabaseID);
            if(database == null || database.OwnerID != UserAccount.ID)
            {
                ShowMessage("No such database", HtmlMessageType.Error);
                return RedirectToAction("index");
            }

            if (ModelState.IsValid)
            {
                if (model.RevisionID == 0)
                {
                    DatabaseRevision revision = new DatabaseRevision
                    {
                        DatabaseID = database.ID,
                        Content = model.Content,
                        Timestamp = DateTime.Now,
                        Revision = database.Revision + 1
                    };
                    Revisions.Add(revision);
                    database.Revision = revision.Revision;
                    Databases.Update(database);

                    ShowMessage("Database revision created.");
                    return RedirectToAction("edit", new { id = revision.DatabaseID }); //TODO: Updating revision count on database seems not working
                }
                else
                {
                    DatabaseRevision revision = Revisions.Get(model.RevisionID);
                    if (revision != null)
                    {
                        revision.Content = model.Content;
                        revision.Timestamp = DateTime.Now;
                        Revisions.Update(revision);

                        ShowMessage("Database revision updated.");
                        return RedirectToAction("edit", new { id = revision.DatabaseID });
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult DeleteRevision(int id)
        {
            DatabaseRevision revision = Revisions.Get(id);
            if (revision == null || revision.Database.OwnerID != UserAccount.ID)
            {
                ShowMessage("No such revision", HtmlMessageType.Error);
                return RedirectToAction("index");
            }

            Revisions.Delete(revision);
            return RedirectToAction("edit", new { id = revision.DatabaseID });
        }
    }
}
