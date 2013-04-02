using System.Web.Mvc;
using Cronos.Core.Domain.Album.Commands;
using Cronos.Core.Domain.Album.Queries;
using Cronos.Infrastructure.Commands;
using Cronos.Infrastructure.Queries;

namespace Cronos.Web.Controllers
{
    public class AlbumsController : Controller
    {
		public IProcessQuery QueryProcessor { get; set; }
		public IProcessCommand CommandProcessor { get; set; }

    	public ActionResult Index(){ return View();}
		
		[HttpGet]
		public ActionResult Browse(GetAllAlbumsQuery query)
		{
			return Json(QueryProcessor.Execute(query), JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Details(GetAlbumsByIdQuery query)
		{
			return PartialView("Details", QueryProcessor.Execute(query));
    	}


		[HttpGet]
		public ActionResult Edit(GetAlbumsByIdQuery query)
		{
			return PartialView("Edit", QueryProcessor.Execute(query));
		}

		[HttpGet]
		public ActionResult Insert()
		{
			return PartialView("Insert", QueryProcessor.Execute(new GetNewAlbumQuery()));
		}


		[HttpPost]
		public void Edit(EditAlbumCommand command)
		{
			CommandProcessor.Execute(command);
		}
		
		[HttpPost]
		public void Insert(InsertAlbumCommand command)
		{
			CommandProcessor.Execute(command);
		}

		[HttpPost]
		public void Delete(DeleteAlbumCommand command)
		{
			CommandProcessor.Execute(command);
		}
    }
}
