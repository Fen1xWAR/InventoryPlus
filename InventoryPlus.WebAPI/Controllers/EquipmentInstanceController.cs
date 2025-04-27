using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryPlus.Domain;
using InventoryPlus.Domain.CustomModels;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPlus.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipmentInstanceController : ControllerBase
    {
        private readonly IEquipmentInstanceRepository _equipmentInstanceRepository;
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;
        private readonly IEquipmentConsumableRepository _equipmentConsumableRepository;
        private readonly IEquipmentModelRepository _equipmentModelRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICabinetRepository _cabinetRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IConsumableModelRepository _consumableModelRepository;
        private readonly IConsumableCategoryRepository _consumableCategoryRepository;


        public EquipmentInstanceController(IEquipmentInstanceRepository equipmentInstanceRepository,
            IEmployeeRepository employeeRepository, ICabinetRepository cabinetRepository,
            IBuildingRepository buildingRepository, IConsumableModelRepository consumableModelRepository,
            IConsumableCategoryRepository consumableCategoryRepository,
            IEquipmentTypeRepository equipmentTypeRepository,
            IEquipmentConsumableRepository equipmentConsumableRepository,
            IEquipmentModelRepository equipmentModelRepository)
        {
            _equipmentInstanceRepository = equipmentInstanceRepository;
            _employeeRepository = employeeRepository;
            _cabinetRepository = cabinetRepository;
            _buildingRepository = buildingRepository;
            _consumableModelRepository = consumableModelRepository;
            _consumableCategoryRepository = consumableCategoryRepository;
            _equipmentTypeRepository = equipmentTypeRepository;
            _equipmentConsumableRepository = equipmentConsumableRepository;
            _equipmentModelRepository = equipmentModelRepository;
            _equipmentInstanceRepository = equipmentInstanceRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _equipmentInstanceRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentInstance>> GetById(Guid id)
        {
            var equipmentInstance = await _equipmentInstanceRepository.GetByIdAsync(id);
            if (equipmentInstance == null) return NotFound();
            return Ok(equipmentInstance);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentFullInfo>> GetEquipmentInstanceReport(Guid id)
        {
            var equipmentInstance = await _equipmentInstanceRepository.GetByIdAsync(id); //Обьект оборудования
            var equipmentModel =
                await _equipmentModelRepository.GetByIdAsync(equipmentInstance.ModelId); //Модель оборудования
            var equipmentType = await _equipmentTypeRepository.GetByIdAsync(equipmentModel.TypeId); // Тип оборудования

            var equipmentConsumables =
                (await _equipmentModelRepository.GetConsumablesWithCategoriesAsync(equipmentModel.ModelId))
                .GroupBy(e => e.Category.Name)
                .ToDictionary(g => g.Key, g => g.AsEnumerable()); //Сгруппированные по типу модели расходников

            var responsibleEmployee = new EmployeeReportInfo();
            var cabinet = await _cabinetRepository.GetByIdAsync(equipmentInstance.CabinetId); // Информация о местоположении
            if (cabinet.ResponsibleEmployeeId != null)
            {
                var employee = await _employeeRepository.GetByIdAsync(cabinet.ResponsibleEmployeeId.Value); // Ответвенное лицо
                responsibleEmployee = new EmployeeReportInfo
                {
                    FullName = employee.FullName,
                    Position = employee.Position,
                    ContactInfo = employee.ContactInfo,
                };
            }

            var building = await _buildingRepository.GetByIdAsync(cabinet.BuildingId); 

            return new EquipmentFullInfo
            {
                Type = equipmentType.Name,
                Model = equipmentModel.Name,
                SerialNumber = equipmentInstance.SerialNumber,
                InventoryNumber = equipmentInstance.InventoryNumber,
                Consumables = equipmentConsumables,
                Status = equipmentInstance.Status,
                InstallationDate = equipmentInstance.InstallationDate,
                Cabinet = new CabinetFullInfo
                {
                    BuildingAddress = building.Address,
                    BuildingName = building.Name,
                    CabinetNumber = cabinet.Number,
                    Description = cabinet.Description,
                    ResponsibleEmployee = responsibleEmployee
                },
            };
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentInstance>> Insert(EquipmentInstanceDto equipmentInstanceDto)
        {
            var equipmentInstance = new EquipmentInstance()
            {
                CabinetId = equipmentInstanceDto.CabinetId,
                InstallationDate = equipmentInstanceDto.InstallationDate,
                InstanceId = Guid.NewGuid(),
                SerialNumber = equipmentInstanceDto.SerialNumber,
                InventoryNumber = equipmentInstanceDto.InventoryNumber,
                ModelId = equipmentInstanceDto.ModelId,
                Status = equipmentInstanceDto.Status,
            };
            await _equipmentInstanceRepository.AddAsync(equipmentInstance);
            return CreatedAtAction(nameof(GetById), new { id = equipmentInstance.InstanceId }, equipmentInstance);
        }


        [HttpPost]
        public async Task<ActionResult<EquipmentInstance>> Update(EquipmentInstance equipmentInstance)
        {
            if (!await _equipmentInstanceRepository.ExistsAsync(e => e.InstanceId.Equals(equipmentInstance.InstanceId)))
                return NotFound();
            await _equipmentInstanceRepository.UpdateAsync(equipmentInstance);
            return CreatedAtAction(nameof(GetById), new { id = equipmentInstance.InstanceId }, equipmentInstance);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsumableCategory>> DeleteById(Guid id)
        {
            var equipmentInstance = await _equipmentInstanceRepository.GetByIdAsync(id);
            if (equipmentInstance == null) return NotFound();
            await _equipmentInstanceRepository.DeleteAsync(equipmentInstance);
            return NoContent();
        }
    }
}