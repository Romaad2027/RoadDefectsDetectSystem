using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.DAL;
using Store.DAL.Entities;
using Store.Exceptions;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Services
{
    public class ProcessedAgentDataService : IProcessedAgentDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessedAgentDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProcessedAgentData> GetProcessedAgentData(int id)
        {
            var data = await RetrieveDataById(id);
            return data;
        }

        public async Task DeleteProcessedAgentData(int id)
        {
            var data = await RetrieveDataById(id);
            await _unitOfWork.ProcessedAgentDataRepository.DeleteById(data.Id);
            await _unitOfWork.SaveChanges();
        }

        public async Task<ProcessedAgentData> AddProcessedAgentData(ProcessedAgentDataRequestModel requestData)
        {
            var entity = MapRequestModelToEntity(requestData);
            var addedData = await _unitOfWork.ProcessedAgentDataRepository.Add(entity);
            await _unitOfWork.SaveChanges();
            return addedData;
        }

        public async Task BulkAddProcessedAgentData(IEnumerable<ProcessedAgentDataRequestModel> requestDataList)
        {
            var entities = requestDataList.Select(MapRequestModelToEntity);
            await _unitOfWork.ProcessedAgentDataRepository.Add(entities);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateProcessedAgentData(int id, ProcessedAgentDataRequestModel requestData)
        {
            var entity = await RetrieveDataById(id);
            UpdateEntityWithRequestData(entity, requestData);
            _unitOfWork.ProcessedAgentDataRepository.Update(entity);
            await _unitOfWork.SaveChanges();
        }

        private async Task<ProcessedAgentData> RetrieveDataById(int id)
        {
            var data = await _unitOfWork.ProcessedAgentDataRepository.GetById(id);
            EntityNotFoundException.ThrowIfNull(data);
            return data!;
        }

        private ProcessedAgentData MapRequestModelToEntity(ProcessedAgentDataRequestModel requestData)
        {
            return new ProcessedAgentData
            {
                RoadState = requestData.RoadState,
                UserId = requestData.UserId,
                X = requestData.X,
                Y = requestData.Y,
                Z = requestData.Z,
                Longitude = requestData.Longitude,
                Latitude = requestData.Latitude,
                TimeStamp = requestData.TimeStamp,
            };
        }

        private void UpdateEntityWithRequestData(ProcessedAgentData entity, ProcessedAgentDataRequestModel requestData)
        {
            entity.RoadState = requestData.RoadState;
            entity.UserId = requestData.UserId;
            entity.X = requestData.X;
            entity.Y = requestData.Y;
            entity.Z = requestData.Z;
            entity.Longitude = requestData.Longitude;
            entity.Latitude = requestData.Latitude;
            entity.TimeStamp = requestData.TimeStamp;
        }
    }
}
