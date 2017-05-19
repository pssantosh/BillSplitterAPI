//-----------------------------------------------------------------------
// <copyright file="GroupController.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace BillSplitterAPI.Controllers.BillSplitter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using BillSplitterAPI.DAL;
    public class GroupUsersController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetUser(int groupId)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var userList = entities.GroupUsers.Where(m => m.GroupId == groupId).ToList();
                    if (userList == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "No users found");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, userList);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetUserDetails(int grpId, int grpUserId)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    entities.Configuration.ProxyCreationEnabled = false;
                    var userEntity = entities.GroupUsers.Where(g => g.GroupId == grpId).ToList().Where(u => u.GroupUserId == grpUserId);
                    if (userEntity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Group/User not found");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, userEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(int groupId,[FromBody]GroupUser grpUser)
        {
            try
            {
                using (BillSplitterEntities entites = new BillSplitterEntities())
                {
                    entites.GroupUsers.Add(grpUser);
                    entites.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateUserDetails(int grpId, [FromBody]GroupUser grpUser)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    var userEntity = entities.GroupUsers.Where(g => g.GroupId == grpId).ToList().FirstOrDefault(u => u.GroupUserId == grpUser.GroupUserId);
                    if (userEntity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Group/User not found");
                    }
                    else
                    {
                        userEntity.UserName = grpUser.UserName;
                        userEntity.DisplayName = grpUser.DisplayName;
                        userEntity.Comment = grpUser.Comment;
                        userEntity.GroupId = grpUser.GroupId;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, userEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteGroupUser(int grpId, int grpUserId)
        {
            try
            {
                using (BillSplitterEntities entities = new BillSplitterEntities())
                {
                    var userEntity = entities.GroupUsers.Where(g => g.GroupId == grpId).ToList().FirstOrDefault(u => u.GroupUserId == grpUserId);
                    if (userEntity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Group/User not found");
                    }
                    else
                    {
                        entities.GroupUsers.Remove(userEntity);
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
