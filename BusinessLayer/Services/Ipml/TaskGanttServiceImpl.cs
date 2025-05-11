using C1.Win.C1GanttView;
using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PresentationLayer.Utils
{
    public class TaskGanttService
    {
        /// <summary>
        /// Loads project tasks into the C1GanttView component
        /// </summary>
        public void LoadProjectTasks(C1GanttView ganttView, IEnumerable<DataLayer.Domain.Task> tasks)
        {
            if (ganttView == null || tasks == null)
                return;

            // Clear existing tasks
            ganttView.Tasks.Clear();
            ganttView.Resources.Clear();

            // Create a dictionary to track added tasks by their ID
            Dictionary<int, C1.Win.C1GanttView.Task> addedTasks = new Dictionary<int, C1.Win.C1GanttView.Task>();

            // First pass: Add all tasks to the gantt view
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

                ganttView.Tasks.Add(ganttTask);
                addedTasks.Add(domainTask.Id, ganttTask);
            }

            // Second pass: Setup parent-child relationships
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

            // Third pass: Setup task dependencies
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

            // Add resources
            HashSet<int> processedUserIds = new HashSet<int>();
            foreach (var domainTask in tasks)
            {
                if (domainTask.IsDeleted.GetValueOrDefault(false) || !addedTasks.ContainsKey(domainTask.Id))
                    continue;

                if (domainTask.AssignedUserId > 0 && !processedUserIds.Contains(domainTask.AssignedUserId))
                {
                    var user = domainTask.User;
                    if (user != null)
                    {
                        Resource resource = new Resource();
                        resource.ID = user.Id;
                        resource.Name = $"{user.FirstName} {user.LastName}";
                        ganttView.Resources.Add(resource);

                        // Add resource to the task
                        ResourceRef resourceRef = new ResourceRef();
                        resourceRef.ResourceID = resource.ID;
                        resourceRef.Amount = 1;
                        addedTasks[domainTask.Id].ResourceRefs.Add(resourceRef);

                        processedUserIds.Add(user.Id);
                    }
                }
            }
        }
    }
}
