using System.Security.Claims;

namespace ReservationSystem.Extensions
{
  public static class ClaimsPrincipalExtensions
  {
    /// <summary>
    /// User Id.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>If is Authenticated return user id, otherwise return null.</returns>
    public static string GetUserId(this ClaimsPrincipal user)
    {
      if (!user.Identity.IsAuthenticated)
      {
        return null;
      }

      ClaimsPrincipal currentUser = user;

      return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
  }
}
