using Bunifu.UI.WinForms.Extensions;
using BusinessLayer.Services;
using C1.Win.C1GanttView;
using C1.Win.C1Themes;
using DataLayer.Domain;
using DTOLayer.Models;
using PresentationLayer.AppContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.UC_SideBar
{
    public partial class CtrlPanelGant : UserControl
    {
        private readonly UserDTO user = UserSession.Instance.User;

        private List<TaskForGanttChartDTO> tasks = new List<TaskForGanttChartDTO>();

        private readonly ITaskServices taskServices = new TaskServices();

        private ProjectForListDTO _currentProject;
        public ProjectForListDTO CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                if (_currentProject != null)
                {
                    LoadTasksList();
                    LoadProjectTasks(tasks);
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                LoadTasksList();
                LoadProjectTasks(tasks);
            }
        }

        public CtrlPanelGant()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this.Dock = DockStyle.Fill;

            c1GanttView1.Dock = DockStyle.Fill;
            c1GanttView1.ToolStrip.Enabled = false;
            c1GanttView1.Columns[0].Visible = false;
            c1GanttView1.EmptyAreaBackColor = Color.White;
            c1GanttView1.Font = new Font("Segoe UI", 14);


            c1GanttView1.Tasks.AllowEdit = false;
            c1GanttView1.Tasks.AllowNew = false;
            c1GanttView1.Tasks.AllowRemove = false;
        }

        private void LoadTasksList()
        {
            if (CurrentProject == null)
                return;

            try
            {
                tasks = taskServices.GetTaskByProjectIdWithDependencies(CurrentProject.Id, user.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving tasks: " + ex.Message);
            }
        }

        public void LoadProjectTasks(IEnumerable<TaskForGanttChartDTO> tasks)
        {
            if (c1GanttView1 == null || tasks == null)
                return;

            c1GanttView1.Tasks.Clear();
            c1GanttView1.Resources.Clear();

            // a dictionary to track added tasks by their ID
            Dictionary<int, C1.Win.C1GanttView.Task> addedTasks = new Dictionary<int, C1.Win.C1GanttView.Task>();

            // Add all tasks to the gantt view
            foreach (var domainTask in tasks)
            {
                if (domainTask.IsDeleted.GetValueOrDefault(false))
                    continue;

                var ganttTask = new C1.Win.C1GanttView.Task();
                ganttTask.ID = domainTask.Id;
                ganttTask.Name = domainTask.Name;
                ganttTask.Start = domainTask.StartDate ?? DateTime.Today;

                // Calculate end date from start date and estimated hours
                if (domainTask.DueDate.HasValue)
                {
                    // Calculate duration in days
                    TimeSpan span = (TimeSpan)(domainTask.DueDate.Value - ganttTask.Start);
                    ganttTask.Duration = Math.Max(1, (int)Math.Ceiling(span.TotalDays));
                }
                else if (domainTask.EstimatedHours.HasValue)
                {
                    // Use estimated hours and convert to days (assuming 8 hour work days)
                    ganttTask.Duration = (int)Math.Ceiling((double)domainTask.EstimatedHours.Value / 8.0);
                }
                else
                {
                    ganttTask.Duration = 1; // Default to 1 day if no estimates
                }

                // Set percent complete
                if (domainTask.PercentComplete.HasValue)
                    ganttTask.PercentComplete = (int)domainTask.PercentComplete.Value;

                // Set constraint type based on dates
                if (domainTask.StartDate.HasValue)
                {
                    ganttTask.ConstraintType = ConstraintType.StartNoEarlierThan;
                    ganttTask.ConstraintDate = domainTask.StartDate.Value;
                }

                c1GanttView1.Tasks.Add(ganttTask);
                addedTasks.Add(domainTask.Id, ganttTask);
            }

            // Set parent-child relationships
            foreach (var domainTask in tasks)
            {
                if (domainTask.IsDeleted.GetValueOrDefault(false) || !addedTasks.ContainsKey(domainTask.Id))
                    continue;

                if (domainTask.ParentTaskId.HasValue && addedTasks.ContainsKey(domainTask.ParentTaskId.Value))
                {
                    var ganttTask = addedTasks[domainTask.Id];
                    var parentTask = addedTasks[domainTask.ParentTaskId.Value];

                    // Set parent
                    ganttTask.OutlineParentID = parentTask.ID;
                }
            }

            // Setup task dependencies
            foreach (var domainTask in tasks)
            {
                if (domainTask.IsDeleted.GetValueOrDefault(false) || !addedTasks.ContainsKey(domainTask.Id))
                    continue;

                // Get task dependencies
                if (domainTask.TaskDependencies != null)
                {
                    foreach (var dependency in domainTask.TaskDependencies)
                    {
                        if (!addedTasks.ContainsKey(dependency.DependsOnTaskId))
                            continue;

                        Predecessor predecessor = new Predecessor();
                        predecessor.PredecessorTaskID = dependency.DependsOnTaskId;

                        // Map dependency type
                        switch (dependency.DependencyType)
                        {
                            case 1:
                                predecessor.PredecessorType = PredecessorType.FinishToStart;
                                break;
                            case 2:
                                predecessor.PredecessorType = PredecessorType.StartToStart;
                                break;
                            case 3:
                                predecessor.PredecessorType = PredecessorType.FinishToFinish;
                                break;
                            case 4:
                                predecessor.PredecessorType = PredecessorType.StartToFinish;
                                break;
                            default:
                                predecessor.PredecessorType = PredecessorType.FinishToStart;
                                break;
                        }

                        addedTasks[domainTask.Id].Predecessors.Add(predecessor);
                    }
                }
            }

            HashSet<int> processedUserIds = new HashSet<int>();
            foreach (var domainTask in tasks)
            {
                if (domainTask.IsDeleted.GetValueOrDefault(false) || !addedTasks.ContainsKey(domainTask.Id))
                    continue;

                if (domainTask.AssignedUserId > 0 && !processedUserIds.Contains(domainTask.AssignedUserId))
                {

                    Resource resource = new Resource();
                    resource.ID = domainTask.AssignedUserId;
                    resource.Name = domainTask.Name;
                    c1GanttView1.Resources.Add(resource);

                    // Add resource to the task
                    ResourceRef resourceRef = new ResourceRef();
                    resourceRef.ResourceID = resource.ID;
                    resourceRef.Amount = 1;
                    addedTasks[domainTask.Id].ResourceRefs.Add(resourceRef);

                    processedUserIds.Add(domainTask.AssignedUserId);
                }
            }

        }
    }
}
