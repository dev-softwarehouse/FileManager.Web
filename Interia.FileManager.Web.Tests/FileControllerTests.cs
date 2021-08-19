using Interia.FileManager.Web.Controllers;
using Interia.FileManager.Web.Services;
using Interia.Resources.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Interia.FileManager.Web.Tests
{
	public class FileControllerTests
	{
		private Mock<IFileService> _mockFileService;
		private Mock<IStringLocalizer<Messages>> _mockLocalizedMessages;

		public FileControllerTests()
		{
			_mockFileService = new Mock<IFileService>();
			_mockLocalizedMessages = new Mock<IStringLocalizer<Messages>>();
		}

		[Fact]
		public void IndexMethodShouldReturnViewResult()
		{
			var controller = new FileController(_mockFileService.Object, _mockLocalizedMessages.Object);
			var result = controller.Index();
			Assert.IsType<ViewResult>(result);
			_mockFileService.Verify(m => m.GetFiles(), Times.Once);
		}
	}
}
