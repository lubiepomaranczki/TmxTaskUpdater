using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TmxTaskUpdater.Dto;

namespace TmxTaskUpdater.Api
{
    public interface IAzureDevOpsApi
    {
        [Headers("Content-Type: application/json")]
        [Post("/{organization}/{project-id}/{team-id}/_apis/wit/wiql?api-version=5.1")]
        Task<QueryResponse> FilterTasksWithQuery([Body(BodySerializationMethod.Serialized)] QueryRequest query, [Header("Authorization")] string authorization);

        [Headers("Content-Type: application/json-patch+json")]
        [Get("/{organization}/{project-id}/{team-id}/_apis/wit/wiql/{queryId}?api-version=5.1")]
        Task<QueryResponse> FilterWithExisitinQuery(string queryID, [Header("Authorization")] string authorization);

        [Headers("Content-Type: application/json-patch+json")]
        [Patch("/{organization}/{project-id}/_apis/wit/workitems/{itemId}?api-version=5.1")]
        Task<QueryResponse> UpdateItem(string itemId, [Body(BodySerializationMethod.Serialized)] IList<UpdateItemRequest> updateQueries, [Header("Authorization")] string authorization);
    }
}

