using AutoMapper;
using OnlineStore.Application.Dtos;
using OnlineStore.Application.Repositories.Interfaces;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }
        public async Task<CustomerDto> AddCustomerAsync(CustomerDto customerDto)
        {
            bool customerCodeExist = await CheckIfCustomerCodeExistAsync(customerDto.Code);

            if (!customerCodeExist)
            {
                var customer = mapper.Map<Customer>(customerDto);
                var addedCustomer = await customerRepository.AddCustomerAsync(customer);
                var addedCustomerDto = mapper.Map<CustomerDto>(addedCustomer);

                return addedCustomerDto;
            }
            else
            {
                throw new ApplicationException("Заказчик с таким кодом уже есть."); //TODO нормальое исключение, нормальная обработка
            }
        }

        public async Task<Guid> GetCustomerIdByUserIdAsync(string userId)
        {
            var customer = await customerRepository.GetCustomerByUserIdAsync(userId);

            return customer.Id;
        }

        private async Task<bool> CheckIfCustomerCodeExistAsync(string customerCode)
        {
            var customer = await customerRepository.GetCustomerByCodeAsync(customerCode);

            return customer != null ? true : false;
        }
    }
}
