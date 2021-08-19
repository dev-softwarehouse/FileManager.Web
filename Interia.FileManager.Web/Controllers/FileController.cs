using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Interia.Core.Helper;
using Interia.FileManager.Web.Models;
using Interia.FileManager.Web.Services;
using Interia.Resources.Messages;
using Microsoft.Extensions.Localization;

namespace Interia.FileManager.Web.Controllers
{
	public class FileController : Controller
	{
		private readonly IFileService _fileService;
		private readonly IStringLocalizer<Messages> _localizer;

		public FileController(IFileService fileService, IStringLocalizer<Messages> localizer)
		{
			_fileService = fileService;
			_localizer = localizer;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var fileViewModel = _fileService.GetFiles();
			return View(fileViewModel);
		}

		[HttpPost]
		public IActionResult Upload(FileInputModel fileInputModel)
		{
			if (ModelState.IsValid)
			{
				var uploadStatus = _fileService.UploadFile(fileInputModel.File);

				if (uploadStatus)
					TempData["Message"] = _localizer.GetString(nameof(Messages.FilesUploaded)).Value;

				return RedirectToAction("Index");
			}

			TempData["Message"] = ModelState.Values.Select(v => v.Errors.FirstOrDefault()?.ErrorMessage).FirstOrDefault();
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Download(string filename)
		{
			if (filename == null)
				return Content(_localizer.GetString(nameof(Messages.InvalidFileName)));

			var stream = _fileService.GetFile(filename);
			var memoryStream = new MemoryStream();
			await stream.CopyToAsync(memoryStream);
			memoryStream.Position = 0;
			return File(memoryStream, FileHelper.GetContentType(filename), Path.GetFileName(filename));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Delete(string filename)
		{
			if (filename == null)
				return Content(_localizer.GetString(nameof(Messages.InvalidFileName)));
			try
			{
				_fileService.DeleteFile(filename);
				TempData["Message"] = _localizer.GetString(nameof(Messages.FileDeleted)).Value;
			}
			catch (FileNotFoundException e)
			{
				TempData["Message"] = e.Message;
			}
			
			 return RedirectToAction("Index");
		}
	}
}
	
