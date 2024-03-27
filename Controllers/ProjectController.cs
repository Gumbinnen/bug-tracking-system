using AutoMapper;
using BugTrackingSystem.Enums;
using BugTrackingSystem.Interfaces;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core;

namespace BugTrackingSystem.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUserRepository userRepository;
        private readonly IPermissionRepository permissionRepository;
        private readonly IMapper mapper;

        public ProjectController(
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IPermissionRepository permissionRepository,
            IMapper mapper)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.permissionRepository = permissionRepository;
            this.mapper = mapper;
        }

        // GET: ProjectController
        [Authorize()]
        public async Task<ActionResult> Index()
        {
            var authenticationErrorResult = this.HandleAuthentication();
            if (authenticationErrorResult != null)
            {
                return authenticationErrorResult;
            }

            var user = await userRepository.GetAsync(User);

            if (user == null)
            {
                return View();
            }

            var projects = await projectRepository.BelongingToUser(user).GetAllAsync();

            var indexProjectVM = new IndexProjectViewModel(projects);

            return View(indexProjectVM);
        }

        public async Task<ActionResult> Project(string id) // Belonging and Accessible handle partly
        {
            var authenticationErrorResult = this.HandleAuthentication();
            if (authenticationErrorResult != null)
            {
                return authenticationErrorResult;
            }

            var user = await userRepository.GetAsync(User);

            if (user == null)
            {
                throw new NotImplementedException("Not implemented when user is null.");
            }

            var project = await projectRepository.BelongingToUser(user).GetByIdAsync(id);

            if (project == null)
            {
                throw new NotImplementedException("Not implemented when project is null.");
            }

            var projectVM = mapper.Map<ProjectViewModel>(project);

            return View(projectVM);
        }

        public async Task<ActionResult> Details(string id)
        {
            var authenticationErrorResult = this.HandleAuthentication();
            if (authenticationErrorResult != null)
            {
                return authenticationErrorResult;
            }

            var user = await userRepository.GetAsync(User);

            if (user == null)
            {
                throw new NotImplementedException("Not implemented when user is null.");
            }

            var project = await projectRepository.AccessibleToUser(user).GetByIdAsync(id);

            if (project == null)
            {
                throw new NotImplementedException("Not implemented when project is null.");
            }

            var permission = await permissionRepository.GetByNameAsync(PermissionName.PROJECT_VIEW_DETAILS)
                                                    ?? throw new ObjectNotFoundException("Permission object not found, but it was expected to exist.");

            bool canViewDetails = await permissionRepository.CheckPair(user, project).HasPermission(permission);

            if (!canViewDetails)
            {
                throw new NotImplementedException("Not implemented when user is not permitted to view project details.");
            }

            var projectVM = mapper.Map<DetailsViewModel>(project);

            return View(projectVM);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// <see langword="null" /> if the user is authenticated; otherwise, an <see cref="ActionResult" />.
        /// </returns>
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

            // TODO: authentication handler here.

            // User authenticated
            return null;
        }
    }
}
