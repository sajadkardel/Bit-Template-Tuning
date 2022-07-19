using MimeTypes;
using BTT.Data.Models.Account;
using SystemFile = System.IO.File;
using BTT.Api.Infrastructure;

namespace BTT.Api.Controllers;

public partial class AttachmentController : BaseController
{
    [AutoInject] private IOptionsSnapshot<AppSettings> _appSettings = default!;

    [AutoInject] private UserManager<User> _userManager = default!;

    [AutoInject] private IWebHostEnvironment _webHostEnvironment = default!;

    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    [DisableRequestSizeLimit]
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadProfileImage(IFormFile? file, CancellationToken cancellationToken)
    {
        if (file is null)
            throw new BadRequestException();

        var userId = User.GetUserId();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            throw new ResourceNotFoundException();

        var profileImageName = Guid.NewGuid().ToString();

        await using var fileStream = file.OpenReadStream();

        Directory.CreateDirectory(_appSettings.Value.UserProfileImagePath);

        var path = Path.Combine($"{_appSettings.Value.UserProfileImagePath}\\{profileImageName}{Path.GetExtension(file.FileName)}");

        await using var targetStream = SystemFile.Create(path);

        await fileStream.CopyToAsync(targetStream, cancellationToken);

        if (user.ProfileImageName is not null)
        {
            try
            {
                var filePath = Directory.GetFiles(_appSettings.Value.UserProfileImagePath,
                    $"{user.ProfileImageName}.*").FirstOrDefault();

                if (filePath != null)
                {
                    SystemFile.Delete(filePath);
                }
            }
            catch
            {
                // not important
            }
        }

        try
        {
            user.ProfileImageName = profileImageName;

            await _userManager.UpdateAsync(user);
        }
        catch
        {
            SystemFile.Delete(path);

            throw;
        }

        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> RemoveProfileImage()
    {
        var userId = User.GetUserId();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            throw new ResourceNotFoundException();

        var filePath = Directory.GetFiles(_appSettings.Value.UserProfileImagePath, $"{user.ProfileImageName}.*")
            .SingleOrDefault();

        if (filePath is null)
            throw new ResourceNotFoundException(nameof(ErrorStrings.UserImageCouldNotBeFound));

        user.ProfileImageName = null;

        await _userManager.UpdateAsync(user);

        SystemFile.Delete(filePath);

        return Ok();
    }

    [HttpGet("[action]")]
    [ResponseCache(NoStore = true)]
    public async Task<PhysicalFileResult> GetProfileImage()
    {
        var userId = User.GetUserId();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            throw new ResourceNotFoundException();

        var filePath = Directory.GetFiles(_appSettings.Value.UserProfileImagePath, $"{user.ProfileImageName}.*")
            .SingleOrDefault();

        if (filePath is null)
            throw new ResourceNotFoundException();

        return PhysicalFile(Path.Combine(_webHostEnvironment.ContentRootPath, filePath),
            MimeTypeMap.GetMimeType(Path.GetExtension(filePath)));
    }
}
