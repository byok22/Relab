using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.Tests
{
    public class UpdateDatesOfTestUseCase
    {
        private readonly ITestRepository _testRepository;

        public UpdateDatesOfTestUseCase(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

         public  async Task<GenericResponse> Execute(TestDto testDto){  

            var test = await _testRepository.UpdateDatesOfTest(testDto.Id, testDto.Start, testDto.End);
            {
                if(test.id > 0)
                {
                    return new GenericResponse
                    {
                        IsSuccessful = true,
                        Message = "Dates of test updated successfully",
                        Id = test.id
                    };

                }
                else{
                    return new GenericResponse
                    {
                        IsSuccessful = false,
                        Message = "Error updating dates of test"
                    };

                }

            }

         }



    }
}