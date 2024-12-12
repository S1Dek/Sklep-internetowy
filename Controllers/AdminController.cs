using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Account;
using System.Threading.Tasks;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public AdminController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> ManageUsers()
    {
        var users = _userManager.Users; // Pobiera wszystkich użytkowników
        return View(users);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles;

        var model = new EditUserViewModel
        {
            User = user,
            AssignedRoles = roles,
            AllRoles = allRoles
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.User.Id.ToString());
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = model.User.FirstName;
        user.LastName = model.User.LastName;
        user.Email = model.User.Email;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // Aktualizacja ról
        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = model.SelectedRoles.Except(currentRoles);
        var rolesToRemove = currentRoles.Except(model.SelectedRoles);

        await _userManager.AddToRolesAsync(user, rolesToAdd);
        await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        return RedirectToAction("ManageUsers");
    }
    public async Task<IActionResult> Details(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles;

        var model = new EditUserViewModel
        {
            User = user,
            AssignedRoles = roles,
            AllRoles = allRoles
        };

        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Unable to delete user.");
        }

        return RedirectToAction("ManageUsers");
    }
}
