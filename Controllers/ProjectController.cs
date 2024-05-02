using AutoMapper;
using BugTrackingSystem.Enums;
using BugTrackingSystem.Enums.PermissionType;
using BugTrackingSystem.Interfaces.Repository;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Models.Entities.Permission;
using BugTrackingSystem.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
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
        public async Task<ActionResult> AllProjects()
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

            var allProjectVM = mapper.Map<IndexProjectViewModel>(projects);

            if (allProjectVM == null)
            {
                throw new NotImplementedException("Not implemented when allProjectVM is null.");
            }

            return View("Index", allProjectVM);
        }

        [Authorize()]
        public async Task<ActionResult> AccessibleProjects()
        {
            //TODO: middleware auth

            var user = await userRepository.GetAsync(User);

            if (user == null)
            {
                throw new NotImplementedException("Not implemented when user is null.");
            }

            var projects = await projectRepository.AccessibleToUser(user).GetAllAsync();

            if (!projects.Any())
            {
                throw new NotImplementedException("Not implemented when project count is zero.");
            }

            var accessibleProjectVM = mapper.Map<IndexProjectViewModel>(projects);

            if (accessibleProjectVM == null)
            {
                throw new NotImplementedException("Not implemented when accessibleProjectVM is null.");
            }

            return View("Index", accessibleProjectVM);
        }

        [Authorize()]
        public async Task<ActionResult> OwnedProjects()
        {
            // middleware auth

            var user = await userRepository.GetAsync(User);

            if (user == null)
            {
                throw new NotImplementedException("Not implemented when user is null.");
            }

            var projects = await projectRepository.BelongingToUser(user).GetAllAsync();

            if (!projects.Any())
            {
                throw new NotImplementedException("Not implemented when project count is zero.");
            }

            var ownedProjectVM = mapper.Map<IndexProjectViewModel>(projects);

            if (ownedProjectVM == null)
            {
                throw new NotImplementedException("Not implemented when ownedProjectVM is null.");
            }

            return View("Index", ownedProjectVM);
        }


        [Authorize()] //!!!!!!!!!!! instead of HandleAuthentication();
                            // also midleware customization. Login page instead of 401 Unautorized
        public async Task<ActionResult> Project(string id) // Belonging and Accessible handle partly. PROJECTS!!!!!!! NOT PROJECT
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

            var projectVM = mapper.Map<ProjectViewModel>(project);

            if (projectVM == null)
            {
                throw new NotImplementedException("Not implemented when projectVM is null.");
            }

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

            var permission = new ProjectPermission(ProjectPermissionType.ViewDetails);
            bool canViewDetails = await permissionRepository.CheckPair(user, project).HasPermission(permission);

            if (!canViewDetails)
            {
                throw new NotImplementedException("Not implemented when user is not permitted to view project details.");
            }

            var projectVM = mapper.Map<DetailsViewModel>(project);

            if (projectVM == null)
            {
                throw new NotImplementedException("Not implemented when projectVM is null.");
            }

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
                return RedirectToAction(nameof(AllProjects));
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
                return RedirectToAction(nameof(AllProjects));
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
                return RedirectToAction(nameof(AllProjects));
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
