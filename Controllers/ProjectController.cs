using BugTrackingSystem.Enums;
using BugTrackingSystem.Interfaces;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUserRepository userRepository;
        private readonly IPermissionRepository permissionRepository;

        public ProjectController(IProjectRepository projectRepository, IUserRepository userRepository, IPermissionRepository permissionRepository)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.permissionRepository = permissionRepository;
        }

        // GET: ProjectController
        [Authorize()]
        public async Task<ActionResult> Index()
        {
            var authenticationResult = HandleAuthentication();
            if (authenticationResult != null)
            {
                return authenticationResult;
            }

            ApplicationUser? user = await userRepository.GetAsync(User);

            if (user == null)
            {
                return View();
            }

            var projects = await projectRepository.BelongingToUser(user).GetAllAsync();

            var indexProjectVM = new IndexProjectViewModel(projects);

            return View(indexProjectVM);
        }

        public async Task<ActionResult> Project(string id)
        {
            var authenticationResult = HandleAuthentication();
            if (authenticationResult != null)
            {
                return authenticationResult;
            }

            ApplicationUser? user = await userRepository.GetAsync(User);

            if (user == null)
            {
                throw new NotImplementedException("Not implemented when user is null.");
            }

            var project = await projectRepository.BelongingToUser(user).GetByIdAsync(id);

            if (project == null)
            {
                throw new NotImplementedException("Not implemented when project is null.");
            }

            var projectVM = new ProjectViewModel(project);

            return View(projectVM);
        }

        public async Task<ActionResult> Details(ProjectViewModel projectVM)
        {
            var authenticationResult = HandleAuthentication();
            if (authenticationResult != null)
            {
                return authenticationResult;
            }

            var user = await userRepository.GetAsync(User);

            if (user == null)
            {
                throw new NotImplementedException("Not implemented when user is null.");
            }

            var project = new Project(projectVM);

            var userPermissions = await projectRepository.WithProject(project).ForUser(user).GetPermissions();

            bool canViewProjectDetails = await permissionRepository.ContainsPermissionAsync(userPermissions, PermissionName.PROJECT_VIEW_DETAILS); //?

            if (!canViewProjectDetails)
            {
                throw new NotImplementedException("Not implemented when user is not permitted to view project details.");
            }

            return View(project);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private ActionResult? HandleAuthentication()
        {
            var userIdentity = User.Identity;

            if (userIdentity == null || !userIdentity.IsAuthenticated)
            {
                return View("Login", "Account");
            }

            var authenticationType = userIdentity.AuthenticationType;
            if (authenticationType != null)
            {
                if (authenticationType.Equals("ApplicationCookie", StringComparison.OrdinalIgnoreCase))
                {
                    // TODO: change cookie auth type logic
                    // Redirect to login for non-cookie authentication types
                    //return RedirectToAction("Login", "Account");
                }
            }

            // User authenticated
            return null;
        }
    }
}
