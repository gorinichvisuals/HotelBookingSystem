global using Microsoft.Extensions.DependencyInjection;
global using System.ComponentModel.DataAnnotations;
global using System.Linq.Expressions;

global using HotelBooking.Application.Services.Abstractions;
global using HotelBooking.Application.Services.Implementations;
global using HotelBooking.Application.MappingExpressions;
global using HotelBooking.Application.DTO.Auth;
global using HotelBooking.Application.DTO.Customer;
global using HotelBooking.Application.DTO.Room;
global using HotelBooking.Application.DTO.Booking;
global using HotelBooking.Application.DTO.Payment;

global using HotelBooking.Infrastructure.Repositories.UOW;
global using HotelBooking.Database.Context.Models;

global using Shared.Services.Services.Abstractions;
global using Shared.Services.AppResult;
global using Shared.Services.Constants;
global using Shared.Services.Enums.User;
global using Shared.Services.Enums.Booking;

global using Mono.PaymentService.Services.Abstractions;
global using Mono.PaymentService.DTO.Requests;
global using Mono.PaymentService.DTO.Responses;