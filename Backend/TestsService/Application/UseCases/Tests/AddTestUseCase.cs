using Application.UseCases.TestAttachmentsUseCases;
using Application.UseCases.TestChangeStatusTestUseCases;
using Application.UseCases.TestEquipmentUseCases;
using Application.UseCases.TestGenericUpdateUseCases;
using Application.UseCases.Tests.Builder;
using Application.UseCases.TestSpecificationsUseCases;
using Application.UseCases.TestTechniciansUseCases;
using AutoMapper;
using Domain.Models;
using Domain.Models.Equipments;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Tests
{
    public class AddTestUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly ITestBuilder _testBuilder;
        private readonly AsignAttachmentToTestUseCase _asignAttachmentToTestUseCase;
        private readonly AsignTechnicianToTestUseCase _asignTechnicianToTestUseCase;
        private readonly AsignSpecificationToTestUseCase _asignSpecificationToTestUseCase;
        private readonly AsignChangeStatusTestToTestUseCase _asignChangeStatusTestToTestUseCase;
        private readonly AsignGenericUpdateToTestUseCase _asignGenericUpdateToTestUseCase;
        private readonly AsignEquipmentToTestUseCase _asignEquipmentToTestUseCase;

        public AddTestUseCase(
            IMapper mapper,
            ITestRepository testRepository,
            ITestBuilder testBuilder,
            AsignAttachmentToTestUseCase asignAttachmentToTestUseCase,
            AsignTechnicianToTestUseCase asignTechnicianToTestUseCase,
            AsignSpecificationToTestUseCase asignSpecificationToTestUseCase,
            AsignChangeStatusTestToTestUseCase asignChangeStatusTestToTestUseCase,
            AsignGenericUpdateToTestUseCase asignGenericUpdateToTestUseCase,
            AsignEquipmentToTestUseCase asignEquipmentToTestUseCase)
        {
            _mapper = mapper;
            _testRepository = testRepository;
            _testBuilder = testBuilder;
            _asignAttachmentToTestUseCase = asignAttachmentToTestUseCase;
            _asignTechnicianToTestUseCase = asignTechnicianToTestUseCase;
            _asignSpecificationToTestUseCase = asignSpecificationToTestUseCase;
            _asignChangeStatusTestToTestUseCase = asignChangeStatusTestToTestUseCase;
            _asignGenericUpdateToTestUseCase = asignGenericUpdateToTestUseCase;
            _asignEquipmentToTestUseCase = asignEquipmentToTestUseCase;
        }

        public async Task<GenericResponse> Execute(TestDto testDto)
            {
                try
                {
                    // Construir el objeto TestDto utilizando el builder
                        var testBuilder = _testBuilder;

                        // Cadena de llamadas asegurando esperar cada una
                        await testBuilder.SetName(testDto.Name ?? string.Empty);
                        await testBuilder.SetDescription(testDto.Description ?? string.Empty);
                        await testBuilder.SetStart(testDto.Start);
                        await testBuilder.SetEnd(testDto.End);
                        await testBuilder.SetSamples(testDto.Samples ?? new SampleDto());
                        await testBuilder.SetSpecialInstructions(testDto.SpecialInstructions ?? string.Empty);
                        await testBuilder.SetProfile(testDto.Profile ?? new AttachmentDto());
                        await testBuilder.SetAttachments(testDto.Attachments ?? new List<AttachmentDto>());
                        await testBuilder.SetEngineer(testDto.Enginner ?? new EmployeeDto());
                        await testBuilder.SetTechnicians(testDto.Technicians ?? new List<EmployeeDto>());
                        await testBuilder.SetSpecifications(testDto.Specifications ?? new List<SpecificationDto>());
                        await testBuilder.SetEquipments(testDto.Equipments ?? new List<Equipment>());
                        await testBuilder.SetStatus(testDto.Status);
                        await testBuilder.SetChangeStatusTests(testDto.changeStatusTest ?? new List<ChangeStatusTest>());
                        await testBuilder.SetLastUpdatedMessage(testDto.LastUpdatedMessage ?? string.Empty);
                        await testBuilder.SetIdRequest(testDto.idRequest);
                        await testBuilder.SetUpdates(testDto.updates ?? new List<GenericUpdateDto>());

                        // Llamar a Build y esperar el resultado
                        var testDto1 = await testBuilder.Build();
                    var test = _mapper.Map<Test>(testDto1);
                    Test resultTestClass = await _testRepository.AddAsync(test);
                    var resultTest = _mapper.Map<TestDto>(resultTestClass);

                    // Asignación de entidades
                    await AsignAllEntitiesToTest(resultTest);

                    return new GenericResponse
                    {
                        IsSuccessful = true,
                        Message = "CreateTestRequest"
                    };
                }
                catch (Exception ex)
                {
                    return new GenericResponse
                    {
                        IsSuccessful = false,
                        Message = "Error Created Tests: " + ex.Message
                    };
                }
            }

        private async Task AsignAllEntitiesToTest(TestDto resultTest)
        {
            var assignTasks = new List<Task>
            {
                AsignAttachmentToTest(resultTest.Id, resultTest.Attachments),
                AsigntechnicianToTest(resultTest.Id, resultTest.Technicians),
                AsignChangeStatusToTest(resultTest.Id, resultTest.changeStatusTest),
                AsigGenericUpdateToTest(resultTest.Id, resultTest.updates),
                AsignEquipmentToTest(resultTest.Id, resultTest.Equipments),
                AsigSpecificationsToTest(resultTest.Id, resultTest.Specifications)
            };

            await Task.WhenAll(assignTasks);
        }

        private async Task AsignEquipmentToTest(int id, List<Equipment>? equipments)
        {
            if (equipments != null)
            {
                foreach (var equipment in equipments)
                {
                    try
                    {
                        await _asignEquipmentToTestUseCase.Execute(id, equipment.Id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error assigning equipment {equipment.Id} to test {id}: {ex.Message}");
                    }
                }
            }
        }

        public async Task AsignAttachmentToTest(int testId, List<AttachmentDto>? attachments)
        {
            if (attachments != null)
            {
            foreach (var attachment in attachments)
            {
                try
                {
                await _asignAttachmentToTestUseCase.Execute(testId, attachment.Id);
                }
                catch (Exception ex)
                {
                // Handle exception or log it
                Console.WriteLine($"Error assigning attachment {attachment.Id} to test {testId}: {ex.Message}");
                }
            }
            }
        }

        public async Task AsigntechnicianToTest(int testId, List<EmployeeDto> technicians)
        {
            foreach (var tech in technicians)
            {
            try
            {
                await _asignTechnicianToTestUseCase.Execute(testId, tech.Id);
            }
            catch (Exception ex)
            {
                // Handle exception or log it
                Console.WriteLine($"Error assigning technician {tech.Id} to test {testId}: {ex.Message}");
            }
            }
        }

        public async Task AsignChangeStatusToTest(int testId, List<ChangeStatusTest> list)
        {
            foreach (var item in list)
            {
            try
            {
                await _asignChangeStatusTestToTestUseCase.Execute(testId, item.Id);
            }
            catch (Exception ex)
            {
                // Handle exception or log it
                Console.WriteLine($"Error assigning change status {item.Id} to test {testId}: {ex.Message}");
            }
            }
        }

        public async Task AsigGenericUpdateToTest(int testId, List<GenericUpdateDto> list)
        {
            foreach (var item in list)
            {
            try
            {
                await _asignGenericUpdateToTestUseCase.Execute(testId, item.Id);
            }
            catch (Exception ex)
            {
                // Handle exception or log it
                Console.WriteLine($"Error assigning generic update {item.Id} to test {testId}: {ex.Message}");
            }
            }
        }

        public async Task AsigSpecificationsToTest(int testId, List<SpecificationDto> list)
        {
            foreach (var item in list)
            {
            try
            {
                await _asignSpecificationToTestUseCase.Execute(testId, item.Id);
            }
            catch (Exception ex)
            {
                // Handle exception or log it
                Console.WriteLine($"Error assigning specification {item.Id} to test {testId}: {ex.Message}");
            }
            }
        }
    }
}
