//-----------------------------------------------------------------------
// <copyright file="GroupController.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace BillSplitterAPI.Controllers.BillSplitter
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using BillSplitterAPI.DAL;

    /// <summary>
    /// Group information
    /// </summary>
    public class GroupsController : ApiController
    {
        /// <summary>
        /// Get all the groups of the user
        /// </summary>
        /// <returns>Group details</returns>
        [HttpGet]
        public HttpResponseMessage GetGroupInfo()
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    if (entities != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Groups.ToList());
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Get specific group details
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>Specific Group details</returns>
        [HttpGet]
        public HttpResponseMessage GetGroupInfoById(int groupId)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var entity = entities.Groups.FirstOrDefault(g => g.GroupId == groupId);

                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Creates a new group
        /// </summary>
        /// <param name="groupName">Group name</param>
        /// <returns>Http status</returns>
        [HttpPost]
        public HttpResponseMessage CreateGroup([FromBody]string groupName)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    Group grp = new Group() { GroupName = groupName, CreatedOn = DateTime.Now, CreatedBy = "Admin" };

                    entities.Groups.Add(grp);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Updates the group info
        /// </summary>
        /// <param name="grp">Group detail</param>
        /// <returns>http status</returns>
        [HttpPut]
        public HttpResponseMessage UpdateGroupInfoById([FromBody]Group grp)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    var entity = entities.Groups.FirstOrDefault(g => g.GroupId == grp.GroupId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Group not found. Please try again later");
                    }
                    else
                    {
                        entity.GroupName = grp.GroupName;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Deletes a group
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>Http Status</returns>
        [HttpDelete]
        public HttpResponseMessage DeleteGroup(int groupId)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    var entity = entities.Groups.FirstOrDefault(g => g.GroupId == groupId);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Group not found. Please try again later");
                    }
                    else
                    {
                        entities.Groups.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
